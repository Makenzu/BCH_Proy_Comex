using BCH.Comex.Common.UI_Modulos;
using System;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Ingreso_Valores
    {
        public UI_TextBox Moneda { set; get; }
        public UI_TextBox Fecha { set; get; }
        public UI_TextBox Paridad { set; get; }
        public UI_TextBox TC_Obs { set; get; }

        public const string Caption = "Ingreso Manual Valores Moneda";
        public string VieneDe = String.Empty;
        public string MensageCancelacion = String.Empty;

        public UI_Frm_Ingreso_Valores()
        {
            Moneda = new UI_TextBox();
            Fecha = new UI_TextBox();
            Paridad = new UI_TextBox();
            TC_Obs = new UI_TextBox();
        }
    }
}
