using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos
{
    public class UI_PrtEnt06
    {

        #region "VARIABLES DINAMICOS"

        public UI_Label Titulo { get; set; }

        #endregion

        #region "LISTA COMISION Y EVENTOS"
        public UI_Combo lista_com { get; set; }
        public UI_Button aceptar { get; set; }
        public UI_Button cancelar { get; set; }
        public UI_Button Agregar { get; set; }
        public UI_Button Eliminar { get; set; }

        #endregion

        #region "INTERES"
        public UI_TextBox prttasint { get; set; }
        public List<UI_OptionItem> _prttipoi_ { get; set; }
        public UI_CheckBox prtflot { get; set; }//UserControl

        #endregion

        #region "GASTO"
        public UI_TextBox prtmongas { get; set; }
        public UI_CheckBox tarifa { get; set; }//public UI_TextBox tarifa { get; set; }//UserControl   

        #endregion

        #region "LISTA MODULO"
        public UI_Combo lista { get; set; }

        #endregion

        #region "FRAME"
        public UI_Panel prtcomision { get; set; }
        public UI_Panel prtgasto { get; set; }
        public UI_Panel prtinteres { get; set; }

        #endregion


        #region "MENU"
        public List<UI_Button> _menu_ { get; set; }

        //-------------Importaciones
        public List<UI_Button> _prod_ { get; set; }
        public List<UI_Button> _etacob_ { get; set; }
        public List<UI_Button> _etacar_ { get; set; }

        //Exportaciones
        //-------Exportaciones 1 nivel
        public List<UI_Button> _prodexp_ { get; set; }

        //----------PAE - Ingreso
        public UI_Button _etapae_0 { get; set; }

        //---------Compra de documentos (Gestión país,Gestión Aladi,Gestión otros países
        public List<UI_Button> _etacom_ { get; set; }

        //---------Descuentos documentos (Gestion pais,Gestion Aladi,Gestion otros países
        public List<UI_Button> _etades_ { get; set; }

        //---------Carta de Crédito
        public List<UI_Button> _etacred_ { get; set; }
        public List<UI_Button> _etapa_ { get; set; }
        public List<UI_Button> _etaret_ { get; set; }

        //Servicios
        public List<UI_Button> _prodser_ { get; set; }
        //---------Ex financiamiento
        public List<UI_Button> _etafin_ { get; set; }

        // Orden de Pago condicionado
        public List<UI_Button> _etaor_ { get; set; }
        public UI_Button _imp_ { get; set; }

        #endregion

        public int EstadoMsjeConfirmacion { get; set; }

        public int idEstadoMsjeExportacion { get; set; }

        public UI_PrtEnt06()
        {

            #region "VARIABLES DINAMICA"
            Titulo = new UI_Label();

            #endregion

            #region "LISTA COMISION Y EVENTOS"
            lista_com = new UI_Combo();
            aceptar = new UI_Button()
            {

                ID = "BtnAceptar",
                Text = "Aceptar",
                Enabled = true
            };
            cancelar = new UI_Button()
            {
                ID = "BtnCancelar",
                Text = "Cancelar",
                Enabled = true
            };
            Agregar = new UI_Button()
            {
                ID = "BtnAgregar",
                Text = "Agregar",
                Enabled = true
            };
            Eliminar = new UI_Button()
            {
                ID = "BtnEliminar",
                Text = "Eliminar",
                Enabled = true
            };
            #endregion

            #region "INTERES"
            prttasint = new UI_TextBox();
            _prttipoi_ = new List<UI_OptionItem>() {
                new UI_OptionItem { ID="0", Value="Libor", Selected=false },
                new UI_OptionItem { ID="1", Value="Prime",Selected=false },
                new UI_OptionItem { ID="2", Value="Nominal",Selected=false }
            };
            prtflot = new UI_CheckBox();
            #endregion

            #region "GASTO"
            prtmongas = new UI_TextBox();
            tarifa = new UI_CheckBox();
            #endregion

            #region "LISTA MODULO"
            lista = new UI_Combo();
            #endregion

            #region "PANEL"
            prtcomision = new UI_Panel();
            prtgasto = new UI_Panel();
            prtinteres = new UI_Panel();
            #endregion

            #region "MENU"

            _menu_ = new List<UI_Button>()  //OK Nivel 0
            {
                new UI_Button { ID="0", Tag="IMP", Text="Importaciones" },
                new UI_Button { ID="1", Tag="EXP", Text ="Exportaciones" },
                new UI_Button { ID="2", Tag="SER", Text ="Servicios" },
                new UI_Button { ID="3", Tag="", Text ="Imprimir" }
            };

            //IMPORTACIONES                
            _prod_ = new List<UI_Button>() 
            {
               new UI_Button { ID="0", Tag="COB",  Text="Cobranzas" },
                new UI_Button { ID="1", Tag="CRE", Text ="Carta Crédito" }
            };

            _etacob_ = new List<UI_Button>() //Cobranzas 
            {
               new UI_Button { ID="0", Tag="CCP",  Text="Comisión pago" },
               new UI_Button { ID="1", Tag="ANU", Text ="Anulación" },
               new UI_Button { ID="2", Tag="CTB", Text ="Traspaso otros bancos" },
               new UI_Button { ID="3", Tag="CPR", Text ="Prórroga" },
               new UI_Button { ID="4", Tag="MAN", Text ="Mantención" }
            };

            _etacar_ = new List<UI_Button>() //Carta Credito
            {
               new UI_Button { ID="0", Tag="CAP",  Text="Apertura" },
               new UI_Button { ID="1", Tag="CM1", Text ="Modificación 1" },
               new UI_Button { ID="2", Tag="CM2", Text ="Modificación 2" },
               new UI_Button { ID="3", Tag="CAS", Text ="Anulación saldo" },
               new UI_Button { ID="4", Tag="CNC", Text ="Negociación a cobertura" },
               new UI_Button { ID="5", Tag="CNV", Text ="Negociación a vencimiento obligación" },
               new UI_Button { ID="6", Tag="CPR", Text ="Prórroga" }
            };


            //EXPORTACIONES          
            _prodexp_ = new List<UI_Button>()  //OK     
            {
               new UI_Button { ID="0", Tag="PAE",  Text="Pae" },
               new UI_Button { ID="1", Tag="ECD", Text ="Compra Documentos" },
               new UI_Button { ID="2", Tag="EDD", Text ="Descuentos Documentos" },
               new UI_Button { ID="3", Tag="CRE", Text ="Carta de Crédito" },
               new UI_Button { ID="4", Tag="EPA", Text ="Pago Anticipado Carta Crédito" },
               new UI_Button { ID="5", Tag="COB", Text ="Cobranzas" },
               new UI_Button { ID="6", Tag="RET", Text ="Retorno" }
            };

            _etapae_0 = new UI_Button()
            {
                ID = "0",
                Tag = "ING",
                Text = "Ingreso"
            };

            _etacom_ = new List<UI_Button>()  //OK           
            {
               new UI_Button { ID="0", Tag="CGP",  Text="Gestión país" },
               new UI_Button { ID="1", Tag="CGA", Text ="Gestión Aladi" },
               new UI_Button { ID="2", Tag="CGO", Text ="Gestión otros países" }
            };
            _etades_ = new List<UI_Button>()    //OK        
            {
               new UI_Button { ID="0", Tag="DGP",  Text="Gestión país" },
               new UI_Button { ID="1", Tag="DGA", Text ="Gestión Aladi" },
               new UI_Button { ID="2", Tag="DGO", Text ="Gestión otros países" }
            };
            _etacred_ = new List<UI_Button>()  //OK
            {
               new UI_Button { ID="0", Tag="AVI",  Text="Aviso" },
               new UI_Button { ID="1", Tag="CNF", Text ="Confirmación" },
               new UI_Button { ID="2", Tag="MOD", Text ="Modificación" },
               new UI_Button { ID="3", Tag="TRF", Text ="Transferencia" },
               new UI_Button { ID="4", Tag="NUT", Text ="No utilización" },
               new UI_Button { ID="5", Tag="PRD", Text ="Pago y/o Revisión Doc." },
               new UI_Button { ID="6", Tag="RDD", Text ="Revisión Doc. Discrep." },
               new UI_Button { ID="7", Tag="TRS", Text ="Traspaso" },
               new UI_Button { ID="8", Tag="AV2", Text ="Aviso 2º Beneficiario" }
            };

            _etapa_ = new List<UI_Button>()  //OK
            {
               new UI_Button { ID="0", Tag="CMN",  Text="Manejo" },
               new UI_Button { ID="1", Tag="CMJ", Text ="Mensaje" }             
            };

            _etaret_ = new List<UI_Button>()  //OK
            {
               new UI_Button { ID="0", Tag="RTR",  Text="Traspaso" },
               new UI_Button { ID="1", Tag="RAD", Text ="Adición 3 planillas" }             
            };

            //SERVICIOS
            //--------1 nivel          
            _prodser_ = new List<UI_Button>()  //OK         
            {
               new UI_Button { ID="0", Tag="SEP",  Text="Endoso planilla" },
               new UI_Button { ID="1", Tag="STI", Text ="Traspaso IDI", Visible = false },
               new UI_Button { ID="2", Tag="CFS", Text ="Cobertura flete/seguro importación" },
               new UI_Button { ID="3", Tag="SEF", Text ="Ex-financiamiento" },
               new UI_Button { ID="4", Tag="EFS", Text ="Venta divisas" },
               new UI_Button { ID="5", Tag="SOP", Text ="Orden de pago condicionado" }            
            };

            _etafin_ = new List<UI_Button>()  //OK
            {
               new UI_Button { ID="0", Tag="EPR",  Text="Proceso" },
               new UI_Button { ID="1", Tag="EAN", Text ="Anulación" }             
            };

            _etaor_ = new List<UI_Button>()  //OK
            {
               new UI_Button { ID="0", Tag="OAV",  Text="Aviso" },
               new UI_Button { ID="1", Tag="OPA", Text ="Pago" }             
            };

            //Imprimir
            _imp_ = new UI_Button()
            {
                ID = "0",
                Tag = "IMP",
                Text = "Imprimir"

            };




            #endregion

            EstadoMsjeConfirmacion = new int();
            idEstadoMsjeExportacion = new int();

        }
    }
}
