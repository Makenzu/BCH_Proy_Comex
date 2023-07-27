using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class PlanillaIngresoVisibleExportViewModel : FundTransferViewModel
    {
        [Display(Name = "N° Presentación")]
        public UI_TextBox Tx_NPresentacion { set; get; }

        [Display(Name = "Fecha")]
        public UI_TextBox Tx_Fecha { set; get; }

        [Display(Name = "Entidad que presenta la Planilla")]
        public UI_TextBox Tx_EntidadPresentaPlanilla { set; get; }

        [Display(Name = "Pza.Banco Central que contabiliza")]
        public UI_TextBox Tx_PlazaBancoContabiliza { set; get; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodigoUno { set; get; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodigoDos { set; get; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodigoTres { set; get; }

        [Display(Name = "Tipo Operación")]
        public UI_TextBox Tx_TipoOperacion { set; get; }

        [Display(Name = "Nombre")]
        public UI_TextBox Tx_Nombre { set; get; }

        [Display(Name = "Dirección")]
        public UI_TextBox Tx_Direccion { set; get; }

        [Display(Name = "R.U.T")]
        public UI_TextBox Tx_Rut { set; get; }


        //Monto Retornado
        [Display(Name = "Moneda")]
        public UI_TextBox Tx_DesMonedaRetornado { set; get; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodMonedaRetornado { set; get; }

        [Display(Name = "Valor Bruto")]
        public UI_TextBox Tx_ValorBruto { set; get; }

        [Display(Name = "Comisiones")]
        public UI_TextBox Tx_Comisiones { set; get; }

        [Display(Name = "Otros Gastos Deduc.")]
        public UI_TextBox Tx_OtrosGastosDeducibles { set; get; }

        [Display(Name = "Valor Liquido")]
        public UI_TextBox Tx_ValorLiquido { set; get; }

        [Display(Name = "Paridad a US$")]
        public UI_TextBox Tx_ParidaMontoRetornado { set; get; }

        [Display(Name = "Monto en US$")]
        public UI_TextBox Tx_MontoMMONRetornado { set; get; }

        [Display(Name = "Tipo Cambio Ope.$")]
        public UI_TextBox Tx_TipCam { set; get; }


        //Datos Dec Export
        [Display(Name = "Aduana")]
        public UI_TextBox Tx_Aduana { set; get; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodAduana { set; get; }

        [Display(Name = "Número Aceptación")]
        public UI_TextBox Tx_NumAceptacion { set; get; }

        [Display(Name = "Fecha Aceptación")]
        public UI_TextBox Tx_FechaAceptacion { set; get; }

        [Display(Name = "Fecha Venc.Retorno")]
        public UI_TextBox Tx_FechaVencimientoRetorno { set; get; }

        //Datos Financ Original
        [Display(Name = "Entidad Autorizada")]
        public UI_TextBox Tx_DesEntidadAutorizada { set; get; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodEntidadAutorizada { set; get; }

        [Display(Name = "Tipo Financiamiento")]
        public UI_TextBox Tx_DesTipoFinanciamiento { set; get; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodTipoFinanciamiento { set; get; }

        [Display(Name = "Plaza Banco Central")]
        public UI_TextBox Tx_DesPlazaBancoCentral { set; get; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodPlazaBancoCentral { set; get; }

        [Display(Name = "Número Presentación")]
        public UI_TextBox Tx_NPresentacionFinancOriginal { set; get; }

        [Display(Name = "Fecha Presentación")]
        public UI_TextBox Tx_FechaPresentacion { set; get; }

        //Antec Financiamiento
        [Display(Name = "Moneda")]
        public UI_TextBox Tx_DesMonedaFinanciamiento { set; get; }

        [Display(Name = "Código")]
        public UI_TextBox Tx_CodMonedaFinanciamiento { set; get; }

        [Display(Name = "Paridad a US$")]
        public UI_TextBox Tx_ParidaFinanciamiento { set; get; }

        [Display(Name = "Monto")]
        public UI_TextBox Tx_MontoFinanciamiento { set; get; }

        [Display(Name = "Monto US$")]
        public UI_TextBox Tx_MontoMMONFinanciamiento { set; get; }

        [Display(Name = "Plazo Vencimiento Financ.")]
        public UI_TextBox Tx_PlazoVencimientoFinanciamiento { set; get; }

        //Datos Informes Export
        [Display(Name = "Plaza Emisora")]
        public UI_TextBox Tx_PlazaEmisora { set; get; }

        [Display(Name = "Banco Central")]
        public UI_TextBox Tx_BancoCentral { set; get; }

        [Display(Name = "Número de Emisión")]
        public UI_TextBox Tx_NumeroEmision { set; get; }

        [Display(Name = "Fecha de Emisión")]
        public UI_TextBox Tx_FechaEmision { set; get; }

        [Display(Name = "País de la Operación")]
        public UI_TextBox Tx_PaisOperacion { set; get; }

        //Observacion
        [Display(Name = "Observaciones")]
        public string Tx_Observacion { set; get; }

        //Variables Publicas
        public string Redireccionar { get; set; }

        //public UI_Button[] Boton { get; set; }
        //public List<UI_Button> Boton;
        public UI_Button Boton_0;
        public UI_Button Boton_1;
        public UI_Button Boton_2;
        public UI_Button Boton_3;
        public UI_Button Boton_4;
        //public UI_Button BtnRetroceder { get; set; }
        //public UI_Button BtnAvanzar { get; set; }
        //public UI_Button BtnModificar { get; set; }
        //public UI_Button BtnAceptar { get; set; }
        //public UI_Button BtnCancelar { get; set; }


        public PlanillaIngresoVisibleExportViewModel()
        {
        }

        public PlanillaIngresoVisibleExportViewModel(UI_Frm_Pln_VisExp frmState)
        {
            //Boton = new List<UI_Button>();
            //for (int i = 0; i <= 4; i++)
            //{
            //    Boton.Add(new UI_Button());
            //}

            this.Boton_0 = frmState.Boton[0];
            this.Boton_1 = frmState.Boton[1];
            this.Boton_2 = frmState.Boton[2];
            this.Boton_3 = frmState.Boton[3];
            this.Boton_4 = frmState.Boton[4];
            Tx_NPresentacion = frmState.Tx_Plnv[0];
            Tx_Fecha = frmState.Tx_Fecha;
            Tx_PlazaBancoContabiliza = frmState.Tx_Plnv[40];
            Tx_CodigoUno = frmState.Tx_Plnv[43];//Lbl_CodigoUno = frmState.Lb_Plnv[52]; //Lb_Plnv_052
            Tx_CodigoDos = frmState.Tx_Plnv[2];
            Tx_CodigoTres = frmState.Tx_Plnv[3];
            Tx_TipoOperacion = frmState.Tx_Plnv[41];
            Tx_Nombre = frmState.Tx_Plnv[4];
            Tx_Direccion = frmState.Tx_Plnv[5];
            Tx_Rut = frmState.Tx_Plnv[6];
            //Monto Retornado      
            Tx_DesMonedaRetornado = frmState.Tx_Plnv[7];
            Tx_CodMonedaRetornado = frmState.Tx_Plnv[8];
            Tx_ValorBruto = frmState.Tx_Plnv[9];
            Tx_Comisiones = frmState.Tx_Plnv[10];
            Tx_OtrosGastosDeducibles = frmState.Tx_Plnv[11];
            Tx_ValorLiquido = frmState.Tx_Plnv[12];
            Tx_ParidaMontoRetornado = frmState.Tx_Plnv[13];
            Tx_MontoMMONRetornado = frmState.Tx_Plnv[14];
            Tx_TipCam = frmState.Tx_Plnv[15];
            //Datos Dec Export  
            Tx_Aduana = frmState.Tx_Plnv[16];
            Tx_CodAduana = frmState.Tx_Plnv[17];
            Tx_NumAceptacion = frmState.Tx_Plnv[18];
            Tx_FechaAceptacion = frmState.Tx_Plnv[19];
            Tx_FechaVencimientoRetorno = frmState.Tx_Plnv[20];
            //Datos Financ Original
            Tx_DesEntidadAutorizada = frmState.Tx_Plnv[21];
            Tx_CodEntidadAutorizada = frmState.Tx_Plnv[22];
            Tx_DesTipoFinanciamiento = frmState.Tx_Plnv[23];
            Tx_CodTipoFinanciamiento = frmState.Tx_Plnv[24];
            Tx_DesPlazaBancoCentral = frmState.Tx_Plnv[25];
            Tx_CodPlazaBancoCentral = frmState.Tx_Plnv[26];
            Tx_NPresentacionFinancOriginal = frmState.Tx_Plnv[27];
            Tx_FechaPresentacion = frmState.Tx_Plnv[28];
            //Antec Financiamiento
            Tx_DesMonedaFinanciamiento = frmState.Tx_Plnv[29];
            Tx_CodMonedaFinanciamiento = frmState.Tx_Plnv[30];
            Tx_ParidaFinanciamiento = frmState.Tx_Plnv[31];
            Tx_MontoFinanciamiento = frmState.Tx_Plnv[32];
            Tx_MontoMMONFinanciamiento = frmState.Tx_Plnv[33];
            Tx_PlazoVencimientoFinanciamiento = frmState.Tx_Plnv[34];

            //Datos Informes Export
            Tx_PlazaEmisora = frmState.Tx_Plnv[35];
            Tx_BancoCentral = frmState.Tx_Plnv[36];
            Tx_NumeroEmision = frmState.Tx_Plnv[37];
            Tx_FechaEmision = frmState.Tx_Plnv[38];
            Tx_PaisOperacion = frmState.Tx_Plnv[42];
            Tx_Observacion = frmState.Tx_Plnv[39].Text;
        }


        public void Update(UI_Frm_Pln_VisExp frm)
        {

            //frm.Tx_Fecha.Text = this.Tx_Fecha;            
            Update(frm.Tx_Fecha, this.Tx_Fecha);
            frm.Tx_Plnv[39].Text = this.Tx_Observacion;
            // Update(frm.Tx_Plnv[39], this.Tx_Observacion);

        }

        //public PlanillaIngresoVisibleExportViewModel(UI_Frm_Pln_VisExp frmState, string accionARedireccionar)
        //    : this(frmState)
        //{
        //    if (!string.IsNullOrEmpty(accionARedireccionar))
        //    {
        //        //transformo el Action a URL
        //        UrlHelper helper = new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext);
        //        this.Redireccionar = helper.Action(accionARedireccionar);
        //    }


        //}

        //public void Update(UI_Frm_Pln_VisExp frmState)
        //{
        //    for (int i = 0; i < frmState.Donde.Count; i++)
        //    {
        //        frmState.Donde[i].Selected = i == (this.SelectedDonde - 1);
        //    }

        //    frmState.LstPartys.ListIndex = this.SelectedPartiList;

        //    frmState.Llave.Text = this.KeyText;
        //}

    }
}