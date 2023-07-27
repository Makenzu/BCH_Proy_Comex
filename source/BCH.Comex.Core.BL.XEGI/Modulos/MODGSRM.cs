using System;
using System.Runtime.InteropServices;
using VBNET = Microsoft.VisualBasic;

namespace BCH.Comex.Core.BL.XEGI.Modulos
{
   public static class MODGSRM
   {
      [DllImport(@"c:\bin\f\srmw8.dll")]
      public static extern int srmw8(string Nodo,string Servidor,string Mensaje,ref object largo,string Status,string Funcion,string Contexto,string CONTROLES);
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

      // Retorna la respuesta de una Consulta.-
      // RespuestaQuery="" => Hubo error.-
      public static string RespuestaQuery(ref string Comando)
      {
         string RespuestaQuery = "";

         string s = "";
         double n = 0.0;
         string R = "";
         int x = 0;
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
         x = srmw8(ParamSrm8k.Nodo,ParamSrm8k.Servidor,ParamSrm8k.Mensaje,ref argTemp1,ParamSrm8k.Status,ParamSrm8k.Funcion,ParamSrm8k.Contexto,ParamSrm8k.Control);

         // Call Genera_log(sNomArchivoLog, "Salida", ParamSrm8K.Nodo, ParamSrm8K.Servidor, ParamSrm8K.mensaje, CLng(ParamSrm8K.largo%), ParamSrm8K.Status$, ParamSrm8K.funcion$, ParamSrm8K.Contexto$, ParamSrm8K.Control$)
         ParamSrm8k.Nodo = Migrado.sNodoOri;
         ParamSrm8k.Servidor = Migrado.sApliOri;

         Migrado.resultado_log = Migrado.Escribe_log("S",x.Str(),"","",ParamSrm8k.Mensaje);

         //  Akzio Migracion SYBASE - Fin

         if (!(x == 0 && ParamSrm8k.Mensaje.Left(2) == "00"))
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

   }
}
