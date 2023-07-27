
using System.Collections.Generic;
using System.Linq;
namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class CartaBancoCentralViewModel
    {
        public string Frase { get; set; }

        public List<BusquedaPlanillasInformadasItemViewModel> Planillas { get; set; }
    }
}
