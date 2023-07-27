using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using System;
using System.Collections.Generic;
using System.Web;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms
{
    public class UI_gl : UI_Frm
    {
        public string Glosa = "";
        public int delista = 0;
        public PartyKey prty_cliente = new PartyKey();
        public PartyKey PartyVacio = new PartyKey();
        public int retorno_vueltos = 0;
        public int EnLoad = 0;
        public int ya_retorno = 0;
        public int en_salvar = 0;
        public int en_swift = 0;
        public int en_docval = 0;
        public int desde_lista = 0;
        public int misma_moneda = 0;
        public int cambio_tipo = 0;
        public int cambio_nemo = 0;
        public string nommon_ant = "";
        public string nemmon_ant = "";
        public int cambio_moneda = 0;
        public string NEMO_INI = "";
        public int en_escribe = 0;
        public int hab_swift = 0;
        public int hab_docval = 0;
        public int debe_pedir = 0;
        public string txtReferenciaCliente = string.Empty;
        public int debe = 0;
        public string Mon = "";
        public string nemo = "";
        public int codmnd = 0;
        public string Moneda = "";
        public int ind_ovd = 0;
        public int Cual = 0;
        public string Nem = "";

        const int HELP_CONTEXT = 0x1;     // Display topic in ulTopic
        const int HELP_QUIT = 0x2;     // Terminate help
        const int HELP_INDEX = 0x3;     // Display index
        const int HELP_CONTENTS = 0x3;
        const int HELP_HELPONHELP = 0x4;     // Display help on using help
        const int HELP_SETINDEX = 0x5;     // Set the current Index for multi index help
        const int HELP_SETCONTENTS = 0x5;
        const int HELP_CONTEXTPOPUP = 0x8;
        const int HELP_FORCEFILE = 0x9;
        const int HELP_KEY = 0x101;     // Display topic for keyword in offabData
        const int HELP_COMMAND = 0x102;
        const int HELP_PARTIALKEY = 0x105;     // call the search engine in winhelp
        const int Pos_Indice = 0;
        const int Pos_Buscar = 1;
        const int Pos_Uso = 3;
        const int Pos_Acerca = 5;
        const int Pos_Sonidos = 0;


        public List<UI_CheckBox> tipo;
        public List<UI_CheckBox> config;

        public UI_TextBox EnlacexDoc;
        public UI_TextBox Impresion;
        public UI_TextBox Contab;

        public UI_TextBox Imprime;
        public UI_TextBox vias_link;
        public UI_TextBox origen_link;
        public UI_TextBox contable;

        public UI_Grid m_n;
        public UI_Grid m_e;
        public UI_Grid me_sort;
        public UI_Grid mn_sort;

        public UI_TextBox txtNumRef;
        public UI_Combo monedas;
        public UI_TextBox nemonico;
        public UI_TextBox monto;
        public UI_TextBox Cliente;
        public UI_TextBox Num_Op;
        public UI_Button ver;
        public UI_Button aceptar;
        public UI_Button cancelar;
        public UI_CheckBox Impuesto;
        public UI_CheckBox cambiar;
        public UI_TextBox Tx_NroFac;
        public UI_TextBox Tx_moneda;
        public UI_TextBox Tx_tipo;
        public UI_TextBox Tx_neto;
        public UI_TextBox Tx_iva;
        public UI_TextBox Tx_MtoOri;
        public UI_TextBox Tx_ReferenciaCliente;

        public UI_CheckBox tipo_001;
        public UI_CheckBox tipo_000;

        //******************************************* CONFIGURACION
        public UI_CheckBox ChkImpresionCartas;
        public UI_CheckBox ChkImpresionContabilidad;
        public UI_CheckBox config3;

        //******************************************* BOTONES
        public UI_Button Bot_Nueva;
        public UI_Button Bot_Salvar;
        public UI_Button Bot_Partys;
        public UI_Button bot_calculadora;
        public UI_Button Menu_Imprimir;
        public UI_Button bot_operacion;
        public UI_Button Bot_Sal;
        public UI_Button bot_factura;
        public UI_Button menu_anulaGL;
        public List<UI_Button> botones;

        //******************************************* FRAMES
        public UI_Frame datos;
        public UI_Frame Frame3D1;
        public UI_Frame frame_ext;
        public UI_Frame frame_nac;

        public int INDICE_TICKET { get; set; }
        public UI_Label LB_Referencia { get; set; }
        public double MON { get; set; }
        public int IND_OVD { get; set; }
        public string MONEDAD { get; set; }
        public bool ESMN { get; set; }
        public int INDEX_LISTA { get; set; }

        public Dictionary<string, List<Tuple<string, double, double, string, int>>> Dic_MN;
        public Dictionary<string, List<Tuple<string, double, double, string, int>>> Dic_ME;

        public bool ValidaMontos { get; set; }

        public UI_gl()
        {
            Text = String.Empty;
            #region BOTONES
            Bot_Nueva = new UI_Button()
            {
                Enabled = true,
                ID = "tbr_nuevo",
                Text = "Nuevo",
                Tag = "~/ContabilidadGenerica/Nueva/Operacion",
                ImgPath = "~/Content/images/ButtonsBarMnu/Nuevo.png",
                TabIndex = "1"
            };
            Bot_Salvar = new UI_Button()
            {
                Enabled = false,
                ID = "tbr_grabar",
                Text = "",
                Tag = "~/ContabilidadGenerica/Grabar/Inicicar",
                ImgPath = "~/Content/images/ButtonsBarMnu/Grabar.png",
                TabIndex = "7"
            };
            Bot_Partys = new UI_Button()
            {
                Enabled = false,
                ID = "tbr_participantes",
                Text = "Participantes",
                Tag = "~/ContabilidadGenerica/Participantes/Identificar",
                ImgPath = "~/Content/images/ButtonsBarMnu/Participantes.png",
                TabIndex = "2"
            };
            bot_calculadora = new UI_Button()
            {
                Enabled = false,
                ID = "tbr_calculadora",
                Text = "Calculadora",
                Tag = "~/ContabilidadGenerica/Calcular/Contabilidad",
                ImgPath = "~/Content/images/ButtonsBarMnu/calculator.png"
            };
            Menu_Imprimir = new UI_Button()
            {
                Enabled = true,
                ID = "tbr_impresion",
                Text = "Impresión de Documentos",
                Tag = "~/Impresion/ImpresionDeDocumentos/ImpresionDeDocumentos?urlHome=" + VirtualPathUtility.ToAbsolute("~/ContabilidadGenerica/Home/"),//ContabilidadGenerica/Home/,
                ImgPath = "~/Content/images/ButtonsBarMnu/ReimpresionDocumentos.png"
            };
            bot_operacion = new UI_Button()
            {
                Enabled = false,
                ID = "tbr_oper",
                Text = "Relacionar Operación",
                Tag = "~/ContabilidadGenerica/RelacionarOperacion/Index",
                ImgPath = "~/Content/images/ButtonsBarMnu/OperacionRelacionada.png"
            };
            bot_factura = new UI_Button()
            {
                Enabled = false,
                ID = "tbr_factura",
                Text = "Emitir Nota de Crédito",
                Tag = "~/ContabilidadGenerica/EmitirNotaCredito/Index",
                ImgPath = "~/Content/images/ButtonsBarMnu/NotadeCredito.png"
            };
            menu_anulaGL = new UI_Button()
            {
                Enabled = false,
                ID = "tbr_anular",
                Text = "Anular GL",
                Tag = "~/ContabilidadGenerica/AnularContabilidad/Index",
                ImgPath = "~/Content/images/ButtonsBarMnu/PlanillasdeAnulacion.png"
            };
            ver = new UI_Button();
            aceptar = new UI_Button();
            cancelar = new UI_Button();
            botones = new List<UI_Button>();

            botones.Add(Bot_Nueva);
            botones.Add(Bot_Salvar);
            botones.Add(Menu_Imprimir);

            botones.Add(bot_operacion);
            botones.Add(Bot_Partys);
            botones.Add(bot_factura);

            botones.Add(bot_calculadora);

            #endregion
            #region CONFIGURACION
            config = new List<UI_CheckBox>();
            ChkImpresionCartas = new UI_CheckBox() { ID = "ChkImpresionCartas", Checked = false };
            ChkImpresionContabilidad = new UI_CheckBox() { ID = "ChkImpresionContabilidad", Checked = false };
            config.Add(ChkImpresionCartas);
            config.Add(ChkImpresionContabilidad);
            #endregion
            #region TIPOS
            tipo = new List<UI_CheckBox>();
            tipo_000 = new UI_CheckBox() { ID = "debehaber1", Checked = true };
            tipo_001 = new UI_CheckBox() { ID = "debehaber2", Checked = false };
            tipo.Add(tipo_000);
            tipo.Add(tipo_001);
            Impuesto = new UI_CheckBox() { Checked = false, Enabled = true };
            { }
            cambiar = new UI_CheckBox() { };
            #endregion
            #region LISTAS
            m_n = new UI_Grid() { ID = "m_n", ListIndex = -1, Enabled = true, Visible = true };
            m_e = new UI_Grid() { ID = "m_e", ListIndex = -1, Enabled = true, Visible = true };
            me_sort = new UI_Grid();
            mn_sort = new UI_Grid();
            #endregion
            #region TEXTOS
            EnlacexDoc = new UI_TextBox() { };
            Impresion = new UI_TextBox() { };
            Contab = new UI_TextBox() { };
            Imprime = new UI_TextBox() { };
            vias_link = new UI_TextBox() { };
            origen_link = new UI_TextBox() { };
            contable = new UI_TextBox() { };
            txtNumRef = new UI_TextBox() { Visible = false };
            nemonico = new UI_TextBox() { };
            monto = new UI_TextBox() { };
            Cliente = new UI_TextBox();
            Num_Op = new UI_TextBox();
            Tx_NroFac = new UI_TextBox();
            Tx_moneda = new UI_TextBox();
            Tx_tipo = new UI_TextBox();
            Tx_neto = new UI_TextBox();
            Tx_iva = new UI_TextBox();
            Tx_MtoOri = new UI_TextBox();
            Tx_ReferenciaCliente = new UI_TextBox();
            monedas = new UI_Combo() { };
            #endregion
            datos = new UI_Frame();
            Frame3D1 = new UI_Frame();

            Frame3D1.Controles.Add(Cliente);
            Frame3D1.Controles.Add(Num_Op);
            Frame3D1.Controles.Add(Tx_NroFac);
            Frame3D1.Controles.Add(Tx_moneda);
            Frame3D1.Controles.Add(Tx_tipo);
            Frame3D1.Controles.Add(Tx_neto);
            Frame3D1.Controles.Add(Tx_iva);
            Frame3D1.Controles.Add(Tx_MtoOri);
            Frame3D1.Enabled = false;

            datos.Controles.Add(monedas);
            datos.Controles.Add(nemonico);
            datos.Controles.Add(monto);
            datos.Controles.Add(txtNumRef);
            datos.Controles.Add(ver);
            datos.Controles.Add(aceptar);
            datos.Controles.Add(cancelar);
            datos.Controles.Add(tipo_000);
            datos.Controles.Add(tipo_001);
            datos.Controles.Add(cambiar);
            datos.Controles.Add(Impuesto);
            datos.Controles.Add(Tx_ReferenciaCliente);

            frame_ext = new UI_Frame();
            frame_ext.Controles.Add(m_e);
            frame_ext.Enabled = false;

            frame_nac = new UI_Frame();
            frame_nac.Controles.Add(m_n);
            frame_nac.Enabled = false;

            LB_Referencia = new UI_Label() { Visible = false };

            Dic_ME = new Dictionary<string, List<Tuple<string, double, double, string, int>>>();
            Dic_MN = new Dictionary<string, List<Tuple<string, double, double, string, int>>>();

        }
    }
}
