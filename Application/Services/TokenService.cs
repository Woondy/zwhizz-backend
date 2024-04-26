using Application.Interfaces;
using Application.Security;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class TokenService(IUnitOfWork unitOfWork, IConfiguration configuration) : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;

        public string GenerateAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshTokenAsync(User user)
        {
            var refreshToken = Guid.NewGuid().ToString();
            var refreshTokenHash = Hasher.HashData(refreshToken, HashAlgorithmType.SHA256);

            var newRefreshToken = new RefreshToken
            {
                UserId = user.Id,
                RefreshTokenHash = refreshTokenHash,
                Expiration = DateTime.Now.AddDays(365)
            };

            await _unitOfWork.RefreshTokenRepository.AddAsync(newRefreshToken);
            await _unitOfWork.SaveChangesAsync();

            return refreshToken;
        }

        public async Task DeleteRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _unitOfWork.RefreshTokenRepository.DeleteAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}