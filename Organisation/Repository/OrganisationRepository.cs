using Core.Entity.Identity;
using Core.RepositoryBase;

namespace OrganisationApi.Repository;

public class OrganisationRepository(RepositoryWithEntityId<OrganisationEntity> organisations,
                            RepositoryBase<UserOrganisationEntity> userOrganisations) {
    public RepositoryWithEntityId<OrganisationEntity> Organisations { get; } = organisations;
    public RepositoryBase<UserOrganisationEntity> UserOrganisations { get; } = userOrganisations;
}
