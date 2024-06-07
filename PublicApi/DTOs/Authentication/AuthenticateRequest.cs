using System.ComponentModel.DataAnnotations;
using PublicApi.DTOs.Base;


namespace PublicApi.DTOs.Authentication;

public class AuthenticateRequest:BaseRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }


    public bool RemmemberMe { get; set; }
}
