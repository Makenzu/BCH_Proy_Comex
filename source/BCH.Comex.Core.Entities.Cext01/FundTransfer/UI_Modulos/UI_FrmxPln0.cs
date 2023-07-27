using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_FrmxPln0
    {
        //****************************************************************************
        public const string FormatoMtoPln = "###,###,###,##0.00";
        //****************************************************************************
        public double ValAnt;
        public short ConMtoLiq;  //Registra las Planillas generadas por el Monto Líquido.
        public short ConMtoInf;  //Registra las Planillas generadas por el Monto a Informar.
        public short ConMtoEst;  //Registra las Planillas generadas por el Monto Estadístico.
        public string guardaobs = "";


        public UI_ListBox LtMto { get; set; }
        public UI_ListBox LtPln { get; set; }
        public UI_Combo LtTPln { get; set; }
        public string Titulo { get; set; }
        public UI_Combo Cb_SecEcBen { get; set; }
        public UI_Combo Cb_SecEcIn { get; set; }
        public UI_Combo Cb_Pais { get; set; }
        public UI_Combo Cb_Bco { get; set; }
        public UI_Combo Cb_Pbc { get; set; }
        public UI_Combo Cb_Tippln { get; set; }
        public UI_TextBox Tx_NumDec {get; set;}
        public UI_TextBox Tx_FecDec {get; set;}
        public UI_TextBox Tx_CodAdn {get; set;}
        public UI_Button Ok_Dec { get; set; }
        public UI_TextBox Tx_Obs { get; set; }
        public UI_TextBox Tx_Fecha { get; set; }
        public UI_TextBox Tx_NumIns { get; set; }
        public List<UI_TextBox> Tx_MtoDec { get; set; }
        public List<UI_TextBox> Tx_MtoPln { get; set; }
        public List<UI_CheckBox> Ch_Deduc { get; set; }
        public UI_TextBox Tx_FecVen { get; set; }
        public UI_TextBox Tx_PrcPar { get; set; }
        public UI_TextBox Tx_NomCom { get; set; }
        public UI_TextBox Tx_PlzFin { get; set; }
        public UI_TextBox Tx_NumPre { get; set; }
        public UI_TextBox Tx_FecPre { get; set; }
        public List<UI_Button> Boton { get; set; }
        public UI_Button Ok { get; set; }
        public UI_Button NO { get; set; }

        public UI_FrmxPln0()
        {
            LtMto = new UI_ListBox();
            LtPln = new UI_ListBox();
            LtTPln = new UI_Combo();

            Cb_SecEcBen = new UI_Combo();
            Cb_SecEcIn = new UI_Combo();

            Tx_NumDec = new UI_TextBox();
            Tx_FecDec = new UI_TextBox();
            Tx_CodAdn = new UI_TextBox();
            Ok_Dec = new UI_Button();
            Tx_Obs = new UI_TextBox() { EsTextArea = true, Rows = 12 };
            Cb_Pais = new UI_Combo();
            Cb_Bco = new UI_Combo();
            Cb_Pbc = new UI_Combo();
            Cb_Tippln = new UI_Combo();
            Tx_Fecha = new UI_TextBox();
            Tx_NumIns = new UI_TextBox();
            Tx_MtoDec = new List<UI_TextBox>();
            for (int i = 0; i < 9; i++)
            {
                Tx_MtoDec.Add(new UI_TextBox());
            }
            Tx_MtoPln = new List<UI_TextBox>();
            for (int i = 0; i < 6; i++)
            {
                Tx_MtoPln.Add(new UI_TextBox());
            }

            Ch_Deduc = new List<UI_CheckBox>();
            for (int i = 0; i < 4; i++)
            {
                Ch_Deduc.Add(new UI_CheckBox());
            }

            Tx_FecVen = new UI_TextBox();
            Tx_PrcPar = new UI_TextBox();
            Tx_NomCom = new UI_TextBox();
            Tx_PlzFin = new UI_TextBox();
            Tx_NumPre = new UI_TextBox();
            Tx_FecPre = new UI_TextBox();
            Boton = new List<UI_Button>{
                new UI_Button() 
                {
                    Text = "Aceptar"
                }, 
                new UI_Button()
                {
                    Text = "Visualizar"
                },
                new UI_Button()
                {
                    Text = "Cancelar"
                },
            };
            Ok = new UI_Button()
            {
                Text = "OK"
            };
            NO = new UI_Button()
            {
                Text = "X"
            };
        }


    }
}
