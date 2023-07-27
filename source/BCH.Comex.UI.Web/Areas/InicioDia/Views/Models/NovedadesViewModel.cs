using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.InicioDia.Views.Models
{
    public class NovedadesViewModel
    {
        public IList<sce_nov_s01_MS_Result> Novedades { get; set; }
        public IList<UI_Message> Mensajes { get; set; }

        public NovedadesViewModel()
        {
            this.Mensajes = new List<UI_Message>();
            this.Novedades = new List<sce_nov_s01_MS_Result>();
        }
    }
}