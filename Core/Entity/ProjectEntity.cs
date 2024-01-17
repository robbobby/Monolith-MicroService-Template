using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity;

[Table("Projects")]
public class ProjectEntity {
    public Guid Id { get; set; }
    public string Name { get; set; }
    [ForeignKey(nameof(OrganisationId))] public OrganisationEntity Organisation { get; set; }
    public Guid OrganisationId { get; set; }
}
