using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Portal
{
    public class CodigosSucursalDTO : ICodigosSucursal
    {
        public string samAccountName { get; set; }
        public string CentroCosto { get; set; }
        public string CodPBC { get; set; }
        public string CodBCH { get; set; }
        public string SucBCH { get; set; }
        public string CodBCCH { get; set; }

        public static CodigosSucursalDTO ToDTO(CodigosSucursal input)
        {
            var output = new CodigosSucursalDTO();
            output.samAccountName = input.samAccountName;
            output.CentroCosto = input.CentroCosto;
            output.CodPBC = input.CodPBC;
            output.CodBCH = input.CodBCH;
            output.SucBCH = input.SucBCH;
            output.CodBCCH = input.CodBCCH;
            return output;
        }
    }
}