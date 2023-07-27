using System;
using System.Diagnostics;
using System.Threading;

namespace BCH.Comex.Common.Tracing
{
    /// <summary>
    /// Representa los datos de trace a almacenar/recuperar
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 05/08/2015
    /// </summary>
    public class TraceRecord
    {
        #region fields

        #region static fields
        static int processId = Process.GetCurrentProcess().Id;
        static string processName = Process.GetCurrentProcess().ProcessName;
        static string machineName = Environment.MachineName;
        static string userDomainName = Environment.UserDomainName;
        static string userName = Environment.UserName;
        #endregion
        /// <summary>
        /// Identificador del trace
        /// </summary>
        public int TraceId { get; set; }
        /// <summary>
        /// Fecha del trace
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Fecha en formato UTC
        /// </summary>
        public DateTime UtcDateTime { get; set; }
        /// <summary>
        /// Fecha en ticks
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// Nombre del usuario Windows del proceso
        /// </summary>
        private string user;
        public string UserName
        {
            get { return user; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.Length > 50)
                        user = value.Substring(0, 50);
                    else
                        user = value;
            }
        }
        /// <summary>
        /// Identificador del proceso
        /// </summary>
        public int ProcessId { get; set; }
        /// <summary>
        /// Nombre del proceso
        /// </summary>
        private string process;
        public string ProcessName
        {
            get { return process; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.Length > 255)
                        process = value.Substring(0, 255);
                    else
                        process = value;
            }
        }
        /// <summary>
        /// Identificador del thread
        /// </summary>
        public int ThreadId { get; set; }
        /// <summary>
        /// Nivel del trhead
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// Origen
        /// </summary>
        private string source;
        public string Source
        {
            get { return source; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.Length > 255)
                        source = value.Substring(0, 255);
                    else
                        source = value;
            }
        }
        /// <summary>
        /// Identificador de la actividad
        /// </summary>
        public Guid ActivityId { get; set; }
        /// <summary>
        /// Nombre de la aplicación
        /// </summary>
        private string application;
        public string Application
        {
            get { return application; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.Length > 50)
                        application = value.Substring(0, 50);
                    else
                        application = value;
            }
        }
        /// <summary>
        /// Método llamador
        /// </summary>
        private string callerMethod;
        public string CallerMethod
        {
            get { return callerMethod; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    if (value.Length > 255)
                        callerMethod = value.Substring(0, 255);
                    else
                        callerMethod = value;
            }
        }
        /// <summary>
        /// Mensaje
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Contexto
        /// </summary>
        public string Context { get; set; }
        /// <summary>
        /// Stack de llamadas
        /// </summary>
        public string CallStack { get; set; }
        /// <summary>
        /// Nombre de la máquina
        /// </summary>
        public string MachineName { get; set; }
        #endregion

        /// <summary>
        /// Convierte el objeto a formato text
        /// </summary>
        /// <returns>Mensaje</returns>
        public override string ToString()
        {
            return Message;
        }

        /// <summary>
        /// Crea una copia del objeto
        /// </summary>
        /// <returns>Copia del objeto</returns>
        public TraceRecord Clone()
        {
            return new TraceRecord()
            {
                ActivityId = this.ActivityId,
                Application = this.Application,
                CallerMethod = this.CallerMethod,
                CallStack = this.Context,
                DateTime = this.DateTime,
                Level = this.Level,
                MachineName = this.MachineName,
                Message = this.Message,
                ProcessId = this.ProcessId,
                ProcessName = this.ProcessName,
                Source = this.Source,
                ThreadId = this.ThreadId,
                Timestamp = this.Timestamp,
                TraceId = this.TraceId,
                UserName = this.UserName,
                UtcDateTime = this.UtcDateTime
            };
        }

        #region Static Methods
        /// <summary>
        /// Recupera los valores por defecto para el objeto
        /// </summary>
        public static TraceRecord Default
        {
            get
            {
                return new TraceRecord()
                {
                    ActivityId = Trace.CorrelationManager.ActivityId,
                    Application = GlobalConstants.ApplicationName,
                    DateTime = DateTime.Now,
                    UtcDateTime = DateTime.UtcNow,
                    Timestamp = DateTime.UtcNow.Ticks,
                    ProcessId = processId,
                    ProcessName = processName,
                    MachineName = machineName,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    UserName = userDomainName + "\\" + userName,
                    Level = TraceEventType.Information.ToString()
                };
            }
        }

        #endregion

    }
}
