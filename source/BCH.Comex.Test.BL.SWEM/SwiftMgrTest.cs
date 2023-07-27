using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCH.Comex.Core.BL.SWEM;
using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Swift;
using BCH.Comex.Data.DAL.Swift.Fakes;
using BCH.Comex.Core.BL.SWI102.Fakes;

namespace BCH.Comex.Test.BL.SWEM
{
    [TestClass]
    public class SwiftMgrTest
    {
//        [TestMethod]
//        public void CargarMensajeSwiftDevuelveDetalleCorrecto()
//        {
//            using (ShimsContext.Create())
//            {
//                using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
//                {
//                    //arrange
//                    string detalleRaw = @"{1:F01BCHICLRMAXXX          }{2:I103ANZBAU3MAXXXN}{4:
//                        :20:753206800055058
//                        :23B:CRED
//                        :32A:150102AUD330000,00
//                        :33B:AUD330000,00
//                        :50F:/CUST/CL/001760237701
//                        1/AUSENCO CHILE S.A.
//                        2/AV. LAS CONDES 11.283, PISO 6
//                        3/CL/SANTIAGO
//                        :57A://AU014002
//                        ANZBAU3M
//                        :59:/837555235
//                        AUSENCO SERVICES PTY LTD
//                        :70:/RFB/PYMT INVOICES PROFESSIONAL
//                        SERVICES
//                        :71A:OUR
//                        -}";


//                    ResultadoBusquedaSwift resultadoMock = new ResultadoBusquedaSwift()
//                    {
//                        id_mensaje = 2530963
//                    };

//                    var mensajeRepository = new BCH.Comex.Data.DAL.Swift.Fakes.ShimMensajeRepository()
//                    {
//                        GetSwiftEnvidoInt32 = (nroMensaje) => resultadoMock
//                    };
//                    BCH.Comex.Data.DAL.Swift.Fakes.ShimUnitOfWorkSwift.AllInstances.MensajeRepositoryGet = @this => mensajeRepository;


//                    IList<proc_trae_tipo_campos_MS_Result> tipoCampos = uow.TipoCampoRepository.GetTipoCamposConMaximo();
//                    IList<sw_campos_msg> campos = uow.CamposMsgRepository.GetFormatoCamposSWEM();


//                    int cantCamposEsperados = 9;
//                    int cantLineasEsperadas = 15;
//                    int idMensaje = 2530963;

//                    SwiftMgr mgr = new SwiftMgr();
//                    ShimMensajeSwiftService.AllInstances.DesencriptaMensajeEnviadoInt32 = (@this, nroMensaje) => detalleRaw;

//                    //act
//                    ResultadoBusquedaSwift result = mgr.CargarMensajeSwiftEnviado(idMensaje, tipoCampos, campos);

//                    //assert
//                    Assert.IsNotNull(result, "El mensaje swift no puede ser nulo");
//                    Assert.AreEqual(cantLineasEsperadas, result.LineasDetalle.Count, "La cantidad de lineas del detalle no son las esperadas");
//                    Assert.AreEqual(cantCamposEsperados, result.LineasDetalle.Where(x => x.EsNuevaLineaDeCampo == true).Count(), "La cantidad de campos del detalle no son los esperados");
//                };
//            };
//        }
    }
}


