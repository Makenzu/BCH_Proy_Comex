using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.Supervisor
{
    public class FrmgUsrDTO
    {
        public string Supervisor { get; set; }
        public Dictionary<string, string> Opcion { get; set; }
        public bool UnCierre { get; set; }
        public bool Cierre { get; set; }
        public string MsgCierre { get; set; }
        public string ClassMsgCierre { get; set; }
    }
}
