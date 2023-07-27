//using BCH.Comex.Common.XGPY.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGPY.Forms;
using System.ComponentModel.DataAnnotations;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;

namespace BCH.Comex.UI.Web.Models.AdminParticipantes
{
    public class DetalleCuentasViewModel : AdminParticipantesViewModel
    {
        [Display(Name = " N° Cuenta")]
        public UI_Combo cbo_cta { get; set; }

        [Display(Name = " Moneda")]
        public UI_Combo Combo1 { get; set; } //Moneda

        [Display(Name = "Activa SCE")]
        public UI_CheckBox prtactiva_1 { get; set; }

        [Display(Name = "Activa Banco")]
        public UI_CheckBox prtactiva_0 { get; set; }

        [Display(Name = "Cuenta Bae")]
        public UI_CheckBox CuentaBae { get; set; }

        [Display(Name = "Cuenta Cheque")]
        public UI_CheckBox especial { get; set; }

        [Display(Name = "Oficina")]
        public UI_TextBox oficina { get; set; }
        public UI_Button AceptarDetalle { get; set; }
        public UI_Button CancelarDetalle { get; set; }
        public UI_Button EliminarDetalle { get; set; }
        public UI_Combo listmon { get; set; }
        public UI_TextBox prtcuenta { get; set; } //UserControl
        public UI_Label txtTitulo { get; set; }

        public UI_Label Label1 { get; set; }
       // public string Label1 { get; set; }

        public int idEstadoMensaje { get; set; }

        public DetalleCuentasViewModel(UI_PrtEnt10 PrtEnt10, InitializationObject initObj)
        {
            MensajesDeError = initObj.Mdi_Principal.MESSAGES;
            cbo_cta = PrtEnt10.cbo_cta;
            Combo1 = PrtEnt10.Combo1;
            prtactiva_1 = PrtEnt10._prtactiva_1;
            prtactiva_0 = PrtEnt10._prtactiva_0;
            CuentaBae = PrtEnt10.CuentaBae;
            especial = PrtEnt10.especial;
            oficina = PrtEnt10.oficina;
            AceptarDetalle = PrtEnt10.aceptar;
            CancelarDetalle = PrtEnt10.cancelar;
            EliminarDetalle = PrtEnt10.Eliminar;
            listmon = PrtEnt10.listmon;
            prtcuenta = PrtEnt10.prtcuenta;
            txtTitulo = PrtEnt10.txtTitulo;
            Label1 = PrtEnt10.Label1;
            idEstadoMensaje = PrtEnt10.idEstadoMensaje;
        }

        public DetalleCuentasViewModel()
        {
        }

        public void Update(UI_PrtEnt10 frm)
        {
            Update(frm.cbo_cta, this.cbo_cta);
            Update(frm.Combo1, this.Combo1);
           // Update(frm.Combo1, this.Combo1);
            //frm._prtactiva_1 = _prtactiva_1;
            //frm._prtactiva_0 = _prtactiva_0;
            //frm.CuentaBae = CuentaBae;
            //frm.especial = especial;
            //Update(frm.oficina, this.oficina);
            //frm.aceptar = aceptar;
            //frm.cancelar = cancelar;
            //frm.Eliminar = Eliminar;
            //Update(frm.listmon, this.listmon);
            //frm.prtcuenta = prtcuenta;
            //frm.txtTitulo = txtTitulo;
            Update(frm._prtactiva_1, this.prtactiva_1);
            Update(frm._prtactiva_0, this.prtactiva_0);
            Update(frm.CuentaBae, this.CuentaBae);
            Update(frm.especial, this.especial);           
            Update(frm.oficina, this.oficina);
            //frm.aceptar = AceptarDetalle;
            //frm.cancelar = CancelarDetalle;
            //frm.Eliminar = EliminarDetalle;           
            Update(frm.listmon, this.listmon);
            //Update(frm.prtcuenta, this.prtcuenta);
            frm.prtcuenta.Text = frm.cbo_cta.Text;
            frm.txtTitulo = txtTitulo;
            frm.Label1 = Label1;            
        }
    }
}