using System.Web.Configuration;

namespace BCH.Comex.Core.BL.XEGI
{
    public class Mdl_Acceso
    {
        public const string MODULO = "Xegi";

        public static string GetConfigValue(string key) 
        {
            return WebConfigurationManager.AppSettings[key];
        }       
    }
}
