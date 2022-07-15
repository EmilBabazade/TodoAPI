using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Helpers;
using TodoAPI.Todo.Todo.DTOs;

namespace TodoAPI.Todo.Todo
{
    [Authorize]
    public class TodoController : BaseApiController
    {
        private readonly IMediator _mediator;
        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<TodoDTO>> AddTodo(AddTodoDTO addTodoDTO)
        {
            return Ok(await _mediator.Send(new AddTodoCommand(addTodoDTO)));
        }
    }
}