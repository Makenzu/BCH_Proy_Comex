using System;

namespace BCH.Comex.UI.Web.Models.ConsultaSwift
{
    public class MensajeSwiftAdjunto
    {
        public int? IdMensaje { get; set; }
        public int Sesion { get; set; }
        public int Secuencia { get; set; }

        private const string SeparadorCampos = ";c;";

        public string GetStrIdentificacion()
        {
            if (this.IdMensaje.HasValue)
            {
                return this.Sesion + SeparadorCampos + this.Secuencia + SeparadorCampos + this.IdMensaje.Value;
            }
            else
            {
                return this.Sesion + SeparadorCampos + this.Secuencia;
            }
        }

        public void CargarDesdeStrIdentificacion(string str)
        {
            string[] parts = str.Split(new string[] { SeparadorCampos }, StringSplitOptions.RemoveEmptyEntries);
            this.Sesion = int.Parse(parts[0]);
            this.Secuencia = int.Parse(parts[1]);
            if (parts.Length == 3)
            {
                this.IdMensaje = int.Parse(parts[2]);
            }
        }
    }
}