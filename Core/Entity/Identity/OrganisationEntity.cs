using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity.Interface;
using Core.Entity.Project;

namespace Core.Entity.Identity;

[Table("Organisations")]
public class OrganisationEntity : IEntityId {
    public string Name { get; set; }
    public IList<UserOrganisationEntity> Users { get; set; }

    [InverseProperty(nameof(ProjectEntity.Organisation))]
    public IList<ProjectEntity> Projects { get; set; }

    public Guid Id { get; set; }
}
