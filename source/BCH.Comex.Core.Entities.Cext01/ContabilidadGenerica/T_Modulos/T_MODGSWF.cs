using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    // Arreglo de Beneficiarios.
    public class T_BenSwf
    {
        public int EsBanco;   //Indica si el Beneficiario es un Banco => MT-202.
        public string SwfBen;   //Registra el Swift del beneficiario en el caso de MT-202.
        public string FunBen;   //Cedente, Agente,........,Otro Benef
        public int IndBen;   //Indice de Combox de Beneficiario
        public string NomBen;   //Nombre del Beneficiario
        public string DirBen1;   //Dirección del Beneficiario 1
        public string DirBen2;   //Dirección del Beneficiario 2
        public int PaiBen;   //Código del Pais del Beneficiario
        public string PaiBen_t;   //Nombre del Pais del Beneficiario

        public T_BenSwf()
        {
            SwfBen= String.Empty;   //Registra el Swift del beneficiario en el caso de MT-202.
            FunBen= String.Empty;   //Cedente, Agente,........,Otro Benef
            NomBen= String.Empty;   //Nombre del Beneficiario
            DirBen1= String.Empty;   //Dirección del Beneficiario 1
            DirBen2= String.Empty;   //Dirección del Beneficiario 2
            PaiBen_t= String.Empty;   //Nombre del Pais del Beneficiario
        }
    }

    // Arreglo de Clientes.
    public class T_CliSwf
    {
        public string NomCli;   //Nombre del Cliente
        public string rutcli;   //Rut del Cliente
        public string DirCli1;   //Dirección del Cliente 1
        public string DirCli2;   //Dirección del Cliente 2
        public string PaiCli;   //Nombre Pais del Cliente

        public T_CliSwf()
        {
            NomCli= String.Empty;   //Nombre del Cliente
            rutcli= String.Empty;   //Rut del Cliente
            DirCli1= String.Empty;   //Dirección del Cliente 1
            DirCli2= String.Empty;   //Dirección del Cliente 2
            PaiCli= String.Empty;   //Nombre Pais del Cliente
        }
    }

    public class T_BcoSwf
    {
        public string SwfBco;   //Swift
        public string NomBco;   //Nombre
        public string DirBco1;   //Dirección 1
        public string DirBco2;   //Dirección 2
        public string PaiBco;   //Nombre Pais
        public string CodCom;
        public int IngMan;   //Determina si el banco fue ingresado en forma manual.-

        public T_BcoSwf()
        {
            SwfBco= String.Empty;   //Swift
            NomBco= String.Empty;   //Nombre
            DirBco1= String.Empty;   //Dirección 1
            DirBco2= String.Empty;   //Dirección 2
            PaiBco= String.Empty;   //Nombre Pais
            CodCom= String.Empty;
        }
    }

    // Datos generales de cada Swift.
    public class T_DatSwf
    {
        public int PlzPag;   //Plaza de Pago
        public string ctacte;   //Cuenta Corriente
        public string RefOpe;   //Referencia Operación
        public string SwfCor;   //Swift Corresponsal
        public string NomBco;   //Nombre del Banco Swift
        public string CiuBco;   //Ciudad del Banco Swift
        public string PaiBco;   //País del Banco Swift
        public int BcoCor;   //Banco Corresponsal
        public string CtaCor;   //Cuenta Corresponsal.-
        public string FecPag;   //Fecha de Pago
        public int TipGas;   //Gastos : BEN, OUR, etc
        public string NroAla;   //Número Aladi.-

        public T_DatSwf()
        {            
            ctacte= String.Empty;   //Cuenta Corriente
            RefOpe= String.Empty;   //Referencia Operación
            SwfCor= String.Empty;   //Swift Corresponsal
            NomBco= String.Empty;   //Nombre del Banco Swift
            CiuBco= String.Empty;   //Ciudad del Banco Swift
            PaiBco= String.Empty;   //País del Banco Swift
            
            CtaCor= String.Empty;   //Cuenta Corresponsal.-
            FecPag= String.Empty;   //Fecha de Pago
            
            NroAla= String.Empty;   //Número Aladi.-

        }
    }

    // Estructura para Montos y monedas distribuídos en OP
    public class T_Swf
    {
        public double mtoswf;   //Monto del Swift
        public int CodMon;   //Moneda Banco Chile
        public string SwfMon;   //Swift de la moneda
        public int EsAladi;   //Es Aladi el Swift?
        public int EstaGen;   //Está generado (Si/No)
        public string DocSwf;   //Documento de Swift
        public T_BenSwf BenSwf;   //Datos Beneficiario
        public T_BcoSwf BcoAla;   //Datos Banco Aladi
        public T_BcoSwf BcoPag;   //Datos Banco Pagador
        public T_BcoSwf BcoInt;   //Datos Banco Intermediario
        public T_DatSwf DatSwf;   //Datos Generales
        public T_BcoSwf BcoCoE;   //Datos Corresponsal Del Emisor
        public T_BcoSwf BcoCoD;   //Datos Corresponsal Del Destinatario
        public T_BcoSwf BcoTer;   //Tercer Banco ????
        public string NroSwf;   //Registrará "MT-103" ó "MT-202".
        public int IndMT;   //Relaciona el swf con estructura VMT_R --> Danilo Jorquera
        public int GrabSW;   //0: No se grabo en Swift 1:Si se grabo
        public int CorSwi;   //Correlativo Swift

        public T_Swf()
        {
            SwfMon = String.Empty;   //Swift de la moneda
            DocSwf = String.Empty;   //Documento de Swift
            BenSwf = new T_BenSwf();   //Datos Beneficiario
            BcoAla = new T_BcoSwf();   //Datos Banco Aladi
            BcoPag = new T_BcoSwf();   //Datos Banco Pagador
            BcoInt = new T_BcoSwf();   //Datos Banco Intermediario
            DatSwf = new T_DatSwf();   //Datos Generales
            BcoCoE = new T_BcoSwf();   //Datos Corresponsal Del Emisor
            BcoCoD = new T_BcoSwf();   //Datos Corresponsal Del Destinatario
            BcoTer = new T_BcoSwf();   //Tercer Banco ????
            NroSwf = String.Empty; ;   //Registrará "MT-103" ó "MT-202".
        }
    }

    public class T_mt103
    {
        public int ch_ori;
        public int tipope;   //Tipo Operacion
        public int codord;   //Codigo Orden
        public int tiptrx;   //Tipo Transaccion
        public int mndori;   //Codigo Moneda Original
        public double mtoori;   //Monto Original
        public double TipCam;   //Tipo Cambio
        public double GasEmi;   //gastos Emisor
        public double GasRec;   //Gastos Receptor
    }

    public class T_Campo23E
    {
        public int numswi;
        public string Codigo;   //Codigo Operacion
        public int estado;   //1:ing:2mod:3:eli

        public T_Campo23E()
        {
            Codigo = String.Empty;
        }
    }

    // Variables generales para los Swift
    public class T_GSwf
    {
        public string NumOpe;   //Número de Operación.
        public int Acepto;   //Indicador de Aceptar todos los Swift generados.

        public T_GSwf()
        {
            NumOpe = String.Empty;
        }
    }


    public class T_MODGSWF
    {
        public T_BenSwf[] VBenSwf = new T_BenSwf[0];
        public T_CliSwf VCliSwf = new T_CliSwf();
        public T_Swf[] VSwf = new T_Swf[0];
        public T_mt103[] VMT103 = new T_mt103[0];
        public T_Campo23E[] VCod = new T_Campo23E[0];
        public T_GSwf VGSwf = new T_GSwf();
        // ****************************************************************************
        // Identificador Archivos para Swift.-
        public string FName = "";
        public int FNum = 0;
        public int Indice_Benef = 0;     // Almacena el itemdata del Beneficiario
        public int Indice_Monto = 0;     // Almacena el itemdata de los Montos
        public int Indice_Bco = 0;     // Almacena el listindex de los bancos (Aladi, Pag, Int)
        public string[] Lineas = null;
        // ****************************************************************************
        public const string MsgSwf = "Emisión de Mensajes Swift";
        public const string FormatoSwf = "#,###,###,###,##0.00";
        public const int MT_103 = 103;
        public const int MT_202 = 202;
        public const int BcoAla = 1;
        public const int BcoPag = 2;
        public const int BcoInt = 3;
        public const int BcoCoE = 4;
        public const int BcoCoD = 5;
        public const int BcoTer = 6;
        public const int BONL = 0;
        public const int CHQB = 1;
        public const int CORT = 2;
        public const int HOLD = 3;
        public const int INTC = 4;
        public const int PHON = 5;
        public const int PHOB = 6;
        public const int PHOI = 7;
        public const int SDVA = 8;
        public const int TELE = 9;
        public const int TELB = 10;
        public const int TELI = 11;
        public const string G1 = "SDVA;HOLD;CHQB";
        public const string G2 = "INTC;BONL;HOLD;CHQB";
        public const string G3 = "CORT;BONL;HOLD;CHQB";
        public const string G4 = "HOLD;CHQB";
        public const string G5 = "PHOB;TELB";
        public const string G6 = "PHON;TELE";
    }
}
