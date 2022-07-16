using MediatR;
using TodoAPI.Todo.Todo.DTOs;

namespace TodoAPI.Todo.Todo
{
    public record AddTodoCommand(AddTodoDTO AddTodoDTO) : IRequest<TodoDTO>;
    public record DeleteTodoCommand(int Id) : IRequest<Unit>;
    public record EditTodoCommand(int Id, EditTodoDTO EditTodo) : IRequest<TodoDTO>;
}