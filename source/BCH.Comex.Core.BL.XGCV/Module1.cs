using System.Runtime.InteropServices;
using VBNET = Microsoft.VisualBasic;

namespace BCH.Comex.Core.BL.XGCV
{
   public static class Module1
   {
      // *************************************************************************************************
      //  se declaran estas funciones para tener acceso a las vistas MPN
      // **********************************************************************************************
      [DllImport(@"c:\bin\f\srmw8.dll")]
      public static extern int srmw8(string Nodo,string Servidor,string Mensaje,ref object largo,string Status,string Funcion,string Contexto,string CONTROLES);
      [DllImport(@"c:\bin\f\srmw8.dll")]
      public static extern int srmw8ex(string Nodo,string Servidor,string Mensaje,ref object largo,string Status,string Funcion,string Contexto,string CONTROLES,string AliasPC,int NumSeg,int NumMil);
      // ****************************************************************************
      // estructura para utilizar los ejecutivos.   (MPN)
      // ****************************************************************************
      private const string MsgSgt = "Módulo General SGT";
      public struct Type_ParamSgt
      {
         [VBNET.VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8)]
         public string Nodo;
         [VBNET.VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8)]
         public string Servidor;
         public string Vista;
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
         public string VisLee;
         public string VisGra;
         public string VisEli;
         public string VisClt;
         [VBNET.VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8)]
         public string SerLee;
         [VBNET.VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8)]
         public string SerGra;
         [VBNET.VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8)]
         public string NodoEjc;
         [VBNET.VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8)]
         public string ServEjc;
         [VBNET.VBFixedString(8),System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr,SizeConst=8)]
         public string BDEjc;
      }
      public static Type_ParamSgt ParamSgt = new Type_ParamSgt();
      public struct CliEsp
      {
         public string nrut;
         public string tipo;
         public string ofieje;
         public string codeje;
         public string feccre;
         public string rutope;
         public string drutope;
         public string filler;
         public int modifica;
         public int nuevo;
         public int borrar;
      }
      public static CliEsp[] VSGTCliEsp = null;
      public static CliEsp[] RSGTCliEsp = null;
      public const string SGT_tipopimp = "03";
      public const string SGT_tipopexp = "04";
      public const string SGT_tipnegoc = "05";
      public static int Cliente_SGT = 0;
      public static int Hab_SGTCliEje = 0;
      public struct tipo_riesgo
      {
         public string codigo;
         public string nombre;
         public string telefono;
         public string Fax;
         public string email;
      }
      public struct T_Especialista
      {
         public string codofi;
         public string codejc;
         public string rut;
         public string nombre;
         public string tipo;
         public string telefono;
         public string Fax;
         public string email;
      }
      public static T_Especialista[] VEjc = null;
      // ***************************************************************************
      //   variables globales para sacar especialista.  (MPN)
      // ***************************************************************************
      public static tipo_riesgo[] ejecutivos = null;
      public static string rutiparty = "";
      public const string EJCOPIMP = "3";
      public const string EJCOPEXP = "4";
      public const string EJCNEGOC = "5";
      public const int MB_ICONEXCLAMATION = 48;
   }
}
