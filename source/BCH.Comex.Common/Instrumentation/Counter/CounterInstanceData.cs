using System;
using System.Diagnostics;

namespace BCH.Comex.Common.Instrumentation.Counter
{
    /// <summary>
    /// Representa una instancia de contador con su información asociada
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    internal class CounterInstanceData : IDisposable
    {
        #region fields

        bool isActive;
        bool isDisposed = false;
        private string name;
        private PerformanceCounter realCounter;
        private PerformanceCounter realCounterBase;

        #endregion fields

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
                if (RealCounter != null)
                    RealCounter.Dispose();
                if (RealCounterBase != null)
                    RealCounterBase.Dispose();

                RealCounter = null;
                RealCounterBase = null;

                this.isDisposed = true;
            }
        }

        /// <summary>
        /// Destructor de la clase
        /// </summary>
        ~CounterInstanceData()
        {
            Dispose(false);
        }

        #endregion 

        #endregion ctor and finalizers

        #region properties


        /// <summary>
        /// Nombre de la instancia
        /// </summary>
        internal string Name
        {
            get
            {
                if (this.isDisposed)
                    throw new ObjectDisposedException(Messages.ResourceDisposed);
                return this.name;
            }
            set
            {
                if (this.isDisposed)
                    throw new ObjectDisposedException(Messages.ResourceDisposed);
                this.name = value;
            }
        }

        /// <summary>
        /// Contador de performance
        /// </summary>
        public PerformanceCounter RealCounter
        {
            get
            {
                if (this.isDisposed)
                    throw new ObjectDisposedException(Messages.ResourceDisposed);
                return this.realCounter;
            }
            set
            {
                if (this.isDisposed)
                    throw new ObjectDisposedException(Messages.ResourceDisposed);
                this.realCounter = value;
            }
        }

        /// <summary>
        /// Contador de performance base asociado a <see cref="RealCounter"/>
        /// </summary>
        public PerformanceCounter RealCounterBase
        {
            get
            {
                if (this.isDisposed)
                    throw new ObjectDisposedException(Messages.ResourceDisposed);
                return this.realCounterBase;
            }
            set
            {
                if (this.isDisposed)
                    throw new ObjectDisposedException(Messages.ResourceDisposed);
                this.realCounterBase = value;
            }
        }

        /// <summary>
        /// Indica si la instancia está activa
        /// </summary>
        internal bool IsActive
        {
            get 
            {
                if (this.isDisposed)
                    throw new ObjectDisposedException(Messages.ResourceDisposed);
                return this.isActive;
            }
            set
            {
                if (this.isDisposed)
                    throw new ObjectDisposedException(Messages.ResourceDisposed);
                this.isActive = value;
            }
        } 

        #endregion properties

        #region methods

        /// <summary>
        /// Registra la diferencia entre <paramref name="startTime"/> y <see cref="DateTime.Now"/>
        /// para incrementar el contador
        /// </summary>
        /// <param name="startTime">Momento inicial a comprarar</param>
        internal void RegisterTime(DateTime startTime)
        {
            if (this.isDisposed)
                throw new ObjectDisposedException(Messages.ResourceDisposed);

            RealCounter.IncrementBy(DateTime.UtcNow.Ticks - startTime.Ticks);
            RealCounterBase.Increment();
        }

        /// <summary>
        /// Decrementa el contador
        /// </summary>
        internal void DecreaseCounter()
        {
            if (this.isDisposed)
                throw new ObjectDisposedException(Messages.ResourceDisposed);

            RealCounter.Decrement();
        }

        /// <summary>
        /// Incrementa el contador la cantidad <paramref name="value"/>
        /// </summary>
        /// <param name="value">Cantidad a incrementar</param>
        internal void IncreaseCounter(long value)
        {
            if (this.isDisposed)
                throw new ObjectDisposedException(Messages.ResourceDisposed);

            RealCounter.IncrementBy(value);
        }

        /// <summary>
        /// Incrementa el contador en 1
        /// </summary>
        internal void IncreaseCounter()
        {
            if (this.isDisposed)
                throw new ObjectDisposedException(Messages.ResourceDisposed);

            RealCounter.Increment();
        } 

        #endregion methods

    }
}
