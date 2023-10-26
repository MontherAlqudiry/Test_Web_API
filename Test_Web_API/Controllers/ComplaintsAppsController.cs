using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Web_API.Data;
using Test_Web_API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Test_Web_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ComplaintsAppsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ComplaintsAppsController> logger;

        public ComplaintsAppsController(ApplicationDbContext context , ILogger<ComplaintsAppsController> _logger)
        {
            _context = context;
            _logger = logger;
           
        }
        

        // GET: api/ComplaintsApps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplaintsApp>>> GetComplaintsApp(string userId)
        {
          if (_context.ComplaintsApp == null)
          {
              return NotFound();
          }
            //the code wase like this before edition
            //return await _context.ComplaintsApp.ToListAsync();

            return await _context.ComplaintsApp.Where(h => h.UserId.ToString() == userId).ToListAsync();
           
        }

        // GET: api/ComplaintsApps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintsApp>> GetComplaintsApp(int id)
        {
          if (_context.ComplaintsApp == null)
          {
              return NotFound();
          }
            var complaintsApp = await _context.ComplaintsApp.FindAsync(id);

            if (complaintsApp == null)
            {
                return NotFound();
            }

            return complaintsApp;
        }

        // PUT: api/ComplaintsApps/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplaintsApp(int id, ComplaintsApp complaintsApp)
        {
            if (id != complaintsApp.Id)
            {
                return BadRequest();
            }

            _context.Entry(complaintsApp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplaintsAppExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ComplaintsApps
        [HttpPost]
        public async Task<ActionResult<ComplaintsApp>> PostComplaint([FromBody]ComplaintsApp complaintsApp)
        {
          if (_context.ComplaintsApp == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ComplaintsApp'  is null.");
          }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.ComplaintsApp.Add(complaintsApp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComplaintsApp", new { id = complaintsApp.Id }, complaintsApp);
        }

        // DELETE: api/ComplaintsApps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaintsApp(int id)
        {
            if (_context.ComplaintsApp == null)
            {
                return NotFound();
            }
            var complaintsApp = await _context.ComplaintsApp.FindAsync(id);
            if (complaintsApp == null)
            {
                return NotFound();
            }

            _context.ComplaintsApp.Remove(complaintsApp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComplaintsAppExists(int id)
        {
            return (_context.ComplaintsApp?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
