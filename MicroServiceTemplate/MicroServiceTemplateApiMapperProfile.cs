using AutoMapper;
using MicroServiceTemplateApi.Model;
using MicroServiceTemplateApi.Repository;

namespace MicroServiceTemplateApi;

public class MicroServiceTemplateApiMapperProfile : Profile {
    public MicroServiceTemplateApiMapperProfile() {
        CreateMap<MicroServiceTemplateEntity, MicroServiceTemplateDto>();
    }
}