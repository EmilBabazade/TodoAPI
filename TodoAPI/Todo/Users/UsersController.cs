using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Todo.Users.DTOs;

namespace TodoAPI.Todo.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            await _mediator.Send(new RegisterCommand(registerDTO));
            return Ok();
        }
    }
}
