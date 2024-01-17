using Core.Entity.Interface;

namespace MicroServiceTemplateApi.Repository;

public class MicroServiceTemplateEntity : IEntityId {
    public Guid Id { get; set; }
}
