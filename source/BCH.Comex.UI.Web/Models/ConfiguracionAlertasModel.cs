using System.ComponentModel;

namespace BCH.Comex.UI.Web.Models
{
    //OJO, este modelo se usa en EnvioSwift pero tambien se utiliza desde AdminSwift
    public class ConfiguracionAlertasModel
    {
        [DisplayName("Intervalo inspeccionar casilla (minutos)")]
        public int intervaloMinutos { set; get; }
        [DisplayName("Desactivar Alarma")]
        public bool noRecibirAlertas { set; get; }

        public ConfiguracionAlertasModel(int minutos)
        {
            this.intervaloMinutos = minutos;
            if (minutos > 0)
            {
                noRecibirAlertas = false;
            }
            else
            {
                noRecibirAlertas = true;
            }
        }
    }
}