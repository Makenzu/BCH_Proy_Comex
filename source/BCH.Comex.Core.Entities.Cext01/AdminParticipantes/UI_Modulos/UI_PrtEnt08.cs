using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;
namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos
{
    public class UI_PrtEnt08
    {
        public UI_Combo Lista1 { get; set; }
        public UI_Combo Lista2 { get; set; }
        //public UI_ListBox_ Lista1 { get; set; }
        //public UI_ListBox_ Lista2 { get; set; }
        public UI_Button aceptar { get; set; }
        public UI_Button cancelar { get; set; }
        public UI_Button guardar { get; set; }
        //public UI_Combo Combo1 { get; set; }
        public UI_TextBox cuenta { get; set; }
        public UI_Label Label1 { get; set; }
        public UI_Label Label2 { get; set; }
        public UI_Label Label3 { get; set; }
        public UI_Label Label4 { get; set; }
        public UI_Label Label5 { get; set; }
        public UI_Label Label6 { get; set; }
        public UI_Label Label7 { get; set; }
        public UI_Label Titulo { get; set; }

        public UI_TextBox Frame1 { get; set; }
        public UI_TextBox Frame2 { get; set; }

        public UI_PrtEnt08()
        {
            Lista1 = new UI_Combo();
            Lista2 = new UI_Combo();          
            cuenta = new UI_TextBox();
            aceptar = new UI_Button();
            cancelar = new UI_Button();
            guardar = new UI_Button();
            Label1 = new UI_Label();
            Label2 = new UI_Label();
            Label3 = new UI_Label();
            Label4 = new UI_Label();
            Label5 = new UI_Label();
            Label6 = new UI_Label();
            Label7 = new UI_Label();
            Titulo = new UI_Label();
            Frame1 = new UI_TextBox();
            Frame2 = new UI_TextBox();   
        }
    }
}