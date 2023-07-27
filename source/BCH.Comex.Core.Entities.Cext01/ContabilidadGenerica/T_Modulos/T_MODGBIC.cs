using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_Bic
    {
        public string BicSwf = String.Empty;
        public string BicSec = String.Empty;
        public string BicNom = String.Empty;
        public string BicDes = String.Empty;
        public string BicCiu = String.Empty;
        public string BicDir = String.Empty;
        public string BicPos = String.Empty;
        public string BicPai = String.Empty;
        public int BicAla;
        public string BicCod = String.Empty;

        public T_Bic Copy()
        {
            return (T_Bic)this.MemberwiseClone();
        }
    }
    public class T_MODGBIC
    {
        public T_Bic VBic = new T_Bic();
        public T_Bic VBicNul = new T_Bic();
        // ****************************************************************************
        public const string MsgBic = "Bancos del Mundo";
    }
}
