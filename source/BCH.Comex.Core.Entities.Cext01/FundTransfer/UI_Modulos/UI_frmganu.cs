using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_frmganu
    {
        public UI_TextBox Tx_NroOpe_000 { get; set; }
        public UI_TextBox Tx_NroOpe_001 { get; set; }
        public UI_TextBox Tx_NroOpe_002 { get; set; }
        public UI_TextBox Tx_NroOpe_003 { get; set; }
        public UI_TextBox Tx_NroOpe_004 { get; set; }
        public List<UI_TextBox> Tx_NroOpe { get; set; }
        public UI_TextBox Tx_Prty { get; set; }
        //public UI_ListBox Lt_Pln { get; set; }
        public UI_TextBox Lt_Pln { get; set; }

        /// <summary>
        /// Boton Aceptar
        /// </summary>
        public UI_Button Co_Boton_000 { get; set; }
        /// <summary>
        /// Boton Cancelar
        /// </summary>
        public UI_Button Co_Boton_001 { get; set; }
        public UI_Button Cmd_Ok { get; set; } //Boton Buscar

        public UI_frmganu()
        {
            Tx_NroOpe_000 = new UI_TextBox();
            Tx_NroOpe_001 = new UI_TextBox();
            Tx_NroOpe_002 = new UI_TextBox();
            Tx_NroOpe_003 = new UI_TextBox();
            Tx_NroOpe_004 = new UI_TextBox();

            Tx_NroOpe = new List<UI_TextBox> { Tx_NroOpe_000, Tx_NroOpe_001, Tx_NroOpe_002, Tx_NroOpe_003, Tx_NroOpe_004 };
            Tx_Prty = new UI_TextBox();

            //Lt_Pln = new UI_ListBox();
            Lt_Pln = new UI_TextBox();

            Co_Boton_000 = new UI_Button();
            Co_Boton_001 = new UI_Button();
            Cmd_Ok = new UI_Button();
        }
    }
}
