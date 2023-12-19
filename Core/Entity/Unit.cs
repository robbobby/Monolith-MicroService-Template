namespace Core.Entity;

public class Unit : IEntityId {
    public string Name { get; set; }
    public ICollection<UserUnit> Users { get; set; }
    public Guid Id { get; set; }
}