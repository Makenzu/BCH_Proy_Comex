using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.Supervisor
{
    public class T_MODCEDER
    {
        // Estructura para rescatar todos los clientes que posee un determinado Especialista.
        public List<T_Cliente> VCli = null;
        public List<T_OpeCli> VProd = null;
        public const string MgbCeder = "Ceder Cartera";

        // Estructura para registrar los contadores de los Productos en relación a un Cliente.
        public class T_OpeCli
        {
            public int CanOpe;
            public int CodPro;
        }

        public T_MODCEDER()
        {
            VCli = new List<T_Cliente>();
            VProd = new List<T_OpeCli>();
        }
    }
}
