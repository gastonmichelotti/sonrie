using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class CodigoProveedorService : GenericRepository<CodigoProveedor>, ICodigoProveedorService
    {
        public CodigoProveedorService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
