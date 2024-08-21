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
        return Ok(await developerService.AddDeveloperAsync(developerDto));
    }

    [HttpGet("EnsureDevToken")]
    public async Task<IActionResult> EnsureDevToken(string token)
    {
        await developerService.EnsureDevTokenAsync(token);
        return Ok();
    }

    [HttpGet("GetDeveloperIdFromToken")]
    public async Task<IActionResult> GetDeveloperFromTokenAsync(string token)
    {
        return Ok(await developerService.GetDeveloperIdFromTokenAsync(token));
    }

    [HttpGet("GetDeveloperById")]
    public async Task<IActionResult> GetDeveloperByIdAsync(Guid id)
    {
        return Ok(await developerService.GetDeveloperByIdAsync(id));
    }

    [HttpGet("GetDeveloperByUsername")]
    public async Task<IActionResult> GetDeveloperByUsernameAsync(string username)
    {
        return Ok(await developerService.GetDeveloperByUsernameAsync(username));
    }

    [HttpPut("UpdateDeveloper")]
    public async Task<IActionResult> UpdateDeveloperAsync(Guid id, DeveloperDto developerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await developerService.UpdateDeveloperAsync(id, developerDto));
    }

    [HttpPatch("DeactivateDeveloper")]
    public async Task<IActionResult> DeactivateDeveloperAsync(Guid id)
    {
        await developerService.DeactivateDeveloperAsync(id);
        return Ok();
    }

    [HttpPatch("ReactivateDeveloper")]
    public async Task<IActionResult> ReactivateDeveloperAsync(Guid id)
    {
        await developerService.ReactivateDeveloperAsync(id);
        return Ok();
    }

    [HttpPatch("SoftDeleteDeveloper")]
    public async Task<IActionResult> SoftDeleteDeveloperAsync(Guid id)
    {
        await developerService.SoftDeleteDeveloperAsync(id);
        return Ok();
    }

    [HttpPatch("RestoreDeveloper")]
    public async Task<IActionResult> RestoreDeveloperAsync(Guid id)
    {
        await developerService.RestoreDeveloperAsync(id);
        return Ok();
    }

    [HttpDelete("DeleteDeveloper")]
    public async Task<IActionResult> DeleteDeveloperAsync(Guid id)
    {
        await developerService.DeleteDeveloperAsync(id);
        return Ok();
    }

    [HttpGet("ListDevelopers")]
    public async Task<IActionResult> ListDevelopersAsync()
    {
        return Ok(await developerService.ListDevelopersAsync());
    }
}