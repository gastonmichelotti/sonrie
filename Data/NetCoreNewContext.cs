using Microsoft.EntityFrameworkCore;
using netCoreNew.Models;
using System.Linq;

namespace netCoreNew.Data
{
    public class NetCoreNewContext : DbContext
    {
        public NetCoreNewContext(DbContextOptions<NetCoreNewContext> options) :
               base(options)
        {
        }

        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }        
        public DbSet<Articulo> Articulo { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}