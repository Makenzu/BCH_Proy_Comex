using BCH.Comex.Core.Entities.Cext01.Supervisor;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.Supervisor.Models
{
    public class DocumentosValoradosViewModel : SupervisorViewModel
    {
        public string nroFolio { get; set; }
        public string fechaEmision { get; set; }
        public List<T_Chq> ltCheque { get; set; }
        public List<T_Vvi> ltValeVista { get; set; }

        public DocumentosValoradosViewModel() { }

        public DocumentosValoradosViewModel(DatosGlobales globales)
        {
            this.ListaErrores = globales.ListaMensajesError;
        }

       


    }
}