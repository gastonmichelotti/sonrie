using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class RolService : GenericRepository<Rol>, IRolService
    {
        public RolService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
