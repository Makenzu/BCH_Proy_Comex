using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FinDia
{
    public class T_MODGTAB0
    {
        public IList<T_Mnd> VMnd;

        public T_MODGTAB0()
        {
            VMnd = new List<T_Mnd>();
        }
    }

    public class T_Mnd
    {
        public int Mnd_MndCod;   //Código Banco Chile.
        public int Mnd_MndCbc;   //Código Banco Central.
        public string Mnd_MndNom;   //Nombre o Descripción.
        public string Mnd_MndNmc;   //Nemónico.
        public string Mnd_MndSwf;   //Código Swift.
        public int Mnd_MndSin;   //Indica Sin Decimal.

    }
}
