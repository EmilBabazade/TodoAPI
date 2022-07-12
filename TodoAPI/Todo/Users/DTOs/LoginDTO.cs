using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Todo.Users.DTOs
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
