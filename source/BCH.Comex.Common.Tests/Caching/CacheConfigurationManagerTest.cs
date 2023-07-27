using BCH.Comex.Common.Caching;
using BCH.Comex.Common.Caching.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace BCH.Comex.Common.Test
{
    /// <summary>
    ///Clase de Test para CacheConfigurationManager.
    ///Autor: Pablo Bertón
    ///Creación: 25/05/2015
    ///</summary>
    [TestClass()]
    public class CacheConfigurationManagerTest
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
        ///Test para CargarConfiguracionPolitica
        ///</summary>
        [TestMethod()]
        public void CargarConfiguracionPoliticaTest()
        {
            CachingSection section =
                ConfigurationManager.GetSection("cachingSection") as CachingSection;

            Assert.IsTrue(section.PolicyCollection[0].Name == "PolicyX");
            Assert.IsTrue(section.PolicyCollection[0].CacheType == CacheType.MemoryCache);
            Assert.IsTrue(section.PolicyCollection[1].Name == "PolicyY");
            Assert.IsTrue(section.PolicyCollection[1].CacheType == CacheType.MemoryCache);
            Assert.IsTrue(section.PolicyCollection[2].Name == "PolicyZ");
            Assert.IsTrue(section.PolicyCollection[2].CacheType == CacheType.NoCache);
            Assert.AreEqual(5, section.PolicyCollection.Count);
        }
    }
}
