using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Models.AutorizacionSwift
{
    public class SolicitarFirmasViewModel
    {
        public int IdMensaje { get; set; }
        public bool NecesitaConfirmacion { get; set; }
        public IList<sw_msgsend_firma> FirmasSolicitadas { get; set; }
        public IList<sw_msgsend_firma> FirmasEliminadas { get; set; }
        public IList<PoderUsuario> FirmasLocales { get; set; }
        public int RutAis { get; set; }
        public int ruta { get; set; }
        public int CasillaMensaje { get; set; }
        public IList<SolicitarFirmasViewModel> Multiple { get; set; }

        public SolicitarFirmasViewModel()
        {
            this.FirmasSolicitadas = new List<sw_msgsend_firma>();
            this.FirmasEliminadas = new List<sw_msgsend_firma>();
            this.FirmasLocales = new List<PoderUsuario>();
            NecesitaConfirmacion = false;
            IdMensaje = -1;
            RutAis = -1;
            ruta = -1;
            CasillaMensaje = -1;
            Multiple = new List<SolicitarFirmasViewModel>();
        }
    }
}
