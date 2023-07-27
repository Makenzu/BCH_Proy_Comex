using BCH.Comex.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BCH.Comex.Common.Instrumentation.Counter
{
    /// <summary>
    /// Clase que representa un contador son su información asociada
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    internal class CounterData : IDisposable
    {
        #region properties

        private bool isDisposed = false;
        private bool hasBaseCounter;
        private string name;
        private string baseName;
        private string description;
        private string baseDescription;
        private PerformanceCounterType type;
        private PerformanceCounterType baseType;

        #endregion

        #region ctor and finalizers

        #region Dispose-Finalize Pattern

        /// <summary>
        /// Cuando se llama a ese metodo se produce la eliminacion del objeto en memoria
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cuando se llama a ese metodo se produce la eliminacion del objeto en memoria
        /// </summary>
        /// <param name="disposing">Si es True elimina de memoria los recursos no manejados 
        /// y llama GC.SuppressFinalize( this )</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.isDisposed)
            {
                foreach (CounterInstanceData instance in GetAllInstances())
                {
                    instance.Dispose();
                }
                this.isDisposed = true;
            }
        }

        /// <summary>
        /// Destructor de la clase
        /// </summary>
        ~CounterData()
        {
            Dispose(false);
        }

        #endregion 

        #endregion ctor and finalizers

        #region fields

        private Dictionary<string, CounterInstanceData> instanceDataList = new
            Dictionary<string, CounterInstanceData>(); 

        #endregion fields

        #region properties

        /// <summary>
        /// Nombre del contador
        /// </summary>
        internal string Name
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
        /// Nombre del contador base asociado
        /// </summary>
        internal string BaseName
        {
            get
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                return this.baseName;
            }
            set
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                this.baseName = value;
            }
        }

        /// <summary>
        /// Descripción del contador
        /// </summary>
        internal string Description
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
        /// Descripción del contador base asociado
        /// </summary>
        internal string BaseDescription
        {
            get
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                return this.baseDescription;
            }
            set
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                this.baseDescription = value;
            }
        }

        /// <summary>
        /// Tipo del contador
        /// </summary>
        internal PerformanceCounterType Type
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
        /// Tipo del contador base asociado
        /// </summary>
        internal PerformanceCounterType BaseType
        {
            get
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                return this.baseType;
            }
            set
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                this.baseType = value;
            }
        }

        /// <summary>
        /// Indica si el contador tiene asociado un contador base
        /// </summary>
        internal bool HasBaseCounter
        {
            get 
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                return hasBaseCounter;
            }
            set 
            {
                if (this.isDisposed)
                    throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                        Messages.ResourceDisposed);

                hasBaseCounter = value;
            }
        } 

        #endregion properties

        #region methods

        /// <summary>
        /// Retorna un <see cref="System.Collections.IEnumerable"/> que lista todas las intancias que contiene
        /// </summary>
        /// <returns><see cref="System.Collections.IEnumerable"/> que lista todas las intancias</returns>
        internal IEnumerable<CounterInstanceData> GetAllInstances()
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            foreach (KeyValuePair<string, CounterInstanceData> keyValueInstance
                in instanceDataList)
            {
                yield return keyValueInstance.Value;
            }
        }

        /// <summary>
        /// Agrega una nueva instancia al contador
        /// </summary>
        /// <param name="instanceName">Nombre de la instancia</param>
        /// <param name="isActive">Si la instancia esta activa</param>
        internal void AddInstance(string instanceName, bool isActive)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);
            
            if (instanceDataList.ContainsKey(instanceName))
            {
                throw new InstrumentationException(string.Format(
                    Messages.CounterInstanceAlreadyExists,
                    instanceName, this.Name));
            }

            instanceDataList.Add(instanceName, new CounterInstanceData()
            {
                Name = instanceName,
                IsActive = isActive
            });
        }

        /// <summary>
        /// Indica si el contador contiene una instancia determinada
        /// </summary>
        /// <param name="instanceName">Nombre de la instancia</param>
        /// <returns>Si contiene o no la instancia</returns>
        internal bool HasInstance(string instanceName)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            return instanceDataList.ContainsKey(instanceName) && instanceDataList[instanceName].IsActive;
        }

        /// <summary>
        /// Recupera la instancia del contador
        /// </summary>
        /// <param name="instanceName">Nombre de la instancia</param>
        /// <returns>Datos de la instancia del contador</returns>
        internal CounterInstanceData GetInstance(string instanceName)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            CounterInstanceData instanceData = null;
            if (instanceDataList.ContainsKey(instanceName))
                instanceData = instanceDataList[instanceName];

            return instanceData;
        }

        /// <summary>
        /// Remueve la instancia <paramref name="instanceName"/> de la lista de instancias
        /// </summary>
        /// <param name="instanceName">Nombre de la instancia a quitar</param>
        internal void RemoveInstance(string instanceName)
        {
            if (this.isDisposed)
                throw new InstrumentationException(new ObjectDisposedException(Messages.ResourceDisposed),
                    Messages.ResourceDisposed);

            if (HasInstance(instanceName))
            {
                instanceDataList[instanceName].Dispose();
                instanceDataList.Remove(instanceName);
            }
        }

        #endregion methods

    }
}
