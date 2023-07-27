using System.Collections.Generic;

namespace BCH.Comex.Common.XGPY.UI_Modulos
{
   public class UI_PrtEnt01
    {
       public List<UI_Combo_> Listarazones { get; set; }
       public List<UI_Combo_> Listadirec { get; set; }
       public UI_TextBox_ Link { get; set; }

       public UI_PrtEnt01()
       {
           Listarazones = new List<UI_Combo_>();
           Listadirec = new List<UI_Combo_>();
           Link = new UI_TextBox_();
       }



    }
}
