namespace Common.Apis.Project;

public class ProjectCreateRequest {
    public Guid? OrganisationId { get; set; }

    public string Name { get; set; }
    // public string Description { get; set; }
}
