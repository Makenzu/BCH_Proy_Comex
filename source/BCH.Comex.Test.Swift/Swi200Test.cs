using BCH.Comex.Core.BL.SWI200;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BCH.Comex.Test.Swift
{
    [TestClass]
    public class Swi200Test
    {
        private Swi200Service service = new Swi200Service();
        private const string resources = "/Resources/Swi200/";
        private const string testIngresarSuccess = "TestIngresarSuccess.txt";
        private const string testModificarSuccessIng = "TestModificarSuccessIng.txt";
        private const string testModificarSuccessMod = "TestModificarSuccessMod.txt";
        private const string testModificarCorrelativoNoExiste = "TestModificarCorrelativoNoExiste.txt";
        private const string testIngresarCasillaNoExiste = "TestIngresarCasillaNoExiste.txt";
        private const string testIngresarTipoMensajeNoCorresponde = "TestIngresarTipoMensajeNoCorresponde.txt";
        private const string testIngresarLargoEncabezado = "TestIngresarLargoEncabezado.txt";

        private string root = Environment.CurrentDirectory = @"..\..\" + resources;

        [TestMethod]
        public void TestIngresarSuccess()
        {
            try
            {
                //Ingresa
                string mensajeSwift = new System.IO.StreamReader(Path.Combine(root, testIngresarSuccess)).ReadToEnd();
                string result = service.Swi200(mensajeSwift);
                Assert.AreEqual(result, "-1");

                //Dejamos la base como estaba
                service.DeleteMensaje(Convert.ToInt32(mensajeSwift.Substring(67, 10)));
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message));
            }
        }

        [TestMethod]
        public void TestModificarSuccess()
        {
            try
            {
                //Ingresa                
                string resultI = service.Swi200(new System.IO.StreamReader(Path.Combine(root, testModificarSuccessIng)).ReadToEnd());
                Assert.AreEqual(resultI, "-1");

                //Modifica
                string mensajeSwiftM = new System.IO.StreamReader(Path.Combine(root, testModificarSuccessMod)).ReadToEnd();
                string resultM = service.Swi200(new System.IO.StreamReader(Path.Combine(root, testModificarSuccessMod)).ReadToEnd());
                Assert.AreEqual(resultM, "-1");

                //Dejamos la base como estaba
                service.DeleteMensaje(Convert.ToInt32(mensajeSwiftM.Substring(67, 10)));
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message));
            }
        }

        [TestMethod]
        public void TestModificarCorrelativoNoExiste()
        {
            string errorMessage = string.Empty;
            try
            {
                service.Swi200(new System.IO.StreamReader(Path.Combine(root, testModificarCorrelativoNoExiste)).ReadToEnd());
            }
            catch (Swi200Exception e)
            {
                errorMessage = e.Message;
            }
            Assert.AreEqual(errorMessage, Swi200Constants.ERROR_CORRELATIVO_NO_EXISTE);
        }

        [TestMethod]
        public void TestModificarCasillaNoExiste()
        {
            string errorMessage = string.Empty;
            try
            {
                service.Swi200(new System.IO.StreamReader(Path.Combine(root, testIngresarCasillaNoExiste)).ReadToEnd());
            }
            catch (Swi200Exception e)
            {
                errorMessage = e.Message;
            }
            Assert.AreEqual(errorMessage, Swi200Constants.ERROR_CASILLA_NO_EXISTE);
        }

        [TestMethod]
        public void TestIngresarTipoMensajeNoCorresponde()
        {
            string errorMessage = string.Empty;
            try
            {
                service.Swi200(new System.IO.StreamReader(Path.Combine(root, testIngresarTipoMensajeNoCorresponde)).ReadToEnd());
            }
            catch (Swi200Exception e)
            {
                errorMessage = e.Message;
            }
            Assert.AreEqual(errorMessage, Swi200Constants.ERROR_TIPO_MENSAJE_NO_CORRESPONDE);
        }

        [TestMethod]
        public void TestIngresarLargoEncabezado()
        {
            string errorMessage = string.Empty;
            try
            {
                service.Swi200(new System.IO.StreamReader(Path.Combine(root, testIngresarLargoEncabezado)).ReadToEnd());
            }
            catch (Swi200Exception e)
            {
                errorMessage = e.Message;
            }
            Assert.AreEqual(errorMessage, Swi200Constants.ERROR_LARGO_ENCABEZADO);
        }

        [TestMethod]
        public void TestValidaMensajeSwiftVerificarTexto()
        {
            var mensaje = new System.IO.StreamReader(Path.Combine(root, "mensajeSwiftValido.txt")).ReadToEnd();
            //var swi200 = new MensajeSwiftSwi200 { SubTexto = mensaje };
            //service.VerificarTexto(swi200);
        }

        [TestMethod]
        public void TestIngresaModificaMensajeSwift()
        {
            var mensaje = new System.IO.StreamReader(Path.Combine(root, "mensajeSwiftValido.txt")).ReadToEnd();
            var respuesta = service.IngresaModificaMensajeSwift(1234, 2593132, 23285174, 714, "USD", 29, 'M', mensaje);
        }

        [TestMethod]
        public void TestValidaMensajeSwiftVerificaSwift()
        {
            var mensaje = new System.IO.StreamReader(Path.Combine(root, "mensajeSwiftValido.txt")).ReadToEnd();
            //var swi200 = new MensajeSwiftSwi200 { SubTexto = mensaje };
            //swi200.BancoEm = swi200.SubTexto.Substring(7, 8);
            //swi200.BranchEm = swi200.SubTexto.Substring(16, 3);
            //swi200.TipoMensaje = "MT" + swi200.SubTexto.Substring(34, 3);
            //swi200.Prioridad = swi200.SubTexto.Substring(49, 1);
            //swi200.Casilla = 714;
            //service.VerificaSwift(swi200);
        }
    }
}
