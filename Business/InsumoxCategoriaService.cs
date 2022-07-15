using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class InsumoxCategoriaService : GenericRepository<InsumoxCategoria>, IInsumoxCategoriaService
    {
        public InsumoxCategoriaService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
