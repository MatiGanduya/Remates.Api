using Microsoft.EntityFrameworkCore;
using Remates.Api.Domain.Entites;

namespace Remates.Api.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        { 
        
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}

