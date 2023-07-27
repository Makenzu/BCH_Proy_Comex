using BCH.Comex.Common.Exceptions;
using System;
using System.Diagnostics;
using System.Text;

namespace BCH.Comex.Common.Instrumentation
{
    /// <summary>
    /// Funciones útiles al módulo de instrumentación
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    static class Utils
    {
        #region methods

        /// <summary>
        /// Convierte la entrada en un <see cref="PerformanceCounterCategoryType"/>
        /// </summary>
        /// <param name="categoryTypeName">Nombre del tipo de categoría</param>
        /// <returns><see cref="PerformanceCounterCategoryType"/> buscado</returns>
        public static PerformanceCounterCategoryType ToPerformanceCounterCategoryType(
            string categoryTypeName)
        {
            if (string.IsNullOrEmpty(categoryTypeName))
                throw new InstrumentationException(Messages.CategoryNameNullOrEmpty,new ArgumentNullException());

            try
            {
                return (PerformanceCounterCategoryType)Enum.Parse(typeof(PerformanceCounterCategoryType),
                    categoryTypeName);
            }
            catch (ArgumentException ae)
            {
                throw new InstrumentationException(string.Format(
                    Messages.InvalidCounterCategoryType, categoryTypeName, 
                    EnumerateEnumValues < PerformanceCounterCategoryType>()), ae);
            }
        }

        /// <summary>
        /// Convierte a <see cref="BCHComexPerformanceCounterType"/> el la representación en string
        /// del tipo de contador
        /// </summary>
        /// <param name="counterTypeName">Respresentacion en string del tipo de contador</param>
        /// <returns>tipo de contador</returns>
        public static BCHComexPerformanceCounterType ToBCHComexPerformanceCounterType(
            string counterTypeName)
        {
            if (string.IsNullOrEmpty(counterTypeName))
                throw new InstrumentationException(Messages.BCHComexCounterTypeNullOrEmpry);

            try
            {
                return (BCHComexPerformanceCounterType)Enum.Parse(typeof(BCHComexPerformanceCounterType),
                    counterTypeName);
            }
            catch (ArgumentException ae)
            {
                throw new InstrumentationException(string.Format(
                    Messages.InvalidBCHComexCounterType,
                    counterTypeName, EnumerateEnumValues<BCHComexPerformanceCounterType>()), ae);
            }
        }

        /// <summary>
        /// Mapea a <paramref name="BCHComexCounterType"/> en su correspondiente
        /// <see cref="PerformanceCounterType"/>
        /// </summary>
        /// <param name="BCHComexCounterType">Tipo de contador de BCHComex</param>
        /// <returns>tipo <see cref="PerformanceCounterType"/> asociado a 
        /// <paramref name="BCHComexCounterType"/>
        /// </returns>
        public static PerformanceCounterType ToPerformanceCounterType(
            BCHComexPerformanceCounterType BCHComexCounterType)
        {
            switch (BCHComexCounterType)
            {
                case BCHComexPerformanceCounterType.NumberOfItems:
                    return PerformanceCounterType.NumberOfItems64;
                case BCHComexPerformanceCounterType.AverageTimer:
                    return PerformanceCounterType.AverageTimer32;
                case BCHComexPerformanceCounterType.RateOfCountsPerSecond:
                    return PerformanceCounterType.RateOfCountsPerSecond64;
                default:
                    throw new InstrumentationException(string.Format(Messages.InvalidValue,
                        BCHComexCounterType.ToString()));
            }
        }

        /// <summary>
        /// Mapea un <see cref="BCHComexPerformanceCounterType"/> al tipo 
        /// <see cref="PerformanceCounterType"/> base asociado.
        /// </summary>
        /// <param name="BCHComexCounterType">tipo de contador</param>
        /// <returns>tipo base asociado</returns>
        /// <remarks>No todos los <see cref="BCHComexPerformanceCounterType"/> tienen
        /// tipo base asociado</remarks>
        public static PerformanceCounterType ToPerformanceCounterBaseType(
            BCHComexPerformanceCounterType BCHComexCounterType)
        {
            switch (BCHComexCounterType)
            {
                case BCHComexPerformanceCounterType.AverageTimer:
                    return PerformanceCounterType.AverageBase;
                default:
                    throw new InstrumentationException(string.Format(
                        Messages.ValueWithoutAssociatedBaseType,
                        BCHComexCounterType.ToString()));
            }
        }

        /// <summary>
        /// Arma un string con todos los valores del enumerado <paramref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Tipo que debe ser <see cref="Enum"/></typeparam>
        /// <returns>string con todos los valores del enumerado</returns>
        public static string EnumerateEnumValues<T>()
        {
            Type enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new InstrumentationException(
                    string.Format(Messages.ValueHasToBeEnum, enumType.ToString()));

            StringBuilder buffer = new StringBuilder();
            foreach (object value in Enum.GetValues(enumType))
            {
                if (buffer.Length == 0)
                    buffer.Append(value.ToString());
                else
                {
                    buffer.Append(" | " + value.ToString());
                }
            }

            return buffer.ToString();
        }

        #endregion
    }
}
