using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.Entities.Cext01
{
    public class ReporteContable
    {
        public sce_mch_s01_MS_Result Cabecera { get; set; }
        public IList<sce_mcd> Lineas { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<sce_mcd> LineasOrdenada()
        {
            IList<sce_mcd> Linea = this.Lineas.OrderByDescending(c => c.codmon).ToList();
            return Linea;
        }

        public IList<LineaTotal> CalcularTotales()
        {
            Dictionary<int, LineaTotal> diccionarioTotales = new Dictionary<int, LineaTotal>();

            foreach (sce_mcd linea in this.Lineas)
            {
                LineaTotal lineaTotal = null;
                if (diccionarioTotales.ContainsKey((int)linea.codmon))
                {
                    lineaTotal = diccionarioTotales[(int)linea.codmon];
                }
                else
                {
                    lineaTotal = new LineaTotal() { CodMoneda = (int)linea.codmon, NemMoneda = linea.nemmon };
                    diccionarioTotales.Add(lineaTotal.CodMoneda, lineaTotal);
                }

                if (linea.cod_dh == "D")
                {
                    lineaTotal.TotalDebe += linea.mtomcd;
                }
                else if(linea.cod_dh == "H")
                {
                    lineaTotal.TotalHaber += linea.mtomcd;
                }
            }
            diccionarioTotales = (Dictionary<int, LineaTotal>)diccionarioTotales.OrderByDescending(key => key.Key).ToDictionary(x => x.Key, x => x.Value);
            return diccionarioTotales.Values.ToList();
        }

        public class LineaTotal
        {
            public int CodMoneda { get; set; }
            public string NemMoneda { get; set; }
            public decimal TotalDebe { get; set; }
            public decimal TotalHaber { get; set; }
        }
    }
}
