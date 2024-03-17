using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetLink.API.Data;
using NetLink.API.DTOs;
using NetLink.API.Models;
using NetLink.API.Services;

namespace NetLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordedValuesController : ControllerBase
    {
        private readonly NetLinkDbContext _context;
        private readonly ISensorService _sensorService;

        public RecordedValuesController(NetLinkDbContext context, ISensorService sensorService)
        {
            _context = context;
            _sensorService = sensorService;
        }

        //// GET: api/RecordedValues
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<RecordedValue>>> GetRecordedValues()
        //{
        //    return await _context.RecordedValues.ToListAsync();
        //}

        //// GET: api/RecordedValues/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<RecordedValue>> GetRecordedValue(Guid id)
        //{
        //    var recordedValue = await _context.RecordedValues.FindAsync(id);

        //    if (recordedValue == null)
        //    {
        //        return NotFound();
        //    }

        //    return recordedValue;
        //}

        //// PUT: api/RecordedValues/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRecordedValue(Guid id, RecordedValue recordedValue)
        //{
        //    if (id != recordedValue.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(recordedValue).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RecordedValueExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/RecordedValues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecordedValueDTO>> PostRecordedValue(RecordedValueDTO recordedValueDTO, string sensorName, string endUserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var recordedValue = await _sensorService.AddRecordedValueAsync(recordedValueDTO, sensorName, endUserId);

            return recordedValue;
            
        }

        // DELETE: api/RecordedValues/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRecordedValue(Guid id)
        //{
        //    var recordedValue = await _context.RecordedValues.FindAsync(id);
        //    if (recordedValue == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.RecordedValues.Remove(recordedValue);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool RecordedValueExists(Guid id)
        //{
        //    return _context.RecordedValues.Any(e => e.Id == id);
        //}
    }
}
