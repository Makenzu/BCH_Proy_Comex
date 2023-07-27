using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.Supervisor
{
    public class FrmCederDTO
    {
        public List<UI_Message> ListaErrores { get; set; }
        public List<string> UsuariosActuales { get; set; }
        public List<string> UsuariosNuevos { get; set; }
        public List<T_Prod> Productos { get; set; }
        public List<T_Cliente> Clientes { get; set; }
        public List<T_OpeCli> Operaciones { get; set; }

        public FrmCederDTO()
        {
            ListaErrores = new List<UI_Message>();
            UsuariosActuales = new List<string>();
            UsuariosNuevos = new List<string>();
            Productos = new List<T_Prod>();
            Clientes = new List<T_Cliente>();
            Operaciones = new List<T_OpeCli>();
        }

    }


}
