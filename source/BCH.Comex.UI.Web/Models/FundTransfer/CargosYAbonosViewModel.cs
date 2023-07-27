using BCH.Comex.Core.Entities.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class CargosYAbonosViewModel
    {
        public IList<AbonoCargoResultDTO> CargosYAbonosParaInyectar { get; set; }
        public IList<AbonoCargoResultDTO> CargosYAbonosParaReversar { get; set; }
    }
}