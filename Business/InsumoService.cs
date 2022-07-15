using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class InsumoService : GenericRepository<Insumo>, IInsumoService
    {
        public InsumoService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
