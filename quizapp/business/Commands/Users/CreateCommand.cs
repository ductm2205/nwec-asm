using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace business.Commands.Users;

public class CreateCommand : BaseCommand<bool>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<string> Roles { get; set; } = [];
}