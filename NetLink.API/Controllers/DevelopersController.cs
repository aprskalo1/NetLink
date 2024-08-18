using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLink.API.Services;
using NetLink.API.Shared.DTOs;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DevelopersController(IDeveloperService developerService) : ControllerBase
{
    [HttpPost("AddDeveloper")]
    public async Task<ActionResult> AddDeveloperAsync(DeveloperDto developerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await developerService.AddTokenAsync(developerDto));
    }

    [HttpGet("CheckIfDevTokenExists")] 
    public async Task<IActionResult> CheckIfDevTokenExistsAsync(string token)
    {
        await developerService.CheckIfDevTokenExistsAsync(token);
        return Ok();
    }
    
    [HttpGet("GetDeveloperIdFromToken")]
    public async Task<IActionResult> GetDeveloperFromTokenAsync(string token)
    {
        return Ok(await developerService.GetDeveloperIdFromTokenAsync(token));
    }
}