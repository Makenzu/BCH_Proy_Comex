using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGNCTA
    {
        //-------------------------------------------------------------
        //Busca los datos de la Cuenta Contable; 1º en memoria; 2º a disco.-
        public static int Get_Cta(string NemCta, DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            int n = 0;
            short i = 0;
            int m = -1;

            n = Globales.MODGNCTA.VCta.Length;

            if (n > 0)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    if (VB6Helpers.UCase(VB6Helpers.Trim(Globales.MODGNCTA.VCta[i].Cta_Nem)) == VB6Helpers.UCase(VB6Helpers.Trim(NemCta)))
                    {
                        m = i;
                        break;
                    }
                }
            }
            else
            {
                VB6Helpers.Redim(ref Globales.MODGNCTA.VCta, 0, 0);
            }

            if (m == -1)
            {
                m = SyGet_Cta(NemCta, Globales, unit);
            }

            return m;
        }

        //Retorna los datos de la Cuenta Contable.-
        public static int SyGet_Cta(string NemCta, DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            var result = unit.SceRepository.sce_cta_s01_1_MS(NemCta);
            int i = Globales.MODGNCTA.VCta.Length;
            VB6Helpers.RedimPreserve(ref Globales.MODGNCTA.VCta, 0, i);
            var item = result.First();
            Globales.MODGNCTA.VCta[i] = new T_Cta();
            Globales.MODGNCTA.VCta[i].Cta_Nem = item.cta_nem;
            Globales.MODGNCTA.VCta[i].Cta_Mon = (short)item.cta_mon;
            Globales.MODGNCTA.VCta[i].Cta_Num = item.cta_num;
            Globales.MODGNCTA.VCta[i].Cta_Nom = item.cta_nom;
            Globales.MODGNCTA.VCta[i].Cta_GL = Convert.ToInt16(item.cta_gl);
            Globales.MODGNCTA.VCta[i].Cta_NroTO = (int)item.cta_nroto;
            Globales.MODGNCTA.VCta[i].Cta_IndTO = (short)item.cta_indto;
            Globales.MODGNCTA.VCta[i].Cta_CIT = Convert.ToInt16(item.cta_cit);
            Globales.MODGNCTA.VCta[i].Cta_CVT = Convert.ToInt16(item.cta_cvt);
            Globales.MODGNCTA.VCta[i].Cta_CAP = Convert.ToInt16(item.cta_cap);
            Globales.MODGNCTA.VCta[i].Cta_CTD = Convert.ToInt16(item.cta_ctd);
            Globales.MODGNCTA.VCta[i].Cta_POS = Convert.ToInt16(item.cta_pos);
            Globales.MODGNCTA.VCta[i].Cta_CDR = Convert.ToInt16(item.cta_cdr);
            Globales.MODGNCTA.VCta[i].Cta_Vig = (short)item.cta_vigtbl;

            return i;
        }
    }
}
