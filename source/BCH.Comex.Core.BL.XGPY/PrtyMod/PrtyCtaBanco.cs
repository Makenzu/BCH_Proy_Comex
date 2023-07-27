using System;

namespace BCH.Comex.Core.BL.XGPY.PrtyMod
{
    public class PrtyCtaBanco
    {
        public EstadoPrty estado { get; set; }
        public int indice { get; set; }
        public Boolean activa { get; set; }
        public string moneda { get; set; }
        public string cuenta { get; set; }
        public Boolean especial { get; set; }
    }
}
