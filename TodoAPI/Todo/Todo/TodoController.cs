using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Helpers;
using TodoAPI.Todo.Todo.DTOs;

namespace TodoAPI.Todo.Todo
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<TodoDTO>> AddTodo([FromBody] AddTodoDTO addTodoDTO)
        {
            return Ok(await _mediator.Send(new AddTodoCommand(addTodoDTO)));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDTO>>> GetTodos(DateTime? dueDate, string? order)
        {
            return Ok(await _mediator.Send(new GetTodosQueries(dueDate, order)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await _mediator.Send(new DeleteTodoCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<TodoDTO>> EditTodo(int id, [FromBody] EditTodoDTO editTodoDTO)
        {
            return Ok(await _mediator.Send(new EditTodoCommand(id, editTodoDTO)));
        }
    }
}