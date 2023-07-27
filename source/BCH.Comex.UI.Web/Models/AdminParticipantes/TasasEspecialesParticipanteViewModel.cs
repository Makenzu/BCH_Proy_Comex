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
    public class TasasEspecialesParticipanteViewModel : AdminParticipantesViewModel
    {
        #region "LISTA COMISION Y EVENTOS"

        //[Display(Name = "Tasa  |  Monto Hasta  |  Minimo  |  Maximo")]
        public UI_Combo ListaComision { get; set; }
        public UI_Button BtnAceptar { get; set; }
        public UI_Button Btncancelar { get; set; }
        public UI_Button BtnAgregar { get; set; }
        public UI_Button BtnEliminar { get; set; }

        #endregion

        #region "INTERES"
        [Display(Name = "Tasa")]
        public UI_TextBox TasaInteres { get; set; }
        public List<UI_OptionItem> prtTipoInteres { get; set; }
        public int SelectedTipoInteres { get; set; }
        public UI_CheckBox Flotante { get; set; }
        #endregion

        #region "GASTOS"

        [Display(Name = "Monto en Dolar USA")]
        public UI_TextBox MontoGastos { get; set; }

        [Display(Name = "Desde Manual_Tarifa")]
        public UI_CheckBox tarifa { get; set; }

        #endregion

        #region "LISTA MODULO"
        [Display(Name = "Modulo   |  Producto  |   Etapa   |   Estado")]
        public UI_Combo ListaModulo { get; set; }
        #endregion

        #region "FRAME"
        public UI_Panel GroupingOfControlsPrtComision { get; set; }
        public UI_Panel GroupingOfControlsPrtGasto { get; set; }
        public UI_Panel GroupingOfControlsPrtInteres { get; set; }


        #endregion

        #region "TITULO"
        public UI_Label Titulo { get; set; }
        #endregion

        #region "MENU"
        public UI_Button menuImportaciones { get; set; }
        public UI_Button menuExportaciones { get; set; }
        public UI_Button menuServicios { get; set; }
        public UI_Button menuImprimir { get; set; }

        //-------------Importaciones      
        public UI_Button prodCobranza { get; set; }
        public UI_Button prodCartaCredito { get; set; }

        //Importaciones Cobranzas
        //public List<UI_Button> _etacob_ { get; set; }
        public UI_Button etacobComisionPago { get; set; }
        public UI_Button etacobAnulacion { get; set; }
        public UI_Button etacobTraspasoOtrosBancos { get; set; }
        public UI_Button etacobProrroga { get; set; }
        public UI_Button etacobMantencion { get; set; }


        //public List<UI_Button> _etacar_ { get; set; }
        public UI_Button etacarApertura { get; set; }
        public UI_Button etacarModificacion1 { get; set; }
        public UI_Button etacarModificacion2 { get; set; }
        public UI_Button etacarAnulacionSaldo { get; set; }
        public UI_Button etacarNegociacionCobertura { get; set; }
        public UI_Button etacarNegociacionVencimiento { get; set; }
        public UI_Button etacarProrroga { get; set; }

        //Exportaciones
        //-------Exportaciones 1 nivel
        //public List<UI_Button> _prodexp_ { get; set; }
        public UI_Button prodexpPae { get; set; }
        public UI_Button prodexpCompraDoc { get; set; }
        public UI_Button prodexpDescDoc { get; set; }
        public UI_Button prodexpCartaCredito { get; set; }
        public UI_Button prodexpPagoAnticipado { get; set; }
        public UI_Button prodexpCobranzas { get; set; }
        public UI_Button prodexpRetorno { get; set; }


        //----------PAE - Ingreso
        public UI_Button etapaeIngreso { get; set; }

        //---------Compra de documentos (Gestión país,Gestión Aladi,Gestión otros países
        //public List<UI_Button> _etacom_ { get; set; }
        public UI_Button etacomPais { get; set; }
        public UI_Button etacomAladi { get; set; }
        public UI_Button etacomOtrosPaises { get; set; }

        //---------Descuentos documentos (Gestion pais,Gestion Aladi,Gestion otros países
        //public List<UI_Button> _etades_ { get; set; }
        public UI_Button etadesPais { get; set; }
        public UI_Button etadesAladi { get; set; }
        public UI_Button etadesOtrosPaises { get; set; }



        //---------Carta de Crédito
        //public UI_Button _etacred_ { get; set; }
        public UI_Button etacredAviso { get; set; }
        public UI_Button etacredConfirmacion { get; set; }
        public UI_Button etacredMotivacion { get; set; }
        public UI_Button etacredTransferencia { get; set; }
        public UI_Button etacredNoUtilizacion { get; set; }
        public UI_Button etacredPago { get; set; }
        public UI_Button etacredRevision { get; set; }
        public UI_Button etacredTraspaso { get; set; }
        public UI_Button etacredAvisoBenificiario { get; set; }



        public UI_Button etapaManejo { get; set; }
        public UI_Button etapaMensaje { get; set; }
        public UI_Button etaretTraspaso { get; set; }
        public UI_Button etaretAdicion { get; set; }

        //Servicios       
        public UI_Button prodserEndosoPlanilla { get; set; }
        public UI_Button prodserTraspasoIDI { get; set; }
        public UI_Button prodserCoberturaFlote { get; set; }
        public UI_Button prodserExFinanciamiento { get; set; }
        public UI_Button prodserVentaDivisas { get; set; }
        public UI_Button prodserOrdenPagoCondicionado { get; set; }


        //---------Ex financiamiento
        public UI_Button exFinanciamientoProceso { get; set; }
        public UI_Button exFinanciamientoAnulacion { get; set; }
        //-------- Orden de Pago condicionado
        public UI_Button EtaordenPagoFinanAviso { get; set; }
        public UI_Button EtaordenPagoFinanPago { get; set; }

        //Imprimir
        public UI_Button Imprimir { get; set; }


        #endregion

        #region "variables Internas"
        public int indexAuxTipoInteres { get; set; }        
        public int EstadoMsjeConfirmacion { get; set; }
        public int idEstadoMsjeExportacion { get; set; }


        #endregion
        public TasasEspecialesParticipanteViewModel()
        {


        }


        public TasasEspecialesParticipanteViewModel(UI_PrtEnt06 frmState, InitializationObject initObj)
        {
            MensajesDeError = initObj.Mdi_Principal.MESSAGES;

            this.ListaComision = frmState.lista_com;
            this.BtnAceptar = frmState.aceptar;
            this.Btncancelar = frmState.cancelar;
            this.BtnAgregar = frmState.Agregar;
            this.BtnEliminar = frmState.Eliminar;

            //Interes
            this.TasaInteres = frmState.prttasint;
            this.prtTipoInteres = frmState._prttipoi_;
            indexAuxTipoInteres = -1;
            for (int i = 0; i < frmState._prttipoi_.Count; i++)
            {
                if (frmState._prttipoi_[i].Selected)
                    indexAuxTipoInteres = 1;
            }

            //  this.SelectedTipoInteres = int.Parse(frmState._prttipoi_.First(x => x.Selected).ID);
            if (indexAuxTipoInteres != -1)
                this.SelectedTipoInteres = int.Parse(frmState._prttipoi_.First(x => x.Selected).ID);

            this.Flotante = frmState.prtflot;

            //Gasto
            this.MontoGastos = frmState.prtmongas;
            this.tarifa = frmState.tarifa;

            //Lista Modulo
            this.ListaModulo = frmState.lista;

            //Panel
            this.GroupingOfControlsPrtComision = frmState.prtcomision;
            this.GroupingOfControlsPrtGasto = frmState.prtgasto;
            this.GroupingOfControlsPrtInteres = frmState.prtinteres;

            //Titulo
            this.Titulo = frmState.Titulo;


            #region "MENU"
            //Menu
            menuImportaciones = frmState._menu_[0];
            menuExportaciones = frmState._menu_[1];
            menuServicios = frmState._menu_[2];
            menuImprimir = frmState._menu_[3];

            //Importaciones
            prodCobranza = frmState._prod_[0];
            prodCartaCredito = frmState._prod_[1];

            etacobComisionPago = frmState._etacob_[0];
            etacobAnulacion = frmState._etacob_[1];
            etacobTraspasoOtrosBancos = frmState._etacob_[2];
            etacobProrroga = frmState._etacob_[3];
            etacobMantencion = frmState._etacob_[4];


            //Exportaciones       
            prodexpPae = frmState._prodexp_[0];
            prodexpCompraDoc = frmState._prodexp_[1];
            prodexpDescDoc = frmState._prodexp_[2];
            prodexpCartaCredito = frmState._prodexp_[3];
            prodexpPagoAnticipado = frmState._prodexp_[4];
            prodexpCobranzas = frmState._prodexp_[5];
            prodexpRetorno = frmState._prodexp_[6];

            etapaeIngreso = frmState._etapae_0;
            etacomPais = frmState._etacom_[0];
            etacomAladi = frmState._etacom_[1];
            etacomOtrosPaises = frmState._etacom_[2];

            etadesPais = frmState._etades_[0];
            etadesAladi = frmState._etades_[1];
            etadesOtrosPaises = frmState._etades_[2];

            etacredAviso = frmState._etacred_[0];
            etacredConfirmacion = frmState._etacred_[1];
            etacredMotivacion = frmState._etacred_[2];
            etacredTransferencia = frmState._etacred_[3];
            etacredNoUtilizacion = frmState._etacred_[4];
            etacredPago = frmState._etacred_[5];
            etacredRevision = frmState._etacred_[6];
            etacredTraspaso = frmState._etacred_[7];
            etacredAvisoBenificiario = frmState._etacred_[8];


            etapaManejo = frmState._etapa_[0];
            etapaMensaje = frmState._etapa_[1];

            etaretTraspaso = frmState._etaret_[0];
            etaretAdicion = frmState._etaret_[1];

            //Servicios
            prodserEndosoPlanilla = frmState._prodser_[0];
            prodserTraspasoIDI = frmState._prodser_[1];
            prodserCoberturaFlote = frmState._prodser_[2];
            prodserExFinanciamiento = frmState._prodser_[3];
            prodserVentaDivisas = frmState._prodser_[4];
            prodserOrdenPagoCondicionado = frmState._prodser_[5];

            exFinanciamientoProceso = frmState._etafin_[0];
            exFinanciamientoAnulacion = frmState._etafin_[1];

            EtaordenPagoFinanAviso = frmState._etaor_[0];
            EtaordenPagoFinanPago = frmState._etaor_[1];
            //Imprimir
            Imprimir = frmState._imp_;

            #endregion

             this.EstadoMsjeConfirmacion = frmState.EstadoMsjeConfirmacion;
            this.idEstadoMsjeExportacion = frmState.idEstadoMsjeExportacion;
        }


        public void Update(UI_PrtEnt06 frmState)
        {
            Update(frmState.lista_com, this.ListaComision);
            Update(frmState.prttasint, this.TasaInteres);
            Update(frmState.prtmongas, this.MontoGastos);

            Update(frmState.prtflot, this.Flotante);
            Update(frmState.tarifa, this.tarifa);
            Update(frmState.lista, this.ListaModulo);
            frmState.EstadoMsjeConfirmacion = this.EstadoMsjeConfirmacion;
            frmState.idEstadoMsjeExportacion = this.idEstadoMsjeExportacion;
            frmState.Titulo = this.Titulo;

        }









    }
}