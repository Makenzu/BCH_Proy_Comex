using BCH.Comex.Core.Entities.Cext01.Common;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class T_Cmd
    {
        public string Cmd;  //Query.-
        public string Tab;  //Tabla.-
        public short Brk;  //Quiebre.-
    }
    //Estructura para cargar comisiones
    public class T_gCom
    {
        public string GlsCom;
        public string NemMnd;
        public double MtoCom;
        public short ConIVA;
        public double MtoIva;
        public double MtoTot;
        public double TipCam;
        public double MtoComp;
        public double MtoIvap;
        public double MtoTotp;
        public string NemCta;
        public short estado;
    }

    //Variables Generales de Comision.-
    public class T_wCom
    {
        public short Acepto;
        public double TipCam;
    }

    //Datos adicionales de las planillas estadísticas
    public class T_AdiPli
    {
        public double NumCre;  //Número del Crédito.
        public string FecCre;  //Fecha  del Crédito.
        public short MndCre;  //Moneda del Crédito.
        public double MtoCre;  //Monto  del Crédito.
        public double DatImp;  //Datos Impuesto.
        public string fecins;  //Fecha Inscripción.
        public string NomFin;  //Nombre Financista.
        public string FecVen;  //Fecha Vencimiento Capital.
        //Datos Para CE Retornados
        public short codpai;  //Pais Acreedor
    }

    public class T_AreCon
    {
        public string CodACon;
        public string DesACon;
    }

    public class T_VInstru
    {
        public string CodIntr;
        public string DesIntr;
    }

    public class T_VTipAut
    {
        public string CodAut;
        public string DesAut;
    }

    //------------------------------------------------------
    //Variable Declaración Exportación general.-
    //------------------------------------------------------
    public class TypexDec
    {
        public string numdec;  //Número Declaración.-
        public string FecDec;  //Fecha  Declaración.-
        public short CodAdn;  //Código Aduana.-
        public short estado;  //Estado Dec.-
        public short TipDec;  //Tipo   Dec.-
        public short CodCCv;  //Clausula Venta.-
        public string RutExp1;  //Rut        Exp. Ppal.-
        public string PrtExp1;  //Party      Exp. Ppal.-
        public short IndNom1;  //Nombre     Exp. Ppal.-
        public short IndDir1;  //Dirección  Exp. Ppal.-
        public double Porcen1;  //Porcentaje Exp. Ppal.-
        //--------------------------------------------------
        public double ValRet1;  //Valores    Exp. Ppal.-
        public double ValCom1;
        public double ValGas1;
        public double ValLiq1;
        public double ValFle1;
        public double ValSeg1;
        //--------------------------------------------------
        public double ValRet1c;  //Valores    Exp. Ppal.-
        public double ValCom1c;
        public double ValGas1c;
        public double ValLiq1c;
        public double ValFle1c;
        public double ValSeg1c;
        //--------------------------------------------------
        public string RutExp2;  //Rut        Exp. Sec.-
        public string PrtExp2;  //Party      Exp. Sec.-
        public short IndNom2;  //Nombre     Exp. Sec.-
        public short IndDir2;  //Dirección  Exp. Sec.-
        public double Porcen2;  //Porcentaje Exp. Sec.-
        //--------------------------------------------------
        public double ValRet2;  //Valores    Exp. Sec.-
        public double ValCom2;
        public double ValGas2;
        public double ValLiq2;
        public double ValFle2;
        public double ValSeg2;
        //--------------------------------------------------
        public double ValRet2c;  //Valores    Exp. Sec.-
        public double ValCom2c;
        public double ValGas2c;
        public double ValLiq2c;
        public double ValFle2c;
        public double ValSeg2c;
        //--------------------------------------------------
        public short DiaRet;  //Plazo  máximo Retorno.-
        public string FecRet;  //Fecha  máxima Retorno.-
        public short CodPbc;  //Plaza  B. Central.-
        public string NumInf;  //Número Informe.-
        public string FecInf;  //Fecha  Informe.-
        //--------------------------------------------------
        public string Fecing;  //Fecha Ingreso.-
        public string FecAct;  //Fecha últ. mod.-
        public short ConTrs;  //Traspasada?.-
        public short EnLock;  //Con bloqueo?.-
        public string cencos;  //CCosto usuario crea.-
        public string codusr;  //Id.    usuario crea.-
        //--------------------------------------------------
        public double ValDis1;  //Disponible Exp1.-
        public double ValDis2;  //Disponible Exp2.-
    }
    //----------------------------------------------------------------------------
    //Arreglo en memoria para mantener nombres de partys.-
    //----------------------------------------------------------------------------
    public class Type_DatPrty
    {
        public string PrtImp;  //Llave Party.-
        public short IndNom;  //Indice Nombre.-
        public short IndDir;  //Indice Direccion.-
        public string NomPrty;  //Nombre Party.-
        public string DirPrty;  //Direccion Party.-
    }

    public class TIndViaOri
    {
        public string OriDes;
        public short ind;
        public string Prty;
    }
}

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos 
{
    public class T_Mdl_Funciones_Varias
    {

        public short Li_Largo1;
        public short Li_Largo2;
        public T_gCom[] V_gCom;
        public T_wCom WgCom;
        public const string MsggCom = "Comisiones Compra-Venta Divisas";
        public T_Cmd[] CmdsQuery;
        public List<Func<short>> CmdsQuerysNew;

        //------------------------------------------------------------------------------------------
        //Constantes para codigo del producto relacionado con tablas: sce_trng y tabla sce_rng.-
        //---------------------------------------------------------------------------------------
        public const string cod_producto = "FTC";
        public const string cod_producto_CVD = "CVI";

        //Fund Transfer Comercio
        //----------------------------------------------------------------------------
        //Constantes para las Operaciones con DDE.-
        //----------------------------------------------------------------------------
        public const short DDE_AUTOMATIC = 1;
        public const short DDE_MANUAL = 2;
        public const short DDE_NONE = 0;
        public const short DDE_OTRO = 3;

        public T_AdiPli VaPli;
        public T_AdiPli VaxPlv;  //Adicional para CER
        public T_AdiPli VaPliNul;
        public T_AreCon[] V_AreCon;
        public T_VInstru[] V_Instru;
        public T_VTipAut[] V_TipAut;
        //public static Dictionary<DateTime, T_VTipAut[]> V_TipAutCache = new Dictionary<DateTime,T_VTipAut[]>();
        public TypexDec VxDec;  //Var. Gen. Dec.-
        public TypexDec VxDecNul;  //Var. Gen. Dec Nula.-
        public TypexDec VxDecVacia;  //Var. Gen. Dec Vacía.-

        //Constantes para el Tipo de Cláusula de Compra-Venta.
        public const short TCcv_CIF = 1;
        //CIF.
        public const short TCcv_CYF = 2;
        //C Y F.
        public const short TCcv_EXF = 3;
        //Ex-Fca.
        public const short TCcv_FAS = 4;
        //FAS.
        public const short TCcv_FOB = 5;
        //Fob.
        public const short TCcv_FOF = 6;
        //Fob-Frontera.
        public const short TCcv_CYS = 7;
        //C y S.
        public const short TCcv_OTR = 8;
        //Otro.
        public const short TCcv_DDP = 9;

        //Delivered Duty Paid.
        public Type_DatPrty[] DatPrtys;

        //----------------------------------------------------------------------------
        //Indicadores de Impresión (Cartas).
        public const short DocxAceLet = 602;
        //Aceptación de Letras.
        public const short DocxCobReg = 601;
        //Registro Cobranza Export.
        public const short DocxCobRen = 603;
        //Reenvío Cobranza Export.
        public const short DocxPagDir = 610;
        //Pago Directo Cobranza Export.
        public const short DocxCobCan = 611;
        //Cancelación Cobranza Export.
        public const short DocxCanRet = 611;
        //Cancelación Retorno Export.
        public const short DocxRegRet = 612;
        //Registro Retorno.
        public const short DocxRegPln = 613;
        //Planillas Visible Export.
        public const short DocxRegCanRet = 614;
        //Registro y Cancelación Retorno Export.
        public const short DocCVD = 620;
        //Compra Venta.
        public const short DocArb = 621;
        //Arbitraje.
        public const short DocCvdI = 402;
        //Ventas Visibles Import.-
        private const short DocGAdeb = 998;//Aviso de Crédito.
        private const short DocGAcre = 999;//Aviso de Débito.
        //Carta de Crédito Export.
        public const short Doc901 = 901;
        //Aviso al Exportador Avisada s/sol conf.
        public const short Doc902 = 902;
        //Consulta de Banco Aladi confirmable a Corresponsales
        public const short Doc903 = 903;
        //Aviso a través de otro Banco Nacional
        public const short Doc904 = 904;
        //Aviso a Beneficiario de Confirmación de Carta de Crédito Disponible por...
        public const short Doc905 = 905;
        //Aviso de Rechazo    Confirmación.-
        public const short Doc906 = 906;
        //Aviso de Aceptación Confirmación.-
        public const short Doc907 = 907;
        //Modificación de CCE a Beneficiarios Tr.-
        public const short Doc908 = 908;
        //Aviso de Transferencia de CCE a Beneficiario Traspasado.-
        public const short Doc909 = 909;
        //Aviso de Traspaso de CCE a Banco de Traspaso.-
        public const short Doc910 = 910;
        //Saldo de Transferencia al BO.-
        public const short Doc911 = 911;
        //Aviso de Modificación al B. O. de la CCE
        public const short Doc912 = 912;
        //Aviso de Traspaso de CCE a Beneficiario Transferido.-
        public const short Doc913 = 913;
        //Aviso de Modificación Banco de Traspaso .-
        public const short Doc914 = 914;
        public const short Doc915 = 915;
        //Aviso de Comisiones.-
        public const short Doc916 = 916;
        public const short Doc917 = 917;
        public const short Doc918 = 918;
        //"Recha Avance s/Conf"
        public const short Doc920 = 920;
        //Solicitud de Confirmación.-
        public const short Doc930 = 930;
        //Aviso de Modificación al Banco Avisador.-
        public const short Doc950 = 950;
        //Remesa al Banco Emisor de Documentos Negociados.
        public const short Doc951 = 951;
        //Remesa al Banco Emisor de Documentos Negociados.
        public const short Doc952 = 952;
        //Carta de Cancelación Carta de Crédito.
        public const short Doc953 = 953;
        public const short Doc954 = 954;
        //Carta al Banco Reembolsante.
        public const short Doc955 = 955;
        //Carta aviso al beneficiario alza Discrepancias.
        public const short Doc801 = 801;
        //"Compra Anticipada L/C"
        public const short Doc802 = 802;


        //"Memo Banco Acreedor"
        //******************************
        public short IExp;
        public short CARGA_AUTOMATICA;  //VARIABLE QUE DETERMINA SI SE ESTA REALIZANDO UNA CARGA AUTOMATICA
        public short LC_MONEDA;  //CODIGO MONEDA
        public string LC_MONTO = "";  //MONTO
        public string LC_XREF = "";  //XREFerencia
        public string LC_CONREFNUM = "";  //Contract Reference Number Automatico
        public string LC_PRD = "";  //IDENTIFICACION PARTICIPANTE
        public string LC_OUTGOING = "";  //072 OUTGOING
        public string LC_INCOMING = "";  //074 - 062 INCOMING
        public string LC_ORD_INST1 = "";  //DESTINO FONDOS
        public string LC_PMNT_DET1 = "";  //DESTINO FONDOS
        public string LC_PMNT_DET2 = "";  //DESTINO FONDOS
        public string LC_PMNT_DET3 = "";  //DESTINO FONDOS
        public string LC_PMNT_DET4 = "";  //DESTINO FONDOS
        public string Lc_BaseNumber = "";  //BASENAMBER
        public string LC_COD_TRANS = "";  //CODIGO TRANSACCION
        public string LC_TRXID_MAN = "";  //TRANSACTION ID MANUAL
        public string LC_DEBIT_REF = "";  //ORIGEN FONDOS
        public string LC_SWFT = "";  //ORIGEN DE FONDOS
        public string LC_BEN_INST1 = "";  //BEN_INST1
        public string LC_ULT_BEN1 = "";  //ULT_BEN1
        public string LC_ULT_BEN2 = "";  //ULT_BEN2
        public string LC_ULT_BEN3 = "";  //ULT_BEN3
        public string LC_ULT_BEN4 = "";  //ULT_BEN4
        public string LC_CHG_WHOM = "";  //CHG_WHOM
        public string LC_FCCFT = "";  //FCCFT
        public string LC_DRVALDT = "";  //DRVALDT
        public string LC_NOM_MDA = "";  //NOMBRE MONEDA
        public string LC_INTRMD1 = "";  //INTRMD1
        public string LC_INTRMD2 = "";  //INTRMD2
        public string LC_RECVR_CORRES1 = "";  //RECVR_CORRES1
        public string LC_RECVR_CORRES2 = "";  //RECVR_CORRES1
        public string LC_US_PAY_ID = "";
        public string LC_SNDR_RECVR_INFO1 = "";
        public string LC_SNDR_RECVR_INFO2 = "";
        public string LC_SNDR_RECVR_INFO3 = "";
        public string LC_SNDR_RECVR_INFO4 = "";
        public string LC_SNDR_RECVR_INFO5 = "";
        public string LC_SNDR_RECVR_INFO6 = "";

        public string LC_CODTRAN_ORI = "";
        public string LC_CODTRAN_DES = "";

        //************************************
        //VARIABLES RETORNO SERVICIO 0027
        public string RUT_PARTY_SERV = "";
        public string NOM_PARTY_SERV = "";
        public string TIP_CTA_SERV = "";
        public short CARGA_PARTY;
        public string LC_BASENUMBER_NUEVO = "";
        public TIndViaOri[] GT_IndViaOri;

        public T_Mdl_Funciones_Varias()
        {
            DatPrtys = new Type_DatPrty[0];
            CmdsQuerysNew = new List<Func<short>>();
            IExp = 0;
            CARGA_AUTOMATICA = 0;  //VARIABLE QUE DETERMINA SI SE ESTA REALIZANDO UNA CARGA AUTOMATICA
            LC_MONEDA = 0;  //CODIGO MONEDA
            LC_MONTO = "";  //MONTO
            LC_XREF = "";  //XREFerencia
            LC_CONREFNUM = "";  //Contract Reference Number Automatico
             LC_PRD = "";  //IDENTIFICACION PARTICIPANTE
             LC_OUTGOING = "";  //072 OUTGOING
             LC_INCOMING = "";  //074 - 062 INCOMING
             LC_ORD_INST1 = "";  //DESTINO FONDOS
             LC_PMNT_DET1 = "";  //DESTINO FONDOS
             LC_PMNT_DET2 = "";  //DESTINO FONDOS
             LC_PMNT_DET3 = "";  //DESTINO FONDOS
             LC_PMNT_DET4 = "";  //DESTINO FONDOS
             Lc_BaseNumber = "";  //BASENAMBER
             LC_COD_TRANS = "";  //CODIGO TRANSACCION
             LC_TRXID_MAN = "";  //TRANSACTION ID MANUAL
             LC_DEBIT_REF = "";  //ORIGEN FONDOS
             LC_SWFT = "";  //ORIGEN DE FONDOS
             LC_BEN_INST1 = "";  //BEN_INST1
             LC_ULT_BEN1 = "";  //ULT_BEN1
             LC_ULT_BEN2 = "";  //ULT_BEN2
             LC_ULT_BEN3 = "";  //ULT_BEN3
             LC_ULT_BEN4 = "";  //ULT_BEN4
             LC_CHG_WHOM = "";  //CHG_WHOM
             LC_FCCFT = "";  //FCCFT
             LC_DRVALDT = "";  //DRVALDT
             LC_NOM_MDA = "";  //NOMBRE MONEDA
             LC_INTRMD1 = "";  //INTRMD1
             LC_INTRMD2 = "";  //INTRMD2
             LC_RECVR_CORRES1 = "";  //RECVR_CORRES1
             LC_RECVR_CORRES2 = "";  //RECVR_CORRES1
             LC_US_PAY_ID = "";
             LC_SNDR_RECVR_INFO1 = "";
             LC_SNDR_RECVR_INFO2 = "";
             LC_SNDR_RECVR_INFO3 = "";
             LC_SNDR_RECVR_INFO4 = "";
             LC_SNDR_RECVR_INFO5 = "";
             LC_SNDR_RECVR_INFO6 = "";

             LC_CODTRAN_ORI = "";
             LC_CODTRAN_DES = "";

            //************************************
            //VARIABLES RETORNO SERVICIO 0027
             RUT_PARTY_SERV = "";
             NOM_PARTY_SERV = "";
             TIP_CTA_SERV = "";
             CARGA_PARTY =0;
            LC_BASENUMBER_NUEVO = "";
            GT_IndViaOri = new TIndViaOri[0];

            VaPli = new T_AdiPli();
            VaxPlv = new T_AdiPli();  //Adicional para CER
            VaPliNul = new T_AdiPli();
            V_AreCon = new T_AreCon[0];
            V_Instru = new T_VInstru[0];
            V_TipAut = new T_VTipAut[0];
            VxDec = new TypexDec();  //Var. Gen. Dec.-
            VxDecNul = new TypexDec();  //Var. Gen. Dec Nula.-
            VxDecVacia = new TypexDec();  //Var. Gen. Dec Vacía.-
             V_gCom = new T_gCom[0];
            WgCom = new T_wCom();
        }

        public static string GetTipoCartaDesc(short tipoCarta)
        {
            string desc = "";

            //--------------Etapa de la Carta-----------------
            if (tipoCarta == DocxAceLet)
            {
                desc = "Aceptación";
            }
            else if (tipoCarta == DocxCobReg)
            {
                desc = "Registro";
            }
            else if (tipoCarta == DocxCobRen)
            {
                desc = "Reenvío";
            }
            else if (tipoCarta == DocxPagDir)
            {
                desc = "Cancelación";
            }
            else if (tipoCarta == DocxCobCan)
            {
                desc = "Cancelación";
            }
            else if (tipoCarta == DocxCanRet)
            {
                desc = "Canc. Retorno";
            }
            else if (tipoCarta == DocxRegRet)
            {
                desc = "Reg. Retorno";
            }
            else if (tipoCarta == DocxRegCanRet)
            {
                desc = "Reg. Retorno";
            }
            else if (tipoCarta == DocxRegPln)
            {
                desc = "Planillas";
            }
            else if (tipoCarta == DocGAdeb)
            {
                desc = "Aviso de Débito";
            }
            else if (tipoCarta == DocGAcre)
            {
                desc = "Aviso de Crédito";
            }
            else if (tipoCarta == DocCVD)
            {
                desc = "Compra/Venta";
            }
            else if (tipoCarta == DocCvdI)
            {
                desc = "Vtas. Vis. Import.";
            }
            else if (tipoCarta == DocArb)
            {
                desc = "Arbitraje";
                //Documentos de Carta de Crédito.-
            }
            else if (tipoCarta == Doc901)
            {
                desc = "Registro";
            }
            else if (tipoCarta == Doc902)
            {
                desc = "Cons. de Bco Aladi";
            }
            else if (tipoCarta == Doc903)
            {
                desc = "Aviso otro Bco Nac";
            }
            else if (tipoCarta == Doc904)
            {
                desc = "Aviso de Conf ";
            }
            else if (tipoCarta == Doc905)
            {
                desc = "Aviso de Rech. Conf";
            }
            else if (tipoCarta == Doc906)
            {
                desc = "Aviso de Acep. Conf";
            }
            else if (tipoCarta == Doc907)
            {
                desc = "Modificación";
            }
            else if (tipoCarta == Doc908)
            {
                desc = "Aviso de Transferencia";
            }
            else if (tipoCarta == Doc909)
            {
                desc = "Aviso de Traspaso Bco.";
            }
            else if (tipoCarta == Doc910)
            {
                desc = "Aviso de Saldo";
            }
            else if (tipoCarta == Doc911)
            {
                desc = "Modificación B. O.";
            }
            else if (tipoCarta == Doc912)
            {
                desc = "Aviso de Traspaso B. P.";
            }
            else if (tipoCarta == Doc913)
            {
                desc = "Aviso Mod. Bco. Trasp.";
            }
            else if (tipoCarta == Doc914)
            {
                desc = "Aviso Mod. Bco. Trasp.";
            }
            else if (tipoCarta == Doc915)
            {
                desc = "Aviso de Comisiones";
            }
            else if (tipoCarta == Doc916)
            {
                desc = "Transferencia Rest Esp.";
            }
            else if (tipoCarta == Doc917)
            {
                desc = "Aprob Avance s/Conf";
            }
            else if (tipoCarta == Doc918)
            {
                desc = "Recha Avance s/Conf";
            }
            else if (tipoCarta == Doc920)
            {
                desc = "Solicitud Crédito";
            }
            else if (tipoCarta == Doc950)
            {
                desc = "Utilizac. Bco. Reemb.";
            }
            else if (tipoCarta == Doc951)
            {
                desc = "Utilización Cliente";
            }
            else if (tipoCarta == Doc952)
            {
                desc = "Cancelación Utilización";
            }
            else if (tipoCarta == Doc953)
            {
                desc = "Cancelación Utilización";
            }
            else if (tipoCarta == Doc954)
            {
                desc = "Reembolso";
            }
            else if (tipoCarta == Doc955)
            {
                desc = "Alzamiento Discrepanc.";
            }
            else if (tipoCarta == 611)
            {
                desc = "Cancelación Cob-Ret";
            }
            else if (tipoCarta == 613)
            {
                desc = "";
            }
            else if (tipoCarta == 614)
            {
                desc = "";
            }
            else if (tipoCarta == Doc801)
            {
                desc = "Compra Anticipada L/C";
            }
            else if (tipoCarta == Doc802)
            {
                desc = "Memo Banco Acreedor";
            }
            else
            {
                desc = "***Indeterminado***";
            }

            return desc;
        }

        public static string GetProductoDesc(string codPro)
        {
            string result = String.Empty;
            if (codPro == T_MODGUSR.IdPro_CobExp)
            {
                result = "Exp. Cobranza";
            }
            else if (codPro == T_MODGUSR.IdPro_RetExp)
            {
                result = "Exp. Retorno";
            }
            else if (codPro == T_MODGUSR.IdPro_CobImp)
            {
                result = "Imp. Cobranza";
            }
            else if (codPro == T_MODGUSR.IdPro_CreImp)
            {
                result = "Cart.Cré.Imp.";
            }
            else if (codPro == T_MODGUSR.IdPro_CreCon)
            {
                result = "Cart.Cré.Cont.";
            }
            else if (codPro == T_MODGUSR.IdPro_CreExp)
            {
                result = "Cart.Cré.Exp.";
            }
            else if (codPro == T_MODGUSR.IdPro_PreExp)
            {
                result = "Prés. Exp.";
            }
            else if (codPro == T_MODGUSR.IdPro_ComVen)
            {
                result = "Compra/Venta";
            }
            else if (codPro == T_MODGUSR.IdPro_ConGen)
            {
                result = "GL";
            }
            else if (codPro == T_MODGUSR.IdPro_InfImp)
            {
                result = "Inf. Import.";
            }
            else if (codPro == T_MODGUSR.IdPro_DecImp)
            {
                result = "Dec. Import.";
            }
            else if (codPro == T_MODGUSR.IdPro_PagCCE)
            {
                result = "Compra L/C";
            }
            else if (codPro == T_MODGUSR.IdPro_AvanEx)
            {
                result = "Avance Exp.";
            }

            return result;
        }

        //Busca el nombre del Party; 1º en memoria; 2º a disco.-
        public static string GetDatPrt(string PrtImp, short IndNom, short IndDir, string NomDir)
        {
            string _retValue = "";
            string p = "";
            short i = 0;
            string s1 = "";
            string s2 = "";
            short n = 0;

            /*
            if (PrtImp == "")
            {
                return _retValue;
            }
            p = PrtImp;
            p = PoneMarcaParty(PrtImp);
            i = VB6Helpers.CShort(FindPrt(p, IndNom, IndDir));
            if (i != -1)
            {
                s1 = DatPrtys[i].NomPrty;
                s2 = DatPrtys[i].DirPrty;
            }
            else
            {
                if (IndNom != -1)
                {
                    s1 = VB6Helpers.Trim(SyGet_Rsa(p, IndNom));
                }
                if (IndDir != -1)
                {
                    s2 = VB6Helpers.Trim(SyGet_Dad(p, IndDir));
                }
                n = (short)(VB6Helpers.UBound(DatPrtys) + 1);
                VB6Helpers.RedimPreserve(ref DatPrtys, 0, n);
                DatPrtys[n].PrtImp = p;
                DatPrtys[n].IndNom = IndNom;
                DatPrtys[n].IndDir = IndDir;
                DatPrtys[n].NomPrty = s1;
                DatPrtys[n].DirPrty = s2;
            }

            if (NomDir == "N")
            {
                _retValue = s1;
            }
            if (NomDir == "D")
            {
                return s2;
            }
            */

            return _retValue;
        }


    }

}

    