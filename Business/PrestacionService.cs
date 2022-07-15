using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class PrestacionService : GenericRepository<Prestacion>, IPrestacionService
    {
        public PrestacionService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
