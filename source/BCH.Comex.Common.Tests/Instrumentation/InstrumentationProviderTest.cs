using BCH.Comex.Common.Instrumentation;
using BCH.Comex.Common.Instrumentation.Counter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace BCHComex.Common.Tests
{
    
    
    /// <summary>
    ///Clase de test que contiene a todos los test para <see cref="InstrumentationProvider"/>
    /// Autor: Microsoft Consulting Services
    /// Fecha de creación: 01/06/2015
    /// Fecha de modificación: 01/06/2015
    ///</summary>
    [TestClass()]
    public class InstrumentationProviderTest
    {
        #region fields

        private TestContext testContextInstance; 

        #endregion fields

        #region ctor and finalizers

        #region Additional test attributes

        /// <summary>
        /// Desinstala los contadores instalados en las corridas previas del test
        /// </summary>
        /// <param name="testContext">contexto del test</param>
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

        #endregion 

        #endregion ctor and finalizers

        #region properties

        /// <summary>
        ///Información de contexto para el Test
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

        /// <summary>
        ///Test que ejecuta todos los test individuales dandoles un orden.
        ///</summary>
        [TestMethod()]
        public void AllInstrumentationProviderTests()
        {
            AddHasRemoveCounterTest();
            AddHasRemoveCounterInstanceTest();
            Usage();
            RegisterTimeTest();
            IncreaseCounterTest1();
            IncreaseCounterTest();
            DecreaseCounterTest();
        }

        /// <summary>
        ///Test para RemoveCounterInstance
        ///</summary>
        //[TestMethod()]
        public void AddHasRemoveCounterInstanceTest()
        {
            lock (typeof(InstrumentationProvider))
            {
                string categoryName = "CategoriaInstrumentationProvider";
                string counterName = "ContadorNumberOfItems";
                string instanceName = "InstanciaAddHasRemoveCounterInstanceTest";

                Assert.IsFalse(InstrumentationProvider.HasCounterInstance(categoryName, counterName,
                    instanceName));

                if (!InstrumentationProvider.HasCounterInstance(categoryName, counterName, instanceName))
                    InstrumentationProvider.AddCounterInstance(categoryName, counterName, instanceName);

                Assert.IsTrue(InstrumentationProvider.HasCounterInstance(categoryName, counterName,
                    instanceName));

                InstrumentationProvider.RemoveCounterInstance(categoryName, counterName, instanceName);

                Assert.IsFalse(InstrumentationProvider.HasCounterInstance(categoryName, counterName,
                    instanceName));
            }
        }

        /// <summary>
        ///Test para RemoveCounter
        ///</summary>
        //[TestMethod()]
        public void AddHasRemoveCounterTest()
        {
            lock (typeof(InstrumentationProvider))
            {
                string categoryName = "CategoriaInstrumentationProvider";
                string counterName = "AddHasRemoveCounterTest";

                Assert.IsFalse(InstrumentationProvider.HasCounter(categoryName, counterName));

                if (!InstrumentationProvider.HasCounter(categoryName, counterName))
                    InstrumentationProvider.AddCounter(categoryName, counterName, "desc",
                        BCHComexPerformanceCounterType.NumberOfItems);

                Assert.IsTrue(InstrumentationProvider.HasCounter(categoryName, counterName));

                InstrumentationProvider.RemoveCounter(categoryName, counterName);

                Assert.IsFalse(InstrumentationProvider.HasCounter(categoryName, counterName));
            }
        }

        ///<summary>
        /// Testea el uso del componente
        ///</summary>
        [TestMethod()]
        public void Usage()
        {
            lock (typeof(InstrumentationProvider))
            {
                string categoryName = "CategoriaInstrumentationProvider";

                InstrumentationProvider.AddCounter(categoryName, "ContadorPruebaUsage", "desc",
                    BCHComexPerformanceCounterType.RateOfCountsPerSecond);
                InstrumentationProvider.AddCounterInstance(categoryName, "ContadorPruebaUsage",
                    "InstanciaContadorPruebaUsage");

                InstrumentationProvider.Initialize();
                //definidos en .config
                InstrumentationProvider.IncreaseCounter(categoryName,
                    "ContadorAverageTimer", "InstanciaUsageContadorAverageTimer");
                InstrumentationProvider.IncreaseCounter(categoryName,
                    "ContadorAverageTimer", "InstanciaUsage2ContadorAverageTimer");
                InstrumentationProvider.IncreaseCounter(categoryName,
                    "ContadorNumberOfItems", "InstanciaUsageContadorNumberOfItems");
                DateTime startTime = DateTime.Now;
                Thread.Sleep(200);
                InstrumentationProvider.RegisterTime(categoryName,
                    "ContadorAverageTimer", "InstanciaUsage2ContadorAverageTimer",
                    startTime);

                //definidos en código
                InstrumentationProvider.IncreaseCounter(categoryName, "ContadorPruebaUsage",
                    "InstanciaContadorPruebaUsage");
            }
        }

        /// <summary>
        ///Test para RegisterTime
        ///</summary>
        [TestMethod()]
        public void RegisterTimeTest()
        {
            lock (typeof(InstrumentationProvider))
            {
                string categoryName = "CategoriaInstrumentationProvider";
                string counterName = "ContadorAverageTimer";
                string instanceName = "InstanciaRegisterTimeTestContadorAverageTimer";
                DateTime startTime = DateTime.Now;

                InstrumentationProvider.Initialize();

                Thread.Sleep(200);

                InstrumentationProvider.RegisterTime(categoryName, counterName, instanceName, startTime);

                CounterInstanceData instance =
                    PerformanceCounterContainer.GetPerformanceCounterInstance(
                    categoryName, counterName, instanceName);

                long value = instance.RealCounterBase.RawValue;
                
                InstrumentationProvider.RegisterTime(categoryName, counterName, instanceName, startTime);

                Assert.AreEqual(value + 1, instance.RealCounterBase.RawValue);

            }
        }

        /// <summary>
        ///Test para IncreaseCounter
        ///</summary>
        //[TestMethod()]
        public void IncreaseCounterTest1()
        {
            lock (typeof(InstrumentationProvider))
            {
                string categoryName = "CategoriaInstrumentationProvider";
                string counterName = "ContadorNumberOfItems";
                string instanceName = "InstanciaIncreaseCounterTest1ContadorNumberOfItems";

                InstrumentationProvider.Initialize();

                InstrumentationProvider.IncreaseCounter(categoryName, counterName, instanceName);
                CounterInstanceData instance =
                     PerformanceCounterContainer.GetPerformanceCounterInstance(
                     categoryName, counterName, instanceName);

                Assert.IsTrue(instance.RealCounter.RawValue == 1);

                InstrumentationProvider.IncreaseCounter(categoryName, counterName, instanceName);

                Assert.IsTrue(instance.RealCounter.RawValue == 2);
            }
        }

        /// <summary>
        /// Test para IncreaseCounter
        /// </summary>
        //[TestMethod()]
        public void IncreaseCounterTest()
        {
            lock (typeof(InstrumentationProvider))
            {
                string categoryName = "CategoriaInstrumentationProvider";
                string counterName = "ContadorNumberOfItems";
                string instanceName = "InstanciaIncreaseCounterTestContadorNumberOfItems";
                long value = 500;

                InstrumentationProvider.Initialize();

                InstrumentationProvider.IncreaseCounter(categoryName, counterName, instanceName, value);
                CounterInstanceData instance =
                     PerformanceCounterContainer.GetPerformanceCounterInstance(
                     categoryName, counterName, instanceName);

                Assert.IsTrue(instance.RealCounter.RawValue == value);

                InstrumentationProvider.IncreaseCounter(categoryName, counterName, instanceName, value);

                Assert.IsTrue(instance.RealCounter.RawValue == 2 * value);
            }
        }

        /// <summary>
        /// Test para DecreaseCounter
        ///</summary>
        [TestMethod()]
        public void DecreaseCounterTest()
        {
            lock (typeof(InstrumentationProvider))
            {
                string categoryName = "CategoriaInstrumentationProvider";
                string counterName = "ContadorNumberOfItems";
                string instanceName = "InstanciaDecreaseCounterTestContadorNumberOfItems";
                long value = 500;

                InstrumentationProvider.Initialize();

                InstrumentationProvider.IncreaseCounter(categoryName, counterName, instanceName, value);
                CounterInstanceData instance =
                     PerformanceCounterContainer.GetPerformanceCounterInstance(
                     categoryName, counterName, instanceName);

                Assert.AreEqual(value, instance.RealCounter.RawValue);

                InstrumentationProvider.DecreaseCounter(categoryName, counterName, instanceName);

                Assert.AreEqual(value -1, instance.RealCounter.RawValue);
            }
        } 

        #endregion methods


    }
}
