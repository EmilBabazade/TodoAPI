using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TodoAPI.Data;
using TodoAPI.Extensions;
using TodoAPI.Todo.Todo.DTOs;
using TodoAPI.Todo.Users;

namespace TodoAPI.Todo.Todo.Handlers
{
    public class AddTodoHandler : IRequestHandler<AddTodoCommand, TodoDTO>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly HttpContext _httpContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddTodoHandler(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _dataContext = dataContext;
            _mapper = mapper;

        }
        public async Task<TodoDTO> Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            UserEntity? user = GetUser();
            TodoEntity newTodo = await AddTodo(request, user);
            return _mapper.Map<TodoDTO>(newTodo);
        }

        private async Task<TodoEntity> AddTodo(AddTodoCommand request, UserEntity? user)
        {
            var addTodoDTO = request.AddTodoDTO;
            var newTodo = new TodoEntity
            {
                DueDate = addTodoDTO.DueDate,
                Text = addTodoDTO.Text,
                User = user
            };
            _dataContext.Add(newTodo);
            await _dataContext.SaveChangesAsync();
            return newTodo;
        }

        private UserEntity? GetUser()
        {
            return _dataContext.Users.Find(_httpContext.User.GetUserId());
        }
    }
}