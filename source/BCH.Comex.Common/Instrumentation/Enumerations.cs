namespace BCH.Comex.Common.Instrumentation
{
    /// <summary>
    /// Tipos de contadores permitidos en la aplicación
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    public enum BCHComexPerformanceCounterType
    {
        /// <summary>
        /// Tipo cantidad de items
        /// </summary>
        NumberOfItems,
        /// <summary>
        /// Tipo promedio por tiempo
        /// </summary>
        AverageTimer,
        /// <summary>
        /// Tipo cantidad por segundo
        /// </summary>
        RateOfCountsPerSecond
    }
}
