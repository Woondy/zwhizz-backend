using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
        Task<ForgetPasswordResponse> ForgetPasswordAsync(ForgetPasswordRequest request);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request);
        Task<LogoutResponse> LogoutAsync(LogoutRequest request);
    }
}
