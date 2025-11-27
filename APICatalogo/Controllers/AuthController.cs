using APICatalogo.DTOs;
using APICatalogo.DTOs.Auth;
using APICatalogo.Models;
using APICatalogo.Services;
using APICatalogo.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APICatalogo.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginrequest)
        {
            var service = await _authService.Login(loginrequest);
            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var service = await _authService.Register(registerRequestDTO);
            return service.Valid ? StatusCode(201, service) : BadRequest(service);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var service = await _authService.RefreshToken(refreshTokenRequest);
            return service.Valid ? Ok(service) : BadRequest(service);
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var service = await _authService.Revoke(username);
            return service.Valid ? Ok(service) : BadRequest(service);
        }
    }
}
