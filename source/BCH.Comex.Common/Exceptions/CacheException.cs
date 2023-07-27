using System;
using System.Runtime.Serialization;

namespace BCH.Comex.Common.Exceptions
{
    ///<summary>
    /// Excepción interna de Caching. Indica un error interno en este módulo.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 08/05/2015
    /// Fecha de modificación: 27/05/2010
    ///</summary>
    
    [Serializable]
    public class CacheException : ComexBaseException
    {
        #region fields
        private const int _eventId = 2002;
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public CacheException(string mensaje, params object[] args)
            : base(_eventId, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mensaje">Mensaje de le excepción</param>
        /// <param name="innerException">Excepción interna</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public CacheException(Exception innerException, string mensaje, params object[] args)
            : base(_eventId, innerException, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor para serialización
        /// </summary>
        /// <param name="info">Información de serialización</param>
        /// <param name="context">Parámetro de contexto</param>
        public CacheException(SerializationInfo info, StreamingContext context)
            : base(_eventId, info, context)
        {
        }
        
        #endregion
    }
}
