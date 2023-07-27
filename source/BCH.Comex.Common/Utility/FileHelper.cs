using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace BCH.Comex.Common.Utility
{
    public class FileHelper
    {
        private static string directoryPath = ConfigurationManager.AppSettings["LogCleaning.LogPath"];

        /// <summary>
        /// Genera un archivo de texto
        /// </summary>
        /// <param name="textoArchivo">Contenido del archivo</param>
        /// <returns>Arreglo de bytes del archivo</returns>
        public static byte[] GenerateTextFile(string textoArchivo)
        {
            MemoryStream ms = new MemoryStream();
            using (var sw = new StreamWriter(ms, Encoding.Default))
            {
                string contenido = textoArchivo;
                sw.Write(contenido);
                sw.Flush();
                ms.Position = 0;

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Genera un archivo zip
        /// </summary>
        /// <param name="originalFileStream">Diccionario contiene nombre de archivo y Memory stream correspondiente</param>
        /// <param name="fileName">Nombre que tendra el archivo dentro del Zip</param>
        /// <returns>Arreglo de bytes del archivo compreso</returns>
        public static byte[] GetZippedFiles(Dictionary<string, MemoryStream> originalFilesStream)
        {
            using (MemoryStream zipStream = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    foreach (var item in originalFilesStream)
                    {
                        var zipEntry = zip.CreateEntry(item.Key);
                        using (var writer = new StreamWriter(zipEntry.Open()))
                        {
                            item.Value.WriteTo(writer.BaseStream);
                        }
                    }
                }
                return zipStream.ToArray();
            }
        }

        /// <summary>
        /// Obtiene archivos de logs necesarios
        /// </summary>
        /// <param name="logDate">Fecha de los logs</param>
        /// <returns>Diccionario de archivos con su respectivo nombre</returns>
        public static Dictionary<string, MemoryStream> GetLogFiles(string logDate)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

            // Se obtienen logs del directorio
            FileInfo[] archivosLog = directoryInfo.GetFiles();

            Dictionary<string, MemoryStream> logFiles = new Dictionary<string, MemoryStream>();

            // Buscamos por fecha los que nos interesan
            var fecha = string.IsNullOrWhiteSpace(logDate) ? DateTime.Now.ToString("yyyy-MM-dd") : logDate;
            var logsNecesarios = archivosLog.Where(x => x.Name.Contains(fecha)).ToList();

            foreach (var log in logsNecesarios)
            {
                var fileName = string.Format("{0}_{1}", Environment.MachineName, log.Name);
                string logTemp = Path.Combine(Path.GetTempPath(), fileName);

                // Los copiamos a una ruta temporal para que no falle porque el archivo de log este en uso
                File.Copy(Path.Combine(directoryPath, log.Name), logTemp, false);

                FileInfo logDisponible = new FileInfo(logTemp);

                MemoryStream ms = new MemoryStream();

                using (FileStream fs = logDisponible.OpenRead())
                {
                    fs.CopyTo(ms);
                    logFiles.Add(logDisponible.Name, ms);
                }

                // Como ya tenemos el log en el diccionario se elimina de la carpeta temp
                File.Delete(logTemp);
            }

            return logFiles;
        }
    }
}
