using Microsoft.AspNetCore.Mvc;
using NetLink.API.Services;
using NetLink.API.Services.Auth;

namespace NetLink.API.Controllers;

public class AuthController(IJwtTokenService jwtTokenService, IDeveloperService developerService) : Controller
{
    [HttpPost("AuthorizeClient")]
    public async Task<IActionResult> AuthorizeClient(string devToken)
    {
        await developerService.CheckIfTokenExistsAsync(devToken);
        return Ok(jwtTokenService.GenerateToken());
    }
}