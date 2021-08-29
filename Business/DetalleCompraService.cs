using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class DetalleCompraService : GenericRepository<DetalleCompra>, IDetalleCompraService
    {
        public DetalleCompraService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
