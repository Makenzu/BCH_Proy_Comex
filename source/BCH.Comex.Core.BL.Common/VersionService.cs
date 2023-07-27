using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Sbcor;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.Common
{
    public partial class VersionService : IDisposable
    {
        private UnitOfWorkCext01 uowCext01;
        private UnitOfWorkSwift uowSwift;
        private UnitOfWorkSbcor uowSbcor;

        public VersionService()
        {
            uowCext01 = new UnitOfWorkCext01();
            uowSwift = new UnitOfWorkSwift();
            uowSbcor = new UnitOfWorkSbcor();
        }

        

        public string GetVersion(VersionType versionType)
        {
            try
            {

                switch (versionType)
                {
                    case VersionType.Cext01Codigo:
                        return uowCext01.BancoRepository.EjecutarSP<string>("proc_sce_version_codigo_MS").FirstOrDefault();
                    case VersionType.Cext01Tablas:
                        return uowCext01.BancoRepository.EjecutarSP<string>("proc_sce_version_tablas_MS").FirstOrDefault();
                    case VersionType.SbcorCodigo:
                        return uowSbcor.BancoRepository.EjecutarSP<string>("proc_sbc_version_codigo_MS").FirstOrDefault();
                    case VersionType.SwiftCodigo:
                        return uowSwift.BancoRepository.EjecutarSP<string>("proc_sw_version_codigo_MS").FirstOrDefault();
                    case VersionType.SwiftTablas:
                        return uowSwift.BancoRepository.EjecutarSP<string>("proc_sw_version_tablas_MS").FirstOrDefault();
                    default:
                        return string.Empty;
                }
            }
            catch (Exception e){
                return string.Format("Error al obtener version '{0}: {1}'", versionType, e.ToString());
            }
        }

        public void Dispose()
        {
            if (uowCext01 != null)
            {
                uowCext01.Dispose();
            }

            if (uowSwift != null)
            {
                uowSwift.Dispose();
            }

            if (uowSbcor != null)
            {
                uowSbcor.Dispose();
            }
        }


    }
}
