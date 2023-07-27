using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.MT300Planilla.Models
{
    public class IndexViewModel
    {
        public int pageSize { get; set; }
        public bool soloConsulta { get; set; }
        public IList<UI_Message> ListaMensajes { get; set; }
        public IndexViewModel()
        {
            ListaMensajes = new List<UI_Message>();
            pageSize = 25;
        }
    }
}