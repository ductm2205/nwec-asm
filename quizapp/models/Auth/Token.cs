using System;
using System.ComponentModel.DataAnnotations.Schema;
using models.Base;

namespace models.Auth;

[Table("refresh_tokens", Schema = "auth")]
public class Token : BaseItem, IHasId
{
    public Guid Id { get; set; }
    public required string Value { get; set; }
    public DateTime Expires { get; set; }

    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsActive => !IsUsed && !IsRevoked && !IsExpired;

    public bool IsExpired => DateTime.UtcNow >= Expires;
}
