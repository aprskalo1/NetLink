using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetLink.API.DTOs.Request;
using NetLink.API.Services;

namespace NetLink.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecordedValuesController(ISensorOperationsService sensorService) : ControllerBase
{
    [HttpPost("RecordValueBySensorName")]
    [Authorize]
    public async Task<ActionResult<RecordedValueRequestDto>> RecordValueBySensorNameAsync(RecordedValueRequestDto recordedValueRequestDto,
        string sensorName, string endUserId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        await sensorService.RecordValueByNameAsync(recordedValueRequestDto, sensorName, endUserId);
        return Ok();
    }

    [HttpPost("RecordValueBySensorId")]
    [Authorize]
    public async Task<ActionResult<RecordedValueRequestDto>> RecordValueBySensorIdAsync(RecordedValueRequestDto recordedValueRequestDto,
        Guid sensorId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        await sensorService.RecordValueByIdAsync(recordedValueRequestDto, sensorId);
        return Ok();
    }

    [HttpGet("GetRecordedValues")]
    [Authorize]
    public async Task<ActionResult<List<RecordedValueRequestDto>>> GetRecordedValuesAsync(Guid sensorId, string endUserId, int quantity,
        bool isAscending = false)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var recordedValues = await sensorService.GetRecordedValuesAsync(sensorId, endUserId, quantity, isAscending);
        return Ok(recordedValues);
    }

    [HttpPost("RecordValueRemotely")]
    public async Task<ActionResult<RecordedValueRequestDto>> RecordValueRemotelyAsync(RecordedValueRequestDto recordedValueRequestDto,
        Guid sensorId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        await sensorService.RecordValueRemotelyAsync(recordedValueRequestDto, sensorId);
        return Ok();
    }
}