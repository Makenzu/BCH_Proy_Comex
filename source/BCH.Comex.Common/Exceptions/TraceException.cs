using System;
using System.Runtime.Serialization;

namespace BCH.Comex.Common.Exceptions
{
    ///<summary>
    /// Excepción interna de Tracing. Indica un error interno en este módulo.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 08/05/2015
    /// Fecha de modificación: 06/08/2015
    ///</summary>
    [Serializable]
    public class TraceException : ComexBaseException
    {
        #region fields
        private const int eventId = 2015;
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public TraceException(string mensaje, params object[] args)
            : base(eventId, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mensaje">Mensaje de le excepción</param>
        /// <param name="innerException">Excepción interna</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public TraceException(Exception innerException, string mensaje, params object[] args)
            : base(eventId, innerException, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor para serialización
        /// </summary>
        /// <param name="info">Información de serialización</param>
        /// <param name="context">Parámetro de contexto</param>
        public TraceException(SerializationInfo info, StreamingContext context)
            : base(eventId, info, context)
        {
        }

        #endregion
    }
}
