
namespace BCH.Comex.Common.Caching
{
    /// <summary>
    /// Configuración de políticas para cache
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 08/05/2015
    /// Fecha de modificación: 14/05/2015
    /// </summary>
    public class CachePolicyConfiguration
    {
        #region properties
        /// <summary>
        /// Nombre de la política
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Tipo de Cache a manejar
        /// </summary>
        public CacheType CacheType
        {
            get;
            set;
        }

        /// <summary>
        /// Lifetime de objetos en cache en segundos 
        /// </summary>
        public int DefaultLifeTimeSeconds
        {
            get;
            set;
        }

        #endregion
    }
}
