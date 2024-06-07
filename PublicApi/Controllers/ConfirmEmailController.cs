using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTOs.Authentication;
using Swashbuckle.AspNetCore.Annotations;

namespace PublicApi.Controllers;

[ApiController]
[Route("api/confirm-email")]
public class ConfirmEmailController : ControllerBase
{


    private readonly UserManager<ApplicationUser> _userManager;

    public ConfirmEmailController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet()]
    [SwaggerOperation(
        Summary = "Confirm Email",
        Description = "Confirm Email",
        OperationId = "ConfirmEmail",
        Tags = new[] { "ConfirmEmailEndpoints" }
    )]
    public async Task<ActionResult<ConfirmEmailResponse>> ConfirmEmail(
        [FromQuery] ConfirmEmailRequest request,
        CancellationToken cancellationToken = default
    )
    {
        if (request.userId == null || request.code == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(request.userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{request.userId}'.");
        }

        var result = await _userManager.ConfirmEmailAsync(user, request.code);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                $"Error confirming email for user with ID '{request.userId}':"
            );
        }

        return new ConfirmEmailResponse() { Message = "your email confirmed successfully" };
    }
}
