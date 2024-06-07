using PublicApi.DTOs.Base;

namespace PublicApi.DTOs.Authentication;

public class ConfirmEmailRequest : BaseRequest
{
    public string userId { get; set; }
    public string code { get; set; }
}
