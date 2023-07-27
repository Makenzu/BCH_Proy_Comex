using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    // --------------------------------------------------------
    // Estructura para almacenar el Header de la Contabilidad.
    // --------------------------------------------------------
    public class T_Mch
    {
        public string CodCct = String.Empty;   //Centro de Costo.
        public string CodPro = String.Empty;   //Producto.
        public string CodEsp = String.Empty;   //Especialista.
        public string CodOfi = String.Empty;   //Empresa.
        public string CodOpe = String.Empty;   //Operación.
        public int CodNeg;   //Utilización.-
        public int CodSec;   //Secuencia.-
        public int NroRpt;   //Correlativo (id_usr + hora).
        public string FecMov = String.Empty;   //Fecha del Movimiento.
        public string cencos = String.Empty;   //Centro de Costo.
        public string codusr = String.Empty;   //Código del Usuario.
        public int estado;   //Estado del Comprobante.
        public int OfiCon;   //Oficina de Contabilización.
        public string PrtCli = String.Empty;   //Party Importador.
        public int IndCli;   //Indice del Importador.
        public string rutcli = String.Empty;   //Rut Importador.
        public string NomCli = String.Empty;   //Nombre Importador.
        public string DirCli = String.Empty;   //Dirección Importador.
        public string ComCli = String.Empty;   //Comuna Importador.
        public string CiuCli = String.Empty;   //Ciudad Importador.
        public string PaiCli = String.Empty;   //País Importador.
        public int codfun;   //Código Función.
        public string operel = String.Empty;   //Operación Relacionada.-
        public string DesGen = String.Empty;   //Descripción General.

        public T_Mch Copy()
        {
            return (T_Mch)this.MemberwiseClone();
        }
    }
    // --------------------------------------------------------
    // Estructura para almacenar el Detalle de la Contabilidad.
    // --------------------------------------------------------
    public class T_Mcd
    {
        public string CodCct = String.Empty;   //Centro de Costo.
        public string CodPro = String.Empty;   //Producto.
        public string CodEsp = String.Empty;   //Especialista.
        public string CodOfi = String.Empty;   //Empresa.
        public string CodOpe = String.Empty;   //Operación.
        public int CodNeg;   //Utilización.-
        public int CodSec;   //Secuencia.-
        public int NroRpt;   //Correlativo (id_usr + hora).
        public string FecMov = String.Empty;   //Fecha del Movimiento.
        public string cencos = String.Empty;   //Centro Costos Dueño.-
        public string codusr = String.Empty;   //Usuario Dueño.-
        public int NroImp;   //Número imputaciones.
        public int estado;   //Estado del Comprobante.
        public string TipMcd = String.Empty;   //<I>ngreso, <R>everso.
        public int IdnCta;   //Identificador Cuenta Contable.
        public string NemCta = String.Empty;   //Nemónico Cuenta Contable.
        public string numcta = String.Empty;   //Número de Cuenta Contable.
        public int CodMon;   //Moneda Banco de Chile.
        public string NemMon = String.Empty;   //Nemónico de la Moneda.
        public double mtomcd;   //Monto del Movimiento.
        public string cod_dh = String.Empty;   //<D>ebe, <H>aber.
        public int NumEmb;   //Número de Embarque.
        public string PrtCli = String.Empty;   //Party Importador.
        public int IndCli;   //Indice del Importador.
        public string rutcli = String.Empty;   //Rut Importador.
        public string PrtBco = String.Empty;   //Party Banco.-
        public int IndBco;   //Indice del Banco.-
        public string RutBco = String.Empty;   //Rut Banco.-
        public string SwiBco = String.Empty;   //Swift Banco.-
        public int CodBco;   //Código Contable Banco.-
        public string numcct = String.Empty;   //Número de Cta. Cte.-
        public string numlin = String.Empty;   //Número de Linea de Crédito.-
        public string FecOri = String.Empty;   //Fecha de Origen.-
        public string FecVen = String.Empty;   //Fecha de Vencimiento.-
        public string FecInt = String.Empty;   //Fecha de Inicio Intereses.-
        public int TasFij;   //Tiene Tasa Fija? (o Variable).-
        public double MtoTas;   //Monto Tasa para Tasa Variable.-
        public int OfiDes;   //Oficina Destino.-
        public int NumPar;   //Número de Partida.-
        public int TipMov;   //Tipo Movimiento 1:Crea;2:Recibe.-
        public string NroRef = String.Empty;   //Referencia del Documento.-
        public double TipCam;   //Tipo de Cambio a Moneda Nacional.-
        public int NroTOp;   //Número de TO.
        public int IndTOp;   //Indicador de TO.-
        public int IntCIT;   //Interfaz al CIT.-
        public int IntCVT;   //Interfaz al CVT.-
        public int intcap;   //Interfaz al CAP.-
        public int IntCTD;   //Interfaz al CTD.-
        public int IntPOS;   //Interfaz al POS.-
        public int IntCDR;   //Interfaz al CDR.-
        public int McdVig;   //Movimiento vigente?.-

        public T_Mcd Copy()
        {
            return (T_Mcd)this.MemberwiseClone();
        }
    }
    public class T_MODGCON0
    {
        // --------------------------------------------------------
        // Constantes para las Opers. de Contabilidad.
        // --------------------------------------------------------
        public const string CONTAB_ING = "I";     // Mov. Ingreso.
        public const string CONTAB_REV = "R";     // Mov. Reverso.
        public const string CONTAB_ANU = "A";     // Mov. Anulado.
                                                  // --------------------------------------------------------
                                                  // Estados para el Comprobante Contable.
                                                  // --------------------------------------------------------
        public const int ECC_ING = 1;     // Estado Ingresado.
        public const int ECC_ANU = 9;     // Estado Anualdo.
                                          // --------------------------------------------------------
        public T_Mch VMch = new T_Mch();     // Header  Contabilidad.-
        public T_Mch VmchNul = new T_Mch();     // Header  Contabilidad.-
        public T_Mcd VMcd = new T_Mcd();     // Detalle Contabilidad.-
        public T_Mcd VMcdNul = new T_Mcd();     // Detalle Contabilidad.-
        public T_Mcd[] VMcds = null;     // Arreglo Detalle Contabilidad.-
        public T_Mcd[] VMcdz = null;     // Arreglo respaldo Contabilidad
        public const string MsgCon = "Contabilidad";
        // --------------------------------------------------------
        // Identificadores de Cuentas Contables Generales.-
        // --------------------------------------------------------
        public const int IdCta_CtaCteMN = 3;     // Cuenta Corriente M/N
        public const int IdCta_VVBCH = 4;     // Vale Vista Bco. Chile
        public const int IdCta_VVOB = 5;     // Vale Vista Otro Banco
        public const int IdCta_CHMNBCH = 6;     // Cheque M/N Bco. Chile
        public const int IdCta_CHMNOB = 7;     // Cheque M/N Otro Banco
        public const int IdCta_CTAPTEMN = 8;     // Cuenta Puente M/N
        public const int IdCta_SCSMN = 9;     // Saldos Con Sucursales M/N
        public const int IdCta_CtaCteME = 10;     // Cuenta Corriente M/E
        public const int IdCta_CHMEBCH = 11;     // Cheque M/E Emi. x B. Chile
        public const int IdCta_CHMEONY = 12;     // Cheque M/E cliente of. N.Y.
        public const int IdCta_CHMEOBC = 13;     // Cheque M/E Otro B (Chile)
        public const int IdCta_CHMEOBE = 14;     // Cheque M/E Otro B (Extr)
        public const int IdCta_CTAPTEME = 15;     // Cheque Puente M/E
        public const int IdCta_SCSME = 16;     // Saldos Con Sucursales M/E
        public const int IdCta_OPC = 17;     // Orden de Pago Convenio
        public const int IdCta_OPOP = 18;     // Orden de Pago Otros Países
        public const int IdCta_VAM = 19;     // Varios Acreedores Import.
        public const int IdCta_VAX = 20;     // Varios Acreedores Export.
        public const int IdCta_VAMC = 21;     // Varios Acreedores Mcdo. Corr.
        public const int IdCta_CTACTEBC = 22;     // Cta. Cte. Banco Central
        public const int IdCta_CTAORD = 23;     // Corresponsal cuenta ordinaria
        public const int IdCta_DIVENPEN = 24;     // Divisas Pendientes.-
        public const int IdCta_OPEPEND = 25;     // Cuenta Divisas Pendientes.-
        public const int IdCta_VAMCC = 26;     // Cuenta Divisas Pendientes.-
        public const int IdCta_VASC = 27;     // Varios Acreedores Servicio Comex.-
        public const int IdCta_CHVBNYM = 29;     // Cheque VºBº B. Chile N.Y. - Miami
        public const int IdCta_OBLACP = 30;     // Obligación aceptación
        public const int IdCta_BOEREG = 31;     // Bco. Centarl Regiones
        public const int IdCta_CHEREG = 32;     // Cheque M/E Regiones
        public const int IdCta_OBLREG = 33;     // Obligación O/Bcos. Regiones
        public const int IdCta_OBLARE = 34;     // Obligación Aladi Regiones
        public const int IdCta_ACEREG = 35;     // Bco Corresponsal Regiones
        public const int IdCta_CHVRF = 54;     // Check Verification
        public const int IdCta_ONMN = 56;     // Otro Nemónico M/N
                                              // D.S.B.
        public const int IdCta_ChqCCME = 55;     // Cheque Cuenta Corriente M/E
        public const int IdCta_ONME = 60;     // Otro Nemónico M/E
        public const int IdCta_BcoCor = 100;     // Banco Corresponsal.-
        // --------------------------------------------------------
        // Constantes Generales para Formatear Montos.-
        // --------------------------------------------------------
        public const string FormatoConDec = "#,###,###,###,##0.00";
        public const string FormatoSinDec = "#,###,###,###,##0";
        public const int CodBcoBC = 24;

    }
}
