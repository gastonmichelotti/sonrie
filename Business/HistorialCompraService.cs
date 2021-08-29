using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class HistorialCompraService : GenericRepository<HistorialCompra>, IHistorialCompraService
    {
        public HistorialCompraService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
