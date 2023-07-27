using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes
{
    public class Planilla
    {
        public string Tint { set; get; }
        public List<Detalle> DetInt { set; get; }
    }
}
