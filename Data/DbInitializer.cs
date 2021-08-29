﻿using Microsoft.AspNetCore.Builder;
using netCoreNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace netCoreNew.Data
{
    public class DbInitializer
    {
        public static void Initialize(IApplicationBuilder app, NetCoreNewContext context)
        {
            foreach (var item in Roles)
            {
                var existeRol = context.Rol.Any(c => c.Nombre == item.Nombre);

                if (!existeRol)
                {
                    context.Rol.Add(item);
                }
            }

            foreach (var item in Negocios)
            {
                var existeNegocio = context.Negocio.Any();

                if (!existeNegocio)
                {
                    context.Negocio.Add(item);
                }
            }

            context.SaveChanges();
        }

        public static List<Negocio> Negocios
        {
            get
            {
                return new List<Negocio>()
                {
                    new Negocio
                    {
                
                    }
                };
            }
        }

        public static List<Rol> Roles
        {
            get
            {
                return new List<Rol>()
                {
                    new Rol
                    {
                        Nombre = "Administrador",
                        Redirect = "/Admin/Proyectos",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre = "Admin",
                                Email = "admin@gmail.com",
                                Password = "admin",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            }
                        }
                    }
                };
            }
        }
    }
}
