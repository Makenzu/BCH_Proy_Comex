
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Common.UI_Modulos;
using System.ComponentModel.DataAnnotations;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;

namespace BCH.Comex.UI.Web.Models.AdminParticipantes
{
    public class ModalTasaEspecialViewModel : AdminParticipantesViewModel
    {

        [Display(Name = "Manual de Tarifas ")]
        public UI_CheckBox manual { get; set; }

        [Display(Name = "A partir del...")]
        public UI_TextBox fecha { get; set; }

        [Display(Name = "Monto Fijo")]
        public UI_CheckBox fijo { get; set; }
        [Display(Name = "Tasa")]
        public UI_TextBox tasa { get; set; }
        [Display(Name = "Monto hasta")]
        public UI_TextBox monto { get; set; }
        [Display(Name = "Monto Mínimo")]
        public UI_TextBox minimo { get; set; }
        [Display(Name = "Monto Máximo")]
        public UI_TextBox maximo { get; set; }
        public UI_Button aceptar { get; set; }
        public UI_Button cancelar { get; set; }
        public UI_TextBox Lb_fecing { get; set; } //Hidden

        public UI_Frame Frame1 { get; set; }
        public int idEstadotasa { get; set; }
        public int idEstadoMonto { get; set; }


        public ModalTasaEspecialViewModel()
        {


        }
        public ModalTasaEspecialViewModel(UI_PrtEnt13 frmState, InitializationObject initObj)
        {
            MensajesDeError = initObj.Mdi_Principal.MESSAGES;
            this.manual = frmState.manual;
            this.fecha = frmState.fecha;
            this.fijo = frmState.fijo;
            this.tasa = frmState.tasa;
            this.monto = frmState.monto;
            this.minimo = frmState.minimo;
            this.maximo = frmState.maximo;
            this.aceptar = frmState.aceptar;
            this.cancelar = frmState.cancelar;
            this.Lb_fecing = frmState.Lb_fecing;
            this.Frame1 = frmState.Frame1;
            this.idEstadotasa = frmState.idEstadotasa;
            this.idEstadoMonto = frmState.idEstadoMonto;
        }

        public void Update(UI_PrtEnt13 frm)
        {

            Update(frm.manual, this.manual);
            Update(frm.fecha, this.fecha);
            Update(frm.fijo, this.fijo);
            Update(frm.tasa, this.tasa);
            Update(frm.monto, this.monto);
            Update(frm.minimo, this.minimo);
            Update(frm.maximo, this.maximo);
            frm.aceptar = aceptar;
            frm.cancelar = cancelar;
            frm.idEstadotasa = this.idEstadotasa;
            frm.idEstadoMonto = this.idEstadoMonto; 
        }

    }
}