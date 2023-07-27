using System.Configuration;

namespace BCH.Comex.Common.Instrumentation.Configuration
{
    /// <summary>
    /// Representa la sección de instrumentación en el archivo de confirugación
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    class InstrumentationSection : ConfigurationSection
    {
        #region Variables

        private const string counterCategoryCollectionProperty = "performanceCounterCategories";

        #endregion Variables

        #region Properties

        /// <summary>
        /// Colección de elementos <see cref="PerformanceCounterCategoryElement"/>
        /// </summary>
        [ConfigurationProperty(counterCategoryCollectionProperty)]
        public PerformanceCounterCategoryElementCollection CategoryCollection
        {
            get
            {
                return (PerformanceCounterCategoryElementCollection)this[counterCategoryCollectionProperty];
            }
        } 

        #endregion Properties
    }
}
