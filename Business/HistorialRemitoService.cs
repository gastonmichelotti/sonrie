using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class HistorialRemitoService : GenericRepository<HistorialRemito>, IHistorialRemitoService
    {
        public HistorialRemitoService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
