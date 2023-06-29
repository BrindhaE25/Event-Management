using AuthAPI.Services.IService;
using Microsoft.AspNetCore.Mvc;
using AuthAPI.Models.Dtos;

namespace AuthAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthAPIController : ControllerBase
{
    private readonly IAuthService _authService;
    protected ResponseDto _responseDto;

    public AuthAPIController(IAuthService authService)
    {
        _authService = authService;
        _responseDto = new ResponseDto();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto requestDto)
    {
        var errorMessage = await _authService.Register(requestDto);

        if (!string.IsNullOrEmpty(errorMessage))
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = errorMessage;
            return BadRequest(_responseDto);

        }
        return Ok(_responseDto);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
    {
        var response = await _authService.Login(loginRequestDto);

        if (response.User == null)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Username or password is incorrect";
            return BadRequest(_responseDto);
        }

        _responseDto.Response = response;
        return Ok(_responseDto);
    }
    
    [HttpPost("AssignRole")]
    public async Task<IActionResult> Login([FromBody]RegistrationRequestDto requestDto)
    {
        var response = await _authService.AssignRole(requestDto.Email,requestDto.Role);

        if (!response)
        {
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Error encountered";
            return BadRequest(_responseDto);
        }

        _responseDto.Response = response;
        return Ok(_responseDto);
    }
    
}