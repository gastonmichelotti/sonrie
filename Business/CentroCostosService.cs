using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class CentroCostosService : GenericRepository<CentroCostos>, ICentroCostosService
    {
        public CentroCostosService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
