using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLink.API.Services;
using NetLink.API.Shared.DTOs;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RecordedValuesController(ISensorOperationsService sensorService) : ControllerBase
{
    [HttpPost("RecordValueBySensorName")]
    public async Task<ActionResult<RecordedValueDto>> RecordValueBySensorNameAsync(RecordedValueDto recordedValueDto,
        string sensorName, string endUserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await sensorService.AddRecordedValueAsync(recordedValueDto, sensorName, endUserId));
    }
}