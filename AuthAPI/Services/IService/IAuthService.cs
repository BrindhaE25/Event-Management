using AuthAPI.Models;
using AuthAPI.Models.Dtos;

namespace AuthAPI.Services.IService;

public interface IAuthService
{
    Task<string> Register(RegistrationRequestDto registerRequestDto);
    Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    Task<bool> AssignRole(string email, string roleName);
}