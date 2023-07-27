using System.Collections.Generic;

namespace BCH.Comex.Common.Caching
{
    /// <summary>
    /// Configuración de colección de políticas
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 08/05/2015
    /// Fecha de modificación: 14/05/2015
    /// </summary>
    class PolicyConfiguration
    {
        #region properties
        /// <summary>
        /// Nombre de la política por default
        /// </summary>
        public string DefaultPolicy
        {
            get;
            set;
        }

        /// <summary>
        /// Lista de políticas de caching
        /// </summary>
        public List<CachePolicyConfiguration> CachePolicyList
        {
            get;
            set;
        }
        #endregion
    }
}
