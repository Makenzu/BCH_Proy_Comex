using System;
using System.Configuration;

namespace BCH.Comex.Common.ExceptionHandling.Configuration
{
    /// <summary>
    /// Configuración para una colección de tipos.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 03/05/2015
    /// Fecha de modificación: 03/05/2015
    /// </summary>
    public class ExceptionTypeElementCollection : ConfigurationElementCollection
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
        /// Nombre del elemento para agregar
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
        /// Nombre del elemento para limpiar la colección
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
        /// Nombre del elemento para remover 
        /// </summary>
        public new string RemoveElementName
        {
            get
            {
                return base.RemoveElementName;
            }
        }

        /// <summary>
        /// Cantidad de elementos
        /// </summary>
        public new int Count
        {
            get { return base.Count; }
        }
        #endregion 

        #region indexers

        /// <summary>
        /// Indizador de acceso a la colección
        /// </summary>
        /// <param name="index">Indice del elemento</param>
        /// <returns>Elemento</returns>
        public ExceptionTypeElement this[int index]
        {
            get
            {
                return (ExceptionTypeElement)BaseGet(index);
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
        /// Indizador de acceso a la colección
        /// </summary>
        /// <param name="name">Nombre del elemento</param>
        /// <returns>Elemento</returns>
        new public ExceptionTypeElement this[string name]
        {
            get
            {
                return (ExceptionTypeElement)BaseGet(name);
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Creación de un nuevo elemento 
        /// </summary>
        /// <returns>Nuevo elemento</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ExceptionTypeElement();
        }

        /// <summary>
        /// Creación de un nuevo elemento
        /// </summary>
        /// <param name="elementName">Nombew del elemento</param>
        /// <returns>Nuevo elemento</returns>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new ExceptionTypeElement(elementName);
        }

        /// <summary>
        /// Recuperación de la clave del elemento
        /// </summary>
        /// <param name="element">Elemento</param>
        /// <returns>Clave</returns>
        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((ExceptionTypeElement)element).Name;
        }

        /// <summary>
        /// Retorna el índice de un elemento
        /// </summary>
        /// <param name="policy">Elemento</param>
        /// <returns>Índice en la coleción</returns>
        public int IndexOf(ExceptionTypeElement policy)
        {
            return BaseIndexOf(policy);
        }

        /// <summary>
        /// Agregado de un elemento
        /// </summary>
        /// <param name="policy">Elemento</param>
        public void Add(ExceptionTypeElement policy)
        {
            BaseAdd(policy);
        }

        /// <summary>
        /// Agregado de un elemento
        /// </summary>
        /// <param name="element">Elemento</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Remueve un elemento de la colección
        /// </summary>
        /// <param name="policy">Elemento</param>
        public void Remove(ExceptionTypeElement policy)
        {
            if (BaseIndexOf(policy) >= 0)
                BaseRemove(policy.Name);
        }

        /// <summary>
        /// Remueve un elemento de la colección
        /// </summary>
        /// <param name="index">Índice del elemento</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Remueve un elmento de la colección
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
        #endregion
    }
}
