using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TodoAPI.Data;
using TodoAPI.Errors;
using TodoAPI.Extensions;
using TodoAPI.Todo.Todo.DTOs;

namespace TodoAPI.Todo.Todo.Handlers
{
    public class GetTodoHandler : IRequestHandler<GetTodoQuery, TodoDTO>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;
        public GetTodoHandler(DataContext dataContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _httpContext = httpContextAccessor.HttpContext;

        }
        public async Task<TodoDTO> Handle(GetTodoQuery request, CancellationToken cancellationToken)
        {
            TodoEntity? todo = await GetTodo(request);
            await CheckTodoBelongsToUser(todo);
            return _mapper.Map<TodoDTO>(todo);
        }

        private async Task CheckTodoBelongsToUser(TodoEntity todo)
        {
            var user = await _dataContext.Users.FindAsync(_httpContext.User.GetUserId());
            if (todo.User != user) throw new ForbiddenException("Todo does not belong to the current user");
        }

        private async Task<TodoEntity> GetTodo(GetTodoQuery request)
        {
            var todo = await _dataContext.Todos.FindAsync(request.Id);
            if (todo == null) throw new NotFoundException("Todo not found");
            return todo;
        }
    }
}