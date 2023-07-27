
namespace BCH.Comex.Data.DAL
{
    //Manuel Babuglia: Se intento agregar un interceptor para hacer trim de todas las columnas, pero como hay varios contextos, por ahora no se logró
    //que funcionara bien. Funciona bien para lo que está en PortalEntities pero los Cext01Entities no lo toma.
    /*public class EFConfiguration : DbConfiguration
    {
        public EFConfiguration()
        {
            AddInterceptor(new StringTrimmerInterceptor());
        }
    }*/
}
