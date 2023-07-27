
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.ContabilidadGenerica.Models
{
    public class ImprimirModel: BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms.UI_Frm
    {
        public List<string> Documentos { set; get; }
        public string FileName { get; set; }
    }
}