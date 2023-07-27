using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGCV
{
   public static class MODFRA
   {
      public const string MsgFra = "Frases Estandares";
      public static string CENTRO_COSTO = "";
      public static string COD_ESPEC = "";
      // forma un nuevo string reemplazando todas las ocurrencias de "Que" por "En"
      // en "Donde".  Si no encuentra ninguna retorna "Donde"
      public static string ComponerUna(string Donde,string Que,string En)
      {
         string ComponerUna = "";

         int Aqui = 0;
         string Sale = "";

         Sale = Donde;
         Aqui = Sale.InStr(Que,1,StringComparison.CurrentCulture);
         if (Aqui != 0)
         {
            Sale = Sale.Left((Aqui - 1)) + En + Sale.Mid((Aqui + Que.Len()));
         }
         ComponerUna = Sale;

         return ComponerUna;
      }
      // ****************************************************************************
      //    1.  Lee una frase dependiendo del código de esta + Idioma.
      //    2.  Luego la concatena con una cadena variable.
      // ****************************************************************************
      public static string SyGet_Fra(int CodFra,string Idioma,string Cadena)
      {
         string SyGet_Fra = "";

         string s = "";
         int i = 0;
         int n = 0;
         int CodMemo = 0;
         string Frase = "";
         List<object> R;
         string Que = "";

         try
         {

            //Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "Sce_Fra_S06 ";
            //Que = Que.LCase();
            //Que = Que + MODGSYB.dbnumesy(CodFra) + " , ";
            //Que = Que + MODGSYB.dbcharSy(Idioma);

            //// Se ejecuta el Query.
            //R = MODGSRM.RespuestaQuery(ref Que);

             R = XgcvService.Instance.Sce_Fra_S06(CodFra.ToString(), Idioma);

             if (R == null)
             {
                 return SyGet_Fra;
             }

            // Resultado nulo de la Consulta.
             Frase = R[0].ToString();
             CodMemo = Convert.ToInt32(R[1]);

            // --------------------------------------
            // Rescata en campo Memo.
            if (CodMemo > 0)
            {
               Frase = MODDOC.SyGetn_Mem("f",CodMemo);
            }

            // --------------------------------------
            // Concatena la Frase con la Cadena variable.
            if (Cadena.TrimB() != "")
            {
               n = MODGDOC.CuentaDeString(Cadena,"~");
               for(i = 1; i <= n; i += 1)
               {
                  s = MODGDOC.CopiarDeString(Cadena,"~",i);
                  if (Frase.TrimB() != "")
                  {
                     Frase = ComponerUna(Frase,"@",s);
                  }
               }
            }

            SyGet_Fra = Frase.TrimB();

            return SyGet_Fra;

         }
         catch(Exception exc)
         {
            //MigrationSupport.GlobalException.Initialize(exc);
            //MigrationSupport.Utils.MsgBox("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescription(MigrationSupport.GlobalException.Instance.Number),MODGDOC.Pito(48).Cast<
            //   MigrationSupport.MsgBoxStyle>(),MODFRA.MsgFra);
             throw;

         }
         return SyGet_Fra;
      }
   }
}
