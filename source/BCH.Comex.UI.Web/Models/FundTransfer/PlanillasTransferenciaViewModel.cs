using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class PlanillasTransferenciaViewModel : FundTransferViewModel
    {

        public UI_Combo Cbo_Moneda { get; set; }
        public UI_Combo Cbo_CptoPln { get; set; }
        public UI_Combo CbPais { get; set; }
        public UI_TextBox Lb_Saldo { get; set; }
        public UI_TextBox Tx_Monto { get; set; }
        public UI_ListBox ListaPlanillas { get; set; }
        public UI_Button Bt_OK { get; set; }
        public UI_Button Bt_Eliminar { get; set; }
        public UI_Button Bt_Aceptar { get; set; }
        public UI_Button Bt_Cancelar { get; set; }

        public List<string> _MensajesDeError { set; get; }
 
        public PlanillasTransferenciaViewModel()
        {

        }

        public PlanillasTransferenciaViewModel(UI_Frm_ChVrf frm, List<UI_Message> errores)
        {
            Cbo_Moneda =  frm.Cbo_Moneda;
            Cbo_CptoPln = frm.Cbo_CptoPln;
            CbPais = frm.CbPais;
            Lb_Saldo = frm.Lb_Saldo;
            Tx_Monto = frm.Tx_Monto;
            ListaPlanillas = frm.ListaPlanillas;
            ListaPlanillas.Items.ForEach(x => x.Value = x.Value.Replace(" ", "\xA0").Replace("\t", "\xA0\xA0\xA0"));
            Bt_OK = frm.Bt_OK;
            Bt_Eliminar = frm.Bt_Eliminar;
            Bt_Aceptar = frm.Bt_Aceptar;
            Bt_Cancelar = frm.Bt_Cancelar;
        }

        internal void Update(UI_Frm_ChVrf frm)
        {
            frm.Cbo_Moneda = Cbo_Moneda;
            frm.Cbo_CptoPln = Cbo_CptoPln;
            frm.CbPais = CbPais;
            frm.Lb_Saldo = frm.Lb_Saldo != null ? frm.Lb_Saldo : frm.Lb_Saldo = new UI_TextBox(); 
            frm.Tx_Monto = Tx_Monto;
            frm.ListaPlanillas = frm.ListaPlanillas != null ? frm.ListaPlanillas: frm.ListaPlanillas = new UI_ListBox();
        }
    }
}