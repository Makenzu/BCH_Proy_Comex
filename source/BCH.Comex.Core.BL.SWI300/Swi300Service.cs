using BCH.Comex.Data.DAL.Swift;

namespace BCH.Comex.Core.BL.SWI300
{
    public class Swi300Service
    {
        private UnitOfWorkSwift unitOfWork;
        public Swi300Service()
        {
            this.unitOfWork = new UnitOfWorkSwift();
        }

        public int GetCorrelativo()
        {
            return unitOfWork.BancoRepository.GetCorrelativo();
        }
    }
}
