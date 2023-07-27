using BCH.Comex.Common.Exceptions;
using System;
using System.Configuration;

namespace BCH.Comex.Common.Caching.Configuration
{
    /// <summary>
    /// Clase que representa la sección de configuración del módulo de Caching en el 
    /// archivo de configuración
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 08/05/2015
    /// Fecha de modificación: 09/05/2015
    /// </summary>
    internal class CachingSection : ConfigurationSection
    {
        #region fields

        private const string policyCollectionProperty = "cachePolicies";
        private const string defaultPolicyProperty = "defaultPolicy";

        #endregion fields

        #region properties

        [ConfigurationProperty(defaultPolicyProperty, IsRequired = false)]
        public string DefaultPolicyName
        {
            get
            {
                return (string)this[defaultPolicyProperty];
            }
        }

        /// <summary>
        /// Coleción de políticas de manejo de cache que componen la sección
        /// </summary>
        [ConfigurationProperty(policyCollectionProperty)]
        public CachePolicyElementCollection PolicyCollection
        {
            get
            {
                return (CachePolicyElementCollection)this[policyCollectionProperty];
            }
        }

        #endregion properties
        
        #region methods

        /// <summary>
        /// Convierte el nombre de un tipo de cache a CacheType
        /// </summary>
        /// <param name="cacheTypeName">Nombre de tipo de cache</param>
        /// <returns>Instancia de CacheType de <paramref name="cacheTypeName"/></returns>
        public static CacheType ToCacheType(string cacheTypeName)
        {
            CacheType type;
            try
            {
                type = (CacheType)Enum.Parse(typeof(CacheType), cacheTypeName);
            }
            catch (ArgumentException ae)
            {
                throw new CacheException(string.Format(
                    Messages.InvalidCacheType, cacheTypeName), ae);
            }

            return type;
        }
        #endregion methods
    }

}
