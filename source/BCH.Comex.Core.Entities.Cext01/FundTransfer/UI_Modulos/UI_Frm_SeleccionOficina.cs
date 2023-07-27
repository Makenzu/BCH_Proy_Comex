using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_SeleccionOficina
    {
        public string Oficinas = "";
        public string[] nomsuc;
        public List<T_Suc> VjSuc;

        public UI_Combo Cb_Oficina;
        public UI_OptionItem opt_CVD;
        public UI_OptionItem opt_Comision;
        public UI_Button BAceptar { get; set; }
        /// <summary>
        /// Boton Cancelar
        /// </summary>
        public UI_Button BCancelar { get; set; }

        public UI_Frm_SeleccionOficina()
        {
            //nomsuc = new List<string>();
            VjSuc = new List<T_Suc>();

            Cb_Oficina = new UI_Combo();
            opt_CVD = new UI_OptionItem() { Name = "tipo", Selected = true };
            opt_Comision = new UI_OptionItem() { Name = "tipo"};
            BAceptar = new UI_Button();
            BCancelar = new UI_Button();
  
        }
    }
}
