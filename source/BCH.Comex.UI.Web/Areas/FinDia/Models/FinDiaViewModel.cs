using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.FinDia.Models
{
    public class FinDiaViewModel
    {
        public IList<UI_Message> ListaErrores { get; set; }

        public void update(IList<UI_Message> ListaErrores)
        {
            this.ListaErrores = ListaErrores;
        }
    }
}