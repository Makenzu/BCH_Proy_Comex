using BCH.Comex.Common.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BCH.Comex.Common.Tracing.Listeners
{
    /// <summary>
    /// TaceListener que procesa la información de manera asincrona a través de una cola en memoria y redirigiendo toda la información
    /// registrada a un <see cref="TraceSource" />  especificado en el atributo initializeDate
    /// </summary>
    public class AsyncTraceListener : TraceListener
    {
        #region Constants
        private const string ThrottlingQueueSizeAttributeName = "throttlingQueueSize";
        private const string MaxThreadsAttributeName = "maxThreads";
        private const long DefaultMaxThreads = 5;

        private const int StartEventId = 8000;
        private const int EndEventId = 8001;

        internal const string PerformanceCountersCategoryName = "BCH.Comex.Common.AsyncLogging";
        internal const string PerformanceCountersCategoryDescription = "Contadores de performance de BCHComex AsyncTraceListener";
        internal const string QueueLengthCounterName = "Profundidad de Cola";
        internal const string QueueLengthCounterDescription = "Numero total de mensajes pendientes de proceso";
        internal const string ActiveWorkerThreadsCounterName = "# Hilos Activos";
        internal const string ActiveWorkerThreadsCounterDescription = "Numero de hilos activos procesando la cola";
        internal const string SuccessCounterName = "# Mensajes registrados con éxito";
        internal const string SuccessCounterDescription = "Numero de mensajes registrados con exito";
        internal const string ErrorCounterName = "# Mensajes no registrados";
        internal const string ErrorCounterDescription = "Numero de mensaje que no se pudieron registrar";
        #endregion

        #region Private Fields
        /// <summary>
        /// Nombre de la instancia de los contadores de performance
        /// </summary>
        string _instanceName;

        /// <summary>
        /// Flag que determina si la instancia del listener está siendo dejada a disposición del Garbage Collector.
        /// </summary>
        bool _listenerDisposing = false;
        
        /// <summary>
        /// Thread que realiza el trabajo de escribir los logs en el source de destino
        /// </summary>
        Thread _writerThread;

        /// <summary>
        /// Semáforo para esperar eventos para escribir
        /// </summary>
        AutoResetEvent _writerWaitForJobEvent;
        
        /// <summary>
        /// Cola concurrente de memoria para almacenar temporalmente los elementos a escribir
        /// </summary>
        ConcurrentQueue<TraceInfo> _queue;
        
        /// <summary>
        /// Source de destino de los eventos.
        /// </summary>
        TraceSource _destination;
        
        /// <summary>
        /// Longitud de la cola a partir de la cual se deben agregar nuevos threads de proceso
        /// </summary>
        long _throttlingQueueSize;

        /// <summary>
        /// Flag que indica si la propiedad de longitud de cola ha sido inicializada
        /// </summary>
        bool _throttlingQueueSizeInitialized;
        
        /// <summary>
        /// Cantidad máxima de threads que puede alcanzar
        /// </summary>
        long _maxThreads;

        /// <summary>
        /// Flag que indica la cantidad máxima de threads ha sido inicializada
        /// </summary>
        bool _maxThreadsInitialized;

        /// <summary>
        /// Cantidad de threads en proceso
        /// </summary>
        int _threadCount = 0;

        /// <summary>
        /// Diccionario para guardar las instancias de los contadores de performance
        /// </summary>
        Dictionary<string, PerformanceCounter> _perfCounters = new Dictionary<string, PerformanceCounter>();

        /// <summary>
        /// Flag que indica si los contadore de performance están disponibles para se utilizados.
        /// </summary>
        /// <remarks>
        /// No estan disponibles si no fueron instalados.
        /// </remarks>
        bool _countersAreAvailable = false;

        #endregion Private Fields

        #region internal classes
        /// <summary>
        /// Calse que contiene la información de un evento registrado
        /// </summary>
        class TraceInfo
        {
            internal TraceEventCache EventCache{get;set;}
            internal string Source{get;set;}
            internal TraceEventType EventType{get;set;}
            internal int Id{get;set;}
            internal object[] Data{get;set;}
            internal string Message{get;set;}
            internal object[] Args{get;set;}
            internal Guid RelatedActivityId{get;set;}
            internal TraceCommand Command;
        }

        /// <summary>
        /// Tipo de comando a ejecutar en el source destino.
        /// </summary>
        enum  TraceCommand
        {
            Data = 1,
            DataArray = 2,
            Event = 3,
            EventMessage = 4,
            EventFormat = 5,
            Transfer = 6,
            Write = 7,
            WriteLine = 8
        }

        #endregion

        #region .ctor
        /// <summary>
        /// Constructor por defecto que debe fallar porque es necesario especificar el nombre del <see cref="TraceSource" />
        /// al que se redirige la informacion
        /// </summary>
        public AsyncTraceListener(): base()
        {
            throw new TraceException(new InvalidOperationException(Messages.InvalidTraceSource), Messages.InvalidTraceSource);
        }

        /// <summary>
        /// Crea e inicializa la instanaci del TraceListener
        /// </summary>
        /// <param name="destinationTraceSource">Nombre del TraceSource destino</param>
        public AsyncTraceListener(string destinationTraceSource) : base()
        {
            InitListener(destinationTraceSource);
        }

        /// <summary>
        /// Crea e inicializa la instanaci del TraceListener
        /// </summary>
        /// <param name="destinationTraceSource">Nombre del TraceSource destino
        /// <param name="name">Nombre del listener</param>
        public AsyncTraceListener(string destinationTraceSource, string name) : base(name)
        {
            InitListener(destinationTraceSource);
        }
        #endregion .ctor

        #region Public Properties
        /// <summary>
        /// Especifica que el Listener puede ser invocado desde múltiples Threads. Simepre retorna Verdadero
        /// </summary>
        public override bool IsThreadSafe
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Retorna el tamaño que debe alcanzar la cola antes de que se inicien más threads de proceso.
        /// Este valor esta asociado al atributo <b>throttlingQueueSize</b> de la configuración del listener
        /// </summary>
        public long ThrottlingQueueSize
        {
            get
            {
                if (!_throttlingQueueSizeInitialized)
                {
                    _throttlingQueueSize = long.MaxValue;
                    if (!String.IsNullOrEmpty(this.Attributes[ThrottlingQueueSizeAttributeName]))
                        long.TryParse(this.Attributes[ThrottlingQueueSizeAttributeName], out _throttlingQueueSize);
                    
                    _throttlingQueueSizeInitialized = true;
                }
                return _throttlingQueueSize;
            }
        }

        /// <summary>
        /// Retorna la cantidad maxima de threads que se pueden crear para procesar elementos de la cola de trace.
        /// Este valor esta asociado al atributo <b>maxThreads</b> de la configuración del listener
        /// </summary>
        public long MaxThreads
        {
            get
            {
                if (!_maxThreadsInitialized)
                {
                    _maxThreads = DefaultMaxThreads;
                    if (!String.IsNullOrEmpty(this.Attributes[MaxThreadsAttributeName]))
                        long.TryParse(this.Attributes[MaxThreadsAttributeName], out _maxThreads);

                    _maxThreadsInitialized= true;
                }
                return _maxThreads;
            }
        }

        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Retorna la lista de atributos soportados por el Listener. En este caso solo retorna un elemento para throttlingQueueSize
        /// </summary>
        protected override string[] GetSupportedAttributes()
        {
            return new string[] { ThrottlingQueueSizeAttributeName, MaxThreadsAttributeName };
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            Enqueue(new TraceInfo
            {
                EventCache = eventCache,
                Source = source,
                EventType = eventType,
                Id = id,
                Data = new object[] { data },
                Command = TraceCommand.Data
            });
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            Enqueue(new TraceInfo
                {
                    EventCache = eventCache,
                    Source = source,
                    EventType = eventType,
                    Id = id,
                    Data = data,
                    Command = TraceCommand.DataArray
                });
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            Enqueue(new TraceInfo
            {
                EventCache = eventCache,
                Source = source,
                EventType = eventType,
                Id = id,
                Command = TraceCommand.Event
            });
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            Enqueue(new TraceInfo
                {
                    EventCache = eventCache,
                    Source = source,
                    EventType = eventType,
                    Id = id,
                    Message = format,
                    Data = args,
                    Command = TraceCommand.EventFormat
                });
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            Enqueue(new TraceInfo
            {
                EventCache = eventCache,
                Source = source,
                EventType = eventType,
                Id = id,
                Message = message,
                Command = TraceCommand.EventMessage
            });
        }

        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            Enqueue(new TraceInfo
                {
                    EventCache = eventCache,
                    Source = source,
                    Id = id,
                    Message = message,
                    RelatedActivityId = relatedActivityId,
                    Command = TraceCommand.Transfer
                });
        }

        public override void WriteLine(string message)
        {
            Enqueue(new TraceInfo
            {
                Message = message,
                Command = TraceCommand.WriteLine
            });
        }

        public override void Write(string message)
        {
            Enqueue(new TraceInfo
                {
                    Message = message,
                    Command = TraceCommand.Write
                });
        }

        public override void Close()
        {
            if (IsWriterThreadAlive())
            {
                _writerThread.Join(new TimeSpan(0, 0, 10));
                _writerThread = null;
                _destination.TraceData(TraceEventType.Verbose, StartEventId, "Finalización del AsyncTraceListener.");
            }
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            _listenerDisposing = true;
            _writerWaitForJobEvent.Set();
            this.Close();
            this.DisposePerformanceCounters();
            base.Dispose(disposing);
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Inicializa el Listener y su thread de procesamiento asíncrono.
        /// </summary>
        private void InitListener(string destinationTraceSource)
        {
            _queue = new ConcurrentQueue<TraceInfo>();
            _destination = new TraceSource(destinationTraceSource);
            _instanceName = destinationTraceSource;

            InitInstrumentation();

            // Se envía un mensaje de inicialización a efectos de que los listeners tomen la configuración
            // y de esa manera evitar el deadlock producido cuando se intenta escribir en simultáneo en un
            // listener no inicializado.
            _destination.TraceData(TraceEventType.Verbose, EndEventId, "Incialización del AsyncTraceListener.");
            
            _writerThread = new Thread(WriterProc);
            _writerWaitForJobEvent = new AutoResetEvent(false);
            _throttlingQueueSizeInitialized = false;
            _maxThreadsInitialized = false;
            _writerThread.Start();
        }

        /// <summary>
        /// Inicializa los contadores de performance
        /// </summary>
        private void InitInstrumentation()
        {
            // Si no existe la categoría es porque no están instalados los contadores.
            if (!PerformanceCounterCategory.Exists(AsyncTraceListener.PerformanceCountersCategoryName))
                return;

            try
            {
                if (PerformanceCounterCategory.CounterExists(AsyncTraceListener.QueueLengthCounterName, AsyncTraceListener.PerformanceCountersCategoryName))
                    _perfCounters.Add(QueueLengthCounterName, new PerformanceCounter(AsyncTraceListener.PerformanceCountersCategoryName, AsyncTraceListener.QueueLengthCounterName, _instanceName, false));

                if (PerformanceCounterCategory.CounterExists(AsyncTraceListener.ActiveWorkerThreadsCounterName, AsyncTraceListener.PerformanceCountersCategoryName))
                    _perfCounters.Add(ActiveWorkerThreadsCounterName, new PerformanceCounter(AsyncTraceListener.PerformanceCountersCategoryName, AsyncTraceListener.ActiveWorkerThreadsCounterName, _instanceName, false));

                if (PerformanceCounterCategory.CounterExists(AsyncTraceListener.SuccessCounterName, AsyncTraceListener.PerformanceCountersCategoryName))
                    _perfCounters.Add(SuccessCounterName, new PerformanceCounter(AsyncTraceListener.PerformanceCountersCategoryName, AsyncTraceListener.SuccessCounterName, _instanceName, false));

                if (PerformanceCounterCategory.CounterExists(AsyncTraceListener.ErrorCounterName, AsyncTraceListener.PerformanceCountersCategoryName))
                    _perfCounters.Add(ErrorCounterName, new PerformanceCounter(AsyncTraceListener.PerformanceCountersCategoryName, AsyncTraceListener.ErrorCounterName, _instanceName, false));

                // Reseteo el valor inicial de los contadores.
                foreach (PerformanceCounter counter in _perfCounters.Values)
                {
                    counter.BeginInit();
                    counter.RawValue = 0;
                    counter.EndInit();
                }

                // Actualizo el flag señalando que los contadores estan disponibles para ser actualizados.
                _countersAreAvailable = true;
            }
            catch (Exception e)
            {
                TraceException te = new TraceException(e, "Ocurrió una excepción durante la inicialización de los contadores de performance del componente de trace asincrónico AsyncTraceListener).");
            }

        }

        /// <summary>
        /// Incrementa el valor del contador de performance
        /// </summary>
        /// <param name="counter"></param>
        private void IncreaseCounter(string counter)
        {
            // Si el listener se encuentra finalizando no se pueden actualizar los counters
            if (!_listenerDisposing && _countersAreAvailable)
            {
                // Si el contador existe, lo incremento.
                if (_perfCounters.ContainsKey(counter))
                    _perfCounters[counter].IncrementBy(1);
            }
        }

        /// <summary>
        /// Decrementa el valor del contador de performance
        /// </summary>
        /// <param name="counter"></param>
        private void DecreaseCounter(string counter)
        {
            // Si el listener se encuentra finalizando no se pueden actualizar los counters
            if (!_listenerDisposing && _countersAreAvailable)
            {
                // Si el contador existe, lo incremento.
                if (_perfCounters.ContainsKey(counter))
                    _perfCounters[counter].Decrement();
            }
        }

        /// <summary>
        /// Libera los contadores de performance
        /// </summary>
        private void DisposePerformanceCounters()
        {
            foreach (PerformanceCounter counter in _perfCounters.Values)
                counter.Dispose();
        }


        /// <summary>
        /// Agrega un elemento a la cola en memoria del Listener
        /// </summary>
        /// <param name="traceInfo"></param>
        private void Enqueue(TraceInfo traceInfo)
        {
            // Si el writer thread murió, se envia una excepción con el mensaje al event log.
            if (!IsWriterThreadAlive())
            {
                HandleError(new TraceException("Se ha detectado que el thread de procesamiento asíncrono del componente de Trace a fallado. Se reiniciará el thread."), traceInfo);
                return;
            }

            _queue.Enqueue(traceInfo);

            // Contador de profundidad de cola basado en el Count de Queue
            IncreaseCounter(QueueLengthCounterName);

            if (_queue.Count > this.ThrottlingQueueSize && _threadCount < MaxThreads)
            {
                Interlocked.Increment(ref _threadCount);

                Task.Factory.StartNew(ProcessQueue);
            }
            else
                _writerWaitForJobEvent.Set();
        }

        /// <summary>
        /// Metodo principal del thread de proceso asincronico
        /// </summary>
        private void WriterProc()
        {
            while (!_listenerDisposing)
            {
                _writerWaitForJobEvent.WaitOne();

                Interlocked.Increment(ref _threadCount);

                ProcessQueue();
            }
        }

        /// <summary>
        /// Procesa la cola en memoria hasta que no quede ningún elemento disponible
        /// </summary>
        private void ProcessQueue()
        {
            try
            {
                TraceInfo trace = null;

                // Incrementar contador de threads Activos
                IncreaseCounter(ActiveWorkerThreadsCounterName);

                while (_queue.TryDequeue(out trace))
                {
                    try
                    {
                        WriteToDestination(trace);

                        // Contador de éxitos.
                        IncreaseCounter(SuccessCounterName);

                    }
                    catch (Exception ex)
                    {
                        // Contador de errores.
                        IncreaseCounter(ErrorCounterName);
                        HandleError(ex, trace);
                    }
                    finally
                    {
                        // Contador de profundidad de cola
                        DecreaseCounter(QueueLengthCounterName);
                    }

                }

                // Decrementar el contador de threads activos
                DecreaseCounter(ActiveWorkerThreadsCounterName);
            }
            finally
            {
                Interlocked.Decrement(ref _threadCount);
            }
        }

        /// <summary>
        /// Registra un error ocurddio sin permitir que se propague
        /// </summary>
        /// <param name="exception">La execpción a registrar</param>
        /// <param name="traceInfo">la información que se solicito registra</param>
        private void HandleError(Exception exception, TraceInfo traceInfo)
        {
            try
            {
                // Creo una excepción solo a efectos de que se guarde en el Event Log.
                TraceException ex = new TraceException(exception, Messages.AsyncTraceError, traceInfo.Message);  
            }
            catch
            {
                // Ignoramos el error para evitar caida del thread de Proceso
            }
        }

        /// <summary>
        /// Escribe el mensaje en el TraceSource de destino
        /// </summary>
        private void WriteToDestination(TraceInfo trace)
        {

            if (trace.Command == TraceCommand.Data)
            {
                foreach (TraceListener listener in _destination.Listeners)
                    listener.TraceData(trace.EventCache, trace.Source, trace.EventType, trace.Id, trace.Data[0]);
                
                return;
            }

            if (trace.Command == TraceCommand.DataArray)
            {
                foreach (TraceListener listener in _destination.Listeners)
                    listener.TraceData(trace.EventCache, trace.Source, trace.EventType, trace.Id, trace.Data);

                return;
            }

            if (trace.Command == TraceCommand.Event)
            {
                foreach (TraceListener listener in _destination.Listeners)
                    listener.TraceEvent(trace.EventCache, trace.Source, trace.EventType, trace.Id);

                return;
            }

            if (trace.Command == TraceCommand.EventFormat)
            {
                foreach (TraceListener listener in _destination.Listeners)
                    listener.TraceEvent(trace.EventCache, trace.Source, trace.EventType, trace.Id, trace.Message, trace.Args);

                return;
            }

            if (trace.Command == TraceCommand.EventMessage)
            {
                foreach (TraceListener listener in _destination.Listeners)
                    listener.TraceEvent(trace.EventCache, trace.Source, trace.EventType, trace.Id, trace.Message);

                return;
            }

            if (trace.Command == TraceCommand.Transfer)
            {
                foreach (TraceListener listener in _destination.Listeners)
                    listener.TraceTransfer(trace.EventCache, trace.Source, trace.Id, trace.Message, trace.RelatedActivityId);

                return;
            }
            
            if (trace.Command == TraceCommand.Write)
            {
                foreach (TraceListener listener in _destination.Listeners)
                    listener.Write(trace.Message);

                return;
            }

            if (trace.Command == TraceCommand.WriteLine)
            {
                foreach (TraceListener listener in _destination.Listeners)
                    listener.WriteLine(trace.Message);

                return;
            }

        }

        /// <summary>
        /// Verifica que el thread de proceso este activo
        /// </summary>
        /// <returns></returns>
        private bool IsWriterThreadAlive()
        {
            return _writerThread!=null && _writerThread.IsAlive;
        }
        #endregion Private Methods
    }
 }
