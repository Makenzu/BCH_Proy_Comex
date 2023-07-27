using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;
using BCH.Comex.Core.Entities.Cext01;
using System.Collections.Generic;
using BCH.Comex.Core.BL.XCFT;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.Common;

namespace BCH.Comex.Test.BL.XCFT
{
    [TestClass]
    public class FundTransferServiceTest
    {
        [TestMethod]
        public void GetDestinatariosTest()
        {
            //using (ShimsContext.Create())
            //{
            //    BCH.Comex.Data.DAL.Cext01.Fakes.ShimSceRepository.AllInstances.sce_tdme_s01_MS = (@this) =>
            //    {
            //        return new List<sce_tdme>() { new sce_tdme{ coddme = 0, desdme = string.Empty } };
            //    };
            //    var ftService = new FundTransferService();
            //    Assert.AreEqual(1, ftService.GetDestinatarios().Count);
            //}
        }

        [TestMethod]
        public void Grabar1TestGrabar2()
        {
            //using (ShimsContext.Create())
            //{
            //    BCH.Comex.Core.BL.XCFT.Modulos.Fakes.Shimmdi_PrincipalLogic.Grabar1InitializationObjectUnitOfWorkCext01 = (initObj, uow) =>
            //    {
            //        return (short)-1;
            //    };
            //    var obj = new InitializationObject();
            //    var ftService = new FundTransferService();
            //    ftService.Grabar1(obj);
            //    Assert.AreEqual("Grabar2", obj.FormularioQueAbrir);
            //}
        }

        [TestMethod]
        public void Grabar1TestGrabar3()
        {
            //using (ShimsContext.Create())
            //{
            //    BCH.Comex.Core.BL.XCFT.Modulos.Fakes.Shimmdi_PrincipalLogic.Grabar1InitializationObjectUnitOfWorkCext01 = (initObj, uow) =>
            //    {
            //        return (short)1;
            //    };
            //    var obj = new InitializationObject();
            //    var ftService = new FundTransferService();
            //    ftService.Grabar1(obj);
            //    Assert.AreEqual("Grabar3", obj.FormularioQueAbrir);
            //}
        }

        [TestMethod]
        public void Grabar1TestIndex()
        {
            //using (ShimsContext.Create())
            //{
            //    BCH.Comex.Core.BL.XCFT.Modulos.Fakes.Shimmdi_PrincipalLogic.Grabar1InitializationObjectUnitOfWorkCext01 = (initObj, uow) =>
            //    {
            //        return (short)0;
            //    };
            //    var obj = new InitializationObject();
            //    var ftService = new FundTransferService();
            //    ftService.Grabar1(obj);
            //    Assert.AreEqual("Index", obj.FormularioQueAbrir);
            //}
        }

        [TestMethod]
        public void GetCodigosDeOrdenPosiblesCampo23Test()
        {
            Assert.AreEqual(12, new FundTransferService().GetCodigosDeOrdenPosiblesCampo23().Count);
        }

        [TestMethod]
        public void CalcularFechaInicialSwiftTest()
        {
            //using (ShimsContext.Create())
            //{
            //    BCH.Comex.Core.BL.XCFT.Forms.Fakes.ShimFrm_Swf0.Pr_Fecha_InicialT_MODGTAB0UnitOfWorkCext01 = (param1, param2) =>
            //    {
            //        return DateTime.Today;
            //    };
            //    Assert.AreEqual(DateTime.Today, new FundTransferService().CalcularFechaInicialSwift(new T_MODGTAB0()));
            //}
        }

        [TestMethod]
        public void ValidarFechaPagoTestFalse()
        {
            //using (ShimsContext.Create())
            //{
            //    string mensajeError = string.Empty;
            //    Assert.IsFalse(new FundTransferService().ValidarFechaPago(new T_MODGTAB0(), DateTime.Today.AddDays(-1), out mensajeError));
            //}
        }

        [TestMethod]
        public void ValidarFechaPagoTestTrueShim()
        {
            //using (ShimsContext.Create())
            //{
            //    string mensajeError = string.Empty;
            //    BCH.Comex.Core.BL.XCFT.Forms.Fakes.ShimFrm_Swf0.ValidarFechaPagoT_MODGTAB0UnitOfWorkCext01DateTimeStringOut = (T_MODGTAB0 param1, UnitOfWorkCext01 param2, DateTime param3, out string param4) =>
            //    {
            //        param4 = string.Empty;
            //        return true;
            //    };
            //    Assert.IsTrue(new FundTransferService().ValidarFechaPago(new T_MODGTAB0(), DateTime.Today.AddDays(1), out mensajeError));
            //}
        }

        [TestMethod]
        public void ValidarFechaPagoTestFalseShim()
        {
            //using (ShimsContext.Create())
            //{
            //    string mensajeError = string.Empty;
            //    BCH.Comex.Core.BL.XCFT.Forms.Fakes.ShimFrm_Swf0.ValidarFechaPagoT_MODGTAB0UnitOfWorkCext01DateTimeStringOut = (T_MODGTAB0 param1, UnitOfWorkCext01 param2, DateTime param3, out string param4) =>
            //    {
            //        param4 = string.Empty;
            //        return false;
            //    };
            //    Assert.IsFalse(new FundTransferService().ValidarFechaPago(new T_MODGTAB0(), DateTime.Today.AddDays(1), out mensajeError));
            //}
        }
    }
}
