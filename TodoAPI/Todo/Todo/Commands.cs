using MediatR;
using TodoAPI.Todo.Todo.DTOs;

namespace TodoAPI.Todo.Todo
{
    public record AddTodoCommand(AddTodoDTO AddTodoDTO) : IRequest<TodoDTO>;
}