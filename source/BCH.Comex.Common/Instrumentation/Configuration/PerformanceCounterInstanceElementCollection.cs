using System;
using System.Configuration;

namespace BCH.Comex.Common.Instrumentation.Configuration
{
    /// <summary>
    /// Representa una colección de elementos de instancias de contadores
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 06/05/2015
    /// Fecha de modificación: 06/05/2015
    /// </summary>
    class PerformanceCounterInstanceElementCollection : ConfigurationElementCollection
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
        /// Nombre del la propiedad add
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
        /// Nombre de la propiedad clear
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
        /// Nombre de la propiedad remove
        /// </summary>
        public new string RemoveElementName
        {
            get
            {
                return base.RemoveElementName;
            }
        }

        /// <summary>
        /// Cantidad de elemento de la colección
        /// </summary>
        public new int Count
        {
            get { return base.Count; }
        }

        /// <summary>
        /// Obtiene un elemento según su posición
        /// </summary>
        /// <param name="index">Posición del elemento</param>
        /// <returns>Elemento</returns>
        public PerformanceCounterInstanceElement this[int index]
        {
            get
            {
                return (PerformanceCounterInstanceElement)BaseGet(index);
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
        /// <returns>Elemento</returns>
        new public PerformanceCounterInstanceElement this[string Name]
        {
            get
            {
                return (PerformanceCounterInstanceElement)BaseGet(Name);
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
            return new PerformanceCounterInstanceElement();
        }

        /// <summary>
        /// Crea un nuevo elemento de nombre <paramref name="elementName"/>
        /// </summary>
        /// <param name="elementName">Nombre del elemento</param>
        /// <returns>Nuevo elemento</returns>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new PerformanceCounterInstanceElement(elementName);
        }

        /// <summary>
        /// Obtiene la clave asociada al elemento en la colección
        /// </summary>
        /// <param name="element">Elemento</param>
        /// <returns>Clave asociada a <paramref name="element"/></returns>
        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((PerformanceCounterInstanceElement)element).Name;
        }

        /// <summary>
        /// Obtiene la posición del elemento en la colección
        /// </summary>
        /// <param name="element">Elemento</param>
        /// <returns>Posición del elemento</returns>
        public int IndexOf(PerformanceCounterInstanceElement element)
        {
            return BaseIndexOf(element);
        }

        /// <summary>
        /// Agrega el elemento <paramref name="element"/> a la colección
        /// </summary>
        /// <param name="element">Elemento</param>
        public void Add(PerformanceCounterInstanceElement element)
        {
            BaseAdd(element);
            // Add custom code here.
        }

        /// <summary>
        /// Agrega el elemento <paramref name="element"/> a la colección
        /// </summary>
        /// <param name="element">Elemento</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        /// <summary>
        /// Remueve el elemento <paramref name="element"/> de la colección
        /// </summary>
        /// <param name="element">Elemento</param>
        public void Remove(PerformanceCounterInstanceElement element)
        {
            if (BaseIndexOf(element) >= 0)
                BaseRemove(element.Name);
        }

        /// <summary>
        /// Remueve un elemento según su posición en la colección
        /// </summary>
        /// <param name="index">Posición del elemento</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Remueve un elemento según su nombre
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
            // Add custom code here.
        } 

        #endregion methods

    }
}
