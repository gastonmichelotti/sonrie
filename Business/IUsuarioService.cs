using netCoreNew.Models;
using netCoreNew.Repository;

namespace netCoreNew.Business
{
    public interface IUsuarioService : IGenericRepository<Usuario>
    {
        Usuario GetByEmail(string email);
    }
}
