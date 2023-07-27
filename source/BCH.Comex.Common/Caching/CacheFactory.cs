using BCH.Comex.Common.Caching.CacheManager;
using BCH.Comex.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Common.Caching
{
    /// <summary>
    /// Clase encargada de obtener los diferentes manejadores de caché posibles
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 08/05/2015
    /// Fecha de modificación: 08/05/2015
    /// </summary>
    public static class CacheFactory
    {
        #region fields
        private static IDictionary<string, ICacheManager>  cacheManagers = null;
        #endregion fields

        #region public methods

        /// <summary>
        /// Obtiene el manejador de cache por defecto.
        /// </summary>
        /// <returns>Manejador de cache por defecto</returns>
        public static ICacheManager GetCache()
        {
           return GetCache(CacheConfigurationManager.GetDefaultPolicyName());
        }

        /// <summary>
        /// Obtiene el manejador de cache según configuración
        /// </summary>
        /// <returns>Manejador de cache configurado</returns>
        public static ICacheManager GetCache(string policyName)
        {
            ICacheManager cacheManager = null;

            if (CacheFactory.cacheManagers == null)
                InitializeCacheManagers();

            CachePolicyConfiguration policyConf = CacheConfigurationManager.GetPolicyConfiguration(policyName);


            if(cacheManagers.TryGetValue(policyConf.Name, out cacheManager) == false)
                throw new CacheException(Messages.InexistentPolicyName,new ArgumentNullException());
            
            return cacheManager;

        }
        #endregion 

        #region private methods
        /// <summary>
        /// Inicializa Los Managers de las policies definidas en archivo de configuración
        /// </summary>
        private static void InitializeCacheManagers()
        {

            cacheManagers = new Dictionary<string, ICacheManager>();
            
            //Inicializo la configuración de políticas
            CacheConfigurationManager.Initialize();

            //Agrego cada políticac de cache a la lista de políticas
            foreach(CachePolicyConfiguration cachePolicyConf in CacheConfigurationManager.GetPolicyConfigurationList())
            {
                switch (cachePolicyConf.CacheType)
                {
                    case CacheType.MemoryCache:
                        cacheManagers.Add(cachePolicyConf.Name, new MemoryCacheManager(cachePolicyConf));
                        break;
                    case CacheType.NoCache:
                        cacheManagers.Add(cachePolicyConf.Name, new NoCacheManager());
                        break;

                    default:
                        throw new CacheException(string.Format(
                            Messages.InvalidCacheType,cachePolicyConf.CacheType.ToString()));
                }
            }
        }
        
        #endregion 
    }
}
