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
using WebAPI.SwaggerExamples.Responses.Identity.ChangePassword;
using WebAPI.SwaggerExamples.Responses.Identity.Delete;
using WebAPI.SwaggerExamples.Responses.Identity.Login;
using WebAPI.SwaggerExamples.Responses.Identity.Register;
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

        /// <summary>
        /// Registers the customer in the system
        /// </summary>
        /// <response code="200">User created successfully!</response>
        /// <response code="409">User with this username already exists!</response>
        /// <response code="500">User creation failed! Please check user details and try again.</response>
        /// <param name="register"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(RegisterResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterResponseStatus409), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(RegisterResponseStatus500), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("RegisterCustomer")]
        public async Task<IActionResult> RegisterUserAsync(RegisterModel register)
        {
            var userExist = await _userManager.FindByNameAsync(register.Username!);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, new Response(false, "User with this username already exists!"));
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
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, "User creation failed! Please check user details and try again.", result.Errors.Select(e => e.Description)));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Customer.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Customer.ToString()));

            await _userManager.AddToRoleAsync(user, UserRoles.Customer.ToString());

            await _emailSenderService.Send(user.Email!, "Registration confirmation", EmailTemplate.WelcomeMessage, user);

            return Ok(new Response(true, "User created successfully!"));
        }

        /// <summary>
        /// Registers the employee in the system
        /// </summary>
        /// <response code="200">User created successfully!</response>
        /// <response code="409">User with this username already exists!</response>
        /// <response code="500">User creation failed! Please check User details and try again.</response>
        /// <param name="register"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(RegisterResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterResponseStatus409), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(RegisterResponseStatus500), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("RegisterEmployee")]
        public async Task<IActionResult> RegisterManagerAsync(RegisterModel register)
        {
            var userExist = await _userManager.FindByNameAsync(register.Username!);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, new Response(false, "User with this username already exists!"));
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
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, "User creation failed! Please check manager details and try again.", result.Errors.Select(e => e.Description)));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Employee.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Employee.ToString()));

            await _userManager.AddToRoleAsync(user, UserRoles.Employee.ToString());

            await _emailSenderService.Send(user.Email!, "Registration confirmation", EmailTemplate.WelcomeMessage, user);

            return Ok(new Response(true, "User created successfully!"));
        }

        /// <summary>
        /// Registers the admin in the system
        /// </summary>
        /// <response code="200">User created successfully!</response>
        /// <response code="409">User with this username already exists!</response>
        /// <response code="500">User creation failed! Please check user details and try again.</response>
        /// <param name="register"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(RegisterResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterResponseStatus409), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(RegisterResponseStatus500), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdminAsync(RegisterModel register)
        {
            var userExist = await _userManager.FindByNameAsync(register.Username!);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, "User with this username already exists!"));
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
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, "User creation failed! Please check admin details and try again.", result.Errors.Select(e => e.Description)));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));

            await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());

            await _emailSenderService.Send(user.Email!, "Registration confirmation", EmailTemplate.WelcomeMessage, user);

            return Ok(new Response(true, "User created successfully!"));
        }

        /// <summary>
        /// Logs the user into the system
        /// </summary>
        /// <response code="200">User logged in successfully!</response>
        /// <response code="401">Invalid credentials</response>
        /// <param name="login"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(LoginResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(LoginResponseStatus401), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel login)
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

                return Ok(new AuthSuccessResponse
                {
                    Response = new Response(true, "User logged in successfully!"),
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                });
            }
            return Unauthorized(new Response(false, "Invalid credentials!"));
        }

        /// <summary>
        /// Deletes the user from the system
        /// </summary>
        /// <response code="200">User deleted successfully!</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">User deletion failed! Please check user details and try again.</response>
        /// <param name="userId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(DeleteResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteResponseStatus404), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DeleteResponseStatus500), StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Authorize(Roles = nameof(UserRoles.Admin))]
        [Route("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new Response(false, "User not found!"));
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(false, "User deletion failed!", result.Errors.Select(e => e.Description)));
            }

            return Ok(new Response(true, "User deleted successfully!"));
        }

        /// <summary>
        /// Changes the password of the user
        /// </summary>
        /// <response code="200">Password changed successfully!</response>
        /// <response code="400">Password change failed! Please check user details and try again.</response>
        /// <param name="changePassword"></param>       
        /// <returns></returns>  
        [ProducesResponseType(typeof(ChangePasswordResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ChangePasswordResponseStatus400), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordModel changePassword)
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

            var result = await _userManager.ChangePasswordAsync(user!, changePassword.CurrentPassword!, changePassword.NewPassword!);
            if (!result.Succeeded)
            {
                return BadRequest(new Response(false, "Password change failed! Please check user details and try again.", result.Errors.Select(e => e.Description)));
            }

            return Ok(new Response(true, "Password changed successfully!"));
        }
    }
}
