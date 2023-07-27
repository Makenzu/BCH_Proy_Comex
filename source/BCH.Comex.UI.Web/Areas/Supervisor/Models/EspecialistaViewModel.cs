using BCH.Comex.Core.BL.XGSV.Modulos;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Models
{
    public class EspecialistaViewModel : SupervisorViewModel
    {
        public string supervisor { get; set; }
        public List<T_Usr> ltEspecialista { get; set; }
        public List<T_Usr> ltEspecialistaFill { get; set; }
        public List<SelectListItem> opciones { get; set; }
        public bool unCierre { get; set; }
        public bool cierre { get; set; }
        public string MsgCierre { get; set; }
        public string ClassMsgCierre { get; set; }


        public EspecialistaViewModel()
        {
        }
    }
}