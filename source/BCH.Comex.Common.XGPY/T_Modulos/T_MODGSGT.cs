using VBNET = Microsoft.VisualBasic;

namespace BCH.Comex.Common.XGPY.T_Modulos
{


    public struct Type_ParamSgt
    {
        [VBNET.VBFixedString(8), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Nodo;
        [VBNET.VBFixedString(8), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Servidor;
        public string Vista;
        [VBNET.VBFixedString(8100), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 8100)]
        public string Mensaje;
        public int largo;
        [VBNET.VBFixedString(8), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Status;
        [VBNET.VBFixedString(8), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Funcion;
        [VBNET.VBFixedString(2), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 2)]
        public string Contexto;
        [VBNET.VBFixedString(10000), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 10000)]
        public string Control;
        public string VisLee;
        public string VisGra;
        public string VisEli;
        public string VisClt;
        [VBNET.VBFixedString(8), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 8)]
        public string SerLee;
        [VBNET.VBFixedString(8), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 8)]
        public string SerGra;
        [VBNET.VBFixedString(8), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 8)]
        public string NodoEjc;
        [VBNET.VBFixedString(8), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 8)]
        public string ServEjc;
        [VBNET.VBFixedString(8), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 8)]
        public string BDEjc;
    }


    public struct T_Tmc
    {
        public double ValTmc;
        public string FecTas;
    }
    public class T_VTasa
    {
        public int CodTas;
        public string FecTas;
        public int PlaMax;
        public int PlaMin;
        public double cosfon;
        public double TasBas;
        public double TasMin;
        public double tasmax;
    }
    public class T_RegSgt
    {
        public int NumReg;
        public string DatReg;
    }

    public class T_MODGSGT
    {
        public static T_Tmc[] VTmc = null;
        public static T_VTasa VTasa = new T_VTasa();
        public static T_RegSgt[] VRegSgt = null;
        public Type_ParamSgt ParamSgt = new Type_ParamSgt();

        public T_MODGSGT()
        {
            VTmc = new T_Tmc[0];
            VTasa = new T_VTasa();
            VRegSgt = new T_RegSgt[0];
            ParamSgt = new Type_ParamSgt();
        }
    }

}
