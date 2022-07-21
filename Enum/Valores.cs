using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace netCoreNew.Enum
{
    public static class Valores
    {
        public static string
            Nombre = "Sonríe Odontología",
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


        public enum FormaDePagoEnum
        {
            Contado = 1,
            Debito = 2,
            Transferencia = 3,
            Credito = 4,
            Prepagado = 5
        }

        public enum EstadoAtencionEnum
        {
            PendientePago = 1,
            PagadaParcial = 2,
            Pagada = 3,
        }

        public enum PiezaEnum
        {
            NoAplica = 0,
            P11 = 11,
            P12 = 12,
            P13 = 13,
            P14 = 14,
            P15 = 15,
            P16 = 16,
            P17 = 17,
            P18 = 18,
            P21 = 21,
            P22 = 22,
            P23 = 23,
            P24 = 24,
            P25 = 25,
            P26 = 26,
            P27 = 27,
            P28 = 28,
            P31 = 31,
            P32 = 32,
            P33 = 33,
            P34 = 34,
            P35 = 35,
            P36 = 36,
            P37 = 37,
            P38 = 38,
            P41 = 41,
            P42 = 42,
            P43 = 43,
            P44 = 44,
            P45 = 45,
            P46 = 46,
            P47 = 47,
            P48 = 48,
            P51 = 51,
            P52 = 52,
            P53 = 53,
            P54 = 54,
            P55 = 55,
            P61 = 61,
            P62 = 62,
            P63 = 63,
            P64 = 64,
            P65 = 65,
            P71 = 71,
            P72 = 72,
            P73 = 73,
            P74 = 74,
            P75 = 75,
            P81 = 81,
            P82 = 82,
            P83 = 83,
            P84 = 84,
            P85 = 85
        }

        public enum CaraEnum
        {
            Oclusal = 1,
            Distal = 2,
            Mesial = 3,
            Vestibular = 4,
            Palatino = 5,
            Lingual = 6,
            Gingival = 7,
        }
    }



}



