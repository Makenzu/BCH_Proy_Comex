using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.Devengo.Models
{
    public class DevengoViewModel
    {
        public List<UI_Message> ListaErrores { get; set; }

        public void update(List<UI_Message> listaErrores)
        {
            this.ListaErrores = listaErrores;
        }
    }
}