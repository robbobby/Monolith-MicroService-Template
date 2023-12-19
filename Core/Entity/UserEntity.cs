using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity.Interface;

namespace Core.Entity;

public class UserEntity : IEntityId {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IList<UserUnitEntity> Units { get; set; }
    public Guid Id { get; set; }
}