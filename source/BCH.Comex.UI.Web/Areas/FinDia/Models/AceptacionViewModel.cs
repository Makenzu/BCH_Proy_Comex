using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FinDia;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.FinDia.Models
{
    public class AceptacionViewModel: FinDiaViewModel
    {
        public IList<T_jAcp> ListaAceptaciones { get; set; }

        public AceptacionViewModel()
        {
            ListaAceptaciones = new List<T_jAcp>();
        }

        public AceptacionViewModel(IList<T_jAcp> Lista)
        {
            this.ListaAceptaciones = Lista;
        }

        public AceptacionViewModel(IList<T_jAcp> Lista, IList<UI_Message> ListaErrores)
        {
            this.ListaAceptaciones = Lista;
            this.ListaErrores = ListaErrores;
        }
    }
}