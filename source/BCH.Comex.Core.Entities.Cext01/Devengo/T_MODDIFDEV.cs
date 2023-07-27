using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.Devengo
{
    public class T_MODDIFDEV
    {
        public List<T_DifDev> DifDev = null;

        public T_MODDIFDEV() 
        {
            DifDev = new List<T_DifDev>();
        }
    }

    public class T_DifDev
    {
        public Int64 numope;
        public int mesdif;
        public int codmon;
        public double mtodeb;
        public double mtohab;
        public double mtodif;
        public int tipmov;
        public string tipmovNombre;
    }
}
