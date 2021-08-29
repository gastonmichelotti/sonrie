using netCoreNew.Models;
using netCoreNew.Repository;

namespace netCoreNew.Business
{
    public interface IProveedorService : IGenericRepository<Proveedor>
    {
        void FixProveedor(Proveedor proveedor);
    }
}
