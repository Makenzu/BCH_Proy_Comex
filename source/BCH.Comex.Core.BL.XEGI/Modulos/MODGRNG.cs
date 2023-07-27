using System;
using System.Windows.Forms;

namespace BCH.Comex.Core.BL.XEGI.Modulos
{
    public static class MODGRNG
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
        // Arreglo de Documentos a rangear.-
        public struct Type_RngDoc
        {
            public string DocCod;   //Código del Documento.
            public string DocGls;   //Glosa del Documento.
            public int DocGen;   //Es General? (no depende del usuario).
            public string DocFmt;   //Formato numérico (99;999;99999, etc.)
        }
        public static Type_RngDoc[] RngDocs = null;
        // Archivo de Rangos.
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
        // Valores Generales.
        public const string RutGeneral = "9999999999";
        public const string CCostoGeneral = "999";
        public const string UsrEspGeneral = "99";
        public const string MsgRng = "Asignación de Rangos";
        public const string MsgRngErr = "No se pudo obtener el Número para realizar esta Operación. Intente obtenerlo nuevamente de lo contrario no podrá registrarla.";
        // Lee un número asociado a la operación que se pide.
        public static double LeeSceRng(XegiService service, string RngDoc)
        {
            double LeeSceRng = 0.0;

            double x = 0.0;
            int i = 0;
            int k = 0;
            int a = 0;

            LeeSceRng = (true ? -1 : 0);

            // Inicializa variables de Rangos.
            a = SyGetn_xTrng(service);

            k = -1;
            for (i = 1; i <= RngDocs.GetUpperBound(0); i += 1)
            {
                if (RngDocs[i].DocCod == RngDoc)
                {
                    k = i;
                    break;
                }
            }
            if (k == -1)
            {
                //System.Windows.Forms.MessageBox.Show("No se conoce documento sobre el cual se pide el número.", MODGRNG.MsgRng, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return LeeSceRng;
            }
            RngGeneral = RngDocs[k].DocGen;
            VRng.RngDoc = RngDocs[k].DocCod;
            if (RngGeneral != 0)
            {
                VRng.RngCct = MODXDOC.UsrEsp.CentroCosto;
                VRng.RngEsp = "00";
                VRng.RngRut = "0000000000";
            }
            else
            {
                VRng.RngCct = MODXDOC.UsrEsp.CentroCosto;
                VRng.RngEsp = MODXDOC.UsrEsp.Especialista;
                VRng.RngRut = MODXDOC.UsrEsp.rut;
            }

            x = SyGetUpd_Rng(VRng.RngCct, VRng.RngEsp, VRng.RngDoc);
            switch (x.ToInt())
            {
                case -1:
                    //System.Windows.Forms.MessageBox.Show("Ya se han asignado todos los numeros permitidos para esta operación.", MODGRNG.MsgRng, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 0:
                    //System.Windows.Forms.MessageBox.Show("No se pudo establecer el Número requerido para la Operación.", MODGRNG.MsgRng, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                default:
                    LeeSceRng = x;
                    break;
            }

            return LeeSceRng;
        }
        // Inicializa algunas variables para utilizar los rangos.
        public static void RngInit()
        {
            int n = 0;

            n = RngDocs.GetUpperBound(0);
            if (n == 0)
            {
                // ------------------------------------------------------------------------
                RngDocs = new Type_RngDoc[13];
                n = 1;
                RngDocs[n].DocCod = MODGRNG.Rng_CobImp;
                RngDocs[n].DocGen = false.ToInt();
                // ------------------------------------------------------------------------
                n = n + 1;
                RngDocs[n].DocCod = MODGRNG.Rng_CobExp;
                RngDocs[n].DocGen = false.ToInt();
                // ------------------------------------------------------------------------
                n = n + 1;
                RngDocs[n].DocCod = MODGRNG.Rng_ContGL;
                RngDocs[n].DocGen = false.ToInt();
                // ------------------------------------------------------------------------
                n = n + 1;
                RngDocs[n].DocCod = MODGRNG.Rng_CVTImp;
                RngDocs[n].DocGen = false.ToInt();
                // ------------------------------------------------------------------------
                n = n + 1;
                RngDocs[n].DocCod = MODGRNG.Rng_PreIdi;
                RngDocs[n].DocGen = true.ToInt();
                // ------------------------------------------------------------------------
                n = n + 1;
                RngDocs[n].DocCod = MODGRNG.Rng_OpeAla;
                RngDocs[n].DocGen = true.ToInt();
                // ------------------------------------------------------------------------
                n = n + 1;
                RngDocs[n].DocCod = MODGRNG.Rng_PlaInv;
                RngDocs[n].DocGen = true.ToInt();
                // ------------------------------------------------------------------------
                n = n + 1;
                RngDocs[n].DocCod = MODGRNG.Rng_PlaVis;
                RngDocs[n].DocGen = true.ToInt();
                // ------------------------------------------------------------------------
                n = n + 1;
                RngDocs[n].DocCod = MODGRNG.Rng_CuaPag;
                RngDocs[n].DocGen = true.ToInt();
                // ------------------------------------------------------------------------
                n = n + 1;
                RngDocs[n].DocCod = MODGRNG.Rng_SconSu;
                RngDocs[n].DocGen = true.ToInt();
                // ------------------------------------------------------------------------
                n = n + 1;
                RngDocs[n].DocCod = MODGRNG.Rng_ValVis;
                RngDocs[n].DocGen = false.ToInt();
                // ------------------------------------------------------------------------
                n = n + 1;
                RngDocs[n].DocCod = MODGRNG.Rng_SolIdi;
                RngDocs[n].DocGen = true.ToInt();
            }

        }
        // Retorna la estructura Codop con la siguiente Operación.
        // True : Exitoso.
        public static int SgteNumOpr(XegiService service, string RngPro, string RngDoc)
        {
            int SgteNumOpr = 0;

            double q = 0.0;
            int i = 0;
            int k = 0;
            int a = 0;

            // Si la Codop no está limpia, => Número de Operación actual.
            if(!string.IsNullOrEmpty(MODXDOC.Codop.Id_Operacion))
            {
                MODXDOC.Codop.Cent_costo = MODXDOC.UsrEsp.CentroCosto;
                MODXDOC.Codop.Id_Product = RngPro;
                MODXDOC.Codop.Id_Especia = MODXDOC.UsrEsp.Especialista;
                MODXDOC.Codop.Id_Operacion = MigrationSupport.Utils.Format(MODXDOC.Codop.Id_Operacion, "00000");
                SgteNumOpr = true.ToInt();
                return SgteNumOpr;
            }

            // Inicializa variables de Rangos.
            a = SyGetn_xTrng(service);

            k = -1;
            for (i = 1; i <= RngDocs.GetUpperBound(0); i += 1)
            {
                if (RngDocs[i].DocCod == RngDoc)
                {
                    k = i;
                    break;
                }
            }
            if (k == -1)
            {
                System.Windows.Forms.MessageBox.Show("No se conoce documento sobre el cual se pide el número.", MODGRNG.MsgRng, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return SgteNumOpr;
            }
            RngGeneral = RngDocs[k].DocGen;
            VRng.RngDoc = RngDocs[k].DocCod;
            if (RngGeneral != 0)
            {
                VRng.RngCct = MODXDOC.UsrEsp.CentroCosto;
                VRng.RngEsp = "00";
                VRng.RngRut = "0000000000";
            }
            else
            {
                VRng.RngCct = MODXDOC.UsrEsp.CentroCosto;
                VRng.RngEsp = MODXDOC.UsrEsp.Especialista;
                VRng.RngRut = MODXDOC.UsrEsp.rut;
            }

            // Actualización y lectura de rango seleccionado.-
            q = SyGetUpd_Rng(VRng.RngCct, VRng.RngEsp, VRng.RngDoc);
            if (q > 0)
            {
                MODXDOC.Codop.Cent_costo = MODXDOC.UsrEsp.CentroCosto;
                MODXDOC.Codop.Id_Product = RngPro;
                MODXDOC.Codop.Id_Especia = MODXDOC.UsrEsp.Especialista;
                MODXDOC.Codop.Id_Operacion = MigrationSupport.Utils.Format(q, "00000");
                SgteNumOpr = true.ToInt();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Ya se han asignado todos los numeros permitidos para esta operación.", MODGRNG.MsgRng, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SgteNumOpr = false.ToInt();
            }

            return SgteNumOpr;
        }
        // Retorna la estructura Codop con la siguiente Operación.
        // True : Exitoso.
        public static int SgteNumOpr2(XegiService service, string RngPro, string RngDoc)
        {
            int SgteNumOpr2 = 0;

            double q = 0.0;
            int x = 0;
            int i = 0;
            int k = 0;
            int a = 0;

            // Si la Codop no está limpia, => Número de Operación actual.
            if(!string.IsNullOrEmpty(MODXDOC.Codop.Id_Operacion))
            {
                MODXDOC.Codop.Cent_costo = MODXDOC.UsrEsp.CentroCosto;
                MODXDOC.Codop.Id_Product = RngPro;
                MODXDOC.Codop.Id_Especia = MODXDOC.UsrEsp.Especialista;
                MODXDOC.Codop.Id_Operacion = MigrationSupport.Utils.Format(MODXDOC.Codop.Id_Operacion, "00000");
                SgteNumOpr2 = true.ToInt();
                return SgteNumOpr2;
            }

            // Inicializa variables de Rangos.
            a = SyGetn_xTrng(service);

            k = -1;
            for (i = 1; i <= RngDocs.GetUpperBound(0); i += 1)
            {
                if (RngDocs[i].DocCod == RngDoc)
                {
                    k = i;
                    break;
                }
            }
            if (k == -1)
            {
                System.Windows.Forms.MessageBox.Show("No se conoce documento sobre el cual se pide el número.", MODGRNG.MsgRng, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return SgteNumOpr2;
            }
            RngGeneral = RngDocs[k].DocGen;
            VRng.RngDoc = RngDocs[k].DocCod;
            if (RngGeneral != 0)
            {
                VRng.RngCct = MODXDOC.UsrEsp.CentroCosto;
                VRng.RngEsp = "00";
                VRng.RngRut = "0000000000";
            }
            else
            {
                VRng.RngCct = MODXDOC.UsrEsp.CentroCosto;
                VRng.RngEsp = MODXDOC.UsrEsp.Especialista;
                VRng.RngRut = MODXDOC.UsrEsp.rut;
            }

            x = SyGetUpd_Rng(VRng.RngCct, VRng.RngEsp, VRng.RngDoc).ToInt();

            // Se obtuvo el número exitosamente.
            if (x != 0)
            {
                q = VRng.RngAct;
                if (VRng.RngAct < VRng.RngSup)
                {
                    VRng.RngAct = VRng.RngAct + 1;
                    x = SyPut_Rng(service);
                    if (x != 0)
                    {
                        MODXDOC.Codop.Cent_costo = MODXDOC.UsrEsp.CentroCosto;
                        MODXDOC.Codop.Id_Product = RngPro;
                        MODXDOC.Codop.Id_Especia = MODXDOC.UsrEsp.Especialista;
                        MODXDOC.Codop.Id_Operacion = MigrationSupport.Utils.Format(q, "00000");
                        SgteNumOpr2 = true.ToInt();
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Ya se han asignado todos los numeros permitidos para esta operación.", MODGRNG.MsgRng, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            return SgteNumOpr2;
        }
        public static int Srm_LeeRngSy()
        {
            int Srm_LeeRngSy = 0;

            int x = 0;

            // Conección a Servidor para Documentos Valorados.
            x = SyGetUpd_Rng(VRng.RngCct, VRng.RngEsp, VRng.RngDoc).ToInt();
            if (x != 0)
            {
                VRng.RngInf_t = MigrationSupport.Utils.Format(VRng.RngInf, "0000000000");
                VRng.RngSup_t = MigrationSupport.Utils.Format(VRng.RngSup, "0000000000");
                VRng.RngAct_t = MigrationSupport.Utils.Format(VRng.RngAct, "0000000000");
                Srm_LeeRngSy = true.ToInt();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Se produjo un error en la operación con el Servidor de Datos.", MODGRNG.MsgRng, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return Srm_LeeRngSy;
        }
        // Pone una entrada en la tabla de Rangos.
        public static int Srm_PutRngSy(XegiService service)
        {
            int Srm_PutRngSy = 0;

            int x = 0;

            x = SyPut_Rng(service);
            if (x != 0)
            {
                Srm_PutRngSy = true.ToInt();
                VRng.RngInf_t = MigrationSupport.Utils.Format(VRng.RngInf, "0000000000");
                VRng.RngSup_t = MigrationSupport.Utils.Format(VRng.RngSup, "0000000000");
                VRng.RngAct_t = MigrationSupport.Utils.Format(VRng.RngAct, "0000000000");
            }

            return Srm_PutRngSy;
        }
        // Rescata el máximo número de operación de un documento con respecto al centro
        // de costo, producto y especialista.-
        public static string SyGetMax_Rng(XegiService service, string NumOpe)
        {
            string SyGetMax_Rng = "";

            string MsgCmDoc = "";
            string R = "";
            //string Que = "";

            try
            {

                /*Que = "";
                Que = Que + "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_vrng_s01_MS ";
                Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(1, 3)) + ",";
                Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(4, 2)) + ",";
                Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(6, 2));
                Que = Que.LCase();

                // Se ejecuta la consulta.-
                R = MODGSRM.RespuestaQuery(ref Que);*/

                R = service.sce_vrng_s01_MS(MODGSYB.dbcharSy(NumOpe.Mid(1, 3)), MODGSYB.dbcharSy(NumOpe.Mid(4, 2)), MODGSYB.dbcharSy(NumOpe.Mid(6, 2)));

                // Error de consulta.-
                if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer los datos de la Compra de Documentos. El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), MsgCmDoc);
                    return SyGetMax_Rng;
                }

                // La consulta se hizo pero no resultó exitosa.-
                if (R == "")
                {
                    return SyGetMax_Rng;
                }

                // La consulta se hizo pero no resultó exitosa.-
                SyGetMax_Rng = MODGSYB.GetPosSy(MODGSYB.NumIni(), "N", R).ToStr();

                return SyGetMax_Rng;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);                
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }            
        }
        // Lee las Listado de Números de Operaciones.
        // Retorno    <> ""  : Lectura Exitosa.
        //            =  ""  : Error o Lectura no Exitosa.
        public static int SyGetn_xTrng(XegiService service)
        {
            int SyGetn_xTrng = 0;

            int i = 0;
            string R = "";
            string Que = "";
            int n = 0;

            string ResultadoQuery = "";

            // Verifica si ya se leyó la tabla.-
            n = RngDocs.GetUpperBound(0);

            if (n == 0)
            {
                RngDocs = new Type_RngDoc[1];
            }
            if (n > 0)
            {
                SyGetn_xTrng = true.ToInt();
                return SyGetn_xTrng;
            }

            // OnErrorGoto(SyGetn_xTrngErr);

            /*Que = "";
            Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_trng_s01_MS ";
            Que = Que.LCase();

            R = MODGSRM.RespuestaQuery(ref Que);*/

            R = service.sce_trng_s01_MS();
            if (R == "-1")
            {
                MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer los Números de Operaciones Posibles (Sce_Trng). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<
                   MigrationSupport.MsgBoxStyle>(), "Números de Operaciones");
                return SyGetn_xTrng;
            }

            // Resultado nulo de la Consulta.
            if (R == "")
            {
                return SyGetn_xTrng;
            }

            n = MODGSRM.RowCount;
            RngDocs = new Type_RngDoc[n + 1];
            for (i = 1; i <= n; i += 1)
            {
                RngDocs[i].DocCod = MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).ToStr().TrimB();
                RngDocs[i].DocGen = MODGSYB.GetPosSy(MODGSYB.NumSig(), "L", R).ToInt();
                R = MODGSRM.NuevaRespuesta(2, R);
            }

            SyGetn_xTrng = true.ToInt();

            return SyGetn_xTrng;

            //TODO ARKANO MigrationSupport.Utils.MsgBox("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescrption(MigrationSupport.GlobalException.Instance.Number),MODGDOC.Pito(48).Cast<
            //TODO ARKANO MigrationSupport.MsgBoxStyle>(),"Números de Operaciones");
            // Resume SyGetn_xTrngEnd;

            return SyGetn_xTrng;
        }
        // Lee y actualiza los números de Rangos.
        public static double SyGetUpd_Rng(string CodCct, string CodEsp, string CodFun)
        {
            double SyGetUpd_Rng = 0.0;

            string R = "";
            string Que = "";

            string ResultadoQuery = "";
            try
            {

                /*Que = "";
                Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_rng_u01_MS ";
                Que = Que.LCase();
                Que = Que + MODGSYB.dbcharSy(CodCct.TrimB()) + ",";
                Que = Que + MODGSYB.dbcharSy(CodEsp.TrimB()) + ",";
                Que = Que + MODGSYB.dbcharSy(CodFun.TrimB());

                // Ejecuta Query.
                R = MODGSRM.RespuestaQuery(ref Que);*/

                // Hubo Error.
                if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer los datos de la Asignación de Rangos (Sce_Rng). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), MODGRNG.MsgRng);
                    return SyGetUpd_Rng;
                }

                // Se realizó el Query pero la consulta no retornó datos.
                if (R == "")
                {
                    MigrationSupport.Utils.MsgBox("La Asignación de Rangos no ha sido encontrada (Sce_Rng). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
                       MODGRNG.MsgRng);
                    return SyGetUpd_Rng;
                }

                SyGetUpd_Rng = MODGSYB.GetPosSy(MODGSYB.NumIni(), "N", R).ToDbl();

                return SyGetUpd_Rng;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }            
        }
        // Actualiza un número en Tabla de Rangos.
        public static int SyPut_Rng(XegiService service)
        {
            int SyPut_Rng = 0;

            string R = "";
            string Que = "";

            try
            {

                /*Que = Que + "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_rng_i01_MS ";
                Que = Que.LCase();
                Que = Que + MODGSYB.dbcharSy(VRng.RngCct) + ", ";
                Que = Que + MODGSYB.dbcharSy(VRng.RngEsp) + ", ";
                Que = Que + MODGSYB.dbcharSy(VRng.RngDoc) + ", ";
                Que = Que + MODGSYB.dbcharSy(VRng.RngRut) + ", ";
                Que = Que + MODGSYB.dbnumesy(((int)VRng.RngInf)) + ", ";
                Que = Que + MODGSYB.dbnumesy(((int)VRng.RngSup)) + ", ";
                Que = Que + MODGSYB.dbnumesy(((int)VRng.RngAct));

                // Se ejecuta el Procedimiento Almacenado.
                R = MODGSRM.RespuestaQuery(ref Que);*/

                R = service.sce_rng_i01_MS(
                    MODGSYB.dbcharSy(VRng.RngCct),
                    MODGSYB.dbcharSy(VRng.RngEsp),
                    MODGSYB.dbcharSy(VRng.RngDoc),
                    MODGSYB.dbcharSy(VRng.RngRut),
                    MODGSYB.dbnumesy(((int)VRng.RngInf)),
                    MODGSYB.dbnumesy(((int)VRng.RngSup)),
                    MODGSYB.dbnumesy(((int)VRng.RngAct)));

                if (MODGSRM.HayErr_Com(R) != 0)
                {
                    MigrationSupport.Utils.MsgBox("Se ha producido un error de Comunicación al tratar de grabar los datos de la Asignación de Rangos (Sce_Rng). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.",
                       MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), MODGRNG.MsgRng);
                    return SyPut_Rng;
                }
                if (MODGSRM.HayErr_Syb(R) != 0)
                {
                    MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de grabar los datos de la Asignación de Rangos (Sce_Rng). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<
                       MigrationSupport.MsgBoxStyle>(), MODGRNG.MsgRng);
                    return SyPut_Rng;
                }
                SyPut_Rng = true.ToInt();

                return SyPut_Rng;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);                
                throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }            
        }
    }
}
