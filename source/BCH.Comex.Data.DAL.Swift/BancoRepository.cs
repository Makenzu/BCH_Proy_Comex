using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class BancoRepository : GenericRepository<sw_bancos, swiftEntities>
    {
        public BancoRepository(swiftEntities context)
            : base(context)
        {

        }

        public IList<sw_bancos> GetBancosByCodAndBranch(string codBanco, string branch)
        {
            return context.proc_trae_bancos_MS(codBanco, branch).ToList();
        }

        public string DesencriptaMensajeS_MS(int valor, string llave)
        {
            return context.Database.SqlQuery<string>("exec dbo.DesencriptaMensajeS_MS @valor, @llave", new SqlParameter("valor", valor), new SqlParameter("llave", llave)).
                FirstOrDefault();
            //return context.DesencriptaMensaje2(sesion, secuencia).First().ToString();
        }

        public string DesencriptaMensaje_MS(int sesion, int secuencia)
        {
            return context.Database.SqlQuery<string>("exec dbo.DesencriptaMensaje_MS @sesion, @secuencia", new SqlParameter("sesion", sesion), new SqlParameter("secuencia", secuencia)).
                FirstOrDefault();
            //return context.DesencriptaMensaje2(sesion, secuencia).First().ToString();
        }

        public int GetCorrelativo()
        {
            return context.Database.SqlQuery<int>("exec proc_sw_trae_folio_MS \"ENVIO\" ").FirstOrDefault();
        }

        public IList<sw_bancos> proc_sw_trae_bancos(string swift, string pais, string ciudad, string banco, string direccion, string branch = "")
        {
            var result = EjecutarSP<sw_bancos>("proc_sw_trae_bancos2_MS", swift, pais, ciudad, banco, direccion, branch).ToList();

            return result;
        }

        public int? proc_sw_count_bancos(string swift, string pais, string ciudad, string banco, string direccion, string branch = "", string intercambioClave = "")
        {
            var result =  context.proc_sw_count_bancos2_MS(swift, pais, ciudad, banco, direccion, branch, intercambioClave).SingleOrDefault();
            return result;
        }
    }
}
