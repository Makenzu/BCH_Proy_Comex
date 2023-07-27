using BCH.Comex.Core.Entities.Portal;
using System.Linq;

namespace BCH.Comex.Data.DAL.Portal
{
    public class DatosUsuarioRepository : GenericRepository<DatosUsuario, Portal.portalEntities>
    {
        public DatosUsuarioRepository(portalEntities context) : base(context)
        {
        }

        /// <summary>
        /// Obtiene los datos de configuracion del usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public DatosUsuario GetDatosUsuario(string user)
        {
            //var result = context.DatosUsuarios.AsNoTracking().FirstOrDefault(u => u.samAccountName.ToUpper() == user.ToUpper());
            var result = context.DatosUsuarios.AsNoTracking()
                .Include("tbl_datos_usuario_codigos_sucursal")
                .FirstOrDefault(u => u.samAccountName.ToUpper() == user.ToUpper());
            
            return result;
        }
    }
}
