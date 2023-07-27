using System;

namespace BCH.Comex.Core.BL.XEGI.Modulos
{
   public static class Migrado
   {
      //  ****************************************************************************************************
      //  * Módulo que carga variables para Migración de Aplicaciones                                        *
      //  * a través de un archivo INI y valida si la Migración aplica.                                      *
      //  *                                                                                                  *
      //  * Akzio Consultores                                                                                *
      //  * Abril de 2014                                                                                    *
      //  ****************************************************************************************************
      //  Constante para Nombre de Archivo log
      public const string sNomArchivoLog = "C:\\data\\SCE\\SCEXDOC.log";
      //  Variables Globales
      public static string sArchivoIniMig = "";     //  Nombre de Archivo ini
      public static string sApliMigrada = "";     //  Aplicación Migrada
      public static string sBdd = "";     //  Base de Datos para archivo log
      public static string sApliOri = "";     //  Aplicación Origen
      public static string sBaseOri = "";     //  Base de Datos Origen
      public static string sNodoOri = "";     //  Nodo Origen
      public static string sMigOK = "";     //  Estado de la Migración
      public static string resultado_log = "";     //  retorno de esritura en log
      public static string dato_anterior = "";
      public static string rut_base = "";
      //  Variables Locales
      static string sRetorno = "";     //  Respuesta Archivo Ini
      static int iNro = 0;     //  Contador Común
      static int iContBDD = 0;     //  Contador Bases de Datos
      static int iContApli = 0;     //  Contador Aplicaciones
      static int iContNodo = 0;     //  Contador Nodos
      static string[] sAplicacion = new string[100];     //  Aplicaciones
      static string[] sBaseDatos = new string[100];     //  Bases de Datos
      static string[] sNodo = new string[100];     //  Nodos
      static string sBDDMigrada = "";     //  Base de Datos Migrada
      static string sNodoMigrado = "";     //  Nodo Migrado
      static string sNodoBase = "";     //  Nodo de la Baes de Datos
      static string sApplOk = "";     //  Ok Aplicación
      static string sBaseOk = "";     //  Ok Base de Datos
      static string sNodoOk = "";     //  Ok Nodo
      static string p_sBaseApli = "";     //  Parametro Base de Datos Origen
      static string p_sApli = "";     //  Parametro Aplicación Origen
      static string p_sNodoApli = "";     //  Parametro Nodo Origen
      static string sCadena = "";     //  Cadena de datos original
      static int iOK = 0;     //  Switch
      public static void Carga_Variables_Migracion()
      {
         //  Abro archivo log para escritura
         //     Open sNomArchivoLog For Append As #2
         // 
         //  **********************************************************************************************************
         //  Inicio Lectura de Aplicaciones desde archivo .ini                                                        *
         //  Rescato cantidad de Aplicaciones a cargar
         sRetorno = sLeer_ArchIni(sArchivoIniMig,"MIG_APLICACIONES","CONT_APLI");
         iContApli = sRetorno.ToInt();

         //     Print #2, Now & " - Carga Variables de Migración"
         //     Print #2, Now & " - [MIG_APLICACIONES]"
         //  Rescato Aplicaciones Origen
         for(iNro = 1; iNro <= iContApli; iNro += 1)
         {
            sRetorno = sLeer_ArchIni(sArchivoIniMig,"MIG_APLICACIONES","APLI" + iNro.ToStr());
            sAplicacion[iNro] = sRetorno;
            //         Print #2, Now & " - " & sAplicacion(iNro)
         }
         //  Fin Lectura de Aplicaciones desde archivo .ini                                                           *
         //  **********************************************************************************************************
         // 
         // 
         //  **********************************************************************************************************
         //  Inicio Lectura de Bases de Datos desde archivo .ini                                                      *
         //  Rescato cantidad de Bases de Datos a cargar
         sRetorno = sLeer_ArchIni(sArchivoIniMig,"MIG_BASE_DATOS","CONT_BDD");
         iContBDD = sRetorno.ToInt();

         //     Print #2, ""
         //     Print #2, Now & " - [MIG_BASE_DATOS]"
         //  Rescato Base de Datos Origen
         for(iNro = 1; iNro <= iContBDD; iNro += 1)
         {
            sRetorno = sLeer_ArchIni(sArchivoIniMig,"MIG_BASE_DATOS","BDD" + iNro.ToStr());
            sBaseDatos[iNro] = sRetorno;
            //         Print #2, Now & " - " & sBaseDatos(iNro)
         }
         //  Fin Lectura de Bases de Datos desde archivo .ini                                                         *
         //  **********************************************************************************************************
         // 
         // 
         //  **********************************************************************************************************
         //  Inicio Lectura de Nodos desde archivo .ini                                                               *
         //  Rescato cantidad de Bases de Datos a cargar
         sRetorno = sLeer_ArchIni(sArchivoIniMig,"MIG_EQ_NODOS","CONT_NODO");
         iContNodo = sRetorno.ToInt();

         //     Print #2, ""
         //     Print #2, Now & " - [MIG_EQ_NODOS]"
         //  Rescato Nodo Origen
         for(iNro = 1; iNro <= iContNodo; iNro += 1)
         {
            sRetorno = sLeer_ArchIni(sArchivoIniMig,"MIG_EQ_NODOS","NODO" + iNro.ToStr());
            sNodo[iNro] = sRetorno;
            //         Print #2, Now & " - " & sNodo(iNro)
         }
         //  Fin Lectura de Nodos desde archivo .ini                                                                  *
         //  **********************************************************************************************************
         // 
         // 
         //  **********************************************************************************************************
         //  Inicio Lectura de Nodo Migración desde archivo .ini                                                      *
         //     Print #2, ""
         //     Print #2, Now & " - [MIG_NODO]"
         //  Rescato Nodo Migrado
         sRetorno = sLeer_ArchIni(sArchivoIniMig,"MIG_NODO","NODO");
         sNodoMigrado = sRetorno;
         //     Print #2, Now & " - " & sNodoMigrado
         //     Print #2, ""
         //    Print #2, ""
         //  Fin Lectura de Nodo Migración desde archivo .ini                                                         *
         //  **********************************************************************************************************
         // 
         //  Cierro el archivo log
         //     Close #2

      }
      public static void Genera_log(string sNomArch,string sOpcion,string lsHost,string lsApps,string lsDatos,int liLargo,string lsStatus,string lsFuncion,string lsContexto,string lsControl)
      {
         int d = 0;
         int iSw = 0;

         iSw = 0;
         for(d = 1; d <= iContBDD; d += 1)
         {
            if (sBdd == sBaseDatos[d].Mid(6))
            {
               iSw = 1;
            }
         }

         if (iSw == 1)
         {
            sBdd = sBdd;
         }
         else
         {
            sBdd = "";
         }

         //     ' Abro archivo log para escritura
         //     Open sNomArch For Append As #2
         // 
         // Imprime Entrada
         //     If sOpcion = "Entrada" Then
         //         Print #2, Now & " - Llamada SRM..."
         //         Print #2, Now & " - Base...: " & Trim(sBdd) & " - Host: " & Trim(lsHost) & " - Apli: " & Trim(lsApps)
         //         Print #2, Now & " - Datos..: " & Trim(lsDatos)
         //         Print #2, Now & " - Largo..: " & Trim(liLargo) & " - Status: " & Trim(lsStatus) & " - Funcion: " & Trim(lsFuncion) & " - Contexto: " & Trim(lsContexto)
         //         Print #2, Now & " - Control: " & Trim(lsControl)
         //     Else ' Imprime Salida
         //         Print #2, Now & " - Respuesta SRM..."
         //         Print #2, Now & " - Base...: " & Trim(sBdd) & " - Host: " & Trim(lsHost) & " - Apli: " & Trim(lsApps)
         //        Print #2, Now & " - Datos..: " & Trim(lsDatos)
         //        Print #2, Now & " - Largo..: " & Trim(liLargo) & " - Status: " & Trim(lsStatus) & " - Funcion: " & Trim(lsFuncion) & " - Contexto: " & Trim(lsContexto)
         //        Print #2, Now & " - Control: " & Trim(lsControl)
         //        Print #2, ""
         //    End If
         // 
         //  Cierro el archivo log
         //    Close #2

      }
      private static string sLeer_ArchIni(string sArchIni,string sSeccion,string sLlave)
      {
         string sLeer_ArchIni = "";


         string retVal = "";
         int worked = 0;
         retVal = new string(0.ToChar(),255);
         worked = MODGDOC.GetPrivateProfileString(sSeccion,sLlave,"0",retVal,retVal.Len(),sArchIni);
         if (worked == 0)
         {
            sLeer_ArchIni = "";
         }
         else
         {
            sLeer_ArchIni = retVal.Left(worked);
         }

         return sLeer_ArchIni;
      }
      public static void Valida_Migracion(ref string p_sBaseApli,ref string p_sApli,ref string p_sNodoApli)
      {
         //  Inicializo Variables
         sApplOk = "";
         sBaseOk = "";
         sNodoOk = "";
         sCadena = "";
         sBaseOri = "";
         sApliOri = "";
         sNodoOri = "";
         sMigOK = "";
         iOK = 0;

         p_sBaseApli = p_sBaseApli.TrimB();
         p_sApli = p_sApli.TrimB();
         p_sNodoApli = p_sNodoApli.TrimB();
         //  Respaldo valor inicial del string Base de Datos origen
         sCadena = p_sBaseApli;
         //  Rescato nombre de la Baes de Datos
         if (p_sBaseApli.Len() > 10)
         {
            iOK = 1;
            p_sBaseApli = p_sBaseApli.Mid(22, 10).TrimB();
         }

         //  Respaldo valores de origen para rollback de variables
         sBaseOri = p_sBaseApli;
         sApliOri = p_sApli;
         sNodoOri = p_sNodoApli;

         //  Valido Aplicación Origen y asigno Aplicación Destino
         sApplOk = Valida_Aplicacion(p_sApli);
         //  Valido Base de Datos Origen y asigno Base de Datos Destino
         sBaseOk = Valida_BaseDatos(p_sBaseApli);
         //  Valido Nodo Origen y asigno Nodo Destino
         sNodoOk = Valida_Nodo(p_sNodoApli);

         //  Abro archivo log para escritura
         //     Open sNomArchivoLog For Append As #2
         //  Valido si aplica ó no aplica Migración

         if (sApplOk == "OK" && sBaseOk == "OK" && sNodoOk == "OK")
         {
            //  Estado de Migración OK, aplica...
            sMigOK = "OK";

            //  Asigno valores Migrados a variables analisadas que cumplen la condición...
            if (iOK == 0)
            {
               p_sBaseApli = sBDDMigrada;     //  Base de Datos Migrada
            }
            else
            {
               p_sBaseApli = sCadena.Mid(1, 21) + sBDDMigrada + new string(' ',10 - sBDDMigrada.Len()) + sCadena.Mid(32);
            }
            p_sApli = sApliMigrada;     //  Aplicación Migrada
            p_sNodoApli = sNodoMigrado;     //  Nodo Migrado

            //         Print #2, Now & " - Validando datos para migración...!!!"
            //         Print #2, Now & " - Existe Aplicación: Ok"
            //         Print #2, Now & " - Existe Base Datos: Ok"
            //         Print #2, Now & " - Existe EQ Nodo...: Ok"
            //         Print #2, Now & " - Se aplica migración para esta instancia...!!!"
         }
         else
         {
            //         Print #2, Now & " - Validando datos para migración...!!!"
            //         If sApplOk = "" Then
            //             Print #2, Now & " - Existe Aplicación: No"
            //         Else
            //             Print #2, Now & " - Existe Aplicación: Ok"
            //         End If
            //         If sBaseOk = "" Then
            //             Print #2, Now & " - Existe Base Datos: No"
            //         Else
            //             Print #2, Now & " - Existe Base Datos: Ok"
            //         End If
            //         If sNodoOk = "" Then
            //             Print #2, Now & " - Existe EQ Nodo...: No"
            //         Else
            //             Print #2, Now & " - Existe EQ Nodo...: Ok"
            //         End If
            //         Print #2, Now & " - No se aplica migración para esta instancia...!!!"

            p_sBaseApli = sCadena;
         }

         //  Cierro el archivo log
         //     Close #2

      }
      private static string Valida_Aplicacion(string p_sApli)
      {
         string Valida_Aplicacion = "";

         int a = 0;

         //  Valido Aplicación Origen y asigno Aplicación Destino Migrada
         for(a = 1; a <= iContApli; a += 1)
         {
            if (sApplOk == "")
            {
               if (p_sApli.UCase() == sAplicacion[a].Mid(1, 4).UCase())
               {
                  sApliMigrada = sAplicacion[a].Mid(6, 4);
                  Valida_Aplicacion = "OK";
                  break;
               }
            }
         }

         return Valida_Aplicacion;
      }
      private static string Valida_BaseDatos(string p_sBaseApli)
      {
         string Valida_BaseDatos = "";

         int b = 0;

         //  Valido Base de Datos Origen y asigno Base de Datos Destino Migrada
         for(b = 1; b <= iContBDD; b += 1)
         {
            if (sBaseOk == "")
            {
               if (p_sBaseApli.UCase() == sBaseDatos[b].Mid(6).UCase())
               {
                  sBDDMigrada = sBaseDatos[b].Mid(6);
                  sNodoBase = sBaseDatos[b].Mid(1, 4);
                  Valida_BaseDatos = "OK";
                  break;
               }
            }
         }

         return Valida_BaseDatos;
      }
      private static string Valida_Nodo(string p_sNodoApli)
      {
         string Valida_Nodo = "";

         int c = 0;

         //  Valido que el Nodo de la Base de Datos sea igual al Nodo de la Aplicación
         if (p_sNodoApli == sNodoBase)
         {
            Valida_Nodo = "OK";
         }
         else
         {
            //  Busco equivalencia del Nodo que trae la Aplicación
            for(c = 1; c <= iContNodo; c += 1)
            {
               if (sNodoOk == "")
               {
                  if (sNodoBase.UCase() == sNodo[c].Mid(1, 4).UCase())
                  {
                     if (p_sNodoApli == sNodo[c].Mid(6, 4))
                     {
                        Valida_Nodo = "OK";
                        break;
                     }
                  }
               }
            }
         }

         return Valida_Nodo;
      }
      public static string Escribe_log(string Tipo_log, string dato_entrada, string nodo_log, string servidor_log, string mensaje_log)
      {
          string Dia_Log = "";
          string Mes_Log = "";
          string Nombre_Log = "";
          int canal = 0;

          Dia_Log = MigrationSupport.Utils.Format(DateTime.Now, "dd");
          Mes_Log = MigrationSupport.Utils.Format(DateTime.Now, "mm");
          Nombre_Log = Migrado.sNomArchivoLog;     //  "c:\data\SMP\TEF" & Mes_Log & Dia_Log & ".LOG"
          //    Open RUTA_DATOS & ARCHIVO_LOG For Append As #1
          canal = MigrationSupport.FileSystem.FreeFile();
          MigrationSupport.FileSystem.FileOpen(canal, Nombre_Log, MigrationSupport.FileSystem.OpenMode.Append, MigrationSupport.FileSystem.OpenAccess.Default, MigrationSupport.FileSystem.OpenShare.Default, -1);

          if (Tipo_log == "I" || Tipo_log == "E")
          {
              MigrationSupport.FileSystem.PrintLine(canal, "*** INICIO DE TRANSACCIÓN ***");
              MigrationSupport.FileSystem.PrintLine(canal, MigrationSupport.Utils.Format(DateTime.Now, "dd/mm/yyyy hh:mm:ss"));
              MigrationSupport.FileSystem.PrintLine(canal, "Proceso General: " + dato_entrada);
              MigrationSupport.FileSystem.PrintLine(canal);
          }

          if (Tipo_log == "F")
          {
              //       If dato_entrada <> dato_anterior Then
              //          dato_anterior = dato_entrada
              MigrationSupport.FileSystem.PrintLine(canal, "Función: " + dato_entrada);
              //       End If
          }

          if (Tipo_log == "E")
          {
              //       Print #canal, "Proceso General: " & dato_entrada
              //       Print #canal, "Función Secundaria: " & dato_entrada
              MigrationSupport.FileSystem.PrintLine(canal, "Antes:");
              if (mensaje_log.Left(8) == "b1225678")
              {
                  rut_base = "b1225678";
                  MigrationSupport.FileSystem.PrintLine(canal, "NAME_BD = ", mensaje_log.Mid(22, 10));
                  MigrationSupport.FileSystem.PrintLine(canal, "NODO_BD= ", nodo_log);
                  MigrationSupport.FileSystem.PrintLine(canal, "SERV_BD= ", servidor_log);
                  MigrationSupport.FileSystem.PrintLine(canal, "ls_sql= ", mensaje_log.Mid(32, 300).TrimR());
              }
              else
              {
                  MigrationSupport.FileSystem.Print(canal, "RUT_USUARIO = ");     // RUT_USUARIO
                  MigrationSupport.FileSystem.Print(canal, "DV_USUARIO = ");     // DV_USUARIO
                  //          Print #canal, "RUT_USUARIO = "; Mid(mensaje_log, 1, 8)
                  MigrationSupport.FileSystem.PrintLine(canal, "Nodo= ", nodo_log);
                  MigrationSupport.FileSystem.PrintLine(canal, "Servidor= ", servidor_log);
                  MigrationSupport.FileSystem.PrintLine(canal, "Llamada= ", mensaje_log.TrimR());
              }
          }

          if (Tipo_log == "S")
          {
              MigrationSupport.FileSystem.PrintLine(canal, "Después:");
              if (rut_base == "b1225678")
              {
                  rut_base = "";
                  if (mensaje_log.Mid(1, 2) == "00")
                  {
                      MigrationSupport.FileSystem.PrintLine(canal, "Retorno= ", mensaje_log.Mid(1, 14));
                  }
                  else
                  {
                      MigrationSupport.FileSystem.PrintLine(canal, "Retorno= ", mensaje_log.Mid(1, 80));
                  }
              }
              else
              {
                  if (mensaje_log.Mid(1, 2) == "00")
                  {
                      MigrationSupport.FileSystem.PrintLine(canal, "Retorno= ", mensaje_log.Mid(1, 10));
                  }
                  else
                  {
                      MigrationSupport.FileSystem.PrintLine(canal, "Retorno= ", Limpia_Data_XML(mensaje_log.TrimR()));
                  }
              }
              MigrationSupport.FileSystem.PrintLine(canal);
          }

          MigrationSupport.FileSystem.FileClose(canal);

          return "";
      }

      public static string Limpia_Data_XML(string Lc_String)
      {
         string Limpia_Data_XML = "";


         int Li_Cont = 0;
         int i = 0;
         string Lc_Limpia_Data = "";

         Li_Cont = 0;
         for(i = 1; i <= Lc_String.Len(); i += 1)
         {
            if (Lc_String.Mid(i, 2) == "**")
            {
               Li_Cont = i;
               Lc_Limpia_Data = Lc_String.Mid(1, Li_Cont - 1);
               break;
            }
         }

         if (Li_Cont == 0)
         {
            Lc_Limpia_Data = Lc_String.TrimB();
         }

         Limpia_Data_XML = Lc_Limpia_Data;

         return Limpia_Data_XML;
      }
   }
}
