
using BCH.Comex.Core.Entities.Portal;

namespace BCH.Comex.Core.Entities.Cext01.FinDia
{
    public class DatosGlobales
    {
        public IDatosUsuario Usuario { get; set; }
        public T_MODCUACC MODCUACC { get; set; }
        public T_CCIRLLVR CCIRLLVR { get; set; }
        public T_MODGUSR MODGUSR { get; set; }
        public T_MODCUAD MODCUAD { get; set; }
        public T_MODFDIA MODFDIA { get; set; }

        public DatosGlobales()
        {
            Usuario = new DatosUsuarioDTO();
            MODCUACC = new T_MODCUACC();
            CCIRLLVR = new T_CCIRLLVR();
            MODGUSR = new T_MODGUSR();
            MODFDIA = new T_MODFDIA();
        }
    }
}
