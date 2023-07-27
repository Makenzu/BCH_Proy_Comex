using System.Configuration;

namespace BCH.Comex.Common.ExceptionHandling.Configuration
{
    /// <summary>
    /// Clase que representa la sección de configuración de manejo de excepciones.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 03/05/2015
    /// </summary>
    public class ExceptionHandlingSection : ConfigurationSection
    {
        #region fields

        private const string policyCollectionProperty = "exceptionPolicies";

        #endregion fields

        #region properties

        /// <summary>
        /// Coleción de políticas de manejo de excepción que componen la sección
        /// </summary>
        [ConfigurationProperty(policyCollectionProperty)]
        public ExceptionPolicyElementCollection PolicyCollection
        {
            get
            {
                return (ExceptionPolicyElementCollection)this[policyCollectionProperty];
            }
        }

        #endregion properties
    }
}
