using BCH.Comex.Core.BL.XGPY.PrtyMod;
//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using System;

namespace BCH.Comex.Core.BL.XGPY
{
    public class XgpyResult
    {
        public Int32 ErrorCode { get; set; }
        public String Message { get; set; }

        public Prty Prty { get; set; }

        public InitializationObject iObject { get; set; }
        public T_PRTGLOB tPrtGlob { get; set; }
    }
}
