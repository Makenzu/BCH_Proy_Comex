using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.SWEN
{
    public class SwenService
    {
        private UnitOfWorkSwift unitOfWork;

        public SwenService()
        {
            this.unitOfWork = new UnitOfWorkSwift();
        }

        public IList<proc_sw_rec_trae_otr_rango_MS_Result> GetMensajesRecibidosRango(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Rec_Trae_Otr_Rango(idCasilla, fechaInicio, fechaFin);
        }

        //Agregado para opción desencasillar
        public IList<proc_sw_rec_trae_enc_rango_MS_Result> GetMensajesRecibidosRangoDes(int idCasilla, DateTime fechaInicio, DateTime fechaFin)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Rec_Trae_Enc_Rango(idCasilla, fechaInicio, fechaFin);
        }

        /// <summary>
        /// Devuelve el estado de un Mensaje Switf
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="secuencia"></param>
        /// <returns></returns>
        public String GetMensajeEstado(int sesion, int secuencia)
        {
            try
            {
                return unitOfWork.MensajeRepository.Proc_Sw_Valida_Estado_Msg_MS(sesion, secuencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return unitOfWork.MensajeRepository.Proc_Sw_Valida_Estado_Msg_MS(sesion, secuencia);
        }

        public int SetChangeCasillaMensaje(int casilla, int sesion, int secuencia, int rut, string observacion)
        {
            try
            {
                return unitOfWork.MensajeRepository.Proc_Sw_Rec_Graba_Enc(casilla, sesion, secuencia, rut, observacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string  Deshacer(int idCasilla, int Sesion, int Secuencia, int Rut)
        {
            return unitOfWork.MensajeRepository.Proc_Sw_Rec_Anula_Enc(idCasilla, Sesion, Secuencia, Rut);
        }
    }
}
