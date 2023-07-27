// INICIO MODIFICACION CNC - ACCENTURE
using BCH.Comex.Common.UI_Modulos;

namespace BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos
{
    public class UI_PrtEnt14
    {
        public UI_TextBox txClasiRiesgo { get; set; }
        public UI_TextBox txActividadEconomica { get; set; }
        public UI_TextBox txTipoEvaluRiesgo { get; set; }
        public UI_TextBox txComposicionInstitucional { get; set; }
        public UI_TextBox txTipoClienteNormativo { get; set; }
        public UI_Button BtnAceptar { get; set; }
        public UI_Button BtnCancelar { get; set; }

        public int tipo { get; set; }
        public int modrut { get; set; }

        public UI_PrtEnt14()
        {
            this.txClasiRiesgo = new UI_TextBox();
            this.txActividadEconomica = new UI_TextBox();
            this.txTipoEvaluRiesgo = new UI_TextBox();
            this.txComposicionInstitucional = new UI_TextBox();
            this.txTipoClienteNormativo = new UI_TextBox();
            this.BtnCancelar = new UI_Button();
            this.BtnAceptar = new UI_Button();
        }
    }
}
// FIN MODIFICACION CNC - ACCENTURE