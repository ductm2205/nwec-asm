using System;
using System.ComponentModel.DataAnnotations;
using business.Auth.Commands;
using business.Handlers;
using business.Services.Auth;
using core.Exceptions;
using core.Models.Responses.Auth;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using models.Auth;
using Newtonsoft.Json;

namespace business.Auth.Handlers;

public class LoginHandler : BaseHandler<LoginCommand, LoginResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    public LoginHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, ITokenService tokenService) : base(unitOfWork)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    protected override async Task<LoginResponse> HandleCommand(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username) ?? throw new EntityNotFoundException();

        if (!user.IsActive)
        {
            throw new ValidationException("Account has been disabled");
        }

        var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isCorrectPassword)
        {
            throw new ValidationException("Incorrect password");
        }

        var roles = await _userManager.GetRolesAsync(user);

        var token = _tokenService.GenerateTokenAsync(user, roles);

        return new LoginResponse
        {
            UserJson = JsonConvert.SerializeObject(user),
            Token = token,
            Expires = DateTime.Now.AddMinutes(15)
        };
    }
}
