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

        public DbSet<Proyecto> Compra { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<Negocio> Negocio { get; set; }
        public DbSet<DetalleRecuento> DetalleRecuento { get; set; }
        public DbSet<Proyecto> Proyecto { get; set; }
        public DbSet<Recuento> Recuento { get; set; }
        public DbSet<CodigoProveedor> CodigoProveedors { get; set; }

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