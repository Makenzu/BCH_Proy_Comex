
namespace BCH.Comex.UI.Web.Models.ControlIntegral
{
    public class IndexViewModel
    {
        public string txtCuenta { get; set; }
        public string txtRut { get; set; }
        public string txtDV { get; set; }
        public string txtBase { get; set; }
        public string lblNombreClte { get; set; }
        public string lblEjecutivo { get; set; }
        public string lblSegmento { get; set; }

        public string HiNombreClte { get; set; } //Hidden
        public string HiEjecutivo { get; set; }//Hidden
        public string HiSegmento { get; set; }//Hidden
        
        public bool opTipo1 { get; set; }
        public string txtCuentab { get; set; }
        public string txtNombreb { get; set; }
        public string txtBancoib { get; set; }
        public string txtBancopb { get; set; }
        public string cmbMoneda { get; set; }
        public string txtMonto { get; set; }


        public bool chkContratoFax { get; set; }
        public string lblChkContratoFax { get; set; }

        public bool chkContratoMift { get; set; }
        public string lblChkContratoMift { get; set; }

        public bool chkfaxNY { get; set; }
        public string lblChkfaxNY { get; set; }

        public bool chkMail { get; set; }
        public string lblChkMail { get; set; }

        public bool chkCiti { get; set; }
        public string lblChkCiti { get; set; }

        public bool Frame1 { get; set; }

        public byte? est_recurrencia { get; set; }

        public string lblResultado { get; set; }
        public string lblMensaje { get; set; }

        public string Indicador_mift { get; set; }
        public string Indicador_fax_local { get; set; }
        public string Indicador_citi_offshore { get; set; }
        public string Indicador_fax_NY_Londres { get; set; }
        public string Indicador_anexo_mail { get; set; }
        public string Indicador_otros { get; set; }


        public string resultado { get; set; } //Contratos

        public string Error_original { get; set; } //Grabar Log

        public bool cmdVRecurrencia { get; set; } //Boton Recurrencia
        public bool cmdMesa { get; set; } //Boton Tipo Cambio
        public bool cmdChkList { get; set; }
        public bool cmdReparo { get; set; }
        public string lblLog2 { get; set; }


        //Nav 2
        public string txtRutM { get; set; }
        public string txtDVM { get; set; }
        public string txtCuentaM { get; set; }
        public string txtBaseM { get; set; }
        public string lblNombreClteM { get; set; }
        public string lblSegmentoM { get; set; }
        public string lblEjecutivoM { get; set; }
        public bool cmbFaxNYM { get; set; } //Combo
        public bool cmbOtrosM { get; set; } //Combo        
        public bool chkCitiM { get; set; }
        public string LblchkCitiM { get; set; }

        public bool chkContratoFaxM { get; set; }
        public string LblchkContratoFaxM { get; set; }

        public bool chkContratoMiftM { get; set; }
        public string LblchkContratoMiftM { get; set; }

        public bool chkfaxNYM { get; set; }
        public string LblchkfaxNYM { get; set; }

        public bool chkMailM { get; set; }
        public string LblchkMailM { get; set; }

        public bool chkOtrosM { get; set; }
        public string LblchkOtrosM { get; set; }     
        
        public bool cmdAgregar { get; set; }
        public bool cmdModificar { get; set; }


        public IndexViewModel()
        {
            Frame1 = false;
        }


    }
}