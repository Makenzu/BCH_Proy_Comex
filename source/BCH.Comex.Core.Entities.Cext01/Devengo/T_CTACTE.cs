using System;
using System.Collections.Generic;


namespace BCH.Comex.Core.Entities.Cext01.Devengo
{
    public class T_CTACTE
    {
        public string txtNemCta { get; set; }
        public string txtNumCta { get; set; }
        public string txtCentroCosto { get; set; }
        public string txtCuentaCorriente { get; set; }
        public string fechaDesde { get; set; }
        public string fechaHasta { get; set; }
        public tipoFiltro optSeleccionada { get; set; }
        public IList<T_DvgCta> DvgCta { get; set; }

        public T_CTACTE(string nemCta, string numCta, string cenCos, string ctacte, string fecIni, string fecHasta, int opt )
        {
            this.txtNemCta = nemCta ?? string.Empty;
            this.txtNumCta = numCta ?? string.Empty;
            this.txtCentroCosto = cenCos ?? string.Empty;
            this.txtCuentaCorriente = ctacte ?? string.Empty;
            this.fechaDesde = fecIni ?? string.Empty;
            this.fechaHasta = fecHasta ?? string.Empty;
            this.optSeleccionada = (tipoFiltro)opt;
            DvgCta = new List<T_DvgCta>();
        }
    }

    public enum tipoFiltro
    {
        NemCta = 1,
        NumCta = 2,
        Todos = 3
    }

    public class T_DvgCta
    {
        public Int64 Operacion{ get; set; }
        public int Codneg{ get; set; }
        public int NroRpt{ get; set; }
        public DateTime FecMov{ get; set; }
        public int Cencos{ get; set; }
        public string codusr{ get; set; }
        public int NroImp{ get; set; }
        public string nemcta{ get; set; }
        public string numcta{ get; set; }
        public string nemmon{ get; set; }
        public double mtomcd{ get; set; }
        public string cod_dh{ get; set; }
        public string rutcli{ get; set; }
        public Int64 numcct { get; set; }
        public double Mtotas{ get; set; }
        public int OfiDes{ get; set; }
        public double NumPar{ get; set; }
        public int tipmov{ get; set; }
    }
    
}
