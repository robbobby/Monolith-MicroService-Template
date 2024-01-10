using System.ComponentModel.DataAnnotations.Schema;
using Core.Entity.Interface;

namespace Core.Entity;

[Table("Units")]
public class UnitEntity : IEntityId {
    public string Name { get; set; }
    public IList<UserUnitEntity> Users { get; set; }
    public Guid Id { get; set; }
}