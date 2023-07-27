using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VBNET = Microsoft.VisualBasic;

namespace BCH.Comex.Core.BL.XGCV
{
   public static class MODXDOC
   {
      // ****************************************************************************
      [DllImport("User")]
      public static extern int GetFocus();
      [DllImport("User")]
      public static extern int SendMessage(int hWnd,int wMsg,int wparam,object lparam);
      // ****************************************************************************
      // Variables generales para las Cartas de Cobranza Exportaciones.
      // ****************************************************************************
      // Sce_xCob : Cobranza Exportaciones.
      public struct T_xCob
      {
         public string RefExp1;
         public string RefExp2;
         public string RefBcoC;
         public int CodMon;
         public double MtoCob;
         public double MtoInt;
         public double MtoAge;
         public string FecVen;
         public string Condicion;
         public string Mercad_t;
         public string Nemonic;
         public double Cedente1;
         public double Cedente2;
         public string FecIng;
      }
      public static T_xCob VxCob = new T_xCob();
      // ****************************************************************************
      // Identificación de Operación.
      public struct T_Operac
      {
         public string ConRaya;
         public string SinRaya;
         public string RefCli;
      }
      public static T_Operac VOperac = new T_Operac();
      // ****************************************************************************
      // Vencimientos.
      public struct T_Ven
      {
         public string CodMon;
         public string FecVen;
         public double TotVen;
      }
      public static T_Ven[] Vencimientos = null;
      // ****************************************************************************
      // Sce_xLet : Letras de Exportaciones.
      public struct T_xLet
      {
         public int CodMon;
         public double MtoLet;
         public string FecVen;
         public int Status;
         public string CodMon_t;   //Display.
      }
      public static T_xLet[] VxLet = null;
      // ****************************************************************************
      // Documentos de Embarque.
      public struct T_xDem
      {
         public string NomDem;
         public string NumDem_t;   //para control.
         public int Estado;   //para control. 1:Ing 2:Mod 3:Eli.
      }
      public static T_xDem[] VxDem = null;
      // ****************************************************************************
      // Variable para Conocimientos de Embarque.
      public struct T_xCem
      {
         public string NumCem;
         public string FecCem;
         public string EmbDes;
         public string EmbHac;
         public int Estado;   //para control. 1:Ing 2:Mod 3:Eli.
      }
      public static T_xCem[] VxCem = null;
      // ****************************************************************************
      // Definimos el tipo party
      public struct PartyKey
      {
         public string NombreUsado;
         public string DireccionUsado;
         public string CiudadUsado;
         public string EstadoUsado;
         public string ComunaUsado;
         public string PaisUsado;
         public string PostalUsado;
         public string telefono;
         public string Fax;
         public string Telex;
         public string CasBanco;
         public string CasPostal;
         public int Enviara;
      }
      public static PartyKey[] PartysOpe = null;
      // ****************************************************************************
      // Estructura de Atributos del Usuario.
      public struct EstrucUsuarios
      {
         public string cencos;   //Centro Costo.
         public string CodUsr;   //Código del Usuario.
         public string nombre;   //Nombre del Especialista.
         public string Direccion;   //Dirección del Especialista.
         public string Ciudad;   //Ciudad del Especialista.
         public string telefono;   //Teléfono del Especialista.
         public string Fax;   //Fax del Especialista.
      }
      public static EstrucUsuarios UsrEsp = new EstrucUsuarios();
      public static EstrucUsuarios[] UsrEsps = null;
      // ****************************************************************************
      // Almacena los débitos y comisiones que serán desplegados en la carta 610 - 611(Exportador).
      public struct T_Det
      {
         public string Glosa;
         public string Monto;
         public string MonDet;
         public string tipo;
      }
      public static T_Det[] VDet = null;
      // ****************************************************************************
      // Almacena el Tipo de Operación, Número de Operación, Número de Correlativo.
      public struct T_Ope
      {
         public int TipoDoc;
         public string NumOpe;
         public int NumCor;
         public string NumOpe_t;
         public int NumCop;   //numero de copias para las cartas
      }
      public static T_Ope VOpe = new T_Ope();
      // ****************************************************************************
      // Almacena las Distribuciones.
      public struct T_Dist
      {
         public string DisBen;   //Nombre del Beneficiario.
         public string DisVia;   //Nombre de la Vía a Distribuir.
         public string DisCon;   //Número de la Cta.Cte.
         public string DisMon;   //Nemónico de la Moneda.
         public string DisMto;   //Monto Total.
      }
      public static T_Dist[] VDist = null;
      // ****************************************************************************
      // Almacena las monedas y montos  de la información General.
      public struct T_Inf
      {
         public string InfMon1;
         public string InfMto1;
         public string InfMon2;
         public string InfMto2;
         public string InfMon3;
         public string InfMto3;
      }
      public static T_Inf VInf = new T_Inf();
      // ****************************************************************************
      // Almacena el detalle de retorno (carta Nº 612).
      public struct T_Detalle
      {
         public string Orden;
         public string Refer;
         public string Moneda;
         public string Monto;
      }
      public static T_Detalle[] VDetalle = null;
      // ****************************************************************************
      // Almacena las Planillas.
      public struct T_Plan
      {
         public string NroPlan;
         public string NroCod;
         public string NroDecl;
         public string PlaMon;
         public string PlaMto;
      }
      public static T_Plan[] VPlan = null;
      // ****************************************************************************
      // Almacena las Frases Estándares
      public struct T_InsEsp
      {
         public string InsAge;
         public string InsCob;
         public string InsExp;
         public string InsLet;
         public string Frases;
      }
      public static T_InsEsp VInsEsp = new T_InsEsp();
      // ****************************************************************************
      // Almacena los Montos para la carta Nro. 999.
      public struct T_Montos
      {
         public string NemMnd;
         public double Montos;
      }
      public static T_Montos[] VMontos = null;
      // ****************************************************************************
      // Arreglo General de Sce_Pli Compras.
      // Arreglo General de Sce_Pli Ventas.
      public struct T_VgPli
      {
         public string NemMnd;
         public double MtoCVD;
         public double TipCam;
         public double MtpPres;
      }
      public static T_VgPli[] Compras = null;
      public static T_VgPli[] Ventas = null;
      // ****************************************************************************
      // Arreglo General de Sce_Via.
      public struct T_Via
      {
         public string NomBen;
         public string NomVia;
         public string NemMon;
         public double MtoTot;
      }
      public static T_Via[] VxRemesa = null;
      // ****************************************************************************
      // Arreglo General de Sce_Via.
      public struct T_Via1
      {
         public string Descri;
         public string NomVia;
         public string NemMon;
         public double MtoTot;
      }
      public static T_Via1[] VxVia = null;
      // ****************************************************************************
      // Arreglo General de Sce_Ori.
      public struct T_Ori
      {
         public string Descri;
         public string NomOri;
         public string NemMon;
         public double MtoTot;
      }
      public static T_Ori[] VxOri = null;
      // ****************************************************************************
      // Arreglo General de Arbitrajes.
      public struct T_VArb
      {
         public string NemMndC;
         public double MtoCom;
         public string NemMndV;
         public double MtoVta;
         public double PrdArb;
      }
      public static T_VArb[] VArb = null;
      // *******DEFINICION DE CONSTANTES*********************************************
      // Indices de Participantes
      public const int IExp1 = 1;
      public const int IGir = 2;
      public const int ICob = 3;
      public const int IAge = 4;
      // Indicadores de Impresión (Cartas).
      public const int DocGAdeb = 998;     // Aviso de Crédito.
      public const int DocGAcre = 999;     // Aviso de Débito.
      public const int DocxAceLet = 602;     // Aceptación de Letras.
      public const int DocxCobReg = 601;     // Registro Cobranza Export.
      public const int DocxCobRen = 603;     // Reenvío Cobranza Export.
      public const int DocxPagDir = 610;     // Pago Directo Cobranza Export.
      public const int DocxCobCan = 611;     // Cancelación Cobranza Export.
      public const int DocxCanRet = 611;     // Cancelación Retorno Export.
      public const int DocxRegRet = 612;     // Registro Retorno.
      public const int DocxRegPln = 613;     // Planillas Visible Export.
      public const int DocxRegCanRet = 614;     // Registro y Cancelacion Retorno.
      public const int DocCVD = 620;     // Compra Venta.
      public const int DocArb = 621;     // Arbitraje.
      public const int DocCVDI = 402;     // Venta Visibles de Importaciones.
      // ****************************************************************************
      public const string Formato = "#,###,###,###,##0.00";
      public const string FormatoTipCamb = "#,###,##0.0000";
      public const string FormatoParidad = "#,###,##0.0000000000";
      public const string FormatoSinDec = "#,###,###,###,##0";
      public const string MsgxDoc = "Impresión de Documentos";
      // *******DEFINICION DE VARIABLES**********************************************
      // Instrucciones.
      public static string[] InsCob = null;
      public static string[] InsExp = null;
      public static string[] InsAge = null;
      public static string[] Lineas = null;
      // ****************************************************************************
      public static string Memo = "";     // almacena el string de las cartas a imprimir
      public static int linea = 0;     // almacena la línea de la posición de impresión.
      public static int Pagina = 0;     // almacena la página de impresión.
      public static int tabulador = 0;     // indicador de línea de tab para el inicio de los despliegues de información
      public static int tab_doc_letra = 0;     // indicador de línea de tab de Documentos (Letra)
      public static int tab_doc_cod = 0;     // indicador de línea de tab de Documentos (código)
      public static int tab_doc_nem = 0;     // indicador de línea de tab de Documentos (nem)
      public static int tab_doc_monto = 0;     // indicador de línea de tab de Documentos (monto)
      public static int tab_mto_descr = 0;     // indicador de línea de tab de Montos (descripción)
      public static int tab_mto_monto = 0;     // indicador de línea de tab de Montos (monto)
      public static int tab_titulos = 0;     // indicador de línea de tab de los Títulos
      public static int Num_Carta = 0;     // indicador del tipo de carta para efectos de párrafos
      public static int Carta = 0;     // indicador del tipo de carta para efectos de procedimientos en donde se emplearán.
      public static string Instrucciones = "";     // Almacena el Texto Libre que existe en la carta al Exportador.
      public static string MonTotal = "";     // Almacena la moneda que tendrá el total débitos y comisiones
      public static string MtoTotal = "";     // Almacena el monto total de débitos y comisiones.
      public static string TipoPago = "";     // Almacena el Tipo de Pago (Total/Parcial).
      public static string PagoDe = "";     // Almacena de que es el Pago (Cobranza/Retorno).
      public static string TipoDC = "";     // Almacena el tipo de Aviso (D)ébito (C)rédito para el caso de la carta Nro. 999.
      public static string RefDC = "";     // Referencia Débito/Crédito.-
      public static string NroCtaCte = "";     // Almacena el Nro de la Cta. Cte.
      public static string Concepto = "";     // Almacena la descripción del concepto que se debe imprimir el la carta 999.
      public static string Idioma = "";     // Almacena el indicador del tipo de carta por Español o Inglés.
      public static int FraseVa = 0;     // Almacena el indicador de imprimir la frase para el caso de la carta Nro. 611.
      public static double MtoCap = 0.0;     // Monto del Capital para la carta 611.
      public static string Moneda = "";     // Moneda del Monto del Capital para la carta 611.
      public static double MtoInt = 0.0;     // Monto del Interés para la carta 611.
      public static string Total_Parcial = "";     // Almacena la continuación de la frase 6724 con respecto al Tipo de Pago (Total/Parcial).
      public static string SaldoRet = "";     // Saldo del Retorno.
      public static int CuantasCompras = 0;
      public static int CuantasVentas = 0;
      public static string CobRet = "";     // Indica C:Cobranza;R:Retorno.-
      // PACP 29/05/2001
      public struct CdOper
      {
         public string Cent_costo;
         public string Id_Product;
         public string Id_Especia;
         public string Id_Empresa;
         public string Id_Operacion;
      }
      public static CdOper Codop = new CdOper();


        // CFV-10/11/2006
        // Recupera los datos del ejecutivo desde la tabla sce_netd_ejc
        public static int Fn_Buscar_Indice_2(string ls_cencos, string ls_codusr, string ls_rutcli, string ls_codcct)
        {
            int Fn_Buscar_Indice_2 = 0;

            string li_rut = "";
            int m = 0;
            int n = 0;
            List<object> R = null;
            string Que = "";
            int largo = 0;
            try
            {

                largo = UsrEsps.GetUpperBound(0);
                

                /*Que = "exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + ".sce_serv_imp_01 ";
                Que = Que + "'" + ls_rutcli + "', ";
                Que = Que + "'" + ls_codcct + "'";

                // Se ejecuta el Query.
                R = MODGSRM.RespuestaQuery(ref Que);*/

                R = XgcvService.Instance.sce_serv_imp_01(ls_rutcli, ls_codcct);

                // Error en el Query
                //if (R == "-1")
                //{
                //    MigrationSupport.Utils.MsgBox("Error al tratar de leer los datos del ejecutivo de negocios. El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
                //       MODXDOC.MsgxDoc);
                //    goto SyGet_UsrEnd;
                //}

                if (R == null || R.Count == 0)
                {
                    return Fn_Buscar_Indice_2;
                }

                //n = MODGSRM.RowCount;
                //Array.Resize(ref UsrEsps, largo + n + 1);
                //m = UsrEsps.GetUpperBound(0);

                //li_rut = MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).Str().TrimB();
                //UsrEsps[m].cencos = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                //UsrEsps[m].CodUsr = ls_codusr;
                //UsrEsps[m].nombre = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                //UsrEsps[m].Direccion = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                //// UsrEsps(m%).Ciudad = Trim$(GetPosSy(NumSig(), "C", R$))
                //UsrEsps[m].telefono = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).Str().TrimB();
                //UsrEsps[m].Fax = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).Str().TrimB();
                
                m = 0; //TODO ARKANO OJO CON OTROS REPORTES QUE SEA MAYOR A 1
                li_rut = R[0].ToString();
                UsrEsps[m].cencos = R[1].ToString();
                UsrEsps[m].CodUsr = ls_codusr;
                UsrEsps[m].nombre = R[2].ToString();
                UsrEsps[m].Direccion = R[3].ToString();
                // UsrEsps(m%).Ciudad = T
                UsrEsps[m].telefono = R[4].ToString();
                UsrEsps[m].Fax = R[5].ToString();

                Fn_Buscar_Indice_2 = m;                           
            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XgcvException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
            }

            return Fn_Buscar_Indice_2;
        }

      // ****************************************************************************
      //    1.  Concatena tres string separados por ",".
      // ****************************************************************************
      public static string Concatena(string s1,string s2,string s3)
      {
         string Concatena = "";

         string s = "";

         s = s1;
         if (s2 != "")
         {
            if (s != "")
            {
               s = s + ", ";
            }
            s = s + s2;
         }
         if (s3 != "")
         {
            if (s != "")
            {
               s = s + ", ";
            }
            s = s + s3;
         }
         Concatena = s;

         return Concatena;
      }
      // ****************************************************************************
      //    1.  Busca en un Arreglo el índice de este para obtener los datos deseados
      //        de un Especialista.
      //    2.  Si no lo encuentra a el arreglo, lo carga desde Sybase dejándolo luego
      //        en un arreglo.
      // ****************************************************************************
      public static int Fn_Buscar_Indice(string cencos,string CodUsr)
      {
         int Fn_Buscar_Indice = 0;

         int a = 0;
         int Indice = 0;
         int n = 0;

         n = UsrEsps.GetUpperBound(0);

         Fn_Buscar_Indice = 0;
         Indice = 0;
         // ------------------------------------------------------
         // RealSystems - Código Original - Inicio
         // Fecha Comentado: 20100518
         // Responsable: Mauricio Aguilera V.
         // Version: 1
         // Descripcion: Se comenta busqueda en Memoria.
         // ------------------------------------------------------
         // For i% = 1 To n%
         //     If (UsrEsps(i%).cencos = cencos) And (UsrEsps(i%).CodUsr = CodUsr) And (UsrEsps(i%).nombre <> "") Then
         //         Indice% = i%
         //         Exit For
         //     End If
         // Next i%
         // --------------------------------------------------
         // RealSystems - Código Original - Termino
         // --------------------------------------------------
         // 
         // 
         // Si no se encuentra en Memoria, Busca en la Base.
         if (Indice == 0)
         {
            a = SyGet_Usr(cencos,CodUsr);
            if (a != 0)
            {
               Fn_Buscar_Indice = a;
            }
         }
         else
         {
            Fn_Buscar_Indice = Indice;
         }

         return Fn_Buscar_Indice;
      }
      public static double SyGet_Ini(string grupo,string elem,int flgAviso)
      {
         double SyGet_Ini = 0.0;

         string MsgCom = "";
         List<object> R = null;
         string Query = "";

         string tipo = "";
         string valor = "";
         int largo = 0;
         int decim = 0;

         try
         {

             R = XgcvService.Instance.sce_ini_s01(MODGSYB.dbcharSy(grupo), MODGSYB.dbcharSy(elem));

             if (R != null)
             {
                 tipo = R[0].ToString();
                 largo = Convert.ToInt32(R[1]);
                 decim = Convert.ToInt32(R[2]);
                 valor = R[3].ToString();
             }
             else
             {
                 if (flgAviso != 0)
                 {
                     MigrationSupport.Utils.MsgBox("No existe en tabla sce_ini valor para [" + grupo + ", " + elem + "]" + ".", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(), "Tabla sce_ini");
                 }
                 return SyGet_Ini;
             }

             if (tipo == "N")
             {
                 SyGet_Ini = valor.ToVal();
             }
             else if (tipo == "C")
             {
                 SyGet_Ini = valor.ToDbl();
             }

             return SyGet_Ini;

         }
         catch (Exception exc)
         {
             MigrationSupport.GlobalException.Initialize(exc);
             MigrationSupport.Utils.MsgBox("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescription(MigrationSupport.GlobalException.Instance.Number), MODGDOC.Pito(48).Cast<
                MigrationSupport.MsgBoxStyle>(), MsgCom);
         }

         return SyGet_Ini;
      }


