using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Todo.Users.DTOs
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
