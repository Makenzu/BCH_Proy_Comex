//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;

using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGPY.Modulos
{
   public class MODGRNG
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

        public struct Type_RngDoc
        {
            public string DocCod;   //Código del Documento.
            public string DocGls;   //Glosa del Documento.
            public int DocGen;   //Es General? (no depende del usuario).
            public string DocFmt;   //Formato numérico (99;999;99999, etc.)
        }
        public static Type_RngDoc[] RngDocs = null;
        public struct Type_Rng
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
        }
        public static Type_Rng VRng = new Type_Rng();
        public static Type_Rng VRngNul = new Type_Rng();
        public static int RngGeneral = 0;
        public static string Servidor = "";
        public static string Nodo = "";
        public static string RngTxId = "";
       
       public static T_MODGRNG GetMODGRNG()
       {
           return new T_MODGRNG();
       }

       public static double LeeSceRng(string RngDoc, InitializationObject iO, XgpyService xS)
       {
           double retLeeSceRng = 0.0;

           double resUpdRng = 0.0;
           int k = 0;
           int a = 0;

           retLeeSceRng = true.ToDbl();

           // Inicializa variables de Rangos.
           a = SyGetn_xTrng(xS);

           k = -1;
           for (int i = 1; i < RngDocs.Count(); i ++)
           {
               if (RngDocs[i].DocCod == RngDoc)
               {
                   k = i;
                   break;
               }
           }
           if (k == -1)
           {
               //No se conoce documento sobre el cual se pide el número.
               return retLeeSceRng;
           }
           RngGeneral = RngDocs[k].DocGen;
           VRng.RngDoc = RngDocs[k].DocCod;
           if (RngGeneral != 0)
           {
               VRng.RngCct = iO.UsrEsp.CentroCosto;
               VRng.RngEsp = "00";
               VRng.RngRut = "0000000000";
           }
           else
           {
               VRng.RngCct = iO.UsrEsp.CentroCosto;
               VRng.RngEsp = iO.UsrEsp.Especialista;
               VRng.RngRut = iO.UsrEsp.rut;
           }

           resUpdRng = SyGetUpd_Rng(VRng.RngCct, VRng.RngEsp, VRng.RngDoc, xS);
           switch (Convert.ToInt32(resUpdRng))
           {
               case -1:
                   //Ya se han asignado todos los numeros permitidos para esta operación.
                   break;
               case 0:
                   //No se pudo establecer el Número requerido para la Operación.
                   break;
               default:
                   retLeeSceRng = resUpdRng;
                   break;
           }

           return retLeeSceRng;
       }

       //Obtiene la lista de documentos cargados en el sistema para ver los datos de la instruccion elegida
       public static int SyGetn_xTrng(XgpyService xS)
       {
           int ret_SyGetn_xTrng = 0;
           int n = 0;

           // Verifica si ya se leyó la tabla.-

           if (RngDocs != null) 
           {
               n = RngDocs.GetUpperBound(0);
           }
           
           if (n == 0)
           {
               RngDocs = new Type_RngDoc[1];
           }
           if (n > 0)
           {
               ret_SyGetn_xTrng = 1;

               return ret_SyGetn_xTrng;
           }

           var res = xS.Sce_Trng_S01_List_MS();
           if(res.Count == 0)
           {
               return ret_SyGetn_xTrng;
           }

           n = res.Count;
           RngDocs = new Type_RngDoc[n + 1];
           for (int i = 1; i < n; i++)
           {
               RngDocs[i].DocCod = res[i].codnope.Trim();
               RngDocs[i].DocGen = (int)res[i].grlnope;
           }

           ret_SyGetn_xTrng = 1;

           return ret_SyGetn_xTrng;
       }

       public static double SyGetUpd_Rng(string CodCct, string codesp, string CodFun, XgpyService xS)
       {
           double res_SyGetUpd_Rng = 0.0;

           try
           {
               var res = xS.Sce_Rng_U01_MS(CodCct.TrimB(), codesp.TrimB(), CodFun.TrimB());
               // Hubo Error.
               if (res == null)
               {
                   //Se ha producido un error al tratar de leer los datos de la Asignación de Rangos (Sce_Rng)
                   return res_SyGetUpd_Rng;
               }

               // Se realizó el Query pero la consulta no retornó datos.
               if (res == -1)
               {
                   //La Asignación de Rangos no ha sido encontrada (Sce_Rng)
                   return res_SyGetUpd_Rng;
               }

               res_SyGetUpd_Rng = res;

               return res_SyGetUpd_Rng;

           }
           catch { }

           return res_SyGetUpd_Rng;
       }
       
       public static double LeeSceRng_New(string RngDoc, InitializationObject iO, XgpyService xS)
       {
           double retLeeSceRng = 0.0;

           double resUpdRng = 0.0;
           int k = 0;
           int a = 0;

           retLeeSceRng = true.ToDbl();

           // Inicializa variables de Rangos.
           a = SyGetn_xTrng(xS);

           k = -1;
           for (int i = 1; i < RngDocs.Count(); i++)
           {
               if (RngDocs[i].DocCod == RngDoc)
               {
                   k = i;
                   break;
               }
           }
           if (k == -1)
           {
               //No se conoce documento sobre el cual se pide el número.
               return retLeeSceRng;
           }
           RngGeneral = RngDocs[k].DocGen;
           VRng.RngDoc = RngDocs[k].DocCod;
           if (RngGeneral != 0)
           {
               VRng.RngCct = iO.UsrEsp.CentroCosto;
               VRng.RngEsp = "00";
               VRng.RngRut = "0000000000";
           }
           else
           {
               VRng.RngCct = iO.UsrEsp.CentroCosto;
               VRng.RngEsp = iO.UsrEsp.Especialista;
               VRng.RngRut = iO.UsrEsp.rut;
           }

           resUpdRng = SyGetUpd_Rng_New(VRng.RngCct, VRng.RngEsp, VRng.RngDoc, xS);
           switch (Convert.ToInt32(resUpdRng))
           {
               case -1:
                   //Ya se han asignado todos los numeros permitidos para esta operación.
                   retLeeSceRng = resUpdRng;
                   break;
               case 0:
                   //No se pudo establecer el Número requerido para la Operación.
                   retLeeSceRng = resUpdRng;
                   break;
               default:
                   retLeeSceRng = resUpdRng;
                   break;
           }

           return retLeeSceRng;
       }

       public static double SyGetUpd_Rng_New(string CodCct, string codesp, string CodFun, XgpyService xS)
       {
           double res_SyGetUpd_Rng = 0.0;

           try
           {
               var res = xS.Sce_Rng_U01_MS(CodCct.TrimB(), codesp.TrimB(), CodFun.TrimB());
               // GVT - No encontro numeracion posible en la tabla sce_rng, la cual tiene los numeros a asignar en la instruccion 
               //de acuerdo al centrocosto, costouser y tipo de documento
               if (res == 0)
               {
                   //Se ha producido un error al tratar de leer los datos de la Asignación de Rangos (Sce_Rng)
                   return res_SyGetUpd_Rng;
               }

               // Se realizó el Query pero la consulta no retornó datos.
               if (res == -1)
               {
                   //La Asignación de Rangos no ha sido encontrada (Sce_Rng)
                   return res_SyGetUpd_Rng;
               }

               res_SyGetUpd_Rng = res;

               return res_SyGetUpd_Rng;

           }
           catch { }

           return res_SyGetUpd_Rng;
       }

        //Obtiene la lista de documentos cargados en el sistema para ver los datos de la instruccion elegida

    }
}
