using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos
{
    public class UI_PrtEnt13
    {
        public UI_CheckBox manual { get; set; }//UserControl
        public UI_TextBox fecha { get; set; }//UserControl
        public UI_CheckBox fijo { get; set; }//UserControl
        public UI_TextBox tasa { get; set; }
        public UI_TextBox monto { get; set; }
        public UI_TextBox minimo { get; set; }
        public UI_TextBox maximo { get; set; }
        public UI_Button aceptar { get; set; }
        public UI_Button cancelar { get; set; }

        public UI_TextBox Lb_fecing { get; set; } //Hidden

        public UI_Frame Frame1 { get; set; }

        public UI_TextBox MontoTagFrm { get; set; }
        public int MarcaMensaje { get; set; }
        public int idEstadotasa { get; set; }
        public int idEstadoMonto { get; set; }
      //  public int idEstadoTasa { get; set; }

        public UI_PrtEnt13()
        {
            manual = new UI_CheckBox();
            fecha = new UI_TextBox();
            fijo = new UI_CheckBox();
            tasa = new UI_TextBox();
            monto = new UI_TextBox();
            minimo = new UI_TextBox();
            maximo = new UI_TextBox();
            aceptar = new UI_Button();
            cancelar = new UI_Button();
            Lb_fecing = new UI_TextBox();
            Frame1 = new UI_Frame();
            MontoTagFrm = new UI_TextBox();
            MarcaMensaje = new int();
            idEstadotasa = new int();
            idEstadoMonto = new int();
          //  idEstadoTasa = new int();
        }
    }
}
