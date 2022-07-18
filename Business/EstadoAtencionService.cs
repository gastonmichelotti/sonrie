﻿using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class EstadoAtencionService : GenericRepository<EstadoAtencion>, IEstadoAtencionService
    {
        public EstadoAtencionService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
