using Core.Entity;
using UnitApi.Repository;

namespace UnitApi.Service;

public class UnitService(UnitRepository unitRepository) {
    public IReadOnlyList<T> GetAll<T>() where T : class {
        return unitRepository.Units.GetAll<T>().ToList();
    }

    public async Task<Guid> Create(string name, Guid userId) {
        var entity = new UnitEntity {
            Name = name
        };

        var unit = await unitRepository.Units.AddAsync(entity);

        Console.WriteLine(unit.Id);
        var userUnit = new UserUnitEntity {
            UserId = userId,
            UnitId = unit.Id
        };

        Console.WriteLine();
        await unitRepository.UserUnits.AddAsync(userUnit);

        return entity.Id;
    }
}