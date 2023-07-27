using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class ConsultaParticipantesViewModel
    {
        public string RazonSocial { get; set; }
        public UI_Grid Resultados { get; set; }

        public ConsultaParticipantesViewModel()
        {

        }

        public ConsultaParticipantesViewModel(UI_Frm_Con_Participantes frmState)
        {
            RazonSocial = frmState.caja.Text;
            //Resultados = frmState.caja.
        }

        internal void Update(UI_Frm_Con_Participantes frmState)
        {
            frmState.caja.Text = RazonSocial;
        }
    }
}
