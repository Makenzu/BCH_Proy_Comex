using BCH.Comex.Core.Entities.Cext01.Supervisor;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Models
{
    public class CambioClaveViewModel : SupervisorViewModel
    {

        [Required(ErrorMessage = "Campo Requerido")]
        [DataType(DataType.Password)]
        public string claveActual { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DataType(DataType.Password)]
        public string nuevaClave { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DataType(DataType.Password)]
        public string nuevaClaveComp { get; set; }


        public CambioClaveViewModel() { }

        public CambioClaveViewModel(DatosGlobales globales)
        {
            claveActual = globales.FrmCamCl.claveActual;
            nuevaClave = globales.FrmCamCl.nuevaClave;
            nuevaClaveComp = globales.FrmCamCl.nuevaClaveComp;
            this.ListaErrores = globales.ListaMensajesError;
        }

        public void Update(CambioClaveViewModel ccvm, DatosGlobales globales)
        {
            globales.FrmCamCl.claveActual = ccvm.claveActual.Trim();
            globales.FrmCamCl.nuevaClave = ccvm.nuevaClave.Trim();
            globales.FrmCamCl.nuevaClaveComp = ccvm.nuevaClaveComp.Trim();
        }

    }
}