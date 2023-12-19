using MicroServiceTemplateApi.Model;
using MicroServiceTemplateApi.Repository;
using AutoMapper;

namespace MicroServiceTemplateApi;

public class MicroServiceTemplateApiMapperProfile: Profile {
    public MicroServiceTemplateApiMapperProfile() {
        CreateMap<MicroServiceTemplateEntity, MicroServiceTemplateDto>();
    }
}
