using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FinDia
{
    public class T_CCIRLLVR
    {
        public IList<T_jAcp> VjAcp = null;

        public T_CCIRLLVR()
        {
            VjAcp = new List<T_jAcp>();
        }
    }

    public class T_jAcp
    {
        public string codcct {get; set;}
        public string codpro {get; set;}
        public string codesp {get; set;}
        public string codofi {get; set;}
        public string codope {get; set;}
        public int numneg { get; set; }
        public int numacp { get; set; }
        public string refere { get; set; }
        public int monacp { get; set; }
        public double salacp {get; set;}
        public string venacp {get; set;}
        public string rollover { get; set; }

        public string monacpnemonico { get; set; }
    }
}
