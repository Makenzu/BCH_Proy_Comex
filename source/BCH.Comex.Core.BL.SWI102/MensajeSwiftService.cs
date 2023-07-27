using BCH.Comex.Data.DAL.Swift;
using System;

namespace BCH.Comex.Core.BL.SWI102
{
    public class MensajeSwiftService
    {
        private UnitOfWorkSwift unitOfWork;
        public MensajeSwiftService()
        {
            this.unitOfWork = new UnitOfWorkSwift();
        }

        public string DesencriptaMensajeRecibido(int sesion, int secuencia)
        {
            if (secuencia >= 1000000) throw new Exception("Error en la secuencia del mensaje");
            try
            {
                return unitOfWork.BancoRepository.DesencriptaMensaje_MS(sesion, secuencia);
            }
            catch
            {
                throw new Exception("No existe mensaje en la base de datos");
            }
        }

        public string DesencriptaMensajeEnviado(int nroMensaje)
        {
            var llave = nroMensaje.ToString().PadLeft(10, '0') + "S";
            try
            {
                return unitOfWork.BancoRepository.DesencriptaMensajeS_MS(nroMensaje, llave);
            }
            catch
            {
                throw new Exception("No existe mensaje en la base de datos");
            }
        }

        public string DesencriptaMensajeSwift(char messageType, string sesion, string secuencia)
        {
            if (messageType == 'R')
            {
                return this.DesencriptaMensajeRecibido(int.Parse(sesion), int.Parse(secuencia));
            }
            else if (messageType == 'S')
            {
                var merged = sesion + secuencia;
                //var correlativo = int.Parse(merged);
                //var llave = merged.PadLeft(10, '0') + "S";
                return this.DesencriptaMensajeEnviado(int.Parse(merged));
            }
            throw new Exception("Tipo de mensaje no identificado");
        }
    }
}
