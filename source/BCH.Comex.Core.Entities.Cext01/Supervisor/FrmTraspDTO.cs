using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.Supervisor
{
    public class FrmTraspDTO
    {
        public List<UI_Message> ListaErrores { get; set; }
        public List<string> UsuariosActivos { get; set; }
        public List<string> UsuariosNuevos { get; set; }
    }
}
