using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class NegocioService : GenericRepository<Negocio>, INegocioService
    {
        public NegocioService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
