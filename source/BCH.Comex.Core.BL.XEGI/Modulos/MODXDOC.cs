using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XEGI.Modulos
{
    public static class MODXDOC
    {
        // *************************************************************************************************
        //  se declaran estas funciones para tener acceso a las vistas MPN
        // **********************************************************************************************
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
            public string RutUsado;
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
            public string rut;   //rut_usuario
            public int Jerarquia;   //nivel de jerarquia
            public string CentroCosto;   //cent_costo_esp
            public string Especialista;   //id_especialista
            public string CCtOrig;   //Centro Costo Original.-
            public string EspOrig;   //Especialista Original.-
            public int Delegada;   //es del mismo nivel que super
            public string CostoSuper;   //jefe superior
            public string Id_Super;   //jefe superior
            public string comuna;   //comuna especialista
            public string Seccion;   //seccion_esp
            public int oficina;   //codigo de su oficina
            public string Swift;   //swift_esp
            public string Reemplazos;   //Reemplazos.-
            public string RempOrig;   //Reemplazos Usuario Original.-
            public string OfixUser;   //Oficinas que puede atender el usuario
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
        public const int IExp1 = 0;
        public const int IGir = 1;
        public const int ICob = 2;
        public const int IAge = 3;
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
        // -----------------------------------------'
        // Realsystems Nov.2008 Estab.Comex.Grp5.4'
        // -----------------------------------------'
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
        public static string Referencia = "";     // Indice la Referencia que se debe imprimir en las cartas.-ç
        public static double TipCam = 0.0;     // Tipo Cambio en Liquidación.-
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
        public static int Fn_Buscar_Indice_2(UnitOfWorkCext01 uow, string ls_cencos, string ls_codusr, string ls_rutcli, string ls_codcct)
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
                /*Que = "exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + ".sce_serv_imp_01_MS ";
                Que = Que + "'" + ls_rutcli + "', ";
                Que = Que + "'" + ls_codcct + "'";

                // Se ejecuta el Query.
                R = MODGSRM.RespuestaQuery(ref Que);*/

                var rut = ls_rutcli.TrimEnd('|').PadLeft(10, '0');
                var result = uow.SceRepository.sce_serv_imp_01_MS(rut, ls_codcct);

                // Error en el Query
                /*if (R == "-1")
                {
                    MigrationSupport.Utils.MsgBox("Error al tratar de leer los datos del ejecutivo de negocios. El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
                       MODXDOC.MsgxDoc);
                    goto SyGet_UsrEnd;
                }

                if (R == "")
                {
                    goto SyGet_UsrEnd;
                }

                n = MODGSRM.RowCount;
                Array.Resize(ref UsrEsps, largo + n + 1);
                m = UsrEsps.GetUpperBound(0);

                li_rut = MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).Str().TrimB();
                UsrEsps[m].cencos = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                UsrEsps[m].CodUsr = ls_codusr;
                UsrEsps[m].nombre = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                UsrEsps[m].Direccion = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                // UsrEsps(m%).Ciudad = Trim$(GetPosSy(NumSig(), "C", R$))
                UsrEsps[m].telefono = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).Str().TrimB();
                UsrEsps[m].Fax = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).Str().TrimB();
                */
                Array.Resize(ref UsrEsps, largo + result.Count + 1);
                m = UsrEsps.GetUpperBound(0); //TODO ARKANO OJO CON OTROS REPORTES QUE SEA MAYOR A 1
                li_rut = result[0].rutcli;
                UsrEsps[m].cencos = result[0].codcct;
                UsrEsps[m].CodUsr = ls_codusr;
                UsrEsps[m].nombre = result[0].nomejc;
                UsrEsps[m].Direccion = result[0].direjc;
                // UsrEsps(m%).Ciudad = T
                UsrEsps[m].telefono = result[0].telejc;
                UsrEsps[m].Fax = result[0].faxejc;

                Fn_Buscar_Indice_2 = m;                           
            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                //throw new XegiException("[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] ", exc);
                return m;
            }

            return Fn_Buscar_Indice_2;
        }
        // ****************************************************************************
        //    1.  Concatena tres string separados por ",".
        // ****************************************************************************
        public static string Concatena(string s1, string s2, string s3)
        {
            string Concatena = "";

            string s = "";

            s = s1;
            if(!string.IsNullOrEmpty(s2))
            {
                if (!string.IsNullOrEmpty(s))
                {
                    s = s + ", ";
                }
                s = s + s2;
            }
            if(!string.IsNullOrEmpty(s3))
            {
                if(!string.IsNullOrEmpty(s))
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
        public static int Fn_Buscar_Indice(UnitOfWorkCext01 uow, string cencos, string CodUsr)
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
            //     If (UsrEsps(i%).cencos = cencos) And (UsrEsps(i%).CodUsr = CodUsr) Then
            //         Indice% = i%
            //         Exit For
            //     End If
            // Next i%
            // --------------------------------------------------
            // RealSystems - Código Original - Termino
            // --------------------------------------------------
            // Si no se encuentra en Memoria, Busca en la Base.
            if (Indice == 0)
            {
                a = SyGet_Usr(uow, cencos, CodUsr);
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
        public static int GetDatOfi(UnitOfWorkCext01 uow, string CodCct, string oficina)
        {
            int GetDatOfi = 0;

            string R = "";
            string Que = "";
            string RutaSyb = "";

            // If Val(Oficina) = 0 Or InStr("827;829", Codcct) = 0 Then Exit Sub
            // FTF:20032001, Segun rutina JLA

            GetDatOfi = false.ToInt();

            // If Val(Oficina) = 0 Or InStr("101;107;225;290", Codcct) <> 0 Then Exit Function
            if (oficina.ToVal() == 0)
            {
                return GetDatOfi;
            }

            RutaSyb = MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + ".";

            /*Que = "";
            Que = Que + "exec " + RutaSyb + "sgt_suc_s04_MS ";
            Que = Que + MODGSYB.dbnumesy(((int)(oficina.ToVal())));

            R = MODGSRM.RespuestaQuery(ref Que);*/

            var result = uow.SceRepository.sgt_suc_s04_MS(MODGSYB.dbnumesy(((int)(oficina.ToVal()))));

            //if (result.Count == "-1")
            //{
            //    MigrationSupport.Utils.MsgBox("Se ha producido un error al leer los datos de la oficina. El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(80) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<MigrationSupport.MsgBoxStyle>(),
            //       "Servidor de Impresión");
            //    return GetDatOfi;
            //}

            //if (R != "")
            //{
                //UsrEsp.nombre = MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).Str();
                //UsrEsp.Direccion = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();
                //UsrEsp.telefono = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();
                //UsrEsp.Fax = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str();
            //}
            if (result != null && result.Count > 0)
            {
                UsrEsp.nombre = result[0].suc_succor;
                UsrEsp.Direccion = result[0].suc_sucdir;
                UsrEsp.telefono = result[0].suc_sucfon;
                UsrEsp.Fax = result[0].suc_sucfax;
                GetDatOfi = true.ToInt();
            }

            return GetDatOfi;
        }
        // ****************************************************************************
        //    1.  Retorna las lineas de una Caja Multilinea en un arreglo de lineas.
        // ****************************************************************************
        public static int GetLines(string Frase, System.Windows.Forms.Control Caja, ref string[] Lineas)
        {
            int GetLines = 0;
            //TODO: ARKANO Hay que revisar GetLines
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
                NumLines = 40;
                //NumLines = SendMessage(((int)Caja.Handle), EM_GETLINECOUNT, 0, 0);
                for (i = 0; i <= NumLines - 1; i += 1)
                {
                    x = new string(' ', 150);
                    NumChars = 150;
                    //NumChars = SendMessage(((int)Caja.Handle), EM_GETLINE, i, x);
                    n = Lineas.GetUpperBound(0) + 1;
                    Array.Resize(ref Lineas, n + 1);
                    Lineas[n] = x.TrimB();
                }

                return GetLines;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XegiException(MigrationSupport.GlobalException.Instance.Number.Str() + " en linea  ", exc);
            }            
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

            d = MigrationSupport.Utils.Format(Fecha, "d");
            m = Glosa_Mes_Espanol(MigrationSupport.Utils.Format(Fecha, "mm").ToInt());
            a = MigrationSupport.Utils.Format(Fecha, "yyyy");
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

            d = MigrationSupport.Utils.Format(Fecha, "d");
            m = Glosa_Mes_Ingles(MigrationSupport.Utils.Format(Fecha, "MM").ToInt()).Str();
            a = MigrationSupport.Utils.Format(DateTime.Now, "yyyy");
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


            switch (Mes)
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


            switch (Mes)
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

            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeName(MigrationSupport.Printer.DefInstance.Font, "Times New Roman");
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeSize(MigrationSupport.Printer.DefInstance.Font, 9.84F);
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
            //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, false);
            //n = (24 - MigrationSupport.Printer.DefInstance.CurrentY.ToVal()).ToInt();
            //for (i = 1; i <= n; i += 1)
            //{
            //    MigrationSupport.Printer.DefInstance.CurrentY = ((int)(MigrationSupport.Printer.DefInstance.CurrentY + 1));
            //}
            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(55),Pagina.Str()});

        }
        
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
            catch (Exception exc)
            {                
                throw new XegiException("Error en Traducción de País a Inglés", exc);
            }            
        }
        // ****************************************************************************
        //    1.  Detalle de las Arbitrajes.
        // ****************************************************************************
        public static void Pr_Arbitrajes(DocumentoReporteModel documento)
        {
            string Par = "";
            int i = 0;
            int n = VArb.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = VArb.GetUpperBound(0);
            //});

            for (i = 0; i < n; i += 1)
            {
                Par = MODGDOC.forma(VArb[i].PrdArb, MODXDOC.FormatoParidad);
                var divisa = new Divisa { 
                    MonedaCompra = VArb[i].NemMndC, 
                    MontoCompra = MODGDOC.forma(VArb[i].MtoCom, MODXDOC.Formato), 
                    MonedaVenta = VArb[i].NemMndV, 
                    MontoVenta = MODGDOC.forma(VArb[i].MtoVta, MODXDOC.Formato),
                    Paridades = Math.Round(VArb[i].PrdArb, 6).ToString("#0.000000"),
                };
                documento.LineasDivisa.Add(divisa);
                
                //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(98),Par.Left((Par.Len() - 4))});
                Pr_Salto_Pagina();
            }

        }
        // ****************************************************************************
        //    1.  Detalle de las Compras.
        // ****************************************************************************
        public static void Pr_Compras(DocumentoReporteModel documento)
        {
            int i = 0;
            int n = 0;

            n = Compras.GetUpperBound(0);
            for (i = 0; i <= n; i += 1)
            {
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(7).Column, Compras[i].NemMnd);
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(20).Column, MODGDOC.forma(Compras[i].MtoCVD, MODXDOC.Formato));
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(45).Column, "Tipo de Cambio $");
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(65).Column, MODGDOC.forma(Compras[i].TipCam, MODXDOC.FormatoTipCamb));
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(88).Column, "Total $");
                //TODO ARKANO   MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(98),MODGDOC.forma(Compras[i].MtpPres,MODXDOC.Formato)});
                CompraVenta cp = new CompraVenta() { 
                                MonedaOrigen = Compras[i].NemMnd,
                                Monto = MODGDOC.forma(Compras[i].MtoCVD, MODXDOC.Formato), 
                                TipoCambio = MODGDOC.forma(Compras[i].TipCam, MODXDOC.FormatoTipCamb),
                                MontoTotal = MODGDOC.forma(Compras[i].MtpPres, MODXDOC.Formato)
                };

                documento.LineasCompra.Add(cp);
            } 

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprimir el Detalle de aceptación.
        // Observaciones  : Coloca un detalle de aceptación para el caso de la
        //                  carta Nº 4 (Exportador....Girado)
        // ****************************************************************************
        public static void Pr_Detalle(DocumentoReporteModel documento)
        {
            int i = 0;
            int n = 0;

            n = VxLet.GetUpperBound(0);

            documento.DetalleAceptacion =  "Detalle de aceptación";

            // si existen letras, se imprimen estas
            if (n > 0)
            {
                for (i = 0; i <= n; i += 1)
                {
                    if (VxLet[i].CodMon != 0)
                    {

                        /*MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(new string(' ', tabulador - 15), "Letra Nro. ", MigrationSupport.FileSystem.TAB((short)(tab_doc_letra - 5)).Column, VxLet[i].CodMon.Str().TrimB());
                        MigrationSupport.Printer.DefInstance.PrintList(new object[]{" ",MigrationSupport.FileSystem.TAB((short)(tab_doc_cod - 6)),VxLet[i].CodMon_t.TrimB() + " ",MigrationSupport.FileSystem.TAB((short)(tab_doc_nem - 5)),MODGDOC.forma(VxLet[i].MtoLet,
                        MODXDOC.Formato),MigrationSupport.FileSystem.TAB((short)(tab_doc_monto - 6))," Vence; " + MigrationSupport.Utils.Format(VxLet[i].FecVen,"dd/mm/yyyy")});*/
                        var letra = new Letra();
                        letra.LetraNro = "Letra Nro. " + VxLet[i].CodMon.Str().TrimB();
                        letra.Monto = VxLet[i].CodMon_t.TrimB() + " " + MODGDOC.forma(VxLet[i].MtoLet, MODXDOC.Formato);
                        letra.Vence = " Vence; " + MigrationSupport.Utils.Format(VxLet[i].FecVen, "dd/mm/yyyy");
                        documento.Letras.Add(letra);
                        Pr_Salto_Pagina();
                    }
                }
            }

        }
        // ****************************************************************************
        //    1.  Despliega n distribución que se efectuará por un determinado pago.
        // ****************************************************************************
        public static void Pr_Distribucion(DocumentoReporteModel documento)
        {
            int i = 0;
            int n = VDist.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = VDist.GetUpperBound(0);
            //});

            //TODO ARKANO   MigrationSupport.Printer.DefInstance.PrintList(new object[]{"Beneficiario",MigrationSupport.FileSystem.TAB(30),"Vía de la Distribución",MigrationSupport.FileSystem.TAB(60),"Concepto",MigrationSupport.FileSystem.TAB(104),"Monto"});
            Pr_Salto_Pagina();

            for (i = 0; i < n; i += 1)
            {
                var remesa = new Remesa { Beneficiario = MODGDOC.Minuscula(VDist[i].DisBen).TrimB().Left(22), ViaRemesa = MODGDOC.Minuscula(VDist[i].DisVia).TrimB(), Concepto = MODGDOC.Minuscula(VDist[i].DisCon).TrimB() + " " + VDist[i].DisMon.TrimB(),Moneda=VDist[i].DisMon.TrimB(), Monto = MODGDOC.forma(VDist[i].DisMto, MODXDOC.Formato) };
                documento.LineasRemesa.Add(remesa);
                documento.MontoCapital = remesa.Monto + " " + "Capital";
                documento.MonedaCapital = remesa.Moneda;
            }
        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprimir los Documentos y letras.
        // Observaciones  : Coloca los documentos y las letras correspondientes
        //                  para el caso de las cartas Nºs 1,2,3,4 (Cobrador..Girador,
        //                  Agente....Exportador, Exportador....Girado)
        // ****************************************************************************
        public static void Pr_Documentos(DocumentoReporteModel documento, int Carta_Aux)
        {
            string Paso = "";
            int i = 0;
            int m = VxDem.Length;
            int n = VxLet.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = VxLet.GetUpperBound(0);
            //    m = VxDem.GetUpperBound(0);
            //});

            switch (Carta_Aux)
            {
                case 1:
                case 3:
                case 20:
                    if (Idioma == "I")
                    {
                        //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(tab_titulos)),"Documents"});
                    }
                    else
                    {
                        //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(tab_titulos)),"Documentos"});
                    }
                    break;
                default:
                    //TODO ARKANO   MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(tab_titulos)),"Documentos"});
                    break;
            }

            // si existen letras, se imprimen estas
            if (n > 0)
            {
                switch (Carta_Aux)
                {
                    case 1:
                    case 3:
                    case 20:
                        if (Idioma == "I")
                        {
                            for (i = 0; i < n; i += 1)
                            {
                                if (VxLet[i].CodMon != 0)
                                {
                                    var letra = new Letra { 
                                        Glosa = "Draft Nr. ", 
                                        CodigoMoneda = MigrationSupport.Utils.Format(VxLet[i].CodMon.Str().TrimB(), "00"), 
                                        Moneda = VxLet[i].CodMon_t.TrimB(), 
                                        Monto = MODGDOC.forma(VxLet[i].MtoLet, MODXDOC.Formato),
                                        Vence = " Maturity " + MigrationSupport.Utils.Format(VxLet[i].FecVen, "mm/dd/yyyy") 
                                    };
                                    documento.Letras.Add(letra);
                                    //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(new string(' ', tabulador - 23), "Draft Nr. ", MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)).Column, MigrationSupport.Utils.Format(VxLet[i].CodMon.Str().TrimB(), "00"));
                                    //TODO ARKANO   MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(tab_mto_descr)),VxLet[i].CodMon_t.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(VxLet[i].MtoLet,MODXDOC.Formato),
                                    //TODO ARKANO     MigrationSupport.FileSystem.TAB(89)," Maturity " + MigrationSupport.Utils.Format(VxLet[i].FecVen,"mm/dd/yyyy")});
                                    Pr_Salto_Pagina();
                                }
                            }
                        }
                        else
                        {
                            for (i = 0; i < n; i += 1)
                            {
                                if (VxLet[i].CodMon != 0)
                                {
                                    var letra = new Letra
                                    {
                                        Glosa = "Letra Nro. ",
                                        CodigoMoneda = MigrationSupport.Utils.Format(VxLet[i].CodMon.Str().TrimB(), "00"),
                                        Moneda = VxLet[i].CodMon_t.TrimB(),
                                        Monto = MODGDOC.forma(VxLet[i].MtoLet, MODXDOC.Formato),
                                        Vence = " Vence; " + MigrationSupport.Utils.Format(VxLet[i].FecVen, "mm/dd/yyyy")
                                    };
                                    documento.Letras.Add(letra);
                                    //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(new string(' ', tabulador - 23), "Letra Nro. ", MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)).Column, MigrationSupport.Utils.Format(VxLet[i].CodMon.Str().TrimB(), "00"));
                                    //TODO ARKANO     MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(tab_mto_descr)),VxLet[i].CodMon_t.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(VxLet[i].MtoLet,MODXDOC.Formato),
                                    //TODO ARKANO       MigrationSupport.FileSystem.TAB(89)," Vence; " + MigrationSupport.Utils.Format(VxLet[i].FecVen,"dd/mm/yyyy")});
                                    Pr_Salto_Pagina();
                                }
                            }
                        }
                        break;
                    default:
                        for (i = 0; i < n; i += 1)
                        {
                            if (VxLet[i].CodMon != 0)
                            {
                                var letra = new Letra
                                {
                                    Glosa = "Letra Nro. ",
                                    CodigoMoneda = MigrationSupport.Utils.Format(VxLet[i].CodMon.Str().TrimB(), "00"),
                                    Moneda = VxLet[i].CodMon_t.TrimB(),
                                    Monto = MODGDOC.forma(VxLet[i].MtoLet, MODXDOC.Formato),
                                    Vence = " Vence; " + MigrationSupport.Utils.Format(VxLet[i].FecVen, "mm/dd/yyyy")
                                };
                                documento.Letras.Add(letra);
                                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(new string(' ', tabulador - 23), "Letra Nro. ", MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)).Column, MigrationSupport.Utils.Format(VxLet[i].CodMon.Str().TrimB(), "00"));
                                //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(tab_mto_descr)),VxLet[i].CodMon_t.TrimB() + " ",MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(VxLet[i].MtoLet,MODXDOC.Formato),
                                //TODO ARKANO    MigrationSupport.FileSystem.TAB(89)," Vence; " + MigrationSupport.Utils.Format(VxLet[i].FecVen,"dd/mm/yyyy")});
                                Pr_Salto_Pagina();
                            }
                        }
                        break;
                }
            }

            // si existen documentos se imprimen dichos documentos
            if (m > 0)
            {
                for (i = 0; i < m; i += 1)
                {
                    if(!string.IsNullOrEmpty(VxDem[i].NomDem.TrimB()))
                    {
                        var embarque = new DocumentoEmbarque();
                        switch (Carta_Aux)
                        {
                            case 1:
                            case 3:
                                if (Idioma == "I")
                                {
                                    
                                    Paso = MODGDOC.CopiarDeString(VxDem[i].NomDem, ";", 2).TrimB();
                                    Paso = Paso.Mid(1, 30);
                                    embarque.Nombre = Paso;
                                    embarque.NumeroEmbarque = VxDem[i].NumDem_t.TrimB();
                                    //TODO ARKANO     MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),Paso,MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4 + 20)),VxDem[i].NumDem_t.TrimB()});
                                    Pr_Salto_Pagina();
                                }
                                else
                                {
                                    Paso = MODGDOC.CopiarDeString(VxDem[i].NomDem, ";", 1).TrimB();
                                    Paso = Paso.Mid(1, 30);
                                    embarque.Nombre = Paso;
                                    embarque.NumeroEmbarque = VxDem[i].NumDem_t.TrimB();
                                    //TODO ARKANO     MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),Paso,MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4 + 20)),VxDem[i].NumDem_t.TrimB()});
                                    Pr_Salto_Pagina();
                                }
                                break;
                            default:
                                Paso = MODGDOC.CopiarDeString(VxDem[i].NomDem, ";", 1).TrimB();
                                Paso = Paso.Mid(1, 30);
                                embarque.Nombre = Paso;
                                embarque.NumeroEmbarque = VxDem[i].NumDem_t.TrimB();
                                //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),Paso,MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4 + 20)),VxDem[i].NumDem_t.TrimB()});
                                Pr_Salto_Pagina();
                                break;
                        }
                        documento.LineasDocumentosEmbarque.Add(embarque);
                    }
                }
            }

        }
        // ****************************************************************************
        //    1.  Imprime el Píe de Página de la carta Nº 620 en donde se coloca el
        //        nombre del Usuario Especialista.
        // ****************************************************************************'
        public static void Pr_Especialista(DocumentoReporteModel documento, UnitOfWorkCext01 uow)
        {
            int i = 0;
            int a = 0;

            a = Fn_Buscar_Indice(uow, UsrEsp.cencos, UsrEsp.CodUsr);
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
            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(80),UsrEsp.nombre.TrimB()});

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

            switch (Carta_Aux)
            {
                case 0:
                case 1:
                    if (Carta_Aux == 0)
                    {
                        if(!string.IsNullOrEmpty(VInsEsp.InsCob.TrimB()))
                        {
                            // Orieta.
                            // Orieta.
                            //MigrationSupport.Printer.DefInstance.Print("Instrucciones al Banco Cobrador");
                        }
                    }
                    break;
                case 2:
                case 4:
                    if (Carta_Aux == 2)
                    {
                        if(!string.IsNullOrEmpty(VInsEsp.InsExp.TrimB()))
                        {
                            // Printer.Print Tab(45);
                            // Printer.Print "Información al Exportador"
                        }
                        else
                        {
                            return;
                        }
                    }
                    else if (Carta_Aux == 4)
                    {
                        if(!string.IsNullOrEmpty(VInsEsp.InsExp.TrimB()))
                        {
                            // Printer.Print Tab(45);
                            // Printer.Print "Información al Exportador"
                        }
                        else
                        {
                            return;
                        }
                    }
                    break;
                case 3:
                    if(!string.IsNullOrEmpty(VInsEsp.InsAge.TrimB()))
                    {
                        // Orieta.
                        // Orieta.
                        // Printer.Print Tab(45);
                        // Printer.Print "Información al Agente"
                    }
                    else
                    {
                        return;
                    }
                    break;
            }

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
            PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumIni()).TrimB();
            PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            PartysOpe[MODXDOC.IGir].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].ComunaUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Letras de Exportación
            n = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToVal();
            VxLet = new T_xLet[(n + 1).ToInt()];
            if (n > 0)
            {
                for (i = 1; i <= VxLet.GetUpperBound(0); i += 1)
                {
                    VxLet[i].CodMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
                    VxLet[i].MtoLet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                    VxLet[i].FecVen = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxLet[i].CodMon_t = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            // Referencias
            VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VxCob.RefBcoC = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Instrucciones Especiales de Frases Estándares
            VInsEsp.InsExp = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Atributos del Usuario
            UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

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
            Idioma = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumIni()).TrimB();
            PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            PartysOpe[MODXDOC.IGir].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].ComunaUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].telefono = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].Telex = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            PartysOpe[MODXDOC.ICob].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Partys Opcional
            e = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            if (e != 0)
            {
                Array.Resize(ref PartysOpe, MODXDOC.IAge);
                PartysOpe[MODXDOC.IAge].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            }

            // Condición de Pago.
            VxCob.Condicion = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Mercadería.
            VxCob.Mercad_t = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VxCob.Mercad_t = MODGDOC.Componer(VxCob.Mercad_t, "ç", 9.Char());

            // Documentos de Embarque
            n = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToVal();
            VxDem = new T_xDem[(n).ToInt()];
            if (n > 0)
            {
                for (i = 0; i < VxDem.Length; i += 1)
                {
                    VxDem[i].NomDem = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxDem[i].NumDem_t = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            // Conocimientos de Embarque
            n = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToVal();
            VxCem = new T_xCem[(n).ToInt()];
            if (n > 0)
            {
                for (i = 0; i < VxCem.Length; i += 1)
                {
                    VxCem[i].NumCem = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxCem[i].FecCem = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxCem[i].EmbDes = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxCem[i].EmbHac = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            // Letras de Exportación
            n = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToVal();
            VxLet = new T_xLet[(n).ToInt()];
            if (n > 0)
            {
                for (i = 0; i < VxLet.Length; i += 1)
                {
                    VxLet[i].CodMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
                    VxLet[i].MtoLet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".",",").ToVal();
                    VxLet[i].FecVen = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxLet[i].CodMon_t = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            // Referencias
            VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VxCob.RefBcoC = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Cobranza de Exportaciones (Moneda y Montos)
            VxCob.Nemonic = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VxCob.CodMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VxCob.MtoCob = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
            VxCob.MtoInt = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
            VxCob.Cedente1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToVal();
            VxCob.Cedente2 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToVal();

            // Vencimientos
            n = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToVal();
            Vencimientos = new T_Ven[(n).ToInt()];
            if (n > 0)
            {
                for (i = 0; i < Vencimientos.Length; i += 1)
                {
                    Vencimientos[i].CodMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    Vencimientos[i].TotVen = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                    Vencimientos[i].FecVen = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            // Abonos (Débitos)
            CuantosDeb = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VDet = new T_Det[CuantosDeb];
            if (CuantosDeb > 0)
            {
                for (i = 0; i < VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].tipo = "D";
                }
            }

            // Cargos (Comisiones)
            CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    m = VDet.GetUpperBound(0);
            //});
            m = VDet.Length;
            if (CuantosCom > 0)
            {
                Array.Resize(ref VDet, m + CuantosCom + 1);
                for (i = m; i < VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].tipo = "C";
                }
                MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            }

            // Atributos del Usuario
            UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Instrucciones Especiales de Frases Estándares
            VInsEsp.Frases = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInsEsp.InsCob = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInsEsp.InsExp = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInsEsp.InsAge = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInsEsp.InsCob = MODGDOC.Componer(VInsEsp.InsCob, "ç", 9.Char());
            VInsEsp.InsExp = MODGDOC.Componer(VInsEsp.InsExp, "ç", 9.Char());
            VInsEsp.InsAge = MODGDOC.Componer(VInsEsp.InsAge, "ç", 9.Char());

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
            Idioma = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumIni()).TrimB();
            PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            PartysOpe[MODXDOC.IGir].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].ComunaUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            PartysOpe[MODXDOC.ICob].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.ICob].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Partys Opcional
            e = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            if (e != 0)
            {
                Array.Resize(ref PartysOpe, MODXDOC.IAge + 1);
                PartysOpe[MODXDOC.IAge].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                PartysOpe[MODXDOC.IAge].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            }

            // Condición de Pago.
            VxCob.Condicion = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Documentos de Embarque
            n = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToVal();
            VxDem = new T_xDem[(n + 1).ToInt()];
            if (n > 0)
            {
                for (i = 1; i <= VxDem.GetUpperBound(0); i += 1)
                {
                    VxDem[i].NomDem = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxDem[i].NumDem_t = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            // Letras de Exportación
            n = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToVal();
            VxLet = new T_xLet[(n + 1).ToInt()];
            if (n > 0)
            {
                for (i = 1; i <= VxLet.GetUpperBound(0); i += 1)
                {
                    VxLet[i].CodMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
                    VxLet[i].MtoLet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToVal();
                    VxLet[i].FecVen = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxLet[i].CodMon_t = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            // Referencias
            VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VxCob.RefBcoC = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Fecha de Ingreso.-
            VxCob.FecIng = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Atributos del Usuario
            UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Instrucciones Especiales de Frases Estándares
            VInsEsp.Frases = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInsEsp.InsCob = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

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
            PartysOpe[0].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumIni()).TrimB();
            PartysOpe[0].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Operacion Relacionada.-
            VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Referencias.
            VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Referencia Cliente.-
            VOperac.RefCli = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Cuantas Compras.
            CuantasCompras = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            // Cuantas Ventas.
            CuantasVentas = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            //Compras = new T_VgPli[CuantasCompras + 1];
            //Ventas = new T_VgPli[CuantasVentas + 1];
            Compras = new T_VgPli[CuantasCompras];
            Ventas = new T_VgPli[CuantasVentas];

            if (CuantasVentas > 0)
            {
                for (i = 0; i < Ventas.Length; i += 1)
                {
                    Ventas[i].NemMnd = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    Ventas[i].MtoCVD = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                    Ventas[i].TipCam = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                    Ventas[i].MtpPres = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                }
            }
            if (CuantasCompras > 0)
            {
                for (i = 0; i < Compras.Length; i += 1)
                {
                    Compras[i].NemMnd = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    Compras[i].MtoCVD = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                    Compras[i].TipCam = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".",",").ToVal();
                    Compras[i].MtpPres = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                }
            }

            // Cuantos Remesa.
            CuantasRemesa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VxRemesa = new T_Via[CuantasRemesa];
            if (CuantasRemesa > 0)
            {
                for (i = 0; i < VxRemesa.Length; i += 1)
                {
                    VxRemesa[i].NomBen = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxRemesa[i].NomVia = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxRemesa[i].NemMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxRemesa[i].MtoTot = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                }
            }

            // Cuantas Vías.
            CuantasVias = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VxVia = new T_Via1[CuantasVias];
            if (CuantasVias > 0)
            {
                for (i = 0; i < VxVia.Length; i += 1)
                {
                    VxVia[i].Descri = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxVia[i].NomVia = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxVia[i].NemMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxVia[i].MtoTot = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                }
            }

            // Cuantos Orígenes.
            CuantasOri = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VxOri = new T_Ori[CuantasOri];
            if (CuantasOri > 0)
            {
                for (i = 0; i < VxOri.Length; i += 1)
                {
                    VxOri[i].Descri = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxOri[i].NomOri = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxOri[i].NemMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxOri[i].MtoTot = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                }
            }

            // Cargos (Comisiones)
            CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VDet = new T_Det[CuantosCom];
            if (CuantosCom > 0)
            {
                for (i = 0; i < VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",");
                    VDet[i].tipo = "C";
                }
                MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",");
                MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",");
            }

            // Instrucciones Especiales.-
            VInsEsp.InsExp = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Atributos del Usuario.
            UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            CuantosPlanilla = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VPlan = new T_Plan[CuantosPlanilla];
            if (CuantosPlanilla > 0)
            {
                for (i = 0; i < VPlan.Length; i += 1)
                {
                    VPlan[i].NroPlan = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }


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
            VxVia = new T_Via1[0];

            PartysOpe = new PartyKey[2];
            PartysOpe[0].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumIni()).TrimB();
            PartysOpe[0].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Operacion Relacionada.-
            VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Referencias.
            VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Referencia Cliente.-
            VOperac.RefCli = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Cuantas Compras.
            ContadorArbitraje = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            VArb = new T_VArb[ContadorArbitraje];
            if (ContadorArbitraje > 0)
            {
                for (i = 0; i < VArb.Length; i += 1)
                {
                    VArb[i].NemMndC = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VArb[i].MtoCom = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                    VArb[i].NemMndV = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VArb[i].MtoVta = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                    VArb[i].PrdArb = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                }
            }

            // Cuantos Remesa.
            CuantasRemesa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VxRemesa = new T_Via[CuantasRemesa];
            if (CuantasRemesa > 0)
            {
                for (i = 0; i < VxRemesa.Length; i += 1)
                {
                    VxRemesa[i].NomBen = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxRemesa[i].NomVia = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxRemesa[i].NemMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxRemesa[i].MtoTot = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                }
            }

            // Cuantos Orígenes.
            CuantasOri = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VxOri = new T_Ori[CuantasOri];
            if (CuantasOri > 0)
            {
                for (i = 0; i < VxOri.Length; i += 1)
                {
                    VxOri[i].Descri = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxOri[i].NomOri = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxOri[i].NemMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VxOri[i].MtoTot = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                }
            }

            // Cargos (Comisiones)
            CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VDet = new T_Det[CuantosCom];
            if (CuantosCom > 0)
            {
                for (i = 0; i < VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].tipo = "C";
                }
                MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            }

            // Instrucciones Especiales.-
            VInsEsp.InsExp = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Atributos del Usuario.
            UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            CuantosPlanilla = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VPlan = new T_Plan[CuantosPlanilla];
            if (CuantosPlanilla > 0)
            {
                for (i = 0; i < VPlan.Length; i += 1)
                {
                    VPlan[i].NroPlan = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

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
            s = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumIni()).TrimB();
            VOperac.SinRaya = s;
            VOperac.ConRaya = s.Mid(1, 3) + "-" + s.Mid(4, 2) + "-" + s.Mid(6, 2) + "-" + s.Mid(8, 3) + "-" + s.Mid(11, 5);
            PartysOpe[0].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[0].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Tipo de Aviso (D)ébito (C)rédito.
            TipoDC = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Nro. Cta.Cte.
            NroCtaCte = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Concepto
            Concepto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Arreglo de Montos
            CuantosMontos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
            VMontos = new T_Montos[(CuantosMontos).ToInt()];
            if (CuantosMontos > 0)
            {
                for (i = 0; i <= VMontos.GetUpperBound(0); i += 1)
                {
                    VMontos[i].NemMnd = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VMontos[i].Montos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
                }
            }
            Referencia = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            if (Referencia == "")
            {
                Referencia = VOperac.ConRaya;
            }

            // Atributos del Usuario.
            UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

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
            PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumIni()).TrimB();
            PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Referencias.
            VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Débitos
            CuantosDeb = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VDet = new T_Det[CuantosDeb];
            if (CuantosDeb > 0)
            {
                for (i = 0; i < VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].tipo = "D";
                }
            }

            // Comisiones
            CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = VDet.GetUpperBound(0);
            //});
            n = VDet.Length;
            if (CuantosCom > 0)
            {
                Array.Resize(ref VDet, n + CuantosCom);
                for (i = n; i < VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].tipo = "C";
                }
                MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            }

            // Instrucciones al Exportador.
            Instrucciones = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Atributos del Usuario.
            UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

        }
        // ****************************************************************************
        //    1.  Se cargan los datos en variables y arreglos que luegos serán
        //        llevadas a papel para imprimir la carta de Exportador Nro. 611.
        // ****************************************************************************
        public static void Pr_Load_Exp611()
        {
            int n = 0;
            int CuantosCom = 0;
            int CuantosDeb = 0;
            int CuantosPlanilla = 0;
            int i = 0;
            int CuantosDist = 0;

            PartysOpe = new PartyKey[MODXDOC.IGir + 1];
            CobRet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumIni()).TrimB();
            PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            PartysOpe[MODXDOC.IGir].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].ComunaUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IGir].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Montos.
            Moneda = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            MtoCap = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
            MtoInt = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();

            // Referencias.
            VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Total/Parcial.
            Total_Parcial = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Tipo de Pago y a Qué
            // TipoPago = Trim$(CopiarDeString(Trim$(MeMo), Chr$(9), NumSig()))
            // PagoDe = Trim$(CopiarDeString(Trim$(MeMo), Chr$(9), NumSig()))
            // 
            // Arreglo de Distribución
            CuantosDist = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VDist = new T_Dist[CuantosDist];
            if (CuantosDist > 0)
            {
                for (i = 0; i < VDist.Length; i += 1)
                {
                    VDist[i].DisBen = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDist[i].DisVia = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDist[i].DisCon = MODGDOC.Minuscula(MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB());
                    VDist[i].DisMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDist[i].DisMto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            FraseVa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Información General.
            VInf.InfMon1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInf.InfMto1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInf.InfMon2 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInf.InfMto2 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInf.InfMon3 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInf.InfMto3 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Frase de Gastos en el Exterior.-
            VInsEsp.Frases = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Instrucciones al Exportador.
            Instrucciones = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Arreglo de Distribución
            CuantosPlanilla = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VPlan = new T_Plan[CuantosPlanilla];
            if (CuantosPlanilla > 0)
            {
                for (i = 0; i < VPlan.Length; i += 1)
                {
                    VPlan[i].NroPlan = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].NroCod = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].NroDecl = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].PlaMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].PlaMto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            // Abonos (Débitos)
            CuantosDeb = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VDet = new T_Det[CuantosDeb];
            if (CuantosDeb > 0)
            {
                for (i = 0; i < VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].tipo = "D";
                }
            }

            // Cargos (Comisiones)
            CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            n = VDet.Length;
            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = VDet.GetUpperBound(0);
            //});
            if (CuantosCom > 0)
            {
                Array.Resize(ref VDet, n + CuantosCom);
                for (i = n; i <= VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].tipo = "C";
                }
                MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            }

            // Referencia.-
            // If VOpe.TipoDoc = DocxCanRet Then
            Referencia = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            // Else
            //     Referencia = VOpe.NumOpe_t
            // End If
            // 
            // Atributos del Usuario.
            UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            TipCam = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB().Replace(".", ",").ToVal();

        }
        // ****************************************************************************
        //    1.  Se cargan los datos en variables y arreglos que luegos serán
        //        llevadas a papel para imprimir la carta de Exportador Nro. 612.
        // ****************************************************************************
        public static void Pr_Load_Exp612()
        {

            PartysOpe = new PartyKey[MODXDOC.IGir];

            PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumIni()).TrimB();
            PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Referencias.
            VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VxCob.RefExp2 = "";

            VDetalle = new T_Detalle[1];
            VDetalle[0].Orden = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VDetalle[0].Refer = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VDetalle[0].Moneda = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VDetalle[0].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Instrucciones al Exportador.
            Instrucciones = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Referencia.-
            Referencia = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            if (Referencia == "")
            {
                Referencia = VOpe.NumOpe_t;
            }

            // Atributos del Usuario.
            UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

        }
        // ****************************************************************************
        //    1.  Se cargan los datos en variables y arreglos que luegos serán
        //        llevadas a papel para imprimir la carta de Exportador Nro. 613.
        // ****************************************************************************
        public static void Pr_Load_Exp613()
        {
            int CuantosCom = 0;
            int CuantosDeb = 0;
            int i = 0;
            int CuantosPlanilla = 0;

            PartysOpe = new PartyKey[MODXDOC.IExp1 + 1];
            PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumIni()).TrimB();
            PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Operacion Relacionada.-
            VOperac.ConRaya = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Referencias.
            VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Referencia Cliente.-
            VOperac.RefCli = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Arreglo de Distribución
            CuantosPlanilla = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VPlan = new T_Plan[CuantosPlanilla];
            if (CuantosPlanilla > 0)
            {
                for (i = 0; i < VPlan.Length; i += 1)
                {
                    VPlan[i].NroPlan = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].NroCod = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].NroDecl = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].PlaMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].PlaMto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            // Abonos (Débitos)
            CuantosDeb = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VDet = new T_Det[CuantosDeb];
            if (CuantosDeb > 0)
            {
                for (i = 0; i < VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].tipo = "D";
                }
            }

            // Cargos (Comisiones)
            CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VDet = new T_Det[CuantosCom];
            if (CuantosCom > 0)
            {
                for (i = 0; i < VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].tipo = "C";
                }
                MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            }

            // Instrucciones al Exportador.
            VInsEsp.InsExp = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Atributos del Usuario.
            UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            TipCam = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();

        }
        // ****************************************************************************
        //    1.  Se cargan los datos en variables y arreglos que luegos serán
        //        llevadas a papel para imprimir la carta de Exportador Nro. 611.
        // ****************************************************************************
        public static void Pr_Load_Exp614()
        {
            int n = 0;
            int CuantosCom = 0;
            int CuantosDeb = 0;
            int CuantosPlanilla = 0;
            int i = 0;
            int CuantosDist = 0;

            PartysOpe = new PartyKey[MODXDOC.IGir + 1];
            CobRet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumIni()).TrimB();
            PartysOpe[MODXDOC.IExp1].NombreUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].DireccionUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CiudadUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].EstadoUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PostalUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].PaisUsado = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Fax = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasBanco = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].CasPostal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            PartysOpe[MODXDOC.IExp1].Enviara = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Montos.
            Moneda = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            MtoCap = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();
            MtoInt = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).Replace(".", ",").ToVal();

            // Referencias Exportador.
            VxCob.RefExp1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Referencias Ordenante.
            VxCob.RefExp2 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Total/Parcial.
            Total_Parcial = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            SaldoRet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Tipo de Pago y a Qué
            // TipoPago = Trim$(CopiarDeString(Trim$(MeMo), Chr$(9), NumSig()))
            // PagoDe = Trim$(CopiarDeString(Trim$(MeMo), Chr$(9), NumSig()))
            // 
            // Arreglo de Distribución
            CuantosDist = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VDist = new T_Dist[CuantosDist];
            if (CuantosDist > 0)
            {
                for (i = 0; i < VDist.Length; i += 1)
                {
                    VDist[i].DisBen = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDist[i].DisVia = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDist[i].DisCon = MODGDOC.Minuscula(MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB());
                    VDist[i].DisMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDist[i].DisMto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            FraseVa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            // Información General.
            VInf.InfMon1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInf.InfMto1 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInf.InfMon2 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInf.InfMto2 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInf.InfMon3 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            VInf.InfMto3 = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Frase de Gastos en el Exterior.-
            VInsEsp.Frases = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Instrucciones al Exportador.
            Instrucciones = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();

            // Arreglo de Distribución
            CuantosPlanilla = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VPlan = new T_Plan[CuantosPlanilla];
            if (CuantosPlanilla > 0)
            {
                for (i = 0; i < VPlan.Length; i += 1)
                {
                    VPlan[i].NroPlan = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].NroCod = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].NroDecl = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].PlaMon = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VPlan[i].PlaMto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                }
            }

            // Abonos (Débitos)
            CuantosDeb = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();
            VDet = new T_Det[CuantosDeb];
            if (CuantosDeb > 0)
            {
                for (i = 0; i < VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].tipo = "D";
                }
            }

            // Cargos (Comisiones)
            CuantosCom = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).ToInt();

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = VDet.GetUpperBound(0);
            //});
            n = VDet.Length;
            if (CuantosCom > 0)
            {
                Array.Resize(ref VDet, n + CuantosCom);
                for (i = n; i < VDet.Length; i += 1)
                {
                    VDet[i].Glosa = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].MonDet = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].Monto = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                    VDet[i].tipo = "C";
                }
                MonTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
                MtoTotal = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            }

            // Referencia.-
            Referencia = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            if (Referencia == "")
            {
                Referencia = VOpe.NumOpe_t;
            }

            // Atributos del Usuario.
            UsrEsp.cencos = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            UsrEsp.CodUsr = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB();
            TipCam = MODGDOC.CopiarDeString(Memo.TrimB(), 9.Char(), MODGSYB.NumSig()).TrimB().Replace(".", ",").ToVal();

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprimir los Montos.
        // Observaciones  : Coloca los Montos correspondientes para el caso de
        //                  las cartas Nºs 2,3,4 (Agente...........Exportador,
        //                  Exportador...........Girado)
        // ****************************************************************************
        public static void Pr_Montos(DocumentoReporteModel documento, int Carta_Aux)
        {
            int i = 0;
            double Total = 0.0;
            int n = Vencimientos.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = Vencimientos.GetUpperBound(0);
            //});

            // Printer.Print : Call Pr_Salto_Pagina
            switch (Carta_Aux)
            {
                case 1:
                case 3:
                    if (Idioma == "I")
                    {
                        documento.Idioma = Idioma;
                        //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(45),"Amounts"});
                        //service.Document.Add(new Phrase("Amounts", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));//TODO ARKANO UNDERLINE
                    }
                    else
                    {
                        documento.Idioma = Idioma;
                        //TODO ARKANO    MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(45),"Montos"});
                    }
                    break;
                default:
                    //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(45),"Montos"});
                    break;
            }

            // si existen datos, se imprimen los montos
            if(!string.IsNullOrEmpty(VxCob.Nemonic.TrimB()))
            {
                switch (Carta_Aux)
                {
                    case 1:
                        if (Idioma == "I")
                        {
                            //service.Document.Add(new Phrase("Amount", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
                            var monto = new MontoRegistro { Glosa = "Amount", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.MtoCob, MODXDOC.Formato) };
                            documento.LineasMontoRegistro.Add(monto);
                            //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Amount",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(
                            //TODO ARKANO  VxCob.MtoCob,MODXDOC.Formato)});
                            Pr_Salto_Pagina();
                            if (VxCob.MtoInt > 0)
                            {
                                var montoInterest = new MontoRegistro { Glosa = "Drawer´s Interest    ", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.MtoInt, MODXDOC.Formato) };
                                documento.LineasMontoRegistro.Add(montoInterest);
                                //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Drawer´s Interest    ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),
                                //TODO ARKANO   MODGDOC.forma(VxCob.MtoInt,MODXDOC.Formato)});
                                Pr_Salto_Pagina();
                            }
                            if (VxCob.Cedente1 > 0)
                            {
                                var montoCharges = new MontoRegistro { Glosa = "Our Charges        ", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.Cedente1, MODXDOC.Formato) };
                                documento.LineasMontoRegistro.Add(montoCharges);
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Our Charges        ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),
                                //TODO ARKANO  MODGDOC.forma(VxCob.Cedente1,MODXDOC.Formato)});
                                Pr_Salto_Pagina();
                            }
                            Total = VxCob.MtoCob + VxCob.MtoInt + VxCob.Cedente1;
                            if (Total > VxCob.MtoCob)
                            {
                                documento.MonedaTotalMontos = VxCob.Nemonic.TrimB();
                                documento.TotalMontos = MODGDOC.forma(Total, MODXDOC.Formato);
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Total ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 7)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto - 3)),MODGDOC.forma
                                //TODO ARKANO (Total,MODXDOC.Formato)});
                            }
                        }
                        else
                        {
                            var monto = new MontoRegistro { Glosa = "Monto", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.MtoCob, MODXDOC.Formato) };
                            documento.LineasMontoRegistro.Add(monto);
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Monto ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(
                            //TODO ARKANO   VxCob.MtoCob,MODXDOC.Formato)});
                            Pr_Salto_Pagina();
                            if (VxCob.MtoInt > 0)
                            {
                                var montoCharges = new MontoRegistro { Glosa = "Interes Proveedor ", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.MtoInt, MODXDOC.Formato) };
                                documento.LineasMontoRegistro.Add(montoCharges);
                                //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Interes Proveedor ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),
                                //TODO ARKANO   MODGDOC.forma(VxCob.MtoInt,MODXDOC.Formato)});
                                Pr_Salto_Pagina();
                            }
                            if (VxCob.Cedente1 > 0)
                            {
                                var montoCharges = new MontoRegistro { Glosa = "Nuestros Gastos ", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.Cedente1, MODXDOC.Formato) };
                                documento.LineasMontoRegistro.Add(montoCharges);
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Nuestros Gastos ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),
                                //TODO ARKANO    MODGDOC.forma(VxCob.Cedente1,MODXDOC.Formato)});
                                Pr_Salto_Pagina();
                            }
                            Total = VxCob.MtoCob + VxCob.MtoInt + VxCob.Cedente1;
                            if (Total > VxCob.MtoCob)
                            {
                                documento.MonedaTotalMontos = VxCob.Nemonic.TrimB();
                                documento.TotalMontos = MODGDOC.forma(Total, MODXDOC.Formato);
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Total ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 7)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto - 3)),MODGDOC.forma
                                //TODO ARKANO  (Total,MODXDOC.Formato)});
                            }
                        }
                        break;
                    case 3:
                        switch (Idioma)
                        {
                            case "E":
                                var monto = new MontoRegistro { Glosa = "Monto", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.MtoCob, MODXDOC.Formato) };
                                documento.LineasMontoRegistro.Add(monto);
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Monto ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(
                                //TODO ARKANO  VxCob.MtoCob,MODXDOC.Formato)});
                                Pr_Salto_Pagina();
                                if (VxCob.MtoInt > 0)
                                {
                                    var montoCharges = new MontoRegistro { Glosa = "Interes Proveedor ", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.MtoInt, MODXDOC.Formato) };
                                    documento.LineasMontoRegistro.Add(montoCharges);
                                    //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Interes Proveedor ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),
                                    //TODO ARKANO    MODGDOC.forma(VxCob.MtoInt,MODXDOC.Formato)});
                                    Pr_Salto_Pagina();
                                }
                                Total = VxCob.MtoCob + VxCob.MtoInt;
                                if (Total > VxCob.MtoCob)
                                {
                                    //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Total ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 7)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto - 3)),MODGDOC.forma
                                    //TODO ARKANO   (Total,MODXDOC.Formato)});
                                    documento.MonedaTotalMontos = VxCob.Nemonic.TrimB();
                                    documento.TotalMontos = MODGDOC.forma(Total, MODXDOC.Formato);
                                    Pr_Salto_Pagina();
                                }
                                break;
                            case "I":
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Amount ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(
                                //TODO ARKANO   VxCob.MtoCob,MODXDOC.Formato)});
                                var montoInicio = new MontoRegistro { Glosa = "Amount", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.MtoCob, MODXDOC.Formato) };
                                documento.LineasMontoRegistro.Add(montoInicio);
                                Pr_Salto_Pagina();
                                if (VxCob.MtoInt > 0)
                                {
                                    var montoInterest = new MontoRegistro { Glosa = "Drawer´s Interest    ", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.MtoInt, MODXDOC.Formato) };
                                    documento.LineasMontoRegistro.Add(montoInterest);
                                    //TODO ARKANO  MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Drawer´s Interest    ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),
                                    //TODO ARKANO    MODGDOC.forma(VxCob.MtoInt,MODXDOC.Formato)});
                                    Pr_Salto_Pagina();
                                }
                                Total = VxCob.MtoCob + VxCob.MtoInt;
                                if (Total > VxCob.MtoCob)
                                {
                                    //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Total ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 7)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto - 3)),MODGDOC.forma
                                    //TODO ARKANO   (Total,MODXDOC.Formato)});
                                    documento.MonedaTotalMontos = VxCob.Nemonic.TrimB();
                                    documento.TotalMontos = MODGDOC.forma(Total, MODXDOC.Formato);
                                    Pr_Salto_Pagina();
                                }
                                break;
                        }
                        break;
                    default:
                        var montoDefault = new MontoRegistro { Glosa = "Monto", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.MtoCob, MODXDOC.Formato) };
                        documento.LineasMontoRegistro.Add(montoDefault);
                        //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Monto ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(
                        //TODO ARKANO    VxCob.MtoCob,MODXDOC.Formato)});
                        Pr_Salto_Pagina();
                        if (VxCob.MtoInt > 0)
                        {
                            var montoCharges = new MontoRegistro { Glosa = "Interes Proveedor ", Moneda = VxCob.Nemonic.TrimB(), Monto = MODGDOC.forma(VxCob.MtoInt, MODXDOC.Formato) };
                            documento.LineasMontoRegistro.Add(montoCharges);
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Interes Proveedor ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),
                            //TODO ARKANO   MODGDOC.forma(VxCob.MtoInt,MODXDOC.Formato)});
                            Pr_Salto_Pagina();
                        }
                        Total = VxCob.MtoCob + VxCob.MtoInt;
                        if (Total > VxCob.MtoCob)
                        {
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Total ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 7)),VxCob.Nemonic.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto - 3)),MODGDOC.forma
                            //TODO ARKANO (Total,MODXDOC.Formato)});
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
                    Pr_Salto_Pagina();
                    if (Idioma == "I" && Carta != 2)
                    {
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(new string(' ', tabulador - 23), "Maturity   : ");
                    }
                    else
                    {
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(new string(' ', tabulador - 23), "Vencimiento: ");
                    }
                    if (Idioma == "I" && Carta != 2)
                    {
                        for (i = 0; i <= n; i += 1)
                        {
                            if (i == 0 && Vencimientos[i].FecVen == "")
                            {
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(Vencimientos[i].TotVen,
                                //TODO ARKANO   MODXDOC.Formato),MigrationSupport.FileSystem.TAB(89),"Payment"});
                            }
                            else
                            {
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(Vencimientos[i].TotVen,
                                //TODO ARKANO  MODXDOC.Formato),MigrationSupport.FileSystem.TAB(89),MigrationSupport.Utils.Format(Vencimientos[i].FecVen,"mm/dd/yyyy").TrimB()});
                            }
                            Pr_Salto_Pagina();
                        }
                    }
                    else
                    {
                        for (i = 0; i <= n; i += 1)
                        {
                            if (i == 0 && Vencimientos[i].FecVen == "")
                            {
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(Vencimientos[i].TotVen,
                                //TODO ARKANO  MODXDOC.Formato),MigrationSupport.FileSystem.TAB(89),"A la Vista"});
                            }
                            else
                            {
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(Vencimientos[i].TotVen,
                                //TODO ARKANO  MODXDOC.Formato),MigrationSupport.FileSystem.TAB(89),MigrationSupport.Utils.Format(Vencimientos[i].FecVen,"dd/mm/yyyy").TrimB()});
                            }
                            Pr_Salto_Pagina();
                        }
                    }
                }
                else
                {
                    for (i = 0; i <= n; i += 1)
                    {
                        if(!string.IsNullOrEmpty(Vencimientos[i].FecVen.TrimB()))
                        {
                            if (i == 0)
                            {
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{new string(' ',tabulador - 23),"Vencimiento: ",MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),
                                //TODO ARKANO  MODGDOC.forma(Vencimientos[i].TotVen,MODXDOC.Formato),MigrationSupport.FileSystem.TAB(89),MigrationSupport.Utils.Format(Vencimientos[i].FecVen,"dd/mm/yyyy").TrimB()});
                            }
                            else
                            {
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB((short)(tab_mto_descr - 4)),Vencimientos[i].CodMon.TrimB(),MigrationSupport.FileSystem.TAB((short)(tab_mto_monto)),MODGDOC.forma(Vencimientos[i].TotVen,
                                //TODO ARKANO  MODXDOC.Formato),MigrationSupport.FileSystem.TAB(89),MigrationSupport.Utils.Format(Vencimientos[i].FecVen,"dd/mm/yyyy").TrimB()});
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
        public static void Pr_Mto_DebitoCredito(DocumentoReporteModel documento)
        {
            string Nemonico = "";
            int i = 0;
            double Total = 0.0;
            int n = 0;

            n = VMontos.GetUpperBound(0);

            // si existen datos, se imprimen los montos
            Total = 0;
            for (i = 0; i <= n; i += 1)
            {
                var debitoCredito = new DebitoCredito { Moneda = VMontos[i].NemMnd.TrimB(), Monto = MODGDOC.forma(VMontos[i].Montos, MODXDOC.Formato) };
                if (i == 2)
                {
                    //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, true);
                }
                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(35),MODGDOC.forma(VMontos[i].Montos,MODXDOC.Formato)});
                if (i == 2)
                {
                    //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeUnderline(MigrationSupport.Printer.DefInstance.Font, false);
                }
                Total = Total + VMontos[i].Montos;
                Nemonico = VMontos[i].NemMnd;
                documento.LineasDebitoCredito.Add(debitoCredito);
            }
            if (Nemonico == "$" && n == 2)
            {
                var debitoCredito = new DebitoCredito { Moneda = Nemonico, Monto = MODGDOC.forma(Total, MODXDOC.Formato) };
                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(30),Nemonico,MigrationSupport.FileSystem.TAB(35),MODGDOC.forma(Total,MODXDOC.Formato)});
            }
        }
        // ****************************************************************************
        //    1.  Despliega la información del Detalle de Retorno
        //        (Ordenantes - Referencia - Monto).
        // ****************************************************************************
        public static void Pr_Ordenante(DocumentoReporteModel documento)
        {
            int i = 0;
            int n = VDetalle.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = VDetalle.GetUpperBound(0);
            //});

            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(15),"Ordenante",MigrationSupport.FileSystem.TAB(53),"Referencia",MigrationSupport.FileSystem.TAB(94),"Monto"});

            for (i = 0; i < n; i += 1)
            {
                var retorno = new Retorno { Ordenante = VDetalle[i].Orden.TrimB(), Referencia = VDetalle[i].Refer.TrimB(), Moneda = VDetalle[i].Moneda.TrimB(), Monto = MODGDOC.forma(VDetalle[i].Monto, MODXDOC.Formato) };
                documento.LineasRetorno.Add(retorno);
                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(16),VDetalle[i].Orden.TrimB(),MigrationSupport.FileSystem.TAB(56),VDetalle[i].Refer.TrimB(),MigrationSupport.FileSystem.TAB(78),VDetalle[i].Moneda.TrimB(),
                //TODO ARKANO MigrationSupport.FileSystem.TAB(85),MODGDOC.forma(VDetalle[i].Monto.ToVal(),MODXDOC.Formato)});
            }
        }
        // ****************************************************************************
        //    1.  Despliega las Planillas del Detalle(Planillas - Código -
        //        Declaración - Monto).
        // ****************************************************************************
        public static void Pr_Planilla(DocumentoReporteModel documento)
        {
            string Formateo = "";
            int i = 0;
            int n = VPlan.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = VPlan.GetUpperBound(0);
            //});

            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{"Planilla",MigrationSupport.FileSystem.TAB(30),"Código",MigrationSupport.FileSystem.TAB(61),"Declaración",MigrationSupport.FileSystem.TAB(104),"Monto"});
            //Pr_Salto_Pagina();

            for (i = 0; i < n; i += 1)
            {
                var linea = new ComercioInvisible();
                if(!string.IsNullOrEmpty(VPlan[i].NroDecl))
                {
                    Formateo = VPlan[i].NroDecl.Left((VPlan[i].NroDecl.Len() - 1)) + "-" + VPlan[i].NroDecl.Right(1);
                    linea.Declaracion = VPlan[i].NroDecl.Left((VPlan[i].NroDecl.Len() - 1)) + "-" + VPlan[i].NroDecl.Right(1);
                }
                else
                {
                    Formateo = "-------";
                    linea.Declaracion = "-------";
                }
                linea.CodigoPlanilla = VPlan[i].NroPlan.TrimB();
                linea.Codigo = VPlan[i].NroCod.TrimB();
                linea.Monto = Format.FormatCurrency(Format.ParseDblFromDB(VPlan[i].PlaMto), MODXDOC.Formato);
                linea.Moneda = VPlan[i].PlaMon.TrimB();
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(VPlan[i].NroPlan.TrimB(), MigrationSupport.FileSystem.TAB(31).Column, VPlan[i].NroCod.TrimB(), MigrationSupport.FileSystem.TAB(65).Column, Formateo.TrimB(), MigrationSupport.FileSystem.TAB(89).Column, VPlan[i].PlaMon.TrimB(),
                //   MigrationSupport.FileSystem.TAB(95).Column, MODGDOC.forma(VPlan[i].PlaMto.ToVal(), MODXDOC.Formato));
                documento.LineasComercioInvisible.Add(linea);
                Pr_Salto_Pagina();
            }

        }
        // ****************************************************************************
        //    1.  Detalle de las Remesas.
        // ****************************************************************************
        public static void Pr_Remesa(DocumentoReporteModel documento)
        {
            int i = 0;
            int n = VxRemesa.Length;

            //MigrationSupport.Utils.ResumeNext(() =>
            //{
            //    n = VxRemesa.GetUpperBound(0);
            //});

            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(6),"Beneficiario",MigrationSupport.FileSystem.TAB(40),"Vía de la Remesa",MigrationSupport.FileSystem.TAB(95),"Monto"});

            for (i = 0; i < n; i += 1)
            {
                var remesa = new Remesa
                {
                    Beneficiario = VxRemesa[i].NomBen,
                    ViaRemesa = VxRemesa[i].NomVia,
                    Moneda = VxRemesa[i].NemMon,
                    Monto = MODGDOC.forma(VxRemesa[i].MtoTot, MODXDOC.Formato)
                };
                documento.LineasRemesa.Add(remesa);
                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(98),MODGDOC.forma(VxRemesa[i].MtoTot,MODXDOC.Formato)});
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
            //TODO: Revisar Pr_Salto_Pagina
            //int i = 0;

            //if (linea >= 57)
            //{
            //    ImprimePagina();
            //    Pagina = Pagina + 1;
            //    MigrationSupport.Printer.DefInstance.NewPage();
            //    linea = 10;
            //    for (i = 1; i <= linea; i += 1)
            //    {
            //        //MigrationSupport.Printer.DefInstance.Print();
            //        Pr_Salto_Pagina();
            //    }
            //}
            //else if (linea < 63)
            //{
            //    linea = linea + 1;
            //}

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
                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(45),"Abonos y/o Cargos"});
            }

        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprime los Tíitulos de Carta.
        // Observaciones  : Coloca el título correspondiente a la carta a imprimir
        // ****************************************************************************
        public static void Pr_Titulos(DocumentoReporteModel documento, int Carta_Aux)
        {
            string Paso = "";
            /*Paragraph paragraph1 = new Paragraph();
            Paragraph paragraph2 = new Paragraph();*/
            documento.TituloCabezal = "Comercio Exterior";
            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Comercio Exterior");
            //paragraph1.Add(new Phrase("Comercio Exterior", FontFactory.GetFont(FontFactory.TIMES_BOLD, 12)));
            //service.Printer.PrintWithoutCrlf("Comercio Exterior");
            
            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(75));

            // -------------------------------
            // Registra la Fecha de la Carta.
            switch (VOpe.NumOpe.Mid(1, 3))
            {
                case "101":
                    DOC_CVDI.Ciudad = "Valparaíso";
                    break;
                case "107":
                    DOC_CVDI.Ciudad = "Iquique";
                    break;
                case "225":
                    DOC_CVDI.Ciudad = "Concepción";
                    break;
                case "290":
                    DOC_CVDI.Ciudad = "Punta Arenas";
                    break;
                default:
                    DOC_CVDI.Ciudad = "Santiago";
                    break;
            }
            
            switch (Carta_Aux)
            {
                case 1:
                case 3:
                case 20:
                    if (Idioma == "I")
                    {
                        Paso = Glosa_Fecha_Hoy_Ingles(MODXDOC01.VDocx.FecEmi);
                        documento.Ciudad = DOC_CVDI.Ciudad + ", " + Paso.TrimB();
                        //paragraph2.Add(new Phrase(DOC_CVDI.Ciudad + ", " + Paso.TrimB() + "\n", FontFactory.GetFont(FontFactory.TIMES, 10)));
                    }
                    else
                    {
                        Paso = Glosa_Fecha_Hoy_Espanol(MODXDOC01.VDocx.FecEmi);
                        documento.Ciudad =  DOC_CVDI.Ciudad + ", " + Paso.TrimB();
                        //paragraph2.Add(new Phrase(DOC_CVDI.Ciudad + ", " + Paso.TrimB(), FontFactory.GetFont(FontFactory.TIMES, 10)));                        
                    }
                    break;
                default:
                    Paso = Glosa_Fecha_Hoy_Espanol(MODXDOC01.VDocx.FecEmi);
                    documento.Ciudad  = DOC_CVDI.Ciudad + ", " + Paso.TrimB();
                    //paragraph2.Add(new Phrase(DOC_CVDI.Ciudad + ", " + Paso.TrimB(), FontFactory.GetFont(FontFactory.TIMES, 10)));
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
                    if(!string.IsNullOrEmpty(VOperac.ConRaya.TrimB()))
                    {
                        if (MODFRA.RefBae == "")
                        {
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(71),"Nuestra Referencia",MigrationSupport.FileSystem.TAB(94),VOperac.ConRaya.TrimB()});                                                        
                            documento.NuestraReferencia = "Nuestra Referencia";
                            documento.NuestraReferenciaValue = VOperac.ConRaya.TrimB();
                        }
                        else
                        {
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),MODFRA.RefBae.TrimB()});                                                        
                        }
                        Pr_Salto_Pagina();
                    }
                }
                switch (Carta_Aux)
                {
                    case 1:
                    case 3:
                        if(!string.IsNullOrEmpty(VOperac.ConRaya.TrimB()))
                        {
                            if (Idioma == "I")
                            {
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(71),"Our Reference");                                
                                //paragraph2.Add(new Phrase("Our Reference     ", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
                                if (MODFRA.RefBae == "")
                                {
                                    //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),VOperac.ConRaya.TrimB()});                                    
                                    //paragraph2.Add(new Phrase(VOperac.ConRaya.TrimB(), FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
                                }
                                else
                                {
                                    //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),MODFRA.RefBae.TrimB()});                                    
                                    //paragraph2.Add(new Phrase(MODFRA.RefBae.TrimB(), FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
                                }
                                Pr_Salto_Pagina();
                            }
                            else
                            {
                                if (MODFRA.RefBae == "")
                                {
                                    //paragraph2.Add(new Phrase(VOperac.ConRaya.TrimB(), FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
                                    //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),VOperac.ConRaya.TrimB()});
                                }
                                else
                                {
                                    //paragraph2.Add(new Phrase(VOperac.ConRaya.TrimB(), FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
                                    //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),MODFRA.RefBae.TrimB()});
                                }
                                Pr_Salto_Pagina();
                            }
                        }
                        break;
                    case 2:
                        if(!string.IsNullOrEmpty(VOperac.ConRaya.TrimB()))
                        {
                            documento.NuestraReferencia = "Nuestra Referencia";
                            if (MODFRA.RefBae == "")
                            {
                                documento.NuestraReferenciaValue = VOperac.ConRaya.TrimB();
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),VOperac.ConRaya.TrimB()});
                            }
                            else
                            {
                                documento.NuestraReferenciaValue = MODFRA.RefBae.TrimB();
                                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),MODFRA.RefBae.TrimB()});
                            }
                            Pr_Salto_Pagina();
                        }
                        break;
                }
            }
            else if (VOpe.TipoDoc == MODXDOC.DocxCobRen)
            {
                if(!string.IsNullOrEmpty(VOperac.ConRaya.TrimB()))
                {
                    if (Idioma == "I")
                    {
                        documento.NuestraReferencia = "Nuestra Referencia";
                        if (MODFRA.RefBae == "")
                        {
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),VOperac.ConRaya.TrimB()});
                        }
                        else
                        {
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),MODFRA.RefBae.TrimB()});
                        }
                        Pr_Salto_Pagina();
                    }
                    else
                    {
                        documento.NuestraReferencia = "Nuestra Referencia";
                        if (MODFRA.RefBae == "")
                        {
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),VOperac.ConRaya.TrimB()});
                        }
                        else
                        {
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),MODFRA.RefBae.TrimB()});
                        }
                        Pr_Salto_Pagina();
                    }
                }
                if(!string.IsNullOrEmpty(VxCob.RefBcoC.TrimB()))
                {
                    documento.CobradorReferencia = "Referencia Cobrador";
                    //MigrationSupport.Printer.DefInstance.Print(VxCob.RefBcoC.TrimB());
                    Pr_Salto_Pagina();
                }
            }
            else if (VOpe.TipoDoc == MODXDOC.DocxAceLet)
            {
                if (Carta_Aux == 4)
                {
                    if(!string.IsNullOrEmpty(VOperac.ConRaya.TrimB()))
                    {
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(71), "Nuestra Referencia");
                        documento.NuestraReferencia= "Nuestra Referencia";
                        if (MODFRA.RefBae == "")
                        {
                            documento.NuestraReferenciaValue = VOperac.ConRaya.TrimB();
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),VOperac.ConRaya.TrimB()});
                        }
                        else
                        {
                            documento.NuestraReferenciaValue = MODFRA.RefBae.TrimB();
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),MODFRA.RefBae.TrimB()});
                        }
                        Pr_Salto_Pagina();
                    }
                }
            }
            else if (VOpe.TipoDoc == MODXDOC.DocxPagDir)
            {
                if (Carta_Aux == 5)
                {
                    if(!string.IsNullOrEmpty(VOpe.NumOpe_t.TrimB()))
                    {
                        documento.NuestraReferencia = "Nuestra Referencia";
                        if (MODFRA.RefBae == "")
                        {
                            documento.NuestraReferenciaValue = VOpe.NumOpe_t.TrimB();
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),VOpe.NumOpe_t.TrimB()});
                        }
                        else
                        {
                            documento.NuestraReferenciaValue = MODFRA.RefBae.TrimB();
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),MODFRA.RefBae.TrimB()});
                        }
                        Pr_Salto_Pagina();
                    }
                }
            }
            else if (VOpe.TipoDoc == MODXDOC.DocxCobCan || VOpe.TipoDoc == MODXDOC.DocxCanRet || VOpe.TipoDoc == MODXDOC.DocxRegCanRet)
            {
                if (VOpe.TipoDoc == MODXDOC.DocxCanRet)
                {
                    // If Trim$(VOpe.NumOpe_t) <> "" Then
                    //TODO: Hay que revisar, se está comentando esto para que funcione
                    if(!string.IsNullOrEmpty(Referencia.TrimB()))
                    {
                        documento.NuestraReferencia = "Nuestra Referencia";
                        if (MODFRA.RefBae == "")
                        {
                            documento.NuestraReferenciaValue = Referencia.TrimB();
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),Referencia.TrimB()});
                        }
                        else
                        {
                            documento.NuestraReferenciaValue = MODFRA.RefBae.TrimB();
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),MODFRA.RefBae.TrimB()});
                        }
                        Pr_Salto_Pagina();
                        documento.MontoCapital = MODGDOC.forma(MtoCap, MODXDOC.Formato);
                        documento.MonedaCapital = Moneda.TrimB();

                        //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(71),"Monto ",Moneda.TrimB() + " " + MODGDOC.forma(MtoCap,MODXDOC.Formato) + " Capital"});
                        Pr_Salto_Pagina();
                        if (MtoInt > 0)
                        {
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(71),"Monto ",Moneda.TrimB() + " " + MODGDOC.forma(MtoInt,MODXDOC.Formato) + " Intereses"});
                            Pr_Salto_Pagina();
                        }
                    }
                }
            }
            else if (VOpe.TipoDoc == MODXDOC.DocxRegRet)
            {
                if (Carta_Aux == 7)
                {
                    // If Trim$(VOpe.NumOpe_t) <> "" Then
                    if(!string.IsNullOrEmpty(Referencia.TrimB()))
                    {

                        documento.NuestraReferencia = "Nuestra Referencia";
                        documento.NuestraReferenciaValue =Referencia.TrimB();
                        //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(94),Referencia.TrimB()});     // Trim$(VOpe.NumOpe_t)
                    }
                }
            }
            else if (VOpe.TipoDoc == MODXDOC.DocxRegPln)
            {
                if (Carta_Aux == 8)
                {
                    //Reporte Operación de Exportación:
                    if(!string.IsNullOrEmpty(VOpe.NumOpe_t.TrimB()))
                    {
                        documento.NuestraReferencia = "Nuestra Referencia";
                        documento.NuestraReferenciaValue = VOpe.NumOpe_t.TrimB();
                        //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(100),VOpe.NumOpe_t.TrimB()});


                        if(!string.IsNullOrEmpty(VOperac.ConRaya))
                        {
                            documento.SuReferencia = "Operación Relacionada";
                            documento.SuReferenciaValue = VOperac.ConRaya;
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(71),"Operación Relacionada",MigrationSupport.FileSystem.TAB(100),VOperac.ConRaya});
                        }
                        if(!string.IsNullOrEmpty(VOperac.RefCli))
                        {
                            documento.SuReferencia = "Su Referencia";
                            documento.SuReferenciaValue = VOperac.RefCli;
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(71),"Su Referencia",MigrationSupport.FileSystem.TAB(100),VOperac.RefCli});
                        }
                    }
                }
            }
            else if (VOpe.TipoDoc == MODXDOC.DocCVD)
            {
                //Reporte Comercio Invisible.
                if (Carta_Aux == 10)
                {
                    if(!string.IsNullOrEmpty(VOpe.NumOpe_t.TrimB()))
                    {
                        documento.NuestraReferencia = "Compra-Venta Divisas Nº : ";
                        documento.NuestraReferenciaValue = VOpe.NumOpe_t.TrimB();
                        //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(71),"Compra-Venta Divisas Nº : " + VOpe.NumOpe_t.TrimB()});
                        if(!string.IsNullOrEmpty(VOperac.ConRaya))
                        {
                            documento.SuReferencia = "Operación Relacionada      : ";
                            documento.SuReferenciaValue = VOperac.ConRaya;
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(71),"Operación Relacionada      : " + VOperac.ConRaya});
                        }
                        if(!string.IsNullOrEmpty(VOperac.RefCli))
                        {
                            documento.SuReferencia = "Su Referencia                      : ";
                            documento.SuReferenciaValue = VOperac.RefCli;
                            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(71),"Su Referencia                      : " + VOperac.RefCli});
                        }
                        Pr_Salto_Pagina();
                        Pr_Salto_Pagina();
                    }
                }
            }
            else if (VOpe.TipoDoc == MODXDOC.DocArb)
            {
                //Reporte Operación de Arbitraje:
                if (Carta_Aux == 11)
                {
                    if(!string.IsNullOrEmpty(VOpe.NumOpe_t.TrimB()))
                    {
                        documento.NuestraReferencia = "Nuestra Referencia";
                        documento.NuestraReferenciaValue = VOpe.NumOpe_t.TrimB();
                        if(!string.IsNullOrEmpty(VOperac.ConRaya))
                        {
                            documento.SuReferencia = "Operación Relacionada";
                            documento.SuReferenciaValue = VOperac.ConRaya;
                        }
                        if(!string.IsNullOrEmpty(VOperac.RefCli))
                        {
                            documento.SuReferencia = "Su Referencia                      : ";
                            documento.SuReferenciaValue = VOperac.RefCli.TrimB();
                        }
                        Pr_Salto_Pagina();
                    }
                }
            }
            else if (VOpe.TipoDoc == MODXDOC.DocGAcre || VOpe.TipoDoc == MODXDOC.DocGAdeb)
            {
                if (MODFRA.RefBae == "")
                {
                    documento.NuestraReferencia = "Operación Número:";
                    documento.NuestraReferenciaValue = VOpe.NumOpe_t.TrimB();
                  
                    //Se incorpora el título Su Referencia:
                    if (!string.IsNullOrEmpty(Referencia))
                    {
                        documento.SuReferencia = "Su Referencia                      : ";
                        documento.SuReferenciaValue = Referencia.TrimB();
                    }
                }
                else
                {
                    documento.NuestraReferencia = "Nuestra Referencia: ";
                    documento.NuestraReferenciaValue = MODFRA.RefBae.TrimB();
                }
            }

            // -------------------------------
            // -------------------------------
            // Registra la Referencia del que envía la carta.
            switch (Carta_Aux)
            {
                case 0:
                case 1:
                    // Referencia del Girador.
                    if(!string.IsNullOrEmpty(VxCob.RefExp1.TrimB()))
                    {
                        if (Carta_Aux == 1)
                        {
                            if (Idioma == "I")
                            {
                                documento.NuestraReferencia = "Drawer's Reference";
                                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(75).Column, "Drawer's Reference", MigrationSupport.FileSystem.TAB(99));
                            }
                            else
                            {
                                documento.NuestraReferencia = "Referencia Girador";
                                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(75).Column, "Referencia Girador", MigrationSupport.FileSystem.TAB(99));
                            }
                        }
                        else
                        {
                            documento.NuestraReferencia = "Referencia Girador";
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(75).Column, "Referencia Girador", MigrationSupport.FileSystem.TAB(99));
                        }
                    }
                    break;
                case 2:
                case 5:
                case 6:
                case 8:
                    // Referencia del Exportador.
                    if(!string.IsNullOrEmpty(VxCob.RefExp1.TrimB()))
                    {
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(75).Column, "Su Referencia", MigrationSupport.FileSystem.TAB(99));                        
                        //MigrationSupport.Printer.DefInstance.Print(VxCob.RefExp1.TrimB());

                        documento.SuReferencia = "Su Referencia";
                        documento.SuReferenciaValue = VxCob.RefExp1.TrimB();
                    }
                    break;
                case 3:
                    // Referencia del Exportador.
                    if(!string.IsNullOrEmpty(VxCob.RefExp1.TrimB()))
                    {
                        if (Idioma == "I")
                        {
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(75).Column, "Drawer's Reference", MigrationSupport.FileSystem.TAB(99));
                            
                            documento.SuReferencia = "Drawer's Reference";
                        }
                        else
                        {
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(75).Column, "Referencia Exportador", MigrationSupport.FileSystem.TAB(99));
                            
                            documento.SuReferencia = "Referencia Exportador";
                        }
                        //MigrationSupport.Printer.DefInstance.Print(VxCob.RefExp1.TrimB());

                        documento.SuReferenciaValue = VxCob.RefExp1.TrimB();
                    }
                    break;
                case 4:
                    // Referencia del Exportador.
                    // Referencia del Cobrador.
                    if(!string.IsNullOrEmpty(VxCob.RefExp1.TrimB()))
                    {   
                        documento.SuReferencia = "Su Referencia";                        
                        documento.SuReferenciaValue = VxCob.RefExp1.TrimB();
                        Pr_Salto_Pagina();
                    }
                    if(!string.IsNullOrEmpty(VxCob.RefBcoC.TrimB()))
                    {
                        
                        documento.CobradorReferencia = "Referencia Cobrador";
                        documento.CobradorReferenciaValue = VxCob.RefBcoC.TrimB();

                        Pr_Salto_Pagina();
                    }
                    // Registro y Reg. + Cancelación Retorno.-
                    break;
                case 7:
                case 14:
                    // Referencia del Exportador.
                    if(!string.IsNullOrEmpty(VxCob.RefExp1.TrimB()))
                    {
                        documento.SuReferencia = "Su Referencia";
                        documento.SuReferenciaValue = VxCob.RefExp1.TrimB();
                        Pr_Salto_Pagina();
                    }
                    // Referencia del Ordenante.
                    if(!string.IsNullOrEmpty(VxCob.RefExp2.TrimB()))
                    {
                        documento.SuReferencia = "Referencia Ordenante";
                        documento.SuReferenciaValue = VxCob.RefExp2.TrimB();
                    }
                    break;
            }
            // -------------------------------

            /*PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.SetWidths(new int[] { 2, 1, 2 });

            table.AddCell(UtilPdf.Cell(paragraph1));
            table.AddCell(UtilPdf.CellEmpty());
            table.AddCell(UtilPdf.Cell(paragraph2));

            service.Document.Add(table);*/

        }
        // ****************************************************************************
        //    1.  Detalle de las Ventas.
        // ****************************************************************************
        public static void Pr_Ventas(DocumentoReporteModel documento)
        {
            int i = 0;
            int n = 0;

            n = Ventas.GetUpperBound(0);

            //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(40),"Ventas de Divisas:"});

            for (i = 0; i <= n; i += 1)
            {
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(7).Column, Ventas[i].NemMnd);
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(20).Column, MODGDOC.forma(Ventas[i].MtoCVD, MODXDOC.Formato));
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(45).Column, "Tipo de Cambio $");
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(65).Column, MODGDOC.forma(Ventas[i].TipCam, MODXDOC.FormatoTipCamb));
                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB(88).Column, "Total $");
                //TODO ARKANO MigrationSupport.Printer.DefInstance.PrintList(new object[]{MigrationSupport.FileSystem.TAB(98),MODGDOC.forma(Ventas[i].MtpPres,MODXDOC.Formato)});
                CompraVenta cp = new CompraVenta()
                {
                    MonedaOrigen = Ventas[i].NemMnd,
                    Monto = MODGDOC.forma(Ventas[i].MtoCVD, MODXDOC.Formato),
                    TipoCambio = MODGDOC.forma(Ventas[i].TipCam, MODXDOC.FormatoTipCamb),
                    MontoTotal = MODGDOC.forma(Ventas[i].MtpPres, MODXDOC.Formato)
                };

                documento.LineasVenta.Add(cp);
                Pr_Salto_Pagina();
            }

        }
        // ****************************************************************************
        //    1.  Entrega la Cobranza en formato xxx-xx-xx-xx-xxxxxx.
        // ****************************************************************************
        public static string Raya_Cobranza(string s)
        {
            string Raya_Cobranza = "";

            string e = "";
            string d = "";
            string c = "";
            string b = "";
            string a = "";

            a = s.Mid(1, 3);
            b = s.Mid(4, 2);
            c = s.Mid(6, 2);
            d = s.Mid(8, 2);
            e = s.Mid(10, 6);

            Raya_Cobranza = a + "-" + b + "-" + c + "-" + d + "-" + e;

            return Raya_Cobranza;
        }
        public static string raya_Cobranza2(string s)
        {
            string raya_Cobranza2 = "";

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

            raya_Cobranza2 = a + "-" + b + "-" + c + "-" + d + "-" + e;

            return raya_Cobranza2;
        }
        // ****************************************************************************
        //    1.  Setea variables importantes relacionadas con la impresión.
        // ****************************************************************************
        //public static void SetupLetras(Phrase phrase = null)
        public static void SetupLetras()
        {
            //if (phrase == null) return;
            try
            {
                // En cms.
                /*if (MigrationSupport.Printer.DefInstance.ScaleMode != MigrationSupport.Utils.ScaleType.vbCentimeters.ToShort())
                {
                    MigrationSupport.Printer.DefInstance.ScaleMode = MigrationSupport.Utils.ScaleType.vbCentimeters.ToShort();
                    MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeName(MigrationSupport.Printer.DefInstance.Font, "Times New Roman");
                    MigrationSupport.Printer.DefInstance.ScaleLeft = -2;
                }*/
                //phrase.Font = FontFactory.GetFont("Times-Roman");


                // Ciudad
                switch (VOpe.NumOpe.Mid(1, 3))
                {
                    case "101":
                        DOC_CVDI.Ciudad = "Valparaíso";
                        break;
                    case "107":
                        DOC_CVDI.Ciudad = "Iquique";
                        break;
                    case "225":
                        DOC_CVDI.Ciudad = "Concepción";
                        break;
                    case "290":
                        DOC_CVDI.Ciudad = "Punta Arenas";
                        break;
                    default:
                        DOC_CVDI.Ciudad = "Santiago";
                        break;
                }
                return;
            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XegiException(MigrationSupport.Utils.Format("Error Number " + MigrationSupport.GlobalException.Instance.Number.Str() + " occurred at line ", String.Empty), exc);
            }
        }
        // ****************************************************************************
        //    1.  Lee la tabla de Usuarios dependiendo de un Centro de Costo y
        //        Código del Usuario.
        //        Retorno    <> ""  : Lectura Exitosa.
        //                   =  ""  : Error o Lectura no Exitosa.
        // ****************************************************************************
        public static int SyGet_Usr(UnitOfWorkCext01 uow, string cencos, string CodUsr)
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

                /*Que = "Exec " + MODGSRM.ParamSrm8k.base_migname + "." + MODGSRM.ParamSrm8k.usuario + "." + "sce_usr_s02_MS ";
                Que = Que + MODGSYB.dbcharSy(cencos) + " , ";
                Que = Que + MODGSYB.dbcharSy(CodUsr);
                Que = Que.LCase();

                // Se ejecuta el Query.
                R = MODGSRM.RespuestaQuery(ref Que);*/

                var result = uow.SceRepository.sce_usr_s02_MS(MODGSYB.dbcharSy(cencos), MODGSYB.dbcharSy(CodUsr));

                // Error en el Query.
                //if (R == "-1")
                //{
                //    MigrationSupport.Utils.MsgBox("Se ha producido un error al tratar de leer los datos de los Usuarios (Sce_Usr). El Servidor reporta : [" + MODGSRM.ParamSrm8k.Mensaje.TrimB().Left(100) + "]. Reporte este problema.", MODGDOC.Pito(48).Cast<
                //       MigrationSupport.MsgBoxStyle>(), MODXDOC.MsgxDoc);
                //    return SyGet_Usr;
                //}

                //if (R == "")
                //{
                //    return SyGet_Usr;
                //}

                //n = MODGSRM.RowCount;
                n = result.Count;
                Array.Resize(ref UsrEsps, largo + n + 1);
                m = UsrEsps.GetUpperBound(0);
                UsrEsps[m].cencos = result[0].cent_costo;
                UsrEsps[m].CodUsr = result[0].id_especia;
                UsrEsps[m].nombre = result[0].nombre;
                UsrEsps[m].Direccion = result[0].direccion;
                UsrEsps[m].Ciudad = result[0].ciudad;
                UsrEsps[m].telefono = result[0].telefono;
                UsrEsps[m].Fax = result[0].fax;
                //UsrEsps[m].cencos = MODGSYB.GetPosSy(MODGSYB.NumIni(), "C", R).Str().TrimB();
                //UsrEsps[m].CodUsr = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                //UsrEsps[m].nombre = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                //UsrEsps[m].Direccion = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                //UsrEsps[m].Ciudad = MODGSYB.GetPosSy(MODGSYB.NumSig(), "C", R).Str().TrimB();
                //UsrEsps[m].telefono = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).Str().TrimB();
                //UsrEsps[m].Fax = MODGSYB.GetPosSy(MODGSYB.NumSig(), "N", R).Str().TrimB();

                SyGet_Usr = m;

                return SyGet_Usr;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                //throw new XegiException(MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty), exc);
                return m;
            }            
        }
        // ****************************************************************************
        // Autor          : Orieta Gamonal Gutiérrez
        // Fecha          : Junio 1995
        // Propósito      : Imprime los Encabecados de las Cartas.
        // Observaciones  : Coloca el encabezado correspondiente a la carta a imprimir
        // ****************************************************************************
        public static void xDoc_Cob(DocumentoReporteModel documento, int Indice_Var1, int Indice_Var2)
        {
            string MsgErrImp = "";
            string Paso = "";
            string Cas = "";
            int n = PartysOpe.Length;

            try
            {
                //Paragraph paragraph1 = new Paragraph();
                //Paragraph paragraph2 = new Paragraph();
                //paragraph2.IndentationLeft = 75 * 4;                

                //MigrationSupport.Utils.ResumeNext(() =>
                //{
                //    n = PartysOpe.GetUpperBound(0);
                //});

                if (Indice_Var1 > n || Indice_Var2 > n)
                {
                    return;
                }

                switch (PartysOpe[Indice_Var1].Enviara)
                {
                    case 0:
                    case 1:
                        switch (Num_Carta)
                        {
                            case 1:
                            case 3:
                            case 20:
                                if (Idioma == "I")
                                {
                                    documento.Tratamiento1 = "Messrs";                                    
                                }
                                else
                                {
                                    documento.Tratamiento1 = "Señores";
                                }
                                break;
                            default:
                                if (Num_Carta == 6 || Num_Carta == 14)
                                {
                                    switch (CobRet)
                                    {
                                        case "C":
                                            documento.Tratamiento1 = "Señor(es)";
                                            documento.Girado = "Girado";
                                            break;
                                        case "R":                                            
                                            documento.Tratamiento1 = "Señor(es)";
                                            break;
                                    }
                                }
                                else
                                {
                                    documento.Tratamiento1 = "Señores";
                                }
                                break;
                        }
                        // Icob = 0: Igrr = 1: Iexp = 2: Igir = 3: Iage = 4
                        if (Num_Carta == 0 || Num_Carta == 1 || Num_Carta == 3 || Num_Carta == 20)
                        {
                            switch (Num_Carta)
                            {
                                case 1:
                                case 3:
                                case 20:
                                    if (Idioma == "I")
                                    {
                                        documento.Girador = "Drawer";
                                        //paragraph2.Add(new Phrase("Drawer\n", FontFactory.GetFont(FontFactory.TIMES_BOLD, 10)));
                                    }
                                    else
                                    {
                                        documento.Girador = "Girador";
                                    }
                                    break;
                                default:
                                    documento.Girador = "Girador";
                                    break;
                            }
                        }
                        else if (Num_Carta == 2 || Num_Carta == 4)
                        {
                            documento.Girado = "Girado";
                        }
                        else if (Num_Carta == 3)
                        {                            
                            documento.Girado = "Exportador";
                        }
                        documento.NombreCliente = PartysOpe[Indice_Var1].NombreUsado.TrimB();
                        documento.GiradoNombre = PartysOpe[Indice_Var2].NombreUsado.TrimB();
                        
                        documento.Direccion1 = PartysOpe[Indice_Var1].NombreUsado.TrimB();
                        documento.Direccion2 = PartysOpe[Indice_Var2].NombreUsado.TrimB();

                        if(!string.IsNullOrEmpty(PartysOpe[Indice_Var1].DireccionUsado))
                        {
                            documento.SubDireccion1 = PartysOpe[Indice_Var1].DireccionUsado.TrimB();
                        }
                        else
                        {
                            if (Idioma == "I")
                            {
                                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("P.O.Box " + PartysOpe[Indice_Var1].CasPostal);
                            }
                            else
                            {
                                //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Casilla Postal " + PartysOpe[Indice_Var1].CasPostal);
                            }
                        }
                        // Casilla Postal para Girado.-
                        if (Num_Carta == 2 || Num_Carta == 4 || Num_Carta == 6)
                        {
                            if(!string.IsNullOrEmpty(PartysOpe[Indice_Var2].CasPostal))
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
                            //MigrationSupport.Printer.DefInstance.Print(Concatena(PartysOpe[Indice_Var2].DireccionUsado.TrimB(), Cas, ""));
                            
                            documento.SubDireccion2 = Concatena(PartysOpe[Indice_Var2].DireccionUsado.TrimB(), Cas, "");
                        }
                        else
                        {
                            documento.SubDireccion2 = PartysOpe[Indice_Var2].DireccionUsado.TrimB();
                        }
                        Pr_Salto_Pagina();
                        // si es de exportador  -  girado
                        // se imprime ciudad de exportador, y ciudad con pais de girado
                        // en caso contrario es sólo ciudad para exportador y girado
                        documento.CiudadCliente = PartysOpe[Indice_Var1].CiudadUsado;
                        switch (Num_Carta)
                        {
                            case 2:
                            case 6:
                                Paso = Concatena(PartysOpe[Indice_Var1].ComunaUsado, PartysOpe[Indice_Var1].CiudadUsado, "");
                                //documento.CiudadCliente = PartysOpe[Indice_Var1].CiudadUsado;
                                break;
                            case 3:
                                // Paso$ = Concatena(PartysOpe(IExp1).ComunaUsado, PartysOpe(IExp1).CiudadUsado, PartysOpe(IExp1).EstadoUsado)
                                // Paso$ = Concatena(Paso$, PartysOpe(IExp1).PostalUsado, "")
                                Paso = Concatena(PartysOpe[Indice_Var1].ComunaUsado, PartysOpe[Indice_Var1].CiudadUsado, PartysOpe[Indice_Var1].EstadoUsado);
                                Paso = Concatena(Paso, PartysOpe[Indice_Var1].PostalUsado, "");
                                break;
                            case 4:
                                Paso = Concatena(PartysOpe[Indice_Var1].ComunaUsado, PartysOpe[Indice_Var1].CiudadUsado, PartysOpe[Indice_Var1].PostalUsado);
                                break;
                            case 14:
                                Paso = PartysOpe[Indice_Var1].CiudadUsado;
                                break;
                            default:
                                Paso = Concatena(PartysOpe[Indice_Var1].ComunaUsado, PartysOpe[Indice_Var1].CiudadUsado, PartysOpe[Indice_Var1].EstadoUsado);
                                Paso = Concatena(Paso, PartysOpe[Indice_Var1].PostalUsado, "");
                                break;
                        }
                        documento.Paso1 = Paso;
                        switch (Num_Carta)
                        {
                            case 0:
                            case 1:
                            case 20:
                                documento.GiradoDireccion1 = PartysOpe[Indice_Var2].CiudadUsado;
                                //paragraph2.Add(new Phrase(Paso, FontFactory.GetFont(FontFactory.TIMES, 10)));
                                //paragraph2.Add(new Phrase(PartysOpe[Indice_Var2].CiudadUsado.TrimB(), FontFactory.GetFont(FontFactory.TIMES, 10)));

                                Pr_Salto_Pagina();
                                if (Idioma == "I")
                                {
                                    documento.Pais2 = PaiEnIng(PartysOpe[Indice_Var1].PaisUsado);
                                    //paragraph2.Add(new Phrase(PaiEnIng(PartysOpe[Indice_Var1].PaisUsado), FontFactory.GetFont(FontFactory.TIMES, 10)));
                                }
                                else
                                {
                                    documento.Pais2 = PartysOpe[Indice_Var1].PaisUsado.TrimB();
                                }
                                documento.GiradoPais = PartysOpe[Indice_Var2].PaisUsado.TrimB();
                                Pr_Salto_Pagina();
                                break;
                            case 2:
                            case 4:
                            case 6:
                                Paso = Concatena(PartysOpe[Indice_Var2].ComunaUsado, PartysOpe[Indice_Var2].CiudadUsado, PartysOpe[Indice_Var2].EstadoUsado);
                                Paso = Concatena(Paso, PartysOpe[Indice_Var2].PostalUsado, "");
                                documento.GiradoDireccion1 = PartysOpe[Indice_Var2].CiudadUsado;
                                documento.Paso2 = Paso;
                                documento.GiradoPais = PartysOpe[Indice_Var2].PaisUsado.TrimB();
                                Pr_Salto_Pagina();
                                break;
                            case 3:
                                if (Idioma == "I")
                                {
                                    //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(PaiEnIng(PartysOpe[Indice_Var1].PaisUsado), MigrationSupport.FileSystem.TAB((short)(tabulador)).Column);
                                }
                                else
                                {
                                    //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].PaisUsado, MigrationSupport.FileSystem.TAB((short)(tabulador)).Column);
                                }
                                documento.GiradoPais = PartysOpe[Indice_Var2].PaisUsado.TrimB();
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
                        //MigrationSupport.Printer.DefInstance.Print(PartysOpe[Indice_Var1].NombreUsado.TrimB());
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Casilla Interna Banco ");
                        //MigrationSupport.Printer.DefInstance.Print(PartysOpe[Indice_Var1].CasBanco.TrimB());
                        //Pr_Salto_Pagina();
                        break;
                    case 3:
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, true);
                        if (Idioma == "I")
                        {
                            //MigrationSupport.Printer.DefInstance.Print("Messrs");
                        }
                        else
                        {
                            //MigrationSupport.Printer.DefInstance.Print("Señores");
                        }
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].NombreUsado.TrimB());
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB((short)(tabulador)).Column);
                        switch (Num_Carta)
                        {
                            case 3:
                                if (Idioma == "I")
                                {
                                    //MigrationSupport.Printer.DefInstance.Print("Drawer");
                                }
                                else
                                {
                                    //MigrationSupport.Printer.DefInstance.Print("Girador");
                                }
                                break;
                            default:
                                //MigrationSupport.Printer.DefInstance.Print("Girado");
                                break;
                        }
                        Pr_Salto_Pagina();
                        // Casilla Postal del Exportador + Nombre del Girado.
                        if (Num_Carta == 3 && Idioma == "I")
                        {
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("P.O.Box ");
                        }
                        else
                        {
                            //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf("Casilla Postal ");
                        }
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].CasPostal.TrimB());
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB((short)(tabulador)).Column);
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, false);
                        //MigrationSupport.Printer.DefInstance.Print(PartysOpe[Indice_Var2].NombreUsado.TrimB());
                        // Ciudad y Estado del Exportador + Dirección del Girado.
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, true);
                        // Paso$ = Concatena(PartysOpe(Indice_Var1).CiudadUsado, PartysOpe(Indice_Var1).EstadoUsado, PartysOpe(Indice_Var1).PostalUsado)
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].CiudadUsado);
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB((short)(tabulador)).Column);
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, false);
                        // Casilla Postal para Girado.-
                        if (Num_Carta == 2 || Num_Carta == 4)
                        {
                            if(!string.IsNullOrEmpty(PartysOpe[Indice_Var2].CasPostal))
                            {
                                Cas = "Casilla Postal " + PartysOpe[Indice_Var2].CasPostal;
                            }
                            //MigrationSupport.Printer.DefInstance.Print(Concatena(PartysOpe[Indice_Var2].DireccionUsado.TrimB(), Cas, ""));
                        }
                        else
                        {
                            //MigrationSupport.Printer.DefInstance.Print(PartysOpe[Indice_Var2].DireccionUsado.TrimB());
                        }
                        // Printer.Print Trim$(PartysOpe(Indice_Var2).DireccionUsado)
                        Pr_Salto_Pagina();
                        // País del Exportador + Comuna, Estado, Ciudad del Girado.
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, true);
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, true);
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(PartysOpe[Indice_Var1].PaisUsado.TrimB());
                        //MigrationSupport.Printer.DefInstance.PrintWithoutCrlf(MigrationSupport.FileSystem.TAB((short)(tabulador)).Column);
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeBold(MigrationSupport.Printer.DefInstance.Font, false);
                        //MigrationSupport.Printer.DefInstance.Font = MigrationSupport.Utils.FontChangeItalic(MigrationSupport.Printer.DefInstance.Font, false);
                        Paso = Concatena(PartysOpe[Indice_Var2].ComunaUsado, PartysOpe[Indice_Var2].CiudadUsado, PartysOpe[Indice_Var2].EstadoUsado);
                        Paso = Concatena(Paso, PartysOpe[Indice_Var2].PostalUsado, PartysOpe[Indice_Var2].PaisUsado);
                        //MigrationSupport.Printer.DefInstance.Print(Paso);
                        Pr_Salto_Pagina();
                        break;
                }
                /*PdfPTable table = new PdfPTable(3);
                table.WidthPercentage = 100;                
                table.SetWidths(new int[] { 2, 1, 2});

                table.AddCell(UtilPdf.Cell(paragraph1));
                table.AddCell(UtilPdf.CellEmpty());
                table.AddCell(UtilPdf.Cell(paragraph2));
                
                service.Document.Add(table);*/

                return;

            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XegiException(MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty), exc);
            }
        }
        // ****************************************************************************
        //    1.  Este procedimiento está destinado para la carta con sólo el
        //        encabezado de Exportador.
        //        Carta Exportador.
        // ****************************************************************************
        public static void xDoc_Exp(DocumentoReporteModel documento, int Indice_Var1)
        {
            string MsgErrImp = "";
            string Paso = "";
            try
            {
                switch (PartysOpe[Indice_Var1].Enviara)
                {
                    case 0:
                    case 1:
                        documento.NombreCliente = PartysOpe[Indice_Var1].NombreUsado.TrimB();
                        documento.DireccionCliente = PartysOpe[Indice_Var1].DireccionUsado.TrimB();
                        documento.CiudadCliente = PartysOpe[Indice_Var1].CiudadUsado.TrimB();
                        break;
                    case 2:
                        documento.NombreCliente = PartysOpe[Indice_Var1].NombreUsado.TrimB();
                        documento.CasillaInternaBanco = PartysOpe[Indice_Var1].CasBanco.TrimB();
                        // Printer.Print "Presente"
                        // Call Pr_Salto_Pagina
                        break;
                    case 3:
                        switch (Carta)
                        {
                            case 9:
                                documento.NombreCliente = PartysOpe[Indice_Var1].NombreUsado.TrimB();
                                documento.CasillaPostal = PartysOpe[Indice_Var1].CasPostal.TrimB();
                                documento.CiudadCliente = PartysOpe[Indice_Var1].CiudadUsado;
                                break;
                            default:
                                documento.NombreCliente = PartysOpe[Indice_Var1].NombreUsado.TrimB();
                                documento.CasillaPostal = PartysOpe[Indice_Var1].CasPostal.TrimB();
                                Paso = Concatena(PartysOpe[Indice_Var1].CiudadUsado, PartysOpe[Indice_Var1].EstadoUsado, PartysOpe[Indice_Var1].PostalUsado);
                                documento.Paso1 = Paso;
                                documento.Pais2 = PartysOpe[Indice_Var1].PaisUsado.TrimB();
                                break;
                        }
                        break;
                }
                return;
            }
            catch (Exception exc)
            {
                MigrationSupport.GlobalException.Initialize(exc);
                throw new XegiException(MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty), exc);
            }
        }
    }
}
