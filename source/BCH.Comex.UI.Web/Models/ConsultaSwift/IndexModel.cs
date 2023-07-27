using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.ConsultaSwift
{
    public class IndexModel
    {
        public string NombreCompletoUsuarioLogueado;
        public int RutUsuarioLogueado;

        public SelectList TodasLasCasillas {get; set; }
        public List<string> IdsCasillasVisibles { get; set; }
        public string IdCasillaDefault { get; set; }
        public IList<ResultadoBusquedaSwift> SwiftsResultado { get; set; }
        public short PageSize { get; set; }
        public bool FuncionalidadesExtraPermitidas { get; set; }
        public bool SoloConsulta { get; set; }
        public ConsultarParaAccion ParaAccion { get; set; }
                
        private SelectList casillasVisibles;
        
        public SelectList CasillasVisibles 
        {
            get
            {
                if (this.casillasVisibles != null)
                {
                    return casillasVisibles;
                }
                else return this.TodasLasCasillas;
            }
            set
            {
                casillasVisibles = value;
            } 
        }

        public IndexModel()
        {
            this.IdsCasillasVisibles = new List<string>();
        }
    }

    public enum ConsultarParaAccion : byte
    {
        SoloConsultarOEnviarMail = 0,
        ModificarAnular = 1,
        SolicitarFirmas = 2,
        FirmasNoSolicitadas = 3,
    }
}