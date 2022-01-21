using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class ProveedorService : GenericRepository<Proveedor>, IProveedorService
    {
        public ProveedorService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }

        public override void Add(Proveedor entity)
        {
            entity.Activo = true;
            entity.FechaAlta = DateTime.UtcNow.AddHours(-3);

            FixProveedor(entity);

            if (_context.Proveedor.Any(c => c.Email == entity.Email))
            {
                throw new Exception("Posible proveedor repetido");
            }

            base.Add(entity);

            _context.SaveChanges();
        }

        public void FixProveedor(Proveedor proveedor)
        {
            proveedor.Email = proveedor.Email.ToLower();
        }
    }
}
