using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using static netCoreNew.Enum.Valores;

namespace netCoreNew.Business
{
    public class CompraService : GenericRepository<Compra>, ICompraService
    {
        private readonly NetCoreNewContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CompraService(NetCoreNewContext context,
            IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _context = context;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public override void Add(Compra entity)
        {
            entity.Historiales.Add(new HistorialCompra
            {
                Fecha = DateTime.UtcNow.AddHours(-3),
                Responsable = httpContextAccessor.HttpContext.User.Identity.Name,
                Propiedad = "Creacion"
            });

            base.Add(entity);
        }

        public override void Edit(Compra entity)
        {
            var entry = _context.Entry(entity);
            var modificados = entry.CurrentValues.Properties.Where(p => entry.Property("IdEstadoCompra").IsModified).ToList();

            if (modificados.Any())
            {
                entity.Historiales.Add(new HistorialCompra
                {
                    Fecha = DateTime.UtcNow.AddHours(-3),
                    Responsable = httpContextAccessor.HttpContext.User.Identity.Name,
                    Propiedad = "Estado",
                    Anterior = ((EstadoCompraEnm)((int)entry.OriginalValues["IdEstadoCompra"])).ToString(),
                    Nuevo = ((EstadoCompraEnm)((int)entry.CurrentValues["IdEstadoCompra"])).ToString()
                });
            }

            base.Edit(entity);
        }
    }
}
