using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Instrumentation.Configuration;
using BCH.Comex.Common.Instrumentation.Counter;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Security;

namespace BCH.Comex.Common.Instrumentation
{
    /// <summary>
    /// Manejador de configuración del componente de instrumentación
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    internal static class InstrumentationConfigurationManager
    {
        #region fields

        private static readonly string instrumentationSectionName = "instrumentationSection";

        #endregion fields

        #region methods

        /// <summary>
        /// Establece las propiedades iniciales de la clase
        /// </summary>
        public static void Initialize()
        {
            InstallPerformanceCounters();
            InitializeCounters();
        }

        /// <summary>
        /// Carga los datos de instrumentación del archivo de configuración en 
        /// <see cref="PerformanceCounterContainer"/>
        /// </summary>
        public static void LoadConfiguration()
        {
            InstrumentationSection section = null;
            try
            {
                section = ConfigurationManager.GetSection(
                   instrumentationSectionName) as InstrumentationSection;
            }
            catch (ConfigurationErrorsException ceex)
            {
                throw new InstrumentationException(string.Format(
                    Messages.ErrorGettingConfigurationSection, instrumentationSectionName),
                    ceex);
            }

            if (section != null)
                LoadConfigurationFromConfigurationSection(section);
            else
                LoadConfigurationFromDefaultConfiguration();
        }

        /// <summary>
        /// Desinstala todos los contadores de la aplicación
        /// </summary>
        public static void UninstallAllApplicationCounters()
        {
            foreach (CounterCategoryData category in PerformanceCounterContainer.GetAllCategories())
            {
                UninstallPerformanceCounterCategory(category);
            }
        }

        /// <summary>
        /// Instala los contadores de performance activos de la lista de contadores en el sistema, 
        /// o los desinstala en caso de no estar activos
        /// </summary>
        private static void InstallPerformanceCounters()
        {
            try
            {
                foreach (CounterCategoryData category in
                    PerformanceCounterContainer.GetAllCategories())
                {
                    if (!PerformanceCounterCategory.Exists(category.Name))
                    {
                        if (category.IsActive)
                            InstallPerformanceCounterCategory(category);
                    }
                    else
                    {
                        if (!category.IsActive)
                            UninstallPerformanceCounterCategory(category);
                    }
                }
            }
            catch (UnauthorizedAccessException uex)
            {
                throw new InstrumentationException(Messages.InsufficientPermissionsForCounterReading, uex);
            }
        }

        /// <summary>
        /// Elimina la categoría y sus contadores asociados instalados en la máquina
        /// </summary>
        /// <param name="category">Categoría a desinstalar</param>
        public static void UninstallPerformanceCounterCategory(CounterCategoryData category)
        {
            try
            {
                if (PerformanceCounterCategory.Exists(category.Name))
                    PerformanceCounterCategory.Delete(category.Name);
            }
            catch (SecurityException secEx)
            {
                throw new InstrumentationException(string.Format(
                    Messages.InsufficientPermissionsForCounterCategoryDeletion, category.Name),
                    secEx);
            }
        }

        /// <summary>
        /// Instala la categoría y sus contadores asociados en la máquina
        /// </summary>
        /// <param name="category">Categoría a instalar</param>
        public static void InstallPerformanceCounterCategory(CounterCategoryData category)
        {
            CounterCreationDataCollection counterDataCol = new CounterCreationDataCollection();
            foreach (CounterData counterData in category.GetAllCounters())
            {
                CounterCreationData creationData;

                creationData = new CounterCreationData(
                    counterData.Name,
                    counterData.Description,
                    counterData.Type);
                counterDataCol.Add(creationData);

                if (counterData.HasBaseCounter)
                {
                    creationData = new CounterCreationData(
                        counterData.BaseName,
                        counterData.BaseDescription,
                        counterData.BaseType);
                    counterDataCol.Add(creationData);
                }
            }

            try
            {
                PerformanceCounterCategory.Create(category.Name, category.Description,
                    category.Type, counterDataCol);
            }
            catch (SecurityException uex)
            {
                throw new InstrumentationException(string.Format(
                    Messages.InsufficientPermissionsForCounterCategoryCreation, category.Name), uex);
            }
        }

        /// <summary>
        /// Crea las instancias de contadores
        /// </summary>
        private static void InitializeCounters()
        {
            foreach (CounterCategoryData category in PerformanceCounterContainer.GetAllCategories())
            {
                if (category.IsActive)
                {
                    foreach (CounterData counterData in category.GetAllCounters())
                    {
                        foreach (CounterInstanceData instanceData in counterData.GetAllInstances())
                        {
                            using (PerformanceCounter counter = new PerformanceCounter(category.Name, counterData.Name, instanceData.Name, false))
                            {
                                instanceData.RealCounter = counter;

                                if (!instanceData.IsActive)
                                    counter.RemoveInstance();

                                if (counterData.HasBaseCounter)
                                {
                                    using (PerformanceCounter baseCounter = new PerformanceCounter(category.Name, counterData.BaseName, instanceData.Name, false))
                                    {
                                        instanceData.RealCounterBase = baseCounter;

                                        if (!instanceData.IsActive)
                                            baseCounter.RemoveInstance();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Carga la configuración por defecto 
        /// </summary>
        private static void LoadConfigurationFromDefaultConfiguration()
        {
            string defaultCategoryName = "BCHComex";
            string defaultCategoryDescription = "Contadores de BCHComex";

            PerformanceCounterContainer.AddPerformanceCounterCategory(defaultCategoryName,
                    defaultCategoryDescription, true,
                    PerformanceCounterCategoryType.MultiInstance);
        }

        /// <summary>
        /// Carga la configuración a partir de <paramref name="section"/>
        /// </summary>
        /// <param name="section">Sección de configuración del archivo de configuración</param>
        private static void LoadConfigurationFromConfigurationSection(
            InstrumentationSection section)
        {
            foreach (PerformanceCounterCategoryElement categoryElement in section.CategoryCollection)
            {
                PerformanceCounterContainer.AddPerformanceCounterCategory(categoryElement.Name,
                    categoryElement.Description, categoryElement.IsActive, categoryElement.Type);

                foreach (PerformanceCounterElement counterElement in categoryElement.PerformanceCounterCollection)
                {
                    PerformanceCounterContainer.AddCounter(categoryElement.Name,
                        counterElement.Name, counterElement.Description, counterElement.Type);

                    foreach (PerformanceCounterInstanceElement instanceElement in counterElement.InstanceCollection)
                    {
                        PerformanceCounterContainer.AddCounterInstance(categoryElement.Name, counterElement.Name,
                            instanceElement.Name, instanceElement.IsActive);
                    }
                }
            }
        }

        #endregion methods
    }
}
