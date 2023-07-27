
namespace BCH.Comex.Core.Entities.Swift
{
    public class CampoMonto
    {
        public string Campo {get; set;}
        public short PosMto {get; set;}
        public short LenMto {get; set;}
        public short PosMnd {get;set;}
        public short LenMnd {get;set;}
        public decimal ValorMto { get; set; }
        public string ValorMnd { get; set; }
        public bool Existe { get; set; }
        public short TipoVal { get; set; }
    }
}
