using AutoMapper;
using Core.RepositoryBase;

namespace MicroServiceTemplateApi.Repository;

public class MicroServiceTemplateRepository(MicroServiceTemplateApiDbContext dbContext, IMapper mapper)
    : RepositoryWithEntityId<MicroServiceTemplateEntity>(dbContext, mapper);
