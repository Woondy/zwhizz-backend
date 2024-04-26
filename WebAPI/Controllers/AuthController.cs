using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("forget-password")]
        public async Task<ActionResult<ForgetPasswordResponse>> ForgetPasswordAsync(ForgetPasswordRequest request)
        {
            var result = await _authService.ForgetPasswordAsync(request);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ResetPasswordResponse>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var result = await _authService.ResetPasswordAsync(request);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<LogoutResponse>> LogoutAsync(LogoutRequest request)
        {
            var result = await _authService.LogoutAsync(request);
            return Ok(result);
        }
    }
}
