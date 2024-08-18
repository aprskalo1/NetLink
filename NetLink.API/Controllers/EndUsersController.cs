using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLink.API.Services;
using NetLink.API.Shared.DTOs;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EndUsersController(IEndUserService endUserService) : ControllerBase
{
    [HttpPost("AddEndUser")]
    public async Task<ActionResult> AddEndUserAsync(EndUserDto endUserDto, string devToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await endUserService.AddEndUserAsync(endUserDto, devToken));
    }

    //TODO: maybe not needed as API endpoint
    [HttpGet("EnsureEndUserStatus")]
    public async Task<IActionResult> EnsureEndUserExistsAsync(string endUserId)
    {
        await endUserService.EnsureEndUserStatusAsync(endUserId);
        return Ok();
    }
}