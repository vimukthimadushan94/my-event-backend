using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using my_event_backend.Dtos;
using my_event_backend.Models;
using System.Security.Claims;

namespace my_event_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpGet("app-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var userDtos = users.Select(user => new GetAllUsrsDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            }).ToList();

            return Ok(userDtos);
        }

        [HttpGet("/api/Auth/profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(new
            {
                firstName = user.FirstName,
                lastName = user.LastName,
                email = user.Email,
                profileImageUrl = user.ProfilePicturePath
            });
        }

        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> updateProfile([FromForm] UpdateProfileDto updateProfileDto)
        {
            if(string.IsNullOrEmpty(updateProfileDto.FirstName))
            {
                return BadRequest("First name is required");
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.FirstName = updateProfileDto.FirstName;
            user.LastName = updateProfileDto.LastName;

            string profilePicturePath = null;
            if(updateProfileDto.ProfilePicture != null)
            {
                var uploadsFolder = Path.Combine(Environment.CurrentDirectory, "uploads/profile-pictures");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(updateProfileDto.ProfilePicture.FileName);
                profilePicturePath = Path.Combine(uploadsFolder, uniqueFileName);

                var fileStream = new FileStream(profilePicturePath, FileMode.Create);
                await updateProfileDto.ProfilePicture.CopyToAsync(fileStream);

                user.ProfilePicturePath = $"/uploads/profile-pictures/{Path.GetFileName(profilePicturePath)}";
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new
            {
                Message = "Profile updated successfully",
                user,
                ProfilePicturePath = profilePicturePath != null ? $"/uploads/profile-pictures/{Path.GetFileName(profilePicturePath)}" : null
            });

        }
    }
}
