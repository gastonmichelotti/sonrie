using Microsoft.AspNetCore.Http;
using netCoreNew.Data;
using netCoreNew.Models;
using netCoreNew.Repository;
using System;
using System.Linq;

namespace netCoreNew.Business
{
    public class RemitoService : GenericRepository<Remito>, IRemitoService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public RemitoService(NetCoreNewContext context,
            IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public override void Add(Remito entity)
        {
            entity.Historiales.Add(new HistorialRemito
            {
                Fecha = DateTime.UtcNow.AddHours(-3),
                Responsable = httpContextAccessor.HttpContext.User.Identity.Name,
                Propiedad = "Creacion"
            });

            base.Add(entity);
        }

        public override void Edit(Remito entity)
        {
            var entry = _context.Entry(entity);
            var modificados = entry.CurrentValues.Properties.ToList();

            foreach (var item in modificados)
            {
                if(entry.Property(item.Name).IsModified)
                {
                    entity.Historiales.Add(new HistorialRemito
                    {
                        Fecha = DateTime.UtcNow.AddHours(-3),
                        Responsable = httpContextAccessor.HttpContext.User.Identity.Name,
                        Propiedad = item.Name,
                        Anterior = entry.OriginalValues[item.Name].ToString(),
                        Nuevo = entry.CurrentValues[item.Name].ToString()
                    });
                }
            }

            if (modificados.Any())
            {
  
            }

            base.Edit(entity);
        }
    }
}
