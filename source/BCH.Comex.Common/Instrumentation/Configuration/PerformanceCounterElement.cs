using System.Configuration;

namespace BCH.Comex.Common.Instrumentation.Configuration
{
    /// <summary>
    /// Representa un elemento de contador de performance en el archivo de configuración
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    class PerformanceCounterElement : ConfigurationElement
    {
        #region fields

        private const string descriptionProperty = "description";
        private const string nameProperty = "name";
        private const string typeProperty = "type";
        private const string instanceProperty = "performanceCounterInstances"; 

        #endregion fields

        #region ctor and finalizers

        /// <summary>
        /// Constructor
        /// </summary>
        public PerformanceCounterElement()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Nombre del elemento</param>
        public PerformanceCounterElement(string name)
        {
            Name = name;
        } 

        #endregion ctor and finalizers

        #region properties

        /// <summary>
        /// Nombre del elemento
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
        /// Descripción del elemento
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
        /// Nombre del tipo del elemento
        /// </summary>
        [ConfigurationProperty(typeProperty, IsRequired = true)]
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
        /// Colección de elementos de instancias de contadores
        /// </summary>
        [ConfigurationProperty(instanceProperty)]
        public PerformanceCounterInstanceElementCollection InstanceCollection
        {
            get
            {
                return (PerformanceCounterInstanceElementCollection)this[instanceProperty];
            }
        }

        /// <summary>
        /// Tipo del elemento
        /// </summary>
        public BCHComexPerformanceCounterType Type
        {
            get
            {
                return Utils.ToBCHComexPerformanceCounterType(TypeName);
            }
            set
            {
                TypeName = value.ToString();
            }
        } 

        #endregion properties
    }
}
