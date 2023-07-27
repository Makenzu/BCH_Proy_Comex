using System;
using System.Configuration;

namespace BCH.Comex.Common.Instrumentation.Configuration
{
    /// <summary>
    /// Colección de elementos <see cref="PerformanceCounterCategoryElement"/>
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    class PerformanceCounterCategoryElementCollection : ConfigurationElementCollection
    {
        #region properties

        /// <summary>
        /// Tipo de colección de elementos
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        /// <summary>
        /// Nombre del elemento add
        /// </summary>
        public new string AddElementName
        {
            get
            {
                return base.AddElementName;
            }

            set
            {
                base.AddElementName = value;
            }
        }

        /// <summary>
        /// Nombre del elemento clear
        /// </summary>
        public new string ClearElementName
        {
            get
            {
                return base.ClearElementName;
            }

            set
            {
                base.AddElementName = value;
            }
        }

        /// <summary>
        /// Nombre del elemento remove
        /// </summary>
        public new string RemoveElementName
        {
            get
            {
                return base.RemoveElementName;
            }
        }

        /// <summary>
        /// Cantidad de elementos en la coleción
        /// </summary>
        public new int Count
        {
            get { return base.Count; }
        }

        /// <summary>
        /// Obtiene un elemento según su índice
        /// </summary>
        /// <param name="index">Posicion en la colección</param>
        /// <returns>Elemento en la ubicación <paramref name="index"/></returns>
        public PerformanceCounterCategoryElement this[int index]
        {
            get
            {
                return (PerformanceCounterCategoryElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Obtiene un elemento según su nombre
        /// </summary>
        /// <param name="Name">Nombre del elemento</param>
        /// <returns>Elemento de nombre <paramref name="Name"/></returns>
        new public PerformanceCounterCategoryElement this[string Name]
        {
            get
            {
                return (PerformanceCounterCategoryElement)BaseGet(Name);
            }
        } 

        #endregion properties

        #region methods

        /// <summary>
        /// Remueve el elemento en la posición <paramref name="index"/>
        /// </summary>
        /// <param name="index">Posición del elemento</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Remueve el elemento de nombre <paramref name="name"/>
        /// </summary>
        /// <param name="name">nombre del elemento</param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }

        /// <summary>
        /// Remueve un elemento de la colección
        /// </summary>
        /// <param name="element">Elemento a eliminar</param>
        public void Remove(PerformanceCounterCategoryElement element)
        {
            if (BaseIndexOf(element) >= 0)
                BaseRemove(element.Name);
        }

        /// <summary>
        /// Vacía la colección
        /// </summary>
        public void Clear()
        {
            BaseClear();
            // Add custom code here.
        }

        /// <summary>
        /// Agrega un elemento a la colección
        /// </summary>
        /// <param name="element">elemento a agregar</param>
        public void Add(PerformanceCounterCategoryElement element)
        {
            BaseAdd(element);
            // Add custom code here.
        }

        /// <summary>
        /// Obtiene la posición del elemento en la colección
        /// </summary>
        /// <param name="element">Elemento</param>
        /// <returns>Posición del elemento</returns>
        public int IndexOf(PerformanceCounterCategoryElement element)
        {
            return BaseIndexOf(element);
        }

        /// <summary>
        /// Crea un nuevo elemento y lo agrega a la colección
        /// </summary>
        /// <returns>Nuevo elemento</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new PerformanceCounterCategoryElement();
        }

        /// <summary>
        /// Crea un nuevo elemento de nombre <paramref name="elementName"/>
        /// </summary>
        /// <param name="elementName">Nombre del elemento</param>
        /// <returns>Nuevo elemento</returns>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new PerformanceCounterCategoryElement(elementName);
        }

        /// <summary>
        /// Obtiene la clave del elemento en la colección
        /// </summary>
        /// <param name="element">Elemento</param>
        /// <returns>Clave del elemento</returns>
        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((PerformanceCounterCategoryElement)element).Name;
        }

        /// <summary>
        /// Agrega un nuevo elemento a la colección
        /// </summary>
        /// <param name="element">Elemento a agregar</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        } 

        #endregion methods

    }
}
