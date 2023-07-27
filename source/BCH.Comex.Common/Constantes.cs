using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Common
{
    public static class Constantes
    {
        public static class AppRoles
        {
            public const string EnvioSwiftAppRole = "COMEX_SWIFT_SWSE";
            public const string ConsultaSwiftAppRole = "COMEX_SWIFT_XCES";
            public const string SupervisorAppRole = "COMEX_CAMBIOS_XGSV";
            public const string BusquedaPlanillaAppRole = "COMEX_GENERAL_XEVA";
            public const string FinDiaAppRole = "COMEX_GENERAL_XGFD";
            public const string InicioDiaAppRole = "COMEX_GENERAL_XGID";
            public const string ContabilidadGenericaAppRole = "COMEX_CAMBIOS_XGGL";
            public const string DevengoAppRole = "COMEX_GENERAL_XGCN";
            public const string MantenedorSwiftAppRole = "COMEX_SWIFT_SWMA";
            public const string RecibirMensajeSwiftAppRole = "COMEX_SWIFT_SWRE";
            public const string AdminSwiftAppRole = "COMEX_SWIFT_SWCE";
            public const string AutorizacionSwiftAppRole = "COMEX_SWIFT_SWAU";
        }
    }
}
