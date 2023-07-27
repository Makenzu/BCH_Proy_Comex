using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.Devengo
{
    public class DevengIntReaj
    {
        public int TipoConsultaDev { get; set; }
        public List<string> Cmbperiodo { get; set; }
        public int CmbperiodoSelected { get; set; }
        public List<T_DevengInt> DevengInt { get; set; }
        public List<T_DevengReaj> DevengReaj { get; set; }
        public string todos { get; set; }

        public DevengIntReaj()
        {
            TipoConsultaDev = 1;
            Cmbperiodo = new List<string>();
            CmbperiodoSelected = -1;
            DevengInt = new List<T_DevengInt>();
            DevengReaj = new List<T_DevengReaj>();
            todos = "S";
        }
    }

    public class T_DevengInt
    {
        public double t_num_ope { get; set; }
        public int t_cui { get; set; }
        public int t_numneg { get; set; }
        public double t_monpro { get; set; }
        public double t_tastot { get; set; }
        public DateTime t_fecini { get; set; }
        public DateTime t_fecfin { get; set; }
        public int t_dias { get; set; }
        public double t_tipcam { get; set; }
        public string t_rut { get; set; }
        public int t_ano_mes { get; set; }
        public int t_estado { get; set; }
        public double t_mtovig { get; set; }
        public string t_cuenta_k { get; set; }
        public string t_nemonico_k { get; set; }
        public double t_mtome_c { get; set; }
        public double t_mtomn_c { get; set; }
        public string t_cuenta_c { get; set; }
        public string t_nemonico_c { get; set; }
        public double t_mtome_gn { get; set; }
        public double t_mtomn_gn { get; set; }
        public string t_cuenta_gn { get; set; }
        public string t_nemonico_gn { get; set; }
        public double t_mtome_gd { get; set; }
        public double t_mtomn_gd { get; set; }
        public string t_cuenta_gd { get; set; }
        public string t_nemonico_gd { get; set; }
        public double t_mtome_gdd { get; set; }
        public double t_mtomn_gdd { get; set; }
        public string t_cuenta_gdd { get; set; }
        public string t_nemonico_gdd { get; set; }
        public double t_mtome_cp { get; set; }
        public double t_mtomn_cp { get; set; }
        public string t_cuenta_cp { get; set; }
        public string t_nemonico_cp { get; set; }
        public double t_mtome_gpn { get; set; }
        public double t_mtomn_gpn { get; set; }
        public string t_cuenta_gpn { get; set; }
        public string t_nemonico_gpn { get; set; }
        public double t_mtome_gpd { get; set; }
        public double t_mtomn_gpd { get; set; }
        public string t_cuenta_gpd { get; set; }
        public string t_nemonico_gpd { get; set; }
        public double t_mtome_gpdd { get; set; }
        public double t_mtomn_gpdd { get; set; }
        public string t_cuenta_gpdd { get; set; }
        public string t_nemonico_gpdd { get; set; }
        public int t_tippro { get; set; }
        public int t_numpro { get; set; }
        public int t_numcuo { get; set; }
        public int t_fec_deterioro { get; set; }
        public double t_tasa_penal { get; set; }
        public int t_to { get; set; }
        public int t_to_plazo { get; set; }
    }

    public class T_DevengReaj
    {
        public double t_num_ope { get; set; }
        public int t_cui { get; set; }
        public int t_numneg { get; set; }
        public int t_monpro { get; set; }
        public double t_tastot { get; set; }
        public DateTime t_fecini { get; set; }
        public DateTime t_fecfin { get; set; }
        public int t_dias { get; set; }
        public double t_tipcam { get; set; }
        public string t_rut { get; set; }
        public int t_ano_mes { get; set; }
        public int t_estado { get; set; }
        public double t_mtovig { get; set; }
        public string t_cuenta_k { get; set; }
        public string t_nemonico_k { get; set; }
        public double t_rmtome_c { get; set; }
        public double t_rmtomn_c { get; set; }
        public string t_rcuenta_c { get; set; }
        public string t_rnemonico_c { get; set; }
        public double t_rmtome_gn { get; set; }
        public double t_rmtomn_gn { get; set; }
        public string t_rcuenta_gn { get; set; }
        public string t_rnemonico_gn { get; set; }
        public double t_rmtome_gd { get; set; }
        public double t_rmtomn_gd { get; set; }
        public int t_rcuenta_gd { get; set; }
        public string t_rnemonico_gd { get; set; }
        public double t_rmtome_gdd { get; set; }
        public double t_rmtomn_gdd { get; set; }
        public string t_rcuenta_gdd { get; set; }
        public string t_rnemonico_gdd { get; set; }
        public int t_tippro { get; set; }
        public int t_numpro { get; set; }
        public int t_sprwsh { get; set; }
        public int t_to { get; set; }
        public int t_to_plazo { get; set; }
    }
}
