using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using CodeArchitects.VB6Library;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class MODXANU
    {
        public static T_MODXANU GetMODXANU()
        {
            return new T_MODXANU();
        }
        
        public static short Nuevo_Pope(DatosGlobales initObject)
        {
            short Fin = -1;

            Fin = (short)VB6Helpers.UBound(initObject.SYGETPRT.PopeOpe);
            if (Fin == -1)
            {
                initObject.SYGETPRT.PopeOpe = new PartysPope[1];
            }
            else
            {
                if (Fin == 0 && string.IsNullOrEmpty(initObject.SYGETPRT.PopeOpe[0].Nombre))
                {
                    initObject.SYGETPRT.PopeOpe = new PartysPope[1];
                }
                else
                {
                    VB6Helpers.RedimPreserve(ref initObject.SYGETPRT.PopeOpe, 0, Fin + 1);
                }

            }
            return (short)VB6Helpers.UBound(initObject.SYGETPRT.PopeOpe);
        }

    }
}
