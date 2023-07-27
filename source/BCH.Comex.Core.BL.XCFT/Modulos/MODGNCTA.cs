using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGNCTA
    {
        public static T_MODGNCTA GetMODGNCTA()
        {
            return new T_MODGNCTA();
        }

        //-------------------------------------------------------------
        //Busca los datos de la Cuenta Contable; 1º en memoria; 2º a disco.-
        public static int Get_Cta(string NemCta, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            int n = 0;
            short i = 0;
            int m = -1;
            
            n = initObj.MODGNCTA.VCta.Length;
            
            if (n > 0)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    if (VB6Helpers.UCase(VB6Helpers.Trim(initObj.MODGNCTA.VCta[i].Cta_Nem.Value)) == VB6Helpers.UCase(VB6Helpers.Trim(NemCta)))
                    {
                        m = i;
                        break;
                    }
                }
            }
            else
            {
                VB6Helpers.Redim(ref initObj.MODGNCTA.VCta, 0, 0);
            }

            if (m == -1)
            {
                m = SyGet_Cta(NemCta, initObj, unit);
            }

            return m;
        }

        //Retorna los datos de la Cuenta Contable.-
        public static int SyGet_Cta(string NemCta, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
                string R = "";
                //@emiliano: por aca tiene que pasar antes
                var result = unit.SceRepository.sce_cta_s01_1_MS(NemCta == null ? NemCta : NemCta.ToUpper());
                int i = initObj.MODGNCTA.VCta.Length;
                VB6Helpers.RedimPreserve(ref initObj.MODGNCTA.VCta, 0, i);

                if (result.Count <= 0) {
                    return 0;
                }
            
                var item = result.First();
                initObj.MODGNCTA.VCta[i] = new T_Cta();
                initObj.MODGNCTA.VCta[i].Cta_Nem.Value = item.cta_nem;
                initObj.MODGNCTA.VCta[i].Cta_Mon = (short)item.cta_mon;
                initObj.MODGNCTA.VCta[i].Cta_Num.Value = item.cta_num;
                initObj.MODGNCTA.VCta[i].Cta_Nom.Value = item.cta_nom;
                initObj.MODGNCTA.VCta[i].Cta_GL = Convert.ToInt16(item.cta_gl);
                initObj.MODGNCTA.VCta[i].Cta_NroTO = (int)item.cta_nroto;
                initObj.MODGNCTA.VCta[i].Cta_IndTO = (short)item.cta_indto;
                initObj.MODGNCTA.VCta[i].Cta_CIT = Convert.ToInt16(item.cta_cit);
                initObj.MODGNCTA.VCta[i].Cta_CVT = Convert.ToInt16(item.cta_cvt);
                initObj.MODGNCTA.VCta[i].Cta_CAP = Convert.ToInt16(item.cta_cap);
                initObj.MODGNCTA.VCta[i].Cta_CTD = Convert.ToInt16(item.cta_ctd);
                initObj.MODGNCTA.VCta[i].Cta_POS = Convert.ToInt16(item.cta_pos);
                initObj.MODGNCTA.VCta[i].Cta_CDR = Convert.ToInt16(item.cta_cdr);
                initObj.MODGNCTA.VCta[i].Cta_Vig = (short)item.cta_vigtbl;
            
                return i;
        }
    }
}
