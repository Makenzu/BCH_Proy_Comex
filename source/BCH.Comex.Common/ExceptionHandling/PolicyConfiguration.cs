using System.Collections.Generic;

namespace BCH.Comex.Common.ExceptionHandling
{
    /// <summary>
    /// Configuración de política
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 03/05/2015
    /// </summary>
    class PolicyConfiguration
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
        /// Lista de políticas de excepciones
        /// </summary>
        public List<ExceptionPolicyConfiguration> ExceptionPolicyList
        {
            get;
            set;
        }
        #endregion
    }
}
