using Application.Mapper.Mappings;
using AutoMapper;

namespace Application.Mapper;

public class MappingConfiguration 
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(config =>
        {
            config.AddProfile<DomainToViewModelMappingProfile>();
            config.AddProfile<DomainToDtoMappingProfile>();
        });
    }
}