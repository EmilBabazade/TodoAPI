using TodoAPI.Todo.Users;

namespace TodoAPI.Services.jwt
{
    public interface IJWTService
    {
        public string CreateToken(UserEntity account);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
