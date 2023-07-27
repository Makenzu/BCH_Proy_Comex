using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Sbcor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Sbcor
{
    public class BancoRepository : GenericRepository<sce_bic, sbcorEntities>
    {
        public BancoRepository(sbcorEntities context): base(context) {}

        public int Count(string swift, string pais, string ciudad, string banco, string direccion, string postal)
        {
            using (var trace = new Tracer())
            {
                int cantidad = 0;
                try
                {
                    cantidad = EjecutarSP<int>("pro_sbc_bancos_count_MS", swift, pais, ciudad, banco, direccion, postal).FirstOrDefault();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta",e);
                    throw;
                }
                return cantidad;
            }
        }

        public List<sce_bic> List(string swift, string pais, string ciudad, string banco, string direccion, string postal)
        {
            using (var trace = new Tracer())
            {
                List<sce_bic> bancos = new List<sce_bic>();
                try
                {
                    bancos = EjecutarSP<sce_bic>("pro_sbc_bancos_MS", swift, pais, ciudad, banco, direccion, postal).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }
                return bancos;
            }
        }        
    }
}
