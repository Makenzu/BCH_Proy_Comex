using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.GeneracionMT300.Models
{
    public class CargaViewModel
    {
        public bool archivoCargado { get; set; }
        public Archivo archivo { get; set; }
        public int nRegistrosTotales { get; set; }
        public int nRegistrosCandidatos { get; set; }
        public int nRegistrosNoCantidatos { get; set; }
        public int nRegistrosNuevosOK { get; set; }
        public int nRegistrosExistentes { get; set; }
        public int nRegistrosErrorFormato { get; set; }
        public int nRegistrosErrorCaracteres { get; set; }
        public IList<ArchivoDetalle> registrosNuevosOK { get; set; }
        public IList<ArchivoDetalle> registrosExistentes { get; set; }
        public IList<UI_Message> ListaMensajes { get; set; }
        public CargaViewModel()
        {
            archivoCargado = false;
        }
    }
}