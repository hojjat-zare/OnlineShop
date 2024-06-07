using PublicApi.DTOs.Base;

namespace PublicApi.DTOs.Authentication;

public class AuthenticateResponse:BaseResponse
{
    public bool Result { get; set; } = false;
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public bool IsLockedOut { get; set; } = false;
    public bool IsNotAllowed { get; set; } = false;
    public bool RequiresTwoFactor { get; set; } = false;
}
