using Microsoft.AspNetCore.Mvc;
using NetLink.API.Services;
using NetLink.API.Services.Auth;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IJwtTokenService jwtTokenService, IDeveloperService developerService, IFirebaseService firebaseService) : Controller
{
    [HttpPost("AuthorizeClient")]
    public async Task<IActionResult> AuthorizeClient(string devToken)
    {
        await developerService.ValidateDeveloperAsync(devToken);
        return Ok(jwtTokenService.GenerateToken());
    }

    [HttpPost("AuthorizeFirebaseClient")]
    public async Task<IActionResult> AuthorizeFirebaseClient(string firebaseToken)
    {
        return Ok(await firebaseService.AuthoriseFirebaseClient(firebaseToken));
    }

    [HttpGet("ValidateDeveloper")]
    public async Task<IActionResult> ValidateDeveloper(string devToken)
    {
        await developerService.ValidateDeveloperAsync(devToken);
        return Ok();
    }
}