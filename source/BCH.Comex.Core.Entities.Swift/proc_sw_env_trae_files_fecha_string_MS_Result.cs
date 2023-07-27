namespace BCH.Comex.Core.Entities.Swift
{
    using System;
    
    public partial class proc_sw_env_trae_files_fecha_string_MS_Result
    {
        public int fd_archivo { get; set; }
        public string nombre_archivo { get; set; }
        public string fecha_creacion { get; set; }
        public string fecha_confirma { get; set; }
        public string estado_archivo { get; set; }
        public Nullable<int> total_mensajes { get; set; }
        public Nullable<int> total_envios { get; set; }
        public Nullable<int> total_rechazos { get; set; }
      
    }
}
