using AutoMapper;
using Core.Entity;
using Core.Entity.Identity;
using Core.RepositoryBase;

namespace UnitApi.Repository;

public class UnitRepository(RepositoryWithEntityId<UnitEntity> units, RepositoryBase<UserUnitEntity> userUnits) {
    public RepositoryWithEntityId<UnitEntity> Units { get; } = units;
    public RepositoryBase<UserUnitEntity> UserUnits { get; } = userUnits;
}
