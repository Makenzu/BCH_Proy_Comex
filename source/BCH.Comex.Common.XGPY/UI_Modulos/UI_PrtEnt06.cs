using System.Collections.Generic;

namespace BCH.Comex.Common.XGPY.UI_Modulos
{
    public class UI_PrtEnt06
    {

        #region "VARIABLES DINAMICOS"

        public UI_Label Titulo { get; set; }

        #endregion

        #region "LISTA COMISION Y EVENTOS"
        public UI_Combo_ lista_com { get; set; }
        public UI_Button_ aceptar { get; set; }
        public UI_Button_ cancelar { get; set; }
        public UI_Button_ Agregar { get; set; }
        public UI_Button_ Eliminar { get; set; }

        #endregion

        #region "INTERES"
        public UI_TextBox_ prttasint { get; set; }
        public List<UI_OptionItem_> _prttipoi_ { get; set; }
        public UI_CheckBox_ prtflot { get; set; }//UserControl

        #endregion

        #region "GASTO"
        public UI_TextBox_ prtmongas { get; set; }
        public UI_CheckBox_ tarifa { get; set; }//public UI_TextBox_ tarifa { get; set; }//UserControl   

        #endregion

        #region "LISTA MODULO"
        public UI_Combo_ lista { get; set; }

        #endregion

        #region "FRAME"
        public UI_Panel prtcomision { get; set; }
        public UI_Panel prtgasto { get; set; }
        public UI_Panel prtinteres { get; set; }

        #endregion


        #region "MENU"
        public List<UI_Button_> _menu_ { get; set; }

        //-------------Importaciones
        public List<UI_Button_> _prod_ { get; set; }
        public List<UI_Button_> _etacob_ { get; set; }
        public List<UI_Button_> _etacar_ { get; set; }

        //Exportaciones
        //-------Exportaciones 1 nivel
        public List<UI_Button_> _prodexp_ { get; set; }

        //----------PAE - Ingreso
        public UI_Button_ _etapae_0 { get; set; }

        //---------Compra de documentos (Gestión país,Gestión Aladi,Gestión otros países
        public List<UI_Button_> _etacom_ { get; set; }

        //---------Descuentos documentos (Gestion pais,Gestion Aladi,Gestion otros países
        public List<UI_Button_> _etades_ { get; set; }

        //---------Carta de Crédito
        public List<UI_Button_> _etacred_ { get; set; }
        public List<UI_Button_> _etapa_ { get; set; }
        public List<UI_Button_> _etaret_ { get; set; }

        //Servicios
        public List<UI_Button_> _prodser_ { get; set; }
        //---------Ex financiamiento
        public List<UI_Button_> _etafin_ { get; set; }

        // Orden de Pago condicionado
        public List<UI_Button_> _etaor_ { get; set; }
        public UI_Button_ _imp_ { get; set; }

        #endregion


