using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core;

[PrimaryKey(nameof(UserId), nameof(UnitId))]
public class UserUnit {
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid UnitId { get; set; }
    public Unit Unit { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
}