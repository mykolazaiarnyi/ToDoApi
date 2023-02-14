using Microsoft.EntityFrameworkCore;
using ToDoApi.Domain;

namespace ToDoApi.Infrastructure.Data
{
    public class ToDoContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
