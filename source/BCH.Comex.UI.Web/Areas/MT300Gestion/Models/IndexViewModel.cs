using BCH.Comex.Common.UI_Modulos;
using System;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.MT300Gestion.Models
{
    public class IndexViewModel
    {
        public int pageSize { get; set; }
        public int rowOffset { get; set; }
        public string referencia { get; set; }
        public string destino { get; set; }
        public string cuenta { get; set; }
        public DateTime? fecha { get; set; }
        public bool usarFiltros { get; set; }
        public bool soloConsulta { get; set; }
        public IList<UI_Message> ListaMensajes { get; set; }
        public IndexViewModel()
        {
            ListaMensajes = new List<UI_Message>();
            pageSize = 25;
            rowOffset = 0;
            usarFiltros = false;
            referencia = "";
            destino = "";
            cuenta = "";
            fecha = null;
        }
    }
}