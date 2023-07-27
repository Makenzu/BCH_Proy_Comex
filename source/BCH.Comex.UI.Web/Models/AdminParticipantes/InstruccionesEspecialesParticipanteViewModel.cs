//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGPY.Forms;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
//using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.AdminParticipantes
{
    public class InstruccionesEspecialesParticipanteViewModel : AdminParticipantesViewModel
    {


        //public List<SelectListItem> Cb_Autor { get; set; }
        //public int SelAutor { get; set; }
        // public List<SelectListItem> cb_Instrucciones { get; set; }
        public UI_Combo CmbMemo { get; set; }
        //public int IdInstrucciones { get; set; }
        public UI_Button Aceptar { get; set; }
        public UI_Button Cancelar { get; set; }

        public UI_TextBox prtinstruc { get; set; }
        //public string Tx_Instrucciones { get; set; }

        public InstruccionesEspecialesParticipanteViewModel()
        {



        }

        public InstruccionesEspecialesParticipanteViewModel(UI_PrtEnt04 PrtEnt04)
        {
            Aceptar = PrtEnt04.aceptar;
            Cancelar = PrtEnt04.cancelar;
            //this.cb_Instrucciones = frmState.CmbMemo;
            //this.Tx_Instrucciones = frmState.prtinstruc.Text;

            prtinstruc = PrtEnt04.prtinstruc;
            CmbMemo = PrtEnt04.CmbMemo;
            //this.cb_Instrucciones = new List<SelectListItem>();
            //cb_Instrucciones = frmState.CmbMemo.Items.Select(x => new SelectListItem
            //{
            //    Text = x.Value,
            //    Value = x.Data.ToString()
            //}).ToList();

            //if (frmState.CmbMemo.ListIndex > 0)
            //    cb_Instrucciones[frmState.CmbMemo.ListIndex].Selected = true;

        }

        public void Update(UI_PrtEnt04 frm)
        {

            //frm.Tx_Fecha.Text = this.Tx_Fecha;            
            Update(frm.CmbMemo, this.CmbMemo);
            frm.prtinstruc = prtinstruc;
            //Update(frm.prtinstruc.Text, this.Tx_Instrucciones);
            //frm.Tx_Plnv[39].Text = this.Tx_Observacion;
            // Update(frm.Tx_Plnv[39], this.Tx_Observacion);

        }


    }
}