using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class AtencionService : GenericRepository<Atencion>, IAtencionService
    {
        public AtencionService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
