using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class ComisionesViewModel : FundTransferViewModel
    {

        public List<UI_Label> Lb_gcom_ { get; set; }
        public UI_Grid Ls_Com { get; set; }

        [Display(Name = "Descripción de Comisión")]
        public UI_TextBox Tx_Com_0 { get; set; }

        [Display(Name = "Monto en M/E")]
        public UI_TextBox Tx_Com_1 { get; set; }

        [Display(Name = "Tipo de Cambio")]
        public UI_TextBox Tx_Com_2 { get; set; }

        [Display(Name = "Monto Total en $")]
        public UI_TextBox Tx_Com_3 { get; set; }

        [Display(Name = "Nem Contable")]
        public UI_TextBox Tx_Com_4 { get; set; }

        [Display(Name = "Descripción Nemónico Cuenta Contable")]
        public UI_TextBox Tx_Com_5 { get; set; }
        //   public UI_CheckBox Ch_com { get; set; }
        [Display(Name = "I.V.A.")]
        public UI_CheckBox Ch_com { get; set; } //Aplica Iva
        // public UI_CheckBox Ch_com { get; set; } //Aplica Iva
        public UI_Button OK_com { get; set; }
        public UI_Button NO_com { get; set; }
        public UI_Button Cm_com_0 { get; set; }
        public UI_Button Cm_com_1 { get; set; }
        [Display(Name = "Nombre Comisión/NemMnd/Monto en US$")]
        public UI_ListBox CmbComisiones { get; set; }


        public ComisionesViewModel()
        {


        }

        public void Update(UI_Frm_Comisiones frmState)
        {
            Update(frmState.Ls_com, this.CmbComisiones);
            Update(frmState.Tx_Com[0], this.Tx_Com_0);
            Update(frmState.Tx_Com[1], this.Tx_Com_1);
            Update(frmState.Tx_Com[2], this.Tx_Com_2);
            Update(frmState.Tx_Com[3], this.Tx_Com_3);
            Update(frmState.Tx_Com[4], this.Tx_Com_4);
            Update(frmState.Tx_Com[5], this.Tx_Com_5);
            frmState.Ch_com.Checked = this.Ch_com.Checked;
            // Update(frmState.Ch_com, this.Ch_com);
        }

        public ComisionesViewModel(UI_Frm_Comisiones frmState)
        {
            Tx_Com_0 = frmState.Tx_Com[0];
            Tx_Com_1 = frmState.Tx_Com[1];
            Tx_Com_2 = frmState.Tx_Com[2];
            Tx_Com_3 = frmState.Tx_Com[3];
            Tx_Com_4 = frmState.Tx_Com[4];
            Tx_Com_5 = frmState.Tx_Com[5];
            OK_com = frmState.OK_com;
            NO_com = frmState.NO_com;
            Cm_com_0 = frmState.Cm_com_[0];
            Cm_com_1 = frmState.Cm_com_[1];
            CmbComisiones = frmState.Ls_com;
            CmbComisiones.Items.ForEach(x => x.Value = x.Value.Replace(" ", "\xA0").Replace("\t", "\xA0\xA0\xA0"));
            Ch_com = frmState.Ch_com;

        }


        public ComisionesViewModel(UI_Frm_Comisiones frmState, List<UI_Message> errores)
        {
            Tx_Com_0 = frmState.Tx_Com[0];
            Tx_Com_1 = frmState.Tx_Com[1];
            Tx_Com_2 = frmState.Tx_Com[2];
            Tx_Com_3 = frmState.Tx_Com[3];
            Tx_Com_4 = frmState.Tx_Com[4];
            Tx_Com_5 = frmState.Tx_Com[5];
            OK_com = frmState.OK_com;
            NO_com = frmState.NO_com;
            Cm_com_0 = frmState.Cm_com_[0];
            Cm_com_1 = frmState.Cm_com_[1];
            CmbComisiones = frmState.Ls_com;
            CmbComisiones.Items.ForEach(x => x.Value = x.Value.Replace(" ", "\xA0").Replace("\t", "\xA0\xA0\xA0"));
            Ch_com = frmState.Ch_com;

            MensajesDeError = errores;
        }

    }
}