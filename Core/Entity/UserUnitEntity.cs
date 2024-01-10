using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core.Entity;

[Table("UserUnits")]
[PrimaryKey(nameof(UserId), nameof(UnitId))]
public class UserUnitEntity {
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid UnitId { get; set; }

    public UnitEntity Unit { get; set; }
    // public Role Role { get; set; } = Identity.Role.User;
}