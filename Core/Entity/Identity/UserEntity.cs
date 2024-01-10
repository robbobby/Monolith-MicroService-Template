using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity.Interface;
using Microsoft.AspNetCore.Identity;

namespace Core.Entity.Identity;

public class UserEntity : IdentityUser<Guid>, IEntityId {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public IList<UserUnitEntity> Units { get; set; }
    public Guid Id { get; set; }
}

public class UserToken : IdentityUserToken<Guid> {
    [Key] public Guid Id { get; set; }
}