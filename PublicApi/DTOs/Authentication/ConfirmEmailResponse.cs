using PublicApi.DTOs.Base;

namespace PublicApi.DTOs.Authentication;

public class ConfirmEmailResponse : BaseResponse
{
    public string Message { get; set; }
}