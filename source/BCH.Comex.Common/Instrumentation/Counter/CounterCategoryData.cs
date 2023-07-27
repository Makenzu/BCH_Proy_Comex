using BCH.Comex.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BCH.Comex.Common.Instrumentation.Counter
{
    /// <summary>
    /// Representa una categoría de contadores con su información asociada
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    internal class CounterCategoryData : IDisposable
    {
        #region fields

        bool isDisposed = false;
        private Dictionary<string, CounterData> counterDataList =
            new Dictionary<string, CounterData>();
        private string name;
        private string description;
        private PerformanceCounterCategoryType type;
        private bool isActive;

        #endregion fields

        #region ctor and finalizers

        #region Dispose-Finalize Pattern

        /// <summary>
        /// Cuando se llama a ese metodo se produce la liberación del objeto en memoria
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cuando se llama a ese metodo se produce la liberación del objeto en memoria
        /// </summary>
        /// <param name="disposing">Si es True libera de memoria los recursos no manejados</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.isDisposed)
            {
                foreach (CounterData counter in GetAllCounters())
                {
                    counter.Dispose();
                }
                this.isDisposed = true;
            }
        }

        /// <summary>
        /// Destructor de la clase
        /// </summary>
        ~CounterCategoryData()
        {
            Dispose(false);
        }

        #endregion

        #endregion ctor and finalizers

        #region properties

        /// <summary>
        /// Nombre de la categoría
        /// </summary>
        public string Name
        {
            get
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed), 
                        Messages.ResourceDisposed);
                    
                return this.name;
            }
            set
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                this.name = value;
            }
        }

        /// <summary>
        /// Descripción de la categoría
        /// </summary>
        public string Description
        {
            get
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                return this.description;
            }
            set
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                this.description = value;
            }
        }

        /// <summary>
        /// Tipo de la categoría
        /// </summary>
        public PerformanceCounterCategoryType Type
        {
            get
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                return this.type;
            }
            set
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                this.type = value;
            }
        }

        /// <summary>
        /// Indica si la categoría esta activa
        /// </summary>
        public bool IsActive
        {
            get
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                return isActive;
            }
            set
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                this.isActive = value;
            }
        }

        #endregion properties

        #region methods

        /// <summary>
        /// Retorna un <see cref="System.Collections.IEnumerable"/> con todos los contadores que contiene
        /// </summary>
        /// <returns><see cref="System.Collections.IEnumerable"/> con todos los contadores que contiene</returns>
        internal IEnumerable<CounterData> GetAllCounters()
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            foreach (KeyValuePair<string, CounterData> keyValueCounterData in
                counterDataList)
            {
                yield return keyValueCounterData.Value;
            }
        }

        /// <summary>
        /// Agrega un nuevo contador a la lista
        /// </summary>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="counterDescription">Descripcion del contador</param>
        /// <param name="counterType">Tipo del contador</param>
        internal void AddCounter(string counterName,
            string counterDescription, BCHComexPerformanceCounterType counterType)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            switch (counterType)
            {
                case BCHComexPerformanceCounterType.NumberOfItems:
                case BCHComexPerformanceCounterType.RateOfCountsPerSecond:
                    AddCounter(counterName, counterDescription,
                        counterType, false);
                    break;
                case BCHComexPerformanceCounterType.AverageTimer:
                    AddCounter(counterName, counterDescription,
                        counterType, true);
                    break;
                default:
                    throw new InstrumentationException(string.Format(
                        Messages.InvalidType, counterType.ToString()));

            }
        }

        /// <summary>
        /// Remueve el contador <paramref name="counterName"/> de la lista
        /// </summary>
        /// <param name="counterName">Nombre del contador</param>
        internal void RemoveCounter(string counterName)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            if (counterDataList.ContainsKey(counterName))
                counterDataList.Remove(counterName);
        }

        /// <summary>
        /// Agrega una nueva instancia de contador
        /// </summary>
        /// <param name="counterName">Nombre del contador que contendrá a la instancia</param>
        /// <param name="instanceName">Nombre de la instancia</param>
        /// <param name="isActive">Indica si la instancia está activa</param>
        internal void AddCounterInstance(string counterName, string instanceName, bool isActive)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            if (counterDataList.ContainsKey(counterName))
            {
                CounterData counterData = counterDataList[counterName];
                counterData.AddInstance(instanceName, isActive);
            }
        }

        /// <summary>
        /// Indica si la categoría contiene al contador <paramref name="counterName"/>
        /// </summary>
        /// <param name="counterName">Nombre del contador</param>
        /// <returns>Indica si contiene o no al contador</returns>
        internal bool HasCounter(string counterName)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            return counterDataList.ContainsKey(counterName);
        }

        /// <summary>
        /// Obtiene un contador por su nombre
        /// </summary>
        /// <param name="counterName">Nombre del contador</param>
        /// <returns>Contador obtenido, o null en caso de no existir</returns>
        internal CounterData GetCounter(string counterName)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            CounterData counterData = null;
            if (counterDataList.ContainsKey(counterName))
                counterData = counterDataList[counterName];

            return counterData;
        }

        /// <summary>
        /// Indica si la categoría contiene una instancia de contador
        /// </summary>
        /// <param name="counterName">Nombre de contador</param>
        /// <param name="instanceName">Nombre de instancia de contador</param>
        /// <returns>Indica si se contiene la instancia del contador</returns>
        internal bool HasCounterInstance(string counterName, string instanceName)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            return counterDataList.ContainsKey(counterName) &&
                counterDataList[counterName].HasInstance(instanceName);
        }

        /// <summary>
        /// Obtiene una instancia de contador
        /// </summary>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="instanceName">Nombre de la instancia de contador</param>
        /// <returns>Instancia obtenida, o null si no existe</returns>
        internal CounterInstanceData GetCounterInstance(string counterName, string instanceName)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            CounterInstanceData instanceData = null;
            if (counterDataList.ContainsKey(counterName))
                instanceData = counterDataList[counterName].GetInstance(instanceName);

            return instanceData;
        }

        /// <summary>
        /// Remueve la instancia <paramref name="instanceName"/> de la lista de instancias
        /// </summary>
        /// <param name="counterName">Nombre el contador que contiene la instancia</param>
        /// <param name="instanceName">Nombre de la instancia a quitar</param>
        internal void RemoveCounterInstance(string counterName, string instanceName)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            if (HasCounter(counterName))
                counterDataList[counterName].RemoveInstance(instanceName);
        }

        /// <summary>
        /// Agrega un nuevo contador a la categoría
        /// </summary>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="counterDescription">Descripción del contador</param>
        /// <param name="counterType">Tipo del contador</param>
        /// <param name="hasBaseCounter">Indica si el contador tiene contador base asociado</param>
        private void AddCounter(string counterName, string counterDescription,
            BCHComexPerformanceCounterType counterType, bool hasBaseCounter)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            if (counterDataList.ContainsKey(counterName))
                throw new InstrumentationException(string.Format(
                    Messages.CategoryAlreadyHasCounterName, this.Name,
                    counterName));

            CounterData counterData = new CounterData()
            {
                Name = counterName,
                Description = counterDescription,
                Type = Utils.ToPerformanceCounterType(counterType),
                HasBaseCounter = hasBaseCounter
            };

            if (hasBaseCounter)
            {
                counterData.BaseName = counterName + "Base";
                counterData.BaseDescription = "Contador base de " + counterName;
                counterData.BaseType = Utils.ToPerformanceCounterBaseType(counterType);
            }

            counterDataList.Add(counterName, counterData);
        }

        #endregion methods

    }
}
