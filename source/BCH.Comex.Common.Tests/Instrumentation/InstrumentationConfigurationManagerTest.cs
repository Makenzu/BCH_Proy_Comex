using BCH.Comex.Common.Instrumentation;
using BCH.Comex.Common.Instrumentation.Counter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace BCH.Comex.Common.Tests
{
    /// <summary>
    /// Clase que contiene todos los tests unitario para <see cref="InstrumentationConfigurationManager"/>
    /// Autor: Microsoft Consulting Services  
    /// Fecha de creación: 01/06/2015
    /// Fecha de modificación: 07/09/2010
    ///</summary>
    [TestClass()]
    public class InstrumentationConfigurationManagerTest
    {
        #region fields

        private TestContext testContextInstance; 

        #endregion fields

        #region properties

        /// <summary>
        ///Contexto del test
        ///</summary>
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

        #endregion properties

        #region methods

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            InstrumentationProvider.UninstallAllApplicationCounters();
        }

        /// <summary>
        /// Deshace los cambios de los tests
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup()
        {
            lock (typeof(InstrumentationProvider))
            {
                InstrumentationProvider.Reset();
                PerformanceCounterContainer.ClearCategoryDataList();
                InstrumentationConfigurationManager.LoadConfiguration();
            }
        }

        /// <summary>
        ///Test para InstallPerformanceCounterCategory y UninstallPerformanceCounterCategory
        ///</summary>
        [TestMethod()]
        public void InstallUninstallPerformanceCounterCategoryTest()
        {
            lock (typeof(InstrumentationProvider))
            {
                CounterCategoryData category = new CounterCategoryData()
                {
                    IsActive = true,
                    Description = "",
                    Name = "Categoría de prueba",
                    Type = PerformanceCounterCategoryType.Unknown
                };
                InstrumentationConfigurationManager.InstallPerformanceCounterCategory(category);
                InstrumentationConfigurationManager.UninstallPerformanceCounterCategory(category);
                Assert.IsTrue(true); //si no lanza excepción, OK
            }
        }


        //[TestMethod]
        public void InstallAsyncCounters()
        {
            lock (typeof(InstrumentationProvider))
            {
                CounterCategoryData category = new CounterCategoryData()
                {
                    IsActive = true,
                    Description = "",
                    Name = "BCH.Comex.Common.AsyncLogging",
                    Type = PerformanceCounterCategoryType.Unknown
                };

                InstrumentationConfigurationManager.InstallPerformanceCounterCategory(category);

                category = new CounterCategoryData()
                {
                    IsActive = true,
                    Description = "",
                    Name = "BCH.Comex.Common",
                    Type = PerformanceCounterCategoryType.Unknown
                };

                InstrumentationConfigurationManager.InstallPerformanceCounterCategory(category);

            }
        }


        /// <summary>
        ///Test para LoadConfiguration
        ///</summary>
        [TestMethod()]
        public void LoadConfigurationTest()
        {
            lock (typeof(InstrumentationProvider))
            {
                string categoryName = "CategoriaInstrumentationConfigurationManagerTest";
                string counterName = "Contador2";
                string instanceName = "InstanciaContador2";

                PerformanceCounterContainer.ClearCategoryDataList();
                InstrumentationConfigurationManager.LoadConfiguration();
                CounterInstanceData instance = PerformanceCounterContainer.GetPerformanceCounterInstance(
                    categoryName, counterName, instanceName);

                Assert.IsNotNull(instance);
            }
        } 

        #endregion methods

    }
}
