using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLink.API.DTOs.Request;
using NetLink.API.Services;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DevelopersController(IDeveloperService developerService) : ControllerBase
{
    [HttpPost("AddDeveloper")]
    public async Task<ActionResult> AddDeveloperAsync(DeveloperRequestDto developerRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await developerService.AddDeveloperAsync(developerRequestDto));
    }

    [HttpGet("GetDeveloperIdFromToken")]
    public async Task<IActionResult> GetDeveloperFromTokenAsync(string token)
    {
        return Ok(await developerService.GetDeveloperIdFromTokenAsync(token));
    }

    [HttpGet("GetDeveloperById")]
    public async Task<IActionResult> GetDeveloperByIdAsync(Guid developerId)
    {
        return Ok(await developerService.GetDeveloperByIdAsync(developerId));
    }

    [HttpGet("GetDeveloperByUsername")]
    public async Task<IActionResult> GetDeveloperByUsernameAsync(string username)
    {
        return Ok(await developerService.GetDeveloperByUsernameAsync(username));
    }

    [HttpPut("UpdateDeveloper")]
    public async Task<IActionResult> UpdateDeveloperAsync(Guid developerId, DeveloperRequestDto developerRequestDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await developerService.UpdateDeveloperAsync(developerId, developerRequestDto));
    }

    [HttpPatch("DeactivateDeveloper")]
    public async Task<IActionResult> DeactivateDeveloperAsync(Guid developerId)
    {
        await developerService.DeactivateDeveloperAsync(developerId);
        return Ok();
    }

    [HttpPatch("ReactivateDeveloper")]
    public async Task<IActionResult> ReactivateDeveloperAsync(Guid developerId)
    {
        await developerService.ReactivateDeveloperAsync(developerId);
        return Ok();
    }

    [HttpPatch("SoftDeleteDeveloper")]
    public async Task<IActionResult> SoftDeleteDeveloperAsync(Guid developerId)
    {
        await developerService.SoftDeleteDeveloperAsync(developerId);
        return Ok();
    }

    [HttpPatch("RestoreDeveloper")]
    public async Task<IActionResult> RestoreDeveloperAsync(Guid developerId)
    {
        await developerService.RestoreDeveloperAsync(developerId);
        return Ok();
    }

    [HttpDelete("DeleteDeveloper")]
    public async Task<IActionResult> DeleteDeveloperAsync(Guid developerId)
    {
        await developerService.DeleteDeveloperAsync(developerId);
        return Ok();
    }

    [HttpGet("ListDevelopers")]
    public async Task<IActionResult> ListDevelopersAsync()
    {
        return Ok(await developerService.ListDevelopersAsync());
    }
}