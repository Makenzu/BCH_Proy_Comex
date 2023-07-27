using BCH.Comex.Common.Caching;
using BCH.Comex.Common.Caching.CacheManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Common.Tests
{
    /// <summary>
    ///Clase de Test para LocalCacheManagerTest. Se utiliza lock en todos los TestMethods
    ///para evitar que se ejecuten en paralelo
    ///</summary>
    [TestClass()]
    public class MemoryCacheManagerTest
    {

        #region methods

        /// <summary>
        /// Deshace las modificaciones hechas por el webtest
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup() 
        {
            lock (typeof(CacheFactory))
            {
                MemoryCacheManager target = new MemoryCacheManager();
                target.Clear();
            }
        }

        /// <summary>
        ///Test para verificar opciones de configuración correctas
        ///</summary>
        [TestMethod]
        public void MemoryCacheManagerConfigTest()
        {
            CachePolicyConfiguration policy = new CachePolicyConfiguration();

            policy.Name = "PolicyX";
            policy.CacheType = CacheType.MemoryCache;

            MemoryCacheManager target = new MemoryCacheManager(policy);

            Assert.AreEqual(100 * 1024 * 1024, target.InnerCache.CacheMemoryLimit);
            Assert.AreEqual(10, target.InnerCache.PhysicalMemoryLimit);
            Assert.AreEqual(new TimeSpan(0, 1, 0), target.InnerCache.PollingInterval);
        }

        /// <summary>
        ///Test para RemoveItem
        ///</summary>
        [TestMethod()]
        public void RemoveItemTest()
        {
            lock (typeof(CacheFactory))
            {
                MemoryCacheManager target = new MemoryCacheManager();
                string key = "claveElemento";
                string value = "valorElemento";

                //intento remuevo un elemento inexistente
                target.RemoveItem(key);

                target.AddItem(key, value, TimeSpan.FromSeconds(3000));
                Assert.IsTrue(target.HasItem(key));

                target.RemoveItem(key);
                Assert.IsFalse(target.HasItem(key));
            }
        }

        /// <summary>
        ///Test para ListItems
        ///</summary>
        [TestMethod()]
        public void ListItemsTest()
        {
            lock (typeof(CacheFactory))
            {
                MemoryCacheManager target = new MemoryCacheManager();
                List<string> expected = new List<string>();
                List<string> actual;

                target.AddItem("clave1", "valor1", TimeSpan.FromSeconds(3000));
                expected.Add("clave1");
                target.AddItem("clave2", "valor2", TimeSpan.FromSeconds(3000));
                expected.Add("clave2");
                target.AddItem("clave3", "valor3", TimeSpan.FromSeconds(3000));
                expected.Add("clave3");
                target.AddItem("clave4", "valor4", TimeSpan.FromSeconds(3000));
                expected.Add("clave4");

                actual = target.ListKeys();

                bool areEqual = actual.Count == expected.Count;
                foreach (string item in actual)
                {
                    areEqual = expected.Contains(item);
                    if (!areEqual)
                        break;
                }

                Assert.IsTrue(areEqual);
            }
        }

        /// <summary>
        ///Test para HasItem
        ///</summary>
        [TestMethod()]
        public void HasItemTest()
        {
            lock (typeof(CacheFactory))
            {
                MemoryCacheManager target = new MemoryCacheManager();
                string key = "claveElemento";
                string value = "valorElemento";

                Assert.IsFalse(target.HasItem(key));
                target.AddItem(key, value, TimeSpan.FromSeconds(3000));
                Assert.IsTrue(target.HasItem(key));
            }
        }

        /// <summary>
        ///Test para GetItem
        ///</summary>
        [TestMethod()]
        public void GetItemTest()
        {
            lock (typeof(CacheFactory))
            {
                MemoryCacheManager target = new MemoryCacheManager();
                string key = "claveElemento";
                string value = "valorElemento";
                object expected = value;
                object actual;

                target.AddItem(key, value, TimeSpan.FromSeconds(3000));

                actual = target.GetItem(key);

                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///Test para Empty
        ///</summary>
        [TestMethod()]
        public void EmptyTest()
        {
            lock (typeof(CacheFactory))
            {
                MemoryCacheManager target = new MemoryCacheManager();

                target.AddItem("clave1", "valor1", TimeSpan.FromSeconds(3000));
                target.AddItem("clave2", "valor2", TimeSpan.FromSeconds(3000));
                target.AddItem("clave3", "valor3", TimeSpan.FromSeconds(3000));
                target.AddItem("clave4", "valor4", TimeSpan.FromSeconds(3000));

                Assert.IsTrue(target.ListKeys().Count == 4);
                target.Clear();

                Assert.IsTrue(target.ListKeys().Count == 0);
            }
        }

        /// <summary>
        ///Test para AddItem
        ///</summary>
        [TestMethod()]
        public void AddItemTest()
        {
            lock (typeof(CacheFactory))
            {
                MemoryCacheManager target = new MemoryCacheManager();
                string key = "clave";
                object value = "valor";
                target.AddItem(key, value, TimeSpan.FromSeconds(3));
                Assert.IsTrue(target.HasItem(key));
            }
        } 

        #endregion methods

    }
}

