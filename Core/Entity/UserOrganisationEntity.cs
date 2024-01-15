using System.ComponentModel.DataAnnotations.Schema;
using Common.Model;
using Core.Entity.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core.Entity;

[Table("UserOrganisation")]
[PrimaryKey(nameof(UserId), nameof(OrganisationId))]
public class UserOrganisationEntity {
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid OrganisationId { get; set; }

    public OrganisationEntity Organisation { get; set; }
    public UserRole Role { get; set; }
}