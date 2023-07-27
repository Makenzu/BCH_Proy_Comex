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
    public class CaracteristicasParticipanteViewModel : AdminParticipantesViewModel
    {
        [Display(Name = "Rut")]
        public UI_TextBox Tx_Rut { get; set; }
        //public bool ClienteComex { get; set; }
        //public bool ClienteBanco { get; set; }
        [Display(Name = "Ofic.Ejecutivo")]
        public UI_Combo cboOficina { get; set; }

        [Display(Name = "Ofic.Ejecutivo")]
        public UI_TextBox oficina { get; set; }

        [Display(Name = "Ejecutivo")]
        public UI_Combo cbEjecutivo { get; set; }

        [Display(Name = "Actividad Económica")]
        public UI_Combo cbActividadEconomico { get; set; }

        [Display(Name = "Clase Riesgo")]
        public UI_Combo cbClaseRiesgo { get; set; }

        [Display(Name = "Clasificación")]
        public UI_Combo cbClasificacion { get; set; }

        public UI_Button BtnAceptar { get; set; }
        public UI_Button BtnCancelar { get; set; }

        //Importacion 
        public UI_Combo CbCenCosImportacion { get; set; } //Cbo_CenCosImp
        public UI_Combo CbEspecImportacion { get; set; } //Cbo_EspecImp
        public UI_Button BtnIngresoImportacion { get; set; }
        public UI_Button BtnEliminarImportacion { get; set; }

        [Display(Name = "De Importación")]
        public UI_TextBox Tx_Importacion { get; set; }

        //Exportacion
        public UI_Combo CbCenCosExportacion { get; set; }  //Cbo_CenCosExp
        public UI_Combo CbEspecExportacion { get; set; } //Cbo_EspecExp
        public UI_Button BtnIngresoExportacion { get; set; }
        public UI_Button BtnEliminarExportacion { get; set; }
        [Display(Name = "De Exportación")]
        public UI_TextBox Tx_Exportacion { get; set; }

        //Negocio
        public UI_Combo CbCenCosNegocio { get; set; } //Cbo_CenCosNeg
        public UI_Combo CbEspecNegocio { get; set; } //Cbo_EspecNeg
        public UI_Button BtnIngresoNegocio { get; set; }
        public UI_Button BtnEliminarNegocio { get; set; }
        [Display(Name = "De Negocios")]
        public UI_TextBox Tx_Negocio { get; set; }

        public List<UI_OptionItem> prtcliente { get; set; }
        public int SelectedCliente { get; set; }

        public UI_TextBox actividad { get; set; } //Hidden



        public CaracteristicasParticipanteViewModel()
        {

        }

        public CaracteristicasParticipanteViewModel(UI_PrtEnt07 frmState, InitializationObject initObj)
        {
            MensajesDeError = initObj.Mdi_Principal.MESSAGES;
            this.Tx_Rut = frmState.prtrut;
            this.oficina = frmState.oficina;
            this.cboOficina = frmState.cboOficina;

            this.prtcliente = frmState.prtcliente;
            this.SelectedCliente = int.Parse(frmState.prtcliente.First(x => x.Selected).ID);


            this.cbEjecutivo = frmState.ejecutivo;
            this.cbActividadEconomico = frmState.Combo2;
            this.cbClaseRiesgo = frmState.Combo4;
            this.cbClasificacion = frmState.Combo1;

            this.BtnAceptar = frmState.aceptar;
            this.BtnCancelar = frmState.cancelar;
            //Importacion 
            this.CbCenCosImportacion = frmState.Cbo_CenCosImp;
            this.CbEspecImportacion = frmState.Cbo_EspecImp;
            this.BtnIngresoImportacion = frmState.Bot_IngImp;
            this.BtnEliminarImportacion = frmState.Bot_EliImp;
            this.Tx_Importacion = frmState.Txt_Imp;
            //Exportacion 
            this.CbCenCosExportacion = frmState.Cbo_CenCosExp;
            this.CbEspecExportacion = frmState.Cbo_EspecExp;
            this.BtnIngresoExportacion = frmState.Bot_IngExp;
            this.BtnEliminarExportacion = frmState.Bot_EliExp;
            this.Tx_Exportacion = frmState.Txt_Exp;
            //Negocio
            this.CbCenCosNegocio = frmState.Cbo_CenCosNeg;
            this.CbEspecNegocio = frmState.Cbo_EspecNeg;
            this.BtnIngresoNegocio = frmState.Bot_EliNeg;
            this.BtnEliminarNegocio = frmState.Bot_EliNeg;
            this.Tx_Negocio = frmState.Txt_Negocios;


        }


        public void Update(UI_PrtEnt07 frmState)
        {
            Update(frmState.oficina, this.oficina);
            Update(frmState.cboOficina, this.cboOficina);
            Update(frmState.Txt_Imp, this.Tx_Importacion);
            Update(frmState.Txt_Exp, this.Tx_Exportacion);
            Update(frmState.Txt_Negocios, this.Tx_Negocio);
            Update(frmState.ejecutivo, this.cbEjecutivo);
            Update(frmState.Combo2, this.cbActividadEconomico);
            Update(frmState.Combo4, this.cbClaseRiesgo);
            Update(frmState.Combo1, this.cbClasificacion);
            Update(frmState.Cbo_EspecImp, this.CbEspecImportacion);
            Update(frmState.Cbo_EspecExp, this.CbEspecExportacion);
            Update(frmState.Cbo_EspecNeg, this.CbEspecNegocio);
        }

    }
}