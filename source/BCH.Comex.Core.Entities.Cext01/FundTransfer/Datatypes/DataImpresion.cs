using System.Collections.Generic;
using System;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes
{
    public class DataImpresion
    {
        public string NumeroOperacion { set; get; }
        public decimal CodigoDocumento { set; get; }
        public decimal NumeroCorrelativo { set; get; }
        public bool Primero { set; get; }
        public bool Segundo { set; get; }
        public bool Tercero { set; get; }
        public bool Cuarto { set; get; }
        public string URL { set; get; }

        public int tipoDoc { get; set; }
        public string nroReporte { get; set; }
        public DateTime fechaOp { get; set; }
        public string nroMensaje { get; set; }
        public string replace { get; set; }
        public string with { get; set; }
        public string nroPresentacion { get; set; }
        public string fileName { get; set; }

        public Dictionary<string,string> Datos { set; get; }

        public DataImpresion()
        {
            Datos = new Dictionary<string, string>();
        }
    }
}
