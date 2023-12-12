using Common;
using IAM.DTOs;
using IAM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace IAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration,
                                RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _config = configuration;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                if (!user.IsActive)
                    return new JsonResult(ApiResponse<string>.Error("User is disabled"))
                    {
                        StatusCode = (int)HttpStatusCode.Forbidden
                    };

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = GetToken(claims);

                return Ok(ApiResponse<string>.Success(new JwtSecurityTokenHandler().WriteToken(token)));
            }

            return Unauthorized(ApiResponse<string>.Error("Invalid username or password"));
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return Conflict(ApiResponse<string>.Error("User already exists"));

            ApplicationUser user = new()
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                IsActive = model.IsActive,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (model.RoleId == 1)
                    await _userManager.AddToRoleAsync(user, "Admin");
                else if (model.RoleId == 2)
                    await _userManager.AddToRoleAsync(user, "User");
                else
                    await _userManager.AddToRoleAsync(user, "Student");

                return Ok(ApiResponse<string>.Success());

            }
            else
            {
                return BadRequest(ApiResponse<string>.Error());
            }
        }

        [Authorize]
        [HttpPost]
        [Route("CreateRole")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> CreateRole(RoleDto model)
        {
            var appRole = new ApplicationRole { Name = model.Role, IsActive = true };
            var result = await _roleManager.CreateAsync(appRole);

            if (result.Succeeded)
            {
                return Ok(ApiResponse<string>.Success());
            }
            else
                return BadRequest(ApiResponse<string>.Error());
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(12),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
