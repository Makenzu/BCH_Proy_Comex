using BCH.Comex.Common.UI_Modulos;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Principal
    {
        public IList<UI_ElementoLista_Frm_Principal> Lt_CI { set; get; }
        public IList<UI_ElementoLista_Frm_Principal> Lt_CVE { set; get; }
        public IList<UI_ElementoLista_Frm_Principal> Lt_CVI { set; get; }
        public string Caption { set; get; }
        public UI_TextBox Tx_MtoOri { set; get; }
        public UI_TextBox Tx_NomPrt { set; get; }

        public UI_TextBox Tx_tipo { set; get; }
        public UI_TextBox Tx_NroFac { set; get; }
        public UI_TextBox Tx_RefCli { set; get; }
        public UI_TextBox Tx_neto { set; get; }
        public UI_TextBox Tx_moneda { set; get; }
        public UI_TextBox Tx_iva { set; get; }
        public UI_TextBox Num_Op { set; get; }



        public UI_Frm_Principal()
        {
            Caption = String.Empty;
            Lt_CI = new List<UI_ElementoLista_Frm_Principal>();
            Lt_CVE = new List<UI_ElementoLista_Frm_Principal>();
            Lt_CVI = new List<UI_ElementoLista_Frm_Principal>();
            Tx_MtoOri = new UI_TextBox() { Enabled = false };
            Tx_NomPrt = new UI_TextBox() { Enabled = false };
            Tx_tipo = new UI_TextBox() { Enabled = false };
            Tx_NroFac = new UI_TextBox() { Enabled = false };
            Tx_RefCli = new UI_TextBox();
            Tx_neto = new UI_TextBox() { Enabled = false };
            Tx_moneda = new UI_TextBox() { Enabled = false };
            Tx_iva = new UI_TextBox() { Enabled = false };
            Num_Op = new UI_TextBox() { Enabled = false };
        }
    }

    public class UI_ElementoLista_Frm_Principal
    {
        public string Texto { set; get; }
        
        public string Operacion { set; get; }
        public string Mnd_Compra { set; get; }
        public string Mto_Compra { set; get; }
        public string Mnd_Venta { set; get; }
        public string Mto_Venta { set; get; }
        public string Monto { set; get; }
        public string Moneda { set; get; }
    }
}
