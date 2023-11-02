using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Test_Web_API.Data;
using Test_Web_API.Models;

namespace Test_Web_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser( User user)
        {
            // Check if the email is already registered
            var existingUser = _context.User.SingleOrDefault(u => u.Email == user.Email);
           // var existingAdmin = _context.Admin.SingleOrDefault(u => u.Email == user.Email);
            if (existingUser != null )
            {
                return Conflict(new { message = "Email already registered!" });
            }



            // using ASP.NET Core's built-in PasswordHasher
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);



            _context.User.Add(user);
            await _context.SaveChangesAsync();



            return Ok(user);
        }


        [HttpPost]
        public  IActionResult Login (UserLogin userLog)
        {

            // Find the user by Email
            var user = _context.User.SingleOrDefault(u => u.Email == userLog.Email);
            //var admin = _context.Admin.SingleOrDefault(u => u.Email == userLog.Email);


            // Check if the user exists
            if (user == null )
            {
                return Unauthorized(new { message = "Email Not Found" });
            }



            var passwordHasher = new PasswordHasher<User>();
            var Userresult = passwordHasher.VerifyHashedPassword(user, user.Password, userLog.Password);
            //var Adminresult = passwordHasher.VerifyHashedPassword(admin, admin.Password, userLog.Password);


            if (Userresult == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Invalid Email or password" });

            }

           
            return Ok( user );
            

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Info(int Id)
        {
            User userInfo =await _context.User.FindAsync(Id);

            if (userInfo == null)
            {
                return NotFound(); // Handle the case where the complaint is not found.
            }


            return Ok(userInfo);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateUser(int Id,User user) {
            if (Id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
