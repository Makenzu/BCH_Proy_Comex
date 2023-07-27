using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VBNET = Microsoft.VisualBasic;

namespace BCH.Comex.Core.BL.XGCV
{
   public static class MODGSRM
   {
      [DllImport(@"c:\bin\f\srmw8.dll")]
      public static extern int srmw8(string Nodo,string Servidor,string Mensaje,ref object largo,string Status,string Funcion,string Contexto,string CONTROLES);
      [DllImport(@"c:\bin\f\srmw8.dll")]
      public static extern int srmw8ex(string Nodo,string Servidor,string Mensaje,ref object largo,string Status,string Funcion,string Contexto,string CONTROLES,string AliasPC,int NumSeg,int NumMil);
      public const int SRM_CANTBUFF = 8;
      public struct Type_ParamSrm8K
      {
         [VBNET.VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8)]
         public string Nodo;
         [VBNET.VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8)]
         public string Servidor;
         [VBNET.VBFixedString(8100),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8100)]
         public string Mensaje;
         public int largo;
         [VBNET.VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8)]
         public string Status;
         [VBNET.VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8)]
         public string Funcion;
         [VBNET.VBFixedString(2),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=2)]
         public string Contexto;
         [VBNET.VBFixedString(10000),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=10000)]
         public string Control;
         public string base_migname;   //Base    SyBase.-
         public string usuario;   //Usuario SyBase.-
         public int APartirDe;   //A partir de que registro.-
         [VBNET.VBFixedString(4),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=4)]
         public string AliasPC;
      }
      public static Type_ParamSrm8K ParamSrm8k = new Type_ParamSrm8K();
      public const string HeaderSy = "b12256781234W";
      public static int RowCount = 0;
      public static string ResQuery = "";
      public static string[] itemsQuery = null;
      public static string[] itemsQuery2 = null;
      // **********************************************************
      public struct T_Cmd
      {
         public string Cmd;   //Query.-
         public string Tab;   //Tabla.-
         public int Brk;   //Quiebre.-
      }
      public static T_Cmd[] CmdsQuery = null;
      public static string RespuestaQuerySTB(ref string Comando)
      {
         string RespuestaQuerySTB = "";

         string s = "";
         double n = 0.0;
         string R = "";
         string CC_NODO_DST = "";
         int X = 0;
         string BaseSy = "";
         //  D.S.B.

         int numcod = 0;
         int numglo = 0;
         int i = 0;
         int pos = 0;
         int Intentos = 0;
         RowCount = 0;
         Intentos = 0;
         ResQuery = "";
         ParamSrm8k.APartirDe = 1;

      Consulta3:
         Intentos = Intentos + 1;
         Comando = Comando.TrimB();
         BaseSy = ParamSrm8k.base_migname.LCase().TrimB();
         BaseSy = BaseSy + new string(' ',10 - BaseSy.Len());
         // m$ = HeaderSy + Format$(ParamSrm8k.APartirDe, "0000000") + "N" + BaseSy$ + Comando
         ParamSrm8k.Mensaje = Comando;
         ParamSrm8k.largo = Comando.Len();
         ParamSrm8k.Status = "00";
         ParamSrm8k.Funcion = "01";
         ParamSrm8k.Contexto = "00";
         object argTemp1 = ParamSrm8k.largo;
         X = srmw8(CC_NODO_DST,"STB2",ParamSrm8k.Mensaje,ref argTemp1,ParamSrm8k.Status,ParamSrm8k.Funcion,ParamSrm8k.Contexto,ParamSrm8k.Control);
         if (!(X == 0 && ParamSrm8k.Mensaje.Left(2) == "00"))
         {
            if (Intentos <= 1)
            {
               goto Consulta3;
            }
            // RespuestaQuerySTB = "-1"
            // Exit Function
         }

         if (X == 0 && ParamSrm8k.Mensaje.Left(2) == "00")
         {
            RespuestaQuerySTB = "00";
            return RespuestaQuerySTB;
         }

         R = ParamSrm8k.Mensaje.TrimB();

         n = R.Len();

         //  Número de Códigos
         if (n >= 13)
         {
            numcod = (R.Mid(11, 3)).ToInt();
         }
         else
         {
            numcod = 0;
         }

         // 
         //  número de Glosas
         if (n >= 15)
         {
            numglo = (R.Mid(14, 2)).ToInt();
         }
         else
         {
            numglo = 0;
         }

         s = "";

         if (numcod > 0)
         {
            s = R.Mid(18, 3);

         }
         else if (numglo > 0)
         {
            pos = 18;
            for(i = 1; i <= numglo; i += 1)
            {
               s = s + R.Mid(pos, 30);
               pos = pos + 30;
            }

         }


         RespuestaQuerySTB = s;

         // 

         return RespuestaQuerySTB;
      }
      // Retorna el Comando Final.-
      public static string Cmd_Build()
      {
         string Cmd_Build = "";

         int i = 0;
         string Que = "";
         int ultcmd = 0;

         ultcmd = -1;
         ultcmd = CmdsQuery.GetUpperBound(0);


         Que = "";
         if (ultcmd < 1)
         {
            Cmd_Build = Que;
         }
         else
         {
            Que = Que + "declare @r smallint, @rr smallint " + 13.Char();
            Que = Que + "select @rr = 0 " + 13.Char();
            // Que$ = Que$ + "BEGIN TRAN " + Chr$(13)  //Comentado Akzio Migracion Mayo 2014
            for(i = 1; i <= ultcmd; i += 1)
            {
               if (i > 1)
               {
                  Que = Que + "if @r = 0 begin ";
               }
               Que = Que + CmdsQuery[i].Cmd + 13.Char();
               if (CmdsQuery[i].Brk != 0)
               {
                  Que = Que + "if @r <> 0 select @rr = " + i.Str() + 13.Char();
               }
               if (i > 1)
               {
                  Que = Que + "end" + 13.Char();
               }
            }
            Que = Que + "if @r = 0 begin if @@trancount > 0 COMMIT TRAN end else begin  if @@trancount > 0 ROLLBACK TRAN end" + 13.Char();
            Que = Que + "select @rr " + 13.Char();
            Cmd_Build = Que;
         }

         return Cmd_Build;
      }
      // Ejecuta el Comando construido.-
      public static int Cmd_Exe()
      {
         int Cmd_Exe = 0;

         int IndCmd = 0;
         string R = "";
         string Que = "";

         // Construye el Comando.-
         Que = Cmd_Build();
         if (Que != "")
         {
            R = RespuestaQuery(ref Que);
            if (R == "")
            {
               Console.Beep();
               MigrationSupport.Utils.MsgBox("Se ha producido un error al ejecutar la Transacción SyBase. El Servidor reporta : [" + ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
                  "Transacción SyBase");
               return Cmd_Exe;
            }
            if (ParamSrm8k.Mensaje.Left(2) == "99")
            {
               Console.Beep();
               MigrationSupport.Utils.MsgBox("Se ha producido un error de sintaxis. El Servidor reporta : [" + ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),"Transacción SyBase");
               return Cmd_Exe;
            }
            if (HayErr_Com(R) != 0)
            {
               Console.Beep();
               MigrationSupport.Utils.MsgBox("Se ha producido un error de comunicación. El Servidor reporta : [" + ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),"Transacción SyBase");
               return Cmd_Exe;
            }
            IndCmd = MODGSYB.GetPosSy(MODGSYB.NumIni(),"N",R).ToInt();
            if (IndCmd > 0)
            {
               Console.Beep();
               MigrationSupport.Utils.MsgBox("Se ha producido un error al Ejecutar la Transaccion " + CmdsQuery[IndCmd].Tab + ". Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),"Transacción SyBase");
               Cmd_Exe = false.ToInt();
               return Cmd_Exe;
            }
            Cmd_Exe = true.ToInt();
         }
         else
         {
            Cmd_Exe = true.ToInt();
         }

         return Cmd_Exe;
      }
      public static int Cmd_Exe2()
      {
         int Cmd_Exe2 = 0;

         int IndCmd = 0;
         int j = 0;
         int i = 0;
         string R = "";
         string Que = "";

         // Construye el Comando.-
         Que = Cmd_Build();
         if (Que != "")
         {
            R = RespuestaQuery2(ref Que);
            if (R == "")
            {
               Console.Beep();
               MigrationSupport.Utils.MsgBox("Se ha producido un error al ejecutar la Transacción SyBase. El Servidor reporta : [" + ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
                  "Transacción SyBase");
               return Cmd_Exe2;
            }
            if (ParamSrm8k.Mensaje.Left(2) == "99")
            {
               Console.Beep();
               MigrationSupport.Utils.MsgBox("Se ha producido un error de sintaxis. El Servidor reporta : [" + ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),"Transacción SyBase");
               return Cmd_Exe2;
            }

            // Este código debe ser eliminado cuando se utilice la función QueryTime
            if (ParamSrm8k.Mensaje.TrimB().Left(32) == "Estado: 6  Error: 5  ErrorSOR: 9")
            {
               Console.Beep();
               // Cmd_Exe2 = True
               // MsgBox "El servidor no ha respondido, presione aceptar para esperar algunos segundos más y luego continuará.", Pito(48), "Transacción SyBase"
               MigrationSupport.Utils.MsgBox("El servidor no ha respondido. No se sabe aún si se realizó con éxito la grabación, revise en servidor de impresión si se generaron los documentos si no vuelva a grabar.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
                  "Transacción SyBase");
               for(i = 1; i <= 10000000; i += 1)
               {
                  j = 1;
               }
               return Cmd_Exe2;
            }
            else if (HayErr_Com(R) != 0)
            {
               Console.Beep();
               MigrationSupport.Utils.MsgBox("Se ha producido un error de comunicación. El Servidor reporta : [" + ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),"Transacción SyBase");
               return Cmd_Exe2;
            }

            IndCmd = MODGSYB.GetPosSy(MODGSYB.NumIni(),"N",R).ToInt();
            if (IndCmd > 0)
            {
               Console.Beep();
               MigrationSupport.Utils.MsgBox("Se ha producido un error al Ejecutar la Transaccion " + CmdsQuery[IndCmd].Tab + ". Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),"Transacción SyBase");
               Cmd_Exe2 = false.ToInt();
               return Cmd_Exe2;
            }
            Cmd_Exe2 = true.ToInt();
         }
         else
         {
            Cmd_Exe2 = true.ToInt();
         }

         return Cmd_Exe2;
      }
      // Inicializa Arreglo de Comandos.-
      public static void Cmd_Init()
      {
         System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
         CmdsQuery = new T_Cmd[1];

      }
      // Ingresa un Comando al Arreglo.-
      public static int Cmd_Put(string Comando,string Tabla,int Quiebre)
      {
         int Cmd_Put = 0;

         int n = 0;

         try
         {

            // Dimensiona nuevamente el Arreglo.-
            n = CmdsQuery.GetUpperBound(0);
            
            if (n == 0)
            {
               CmdsQuery = new T_Cmd[1];
            }

            // Ingresa el Comando.-
            n = CmdsQuery.GetUpperBound(0) + 1;
            Array.Resize(ref CmdsQuery, n + 1);
            CmdsQuery[n].Cmd = "exec @r = " + Comando.Right((Comando.Len() - 4));
            CmdsQuery[n].Tab = Tabla;
            CmdsQuery[n].Brk = Quiebre;
            Cmd_Put = true.ToInt();

            return Cmd_Put;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            MigrationSupport.Utils.MsgBox("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescription(MigrationSupport.GlobalException.Instance.Number),MODGDOC.Pito(48).Cast<
               MigrationSupport.MsgBoxStyle>(),"Confección Comando SyBase");

         }
         return Cmd_Put;
      }
      // Lee datos para accesar Servidor SyBase.-
      public static int GetDatosSy()
      {
         int GetDatosSy = 0;

         string MsgCVD = "";

         try
         {

            // ------------------------------------------------------------------------
            // Se leen los parámetros para trabajar con SyBase.-
            // ------------------------------------------------------------------------
            ParamSrm8k.Nodo = MODGDOC.GetSceIni("SyBase","Nodo");
            if (ParamSrm8k.Nodo == "")
            {
               MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del Nodo. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),MsgCVD);
               return GetDatosSy;
            }
            ParamSrm8k.Servidor = MODGDOC.GetSceIni("SyBase","Servidor");
            if (ParamSrm8k.Servidor == "")
            {
               MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del Servidor. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),MsgCVD);
               return GetDatosSy;
            }
            ParamSrm8k.base_migname = MODGDOC.GetSceIni("SyBase","Base");
            if (ParamSrm8k.base_migname == "")
            {
               MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación de la Base SyBase. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),MsgCVD);
               return GetDatosSy;
            }
            ParamSrm8k.usuario = MODGDOC.GetSceIni("SyBase","Usuario");
            if (ParamSrm8k.usuario == "")
            {
               MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del archivo SyBase. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),MsgCVD);
               return GetDatosSy;
            }
            ParamSrm8k.AliasPC = MODGDOC.GetSceIni("Identificacion","Alias");
            if (ParamSrm8k.AliasPC == "")
            {
               MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del Alias. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),MsgCVD);
               return GetDatosSy;
            }
            GetDatosSy = true.ToInt();

            return GetDatosSy;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            MigrationSupport.Utils.MsgBox("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescription(MigrationSupport.GlobalException.Instance.Number),MODGDOC.Pito(48).Cast<
               MigrationSupport.MsgBoxStyle>(),"Acceso a Parametros SyBase");

         }
         return GetDatosSy;
      }
      // Retorna el número de ocurrencias en la Consulta.-
      public static int getocurrs()
      {
         int getocurrs = 0;

         string s = "";

         s = ParamSrm8k.Mensaje.Mid(4, 7);
         getocurrs = s.ToInt();

         return getocurrs;
      }
      // True   : Hay Error de Comunicación.-
      // False  : NO Hay Error de Comunicación.-
      public static int HayErr_Com(string Respuesta)
      {
         int HayErr_Com = 0;


         if (Respuesta == "-1")
         {
            HayErr_Com = true.ToInt();
         }
         else
         {
            HayErr_Com = false.ToInt();
         }

         return HayErr_Com;
      }
      // True   : Hay Error de SyBase.-
      // False  : NO Hay Error de SyBase.-
      public static int HayErr_Syb(string Respuesta)
      {
         int HayErr_Syb = 0;


         HayErr_Syb = true.ToInt();
         if (Respuesta.Left(1) == "0")
         {
            HayErr_Syb = false.ToInt();
         }

         return HayErr_Syb;
      }
      // Retorno    True    : Si NO hay más registros que traer.-
      //            False   : Si    hay más registros que traer.-
      public static int NoHayMasRegistros()
      {
         int NoHayMasRegistros = 0;


         if (ParamSrm8k.APartirDe > 1)
         {
            NoHayMasRegistros = false.ToInt();
         }
         else
         {
            NoHayMasRegistros = true.ToInt();
         }

         return NoHayMasRegistros;
      }
      // Confecciona una nueva Respuesta borrando los campos ya leidos.-
      public static string NuevaRespuesta(int Campos,string Respuesta)
      {
         string NuevaRespuesta = "";

         int j = 0;
         int l = 0;

         l = 0;
         for(j = 1; j <= Campos; j += 1)
         {
            l = l + MODGDOC.CopiarDeString(Respuesta,"~",j).Len() + 1;
         }
         NuevaRespuesta = Respuesta.Right((Respuesta.Len() - l));

         return NuevaRespuesta;
      }
      public static int QueryLong(ref string Comando)
      {
         int QueryLong = 0;

         int Ind = 0;
         string s = "";
         double n = 0.0;
         string R = "";
         int X = 0;
         string m = "";
         string BaseSy = "";

         // Dim ResQuery   As String
         int Intentos = 0;
         RowCount = 0;
         Intentos = 0;
         ResQuery = "";
         ParamSrm8k.APartirDe = 1;
         itemsQuery = new string[1];

      Conque:
         Intentos = Intentos + 1;
         Comando = Comando.TrimB();
         BaseSy = ParamSrm8k.base_migname.LCase().TrimB();
         BaseSy = BaseSy + new string(' ',10 - BaseSy.Len());
         m = MODGSRM.HeaderSy + MigrationSupport.Utils.Format(ParamSrm8k.APartirDe,"0000000") + "N" + BaseSy + Comando;
         ParamSrm8k.Mensaje = m;
         ParamSrm8k.largo = m.Len();
         ParamSrm8k.Status = "00";
         ParamSrm8k.Funcion = "01";
         ParamSrm8k.Contexto = "00";

         //  Akzio Migracion SYBASE - Inicio
         //  Abril 2014
         Migrado.sBdd = ParamSrm8k.Mensaje.Mid(22, 10).TrimB();
         // Call Genera_log(sNomArchivoLog, "Entrada", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje$, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         Migrado.Valida_Migracion(ref Migrado.sBdd,ref ParamSrm8k.Servidor,ref ParamSrm8k.Nodo);

         Migrado.resultado_log = Migrado.Escribe_log("E","QueryLong",ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje);

         object argTemp1 = ParamSrm8k.largo;
         X = srmw8(ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje,ref argTemp1,ParamSrm8k.Status,ParamSrm8k.Funcion,ParamSrm8k.Contexto,ParamSrm8k.Control);
         // Call Genera_log(sNomArchivoLog, "Salida", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje$, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         ParamSrm8k.Nodo = Migrado.sNodoOri;
         ParamSrm8k.Servidor = Migrado.sApliOri;

         Migrado.resultado_log = Migrado.Escribe_log("S",X.Str(),"","",ParamSrm8k.Mensaje);

         //  Akzio Migracion SYBASE - Fin

         if (!(X == 0 && ParamSrm8k.Mensaje.Left(2) == "00"))
         {
            if (Intentos <= 2)
            {
               goto Conque;
            }
            QueryLong = -1;
            return QueryLong;
         }
         R = ParamSrm8k.Mensaje.TrimB();
         n = (R.Mid(4, 7)).ToVal();
         if (n > 0)
         {
            s = R.Right((R.Len() - 14));
         }
         else
         {
            s = "";
         }

         Ind = itemsQuery.GetUpperBound(0);
         
         if (Ind <= MODGSRM.SRM_CANTBUFF - 1)
         {
            Array.Resize(ref itemsQuery, Ind + 2);
            Ind = itemsQuery.GetUpperBound(0);
            itemsQuery[Ind] = s;
         }
         else
         {
            System.Windows.Forms.MessageBox.Show("La consulta quedará incompleta.  El buffer utilizado es más pequeño.  Se recomienda utilizar función QueryLong2().", "", MessageBoxButtons.OK);
         }
         RowCount = (RowCount + n).ToInt();

         if (R.Mid(3, 1) == "S")
         {
            ParamSrm8k.APartirDe = (ParamSrm8k.APartirDe + n).ToInt();
            goto Conque;
         }
         else
         {
            ParamSrm8k.APartirDe = 1;
         }
         QueryLong = Ind;

         return QueryLong;
      }
      public static int QueryLong2(ref string Comando)
      {
         int QueryLong2 = 0;

         int ind2 = 0;
         int Ind = 0;
         string s = "";
         double n = 0.0;
         string R = "";
         int X = 0;
         string m = "";
         string BaseSy = "";

         // Dim ResQuery   As String
         int Intentos = 0;
         RowCount = 0;
         Intentos = 0;
         ResQuery = "";
         ParamSrm8k.APartirDe = 1;
         itemsQuery = new string[1];
         itemsQuery2 = new string[1];

      Conque2:
         System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
         Intentos = Intentos + 1;
         Comando = Comando.TrimB();
         BaseSy = ParamSrm8k.base_migname.LCase().TrimB();
         BaseSy = BaseSy + new string(' ',10 - BaseSy.Len());
         m = MODGSRM.HeaderSy + MigrationSupport.Utils.Format(ParamSrm8k.APartirDe,"0000000") + "N" + BaseSy + Comando;
         ParamSrm8k.Mensaje = m;
         ParamSrm8k.largo = m.Len();
         ParamSrm8k.Status = "00";
         ParamSrm8k.Funcion = "01";
         ParamSrm8k.Contexto = "00";

         //  Akzio Migracion SYBASE - Inicio
         //  Abril 2014
         Migrado.sBdd = ParamSrm8k.Mensaje.Mid(22, 10).TrimB();
         // Call Genera_log(sNomArchivoLog, "Entrada", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje$, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         Migrado.Valida_Migracion(ref Migrado.sBdd,ref ParamSrm8k.Servidor,ref ParamSrm8k.Nodo);

         Migrado.resultado_log = Migrado.Escribe_log("E","QueryLong2",ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje);

         object argTemp1 = ParamSrm8k.largo;
         X = srmw8(ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje,ref argTemp1,ParamSrm8k.Status,ParamSrm8k.Funcion,ParamSrm8k.Contexto,ParamSrm8k.Control);
         // Call Genera_log(sNomArchivoLog, "Salida", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje$, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         ParamSrm8k.Nodo = Migrado.sNodoOri;
         ParamSrm8k.Servidor = Migrado.sApliOri;

         Migrado.resultado_log = Migrado.Escribe_log("S",X.Str(),"","",ParamSrm8k.Mensaje);

         //  Akzio Migracion SYBASE - Fin

         if (!(X == 0 && ParamSrm8k.Mensaje.Left(2) == "00"))
         {
            if (Intentos <= 2)
            {
               goto Conque2;
            }
            QueryLong2 = -1;
            return QueryLong2;
         }
         R = ParamSrm8k.Mensaje.TrimB();
         n = (R.Mid(4, 7)).ToVal();
         if (n > 0)
         {
            s = R.Right((R.Len() - 14));
         }
         else
         {
            s = "";
         }

         System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

         Ind = itemsQuery.GetUpperBound(0);
         
         if (Ind <= MODGSRM.SRM_CANTBUFF - 1)
         {
            Array.Resize(ref itemsQuery, Ind + 2);
            Ind = itemsQuery.GetUpperBound(0);
            itemsQuery[Ind] = s;
         }
         else if (ind2 <= MODGSRM.SRM_CANTBUFF - 1)
         {
            ind2 = itemsQuery2.GetUpperBound(0);
            
            Array.Resize(ref itemsQuery2, ind2 + 2);
            ind2 = itemsQuery2.GetUpperBound(0);
            itemsQuery2[ind2] = s;
         }
         else
         {
            System.Windows.Forms.MessageBox.Show("La consulta quedará incompleta.  El buffer utilizado es más pequeño.", "", MessageBoxButtons.OK);
         }
         RowCount = (RowCount + n).ToInt();

         if (R.Mid(3, 1) == "S")
         {
            ParamSrm8k.APartirDe = (ParamSrm8k.APartirDe + n).ToInt();
            goto Conque2;
         }
         else
         {
            ParamSrm8k.APartirDe = 1;
         }

         QueryLong2 = Ind + ind2;

         return QueryLong2;
      }
      public static int QueryLongTime(ref string Comando,int Segundos)
      {
         int QueryLongTime = 0;

         int Ind = 0;
         string s = "";
         double n = 0.0;
         string R = "";
         int X = 0;
         string m = "";
         string BaseSy = "";

         // Dim ResQuery   As String
         int Intentos = 0;
         RowCount = 0;
         Intentos = 0;
         ResQuery = "";
         ParamSrm8k.APartirDe = 1;
         itemsQuery = new string[1];

      ConqueLT:
         Intentos = Intentos + 1;
         Comando = Comando.TrimB();
         BaseSy = ParamSrm8k.base_migname.LCase().TrimB();
         BaseSy = BaseSy + new string(' ',10 - BaseSy.Len());
         m = MODGSRM.HeaderSy + MigrationSupport.Utils.Format(ParamSrm8k.APartirDe,"0000000") + "N" + BaseSy + Comando;
         ParamSrm8k.Mensaje = m;
         ParamSrm8k.largo = m.Len();
         ParamSrm8k.Status = "00";
         ParamSrm8k.Funcion = "01";
         ParamSrm8k.Contexto = "00";

         //  Akzio Migracion SYBASE - Inicio
         //  Abril 2014
         Migrado.sBdd = ParamSrm8k.Mensaje.Mid(22, 10).TrimB();
         // Call Genera_log(sNomArchivoLog, "Entrada", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje$, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         Migrado.Valida_Migracion(ref Migrado.sBdd,ref ParamSrm8k.Servidor,ref ParamSrm8k.Nodo);

         Migrado.resultado_log = Migrado.Escribe_log("E","QueryLongTime",ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje);

         object argTemp1 = ParamSrm8k.largo;
         X = srmw8ex(ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje,ref argTemp1,ParamSrm8k.Status,ParamSrm8k.Funcion,ParamSrm8k.Contexto,ParamSrm8k.Control,ParamSrm8k.AliasPC,Segundos,0);
         // Call Genera_log(sNomArchivoLog, "Salida", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje$, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         ParamSrm8k.Nodo = Migrado.sNodoOri;
         ParamSrm8k.Servidor = Migrado.sApliOri;

         Migrado.resultado_log = Migrado.Escribe_log("S",X.Str(),"","",ParamSrm8k.Mensaje);

         //  Akzio Migracion SYBASE - Fin
         // 

         if (!(X == 0 && ParamSrm8k.Mensaje.Left(2) == "00"))
         {
            if (Intentos <= 2)
            {
               goto ConqueLT;
            }
            QueryLongTime = -1;
            return QueryLongTime;
         }
         R = ParamSrm8k.Mensaje.TrimB();
         n = (R.Mid(4, 7)).ToVal();
         if (n > 0)
         {
            s = R.Right((R.Len() - 14));
         }
         else
         {
            s = "";
         }

         Ind = itemsQuery.GetUpperBound(0);
         
         if (Ind <= MODGSRM.SRM_CANTBUFF - 1)
         {
            Array.Resize(ref itemsQuery, Ind + 2);
            Ind = itemsQuery.GetUpperBound(0);
            itemsQuery[Ind] = s;
         }
         else
         {
            System.Windows.Forms.MessageBox.Show("La consulta quedará incompleta. El buffer utilizado es más pequeño.  Se recomienda utilizar función QueryLong2().", "", MessageBoxButtons.OK);
         }
         RowCount = (RowCount + n).ToInt();

         if (R.Mid(3, 1) == "S")
         {
            ParamSrm8k.APartirDe = (ParamSrm8k.APartirDe + n).ToInt();
            goto ConqueLT;
         }
         else
         {
            ParamSrm8k.APartirDe = 1;
         }
         QueryLongTime = Ind;

         return QueryLongTime;
      }
      public static string QueryTime(ref string Comando,int Segundos)
      {
         string QueryTime = "";

         string s = "";
         double n = 0.0;
         string R = "";
         int X = 0;
         string m = "";
         string BaseSy = "";

         int Intentos = 0;
         RowCount = 0;
         Intentos = 0;
         ResQuery = "";
         ParamSrm8k.APartirDe = 1;

      ConsultaTime:
         Intentos = Intentos + 1;
         Comando = Comando.TrimB();
         BaseSy = ParamSrm8k.base_migname.LCase().TrimB();
         BaseSy = BaseSy + new string(' ',10 - BaseSy.Len());
         m = MODGSRM.HeaderSy + MigrationSupport.Utils.Format(ParamSrm8k.APartirDe,"0000000") + "N" + BaseSy + Comando;
         ParamSrm8k.Mensaje = m;
         ParamSrm8k.largo = m.Len();
         ParamSrm8k.Status = "00";
         ParamSrm8k.Funcion = "01";
         ParamSrm8k.Contexto = "00";

         //  Akzio Migracion SYBASE - Inicio
         //  Abril 2014
         Migrado.sBdd = ParamSrm8k.Mensaje.Mid(22, 10).TrimB();
         // Call Genera_log(sNomArchivoLog, "Entrada", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje$, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         Migrado.Valida_Migracion(ref Migrado.sBdd,ref ParamSrm8k.Servidor,ref ParamSrm8k.Nodo);

         Migrado.resultado_log = Migrado.Escribe_log("E","QueryTime",ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje);

         object argTemp1 = ParamSrm8k.largo;
         X = srmw8ex(ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje,ref argTemp1,ParamSrm8k.Status,ParamSrm8k.Funcion,ParamSrm8k.Contexto,ParamSrm8k.Control,ParamSrm8k.AliasPC,Segundos,0);
         // Call Genera_log(sNomArchivoLog, "Salida", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje$, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         ParamSrm8k.Nodo = Migrado.sNodoOri;
         ParamSrm8k.Servidor = Migrado.sApliOri;

         Migrado.resultado_log = Migrado.Escribe_log("S",X.Str(),"","",ParamSrm8k.Mensaje);

         //  Akzio Migracion SYBASE - Fin

         if (!(X == 0 && ParamSrm8k.Mensaje.Left(2) == "00"))
         {
            if (Intentos <= 1)
            {
               goto ConsultaTime;
            }
            QueryTime = "-1";
            return QueryTime;
         }
         R = ParamSrm8k.Mensaje.TrimB();
         n = (R.Mid(4, 7)).ToVal();
         if (n > 0)
         {
            s = R.Right((R.Len() - 14));
         }
         else
         {
            s = "";
         }
         ResQuery = ResQuery + s;
         RowCount = (RowCount + n).ToInt();
         if (ResQuery.Len() >= 30000)
         {
            QueryTime = ResQuery;
            return QueryTime;
         }
         if (R.Mid(3, 1) == "S")
         {
            ParamSrm8k.APartirDe = (ParamSrm8k.APartirDe + n).ToInt();
            goto ConsultaTime;
         }
         else
         {
            ParamSrm8k.APartirDe = 1;
         }
         QueryTime = ResQuery;

         return QueryTime;
      }
      // Retorna la respuesta de una Consulta.-
      // RespuestaQuery="" => Hubo error.-
      public static string RespuestaQuery(ref string Comando)
      {
         string RespuestaQuery = "";

         string s = "";
         double n = 0.0;
         string R = "";
         int X = 0;
         string m = "";
         string BaseSy = "";

         int Intentos = 0;
         RowCount = 0;
         Intentos = 0;
         ResQuery = "";
         ParamSrm8k.APartirDe = 1;

      Consulta:
         Intentos = Intentos + 1;
         Comando = Comando.TrimB();
         BaseSy = ParamSrm8k.base_migname.LCase().TrimB();
         BaseSy = BaseSy + new string(' ',10 - BaseSy.Len());
         m = MODGSRM.HeaderSy + MigrationSupport.Utils.Format(ParamSrm8k.APartirDe,"0000000") + "N" + BaseSy + Comando;
         ParamSrm8k.Mensaje = m;
         ParamSrm8k.largo = m.Len();
         ParamSrm8k.Status = "00";
         ParamSrm8k.Funcion = "01";
         ParamSrm8k.Contexto = "00";

         //  Akzio Migracion SYBASE - Inicio
         //  Abril 2014
         Migrado.sBdd = ParamSrm8k.Mensaje.Mid(22, 10).TrimB();
         // Call Genera_log(sNomArchivoLog, "Entrada", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         Migrado.Valida_Migracion(ref Migrado.sBdd,ref ParamSrm8k.Servidor,ref ParamSrm8k.Nodo);

         Migrado.resultado_log = Migrado.Escribe_log("E","RespuestaQuery",ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje);

         object argTemp1 = ParamSrm8k.largo;
         X = srmw8(ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje,ref argTemp1,ParamSrm8k.Status,ParamSrm8k.Funcion,ParamSrm8k.Contexto,ParamSrm8k.Control);
         // Call Genera_log(sNomArchivoLog, "Salida", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         ParamSrm8k.Nodo = Migrado.sNodoOri;
         ParamSrm8k.Servidor = Migrado.sApliOri;

         Migrado.resultado_log = Migrado.Escribe_log("S",X.Str(),"","",ParamSrm8k.Mensaje);

         //  Akzio Migracion SYBASE - Fin
         // 

         if (!(X == 0 && ParamSrm8k.Mensaje.Left(2) == "00"))
         {
            if (Intentos <= 1)
            {
               goto Consulta;
            }
            RespuestaQuery = "-1";
            return RespuestaQuery;
         }
         R = ParamSrm8k.Mensaje.TrimB();
         n = (R.Mid(4, 7)).ToVal();
         if (n > 0)
         {
            s = R.Right((R.Len() - 14));
         }
         else
         {
            s = "";
         }
         ResQuery = ResQuery + s;
         RowCount = (RowCount + n).ToInt();
         if (ResQuery.Len() >= 30000)
         {
            RespuestaQuery = ResQuery;
            return RespuestaQuery;
         }
         if (R.Mid(3, 1) == "S")
         {
            ParamSrm8k.APartirDe = (ParamSrm8k.APartirDe + n).ToInt();
            goto Consulta;
         }
         else
         {
            ParamSrm8k.APartirDe = 1;
         }
         RespuestaQuery = ResQuery;

         return RespuestaQuery;
      }
      public static string RespuestaQuery2(ref string Comando)
      {
         string RespuestaQuery2 = "";

         string s = "";
         double n = 0.0;
         string R = "";
         int X = 0;
         string m = "";
         string BaseSy = "";

         RowCount = 0;
         ResQuery = "";
         ParamSrm8k.APartirDe = 1;

      Consulta2:
         Comando = Comando.TrimB();
         BaseSy = ParamSrm8k.base_migname.LCase().TrimB();
         BaseSy = BaseSy + new string(' ',10 - BaseSy.Len());
         m = MODGSRM.HeaderSy + MigrationSupport.Utils.Format(ParamSrm8k.APartirDe,"0000000") + "N" + BaseSy + Comando;
         ParamSrm8k.Mensaje = m;
         ParamSrm8k.largo = m.Len();
         ParamSrm8k.Status = "00";
         ParamSrm8k.Funcion = "01";
         ParamSrm8k.Contexto = "00";

         //  Akzio Migracion SYBASE - Inicio
         //  Abril 2014
         Migrado.sBdd = ParamSrm8k.Mensaje.Mid(22, 10).TrimB();
         // Call Genera_log(sNomArchivoLog, "Entrada", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         Migrado.Valida_Migracion(ref Migrado.sBdd,ref ParamSrm8k.Servidor,ref ParamSrm8k.Nodo);

         Migrado.resultado_log = Migrado.Escribe_log("E","RespuestaQuery2",ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje);

         object argTemp1 = ParamSrm8k.largo;
         X = srmw8(ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje,ref argTemp1,ParamSrm8k.Status,ParamSrm8k.Funcion,ParamSrm8k.Contexto,ParamSrm8k.Control);
         // Call Genera_log(sNomArchivoLog, "Salida", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         ParamSrm8k.Nodo = Migrado.sNodoOri;
         ParamSrm8k.Servidor = Migrado.sApliOri;

         Migrado.resultado_log = Migrado.Escribe_log("S",X.Str(),"","",ParamSrm8k.Mensaje);

         //  Akzio Migracion SYBASE - Fin

         if (!(X == 0 && ParamSrm8k.Mensaje.Left(2) == "00"))
         {
            RespuestaQuery2 = "-1";
            return RespuestaQuery2;
         }
         R = ParamSrm8k.Mensaje.TrimB();
         n = (R.Mid(4, 7)).ToVal();
         if (n > 0)
         {
            s = R.Right((R.Len() - 14));
         }
         else
         {
            s = "";
         }
         ResQuery = ResQuery + s;
         RowCount = (RowCount + n).ToInt();
         if (ResQuery.Len() >= 30000)
         {
            RespuestaQuery2 = ResQuery;
            return RespuestaQuery2;
         }
         if (R.Mid(3, 1) == "S")
         {
            ParamSrm8k.APartirDe = (ParamSrm8k.APartirDe + n).ToInt();
            goto Consulta2;
         }
         else
         {
            ParamSrm8k.APartirDe = 1;
         }
         RespuestaQuery2 = ResQuery;


         return RespuestaQuery2;
      }
      public static int SeRealizoCierre()
      {

         // Verificar si ya se realizó el cierre diario
         // If VUsr(1).FecOut <> "" Then
         //     If DateValue(VUsr(1).FecOut) = DateValue(Format$(Now, "dd/mm/yy")) Then
         //         MsgBox "Ya se realizó el Cierre de Comercio Exterior de este día.", Pito(48), MsgUsr
         //         SeRealizoCierre = True
         //         Exit Function
         //     End If
         // End If
         // 

         return 0;
      }
      // Realiza el Inicio o Fin de Una Transacción.-
      // Modo   B : BEGIN       TRAN.-
      //        C : COMMIT      TRAN.-
      //        R : ROLLBACK    TRAN.-
      public static int SyTRAN(string Modo)
      {
         int SyTRAN = 0;

         string R = "";
         string Que = "";

         try
         {

            switch(Modo)
            {
            case "B":
               Que = "BEGIN TRAN";
               break;
            case "C":
               Que = "COMMIT TRAN";
               break;
            case "R":
               Que = "ROLLBACK TRAN";
               break;
            }

            // Se ejecuta la consulta.
            R = RespuestaQuery(ref Que);
            if (R == "-1")
            {
               MigrationSupport.Utils.MsgBox("Se ha producido un error al Ejecutar una Transacción. El Servidor reporta : [" + ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),"Transacción SyBase");
               return SyTRAN;
            }

            // Resultado Exitoso de la Consulta.
            SyTRAN = true.ToInt();

            return SyTRAN;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            MigrationSupport.Utils.MsgBox("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescription(MigrationSupport.GlobalException.Instance.Number),MODGDOC.Pito(48).Cast<
               MigrationSupport.MsgBoxStyle>(),"Transacción SyBase");

         }
         return SyTRAN;
      }
   }
}
