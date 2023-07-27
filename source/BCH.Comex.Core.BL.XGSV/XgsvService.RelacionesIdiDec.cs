/*using BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain;*/
using BCH.Comex.Core.Entities.Cext01;
using System.Collections.Generic;


namespace BCH.Comex.Core.BL.XGSV
{
    partial class XgsvService
    {
        

        #region Metodos Públicos

        public IList<sce_grdo_s01_MS_Result> BuscarDeclaracionesAsociadasOperacion(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return uow.SceRepository.Sce_grdo_s01(codcct, codpro, codesp, codofi, codope);
        }

        public IList<sce_grio_s01_MS_Result> BuscarIDIsAsociadasOperacion(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return uow.SceRepository.Sce_grio_s01(codcct, codpro, codesp, codofi, codope);
        }

        public void EliminarIdiDecAsociadasOperacion(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            uow.SceRepository.Sce_grdo_d01(codcct, codpro, codesp, codofi, codope);
            uow.SceRepository.Sce_grio_d01(codcct, codpro, codesp, codofi, codope);
        }
        

        #endregion

        #region Metodos Privados



        #endregion
    }
}
