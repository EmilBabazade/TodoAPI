using MediatR;
using TodoAPI.Todo.Users.DTOs;

namespace TodoAPI.Todo.Users
{
    public record RegisterCommand(RegisterDTO RegisterDTO) : IRequest<Unit>;
    public record LoginCommand(LoginDTO LoginDTO) : IRequest<UserDTO>;
}
