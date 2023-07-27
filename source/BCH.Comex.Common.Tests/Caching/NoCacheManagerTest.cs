using BCH.Comex.Common.Caching;
using BCH.Comex.Common.Caching.CacheManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Common.Test
{
    /// <summary>
    ///Clase de Test para NoCacheManagerTest. 
    ///Autor: Pablo Bertón
    ///Creación: 25/05/2015
    [TestClass()]
    public class NoCacheManagerTest
    {

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        /// Deshace las modificaciones hechas por el webtest
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup()
        {
            lock (typeof(CacheFactory))
            {
                NoCacheManager target = new NoCacheManager();
                target.Clear();
            }
        }

        /// <summary>
        /// Test para AddItem
        ///</summary>
        [TestMethod()]
        public void AddItemTest()
        {
            NoCacheManager target = new NoCacheManager(); 
            string key = "claveElemento";
            string value = "valorElemento";

            target.AddItem(key, value);
            Assert.IsFalse(target.HasItem(key));
        }

        /// <summary>
        /// Test para AddItem con lifetime
        ///</summary>
        [TestMethod()]
        public void AddItemTest1()
        {
            NoCacheManager target = new NoCacheManager(); 
            string key = "claveElemento";
            string value = "valorElemento";
            int lifetime = 100;
            target.AddItem(key, value, TimeSpan.FromSeconds(lifetime));
            Assert.IsFalse(target.HasItem(key));

        }

        
        /// <summary>
        /// Test para GetItem
        ///</summary>
        [TestMethod()]
        public void GetItemTest()
        {
            lock (typeof(CacheFactory))
            {
                NoCacheManager target = new NoCacheManager();
                string key = "claveElemento";
                string value = "valorElemento";
                object expected = value;
                object actual;

                target.AddItem(key, value, TimeSpan.FromSeconds(30000));

                actual = target.GetItem(key);

                Assert.IsNull(actual);
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
                NoCacheManager target = new NoCacheManager();
                string key = "claveElemento";
                string value = "valorElemento";

                Assert.IsFalse(target.HasItem(key));
                target.AddItem(key, value, TimeSpan.FromSeconds(30000));
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
                NoCacheManager target = new NoCacheManager();
                List<string> actual;

                target.AddItem("clave1", "valor1", TimeSpan.FromSeconds(30000));
                target.AddItem("clave2", "valor2", TimeSpan.FromSeconds(30000));
                target.AddItem("clave3", "valor3", TimeSpan.FromSeconds(30000));
                target.AddItem("clave4", "valor4", TimeSpan.FromSeconds(30000));

                actual = target.ListKeys();

                Assert.IsNull(actual);
            }
        }

        /// <summary>
        ///Test para RemoveItem
        ///</summary>
        [TestMethod()]
        public void RemoveItemTest()
        {
            lock (typeof(CacheFactory))
            {
                NoCacheManager target = new NoCacheManager();
                string key = "claveElemento";
                string value = "valorElemento";

                //intento remuevo un elemento inexistente
                target.RemoveItem(key);

                target.AddItem(key, value, TimeSpan.FromSeconds(30000));
                Assert.IsFalse(target.HasItem(key));

                target.RemoveItem(key);
                Assert.IsFalse(target.HasItem(key));
            }
        }


        /// <summary>
        ///Test para RenewItemLifetime
        ///</summary>
        [TestMethod()]
        public void RenewItemLifetimeTest()
        {
            lock (typeof(CacheFactory))
            {
                NoCacheManager target = new NoCacheManager();
                string key = "claveElemento";
                string value = "valorElemento";
                bool expected;

                target.AddItem(key, value, TimeSpan.FromSeconds(300));
                Assert.IsFalse(target.HasItem(key));

                expected = target.RenewItemLifetime(key, TimeSpan.FromSeconds(100));
                Assert.IsFalse(expected);

            }
        }
    }
}
