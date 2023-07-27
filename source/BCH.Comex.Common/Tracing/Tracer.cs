using BCH.Comex.Common.Exceptions;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace BCH.Comex.Common.Tracing
{
    /// <summary>
    /// Clase encargada de realizar el tracing
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 05/08/2015
    /// </summary>
    public class Tracer : IDisposable
    {
        #region fields
        /// <summary>
        /// Cantidad e frames a saltearse cuando se obtiene el nombre del metodo llamador
        /// desde Initialize()
        /// </summary>
        private const int initializeMethodFramesQuantity = 2;
        /// <summary>
        /// Nombre de la clave en .config del nombre del source a utilizar
        /// </summary>
        private const string activeTraceSourceKeyName = "activeTraceSourceName";
        /// <summary>
        /// Nombre del source de trace por defecto en caso de no encontrarse configurado;
        /// </summary>
        private const string defaultSourceName = "BCHComexTraceSource";
        /// <summary>
        /// Nombre de la clave en .config del nombre del switch de log liviano
        /// </summary>
        private const string lightweightSwitchKeyName = "lightweight";

        private TraceSource traceSource;
        private BooleanSwitch lightweightSwitch;
        private bool isDisposed = false;
        private bool isLogicalOperationStart = false;
        private TraceRecord defaultTraceRecord;
        private string operation;

        private static string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Propiedades definidas por el usuario 
        /// </summary>
        private Dictionary<string, object> context;

        public string ActividadID
        {
            get
            {
                return defaultTraceRecord.ActivityId.ToString();                    
            }
        }

        #endregion fields

        #region ctor & finalizers

        /// <summary>
        /// Constructor que no inicia una nueva operación lógica
        /// </summary>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer())
        /// {
        ///     Metodo();
        ///     OtroMetodo();
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer()
        {
            Initialize(null, null, null);
        }

        /// <summary>
        /// Constructor que inicia una nueva operación lógica
        /// </summary>
        /// <param name="operation">Nombre de la operación lógica que se inicia</param>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer("Nombre de operación lógica"))
        /// {
        ///     Metodo();
        ///     OtroMetodo();
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer(string operation)
        {
            Initialize(operation, null, null);
        }


        /// <summary>
        /// Constructor.
        /// Este overload permite especificarle un source específico donde hacer el log en lugar
        /// de utilizar el por default de la aplicacion.
        /// </summary>
        /// <param name="customTraceSource">trace source explicito al cual loguear</param>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer(new TraceSource(minombredetracesource, SourceLevels.All))
        /// {
        ///     t.TraceError(mimensaje)
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer(TraceSource customTraceSource)
        {
            Initialize(null, customTraceSource, null);
        }

        /// <summary>
        /// Constructor.
        /// Este overload permite especificarle un source específico donde hacer el log en lugar
        /// de utilizar el por default de la aplicacion.
        /// </summary>
        /// <param name="customTraceSource">trace source explicito al cual loguear</param>
        /// <param name="callerMethodName">método que inicia el trace</param>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer(new TraceSource(minombredetracesource, SourceLevels.All))
        /// {
        ///     t.TraceError(mimensaje)
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer(TraceSource customTraceSource, string callerMethodName)
        {
            Initialize(null, customTraceSource, callerMethodName);
        }

        /// <summary>
        /// Constructor.
        /// Este overload permite especificarle un source específico donde hacer el log en lugar
        /// de utilizar el por default de la aplicacion.
        /// </summary>
        /// <param name="operation">Nombre de la operación lógica que se inicia</param>
        /// <param name="customTraceSource">trace source explicito al cual loguear</param>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer("Nombre de operación lógica", new TraceSource(minombredetracesource, SourceLevels.All))
        /// {
        ///     t.TraceError(mimensaje)
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer(string operation, TraceSource customTraceSource)
        {
            Initialize(operation, customTraceSource, null);
        }

        /// <summary>
        /// Constructor.
        /// Este overload permite especificarle un source específico donde hacer el log en lugar
        /// de utilizar el por default de la aplicacion.
        /// </summary>
        /// <param name="operation">Nombre de la operación lógica que se inicia</param>
        /// <param name="customTraceSource">trace source explicito al cual loguear</param>
        /// <param name="callerMethodName">nombre del método invocador</param>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer("Nombre de operación lógica", new TraceSource(minombredetracesource, SourceLevels.All, callerMethodName))
        /// {
        ///     t.TraceError(mimensaje)
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer(string operation, TraceSource customTraceSource, string callerMethodName)
        {
            Initialize(operation, customTraceSource, callerMethodName);
        }


        /// <summary>
        /// Constructor.
        /// Este overload permite especificarle un source específico donde hacer el log en lugar
        /// de utilizar el por default de la aplicacion.
        /// </summary>
        /// <param name="operation">Nombre de la operación lógica que se inicia</param>
        /// <param name="customTraceSourceName">nombre del trace source explicito al cual loguear</param>
        /// <example>
        /// En el ejemplo se muestra el uso del componente de tracing
        /// <code>
        /// using (Tracer t = new Tracer("Nombre de operación lógica", minombredetracesource)
        /// {
        ///     t.TraceError(mimensaje)
        ///     ...
        /// }
        /// </code>
        /// </example>
        public Tracer(string operation, string customTraceSourceName)
        {
            Initialize(operation, new TraceSource(customTraceSourceName, SourceLevels.All), null);
        }

        /// <summary>
        /// Libera los recursos utilizados por el objeto
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Libera los recursos utilizados por el objeto
        /// </summary>
        /// <param name="disposing">Define si ya se comenzó el proceso de liberación de recuross</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.isDisposed)
            {
                if (!this.IsLightweightEnabled)
                {
                    if (context.Count > 0)
                    {
                        TraceRecord traceContext = defaultTraceRecord.Clone();
                        traceContext.DateTime = DateTime.Now;
                        traceContext.UtcDateTime = DateTime.UtcNow;
                        traceContext.Timestamp = DateTime.UtcNow.Ticks;
                        traceContext.Level = TraceEventType.Verbose.ToString();
                        traceContext.Context = ContextToText();
                        traceContext.Message = "Valores del contexto de la actividad.";
                        traceContext.Source = traceSource.Name;
                        traceSource.TraceData(TraceEventType.Verbose, (int)EventType.ContextValues, traceContext);
                    }

                    TraceRecord traceStop = defaultTraceRecord.Clone();
                    traceStop.DateTime = DateTime.Now;
                    traceStop.UtcDateTime = DateTime.UtcNow;
                    traceStop.Timestamp = DateTime.UtcNow.Ticks;
                    traceStop.Level = TraceEventType.Stop.ToString();
                    traceStop.Message = "Finalizo actividad";
                    if (this.operation != null)
                        traceStop.Message += ": " + this.operation;
                    traceStop.Source = traceSource.Name;

                    traceSource.TraceData(TraceEventType.Stop, (int)EventType.EndMethod, traceStop);
                }

                if (this.isLogicalOperationStart)
                    Trace.CorrelationManager.StopLogicalOperation();

                this.traceSource.Flush();

                this.isDisposed = true;
            }
        }

        /// <summary>
        /// Libera los recursos utilizados por el objeto
        /// </summary>
        ~Tracer()
        {
            Dispose(false);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Obtiene la bandera que especifica si es el comienzo de una operación lógica
        /// </summary>
        internal bool IsLogicalOperationStart
        {
            get
            {
                return this.isLogicalOperationStart;
            }
        }

        /// <summary>
        /// Obtiene el nombre de la aplicación del archivo de configuración
        /// </summary>
        internal string ActiveTraceSourceName
        {
            get
            {
                if (this.isDisposed)
                    throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

                string sourceName = ConfigurationManager.AppSettings[activeTraceSourceKeyName] ?? defaultSourceName;
                return sourceName;
            }
        }

        /// <summary>
        /// Retorna si la bandera de log liviano está activa
        /// </summary>
        private bool IsLightweightEnabled
        {
            get
            {
                if (this.isDisposed)
                    throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

                if (this.lightweightSwitch == null)
                {
                    //switch definido en configuración
                    this.lightweightSwitch = new BooleanSwitch(lightweightSwitchKeyName,
                        string.Empty);
                }
                return this.lightweightSwitch.Enabled;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Escribe una excepcion en el log
        /// </summary>
        /// <param name="message">Mensaje a insertar en el log</param>
        /// <param name="args">Parametros de formateo del mensaje</param>
        public void TraceException(string message, Exception ex)
        {
            if (this.isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            var inner = ex.InnerException;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[{0}] {1}: \n", version, message);
            sb.AppendLine(ex.ToString());
            while(inner != null)
            {
                sb.AppendFormat("InnerException: {0}", inner.ToString());
                inner = inner.InnerException;
            }

            TraceRecord trace = GetTraceRecord(TraceEventType.Warning, sb.ToString(), new object[0]);

            this.traceSource.TraceData(TraceEventType.Warning, (int)EventType.TraceWrite, trace);
        }

        /// <summary>
        /// Escribe un mensaje de error en el log
        /// </summary>
        /// <param name="message">Mensaje a insertar en el log</param>
        /// <param name="args">Parametros de formateo del mensaje</param>
        public void TraceError(string message, params object[] args)
        {
            if (this.isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            TraceRecord trace = GetTraceRecord(TraceEventType.Warning, message, args);

            this.traceSource.TraceData(TraceEventType.Warning, (int)EventType.TraceWrite, trace);
        }

        /// <summary>
        /// Escribe un mensaje de información en el log
        /// </summary>
        /// <param name="message">Mensaje a insertar en el log</param>
        /// <param name="args">Parametros de formateo del mensaje</param>
        public void TraceInformation(string message, params object[] args)
        {
            if (this.isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            TraceRecord trace = GetTraceRecord(TraceEventType.Information, message, args);

            this.traceSource.TraceData(TraceEventType.Information, (int)EventType.TraceWrite, trace);
        }

        /// <summary>
        /// Escribe un mensaje de advertencia en el log
        /// </summary>
        /// <param name="message">Mensaje a insertar en el log</param>
        /// <param name="args">Parametros de formateo del mensaje</param>
        public void TraceWarning(string message, params object[] args)
        {
            if (this.isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            TraceRecord trace = GetTraceRecord(TraceEventType.Warning, message, args);

            this.traceSource.TraceData(TraceEventType.Warning, (int)EventType.TraceWrite, trace);
        }

        /// <summary>
        /// Escribe un mensaje verborrajico en el log
        /// </summary>
        /// <param name="message">Mensaje a insertar en el log</param>
        /// <param name="args">Parametros de formateo del mensaje</param>
        public void TraceVerbose(string message, params object[] args)
        {
            if (this.isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            TraceRecord trace = GetTraceRecord(TraceEventType.Verbose, message, args);

            this.traceSource.TraceData(TraceEventType.Verbose, (int)EventType.TraceWrite, trace);
        }


        /// <summary>
        /// Escribe en el log una entrada con los valores especificados en <paramref name="data"/>
        /// </summary>
        /// <param name="message">Información a insertar en el log</param>
        /// <param name="args">Parametros de formateo del mensaje</param>
        public void TraceWrite(string message, params object[] args)
        {
            if (this.isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            this.TraceInformation(message, args);
        }

        /// <summary>
        /// Agrega un valor con un id al contexto del tracer
        /// </summary>
        /// <param name="id">Idenrificador de lo que se quiere agregar al contexto</param>
        /// <param name="value">Valor de lo que se quiere agregar al contexto</param>
        public void AddToContext(string id, object value)
        {
            if (this.isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            int i = 0;
            if (context.ContainsKey(id))
            {
                while (context.ContainsKey(id + i))
                {
                    i++;
                }
                context.Add(id + i, value);
            }
            else
            {
                context.Add(id, value);
            }
        }

        /// <summary>
        /// Quita el valor con determinado id del contexto
        /// </summary>
        /// <param name="id">Identificador del valor a quitar del contexto</param>
        public void RemoveFromContext(string id)
        {
            if (this.isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            if (context.ContainsKey(id))
                context.Remove(id);
        }

        /// <summary>
        /// Obtiene el valor del contexto a partir de su id
        /// </summary>
        /// <param name="id">Identificador del valor</param>
        /// <returns>Valor obtenido</returns>
        public object GetFromContext(string id)
        {
            if (this.isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            object retorno = null;
            if (context.ContainsKey(id))
                retorno = context[id];

            return retorno;
        }

        /// <summary>
        /// Inicializa el contexto de la operación lógica
        /// </summary>
        /// <param name="operation">Nombre de la operación lógica que se inicia</param>
        /// <param name="customTraceSource">custom trace source opcional para utilizar para loguear la excepcion. Este valor puede ser null</param>
        public void Initialize(string operation, TraceSource customTraceSource, string callerMethodName)
        {
            if (this.isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            this.context = new Dictionary<string, object>();
            this.operation = operation;

            if (Trace.CorrelationManager.ActivityId == Guid.Empty)
                Trace.CorrelationManager.ActivityId = Guid.NewGuid();

            if (!String.IsNullOrEmpty(operation))
            {
                this.isLogicalOperationStart = true;
                Trace.CorrelationManager.StartLogicalOperation(operation);
            }

            // Obtengo todos los datos de ambiente en la inicialización, 
            // ya que en general resulta una operación costosa.
            defaultTraceRecord = TraceRecord.Default;

            if (!String.IsNullOrEmpty(callerMethodName))
            {
                defaultTraceRecord.CallerMethod = callerMethodName;
            }
            this.traceSource = customTraceSource ?? new TraceSource(this.ActiveTraceSourceName, SourceLevels.All);

            if (!this.IsLightweightEnabled)
            {
                if (String.IsNullOrEmpty(defaultTraceRecord.CallerMethod))
                {
                    defaultTraceRecord.CallerMethod = GetCallerMethodName(Tracer.initializeMethodFramesQuantity);
                }

                TraceRecord trace = defaultTraceRecord.Clone();
                trace.DateTime = DateTime.Now;
                trace.UtcDateTime = DateTime.UtcNow;
                trace.Timestamp = DateTime.UtcNow.Ticks;
                trace.Level = TraceEventType.Start.ToString();
                trace.Message = "Inicio de actividad";
                if (operation != null)
                    trace.Message += ": " + this.operation;
                trace.Source = traceSource.Name;
                
                this.traceSource.TraceData(TraceEventType.Start, (int)EventType.BeginMethod, trace);
            }
        }

        /// <summary>
        /// Obtiene el nombre del método que lo invocó según <see cref="System.Diagnostics.StackFrame"/> y 
        /// <paramref name="framesQty"/>
        /// </summary>
        /// <param name="framesQty">Cantidad de frames en el stack que hay que saltearse para 
        /// obtener el frame del método llamador</param>
        /// <returns>Nombre del método buscado o nulo el modo liviano está activo</returns>
        private string GetCallerMethodName(int framesQty)
        {
            if (this.isDisposed)
                throw new TraceException(new ObjectDisposedException(Messages.ResourceDisposed), Messages.ResourceDisposed);

            MethodBase mb = new StackFrame(framesQty + 1, false).GetMethod();
            return (mb != null && mb.ReflectedType != null) ? mb.ReflectedType.FullName + "." + mb.Name : string.Empty;
        }

        /// <summary>
        /// Convierte a texto los datos del contexto
        /// </summary>
        /// <returns></returns>
        private string ContextToText()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pair in context)
                sb.Append(String.Format("[{0}] = {1} | ", pair.Key, pair.Value ?? "[null]"));

            return sb.ToString();
        }

        /// <summary>
        /// Recupera el objeto de traza configurado 
        /// </summary>
        /// <param name="level">Nivel de traza</param>
        /// <param name="message">Mensaje</param>
        /// <param name="args">Argumentos para formatear el mensaje</param>
        /// <returns></returns>
        private TraceRecord GetTraceRecord(TraceEventType level, string message, object[] args)
        {
            TraceRecord trace = this.defaultTraceRecord.Clone();
            trace.DateTime = DateTime.Now;
            trace.UtcDateTime = DateTime.UtcNow;
            trace.Timestamp = DateTime.UtcNow.Ticks;
            trace.Level = level.ToString();
            if (args != null && args.Length > 0)
            {
            trace.Message = String.Format(message, args);
            }
            else
            {
                trace.Message = message;
            }
            trace.Source = this.traceSource.Name;
            return trace;
        }

        #endregion
    }
}
