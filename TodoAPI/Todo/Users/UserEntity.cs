﻿using System.ComponentModel.DataAnnotations;
using TodoAPI.Todo.Todo;

namespace TodoAPI.Todo.Users
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public virtual List<TodoEntity> Todos { get; set; }
    }
}
