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
      

        public ComplaintsAppsController(ApplicationDbContext context)
        {
            _context = context;
          

        }


        // GET: api/ComplaintsApps/Get the complaints of user (for user role and it's connected with userid)
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintsApp>> GetComplaintById(int id)
        {
            var complaint = await _context.ComplaintsApp.FindAsync(id);

            if (complaint == null)
            {
                return NotFound(); // Return 404 Not Found if no complaint with the given ID is found
            }

            return Ok(complaint);
        }


        // GET: api/ComplaintsApps/5//get ALL the Complaints of all USER this method for Admin Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplaintsApp>>> GetAllComplaintsApp()
        {
            if (_context.ComplaintsApp == null)
            {
                return NotFound();
            }
            //the code wase like this before edition
            //return await _context.ComplaintsApp.ToListAsync();

            return await _context.ComplaintsApp.Where(h => h.Status =="Pending").OrderBy(h => h.UserId).ToListAsync();
        }


        [HttpPost("{Id}")]
        public async Task<IActionResult> ChangeComplaintsAppStutusApprove(int Id)
        {
      
            ComplaintsApp complaint =await _context.ComplaintsApp.FindAsync(Id);
            if (complaint == null)
            {
                return NotFound(); // Handle the case where the complaint is not found.
            }
            //var UserSessionJson = HttpContext.Session.GetString("UserObject");
            //var UserSessionObj = JsonConvert.DeserializeObject<User>(UserSessionJson);

            complaint.Status = "Approved";
            _context.Entry(complaint).State = EntityState.Modified;
            await _context.SaveChangesAsync();
                // Successful response
            
            return Ok();

            
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> ChangeComplaintsAppStutusDeny(int Id)
        {
            ComplaintsApp complaint = await _context.ComplaintsApp.FindAsync(Id);
            if (complaint == null)
            {
                return NotFound(); // Handle the case where the complaint is not found.
            }
            //var UserSessionJson = HttpContext.Session.GetString("UserObject");
            //var UserSessionObj = JsonConvert.DeserializeObject<User>(UserSessionJson);

            complaint.Status = "Denied";
            await _context.SaveChangesAsync();
            // Successful response

            return Ok();
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
        public async Task<ActionResult<ComplaintsApp>> PostComplaint(ComplaintsApp complaintsApp)
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

        [HttpPost]
        public async Task<IActionResult> CreateDemandOne(demandOne demand) {

            if (_context.ComplaintsApp == null)
            {
                return Problem("Entity set 'ApplicationDbContext.demandOne'  is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.demandOne.Add(demand);
            await _context.SaveChangesAsync();

            return Ok();
        
        }

        [HttpPost]
        public async Task<IActionResult> CreateDemandTwo(demandTwo demand)
        {

            if (_context.ComplaintsApp == null)
            {
                return Problem("Entity set 'ApplicationDbContext.demandOne'  is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.demandTwo.Add(demand);
            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> CreateDemandThree(demandTwo demand)
        {

            if (_context.ComplaintsApp == null)
            {
                return Problem("Entity set 'ApplicationDbContext.demandOne'  is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.demandTwo.Add(demand);
            await _context.SaveChangesAsync();

            return Ok();

        }


        private bool ComplaintsAppExists(int id)
        {
            return (_context.ComplaintsApp?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
