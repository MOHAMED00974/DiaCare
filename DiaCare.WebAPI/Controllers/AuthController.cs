using DiaCare.Domain.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DiaCare.Infrastructure;
using DiaCare.Application.Interfaces;
using DiaCare.Application.Helpers;



namespace DiaCare.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase // Handel Requst (No Logic(logic in service layer)
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var authResult = await _authService.RegisterAsync(dto);

            if (!authResult.IsAuthenticated)
            {
                //  Failure With Message 
                return BadRequest(Result<AuthResultDto>.Failure(authResult.Message));
            }

            //  Success with Data in AuthResultDto
            return Ok(Result<AuthResultDto>.Success(authResult, "User Registered Successfully"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var authResult = await _authService.LoginAsync(dto);

            if (!authResult.IsAuthenticated)
            {
                // Failure 401
                return Unauthorized(Result<AuthResultDto>.Failure(authResult.Message, 401));
            }

            return Ok(Result<AuthResultDto>.Success(authResult, "Logged in Successfully"));
        }

    }
}
