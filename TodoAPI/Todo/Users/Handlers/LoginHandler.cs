using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Errors;
using TodoAPI.Services.jwt;
using TodoAPI.Todo.Users.DTOs;

namespace TodoAPI.Todo.Users.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, UserDTO>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwt;

        public LoginHandler(DataContext dataContext, IMapper mapper, IJWTService jwt)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _jwt = jwt;
        }
        public async Task<UserDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            LoginDTO loginDTO = request.LoginDTO;

            var user = await GetUser(request);
            CheckPassword(loginDTO, user);

            return new UserDTO
            {
                Token = _jwt.CreateToken(user),
                Email = loginDTO.Email,
                Id = user.Id,
                Name = user.Name
            };
        }

        private void CheckPassword(LoginDTO loginDTO, UserEntity user)
        {
            if (!_jwt.VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
                throw new BadRequestException("Wrong password");
        }

        private async Task<UserEntity> GetUser(LoginCommand request)
        {
            UserEntity? user = await _dataContext.Users.SingleOrDefaultAsync(a => a.Email.Trim() == request.LoginDTO.Email);
            if (user == null)
                throw new BadRequestException("User not found");
            return user;
        }
    }
}
