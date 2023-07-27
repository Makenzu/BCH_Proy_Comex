using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_ChVrf
    {
        public UI_Frame Frame2 { get; set; }
        public UI_Button Bt_Aceptar { get; set; }
        public UI_Button Bt_Cancelar { get; set; }
        public UI_Frame Frame1 { get; set; }
        public UI_Button Bt_OK { get; set; }
        public UI_Button Bt_Eliminar { get; set; }
        public UI_Combo Cbo_CptoPln { get; set; }
        public UI_ListBox ListaPlanillas { get; set; }
        public UI_TextBox Tx_Monto { get; set; }
        public UI_Combo CbPais { get; set; }
        public UI_Combo Cbo_Moneda { get; set; }
        public UI_TextBox Lb_Saldo { set; get; }

        public short IndVgChV;
        public string FtoSal = "";
        public short ModificaPln;
        public short EjecProcMon;



        public UI_Frm_ChVrf()
        {
            Cbo_Moneda = new UI_Combo();
            Cbo_CptoPln = new UI_Combo();
            CbPais = new UI_Combo();
            Tx_Monto = new UI_TextBox();
            ListaPlanillas = new UI_ListBox() { Enabled = true };
            Bt_OK = new UI_Button();
            Bt_Eliminar = new UI_Button();
            Bt_Aceptar = new UI_Button() { Text="Aceptar" };
            Bt_Cancelar = new UI_Button() { Text="Cancelar"};
            Lb_Saldo = new UI_TextBox();

            Frame2 = new UI_Frame();
            Frame2.Controles.Add(Bt_Aceptar);
            Frame2.Controles.Add(Bt_Cancelar);

            Frame1 = new UI_Frame();
            Frame1.Controles.Add(Bt_OK);
            Frame1.Controles.Add(Bt_Eliminar);
            Frame1.Controles.Add(Cbo_CptoPln);
            Frame1.Controles.Add(ListaPlanillas);
            Frame1.Controles.Add(Tx_Monto);
            Frame1.Controles.Add(CbPais);
            Frame1.Controles.Add(Cbo_Moneda);
            Frame1.Controles.Add(Lb_Saldo);
        }

    }
}
