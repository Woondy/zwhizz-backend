using Application.DTOs;
using Application.Interfaces;
using Application.Security;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IEmailService emailService) : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IEmailService _emailService = emailService;

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email!);
            if (user == null)
            {
                return new LoginResponse(false, "We couldn't find an account associated with that email address.");
            }

            bool checkPassword = Hasher.VerifyPassword(request.Password, user.PasswordHash!);
            if (checkPassword)
            {
                var accessToken = _tokenService.GenerateAccessToken(user);
                var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);

                user.LastLoginDate = DateTime.UtcNow;

                await _unitOfWork.SaveChangesAsync();

                return new LoginResponse(true, "Login successful!", accessToken, refreshToken);
            }
            else
            {
                return new LoginResponse(false, "Invalid email or password. Please try again.");
            }
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email!);
            if (user != null)
            {
                return new RegistrationResponse(false, "An account with that email address already exists.");
            }

            user = new User
            {
                UserName = request.Name,
                NormalizedUserName = request.Name.ToUpper(),
                Email = request.Email,
                PasswordHash = Hasher.HashPassword(request.Password)
            };

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new RegistrationResponse(true, "Your account has been created successfully!");
        }

        public async Task<ForgetPasswordResponse> ForgetPasswordAsync(ForgetPasswordRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return new ForgetPasswordResponse(false, "We couldn't find an email address matching that in our records. Please check the email address you entered and try again.");
            }

            var resetToken = RandomStringGenerator.GenerateRandomBase64UrlString(32);
            user.PasswordResetToken = resetToken;
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddMinutes(30);

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            await _emailService.SendPasswordResetEmail(user, resetToken);

            return new ForgetPasswordResponse(false, "A password reset link has been sent to your email address. Please follow the instructions in the email to reset your password.");
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);
            if (user == null || user.PasswordResetToken != request.ResetToken)
            {
                return new ResetPasswordResponse(false, "The password reset link you used is invalid or expired. Please request a new password reset.");
            }

            if (!request.NewPassword.Equals(request.ConfirmPassword, StringComparison.Ordinal))
            {
                return new ResetPasswordResponse(false, "The new passwords you entered don't match. Please try again.");
            }

            var hashedPassword = Hasher.HashPassword(request.NewPassword);

            user.PasswordHash = hashedPassword;
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            await _emailService.SendPasswordResetConfirmationEmail(user); 

            return new ResetPasswordResponse(false, "Your password has been reset successfully. You can now log in with your new password.");
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return new RefreshTokenResponse(false, "Missing refresh token. Please login again.");
            }

            var refreshTokenHash = Hasher.HashData(request.RefreshToken, HashAlgorithmType.SHA256);

            var storedRefreshToken = await _unitOfWork.RefreshTokenRepository.GetRefreshTokenByRefreshTokenHashAsync(refreshTokenHash);
            if (storedRefreshToken == null)
            {
                return new RefreshTokenResponse(false, "Invalid refresh token. Please login again.");
            }

            if (storedRefreshToken.Expiration < DateTime.Now)
            {
                return new RefreshTokenResponse(false, "Refresh token expired. Please login again.");
            }

            var accessToken = _tokenService.GenerateAccessToken(storedRefreshToken.User!);

            return new RefreshTokenResponse(true, "Refresh token successful!", accessToken);
        }

        public async Task<LogoutResponse> LogoutAsync(LogoutRequest request)
        {
            if (!string.IsNullOrEmpty(request.RefreshToken))
            {
                var refreshTokenHash = Hasher.HashData(request.RefreshToken, HashAlgorithmType.SHA256);

                var storedRefreshToken = await _unitOfWork.RefreshTokenRepository.GetRefreshTokenByRefreshTokenHashAsync(refreshTokenHash);
                if (storedRefreshToken != null)
                {
                    await _tokenService.DeleteRefreshTokenAsync(storedRefreshToken);
                }
            }

            return new LogoutResponse(true, "You have been successfully logged out.");
        }
    }
}
