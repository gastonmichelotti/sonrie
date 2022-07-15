using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class PrestacionxAtencionService : GenericRepository<PrestacionxAtencion>, IPrestacionxAtencionService
    {
        public PrestacionxAtencionService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
