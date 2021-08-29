using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class UsuarioService : GenericRepository<Usuario>, IUsuarioService
    {
        public UsuarioService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }

        public Usuario GetByEmail(string email)
        {
            return GetSingle(c => c.Email.Trim().ToLower() == email.Trim().ToLower());
        }

        private bool ExisteEmail(string email, int id)
        {
            return _context.Usuario.Any(c => c.Email.Trim().ToLower() == email.Trim().ToLower() && c.Id != id);
        }

        public override void Edit(Usuario entity)
        {
            if (ExisteEmail(entity.Email, entity.Id))
            {
                throw new Exception("Email existente");
            }

            base.Edit(entity);
        }
    }
}
