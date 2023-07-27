using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XGSV.Modulos;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Core.Entities.Portal;

namespace BCH.Comex.Core.BL.XGSV
{

    public partial class XgsvService
    {
        /// <summary>
        /// Inicia la aplicación con datos iniciales de configuración
        /// </summary>
        /// <param name="datosUsuario"></param>
        /// <returns></returns>
        public DatosGlobales Iniciar(IDatosUsuario datosUsuario)
        {
            using (Tracer tracer = new Tracer("XgsvService - Iniciar"))
            {
                DatosGlobales globales = new DatosGlobales();
                globales.DatosUsuario = datosUsuario;

                string centroCostoUsuario = globales.DatosUsuario.Identificacion_CCtUsr.Substring(0, 3);
                string codigoUsuario = globales.DatosUsuario.Identificacion_CCtUsr.Substring(
                    globales.DatosUsuario.Identificacion_CCtUsr.Length - 2);

                if (!MODGSUP.SyGet_Usr(centroCostoUsuario, codigoUsuario, globales, uow))
                    return null;

                if (!MODGUSR.SyGet_OfiUsr(centroCostoUsuario, codigoUsuario, globales, uow))
                    return null;

                return globales;
            }
        }
    }
}
