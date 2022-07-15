using Microsoft.EntityFrameworkCore;
using TodoAPI.Todo.Todo;
using TodoAPI.Todo.Users;

namespace TodoAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {

        }

        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<TodoEntity> Todos { get; set; }
    }
}
