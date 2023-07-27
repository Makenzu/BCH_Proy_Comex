using BCH.Comex.Core.BL.SWEM.Modulos;
using BCH.Comex.Core.BL.SWEM.UI_Modulos;
namespace BCH.Comex.Core.BL.SWEM.Datatypes
{
    public class InitialBotones
    {

        public UI_Mdi_Principal Mdi_Principal { set; get; }
        public Usuario usuario { set; get; }

        public InitialBotones()
        {
            Mdi_Principal = new UI_Mdi_Principal();
            usuario = new Usuario();
        }

    }
}
