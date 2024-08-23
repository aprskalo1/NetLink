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
        await developerService.EnsureDevTokenAsync(devToken);
        return Ok(jwtTokenService.GenerateToken());
    }

    [HttpGet("EnsureDevToken")]
    public async Task<IActionResult> EnsureDevToken(string token)
    {
        await developerService.EnsureDevTokenAsync(token);
        return Ok();
    }
}