using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Portal;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.MT300Planilla
{
    public class DatosGlobales
    {
        /// <summary>
        /// Datos de configuracion del usuario logueado
        /// </summary>
        public IDatosUsuario DatosUsuario { get; set; }
        public List<UI_Message> ListaMensajes { get; set; }

        public DatosGlobales() 
        {
            this.ListaMensajes = new List<UI_Message>();
        }
    }
    
}
