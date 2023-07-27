
namespace BCH.Comex.Core.Entities.Cext01.Common
{
    //Estructura Archivo Sce_Bic.
    public class T_Bic
    {
        public string BicSwf;
        public string BicSec;
        public string BicNom;
        public string BicDes;
        public string BicCiu;
        public string BicDir;
        public string BicPos;
        public string BicPai;
        public bool BicAla;
        public string BicCod;
        public int CouCod; //código del país tomado de la tabla cou_pai en base al valor Cou_Pai (BicPaic)
    }

    public class T_MODGBIC
    {
        public T_Bic VBic = new T_Bic();
        public T_Bic VBicNul = new T_Bic();
        // ****************************************************************************
        public const string MsgBic = "Bancos del Mundo";
    }
}
