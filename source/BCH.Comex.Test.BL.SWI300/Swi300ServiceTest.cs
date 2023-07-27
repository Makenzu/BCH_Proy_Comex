using BCH.Comex.Core.BL.SWI300;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BCH.Comex.Test.BL.SWI300
{
    [TestClass]
    public class Swi300ServiceTest
    {
        [TestMethod]
        public void GetCorrelativoTest()
        {
            using (ShimsContext.Create())
            {
                int correlativo = 51010;
                var bancoRepository = new BCH.Comex.Data.DAL.Swift.Fakes.ShimBancoRepository(){
                    GetCorrelativo = () => correlativo
                };
                BCH.Comex.Data.DAL.Swift.Fakes.ShimUnitOfWorkSwift.AllInstances.BancoRepositoryGet = @this => bancoRepository;
                //BCH.Comex.Data.DAL.Swift.Fakes.ShimswiftEntities.Constructor = (@this) =>
                //{

                //};
                //BCH.Comex.Data.DAL.Swift.Fakes.ShimBancoRepository.ConstructorswiftEntities = (@this, value) =>
                //{

                //};
                var swi300Service = new Swi300Service();
                Assert.AreEqual(correlativo, swi300Service.GetCorrelativo());
            }
        }
    }
}
