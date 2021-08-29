using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class ProyectoService : GenericRepository<Proyecto>, IProyectoService
    {
        public ProyectoService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
