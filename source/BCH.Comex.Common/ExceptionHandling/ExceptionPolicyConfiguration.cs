using System;

namespace BCH.Comex.Common.ExceptionHandling
{
    /// <summary>
    /// Configuración de políticas para exceptiones
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 03/05/2015
    /// </summary>
    internal class ExceptionPolicyConfiguration
    {
        #region properties
        /// <summary>
        /// Nombre de la política
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Acción a tomarse en la política
        /// </summary>
        public HandlingAction Action
        {
            get;
            set;
        }

        /// <summary>
        /// Tipo de excepción a manejar
        /// </summary>
        public Type ExceptionType
        {
            get;
            set;
        }

        /// <summary>
        /// Tipo de la nueva excepción
        /// </summary>
        public Type NewExceptionType
        {
            get;
            set;
        }

        /// <summary>
        /// Mensaje de la nueva excepción
        /// </summary>
        public string NewExceptionMessage
        {
            get;
            set;
        }
        #endregion
    }
}
