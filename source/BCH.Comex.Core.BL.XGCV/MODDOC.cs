using BCH.Comex.Core.Entities.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGCV
{
   public static class MODDOC
   {
      // Constantes de Impresión y Salir.-
      public const int CmdDoc_Imp = 1;
      public const int CmdDoc_Sal = 9;
      // Tabla de Documentos Export.-
      public struct TxDoc
      {
         public string FecEmi;
         public int NroMem;
      }
      public static TxDoc VDocx = new TxDoc();
      // Retorna un arreglo de Lineas para un string de largo especificado.-
      public static void GetLineas(string Dato,ref string[] Arreglo,int Largo,string Tabla)
      {
         int n = 0;
         string s = "";

         Arreglo = new string[1];
         s = Dato;
         while(s != "")
         {
            n = Arreglo.GetUpperBound(0) + 1;
            Array.Resize(ref Arreglo, n + 1);
            Arreglo[n] = s.Left(Largo);
            // No Válido para Swift, sólo cartas.-
            if (Arreglo[n].Mid(255, 1) == " " && Tabla != "s")
            {
               Arreglo[n] = Arreglo[n].Left(254) + "Ç";
            }
            if (s.Len() < Largo)
            {
               s = "";
            }
            else
            {
               s = s.Right((s.Len() - Largo));
            }
         }

      }
      // Lee el Código del Campo Memo para una carta.
      // Retorno    <> ""  : Lectura Exitosa.
      //            =  ""  : Error o Lectura no Exitosa.
      public static int SyGet_xDoc(string NumOpe,int NroCor)
      {
         int SyGet_xDoc = 0;

         string MsgxCob = "";
         string R = "";
         string Que = "";

         string ResultadoQuery = "";
         try
         {

            //Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "Sce_xDoc_s01 ";
            //Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(1, 3)) + " , ";
            //Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(4, 2)) + " , ";
            //Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(6, 2)) + " , ";
            //Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(8, 3)) + " , ";
            //Que = Que + MODGSYB.dbcharSy(NumOpe.Mid(11, 5)) + " , ";
            //Que = Que + MODGSYB.dbnumesy(NroCor);
            //Que = Que.LCase();

            //// Se ejecuta el Query.
            //R = MODGSRM.RespuestaQuery(ref Que);

            List<sce_xdoc_s01_MS_Result> result = XgcvService.Instance.Sce_xDoc_s01(
                NumOpe.Mid(1, 3),
                NumOpe.Mid(4, 2),
                NumOpe.Mid(6, 2),
                NumOpe.Mid(8, 3),
                NumOpe.Mid(11, 5),
                NroCor.ToString());


            // Resultado nulo del Query.
            if (result == null || result.Count == 0)
            {
               //MigrationSupport.Utils.MsgBox("No se encontró el Código del Campo Memo " + MigrationSupport.Utils.Format(NroCor,"0000") + " (Sce_xDoc). [" + NumOpe + MigrationSupport.Utils.Format(NroCor,"000") + "] .El Servidor reporta : [" +
               //   MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),MsgxCob);
               return SyGet_xDoc;
            }

            // Resultado nulo de la Consulta.
            VDocx.FecEmi = result[0].fecing.ToString("dd/mm/yyyy");//MODGSYB.GetPosSy(MODGSYB.NumIni(),"F",R).ToStr();
            VDocx.NroMem = (int)result[0].codmem;//MODGSYB.GetPosSy(MODGSYB.NumSig(),"N",R).ToInt();

            SyGet_xDoc = VDocx.NroMem;

            return SyGet_xDoc;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            MigrationSupport.Utils.MsgBox("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescription(MigrationSupport.GlobalException.Instance.Number),MODGDOC.Pito(48).Cast<
               MigrationSupport.MsgBoxStyle>(),MsgxCob);

         }
         return SyGet_xDoc;
      }
      // Lee un Campo Memo.-
      // Retorno    <> ""  : Lectura Exitosa.-
      //            =  ""  : Error o Lectura no Exitosa.-
      public static string SyGetn_Mem(string Tabla,int CodMem)
      {
         string SyGetn_Mem = "";

         string MsgxCob = "";
         string s = "";
         string z = "";
         int i = 0;
         int n = 0;
         string R = "";
         string Que = "";

         string ResultadoQuery = "";
         try
         {

            // Si no viene el código del memo => retornar vacío.-
            if (CodMem == 0)
            {
               return SyGetn_Mem;
            }

            // Confeccionar consulta.-
            //Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "Sce_Memg_s01 ";
            //Que = Que + MODGSYB.dbcharSy(Tabla) + ", ";
            //Que = Que + MODGSYB.dbnumesy(CodMem);
            //Que = Que.LCase();

            //R = MODGSRM.RespuestaQuery(ref Que);

            R = XgcvService.Instance.Sce_Memg_s01(Tabla, CodMem.ToString());

            //if (R == "-1")
            //{
            //   MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer el Campo Memo (Sce_Mem*). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle
            //      >(),"Lectura de Campos Memo");
            //   return SyGetn_Mem;
            //}

            // Resultado nulo de la Consulta.-
            n = MODGSRM.RowCount;
            for(i = 1; i <= n; i += 1)
            {
               z = MODGSYB.GetPosSy(MODGSYB.NumIni(),"C",R).ToStr();
               switch(Tabla)
               {
               case "s":
                  z = MODGDOC.Componer(z,"*"," ");
                  break;
               case "p":
                  break;
               default:
                  if (z.Mid(255, 1) == "Ç")
                  {
                     z = z.Left(254) + " ";
                  }
                  break;
               }
               s = s + z;
               R = MODGSRM.NuevaRespuesta(1,R);
            }
            SyGetn_Mem = s;

            return SyGetn_Mem;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            MigrationSupport.Utils.MsgBox("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescription(MigrationSupport.GlobalException.Instance.Number),MODGDOC.Pito(48).Cast<
               MigrationSupport.MsgBoxStyle>(),MsgxCob);

         }
         return SyGetn_Mem;
      }
   }
}
