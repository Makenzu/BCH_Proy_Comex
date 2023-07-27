
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //Estructura que guarda detalles de origen y destino para cuenta CheckVerification
    public class T_Chv
    {
        public double Saldo;
        public string NemMon;
        public short CodMon;
        public string IdParty;
        public short IdNom;
        public short IdDir;
        public short CantPln;
        public short Acepto;
        public string Cod_IE;

        public T_Chv Copy()
        {
            return (T_Chv)this.MemberwiseClone();
        }
    }

    //****************************************************************************
    //Tabla de Código de Concepto Planillas.-
    //****************************************************************************
    public class T_CCpl
    {
        public string CodCom;
        public string CptCom;
        public string DesCom;
        public string tipope;
        public short flging;
        public short rutint;
        public short nomint;
        public short dirint;
        public short codpai;
        public short mtopss;
        public short dataut;
        public short operel;
        public short numins;
        public short fecins;
        public short finext;
        public short vtocic;
        public short fecdes;
        public short mondes;
        public short mtodes;
        public short impadc;
        public short decexp;
        public short infexp;
        public short vtoret;
        public short mtoexp;
        public short vtofin;
        public short nomcom;
        public short infimp;
        public short decimp;
        public short codfdp;
        public short conemb;
        public short vtoope;
        public short mtoimp;
        public short datint;
        public short datder;
        public short acuaco;
        public short codccr;
        public short observ;
    }

    public class T_PlnChV
    {
        public double Monto;
        public string NemMon;
        public short CodMon;
        public string IdParty;
        public short IdNom;
        public short IdDir;
        public string CodCom;
        public string CptCom;
        public string DesCom;
        public short CodPais;
        public short estado;
        public string Cod_IE;

        public T_PlnChV Copy()
        {
            return (T_PlnChV)this.MemberwiseClone();
        }
    }

    public class T_ModChVrf
    {
        public  short AceptoPantallaChVrf;
        public  string CodigoIE ;

        public  T_Chv[] VgChV;
        public  T_Chv[] Aux_VgChV;

        public  T_CCpl[] VCcpl;
        public  T_PlnChV[] VPlnChV;
        public  T_PlnChV[] Aux_VPlnChV;
    }
}
