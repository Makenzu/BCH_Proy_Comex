using System;

namespace BCH.Comex.UI.Web.Models.AdministracionSwift
{
    public class DetalleReporteArchivosModel
    {
                public int fd_archivo { get; set; }
        public string nombre_archivo { get; set; }
        public System.DateTime fecha_creacion { get; set; }
        public Nullable<System.DateTime> fecha_confirma { get; set; }
        public string estado_archivo { get; set; }
        public Nullable<int> total_mensajes { get; set; }
        public Nullable<int> total_envios { get; set; }
        public Nullable<int> total_rechazos { get; set; }

        public string Verbo
        {
            get {
                if (estado_archivo == "R")
                {
                    return "Recepcionados";
                }
                if (estado_archivo == "P")
                {
                    return "Pendientes";
                }
                else
                {
                    return "No Transmitidos";
                }
            }
        }

    }
}