
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //--------------------------------------------------------
    //Estructura para almacenar el Header de la Contabilidad.
    //--------------------------------------------------------
    public class T_Mch
    {
        public string codcct;  //Centro de Costo.
        public string codpro;  //Producto.
        public string codesp;  //Especialista.
        public string codofi;  //Empresa.
        public string codope;  //Operación.
        public short CodNeg;  //Utilización.-
        public short CodSec;  //Secuencia.-
        public int NroRpt;  //Correlativo (id_usr + hora).
        public string fecmov;  //Fecha del Movimiento.
        public string cencos;  //Centro de Costo.
        public string codusr;  //Código del Usuario.
        public short Estado;  //Estado del Comprobante.
        public short OfiCon;  //Oficina de Contabilización.
        public string PrtCli;  //Party Importador.
        public short IndCli;  //Indice del Importador.
        public string rutcli;  //Rut Importador.
        public string NomCli;  //Nombre Importador.
        public string DirCli;  //Dirección Importador.
        public string ComCli;  //Comuna Importador.
        public string CiuCli;  //Ciudad Importador.
        public string PaiCli;  //País Importador.
        public short codfun;  //Código Función.
        public string operel;  //Operación Relacionada.-
        public string DesGen;  //Descripción General.

        public T_Mch Copy()
        {
            return (T_Mch)this.MemberwiseClone();
        }
    }
    //--------------------------------------------------------
    //Estructura para almacenar el Detalle de la Contabilidad.
    //--------------------------------------------------------
    public class T_Mcd
    {
        public string codcct;  //Centro de Costo.
        public string codpro;  //Producto.
        public string codesp;  //Especialista.
        public string codofi;  //Empresa.
        public string codope;  //Operación.
        public short CodNeg;  //Utilización.-
        public short CodSec;  //Secuencia.-
        public int NroRpt;  //Correlativo (id_usr + hora).
        public string fecmov;  //Fecha del Movimiento.
        public string cencos;  //Centro Costos Dueño.-
        public string codusr;  //Usuario Dueño.-
        public short nroimp;  //Número imputaciones.
        public short Estado;  //Estado del Comprobante.
        public string TipMcd;  //<I>ngreso, <R>everso.
        public short IdnCta;  //Identificador Cuenta Contable.
        public string NemCta;  //Nemónico Cuenta Contable.
        public string NumCta;  //Número de Cuenta Contable.
        public short CodMon;  //Moneda Banco de Chile.
        public string NemMon;  //Nemónico de la Moneda.
        public double mtomcd;  //Monto del Movimiento.
        public string cod_dh;  //<D>ebe, <H>aber.
        public short NumEmb;  //Número de Embarque.
        public string PrtCli;  //Party Importador.
        public short IndCli;  //Indice del Importador.
        public string rutcli;  //Rut Importador.
        public string PrtBco;  //Party Banco.-
        public short IndBco;  //Indice del Banco.-
        public string RutBco;  //Rut Banco.-
        public string SwiBco;  //Swift Banco.-
        public short CodBco;  //Código Contable Banco.-
        public string numcct;  //Número de Cta. Cte.-
        public string numlin;  //Número de Linea de Crédito.-
        public string FecOri;  //Fecha de Origen.-
        public string FecVen;  //Fecha de Vencimiento.-
        public string FecInt;  //Fecha de Inicio Intereses.-
        public short TasFij;  //Tiene Tasa Fija? (o Variable).-
        public double MtoTas;  //Monto Tasa para Tasa Variable.-
        public short OfiDes;  //Oficina Destino.-
        public int NumPar;  //Número de Partida.-
        public short TipMov;  //Tipo Movimiento 1:Crea;2:Recibe.-
        public string NroRef;  //Referencia del Documento.-
        public double TipCam;  //Tipo de Cambio a Moneda Nacional.-
        public int NroTOp;  //Número de TO.
        public short IndTOp;  //Indicador de TO.-
        public short IntCIT;  //Interfaz al CIT.-
        public short IntCVT;  //Interfaz al CVT.-
        public short intcap;  //Interfaz al CAP.-
        public short IntCTD;  //Interfaz al CTD.-
        public short IntPOS;  //Interfaz al POS.-
        public short IntCDR;  //Interfaz al CDR.-
        public short McdVig;  //Movimiento vigente?.-

        public T_Mcd Copy()
        {
            return (T_Mcd)this.MemberwiseClone();
        }
    }
    public class T_MODGCON0
    {
        public T_MODGCON0()
        {
            VMch = new T_Mch();
            VmchNul = new T_Mch();
            VMcd = new T_Mcd();
            VMcdNul = new T_Mcd();
            VMcds = new T_Mcd[0];
            VMcdz = new T_Mcd[0];
        }

        //--------------------------------------------------------
        public  T_Mch VMch;  //Header  Contabilidad.-
        public  T_Mch VmchNul;  //Header  Contabilidad.-
        public  T_Mcd VMcd;  //Detalle Contabilidad.-
        public  T_Mcd VMcdNul;  //Detalle Contabilidad.-
        public  T_Mcd[] VMcds;  //Arreglo Detalle Contabilidad.-
        public  T_Mcd[] VMcdz;  //Arreglo respaldo Contabilidad

        //--------------------------------------------------------
        //Constantes para las Opers. de Contabilidad.
        //--------------------------------------------------------
        // UPGRADE_INFO (#0561): The 'CONTAB_ING' symbol was defined without an explicit "As" clause.
        public const string CONTAB_ING = "I";
        //--------------------------------------------------------
        //Estados para el Comprobante Contable.
        //--------------------------------------------------------
        // UPGRADE_INFO (#0561): The 'ECC_ING' symbol was defined without an explicit "As" clause.
        public const short ECC_ING = 1;
        //Estado Ingresado.
        // UPGRADE_INFO (#0561): The 'ECC_ANU' symbol was defined without an explicit "As" clause.
        public const short ECC_ANU = 9;
        //Estado Anualdo.
        

        // UPGRADE_INFO (#0561): The 'MsgCon' symbol was defined without an explicit "As" clause.
        public const string MsgCon = "Contabilidad";
        //--------------------------------------------------------
        //Identificadores de Cuentas Contables Generales.-
        //--------------------------------------------------------
        // UPGRADE_INFO (#0561): The 'IdCta_CtaCteMN' symbol was defined without an explicit "As" clause.
        public const short IdCta_CtaCteMN = 3;
        //Cuenta Corriente M/N
        // UPGRADE_INFO (#0561): The 'IdCta_VVBCH' symbol was defined without an explicit "As" clause.
        public const short IdCta_VVBCH = 4;
        //Vale Vista Bco. Chile
        // UPGRADE_INFO (#0561): The 'IdCta_VVOB' symbol was defined without an explicit "As" clause.
        public const short IdCta_VVOB = 5;
        //Vale Vista Otro Banco
        // UPGRADE_INFO (#0561): The 'IdCta_CTAPTEMN' symbol was defined without an explicit "As" clause.
        public const short IdCta_CTAPTEMN = 8;
        //Cuenta Puente M/N
        // UPGRADE_INFO (#0561): The 'IdCta_SCSMN' symbol was defined without an explicit "As" clause.
        public const short IdCta_SCSMN = 9;
        //Saldos Con Sucursales M/N
        // UPGRADE_INFO (#0561): The 'IdCta_CtaCteME' symbol was defined without an explicit "As" clause.
        public const short IdCta_CtaCteME = 10;
        //Cuenta Corriente M/E
        // UPGRADE_INFO (#0561): The 'IdCta_CHMEBCH' symbol was defined without an explicit "As" clause.
        public const short IdCta_CHMEBCH = 11;
        //Cheque M/E Emi. x B. Chile
        // UPGRADE_INFO (#0561): The 'IdCta_CHMEONY' symbol was defined without an explicit "As" clause.
        public const short IdCta_CHMEONY = 12;
        //Cheque M/E cliente of. N.Y.
        // UPGRADE_INFO (#0561): The 'IdCta_CHMEOBC' symbol was defined without an explicit "As" clause.
        public const short IdCta_CHMEOBC = 13;
        //Cheque M/E Otro B (Chile)
        // UPGRADE_INFO (#0561): The 'IdCta_CHMEOBE' symbol was defined without an explicit "As" clause.
        public const short IdCta_CHMEOBE = 14;
        //Cheque M/E Otro B (Extr)
        // UPGRADE_INFO (#0561): The 'IdCta_CTAPTEME' symbol was defined without an explicit "As" clause.
        public const short IdCta_CTAPTEME = 15;
        //Cheque Puente M/E
        // UPGRADE_INFO (#0561): The 'IdCta_SCSME' symbol was defined without an explicit "As" clause.
        public const short IdCta_SCSME = 16;
        //Saldos Con Sucursales M/E
        // UPGRADE_INFO (#0561): The 'IdCta_OPC' symbol was defined without an explicit "As" clause.
        public const short IdCta_OPC = 17;
        //Orden de Pago Convenio
        // UPGRADE_INFO (#0561): The 'IdCta_OPOP' symbol was defined without an explicit "As" clause.
        public const short IdCta_OPOP = 18;
        //Orden de Pago Otros Países
        // UPGRADE_INFO (#0561): The 'IdCta_VAM' symbol was defined without an explicit "As" clause.
        public const short IdCta_VAM = 19;
        //Varios Acreedores Import.
        // UPGRADE_INFO (#0561): The 'IdCta_VAX' symbol was defined without an explicit "As" clause.
        public const short IdCta_VAX = 20;
        //Varios Acreedores Export.
        // UPGRADE_INFO (#0561): The 'IdCta_VAMC' symbol was defined without an explicit "As" clause.
        public const short IdCta_VAMC = 21;
        //Varios Acreedores Mcdo. Corr.
        // UPGRADE_INFO (#0561): The 'IdCta_CTACTEBC' symbol was defined without an explicit "As" clause.
        public const short IdCta_CTACTEBC = 22;
        //Cta. Cte. Banco Central
        // UPGRADE_INFO (#0561): The 'IdCta_CTAORD' symbol was defined without an explicit "As" clause.
        public const short IdCta_CTAORD = 23;
        //Corresponsal cuenta ordinaria
        // UPGRADE_INFO (#0561): The 'IdCta_DIVENPEN' symbol was defined without an explicit "As" clause.
        public const short IdCta_DIVENPEN = 24;
        //Divisas Pendientes.-
        // UPGRADE_INFO (#0561): The 'IdCta_OPEPEND' symbol was defined without an explicit "As" clause.
        public const short IdCta_OPEPEND = 25;
        //Cuenta Divisas Pendientes.-
        // UPGRADE_INFO (#0561): The 'IdCta_VAMCC' symbol was defined without an explicit "As" clause.
        public const short IdCta_VAMCC = 26;
        //Cuenta Divisas Pendientes.-
        // UPGRADE_INFO (#0561): The 'IdCta_VASC' symbol was defined without an explicit "As" clause.
        public const short IdCta_VASC = 27;
        //Varios Acreedores Servicio Comex.-
        // UPGRADE_INFO (#0561): The 'IdCta_CHVBNYM' symbol was defined without an explicit "As" clause.
        public const short IdCta_CHVBNYM = 29;
        //Cheque VºBº B. Chile N.Y. - Miami
        // UPGRADE_INFO (#0561): The 'IdCta_OBLACP' symbol was defined without an explicit "As" clause.
        public const short IdCta_OBLACP = 30;
        //Obligación aceptación
        // UPGRADE_INFO (#0561): The 'IdCta_BOEREG' symbol was defined without an explicit "As" clause.
        public const short IdCta_BOEREG = 31;
        //Bco. Centarl Regiones
        // UPGRADE_INFO (#0561): The 'IdCta_CHEREG' symbol was defined without an explicit "As" clause.
        public const short IdCta_CHEREG = 32;
        //Cheque M/E Regiones
        // UPGRADE_INFO (#0561): The 'IdCta_OBLREG' symbol was defined without an explicit "As" clause.
        public const short IdCta_OBLREG = 33;
        //Obligación O/Bcos. Regiones
        // UPGRADE_INFO (#0561): The 'IdCta_OBLARE' symbol was defined without an explicit "As" clause.
        public const short IdCta_OBLARE = 34;
        //Obligación Aladi Regiones
        // UPGRADE_INFO (#0561): The 'IdCta_ACEREG' symbol was defined without an explicit "As" clause.
        public const short IdCta_ACEREG = 35;
        //Bco Corresponsal Regiones
        // UPGRADE_INFO (#0561): The 'IdCta_CHVRF' symbol was defined without an explicit "As" clause.
        public const short IdCta_CHVRF = 54;
        //Check Verification
        // UPGRADE_INFO (#0561): The 'IdCta_ONMN' symbol was defined without an explicit "As" clause.
        public const short IdCta_ONMN = 56;
        //Otro Nemónico M/N
        // UPGRADE_INFO (#0561): The 'IdCta_CtaCteAUTN' symbol was defined without an explicit "As" clause.
        public const short IdCta_CtaCteAUTN = 62;
        //Cuenta Corriente Automatica Moneda Nacional
        // UPGRADE_INFO (#0561): The 'IdCta_CtaCteAUTE' symbol was defined without an explicit "As" clause.
        public const short IdCta_CtaCteAUTE = 63;
        //Cuenta Corriente Automatica Moneda Extranjera
        // UPGRADE_INFO (#0561): The 'IdCta_CtaCteMANN' symbol was defined without an explicit "As" clause.
        public const short IdCta_CtaCteMANN = 64;
        //Cuenta Corriente Manual Moneda Nacional
        // UPGRADE_INFO (#0561): The 'IdCta_CtaCteMANE' symbol was defined without an explicit "As" clause.
        public const short IdCta_CtaCteMANE = 65;
        //Cuenta Corriente Manual Moneda Extranjera
        // UPGRADE_INFO (#0561): The 'IdCta_GAPMN' symbol was defined without an explicit "As" clause.
        public const short IdCta_GAPMN = 66;
        //Cuenta GAP moneda nacional
        // UPGRADE_INFO (#0561): The 'IdCta_GAPME' symbol was defined without an explicit "As" clause.
        public const short IdCta_GAPME = 67;
        //Cuenta GAP moneda extranjera
        // UPGRADE_INFO (#0561): The 'IdBRANCH_NUMBER' symbol was defined without an explicit "As" clause.
        public const short IdBRANCH_NUMBER = 152;
        //Identificador OPeración Manual BRANCH_NUMBER
        // UPGRADE_INFO (#0561): The 'IdCta_ChqCCME' symbol was defined without an explicit "As" clause.
        public const short IdCta_ChqCCME = 55;
        //Cheque Cuenta Corriente M/E
        // UPGRADE_INFO (#0561): The 'IdCta_ONME' symbol was defined without an explicit "As" clause.
        public const short IdCta_ONME = 60;
        //Otro Nemónico M/E
        // UPGRADE_INFO (#0561): The 'IdCta_BcoCor' symbol was defined without an explicit "As" clause.
        public const short IdCta_BcoCor = 100;
        //Banco Corresponsal.-

        //--------------------------------------------------------
        //Constantes Generales para Formatear Montos.-
        //--------------------------------------------------------
        // UPGRADE_INFO (#0561): The 'FormatoConDec' symbol was defined without an explicit "As" clause.
        public const string FormatoConDec = "#,###,###,###,##0.00";
        // UPGRADE_INFO (#0561): The 'FormatoSinDec' symbol was defined without an explicit "As" clause.
        public const string FormatoSinDec = "#,###,###,###,##0";
        // UPGRADE_INFO (#0561): The 'CodBcoBC' symbol was defined without an explicit "As" clause.
        public const short CodBcoBC = 24;
    }
}
