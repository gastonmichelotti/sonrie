using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netCoreNew.Models
{
    public class Rol
    {
        public Rol()
        {
            this.Usuarios = new List<Usuario>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Redirect { get; set; }
        public string Observaciones { get; set; }

        public bool Eliminado { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}