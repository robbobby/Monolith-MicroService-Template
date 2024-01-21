using Common.Model;
using Core.Entity;
using UnitApi.Repository;

namespace UnitApi.Service;

public class UnitService(UnitRepository unitRepository) {
    public IReadOnlyList<T> GetAll<T>() where T : class {
        return unitRepository.Units.GetAll<T>().ToList();
    }

    public async Task<Guid> Create(string name, Guid userId) {
        var entity = new OrganisationEntity {
            Name = name
        };

        var unit = await unitRepository.Units.CreateAsync(entity);

        var userUnit = new UserOrganisationEntity {
            UserId = userId,
            OrganisationId = unit.Id,
            Role = UserRole.Owner
        };

        await unitRepository.UserUnits.CreateAsync(userUnit);

        return entity.Id;
    }
}
