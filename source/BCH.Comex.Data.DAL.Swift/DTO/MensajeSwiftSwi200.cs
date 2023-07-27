
namespace BCH.Comex.Data.DAL.Swift.DTO
{
    public class MensajeSwiftSwi200
    {
        //private char tipoIngreso;
        private string mensajeSwift;
        private const string TEXTO_BENEFICIARIO = "59";
        private const string TEXTO_REFERENCIA = "20";

        public MensajeSwiftSwi200(int idMensaje, int rutDigitador, int casilla, string moneda, double monto, char tipoIngreso, string mensajeSwift)
        {
            this.IdMensaje = idMensaje;
            this.RutDigita = rutDigitador;
            this.Casilla = casilla;
            this.Unidad = casilla;
            this.Moneda = moneda;
            this.Monto = monto;
            this.TipoIngreso = tipoIngreso.ToString();
            this.mensajeSwift = mensajeSwift;
            this.SubTexto = mensajeSwift;
            this.TipoMensaje = "MT" + mensajeSwift.Substring(34, 3);
            this.BancoEm = mensajeSwift.Substring(7, 8);
            this.BranchEm = mensajeSwift.Substring(16, 3);
            this.Prioridad = mensajeSwift.Substring(49, 1);
            this.BancoRe = mensajeSwift.Substring(37, 8);
            this.BranchRe = mensajeSwift.Substring(46, 3);
            this.Beneficiario = ObtenerFila(mensajeSwift, TEXTO_BENEFICIARIO);
            this.Referencia = ObtenerFila(mensajeSwift, TEXTO_REFERENCIA);
            this.Texto = mensajeSwift;
        }

        private string ObtenerFila(string texto, string id)
        {
            id = ':' + id + ':';
            string[] filas = texto.Split('\n');
            for (int i = 0; i < filas.Length; i++)
            {
                if (filas[i].StartsWith(id))
                {
                    return filas[i].Replace(id, " ").Trim();
                }
            }
            return string.Empty;
        }
        public string Texto { get; set; }
        public string SubTexto { get; set; }

        public string Rut { get; set; }
        public string Vista { get; set; }
        public string BaseDatos { get; set; }
        public int RutDigita { get; set; }
        public int Casilla { get; set; }
        public string Moneda { get; set; }
        public double Monto { get; set; }
        public int IdMensaje { get; set; }
        public string Operacion { get; set; }
        public string TipoIngreso { get; set; }
        public int Sesion { get; set; }
        public int Secuencia { get; set; }
        public int Unidad { get; set; }
        public string TipoMensaje { get; set; }
        public string Prioridad { get; set; }
        public string EstadoMensaje { get; set; }
        public string BancoRe { get; set; }
        public string BranchRe { get; set; }
        public string BancoEm { get; set; }
        public string BranchEm { get; set; }
        public string Referencia { get; set; }
        public string Beneficiario { get; set; }
        public string Comentario { get; set; }
    }
}
