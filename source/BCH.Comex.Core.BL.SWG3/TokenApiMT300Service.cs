using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.SWG3
{
    public class TokenApiMT300Service : IDisposable
    {
        private readonly UnitOfWorkCext01 uow;

        public TokenApiMT300Service()
        {
            uow = new UnitOfWorkCext01();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (uow != null)
            {
                uow.Dispose();
            }
        }

        public bool ValidarToken(string token)
        {
            using (Tracer tracer = new Tracer("Obtiene y valida token api"))
            {
                try
                {

                    List<ParametroComex> parametrosConfig = this.uow.ParametroComexRepository.GetParametrosMT300("apitoken", "", "").ToList();
                    String tokenBD = parametrosConfig.First().trans_vlr_parametro;

                    if (token == tokenBD)
                    {
                        tracer.TraceVerbose("El token enviado coincide");
                        return true;
                    }
                    else
                    {
                        tracer.TraceError("El token enviado no es valido");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    tracer.TraceError("Error al tratar de obtener y validar token de api " + e);
                    return false;
                }
            }
        }
    }
}
