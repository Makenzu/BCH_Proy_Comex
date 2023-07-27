using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Portal
{
    public interface ICodigosSucursal
    {
        string samAccountName { get; set; }
        string CentroCosto { get; set; }
        string CodPBC { get; set; }
        string CodBCH { get; set; }
        string SucBCH { get; set; }
        string CodBCCH { get; set; }
    }
}