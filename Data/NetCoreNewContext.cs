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

        public DbSet<Compra> Compra { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Articulo> Articulo { get; set; }
        public DbSet<EstadoCompra> EstadoCompra { get; set; }
        public DbSet<DetalleCompra> DetalleCompra { get; set; }
        public DbSet<CentroCostos> CentroCostos { get; set; }
        public DbSet<Negocio> Negocio { get; set; }
        public DbSet<Remito> Remito { get; set; }
        public DbSet<DetalleRemito> DetalleRemito { get; set; }
        public DbSet<HistorialCompra> HistorialCompra { get; set; }
        public DbSet<HistorialRemito> HistorialRemito { get; set; }

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