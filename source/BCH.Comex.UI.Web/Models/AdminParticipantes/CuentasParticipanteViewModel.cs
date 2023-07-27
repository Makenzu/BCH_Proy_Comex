//using BCH.Comex.Common.XGPY.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGPY.Forms;
using System.ComponentModel.DataAnnotations;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;

namespace BCH.Comex.UI.Web.Models.AdminParticipantes
{
    public class CuentasParticipanteViewModel : AdminParticipantesViewModel
    {

        [Display(Name = "N° Cuenta / Moneda / Estado")]
        public UI_Combo Lista1 { get; set; }

        [Display(Name = "N° Cuenta / Oficina / Moneda / Estado")]
        public UI_Combo Lista2 { get; set; }
        public UI_Button aceptar { get; set; }
        public UI_Button cancelar { get; set; }
        public UI_Button guardar { get; set; }
        public UI_Combo Combo1 { get; set; }
        public UI_TextBox cuenta { get; set; }//UserControl

        public UI_Label Titulo { get; set; }
        public UI_TextBox Frame1 { get; set; }
        public UI_TextBox Frame2 { get; set; }

        public UI_Label Label1 { get; set; }
        public UI_Label Label2 { get; set; }
        public UI_Label Label3 { get; set; }
        public UI_Label Label4 { get; set; }
        public UI_Label Label5 { get; set; }
        public UI_Label Label6 { get; set; }
        public UI_Label Label7 { get; set; }
        public CuentasParticipanteViewModel(UI_PrtEnt08 PrtEnt08, InitializationObject initObj)
        {
            MensajesDeError = initObj.Mdi_Principal.MESSAGES;
            Lista1 = PrtEnt08.Lista1;
            Lista2 = PrtEnt08.Lista2;
            aceptar = PrtEnt08.aceptar;
            cancelar = PrtEnt08.cancelar;
            guardar = PrtEnt08.guardar;
            //Combo1 = PrtEnt08.Combo1;
            cuenta = PrtEnt08.cuenta;
            Titulo = PrtEnt08.Titulo;
            Label1 = PrtEnt08.Label1;
            Label2 = PrtEnt08.Label2;
            Label3 = PrtEnt08.Label3;
            Label4 = PrtEnt08.Label4;
            Label5 = PrtEnt08.Label5;
            Label6 = PrtEnt08.Label6;
            Label7 = PrtEnt08.Label7;
            Frame1 = PrtEnt08.Frame1;
            Frame2 = PrtEnt08.Frame2;
            //lista 1 
            Label1.Text = "N° Cuenta".PadRight(80, ' ').Replace(" ", "\xA0"); //50  130
            Label2 = PrtEnt08.Label2;
            Label3.Text = "Estado";

            //lista 2 
            Label4 = PrtEnt08.Label4;
            Label5.Text = "Moneda".PadRight(65, ' ').Replace(" ", "\xA0"); //55  85
            Label6.Text = "Estado";
            Label7.Text = "Oficina".PadRight(60, ' ').Replace(" ", "\xA0"); //50 80
        }
        public CuentasParticipanteViewModel()
        {
        }
        public void Update(UI_PrtEnt08 frm)
        {
            Update(frm.Lista1, this.Lista1);
            Update(frm.Lista2, this.Lista2);
            frm.aceptar = aceptar;
            frm.cancelar = cancelar;

        }

    }
}