using BCH.Comex.Common.Caching.Configuration;
using BCH.Comex.Common.Exceptions;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace BCH.Comex.Common.Caching
{
    /// <summary>
    /// Manejador de la configuración del cache. Se asegura que se obtenga la sección de
    /// configuración de cache una sola vez.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 08/05/2015
    /// Fecha de modificación: 08/05/2015
    /// </summary>
    static class CacheConfigurationManager
    {
        #region Static fields
        private static string defaultPolicyName = string.Empty;
        private static readonly string cachingSectionName = "cachingSection";
        private static List<CachePolicyConfiguration> policyConfigurationList;
        private static TraceSource traceSource;
        
        /// <summary>
        /// Seccion de caching del archivo de configuración
        /// </summary>
        private static CachingSection section = null;

        #endregion fields

        #region ctor and finalizers

        /// <summary>
        /// Constructor
        /// </summary>
        static CacheConfigurationManager()
        {
            section = ConfigurationManager.GetSection(cachingSectionName) as CachingSection;
        
        }

        #endregion ctor and finalizers

        #region Public methods
        /// <summary>
        /// Establece las propiedades iniciales de la clase
        /// </summary>
        public static void Initialize()
        {
            LoadConfiguration();
        }

        /// <summary>
        /// Carga los datos de propiedades de cache del archivo de configuración 
        /// <see cref="PerformanceCounterContainer"/>
        /// </summary>
        public static void LoadConfiguration()
        {
            policyConfigurationList = new List<CachePolicyConfiguration>();
            CachingSection section = null;
            try
            {
                section = ConfigurationManager.GetSection(cachingSectionName) as CachingSection;
            }
            catch (ConfigurationErrorsException ceex)
            {
                throw new CacheException(Messages.CacheLoadError, ceex);
            }

            if (section == null)
                throw new CacheException(Messages.CacheLoadError);
            else
                LoadConfigurationFromConfigurationSection(section);

            traceSource = new TraceSource("BCHComexCachingSource", SourceLevels.All);
        }

        /// <summary>
        /// Obtiene el <see cref="PolicyConfiguration"/> de la lista
        /// </summary>
        /// <param name="policyName">Nombre de la política a obtener</param>
        /// <returns>Política obtenida</returns>
        public static CachePolicyConfiguration GetPolicyConfiguration(string policyName)
        {
            //Si el nombre de la política es vacio 
            if (policyName == string.Empty)
                throw new CacheException(Messages.EmptyPolicyName);

            CachePolicyConfiguration policyConf = policyConfigurationList.Find(
                cp => cp.Name == policyName);

            //Si no encuentro la política
            if (policyConf == null)
                throw new CacheException(Messages.InexistentPolicyName + policyName);

            return policyConf;
        }

        /// <summary>
        /// Obtiene el nombre de la política por default
        /// </summary>
        /// <returns>Nombre de la Política por default </returns>
        public static string GetDefaultPolicyName()
        {
            //Obtengo el nombre de la política default del archivo de configuración
            return section.DefaultPolicyName;

        }

        /// <summary>
        /// Retorna la lista de políticas de cache
        /// </summary>
        public static List<CachePolicyConfiguration> GetPolicyConfigurationList()
        {
            return policyConfigurationList;
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Carga la configuración a partir de <paramref name="section"/>
        /// </summary>
        /// <param name="section">Sección de configuración del archivo de configuración</param>
        private static void LoadConfigurationFromConfigurationSection(CachingSection section)
        {
            foreach (CachePolicyElement policyElement in section.PolicyCollection)
            {
                CachePolicyConfiguration policyConf = new CachePolicyConfiguration()
                {
                    Name = policyElement.Name,
                    CacheType = policyElement.CacheType,
                    DefaultLifeTimeSeconds = policyElement.DefaultLifeTimeSeconds,
                };
                CacheConfigurationManager.policyConfigurationList.Add(policyConf);
            }

        }
        #endregion
    }
}
