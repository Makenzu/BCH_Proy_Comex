using System;
using System.Configuration;

namespace BCH.Comex.Common.Instrumentation.Configuration
{
    /// <summary>
    /// Colección de elementos de configuración de contadores de performance
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    class PerformanceCounterElementCollection : ConfigurationElementCollection
    {
        #region properties

        /// <summary>
        /// Tipo de colección
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
        /// Cantidad de elementos de la colección
        /// </summary>
        public new int Count
        {
            get { return base.Count; }
        }

        /// <summary>
        /// Retorna el elemento según su posición en la colección
        /// </summary>
        /// <param name="index">Posición del elemento</param>
        /// <returns>Elemento</returns>
        public PerformanceCounterElement this[int index]
        {
            get
            {
                return (PerformanceCounterElement)BaseGet(index);
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
        /// Retorna el elemento según su nombre
        /// </summary>
        /// <param name="Name">Nombre del elemento</param>
        /// <returns>Elemento</returns>
        new public PerformanceCounterElement this[string Name]
        {
            get
            {
                return (PerformanceCounterElement)BaseGet(Name);
            }
        } 

        #endregion properties

        #region methods

        /// <summary>
        /// Crea un nuevo elemento en la colección
        /// </summary>
        /// <returns>Nuevo elemento</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new PerformanceCounterElement();
        }

        /// <summary>
        /// Crea un nuevo elemento de nombre <paramref name="elementName"/>
        /// </summary>
        /// <param name="elementName">Nombre del elemento</param>
        /// <returns>Nuevo elemento</returns>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new PerformanceCounterElement(elementName);
        }

        /// <summary>
        /// Retorna la clave asociada al elemento en la colección
        /// </summary>
        /// <param name="element">Elemento</param>
        /// <returns>Clave asociada al elemento</returns>
        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((PerformanceCounterElement)element).Name;
        }

        /// <summary>
        /// Obtiene la posición del elemento en la colección
        /// </summary>
        /// <param name="element">Nombre del elemento</param>
        /// <returns>Posición del elemento</returns>
        public int IndexOf(PerformanceCounterElement element)
        {
            return BaseIndexOf(element);
        }

        /// <summary>
        /// Agrega un elemento a la colección
        /// </summary>
        /// <param name="element">Elemento</param>
        public void Add(PerformanceCounterElement element)
        {
            BaseAdd(element);
            // Add custom code here.
        }

        /// <summary>
        /// Agrega un nuevo elemento a la colección
        /// </summary>
        /// <param name="element">Elemento</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Remueve un elemento de la colección
        /// </summary>
        /// <param name="element">Elemento</param>
        public void Remove(PerformanceCounterElement element)
        {
            if (BaseIndexOf(element) >= 0)
                BaseRemove(element.Name);
        }

        /// <summary>
        /// Remueve un elemento de la colección según su posición
        /// </summary>
        /// <param name="index">Posición del elemento</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Remueve un elemento de la colección según su nombre
        /// </summary>
        /// <param name="name">Nombre del elemento</param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }

        /// <summary>
        /// Vacía la colección
        /// </summary>
        public void Clear()
        {
            BaseClear();
        } 

        #endregion methods

    }
}
