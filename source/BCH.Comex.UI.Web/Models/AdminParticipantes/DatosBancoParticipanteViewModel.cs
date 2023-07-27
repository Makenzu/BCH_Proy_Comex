//using BCH.Comex.Common.XGPY.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;

namespace BCH.Comex.UI.Web.Models.AdminParticipantes
{
    public class DatosBancoParticipanteViewModel : AdminParticipantesViewModel
    {
        [Display(Name = "Código")]
        public UI_TextBox Tx_Codigo { get; set; }

        [Display(Name = "Dirección Swiff")]
        public UI_TextBox Tx_DireccionSwiff { get; set; }

        [Display(Name = "RUT")]
        public UI_TextBox Tx_Rut { get; set; }

        [Display(Name = "Aladi")]
        public UI_CheckBox ch_Aladi { get; set; }

        [Display(Name = "Plaza Aladi")]
        public UI_TextBox Tx_PlazaAladi { get; set; }

        [Display(Name = "Ejecutivo Corresponsal")]
        public UI_TextBox Tx_EjecutivoCorresponsal { get; set; }

        //Tipo Banco

        [Display(Name = "Acreedor")]
        public UI_CheckBox ch_Acreedor { get; set; }

        [Display(Name = "Corresponsal")]
        public UI_CheckBox ch_Corresponsal { get; set; }

        [Display(Name = "Avisador")]
        public UI_CheckBox ch_Avisador { get; set; }      

        //Tasa Refinanciamiento
        public List<UI_OptionItem> prtTasaRefinanciamiento { get; set; }
        public int SelectedTasaRefinanciamiento { get; set; }

        [Display(Name = "Spread")]
        public UI_TextBox Tx_SpreadTasaRefinanciamiento { get; set; }

        [Display(Name = "Clasificación")]
        public UI_Combo cb_Clasificacion { get; set; }
        public UI_Button BotonAceptar { get; set; }
        public UI_Button BotonCancelar { get; set; }

        public DatosBancoParticipanteViewModel()
        {


        }

        public DatosBancoParticipanteViewModel(UI_PrtEnt11 frmState, InitializationObject initObj)
        {
            this.Tx_Codigo = frmState.prtcodigo;
            this.Tx_DireccionSwiff = frmState.prtswif;
            this.Tx_Rut = frmState.prtrut;
            this.ch_Aladi = frmState.prtaladi;
            this.Tx_PlazaAladi = frmState.prtplaza;
            this.Tx_EjecutivoCorresponsal = frmState.ejecorr;

            //Tipo Banco
            this.ch_Acreedor = frmState._prttipob_[0];
            this.ch_Corresponsal = frmState._prttipob_[1];
            this.ch_Avisador = frmState._prttipob_[2];          
            this.Tx_EjecutivoCorresponsal = frmState.ejecorr;
            //Tasa de Refinanciamiento 
            this.prtTasaRefinanciamiento = frmState._prttasa_;
            this.SelectedTasaRefinanciamiento = int.Parse(frmState._prttasa_.First(x => x.Selected).ID);
            Tx_SpreadTasaRefinanciamiento = frmState.prtspread;
            //Clasificacion
            this.cb_Clasificacion = frmState.Combo1;
            //Botones
            this.BotonAceptar = frmState.aceptar;
            this.BotonCancelar = frmState.cancelar;



        }


        public void Update(UI_PrtEnt11 frmState)
        {
            //for (int i = 0; i < frmState._prttasa_.Count; i++)
            //{
            //    frmState._prttasa_[i].Selected = i == (this.SelectedTasaRefinanciamiento - 1);
            //}

            Update(frmState.prtcodigo, this.Tx_Codigo);
            Update(frmState.prtrut, this.Tx_Rut);
            Update(frmState.ejecorr, this.Tx_EjecutivoCorresponsal);
            Update(frmState.prtspread, this.Tx_SpreadTasaRefinanciamiento);
            Update(frmState.prtplaza, this.Tx_PlazaAladi);
            //Update(frmState.Combo1, this.cb_Clasificacion);
            //for (int i = 0; i < frmState._prttipob_.Count; i++)
            //{
            //    frmState._prttipob_[i].Selected = i == (this.CheckedTipoBanco - 1);
            //}

            //frmState.aceptar = this.BotonAceptar;
            //frmState.cancelar = this.BotonCancelar;
        }




    }
}