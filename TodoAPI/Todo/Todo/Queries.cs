using MediatR;
using TodoAPI.Todo.Todo.DTOs;

namespace TodoAPI.Todo.Todo
{
    public record GetTodosQuery(DateTime? Date, string Order = "asc") : IRequest<IEnumerable<TodoDTO>>;
    public record GetTodoQuery(int Id) : IRequest<TodoDTO>;
}