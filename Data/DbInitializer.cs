using Microsoft.AspNetCore.Builder;
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

            foreach (var item in EstadosCompra)
            {
                var existeEstado = context.EstadoCompra.Any(c => c.Nombre == item.Nombre);

                if (!existeEstado)
                {
                    context.EstadoCompra.Add(item);
                }
            }

            foreach (var item in CentroCostos)
            {
                var existeCentro = context.CentroCostos.Any(c => c.Nombre == item.Nombre);

                if (!existeCentro)
                {
                    context.CentroCostos.Add(item);
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

            //var existe = context.Usuario.Any();

            //foreach (var item in Usuarios)
            //{
            //    item.IdRol = context.Rol.First(c => c.Nombre == "Administrador").Id;

            //    context.Usuario.Add(item);
            //}

            //context.SaveChanges();

            //existe = context.Provincia.Any();

            //if (!existe)
            //{
            //    foreach (var item in Provincias)
            //    {
            //        context.Provincia.Add(new Provincia
            //        {
            //            Nombre = item
            //        });
            //    }
            //}

            //context.SaveChanges();
        }

        //public static List<Usuario> Usuarios
        //{
        //    get
        //    {
        //        return new List<Usuario>()
        //        {
        //            new Usuario
        //            {
        //                Nombre = "Admin",
        //                Email = "admin@gmail.com",
        //                Password = "admin",
        //                Telefono = "12345",
        //                FechaAlta = DateTime.Now
        //            }
        //        };
        //    }
        //}

        public static List<Negocio> Negocios
        {
            get
            {
                return new List<Negocio>()
                {
                    new Negocio
                    {
                        MontoMinimo = 100,
                        MontoDiferencia = 10
                    }
                };
            }
        }

        public static List<CentroCostos> CentroCostos
        {
            get
            {
                return new List<CentroCostos>()
                {
                    new CentroCostos
                    {
                        Nombre = "CentroCostos1",
                    },
                    new CentroCostos
                    {
                        Nombre = "CentroCostos2",
                    }
                };
            }
        }

        public static List<EstadoCompra> EstadosCompra
        {
            get
            {
                return new List<EstadoCompra>()
                {
                    new EstadoCompra
                    {
                        Nombre = "Abierta",
                        Color = "info"
                    },
                    new EstadoCompra
                    {
                        Nombre = "Pendiente de aprobacion",
                        Color = "warning"
                    },
                    new EstadoCompra
                    {
                        Nombre = "Aprobada",
                        Color = "success"
                    },
                    new EstadoCompra
                    {
                        Nombre = "Rechazada",
                        Color = "danger"
                    },
                    new EstadoCompra
                    {
                        Nombre = "Cancelada",
                        Color = "danger"
                    },
                    new EstadoCompra
                    {
                        Nombre = "Finalizada",
                        Color = "info"
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
                        Redirect = "/Admin/Clientes",
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
                    },
                    new Rol
                    {
                        Nombre = "Gerencia General",
                        Redirect = "/Admin/Compras",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre = "pablo.coleoni",
                                Email = "pablo.coleoni@vilahouse.com",
                                Password = "pablo.coleoni",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "florencio.ferrara",
                                Email = "florencio.ferrara@vilahouse.com",
                                Password = "florencio.ferrara",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "martin.ortiz",
                                Email = "martin.ortiz@vilahouse.com",
                                Password = "martin.ortiz",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "otto.klix",
                                Email = "otto.klix@vilahouse.com",
                                Password = "otto.klix",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            }
                        }
                    },
                    new Rol
                    {
                        Nombre = "Gerencia Operaciones",
                        Redirect = "/Admin/Compras",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre = "franco.catini",
                                Email = "franco.catini@vilahouse.com",
                                Password = "franco.catini",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "mateo.bustos",
                                Email = "mateo.bustos@vilahouse.com",
                                Password = "mateo.bustos",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            }
                        }
                    },
                    new Rol
                    {
                        Nombre = "Administracion",
                        Redirect = "/Admin/Clientes",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre = "maria.butleer",
                                Email = "maria.butleer@vilahouse.com",
                                Password = "maria.butleer",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "paula.nerac",
                                Email = "paula.nerac@vilahouse.com",
                                Password = "paula.nerac",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "matias.ierolano",
                                Email = "matias.ierolano@vilahouse.com",
                                Password = "matias.ierolano",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "luciana.ossola",
                                Email = "luciana.ossola@vilahouse.com",
                                Password = "luciana.ossola",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "manuel.ropellatto",
                                Email = "manuel.ropellatto@vilahouse.com",
                                Password = "manuel.ropellatto",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "florencia.barrionuevo",
                                Email = "florencia.barrionuevo@vilahouse.com",
                                Password = "florencia.barrionuevo",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "david.valenzuela",
                                Email = "david.valenzuela@vilahouse.com",
                                Password = "david.valenzuela",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            }
                        }
                    },
                    new Rol
                    {
                        Nombre = "Finanzas",
                        Redirect = "/Admin/Clientes",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre = "valentia.oliva",
                                Email = "valentia.oliva@vilahouse.com",
                                Password = "valentia.oliva",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "veronica.etchegaray",
                                Email = "veronica.etchegaray@vilahouse.com",
                                Password = "veronica.etchegaray",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "noelia.alderete",
                                Email = "noelia.alderete@vilahouse.com",
                                Password = "noelia.alderete",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "victoria.vaca",
                                Email = "victoria.vaca@vilahouse.com",
                                Password = "victoria.vaca",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "trinidad.pizarro",
                                Email = "trinidad.pizarro@vilahouse.com",
                                Password = "trinidad.pizarro",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            }
                        }
                    },
                    new Rol
                    {
                        Nombre = "Marketing",
                        Redirect = "/Admin/Clientes",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre = "tatiana.caccivillani",
                                Email = "tatiana.caccivillani@vilahouse.com",
                                Password = "tatiana.caccivillani",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "marina.tsernotopulos",
                                Email = "marina.tsernotopulos@vilahouse.com",
                                Password = "marina.tsernotopulos",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "facundo.jara",
                                Email = "facundo.jara@vilahouse.com",
                                Password = "facundo.jara",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "ayelen.wurch",
                                Email = "ayelen.wurch@vilahouse.com",
                                Password = "ayelen.wurch",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "lourdes.cabral",
                                Email = "lourdes.cabral@vilahouse.com",
                                Password = "lourdes.cabral",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "melania.garcia",
                                Email = "melania.garcia@vilahouse.com",
                                Password = "melania.garcia",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "belen.berretti",
                                Email = "belen.berretti@vilahouse.com",
                                Password = "belen.berretti",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "nicolas.lozada",
                                Email = "nicolas.lozadavilahouse.com",
                                Password = "nicolas.lozada",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                        }
                    },
                    new Rol
                    {
                        Nombre = "Comercial",
                        Redirect = "/Admin/Clientes",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre = "ezequiel.tolosa",
                                Email = "ezequiel.tolosa@vilahouse.com",
                                Password = "ezequiel.tolosa",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            }
                        }
                    },
                    new Rol
                    {
                        Nombre = "Supervisor Comercial",
                        Redirect = "/Admin/Clientes",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre = "marco.coleoni",
                                Email = "marco.coleoni@vilahouse.com",
                                Password = "marco.coleoni",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "fernando.taborda",
                                Email = "fernando.taborda@vilahouse.com",
                                Password = "fernando.taborda",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "martin.frey",
                                Email = "martin.frey@vilahouse.com",
                                Password = "martin.frey",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "leonardo.carmona",
                                Email = "leonardo.carmona@vilahouse.com",
                                Password = "leonardo.carmona",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre = "jode.bentancourt",
                                Email = "jode.bentancourt@vilahouse.com",
                                Password = "jode.bentancourt",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "martin.ortiz",
                                Email =     "martin.ortiz@vilahouse.com",
                                Password =  "martin.ortiz",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "nicolas.sagario",
                                Email =     "nicolas.sagario@vilahouse.com",
                                Password =  "nicolas.sagario",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "damian.alaniz",
                                Email =     "damian.alaniz@vilahouse.com",
                                Password =  "damian.alaniz",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "gonzalo.alberini",
                                Email =     "gonzalo.alberini@vilahouse.com",
                                Password =  "gonzalo.alberini",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "martin.adams",
                                Email =     "martin.adams@vilahouse.com",
                                Password =  "martin.adams",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "micalea.goldam",
                                Email =     "micalea.goldam@vilahouse.com",
                                Password =  "micalea.goldam",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                        }
                    },
                    new Rol
                    {
                        Nombre = "RRHH",
                        Redirect = "/Admin/Clientes",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre =    "valentina.ropellatto",
                                Email =     "valentina.ropellatto@vilahouse.com",
                                Password =  "valentina.ropellatto",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "emir.burirges",
                                Email =     "emir.burirges@vilahouse.com",
                                Password =  "emir.burirges",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "santiago.fischer",
                                Email =     "santiago.fischer@vilahouse.com",
                                Password =  "santiago.fischer",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                        }
                    },
                    new Rol
                    {
                        Nombre = "Programacion de Produccion",
                        Redirect = "/Admin/Clientes",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre =    "luciano.rodrigez",
                                Email =     "luciano.rodrigez@vilahouse.com",
                                Password =  "luciano.rodrigez",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "nicanor.beretta",
                                Email =     "nicanor.beretta@vilahouse.com",
                                Password =  "nicanor.beretta",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "yamila.boxler",
                                Email =     "yamila.boxler@vilahouse.com",
                                Password =  "yamila.boxler",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "camille.olivo",
                                Email =     "camille.olivo@vilahouse.com",
                                Password =  "camille.olivo",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "nicolas.sueldo",
                                Email =     "nicolas.sueldo@vilahouse.com",
                                Password =  "nicolas.sueldo",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "victoria.robino",
                                Email =     "victoria.robino@vilahouse.com",
                                Password =  "victoria.robino",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "julieta.milanesio",
                                Email =     "julieta.milanesio@vilahouse.com",
                                Password =  "julieta.milanesio",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "luciana.medina ",
                                Email =     "luciana.medina @vilahouse.com",
                                Password =  "luciana.medina ",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "fresio.albaretto",
                                Email =     "fresio.albaretto@vilahouse.com",
                                Password =  "fresio.albaretto",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "mariano.paillet",
                                Email =     "mariano.paillet@vilahouse.com",
                                Password =  "mariano.paillet",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "florencia.martinengo",
                                Email =     "florencia.martinengo@vilahouse.com",
                                Password =  "florencia.martinengo",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "marilyn.andrada",
                                Email =     "marilyn.andrada@vilahouse.com",
                                Password =  "marilyn.andrada",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "silvana.burgener",
                                Email =     "silvana.burgener@vilahouse.com",
                                Password =  "silvana.burgener",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "ivana.gaytan",
                                Email =     "ivana.gaytan@vilahouse.com",
                                Password =  "ivana.gaytan",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "laura.adams",
                                Email =     "laura.adams@vilahouse.com",
                                Password =  "laura.adams",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "camila.gonzalez",
                                Email =     "camila.gonzalez@vilahouse.com",
                                Password =  "camila.gonzalez",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "magali.agioi",
                                Email =     "magali.agioi@vilahouse.com",
                                Password =  "magali.agioi",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "sofia.giobergia",
                                Email =     "sofia.giobergia@vilahouse.com",
                                Password =  "sofia.giobergia",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "sandra.weigant",
                                Email =     "sandra.weigant@vilahouse.com",
                                Password =  "sandra.weigant",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "nahuel.carpio",
                                Email =     "nahuel.carpio@vilahouse.com",
                                Password =  "nahuel.carpio",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            }
                        }
                    },
                    new Rol
                    {
                        Nombre = "Compras",
                        Redirect = "/Admin/Clientes",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre =    "pilar.morante",
                                Email =     "pilar.morante@vilahouse.com",
                                Password =  "pilar.morante",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "luis.mazzola",
                                Email =     "luis.mazzola@vilahouse.com",
                                Password =  "luis.mazzola",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "luis.varone",
                                Email =     "luis.varone@vilahouse.com",
                                Password =  "luis.varone",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "martin.iglesias",
                                Email =     "martin.iglesias@vilahouse.com",
                                Password =  "martin.iglesias",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "jonathan.jatip",
                                Email =     "jonathan.jatip@vilahouse.com",
                                Password =  "jonathan.jatip",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                        }
                    },
                     new Rol
                    {
                        Nombre = "Logistica",
                        Redirect = "/Admin/Clientes",
                        Usuarios = new List<Usuario>
                        {
                            new Usuario
                            {
                                Nombre =    "lorenzo.egues",
                                Email =     "lorenzo.egues@vilahouse.com",
                                Password =  "lorenzo.egues",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "sol.barbero",
                                Email =     "sol.barbero@vilahouse.com",
                                Password =  "sol.barbero",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "romina.acuña",
                                Email =     "romina.acuña@vilahouse.com",
                                Password =  "romina.acuña",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "bernardo.gonzalez",
                                Email =     "bernardo.gonzalez@vilahouse.com",
                                Password =  "bernardo.gonzalez",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "alexis.strada",
                                Email =     "alexis.strada@vilahouse.com",
                                Password =  "alexis.strada",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "hector.villa",
                                Email =     "hector.villa@vilahouse.com",
                                Password =  "hector.villa",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "alexis.tomasi",
                                Email =     "alexis.tomasi@vilahouse.com",
                                Password =  "alexis.tomasi",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "giuliano.luna",
                                Email =     "giuliano.luna@vilahouse.com",
                                Password =  "giuliano.luna",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                            new Usuario
                            {
                                Nombre =    "cristian.romero",
                                Email =     "cristian.romero@vilahouse.com",
                                Password =  "cristian.romero",
                                Telefono = "12345",
                                FechaAlta = DateTime.Now
                            },
                        }
                    }
                };
            }
        }

        //public static List<string> Provincias
        //{
        //    get
        //    {
        //        return new List<string>()
        //        {
        //            "Cordoba",
        //            "Buenos Aires",
        //            "Santa Fe",
        //            "Mendoza"
        //        };
        //    }
        //}
    }
}
