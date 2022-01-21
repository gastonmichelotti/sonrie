using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class RecuentoService : GenericRepository<Recuento>, IRecuentoService
    {
        public RecuentoService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
