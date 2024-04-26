
namespace Application.DTOs
{
    public record class LoginResponse(bool Flag, string Message = null!, string AccessToken = null!, string RefreshToken = null!);
}
