using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms
{
    public class UI_FrmGlanu : UI_Frm
    {
        public UI_TextBox Tx_NroRep { get; set; }
        public UI_TextBox Tx_FecRep { get; set; }
        public UI_TextBox Tx_Cliente { get; set; }
        public List<FrmGlanuMovimientoDTO> ListaDatos { get; set; }
        public List<string> RptContable { get; set; }


        public UI_FrmGlanu()
        {
            Tx_NroRep = new UI_TextBox();   
            Tx_FecRep = new UI_TextBox();
            Tx_Cliente= new UI_TextBox();
            ListaDatos = new List<FrmGlanuMovimientoDTO>();
            RptContable = new List<string>();
        }

    }
}
