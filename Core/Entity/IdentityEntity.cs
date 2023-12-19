using Core.Entity.Interface;

namespace Core.Entity;

public class IdentityEntity : IEntityId {
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}