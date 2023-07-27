using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Data.DAL.Swift
{
    public class CaracterErrorRepository : GenericRepository<sw_caracter_error, swiftEntities>
    {
        public CaracterErrorRepository(swiftEntities context)
            : base(context){}

        /// <summary>
        /// Obtiene lista caracteres error X
        /// </summary>
        /// <returns>Lista con valor ascii, caracter y descripcion caracteres error X</returns>
        public List<sw_caracter_error> LlenaMatrizCaracter()
        {
            return context.sw_caracter_error.SqlQuery("select valor_ascii, caracter, descripcion from sw_caracter_error").ToList();
        }

        /// <summary>
        /// Obtiene lista caracteres error Z
        /// </summary>
        /// <returns>Lista con valor ascii, caracter y descripcion caracteres error Z</returns>
        public List<sw_caracter_error> LlenaMatrizCaracterZ()
        {
            return context.sw_caracter_error.SqlQuery("select valor_ascii, caracter, descripcion from sw_caracter_error_z").ToList();
        }
    }
}
