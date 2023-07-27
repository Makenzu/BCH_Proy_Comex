using BCH.Comex.Common.Exceptions;
using BCH.Comex.Core.BL.Portal.Users;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Portal;
using System.Collections.Generic;
using System.Linq;
using BCH.Comex.Common.ExceptionHandling;
using System;
using System.Reflection;

namespace BCH.Comex.Core.BL.Portal
{
    public class PortalService
    {
        private UnitOfWorkPortal unitOfWork;
        private UserMembershipProvider userMembershipProvider;
   
        public PortalService()
        {
            this.unitOfWork = new UnitOfWorkPortal();
            this.userMembershipProvider= new UserMembershipProvider();
        }

        internal PortalService(UnitOfWorkPortal unitOfWork, UserMembershipProvider userMembershipProvider = null)
        {
            this.unitOfWork = unitOfWork;
            this.userMembershipProvider = userMembershipProvider ?? new UserMembershipProvider();
        }

        public List<GrupoAplicacion> ListGrupos() 
        {
            return unitOfWork.GruposAplicacionesRepository.GetAll().OrderBy(p => p.name).ToList();
        }

        public Dictionary<GrupoAplicacion, List<Aplicacion>> GetUserApps(ComexUser user, bool refresh = false) {
            var groups = userMembershipProvider.GetGroups(user, refresh);

            var availableApps = unitOfWork.AplicacionesRepository.GetAll().Where(app => groups == null || groups.Contains(app.ad_group_name));
            return availableApps.Where(a => a.id != "XGPT").GroupBy(app => app.Grupo).ToDictionary(appGroup => appGroup.Key, appGroup => appGroup.ToList());// eliminar where cuando haya que habilitar tasas paridades
        }

        public IDatosUsuario GetDatosUsuario(ComexUser user)
        {
            var retorno = unitOfWork.DatosUsuarioRepository.GetDatosUsuario(user.Name.ToUpper());
            try
            {
                if (retorno == null && string.IsNullOrEmpty(user.Name))
                {
                    throw new ComexUserException(string.Format("Su sesión ha expirado, favor volver a ingresar al portal.", user.Name));
                }
                else if (retorno == null)
                {
                    throw new ComexUserException(string.Format("Su usuario '{0}' no tiene datos de inicialización", user.Name));
                }

                if (string.IsNullOrEmpty(retorno.CodBCCH) || string.IsNullOrEmpty(retorno.CodBCH) || string.IsNullOrEmpty(retorno.CodPBC) || string.IsNullOrEmpty(retorno.SucBCH))
                {
                    throw new ComexUserException(string.Format("Su usuario '{0}' no tiene los datos de los códigos de las sucursales", user.Name));
                }
            }
            catch (Exception exc)
            {
                if (ExceptionPolicy.HandleException(exc, "PoliticaBLFundTransfer")) throw;                
            }
            
            return DatosUsuarioDTO.ToDTO(retorno);
        }

        public void CambiarCCtUsr(IDatosUsuario usuario)
        {
            DatosUsuario aModificar = unitOfWork.DatosUsuarioRepository.GetByID(usuario.samAccountName); //lo cargo de nuevo ya que no lo tengo en este contexto
            aModificar.Identificacion_CCtUsr = usuario.Identificacion_CCtUsr;
            
            unitOfWork.DatosUsuarioRepository.Update(aModificar);
            unitOfWork.Save();
        }

        public void CambiarBCHComexSwem_Casillas(IDatosUsuario usuario)
        {
            DatosUsuario aModificar = unitOfWork.DatosUsuarioRepository.GetByID(usuario.samAccountName); //lo cargo de nuevo ya que no lo tengo en este contexto
            aModificar.BCHComexSwem_Casillas = usuario.BCHComexSwem_Casillas;

            unitOfWork.DatosUsuarioRepository.Update(aModificar);
            unitOfWork.Save();
        }

        public void CambiarFirmasLocales(IDatosUsuario usuario)
        {
            DatosUsuario aModificar = unitOfWork.DatosUsuarioRepository.GetByID(usuario.samAccountName); //lo cargo de nuevo ya que no lo tengo en este contexto
            aModificar.FirmasLocales = usuario.FirmasLocales;

            unitOfWork.DatosUsuarioRepository.Update(aModificar);
            unitOfWork.Save();
        }

