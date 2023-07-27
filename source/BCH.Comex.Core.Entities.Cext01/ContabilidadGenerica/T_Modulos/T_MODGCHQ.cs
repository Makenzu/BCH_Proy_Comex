using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    // ----------------------------------------------------------------------------
    // Estructura para Montos y monedas distribuídos Cheques/Vales Vistas.-
    // ----------------------------------------------------------------------------
    public class Chq_Vvi
    {
        public string NroOpe;   //#Operación.-
        public string CCosto;   //Centro de Costo.-
        public string SupUsr;   //#SuperUsuario.-
        public string NroSiu;
        public int estado;
        public int NroCor;   //Correlativo.-
        public string NroPro;   //#Producto.-
        public string FecEmi;   //Fecha Emisión.-
        public double MtoDoc;   //Monto.-
        public string NomBen;   //Beneficiario.-
        public string RutTom;   //Rut Tomador.-
        public string NomTom;   //Nombre Tomador.-
        public string NomPag;   //Banco Pagador.-
        public string DirPag;
        public string SwfPag;
        public string CiuPag;
        public string PaiPag;
        public int BcoPag;   //Cód. Banco Chile.-
        public string NumCta;   //#Cuenta.-
        public string NomCli;   //Cliente.-
        public int Folio;   //Folio Vale Vista.-
        public int ChqEmi;   //Es Cheque Emitido?.-
        public int CodMon;   //Moneda B. Chile.-
        public string nemmon;   //Nemónico Moneda.-
        public string TipoDoc;   //CH/VV.-
        public int IndCon;   //Contabilidad.-
        public int IndBen;   //Beneficiario.-
        public int Pinchado;   //Lista para ser construído.-
        public int Generado;   //Ya generado.-
        public string Documento;
        public string MtoDoc_t;   //Monto en caracter.-
        public string TipoDoc_t;   //Cheque/Vale-Vista.-
        public string MtoChe;   //Para Imprimir el Cheque.-
        public string GlsMto;   //Monto en palabras.-
        public string NomPro;   //Nombre Producto.-
        public int BenVia;   //Beneficiario al Registro.-
        public int IndBenef;   //Indice Beneficiarios
        public int IndBanco;   //Indice Bancos
        public int IndCorres;   //Indice Corresponsables

        public Chq_Vvi()
        {
            NroOpe= String.Empty;   //#Operación.-
            CCosto= String.Empty;   //Centro de Costo.-
            SupUsr= String.Empty;   //#SuperUsuario.-
            NroSiu= String.Empty;
            
            NroPro= String.Empty;   //#Producto.-
            FecEmi= String.Empty;   //Fecha Emisión.-
            
            NomBen= String.Empty;   //Beneficiario.-
            RutTom= String.Empty;   //Rut Tomador.-
            NomTom= String.Empty;   //Nombre Tomador.-
            NomPag= String.Empty;   //Banco Pagador.-
            DirPag= String.Empty;
            SwfPag= String.Empty;
            CiuPag= String.Empty;
            PaiPag= String.Empty;
            
            NumCta= String.Empty;   //#Cuenta.-
            NomCli= String.Empty;   //Cliente.-
            
            nemmon= String.Empty;   //Nemónico Moneda.-
            TipoDoc= String.Empty;   //CH/VV.-
            
            Documento= String.Empty;
            MtoDoc_t= String.Empty;   //Monto en caracter.-
            TipoDoc_t= String.Empty;   //Cheque/Vale-Vista.-
            MtoChe= String.Empty;   //Para Imprimir el Cheque.-
            GlsMto= String.Empty;   //Monto en palabras.-
            NomPro= String.Empty;   //Nombre Producto.-
            
        }
    }
    // ----------------------------------------------------------------------------
    // Arreglo de Beneficiarios de Documentos Valorados.-
    // ----------------------------------------------------------------------------
    public class Type_BenDocVal
    {
        public string FunBen;   //Función Benef.-
        public string NomBen;   //Nombre de Beneficiario
        public int PaiBen;   //País del Beneficiario
        public string RutTom;   //Rut del Tomador

        public Type_BenDocVal()
        {
            FunBen= String.Empty;   //Función Benef.-
            NomBen= String.Empty;   //Nombre de Beneficiario
            RutTom= String.Empty;   //Rut del Tomador
        }
    }

    public class T_GChq
    {
        public int Acepto;   //Indicador de Aceptar todos los Cheques generados.
    }

    public class Type_DocVal
    {
        public string CodPro;   //Código Producto.-
        public string CodObc;   //Oficina B. Central.-
        public string RefCed;   //Referencia Cedente.-
        public string RefPag;   //Referencia Pago.-
        public string RefOpe;   //Referencia Operacion.-
        public int I_Clte;   //Indice Cliente en PartysOpe.-
        public int AceptoChq;   //Aceptaron los Cheques/VVs.-
        public int AceptoSwf;   //Aceptaron los Swift's.-
        public int ComVen;   //Indica si está en Compra Venta.-
        public int PaiNac;   //Código del País Nacional.-
        public string ProChq;   //Glosa Producto en el Cheque.-
        public int Manual;   //Si se genera automáticamente.-
        public int IndBen;   //Indice Party en Vías.-
        public int Under;   //En proceso automático.-

        public Type_DocVal()
        {
            CodPro= String.Empty;   //Código Producto.-
            CodObc= String.Empty;   //Oficina B. Central.-
            RefCed= String.Empty;   //Referencia Cedente.-
            RefPag= String.Empty;   //Referencia Pago.-
            RefOpe= String.Empty;   //Referencia Operacion.-
            ProChq= String.Empty;   //Glosa Producto en el Cheque.-
        }
    }


    public class T_MODGCHQ
    {
        public Chq_Vvi[] V_Chq_VVi = null;
        public int ITom = 0;
        public const string FormatoChq = "#,###,###,###,##0.00";
        public const string MsgDV = "Documentos Valorados";
        public Type_BenDocVal[] BenDocVal = new Type_BenDocVal[0];
        public T_GChq VGChq = new T_GChq();
        // ----------------------------------------------------------------------------
        // Constantes para Documentos Valorados.-
        // ----------------------------------------------------------------------------
        public const string MsgDocVal = "Documentos Valorados";
        public const string DOCVAL_CHEQUE = "CH";
        public const string DOCVAL_VALVIS = "VV";
        public const int DOCV_Ing = 1;     // Generado.-
        public const int DOCV_Imp = 2;     // Impreso.-
        public const int DOCV_Anu = 3;     // Anulado.-
        public int Indice = 0;     // Indice del arreglo de documentos.-
        public Type_DocVal DocVals = new Type_DocVal();
    }
}
