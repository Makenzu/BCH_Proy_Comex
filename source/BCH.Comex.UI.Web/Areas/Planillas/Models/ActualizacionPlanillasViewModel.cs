using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class ActualizacionPlanillasViewModel
    {
        public ActualizacionPlanillasViewModel()
        {
            Declaracion = new DeclaracionPlanillaViewModel();
            Listado = new ListadoActualizacionPlanillasViewModel();
        }

        [Required]
        public DeclaracionPlanillaViewModel Declaracion { get; set; }

        public ListadoActualizacionPlanillasViewModel Listado { get; set; }
       
    }
}