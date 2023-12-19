using AutoMapper;
using Core.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using UnitApi.Repository;

namespace UnitApi.Service;

public class UnitService(UnitRepository unitRepository) {
    public IReadOnlyList<T> GetAll<T>() where T : class {
        return unitRepository.GetAll<T>().ToList();
    }
}