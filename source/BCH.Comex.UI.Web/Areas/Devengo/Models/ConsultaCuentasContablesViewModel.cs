using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.Devengo.Models
{
    public class ConsultaCuentasContablesViewModel: DevengoViewModel
    {
        public IList<UI_OptionItem> opt_Filtros { set; get; }
        public int FiltroSelected { get; set; }
        public UI_TextBox txtNumcta { get; set; }
        public UI_TextBox txtCentroCosto { get; set; }
        public UI_TextBox txtNemcta { get; set; }
        public UI_TextBox txtCuentaCorriente { get; set; }
        public UI_TextBox txtFechaHasta { get; set; }
        public UI_TextBox txtFechaDesde { get; set; }

        public ConsultaCuentasContablesViewModel()
        {
            opt_Filtros = new List<UI_OptionItem>() { 
                new UI_OptionItem() {Value = "Nem. Cta.", ID = "1"},
                new UI_OptionItem() {Value = "Num. Cta.", ID = "2"},
                new UI_OptionItem() {Value = "Todos" , ID = "3"}
            };
            FiltroSelected = 3;
            txtNumcta = new UI_TextBox() { Enabled = false};
            txtCentroCosto = new UI_TextBox() { Enabled = false };
            txtNemcta = new UI_TextBox() { Enabled = false };
            txtCuentaCorriente = new UI_TextBox();
            txtFechaHasta = new UI_TextBox();
            txtFechaDesde = new UI_TextBox();
        }

        public ConsultaCuentasContablesViewModel(ConsultaCuentasContablesViewModel model)
        {
            this.opt_Filtros = new List<UI_OptionItem>();
            foreach (var op in model.opt_Filtros)
            {
                this.opt_Filtros.Add(op);
            }
            this.FiltroSelected = model.FiltroSelected;
            this.txtCuentaCorriente = model.txtCuentaCorriente;
            this.txtCentroCosto = model.txtCentroCosto;
            this.txtNemcta = model.txtNemcta;
            this.txtNumcta = model.txtNumcta;
            this.txtFechaDesde = model.txtFechaDesde;
            this.txtFechaHasta = model.txtFechaHasta;

            this.txtNemcta.Enabled = model.FiltroSelected == 1;
            this.txtNumcta.Enabled = model.FiltroSelected == 2;
            this.txtCentroCosto.Enabled = model.FiltroSelected != 3;
        }
    }
}