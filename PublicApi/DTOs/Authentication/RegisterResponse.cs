using PublicApi.DTOs.Base;

namespace PublicApi.DTOs.Authentication;

public class RegisterResponse:BaseResponse
{
    public bool IsRegistered { get; set; }
}