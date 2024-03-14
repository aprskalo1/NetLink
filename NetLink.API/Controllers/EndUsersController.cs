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
    public class EndUsersController : ControllerBase
    {
        private readonly IEndUserService _endUserService;

        public EndUsersController(IEndUserService endUserService)
        {
            _endUserService = endUserService;
        }

        //// GET: api/EndUsers
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<EndUser>>> GetEndUsers()
        //{
        //    return await _context.EndUsers.ToListAsync();
        //}

        // GET: api/EndUsers/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<EndUser>> GetEndUser(string id)
        //{
        //    var endUser = await _context.EndUsers.FindAsync(id);

        //    if (endUser == null)
        //    {
        //        return NotFound();
        //    }

        //    return endUser;
        //}

        // PUT: api/EndUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEndUser(string id, EndUser endUser)
        //{
        //    if (id != endUser.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(endUser).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EndUserExists(id))
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

        // POST: api/EndUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> AddEndUserAsync(EndUserDTO endUserDTO, string devToken)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var endUserId = await _endUserService.AddEndUserAsync(endUserDTO, devToken);
            return Ok(endUserId);
        }

        //// DELETE: api/EndUsers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEndUser(string id)
        //{
        //    var endUser = await _context.EndUsers.FindAsync(id);
        //    if (endUser == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.EndUsers.Remove(endUser);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool EndUserExists(string id)
        //{
        //    return _context.EndUsers.Any(e => e.Id == id);
        //}
    }
}
