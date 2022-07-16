using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Errors;
using TodoAPI.Services.jwt;

namespace TodoAPI.Todo.Users.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly DataContext _dataContext;
        private readonly IJWTService _jwt;

        public RegisterHandler(DataContext dataContext, IJWTService jwt)
        {
            _dataContext = dataContext;
            _jwt = jwt;
        }
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await CheckEmailUnique(request);
            await AddUser(request);
            return Unit.Value;
        }

        private async Task AddUser(RegisterCommand request)
        {
            _jwt.CreatePasswordHash(request.RegisterDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
            _dataContext.Add(new UserEntity
            {
                Email = request.RegisterDTO.Email,
                Name = request.RegisterDTO.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });
            await _dataContext.SaveChangesAsync();
        }

        private async Task CheckEmailUnique(RegisterCommand request)
        {
            if (await _dataContext.Users.AnyAsync(u => u.Email == request.RegisterDTO.Email))
                throw new BadRequestException("User with given email already exists");
        }
    }
}
