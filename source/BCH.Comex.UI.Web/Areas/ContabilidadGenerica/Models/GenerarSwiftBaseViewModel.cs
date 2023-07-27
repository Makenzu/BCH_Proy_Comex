using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models
{
    public class GenerarSwiftBaseViewModel : BCH.Comex.UI.Web.Models.FundTransfer.GenerarSwiftViewModel
    {
        public List<UI_Message> ListaErrores { get; set; }

        public GenerarSwiftBaseViewModel()
        {

        }
    }
}