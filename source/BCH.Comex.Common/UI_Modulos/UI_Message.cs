using System;

namespace BCH.Comex.Common.UI_Modulos
{
    public enum TipoMensaje
    {
        Nada = 0,
        Correcto = 1,
        Informacion = 2,
        Error = 3,
        Critical = 4,
        YesNo = 5,
        Warning = 6
    }

    public class UI_Message
    {
        public TipoMensaje Type { set; get; }
        public string Text { set; get; }
        public string Title { get; set; }
        public string ControlName { get; set; } //se agrega para hacer referencia a un control determinado
        public bool AutoClose { get; set; }

        public UI_Message()
        {
            this.Type = TipoMensaje.Nada;
            this.Text = String.Empty;
            this.Title = String.Empty;
        }

        public bool IsError
        {
            get
            {
                return (this.Type == TipoMensaje.Error || this.Type == TipoMensaje.Critical);
            }
        }

    }
}
