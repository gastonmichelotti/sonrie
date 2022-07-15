using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class PacienteService : GenericRepository<Paciente>, IPacienteService
    {
        public PacienteService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