      // ****************************************************************************
      //    1.  Retorna una palabra con la primera letra el mayúscula y las demás
      //        en minúsula.
      // ****************************************************************************
      public static string Fn_Minuscula(string Texto)
      {
         string Fn_Minuscula = "";


         string Palabra = "";
         string Pal_Minuscula = "";
         int Largo_Palabra = 0;

         if (Texto.TrimB() == "")
         {
            return Fn_Minuscula;
         }
         Palabra = Texto;
         Largo_Palabra = Palabra.Len() - 1;

         Pal_Minuscula = Palabra.Mid(1, 1).UCase() + Palabra.Mid(2, Largo_Palabra).LCase();
         Fn_Minuscula = Pal_Minuscula.TrimB();


         return Fn_Minuscula;
      }
      public static int GetDatOfi(string CodCct, string oficina)
      {
          int GetDatOfi = 0;

          string R = "";
          string Que = "";
          string RutaSyb = "";

          // If Val(Oficina) = 0 Or InStr("827;829", Codcct) = 0 Then Exit Sub
          // FTF:20032001, Segun rutina JLA

          GetDatOfi = false.ToInt();

          if (oficina.ToVal() == 0)
          {
              return GetDatOfi;
          }

          RutaSyb = MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + ".";

          /*Que = "";
          Que = Que + "exec " + RutaSyb + "sgt_suc_s04 ";
          Que = Que + MODGSYB.dbnumesy(((int)(oficina.ToVal())));

          R = MODGSRM.RespuestaQuery(ref Que);*/

          var result = XgcvService.Instance.sgt_suc_s04(MODGSYB.dbnumesy(((int)(oficina.ToVal()))));

          if (result != null && result.Count > 0)
          {
              UsrEsp.nombre = result[0].suc_succor;// MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).Str();
              UsrEsp.Direccion = result[0].suc_sucdir;// MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();
              UsrEsp.telefono = result[0].suc_sucfon; // MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();
              UsrEsp.Fax = result[0].suc_sucfax;// MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();
          }

          GetDatOfi = true.ToInt();

          return GetDatOfi;
      }

