using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.Devengo.Models
{
    public class DevengamientoHistoricoViewModel: DevengoViewModel
    {
        public UI_Combo listaPeriodos { get; set; }
        
        public DevengamientoHistoricoViewModel() 
        {
            listaPeriodos = new UI_Combo();
        }

        public DevengamientoHistoricoViewModel(IList<decimal> lista, List<UI_Message> Errores)
        {
            listaPeriodos = new UI_Combo();
            foreach (decimal periodo in lista)
            {
                this.listaPeriodos.AddItem((int)periodo, periodo.ToString());
            }
            this.ListaErrores = Errores;
        }

        public void update(IList<decimal> lista, int selected, List<UI_Message> Errores)
        {
            listaPeriodos = new UI_Combo();
            foreach (decimal periodo in lista)
            {
                this.listaPeriodos.AddItem((int)periodo, periodo.ToString());
            }
            listaPeriodos.SelectedValue = selected;

            this.ListaErrores = Errores;
        }
    }
}