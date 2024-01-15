namespace Common.Model;

public class UserOrganisation {
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public UserRole Role { get; set; }
}