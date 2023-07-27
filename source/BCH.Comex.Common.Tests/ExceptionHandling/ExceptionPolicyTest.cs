using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.ExceptionHandling.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace BCH.Comex.Common.Tests
{

    /// <summary>
    ///Contiene los tests unitarios para ExceptionPolicy
    ///</summary>
    [TestClass()]
    public class ExceptionPolicyTest
    {

        /// <summary>
        ///Test para CargarConfiguracionPolitica
        ///</summary>
        [TestMethod()]
        public void CargarConfiguracionPoliticaTest()
        {
            
            ExceptionHandlingSection section =
                ConfigurationManager.GetSection("exceptionHandlingSection") as ExceptionHandlingSection;

            Assert.IsTrue(section.PolicyCollection[0].Name == "PoliticaRemplazar");
            Assert.IsTrue(section.PolicyCollection[1].Name == "PoliticaEnvolver");
            Assert.AreEqual(section.PolicyCollection[1].ExceptionTypeCollection[1].NewExceptionType,
                typeof(NullReferenceException));
             
        }

        /// <summary>
        ///Test para RemplazarExcepcion
        ///</summary>
        [TestMethod()]
        public void RemplazarExcepcionTest()
        {
            Exception exception = new DivideByZeroException();
            ExceptionPolicyConfiguration exPolicyConf = new ExceptionPolicyConfiguration();
            exPolicyConf.ExceptionType = exception.GetType();
            exPolicyConf.Action = HandlingAction.Replace;
            exPolicyConf.Name = "DivideByZeroExceptionType";
            exPolicyConf.NewExceptionMessage = "nuevo mensaje";
            exPolicyConf.NewExceptionType = typeof(NullReferenceException);
            try
            {
                ExceptionPolicy.ReplaceException(exception, exPolicyConf);
            }
            catch (NullReferenceException ex)
            {
                Assert.IsTrue(ex.InnerException == null &&
                    ex.Message == exPolicyConf.NewExceptionMessage);
            }
        }

        /// <summary>
        /// Test para WrapException
        /// </summary>
        [TestMethod()]
        public void WrapExcepcionTest()
        {
            Exception exception = new DivideByZeroException();
            ExceptionPolicyConfiguration confPoliticaExcepcion = new ExceptionPolicyConfiguration();
            confPoliticaExcepcion.ExceptionType = exception.GetType();
            confPoliticaExcepcion.Action = HandlingAction.Wrap;
            confPoliticaExcepcion.Name = "DivideByZeroExceptionType";
            confPoliticaExcepcion.NewExceptionMessage = "nuevo mensaje";
            confPoliticaExcepcion.NewExceptionType = typeof(NullReferenceException);
            try
            {
                ExceptionPolicy.WrapException(exception, confPoliticaExcepcion);
            }
            catch (NullReferenceException ex)
            {
                Assert.IsTrue(ex.InnerException == exception &&
                    ex.Message == confPoliticaExcepcion.NewExceptionMessage);
            }
        }

        /// <summary>
        /// Test para política del tipo Replace
        /// </summary>
        [TestMethod]
        public void PolicyReplace1()
        {
            Exception exception = new DivideByZeroException();
            string policyName = "PoliticaRemplazar";
            try
            {
                ExceptionPolicy.HandleException(exception, policyName);
            }
            catch (NullReferenceException nrex)
            {
                Assert.IsTrue(nrex.InnerException == null &&
                    nrex.Message == "El valor de la nueva excepcion en vez de DivideByZeroException!");
            }
        }

        /// <summary>
        /// Test para política del tipo Replace
        /// </summary>
        [TestMethod]
        public void PolicyReplace2()
        {
            Exception exception = new Exception();
            string policyName = "PoliticaRemplazar";
            try
            {
                ExceptionPolicy.HandleException(exception, policyName);
            }
            catch (ApplicationException ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(ApplicationException) &&
                    ex.InnerException == null &&
                    ex.Message == "El valor de la nueva excepcion!");
            }
        }

        /// <summary>
        /// Test para política del tipo Wrap
        /// </summary>
        [TestMethod]
        public void PolicyWrap()
        {
            try
            {
                ExceptionPolicy.HandleException(new DivideByZeroException(),
                    "PoliticaEnvolver");
            }
            catch (NullReferenceException nrex)
            {
                Assert.IsTrue(nrex.InnerException.GetType() == typeof(DivideByZeroException) &&
                    nrex.Message == "El valor de la nueva excepcion en vez de DivideByZeroException!");
            }
        }

        /// <summary>
        /// Test para política del tipo Rethrow
        /// </summary>
        [TestMethod]
        public void PolicyRethrow()
        {
            bool actual = ExceptionPolicy.HandleException(new DivideByZeroException(),
                "PoliticaRelanzar/Nada");
            Assert.AreEqual(true, actual);
        }

        /// <summary>
        /// Test para política Default
        /// </summary>
        [TestMethod]
        public void PolicyDefault()
        {
            bool actual = ExceptionPolicy.HandleException(new NullReferenceException());
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void DemoExceptionHandling()
        {

            try
            {
                throw new Exception("Excepcion ocurrida en una llamada inferior...");
            }
            catch (Exception e)
            {

                #region Reemplazar
                try
                {
                    ExceptionPolicy.HandleException(e, "PoliticaRemplazar");
                }
                catch (Exception e1)
                {
                    Console.Out.WriteLine(e1.Message);
                }
                #endregion

                #region Envolver
                try
                {
                    ExceptionPolicy.HandleException(e, "PoliticaEnvolver");
                }
                catch (Exception e2)
                {
                    Console.Out.WriteLine(e2.Message);
                }
                #endregion

                #region Relanzar
                try
                {
                    if (ExceptionPolicy.HandleException(e, "PoliticaRelanzar/Nada"))
                        throw e;
                }
                catch (Exception e2)
                {
                    Console.Out.WriteLine(e2.Message);
                }
                #endregion

            }

        }
    }
}
