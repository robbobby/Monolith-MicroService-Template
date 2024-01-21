using Core.Entity;
using Core.Entity.Identity;
using Core.RepositoryBase;

namespace UnitApi.Repository;

public class UnitRepository(RepositoryWithEntityId<OrganisationEntity> units,
                            RepositoryBase<UserOrganisationEntity> userUnits) {
    public RepositoryWithEntityId<OrganisationEntity> Units { get; } = units;
    public RepositoryBase<UserOrganisationEntity> UserUnits { get; } = userUnits;
}
