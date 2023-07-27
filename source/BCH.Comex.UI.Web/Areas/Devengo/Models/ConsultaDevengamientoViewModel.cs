using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.Devengo.Models
{
    public class ConsultaDevengamientoViewModel: DevengoViewModel
    {
        public UI_Combo listaPeriodos { get; set; }
        public string Titulo { get; set; }

        public ConsultaDevengamientoViewModel() 
        {
            listaPeriodos = new UI_Combo();
        }

        public ConsultaDevengamientoViewModel(List<string> lista, string tit,List<UI_Message> Errores)
        {
            listaPeriodos = new UI_Combo();
            foreach (string periodo in lista)
            {
                this.listaPeriodos.AddItem(int.Parse(periodo), periodo);
            }
            this.Titulo = tit;
            this.ListaErrores = Errores;
        }

        public void update(List<string> lista, string tit, int selected, List<UI_Message> Errores)
        {
            listaPeriodos = new UI_Combo();
            foreach (string periodo in lista)
            {
                this.listaPeriodos.AddItem(int.Parse(periodo), periodo);
            }
            this.Titulo = tit;
            listaPeriodos.SelectedValue = selected;

            this.ListaErrores = Errores;
        }
    }
}