using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class DestinoFondosViewModel:ViewModel
    {
        internal int l_mto_id;

        public List<string> l_mto_headers { set; get; }
        public List<L_Mto_Model> l_mto_items { set; get; }

        public List<string> l_via_headers { set; get; }
        public List<L_Fondo_Model> l_via_items { set; get; }

        public List<SelectListItem> l_mnd { set; get; }
        public int selected_mnd_id { set; get; }

        public List<SelectListItem> L_Partys { set; get; }
        public int selected_partys_id { set; get; }

        public List<SelectListItem> L_Cuentas { set; get; }
        public int selected_cuentas_id { set; get; }

        public List<SelectListItem> Cb_Destino { set; get; }
        public int selected_cb_destino_id { set; get; }
        public bool Cb_Destino_E { set; get; }
        public List<SelectListItem> L_Cta { set; get; }
        public int selected_l_cta_id { set; get; }

        public string MtoVia { set; get; }
        public bool MtoVia_visible { set; get; }
        public bool MtoVia_enabled { set; get; }

        public List<SelectListItem> cmb_codtran { set; get; }
        public int selected_cmb_codtran_id { set; get; }
        public bool cmb_codtran_V { set; get; }
        public bool cmb_codtran_E { set; get; }

        public bool? MostrarVolverADefinir { get; set; }

        public DestinoFondosViewModel()
        {
            
        }
    }
}
