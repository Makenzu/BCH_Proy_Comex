using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class T_MODXSWF
    {
        public class SwiftGenerado
        {
            public string NroOperacion { get; set; }
            public int CodMem { get; set; } 
        }

        //Arreglo de Swift's generados.-
        public List<SwiftGenerado> VxSwfGen;

        public T_MODXSWF()
        {
            this.VxSwfGen = new List<SwiftGenerado>();
        }
    }
}
