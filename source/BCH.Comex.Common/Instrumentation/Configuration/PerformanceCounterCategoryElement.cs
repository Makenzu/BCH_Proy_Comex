using System.Configuration;
using System.Diagnostics;

namespace BCH.Comex.Common.Instrumentation.Configuration
{
    /// <summary>
    /// Elemento que representa una categoría de contadores de performance
    /// en el archivo de configuración
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    class PerformanceCounterCategoryElement : ConfigurationElement
    {
        #region fields

        private const string descriptionProperty = "description";
        private const string nameProperty = "name";
        private const string performanceCounterProperty = "performanceCounters";
        private const string isActiveProperty = "isActive";
        private const string typeProperty = "type"; 

        #endregion fields

        #region ctor and finalizers

        /// <summary>
        /// Constructor
        /// </summary>
        public PerformanceCounterCategoryElement()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">nombre de la categoría</param>
        public PerformanceCounterCategoryElement(string name)
        {
            Name = name;
        }

        #endregion ctor and finalizers

        #region properties

        /// <summary>
        /// Nombre de la categoría
        /// </summary>
        [ConfigurationProperty(nameProperty)]
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
        /// Descripción de la categoría
        /// </summary>
        [ConfigurationProperty(descriptionProperty)]
        public string Description
        {
            get
            {
                return (string)this[descriptionProperty];
            }
            set
            {
                this[descriptionProperty] = value;
            }
        }

        /// <summary>
        /// Indica si la categoría esta activa
        /// </summary>
        [ConfigurationProperty(isActiveProperty, DefaultValue = true)]
        public bool IsActive
        {
            get
            {
                return (bool)this[isActiveProperty];
            }
            set
            {
                this[isActiveProperty] = value;
            }
        }

        /// <summary>
        /// Nombre del tipo de la categoría
        /// </summary>
        [ConfigurationProperty(typeProperty, DefaultValue = "MultiInstance")]
        public string TypeName
        {
            get
            {
                return (string)this[typeProperty];
            }
            set
            {
                this[typeProperty] = value;
            }
        }

        /// <summary>
        /// Tipo <see cref="PerformanceCounterCategoryType"/>  de la categoría
        /// </summary>
        public PerformanceCounterCategoryType Type
        {
            get
            {
                return Utils.ToPerformanceCounterCategoryType(TypeName);
            }
            set
            {
                TypeName = value.ToString();
            }
        }

        /// <summary>
        /// Colección de elementos de contadores de performance <see cref="PerformanceCounterElement"/>
        /// </summary>
        [ConfigurationProperty(performanceCounterProperty)]
        public PerformanceCounterElementCollection PerformanceCounterCollection
        {
            get
            {
                return (PerformanceCounterElementCollection)this[performanceCounterProperty];
            }
        } 

        #endregion properties
    }
}
