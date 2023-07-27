using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class MantencionSwiftRepository : GenericRepository<proc_sw_trae_usuarios_MS_Result,swiftEntities>
    {
        public MantencionSwiftRepository(swiftEntities context)
            : base(context)
        {

        }

        public IList<proc_sw_trae_usuarios_MS_Result1> proc_sw_trae_usuarios_MS(proc_sw_trae_usuarios_MS_DTO users)
        {

            return context.proc_sw_trae_usuarios_MS(users.rut,users.nombre,users.tipo).ToList();
        }  
        public IList<proc_sw_trae_casillas_MS_Result> proc_sw_trae_casillas_MS(proc_sw_trae_casillas_MS_DTO casillas)
        {
            return context.proc_sw_trae_casillas_MS(casillas.codigo, casillas.nombre, casillas.origen).ToList();
        }
        public IList<proc_sw_trae_monedas_MS_Result> proc_sw_trae_monedas_MS()
        {
            return context.proc_sw_trae_monedas_MS().ToList();
        }
        public IList<proc_sw_trae_bancos2_MS_Result> proc_sw_trae_bancos2_MS(string swift, string pais, string ciudad, 
            string direccion, string banco, string branch, string intercambioClave)
        {
            return context.proc_sw_trae_bancos2_MS(swift, pais, ciudad, direccion, banco, branch, intercambioClave).ToList();
        }
        public IList<proc_sw_trae_bancos_verificados_MS_Result> proc_sw_trae_bancos_verificados_MS(string Codigo)
        {
            return context.proc_sw_trae_bancos_verificados_MS(Codigo).ToList();
        }
        public IList<proc_sw_trae_paridad_MS_Result> proc_sw_trae_paridad_MS(proc_sw_trae_paridades_MS_DTO paridades)
        {
            return context.proc_sw_trae_paridad_MS(paridades.codigo,paridades.nombre).ToList();
        }
        public IList<proc_sw_trae_valoresCampos_MS_Result> proc_sw_trae_valoresCampos_MS(proc_sw_trae_valoresCampos_MS_DTO ValoresCampos)
        {
            return context.proc_sw_trae_valoresCampos_MS(ValoresCampos.codigo).ToList();
        }
        public IList<proc_sw_trae_TiposMensajes_MS_Result> proc_sw_trae_TiposMensajes_MS(proc_sw_trae_TiposMensajes_MS_DTO tipoMensaje)
        {
            return context.proc_sw_trae_TiposMensajes_MS(tipoMensaje.codigo).ToList();
        }
        public IList<proc_sw_trae_FormatoMensajes_MS_Result> proc_sw_trae_FormatoMensajes_MS(proc_sw_trae_FormatoMensajes_MS_DTO formatoMensajes)
        {
            return context.proc_sw_trae_FormatoMensajes_MS(formatoMensajes.codigo).ToList();
        }
        public IList<proc_sw_trae_CampoMensajes_MS_Result> proc_sw_trae_CampoMensajes_MS(proc_sw_trae_CampoMensajes_MS_DTO camposMensajes)
        {
            return context.proc_sw_trae_CampoMensajes_MS(camposMensajes.codigo).ToList();
        }
        public IList<proc_sw_trae_GlosaCampos_MS_Result> proc_sw_trae_GlosaCampos_MS(proc_sw_trae_GlosaCampos_MS_DTO glosaCampos)
        {
            return context.proc_sw_trae_GlosaCampos_MS(glosaCampos.codigo).ToList();
        }
        public IList<proc_sw_trae_CaracterInvalido_MS_Result> proc_sw_trae_CaracterInvalido_MS(proc_sw_trae_CaracterInvalido_MS_DTO caracterInvalido)
        {
            return context.proc_sw_trae_CaracterInvalido_MS(caracterInvalido.codigo).ToList();
        }
        public IList<int?> proc_sw_trae_count_bancos_MS(string swift, string pais, string ciudad, string direccion,
            string banco, string branch, string intercambioClave)
        {
            return context.proc_sw_trae_count_bancos_MS(swift, pais, ciudad, direccion, banco, branch, intercambioClave).ToList();

        }
        //public IList<proc_sw_trae_count_bancos_MS_DTO> proc_sw_trae_count_bancos_MS(string swift, string pais, string ciudad, string direccion, string banco)
        //{

        //    return context.Database.SqlQuery<proc_sw_trae_count_bancos_MS_DTO>("exec proc_sw_trae_count_bancos_MS  @swift,@pais,@ciudad,@direccion,@banco",
        //        new SqlParameter("swift", swift), new SqlParameter("pais", pais), new SqlParameter("ciudad", ciudad), new SqlParameter("direccion", direccion), new SqlParameter("banco", banco)).ToList<proc_sw_trae_count_bancos_MS_DTO>();

        //}

    }
}
