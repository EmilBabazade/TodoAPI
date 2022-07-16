using MediatR;
using TodoAPI.Todo.Todo.DTOs;

namespace TodoAPI.Todo.Todo
{
    public record GetTodosQueries(DateTime? Date, string Order = "asc") : IRequest<IEnumerable<TodoDTO>>;
}