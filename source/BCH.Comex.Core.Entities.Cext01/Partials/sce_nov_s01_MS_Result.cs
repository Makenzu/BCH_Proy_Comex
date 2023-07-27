using System;

namespace BCH.Comex.Core.Entities.Cext01
{
    [Serializable]
    public partial class sce_nov_s01_MS_Result
    {
        public string NombreParty { get; set; }
        public string NemonicoMoneda { get; set; }

        public string NroOperacion
        {
            get
            {
                string nroBase = this.codcct + "-" + this.codpro + "-" + this.codesp + "-" + this.codofi + "-" + this.codope;
                int codnegAux = 0;
                if (int.TryParse(this.codneg, out codnegAux) && (codnegAux != 0))
                {
                    return nroBase + "-" + codnegAux;
                }
                else
                {
                    return nroBase;
                }
            }
        }

        public string NroEspecialista
        {
            get
            {
                return this.cencos + "-" + this.codusr;
            }
        }
    }
}
