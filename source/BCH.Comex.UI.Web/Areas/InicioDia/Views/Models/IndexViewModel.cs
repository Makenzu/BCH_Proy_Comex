using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.InicioDia.Views.Models
{
    public class IndexViewModel
    {
        public SessionDatosUsuario InfoUsuario { get; set; }
        public string DescCCActual { get; set; }
        public IList<UI_Message> Mensajes { get; set; }
        
        public IList<sce_usr> Especialistas { get; set; }

        public string CCYCodUsrImpersonado 
        { 
            get
            {
                return this.InfoUsuario.UsuarioImpersonado.cent_costo + "-" + this.InfoUsuario.UsuarioImpersonado.id_especia; 
            }
        }

    }
}