using BCH.Comex.Core.Entities.Swift;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class PaymentPlusRepository : GenericRepository<PaymentPlus, swiftEntities>
    {
        public PaymentPlusRepository(swiftEntities context)
            : base(context)
        {
        }

        public IList<PaymentPlus> GetBancoPorSwift(string swiftBanco, string secuencia)
        {
            return EjecutarSP<PaymentPlus>("sce_c50f_obtener_banco_payment_plus", swiftBanco, secuencia).ToList();
        }
    }
}
