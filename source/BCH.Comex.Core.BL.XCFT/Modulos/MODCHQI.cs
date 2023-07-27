using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public static class MODCHQI
    {


        //Procedimiento que carga los valores de los Cheques.-
        //Requiere las Tablas :
        // Sgt_Pai
        // Sce_Nom
        // Sce_Cor
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short InterfazChq(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODGCHQ MODGCHQ = initObject.MODGCHQ;
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_Module1 Module1 = initObject.Module1;
            // UPGRADE_INFO (#05B1): The 'a' variable wasn't declared explicitly.
            short a = 0;
            // UPGRADE_INFO (#05B1): The 'n' variable wasn't declared explicitly.
            short n = 0;
            // UPGRADE_INFO (#05B1): The 'k' variable wasn't declared explicitly.
            short k = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            // UPGRADE_INFO (#05B1): The 'm' variable wasn't declared explicitly.
            short m = 0;

            //Carga las Tablas.-
            a = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGetn_Pai(initObject.MODGTAB0, unit);
            a = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGetn_Nom(initObject, unit);
            //a = 
            BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.SyGetn_Cor(initObject.MODGTAB0, unit);

            //Total Cheques.-
            n = BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.TotalChq(initObject.MODXVIA);
            if (n == 0)
            {
                return 0;
            }

            //Se cargan los Documentos según Vías.-
            
            k = (short)VB6Helpers.UBound(MODGCHQ.V_Chq_VVi);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0
            //if (k == 0)
            if (k == -1)
            {
                MODGCHQ.V_Chq_VVi = new Chq_Vvi[0];
                for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxVia); i++)
                {
                    MODXVIA.VxVia[i].IndChq = -1;
                }

            }

            //n = 1;
            n = 0;            
            for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxVia); i++)
            {
                if (MODXVIA.VxVia[i].Status != T_MODGCVD.EstadoEli)
                {
                    if (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CHMEBCH)
                    {
                        if (MODXVIA.VxVia[i].IndChq == -1)
                        {
                            m = (short)(VB6Helpers.UBound(MODGCHQ.V_Chq_VVi) + 1);
                            VB6Helpers.RedimPreserve(ref MODGCHQ.V_Chq_VVi, 0, m);

                            MODXVIA.VxVia[i].IndChq = Convert.ToInt16(n);
                            MODGCHQ.V_Chq_VVi[n].MtoDoc = MODXVIA.VxVia[i].MtoTot;
                            MODGCHQ.V_Chq_VVi[n].TipoDoc = "CH";
                            MODGCHQ.V_Chq_VVi[n].CodMon = MODXVIA.VxVia[i].CodMon;
                        }

                        n = (short)(n + 1);
                    }

                    if (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_VVBCH)
                    {
                        if (MODXVIA.VxVia[i].IndChq == -1)
                        {
                            m = (short)(VB6Helpers.UBound(MODGCHQ.V_Chq_VVi) + 1);
                            VB6Helpers.RedimPreserve(ref MODGCHQ.V_Chq_VVi, 0, m);
                            MODXVIA.VxVia[i].IndChq = Convert.ToInt16(n);
                            MODGCHQ.V_Chq_VVi[n].MtoDoc = MODXVIA.VxVia[i].MtoTot;
                            MODGCHQ.V_Chq_VVi[n].TipoDoc = "VV";
                            MODGCHQ.V_Chq_VVi[n].CodMon = MODXVIA.VxVia[i].CodMon;
                            MODGCHQ.V_Chq_VVi[n].RutTom = BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.SoloNumeros(Module1.PartysOpe[MODGCHQ.ITom].rut, 10);
                        }

                        n = (short)(n + 1);
                    }

                }

            }

            //Carga los posibles Beneficiarios.-
            MODGCHQ.BenDocVal = new Type_BenDocVal[0];
            for (i = 0; i <= (short)VB6Helpers.UBound(initObject.MODGCVD.Beneficiario); i++)
            {
                if(!string.IsNullOrEmpty(Module1.PartysOpe[i].NombreUsado))
                {
                    m = (short)(VB6Helpers.UBound(MODGCHQ.BenDocVal) + 1);
                    VB6Helpers.RedimPreserve(ref MODGCHQ.BenDocVal, 0, m);
                    MODGCHQ.BenDocVal[m].FunBen = initObject.MODGCVD.Beneficiario[i];
                    MODGCHQ.BenDocVal[m].NomBen = Module1.PartysOpe[i].NombreUsado;
                    MODGCHQ.BenDocVal[m].RutTom = BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.SoloNumeros(Module1.PartysOpe[MODGCHQ.ITom].rut, 10);
                    MODGCHQ.BenDocVal[m].PaiBen = Module1.PartysOpe[i].CodPais;
                }

            }
            short aux = (short)(VB6Helpers.UBound(MODGCHQ.BenDocVal) + 1);
            m = aux;
            VB6Helpers.RedimPreserve(ref MODGCHQ.BenDocVal, 0, m);
            MODGCHQ.BenDocVal[m].FunBen = "Beneficiario";
            aux = (short)VB6Helpers.UBound(MODGCHQ.V_Chq_VVi);
            return aux;
        }
    }
}
