using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGPL
{
    public static class MOD_PLAV
    {
        // Estados de la Planilla Visible
        public const int Errada = 2;
        public const int Pendiente = 3;
        public const int Generada = 4;
        public const int impresa = 5;
        public const int Endoba = 6;
        public const int EndBch = 7;
        // Constantes para tipo de interes
        public const int TI_PR = 1;
        public const int TI_AC = 2;
        public const int TI_IC = 3;
        public const int TI_BA = 4;

        public static readonly IDictionary<int, string> EstadoPlanilla = new Dictionary<int, string> {
            {0, "SVAL"},
            {1, "VAL"},
            {Errada, "ERR"},
            {Pendiente, "PEND"},
            {Generada, "GEN"},
            {impresa, "IMP"},
            {Endoba, "ENDO"},
            {EndBch, "ENDO"},
        };

        public static IQueryable GetFormasPago(XgplService service)
        {
            return service.GetFormasPago();
        }

        public static IDictionary<decimal, string> GetPlazasBancoCentral(XgplService service)
        {
            return service.ObtenerPlazasBancoCentral();
        }

        public static readonly IDictionary<int, string> TipoInteres = new Dictionary<int, string> {            
               {TI_PR, "PR"},                   
               {TI_AC, "AC"},
               {TI_IC, "IC"},
               {TI_BA, "BA"}                  
        };

    }
}
