using MediatR;
using TodoAPI.Data;
using TodoAPI.Errors;
using TodoAPI.Extensions;

namespace TodoAPI.Todo.Todo.Handlers
{
    public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, Unit>
    {
        private readonly DataContext _dataContext;
        private readonly HttpContext _httpContext;
        public DeleteTodoHandler(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            TodoEntity? todo = await GetTodo(request);
            await CheckTodoBelongsToCurrentUser(todo);
            await DeleteTodo(todo);
            return Unit.Value;
        }

        private async Task DeleteTodo(TodoEntity todo)
        {
            _dataContext.Remove(todo);
            await _dataContext.SaveChangesAsync();
        }

        private async Task CheckTodoBelongsToCurrentUser(TodoEntity todo)
        {
            var user = await _dataContext.Users.FindAsync(_httpContext.User.GetUserId());
            if (todo.User != user)
                throw new ForbiddenException("Todo does not belong to logged in user");
        }

        private async Task<TodoEntity> GetTodo(DeleteTodoCommand request)
        {
            var todo = await _dataContext.Todos.FindAsync(request.Id);
            if (todo == null)
                throw new NotFoundException("Todo not found for given id");
            return todo;
        }
    }
}