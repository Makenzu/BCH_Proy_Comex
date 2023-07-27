using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class GanuViewModel : FundTransferViewModel
    {
        public UI_TextBox Tx_NroOpe_0 { get; set; }
        public UI_TextBox Tx_NroOpe_1 { get; set; }
        public UI_TextBox Tx_NroOpe_2 { get; set; }
        public UI_TextBox Tx_NroOpe_3 { get; set; }
        public UI_TextBox Tx_NroOpe_4 { get; set; }
        public List<SelectListItem> Txt_NroOpe { get; set; }
        public UI_TextBox Tx_Prty { get; set; }
        public UI_TextBox Lt_Pln { get; set; }
        public UI_Button Cmd_Ok { get; set; }
        public UI_Button Co_Boton_0 { get; set; }
        public UI_Button Co_Boton_1 { get; set; }
        public string Redireccionar { get; set; }

        public GanuViewModel()
        {
            Co_Boton_0 = new UI_Button() { Enabled = false };
            Co_Boton_1 = new UI_Button() { Enabled = false };
            Cmd_Ok = new UI_Button();
        }

        public GanuViewModel(UI_frmganu frmState)
        {
            if (frmState == null)
            {
                frmState = new UI_frmganu();
            }
            Tx_NroOpe_0 = frmState.Tx_NroOpe_000;
            Tx_NroOpe_1 = frmState.Tx_NroOpe_001;
            Tx_NroOpe_2 = frmState.Tx_NroOpe_002;
            Tx_NroOpe_3 = frmState.Tx_NroOpe_003;
            Tx_NroOpe_4 = frmState.Tx_NroOpe_004;
            Co_Boton_0 = frmState.Co_Boton_000;
            Co_Boton_1 = frmState.Co_Boton_001;
            Cmd_Ok = frmState.Cmd_Ok;
            Tx_Prty = frmState.Tx_Prty;
            Lt_Pln = frmState.Lt_Pln;
        }

        public void Update(UI_frmganu frmState)
        {
            if (frmState == null)
            {
                frmState = new UI_frmganu();
            }
            Update(frmState.Tx_NroOpe_000, Tx_NroOpe_0);
            Update(frmState.Tx_NroOpe_001, Tx_NroOpe_1);
            Update(frmState.Tx_NroOpe_002, Tx_NroOpe_2);
            Update(frmState.Tx_NroOpe_003, Tx_NroOpe_3);
            Update(frmState.Tx_NroOpe_004, Tx_NroOpe_4);
            frmState.Co_Boton_000 = Co_Boton_0;
            frmState.Co_Boton_001 = Co_Boton_1;
            frmState.Cmd_Ok = Cmd_Ok;

            Update(frmState.Tx_Prty, Tx_Prty);
            Update(frmState.Lt_Pln, Lt_Pln);

        }

        public GanuViewModel(UI_frmganu frmState, string accionARedireccionar)
            : this(frmState)
        {
            if (!string.IsNullOrEmpty(accionARedireccionar))
            {
                //transformo el Action a URL
                UrlHelper helper = new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext);
                this.Redireccionar = helper.Action(accionARedireccionar);
            }
        }
    }
}