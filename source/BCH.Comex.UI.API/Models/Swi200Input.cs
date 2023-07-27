
namespace BCH.Comex.UI.API.Models
{
    public class Swi200Input
    {
        public int Vista { get; set; }
        public int IdMensaje { get; set; }
        public int RutDigitador { get; set; }
        public int Casilla { get; set; }
        public string Moneda { get; set; }
        public double Monto { get; set; }
        public char TipoIngreso { get; set; }
        public string MensajeSwift { get; set; }
    }
}