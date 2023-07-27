
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //Arreglo de Documentos a rangear.-
    public class Type_RngDoc
    {
        public string DocCod;  //Código del Documento.
        public string DocGls;  //Glosa del Documento.
        public short DocGen;  //Es General? (no depende del usuario).
        public string DocFmt;  //Formato numérico (99;999;99999, etc.)
    }

    //Archivo de Rangos.
    public class Type_Rng
    {
        public string RngRut;  //Rut Especialista.
        public string RngCct;  //CCosto Especialista.
        public string RngEsp;  //Especialista.
        public string RngDoc;  //Tipo de Documento.
        public double RngInf;  //Limite Inferior.
        public double RngSup;  //Limite Superior.
        public double RngAct;  //Ultimo Usado.
        public string RngInf_t;  //Limite Inferior para Srm.
        public string RngSup_t;  //Limite Superior para Srm.
        public string RngAct_t;  //Ultimo Usado para Srm.
    }
    
    public class T_MODGRNG
    {
        //Usuario tiene rango permitido para realizar operaciones
        public  bool Rango_Permitido;
        public  Type_RngDoc[] RngDocs;
        public  Type_Rng VRng;
        public  short RngGeneral;

        public T_MODGRNG()
        {
            Rango_Permitido = false;
            RngDocs = new Type_RngDoc[0];
            VRng = new Type_Rng();
            RngGeneral = 0;
        }

        public const string Rng_OpeAla = "OPA";
        public const string Rng_PlaInv = "PLI";
        public const string Rng_PlaVis = "PLV";
        public const string Rng_SconSu = "SCS";
        public const string Rng_ValVis = "VVS";
        public const string Rng_Memf = "MMF";
        public const string Rng_Memp = "MMP";
        public const string Rng_Memx = "MMX";
        public const string Rng_Memm = "MMM";
        public const string Rng_Mems = "MMS";
        public const string Rng_Memi = "MMI";
        public const string Rng_Memjd = "MJD";
        public const string Rng_Memjm = "MJM";
        public const string Rng_Memy = "MMY";
        public const string Rng_Meme = "MME";
        public const string Rng_Memld = "MLD";
        public const string Rng_Memlm = "MLM";
        public const string RngMemC = "MMC";
    }
}
