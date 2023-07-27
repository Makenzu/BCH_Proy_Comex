using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos
{
   public class UI_PrtEnt01
    {
       public List<UI_Combo> Listarazones { get; set; }
       public List<UI_Combo> Listadirec { get; set; }
       public UI_TextBox Link { get; set; }

       public UI_PrtEnt01()
       {
           Listarazones = new List<UI_Combo>();
           Listadirec = new List<UI_Combo>();
           Link = new UI_TextBox();
       }



    }
}
