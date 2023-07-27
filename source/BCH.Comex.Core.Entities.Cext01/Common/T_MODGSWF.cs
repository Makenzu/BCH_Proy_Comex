
namespace BCH.Comex.Core.Entities.Cext01.Common
{
    //Attribute VB_Name = "MODGSWF"
    //Arreglo de Beneficiarios.
    public class T_BenSwf
    {
        public bool EsBanco { get; set; }  //Indica si el Beneficiario es un Banco => MT-202.7
        public string SwfBen { get; set; }  //Registra el Swift del beneficiario en el caso de MT-202.
        public string FunBen { get; set; }  //Cedente, Agente,........,Otro Benef
        public short IndBen { get; set; }  //Indice de Combox de Beneficiario
        public string NomBen { get; set; }  //Nombre del Beneficiario
        public string DirBen1 { get; set; }  //Dirección del Beneficiario 1
        public string DirBen2 { get; set; }  //Dirección del Beneficiario 2
        public short PaiBen { get; set; }  //Código del Pais del Beneficiario
        public string PaiBen_t { get; set; }  //Nombre del Pais del Beneficiario
        public bool Es59F { get; set; }     //Indica si el beneficiario es 59F
        public string PaiBen59F { get; set; }
    }

    //Arreglo de Clientes.
    public class T_CliSwf
    {
        public string NomCli { get; set; }  //Nombre del Cliente
        public string rutcli { get; set; }  //Rut del Cliente
        public string DirCli1 { get; set; }  //Dirección del Cliente 1
        public string DirCli2 { get; set; }  //Dirección del Cliente 2
        public string PaiCli { get; set; }  //Nombre Pais del Cliente
        public string PaiCliCod { get; set; }  //Cod Pais del Cliente si es 50F
        public string CiuCli { get; set; }  //Nombre Ciudad del Cliente
        public string CtaCli { get; set; }  //Cuenta corriente del Cliente
        public bool Es50F { get; set; }
    }

    //Arreglo de Bancos.
    public class T_BcoSwf
    {
        public string SwfBco { get; set; }  //Swift
        public string NomBco { get; set; }  //Nombre
        public string DirBco1 { get; set; }  //Dirección 1
        public string DirBco2 { get; set; }  //Dirección 2
        public string PaiBco { get; set; }  //Nombre Pais
        public short IngMan { get; set; }  //Determina si el banco fue ingresado en forma manual.-
        public string CodCom { get; set; }
    }

    //Datos generales de cada Swift.
    public class T_DatSwf
    {
        public short PlzPag { get; set; }  //Plaza de Pago
        public string ctacte { get; set; }  //Cuenta Corriente
        public string RefOpe { get; set; }  //Referencia Operación
        public string SwfCor { get; set; }  //Swift Corresponsal
        public string NomBco { get; set; }  //Nombre del Banco Swift
        public string CiuBco { get; set; }  //Ciudad del Banco Swift
        public string PaiBco { get; set; }  //País del Banco Swift
        public short BcoCor { get; set; }  //Banco Corresponsal
        public string CtaCor { get; set; }  //Cuenta Corresponsal.-
        public string FecPag { get; set; }  //Fecha de Pago
        public short TipGas { get; set; }  //Gastos : BEN, OUR, etc
        public string NroAla { get; set; }  //Número Aladi.-

        public T_DatSwf()
        {

        }
    }

    //Estructura para Montos y monedas distribuídos en OP
    public class T_Swf
    {
        public double mtoswf { get; set; }  //Monto del Swift
        public short CodMon { get; set; }  //Moneda Banco Chile
        public string SwfMon { get; set; }  //Swift de la moneda
        public bool EsAladi { get; set; }  //Es Aladi el Swift?
        public short EstaGen { get; set; }  //Está generado (Si/No)
        public string DocSwf { get; set; }  //Documento de Swift
        public T_BenSwf BenSwf { get; set; }  //Datos Beneficiario
        public T_BcoSwf BcoAla { get; set; }  //Datos Banco Aladi
        public T_BcoSwf BcoPag { get; set; }  //Datos Banco Pagador
        public T_BcoSwf BcoInt { get; set; }  //Datos Banco Intermediario
        public T_BcoSwf BcoCoE { get; set; }  //Datos Corresponsal Del Emisor
        public T_BcoSwf BcoCoD { get; set; }  //Datos Corresponsal Del Destinatario
        public T_BcoSwf BcoTer { get; set; }  //Tercer Banco ????
        public T_DatSwf DatSwf { get; set; }  //Datos Generales
        public string NroSwf { get; set; }  //Registrará "MT-103" ó "MT-202".
        public short IndMT { get; set; }  //Relaciona el swf con estructura VMT_R --> Danilo Jorquera
        public short GrabSW { get; set; }  //0: No se grabo en Swift 1:Si se grabo
        public int CorSwi { get; set; }  //Correlativo Swift

        public T_Swf()
        {
            this.BenSwf = new T_BenSwf();
            this.DatSwf = new T_DatSwf();
        }

        public string TipoOpSegunAladi
        {
            get
            {
                return (this.EsAladi ? "O.P. Aladi" : "Otros Paises");
            }
        }
    }

    public class T_mt103
    {
        public bool Ch_Ori { get; set; }
        public short tipope { get; set; }  //Tipo Operacion
        public short codord { get; set; }  //Codigo Orden
        public short tiptrx { get; set; }  //Tipo Transaccion
        public short MndOri { get; set; }  //Codigo Moneda Original
        public double MtoOri { get; set; }  //Monto Original
        public double TipCam { get; set; }  //Tipo Cambio
        public double GasEmi { get; set; }  //gastos Emisor
        public double GasRec { get; set; }  //Gastos Receptor
    }

    public class T_Campo23E
    {
        public short numswi { get; set; }
        public string Codigo { get; set; }  //Codigo Operacion
        public short Estado { get; set; }  //1:ing:2mod:3:eli
    }

    //Variables generales para los Swift
    public class T_GSwf
    {
        public string NumOpe { get; set; }  //Número de Operación.
        public bool Acepto { get; set; }  //Indicador de Aceptar todos los Swift generados.
        public bool ProblemasAlGenerar { get; set; }
    }

    public class T_MODGSWF
    {
        public T_BenSwf[] VBenSwf;
        public T_CliSwf VCliSwf;
        public T_Swf[] VSwf;
        public T_mt103[] VMT103;
        public T_Campo23E[] VCod;
        public T_GSwf VGSwf;
        //****************************************************************************

        //Identificador Archivos para Swift.-
        public string FName;
        public short FNum;
        public short Indice_Benef;  //Almacena el itemdata del Beneficiario
        public short Indice_Monto;  //Almacena el itemdata de los Montos
        public short Indice_Bco;  //Almacena el listindex de los bancos (Aladi, Pag, Int)


        public const string MsgSwf = "Emisión de Mensajes Swift";
        public const string FormatoSwf = "#,###,###,###,##0.00";


        public const short MT_103 = 103;
        public const short MT_202 = 202;
        public const short MT_100 = 100;

        public const short BcoAla = 1;
        public const short BcoPag = 2;
        public const short BcoInt = 3;
        public const short BcoCoE = 4;
        public const short BcoCoD = 5;
        public const short BcoTer = 6;

        public T_MODGSWF()
        {
            VBenSwf = new T_BenSwf[0];
            VSwf = new T_Swf[0];
            VMT103 = new T_mt103[0];
            VCod = new T_Campo23E[0];
            VGSwf = new T_GSwf();
        }
    }
}
