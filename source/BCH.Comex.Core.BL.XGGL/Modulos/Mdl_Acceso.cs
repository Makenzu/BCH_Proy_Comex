using System.Web.Configuration;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class Mdl_Acceso
    {
        public static string GetConfigValue(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

    }
}
