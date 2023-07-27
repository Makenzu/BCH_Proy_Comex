using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class PlanillaAnulacionViewModel : FundTransferViewModel
    {
        [Display(Name = "N° Presentación")]
        public UI_TextBox Tx_NPresentacion { get; set; }

        [Display(Name = "Fecha")]
        public UI_TextBox Tx_Fecha { get; set; }

        [Display(Name = "Pza.Banco Central contabiliza")]
        public UI_TextBox Tx_PlazaBancoContabiliza { get; set; }

        [Display(Name = "Tipo Anulación")]
        public UI_TextBox Tx_TipoAnulacion { get; set; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodigoUno { get; set; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodigoDos { get; set; }

        [Display(Name = "Nombre")]
        public UI_TextBox Tx_Nombre { get; set; }

        [Display(Name = "Dirección")]
        public UI_TextBox Tx_Direccion { get; set; }

        [Display(Name = "R.U.T")]
        public UI_TextBox Tx_Rut { get; set; }

        //DATOS PLANILLA ANULADA
        [Display(Name = "Ent. Autorizada")]
        public UI_TextBox Tx_DesEntidadAutorizadaPlanillaAnulada { get; set; }


        [Display(Name = "Código")]
        public UI_TextBox Tx_CodEntidadAutorizadaPlanillaAnulada { get; set; }

        [Display(Name = "Número Presentación")]
        public UI_TextBox Tx_NumeroPresentacionAnulada { get; set; }

        [Display(Name = "Fecha Presentación")]
        public UI_TextBox Tx_FechaPresentacionAnulada { get; set; }

        [Display(Name = "Tipo Operación")]
        public UI_TextBox Tx_TipoOperacionAnulada { get; set; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodigoOperacionAnulada { get; set; }

        [Display(Name = "Pza.Banco Central")]
        public UI_TextBox Tx_PlazaBancoCentralAnulada { get; set; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodigoBancaCentralAnulada { get; set; }

        [Display(Name = "Monto US$")]
        public UI_TextBox Tx_MontoMMONAnulada { get; set; }

        [Display(Name = "Paridad A US$")]
        public UI_TextBox Tx_ParidadMMONAnulada { get; set; }

        // DECLARACION EXPORTACION
        [Display(Name = "Aduana")]
        public UI_TextBox Tx_AduanaExportacion { get; set; }
        [Display(Name = "Código")]
        public UI_TextBox Tx_CodigoExportacion { get; set; }
        [Display(Name = "Número Aceptación")]
        public UI_TextBox Tx_NumeroAceptacionExportacion { get; set; }
        [Display(Name = "Fecha Aceptación")]
        public UI_TextBox Tx_FechaAceptacionExportacion { get; set; }
        [Display(Name = "Fecha Vencimiento Retorno")]
        public UI_TextBox Tx_FechaVencimientoRetornoExportacion { get; set; }

        //MONTO ANULADO
        [Display(Name = "Moneda")]
        public UI_TextBox Tx_DescripcionMonedaMontoAnulado { get; set; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodigoMonedaAnulado { get; set; }

        [Display(Name = "Monto Anulada")]
        public UI_TextBox Tx_MontoAnulado { get; set; }

        [Display(Name = "Paridad A US$")]
        public UI_TextBox Tx_ParidadMMONMontoAnulado { get; set; }

        [Display(Name = "Monto en US$")]
        public UI_TextBox Tx_MontoMMONMontoAnulado { get; set; }

        [Display(Name = "Monto en US$ (Paridad Original)")]
        public UI_TextBox Tx_MontoMMONParidadOriginalAnulado { get; set; }

        [Display(Name = "Observaciones")]
        public UI_TextBox Tx_Observaciones { set; get; }


        //AUTORIZACION
        [Display(Name = "Tipo de Autorización")]
        public List<SelectListItem> Cb_Autor { get; set; }
        public int SelAutor { get; set; }
        [Display(Name = "Número Autorización")]
        public UI_TextBox Tx_NumeroAutorizacion { get; set; }

        [Display(Name = "Fecha Autorización")]
        public UI_TextBox Tx_FechaAutorizacion { get; set; }
        [Display(Name = "Tipo de Cambio")]
        public UI_TextBox Tx_TipoCambioAutorizacion { get; set; }


        public UI_Button Boton_0; //Boton hacia atras
        public UI_Button Boton_1; //Boton hacia adelante
        public UI_Button Boton_2; //Boton Ticket
        public UI_Button Boton_3; //Boton Aceptar
        public UI_Button Boton_4; //Boton Cancelar


        public PlanillaAnulacionViewModel()
        {
        }

        public PlanillaAnulacionViewModel(UI_FrmxAnu frmState, InitializationObject initObj)
        {
            MensajesDeError = initObj.Mdi_Principal.MESSAGES;

            this.Tx_NPresentacion = frmState.Tx_PlAnu[0];
            this.Tx_CodigoUno = frmState.Tx_PlAnu[1]; // en VB es Lb_PlAnu_052 va seteado 15
            this.Tx_TipoAnulacion = frmState.Tx_PlAnu[2];
            this.Tx_Fecha = frmState.Tx_Fecha;
            this.Tx_PlazaBancoContabiliza = frmState.Tx_PlAnu[3];
            this.Tx_CodigoDos = frmState.Tx_PlAnu[4];
            this.Tx_Nombre = frmState.Tx_PlAnu[5];
            this.Tx_Direccion = frmState.Tx_PlAnu[6];
            this.Tx_Rut = frmState.Tx_PlAnu[7];

            //Datos Planilla Anulada
            this.Tx_DesEntidadAutorizadaPlanillaAnulada = frmState.Tx_PlAnu[8];
            this.Tx_CodEntidadAutorizadaPlanillaAnulada = frmState.Tx_PlAnu[9];
            this.Tx_NumeroPresentacionAnulada = frmState.Tx_PlAnu[10];
            this.Tx_FechaPresentacionAnulada = frmState.Tx_PlAnu[11];
            this.Tx_TipoOperacionAnulada = frmState.Tx_PlAnu[12];
            this.Tx_CodigoOperacionAnulada = frmState.Tx_PlAnu[13];
            this.Tx_PlazaBancoCentralAnulada = frmState.Tx_PlAnu[14];
            this.Tx_CodigoBancaCentralAnulada = frmState.Tx_PlAnu[15];
            this.Tx_MontoMMONAnulada = frmState.Tx_PlAnu[16];
            this.Tx_ParidadMMONAnulada = frmState.Tx_PlAnu[17];

            //Declaracion Exportacion
            this.Tx_AduanaExportacion = frmState.Tx_PlAnu[18];
            this.Tx_CodigoExportacion = frmState.Tx_PlAnu[19];
            this.Tx_NumeroAceptacionExportacion = frmState.Tx_PlAnu[20];
            this.Tx_FechaAceptacionExportacion = frmState.Tx_PlAnu[21];
            this.Tx_FechaVencimientoRetornoExportacion = frmState.Tx_PlAnu[22];

            //Monto Anulado
            this.Tx_DescripcionMonedaMontoAnulado = frmState.Tx_PlAnu[23];
            this.Tx_CodigoMonedaAnulado = frmState.Tx_PlAnu[24];
            this.Tx_MontoAnulado = frmState.Tx_PlAnu[25];
            this.Tx_ParidadMMONMontoAnulado = frmState.Tx_PlAnu[26];
            this.Tx_MontoMMONMontoAnulado = frmState.Tx_PlAnu[27];
            this.Tx_MontoMMONParidadOriginalAnulado = frmState.Tx_PlAnu[28];
            this.Tx_Observaciones = frmState.Tx_PlAnu[29];


            //Autorizacion
            //Cb_Autor = ToSelectList(frmState.Cb_Autor);
            //SelAutor = frmState.Cb_Autor.get_ItemData_(frmState.Cb_Autor.ListIndex);
            this.Tx_NumeroAutorizacion = frmState.Tx_PlAnu[30];
            this.Tx_FechaAutorizacion = frmState.Tx_PlAnu[31];
            this.Tx_TipoCambioAutorizacion = frmState.Tx_PlAnu[32];

            this.Boton_0 = frmState.Boton[0];
            this.Boton_1 = frmState.Boton[1];
            this.Boton_2 = frmState.Boton[2];
            this.Boton_3 = frmState.Boton[3];
            this.Boton_4 = frmState.Boton[4];

            this.Cb_Autor = new List<SelectListItem>();
            Cb_Autor = frmState.Cb_Autor.Items.Select(x => new SelectListItem
            {
                Text = x.Value,
                Value = x.Data.ToString()
            }).ToList();

            if (frmState.Cb_Autor.ListIndex > 0)
                Cb_Autor[frmState.Cb_Autor.ListIndex].Selected = true;
        }

        public void Update(UI_FrmxAnu frm)
        {

            Update(frm.Tx_PlAnu[10], this.Tx_NumeroPresentacionAnulada);
            Update(frm.Tx_PlAnu[11], this.Tx_FechaPresentacionAnulada);

            Update(frm.Tx_PlAnu[20], this.Tx_NumeroAceptacionExportacion);
            Update(frm.Tx_PlAnu[21], this.Tx_FechaAceptacionExportacion);
            Update(frm.Tx_PlAnu[22], this.Tx_FechaVencimientoRetornoExportacion);


            Update(frm.Tx_PlAnu[16], this.Tx_MontoMMONAnulada);
            Update(frm.Tx_PlAnu[17], this.Tx_ParidadMMONAnulada);


            //Seccion Datos de la Autorizacion
            Update(frm.Tx_PlAnu[29], this.Tx_Observaciones);
            Update(frm.Tx_PlAnu[30], this.Tx_NumeroAutorizacion);
            Update(frm.Tx_PlAnu[31], this.Tx_FechaAutorizacion);
            Update(frm.Tx_PlAnu[32], this.Tx_TipoCambioAutorizacion);

            //Seccion Monto Anulado
            Update(frm.Tx_PlAnu[25], this.Tx_MontoAnulado);
            Update(frm.Tx_PlAnu[26], this.Tx_ParidadMMONMontoAnulado);
            Update(frm.Tx_PlAnu[27], this.Tx_MontoMMONMontoAnulado);
            Update(frm.Tx_PlAnu[28], this.Tx_MontoMMONParidadOriginalAnulado);


            Update(frm.Tx_PlAnu[2], this.Tx_TipoAnulacion);
            Update(frm.Tx_PlAnu[7], this.Tx_Rut);


        }


    }
}