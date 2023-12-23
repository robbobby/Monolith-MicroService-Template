using Microsoft.AspNetCore.Identity;

namespace Core.Entity.Identity;

public class RoleEntity : IdentityRole<Guid> {
    public string Description { get; set; }
}