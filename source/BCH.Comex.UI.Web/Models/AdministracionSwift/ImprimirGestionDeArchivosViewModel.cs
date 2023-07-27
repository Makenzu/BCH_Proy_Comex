using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCH.Comex.UI.Web.Models.AdministracionSwift
{
    public class ImprimirGestionDeArchivosViewModel
    {
        public bool Enviados { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public IEnumerable<IGrouping<int, proc_sw_env_trae_files_MS_Result>> Registros { get; set; }
        public string Verbo
        {
            get
            {
                if (this.Enviados)
                {
                    return "Archivo de Mensajes Terminados";
                }
                else
                {
                    return "Archivo de Mensajes Terminados";
                }
            }
        }
    }
}