
namespace BCH.Comex.Core.Entities.Swift
{
    public class LineaDetalleMensajeSwift
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }

        public bool EsNuevaLineaDeCampo { get; set; }
        public bool TieneError { get; set; }

        public LineaDetalleMensajeSwift()
        {
            Codigo = string.Empty;
            Descripcion = string.Empty;
            Detalle = string.Empty;
        }
    }
}
