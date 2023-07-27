using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Common.Tracing
{
    [ComVisible(false)]
    public class Log4netTraceListener : TraceListener
    {
        /// <summary>
        /// Creación del Obj. del Log4Net
        /// </summary>
        protected static ILog _log;

        /// <summary>
        /// Constructor
        /// </summary>
        public Log4netTraceListener() : base() { }

        /// <summary>
        /// Se crear el archivo log en el disco
        /// </summary>
        /// <param name="name"></param>
        public Log4netTraceListener(string name) : base(name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _log = LogManager.GetLogger(name);
            }
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            TraceRecord traceData = data as TraceRecord;
            if (traceData != null)
            {
                try
                {
                    // le decimos al _log donde queremos escribir
                    _log = LogManager.GetLogger(Name);
                    // escribimos el mensaje
                    _log.Info(string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                        traceData.DateTime.ToString("yyyy-MM-dd HH:mm:ss.ffff"),
                        traceData.ActivityId,
                        traceData.Level,
                        traceData.Message,
                        traceData.Context));
                }
                catch (Exception)
                {
                    //no hago nada si falla el log
                }
            }
            else
            {
                base.TraceData(eventCache, source, eventType, id, data);
            }
        }

        /// <summary>
        /// Escribe desde la clase ComexTextWriterTraceListener
        /// </summary>
        /// <param name="message"></param>
        public override void Write(string message)
        {
            if (_log != null && !string.IsNullOrEmpty(message))
            {
                _log.Info(message);
            }
        }

        /// <summary>
        /// Escribe desde la clase ComexTextWriterTraceListener
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string message)
        {
            if (_log != null && !string.IsNullOrEmpty(message))
            {
                _log.Info(message);
            }
        }
    }
}