      // ****************************************************************************
      //    1.  Retorna las lineas de una Caja Multilinea en un arreglo de lineas.
      // ****************************************************************************
      public static int GetLines(string Frase,System.Windows.Forms.Control Caja,ref string[] Lineas)
      {
         int GetLines = 0;

         int n = 0;
         int NumChars = 0;
         string x = "";
         int i = 0;
         int NumLines = 0;
         int EM_GETLINE = 0;
         int EM_GETLINECOUNT = 0;
         int WM_USER = 0;

         try
         {

            WM_USER = 0x400;
            EM_GETLINECOUNT = WM_USER + 10;
            EM_GETLINE = WM_USER + 20;
            Caja.Text = Frase;
            Lineas = new string[1];
            NumLines = SendMessage(((int)Caja.Handle),EM_GETLINECOUNT,0,0);
            for(i = 0; i <= NumLines - 1; i += 1)
            {
               x = new string(' ',150);
               NumChars = SendMessage(((int)Caja.Handle),EM_GETLINE,i,x);
               n = Lineas.GetUpperBound(0) + 1;
               Array.Resize(ref Lineas, n + 1);
               Lineas[n] = x.TrimB();
            }

            return GetLines;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            System.Windows.Forms.MessageBox.Show(MigrationSupport.GlobalException.Instance.Number.ToStr() + " en linea  " + VBNET.Information.Erl().ToStr(), "", MessageBoxButtons.OK);

         }
         return GetLines;
      }
      // ****************************************************************************
      //    1.  Rescata la fecha actual de tipo dd de mes de aaaa en Español.
      // ****************************************************************************
      public static string Glosa_Fecha_Hoy_Espanol(string Fecha)
      {
         string Glosa_Fecha_Hoy_Espanol = "";

         string a = "";
         string m = "";
         string d = "";

         d = MigrationSupport.Utils.Format(Fecha,"d");
         m = Glosa_Mes_Espanol(MigrationSupport.Utils.Format(Fecha,"m").ToInt()).ToStr();
         a = MigrationSupport.Utils.Format(DateTime.Now,"yyyy");
         Glosa_Fecha_Hoy_Espanol = d + " de " + m + " de " + a;

         return Glosa_Fecha_Hoy_Espanol;
      }
      // ****************************************************************************
      //    1.  Rescata la fecha actual de tipo dd de mes de aaaa en Inglés.
      // ****************************************************************************
      public static string Glosa_Fecha_Hoy_Ingles(string Fecha)
      {
         string Glosa_Fecha_Hoy_Ingles = "";

         string a = "";
         string m = "";
         string d = "";

         d = MigrationSupport.Utils.Format(Fecha,"d");
         m = Glosa_Mes_Ingles(MigrationSupport.Utils.Format(Fecha,"m").ToInt()).ToStr();
         a = MigrationSupport.Utils.Format(DateTime.Now,"yyyy");
         Glosa_Fecha_Hoy_Ingles = m + " " + d + ", " + a;

         return Glosa_Fecha_Hoy_Ingles;
      }
      // ****************************************************************************
      //    1.  Rescata el mes en español en palabra desde el mes enviado como parámetro de
      //        tipo numerico.
      // ****************************************************************************
      public static string Glosa_Mes_Espanol(int Mes)
      {
         string Glosa_Mes_Espanol = "";


         switch(Mes)
         {
         case 1:
            Glosa_Mes_Espanol = "Enero";
            break;
         case 2:
            Glosa_Mes_Espanol = "Febrero";
            break;
         case 3:
            Glosa_Mes_Espanol = "Marzo";
            break;
         case 4:
            Glosa_Mes_Espanol = "Abril";
            break;
         case 5:
            Glosa_Mes_Espanol = "Mayo";
            break;
         case 6:
            Glosa_Mes_Espanol = "Junio";
            break;
         case 7:
            Glosa_Mes_Espanol = "Julio";
            break;
         case 8:
            Glosa_Mes_Espanol = "Agosto";
            break;
         case 9:
            Glosa_Mes_Espanol = "Septiembre";
            break;
         case 10:
            Glosa_Mes_Espanol = "Octubre";
            break;
         case 11:
            Glosa_Mes_Espanol = "Noviembre";
            break;
         case 12:
            Glosa_Mes_Espanol = "Diciembre";
            break;
         default:
            Glosa_Mes_Espanol = "";
            break;
         }

         return Glosa_Mes_Espanol;
      }
      // ****************************************************************************
      //    1.  Rescata el mes en inglés en palabra desde el mes enviado como parámetro de
      //        tipo numerico.
      // ****************************************************************************
      public static string Glosa_Mes_Ingles(int Mes)
      {
         string Glosa_Mes_Ingles = "";


         switch(Mes)
         {
         case 1:
            Glosa_Mes_Ingles = "January";
            break;
         case 2:
            Glosa_Mes_Ingles = "February";
            break;
         case 3:
            Glosa_Mes_Ingles = "March";
            break;
         case 4:
            Glosa_Mes_Ingles = "April";
            break;
         case 5:
            Glosa_Mes_Ingles = "May";
            break;
         case 6:
            Glosa_Mes_Ingles = "June";
            break;
         case 7:
            Glosa_Mes_Ingles = "July";
            break;
         case 8:
            Glosa_Mes_Ingles = "August";
            break;
         case 9:
            Glosa_Mes_Ingles = "September";
            break;
         case 10:
            Glosa_Mes_Ingles = "October";
            break;
         case 11:
            Glosa_Mes_Ingles = "November";
            break;
         case 12:
            Glosa_Mes_Ingles = "December";
            break;
         default:
            Glosa_Mes_Ingles = "";
            break;
         }

         return Glosa_Mes_Ingles;
      }
      // Imprime la página de la carta.-
      public static void ImprimePagina()
      {
         int i = 0;
         int n = 0;

         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeName(XGCV.Printer.DefInstance.Font, "Times New Roman");
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeSize(XGCV.Printer.DefInstance.Font, 9.84F);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
         n = (24 - XGCV.Printer.DefInstance.CurrentY.ToVal()).ToInt();
         for(i = 1; i <= n; i += 1)
         {
             //TODO:@estanislao revisar si este remplazo esta bien
            //XGCV.Printer.DefInstance.CurrentY = ((int)(XGCV.Printer.DefInstance.CurrentY + 1));
             XGCV.Printer.DefInstance.Print();
         }
         XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(55),Pagina.Str()});

      }
      //[STAThread]
      //public static void main()
      //{

      //   // ------------------------------------------------------------------------
      //   // Se leen los parámetros para trabajar con Sybase.
      //   // ------------------------------------------------------------------------
      //   MODGSRM.ParamSrm8k.Nodo = MODGDOC.GetSceIni("Sybase","Nodo");
      //   if (MODGSRM.ParamSrm8k.Nodo == "")
      //   {
      //      MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del Nodo. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),MODXDOC.MsgxDoc);
      //      Environment.Exit(0);
      //   }
      //   MODGSRM.ParamSrm8k.Servidor = MODGDOC.GetSceIni("Sybase","Servidor");
      //   if (MODGSRM.ParamSrm8k.Servidor == "")
      //   {
      //      MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del Servidor. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),MODXDOC.MsgxDoc);
      //      Environment.Exit(0);
      //   }
      //   MODGSRM.ParamSrm8k.base_migname = MODGDOC.GetSceIni("Sybase","Base");
      //   if (MODGSRM.ParamSrm8k.base_migname == "")
      //   {
      //      MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación de la Base Sybase. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),MODXDOC.MsgxDoc);
      //      Environment.Exit(0);
      //   }
      //   MODGSRM.ParamSrm8k.usuario = MODGDOC.GetSceIni("Sybase","Usuario");
      //   if (MODGSRM.ParamSrm8k.usuario == "")
      //   {
      //      MigrationSupport.Utils.MsgBox("No se pudo encontrar la identificación del archivo Sybase. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema.",MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),MODXDOC.MsgxDoc);
      //      Environment.Exit(0);
      //   }

      //   //  Akzio Migración SYBASE - Inicio
      //   //  Abril 2014
      //   Migrado.sArchivoIniMig = "Sce.ini";
      //   Migrado.Carga_Variables_Migracion();
      //   //  Akzio Migración SYBASE - Fin
      //   // ------------------------------------------------------------------------
      //   System.Windows.Forms.Application.Run(ServxDoc.DefInstance);

      //}

      // Realiza la conversión de Países de Español a Inglés.-
      public static string PaiEnIng(string PaisEsp)
      {
         string PaiEnIng = "";


         string strCaseArg = "";
         try
         {

            strCaseArg = PaisEsp.UCase();
            if (strCaseArg == "AFGHANISTAN")
            {
               PaiEnIng = "Afghanistan";
            }
            else if (strCaseArg == "ALBANIA")
            {
               PaiEnIng = "Albania";
            }
            else if (strCaseArg == "ALEMANIA")
            {
               PaiEnIng = "Germany";
            }
            else if (strCaseArg == "ALTO")
            {
               PaiEnIng = "Volta ";
            }
            else if (strCaseArg == "ANDORRA")
            {
               PaiEnIng = "Andorra";
            }
            else if (strCaseArg == "ANGOLA")
            {
               PaiEnIng = "Angola";
            }
            else if (strCaseArg == "ANTIGUA Y BARBUDA")
            {
               PaiEnIng = "Antigua and Barbuda";
            }
            else if (strCaseArg == "APROVISIONAMIENTO DE NAVES")
            {
               PaiEnIng = "Aprovisionamiento de Naves";
            }
            else if (strCaseArg == "ARABIA SAUDITA")
            {
               PaiEnIng = "Saudi Arabia";
            }
            else if (strCaseArg == "ARGELIA")
            {
               PaiEnIng = "Algeria";
            }
            else if (strCaseArg == "ARGENTINA")
            {
               PaiEnIng = "Argentina ";
            }
            else if (strCaseArg == "ARMENIA")
            {
               PaiEnIng = "Armenia";
            }
            else if (strCaseArg == "AUSTRALIA")
            {
               PaiEnIng = "Australia";
            }
            else if (strCaseArg == "AUSTRIA")
            {
               PaiEnIng = "Austria";
            }
            else if (strCaseArg == "AZERBAIJAN")
            {
               PaiEnIng = "Azerbaijan ";
            }
            else if (strCaseArg == "BAHAMAS, ISLAS")
            {
               PaiEnIng = "Bahamas";
            }
            else if (strCaseArg == "BAHREIN")
            {
               PaiEnIng = "Bahrain";
            }
            else if (strCaseArg == "BANGLADESH")
            {
               PaiEnIng = "Bangladesh";
            }
            else if (strCaseArg == "BARBADOS")
            {
               PaiEnIng = "Barbados";
            }
            else if (strCaseArg == "BELARUS")
            {
               PaiEnIng = "Belarus";
            }
            else if (strCaseArg == "BELAU")
            {
               PaiEnIng = "Belau";
            }
            else if (strCaseArg == "BELGICA")
            {
               PaiEnIng = "Belgium";
            }
            else if (strCaseArg == "BELICE")
            {
               PaiEnIng = "Belize";
            }
            else if (strCaseArg == "BENIN")
            {
               PaiEnIng = "Benin";
            }
            else if (strCaseArg == "BOLIVIA")
            {
               PaiEnIng = "Bolivia";
            }
            else if (strCaseArg == "BOPHUTHATSWANA")
            {
               PaiEnIng = "Bophuthatswana";
            }
            else if (strCaseArg == "BOSNIA Y HERZEGOVINA")
            {
               PaiEnIng = "Bosnia - Herzegovina";
            }
            else if (strCaseArg == "BOTSWANA")
            {
               PaiEnIng = "Botswana";
            }
            else if (strCaseArg == "BRASIL")
            {
               PaiEnIng = "Brazil";
            }
            else if (strCaseArg == "BRUNEI")
            {
               PaiEnIng = "Brunei";
            }
            else if (strCaseArg == "BULGARIA")
            {
               PaiEnIng = "Bulgaria";
            }
            else if (strCaseArg == "BURKINA FASO")
            {
               PaiEnIng = "Burkina Faso";
            }
            else if (strCaseArg == "BURUNDI")
            {
               PaiEnIng = "Burundi";
            }
            else if (strCaseArg == "BUTAN")
            {
               PaiEnIng = "Bhutan";
            }
            else if (strCaseArg == "CABO VERDE")
            {
               PaiEnIng = "Cape Verde";
            }
            else if (strCaseArg == "CAMBODIA")
            {
               PaiEnIng = "Cambodia";
            }
            else if (strCaseArg == "CAMERUN")
            {
               PaiEnIng = "Cameroon";
            }
            else if (strCaseArg == "CANADA")
            {
               PaiEnIng = "Canada";
            }
            else if (strCaseArg == "CHAD")
            {
               PaiEnIng = "Chad";
            }
            else if (strCaseArg == "CHILE")
            {
               PaiEnIng = "Chile";
            }
            else if (strCaseArg == "CHINA")
            {
               PaiEnIng = "China";
            }
            else if (strCaseArg == "CHIPRE")
            {
               PaiEnIng = "Cyprus";
            }
            else if (strCaseArg == "CISKEI")
            {
               PaiEnIng = "Ciskei";
            }
            else if (strCaseArg == "COLOMBIA")
            {
               PaiEnIng = "Colombia";
            }
            else if (strCaseArg == "COMORAS")
            {
               PaiEnIng = "Comoros";
            }
            else if (strCaseArg == "CONGO")
            {
               PaiEnIng = "Congo";
            }
            else if (strCaseArg == "COREA DEL NORTE")
            {
               PaiEnIng = "North Korea";
            }
            else if (strCaseArg == "COREA DEL SUR")
            {
               PaiEnIng = "South Korea";
            }
            else if (strCaseArg == "COSTA DE MARFIL")
            {
               PaiEnIng = "Cote D’Ivoire";
            }
            else if (strCaseArg == "COSTA RICA")
            {
               PaiEnIng = "Costa Rica";
            }
            else if (strCaseArg == "CROACIA")
            {
               PaiEnIng = "Croatia";
            }
            else if (strCaseArg == "CUBA")
            {
               PaiEnIng = "Cuba";
            }
            else if (strCaseArg == "DEPOSITO FRANCO")
            {
               PaiEnIng = "Deposito Franco";
            }
            else if (strCaseArg == "DINAMARCA")
            {
               PaiEnIng = "Denmark";
            }
            else if (strCaseArg == "DJIBOUTI")
            {
               PaiEnIng = "Djibouti";
            }
            else if (strCaseArg == "DOMINICA")
            {
               PaiEnIng = "Dominica";
            }
            else if (strCaseArg == "ECUADOR")
            {
               PaiEnIng = "Ecuador";
            }
            else if (strCaseArg == "EGIPTO")
            {
               PaiEnIng = "Egypt";
            }
            else if (strCaseArg == "EL SALVADOR")
            {
               PaiEnIng = "El Salvador";
            }
            else if (strCaseArg == "EMIRATOS ARABES UNIDOS")
            {
               PaiEnIng = "United Arab Emirates";
            }
            else if (strCaseArg == "ERITREA")
            {
               PaiEnIng = "Eritrea";
            }
            else if (strCaseArg == "ESLOVENIA")
            {
               PaiEnIng = "Slovenia";
            }
            else if (strCaseArg == "ESPANA")
            {
               PaiEnIng = "Spain";
            }
            else if (strCaseArg == "ESTADOS UNIDOS")
            {
               PaiEnIng = "U.S.A.";
            }
            else if (strCaseArg == "ESTONIA")
            {
               PaiEnIng = "Estonia";
            }
            else if (strCaseArg == "ETIOPIA")
            {
               PaiEnIng = "Ethiopia";
            }
            else if (strCaseArg == "FIJI")
            {
               PaiEnIng = "Fiji";
            }
            else if (strCaseArg == "FILIPINAS")
            {
               PaiEnIng = "Philippines";
            }
            else if (strCaseArg == "FINLANDIA")
            {
               PaiEnIng = "Finland";
            }
            else if (strCaseArg == "FRANCIA")
            {
               PaiEnIng = "France";
            }
            else if (strCaseArg == "GABON")
            {
               PaiEnIng = "Gabon";
            }
            else if (strCaseArg == "GAMBIA")
            {
               PaiEnIng = "Gambia";
            }
            else if (strCaseArg == "GEORGIA")
            {
               PaiEnIng = "Georgia";
            }
            else if (strCaseArg == "GHANA")
            {
               PaiEnIng = "Ghana";
            }
            else if (strCaseArg == "GRANADA")
            {
               PaiEnIng = "Grenada";
            }
            else if (strCaseArg == "GRECIA")
            {
               PaiEnIng = "Greece";
            }
            else if (strCaseArg == "GUATEMALA")
            {
               PaiEnIng = "Guatemala";
            }
            else if (strCaseArg == "GUINEA")
            {
               PaiEnIng = "Guinea";
            }
            else if (strCaseArg == "GUINEA BISSAU")
            {
               PaiEnIng = "Guinea-Bissau";
            }
            else if (strCaseArg == "GUINEA ECUATORIAL")
            {
               PaiEnIng = "Equatorial Guinea";
            }
            else if (strCaseArg == "GUYANA")
            {
               PaiEnIng = "Guyana";
            }
            else if (strCaseArg == "HAITI")
            {
               PaiEnIng = "Haiti";
            }
            else if (strCaseArg == "HAWAII")
            {
               PaiEnIng = "Hawaii";
            }
            else if (strCaseArg == "HOLANDA")
            {
               PaiEnIng = "Netherlands";
            }
            else if (strCaseArg == "HONDURAS")
            {
               PaiEnIng = "Honduras";
            }
            else if (strCaseArg == "HONG KONG")
            {
               PaiEnIng = "Hong Kong";
            }
            else if (strCaseArg == "HUNGRIA")
            {
               PaiEnIng = "Hungary";
            }
            else if (strCaseArg == "INDIA")
            {
               PaiEnIng = "India";
            }
            else if (strCaseArg == "INDONESIA")
            {
               PaiEnIng = "Indonesia";
            }
            else if (strCaseArg == "INGLATERRA")
            {
               PaiEnIng = "England";
            }
            else if (strCaseArg == "IRAK")
            {
               PaiEnIng = "Iraq";
            }
            else if (strCaseArg == ")IRAN")
            {
               PaiEnIng = "Iran";
            }
            else if (strCaseArg == "IRLANDA")
            {
               PaiEnIng = "Ireland";
            }
            else if (strCaseArg == "ISLANDIA")
            {
               PaiEnIng = "Iceland";
            }
            else if (strCaseArg == "ISLAS MALDIVAS")
            {
               PaiEnIng = "Maldives Islands";
            }
            else if (strCaseArg == "ISLAS MARSHALL")
            {
               PaiEnIng = "Marshall Islands";
            }
            else if (strCaseArg == "ISLAS SALOMON")
            {
               PaiEnIng = "Solomon Islands";
            }
            else if (strCaseArg == "ISLAS TONGA")
            {
               PaiEnIng = "Tonga Islands";
            }
            else if (strCaseArg == "ISRAEL")
            {
               PaiEnIng = "Israel";
            }
            else if (strCaseArg == "ITALIA")
            {
               PaiEnIng = "Italy";
            }
            else if (strCaseArg == "JAMAICA")
            {
               PaiEnIng = "Jamaica";
            }
            else if (strCaseArg == "JAPON")
            {
               PaiEnIng = "Japan";
            }
            else if (strCaseArg == "JORDANIA")
            {
               PaiEnIng = "Jordan";
            }
            else if (strCaseArg == "KASAJSTAN")
            {
               PaiEnIng = "Kazakhstan";
            }
            else if (strCaseArg == "KENIA")
            {
               PaiEnIng = "Kenya";
            }
            else if (strCaseArg == "KIRGISTAN")
            {
               PaiEnIng = "Kyrgyzstan";
            }
            else if (strCaseArg == "KIRIBATI")
            {
               PaiEnIng = "Kiribati";
            }
            else if (strCaseArg == "KUWAIT")
            {
               PaiEnIng = "Kuwait";
            }
            else if (strCaseArg == "LAOS")
            {
               PaiEnIng = "Laos";
            }
            else if (strCaseArg == "LESOTHO")
            {
               PaiEnIng = "Lesotho";
            }
            else if (strCaseArg == "LETONIA")
            {
               PaiEnIng = "Latvia";
            }
            else if (strCaseArg == "LIBANO")
            {
               PaiEnIng = "Lebanon";
            }
            else if (strCaseArg == "LIBERIA")
            {
               PaiEnIng = "Liberia";
            }
            else if (strCaseArg == "LIBIA")
            {
               PaiEnIng = "Libya";
            }
            else if (strCaseArg == "LIECHTENSTEIN")
            {
               PaiEnIng = "Liechtenstein";
            }
            else if (strCaseArg == "LITUANIA")
            {
               PaiEnIng = "Lithuania";
            }
            else if (strCaseArg == "LUXEMBURGO")
            {
               PaiEnIng = "Luxembourg";
            }
            else if (strCaseArg == "MACEDONIA")
            {
               PaiEnIng = "Macedonia";
            }
            else if (strCaseArg == "MADAGASCAR")
            {
               PaiEnIng = "Madagascar";
            }
            else if (strCaseArg == "MALASIA")
            {
               PaiEnIng = "Malaysia";
            }
            else if (strCaseArg == "MALAWI")
            {
               PaiEnIng = "Malawi";
            }
            else if (strCaseArg == "MALI")
            {
               PaiEnIng = "Mali";
            }
            else if (strCaseArg == "MALTA")
            {
               PaiEnIng = "Malta";
            }
            else if (strCaseArg == "MARRUECOS")
            {
               PaiEnIng = "Morocco";
            }
            else if (strCaseArg == "MAURICIO")
            {
               PaiEnIng = "Mauritiu";
            }
            else if (strCaseArg == "MAURITANIA")
            {
               PaiEnIng = "Mauritania";
            }
            else if (strCaseArg == "MEXICO")
            {
               PaiEnIng = "Mexico";
            }
            else if (strCaseArg == "MICRONESIA")
            {
               PaiEnIng = "Micronesia";
            }
            else if (strCaseArg == "MOLDOVA")
            {
               PaiEnIng = "Moldova";
            }
            else if (strCaseArg == "MONACO")
            {
               PaiEnIng = "Monaco";
            }
            else if (strCaseArg == "MONGOLIA")
            {
               PaiEnIng = "Mongolia";
            }
            else if (strCaseArg == "MOZAMBIQUE")
            {
               PaiEnIng = "Mozambique";
            }
            else if (strCaseArg == "MYANMAR")
            {
               PaiEnIng = "(Ex Birmania) Myanmar";
            }
            else if (strCaseArg == "NACIONAL REPUTADA")
            {
               PaiEnIng = "Nacional Reputada";
            }
            else if (strCaseArg == "NAMIBIA")
            {
               PaiEnIng = "Namibia";
            }
            else if (strCaseArg == "NAURU")
            {
               PaiEnIng = "Nauru";
            }
            else if (strCaseArg == "NEPAL")
            {
               PaiEnIng = "Nepal";
            }
            else if (strCaseArg == "NICARAGUA")
            {
               PaiEnIng = "Nicaragua";
            }
            else if (strCaseArg == "NIGER")
            {
               PaiEnIng = "Niger";
            }
            else if (strCaseArg == "NIGERIA")
            {
               PaiEnIng = "Nigeria";
            }
            else if (strCaseArg == "NORUEGA")
            {
               PaiEnIng = "Norway";
            }
            else if (strCaseArg == "NUEVA ZELANDIA")
            {
               PaiEnIng = "New Zealand";
            }
            else if (strCaseArg == "OMAN")
            {
               PaiEnIng = "Oman";
            }
            else if (strCaseArg == "PAKISTAN")
            {
               PaiEnIng = "Pakistan";
            }
            else if (strCaseArg == "PANAMA")
            {
               PaiEnIng = "Panama";
            }
            else if (strCaseArg == "PAPUA, NUEVA GUINEA")
            {
               PaiEnIng = "Papua New Guinea";
            }
            else if (strCaseArg == "PARAGUAY")
            {
               PaiEnIng = "Paraguay";
            }
            else if (strCaseArg == "PERU")
            {
               PaiEnIng = "Peru";
            }
            else if (strCaseArg == "PESCA EXTRATERRITORIAL")
            {
               PaiEnIng = "Pesca Extraterritorial";
            }
            else if (strCaseArg == "POLONIA")
            {
               PaiEnIng = "Poland";
            }
            else if (strCaseArg == "PORTUGAL")
            {
               PaiEnIng = "Portugal";
            }
            else if (strCaseArg == "QATAR")
            {
               PaiEnIng = "Qatar";
            }
            else if (strCaseArg == "RANCHO NAVES Y AERONAVES")
            {
               PaiEnIng = "Rancho Naves Y Aeronaves";
            }
            else if (strCaseArg == "REP. FEDERAL DE YUGOSLAVIA")
            {
               PaiEnIng = "Federal Republic of Yugoslavia";
            }
            else if (strCaseArg == "REPUBLICA CENTRO AFRICANA")
            {
               PaiEnIng = "Central African Republic";
            }
            else if (strCaseArg == "REPUBLICA CHECA")
            {
               PaiEnIng = "Czech Republic";
            }
            else if (strCaseArg == "REPUBLICA DOMINICANA")
            {
               PaiEnIng = "Dominican Republic";
            }
            else if (strCaseArg == "REPUBLICA ESLOVACA")
            {
               PaiEnIng = "Slovakia Republic";
            }
            else if (strCaseArg == "RUMANIA")
            {
               PaiEnIng = "Romania";
            }
            else if (strCaseArg == "RUSIA")
            {
               PaiEnIng = "Russian Federation";
            }
            else if (strCaseArg == "RWANDA")
            {
               PaiEnIng = "Rwanda";
            }
            else if (strCaseArg == "SAHARAVI")
            {
               PaiEnIng = "Saharavi";
            }
            else if (strCaseArg == "SAINT KITTS & NEVIS")
            {
               PaiEnIng = "Saint Kitts and Nevis";
            }
            else if (strCaseArg == "SAMOA OCCIDENTAL")
            {
               PaiEnIng = "Samoa";
            }
            else if (strCaseArg == "SAN MARINO")
            {
               PaiEnIng = "San Marino";
            }
            else if (strCaseArg == "SAN VICENTE Y LAS GRANADINAS")
            {
               PaiEnIng = "Saint Vincent and the Grenadines";
            }
            else if (strCaseArg == "SANTA LUCIA")
            {
               PaiEnIng = "Saint Lucia";
            }
            else if (strCaseArg == "SANTA SEDE")
            {
               PaiEnIng = "Vatican";
            }
            else if (strCaseArg == "SAO TOME Y PRINCIPE")
            {
               PaiEnIng = "Sao Tome and Principe";
            }
            else if (strCaseArg == "SENEGAL")
            {
               PaiEnIng = "Senegal";
            }
            else if (strCaseArg == "SEYCHELLES")
            {
               PaiEnIng = "Seychelles";
            }
            else if (strCaseArg == "SIERRA LEONA")
            {
               PaiEnIng = "Sierra Leone";
            }
            else if (strCaseArg == "SINGAPUR")
            {
               PaiEnIng = "Singapore";
            }
            else if (strCaseArg == "SIRIA")
            {
               PaiEnIng = "Syrian Arab Republic";
            }
            else if (strCaseArg == "SOMALIA")
            {
               PaiEnIng = "Somalia";
            }
            else if (strCaseArg == "SRI LANKA(CEILAN)")
            {
               PaiEnIng = "Sri Lanka";
            }
            else if (strCaseArg == "SUDAFRICA")
            {
               PaiEnIng = "South Africa";
            }
            else if (strCaseArg == "SUDAN")
            {
               PaiEnIng = "Sudan";
            }
            else if (strCaseArg == "SUECIA")
            {
               PaiEnIng = "Sweden";
            }
            else if (strCaseArg == "SUIZA")
            {
               PaiEnIng = "Switzerland";
            }
            else if (strCaseArg == "SURINAME")
            {
               PaiEnIng = "Suriname";
            }
            else if (strCaseArg == "SWAZILANDIA")
            {
               PaiEnIng = "Swaziland";
            }
            else if (strCaseArg == "TADJIKISTAN")
            {
               PaiEnIng = "Tadjikistan";
            }
            else if (strCaseArg == "TAHITI")
            {
               PaiEnIng = "Tahiti";
            }
            else if (strCaseArg == "TAIWAN")
            {
               PaiEnIng = "Taiwan";
            }
            else if (strCaseArg == "TANZANIA")
            {
               PaiEnIng = "Tanzania";
            }
            else if (strCaseArg == "TERRIT. BRITANICO EN AMERICA")
            {
               PaiEnIng = "Territ. Britanico en America";
            }
            else if (strCaseArg == "TERRIT. HOLANDES EN AMERICA")
            {
               PaiEnIng = "Territ. Holandes en America";
            }
            else if (strCaseArg == "TERRITORIO BRITANICO AUSTRALIA")
            {
               PaiEnIng = "Territorio Britanico Australia";
            }
            else if (strCaseArg == "TERRITORIO BRITANICO EN AFRICA")
            {
               PaiEnIng = "Territorio Britanico en Africa";
            }
            else if (strCaseArg == "TERRITORIO BRITANICO EN ASIA")
            {
               PaiEnIng = "Territorio Britanico en Africa";
            }
            else if (strCaseArg == "TERRITORIO DE DINAMARCA")
            {
               PaiEnIng = "Territorio de Dinamarca";
            }
            else if (strCaseArg == "TERRITORIO EEUU AUSTRALIA")
            {
               PaiEnIng = "Territorio EEUU Australia";
            }
            else if (strCaseArg == "TERRITORIO ESPANOL EN AFRICA")
            {
               PaiEnIng = "Territorio Espanol en Africa";
            }
            else if (strCaseArg == "TERRITORIO FRANCES AUSTRALIA")
            {
               PaiEnIng = "Territorio Frances Australia";
            }
            else if (strCaseArg == "TERRITORIO FRANCES EN AFRICA")
            {
               PaiEnIng = "Territorio Frances en Africa";
            }
            else if (strCaseArg == "TERRITORIO FRANCES EN AMERICA")
            {
               PaiEnIng = "Territorio Frances en America";
            }
            else if (strCaseArg == "TERRITORIO PORTUGUES EN ASIA")
            {
               PaiEnIng = "Territorio Portugues en Asia";
            }
            else if (strCaseArg == "THAILANDIA")
            {
               PaiEnIng = "Thailand";
            }
            else if (strCaseArg == "TOGO")
            {
               PaiEnIng = "Togo";
            }
            else if (strCaseArg == "TRANSKEI")
            {
               PaiEnIng = "Transkei";
            }
            else if (strCaseArg == "TRINIDAD Y TOBAGO")
            {
               PaiEnIng = "Trinidad and Tobago";
            }
            else if (strCaseArg == "TUNEZ")
            {
               PaiEnIng = "Tunisia";
            }
            else if (strCaseArg == "TURKMENISTAN")
            {
               PaiEnIng = "Turkmenistan";
            }
            else if (strCaseArg == "TURQUIA")
            {
               PaiEnIng = "Turkey";
            }
            else if (strCaseArg == "TUVALU")
            {
               PaiEnIng = "Tuvalu";
            }
            else if (strCaseArg == "UCRANIA")
            {
               PaiEnIng = "Ukraine";
            }
            else if (strCaseArg == "UGANDA")
            {
               PaiEnIng = "Uganda";
            }
            else if (strCaseArg == "URUGUAY")
            {
               PaiEnIng = "Uruguay";
            }
            else if (strCaseArg == "UZBEKISTAN")
            {
               PaiEnIng = "Uzbekistan";
            }
            else if (strCaseArg == "VANUATU")
            {
               PaiEnIng = "Vanuatu";
            }
            else if (strCaseArg == "VENDA")
            {
               PaiEnIng = "Venda";
            }
            else if (strCaseArg == "VENEZUELA")
            {
               PaiEnIng = "Venezuela";
            }
            else if (strCaseArg == "VIETNAM")
            {
               PaiEnIng = "Vietnam";
            }
            else if (strCaseArg == "YEMEN DEL NORTE")
            {
               PaiEnIng = "North Yemen";
            }
            else if (strCaseArg == "YEMEN DEL SUR")
            {
               PaiEnIng = "South Yemen";
            }
            else if (strCaseArg == "ZAIRE")
            {
               PaiEnIng = "Zaire";
            }
            else if (strCaseArg == "ZAMBIA")
            {
               PaiEnIng = "Zambia";
            }
            else if (strCaseArg == "ZIMBABWE")
            {
               PaiEnIng = "Zimbabwe";
            }
            else if (strCaseArg == "ZONA FRANCA ARICA")
            {
               PaiEnIng = "Zona Franca Arica";
            }
            else if (strCaseArg == "ZONA FRANCA IQUIQUE")
            {
               PaiEnIng = "Zona Franca Iquique";
            }
            else if (strCaseArg == "ZONA FRANCA PUNTA ARENAS")
            {
               PaiEnIng = "Zona Franca Punta Arenas";
            }
            else
            {
               PaiEnIng = PaisEsp;
            }

            return PaiEnIng;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            MigrationSupport.Utils.MsgBox("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescription(MigrationSupport.GlobalException.Instance.Number),MODGDOC.Pito(48).Cast<
               MigrationSupport.MsgBoxStyle>(),"Error en Traducción de País a Inglés");

         }
         return PaiEnIng;
      }
      // ****************************************************************************
      //    1.  Detalle de las Arbitrajes.
      // ****************************************************************************
      public static void Pr_Arbitrajes()
      {
         string Par = "";
         int i = 0;
         int n = 0;

         n = VArb.GetUpperBound(0);

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(8),"Compra de Divisas:");
         XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(50),"Venta de Divisas:");
         XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(95),"Paridades:"});
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         for(i = 1; i <= n; i += 1)
         {
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(7),VArb[i].NemMndC);
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(15),MODGDOC.forma(VArb[i].MtoCom,MODXDOC.Formato));
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(48),VArb[i].NemMndV);
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(60),MODGDOC.forma(VArb[i].MtoVta,MODXDOC.Formato));
            Par = MODGDOC.forma(VArb[i].PrdArb,MODXDOC.FormatoParidad);
            XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(98),Par.Left((Par.Len() - 4))});
            Pr_Salto_Pagina();
         }

      }
      // ****************************************************************************
      //    1.  Detalle de las Compras.
      // ****************************************************************************
      public static void Pr_Compras()
      {
         int i = 0;
         int n = 0;

         n = Compras.GetUpperBound(0);
         

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(40),"Compras de Divisas:"});
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         for(i = 1; i <= n; i += 1)
         {
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(7),Compras[i].NemMnd);
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(20),MODGDOC.forma(Compras[i].MtoCVD,MODXDOC.Formato));
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(45),"Tipo de Cambio $");
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(65),MODGDOC.forma(Compras[i].TipCam,MODXDOC.FormatoTipCamb));
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(88),"Total $");
            XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(98),MODGDOC.forma(Compras[i].MtpPres,MODXDOC.Formato)});
            Pr_Salto_Pagina();
         }

      }
      // ****************************************************************************
      // Autor          : Orieta Gamonal Gutiérrez
      // Fecha          : Junio 1995
      // Propósito      : Imprimir el Detalle de aceptación.
      // Observaciones  : Coloca un detalle de aceptación para el caso de la
      //                  carta Nº 4 (Exportador....Girado)
      // ****************************************************************************
      public static void Pr_Detalle()
      {
         int i = 0;
         int n = 0;

         n = VxLet.GetUpperBound(0);
         

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_titulos)),"Detalle de aceptación"});
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         // si existen letras, se imprimen estas
         if (n > 0)
         {
            for(i = 0; i <= n; i += 1)
            {
               if (VxLet[i].CodMon != 0)
               {
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(new string(' ',tabulador - 15),"Letra Nro. ",XGCV.Printer.TAB((short)(tab_doc_letra - 5)),VxLet[i].CodMon.Str().TrimB());
                  XGCV.Printer.DefInstance.PrintList(new object[]{" ",XGCV.Printer.TAB((short)(tab_doc_cod - 6)),VxLet[i].CodMon_t.TrimB() + " ",XGCV.Printer.TAB((short)(tab_doc_nem - 5)),MODGDOC.forma(VxLet[i].MtoLet,
                     MODXDOC.Formato),XGCV.Printer.TAB((short)(tab_doc_monto - 6))," Vence; " + MigrationSupport.Utils.Format(VxLet[i].FecVen,"dd/mm/yyyy")});
                  Pr_Salto_Pagina();
               }
            }
         }

      }
      // ****************************************************************************
      //    1.  Despliega n distribución que se efectuará por un determinado pago.
      // ****************************************************************************
      public static void Pr_Distribucion()
      {
         int i = 0;
         int n = 0;

         n = VDist.GetUpperBound(0);
         

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.PrintList(new object[]{"Beneficiario",XGCV.Printer.TAB(30),"Vía de la Distribución",XGCV.Printer.TAB(60),"Concepto",XGCV.Printer.TAB(104),"Monto"});
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         for(i = 1; i <= n; i += 1)
         {
            XGCV.Printer.DefInstance.PrintList(new object[]{MODGDOC.Minuscula(VDist[i].DisBen).TrimB().Left(22),XGCV.Printer.TAB(31),MODGDOC.Minuscula(VDist[i].DisVia).TrimB(),XGCV.Printer.TAB(64),
               MODGDOC.Minuscula(VDist[i].DisCon).TrimB(),XGCV.Printer.TAB(89),VDist[i].DisMon.TrimB(),XGCV.Printer.TAB(95),MODGDOC.forma(VDist[i].DisMto,MODXDOC.Formato)});
            Pr_Salto_Pagina();
         }
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

      }
      // ****************************************************************************
      // Autor          : Orieta Gamonal Gutiérrez
      // Fecha          : Junio 1995
      // Propósito      : Imprimir los Documentos y letras.
      // Observaciones  : Coloca los documentos y las letras correspondientes
      //                  para el caso de las cartas Nºs 1,2,3,4 (Cobrador..Girador,
      //                  Agente....Exportador, Exportador....Girado)
      // ****************************************************************************
      public static void Pr_Documentos(int Carta_Aux)
      {
         string Paso = "";
         int i = 0;
         int m = 0;
         int n = 0;

            n = VxLet.GetUpperBound(0);
            m = VxDem.GetUpperBound(0);

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
         switch(Carta_Aux)
         {
         case 1:
         case 3:
         case 20:
            if (Idioma == "I")
            {
               XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_titulos)),"Documents"});
            }
            else
            {
               XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_titulos)),"Documentos"});
            }
            break;
         default:
            XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_titulos)),"Documentos"});
            break;
         }
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         // si existen letras, se imprimen estas
         if (n > 0)
         {
            switch(Carta_Aux)
            {
            case 1:
            case 3:
            case 20:
               if (Idioma == "I")
               {
                  for(i = 1; i <= n; i += 1)
                  {
                     if (VxLet[i].CodMon != 0)
                     {
                        XGCV.Printer.DefInstance.PrintWithoutCrlf(new string(' ',tabulador - 23),"Draft Nr. ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),MigrationSupport.Utils.Format(VxLet[i].CodMon.Str().TrimB(),"00"));
                        XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_mto_descr)),VxLet[i].CodMon_t.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(VxLet[i].MtoLet,MODXDOC.Formato),
                           XGCV.Printer.TAB(89)," Maturity " + MigrationSupport.Utils.Format(VxLet[i].FecVen,"mm/dd/yyyy")});
                        Pr_Salto_Pagina();
                     }
                  }
               }
               else
               {
                  for(i = 1; i <= n; i += 1)
                  {
                     if (VxLet[i].CodMon != 0)
                     {
                        XGCV.Printer.DefInstance.PrintWithoutCrlf(new string(' ',tabulador - 23),"Letra Nro. ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),MigrationSupport.Utils.Format(VxLet[i].CodMon.Str().TrimB(),"00"));
                        XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_mto_descr)),VxLet[i].CodMon_t.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(VxLet[i].MtoLet,MODXDOC.Formato),
                           XGCV.Printer.TAB(89)," Vence; " + MigrationSupport.Utils.Format(VxLet[i].FecVen,"dd/mm/yyyy")});
                        Pr_Salto_Pagina();
                     }
                  }
               }
               break;
            default:
               for(i = 1; i <= n; i += 1)
               {
                  if (VxLet[i].CodMon != 0)
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf(new string(' ',tabulador - 23),"Letra Nro. ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),MigrationSupport.Utils.Format(VxLet[i].CodMon.Str().TrimB(),"00"));
                     XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_mto_descr)),VxLet[i].CodMon_t.TrimB() + " ",XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(VxLet[i].MtoLet,MODXDOC.Formato),
                        XGCV.Printer.TAB(89)," Vence; " + MigrationSupport.Utils.Format(VxLet[i].FecVen,"dd/mm/yyyy")});
                     Pr_Salto_Pagina();
                  }
               }
               break;
            }
         }

         // si existen documentos se imprimen dichos documentos
         if (m > 0)
         {
            for(i = 0; i <= m; i += 1)
            {
               if (VxDem[i].NomDem.TrimB() != "")
               {
                  switch(Carta_Aux)
                  {
                  case 1:
                  case 3:
                     if (Idioma == "I")
                     {
                        Paso = MODGDOC.CopiarDeString(VxDem[i].NomDem,";",2).TrimB();
                        Paso = Paso.Mid(1, 30);
                        XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),Paso,XGCV.Printer.TAB((short)(tab_mto_descr - 4 + 20)),VxDem[i].NumDem_t.TrimB()});
                        Pr_Salto_Pagina();
                     }
                     else
                     {
                        Paso = MODGDOC.CopiarDeString(VxDem[i].NomDem,";",1).TrimB();
                        Paso = Paso.Mid(1, 30);
                        XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),Paso,XGCV.Printer.TAB((short)(tab_mto_descr - 4 + 20)),VxDem[i].NumDem_t.TrimB()});
                        Pr_Salto_Pagina();
                     }
                     break;
                  default:
                     Paso = MODGDOC.CopiarDeString(VxDem[i].NomDem,";",1).TrimB();
                     Paso = Paso.Mid(1, 30);
                     XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),Paso,XGCV.Printer.TAB((short)(tab_mto_descr - 4 + 20)),VxDem[i].NumDem_t.TrimB()});
                     Pr_Salto_Pagina();
                     break;
                  }
               }
            }
         }

      }
      // ****************************************************************************
      //    1.  Imprime el Píe de Página de la carta Nº 620 en donde se coloca el
      //        nombre del Usuario Especialista.
      // ****************************************************************************'
      public static void Pr_Especialista()
      {
         int i = 0;
         int a = 0;

         a = Fn_Buscar_Indice(UsrEsp.cencos,UsrEsp.CodUsr);
         if (a != 0)
         {
            UsrEsp.nombre = UsrEsps[a].nombre;
            UsrEsp.Direccion = UsrEsps[a].nombre;
            UsrEsp.Ciudad = UsrEsps[a].nombre;
            UsrEsp.telefono = UsrEsps[a].nombre;
            UsrEsp.Fax = UsrEsps[a].nombre;
         }
         else
         {
            return;
         }

         if (linea < 50)
         {
            for(i = linea; i <= 50; i += 1)
            {
               XGCV.Printer.DefInstance.Print( );
               Pr_Salto_Pagina();
            }
         }
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(80),UsrEsp.nombre.TrimB()});
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);

      }
      // ****************************************************************************
      // Autor          : Orieta Gamonal Gutiérrez
      // Fecha          : Junio 1995
      // Propósito      : Imprimir los Títulos de los siguientes párrafos.
      // Observaciones  : Coloca los títulos dependiendo de la carta a imprimir
      // ****************************************************************************
      public static void Pr_Instrucciones(int Carta_Aux)
      {

         // Orieta.
         // Printer.Print : Call Pr_Salto_Pagina
         // Printer.Print : Call Pr_Salto_Pagina
         // Printer.FontBold = True
         // Printer.FontUnderline = True
         // Orieta.

         switch(Carta_Aux)
         {
         case 0:
         case 1:
            if (Carta_Aux == 0)
            {
               if (VInsEsp.InsCob.TrimB() != "")
               {
                  // Orieta.
                  XGCV.Printer.DefInstance.Print( );
                  Pr_Salto_Pagina();
                  XGCV.Printer.DefInstance.Print( );
                  Pr_Salto_Pagina();
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
                  // Orieta.
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(45));
                  XGCV.Printer.DefInstance.Print("Instrucciones al Banco Cobrador");
               }
            }
            break;
         case 2:
         case 4:
            if (Carta_Aux == 2)
            {
               if (VInsEsp.InsExp.TrimB() != "")
               {
                  // Printer.Print Tab(45);
                  // Printer.Print "Información al Exportador"
               }
               else
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
                  return;
               }
            }
            else if (Carta_Aux == 4)
            {
               if (VInsEsp.InsExp.TrimB() != "")
               {
                  // Printer.Print Tab(45);
                  // Printer.Print "Información al Exportador"
               }
               else
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
                  return;
               }
            }
            break;
         case 3:
            if (VInsEsp.InsAge.TrimB() != "")
            {
               // Orieta.
               XGCV.Printer.DefInstance.Print( );
               Pr_Salto_Pagina();
               XGCV.Printer.DefInstance.Print( );
               Pr_Salto_Pagina();
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
               // Orieta.
               // Printer.Print Tab(45);
               // Printer.Print "Información al Agente"
            }
            else
            {
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
               return;
            }
            break;
         }

         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
         Pr_Salto_Pagina();

      }
      // ****************************************************************************
      // Autor          : Orieta Gamonal Gutiérrez
      // Fecha          : Junio 1995
      // Propósito      : Carga inicialmente los datos de prueba.
      // Observaciones  : Se ingresan datos de los Partys y Letras
      // ****************************************************************************
      public static void Pr_Load_Datos_Letra()
      {
         int i = 0;
         double n = 0.0;

         PartysOpe = new PartyKey[MODXDOC.IAge];
         PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumIni()).TrimB();
         PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         PartysOpe[MODXDOC.IGir].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].ComunaUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Letras de Exportación
         n = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         VxLet = new T_xLet[(n + 1).ToInt()];
         if (n > 0)
         {
            for(i = 1; i <= VxLet.GetUpperBound(0); i += 1)
            {
               VxLet[i].CodMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
               VxLet[i].MtoLet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
               VxLet[i].FecVen = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxLet[i].CodMon_t = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         // Referencias
         VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VxCob.RefBcoC = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Instrucciones Especiales de Frases Estándares
         VInsEsp.InsExp = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Atributos del Usuario
         UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

      }
      // ****************************************************************************
      // Autor          : Orieta Gamonal Gutiérrez
      // Fecha          : Junio 1995
      // Propósito      : Carga inicialmente los datos de prueba.
      // Observaciones  : Se ingresan datos de los Partys, Documentos, Letras,
      //                  Montos, Vencimientos, etc.
      // ****************************************************************************
      public static void Pr_Load_Datos_Memo()
      {
         int m = 0;
         int CuantosCom = 0;
         int CuantosDeb = 0;
         int i = 0;
         double n = 0.0;
         int e = 0;

         PartysOpe = new PartyKey[MODXDOC.ICob + 1];
         Idioma = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumIni()).TrimB();
         PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         PartysOpe[MODXDOC.IGir].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].ComunaUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].telefono = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].Telex = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         PartysOpe[MODXDOC.ICob].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Partys Opcional
         e = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         if (e != 0)
         {
            Array.Resize(ref PartysOpe, MODXDOC.IAge + 1);
            PartysOpe[MODXDOC.IAge].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         }

         // Condición de Pago.
         VxCob.Condicion = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Mercadería.
         VxCob.Mercad_t = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VxCob.Mercad_t = MODGDOC.Componer(VxCob.Mercad_t,"ç",9.Char());

         // Documentos de Embarque
         n = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         VxDem = new T_xDem[(n + 1).ToInt()];
         if (n > 0)
         {
            for(i = 1; i <= VxDem.GetUpperBound(0); i += 1)
            {
               VxDem[i].NomDem = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxDem[i].NumDem_t = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         // Conocimientos de Embarque
         n = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         VxCem = new T_xCem[(n + 1).ToInt()];
         if (n > 0)
         {
            for(i = 1; i <= VxCem.GetUpperBound(0); i += 1)
            {
               VxCem[i].NumCem = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxCem[i].FecCem = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxCem[i].EmbDes = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxCem[i].EmbHac = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         // Letras de Exportación
         n = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         VxLet = new T_xLet[(n + 1).ToInt()];
         if (n > 0)
         {
            for(i = 1; i <= VxLet.GetUpperBound(0); i += 1)
            {
               VxLet[i].CodMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
               VxLet[i].MtoLet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
               VxLet[i].FecVen = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxLet[i].CodMon_t = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         // Referencias
         VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VxCob.RefBcoC = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Cobranza de Exportaciones (Moneda y Montos)
         VxCob.Nemonic = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VxCob.CodMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VxCob.MtoCob = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         VxCob.MtoInt = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         VxCob.Cedente1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         VxCob.Cedente2 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();

         // Vencimientos
         n = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         Vencimientos = new T_Ven[(n + 1).ToInt()];
         if (n > 0)
         {
            for(i = 1; i <= Vencimientos.GetUpperBound(0); i += 1)
            {
               Vencimientos[i].CodMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               Vencimientos[i].TotVen = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
               Vencimientos[i].FecVen = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         // Abonos (Débitos)
         CuantosDeb = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VDet = new T_Det[CuantosDeb + 1];
         if (CuantosDeb > 0)
         {
            for(i = 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "D";
            }
         }

         // Cargos (Comisiones)
         CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         m = VDet.GetUpperBound(0);
         
         if (CuantosCom > 0)
         {
            Array.Resize(ref VDet, m + CuantosCom + 1);
            for(i = m + 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "C";
            }
            MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         }

         // Atributos del Usuario
         UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Instrucciones Especiales de Frases Estándares
         VInsEsp.Frases = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInsEsp.InsCob = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInsEsp.InsExp = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInsEsp.InsAge = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInsEsp.InsCob = MODGDOC.Componer(VInsEsp.InsCob,"ç",9.Char());
         VInsEsp.InsExp = MODGDOC.Componer(VInsEsp.InsExp,"ç",9.Char());
         VInsEsp.InsAge = MODGDOC.Componer(VInsEsp.InsAge,"ç",9.Char());

      }
      // ****************************************************************************
      // Autor          : Claudio Cárcamo M.-
      // Fecha          : Marzo 1996
      // Observaciones  : Se ingresan datos de Documentos y Letras.-
      // ****************************************************************************
      public static void Pr_Load_Datos_Reenvio()
      {
         int i = 0;
         double n = 0.0;
         int e = 0;

         PartysOpe = new PartyKey[MODXDOC.ICob + 1];
         Idioma = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumIni()).TrimB();
         PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         PartysOpe[MODXDOC.IGir].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].ComunaUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         PartysOpe[MODXDOC.ICob].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.ICob].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Partys Opcional
         e = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         if (e != 0)
         {
            Array.Resize(ref PartysOpe, MODXDOC.IAge + 1);
            PartysOpe[MODXDOC.IAge].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IAge].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         }

         // Condición de Pago.
         VxCob.Condicion = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Documentos de Embarque
         n = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         VxDem = new T_xDem[(n + 1).ToInt()];
         if (n > 0)
         {
            for(i = 1; i <= VxDem.GetUpperBound(0); i += 1)
            {
               VxDem[i].NomDem = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxDem[i].NumDem_t = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         // Letras de Exportación
         n = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         VxLet = new T_xLet[(n + 1).ToInt()];
         if (n > 0)
         {
            for(i = 1; i <= VxLet.GetUpperBound(0); i += 1)
            {
               VxLet[i].CodMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
               VxLet[i].MtoLet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
               VxLet[i].FecVen = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxLet[i].CodMon_t = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         // Referencias
         VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VxCob.RefBcoC = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Fecha de Ingreso.-
         VxCob.FecIng = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Atributos del Usuario
         UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Instrucciones Especiales de Frases Estándares
         VInsEsp.Frases = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInsEsp.InsCob = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

      }
      // ****************************************************************************
      //    1.  Se cargan los datos en variables y arreglos que luegos serán
      //        llevadas a papel para imprimir la carta Nro. 620.
      // ****************************************************************************'
      public static void Pr_Load_Doc620()
      {
         int CuantosPlanilla = 0;
         int CuantosCom = 0;
         int CuantasOri = 0;
         int CuantasVias = 0;
         int CuantasRemesa = 0;
         int i = 0;

         PartysOpe = new PartyKey[2];
         PartysOpe[1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumIni()).TrimB();
         PartysOpe[1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Operacion Relacionada.-
         VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Referencias.
         VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Referencia Cliente.-
         VOperac.RefCli = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Cuantas Compras.
         CuantasCompras = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         // Cuantas Ventas.
         CuantasVentas = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         Compras = new T_VgPli[CuantasCompras + 1];
         Ventas = new T_VgPli[CuantasVentas + 1];
         if (CuantasVentas > 0)
         {
            for(i = 1; i <= Ventas.GetUpperBound(0); i += 1)
            {
               Ventas[i].NemMnd = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               Ventas[i].MtoCVD = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
               Ventas[i].TipCam = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
               Ventas[i].MtpPres = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
            }
         }
         if (CuantasCompras > 0)
         {
            for(i = 1; i <= Compras.GetUpperBound(0); i += 1)
            {
               Compras[i].NemMnd = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               Compras[i].MtoCVD = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
               Compras[i].TipCam = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
               Compras[i].MtpPres = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
            }
         }

         // Cuantos Remesa.
         CuantasRemesa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VxRemesa = new T_Via[CuantasRemesa + 1];
         if (CuantasRemesa > 0)
         {
            for(i = 1; i <= VxRemesa.GetUpperBound(0); i += 1)
            {
               VxRemesa[i].NomBen = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxRemesa[i].NomVia = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxRemesa[i].NemMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxRemesa[i].MtoTot = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
            }
         }

         // Cuantas Vías.
         CuantasVias = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VxVia = new T_Via1[CuantasVias + 1];
         if (CuantasVias > 0)
         {
            for(i = 1; i <= VxVia.GetUpperBound(0); i += 1)
            {
               VxVia[i].Descri = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxVia[i].NomVia = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxVia[i].NemMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxVia[i].MtoTot = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
            }
         }

         // Cuantos Orígenes.
         CuantasOri = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VxOri = new T_Ori[CuantasOri + 1];
         if (CuantasOri > 0)
         {
            for(i = 1; i <= VxOri.GetUpperBound(0); i += 1)
            {
               VxOri[i].Descri = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxOri[i].NomOri = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxOri[i].NemMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxOri[i].MtoTot = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
            }
         }

         // Cargos (Comisiones)
         CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VDet = new T_Det[CuantosCom + 1];
         if (CuantosCom > 0)
         {
            for(i = 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "C";
            }
            MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         }

         // Instrucciones Especiales.-
         VInsEsp.InsExp = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Atributos del Usuario.
         UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // PACP 29/05/2001
         CuantosPlanilla = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VPlan = new T_Plan[CuantosPlanilla + 1];
         if (CuantosPlanilla > 0)
         {
            for(i = 1; i <= VPlan.GetUpperBound(0); i += 1)
            {
               VPlan[i].NroPlan = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }
         // PACP 29/05/2001

      }
      // ****************************************************************************
      //    1.  Se cargan los datos en variables y arreglos que luegos serán
      //        llevadas a papel para imprimir la carta Nro. 621.
      // ****************************************************************************'
      public static void Pr_Load_Doc621()
      {
         int CuantosPlanilla = 0;
         int CuantosCom = 0;
         int CuantasOri = 0;
         int CuantasRemesa = 0;
         int i = 0;
         int ContadorArbitraje = 0;

         // No existen Cargos por Vías.-
         VxVia = new T_Via1[1];

         PartysOpe = new PartyKey[2];
         PartysOpe[1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumIni()).TrimB();
         PartysOpe[1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Operacion Relacionada.-
         VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Referencias.
         VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Referencia Cliente.-
         VOperac.RefCli = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Cuantas Compras.
         ContadorArbitraje = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         VArb = new T_VArb[ContadorArbitraje + 1];
         if (ContadorArbitraje > 0)
         {
            for(i = 1; i <= VArb.GetUpperBound(0); i += 1)
            {
               VArb[i].NemMndC = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VArb[i].MtoCom = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
               VArb[i].NemMndV = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VArb[i].MtoVta = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
               VArb[i].PrdArb = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
            }
         }

         // Cuantos Remesa.
         CuantasRemesa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VxRemesa = new T_Via[CuantasRemesa + 1];
         if (CuantasRemesa > 0)
         {
            for(i = 1; i <= VxRemesa.GetUpperBound(0); i += 1)
            {
               VxRemesa[i].NomBen = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxRemesa[i].NomVia = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxRemesa[i].NemMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxRemesa[i].MtoTot = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
            }
         }

         // Cuantos Orígenes.
         CuantasOri = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VxOri = new T_Ori[CuantasOri + 1];
         if (CuantasOri > 0)
         {
            for(i = 1; i <= VxOri.GetUpperBound(0); i += 1)
            {
               VxOri[i].Descri = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxOri[i].NomOri = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxOri[i].NemMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VxOri[i].MtoTot = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
            }
         }

         // Cargos (Comisiones)
         CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VDet = new T_Det[CuantosCom + 1];
         if (CuantosCom > 0)
         {
            for(i = 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "C";
            }
            MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         }

         // Instrucciones Especiales.-
         VInsEsp.InsExp = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Atributos del Usuario.
         UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // PACP 29/05/2001
         CuantosPlanilla = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VPlan = new T_Plan[CuantosPlanilla + 1];
         if (CuantosPlanilla > 0)
         {
            for(i = 1; i <= VPlan.GetUpperBound(0); i += 1)
            {
               VPlan[i].NroPlan = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }
         // PACP 29/05/2001

      }
      // ****************************************************************************
      //    1.  Se cargan los datos en variables y arreglos que luegos serán
      //        llevadas a papel para imprimir la carta Nro. 999.
      // ****************************************************************************'
      public static void Pr_Load_Doc999()
      {
         int i = 0;
         double CuantosMontos = 0.0;
         string s = "";

         PartysOpe = new PartyKey[2];
         s = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumIni()).TrimB();
         VOperac.SinRaya = s;
         VOperac.ConRaya = s.Mid(1, 3) + "-" + s.Mid(4, 2) + "-" + s.Mid(6, 2) + "-" + s.Mid(8, 3) + "-" + s.Mid(11, 5);
         PartysOpe[1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Tipo de Aviso (D)ébito (C)rédito.
         TipoDC = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Nro. Cta.Cte.
         NroCtaCte = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Concepto
         Concepto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Arreglo de Montos
         CuantosMontos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         VMontos = new T_Montos[(CuantosMontos + 1).ToInt()];
         if (CuantosMontos > 0)
         {
            for(i = 1; i <= VMontos.GetUpperBound(0); i += 1)
            {
               VMontos[i].NemMnd = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VMontos[i].Montos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
            }
         }
         RefDC = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // PACP 29/05/2001
         if (RefDC == "")
         {
            RefDC = VOperac.ConRaya;
         }
         // en EXP se usa Referencia
         // 
         // Atributos del Usuario.
         UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

      }
      // ****************************************************************************
      //    1.  Se cargan los datos en variables y arreglos que luegos serán
      //        llevadas a papel para imprimir la carta de Exportador Nro. 610.
      // ****************************************************************************
      public static void Pr_Load_Exp610()
      {
         int n = 0;
         int CuantosCom = 0;
         int i = 0;
         int CuantosDeb = 0;

         PartysOpe = new PartyKey[MODXDOC.IExp1 + 1];
         PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumIni()).TrimB();
         PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Referencias.
         VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Débitos
         CuantosDeb = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VDet = new T_Det[CuantosDeb + 1];
         if (CuantosDeb > 0)
         {
            for(i = 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "D";
            }
         }

         // Comisiones
         CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         n = VDet.GetUpperBound(0);
         

         if (CuantosCom > 0)
         {
            Array.Resize(ref VDet, n + CuantosCom + 1);
            for(i = n + 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "C";
            }
            MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         }

         // Instrucciones al Exportador.
         Instrucciones = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Atributos del Usuario.
         UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

      }
      // ****************************************************************************
      //    1.  Se cargan los datos en variables y arreglos que luegos serán
      //        llevadas a papel para imprimir la carta de Exportador Nro. 611.
      // ****************************************************************************
      public static void Pr_Load_Exp611()
      {
         double TipCam = 0.0;
         int n = 0;
         int CuantosCom = 0;
         int CuantosDeb = 0;
         int CuantosPlanilla = 0;
         int i = 0;
         int CuantosDist = 0;

         PartysOpe = new PartyKey[MODXDOC.IGir + 1];
         CobRet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumIni()).TrimB();
         PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         PartysOpe[MODXDOC.IGir].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].ComunaUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IGir].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Montos.
         Moneda = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         MtoCap = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         MtoInt = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();

         // Referencias.
         VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Total/Parcial.
         Total_Parcial = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Tipo de Pago y a Qué
         // TipoPago = Trim$(CopiarDeString(Trim$(MeMo), Chr$(9), NumSig()))
         // PagoDe = Trim$(CopiarDeString(Trim$(MeMo), Chr$(9), NumSig()))
         // 
         // Arreglo de Distribución
         CuantosDist = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VDist = new T_Dist[CuantosDist + 1];
         if (CuantosDist > 0)
         {
            for(i = 1; i <= VDist.GetUpperBound(0); i += 1)
            {
               VDist[i].DisBen = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDist[i].DisVia = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDist[i].DisCon = MODGDOC.Minuscula(MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB());
               VDist[i].DisMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDist[i].DisMto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         FraseVa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Información General.
         VInf.InfMon1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInf.InfMto1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInf.InfMon2 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInf.InfMto2 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInf.InfMon3 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInf.InfMto3 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Frase de Gastos en el Exterior.-
         VInsEsp.Frases = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Instrucciones al Exportador.
         Instrucciones = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Arreglo de Distribución
         CuantosPlanilla = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VPlan = new T_Plan[CuantosPlanilla + 1];
         if (CuantosPlanilla > 0)
         {
            for(i = 1; i <= VPlan.GetUpperBound(0); i += 1)
            {
               VPlan[i].NroPlan = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].NroCod = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].NroDecl = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].PlaMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].PlaMto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         // Abonos (Débitos)
         CuantosDeb = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VDet = new T_Det[CuantosDeb + 1];
         if (CuantosDeb > 0)
         {
            for(i = 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "D";
            }
         }

         // Cargos (Comisiones)
         CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         n = VDet.GetUpperBound(0);
         
         if (CuantosCom > 0)
         {
            Array.Resize(ref VDet, n + CuantosCom + 1);
            for(i = n + 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "C";
            }
            MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         }

         // PACP 29/05/2001
         RefDC = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         // PACP 29/05/2001
         // 
         // Atributos del Usuario.
         UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // PACP 29/05/2001
         TipCam = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB().ToVal();
         // PACP 29/05/2001

      }
      // ****************************************************************************
      //    1.  Se cargan los datos en variables y arreglos que luegos serán
      //        llevadas a papel para imprimir la carta de Exportador Nro. 612.
      // ****************************************************************************
      public static void Pr_Load_Exp612()
      {

         PartysOpe = new PartyKey[MODXDOC.IGir + 1];

         PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumIni()).TrimB();
         PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Referencias.
         VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VxCob.RefExp2 = "";

         VDetalle = new T_Detalle[2];
         VDetalle[1].Orden = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VDetalle[1].Refer = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VDetalle[1].Moneda = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VDetalle[1].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Instrucciones al Exportador.
         Instrucciones = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // PACP 29/05/2001
         // Referencia.-
         RefDC = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         if (RefDC == "")
         {
            RefDC = VOpe.NumOpe_t;
         }
         // PACP 29/05/2001
         // 
         // Atributos del Usuario.
         UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

      }
      // ****************************************************************************
      //    1.  Se cargan los datos en variables y arreglos que luegos serán
      //        llevadas a papel para imprimir la carta de Exportador Nro. 613.
      // ****************************************************************************
      public static void Pr_Load_Exp613()
      {
         double TipCam = 0.0;
         int CuantosCom = 0;
         int CuantosDeb = 0;
         int i = 0;
         int CuantosPlanilla = 0;

         PartysOpe = new PartyKey[MODXDOC.IExp1 + 1];
         PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumIni()).TrimB();
         PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Operacion Relacionada.-
         VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Referencias.
         VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Referencia Cliente.-
         VOperac.RefCli = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Arreglo de Distribución
         CuantosPlanilla = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VPlan = new T_Plan[CuantosPlanilla + 1];
         if (CuantosPlanilla > 0)
         {
            for(i = 1; i <= VPlan.GetUpperBound(0); i += 1)
            {
               VPlan[i].NroPlan = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].NroCod = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].NroDecl = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].PlaMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].PlaMto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         // Abonos (Débitos)
         CuantosDeb = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VDet = new T_Det[CuantosDeb + 1];
         if (CuantosDeb > 0)
         {
            for(i = 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "D";
            }
         }

         // Cargos (Comisiones)
         CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VDet = new T_Det[CuantosCom + 1];
         if (CuantosCom > 0)
         {
            for(i = 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "C";
            }
            MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         }

         // Instrucciones al Exportador.
         VInsEsp.InsExp = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Atributos del Usuario.
         UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // PACP 29/05/2001
         TipCam = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         // PACP 29/05/2001

      }
      // ****************************************************************************
      //    1.  Se cargan los datos en variables y arreglos que luegos serán
      //        llevadas a papel para imprimir la carta de Exportador Nro. 611.
      // ****************************************************************************
      public static void Pr_Load_Exp614()
      {
         double TipCam = 0.0;
         int n = 0;
         int CuantosCom = 0;
         int CuantosDeb = 0;
         int CuantosPlanilla = 0;
         int i = 0;
         int CuantosDist = 0;

         PartysOpe = new PartyKey[MODXDOC.IGir + 1];
         CobRet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumIni()).TrimB();
         PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Montos.
         Moneda = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         MtoCap = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();
         MtoInt = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToVal();

         // Referencias Exportador.
         VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Referencias Ordenante.
         VxCob.RefExp2 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Total/Parcial.
         Total_Parcial = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         SaldoRet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Tipo de Pago y a Qué
         // TipoPago = Trim$(CopiarDeString(Trim$(MeMo), Chr$(9), NumSig()))
         // PagoDe = Trim$(CopiarDeString(Trim$(MeMo), Chr$(9), NumSig()))
         // 
         // Arreglo de Distribución
         CuantosDist = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VDist = new T_Dist[CuantosDist + 1];
         if (CuantosDist > 0)
         {
            for(i = 1; i <= VDist.GetUpperBound(0); i += 1)
            {
               VDist[i].DisBen = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDist[i].DisVia = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDist[i].DisCon = MODGDOC.Minuscula(MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB());
               VDist[i].DisMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDist[i].DisMto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         FraseVa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         // Información General.
         VInf.InfMon1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInf.InfMto1 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInf.InfMon2 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInf.InfMto2 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInf.InfMon3 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         VInf.InfMto3 = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Frase de Gastos en el Exterior.-
         VInsEsp.Frases = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Instrucciones al Exportador.
         Instrucciones = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // Arreglo de Distribución
         CuantosPlanilla = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VPlan = new T_Plan[CuantosPlanilla + 1];
         if (CuantosPlanilla > 0)
         {
            for(i = 1; i <= VPlan.GetUpperBound(0); i += 1)
            {
               VPlan[i].NroPlan = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].NroCod = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].NroDecl = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].PlaMon = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VPlan[i].PlaMto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            }
         }

         // Abonos (Débitos)
         CuantosDeb = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();
         VDet = new T_Det[CuantosDeb + 1];
         if (CuantosDeb > 0)
         {
            for(i = 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "D";
            }
         }

         // Cargos (Comisiones)
         CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).ToInt();

         n = VDet.GetUpperBound(0);
         
         if (CuantosCom > 0)
         {
            Array.Resize(ref VDet, n + CuantosCom + 1);
            for(i = n + 1; i <= VDet.GetUpperBound(0); i += 1)
            {
               VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
               VDet[i].tipo = "C";
            }
            MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
            MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         }

         // PACP 29/05/2001
         // Referencia.-
         RefDC = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         if (RefDC == "")
         {
            RefDC = VOpe.NumOpe_t;
         }
         // PACP 29/05/2001
         // 
         // Atributos del Usuario.
         UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();
         UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB();

         // PACP 29/05/2001
         TipCam = MODGDOC.CopiarDeString(Memo.TrimB(),9.Char(),MODGSYB.NumSig()).TrimB().ToVal();
         // PACP 29/05/2001

      }
      // ****************************************************************************
      // Autor          : Orieta Gamonal Gutiérrez
      // Fecha          : Junio 1995
      // Propósito      : Imprimir los Montos.
      // Observaciones  : Coloca los Montos correspondientes para el caso de
      //                  las cartas Nºs 2,3,4 (Agente...........Exportador,
      //                  Exportador...........Girado)
      // ****************************************************************************
      public static void Pr_Montos(int Carta_Aux)
      {
         int i = 0;
         double Total = 0.0;
         int n = 0;

         n = Vencimientos.GetUpperBound(0);
         

         // Printer.Print : Call Pr_Salto_Pagina
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
         switch(Carta_Aux)
         {
         case 1:
         case 3:
            if (Idioma == "I")
            {
               XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(45),"Amounts"});
            }
            else
            {
               XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(45),"Montos"});
            }
            break;
         default:
            XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(45),"Montos"});
            break;
         }
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         // si existen datos, se imprimen los montos
         if (VxCob.Nemonic.TrimB() != "")
         {
            switch(Carta_Aux)
            {
            case 1:
               if (Idioma == "I")
               {
                  XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Amount",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(
                     VxCob.MtoCob,MODXDOC.Formato)});
                  Pr_Salto_Pagina();
                  if (VxCob.MtoInt > 0)
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Drawer´s Interest    ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),
                        MODGDOC.forma(VxCob.MtoInt,MODXDOC.Formato)});
                     Pr_Salto_Pagina();
                  }
                  if (VxCob.Cedente1 > 0)
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Our Charges        ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),
                        MODGDOC.forma(VxCob.Cedente1,MODXDOC.Formato)});
                     Pr_Salto_Pagina();
                  }
                  Total = VxCob.MtoCob + VxCob.MtoInt + VxCob.Cedente1;
                  if (Total > VxCob.MtoCob)
                  {
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                     XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Total ",XGCV.Printer.TAB((short)(tab_mto_descr - 7)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto - 3)),MODGDOC.forma
                        (Total,MODXDOC.Formato)});
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                     Pr_Salto_Pagina();
                  }
               }
               else
               {
                  XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Monto ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(
                     VxCob.MtoCob,MODXDOC.Formato)});
                  Pr_Salto_Pagina();
                  if (VxCob.MtoInt > 0)
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Interes Proveedor ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),
                        MODGDOC.forma(VxCob.MtoInt,MODXDOC.Formato)});
                     Pr_Salto_Pagina();
                  }
                  if (VxCob.Cedente1 > 0)
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Nuestros Gastos ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),
                        MODGDOC.forma(VxCob.Cedente1,MODXDOC.Formato)});
                     Pr_Salto_Pagina();
                  }
                  Total = VxCob.MtoCob + VxCob.MtoInt + VxCob.Cedente1;
                  if (Total > VxCob.MtoCob)
                  {
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                     XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Total ",XGCV.Printer.TAB((short)(tab_mto_descr - 7)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto - 3)),MODGDOC.forma
                        (Total,MODXDOC.Formato)});
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                     Pr_Salto_Pagina();
                  }
               }
               break;
            case 3:
               switch(Idioma)
               {
               case "E":
                  XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Monto ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(
                     VxCob.MtoCob,MODXDOC.Formato)});
                  Pr_Salto_Pagina();
                  if (VxCob.MtoInt > 0)
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Interes Proveedor ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),
                        MODGDOC.forma(VxCob.MtoInt,MODXDOC.Formato)});
                     Pr_Salto_Pagina();
                  }
                  Total = VxCob.MtoCob + VxCob.MtoInt;
                  if (Total > VxCob.MtoCob)
                  {
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                     XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Total ",XGCV.Printer.TAB((short)(tab_mto_descr - 7)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto - 3)),MODGDOC.forma
                        (Total,MODXDOC.Formato)});
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                     Pr_Salto_Pagina();
                  }
                  break;
               case "I":
                  XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Amount ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(
                     VxCob.MtoCob,MODXDOC.Formato)});
                  Pr_Salto_Pagina();
                  if (VxCob.MtoInt > 0)
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Drawer´s Interest    ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),
                        MODGDOC.forma(VxCob.MtoInt,MODXDOC.Formato)});
                     Pr_Salto_Pagina();
                  }
                  Total = VxCob.MtoCob + VxCob.MtoInt;
                  if (Total > VxCob.MtoCob)
                  {
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                     XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Total ",XGCV.Printer.TAB((short)(tab_mto_descr - 7)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto - 3)),MODGDOC.forma
                        (Total,MODXDOC.Formato)});
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                     Pr_Salto_Pagina();
                  }
                  break;
               }
               break;
            default:
               XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Monto ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(
                  VxCob.MtoCob,MODXDOC.Formato)});
               Pr_Salto_Pagina();
               if (VxCob.MtoInt > 0)
               {
                  XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Interes Proveedor ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),
                     MODGDOC.forma(VxCob.MtoInt,MODXDOC.Formato)});
                  Pr_Salto_Pagina();
               }
               Total = VxCob.MtoCob + VxCob.MtoInt;
               if (Total > VxCob.MtoCob)
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Total ",XGCV.Printer.TAB((short)(tab_mto_descr - 7)),VxCob.Nemonic.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto - 3)),MODGDOC.forma
                     (Total,MODXDOC.Formato)});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
               }
               break;
            }
         }

         // si existen vencimientos, se imprimen las fechas
         if (n > 0)
         {
            if (Carta_Aux == 1 || Carta_Aux == 2 || Carta_Aux == 3)
            {
               XGCV.Printer.DefInstance.Print( );
               Pr_Salto_Pagina();
               if (Idioma == "I" && Carta != 2)
               {
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(new string(' ',tabulador - 23),"Maturity   : ");
               }
               else
               {
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(new string(' ',tabulador - 23),"Vencimiento: ");
               }
               if (Idioma == "I" && Carta != 2)
               {
                  for(i = 1; i <= n; i += 1)
                  {
                     if (i == 1 && Vencimientos[i].FecVen == "")
                     {
                        XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(Vencimientos[i].TotVen,
                           MODXDOC.Formato),XGCV.Printer.TAB(89),"Payment"});
                     }
                     else
                     {
                        XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(Vencimientos[i].TotVen,
                           MODXDOC.Formato),XGCV.Printer.TAB(89),MigrationSupport.Utils.Format(Vencimientos[i].FecVen,"mm/dd/yyyy").TrimB()});
                     }
                     Pr_Salto_Pagina();
                  }
               }
               else
               {
                  for(i = 1; i <= n; i += 1)
                  {
                     if (i == 1 && Vencimientos[i].FecVen == "")
                     {
                        XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(Vencimientos[i].TotVen,
                           MODXDOC.Formato),XGCV.Printer.TAB(89),"A la Vista"});
                     }
                     else
                     {
                        XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(Vencimientos[i].TotVen,
                           MODXDOC.Formato),XGCV.Printer.TAB(89),MigrationSupport.Utils.Format(Vencimientos[i].FecVen,"dd/mm/yyyy").TrimB()});
                     }
                     Pr_Salto_Pagina();
                  }
               }
            }
            else
            {
               for(i = 1; i <= n; i += 1)
               {
                  if (Vencimientos[i].FecVen.TrimB() != "")
                  {
                     if (i == 1)
                     {
                        XGCV.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Vencimiento: ",XGCV.Printer.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),
                           MODGDOC.forma(Vencimientos[i].TotVen,MODXDOC.Formato),XGCV.Printer.TAB(89),MigrationSupport.Utils.Format(Vencimientos[i].FecVen,"dd/mm/yyyy").TrimB()});
                     }
                     else
                     {
                        XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),XGCV.Printer.TAB((short)(tab_mto_monto)),MODGDOC.forma(Vencimientos[i].TotVen,
                           MODXDOC.Formato),XGCV.Printer.TAB(89),MigrationSupport.Utils.Format(Vencimientos[i].FecVen,"dd/mm/yyyy").TrimB()});
                     }
                     Pr_Salto_Pagina();
                  }
               }
            }
         }

      }
      // ****************************************************************************
      // Autor          : Orieta Gamonal Gutiérrez
      // Fecha          : Septiembre 1995
      // Propósito      : Imprimir los Montos de los Avisos de Débitos o Créditos.
      // Observaciones  : Coloca los Montos correspondientes para el caso de
      //                  la carta Nº 999.
      // Cuando vienen 2 movimientos => Se asume que Moneda Nacional c/Imp. de $ 103.-
      // ****************************************************************************
      public static void Pr_Mto_DebitoCredito()
      {
         string Nemonico = "";
         int i = 0;
         double Total = 0.0;
         int n = 0;

         n = VMontos.GetUpperBound(0);
         

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         // si existen datos, se imprimen los montos
         Total = 0;
         for(i = 1; i <= n; i += 1)
         {
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(30),VMontos[i].NemMnd.TrimB());
            if (i == 2)
            {
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
            }
            XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(35),MODGDOC.forma(VMontos[i].Montos,MODXDOC.Formato)});
            if (i == 2)
            {
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
            }
            Total = Total + VMontos[i].Montos;
            Nemonico = VMontos[i].NemMnd;
         }
         if (Nemonico == "$" && n == 2)
         {
            XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(30),Nemonico,XGCV.Printer.TAB(35),MODGDOC.forma(Total,MODXDOC.Formato)});
         }
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

      }
      // ****************************************************************************
      //    1.  Despliega la información del Detalle de Retorno
      //        (Ordenantes - Referencia - Monto).
      // ****************************************************************************
      public static void Pr_Ordenante()
      {
         int i = 0;
         int n = 0;

         n = VDetalle.GetUpperBound(0);
         

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(15),"Ordenante",XGCV.Printer.TAB(53),"Referencia",XGCV.Printer.TAB(94),"Monto"});
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         for(i = 1; i <= n; i += 1)
         {
            XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(16),VDetalle[i].Orden.TrimB(),XGCV.Printer.TAB(56),VDetalle[i].Refer.TrimB(),XGCV.Printer.TAB(78),VDetalle[i].Moneda.TrimB(),
               XGCV.Printer.TAB(85),MODGDOC.forma(VDetalle[i].Monto.ToVal(),MODXDOC.Formato)});
         }
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

      }
      // ****************************************************************************
      //    1.  Despliega las Planillas del Detalle(Planillas - Código -
      //        Declaración - Monto).
      // ****************************************************************************
      public static void Pr_Planilla()
      {
         string Formateo = "";
         int i = 0;
         int n = 0;

         n = VPlan.GetUpperBound(0);
         

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.PrintList(new object[]{"Planilla",XGCV.Printer.TAB(30),"Código",XGCV.Printer.TAB(61),"Declaración",XGCV.Printer.TAB(104),"Monto"});
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         for(i = 1; i <= n; i += 1)
         {
            if (VPlan[i].NroDecl != "")
            {
               Formateo = VPlan[i].NroDecl.Left((VPlan[i].NroDecl.Len() - 1)) + "-" + VPlan[i].NroDecl.Right(1);
            }
            else
            {
               Formateo = "-------";
            }
            XGCV.Printer.DefInstance.PrintWithoutCrlf(VPlan[i].NroPlan.TrimB(),XGCV.Printer.TAB(31),VPlan[i].NroCod.TrimB(),XGCV.Printer.TAB(65),Formateo.TrimB(),XGCV.Printer.TAB(89),VPlan[i].PlaMon.TrimB(),
               XGCV.Printer.TAB(95),MODGDOC.forma(VPlan[i].PlaMto.ToVal(),MODXDOC.Formato));
            XGCV.Printer.DefInstance.Print( );
            Pr_Salto_Pagina();
         }

      }
      // ****************************************************************************
      //    1.  Detalle de las Remesas.
      // ****************************************************************************
      public static void Pr_Remesa()
      {
         int i = 0;
         int n = 0;

         n = VxRemesa.GetUpperBound(0);
         

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(6),"Beneficiario",XGCV.Printer.TAB(40),"Vía de la Remesa",XGCV.Printer.TAB(95),"Monto"});
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         for(i = 1; i <= n; i += 1)
         {
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(7),VxRemesa[i].NomBen);
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(43),VxRemesa[i].NomVia);
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(85),VxRemesa[i].NemMon);
            XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(98),MODGDOC.forma(VxRemesa[i].MtoTot,MODXDOC.Formato)});
            Pr_Salto_Pagina();
         }

      }
      // ****************************************************************************
      // Autor          : Orieta Gamonal Gutiérrez
      // Fecha          : Junio 1995
      // Propósito      : Salta de Página.
      // Observaciones  : Almacena la línea de posición para controlar el alto de
      //                  la página, cuando este llega a 56, comienza con una nueva
      //                  página imprimiendo, inicialmente, el encabezado y títulos
      //                  correspondientes
      // ****************************************************************************
      public static void Pr_Salto_Pagina()
      {
         int i = 0;

         if (linea >= 57)
         {
            ImprimePagina();
            Pagina = Pagina + 1;
            XGCV.Printer.DefInstance.NewPage();
            linea = 10;
            for(i = 1; i <= linea; i += 1)
            {
               XGCV.Printer.DefInstance.Print( );
               Pr_Salto_Pagina();
            }
         }
         else if (linea < 63)
         {
            linea = linea + 1;
         }

      }
      // ****************************************************************************
      // Autor          : Orieta Gamonal Gutiérrez
      // Fecha          : Junio 1995
      // Propósito      : Salta de Página.
      // Observaciones  : Almacena la línea de posición para controlar el alto de
      //                  la página, cuando este llega a 56, comienza con una nueva
      //                  página imprimiendo, inicialmente, el encabezado y títulos
      //                  correspondientes
      // ****************************************************************************
      public static void Pr_SaltoPag()
      {

         linea = linea + 1;

      }
      // ****************************************************************************
      //    1.  Despliega el Título de los Abonos y/o Cargos, esto es sólo para el
      //        caso de la carta 611.
      // ****************************************************************************
      public static void Pr_Titulo_Abono()
      {
         int n = 0;

         n = VDet.GetUpperBound(0);
         

         if (n > 0)
         {
            // Printer.Print : Call Pr_Salto_Pagina
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
            XGCV.Printer.DefInstance.Print( );
            Pr_Salto_Pagina();
            XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(45),"Abonos y/o Cargos"});
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
            XGCV.Printer.DefInstance.Print( );
            Pr_Salto_Pagina();
         }

      }
      // ****************************************************************************
      // Autor          : Orieta Gamonal Gutiérrez
      // Fecha          : Junio 1995
      // Propósito      : Imprime los Tíitulos de Carta.
      // Observaciones  : Coloca el título correspondiente a la carta a imprimir
      // ****************************************************************************
      public static void Pr_Titulos(int Carta_Aux)
      {
         string Paso = "";

         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeSize(XGCV.Printer.DefInstance.Font, 12);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         if (VOpe.NumOpe.Mid(1, 3) == "753")
         {
            XGCV.Printer.DefInstance.PrintWithoutCrlf("Servicios M/E");
         }
         else
         {
            XGCV.Printer.DefInstance.PrintWithoutCrlf("Comercio Exterior");
         }
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeSize(XGCV.Printer.DefInstance.Font, 10);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75));

         // -------------------------------
         // Registra la Fecha de la Carta.
         switch(Carta_Aux)
         {
         case 1:
         case 3:
         case 20:
            if (Idioma == "I")
            {
               Paso = Glosa_Fecha_Hoy_Ingles(MODDOC.VDocx.FecEmi);
               XGCV.Printer.DefInstance.Print(DOC_CVDI.Ciudad + ", " + Paso.TrimB());
            }
            else
            {
               Paso = Glosa_Fecha_Hoy_Espanol(MODDOC.VDocx.FecEmi);
               XGCV.Printer.DefInstance.Print(DOC_CVDI.Ciudad + ", " + Paso.TrimB());
            }
            break;
         case 9:
            Paso = Glosa_Fecha_Hoy_Espanol(MODDOC.VDocx.FecEmi);
            XGCV.Printer.DefInstance.Print(DOC_CVDI.Ciudad + ", " + Paso.TrimB());
            break;
         default:
            Paso = Glosa_Fecha_Hoy_Espanol(MODDOC.VDocx.FecEmi);
            XGCV.Printer.DefInstance.Print(DOC_CVDI.Ciudad + ", " + Paso.TrimB());
            break;
         }
         Pr_Salto_Pagina();
         // -------------------------------
         // 
         // -------------------------------
         // Referencia del Banco de Chile.
         if (VOpe.TipoDoc == MODXDOC.DocxCobReg)
         {
            if (Carta_Aux == 0)
            {
               if (VOperac.ConRaya.TrimB() != "")
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Nuestra Referencia");
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(94),VOperac.ConRaya.TrimB()});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
               }
            }
            switch(Carta_Aux)
            {
            case 1:
            case 3:
               if (VOperac.ConRaya.TrimB() != "")
               {
                  if (Idioma == "I")
                  {
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                     XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Our Reference");
                     XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(94),VOperac.ConRaya.TrimB()});
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                     Pr_Salto_Pagina();
                  }
                  else
                  {
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                     XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Nuestra Referencia");
                     XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(94),VOperac.ConRaya.TrimB()});
                     XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                     Pr_Salto_Pagina();
                  }
               }
               break;
            case 2:
               if (VOperac.ConRaya.TrimB() != "")
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Nuestra Referencia");
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(94),VOperac.ConRaya.TrimB()});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
               }
               break;
            }
         }
         else if (VOpe.TipoDoc == MODXDOC.DocxCobRen)
         {
            if (VOperac.ConRaya.TrimB() != "")
            {
               if (Idioma == "I")
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Our Reference");
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(94),VOperac.ConRaya.TrimB()});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
               }
               else
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Nuestra Referencia");
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(94),VOperac.ConRaya.TrimB()});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
               }
            }
            if (VxCob.RefBcoC.TrimB() != "")
            {
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Referencia Cobrador",XGCV.Printer.TAB(99));
               XGCV.Printer.DefInstance.Print(VxCob.RefBcoC.TrimB());
               Pr_Salto_Pagina();
            }
         }
         else if (VOpe.TipoDoc == MODXDOC.DocxAceLet)
         {
            if (Carta_Aux == 4)
            {
               if (VOperac.ConRaya.TrimB() != "")
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Nuestra Referencia");
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(94),VOperac.ConRaya.TrimB()});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
               }
            }
         }
         else if (VOpe.TipoDoc == MODXDOC.DocxPagDir)
         {
            if (Carta_Aux == 5)
            {
               if (VOpe.NumOpe_t.TrimB() != "")
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Nuestra Referencia");
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(94),VOpe.NumOpe_t.TrimB()});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
               }
            }
         }
         else if (VOpe.TipoDoc == MODXDOC.DocxCobCan || VOpe.TipoDoc == MODXDOC.DocxCanRet || VOpe.TipoDoc == MODXDOC.DocxRegCanRet)
         {
            if (Carta_Aux == 6 || Carta_Aux == 14)
            {
               if (VOpe.NumOpe_t.TrimB() != "")
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Nuestra Referencia");
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(94),VOpe.NumOpe_t.TrimB()});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(71),"Monto ",Moneda.TrimB() + " " + MODGDOC.forma(MtoCap,MODXDOC.Formato) + " Capital"});
                  Pr_Salto_Pagina();
                  if (MtoInt > 0)
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(71),"Monto ",Moneda.TrimB() + " " + MODGDOC.forma(MtoInt,MODXDOC.Formato) + " Intereses"});
                     Pr_Salto_Pagina();
                  }
               }
            }
         }
         else if (VOpe.TipoDoc == MODXDOC.DocxRegRet)
         {
            if (Carta_Aux == 7)
            {
               if (VOpe.NumOpe_t.TrimB() != "")
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Nuestra Referencia");
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(94),VOpe.NumOpe_t.TrimB()});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
               }
            }
         }
         else if (VOpe.TipoDoc == MODXDOC.DocxRegPln)
         {
            if (Carta_Aux == 8)
            {
               if (VOpe.NumOpe_t.TrimB() != "")
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Nuestra Referencia");
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(100),VOpe.NumOpe_t.TrimB()});
                  if (VOperac.ConRaya != "")
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(71),"Operación Relacionada",XGCV.Printer.TAB(100),VOperac.ConRaya});
                  }
                  if (VOperac.RefCli != "")
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(71),"Su Referencia",XGCV.Printer.TAB(100),VOperac.RefCli});
                  }
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
               }
            }
         }
         else if (VOpe.TipoDoc == MODXDOC.DocCVD)
         {
            if (Carta_Aux == 10)
            {
               if (VOpe.NumOpe_t.TrimB() != "")
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(71),"Compra-Venta Divisas Nº : " + VOpe.NumOpe_t.TrimB()});
                  if (VOperac.ConRaya != "")
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(71),"Operación Relacionada      : " + VOperac.ConRaya});
                  }
                  if (VOperac.RefCli != "")
                  {
                     XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(71),"Su Referencia                      : " + VOperac.RefCli});
                  }
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
                  Pr_Salto_Pagina();
               }
            }
         }
         else if (VOpe.TipoDoc == MODXDOC.DocArb)
         {
            if (Carta_Aux == 11)
            {
               if (VOpe.NumOpe_t.TrimB() != "")
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Nuestra Referencia");
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(98),VOpe.NumOpe_t.TrimB()});
                  if (VOperac.ConRaya != "")
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Operación Relacionada");
                     XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(98),VOperac.ConRaya});
                  }
                  if (VOperac.RefCli != "")
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Su Referencia");
                     XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(98),VOperac.RefCli});
                  }
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
               }
            }
         }
         else if (VOpe.TipoDoc == MODXDOC.DocGAcre || VOpe.TipoDoc == MODXDOC.DocGAdeb)
         {
            if (Carta_Aux == 9)
            {
               if (VOpe.NumOpe_t.TrimB() != "")
               {
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(71),"Operación Número");
                  XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(94),VOpe.NumOpe_t.TrimB()});
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  Pr_Salto_Pagina();
               }
            }
            if (RefDC != "")
            {
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Su Referencia",XGCV.Printer.TAB(99));
               XGCV.Printer.DefInstance.Print(RefDC.TrimB());
            }
         }

         // -------------------------------
         // -------------------------------
         // Registra la Referencia del que envía la carta.
         switch(Carta_Aux)
         {
         case 0:
         case 1:
            // Referencia del Girador.
            if (VxCob.RefExp1.TrimB() != "")
            {
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               if (Carta_Aux == 1)
               {
                  if (Idioma == "I")
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Drawer's Reference",XGCV.Printer.TAB(99));
                  }
                  else
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Referencia Girador",XGCV.Printer.TAB(99));
                  }
               }
               else
               {
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Referencia Girador",XGCV.Printer.TAB(99));
               }
               XGCV.Printer.DefInstance.Print(VxCob.RefExp1.TrimB());
               Pr_Salto_Pagina();
            }
            break;
         case 2:
         case 5:
         case 6:
         case 8:
            // Referencia del Exportador.
            if (VxCob.RefExp1.TrimB() != "")
            {
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Su Referencia",XGCV.Printer.TAB(99));
               XGCV.Printer.DefInstance.Print(VxCob.RefExp1.TrimB());
               Pr_Salto_Pagina();
            }
            // Registro y Reg. + Cancelación Retorno.-
            break;
         case 7:
         case 14:
            // Referencia del Exportador.
            if (VxCob.RefExp1.TrimB() != "")
            {
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Su Referencia",XGCV.Printer.TAB(99));
               XGCV.Printer.DefInstance.Print(VxCob.RefExp1.TrimB());
               Pr_Salto_Pagina();
            }
            // Referencia del Ordenante.
            if (VxCob.RefExp2.TrimB() != "")
            {
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Referencia Ordenante",XGCV.Printer.TAB(99));
               XGCV.Printer.DefInstance.Print(VxCob.RefExp2.TrimB());
               Pr_Salto_Pagina();
            }
            break;
         case 3:
            // Referencia del Exportador.
            if (VxCob.RefExp1.TrimB() != "")
            {
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               if (Idioma == "I")
               {
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Drawer's Reference",XGCV.Printer.TAB(99));
               }
               else
               {
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Referencia Exportador",XGCV.Printer.TAB(99));
               }
               XGCV.Printer.DefInstance.Print(VxCob.RefExp1.TrimB());
               Pr_Salto_Pagina();
            }
            break;
         case 4:
            // Referencia del Exportador.
            // Referencia del Cobrador.
            if (VxCob.RefExp1.TrimB() != "")
            {
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Su Referencia",XGCV.Printer.TAB(99));
               XGCV.Printer.DefInstance.Print(VxCob.RefExp1.TrimB());
               Pr_Salto_Pagina();
            }
            if (VxCob.RefBcoC.TrimB() != "")
            {
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(75),"Referencia Cobrador",XGCV.Printer.TAB(99));
               XGCV.Printer.DefInstance.Print(VxCob.RefBcoC.TrimB());
               Pr_Salto_Pagina();
            }
            break;
         }
         // -------------------------------

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

      }
      // ****************************************************************************
      //    1.  Detalle de las Ventas.
      // ****************************************************************************
      public static void Pr_Ventas()
      {
         int i = 0;
         int n = 0;

         n = Ventas.GetUpperBound(0);
         

         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, true);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
         //TODO XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(40),"Ventas de Divisas:"});
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
         XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(XGCV.Printer.DefInstance.Font, false);
         Pr_Salto_Pagina();
         XGCV.Printer.DefInstance.Print( );
         Pr_Salto_Pagina();

         for(i = 1; i <= n; i += 1)
         {
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(7),Ventas[i].NemMnd);
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(20),MODGDOC.forma(Ventas[i].MtoCVD,MODXDOC.Formato));
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(45),"Tipo de Cambio $");
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(65),MODGDOC.forma(Ventas[i].TipCam,MODXDOC.FormatoTipCamb));
            XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB(88),"Total $");
            //TODO XGCV.Printer.DefInstance.PrintList(new object[]{XGCV.Printer.TAB(98),MODGDOC.forma(Ventas[i].MtpPres,MODXDOC.Formato)});
            Pr_Salto_Pagina();
         }

      }
      // ****************************************************************************
      //    1.  Entrega la Cobranza en formato xxx-xx-xx-xx-xxxxxx.
      // ****************************************************************************
      public static string raya_Cobranza(string s)
      {
         string raya_Cobranza = "";

         string e = "";
         string d = "";
         string c = "";
         string b = "";
         string a = "";

         a = s.Mid(1, 3);
         b = s.Mid(4, 2);
         c = s.Mid(6, 2);
         d = s.Mid(8, 3);
         e = s.Mid(11, 5);

         raya_Cobranza = a + "-" + b + "-" + c + "-" + d + "-" + e;

         return raya_Cobranza;
      }
      // ****************************************************************************
      //    1.  Setea variables importantes relacionadas con la impresión.
      // ****************************************************************************
      public static void SetupLetras()
      {

         try
         {
            // En cms.
            if (XGCV.Printer.DefInstance.ScaleMode != (short)MigrationSupport.Utils.ScaleType.vbCentimeters)
            {
               // UPGRADE_TODO: Printer.ScaleMode
               // UPGRADE_TODO: XGCV.Printer.DefInstance.ScaleMode = MigrationSupport.Utils.ScaleType.vbCentimeters;
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeName(XGCV.Printer.DefInstance.Font, "Times New Roman");
            }

            return;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            System.Windows.Forms.MessageBox.Show("Error Number " + MigrationSupport.GlobalException.Instance.Number.ToStr() + " occurred at line " + VBNET.Information.Erl().ToStr(), "", MessageBoxButtons.OK);

         }
      }
      // ****************************************************************************
      //    1.  Lee la tabla de Usuarios dependiendo de un Centro de Costo y
      //        Código del Usuario.
      //        Retorno    <> ""  : Lectura Exitosa.
      //                   =  ""  : Error o Lectura no Exitosa.
      // ****************************************************************************
      public static int SyGet_Usr(string cencos,string CodUsr)
      {
         int SyGet_Usr = 0;

         int m = 0;
         int n = 0;
         string R = "";
         string Que = "";
         int largo = 0;

         try
         {

            largo = UsrEsps.GetUpperBound(0);
            

            //Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "Sce_Usr_S02 ";
            //Que = Que + MODGSYB.dbcharSy(cencos) + " , ";
            //Que = Que + MODGSYB.dbcharSy(CodUsr);
            //Que = Que.LCase();

            // Se ejecuta el Query.
            //R = MODGSRM.RespuestaQuery(ref Que);

            var result = XgcvService.Instance.Sce_Usr_S02(MODGSYB.dbcharSy(cencos), MODGSYB.dbcharSy(CodUsr));



            if (result == null || result.Count == 0)
            {
               return SyGet_Usr;
            }

            n = MODGSRM.RowCount;
            Array.Resize(ref UsrEsps, largo + n + 1);
            m = UsrEsps.GetUpperBound(0);

            UsrEsps[m].cencos = result[0].cent_costo;//MODGSYB.GetPosSy(MODGSYB.NumIni(),"C",R).ToStr().TrimB();
            UsrEsps[m].CodUsr = result[0].id_especia; //MODGSYB.GetPosSy(MODGSYB.NumSig(),"C",R).ToStr().TrimB();
            UsrEsps[m].nombre = result[0].nombre;// MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr().TrimB();
            UsrEsps[m].Direccion = result[0].direccion;// MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr().TrimB();
            UsrEsps[m].Ciudad = result[0].ciudad;// MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).ToStr().TrimB();
            UsrEsps[m].telefono = result[0].telefono; // MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).ToStr().TrimB();
            UsrEsps[m].Fax = result[0].fax; // MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).ToStr().TrimB();

            SyGet_Usr = m;

            return SyGet_Usr;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            MigrationSupport.Utils.MsgBox("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + "] " + MigrationSupport.Utils.GetErrorDescription(MigrationSupport.GlobalException.Instance.Number),MODGDOC.Pito(48).Cast<
               MigrationSupport.MsgBoxStyle>(),MODXDOC.MsgxDoc);

         }
         return SyGet_Usr;
      }
      // ****************************************************************************
      // Autor          : Orieta Gamonal Gutiérrez
      // Fecha          : Junio 1995
      // Propósito      : Imprime los Encabecados de las Cartas.
      // Observaciones  : Coloca el encabezado correspondiente a la carta a imprimir
      // ****************************************************************************
      public static void xDoc_Cob(int Indice_Var1,int Indice_Var2)
      {
         string MsgErrImp = "";
         string Paso = "";
         string Cas = "";
         int n = 0;

         try
         {

            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, true);

            n = PartysOpe.GetUpperBound(0);
            

            if (Indice_Var1 > n || Indice_Var2 > n)
            {
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
               return;
            }

            switch(PartysOpe[Indice_Var1].Enviara)
            {
            case 0:
            case 1:
               switch(Num_Carta)
               {
               case 1:
               case 3:
               case 20:
                  if (Idioma == "I")
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf("Messrs",XGCV.Printer.TAB((short)(tabulador)));
                  }
                  else
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf("Señores",XGCV.Printer.TAB((short)(tabulador)));
                  }
                  break;
               default:
                  if (Num_Carta == 6 || Num_Carta == 14)
                  {
                     switch(CobRet)
                     {
                     case "C":
                        XGCV.Printer.DefInstance.PrintWithoutCrlf("Señor(es)",XGCV.Printer.TAB((short)(tabulador)));
                        XGCV.Printer.DefInstance.Print("Girado");
                        break;
                     case "R":
                        XGCV.Printer.DefInstance.Print("Señor(es)");
                        break;
                     }
                  }
                  else
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf("Señores",XGCV.Printer.TAB((short)(tabulador)));
                  }
                  break;
               }
               // Icob = 0: Igrr = 1: Iexp = 2: Igir = 3: Iage = 4
               if (Num_Carta == 0 || Num_Carta == 1 || Num_Carta == 3 || Num_Carta == 20)
               {
                  switch(Num_Carta)
                  {
                  case 1:
                  case 3:
                  case 20:
                     if (Idioma == "I")
                     {
                        XGCV.Printer.DefInstance.Print("Drawer");
                     }
                     else
                     {
                        XGCV.Printer.DefInstance.Print("Girador");
                     }
                     break;
                  default:
                     XGCV.Printer.DefInstance.Print("Girador");
                     break;
                  }
               }
               else if (Num_Carta == 2 || Num_Carta == 4)
               {
                  XGCV.Printer.DefInstance.Print("Girado");
               }
               else if (Num_Carta == 3)
               {
                  XGCV.Printer.DefInstance.Print("Exportador");
               }
               Pr_Salto_Pagina();
               XGCV.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].NombreUsado.TrimB());
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB((short)(tabulador)));
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var2].NombreUsado.TrimB());
               Pr_Salto_Pagina();
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, true);
               if (PartysOpe[Indice_Var1].DireccionUsado != "")
               {
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].DireccionUsado.TrimB());
               }
               else
               {
                  if (Idioma == "I")
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf("P.O.Box " + PartysOpe[Indice_Var1].CasPostal);
                  }
                  else
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf("Casilla Postal " + PartysOpe[Indice_Var1].CasPostal);
                  }
               }
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB((short)(tabulador)));
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
               // Casilla Postal para Girado.-
               if (Num_Carta == 2 || Num_Carta == 4 || Num_Carta == 6)
               {
                  if (PartysOpe[Indice_Var2].CasPostal != "")
                  {
                     if (Idioma == "I")
                     {
                        Cas = "P.O.Box " + PartysOpe[Indice_Var2].CasPostal;
                     }
                     else
                     {
                        Cas = "Casilla Postal " + PartysOpe[Indice_Var2].CasPostal;
                     }
                  }
                  XGCV.Printer.DefInstance.Print(Concatena(PartysOpe[Indice_Var2].DireccionUsado.TrimB(),Cas,""));
               }
               else
               {
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var2].DireccionUsado.TrimB());
               }
               Pr_Salto_Pagina();
               // si es de exportador  -  girado
               // se imprime ciudad de exportador, y ciudad con pais de girado
               // en caso contrario es sólo ciudad para exportador y girado
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, true);
               switch(Num_Carta)
               {
               case 2:
               case 6:
                  Paso = Concatena(PartysOpe[Indice_Var1].ComunaUsado,PartysOpe[Indice_Var1].CiudadUsado,"");
                  break;
               case 3:
                  // Paso$ = Concatena(PartysOpe(IExp1).ComunaUsado, PartysOpe(IExp1).CiudadUsado, PartysOpe(IExp1).EstadoUsado)
                  // Paso$ = Concatena(Paso$, PartysOpe(IExp1).PostalUsado, "")
                  Paso = Concatena(PartysOpe[Indice_Var1].ComunaUsado,PartysOpe[Indice_Var1].CiudadUsado,PartysOpe[Indice_Var1].EstadoUsado);
                  Paso = Concatena(Paso,PartysOpe[Indice_Var1].PostalUsado,"");
                  break;
               case 4:
                  Paso = Concatena(PartysOpe[Indice_Var1].ComunaUsado,PartysOpe[Indice_Var1].CiudadUsado,PartysOpe[Indice_Var1].PostalUsado);
                  break;
               case 14:
                  Paso = PartysOpe[Indice_Var1].CiudadUsado;
                  break;
               default:
                  Paso = Concatena(PartysOpe[Indice_Var1].ComunaUsado,PartysOpe[Indice_Var1].CiudadUsado,PartysOpe[Indice_Var1].EstadoUsado);
                  Paso = Concatena(Paso,PartysOpe[Indice_Var1].PostalUsado,"");
                  break;
               }
               XGCV.Printer.DefInstance.PrintWithoutCrlf(Paso);
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB((short)(tabulador)));
               switch(Num_Carta)
               {
               case 0:
               case 1:
               case 20:
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var2].CiudadUsado.TrimB());
                  Pr_Salto_Pagina();
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, true);
                  if (Idioma == "I")
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf(PaiEnIng(PartysOpe[Indice_Var1].PaisUsado));
                  }
                  else
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].PaisUsado.TrimB());
                  }
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB((short)(tabulador)));
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var2].PaisUsado.TrimB());
                  Pr_Salto_Pagina();
                  break;
               case 2:
               case 4:
               case 6:
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
                  Paso = Concatena(PartysOpe[Indice_Var2].ComunaUsado,PartysOpe[Indice_Var2].CiudadUsado,PartysOpe[Indice_Var2].EstadoUsado);
                  Paso = Concatena(Paso,PartysOpe[Indice_Var2].PostalUsado,"");
                  XGCV.Printer.DefInstance.Print(Paso);
                  XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB((short)(tabulador + 4)));
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var2].PaisUsado);
                  Pr_Salto_Pagina();
                  break;
               case 3:
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var2].CiudadUsado.TrimB());
                  Pr_Salto_Pagina();
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, true);
                  if (Idioma == "I")
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf(PaiEnIng(PartysOpe[Indice_Var1].PaisUsado),XGCV.Printer.TAB((short)(tabulador)));
                  }
                  else
                  {
                     XGCV.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].PaisUsado,XGCV.Printer.TAB((short)(tabulador)));
                  }
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
                  XGCV.Printer.DefInstance.Print(MODGDOC.Minuscula(PartysOpe[Indice_Var2].PaisUsado));
                  Pr_Salto_Pagina();
                  // Case 6
                  //     Printer.FontBold = False
                  //     Printer.FontItalic = False
                  //     Paso$ = Concatena(PartysOpe(Indice_Var2).ComunaUsado, PartysOpe(Indice_Var2).CiudadUsado, PartysOpe(Indice_Var2).EstadoUsado)
                  //     Paso$ = Concatena(Paso$, PartysOpe(Indice_Var2).PostalUsado, PartysOpe(Indice_Var2).PaisUsado)
                  //     Printer.Print Paso$
                  //     Call Pr_Salto_Pagina
                  break;
               }
               break;
            case 2:
               XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].NombreUsado.TrimB());
               XGCV.Printer.DefInstance.PrintWithoutCrlf("Casilla Interna Banco ");
               XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].CasBanco.TrimB());
               Pr_Salto_Pagina();
               break;
            case 3:
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, true);
               if (Idioma == "I")
               {
                  XGCV.Printer.DefInstance.Print("Messrs");
               }
               else
               {
                  XGCV.Printer.DefInstance.Print("Señores");
               }
               XGCV.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].NombreUsado.TrimB());
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB((short)(tabulador)));
               switch(Num_Carta)
               {
               case 3:
                  if (Idioma == "I")
                  {
                     XGCV.Printer.DefInstance.Print("Drawer");
                  }
                  else
                  {
                     XGCV.Printer.DefInstance.Print("Girador");
                  }
                  break;
               default:
                  XGCV.Printer.DefInstance.Print("Girado");
                  break;
               }
               Pr_Salto_Pagina();
               // Casilla Postal del Exportador + Nombre del Girado.
               if (Num_Carta == 3 && Idioma == "I")
               {
                  XGCV.Printer.DefInstance.PrintWithoutCrlf("P.O.Box ");
               }
               else
               {
                  XGCV.Printer.DefInstance.PrintWithoutCrlf("Casilla Postal ");
               }
               XGCV.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].CasPostal.TrimB());
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB((short)(tabulador)));
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var2].NombreUsado.TrimB());
               Pr_Salto_Pagina();
               // Ciudad y Estado del Exportador + Dirección del Girado.
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, true);
               // Paso$ = Concatena(PartysOpe(Indice_Var1).CiudadUsado, PartysOpe(Indice_Var1).EstadoUsado, PartysOpe(Indice_Var1).PostalUsado)
               XGCV.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].CiudadUsado);
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB((short)(tabulador)));
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
               // Casilla Postal para Girado.-
               if (Num_Carta == 2 || Num_Carta == 4)
               {
                  if (PartysOpe[Indice_Var2].CasPostal != "")
                  {
                     Cas = "Casilla Postal " + PartysOpe[Indice_Var2].CasPostal;
                  }
                  XGCV.Printer.DefInstance.Print(Concatena(PartysOpe[Indice_Var2].DireccionUsado.TrimB(),Cas,""));
               }
               else
               {
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var2].DireccionUsado.TrimB());
               }
               // Printer.Print Trim$(PartysOpe(Indice_Var2).DireccionUsado)
               Pr_Salto_Pagina();
               // País del Exportador + Comuna, Estado, Ciudad del Girado.
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, true);
               XGCV.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].PaisUsado.TrimB());
               XGCV.Printer.DefInstance.PrintWithoutCrlf(XGCV.Printer.TAB((short)(tabulador)));
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
               XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
               Paso = Concatena(PartysOpe[Indice_Var2].ComunaUsado,PartysOpe[Indice_Var2].CiudadUsado,PartysOpe[Indice_Var2].EstadoUsado);
               Paso = Concatena(Paso,PartysOpe[Indice_Var2].PostalUsado,PartysOpe[Indice_Var2].PaisUsado);
               XGCV.Printer.DefInstance.Print(Paso);
               Pr_Salto_Pagina();
               break;
            }

            XGCV.Printer.DefInstance.Print( );
            Pr_Salto_Pagina();
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
            return;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            System.Windows.Forms.MessageBox.Show(MsgErrImp + " xDoc_Cob (" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + MigrationSupport.Utils.GetErrorDescription(MigrationSupport.GlobalException.Instance.Number) +
               ")", "", MessageBoxButtons.OK);
         }
      }
      // ****************************************************************************
      //    1.  Este procedimiento está destinado para la carta con sólo el
      //        encabezado de Exportador.
      //        Carta Exportador.
      // ****************************************************************************
      public static void xDoc_Exp(int Indice_Var1)
      {
         string MsgErrImp = "";
         string Paso = "";
         try
         {

            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, true);
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, true);

            switch(PartysOpe[Indice_Var1].Enviara)
            {
            case 0:
            case 1:
               XGCV.Printer.DefInstance.Print("Señores");
               Pr_Salto_Pagina();
               XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].NombreUsado.TrimB());
               Pr_Salto_Pagina();
               XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].DireccionUsado.TrimB());
               Pr_Salto_Pagina();
               XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].CiudadUsado.TrimB());
               Pr_Salto_Pagina();
               break;
            case 2:
               XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].NombreUsado.TrimB());
               XGCV.Printer.DefInstance.PrintWithoutCrlf("Casilla Interna Banco ");
               XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].CasBanco.TrimB());
               Pr_Salto_Pagina();
               // Printer.Print "Presente"
               // Call Pr_Salto_Pagina
               break;
            case 3:
               switch(Carta)
               {
               case 9:
                  XGCV.Printer.DefInstance.Print("Señores");
                  Pr_Salto_Pagina();
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].NombreUsado.TrimB());
                  XGCV.Printer.DefInstance.PrintWithoutCrlf("Casilla Postal ");
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].CasPostal.TrimB());
                  Pr_Salto_Pagina();
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].CiudadUsado);
                  Pr_Salto_Pagina();
                  break;
               default:
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].NombreUsado.TrimB());
                  XGCV.Printer.DefInstance.PrintWithoutCrlf("Casilla Postal ");
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].CasPostal.TrimB());
                  Pr_Salto_Pagina();
                  Paso = Concatena(PartysOpe[Indice_Var1].CiudadUsado,PartysOpe[Indice_Var1].EstadoUsado,PartysOpe[Indice_Var1].PostalUsado);
                  XGCV.Printer.DefInstance.Print(Paso);
                  Pr_Salto_Pagina();
                  XGCV.Printer.DefInstance.Print(PartysOpe[Indice_Var1].PaisUsado.TrimB());
                  Pr_Salto_Pagina();
                  break;
               }
               break;
            }

            XGCV.Printer.DefInstance.Print( );
            Pr_Salto_Pagina();
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(XGCV.Printer.DefInstance.Font, false);
            XGCV.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(XGCV.Printer.DefInstance.Font, false);
            return;

         }
         catch(Exception exc)
         {
            MigrationSupport.GlobalException.Initialize(exc);
            System.Windows.Forms.MessageBox.Show(MsgErrImp + " xDoc (" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number,String.Empty) + MigrationSupport.Utils.GetErrorDescription(MigrationSupport.GlobalException.Instance.Number) + ")",
               "", MessageBoxButtons.OK);

         }
      }
   }
}
