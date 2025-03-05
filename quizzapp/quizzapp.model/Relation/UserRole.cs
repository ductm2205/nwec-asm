using quizzapp.model.Auth;

namespace quizzapp.model.Relation;

public class UserRole
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

}