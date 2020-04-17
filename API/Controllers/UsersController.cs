using API.Services.Interface;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IConfiguration _configuration;
        ITokenService _tokenService;

        public UsersController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserVM loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TokenVM tokenVM = new TokenVM();
                    tokenVM.Username = loginViewModel.UserName;
                    tokenVM.Email = loginViewModel.Email;
                    if (await _userManager.FindByEmailAsync(tokenVM.Email) != null)
                    {
                        TokenVM generate = await GenerateToken(tokenVM);
                        return Ok(generate);
                    }
                    return BadRequest("User not Found");
                }
                catch (Exception e)
                {
                    return BadRequest(new { message = e });
                }
            }
            else
            {
                return BadRequest("Failed");
            }
        }

        public async Task<TokenVM> GenerateToken(TokenVM tokenVM)
        {
            var user = await _userManager.FindByEmailAsync(tokenVM.Email);
            TokenVM response = _tokenService.Get(tokenVM.Email);

            var authClaim = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var acctoken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                authClaim,
                expires: DateTime.UtcNow.AddMinutes(40),
                signingCredentials: signIn);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(acctoken);
            var expirationToken = DateTime.UtcNow.AddMinutes(40).Ticks;
            var refreshToken = GenerateRefreshToken();
            var expirationRefreshToken = DateTime.UtcNow.AddMinutes(140).Ticks;
            TokenVM generate = new TokenVM
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                AccessToken = accessToken,
                ExpireToken = expirationToken,
                RefreshToken = refreshToken,
                ExpireRefreshToken = expirationRefreshToken
            };

            if (response == null)
            {
                _tokenService.Insert(new TokenVM
                {
                    Email = generate.Email,
                    AccessToken = generate.AccessToken,
                    ExpireToken = generate.ExpireToken,
                    RefreshToken = generate.RefreshToken,
                    ExpireRefreshToken = generate.ExpireRefreshToken
                });
            }
            else
            {
                _tokenService.Update(new TokenVM
                {
                    Email = generate.Email,
                    AccessToken = generate.AccessToken,
                    ExpireToken = generate.ExpireToken,
                    RefreshToken = generate.RefreshToken,
                    ExpireRefreshToken = generate.ExpireRefreshToken
                });
            }
            return generate;
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(TokenVM tokenViewModel)
        {
            try
            {
                TokenVM refToken = _tokenService.Get(tokenViewModel.Email);
                if (refToken.ExpireRefreshToken < DateTime.UtcNow.Ticks)
                {
                    return Unauthorized();
                }
                if (refToken.RefreshToken == tokenViewModel.RefreshToken)
                {
                    TokenVM generate = await GenerateToken(tokenViewModel);
                    return Ok(generate);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out");
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User { };
                    user.Id = Guid.NewGuid().ToString();
                    user.FirstName = userVM.FirstName;
                    user.LastName = userVM.LastName;
                    user.Email = userVM.Email;
                    user.Password = userVM.Password;
                    user.UserName = userVM.UserName;
                    user.CreateDate = DateTime.Now;

                    if (userVM.Role != null)
                    {
                        if (userVM.Role.ToLower() == "admin" || userVM.Role.ToLower() == "teacher")
                        {

                            var role = await _roleManager.RoleExistsAsync(userVM.Role);
                            var check = await _roleManager.FindByNameAsync(userVM.Role);

                            //var result = await _userService.Register(userVM);
                            if (role)
                            {
                                var result = await _userManager.CreateAsync(user, userVM.Password);
                                if (result.Succeeded)
                                {
                                    await _userManager.AddToRoleAsync(user, userVM.Role);
                                }
                                return Ok("Register succes");
                            }
                            else
                            {
                                //var roles = _roleManager.Roles.ToList();
                                var userrole = new IdentityRole(userVM.Role);
                                var result = await _roleManager.CreateAsync(userrole);
                                if (result.Succeeded)
                                {
                                    await _userManager.CreateAsync(user, userVM.Password);
                                    await _userManager.AddToRoleAsync(user, userVM.Role);
                                }
                                return Ok("Register succes");
                            }
                        }
                        return BadRequest("Role Not Found");
                    }
                    else
                    {
                        var role = await _roleManager.RoleExistsAsync("Teacher");
                        if (role)
                        {
                            await _userManager.CreateAsync(user, userVM.Password);
                            await _userManager.AddToRoleAsync(user, "Teacher");
                            return Ok("Register succes");
                        }
                        else
                        {
                            var userrole = new IdentityRole("Teacher");
                            var result = await _roleManager.CreateAsync(userrole);
                            if (result.Succeeded)
                            {
                                await _userManager.CreateAsync(user, userVM.Password);
                                await _userManager.AddToRoleAsync(user, "Teacher");
                            }
                            return Ok("Register succes");
                        }
                    }
                }
                catch (Exception) { }
            }

            return BadRequest(ModelState);
        }
    }
}