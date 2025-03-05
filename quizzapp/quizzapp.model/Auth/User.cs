using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using quizzapp.model.Base;
using quizzapp.model.Relation;

namespace quizzapp.model.Auth;

public class User : IdentityUser<Guid>, IEntity
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string FirstName { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string LastName { get; set; }
    [NotMapped]
    public string DisplayName => FirstName + " " + LastName;
    public DateTime DateOfBirth { get; set; }
    public string? Avatar { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    [Required]
    public required bool IsActive { get; set; } = true;
    public ICollection<UserRole>? UserRoles { get; set; } = [];
    public ICollection<UserQuiz> UserQuizzes { get; set; } = [];
}
