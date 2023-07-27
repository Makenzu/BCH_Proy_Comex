using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Instrumentation.Counter;
using System;

namespace BCH.Comex.Common.Instrumentation
{
    /// <summary>
    /// Provider de instrumentación. Esta clase el punto de entrada de el módulo de instrumentación
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    public static class InstrumentationProvider
    {
        #region fields

        private static bool isInitialized = false;
        public static bool IsInitialized
        {
            get { return InstrumentationProvider.isInitialized; }
        }


        private static object sync = new object();

        #endregion fields

        #region ctor and finalizers

        /// <summary>
        /// Type initialzer
        /// </summary>
        static InstrumentationProvider()
        {
            InstrumentationConfigurationManager.LoadConfiguration();
        }

        #endregion ctor and finalizers

        #region methods

        internal static void Reset()
        {
            InstrumentationProvider.isInitialized = false;
        }

        /// <summary>
        /// Desinstala todos los contadores de la aplicación
        /// </summary>
        public static void UninstallAllApplicationCounters()
        {
            InstrumentationConfigurationManager.UninstallAllApplicationCounters();
        }

        /// <summary>
        /// Inicializa la configuración de los contadores
        /// </summary>
        public static void Initialize()
        {
            try
            {
                if (!isInitialized)
                {
                    lock (sync)
                    {
                        if (!isInitialized)
                        {
                            InstrumentationConfigurationManager.Initialize();
                            isInitialized = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InstrumentationException(Messages.InstrumentationInitializationError, ex);
            }
        }

        /// <summary>
        /// Agrega un nuevo contador
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría del contador</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="counterDescription">Descripción del contador</param>
        /// <param name="counterType">Tipo del contador</param>
        public static void AddCounter(string categoryName, string counterName, string counterDescription,
            BCHComexPerformanceCounterType counterType)
        {
            if (!isInitialized)
            {
                PerformanceCounterContainer.AddCounter(categoryName, counterName,
                    counterDescription, counterType);
            }
            else
            {
                throw new InstrumentationException(
                    "El InstrumentationProvider ya está inicializado. Debe llamar a este método antes de llamar a Initialize()");
            }
        }

        /// <summary>
        /// Indica si la categoría <paramref name="categoryName"/> contiene el contador
        /// <paramref name="counterName"/>
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <returns>Si contiene o no el contador</returns>
        public static bool HasCounter(string categoryName, string counterName)
        {
            return PerformanceCounterContainer.HasCounter(categoryName, counterName);
        }

        /// <summary>
        /// Quita el contador <paramref name="counterName"/> de la categoría <paramref name="categoryName"/>
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría</param>
        /// <param name="counterName">Nombre del contador</param>
        public static void RemoveCounter(string categoryName, string counterName)
        {
            if (!isInitialized)
            {
                PerformanceCounterContainer.RemoveCounter(categoryName, counterName);
            }
            else
            {
                throw new InstrumentationException(
                    "El InstrumentationProvider ya está inicializado. Debe llamar a este método antes de llamar a Initialize()");
            }
        }

        /// <summary>
        /// Agrega una nueva instancia de contador
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="instanceName">Nombre de la instancia</param>
        public static void AddCounterInstance(string categoryName, string counterName, string instanceName)
        {
            if (!isInitialized)
            {
                PerformanceCounterContainer.AddCounterInstance(categoryName, counterName, instanceName, true);
            }
        }

        /// <summary>
        /// Indica si se tiene la instancia especificada
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="instanceName">Nombre de la instancia</param>
        /// <returns>Si contiene o no la intancia</returns>
        public static bool HasCounterInstance(string categoryName, string counterName, string instanceName)
        {
            return PerformanceCounterContainer.HasCounterInstance(categoryName, counterName, instanceName);
        }

        /// <summary>
        /// Remueve la instancia <paramref name="instanceName"/> de la lista de instancias
        /// </summary>
        /// <param name="categoryName">Nombre el contador que contiene la instancia</param>
        /// <param name="counterName">Nombre el contador que contiene la instancia</param>
        /// <param name="instanceName">Nombre de la instancia a quitar</param>
        public static void RemoveCounterInstance(string categoryName, string counterName, string instanceName)
        {
            if (!isInitialized)
            {
                PerformanceCounterContainer.RemoveCounterInstance(categoryName, counterName, instanceName);
            }
        }

        /// <summary>
        /// Incrementa en 1 la instancia del contador especificado
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría del contador</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="instanceName">Nombre de la instancia del contador</param>
        public static void IncreaseCounter(string categoryName, string counterName,
            string instanceName)
        {
            if (isInitialized)
            {
                CounterInstanceData instanceData =
                    PerformanceCounterContainer.GetPerformanceCounterInstance(categoryName, counterName, instanceName);

                if (instanceData != null)
                    instanceData.IncreaseCounter();
            }
        }

        /// <summary>
        /// Incrementa en <paramref name="value"/> la instancia del contador especificado
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría del contador</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="instanceName">Nombre de la instancia del contador</param>
        /// <param name="value">Cantidad a incrementar</param>
        public static void IncreaseCounter(string categoryName, string counterName,
            string instanceName, long value)
        {
            if (isInitialized)
            {
                CounterInstanceData instanceData =
                    PerformanceCounterContainer.GetPerformanceCounterInstance(categoryName, counterName,
                        instanceName);

                if (instanceData != null)
                    instanceData.IncreaseCounter(value);
            }
        }

        /// <summary>
        /// Decrementa en 1 la instancia del contador especificado
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría del contador</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="instanceName">Nombre de la instancia del contador</param>
        public static void DecreaseCounter(string categoryName, string counterName,
            string instanceName)
        {
            if (isInitialized)
            {
                CounterInstanceData instanceData =
                    PerformanceCounterContainer.GetPerformanceCounterInstance(categoryName, counterName,
                        instanceName);

                if (instanceData != null)
                    instanceData.DecreaseCounter();
            }
        }

        /// <summary>
        /// Registra la diferencia de tiempo entre <paramref name="startTime"/> y el momento en que
        /// se llama a la operación. El contador debe ser de tipo Average.
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría del contador</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="instanceName">Nombre de la instancia del contador</param>
        /// <param name="startTime">Instante inicial que se usará para comparar con el momento
        /// actual para calcular la diferencia entre ambos e incrementar el contador acorde</param>
        public static void RegisterTime(string categoryName, string counterName,
            string instanceName, DateTime startTime)
        {
            if (isInitialized)
            {
                CounterData counterData =
                    PerformanceCounterContainer.GetCounter(categoryName, counterName);

                if (counterData != null)
                {
                    if (!counterData.HasBaseCounter)
                        throw new InstrumentationException(Messages.InvalidCounterForOperation);

                    if (counterData.HasInstance(instanceName))
                        counterData.GetInstance(instanceName).RegisterTime(startTime);
                }
            }
        } 

        #endregion methods
    }
}
