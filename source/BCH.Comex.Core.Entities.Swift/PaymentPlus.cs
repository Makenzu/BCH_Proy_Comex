
namespace BCH.Comex.Core.Entities.Swift
{
    public class PaymentPlus
    {
        public string trans_dsc_bic8 {get; set;}
        public string trans_dsc_branch_bic { get; set;}
        public string trans_dsc_institution_name { get; set;}
        public string trans_dsc_zip_code { get; set; }
        public string trans_dsc_city { get; set; }
        public string trans_dsc_street_address_1 { get; set; }
        public string trans_dsc_street_address_2 { get; set; }
        public string trans_dsc_street_address_3 { get; set; }
        public string trans_dsc_street_address_4 { get; set; }
        public string trans_dsc_country_name { get; set; }
        public string trans_dsc_iso_country_code { get; set; }
    }
}
