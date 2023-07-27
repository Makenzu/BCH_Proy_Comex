using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Portal;

namespace BCH.Comex.UI.Web.Areas.InicioDia
{
    //clase para poder guardar en session en una sola key las distintas propiedades del usuario logueado
    public class SessionDatosUsuario
    {
        public IDatosUsuario Configuracion { get; set; }
        public sce_usr UsuarioImpersonado { get; set; }     //Corresponde al CCtUsr, es el usuario que se esta impersonando o el propio usuario
        public sce_usr UsuarioOriginal { get; set; } //Corresponde a CCtUsro, o usuario original, según sus credenciales de Windows
    }
}