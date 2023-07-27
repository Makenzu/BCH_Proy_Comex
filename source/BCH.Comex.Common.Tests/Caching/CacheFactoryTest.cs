using BCH.Comex.Common.Caching;
using BCH.Comex.Common.Caching.CacheManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BCH.Comex.Common.Test
{
    /// <summary>
    ///Clase de Test para CacheFactoryTest.
    ///Autor: Pablo Bertón
    ///Creación: 25/05/2015
    ///</summary>
    [TestClass()]
    public class CacheFactoryTest
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
        ///Test para GetCache pasando como parámetro el nombre de la política
        ///</summary>
        [TestMethod()]
        public void GetCacheTest()
        {
            //Verificar tipo devuelto por el Accessor
           lock (typeof(CacheFactory))
            {
                string policyName = "PolicyX";
                Type expected = typeof(MemoryCacheManager);
                Type actual;
                actual = CacheFactory.GetCache(policyName).GetType();
                Assert.AreEqual(expected, actual);
            }
            
        }

        /// <summary>
        ///Test para GetCache que obtiene la policy por Default
        ///</summary>
        [TestMethod()]
        public void GetDefaultCacheTest()
        {
            //Verificar tipo devuelto por el Accessor
            lock (typeof(CacheFactory))
            {
                Type expected = typeof(MemoryCacheManager);
                Type actual;
                actual = CacheFactory.GetCache().GetType();
                Assert.AreEqual(expected, actual);
            }  
        }
    }
}
