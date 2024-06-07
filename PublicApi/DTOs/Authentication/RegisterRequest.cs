using System.ComponentModel.DataAnnotations;
using PublicApi.DTOs.Base;


namespace PublicApi.DTOs.Authentication;

public class RegisterRequest:BaseRequest
{
    [Required]
    [Display(Name = "User name")]
    public string Username { get; set; }


    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }


    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

}
