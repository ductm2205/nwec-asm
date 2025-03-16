using System;

namespace core.Models.Responses.Auth;

public class LoginResponse : IResponse
{
    public string UserJson { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;

    public DateTime Expires { get; set; }
}
