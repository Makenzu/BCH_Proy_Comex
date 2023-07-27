using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos
{
    public class UI_Frm_Participantes
    {
        public short EnLoad;
        public bool AbrirIdentParticipantes { get; set; }
        public bool AbrirDesdeCargaOperaciones { get; set; }
        public int CargaAutomatica { get; set; }

        public UI_Button Aceptar { get; set; }
        public UI_Button Cancelar { get; set; }
        public UI_Button Bot_Nem { get; set; }
        public UI_Label Label1 { get; set; }
        public UI_ListBox LstPartys { get; set; }
        public List<UI_OptionItem> Donde { get; set; }
        public List<UI_OptionItem> TipoOperacion { get; set; }
        public UI_TextBox Llave { get; set; }
        public UI_Button Identificar { get; set; }
        public UI_Button Eliminar { get; set; }
        public UI_Button Instrucciones { get; set; }
        public UI_TextBox Tx_Dir { get; set; }

        public UI_Frm_Participantes()
        {
            Aceptar = new UI_Button() { Enabled = false };
            Cancelar = new UI_Button();
            Bot_Nem = new UI_Button();
            Label1 = new UI_Label();
            LstPartys = new UI_ListBox();
            Donde = new List<UI_OptionItem>() {
                new UI_OptionItem { ID="1", Value="Base de Participantes", Selected=true },
                new UI_OptionItem { ID="2", Value="En Operación" },
            };
            Llave = new UI_TextBox() { Enabled = true };
            Identificar = new UI_Button() { Text = "Identificar" };
            Eliminar = new UI_Button() { Enabled = false };
            Instrucciones = new UI_Button();
            Tx_Dir = new UI_TextBox();
            CargaAutomatica = 0;

            TipoOperacion = new List<UI_OptionItem>() {
                new UI_OptionItem { ID="20", Value="Banco de Chile", Selected=true },
                new UI_OptionItem { ID="30", Value="Cosmos" },
            };
        }
    }
}
