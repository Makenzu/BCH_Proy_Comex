using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms
{
    public class UI_GetPrty1:UI_Frm
    {
        public const string GPrt_Seg1 = "El participante seleccionado se encuentra en la base de participantes del Host. ";
        public const string GPrt_Seg2 = "Para poder utilizarlo será copiado a la base local.";
        public const string GPrt_NoServer = "El Servidor de actualización de participantes no se encuentra disponible en su estación.";
        public const string GPrt_BusyServer = "El Servidor de actualizacion de participantes se encuentra ocupado por otra aplicación.";
        public const int GPrt_RetNoServer = 1;
        public const int GPrt_RetBusyServer = 2;

        public UI_TextBox Enlace { set; get; }
        public UI_Combo Otro { set; get; }
        public UI_Combo Dire { set; get; }
        public UI_Combo Nome { set; get; }
        public UI_Button Cancelar { set; get; }
        public UI_Button Aceptar { set; get; }
        public UI_Label Label2 { set; get; }
        public UI_Label Label1 { set; get; }

        public UI_GetPrty1()
        {
            Enlace = new UI_TextBox();
            Otro = new UI_Combo();
            Dire = new UI_Combo() { ListIndex = 0 };
            Nome = new UI_Combo() { ListIndex = 0 };
            Cancelar = new UI_Button();
            Aceptar = new UI_Button();
            Label1 = new UI_Label();
            Label2 = new UI_Label();
        }
    }
}
