using BCH.Comex.Core.BL.SWI200;
using BCH.Comex.Core.Entities.Swift;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BCH.Comex.Test.BL.SWI200
{
    [TestClass]
    public class Swi200ServiceTest
    {
        private string root = Environment.CurrentDirectory = @"..\..\" + resources;
        private const string resources = "/Resources/Swi200/";

        [TestMethod]
        public void IngresoCorrecto()
        {
            var mensaje = new System.IO.StreamReader(Path.Combine(root, "mensajeSwiftValido.txt")).ReadToEnd();
            using (ShimsContext.Create())
            {
                BCH.Comex.Core.BL.SWI200.Fakes.ShimSwi200Service.AllInstances.IngresarMensajeMensajeSwiftSwi200 = (@this, msg) =>
                {
                    return true;
                };
                var service = new Swi200Service();
                var respuesta = service.IngresaModificaMensajeSwift(1234, 2593132, 23285174, 714, "USD", 29, 'I', mensaje);
                Assert.IsNotNull(respuesta);
            }
        }

        [TestMethod]
        public void ModificacionCorrecta()
        {
            var mensaje = new System.IO.StreamReader(Path.Combine(root, "mensajeSwiftValido.txt")).ReadToEnd();
            using (ShimsContext.Create())
            {
                BCH.Comex.Core.BL.SWI200.Fakes.ShimSwi200Service.AllInstances.EditarMensajeMensajeSwiftSwi200 = (@this, msg) =>
                {
                    return true;
                };
                var service = new Swi200Service();
                var respuesta = service.IngresaModificaMensajeSwift(12345, 2593132, 23285174, 714, "USD", 29, 'I', mensaje);
                Assert.IsNotNull(respuesta);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Swi200Exception))]
        public void ErrorPorMensajeExistente()
        {
            var mensaje = new System.IO.StreamReader(Path.Combine(root, "mensajeSwiftValido.txt")).ReadToEnd();
            using (ShimsContext.Create())
            {
                BCH.Comex.Data.DAL.Swift.Fakes.ShimMensajeRepository.AllInstances.GetInt32 = (@this, id) =>
                {
                    return new sw_msgsend();
                };
                var service = new Swi200Service();
                var respuesta = service.IngresaModificaMensajeSwift(1234, 2593132, 23285174, 714, "USD", 29, 'I', mensaje);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Swi200Exception))]
        public void ErrorPorCaracterInicial()
        {
            var mensaje = new System.IO.StreamReader(Path.Combine(root, "mensajeSwiftErrorPrimerCaracter.txt")).ReadToEnd();
            using (ShimsContext.Create())
            {
                BCH.Comex.Core.BL.SWI200.Fakes.ShimSwi200Service.AllInstances.VerificaCasillasInt32 = (@this, casilla) =>
                {
                    return;
                };
                BCH.Comex.Data.DAL.Swift.Fakes.ShimMensajeRepository.AllInstances.GetInt32 = (@this, id) =>
                {
                    return null;
                };
                var service = new Swi200Service();
                var respuesta = service.IngresaModificaMensajeSwift(1234, 2593132, 23285174, 714, "USD", 29, 'I', mensaje);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Swi200Exception))]
        public void ErrorPorCaracterFinal()
        {
            var mensaje = new System.IO.StreamReader(Path.Combine(root, "mensajeSwiftErrorUltimoCaracter.txt")).ReadToEnd();
            using (ShimsContext.Create())
            {
                BCH.Comex.Core.BL.SWI200.Fakes.ShimSwi200Service.AllInstances.VerificaCasillasInt32 = (@this, casilla) =>
                {
                    return;
                };
                BCH.Comex.Data.DAL.Swift.Fakes.ShimMensajeRepository.AllInstances.GetInt32 = (@this, id) =>
                {
                    return null;
                };
                var service = new Swi200Service();
                var respuesta = service.IngresaModificaMensajeSwift(1234, 2593132, 23285174, 714, "USD", 29, 'I', mensaje);
            }
        }
    }
}
