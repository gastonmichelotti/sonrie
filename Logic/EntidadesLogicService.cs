using netCoreNew.Business;
using netCoreNew.Models;
using System.Transactions;

namespace netCoreNew.Logic
{
    public class EntidadesLogicService : IEntidadesLogicService
    {
        private readonly IUsuarioService usuarioService;

        public EntidadesLogicService(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }
    }
}
