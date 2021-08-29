﻿using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class EstadoCompraService : GenericRepository<EstadoCompra>, IEstadoCompraService
    {
        public EstadoCompraService(NetCoreNewContext context) : base(context)
        {
            _context = context;
        }
    }
}
