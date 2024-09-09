using Application.Dto.TaskItem;
using Application.Dto.TaskList;
using AutoMapper;
using Domain.Commands;

namespace Application.Mapper.Mappings;

public class DtoToDomainMappingProfile : Profile
{
    public DtoToDomainMappingProfile()
    {
        //Command
        CreateMap<CreateTaskListDto, CreateTaskListCommand>();
        CreateMap<CreatePartialTaskItemDto, CreatePartialTaskItemCommand>();
    }
}