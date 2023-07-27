using BCH.Comex.Core.BL.BancaMundial;
using BCH.Comex.Core.Entities.Sbcor;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Test.BL.SBCB
{
    [TestClass]
    public class BancaMundialServiceTest
    {
        /*[TestMethod]
        public void BuscarBancaMundialConsultaMenorLimiteDevuelveResultados()
        {
            using (ShimsContext.Create())
            {
                //arrange

                int cantidadMenorAPagina = 500;
                
                List<sce_bic> bancos = new List<sce_bic>();
                bancos.Add(new sce_bic()
                {
                    bic_swf = "XXX",
                    bic_pai = "CL"
                });
                bancos.Add(new sce_bic()
                {
                    bic_swf = "YYY",
                    bic_pai = "US",
                });

                      var bancoRepository = new BCH.Comex.Data.DAL.Sbcor.Fakes.ShimBancoRepository()
                {
                    CountStringStringStringStringStringString = (str1, str2, str3, str4, str5, str6) => cantidadMenorAPagina,
                    ListStringStringStringStringStringString = (str1, str2, str3, str4, str5, str6) => bancos
                };
                
                BCH.Comex.Data.DAL.Sbcor.Fakes.ShimUnitOfWorkSbcor.AllInstances.BancoRepositoryGet = @this => bancoRepository;

                BancaMundialService service = new BancaMundialService();
                
                //act
                object result = service.BuscarBancaMundial(String.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                //assert
                Assert.IsNotNull(result, "El resultado es nulo");
                Assert.IsInstanceOfType(result, typeof(List<sce_bic>), "El resultado no tiene el tipo esperado");
                Assert.IsTrue(((List<sce_bic>)result).Count > 0, "La busqueda no devolvió resultados");
            };
        }

        [TestMethod]
        public void BuscarBancaMundialConsultaMayorLimiteDevuelveError()
        {
            using (ShimsContext.Create())
            {
                //arrange

                int cantidadMayorLimite = 2500;

                var bancoRepository = new BCH.Comex.Data.DAL.Sbcor.Fakes.ShimBancoRepository()
                {
                    CountStringStringStringStringStringString = (str1, str2, str3, str4, str5, str6) => cantidadMayorLimite,
                };

                BCH.Comex.Data.DAL.Sbcor.Fakes.ShimUnitOfWorkSbcor.AllInstances.BancoRepositoryGet = @this => bancoRepository;

                BancaMundialService service = new BancaMundialService();

                //act
                object result = service.BuscarBancaMundial(String.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                //assert
                Assert.IsNotNull(result, "El resultado es nulo");
                Assert.AreEqual(BancaMundialService.LIMIT_EXCEEDED, result.ToString(), "La busqueda no retornó error de límite");
            };
        }*/
    }
}
