using BCH.Comex.Core.Entities.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class UsuarioRepository : GenericRepository<sce_usr, cext01Entities>
    {
        public UsuarioRepository(cext01Entities context)
            : base(context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="idEspecialista"></param>
        /// <returns></returns>
        public sce_usr GetUsuario(string cc, string idEspecialista)
        {
            return context.sce_usr.Where(u => u.cent_costo == cc && u.id_especia == idEspecialista).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="centroCosto"></param>
        /// <param name="especialista"></param>
        /// <returns></returns>
        public IList<sce_usr> GetEspecialistasPorCentroDeCosto(string centroCosto = "", string especialista = "")
        {
            IList<sce_usr> result = null;
            /// Obtener todos los de mi centro de costo
            if (string.IsNullOrEmpty(centroCosto))
            {
                result = context.sce_usr.OrderBy(u => u.cent_costo).ThenBy(u => u.id_especia).ToList();
            }
            /// Obtener solo aquellos puedo suplantar
            else if (!string.IsNullOrEmpty(centroCosto) && !string.IsNullOrEmpty(especialista))
            {
                result = context.sce_usr.Where(u => u.reemplazos.Contains(centroCosto+ especialista)).OrderBy(u => u.cent_costo).ThenBy(u => u.id_especia).ToList();
                result.Add(context.sce_usr.Where(c => c.cent_costo == centroCosto && c.id_especia == especialista).FirstOrDefault());
                result = result.OrderBy(u => u.cent_costo).ThenBy(u => u.id_especia).ToList();
            }
            /// Obtener todos los especialistas
            else
            {
                result = context.sce_usr.Where(u => u.cent_costo == centroCosto).OrderBy(u => u.cent_costo).ThenBy(u => u.id_especia).ToList();
            }

            foreach (sce_usr usr in result)
            {
                usr.cent_costo = usr.cent_costo.Trim();
                usr.cent_super = usr.cent_super.Trim();
                usr.ciudad = usr.ciudad.Trim();
                usr.clasup = usr.clasup.Trim();
                usr.comuna = usr.comuna.Trim();
                usr.direccion = usr.direccion.Trim();
                usr.fax = usr.fax.Trim();
                usr.id_especia = usr.id_especia.Trim();
                usr.id_super = usr.id_super.Trim();
                usr.impresora = usr.impresora.Trim();
                usr.nombre = usr.nombre.Trim();
                usr.ofixusr = usr.ofixusr.Trim();
                usr.reemplazos = usr.reemplazos.Trim();
                usr.rut = usr.rut.Trim();
                usr.seccion = usr.seccion.Trim();
                usr.swift = usr.swift.Trim();
                usr.telefono = usr.telefono.Trim();
                usr.tipeje = usr.tipeje.Trim();
            }

            return result; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="centroCosto"></param>
        /// <param name="codusr"></param>
        /// <returns></returns>
        public sce_usr_s04_MS_Result GetFechasUsuario(string centroCosto, string codusr)
        {
            return EjecutarSP<sce_usr_s04_MS_Result>("sce_usr_s04_MS", centroCosto, codusr).FirstOrDefault();
        }
    }
}
