﻿using System;
using System.Configuration;

namespace BCH.Comex.Common.Caching.Configuration
{
    /// <summary>
    /// Representa la colección de <see cref="CachePolicyElement"/>.
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 08/05/2015
    /// Fecha de modificación: 08/05/2015
    /// </summary>
    internal class CachePolicyElementCollection : ConfigurationElementCollection
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
        /// Nombre del elemento de agregado
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
        /// Nombre del elemento de limpieza
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
        /// Nombre del elemento de eliminado
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
        /// Inidizador de elementos de la colección
        /// </summary>
        /// <param name="index">Indice del elemento</param>
        /// <returns>Elemento de la colección</returns>
        public CachePolicyElement this[int index]
        {
            get
            {
                return (CachePolicyElement)BaseGet(index);
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
        /// Inidizador de elementos de la colección
        /// </summary>
        /// <param name="name">Nombre del elemento</param>
        /// <returns>Elemento de la colección</returns>
        new public CachePolicyElement this[string name]
        {
            get
            {
                return (CachePolicyElement)BaseGet(name);
            }
        } 

        #endregion 

        #region methods
        /// <summary>
        /// Retorna el índice de un elemento
        /// </summary>
        /// <param name="policy">Elemento</param>
        /// <returns>Índice</returns>
        public int IndexOf(CachePolicyElement policy)
        {
            return BaseIndexOf(policy);
        }

        /// <summary>
        /// Agrega un elemento
        /// </summary>
        /// <param name="policy">Elemento</param>
        public void Add(CachePolicyElement policy)
        {
            BaseAdd(policy);
        }

        /// <summary>
        /// Remueve un elemento de la colección
        /// </summary>
        /// <param name="policy">Elemento</param>
        public void Remove(CachePolicyElement policy)
        {
            if (BaseIndexOf(policy) >= 0)
                BaseRemove(policy.Name);
        }

        /// <summary>
        /// Remueve un elemento en una posicicón de la colección 
        /// </summary>
        /// <param name="index">Índice</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Remueve un elemento de la colección a partir de su nombre
        /// </summary>
        /// <param name="name"></param>
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

        /// <summary>
        /// Crea un nuevo elemento 
        /// </summary>
        /// <returns>Nuevo elemento</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new CachePolicyElement();
        }

        /// <summary>
        /// Crea un nuevo elemento a partir de un nombre
        /// </summary>
        /// <param name="elementName">Nombre del elemento</param>
        /// <returns>Nuevo elemento</returns>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new CachePolicyElement(elementName);
        }

        /// <summary>
        /// Recupera la clave del elemento en la coleción
        /// </summary>
        /// <param name="element">Elemento</param>
        /// <returns>Clave</returns>
        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((CachePolicyElement)element).Name;
        }

        /// <summary>
        /// Agrega un elemento en la colección base 
        /// </summary>
        /// <param name="element">Elemento</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        } 

        #endregion methods

    }
}
