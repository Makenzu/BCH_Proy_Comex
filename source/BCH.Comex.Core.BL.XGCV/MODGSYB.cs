using System;

namespace BCH.Comex.Core.BL.XGCV
{
   public static class MODGSYB
   {
      // Guarda las estructuras de las Tablas.-
      public static int SyContador = 0;
      // Retorna un valor string formateado para operar con Sybase.-
      // Los valores con caracteres alt(126) {~} se reemplazan con alt(124) {|}.-
      public static string dbcharSy(string valor)
      {
         string dbcharSy = "";

         string s = "";

         s = valor;
         if (s.InStr("~",1,StringComparison.CurrentCulture) != 0)
         {
            s = MODGDOC.Componer(s,"~","|");
         }
         if (s.InStr("'",1,StringComparison.CurrentCulture) != 0)
         {
            s = MODGDOC.Componer(s,"'","´");
         }
         dbcharSy = "'" + s + "'";

         return dbcharSy;
      }
      // Retorna un valor fecha formateado para operar con Sybase.-
      public static string dbdatesy(string valor)
      {
         string dbdatesy = "";


         dbdatesy = "'" + MigrationSupport.Utils.Format(valor,"mm/dd/yyyy") + "'";

         return dbdatesy;
      }
      // Retorna un valor fecha + hora formateado para operar con Sybase.-
      public static string dbdatetimeSy(string valor)
      {
         string dbdatetimeSy = "";


         dbdatetimeSy = "'" + MigrationSupport.Utils.Format(valor,"mm/dd/yyyy") + " " + MigrationSupport.Utils.Format(valor,"hh:nn:ss") + "'";

         return dbdatetimeSy;
      }
      // Retorna un valor lógico (bit : 0 ó 1 ) formateado para operar con Sybase.-
      public static string dblogisy(bool valor)
      {
         string dblogisy = "";


         if (valor)
         {
            dblogisy = "1";

         }
         else
         {
            dblogisy = "0";
         }

         return dblogisy;
      }
      public static string dbmontoSy(object valor)
      {
         string dbmontoSy = "";

         double Monto = 0.0;

         Monto = MigrationSupport.Utils.Format(valor,"#0.00").ToDbl();
         dbmontoSy = (Monto.ToInt()).Str().TrimB();

         return dbmontoSy;
      }
      // Retorna un valor numérico formateado para operar con Sybase.-
      public static string dbnumesy(int valor)
      {
         string dbnumesy = "";


         dbnumesy = valor.Str().TrimB();

         return dbnumesy;
      }
      public static string dbPardSy(object valor)
      {
         string dbPardSy = "";

         double parid = 0.0;

         parid = MigrationSupport.Utils.Format(valor,"#0.00000000").ToDbl();
         dbPardSy = (parid.ToInt()).Str().TrimB();

         return dbPardSy;
      }
      public static string dbtasaSy(object valor)
      {
         string dbtasaSy = "";

         double Tasa = 0.0;

         Tasa = MigrationSupport.Utils.Format(valor,"#0.000000").ToDbl();
         dbtasaSy = (Tasa.ToInt()).Str().TrimB();

         return dbtasaSy;
      }
      public static string dbTCamSy(object valor)
      {
         string dbTCamSy = "";

         double TipCam = 0.0;

         TipCam = MigrationSupport.Utils.Format(valor,"#0.0000").ToDbl();
         dbTCamSy = (TipCam.ToInt()).Str().TrimB();

         return dbTCamSy;
      }
      // Retorna el valor de un campo teniendo el resultado de la consulta.-
      public static double GetPosSy(int Posicion,string tipo,string Resultado)
      {
         double GetPosSy = 0.0;

         string h = "";
         string Hora = "";
         string Fecha = "";
         string f = "";
         string ResultQuery = "";

         // TipoCampo=[N/C/F/L]: Numérico, Caracter, Fecha, Lógico.-
         ResultQuery = MODGDOC.CopiarDeString(Resultado,"~",Posicion);
         if (tipo == "N")
         {
            GetPosSy = ResultQuery.ToVal();
         }
         else if (tipo == "C")
         {
            ResultQuery = MODGDOC.Componer(ResultQuery,"|","~");
            ResultQuery = MODGDOC.Componer(ResultQuery,"´","'");
            ResultQuery = MODGDOC.Componer(ResultQuery,"¢","ó");
            ResultQuery = MODGDOC.Componer(ResultQuery,"§","º");
            GetPosSy = ResultQuery.ToDbl();
         }
         else if (tipo == "F")
         {
            f = MODGDOC.CopiarDeString(ResultQuery.TrimB()," ",1);
            if (MigrationSupport.Utils.Format(f,"dd/mm/yyyy") == "01/01/1900")
            {
                    // Fecha nula.-
               GetPosSy = "".ToDbl();
            }
            else
            {
               // GetPosSy = GetFechaOK(f$)
               GetPosSy = f.ToDbl();
            }
         }
         else if (tipo == "H")
         {
            f = MODGDOC.CopiarDeString(ResultQuery.TrimB()," ",2);
            GetPosSy = (f.Left(8)).ToDbl();
         }
         else if (tipo == "D")
         {
            ResultQuery = ResultQuery.TrimB();
            Fecha = MODGDOC.moverdestring(ref ResultQuery," ",1);
            if (ResultQuery.Right(2) == "PM" || ResultQuery.Right(2) == "AM")
            {
               Hora = ResultQuery.Left(8).TrimB() + ResultQuery.Right(2);
            }
            else
            {
               Hora = ResultQuery.Left(8).TrimB();
            }
            // --------------------
            // OGG    03/03/1999.-
            // If DateValue(Fecha$) <> DateValue("01/01/1900") Then
            if (MigrationSupport.Utils.Format(Fecha,"yyyymmdd") != MigrationSupport.Utils.Format("01/01/1900","yyyymmdd"))
            {
               // OGG    03/03/1999.-
               // --------------------
               f = MigrationSupport.Utils.Format(Fecha.GetDate(),"dd/mm/yyyy");
               h = MigrationSupport.Utils.Format(Hora.GetTime(),"hh:nn:ss");
               GetPosSy = (f + " " + h).ToDbl();
            }
            else
            {
               GetPosSy = "".ToDbl();
            }
         }
         else if (tipo == "f")
         {
            f = ResultQuery;
            if (MigrationSupport.Utils.Format(f,"dd/mm/yyyy") == "01/01/1900")
            {
               GetPosSy = "".ToDbl();
            }
            else
            {
               // GetPosSy = Format$(GetFechaOK(f$), "dd/mm/yyyy")
               GetPosSy = MigrationSupport.Utils.Format(f,"dd/mm/yyyy").ToDbl();
            }
         }
         else if (tipo == "L")
         {
            if (ResultQuery.ToVal() == 0)
            {
               GetPosSy = false.ToDbl();
            }
            else
            {
               GetPosSy = true.ToDbl();
            }
         }
         else if (tipo == "T")
         {
            f = ResultQuery;
            GetPosSy = MigrationSupport.Utils.Format(f,"dd/mm/yyyy").ToDbl();
         }

         return GetPosSy;
      }
      // Inicializa Contador de Campos.-
      public static int NumIni()
      {
         int NumIni = 0;


         SyContador = 1;
         NumIni = SyContador;

         return NumIni;
      }
      // Retorna el contador incrementado.-
      public static int NumSig()
      {

         SyContador = SyContador + 1;
         return SyContador;
      }
   }
}
