namespace BCH.Comex.Common.ExceptionHandling
{
    /// <summary>
    /// Enumerado con los valores de la acción a tomar al manejar una excepción.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 03/05/2015
    /// </summary>
    public enum HandlingAction
    {
        /// <summary>
        /// Ninguno
        /// </summary>
        None = 0,
        /// <summary>
        /// Relanzar la excepción
        /// </summary>
        Rethrow = 1,
        /// <summary>
        /// Encapsular la excepción en otra
        /// </summary>
        Wrap = 2,
        /// <summary>
        /// Reemplazar la excepción
        /// </summary>
        Replace = 3,
        /// <summary>
        /// Loguea + Relanzar la excepción
        /// </summary>
        LogAndRethrow = 4,
        /// <summary>
        /// Encapsular la excepción en otra y loguea
        /// </summary>
        LogAndWrap = 5,
    }
}
