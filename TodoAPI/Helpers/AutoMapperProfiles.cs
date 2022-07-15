using AutoMapper;
using TodoAPI.Todo.Todo;
using TodoAPI.Todo.Todo.DTOs;

namespace TodoAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TodoEntity, TodoDTO>()
                .ForMember(
                    dest => dest.UserId,
                    opts => opts.MapFrom(
                        src => src.User.Id)
                );
        }
    }
}
