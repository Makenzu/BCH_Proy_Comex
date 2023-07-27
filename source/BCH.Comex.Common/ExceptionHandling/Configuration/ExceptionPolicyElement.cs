using System.Configuration;

namespace BCH.Comex.Common.ExceptionHandling.Configuration
{
    /// <summary>
    /// Representa una política en el archivo de configuración.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 03/05/2015
    /// </summary>
    public class ExceptionPolicyElement : ConfigurationElement
    {
        #region fields

        private const string nameProperty = "name";
        private const string exceptionTypeProperty = "exceptionTypes";

        #endregion fields

        #region ctor and finalizers

        /// <summary>
        /// Constructor del elemento
        /// </summary>
        public ExceptionPolicyElement()
        {

        }

        /// <summary>
        /// Constructor del elemento
        /// </summary>
        /// <param name="name">Nombre del elemento</param>
        public ExceptionPolicyElement(string name)
        {
            Name = name;
        } 

        #endregion ctor and finalizers

        #region properties
        /// <summary>
        /// Propiedad Name
        /// </summary>
        [ConfigurationProperty(nameProperty, IsKey = true)]
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
        /// Colección de <see cref="ExceptionTypeCollection"/>
        /// </summary>
        [ConfigurationProperty(exceptionTypeProperty)]
        public ExceptionTypeElementCollection ExceptionTypeCollection
        {
            get
            {
                return (ExceptionTypeElementCollection)this[exceptionTypeProperty];
            }
        } 

        #endregion properties
    }
}
