using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace netCoreNew.Enum
{
    public static class Valores
    {
        public static string
            Nombre = "Vilahouse",
            Slogan = "",
            Url = "",
            Descripcion = "",
            Email = "",
            Telefono = "",
            Direccion = "Córdoba, Argentina ",
            Creacion = "Creación exitosa!",
            Edicion = "Modificación exitosa!",
            Eliminacion = "Registro eliminado",
            Incorrectos = "Algunos datos no son correctos",
            Error = "Ups! Surgió un error inesperado";

        public enum CondicionFrenteIvaEnum
        {
            ResponsableInscripto = 1,
            Excento = 2,
            ConsumidorFinal = 3,
            Monotributista = 4
        }

        public enum ProvinciaEnum
        {
            Cordoba = 1,
            BuenosAires = 2,
            Neuquen = 3,
            RioNegro = 4,
            Tucuman = 5,
            SantaFe = 6,
            SantiagoDelEstero = 7,
            Chubut = 8,
            SanLuis = 9,
            Catamarca = 10,
            Corrientes = 11,
            EntreRios = 12,
            Formosa = 13,
            Jujuy = 14,
            LaPampa = 15,
            LaRioja = 16,
            Mendoza = 17,
            Misiones = 18,
            Salta = 19,
            SanJuan = 20,
            SantaCruz = 21,
            TierraDelFuego = 22
            
        }

        public enum ProvinciaUsuarioEnum
        {
            Cordoba = 1,
            BuenosAires = 2
        }

        public enum TipoArticuloEnum
        {
            Producto = 1,
            Servicio = 2
        }

        public static int[] CondicionPagoEnum => new int[]
        {
            7, 15, 30, 45, 60, 90, 120, 150, 180
        };

        public static double[] IvaEnum => new double[]
        {
            21, 10.5, 0
        };  

        public enum EstadoCompraEnm
        {
            Abierta = 1,
            PendienteAprobacion = 2,
            Aprobada = 3,
            Rechazada = 4,
            Cancelada = 5,
            Finalizada = 6
        }

        public enum EstadoLogisticaEnm
        {
            Abierta = EstadoCompraEnm.Abierta,
            PendienteAprobacion = EstadoCompraEnm.PendienteAprobacion,
            Aprobada = EstadoCompraEnm.Aprobada,
            Rechazada = EstadoCompraEnm.Rechazada,
            Cancelada = EstadoCompraEnm.Cancelada,
            Finalizada = EstadoCompraEnm.Finalizada,
            Recibida = 7,
            RecibidaParcial = 8
        }

        public enum TipoCompraEnum
        {
            General = 1,
            Productiva = 2
        }

        //public enum FormaPagoEnum
        //{
        //    FormaPago1 = 1,
        //    FormaPago2 = 2,
        //    FormaPago3 = 3
        //}

        public enum CondicionPagoOrdenEnum
        {
            ContraEntregaRemito = 1,
            ContraFactura = 2,
            PrevioEntrega = 3,
            Otro = 4
        }
    }
}
