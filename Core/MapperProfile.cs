using AutoMapper;

namespace Core;

public class MapperProfile : Profile { }

public class UnitDto {
    public string Name { get; set; } = null!;
    public Guid Id { get; set; }
}