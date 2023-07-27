using BCH.Comex.Core.Entities.Cext01;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Models
{
    public class InyectarYReversarViewModel: SupervisorViewModel
    {
        public IList<AbonoCargoResultDTO> CargosYAbonosParaInyectar { get; set; }
        public IList<AbonoCargoResultDTO> CargosYAbonosParaReversar { get; set; }
    }
}