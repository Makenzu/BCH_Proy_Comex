using System;
using System.Runtime.Serialization;
using BCH.Comex.Common.Exceptions;

namespace BCH.Comex.Core.BL.SWG3
{
    ///<summary>
    /// Excepción interna de MT300. Indica un error interno en este módulo.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 14/02/2022
    /// Fecha de modificación: 
    ///</summary>

    [Serializable]
    public class Mt300Exception : ComexBaseException
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
        public Mt300Exception(string mensaje, params object[] args)
            : base(_eventId, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mensaje">Mensaje de le excepción</param>
        /// <param name="innerException">Excepción interna</param>
        /// <param name="args">Parametros a reemplazar en el mensaje</param>
        public Mt300Exception(Exception innerException, string mensaje, params object[] args)
            : base(_eventId, innerException, mensaje, args)
        {
        }

        /// <summary>
        /// Constructor para serialización
        /// </summary>
        /// <param name="info">Información de serialización</param>
        /// <param name="context">Parámetro de contexto</param>
        public Mt300Exception(SerializationInfo info, StreamingContext context)
            : base(_eventId, info, context)
        {
        }

        #endregion
    }
}
