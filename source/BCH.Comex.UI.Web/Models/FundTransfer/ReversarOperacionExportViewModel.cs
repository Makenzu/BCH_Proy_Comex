using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class ReversarOperacionExportViewModel : FundTransferViewModel
    {
        public string TipoAnuls { get; set; }
        public string   Tx_NroOpe_000 { get; set; }

        public string  Tx_NroOpe_001 { get; set; }
        public string Tx_Separador000 { get; set; }

        public string Tx_NroOpe_002 { get; set; }
        public string Tx_Separador001 { get; set; }

        public string  Tx_NroOpe_003 { get; set; }
        public string Tx_Separador002 { get; set; }

        public string Tx_NroOpe_004 { get; set; }
        public string Tx_Separador003 { get; set; }
       
        public string Tx_Cliente { get; set; }

        public int indexLi_Pln { set; get; }
        public List<SelectListItem> lst_Li_Pln { get; set; }
        public IEnumerable<string> selectedLi_Pln { get; set; }

        [Display(Name = "Planillas")]
        public UI_Combo CmbPlanilla { get; set; }

        [Display(Name = "Observacion")]
        public UI_TextBox Tx_Observacion { get; set; }
       
        public int index_TipAnu { set; get; }
        public UI_Combo Cb_TipAnu { set; get; }
        public UI_TextBox  CamTipCam { get; set; }
        
        public UI_Button BtnAceptar { get; set; }
        public UI_Button BtnCancelar { get; set; }
        public UI_Button BtnOkOperacion { set; get; }
        public UI_Button BtnOk { set; get; }
        public UI_Button BtnDeclaracion { get; set; }
        public UI_Button BtnObservacion { get; set; }
        public UI_ListBox listPln { get; set; }

        public UI_TextBox Numero { get; set; }
        public UI_TextBox Fecha { get; set; }
        

        public int index_SucursalBCCH { get; set; }
        public UI_Combo Cb_SucursalBCCH { get; set; }
        
        public UI_TextBox Motivo { get; set; }
 
        public int index_Tipo { get; set; }
        public UI_Combo Cb_Tipo { get; set; }
 
        public UI_TextBox Tx_TipCam { get; set; }
    
        public List<UI_Message> MensajesDeErrores { set; get; }

        public List<string> MensajesDeConfirmacion { set; get; }

        public bool AbrirReversarOperacionDeclaracion { get; set; }
        
        public string Redireccionar { get; set; }

        public ReversarOperacionExportViewModel()
        {
        }
        
        public T_AnuPl  Anuls { get; set; }

        public bool vieneDeMensaje { get; set; }
        public void Update(UI_Frmgrev frmState)
        {
            Update(frmState.Lt_Pln, this.CmbPlanilla);
            Update(frmState.Tx_TipCam, this.Tx_TipCam);
            Update(frmState.Cb_Tipo, this.Cb_Tipo);

        }

        private ReversarOperacionExportViewModel(UI_Frmgrev frmState)
        {
            #region UI_TextBox
            this.Tx_NroOpe_000 = frmState.Tx_NroOpe[0].Text;
            this.Tx_NroOpe_001 = frmState.Tx_NroOpe[1].Text;
            this.Tx_NroOpe_002 = frmState.Tx_NroOpe[2].Text;
            this.Tx_NroOpe_003 = frmState.Tx_NroOpe[3].Text;
            this.Tx_NroOpe_004 = frmState.Tx_NroOpe[4].Text;
            this.Tx_Cliente = frmState.Tx_Prty.Text;
            #endregion
            #region UI_ComboBox
            this.indexLi_Pln = -1;
            this.lst_Li_Pln = frmState.Lt_Pln.Items.Select((x => new SelectListItem()
            {
                Text = x.Value,
                Value = x.Data.ToString()
            })).ToList();
            this.Cb_TipAnu = frmState.CB_Tipanu;
            this.CamTipCam = frmState.CAM_Tipcam;
            this.index_SucursalBCCH = frmState.Cb_Pbc.get_ItemData_(frmState.Cb_Pbc.ListIndex);
            this.Cb_SucursalBCCH = frmState.Cb_Pbc;
            this.index_Tipo = frmState.Cb_Tipo.get_ItemData_(frmState.Cb_Tipo.ListIndex);
            this.Cb_Tipo = frmState.Cb_Tipo;

            #endregion
            #region UI_Boton
            this.BtnOkOperacion = frmState.Ok_Operacion;
            this.BtnAceptar = frmState.Co_Boton[0];
            this.BtnCancelar = frmState.Co_Boton[1];
            this.BtnOk = frmState.Ok;
            this.BtnDeclaracion = frmState.Bot_Dec;
            this.BtnObservacion = frmState.BOT_Obs;
            #endregion
            #region UI_ListBox
            CmbPlanilla = frmState.Lt_Pln;
            CmbPlanilla.Items.ForEach(x => x.Value = x.Value.Replace(" ", "\xA0").Replace("\t", "\xA0\xA0\xA0"));
            Tx_Observacion = frmState.Tx_ObsPln;
            #endregion
            #region Identificador
            this.Numero = frmState.Tx_NroPln;
            this.Fecha = frmState.Tx_Fecha;
            this.Motivo = frmState.Tx_Motivo;
            this.Tx_TipCam = frmState.Tx_TipCam;
            this.CamTipCam = frmState.CAM_Tipcam; 
            this.MensajesDeErrores = frmState.Errores;
            this.MensajesDeConfirmacion = frmState.PopUps.Where(x => x.Type == TipoMensaje.YesNo).Select(x => x.Text).ToList();
            this.AbrirReversarOperacionDeclaracion = frmState.AbrirReversarOperacionDeclaracion;
            #endregion
        }

        public ReversarOperacionExportViewModel(UI_Frmgrev frmState, T_AnuPl[] anuPlList)
            : this(frmState)
        {
            try
            {
                if (anuPlList[0] != null)
                    foreach (var item in anuPlList)
                    {
                        TipoAnuls += item.VisInv + ";";
                    }
            }
            catch (Exception e) { 
            } 
        }
    }
}
