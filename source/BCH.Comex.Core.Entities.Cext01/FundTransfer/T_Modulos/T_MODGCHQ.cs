
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //----------------------------------------------------------------------------
    //Estructura para Montos y monedas distribuídos Cheques/Vales Vistas.-
    //----------------------------------------------------------------------------
    public class Chq_Vvi
    {
        public string NroOpe;  //#Operación.-
        public string CCosto;  //Centro de Costo.-
        public string SupUsr;  //#SuperUsuario.-
        public string NroSiu;  //
        public short estado;  //
        public int NroCor;  //Correlativo.-
        public string NroPro;  //#Producto.-
        public string FecEmi;  //Fecha Emisión.-
        public double MtoDoc;  //Monto.-
        public string NomBen;  //Beneficiario.-
        public string RutTom;  //Rut Tomador.-
        public string NomTom;  //Nombre Tomador.-
        public string NomPag;  //Banco Pagador.-
        public string DirPag;  //
        public string SwfPag;  //
        public string CiuPag;  //
        public string PaiPag;  //
        public short BcoPag;  //Cód. Banco Chile.-
        public string NumCta;  //#Cuenta.-
        public string NomCli;  //Cliente.-
        public int Folio;  //Folio Vale Vista.-
        public short ChqEmi;  //Es Cheque Emitido?.-
        //---------------------------------------------------
        public short CodMon;  //Moneda B. Chile.-
        public string NemMon;  //Nemónico Moneda.-
        public string TipoDoc;  //CH/VV.-
        public short IndCon;  //Contabilidad.-
        public short IndBen;  //Beneficiario.-
        public short Pinchado;  //Lista para ser construído.-
        public short Generado;  //Ya generado.-
        public string Documento;  //
        public string MtoDoc_t;  //Monto en caracter.-
        public string TipoDoc_t;  //Cheque/Vale-Vista.-
        public string MtoChe;  //Para Imprimir el Cheque.-
        public string GlsMto;  //Monto en palabras.-
        public string NomPro;  //Nombre Producto.-
        public short BenVia;  //Beneficiario al Registro.-
        public short IndBenef;  //Indice Beneficiarios
        public short IndBanco;  //Indice Bancos
        public short IndCorres;  //Indice Corresponsables
        //---------------------------------------------------
    }
    //----------------------------------------------------------------------------
    //Arreglo de Beneficiarios de Documentos Valorados.-
    //----------------------------------------------------------------------------
    public class Type_BenDocVal
    {
        public string FunBen;  //Función Benef.-
        public string NomBen;  //Nombre de Beneficiario
        public short PaiBen;  //País del Beneficiario
        public string RutTom;  //Rut del Tomador
    }

    //----------------------------------------------------------------------------
    //Variables generales para los Cheques
    public class T_GChq
    {
        public short Acepto;  //Indicador de Aceptar todos los Cheques generados.
    }
    //----------------------------------------------------------------------------
    public class Type_DocVal
    {
        public string codpro;  //Código Producto.-
        public string CodObc;  //Oficina B. Central.-
        public string RefCed;  //Referencia Cedente.-
        public string RefPag;  //Referencia Pago.-
        public string RefOpe;  //Referencia Operacion.-
        public short I_Clte;  //Indice Cliente en PartysOpe.-
        public short AceptoChq;  //Aceptaron los Cheques/VVs.-
        public short AceptoSwf;  //Aceptaron los Swift's.-
        public short ComVen;  //Indica si está en Compra Venta.-
        public short PaiNac;  //Código del País Nacional.-
        public string ProChq;  //Glosa Producto en el Cheque.-
        public short Manual;  //Si se genera automáticamente.-
        public short IndBen;  //Indice Party en Vías.-
        public short Under;  //En proceso automático.-
    }
    
    public class T_MODGCHQ
    {
        public  Chq_Vvi[] V_Chq_VVi;
        public  short ITom;
        public  Type_BenDocVal[] BenDocVal;
        public  T_GChq VGChq;
        //Anulado.-
        public short Indice;  //Indice del arreglo de documentos.-
        public  Type_DocVal DocVals;

        // UPGRADE_INFO (#0561): The 'FormatoChq' symbol was defined without an explicit "As" clause.
        public const string FormatoChq = "#,###,###,###,##0.00";
        //----------------------------------------------------------------------------
        //Constantes para Documentos Valorados.-
        //----------------------------------------------------------------------------
        // UPGRADE_INFO (#0561): The 'MsgDocVal' symbol was defined without an explicit "As" clause.
        public const string MsgDocVal = "Documentos Valorados";
        // UPGRADE_INFO (#0561): The 'DOCVAL_CHEQUE' symbol was defined without an explicit "As" clause.
        public const string DOCVAL_CHEQUE = "CH";
        // UPGRADE_INFO (#0561): The 'DOCVAL_VALVIS' symbol was defined without an explicit "As" clause.
        public const string DOCVAL_VALVIS = "VV";
        // UPGRADE_INFO (#0561): The 'DOCV_Ing' symbol was defined without an explicit "As" clause.
        public const short DOCV_Ing = 1;

        public T_MODGCHQ()
        {
            VGChq = new T_GChq();
            V_Chq_VVi = new Chq_Vvi[0];
            BenDocVal = new Type_BenDocVal[0];
            DocVals = new Type_DocVal();
        }
    }
}
