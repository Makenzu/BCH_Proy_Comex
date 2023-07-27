using BCH.Comex.Common.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BCH.Comex.Common.Test
{
    /// <summary>
    ///Clase de Test para CacheException.
    ///Autor: Pablo Bertón
    ///Creación: 25/05/2015
    ///</summary>
    [TestClass()]
    public class CacheExceptionTest
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
        ///Test para el constructor de CacheException 
        ///</summary>
        [TestMethod()]
        public void CacheExceptionConstructorTest1()
        {
            Exception innerException = new ArgumentNullException();
            string mensaje = "Ocurrió un error en el bloque de {0}";
            object[] args = { "Caching" }; 
            CacheException target = new CacheException(innerException, mensaje, args);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///Test para CacheException Constructor
        ///</summary>
        [TestMethod()]
        public void CacheExceptionConstructorTest2()
        {
            string mensaje = "Ocurrió un error en el bloque de {0}";
            object[] args = { "Caching" };
            CacheException target = new CacheException(mensaje, args);
            Assert.IsNotNull(target);
        }
    }
}
