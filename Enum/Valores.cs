using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace netCoreNew.Enum
{
    public static class Valores
    {
        public static string
            Nombre = "Intelmec",
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
    }

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



