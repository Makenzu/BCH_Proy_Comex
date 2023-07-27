using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Custodia.ControlIntegral.UI_Modulos;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Models.ControlIntegral
{
    public class EReparoViewModel
    {
        //public UI_TextBox NombreCliente { get; set; }
        //public UI_TextBox MailEjecutivo { get; set; }
        public string NombreCliente { get; set; }
        public UI_Combo cmbDocName { get; set; }
        public string MailEjecutivo { get; set; }
        public string MailEjecutivoCorreo { get; set; }

        public string Frame1 { get; set; }
        [Display(Name = "FALTA FIRMA APODERADO")]
        public UI_CheckBox chkReparo_0 { get; set; }

        [Display(Name = "APODERADOS SIN FACULTADES SUFICIENTES PARA : ")]
        public UI_CheckBox chkReparo_1 { get; set; }

        [Display(Name = "Facultad 2 (Giro en cuenta corriente)")]
        public UI_CheckBox chkReparo_17 { get; set; }

        [Display(Name = "Facultad 17 (Adquirir bienes inmuebles)")]
        public UI_CheckBox chkReparo_18 { get; set; }

        [Display(Name = "Facultad 25 /27 (Efectuar operaciones de Cambio y Comex)")]
        public UI_CheckBox chkReparo_19 { get; set; }

        [Display(Name = "SE REQUIERE IDENTIFICAR CON NOMBRE Y RUT A LOS APODERADOS QUE FIRMAN.")]
        public UI_CheckBox chkReparo_2 { get; set; }
        [Display(Name = "IUV EN MANTENCION.")]
        public UI_CheckBox chkReparo_3 { get; set; }

        [Display(Name = "FIRMAS NO REGISTRADAS O NO DIGITALIZADAS.")]
        public UI_CheckBox chkReparo_4 { get; set; }

        [Display(Name = "OTROS")]
        public UI_CheckBox chkOtros_0 { get; set; }
        public UI_TextBox txtOtros_0 { get; set; }



        public string Frame2 { get; set; }
        [Display(Name = "CLIENTE NO AUTORIZADO A OPERAR POR FAX")]
        public UI_CheckBox chkReparo_5 { get; set; }

        [Display(Name = "BENEFICIARIO NO RECURRENTE. DEBE SOLICITAR CALL BACK")]
        public UI_CheckBox chkReparo_6 { get; set; }

        [Display(Name = "OTROS")]
        public UI_CheckBox chkOtros_1 { get; set; }
        public UI_TextBox txtOtros_1 { get; set; }


        public string Frame3 { get; set; }
        [Display(Name = "FECHA EMISIÓN ERRONEA O FALTANTE ")]
        public UI_CheckBox chkReparo_7 { get; set; }

        [Display(Name = "FALTA CUENTA ORIGEN/CARGO")]
        public UI_CheckBox chkReparo_8 { get; set; }

        [Display(Name = "FALTA CONCEPTO ORIGEN/DESTINO DE LAS DIVISAS O CÓDIGO BCCH INDICADO ES ERRONEO")]
        public UI_CheckBox chkReparo_9 { get; set; }

        [Display(Name = "OTROS")]
        public UI_CheckBox chkOtros_2 { get; set; }

        public UI_TextBox txtOtros_2 { get; set; }


        public string Frame4 { get; set; }
        [Display(Name = "FALTA REGISTRO DE CIERRE DE TIPO DE CAMBIO EN CVD")]
        public UI_CheckBox chkReparo_10 { get; set; }

        [Display(Name = "TIPO DE CAMBIO NO ES COINCIDENTE ENTRE CARTA Y REGISTRO CVD")]
        public UI_CheckBox chkReparo_11 { get; set; }

        [Display(Name = "RUT DE REGISTRO EN CVD NO CORRESPONDE")]
        public UI_CheckBox chkReparo_12 { get; set; }

        [Display(Name = "CIERRE CON DESFASE MAYOR A 48 HORAS")]
        public UI_CheckBox chkReparo_13 { get; set; }

        [Display(Name = "OTROS")]
        public UI_CheckBox chkOtros_3 { get; set; }
        public UI_TextBox txtOtros_3 { get; set; }

        public string Frame5 { get; set; }
        [Display(Name = "REPARO")]
        public UI_CheckBox chkReparo_14 { get; set; }

        [Display(Name = "CUENTA CORRIENTE SIN FONDOS SUFICIENTES PARA REALIZAR OPERACIÓN")]
        public UI_CheckBox chkReparo_15 { get; set; }

        [Display(Name = "PARTICIPANTE NO CREADO EN BASE COMEX.")]
        public UI_CheckBox chkReparo_16 { get; set; }

        [Display(Name = "OTROS")]
        public UI_CheckBox chkOtros_4 { get; set; }
        public UI_TextBox txtOtros_4 { get; set; }

        public EReparoViewModel()
        {


        }
        public EReparoViewModel(UI_frmEReparo frmState)
        {

            //this.NombreCliente = frmState.txtNomCli;
            //this.MailEjecutivo = frmState.txtMailEjec;

            this.Frame1 = "ATRIBUCIONES DE FIRMA EN IUV:";
            this.cmbDocName = frmState.cmbDocName;
            this.chkReparo_0 = frmState.chkReparo[0];
            this.chkReparo_1 = frmState.chkReparo[1];
            this.chkReparo_17 = frmState.chkReparo[17];
            this.chkReparo_18 = frmState.chkReparo[18];
            this.chkReparo_19 = frmState.chkReparo[19];
            this.chkReparo_2 = frmState.chkReparo[2];
            this.chkReparo_3 = frmState.chkReparo[3];
            this.chkReparo_4 = frmState.chkReparo[4];
            this.chkOtros_0 = frmState.chkOtros[0];
            this.txtOtros_0 = frmState.txtOtros[0];

            this.Frame2 = "[ MIFT / SVS ]";
            this.chkReparo_5 = frmState.chkReparo[5];
            this.chkReparo_6 = frmState.chkReparo[6];
            this.chkOtros_1 = frmState.chkOtros[1];
            this.txtOtros_1 = frmState.txtOtros[1];

            this.Frame3 = "[ ESTRUCTURA DE CARTA: ]";
            this.chkReparo_7 = frmState.chkReparo[7];
            this.chkReparo_8 = frmState.chkReparo[8];
            this.chkReparo_9 = frmState.chkReparo[9];
            this.chkOtros_2 = frmState.chkOtros[2];
            this.txtOtros_2 = frmState.txtOtros[2];

            this.Frame4 = " [ CIERRE TIPO DE CAMBIO: ]";
            this.chkReparo_10 = frmState.chkReparo[10];
            this.chkReparo_11 = frmState.chkReparo[11];
            this.chkReparo_12 = frmState.chkReparo[12];
            this.chkReparo_13 = frmState.chkReparo[13];
            this.chkOtros_3 = frmState.chkOtros[3];
            this.txtOtros_3 = frmState.txtOtros[3];

            this.Frame5 = "[ OTROS REPAROS: ]";
            this.chkReparo_14 = frmState.chkReparo[14];
            this.chkReparo_15 = frmState.chkReparo[15];
            this.chkReparo_16 = frmState.chkReparo[16];
            this.chkOtros_4 = frmState.chkOtros[4];
            this.txtOtros_4 = frmState.txtOtros[4];


        }




    }
}