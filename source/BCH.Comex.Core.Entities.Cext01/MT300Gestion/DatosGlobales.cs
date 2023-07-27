using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Core.Entities.Portal;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.MT300Gestion
{
    public class DatosGlobales
    {
        /// <summary>
        /// Datos de configuracion del usuario logueado
        /// </summary>
        public IDatosUsuario DatosUsuario { get; set; }
        public List<UI_Message> ListaMensajes { get; set; }
        public DatosIndex DatosIndex { get; set; }
        public DatosGlobales() 
        {
            ListaMensajes = new List<UI_Message>();
            DatosIndex = new DatosIndex();
        }
    }
    
}
