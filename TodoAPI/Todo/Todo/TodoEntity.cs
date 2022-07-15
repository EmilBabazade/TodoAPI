using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Todo.Users;

namespace TodoAPI.Todo.Todo
{
    public class TodoEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Done { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public virtual UserEntity User { get; set; }
    }
}