using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity.Interface;

namespace Core.Entity;

[Table("Projects")]
public class ProjectEntity : IEntityId {
    public string Name { get; set; }
    [ForeignKey(nameof(OrganisationId))] public OrganisationEntity Organisation { get; init; } = null!;
    public Guid OrganisationId { get; set; }
    public Guid Id { get; set; }
}
