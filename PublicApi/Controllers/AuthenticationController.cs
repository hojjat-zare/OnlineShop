using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTOs.Authentication;
using Swashbuckle.AspNetCore.Annotations;

namespace PublicApi.Controllers;

[ApiController]
[Route("api/authenticate")]
public class AuthenticationController : ControllerBase
{

    private SignInManager<ApplicationUser> _signInManager;
    private ITokenClaimsService _tokenClaimsService;


    public AuthenticationController(SignInManager<ApplicationUser> signInManager, ITokenClaimsService tokenClaimsService)
    {
        _signInManager = signInManager;
        _tokenClaimsService = tokenClaimsService;
    }

    [HttpPost()]
    [SwaggerOperation(
        Summary = "Authenticates a user",
        Description = "Authenticates a user",
        OperationId = "auth.authenticate",
        Tags = new[] { "AuthEndpoints" })
    ]
    public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        var response = new AuthenticateResponse();

        // This count login failures towards account lockout
        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, request.RemmemberMe, true);

        response.Result = result.Succeeded;
        response.IsLockedOut = result.IsLockedOut;
        response.IsNotAllowed = result.IsNotAllowed; // means reqire email or phone confirmation
        response.RequiresTwoFactor = result.RequiresTwoFactor;
        response.Username = request.Username;

        if (result.Succeeded)
        {
            response.Token = await _tokenClaimsService.GetTokenAsync(request.Username);
        }

        return response;
    }
}
