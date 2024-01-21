using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Entity.Identity;

[PrimaryKey(nameof(AccessToken), nameof(RefreshToken))]
public class TokenEntity {
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))] public UserEntity User { get; set; } = null!;

    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string ExpiresAt { get; set; }
}
