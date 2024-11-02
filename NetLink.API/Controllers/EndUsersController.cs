using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLink.API.DTOs.Request;
using NetLink.API.DTOs.Response;
using NetLink.API.Services;
using NetLink.API.Utils;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EndUsersController(IEndUserService endUserService) : ControllerBase
{
    [HttpPost("RegisterEndUser")]
    public async Task<ActionResult> RegisterEndUserAsync(EndUserRequestDto endUserRequestDto, string devToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await endUserService.RegisterEndUserAsync(endUserRequestDto, devToken));
    }

    [HttpGet("GetEndUserById")]
    public async Task<ActionResult<EndUserResponseDto>> GetEndUserByIdAsync(string endUserId, string devToken)
    {
        return Ok(await endUserService.GetEndUserByIdAsync(endUserId, devToken));
    }

    [HttpGet("ValidateEndUser")]
    public async Task<IActionResult> ValidateEndUserAsync(string endUserId)
    {
        await endUserService.ValidateEndUserAsync(endUserId);
        return Ok();
    }

    [HttpGet("ListDevelopersEndUsers")]
    public async Task<ActionResult<List<EndUserResponseDto>>> ListDeveloperEndUsersAsync(string devToken)
    {
        return Ok(await endUserService.ListDevelopersEndUsersAsync(devToken));
    }

    [HttpGet("ListPagedDevelopersEndUsers")]
    public async Task<ActionResult<PagedEndUserResponseDto>> ListPagedDeveloperEndUsersAsync(int page, int pageSize,
        string? searchTerm = null)
    {
        var developerId = JwtClaimsHelper.GetDeveloperId(User);
        var pagedResult =
            await endUserService.ListPagedDevelopersEndUsersAsync(developerId, page, pageSize, searchTerm);
        return Ok(pagedResult);
    }

    [HttpPatch("DeactivateEndUser")]
    public async Task<IActionResult> DeactivateEndUserAsync(string endUserId)
    {
        await endUserService.DeactivateEndUserAsync(endUserId);
        return Ok();
    }

    [HttpPatch("ReactivateEndUser")]
    public async Task<IActionResult> ReactivateEndUserAsync(string endUserId)
    {
        await endUserService.ReactivateEndUserAsync(endUserId);
        return Ok();
    }

    [HttpPatch("SoftDeleteEndUser")]
    public async Task<IActionResult> SoftDeleteEndUserAsync(string endUserId)
    {
        await endUserService.SoftDeleteEndUserAsync(endUserId);
        return Ok();
    }

    [HttpPatch("RestoreEndUser")]
    public async Task<IActionResult> RestoreEndUserAsync(string endUserId)
    {
        await endUserService.RestoreEndUserAsync(endUserId);
        return Ok();
    }

    [HttpPost("AssignSensorsToEndUser")]
    public async Task<IActionResult> AssignSensorsToEndUserAsync(List<Guid> sensorIds, string endUserId)
    {
        await endUserService.AssignSensorsToEndUserAsync(sensorIds, endUserId);
        return Ok();
    }

    [HttpGet("ListEndUserSensors")]
    public async Task<ActionResult<List<SensorResponseDto>>> ListEndUserSensorsAsync(string endUserId)
    {
        return Ok(await endUserService.ListEndUserSensorsAsync(endUserId));
    }
}