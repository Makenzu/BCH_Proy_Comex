using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{

    //----------------------------------------------------------------------------
    //Arreglo en memoria para mantener nombres de partys.-
    //----------------------------------------------------------------------------
    public class Type_DatPrty
    {
        public string PrtImp;  //Llave Party.-
        public short IndNom;  //Indice Nombre.-
        public short IndDir;  //Indice Direccion.-
        public string NomPrty;  //Nombre Party.-
        public string DirPrty;  //Direccion Party.-
    }

    public class TIndViaOri
    {
        public string OriDes;
        public short ind;
        public string Prty;
    }

    public class T_Mdl_Funciones_Varias
    {
        public Type_DatPrty[] DatPrtys;

        public T_Mdl_Funciones_Varias()
        {
            DatPrtys = new Type_DatPrty[0];
        }

    }
}
