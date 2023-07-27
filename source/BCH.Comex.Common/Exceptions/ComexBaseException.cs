using System;
using System.Runtime.Serialization;
using System.Text;

namespace BCH.Comex.Common.Exceptions
{
    ///<summary>
    /// Excepción base de la solucion. Clase base. De esta clase heredan el resto de las
    /// excepciones propias de la solucion
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 06/08/2015
    ///</summary>
    [Serializable]
    public abstract class ComexBaseException : Exception
    {
        #region fields

        private string uniqueId = Guid.NewGuid().ToString();
        private const short category = 1;
        private string message;
        private string innerExceptionMessages = string.Empty;
        private int eventId = 0;

        #endregion fields

        #region properties
        /// <summary>
        /// Identificador único de instancia de error.
        /// (un Guid único para identificar la instancia del error). Este dato perfectamente 
        /// se le puede mostrar al usuario, para tener una trazabilidad exacta del error y 
        /// su correspondiente mensaje.
        /// </summary>
        public string UniqueId
        {
            get
            {
                return uniqueId;
            }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje de la excepción</param>
        public ComexBaseException(int eventId, string message, params object[] args)
            : base(String.Format(message, args))
        {
            this.eventId = eventId;
            this.message = String.Format(message, args);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Mensaje de la excepción</param>
        /// <param name="innerException">Excepción interna</param>
        public ComexBaseException(int eventId, Exception innerException, string message, params object[] args)
            : base(String.Format(message, args), innerException)
        {
            this.eventId = eventId;
            this.message = String.Format(message, args);

            SetInnerExceptionsMessages(innerException);
        }

        /// <summary>
        /// Constructor para serialización
        /// </summary>
        /// <param name="info">Información de serialización</param>
        /// <param name="context">Parámetro de contexto</param>
        public ComexBaseException(int eventId, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.eventId = eventId;
            this.message = context.ToString();
        }

        #endregion

        #region Private methods
        private void SetInnerExceptionsMessages(Exception innerException)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("InnerExceptions->");
            while (innerException != null)
            {
                sb.Append(innerException.GetType().ToString());
                sb.Append(":");
                sb.Append(innerException.Message);
                sb.Append("Stack Trace:");
                sb.Append(innerException.StackTrace);
                sb.Append("|");

                innerException = innerException.InnerException;
            }

            this.innerExceptionMessages = sb.ToString();
        }

        public override string ToString()
        {
            object[] args = new object[] { this.UniqueId, this.message, this.StackTrace, this.innerExceptionMessages };

            return String.Format(Messages.ComexCommonException, args);
        }
        #endregion
    }
}
