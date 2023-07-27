using BCH.Comex.Core.Entities.Swift;
using System;

namespace BCH.Comex.Data.DAL.Swift
{
    public class FirmaRepository : GenericRepository<sw_msgsend_firma, swiftEntities>
    {
        public FirmaRepository(swiftEntities context)
            : base(context)
        {

        }

        public bool proc_sw_env_del_firnul_MS(int idMensaje, int rut, DateTime p_fecha_solic)
        {
            int result = EjecutarSPConRetorno("proc_sw_env_del_firnul_MS", String.Empty, idMensaje.ToString(), rut.ToString(), p_fecha_solic.ToString("yyyy-MM-dd HH:mm:ss"));
            return (result == 0);  //0 para OK
        }

        public bool proc_sw_env_del_firma_MS(int idMensaje, int rut)
        {
            int result = EjecutarSPConRetorno("proc_sw_env_del_firma_MS", String.Empty, idMensaje.ToString(), rut.ToString());
            return (result == 0);  //0 para OK
        }

    }
}
