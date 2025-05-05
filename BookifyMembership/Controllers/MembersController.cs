using BookifyMembership.Models.Dtos;
using BookifyMembership.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookifyMembership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IUserService _userService;

        public MembersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/members/register
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var user = _userService.Register(request);
            if (user == null)
                return BadRequest(new { message = "Username or Email already exists." });

            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, new
            {
                message = "Registration successful",
                userId = user.UserId,
                membershipTier = user.MembershipTier
            });
        }

        // POST: api/members/login
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _userService.Login(request);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials." });

            return Ok(new
            {
                message = "Login successful",
                userId = user.UserId,
                membershipTier = user.MembershipTier
            });
        }

        // GET: api/members/{id}
        [HttpGet("Members{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound(new { message = "User not found." });

            return Ok(new
            {
                user.UserId,
                user.Username,
                user.Email,
                user.MembershipTier
            });
        }

        // PUT: api/members/{id}/upgrade
        [HttpPut("{id}/upgrade to Premium")]
        public IActionResult Upgrade(int id)
        {
            var success = _userService.UpgradeMembership(id);
            if (!success)
                return NotFound(new { message = "User not found." });

            return Ok(new { message = "Membership upgraded to Premium." });
        }
    }
}

