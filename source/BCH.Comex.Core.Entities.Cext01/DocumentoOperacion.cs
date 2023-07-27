using System;

namespace BCH.Comex.Core.Entities.Cext01
{
    public class DocumentoOperacion
    {
        public enum TipoDocEnum: byte
        {
            SinEspecificar = 0,
            Carta = 1,
            Swift = 2,
            Contabilidad = 3
        }
        
        public string CodCct { get; set; }
        public string CodPro { get; set; }
        public string CodEsp { get; set; }
        public string CodOfi { get; set; }
        public string CodOpe { get; set; }
        public int NroRpt { get; set; }
        
        public DateTime FechaOperacion { get; set; }
        public string CodigoPropio { get; set; }
        public TipoDocEnum TipoDoc { get; set; }
        
        public string DescripcionDoc { get; set; }
        public string DescripcionProducto { get; set; }
        public short TipoSwift { get; set; }

        public string NroOperacion
        {
            get
            {
                return this.CodCct + "-" + this.CodPro + "-" + this.CodEsp + "-" + this.CodOfi + "-" + this.CodOpe;
            }
        }

        public string TipoDocDesc
        {
            get
            {
                switch(this.TipoDoc)
                {
                    case DocumentoOperacion.TipoDocEnum.Contabilidad:
                        return "Contabilidad";

                    case DocumentoOperacion.TipoDocEnum.Swift:
                        return "Swift";

                    case DocumentoOperacion.TipoDocEnum.Carta:
                    default:
                        return "Carta";

                }
            }
        }
    }
}
