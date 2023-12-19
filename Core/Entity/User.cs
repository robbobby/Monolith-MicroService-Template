namespace Core.Entity;

public class User : IEntityId {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IList<UserUnit> Units { get; set; }
    public Guid Id { get; set; }
}