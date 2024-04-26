
namespace Application.DTOs
{
    public record class RefreshTokenResponse(bool Flag, string Message = null!, string AccessToken = null!);
}
