using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Portal;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.Devengo
{
    public class DatosGlobales
    {
        public T_MODGUSR MODGUSR { get; set; }
        public T_MODDIFDEV MODDIFDEV { get; set; }
        public DevengIntReaj DatosIntReaj { get; set; }
        public T_DevHist DatosDevHist { get; set; }
        public T_INFORME DatosInforme { get; set; }

        /// <summary>
        /// Datos de configuracion del usuario logueado
        /// </summary>
        public IDatosUsuario DatosUsuario { get; set; }
        public List<UI_Message> ListaMensajesError { get; set; }

        public DatosGlobales() 
        {
            this.MODDIFDEV = new T_MODDIFDEV();
            this.MODGUSR = new T_MODGUSR();
            this.ListaMensajesError = new List<UI_Message>();
            this.DatosIntReaj = new DevengIntReaj();
            this.DatosDevHist = new T_DevHist();
            this.DatosInforme = new T_INFORME();
        }
    }
    //public enum TipoMensaje
    //{
    //    Nada = 0,
    //    Correcto = 1,
    //    Informacion = 2,
    //    Error = 3,
    //    Critical = 4,
    //    YesNo = 5,
    //    Warning = 6
    //}

    //public class UI_Message
    //{
    //    public TipoMensaje Type { set; get; }
    //    public string Text { set; get; }
    //    public string Title { get; set; }
    //    public string ControlName { get; set; } //se agrega para hacer referencia a un control determinado
    //    public bool AutoClose { get; set; }

    //    public UI_Message()
    //    {
    //        this.Type = TipoMensaje.Nada;
    //        this.Text = String.Empty;
    //        this.Title = String.Empty;
    //    }

    //    public bool IsError
    //    {
    //        get
    //        {
    //            return (this.Type == TipoMensaje.Error || this.Type == TipoMensaje.Critical);
    //        }
    //    }

    //}

}
