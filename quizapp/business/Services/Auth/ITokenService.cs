using models.Auth;

namespace business.Services.Auth;

public interface ITokenService
{
    string GenerateTokenAsync(User user, IList<string> roles);
}