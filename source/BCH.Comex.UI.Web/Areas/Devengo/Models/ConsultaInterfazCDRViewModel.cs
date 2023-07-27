using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.Devengo.Models
{
    public class ConsultaInterfazCDRViewModel : DevengoViewModel
    {
        public string titulo { get; set; }
        public UI_Combo listaPeriodos { get; set; }
        public UI_Combo listaDias { get; set; }
        public bool muestraFiltros { get; set; }
        public string txtRut { get; set; }

        public ConsultaInterfazCDRViewModel()
        {
            this.titulo = string.Empty;
            listaPeriodos = new UI_Combo();
            listaDias = new UI_Combo();
            muestraFiltros = false;
            txtRut = string.Empty;
        }

        public ConsultaInterfazCDRViewModel(bool cartera)
        {
            this.titulo = cartera ? "Cartera" : "Devengamiento";
            listaPeriodos = new UI_Combo();
            listaDias = new UI_Combo();
            this.muestraFiltros = cartera;
            txtRut = string.Empty;
        }

        public ConsultaInterfazCDRViewModel(bool cartera, IList<string> peridos)
        {
            this.titulo = cartera ? "Cartera" : "Devengamiento";
            listaPeriodos = new UI_Combo();
            foreach (var per in peridos)
            {
                listaPeriodos.AddItem(int.Parse(per), per);
            }

            listaDias = new UI_Combo();
            this.muestraFiltros = cartera;
            txtRut = string.Empty;
        }

        public ConsultaInterfazCDRViewModel(bool cartera, IList<string> peridos, IList<int> dias)
        {
            this.titulo = cartera ? "Cartera" : "Devengamiento";
            listaPeriodos = new UI_Combo();
            foreach (var per in peridos)
            {
                listaPeriodos.AddItem(int.Parse(per), per);
            }

            listaDias = new UI_Combo();
            foreach (var dia in dias)
            {
                listaDias.AddItem(dia, dia.ToString());
            }

            this.muestraFiltros = cartera;
            txtRut = string.Empty;
        }

    }
}