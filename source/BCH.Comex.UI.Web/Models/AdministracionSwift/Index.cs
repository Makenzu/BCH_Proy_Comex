using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.AdministracionSwift
{
    public class Index
    {
        public SelectList TodasLasCasillas {get; set; }
        public List<string> IdsCasillasVisibles { get; set; }
        public string IdCasillaDefault { get; set; }
        public IList<ResultadoBusquedaSwift> SwiftsResultado { get; set; }
        public short PageSize { get; set; }
        public List<UI_Message> Mensajes { get; set; }

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

       


        public Index()
        {
            this.IdsCasillasVisibles = new List<string>();
            this.Mensajes = new List<UI_Message>();
        }
    }
}