using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Mdi_Principal
    {
        //----------------------------------------------------------------------------
        //Menu Opciones.
        //----------------------------------------------------------------------------
        public const short Pos_Partys = 0;
        //Participantes.
        public const short Pos_Anu = 2;
        //Anulación CVD.
        public const short Pos_Rev = 3;
        //Reversar CVD.
        public const short Pos_RyR = 4;
        //Reversar y Reemplazar CVD.
        public const short Pos_AnuSin = 6;
        //Anular Sin Operacion.
        public const short Pos_AnuVIm = 8;
        //Anulación Planilla Visible Import.
        // UPGRADE_INFO (#0561): The 'Pos_RevCtaCte' symbol was defined without an explicit "As" clause.
        public const short Pos_RevCtaCte = 11;
        //Reversas de Inyecciones a Cuentas Corrientes

        public UI_CheckBox mnu_cartas;
        public UI_CheckBox mnu_conta;
        public UI_CheckBox mnu_planillas;

        public List<UI_Button> Opciones;
        public Dictionary<string, UI_Button> BUTTONS { set; get; }
        public List<UI_Message> MESSAGES { set; get; }
        public bool MantenerMensajes { get; set; }
        public UI_Mdi_Principal()
        {
            Opciones = new List<UI_Button>{
                new UI_Button{
                    ID="mnu_party",
                    Tag = "~/FundTransfer/Participantes",
                    Enabled = false,
                    Text = "Participantes"
                },
                new UI_Button{
                    ID="1",
                    Tag = "separator",
                    Enabled = true,
                    Text = ""
                },
                new UI_Button{
                    ID="2",
                    Tag = "~/FundTransfer/AnulacionOperaciones",
                    Enabled = true,
                    Text = "Anular Operación (Mismo Día)"
                },
                new UI_Button{
                    ID="3",
                    Tag = "~/FundTransfer/ReversarOperacionExport",
                    Enabled = true,
                    Text = "Reverso Operación USME y EXPORT"
                },
                new UI_Button{
                    ID="4",
                    Tag = "~/FundTransfer/ReversarOperacionExport",
                    Enabled = true,
                    Text = "Reverso Invisible IMPORT"
                },
                new UI_Button{
                    ID="6",
                    Tag = "~/FundTransfer/PlanillaAnularNumero",
                    Enabled = true,
                    Text = "Anulación Plantillas sin Operación (EXP)"
                },
                new UI_Button{
                    ID="8",
                    Tag = "~/FundTransfer/AnulacionPlanillaVisibleImportIndex",
                    Enabled = true,
                    Text = "Reverso y/o Reemplazo Vis. Importaciones"
                },
                new UI_Button{
                    ID="9",
                    Tag = "separator",
                    Enabled = true,
                    Text = ""
                },
                new UI_Button{
                    ID="11",
                    Tag = "~/Supervisor/InyectarYReversar",
                    Enabled = true,
                    Text = "Reverso de Cuentas Corrientes"
                },
                new UI_Button{
                    ID="12",
                    Tag = "separator",
                    Enabled = true,
                    Text = ""
                },
                new UI_Button{
                    ID="13",
                    Tag = "~/FundTransfer/Index?refrescarSesion=true",
                    Enabled = true,
                    Text = "Refrescar Sesión"
                },
            };

            #region MENSAJES
            MESSAGES = new List<UI_Message>();
            MantenerMensajes = false;
            #endregion

            #region BOTONES
            this.BUTTONS = new Dictionary<string, UI_Button>();
            this.BUTTONS.Add("tbr_nuevo", new UI_Button()
            {
                Enabled = true,
                ID = "tbr_nuevo",
                Text = "Nuevo",
                Tag = "~/FundTransfer/NuevaOperacion",
                ImgPath = "~/Content/images/ButtonsBarMnu/Nuevo.png",
                TabIndex = "1",
                Focus = true
            });
            this.BUTTONS.Add("tbr_consulta", new UI_Button()
            {
                Enabled = true,
                ID = "tbr_consulta",
                Text = "Abrir Consulta",
                Tag = "~/FundTransfer/CargarOperaciones",
                ImgPath = "~/Content/images/ButtonsBarMnu/AbrirConsulta.png"
            });
            this.BUTTONS.Add("tbr_Comercio_invisible", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_Comercio_invisible",
                Text = "Comercio Invisible",
                Tag = "~/FundTransfer/ComercioInvisible",
                ImgPath = "~/Content/images/ButtonsBarMnu/ComercioInvisible.png",
                TabIndex = "3"
            });
            this.BUTTONS.Add("tbr_arbitrajes", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_arbitrajes",
                Text = "Arbitraje",
                Tag = "~/FundTransfer/Arbitrajes",
                ImgPath = "~/Content/images/ButtonsBarMnu/Arbitraje.png",
                TabIndex = "5"
            });
            this.BUTTONS.Add("tbr_vtas_export", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_vtas_export",
                Text = "Ventas Visibles Exportación",
                Tag = "~/FundTransfer/ComercioVisibleExport",
                ImgPath = "~/Content/images/ButtonsBarMnu/VentasVisExport.png",
                TabIndex = "6"
            });
            this.BUTTONS.Add("tbr_vtas_import", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_vtas_import",
                Text = "Ventas Visibles Import",
                Tag = "~/FundTransfer/PlanillaVisibleImport",
                ImgPath = "~/Content/images/ButtonsBarMnu/VentasVisiblesImport.png",
                TabIndex = "4"
            });
            this.BUTTONS.Add("tbr_grabar", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_grabar",
                Text = "",
                Tag = "~/FundTransfer/Grabar1",
                ImgPath = "~/Content/images/ButtonsBarMnu/Grabar.png",
                TabIndex = "12"
            });
            this.BUTTONS.Add("tbr_impresion", new UI_Button()
            {
                Enabled = true,
                ID = "tbr_impresion",
                Text = "Impresión de Documentos",
                Tag = "~/FundTransfer/ImpresionDeDocumentos",
                ImgPath = "~/Content/images/ButtonsBarMnu/ReimpresionDocumentos.png"
            });
            this.BUTTONS.Add("tbr_participantes", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_participantes",
                Text = "Participantes",
                Tag = "~/FundTransfer/Participantes",
                ImgPath = "~/Content/images/ButtonsBarMnu/Participantes.png",
                TabIndex = "2"
            });
            this.BUTTONS.Add("tbr_operel", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_operel",
                Text = "Relacionar Operación",
                Tag = "~/FundTransfer/RelacionarOperacion",
                ImgPath = "~/Content/images/ButtonsBarMnu/OperacionRelacionada.png"
            });
            this.BUTTONS.Add("tbr_comisiones", new UI_Button()
            {
                Enabled = true,
                ID = "tbr_comisiones",
                Text = "Comisiones",
                Tag = "~/FundTransfer/Comisiones",
                ImgPath = "~/Content/images/ButtonsBarMnu/Comisiones.png",
                TabIndex = "7"
            });
            this.BUTTONS.Add("tbr_dedfondos", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_dedfondos",
                Text = "Vias",
                Tag = "~/FundTransfer/DestinoFondos?hayMensaje=false&respuestaMensaje=false",
                ImgPath = "~/Content/images/ButtonsBarMnu/Vias.png",
                TabIndex = "8"
            });
            this.BUTTONS.Add("tbr_origfondos", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_origfondos",
                Text = "Origenes",
                Tag = "~/FundTransfer/OrigenFondos",
                ImgPath = "~/Content/images/ButtonsBarMnu/Origenes.png",
                TabIndex = "9"
            });
            this.BUTTONS.Add("tbr_Swift", new UI_Button()
            {
                Enabled = true,
                ID = "tbr_Swift",
                Text = "Swift",
                Tag = "~/Fundtransfer/GenerarSwift",
                ImgPath = "~/Content/images/ButtonsBarMnu/Swift.png",
                TabIndex = "10"
            });
            this.BUTTONS.Add("tbr_vueltos", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_vueltos",
                Text = "Vueltos",
                Tag = "~/FundTransfer/Vueltos",
                ImgPath = "~/Content/images/ButtonsBarMnu/Vueltos.png"
            });
            this.BUTTONS.Add("tbr_Gchq", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_Gchq",
                Text = "Emitir Cheque",
                Tag = "~/Fundtransfer/EmitirCheque",
                ImgPath = "~/Content/images/ButtonsBarMnu/GeneradordeCheques.png",
                TabIndex = "11"
            });
            this.BUTTONS.Add("tbr_nota", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_nota",
                Text = "Emitir Nota de Crédito",
                Tag = "~/FundTransfer/EmitirNotaCredito",
                ImgPath = "~/Content/images/ButtonsBarMnu/NotadeCredito.png"
            });

            this.BUTTONS.Add("tbr_planilla1", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_planilla1",
                Text = "Planilla Invisible Editar",
                Tag = "~/FundTransfer/PlanillaInvisibleEditar",
                ImgPath = "~/Content/images/ButtonsBarMnu/PlanillasInvisibles.png"
            });
            this.BUTTONS.Add("tbr_planilla2", new UI_Button()
            {
                Enabled = true,
                ID = "tbr_planilla2",
                Text = "Planillas Visibles Export",
                Tag = "~/FundTransfer/PlanillaIngresoVisibleExport",
                ImgPath = "~/Content/images/ButtonsBarMnu/PlanillasVisiblesExport.png"
            });
            this.BUTTONS.Add("tbr_planilla3", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_planilla3",
                Text = "Planillas de Anulación",
                Tag = "~/",
                ImgPath = "~/Content/images/ButtonsBarMnu/PlanillasdeAnulacion.png"
            });
            this.BUTTONS.Add("tbr_planilla4", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_planilla4",
                Text = "Editar de Plantilla de Importación",
                Tag = "~/FundTransfer/PlanillaVisibleImportEditar",
                ImgPath = "~/Content/images/ButtonsBarMnu/PlanillasVisiblesImpor.png"
            });
            this.BUTTONS.Add("tbr_cargo_abono", new UI_Button()
            {
                Enabled = true,
                ID = "tbr_cargo_abono",
                Text = "Cargos y Abonos",
                Tag = "~/Supervisor/InyectarYReversar",
                ImgPath = "~/Content/images/ButtonsBarMnu/CargoAbonodeOperaciones.png"
            });
            this.BUTTONS.Add("tbr_excel", new UI_Button()
            {
                Enabled = false,
                ID = "tbr_excel",
                Text = "Listado SPOT y OP",
                Tag = "~/",
                ImgPath = "~/Content/images/ButtonsBarMnu/ListadoSPOTyOP.png"
            });
            //this.BUTTONS.Add("tbr_lista", new UI_Button()
            //{
            //    Enabled = false,
            //    ID = "tbr_lista",
            //    Text = "",
            //    Tag = "~/",
            //    ImgPath = "~/Content/images/ButtonsBarMnu/"
            //});
            //this.BUTTONS.Add("tbr_Salida", new UI_Button()
            //{
            //    Enabled = true,
            //    ID = "tbr_Salida",
            //    Text = "",
            //    Tag = "~/",
            //    ImgPath = "~/Content/images/ButtonsBarMnu/Salir.png"
            //});
            #endregion

            mnu_cartas = new UI_CheckBox();
            mnu_conta = new UI_CheckBox();
            mnu_planillas = new UI_CheckBox();
        }

        public short NumParty { get; set; }

    }

    //public enum TipoMensaje
    //{
    //    Nada = 0,
    //    Correcto = 1,
    //    Informacion = 2,
    //    Error = 3,
    //    Critical = 4,
    //    YesNo = 5,
    //    Warning = 6
    //}
    //public class UI_Message
    //{
    //    public TipoMensaje Type { set; get; }
    //    public string Text { set; get; }
    //    public string Title { get; set; }
    //    public string ControlName { get; set; } //se agrega para hacer referencia a un control determinado
    //    public bool AutoClose { get; set; }

    //    public UI_Message()
    //    {
    //        this.Type = TipoMensaje.Nada;
    //        this.Text = String.Empty;
    //        this.Title = String.Empty;
    //    }

    //    public bool IsError
    //    {
    //        get
    //        {
    //            return (this.Type == TipoMensaje.Error || this.Type == TipoMensaje.Critical);
    //        }
    //    }

    //}
}
