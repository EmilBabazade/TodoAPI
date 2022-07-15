using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Todo.Todo.DTOs
{
    public class TodoDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Done { get; set; }
        public DateTime? DueDate { get; set; }
        public int UserId { get; set; }
    }
}