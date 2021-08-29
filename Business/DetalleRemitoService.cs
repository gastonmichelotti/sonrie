using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class DetalleRemitoService : GenericRepository<DetalleRemito>, IDetalleRemitoService
    {
        public DetalleRemitoService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
