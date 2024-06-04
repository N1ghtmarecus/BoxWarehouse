using Application.Interfaces;
using Domain.Enums;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSenderService _emailSenderService;

        public IdentityController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailSenderService emailSenderService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailSenderService = emailSenderService;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterModel register)
        {
            var userExist = await _userManager.FindByNameAsync(register.Username!);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, "User already exists!"));
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Username
            };
            var result = await _userManager.CreateAsync(user, register.Password!);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, "User creation failed! Please check user details and try again.", result.Errors.Select(e => e.Description)));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            await _userManager.AddToRoleAsync(user, UserRoles.User);

            await _emailSenderService.Send(user.Email!, "Registration confirmation", EmailTemplate.WelcomeMessage, user);

            return Ok(new Response(true, "User created successfully!"));
        }

        [HttpPost]
        [Route("RegisterManager")]
        public async Task<IActionResult> RegisterManager(RegisterModel register)
        {
            var userExist = await _userManager.FindByNameAsync(register.Username!);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, $"User with username {register.Username} already exists!"));
            }

            ApplicationUser user = new()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Username
            };
            var result = await _userManager.CreateAsync(user, register.Password!);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, "Manager creation failed! Please check manager details and try again.", result.Errors.Select(e => e.Description)));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));

            await _userManager.AddToRoleAsync(user, UserRoles.Manager);

            await _emailSenderService.Send(user.Email!, "Registration confirmation", EmailTemplate.WelcomeMessage, user);

            return Ok(new Response(true, "Manager created successfully!"));
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterModel register)
        {
            var userExist = await _userManager.FindByNameAsync(register.Username!);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, $"User with username {register.Username} already exists!"));
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Username
            };
            var result = await _userManager.CreateAsync(user, register.Password!);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, "Admin creation failed! Please check admin details and try again.", result.Errors.Select(e => e.Description)));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            await _emailSenderService.Send(user.Email!, "Registration confirmation", EmailTemplate.WelcomeMessage, user);

            return Ok(new Response(true, "Admin created successfully!"));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Username!);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password!))
            {
                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Name, user.UserName!),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized(new Response(false, "Invalid credentials"));
        }

        [HttpDelete]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new Response(false, "User not found"));
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, "User deletion failed", result.Errors.Select(e => e.Description)));
            }

            return Ok(new Response(true, "User deleted successfully"));
        }
    }
}
