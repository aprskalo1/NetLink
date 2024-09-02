using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLink.API.DTOs.Request;
using NetLink.API.Services;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GroupingController(IGroupingService groupingService) : ControllerBase
{
    [HttpPost("CreateGroup")]
    public async Task<ActionResult> CreateGroupAsync(SensorGroupRequestDto groupRequestDto, string endUserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await groupingService.CreateGroupAsync(groupRequestDto, endUserId));
    }
}