using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public  class ReversarOperacionDeclaracionViewModel:ViewModel
    {
        public string Caption { get; set; }
        public string Tag { get; set; }
        public UI_TextBox CAM_Clausula { get; set; }
        public UI_TextBox CAM_Comisiones { get; set; }
        public UI_TextBox CAM_Otros { get; set; }
        public UI_TextBox CAM_Liquido { get; set; }
        public UI_Button Aceptar { get; set; }
        public UI_Button Cancelar { get; set; }

        public ReversarOperacionDeclaracionViewModel(UI_Frm_Declaracion form) {
            
            this.CAM_Clausula = form.CAM_Clausula;
            this.CAM_Clausula.Tag  = form.CAM_Clausula.Tag;

            this.CAM_Comisiones = form.CAM_Comisiones;
            this.CAM_Comisiones.Tag = form.CAM_Comisiones.Tag;

            this.CAM_Otros = form.CAM_Otros;
            this.CAM_Otros.Tag = form.CAM_Otros.Tag;

            this.CAM_Liquido = form.CAM_Liquido;
            this.CAM_Liquido.Tag = form.CAM_Liquido.Tag;
            
            this.Aceptar = form.Aceptar;
            this.Cancelar = form.Cancelar;
            this.Caption = form.Caption;
        }
    }
}