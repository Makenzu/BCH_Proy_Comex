using BCH.Comex.Common.Exceptions;
using BCH.Comex.Core.BL.XGPY;
using BCH.Comex.Core.BL.XGPY.Modulos;
using BCH.Comex.Core.Entities.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCH.Comex.UI.Web.Common
{
    /// <summary>
    /// Metodos utiles
    /// </summary>
    public static class Utils
    {
        public static void ValidarEjecucionFinDia(HttpSessionStateBase session)
        {
            var finDiaEjecutado = session[Common.SessionKeys.Common.FinDiaEjecutado] as string;
            if (!string.IsNullOrEmpty(finDiaEjecutado) && finDiaEjecutado == true.ToString())
            {
                XgpyService xgpyService = new XgpyService();
                /// Validar inicio de día del usuario trabajando (si esta suplantando a esta persona, considera ese usuario.)
                string msj = string.Empty;
                var datos = session[SessionKeys.Common.DatosUsuario] as IDatosUsuario;
                if (MODGUSR.SyGetf_Usr(datos.Identificacion_CentroDeCostosImpersonado, datos.Identificacion_IdEspecialistaImpersonado, "I", xgpyService, ref msj))
                {
                    throw new ComexUserException(msj);
                }
            }
        }

        /// <summary>
        /// Invalida los datos de sesion para el usuario
        /// </summary>
        internal static void InvalidarSesion(HttpSessionStateBase session)
        {
            session.Clear();

        }
    }
}