using AutoMapper;
using MediatR;
using TodoAPI.Data;
using TodoAPI.Errors;
using TodoAPI.Extensions;
using TodoAPI.Todo.Todo.DTOs;

namespace TodoAPI.Todo.Todo.Handlers
{
    public class EditTodoHandler : IRequestHandler<EditTodoCommand, TodoDTO>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;

        public EditTodoHandler(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<TodoDTO> Handle(EditTodoCommand request, CancellationToken cancellationToken)
        {
            TodoEntity todo = await GetTodo(request);
            await CheckTodoBelongsToLoggedInUser(todo);
            await UpdateTodo(request.EditTodo, todo);
            return _mapper.Map<TodoDTO>(todo);
        }

        private async Task UpdateTodo(EditTodoDTO editTodoDTO, TodoEntity todo)
        {
            todo.UpdatedAt = DateTime.Now;
            todo.Done = editTodoDTO.Done;
            todo.DueDate = editTodoDTO.DueDate;
            todo.Text = editTodoDTO.Text;
            _dataContext.Update(todo);
            await _dataContext.SaveChangesAsync();
        }

        private async Task CheckTodoBelongsToLoggedInUser(TodoEntity todo)
        {
            var user = await _dataContext.Users.FindAsync(_httpContext.User.GetUserId());
            if (todo.User != user)
                throw new ForbiddenException("Todo does not belong to the current user");
        }

        private async Task<TodoEntity> GetTodo(EditTodoCommand request)
        {
            var todo = await _dataContext.Todos.FindAsync(request.Id);
            if (todo == null)
                throw new NotFoundException("Todo does not exist for given id");
            return todo;
        }
    }
}