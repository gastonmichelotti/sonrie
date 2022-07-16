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
        public DbSet<InsumoxCategoria> InsumoxCategoria { get; set; }        
        public DbSet<Paciente> Paciente { get; set; }        
        public DbSet<ObraSocial> ObraSocial { get; set; }        
        public DbSet<Prestacion> Prestacion { get; set; }        
        public DbSet<Atencion> Atencion { get; set; }        
        public DbSet<PrestacionxAtencion> PrestacionxAtencion { get; set; }        
        public DbSet<CategoriaPestacion> CategoriaPretacion  { get; set; }        
        public DbSet<Insumo> Insumo { get; set; }        
        public DbSet<Precio> Precio { get; set; }        

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