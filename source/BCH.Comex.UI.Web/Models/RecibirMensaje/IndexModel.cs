using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.RecibirMensaje
{
    public class IndexModel
    {
        public SelectList TodasLasCasillas { get; set; }
        public List<string> IdsCasillasVisibles { get; set; }
        public string IdCasillaDefault { get; set; }
        //public IList<ResultadoBusquedaSwift> SwiftsResultado { get; set; }
        public short PageSize { get; set; }
        
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
}