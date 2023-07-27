using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FinDia
{
    public class T_MODCUACC
    {
        public IList<T_CodTra> VCodTra = null;
        public string RutwAis = "";
        public bool ABONO_VB = false;

        public IList<T_ConCCLin> VConCCLin = null;
        public IList<T_ConCCLin> VConCCLin2 = null;
        public IList<T_TraSTB> TTraSTB = null;

        public IList<CargoAbono> V_CYA = null;

        public T_MODCUACC()
        {
            VCodTra = new List<T_CodTra>();
            VConCCLin = new List<T_ConCCLin>();
            VConCCLin2 = new List<T_ConCCLin>();
            V_CYA = new List<CargoAbono>();
        }
    }

    public struct CargoAbono
      {
         public string numcct;
         public string nemcta;
         public string fecmov;
         public int nroimp;
         public string nombre;
         public string cod_dh;
         public string nemmon;
         public int codmon;
         public double mtomcd;
         public string operacion;
         public int nrorpt;
         public int enlinea;
         public int estado;
         public string codcct;
         public string codpro;
         public string codesp;
         public string codofi;
         public string codope;
         public int codfun;
         public int indice;
      }


    public struct T_TraSTB
    {
        public string codcct;
        public string codesp;
        public string codofi;
        public string rutesp;
        public string codtra;
        public string cod_dh;
        public string propto;
        public string hortra;
        public string numccl;
        public string numfol;
        public string campo3;
        public string mtotra;
        public string superv;
        public string indrev;
        public string interf;
        public int cuadra;
        public int error;
        public string nemtra;
    }

    public class T_ConCCLin
    {
        public string codcct;
        public string codpro;
        public string codesp;
        public string codofi;
        public string codope;
        public string cencos;
        public string codusr;
        public int nrorpt;
        public string fecmov;
        public string cod_dh;
        public string nemcta;
        public string numcct;
        public double mtomcd;
        public string nemmon;
        public int estado;
        public int cuadra;
        public int error;
    }

    //  Estructura de Códigos de Transacción Cta Cte Línea
    public class T_CodTra
    {
        public string codtra;
        public string nemtra;
        public string destra;
    }

}
