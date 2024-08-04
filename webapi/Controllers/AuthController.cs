using Business_Logic_Layer.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Business_Logic_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace webapi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _authenticationService.Authenticate(loginDto);

            if (token == null)
            {
                return Unauthorized("Incorrect E-mail or password");
            }

            return Ok(token);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {

            var result = await _authenticationService.Register(registerDto);

            if (!result)
            {
                return BadRequest("User registration failed. Username may already be taken.");
            }

            return Ok("User registered successfully.");
        }
    }
}
