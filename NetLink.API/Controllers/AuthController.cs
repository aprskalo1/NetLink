using Microsoft.AspNetCore.Mvc;
using NetLink.API.Services;
using NetLink.API.Services.Auth;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IJwtTokenService jwtTokenService, IDeveloperService developerService) : Controller
{
    [HttpPost("AuthorizeClient")]
    public async Task<IActionResult> AuthorizeClient(string devToken)
    {
        await developerService.ValidateDeveloperAsync(devToken);
        return Ok(jwtTokenService.GenerateToken());
    }

    [HttpGet("ValidateDeveloper")]
    public async Task<IActionResult> ValidateDeveloper(string token)
    {
        await developerService.ValidateDeveloperAsync(token);
        return Ok();
    }
}