using System.Text.Encodings.Web;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTOs.Authentication;
using Swashbuckle.AspNetCore.Annotations;

namespace PublicApi.Controllers;

[ApiController]
[Route("api/register")]
public class RegisterController : ControllerBase
{
    private UserManager<ApplicationUser> _userManager { get; set; }
    public IEmailSender _emailSender { get; set; }

    public RegisterController(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [HttpPost()]
    [SwaggerOperation(
        Summary = "Register a user",
        Description = "Register a user",
        OperationId = "Register",
        Tags = new[] { "RegisterEndpoints" }
    )]
    public async Task<ActionResult<RegisterResponse>> Rgister(
        RegisterRequest Input,
        CancellationToken cancellationToken = default
    )
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser { UserName = Input.Username, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                //_logger.LogInformation("User created a new account with password.");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string? callbackUrl = Url.Action(
                    nameof(ConfirmEmailController.ConfirmEmail).Replace("Async",""),
                    typeof(ConfirmEmailController).Name.Replace("Controller",""),
                    new { userId = user.Id, code = code },
                    Url.ActionContext.HttpContext.Request.Scheme
                );

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                );

                //await _signInManager.SignInAsync(user, isPersistent: false);
                return new RegisterResponse() { IsRegistered = true };
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        // If we got this far, something failed, redisplay form
        return BadRequest(ModelState);
    }
}
