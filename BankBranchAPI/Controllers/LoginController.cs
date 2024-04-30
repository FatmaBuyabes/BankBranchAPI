using BankBranchAPI.Models;
using Microsoft.AspNetCore.Mvc;
using static BankBranchAPI.Controllers.BankController;

namespace BankBranchAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly BankContext _context;

        private readonly AuthService service;

        public LoginController(AuthService service, BankContext context ) 
        {
            this.service = service;
            _context = context;
        }


        [HttpPost]
        public IActionResult Login(UserLoginRequest loginDetails)
        {
            var response = this.service.GenerateToken(loginDetails.Username,loginDetails.Password);
            if (response.IsValid)
            {
                return Ok(new { token = response.Token });
            }
            else
            {
                return BadRequest("Username and/or Password is wrong");
            }

        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserAccounts userRegistration)
        {
            bool isAdmin = false;
            if (_context.Users.Count() == 0)
            {
                isAdmin = true;
            }

            var newAccount = UserAccounts.Create(userRegistration.username, userRegistration.password, isAdmin);

            _context.Users.Add(newAccount);
            
            _context.SaveChanges();

            return Ok(new { Message = "User Created" });


        }

   

        
        
    }
}
