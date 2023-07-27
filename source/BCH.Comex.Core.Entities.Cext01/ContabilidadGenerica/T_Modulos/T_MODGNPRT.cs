
namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{

    public class Type_DatPrty
    {
        public string PrtImp;  //Llave Party.-
        public short IndNom;  //Indice Nombre.-
        public short IndDir;  //Indice Direccion.-
        public string NomPrty;  //Nombre Party.-
        public string DirPrty;  //Direccion Party.-
    }

    public class T_MODGNPRT
    {
        public Type_DatPrty[] DatPrtys;

        public T_MODGNPRT()
        {
            DatPrtys = new Type_DatPrty[0];
        }
    }
}
