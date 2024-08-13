using Microsoft.AspNetCore.Mvc;
using NetLink.API.DTOs;
using NetLink.API.Services;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EndUsersController(IEndUserService endUserService) : ControllerBase
{
    [HttpPost("AddEndUser")]
    public async Task<ActionResult> AddEndUserAsync(EndUserDto endUserDto, string devToken)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var endUserId = await endUserService.AddEndUserAsync(endUserDto, devToken);
        return Ok(endUserId);
    }

    [HttpGet("CheckIfEndUserExists")]
    public async Task<IActionResult> CheckIfUserExistsAsync(string endUserId)
    {
        var endUserExists = await endUserService.CheckIfEndUserExistsAsync(endUserId); //check if it is active
        return endUserExists ? Ok(new { exists = endUserExists }) : NotFound(new { exists = endUserExists });
    }   
}