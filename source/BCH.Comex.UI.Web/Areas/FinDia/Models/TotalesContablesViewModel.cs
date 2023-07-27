using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.FinDia.Models
{
    public class TotalesContablesViewModel : FinDiaViewModel
    {
        public string Reporte { get; set; }

        public TotalesContablesViewModel(string reporte, List<UI_Message> listaMensajes)
        {
            this.ListaErrores = listaMensajes;
            this.Reporte = reporte;
        }
    }
}
