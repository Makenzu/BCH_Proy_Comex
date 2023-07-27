using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica
{
    public class FrmgAsoDTO
    {
        public virtual List<UI_Message> Errores { get; set; }
        public virtual UI_Combo Cb_Producto { get; set; }
        public virtual UI_TextBox Tx_NumOpe_000 { get; set; }
        public virtual UI_TextBox Tx_NumOpe_001 { get; set; }
        public virtual UI_TextBox Tx_NumOpe_002 { get; set; }
        public virtual UI_TextBox Tx_NumOpe_003 { get; set; }
        public virtual UI_TextBox Tx_NumOpe_004 { get; set; }
        public virtual UI_TextBox Tx_NumOpe_005 { get; set; }
        public virtual UI_TextBox Tx_NumOpe_006 { get; set; }
        public virtual UI_TextBox Tx_DirPrt { get; set; }
        public virtual UI_TextBox Tx_NomPrt { get; set; }
        public virtual UI_TextBox Tx_RutPrt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FrmgAsoDTO()
        {
            Cb_Producto = new UI_Combo();
            Tx_NumOpe_000 = new UI_TextBox();
            Tx_NumOpe_001 = new UI_TextBox();
            Tx_NumOpe_002 = new UI_TextBox();
            Tx_NumOpe_003 = new UI_TextBox();
            Tx_NumOpe_004 = new UI_TextBox();
            Tx_NumOpe_005 = new UI_TextBox();
            Tx_NumOpe_006 = new UI_TextBox();
            Tx_DirPrt = new UI_TextBox();
            Tx_NomPrt = new UI_TextBox();
            Tx_RutPrt = new UI_TextBox();
            Errores = new List<UI_Message>();
        }
    }
}
