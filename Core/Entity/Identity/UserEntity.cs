using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity.Interface;
using Microsoft.AspNetCore.Identity;

namespace Core.Entity.Identity;

[Table("Users")]
public class UserEntity : IdentityUser<Guid>, IEntityId {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public IList<UserOrganisationEntity> Organisations { get; set; }
    public Guid Id { get; set; }
}