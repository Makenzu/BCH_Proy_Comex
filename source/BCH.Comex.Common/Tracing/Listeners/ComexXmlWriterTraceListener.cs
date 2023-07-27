using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BCH.Comex.Common.Tracing
{
    /// <summary>
    /// An extended XmlWriterTraceListener that starts a new file after a configured trace file size.
    /// </summary>
    [HostProtection(Synchronization = true)]
    public class ComexXmlWriterTraceListener : XmlWriterTraceListener
    {
        #region private fields
        /// <summary>
        /// This is the named capture group to find the numeric suffix of a trace file
        /// </summary>
        private readonly int LogFileNumberCaptureName = 6;

        /// <summary>
        /// Expresion regular usada para hacer match contra los archivos del log de WCF
        /// </summary>
        private Regex logfileSuffixExpression;

        /// <summary>
        /// The current numeric suffix for trace file names
        /// </summary>
        private int currentFileSuffixNumber = 0;

        /// <summary>
        /// The basic trace file name as it is configured in configuration file's system.diagnostics section. However, this
        /// class will append a numeric suffix to the file name (respecting the original file extension).
        /// </summary>
        private string basicTraceFileName = String.Empty;

        /// <summary>
        /// flag to knows if the attributes are loaded
        /// </summary>
        private bool attributesAreLoaded = false;

        /// <summary>
        /// max file size for the pagination in bytes.
        /// </summary>
        private long maxFileSize = 314572800;

        /// <summary>
        /// current file size of the writer file
        /// </summary>
        private long currentFileSize;

        /// <summary>
        /// expire date of the currentFileSize
        /// </summary>
        private DateTime currentFileSizeExpireTime = DateTime.MinValue;

        /// <summary>
        /// expire time in minutes to get the file length
        /// </summary>
        private int currentFileSizeMinutesToExpire = 1;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the ComexXmlWriterTraceListener class by specifying the trace file name.
        /// </summary>
        /// <param name="filename">The trace file name.</param>
        public ComexXmlWriterTraceListener(string filename) : base(filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                this.basicTraceFileName = filename;
                logfileSuffixExpression = new Regex(Path.GetFileNameWithoutExtension(this.basicTraceFileName) + @"-(\d+)-(\d+)-(\d+)((.(\d+))?)((.svclog)?)", RegexOptions.Compiled);
                this.currentFileSuffixNumber = this.GetTraceFileNumber();
                StartNewTraceFile(true);
            }
        }
        #endregion

        #region properties
        /// <summary>
        /// Gets the name of the current trace file. It is combined from the configured trace file plus an increasing number
        /// </summary>
        /// <value>The name of the current trace file.</value>
        public string CurrentTraceFileName
        {
            get
            {
                return Path.Combine(
                    Path.GetDirectoryName(this.basicTraceFileName),
                    Path.GetFileNameWithoutExtension(this.basicTraceFileName) + DateTime.Now.ToString("-yyyy-MM-dd") + "." + currentFileSuffixNumber.ToString() + Path.GetExtension(this.basicTraceFileName));
            }
        }

        public long MaxFileSize
        {
            get
            {
                return maxFileSize;
            }

            set
            {
                maxFileSize = value;
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Writes trace information, a data object, and event information to the file or stream.
        /// </summary>
        /// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache"/> that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">The source name.</param>
        /// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType"/> values.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">A data object to emit.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            StartNewTraceFileIfNeeded();

            base.TraceData(eventCache, source, eventType, id, data);
        }

        
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            StartNewTraceFileIfNeeded();

            base.TraceData(eventCache, source, eventType, id, data);
        }
        
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            StartNewTraceFileIfNeeded();

            base.TraceEvent(eventCache, source, eventType, id, format, args);
        }
        
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            StartNewTraceFileIfNeeded();

            base.TraceEvent(eventCache, source, eventType, id, message);
        }
        
        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            StartNewTraceFileIfNeeded();

            base.TraceTransfer(eventCache, source, id, message, relatedActivityId);
        }

        /// <summary>
        /// Writes the value of the object's <see cref="M:System.Object.ToString"/> method to the listener.
        /// </summary>
        /// <param name="o">An <see cref="T:System.Object"/> whose fully qualified class name you want to write.</param>
        public override void Write(object o)
        {
            StartNewTraceFileIfNeeded();

            base.Write(o);
        }

        /// <summary>
        /// Writes a category name and the value of the object's <see cref="M:System.Object.ToString"/> method to the listener.
        /// </summary>
        /// <param name="o">An <see cref="T:System.Object"/> whose fully qualified class name you want to write.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void Write(object o, string category)
        {
            StartNewTraceFileIfNeeded();

            base.Write(o, category);
        }

        /// <summary>
        /// Writes a verbatim message without any additional context information to the file or stream.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public override void Write(string message)
        {
            StartNewTraceFileIfNeeded();

            base.Write(message);
        }

        /// <summary>
        /// Writes a category name and a message to the listener.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void Write(string message, string category)
        {
            StartNewTraceFileIfNeeded();

            base.Write(message, category);
        }

        /// <summary>
        /// Writes the value of the object's <see cref="M:System.Object.ToString"/> method to the listener.
        /// </summary>
        /// <param name="o">An <see cref="T:System.Object"/> whose fully qualified class name you want to write.</param>
        public override void WriteLine(object o)
        {
            StartNewTraceFileIfNeeded();

            base.WriteLine(o);
        }

        /// <summary>
        /// Writes a category name and the value of the object's <see cref="M:System.Object.ToString"/> method to the listener.
        /// </summary>
        /// <param name="o">An <see cref="T:System.Object"/> whose fully qualified class name you want to write.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void WriteLine(object o, string category)
        {
            StartNewTraceFileIfNeeded();

            base.WriteLine(o, category);
        }

        /// <summary>
        /// Writes a verbatim message without any additional context information followed by the current line terminator to the file or stream.
        /// </summary>
        /// <param name="message">The message to write.</param>
        public override void WriteLine(string message)
        {
            StartNewTraceFileIfNeeded();

            base.WriteLine(message);
        }

        /// <summary>
        /// Writes a category name and a message to the listener, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void WriteLine(string message, string category)
        {
            StartNewTraceFileIfNeeded();

            base.WriteLine(message, category);
        }

        public override void Fail(string message, string detailMessage)
        {
            StartNewTraceFileIfNeeded();

            base.Fail(message, detailMessage);
        }
        #endregion

        #region private methods
        /// <summary>
        /// Causes the writer to start a new trace file
        /// </summary>
        private void StartNewTraceFile(bool firstTime)
        {
            currentFileSizeExpireTime = DateTime.MinValue;
            try
            {
                // si se llama en el constructor quiere decir que es la primera vez que cargo el writer
                if (firstTime)
                {
                    // instancio el writer
                    this.Writer = new StreamWriter(new FileStream(this.CurrentTraceFileName, FileMode.Append));
                }
                else
                {
                    // si no es primera vez, tomamos el writer existente
                    StreamWriter streamWriter = (StreamWriter)this.Writer;
                    FileStream fileStream = (FileStream)streamWriter.BaseStream;

                    // lo cerramos
                    fileStream.Close();

                    // abrimos uno nuevo y se lo pasamos al writer
                    this.Writer = new StreamWriter(new FileStream(this.CurrentTraceFileName, FileMode.Append));
                }
            }
            catch (Exception)
            {
                // no hacemos nada
            }
        }

        /// <summary>
        /// Gets the trace file number by checking whether similar trace files are already existant. The method will find the latest trace 
        /// file and return its number.
        /// </summary>
        /// <returns>The number of the latest trace file</returns>
        private int GetTraceFileNumber()
        {
            string directoryName = Path.GetDirectoryName(this.basicTraceFileName);
            string basicTraceFileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.basicTraceFileName);
            string basicTraceFileNameExtension = Path.GetExtension(this.basicTraceFileName);
            string[] existingLogFiles = Directory.GetFiles(directoryName, basicTraceFileNameWithoutExtension + "*");
            existingLogFiles = existingLogFiles.Where(c => DateTime.Compare(File.GetCreationTime(c).Date, DateTime.Today) == 0).ToArray<string>();

            int highestNumber = 0;
            foreach (string existingLogFile in existingLogFiles)
            {
                string tempFile = Path.GetFileNameWithoutExtension(existingLogFile);
                Match match = this.logfileSuffixExpression.Match(tempFile);
                if (match != null)
                {
                    int tempInt;
                    if (match.Groups.Count >= 1 && int.TryParse(match.Groups[LogFileNumberCaptureName].Value, out tempInt) && tempInt >= highestNumber)
                    {
                        highestNumber = tempInt;
                    }
                }
            }
            return highestNumber;
        }

        /// <summary>
        /// Get the fileInfo of the current Writer
        /// </summary>
        /// <returns></returns>
        private FileInfo GetFileInfo()
        {
            StreamWriter streamWriter = this.Writer as StreamWriter;
            FileStream fileStream = streamWriter.BaseStream as FileStream;
            return new FileInfo(fileStream.Name);
        }

        /// <summary>
        /// Create new trace file if needed
        /// </summary>
        private void StartNewTraceFileIfNeeded()
        {
            if (!attributesAreLoaded)
            {
                LoadAttributes();
            }

            FileInfo fileInfo = GetFileInfo();
            if (!fileInfo.Name.Contains(DateTime.Today.ToString("yyyy-MM-dd")))
            {
                this.currentFileSuffixNumber = 0;
                StartNewTraceFile(false);
            }
            else if (GetCurrentFileSize(fileInfo) > MaxFileSize)
            {
                this.currentFileSuffixNumber++;
                StartNewTraceFile(false);
            }
        }

        /// <summary>
        /// Gets the current file size of the writer
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public long GetCurrentFileSize(FileInfo fileInfo)
        {
            // Evitamos llamar 2 veces a la funcion y además evitamos que ambas devuelvan tiempos diferentes
            DateTime now = DateTime.Now;
            if (now > currentFileSizeExpireTime)
            {
                currentFileSize = fileInfo.Length;
                currentFileSizeExpireTime = now.AddMinutes(currentFileSizeMinutesToExpire);
            }
            return currentFileSize;
        }

        /// <summary>
        /// Get the supported attributes, in this case MaxFileSize is a custom attribute.
        /// </summary>
        protected override string[] GetSupportedAttributes()
        {
            return new string[] { "MaxFileSize" };
        }

        /// <summary>
        /// Load the max file size attribute for the shared listener in the web config
        /// </summary>
        private void LoadAttributes()
        {
            if(Attributes.ContainsKey("MaxFileSize"))
            {
                long fileSize = 0;
                if (long.TryParse(Attributes["MaxFileSize"], out fileSize))
                {
                    MaxFileSize = fileSize;
                }
            }
            attributesAreLoaded = true;
        }
        #endregion
    }
}
