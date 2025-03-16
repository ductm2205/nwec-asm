using System;
using System.ComponentModel.DataAnnotations;
using business.Auth.Commands;
using business.Handlers;
using business.Services.Auth;
using core.Models.Responses.Auth;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using models.Auth;
using Newtonsoft.Json;

namespace business.Auth.Handlers;

public class RegisterHandler : BaseHandler<RegisterCommand, LoginResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    public RegisterHandler(IUnitOfWork unitOfWork, UserManager<User> userManager, ITokenService tokenService) : base(unitOfWork)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    protected override async Task<LoginResponse> HandleCommand(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isExisted = await _userManager.FindByEmailAsync(request.Email) ?? await _userManager.FindByNameAsync(request.Username);

        if (isExisted != null && isExisted.IsActive && !isExisted.IsDeleted)
        {
            throw new ValidationException("Email/Username has already been taken");
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Username,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth,
            IsActive = true,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException($"Failed to create user: {errors}");
        }

        await _userManager.AddToRoleAsync(user, "User");

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
