using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.GestionControlSwift
{
    public class GestionEnviados
    {
        public SelectList TodasLasCasillas { get; set; }
        public List<string> IdsCasillasVisibles { get; set; }
        public string IdCasillaDefault { get; set; }
        public IList<ResultadoBusquedaSwift> SwiftsResultado { get; set; }
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

        public GestionEnviados()
        {
            this.IdsCasillasVisibles = new List<string>();
        }
    }
}