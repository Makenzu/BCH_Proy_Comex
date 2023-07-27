using BCH.Comex.Core.Entities.Swift;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.BL.SWBO
{
    public class SwboService
    {
        private UnitOfWorkSwift unitOfWork;

        public SwboService()
        {
            this.unitOfWork = new UnitOfWorkSwift();
        }

        public void EliminarCasilla(int idCasilla)
        {
            var result = unitOfWork.MensajeRepository.Proc_Sw_Rec_Borra_Casilla(idCasilla);
            if (result != 0)
                throw new Exception("Ocurrió un error al momento de la eliminación");
        }

        public proc_sw_busca_eliminar_MS_Result BuscaEliminar(int sesion, int secuencia)
        {
            var result = unitOfWork.MensajeRepository.Proc_Sw_Busca_Eliminar(sesion, secuencia);
            if (result == null)
                throw new Exception("No se encontró el mensaje a eliminar");
            return result;
        }

        public void EliminarMensaje(int sesion, int secuencia)
        {
            unitOfWork.MensajeRepository.Proc_Sw_Elimina_Mensaje(sesion, secuencia);
        }
    }
}
