using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    // Arreglo de Documentos a rangear.-
    public class Type_RngDoc
    {
        public string DocCod;   //Código del Documento.
        public string DocGls;   //Glosa del Documento.
        public int DocGen;   //Es General? (no depende del usuario).
        public string DocFmt;   //Formato numérico (99;999;99999, etc.)

        public Type_RngDoc()
        {
            DocCod = String.Empty;   //Código del Documento.
            DocGls = String.Empty;   //Glosa del Documento.
            DocFmt = String.Empty;   //Formato numérico (99;999;99999, etc.)
        }
    }
    // Archivo de Rangos.
    public class Type_Rng
    {
        public string RngRut;   //Rut Especialista.
        public string RngCct;   //CCosto Especialista.
        public string RngEsp;   //Especialista.
        public string RngDoc;   //Tipo de Documento.
        public double RngInf;   //Limite Inferior.
        public double RngSup;   //Limite Superior.
        public double RngAct;   //Ultimo Usado.
        public string RngInf_t;   //Limite Inferior para Srm.
        public string RngSup_t;   //Limite Superior para Srm.
        public string RngAct_t;   //Ultimo Usado para Srm.

        public Type_Rng()
        {
            RngRut= String.Empty;   //Rut Especialista.
            RngCct= String.Empty;   //CCosto Especialista.
            RngEsp= String.Empty;   //Especialista.
            RngDoc= String.Empty;   //Tipo de Documento.
            RngInf_t= String.Empty;   //Limite Inferior para Srm.
            RngSup_t= String.Empty;   //Limite Superior para Srm.
            RngAct_t= String.Empty;   //Ultimo Usado para Srm.
        }
    }
    public class T_MODGRNG
    {
        // Constantes de Numeros de Operacion Posibles.
        public const string Rng_CobImp = "CBI";
        public const string Rng_CobExp = "CBE";
        public const string Rng_ContGL = "CGL";
        public const string Rng_CVTImp = "CVI";
        public const string Rng_PreIdi = "PID";
        public const string Rng_OpeAla = "OPA";
        public const string Rng_NumAla = "NAL";
        public const string Rng_PlaInv = "PLI";
        public const string Rng_PlaVis = "PLV";
        public const string Rng_CuaPag = "SCP";
        public const string Rng_SconSu = "SCS";
        public const string Rng_ValVis = "VVS";
        public const string Rng_SolIdi = "SII";
        public const string Rng_CreImp = "CCI";
        public const string Rng_CreCon = "CCC";
        public const string Rng_PreExp = "PAE";
        public const string Rng_ComExp = "CEX";
        public const string Rng_DesExp = "DEX";
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
        public const string Rng_CreLoc = "CCL";
        public const string Rng_Memld = "MLD";
        public const string Rng_Memlm = "MLM";
        public const string Rng_Doc = "DOC";
        public const string RngMemC = "MMC";

        public Type_RngDoc[] RngDocs = new Type_RngDoc[0];
        public Type_Rng VRng = new Type_Rng();
        public Type_Rng VRngNul = new Type_Rng();
        public int RngGeneral = 0;
        public string Servidor = "";
        public string Nodo = "";
        public string RngTxId = "";
        // Valores Generales.
        public const string RutGeneral = "9999999999";
        public const string CCostoGeneral = "999";
        public const string UsrEspGeneral = "99";
        public const string MsgRng = "Asignación de Rangos";
        public const string MsgRngErr = "No se pudo obtener el Número para realizar esta Operación. Intente obtenerlo nuevamente de lo contrario no podrá registrarla.";
    }
}
