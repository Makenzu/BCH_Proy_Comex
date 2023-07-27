using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.Devengo
{
    public class T_CDR
    {
        public IList<T_DvgCred> DvgCred { get; set; }
        public IList<T_DvgCred_Excel> DvgCredE { get; set; }

        public T_CDR()
        {
            DvgCred = new List<T_DvgCred>();
            DvgCredE = new List<T_DvgCred_Excel>();
        }
    }
     public class T_DvgCred
      {
         public Int64 Operacion { get; set; }
          public double Numcor { get; set; }
          public double Numcuo { get; set; }
          public string moneda { get; set; }
          public double Numacc { get; set; }
          public string Num_me { get; set; }
          public string dig_me { get; set; }
          public string Tipope { get; set; }
          public string indcdr { get; set; }
          public DateTime Feccon { get; set; }
          public DateTime FecVen { get; set; }
          public DateTime FecInt { get; set; }
          public DateTime FecOri { get; set; }
          public string Val_mo { get; set; }
          public string NomCli { get; set; }
          public string Tiptas { get; set; }
          public string Tasbas { get; set; }
          public string Spread { get; set; }
          public string Tastot { get; set; }
          public string numcli { get; set; }
          public string digcli { get; set; }
          public string Numava1 { get; set; }
          public string Digava1 { get; set; }
          public string Numava2 { get; set; }
          public string Digava2 { get; set; }
          public string TipCam { get; set; }
          public string Diadev { get; set; }
          public string Moneda_int { get; set; }
          public string Valori_cre_mo { get; set; }
          public string Int_al_ven_mo { get; set; }
          public string Dev_normal_mo { get; set; }
          public string Real_normal { get; set; }
          public string Tc_origen { get; set; }
          public string Fogape { get; set; } 
      }

     public class T_DvgCred_Excel
     {
         public Int64 Operacion { get; set; }
         public int Numcor { get; set; }
         public int Numcuo { get; set; }
         public string moneda { get; set; }
         public int Numacc { get; set; }
         public string Num_me_dig_me { get; set; }
         public int Tipope { get; set; }
         public int indcdr { get; set; }
         public DateTime Feccon { get; set; }
         public DateTime FecVen { get; set; }
         public DateTime FecInt { get; set; }
         public DateTime FecOri { get; set; }
         public double Val_mo { get; set; }
         public string NomCli { get; set; }
         public string Tiptas { get; set; }
         public double Tasbas { get; set; }
         public double Spread { get; set; }
         public double Tastot { get; set; }
         public string numcli_digcli { get; set; }
         public string Numava1_Digava1 { get; set; }
         public string Numava2_Digava2 { get; set; }
         public double TipCam { get; set; }
         public int Diadev { get; set; }
         public string Moneda_int { get; set; }
         public double Valori_cre_mo { get; set; }
         public double Int_al_ven_mo { get; set; }
         public double Dev_normal_mo { get; set; }
         public double Real_normal { get; set; }
         public double Tc_origen { get; set; }
         public string Fogape { get; set; }
     }
     
      
}
