using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01
{
    public partial class sce_usr
    {
        public IList<string> ListaReemplazos { get; set; }
        public string Oficina { get; set; }
        
        public string CentroCostoYCodUsr
        {
            get
            {
                return this.cent_costo + "-" + this.id_especia;
            }
        }

        public string CentroCostoYCodUsrSupervisor
        {
            get
            {
                return this.cent_super + "-" + this.id_super;
            }
        }
    }
}
