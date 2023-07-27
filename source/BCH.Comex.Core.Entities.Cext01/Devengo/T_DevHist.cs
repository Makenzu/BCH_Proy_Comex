using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.Devengo
{
    public class T_DevHist
    {
        public IList<decimal> periodos { get; set; }
        public IList<T_Deveng> Deveng { get; set; }
        public int periodoSeleccionado { get; set; }

        public T_DevHist()
        {
            periodos = new List<decimal>();
            Deveng = new List<T_Deveng>();
        }
    }

    public class T_Deveng
    {
        public Int64 Operacion { get; set; }
        public int NumNeg { get; set; }
        public int moneda { get; set; }
        public double Mtovigente { get; set; }
        public double Tasa { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public int Dias { get; set; }
        public double TipCambio { get; set; }
        public double MtoInter { get; set; }
        public string rut { get; set; }
        public Int64 CtaIng { get; set; }
        public Int64 CtaIngXCob { get; set; }
        public double MtoNac { get; set; }
        public double periodo { get; set; }
    }
}
