using System;
using System.Runtime.InteropServices;

namespace BCH.Comex.Core.BL.XGPY.Modulos
{
    public static class MODGSRM
    {
        [DllImport(@"c:\bin\f\srmw8.dll")]
        public static extern int srmw8(string Nodo, string Servidor, string Mensaje, ref object largo, string Status, string Funcion, string Contexto, string CONTROLES);
        
        public static int RowCount = 0;


        public static string NuevaRespuesta(int Campos, string Respuesta)
        {
            string NuevaRespuesta = "";

            int j = 0;
            int l = 0;

            l = 0;
            for (j = 1; j <= Campos; j += 1)
            {
                l = l + (UTILES.copiardestring(Respuesta, "~", (short)j).ToString().Length) + 1;
            }
            NuevaRespuesta = Respuesta.Right((Respuesta.Len() - l));

            return NuevaRespuesta;
        }


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

    }
}