        public void CambiarMinsAlertaEnvioSwift(IDatosUsuario usuario)
        {
            DatosUsuario aModificar = unitOfWork.DatosUsuarioRepository.GetByID(usuario.samAccountName); //lo cargo de nuevo ya que no lo tengo en este contexto
            aModificar.MinsAlertaEnvioSwift = usuario.MinsAlertaEnvioSwift;

            unitOfWork.DatosUsuarioRepository.Update(aModificar);
            unitOfWork.Save();
        }

        public void CambiarMinsAlertaAdminEnvioSwift(IDatosUsuario usuario)
        {
            DatosUsuario aModificar = unitOfWork.DatosUsuarioRepository.GetByID(usuario.samAccountName); //lo cargo de nuevo ya que no lo tengo en este contexto
            aModificar.MinsAlertaAdminEnvioSwift = usuario.MinsAlertaAdminEnvioSwift;

            unitOfWork.DatosUsuarioRepository.Update(aModificar);
            unitOfWork.Save();
        }

        public void CambiarMinsAlertaRecepcionSwift(IDatosUsuario usuario)
        {
            DatosUsuario aModificar = unitOfWork.DatosUsuarioRepository.GetByID(usuario.samAccountName); //lo cargo de nuevo ya que no lo tengo en este contexto
            aModificar.MinsAlertaRecepcionSwift = usuario.MinsAlertaRecepcionSwift;

            unitOfWork.DatosUsuarioRepository.Update(aModificar);
            unitOfWork.Save();
        }

        public void CambiarConfigImpres_PrintFormat(IDatosUsuario usuario)
        {
            DatosUsuario aModificar = unitOfWork.DatosUsuarioRepository.GetByID(usuario.samAccountName); //lo cargo de nuevo ya que no lo tengo en este contexto
            aModificar.ConfigImpres_PrintFormat = usuario.ConfigImpres_PrintFormat;

            unitOfWork.DatosUsuarioRepository.Update(aModificar);
            unitOfWork.Save();
        }

        public void RegisterApp(string appId, string appName, string groupName, string adFunctionalGroup, string groupId, string controller, string action = null, string parameters=null) {
            var group = unitOfWork.GruposAplicacionesRepository.GetByID(groupId);
            if (group == null)
            {
                group = new GrupoAplicacion() { id = groupId, name = groupName };
                unitOfWork.GruposAplicacionesRepository.Insert(group);
            }

            var app = unitOfWork.AplicacionesRepository.GetByID(appId);
            bool isMissing = app == null;

            if (isMissing)
                app = new Aplicacion() { id = appId, name = appName, ad_group_name = adFunctionalGroup, grupo_id = groupId };
           
            app.name = appName;
            app.ad_group_name = adFunctionalGroup;
            app.grupo_id = groupId;
            app.controller = controller;
            app.action = action;
            app.parameters = parameters;
            

            if (isMissing)
                unitOfWork.AplicacionesRepository.Insert(app);
            else
                unitOfWork.AplicacionesRepository.Update(app);

            unitOfWork.Save();
        }

        private object CloneObject(object o)
        {
            Type t = o.GetType();
            PropertyInfo[] properties = t.GetProperties();

            Object p = t.InvokeMember("", System.Reflection.BindingFlags.CreateInstance,
                null, o, null);

            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    pi.SetValue(p, pi.GetValue(o, null), null);
                }
            }

            return p;
        }

        /// <summary>
        /// Buscar parametros en la tabla tbl_sce_tabcomex_vchar
        /// </summary>
        /// <param name="parametro">Nombre del parametro</param>
        /// <returns></returns>
        public string BuscarParametro(string parametro)
        {
            string Resultado = string.Empty;
            if (!string.IsNullOrEmpty(parametro))
            {
                Resultado = unitOfWork.AplicacionesRepository.tbl_sce_tabcomex(parametro);
            }
            return Resultado;
        }

        /// <summary>
        /// Validamos si el usuario activo, cuenta con codigo de sucursal configurado.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool ValidarSucursalUsuario(IDatosUsuario usuario)
        {            
            var retorno = unitOfWork.DatosUsuarioRepository.GetDatosUsuario(usuario.samAccountName.ToUpper());
            if (string.IsNullOrEmpty(retorno.CodBCCH) || string.IsNullOrEmpty(retorno.CodBCH) || string.IsNullOrEmpty(retorno.CodPBC) || string.IsNullOrEmpty(retorno.SucBCH))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
