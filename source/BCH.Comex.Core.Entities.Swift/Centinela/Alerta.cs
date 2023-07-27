
namespace BCH.Comex.Core.Entities.Swift.Centinela
{
    public class Alerta
    {
        public ConfiguracionAlerta.AplicacionEmiteAlertas Aplicacion { get; set; }
        public bool Leida { get; set; }
        public string RutUsuario { get; set; }
        public int CantidadMensajes { get; set; }
        public string Texto { get; set; }
    }
}
