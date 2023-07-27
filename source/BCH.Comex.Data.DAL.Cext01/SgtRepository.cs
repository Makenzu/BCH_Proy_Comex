using BCH.Comex.Core.Entities.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class SgtRepository : GenericRepository<tbl_sce_fts, cext01Entities>
    {
        public SgtRepository(cext01Entities context) : base(context) { }

        /// <summary>
        /// Carga de sucursales
        /// </summary>
        /// <returns></returns>
        public IList<sgt_suc> sgt_suc_s01_MS()
        {
            return context.Database.SqlQuery<sgt_suc>("exec sgt_suc_s01_MS").ToList();
        }

        public IList<sgt_pai_s02_MS_Result> sgt_pai_s02_MS()
        {
            return this.EjecutarSP<sgt_pai_s02_MS_Result>("sgt_pai_s02_MS").ToList();
        }

        public sgt_vmd_s02_MS_Result sgt_vmd_s02_MS(DateTime? vmdfec, int? vmdcod)
        {
            var result = context.sgt_vmd_s02_MS(vmdfec, vmdcod).FirstOrDefault();

            return result;
        }
        public IList<sgt_suc_s01_MS_Result> Sgt_Suc_S01_MS()
        {
            return context.sgt_suc_s01_MS().ToList();
        }

        /// <summary>
        /// Tabla de Monedas
        /// </summary>
        /// <returns></returns>
        public IQueryable<sgt_mnd_s02_MS_Result> Sgt_Mnd_S02()
        {
            return context.sgt_mnd_s02_MS().AsQueryable();
        }
        public IList<sgt_mnd_s02_MS_Result> Sgt_Mnd_S02_MS()
        {
            return context.sgt_mnd_s02_MS().ToList();
        }

        public IList<sgt_pai_s02_MS_Result> Sgt_Pai_S02_MS()
        {
            return context.sgt_pai_s02_MS().ToList();
        }

        /// <summary>
        /// Paises
        /// </summary>
        /// <returns></returns>
        public IQueryable<sgt_pai_s02_MS_Result> Sgt_Pai_S02()
        {
            return context.sgt_pai_s02_MS().AsQueryable();
        }

        /// <summary>
        /// Localidades
        /// </summary>
        /// <returns></returns>
        public IList<sgt_loc_s01_MS_Result> Sgt_Loc_S01_MS()
        {
            return context.sgt_loc_s01_MS().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoOficina"></param>
        /// <returns></returns>
        public IList<sgt_ejc_s04_MS_Result> Sgt_Ejc_S04_MS(short codigoOficina)
        {
            return context.sgt_ejc_s04_MS(codigoOficina).ToList();

        }

        public IList<sgt_ejc_s03_MS_Result> Sgt_Ejc_S03_MS(String ejCopImp, String ejCopExp, String ejCneGoc)
        {
            return context.sgt_ejc_s03_MS(ejCopImp, ejCopExp, ejCneGoc).ToList();
        }

        public IList<sgt_clf_s01_MS_Result> Sgt_Clf_S01_MS()
        {
            return context.sgt_clf_s01_MS().ToList();
        }

        public IList<sgt_mnd_MS_Result> sgt_mnd_MS()
        {
            return context.sgt_mnd_MS().ToList();
        }

        public IList<sgt_mnd_s03_MS_Result> sgt_mnd_s03_MS()
        {
            return context.sgt_mnd_s03_MS().ToList();
        }

        // INICIO MODIFICACION CNC - ACCENTURE
        public IList<sce_campos_cla_cliente_S01_Result> sce_campos_cla_cliente_S01(byte id_campo, string cod_campo, string des_campo)
        {
            return context.sce_campos_cla_cliente_S01(id_campo, cod_campo, des_campo).ToList();
        }


        public IList<sce_cred_cla_cliente_S01_Result> sce_cred_cla_cliente_S01()
        {
            return context.sce_cred_cla_cliente_S01().ToList();
        }

        // FIN MODIFICACION CNC - ACCENTURE
    }
}
