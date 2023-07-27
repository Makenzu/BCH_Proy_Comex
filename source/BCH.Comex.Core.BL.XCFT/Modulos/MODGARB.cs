using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGARB
    {
        public static T_MODGARB GetMODGARB() {
            return new T_MODGARB();
        }

        public static short SyPutn_ArbAtom(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            T_MODGARB MODGARB = initObject.MODGARB;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;

            short _retValue = 0;
            short n = 0;
            short HayError = 0;
            short i = 0;
            string Que = "";
            
            n = (short)VB6Helpers.UBound(MODGARB.VArb);            

            HayError = (short)(false ? -1 : 0);
            //Recorre estructura de Arbitarje.
            for (i = 0; i <= (short)n; i++)
            {
                if (MODGARB.VArb[i].Status != T_MODGCVD.EstadoEli)
                {
                    //---->> FELIPE: ESTO FUNCIONO!!!

                    //se crea una variable nueva que se utiliza para acceder desde la clausura
                    //de la funcion y no cambia en el loop
                    int indice = i;
                    _retValue = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(Mdl_Funciones_Varias, () =>
                    {
                        return (short)unit.SceRepository.sce_arb_i01_MS(MODGSYB.dbcharSy(MODGCVD.VgCvd.codcct), MODGSYB.dbcharSy(MODGCVD.VgCvd.codpro), MODGSYB.dbcharSy(MODGCVD.VgCvd.codesp), MODGSYB.dbcharSy(MODGCVD.VgCvd.codofi), MODGSYB.dbcharSy(MODGCVD.VgCvd.codope),indice.ToString(), MODGARB.VArb[indice].Status.ToString(), MODGARB.VArb[indice].codpai.ToString(), MODGARB.VArb[indice].MndCom.ToString(), MODGARB.VArb[indice].MndVta.ToString(), MODGSYB.dbmontoSy(MODGARB.VArb[indice].MtoCom), MODGSYB.dbmontoSy(MODGARB.VArb[indice].MtoVta), MODGSYB.dbPardSy(MODGARB.VArb[indice].PrdArb), MODGSYB.dbTCamSy(MODGARB.VArb[indice].TipCam), MODGSYB.dbmontoSy(MODGARB.VArb[indice].MtoDol), MODGSYB.dbmontoSy(MODGARB.VArb[indice].MtoPes), MODGARB.VArb[indice].Conven==-1 ? "1" : "0");
                    });

                    if (~_retValue != 0)
                    {
                        return _retValue;
                    }
                    
                }
            }
            _retValue = (short)(true ? -1 : 0);
            return _retValue;
        }
    }
}
