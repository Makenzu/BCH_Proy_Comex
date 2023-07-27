using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    //*************************************************
    //Variables Generales de Compra Venta.
    //*************************************************
    public class T_gCVD
    {
        public string codcct = string.Empty;  //Centro de Costo.
        public string codpro = string.Empty;  //Código Producto.
        public string codesp = string.Empty;  //Código Especialista.
        public string codofi = string.Empty;  //Código Empresa.
        public string codope = string.Empty;  //Código Operación.
        public string cencos = string.Empty;
        public string codusr = string.Empty;
        public string Fecing = string.Empty;
        public string FecAct = string.Empty;
        public short TipCVD;  //1:CVD;2:Arb;3:Vis/Exp;4:Inv/Imp.
        public short estado;  //Estado.
        public string operel;  //Operación asociada.
        public string PrtCli = string.Empty;
        public string rutcli = string.Empty;  //Rut del Cliente
        public short IndNomC;
        public short IndDirC;
        public string IndPopC = string.Empty;
        public string PrtOtr = string.Empty;
        public short IndNomO;
        public short IndDirO;
        public string IndPopO = string.Empty;
        //---------------------------------------------
        public string Etapa = string.Empty;  //Etapas antes de Grabar.-
        public short IndPrt;  //Indice Party en PartysOpe.
        public string OpeSin = string.Empty;  //Operación Sin Raya.
        public string OpeCon = string.Empty;  //Operación Con Raya.
        public string AvisoDC = string.Empty;  //Aviso Débito-Crédito.
        public short DocCVD;  //Carta Compra Venta.
        public short DocArb;  //Carta Arbitraje.
        public short DocPln;  //Carta de Planillas.-
        public short DocPlSO;  //Carta de Planillas sin operacion.
        public short Acepto;  //Indica si aceptó la Operación.
        public short AceptoRev;  //Indica si aceptó el Reversar una Operación.
        public string TcpConDec = string.Empty;  //Indica Tcp con Declaración.
        public string TcpSinPai = string.Empty;  //Indica Tcp sin País requerido.
        public string TcpConvenio = string.Empty;  //Indica Convenio Crédito Recíproco.
        public string TcpAutBcoCen = string.Empty;  //Indica si necesita autorización del banco central.-
        public short InsExp;
        public string RefCli = string.Empty;  //Referencia Cliente.-
        public short AnuSin;  //Indica anulación sin operación original.-
        public short DocCvdI;  //Número del documento de compra venta.
        public short filler;  //Campo de uso generico.
        public short Futuro;  //Si es arbitraje a futuro

        public T_gCVD Copy()
        {
            return (T_gCVD)this.MemberwiseClone();
        }
    }

    //***********************************************
    //Variables Generales Compra-Venta.
    //***********************************************
    public class T_gPli
    {
        public short numcor;  //Correlativo.
        public string TipCVD;  //C:Compra; V:Venta-Imp;W:Venta Exp.-
        public short codpai;  //País.
        public short CodMnd;  //Moneda.
        public short MndCBC;  //Moneda Mnd. Bco. Central.
        public string NemMnd;  //Nemónico Moneda.
        public double MtoCVD;  //Monto Compra-Venta.
        public double TipCam;  //Tipo de Cambio.
        public double MtoPes;  //Monto en Pesos.
        public double Mtopar;  //Monto Paridad.
        public string CodTcp;  //Concepto Planilla.
        public short CodOci;  //Operación Comercio Invisible.
        public string IngEgr;  //I:Ingreso;E:Egreso.
        public short Conven;  //Pertenece al Convenio?.
        public short Status;  //Estado Eli:Ing:Mod.
        public string numdec;  //Nro. Declaración.
        public string FecDec;  //Fecha Declaración.
        public short CodAdn;  //Código de Aduana.
        public string CodEOR;  //Código de reexportación
        public string DieNum;  //Nro, Informe.
        public string DieFec;  //Fecha de Informe.
        public short DiePbc;  //Código de Plaza Banco Central.
        public short IndDec;  //Indica si corresponde a una dec.
        public string FecDeb;  //Fecha Autorización Débito.
        public string DocNac;  //Documento Nacional.
        public string DocExt;  //Documento extranjero.
        public string ApcTip;
        public string ApcNum;  //Número de Autorización Previa del Banco Central.
        public string ApcFec;  //Fecha de Autorización Previa del Banco Central.
        public short ApcPbc;  //Código de Plaza Banco Central(Autorización Previa del Banco Central).
        public short NumAcu;
        public string Desacu;
        public double NumCre;  //Número del Crédito.
        public string FecCre;  //Fecha  del Crédito.
        public short MndCre;  //Moneda del Crédito.
        public double MtoCre;  //Monto  del Crédito.
        public double DatImp;  //Datos Impuesto.
        public string fecins;  //Fecha Inscripción.
        public string NomFin;  //Nombre Financista.
        public string FecVenCp;  //Fecha Vencimiento Capital.
        public string AnuFec;
        public int AnuNum;
        public short AnuPbc;
        public int NumCon;
        public string fecsus;
        public string VenOfi;
        public string VenOd;
        public short insuti;
        public double partip;
        public short arecon;
        public int canacu;
        public short afeder;
        public short SecBen;
        public short SecInv;
        public short ZonFra;
        public double PrcPar;
    }

    //Variables Generales para Anulación s/Operación.-
    public class T_Planau
    {
        public short Moneda;  //Moneda.-
        public double TipCam;  //Tipo de Cambio.-
        public short Nropla;  //Número de Planillas.-
    }
    public class T_MODGCVD
    {
        // UPGRADE_INFO (#0561): The 'FormatoDec' symbol was defined without an explicit "As" clause.
        public const string FormatoDec = "#,###,###,###,##0.00";

        public const string MsgCVD = "Fund Transfer";

        //Tipos de Operaciones de Compra Venta.
        // UPGRADE_INFO (#0561): The 'TCvd_CVD' symbol was defined without an explicit "As" clause.
        public const short TCvd_CVD = 1;
        //Compra y Venta (Invisible).
        // UPGRADE_INFO (#0561): The 'TCvd_Arb' symbol was defined without an explicit "As" clause.
        public const short TCvd_Arb = 2;
        //Arbitraje (Invisible).
        // UPGRADE_INFO (#0561): The 'TCvd_VisExp' symbol was defined without an explicit "As" clause.
        public const short TCvd_VisExp = 3;
        //Visible Exportaciones.
        // UPGRADE_INFO (#0561): The 'TCvd_VisImp' symbol was defined without an explicit "As" clause.
        public const short TCvd_VisImp = 4;
        //Visible Importaciones.
        // UPGRADE_INFO (#0561): The 'TCvd_PlnoBco' symbol was defined without an explicit "As" clause.
        public const short TCvd_PlnoBco = 5;
        //Planillas anuladas otro banco.
        // UPGRADE_INFO (#0561): The 'TCvd_PlnVEst' symbol was defined without an explicit "As" clause.
        public const short TCvd_PlnVEst = 6;
        //Planillas estadisicas visibles importaciones.
        // UPGRADE_INFO (#0561): The 'TCvd_Anu' symbol was defined without an explicit "As" clause.
        public const short TCvd_Anu = 10;
        //Anulación.
        // UPGRADE_INFO (#0561): The 'TCvd_Rev' symbol was defined without an explicit "As" clause.
        public const short TCvd_Rev = 11;
        //Reverso.
        // UPGRADE_INFO (#0561): The 'TCvd_RyR' symbol was defined without an explicit "As" clause.
        public const short TCvd_RyR = 12;
        //Reverso y Reemplazo.
        // UPGRADE_INFO (#0561): The 'TCvd_AnuVisI' symbol was defined without an explicit "As" clause.
        public const short TCvd_AnuVisI = 13;
        //Anulacion planillas Visibles Import.
        // UPGRADE_INFO (#0561): The 'TCvd_PlanSO' symbol was defined without an explicit "As" clause.
        public const short TCvd_PlanSO = 14;

        //Planillas de Transferencia.

        //Estados de las Operaciones de Compra Venta.
        // UPGRADE_INFO (#0561): The 'ECvd_Ing' symbol was defined without an explicit "As" clause.
        public const short ECvd_Ing = 1;
        //Ingresada.
        // UPGRADE_INFO (#0561): The 'ECvd_Anu' symbol was defined without an explicit "As" clause.
        public short ECvd_Anu = 9;

        public const short ICli = 0;
        public const short IOtr = 1;
        public const short ICom = 2;

        //Constantes Generales de Status.
        public const short EstadoIng = 1;
        public const short EstadoMod = 2;
        public const short EstadoEli = 3;

        public const string PrtOpc = "02";

        public short CodPlazaCentral;
        public short CodMonedaNac;
        public short CodMonDolar;
        public short CodMonedaNacional;

        public string[] Beneficiario;

        public bool COMISION;
        public bool TIN;
        public bool NOTACRED;


        public short BotPrt;
        public T_gCVD VgCvd;
        public T_gCVD VgCVDo;  //Operación Original Relacionada.-
        public T_gCVD VgCVDNul;
        public T_gCVD VgCVDVacia;

        public T_gPli[] VgPli;

        // Se definen variables globales para almacenar los últimos
        // movimientos generardos en una compra/venta
        // ----------------------------------------------

        public short AntOper;
        public short Antmon;
        public double Anttip;

        // UPGRADE_INFO (#0561): The 'PrtOpc' symbol was defined without an explicit "As" clause.

        public string RutwAis;
        public T_Planau VxPlaAnu;


        public T_MODGCVD()
        {
            Beneficiario = new string[2] { String.Empty, String.Empty };
            VxPlaAnu = new T_Planau();
            VgCvd = new T_gCVD();
            VgCVDo = new T_gCVD();
            VgCVDNul = new T_gCVD();
            VgCVDVacia = new T_gCVD();
            VgPli = new T_gPli[0];
        }
    }
}
