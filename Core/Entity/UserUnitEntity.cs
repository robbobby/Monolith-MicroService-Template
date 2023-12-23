using Core.Entity.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core.Entity;

[PrimaryKey(nameof(UserId), nameof(UnitId))]
public class UserUnitEntity {
    public Guid UserId { get; set; }
    public UserEntity Users { get; set; }
    public Guid UnitId { get; set; }

    public UnitEntity Units { get; set; }
    // public Role Role { get; set; } = Identity.Role.User;
}