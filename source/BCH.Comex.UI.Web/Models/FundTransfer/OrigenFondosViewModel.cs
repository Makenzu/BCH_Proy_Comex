using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class OrigenFondosViewModel
    {
        public List<string> l_mto_headers { set; get; }
        public List<L_Mto_Model> l_mto_items { set; get;}

        public List<string> l_ori_headers { set; get; }
        public List<L_Fondo_Model> l_ori_items { set; get; }

        public List<SelectListItem> l_mnd { set; get; }
        public int selected_mnd_id { set; get; }

        public List<SelectListItem> L_Partys { set; get; }
        public int selected_partys_id { set; get; }

        public List<SelectListItem> L_Cuentas { set; get; }
        public int selected_cuentas_id { set; get; }

        public List<SelectListItem> cmb_codtran { set; get; }
        public int selected_cmb_codtran { set; get; }

        public List<SelectListItem> L_Cta { set; get; }
        public int selected_l_cta_id { set; get; }

        public string MtoOri { set; get; }
        public bool MtoOri_visible { set; get; }
        public bool MtoOri_enabled { set; get; }

        public bool frm_infoctagap_enabled { set; get; }
        public bool frm_infoctagap_visible { set; get; }
        public string txt_cuenta { set; get; }
        public string txt_CRN { set; get; }

        public OrigenFondosViewModel()
        {
            l_mto_headers = new List<string>();
            l_mto_items = new List<L_Mto_Model>();

            l_ori_headers = new List<string>();
            l_ori_items = new List<L_Fondo_Model>();

            L_Cta = new List<SelectListItem>();
            l_mnd = new List<SelectListItem>();
            L_Partys = new List<SelectListItem>();
            L_Cuentas = new List<SelectListItem>();
            cmb_codtran = new List<SelectListItem>();

        }
    }
}
