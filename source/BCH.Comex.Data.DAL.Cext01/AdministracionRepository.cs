using BCH.Comex.Core.Entities.Cext01;
using System.Data.SqlClient;
using System.Linq;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class AdministracionRepository 
    {
        cext01Entities context;

        public AdministracionRepository(cext01Entities aContext)
        {
            context = aContext;
        }


        //public proc_rh_swi_001DTO proc_rh_swi_001(int rut)
        //{
        //    return context.Database.SqlQuery<proc_rh_swi_001DTO>("exec proc_rh_swi_001  @nurut",
        //        new SqlParameter("nurut", rut)).ToList<proc_rh_swi_001DTO>().FirstOrDefault();
        //    //return context.proc_rh_swi_001(rut).ToList();
        //}

    }
}