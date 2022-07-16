using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class PrecioService : GenericRepository<Precio>, IPrecioService
    {
        public PrecioService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
