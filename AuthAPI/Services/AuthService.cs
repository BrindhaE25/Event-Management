using AuthAPI.Data;
using AuthAPI.Models;
using AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;
using AuthAPI.Models.Dtos;

namespace AuthAPI.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly UserManager<ApplicationUser> _applicationUser;
    private readonly RoleManager<IdentityRole> _role;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(AppDbContext db, UserManager<ApplicationUser> applicationUser, RoleManager<IdentityRole> role,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _db = db;
        _applicationUser = applicationUser;
        _role = role;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> Register(RegistrationRequestDto registerRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registerRequestDto.Email,
            Email = registerRequestDto.Email,
            NormalizedEmail = registerRequestDto.Email.ToUpper(),
            PhoneNumber = registerRequestDto.PhoneNumber,
            Name = registerRequestDto.Name
        };
        try
        {
            var result = await _applicationUser.CreateAsync(user, registerRequestDto.Password);
            if (result.Succeeded)
            {
                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }
        }
        catch (Exception e)
        {
            return "Error occured";
        }
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

        bool isValid = await _applicationUser.CheckPasswordAsync(user, loginRequestDto.Password);

        if (user == null || !isValid)
        {
            return new LoginResponseDto() { User = null, Token = "" };
        }

        var roles = await _applicationUser.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user,roles);

        UserDto userDto = new()
        {
            Name = user.UserName,
            Email = user.Email,
            ID = user.Id,
            PhoneNumber = user.PhoneNumber
        };

        LoginResponseDto loginResponseDto = new()
        {
            User = userDto,
            Token = token
        };
        return loginResponseDto;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

        if (user != null)
        {
            if (!_role.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                _role.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }

            await _applicationUser.AddToRoleAsync(user, roleName.ToLower());
            return true;
        }

        return false;
    }
}