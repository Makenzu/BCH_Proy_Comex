using System;

namespace BCH.Comex.Core.Entities.Swift
{
    public class proc_sw_env_trae_filesDTO
    {
        public int fd_archivo { get; set; }
        public string nombre_archivo { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_confirma { get; set; }
        public string estado_archivo { get; set; }
        public int total_mensajes { get; set; }
        public int total_envios { get; set; }
        public int total_rechazos { get; set; }
        public int campo1 { get; set; }
    }
}
