using Common.Model;

namespace Core;

public class OrganisationDto {
    public string Name { get; set; } = null!;
    public Guid Id { get; set; }
    public UserRole Role { get; set; }
    public IList<ProjectDto> Projects { get; set; } = [];
}

public class ProjectDto {
    public string Name { get; set; } = null!;
    public Guid Id { get; set; }
}
