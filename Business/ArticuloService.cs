using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class ArticuloService : GenericRepository<Articulo>, IArticuloService
    {
        public ArticuloService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
