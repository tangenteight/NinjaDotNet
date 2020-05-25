using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NinjaDotNet.Api.Contracts;
using NinjaDotNet.Api.DTOs;

namespace NinjaDotNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggerService _logger;
        private readonly IConfiguration _config;
        public UsersController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILoggerService logger, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _config = config;
        }

        /// <summary>
        /// Register an account
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserSignInDto registerDto)
        {
            try
            {
                var userName = registerDto.EmailAddress;
                var password = registerDto.Password;

                var newUser = new IdentityUser
                {
                    Email = userName,
                    UserName = userName.Substring(0,userName.IndexOf("@"))
                };
                var result = await _userManager.CreateAsync(newUser, password);
                if (!result.Succeeded)
                {
                    string errorMessage = "";
                    foreach (var error in result.Errors)
                    {
                        errorMessage += $"\r\n{error}";
                        return BadRequest(errorMessage);
                    }
                }
                return Ok(new { result.Succeeded });
                
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// User Login, request some access.
        /// </summary>
        /// <param name="userDto">{EmailAddress: 'string', Password: 'string' }</param>
        /// <returns></returns>
        [Route("Login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserSignInDto userDto)
        {
            try
            {
                var userName = userDto.EmailAddress;
                var password = userDto.Password;
                var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(userName);
                    var tokenString = await GenerateJsonWebToken(user);
                    return Ok(new { token = tokenString });
                }
                else
                {
                    return Unauthorized("Invalid Username or Password");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                throw;
            }
        }

        private async Task<string> GenerateJsonWebToken(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType,r)));
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                null,
                expires: DateTime.Now.AddDays(35),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
