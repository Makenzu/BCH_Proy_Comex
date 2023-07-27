using BCH.Comex.Core.Entities.Cext01.Supervisor;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Models
{
    public class ProductividadViewModel : SupervisorViewModel
    {
        public string anio { get; set; }
        public List<SelectListItem> ltMeses { get; set; }
        public int mesSelected { get; set; }
        public string stringHtml { get; set; }

        public ProductividadViewModel() { }

        public ProductividadViewModel(DatosGlobales globales)
        {
            ltMeses = new List<SelectListItem>();
            ltMeses = globales.FrmAyM.ltMeses.ToList().Select(x => new SelectListItem { Text = x.Value, Value = x.Key }).ToList();
            anio = globales.FrmAyM.anio;
            stringHtml = globales.FrmAyM.stringHtml;
            this.ListaErrores = globales.ListaMensajesError;
        }


    }
}