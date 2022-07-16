using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Extensions;
using TodoAPI.Todo.Todo.DTOs;

namespace TodoAPI.Todo.Todo.Handlers
{
    public class GetTodosHandler : IRequestHandler<GetTodosQueries, IEnumerable<TodoDTO>>
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly HttpContext _httpContext;
        public GetTodosHandler(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<IEnumerable<TodoDTO>> Handle(GetTodosQueries request, CancellationToken cancellationToken)
        {
            IQueryable<TodoEntity> query = await TodosForUser();
            query = OrderById(request, query);
            query = FilterByDate(request, query);
            return await query.ProjectTo<TodoDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        private async Task<IQueryable<TodoEntity>> TodosForUser()
        {
            var user = await _dataContext.Users.FindAsync(_httpContext.User.GetUserId());
            var query = _dataContext.Todos.Where(t => t.User == user).AsQueryable();
            return query;
        }

        private static IQueryable<TodoEntity> FilterByDate(GetTodosQueries request, IQueryable<TodoEntity> query)
        {
            if (request.Date != null)
            {
                query = query.Where(t => t.DueDate != null && t.DueDate.Value.Date.Equals(request.Date.Value.Date));
            }

            return query;
        }

        private static IQueryable<TodoEntity> OrderById(GetTodosQueries request, IQueryable<TodoEntity> query)
        {
            query = request.Order switch
            {
                "asc" => query.OrderBy(t => t.Id),
                "desc" => query.OrderByDescending(t => t.Id),
                _ => query
            };
            return query;
        }
    }
}