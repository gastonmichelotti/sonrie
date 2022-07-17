using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class CategoriaPrestacionService : GenericRepository<CategoriaPrestacion>, ICategoriaPrestacionService
    {
        public CategoriaPrestacionService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
