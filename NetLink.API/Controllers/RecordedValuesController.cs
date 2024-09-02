using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLink.API.DTOs.Request;
using NetLink.API.Services;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RecordedValuesController(ISensorOperationsService sensorService) : ControllerBase
{
    [HttpPost("RecordValueBySensorName")]
    public async Task<ActionResult<RecordedValueRequestDto>> RecordValueBySensorNameAsync(RecordedValueRequestDto recordedValueRequestDto,
        string sensorName, string endUserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        return Ok(await sensorService.AddRecordedValueAsync(recordedValueRequestDto, sensorName, endUserId));
    }
}