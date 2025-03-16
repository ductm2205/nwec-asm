using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using models.Auth;

namespace business.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _configuration = configuration;
    }

    public string GenerateTokenAsync(User user, IList<string> roles)
    {
        var claims = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        System.Console.WriteLine("Security key: {0}",_configuration["JWT:Secret"]);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? "DefaultSecretKeyWithAtLeast32Characters"));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expire = DateTime.Now.AddMinutes(15);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            signingCredentials: credentials,
            expires: expire
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
