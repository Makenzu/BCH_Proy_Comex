using BCH.Comex.Common.UI_Modulos;
//using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using System.Collections.Generic;
using System.Web.Mvc;
namespace BCH.Comex.UI.Web.Models.AdminParticipantes
{
    public class ActivarRazonViewModel : AdminParticipantesViewModel
    {
        public int IdEstado { get; set; }

        public ActivarRazonViewModel(InitializationObject initObj)
        {
            IdEstado = 0;
            MensajesDeError = initObj.Mdi_Principal.MESSAGES;
        }


    }


}