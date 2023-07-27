using System;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain
{
    //***************************************************************
    //Estructura Asociación de Operación.-
    //***************************************************************
    public class T_Aso
    {
        public string OpeSin{set;get;}  //Operación Sin Raya divisora.-
        public string OpeCon{set;get;}  //Operación Con Raya divisora.-
        public string PrtCli{set;get;}  //Partys de la Operación.-
        public short IndNom{set;get;}  //Indicador de Nombre    del Cliente.-
        public short IndDir{set;get;}  //Indicador de Dirección del Cliente.-
        public string IndNom_t{set;get;}  //Nombre.-
        public string IndDir_t{set;get;}  //Dirección.

        public T_Aso()
        {
            OpeSin = String.Empty;
            OpeCon = String.Empty;
            PrtCli = String.Empty;
            IndNom =0;
            IndDir = 0;
            IndNom_t = String.Empty;
            IndDir_t = String.Empty;
        }

        public T_Aso Clone()
        {
            return (T_Aso)this.MemberwiseClone();
        }
    }
}
