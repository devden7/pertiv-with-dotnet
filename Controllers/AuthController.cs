using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pertiv_be_with_dotnet.Dtos;
using Pertiv_be_with_dotnet.Services;

namespace Pertiv_be_with_dotnet.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private static AuthService _authService { get; set; }
        private readonly IConfiguration _configuration;

        public AuthController(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthDto dto)
        {
            try
            {
               var tokenHandler = new JwtSecurityTokenHandler();
               var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);


               var response =  _authService.LoginAccount(dto.Email, dto.Password);


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, response.Name),
                        new Claim(ClaimTypes.Email, response.Email),
                        new Claim(ClaimTypes.Role, response.Role.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                } ;

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);


                return Ok(new
                {
                    success = 201,
                    message = "Login successfully",
                    data = new { 
                        name = response.Name, 
                        email = response.Email, 
                        role = response.Role.ToString(), 
                        token = tokenString }
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    statusCode = 400,
                    message = ex.Message,
                });
            }
        }
    }
}
