using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.Entities.Cext01.Impresion.T_Modulos
{
    //-------------------------------------------------------------
    //Arreglo en memoria para mantener datos de cuentas contables.-
    //-------------------------------------------------------------
    // Se agrega elemento Cta_Vig para determinar cuenta vigenteable
    public class T_Cta : ICloneable
    {
        public VB6FixedString Cta_Nem;
        public short Cta_Mon;
        public VB6FixedString Cta_Num;
        public VB6FixedString Cta_Nom;
        public short Cta_GL;
        public int Cta_NroTO;
        public short Cta_IndTO;
        public short Cta_CIT;
        public short Cta_CVT;
        public short Cta_CAP;
        public short Cta_CTD;
        public short Cta_POS;
        public short Cta_CDR;
        public short Cta_Vig;  // Vigenteable

        #region Initialization

        public T_Cta()
        {
            Cta_Nem = new VB6FixedString(15);
            Cta_Num = new VB6FixedString(8);
            Cta_Nom = new VB6FixedString(38);
            Cta_Mon = 0;
            Cta_GL = 0;
            Cta_NroTO = 0;
            Cta_IndTO = 0;
            Cta_CIT = 0;
            Cta_CVT = 0;
            Cta_CAP = 0;
            Cta_CTD = 0;
            Cta_POS = 0;
            Cta_CDR = 0;
            Cta_Vig = 0;
        }

        #endregion

        #region Clone methods

        public T_Cta Clone()
        {
            T_Cta _copy = this;
            _copy.Cta_Nem = Cta_Nem.Clone();
            _copy.Cta_Num = Cta_Num.Clone();
            _copy.Cta_Nom = Cta_Nom.Clone();
            return _copy;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion
    }

    public class T_MODGNCTA
    {
        public T_Cta[] VCta;

        public const short CtaMonExt = 1;
        
        public const short CtaMonNac = 2;
        
        public const short CtaMonAmb = 3;

        public T_MODGNCTA()
        {
            this.VCta = new T_Cta[0];
        }
    }
}
