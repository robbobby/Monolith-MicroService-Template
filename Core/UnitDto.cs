using Common.Model;

namespace Core;

public class UnitDto {
    public string Name { get; set; } = null!;
    public Guid Id { get; set; }
    public UserRole Role { get; set; }
}
