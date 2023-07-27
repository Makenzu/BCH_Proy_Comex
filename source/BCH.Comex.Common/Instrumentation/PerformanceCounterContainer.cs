using BCH.Comex.Common.Exceptions;
using BCH.Comex.Common.Instrumentation.Counter;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BCH.Comex.Common.Instrumentation
{
    /// <summary>
    /// Clase contenedora de los datos de los contadores de performance.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    internal static class PerformanceCounterContainer
    {
        #region fields

        private static readonly string defaultInstanceName = AppDomain.CurrentDomain.FriendlyName;
        private static readonly Dictionary<string, CounterCategoryData>
            categoryDataList = new Dictionary<string, CounterCategoryData>(); 

        #endregion fields

        #region properties

        /// <summary>
        /// Obtiene todos los <see cref="CounterCategoryData"/> contenidos por la clase
        /// </summary>
        /// <returns><see cref="System.Collections.IEnumerable"/> con toods los <see cref="CounterCategoryData"/>
        /// </returns>
        public static IEnumerable<CounterCategoryData> GetAllCategories()
        {
            foreach (KeyValuePair<string, CounterCategoryData> keyValueCategory in
                PerformanceCounterContainer.categoryDataList)
            {
                yield return keyValueCategory.Value;
            }
        } 

        #endregion properties

        #region methods

        /// <summary>
        /// Agrega una nueva categoría a la lista de categorías de contadores
        /// </summary>
        /// <param name="name">Nombre de la categoría</param>
        /// <param name="description">Descripción de la categoría</param>
        /// <param name="isActive">Indica si la categoría es activa o no</param>
        /// <param name="categoryType">Tipo de categoría</param>
        public static void AddPerformanceCounterCategory(string name, string description,
            bool isActive, PerformanceCounterCategoryType categoryType)
        {
            if (!categoryDataList.ContainsKey(name))
            {
                categoryDataList.Add(name, new CounterCategoryData()
                {
                    Name = name,
                    Description = description,
                    IsActive = isActive,
                    Type = categoryType
                });
            }
            else
            {
                throw new InstrumentationException(string.Format(
                    Messages.CounterCategoryAlreadyExists, name));
            }
        }

        /// <summary>
        /// Indica si la clase contiene a la categoría <paramref name="name"/> y está activa
        /// </summary>
        /// <param name="name">Nombre de la categoría</param>
        /// <returns>Si se encontro la categoría o no y si está activa o no</returns>
        public static bool HasCategory(string name)
        {
            return categoryDataList.ContainsKey(name) && categoryDataList[name].IsActive;
        }

        /// <summary>
        /// Elimina una categoría de contadores, junto con los contadores que contenga
        /// </summary>
        /// <param name="name">Nombre identificador de la caregoría a eliminar</param>
        public static void RemovePerformanceCounterCategory(string name)
        {
            if (HasCategory(name))
                categoryDataList.Remove(name);
        }

        /// <summary>
        /// Agrega un nuevo contador
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría del contador</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="counterDescription">Descripción del contador</param>
        /// <param name="counterType">Tipo del contador</param>
        public static void AddCounter(string categoryName, string counterName,
            string counterDescription, BCHComexPerformanceCounterType counterType)
        {
            if (!categoryDataList.ContainsKey(categoryName))
                throw new InstrumentationException(string.Format(Messages.CounterCategoryDoesntExist,
                    categoryName));

            CounterCategoryData categoryData = categoryDataList[categoryName];
            if (categoryData != null)
            {
                categoryData.AddCounter(counterName, counterDescription, counterType);
            }
        }

        /// <summary>
        /// Indica si la categoría <paramref name="categoryName"/> contiene el contador
        /// <paramref name="counterName"/>
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <returns>Si contiene o no el contador</returns>
        public static bool HasCounter(string categoryName, string counterName)
        {
            return HasCategory(categoryName) &&
                categoryDataList[categoryName].HasCounter(counterName);
        }

        /// <summary>
        /// Obtiene el contador especificado
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <returns>Contador especificado o null si no existe</returns>
        public static CounterData GetCounter(string categoryName,
            string counterName)
        {
            CounterData counterData = null;
            if (HasCategory(categoryName))
            {
                counterData = categoryDataList[categoryName].GetCounter(counterName);
            }

            return counterData;
        }

        /// <summary>
        /// Quita el contador <paramref name="counterName"/> de la categoría <paramref name="categoryName"/>
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría</param>
        /// <param name="counterName">Nombre del contador</param>
        public static void RemoveCounter(string categoryName, string counterName)
        {
            if (HasCategory(categoryName))
                categoryDataList[categoryName].RemoveCounter(counterName);
        }

        /// <summary>
        /// Agrega una nueva instancia de contador
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="instanceName">Nombre de la instancia</param>
        /// <param name="isActive">Instancia activa o no</param>
        public static void AddCounterInstance(string categoryName, string counterName,
            string instanceName, bool isActive)
        {
            if (HasCategory(categoryName))
                categoryDataList[categoryName].AddCounterInstance(counterName, instanceName, isActive);
        }

        /// <summary>
        /// Indica si se tiene la instancia especificada
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="instanceName">Nombre de la instancia</param>
        /// <returns>Si contiene o no la intancia</returns>
        public static bool HasCounterInstance(string categoryName, string counterName,
            string instanceName)
        {
            return HasCategory(categoryName) &&
                categoryDataList[categoryName].HasCounterInstance(counterName,
                    instanceName);
        }

        /// <summary>
        /// Obtiene la instancia de contador especificada
        /// </summary>
        /// <param name="categoryName">Nombre de la categoría</param>
        /// <param name="counterName">Nombre del contador</param>
        /// <param name="instanceName">Nombre de la instancia</param>
        /// <returns>Instancia de contador especificada o nula de lo contrario</returns>
        public static CounterInstanceData GetPerformanceCounterInstance(string categoryName,
            string counterName, string instanceName)
        {
            CounterInstanceData instanceData = null;

            if (HasCategory(categoryName))
                instanceData = categoryDataList[categoryName].GetCounterInstance(counterName,
                    instanceName);

            return instanceData;
        }

        /// <summary>
        /// Remueve la instancia <paramref name="instanceName"/> de la lista de instancias
        /// </summary>
        /// <param name="categoryName">Nombre el contador que contiene la instancia</param>
        /// <param name="counterName">Nombre el contador que contiene la instancia</param>
        /// <param name="instanceName">Nombre de la instancia a quitar</param>
        internal static void RemoveCounterInstance(string categoryName, string counterName, string instanceName)
        {
            if (HasCategory(categoryName))
            {
                categoryDataList[categoryName].RemoveCounterInstance(counterName,
                    instanceName);
            }
        }

        /// <summary>
        /// Limpia la lista de categorías
        /// </summary>
        public static void ClearCategoryDataList()
        {
            categoryDataList.Clear();
        }

        #endregion methods

    }
}