        public UI_PrtEnt06()
        {

            #region "VARIABLES DINAMICA"
            Titulo = new UI_Label();

            #endregion

            #region "LISTA COMISION Y EVENTOS"
            lista_com = new UI_Combo_();
            aceptar = new UI_Button_();
            cancelar = new UI_Button_();
            Agregar = new UI_Button_()
            {
                ID = "Agregar",
                Text = "Agregar",
                Enabled = true
            };
            Eliminar = new UI_Button_();
            #endregion

            #region "INTERES"
            prttasint = new UI_TextBox_();
            _prttipoi_ = new List<UI_OptionItem_>() {
                new UI_OptionItem_ { ID="0", Value="Libor", Selected=true },
                new UI_OptionItem_ { ID="1", Value="Prime" },
                new UI_OptionItem_ { ID="2", Value="Nominal" }
            };
            prtflot = new UI_CheckBox_();
            #endregion

            #region "GASTO"
            prtmongas = new UI_TextBox_();
            tarifa = new UI_CheckBox_();
            #endregion

            #region "LISTA MODULO"
            lista = new UI_Combo_();
            #endregion

            #region "PANEL"
            prtcomision = new UI_Panel();
            prtgasto = new UI_Panel();
            prtinteres = new UI_Panel();
            #endregion

            #region "MENU"

            _menu_ = new List<UI_Button_>()  //OK Nivel 0
            {
                new UI_Button_ { ID="0", Tag="IMP", Text="Importaciones" },
                new UI_Button_ { ID="1", Tag="EXP", Text ="Exportaciones" },
                new UI_Button_ { ID="2", Tag="SER", Text ="Servicios" },
                new UI_Button_ { ID="3", Tag="", Text ="Imprimir" }
            };

            //IMPORTACIONES                
            _prod_ = new List<UI_Button_>() 
            {
               new UI_Button_ { ID="0", Tag="COB",  Text="Cobranzas" },
                new UI_Button_ { ID="1", Tag="CRE", Text ="Carta Crédito" }
            };

            _etacob_ = new List<UI_Button_>() //Cobranzas 
            {
               new UI_Button_ { ID="0", Tag="CCP",  Text="Comisión pago" },
               new UI_Button_ { ID="1", Tag="ANU", Text ="Anulación" },
               new UI_Button_ { ID="2", Tag="CTB", Text ="Traspaso otros bancos" },
               new UI_Button_ { ID="3", Tag="CPR", Text ="Prórroga" },
               new UI_Button_ { ID="4", Tag="MAN", Text ="Mantención" }
            };

            _etacar_ = new List<UI_Button_>() //Carta Credito
            {
               new UI_Button_ { ID="0", Tag="CAP",  Text="Apertura" },
               new UI_Button_ { ID="1", Tag="CM1", Text ="Modificación 1" },
               new UI_Button_ { ID="2", Tag="CM2", Text ="Modificación 2" },
               new UI_Button_ { ID="3", Tag="CAS", Text ="Anulación saldo" },
               new UI_Button_ { ID="4", Tag="CNC", Text ="Negociación a cobertura" },
               new UI_Button_ { ID="5", Tag="CNV", Text ="Negociación a vencimiento obligación" },
               new UI_Button_ { ID="6", Tag="CPR", Text ="Prórroga" }
            };


            //EXPORTACIONES          
            _prodexp_ = new List<UI_Button_>()  //OK     
            {
               new UI_Button_ { ID="0", Tag="PAE",  Text="Pae" },
               new UI_Button_ { ID="1", Tag="ECD", Text ="Compra Documentos" },
               new UI_Button_ { ID="2", Tag="EDD", Text ="Descuentos Documentos" },
               new UI_Button_ { ID="3", Tag="CRE", Text ="Carta de Crédito" },
               new UI_Button_ { ID="4", Tag="EPA", Text ="Pago Anticipado Carta Crédito" },
               new UI_Button_ { ID="5", Tag="COB", Text ="Cobranzas" },
               new UI_Button_ { ID="6", Tag="RET", Text ="Retorno" }
            };

            _etapae_0 = new UI_Button_()
            {
                ID = "0",
                Tag = "ING",
                Text = "Ingreso"
            };

            _etacom_ = new List<UI_Button_>()  //OK           
            {
               new UI_Button_ { ID="0", Tag="CGP",  Text="Gestión país" },
               new UI_Button_ { ID="1", Tag="CGA", Text ="Gestión Aladi" },
               new UI_Button_ { ID="2", Tag="CGO", Text ="Gestión otros países" }
            };
            _etades_ = new List<UI_Button_>()    //OK        
            {
               new UI_Button_ { ID="0", Tag="DGP",  Text="Gestión país" },
               new UI_Button_ { ID="1", Tag="DGA", Text ="Gestión Aladi" },
               new UI_Button_ { ID="2", Tag="DGO", Text ="Gestión otros países" }
            };
            _etacred_ = new List<UI_Button_>()  //OK
            {
               new UI_Button_ { ID="0", Tag="AVI",  Text="Aviso" },
               new UI_Button_ { ID="1", Tag="CNF", Text ="Confirmación" },
               new UI_Button_ { ID="2", Tag="MOD", Text ="Modificación" },
               new UI_Button_ { ID="3", Tag="TRF", Text ="Transferencia" },
               new UI_Button_ { ID="4", Tag="NUT", Text ="No utilización" },
               new UI_Button_ { ID="5", Tag="PRD", Text ="Pago y/o Revisión Doc." },
               new UI_Button_ { ID="6", Tag="RDD", Text ="Revisión Doc. Discrep." },
               new UI_Button_ { ID="7", Tag="TRS", Text ="Traspaso" },
               new UI_Button_ { ID="8", Tag="AV2", Text ="Aviso 2º Beneficiario" }
            };

            _etapa_ = new List<UI_Button_>()  //OK
            {
               new UI_Button_ { ID="0", Tag="CMN",  Text="Manejo" },
               new UI_Button_ { ID="1", Tag="CMJ", Text ="Mensaje" }             
            };

            _etaret_ = new List<UI_Button_>()  //OK
            {
               new UI_Button_ { ID="0", Tag="RTR",  Text="Traspaso" },
               new UI_Button_ { ID="1", Tag="RAD", Text ="Adición 3 planillas" }             
            };

            //SERVICIOS
            //--------1 nivel          
            _prodser_ = new List<UI_Button_>()  //OK         
            {
               new UI_Button_ { ID="0", Tag="SEP",  Text="Endoso planilla" },
               new UI_Button_ { ID="1", Tag="STI", Text ="Traspaso IDI", Visible = false },
               new UI_Button_ { ID="2", Tag="CFS", Text ="Cobertura flete/seguro importación" },
               new UI_Button_ { ID="3", Tag="SEF", Text ="Ex-financiamiento" },
               new UI_Button_ { ID="4", Tag="EFS", Text ="Venta divisas" },
               new UI_Button_ { ID="5", Tag="SOP", Text ="Orden de pago condicionado" }            
            };

            _etafin_ = new List<UI_Button_>()  //OK
            {
               new UI_Button_ { ID="0", Tag="EPR",  Text="Proceso" },
               new UI_Button_ { ID="1", Tag="EAN", Text ="Anulacion" }             
            };

            _etaor_ = new List<UI_Button_>()  //OK
            {
               new UI_Button_ { ID="0", Tag="OAV",  Text="Aviso" },
               new UI_Button_ { ID="1", Tag="OPA", Text ="Pago" }             
            };

            //Imprimir
            _imp_ = new UI_Button_()
            {
                ID = "0",
                Tag = "IMP",
                Text = "Imprimir"

            };




            #endregion


        }
    }
}
