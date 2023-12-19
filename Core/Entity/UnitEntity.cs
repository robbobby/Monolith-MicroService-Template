using Core.Entity.Interface;

namespace Core.Entity;

public class UnitEntity : IEntityId {
    public string Name { get; set; }
    public IList<UserUnitEntity> Users { get; set; }
    public Guid Id { get; set; }
}