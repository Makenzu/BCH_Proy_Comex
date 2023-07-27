
namespace BCH.Comex.Common.Tracing
{
    /// <summary>
    /// Tipo de evento procesado por el Tracer
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 05/08/2015
    /// </summary>
    internal enum EventType
    {
        /// <summary>
        /// Comienzo de método
        /// </summary>
        BeginMethod,
        /// <summary>
        /// Fin de método
        /// </summary>
        EndMethod,
        /// <summary>
        /// Escritura de valores de contexto
        /// </summary>
        ContextValues,
        /// <summary>
        /// Escritura de un <see cref="TraceData"/>
        /// </summary>
        TraceWrite,
    }
}
