using System.Configuration;

namespace BCH.Comex.Common.Caching.Configuration
{
    /// <summary>
    /// Representa una política en el archivo de configuración.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 08/05/2015
    /// Fecha de modificación: 14/05/2015
    /// </summary>
    internal class CachePolicyElement : ConfigurationElement
    {
        #region fields

        private const string nameProperty = "name";
        private const string cacheTypeProperty = "cacheType";
        private const string defaultLifetimeSecondsProperty = "defaultLifetimeSeconds";

        private CacheType? cacheType = null;
                
        #endregion fields

        #region ctor and finalizers

        /// <summary>
        /// Constructor del elemento
        /// </summary>
        public CachePolicyElement()
        {

        }

        /// <summary>
        /// Constructor del elemento
        /// </summary>
        /// <param name="name">Nombre del elemento</param>
        public CachePolicyElement(string name)
        {
            Name = name;
        } 

        #endregion ctor and finalizers

        #region properties

        /// <summary>
        /// Propiedad Name
        /// </summary>
        [ConfigurationProperty(nameProperty, IsKey = true, IsRequired=true)]
        public string Name
        {
            get
            {
                return (string)this[nameProperty];
            }
            set
            {
                this[nameProperty] = value;
            }
        }

        /// <summary>
        /// Nombre del tipo de cache obtenido de configuración
        /// </summary>
        [ConfigurationProperty(cacheTypeProperty, IsRequired = true)]
        public string CacheTypeName
        {
            get
            {
                return (string)this[cacheTypeProperty];
            }
        }

        /// <summary>
        /// Tipo de cache obtenido de configuración
        /// </summary>
        public CacheType CacheType
        {
            get
            {
                if (this.cacheType == null)
                    this.cacheType = CachingSection.ToCacheType(this.CacheTypeName);

                return this.cacheType.Value;
            }
        }

        /// <summary>
        /// Tiempo de vida por defecto de los objetos del cache
        /// </summary>
        [ConfigurationProperty(defaultLifetimeSecondsProperty)]
        public int DefaultLifeTimeSeconds
        {
            get
            {
                return (int)this[defaultLifetimeSecondsProperty];
            }
            set
            {
                this[defaultLifetimeSecondsProperty] = value;
            }
        }
        #endregion properties
    }
}

