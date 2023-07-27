using System;
using System.Runtime.Serialization;

namespace BCH.Comex.Common.Exceptions
{
    /// <summary>
    /// Excepction utilizada para errores internos de las aplicaciones
    /// </summary>
    public class ComexApplicationException : ComexBaseException
    {
        #region fields
        private const int _eventId = 2020;
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public ComexApplicationException(string mensaje, params object[] args)
            : base(_eventId, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mensaje">Mensaje de le excepción</param>
        /// <param name="innerException">Excepción interna</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public ComexApplicationException(string mensaje, Exception innerException, params object[] args)
            : base(_eventId, innerException, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor para serialización
        /// </summary>
        /// <param name="info">Información de serialización</param>
        /// <param name="context">Parámetro de contexto</param>
        public ComexApplicationException(SerializationInfo info, StreamingContext context)
            : base(_eventId, info, context)
        {
        }

        #endregion
    }
}
