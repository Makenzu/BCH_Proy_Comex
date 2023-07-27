using BCH.Comex.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace BCH.Comex.Common.Caching.CacheManager
{
    /// <summary>
    /// Manejador de cache que utiliza <see cref="System.Runtime.Caching.ObjectCache"/>
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 08/05/2015
    /// Fecha de modificación: 14/05/2015
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        #region fields

        private string Name { get; set; }
        internal MemoryCache InnerCache { get; private set; }
        private TimeSpan DefaultLifetime { get; set; }

        #endregion fields

        #region ctor and finalizers
        
        public MemoryCacheManager()
        {
            DefaultLifetime = TimeSpan.FromSeconds(120);
            InnerCache = MemoryCache.Default;
        }

        public MemoryCacheManager(CachePolicyConfiguration policyConf)
        { 
            Name = policyConf.Name;
            DefaultLifetime = TimeSpan.FromSeconds(policyConf.DefaultLifeTimeSeconds);

            InnerCache = new MemoryCache(policyConf.Name);
        }

        #endregion ctor and finalizers

        #region Properties

        /// <summary>
        /// Obtiene el cache
        /// </summary>
        #endregion properties

        #region ICacheManager Members
        
        /// <summary>
        /// Inserta un nuevo elemento al cache
        /// </summary>
        /// <param name="key">Clave del elemento</param>
        /// <param name="value">Elemento a insertar</param>
        /// <param name="lifetime">Duración de vida del objeto</param>
        public void AddItem(string key, object value, TimeSpan? lifetime = null)
        {
            this.InnerCache.Add(key, value, DateTime.UtcNow.Add(lifetime ?? DefaultLifetime));
        }

        /// <summary>
        /// Inserta o actualiza elemento al cache
        /// </summary>
        /// <param name="key">Clave del elemento</param>
        /// <param name="value">Elemento a insertar</param>
        /// <param name="lifetime">Duración de vida del objeto</param>
        public void Set(string key, object value, TimeSpan? lifetime = null)
        {
            this.InnerCache.Set(key, value, DateTime.UtcNow.Add(lifetime ?? DefaultLifetime));
        }

        /// <summary>
        /// Verifica si el cache tiene un item con clave <paramref name="key"/>
        /// </summary>
        /// <param name="key">Clave del item a buscar</param>
        /// <returns>Retorna true si el item se encuentra en el cache</returns>
        public bool HasItem(string key)
        {
            return (this.InnerCache[key] != null);
        }

        /// <summary>
        /// Obtiene un item del cache
        /// </summary>
        /// <param name="key">Clave del item</param>
        /// <returns>Item obtenido</returns>
        public object GetItem(string key)
        {
            return this.InnerCache[key];
        }

        /// <summary>
        /// Quita un item del cache
        /// </summary>
        /// <param name="key">Clave del item a quitar</param>
        public void RemoveItem(string key)
        {
            this.InnerCache.Remove(key);
        }

        /// <summary>
        /// Renueva el tiempo de vida del item especificado
        /// </summary>
        /// <param name="key">Clave del item</param>
        /// <param name="lifetime">Nuevo tiempo de vida del item en segundos</param>
        /// <returns>Retorna true si se encontro el item en el cache y se pudo completar
        /// la operación correctamente</returns>
        public bool RenewItemLifetime(string key, TimeSpan lifetime)
        {
            object item = InnerCache[key];
            if (item != null)
            {
                AddItem(key, item, lifetime); 
            }

            return (item != null);
        }

        /// <summary>
        /// Obtiene una lista con todos los items del cache
        /// </summary>
        /// <returns>Lista con todos los items del cache</returns>
        public List<string> ListKeys()
        {
            List<string> cachedItemList = new List<string>();
            foreach (KeyValuePair<string, object> entry in this.InnerCache)
            {
                cachedItemList.Add(entry.Key.ToString());
            }

            return cachedItemList;
                        
        }

        /// <summary>
        /// Vacía todo el contenido del cache
        /// </summary>
        public void Clear()
        {
            foreach (KeyValuePair<string, object> entry in this.InnerCache)
            {
                this.InnerCache.Remove(entry.Key.ToString());
            }
        }

        /// <summary>
        /// Intenta obtener un elemento del caché y si no lo encuentra intenta obtenerlo invocando a la funcion pasada por parámetro.
        /// </summary>
        /// <remarks>
        /// Si el item es obtenido llamando a la función, es agregado al caché para obtenerlo desde allí en las futuras invocaciones. 
        /// </remarks>
        /// <param name="key">Clave del item</param>
        /// <param name="load">Función para obtener el elemento en caso de que no se encuentre en cache</param>
        /// <param name="lifetime">Duración de vida del objeto</param>
        /// <returns>Devuelve el item solicitado</returns>
        public T GetOrAdd<T>(string key, Func<T> load, TimeSpan? lifetime = null) where T: class
        {
            T item = this.GetItem(key) as T;

            if (item == null)
            {
                item = load();

                if (item == null)
                    throw new CacheException(Messages.CacheAddOrGetItemFailed, key);
                
                this.AddItem(key, item, lifetime);
            }

            return item;
        }

        #endregion

    }
}
