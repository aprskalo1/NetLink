using Microsoft.AspNetCore.Mvc;
using NetLink.API.DTOs;
using NetLink.API.Services;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DevelopersController(IDeveloperService developerService) : ControllerBase
{
    [HttpPost("AddDeveloper")]
    public async Task<ActionResult> AddDeveloperAsync(DeveloperDto developerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var developerId = await developerService.AddTokenAsync(developerDto);
        return Ok(developerId);
    }

    [HttpGet("CheckIfTokenExists")]
    public async Task<IActionResult> CheckTokenExistsAsync(string token) 
    {
        await developerService.CheckIfTokenExistsAsync(token);
        return Ok();
    }
}