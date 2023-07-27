using System;
using System.Configuration;

namespace BCH.Comex.Common
{
    /// <summary>
    /// Representa constantes globales utilizadas
    /// dentro del proyecto common para 
    /// la configuración de las piezas transversales
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 05/08/2015
    /// Fecha de modificación: 05/08/2015
    /// </summary>
    public class GlobalConstants
    {
        public const string PerformanceCountersCategory = "BCH.Comex.Common";
        public const string ExceptionPolicyDefault = "BCHComexDefaultPolicy";
        public const string EventLogSource = "BCHComex";
        public const string EventLogName = "Application";
        public const string ApplicationNameSetting = "ApplicationName";
        public const string ExceptionTraceSourceDefault = "BCHComexExceptionsTraceSource";

        private static string applicationName;
        public static string ApplicationName
        {
            get
            {
                if (applicationName == null)
                {
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings[ApplicationNameSetting]))
                        applicationName = ConfigurationManager.AppSettings[ApplicationNameSetting];
                    else
                        applicationName = AppDomain.CurrentDomain.FriendlyName;
                }
                return applicationName;
            }
        }
    }
}
