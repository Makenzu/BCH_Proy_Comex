using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BCH.Comex.Common.Tracing
{
    public class LogFileCleanupTask
    {
        #region Constructor
        public LogFileCleanupTask()
        {
        }
        #endregion

        #region - Methods -
        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="logPath">The log directory.</param>
        /// <param name="date">Anything prior will not be kept.</param>
        public void CleanUp(string logPath, DateTime deleteFilesOlderThan, int maxFilesToDelete)
        {
            if (string.IsNullOrEmpty(logPath))
                throw new Exception("No está configurada la ruta de los archivos de log en el web.config.");

            var dirInfo = new DirectoryInfo(logPath);
            if (!dirInfo.Exists)
                throw new Exception(string.Format("La ruta \"{0}\" configurada en el web.config no existe.", logPath));

            List<FileInfo> fileInfos = dirInfo.GetFiles("*.log").ToList();
            fileInfos.AddRange(dirInfo.GetFiles("*.svclog").ToList());

            // Recorre todos los archivos existentes en logDirectory, con los filtros de .log y .svclog
            using (var tracer = new Tracer("LogFileCleanUpTask.cs - CleanUp"))
            {
                // inicializamos la cantidad de archivos que hemos eliminado en la tarea
                int filesDeleted = 0;
                foreach (var info in fileInfos)
                {
                    // validamos que solo se ejecute el borrado para la cantidad de archivos configurado en maxFilesToDelete
                    if (filesDeleted >= maxFilesToDelete)
                        // salimos forzadamente del foreach para que no siga eliminando archivos
                        break;

                    // validamos la antiguedad del archivo contra lo configurado en el web.config
                    if (info.CreationTime.Date < deleteFilesOlderThan.Date)
                    {
                        try
                        {
                            filesDeleted++;
                            info.Delete();
                        }
                        catch (Exception e)
                        {
                            tracer.TraceError(string.Format("Error: No se pudo eliminar el archivo \"{0}\"", info.Name), e);
                        }
                    }
                }
                tracer.TraceInformation(string.Format("Log CleanUp Completo: Se eliminaron {0} archivo(s)", filesDeleted));
            }
        }
        #endregion
    }
}
