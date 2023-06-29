using AuthAPI.Models;

namespace AuthAPI.Services.IService;

public interface IJwtTokenGenerator
{
    public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
}