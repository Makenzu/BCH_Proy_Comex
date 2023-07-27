using System;

namespace BCH.Comex.Common.Tracing.Listeners
{
    public class ComexTextWriterTraceListener : Microsoft.VisualBasic.Logging.FileLogTraceListener
    {
        public ComexTextWriterTraceListener() : base() 
        { 
        }

        public ComexTextWriterTraceListener(string fileName) : base(fileName) 
        { 
        }

        public override void TraceData(System.Diagnostics.TraceEventCache eventCache, string source, System.Diagnostics.TraceEventType eventType, int id, params object[] data)
        {
            base.TraceData(eventCache, source, eventType, id, data);
        }

        public override void TraceData(System.Diagnostics.TraceEventCache eventCache, string source,
            System.Diagnostics.TraceEventType eventType, int id, object data)
        {
            var traceData = data as TraceRecord;
            if (traceData != null)
            {
                try
                {
                    base.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{4}",
                        traceData.DateTime.ToString("yyyy-MM-dd HH:mm:ss.ffff"),
                        traceData.ActivityId,
                        //traceData.MachineName,
                        //traceData.Application,
                        //traceData.UserName,
                        traceData.Level,
                        traceData.Message,
                        traceData.Context));
                }
                catch (Exception){ 
                    //no hago nada si falla el log
                }
            }
            else
            {
                base.TraceData(eventCache, source, eventType, id, data);
            }
        }

    }
}
