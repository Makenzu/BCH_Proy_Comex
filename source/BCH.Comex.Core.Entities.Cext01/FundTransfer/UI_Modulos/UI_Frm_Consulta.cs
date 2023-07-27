using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Consulta
    {
        public List<pro_sce_prty_s04_MS_Result> items = new List<pro_sce_prty_s04_MS_Result>();
        public UI_Button img_Buscar;
        public UI_Button lbl_exportar;
        public UI_Button lbl_limpiar;
        public UI_Button lbl_Cerrar;
        public UI_Button img_Pare;

        public UI_TextBox txt_folio;
        public UI_Combo cmb_estado;
        public UI_Label txt_tot_oper;

        public UI_Frm_Consulta()
        {
            img_Buscar = new UI_Button();
            lbl_exportar = new UI_Button();
            lbl_limpiar = new UI_Button();
            lbl_Cerrar = new UI_Button();
            img_Pare = new UI_Button();

            cmb_estado = new UI_Combo();
            txt_tot_oper = new UI_Label();
            txt_folio = new UI_TextBox();
        }
    }
}
