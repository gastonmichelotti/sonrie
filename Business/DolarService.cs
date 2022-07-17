using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class DolarService : GenericRepository<Dolar>, IDolarService
    {
        public DolarService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
