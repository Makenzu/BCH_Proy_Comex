using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Core.Entities.Mcambio;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes
{
    public class InitializationObject
    {
        public T_MODGCVD MODGCVD { set; get; } //emula las propiedades del modulo MODGCVD
        public T_Module1 Module1 { set; get; } //emula las propiedades del modulo Module1
        public T_MODGFRA MODGFRA { set; get; } //emula las propiedades del modulo MODGFRA
        public T_MODSWENN MODSWENN { set; get; }//emula las propiedades del modulo MODSWENN
        public T_MODGUSR MODGUSR { set; get; }//emula las propiedades del modulo MODGUSR
        public T_MODGMTA MODGMTA { set; get; }  //emula las propiedades del modulo MODGMTA
        public T_MODGSCE MODGSCE { set; get; } //emula las propiedades del modulo MODGSCE
        public T_MODGTAB0 MODGTAB0 { set; get; } //emula las propiedades del modulo MODGTAB0
        public T_MODGTAB1 MODGTAB1 { set; get; } //emula las propiedades del modulo MODGTAB1
        public T_MODXORI MODXORI { set; get; } //emula las propiedades del modulo MODXORI
        public T_MODCVDIM MODCVDIM { set; get; } //emula las propiedades del modulo MODCVDIM
        public T_MODGFYS MODGFYS { set; get; } //emula las propiedades del modulo MODGFYS
        public T_MODGASO MODGASO { set; get; }//emula las propiedades del modulo MODGASO
        public T_MODCVDIMMM MODCVDIMMM { set; get; } //emula las propiedades del modulo MODGASO
        public T_ModChVrf ModChVrf { set; get; }//emula las propiedades del modulo ModChVrf
        public T_MODPREEM MODPREEM { set; get; }//emula las propiedades del modulo MODPREEM
        public T_MODANUVI MODANUVI { set; get; }//emula las propiedades del modulo MODANUVI
        public T_MODVPLE MODVPLE { set; get; }//emula las propiedades del modulo MODPREEM
        public T_Mdl_Funciones_Varias Mdl_Funciones_Varias { set; get; } //emula las propiedades del modulo Mdl_Funciones_Varias
        public T_MODXVIA MODXVIA { set; get; }
        public T_MODGARB MODGARB { set; get; }
        public T_MODGPLI1 MODGPLI1 { set; get; }
        public T_MODXPLN1 MODXPLN1 { set; get; }
        public T_MODXANU MODXANU { set; get; }
        public T_MODGRNG MODGRNG { set; get; }
        public T_MODCARAB MODCARAB { set; get; }
        public T_MODCARMAS MODCARMAS { set; get; }
        public T_MODGSWF MODGSWF { set; get; }
        public T_MODGCHQ MODGCHQ { set; get; }
        public T_MODXPLN0 MODXPLN0 { set; get; }
        public T_MODGANU MODGANU { set; get; }
        public T_MODGCON0 MODGCON0 { set; get; }
        public T_MODGPYF0 MODGPYF0 { set; get; }
        public T_MODCONT MODCONT { get; set; }
        public T_MODGNCTA MODGNCTA { get; set; }
        public T_MODGSYB MODGSYB { get; set; }
        public T_MODGPYF1 MODGPYF1 { get; set; }

        public T_Mdl_Funciones Mdl_Funciones { set; get; }        
        public T_MODGPYF2 MODGPYF2 { set; get; }
        public T_ModSaldo ModSaldo { set; get; }
        public T_MODABDC MODABDC { set; get; }
        public T_MODXSWF MODXSWF { set; get; }
        public T_MOD_50F MOD_50F { get; set; }
        public bool DesabilitarBotones { get; set; }
        

        #region MODULOS DE UI
        public UI_Frm_Participantes Frm_Participantes { get; set; }
        public UI_Frm_Iden_Participantes Frm_Iden_Participantes { get; set; }
        public UI_Mdi_Principal Mdi_Principal { set; get; }
        public UI_Frm_Principal Frm_Principal { set; get; }
        public UI_Frm_Ingreso_Valores Frm_Ingreso_Valores { set; get; }
        public UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible { set; get; }
        public UI_Frm_SeleccionOficina Frm_SeleccionOficina { set; get; }
        public UI_Frm_Parti_No Frm_Parti_No { get; set; }
        public UI_Frm_Crea_Participante Frm_Crea_Participante { get; set; }
        public UI_Frm_Con_Participantes Frm_Con_Participantes { get; set; }
        public UI_Frm_Destino_Fondos Frm_Destino_Fondos { get; set; }
        public UI_Frm_Arbitrajes Frm_Arbitrajes { set; get; }
        public UI_Frm_VisE Frm_VisE { set; get; }
        public UI_FrmxPln0 FrmxPln0 { set; get; }
        public UI_Frm_Origen_Fondos Frm_Origen_Fondos { set; get; }
        public UI_Frm_Cta Frm_Cta { set; get; }
        public UI_Frm_ChVrf Frm_ChVrf { set; get; }
        public UI_Frm_PlvSO Frm_PlvSO { set; get; }
        public UI_Frm_Consulta Frm_Consulta { set; get; }
        public UI_Frm_Ticket Frm_Ticket { set; get; }
        public UI_Frmgrev frmgrev { get; set; }
        public UI_Frm_Pln_Invisible Frm_Pln_Invisible { set; get; }
        public UI_Frm_Pln_VisExp Frm_Pln_VisExp { set; get; }
        public UI_frmganu Frmganu { set; get; }
        public UI_Frm_Pln_cob Frm_Pln_cob { set; get; }
        public UI_Frm_Comisiones Frm_Comisiones { set; get; }
        public UI_Frm_Anu_Vi Frm_Anu_Vi { get; set; }
        public UI_Frm_Rem_PVI Frm_Rem_PVI { get; set; }
        public UI_FrmChq Frm_Chq { set; get; }
        public UI_frmnroa Frmnroa { get; set; }
        public UI_FrmgAso FrmgAso { set; get; }
        public UI_FrmxAnu Frmxanu { set; get; }

        public UI_Frmgnota Frmgnota { set; get; }
        public UI_Frm_Declaracion  Frm_Declaracion { get; set; }
        public UI_FrmFact FrmFact { set; get; }

        public string FormularioQueAbrir = String.Empty;
        public string MetodoQueEjecuta = String.Empty;
        public string VieneDe = String.Empty;//todo: esto seguramente se cambie por el stack
        public string CaptionAddition = String.Empty;
        public string Base { set; get; }
        public string UsuarioBase { set; get; }

        public int oriLoop { set; get; }//es la variable que se mantiene para el loop de VxOri
        public int viaLoop { set; get; }//es la variable que se mantiene para el loop de VxVia
        public string SyPutn_Adc_Str { set; get; }//variable que se setea al dar aceptar al ticket

        /// <summary>
        /// Permite inicializar el objeto
        /// </summary>
        public bool refrescarSesion { set; get; }
        #endregion

        #region IMPRESION
        public List<DataImpresion> DocumentosAImprimir { set; get; }//lista de los documentos pendientes de imprimir
        public List<ImprimirPlanilla> Planillas { set; get; }//planillas que se deben imprimir al grabar
        public List<PlanillaInvisible> PlanillasInvisibles { set; get; }
        public List<Planilla401> Planillas401 { set; get; }
        public List<Planilla500> Planillas500 { set; get; }
        public List<PlanillaVisibleAnulada> PlanillasVisiblesAnuladas { set; get; }
        public List<PlanillaVisibleExportacion> PlanillasVisiblesExportacion { set; get; }
        public List<PlanillaEstadistica> PlanillasEstadisticas { set; get; }
        public List<PlanillaReemplazo> PlanillasReemplazo { set; get; }
        #endregion

        #region FundTransfer - Mcambio - Comex
        public List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD { get; set; }
        public List<DealsIngresadosParaProcesar> DealsIngParaProces { get; set; }
        public Nullable<int> DealPrevSel { get; set; }
        public Nullable<int> DealActualSel { get; set; }
        public Nullable<int> DealManual { get; set; }
        public string PagOri { get; set; }
        public int Flag_transferencia { get; set; }
        public int Flag_eliminar { get; set; }
        public Nullable<int> SelUdp { get; set; }
        public int GrabarOk { get; set; }
        public int Frag_Anula { get; set; }
        public string NroSce_Anula { get; set; }
        public int Frag_TransInt { get; set; }
        #endregion

        /// <summary>
        /// Datos del usuario
        /// </summary>
        public BCH.Comex.Core.Entities.Portal.IDatosUsuario Usuario { get; set; }

        public InitializationObject()
        {
            #region Init - FundTransfer - Mcambio - Comex
            datSPMcambioCDD = new List<Mcambio_Consulta_Deals_Disponible>();
            datSPMcambioCDD = null;
            DealsIngParaProces = new List<DealsIngresadosParaProcesar>();
            DealsIngParaProces = null;
            DealPrevSel = null;
            DealActualSel = null;
            DealManual = null;
            PagOri = null;
            Flag_transferencia = 0;
            Flag_eliminar = -1;
            SelUdp = null;
            GrabarOk = 0;
            Frag_Anula = 0;
            NroSce_Anula = null;
            Frag_TransInt = 0;
            #endregion

            DocumentosAImprimir = new List<DataImpresion>();
            Planillas = new List<ImprimirPlanilla>();
            PlanillasInvisibles = new List<PlanillaInvisible>();
            Planillas401 = new List<Planilla401>();
            Planillas500 = new List<Planilla500>();
            PlanillasVisiblesAnuladas = new List<PlanillaVisibleAnulada>();
            PlanillasVisiblesExportacion = new List<PlanillaVisibleExportacion>();
            PlanillasEstadisticas = new List<PlanillaEstadistica>();
            PlanillasReemplazo = new List<PlanillaReemplazo>();
            //Frm_Participantes = new UI_Frm_Participantes();
            Frm_Con_Participantes = new UI_Frm_Con_Participantes();

            Mdi_Principal = new UI_Mdi_Principal();
            Frm_Principal = new UI_Frm_Principal();
            Frm_PlvSO = new UI_Frm_PlvSO();
            Frm_Consulta = new UI_Frm_Consulta();
            //Frm_Ingreso_Valores = new UI_Frm_Ingreso_Valores();
            //Frm_Comercio_Invisible = new UI_Frm_Comercio_Invisibles();
            Frm_Iden_Participantes = new UI_Frm_Iden_Participantes();
            Frm_Parti_No = new UI_Frm_Parti_No();
            Frm_Pln_VisExp = new UI_Frm_Pln_VisExp();
            Frm_Declaracion = new UI_Frm_Declaracion();
            Frmnroa = new UI_frmnroa();
            FrmgAso = new UI_FrmgAso();
            Frm_Chq = new UI_FrmChq();
            refrescarSesion = false;
            DesabilitarBotones = false;
        }

    }
}

