using System.ComponentModel.DataAnnotations.Schema;
using Common.Model;
using Microsoft.EntityFrameworkCore;

namespace Core.Entity.Identity;

[Table("UserOrganisation")]
[PrimaryKey(nameof(UserId), nameof(OrganisationId))]
public class UserOrganisationEntity {
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid OrganisationId { get; set; }

    public OrganisationEntity Organisation { get; set; }
    public UserRole Role { get; set; }
}
