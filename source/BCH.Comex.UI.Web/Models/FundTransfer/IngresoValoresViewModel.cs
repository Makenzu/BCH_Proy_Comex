using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class IngresoValoresViewModel
    {
        public string Caption { set; get; }
        public string Moneda { set; get; }
        public string Fecha { set; get; }
        [Required]
        public string Paridad { set; get; }
        [Required]
        public string TipoCambio { set; get; }

        public IngresoValoresViewModel()
        {
            this.Caption = UI_Frm_Ingreso_Valores.Caption;
        }
    }
}