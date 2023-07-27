using BCH.Comex.Common.Tracing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace BCH.Comex.Common.Tests
{ 
    /// <summary>
    ///Contiene todos los tests unitario para <see cref="Tracer"/>
    ///</summary>
    [TestClass()]
    public class TracerTest
    {

        
        #region Tests
        /// <summary>
        ///Test para ActiveTraceSourceName
        ///</summary>
        [TestMethod()]
        public void ActiveTraceSourceNameTest()
        {
            using (Tracer target = new Tracer())
            {
                Assert.AreEqual("EventLogTraceSource", target.ActiveTraceSourceName);
            }
        }

        /// <summary>
        ///Test para RemoveFromContext
        ///</summary>
        [TestMethod()]
        public void RemoveFromContextTest()
        {
            using (Tracer target = new Tracer())
            {
                string id = "clave";
                object expected = "valor";
                object actual;

                target.AddToContext(id, expected);
                actual = target.GetFromContext(id);
                Assert.AreEqual(expected, actual);
                target.RemoveFromContext(id);
                Assert.IsNull(target.GetFromContext(id));
            }
        }

        /// <summary>
        ///Test para GetFromContext
        ///</summary>
        [TestMethod()]
        public void GetFromContextTest()
        {
            using (Tracer target = new Tracer())
            {
                string id = "clave";
                object expected = "valor";
                object actual;

                target.AddToContext(id, expected);
                actual = target.GetFromContext(id);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///Test para InitializeTracer
        ///</summary>
        [TestMethod()]
        public void InitializeTracerOperationTest()
        {
            using (Tracer target = new Tracer())
            {
                string operacionLogica = "nombreOperacion";
                target.Initialize(operacionLogica, null, null);
                Assert.IsTrue(target.IsLogicalOperationStart);
            }
        }

        /// <summary>
        ///Test para Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest()
        {
            using (Tracer target = new Tracer())
            { }

            Assert.IsTrue(true); //no lanza excepcion => OK
        }

        /// <summary>
        ///Test para AddToContext
        ///</summary>
        [TestMethod()]
        public void AddToContextTest()
        {
            using (Tracer target = new Tracer())
            {
                string id = "clave";
                object valor = "Valor";
                target.AddToContext(id, valor);

                Assert.AreEqual(valor, target.GetFromContext(id));
            }
        }

        /// <summary>
        ///Test para el contructor de <see cref="Tracer"/>
        ///</summary>
        [TestMethod()]
        public void TracerDefaultConstructorTest()
        {
            using (Tracer target = new Tracer())
            {
                Assert.IsTrue(true);//no lanza excepcion => OK
            }
        }

        /// <summary>
        ///Test para el contructor de <see cref="Tracer"/>
        ///</summary>
        [TestMethod()]
        public void TracerConstructorTest()
        {
            string operacion = "Nombre de la operación";
            using (Tracer target = new Tracer(operacion))
            {
                Assert.IsTrue(true); //no lanza excepcion => OK
            }
        }

        /// <summary>
        ///Test log de <see cref="Tracer"/>
        ///</summary>
        [TestMethod()]
        public void TracerLogTest()
        {
            string operacion = "Nombre de la operación";
            using (Tracer target = new Tracer(operacion))
            {
                target.TraceInformation("info");
                try
                {
                    target.TraceWarning("about to execute dangerous operation");
                    int zero = 0;
                    double value = 4 / zero;
                }
                catch (Exception e) {
                    target.TraceError(e.ToString());
                }
            }
        }

        /// <summary>
        /// Test de uso del componente <see cref="Tracer"/>
        /// </summary>
        [TestMethod()]
        public void TracerUsageTest()
        {
            using (Tracer t = new Tracer("Principal"))
            {
                t.AddToContext("contextoprincipal", "valorcontextoPrincipal");
                t.AddToContext("contextoprincipalASacar", "valorcontextoprincipalASacar");
                
                Method1();
                t.TraceInformation("Método Method1 ejecutado");
                
                Method2();
                t.TraceWarning("Método Method2() no lanzó excepción");

                string contextMessage = t.GetFromContext("contextoprincipal") as string;
                Assert.AreEqual("valorcontextoPrincipal", contextMessage);
                t.RemoveFromContext("contextoprincipalASacar");
                Assert.IsNull(t.GetFromContext("contextoprincipalASacar"));
                
            }
        }
        
       

        
        [TestMethod]
        public void NoTraceLoadTest()
        {
            using (Tracer trace = new Tracer(new TraceSource("NoTraceSource", SourceLevels.All)))
            {
                int i = 0;
                while (i < 5)
                {
                    trace.TraceWrite("Dummy trace message no. {0}.", i);
                    i++;
                }
            }
        }
        
        [TestMethod]
        public void ConsoleTraceTest()
        {
            using (Tracer trace = new Tracer(new TraceSource("ConsoleTraceSource", SourceLevels.All)))
            {
                int i = 0;
                while (i < 5)
                {
                    trace.TraceWrite("Dummy trace message no. {0}.", i);
                    i++;
                }
            }
        }

        [TestMethod]
        public void ETWTraceTest()
        {
            using (Tracer trace = new Tracer("Operacion", "ETWTraceSource"))
            {
                int i = 0;
                while (i < 5)
                {
                    trace.TraceWrite("Dummy trace message no. {0}.", i);
                    i++;
                }
            }
        }

        [TestMethod]
        public void TraceWithAndWithoutParameter()
        {
            using (Tracer trace = new Tracer(new TraceSource("NoTraceSource", SourceLevels.All)))
            {
                trace.TraceError("Dummy trace message");
                trace.TraceWarning("Dummy trace message");
                trace.TraceInformation("Dummy trace message");
                trace.TraceVerbose("Dummy trace message");
                trace.TraceWrite("Dummy trace message");

                trace.TraceError("Dummy trace message no. {0}.", 1);
                trace.TraceWarning("Dummy trace message no. {0}.", 1);
                trace.TraceInformation("Dummy trace message no. {0}.", 1);
                trace.TraceVerbose("Dummy trace message no. {0}.", 1);
                trace.TraceWrite("Dummy trace message no. {0}.", 1);
            }
        }


        
        #endregion


        #region Private Methods

        
        /// <summary>
        /// Método de prueba
        /// </summary>
        private static void Method1()
        {
            using (Tracer t = new Tracer("Primera operación"))
            {
                SharedMethod();
            }
        }

        /// <summary>
        /// Método de prueba
        /// </summary>
        private static void Method2()
        {
            using (Tracer t = new Tracer("Segunda operación"))
            {
                SharedMethod();
            }
        }

        /// <summary>
        /// Método de prueba compartido
        /// </summary>
        private static void SharedMethod()
        {
            using (Tracer t = new Tracer())
            {
                t.AddToContext("MetodoCompartido", "valor MetodoCompartido");
            }
        } 

        #endregion methods

    }
}

