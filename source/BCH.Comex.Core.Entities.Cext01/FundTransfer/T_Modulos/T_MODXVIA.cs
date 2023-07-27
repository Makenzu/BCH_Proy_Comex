
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //****************************************************************************
    //Arreglo para los Destinos de Fondos con Moneda Extranjera.
    //****************************************************************************
    public class T_Tdme
    {
        public short CodDme;  //Código de Destino M/E.
        public string DesDme;  //Descripción de Destino M/E.
    }

    //****************************************************************************
    //Variable para Montos de Destino de Fondos.
    //****************************************************************************
    public class T_xMtoVia
    {
        public short CodMon;  //Moneda de la Comisión.
        public string NemMon;  //Nemónico Moneda Comisión.
        public double MtoTot;  //Monto incluyendo IVA.
        public short Vuelto;  //Es Vuelto.-
    }
    //****************************************************************************
    //Variable para Vías.
    //****************************************************************************
    public class T_xVia
    {
        public short NumCta;  //Id. Cuenta Contable.
        public string NemCta;  //Nemónico Cuenta Contable.
        public string NomVia;  //Nombre de la Vía.
        public short CodMon;  //Moneda de la Comisión.
        public double MtoTot;  //Monto total para la moneda.
        public string NemMon;  //Nemónico Moneda.
        public short Status;  //1:Ing;2:Mod;3:Eli.
        //-------------------------------------------------------------
        //Datos adicionales.
        //-------------------------------------------------------------
        public string IdPrty;  //Llave del Party.
        public string NomPrty;  //Nombre del Party.
        public short PosPrty;  //Posición en PartysOpe.
        public string ctacte;  //# Cuenta Corriente.
        public string CtaCte_t;  //# Cuenta Corriente_t.
        public short MonExt;  //Indica si es Mon. Extranjera.
        //-------------------------------------------------------------
        public short codofi;  //Oficina en ScS's.
        public short TipMov;  //Tipo de Movimiento en ScS's.
        public int NumPar;  //Numero de Partida en ScS's.
        //-------------------------------------------------------------
        public string CodSwf;  //Swift  del Banco.
        public string numdoc;  //Número del Documento.
        public short IndSwf;  //Indice Swf().-
        public short IndChq;  //Indice Chq().-
        //-------------------------------------------------------------
        public short CodDme;  //Código de Destino Fondoc M/E.
        public short Vuelto;  //Indica si es Vuelto.-
        public short ImpChq;  //Indica si lleva Impuesto Sobre Cheque.-
        public short GenPln;  //Indica si Genera planillas. Esto es para Check verif y Corresp.-
        public short cctlin;  //Se realizó transacción Cta. Cte. en Línea

        public string NroRef;
        public string SwiBco;
        public string Text1;
        public string Text2;
        public string Text3;
        public string Text4;
        public string Text5;
        public short IdCtran;
        public short nroimp;

        
    }
    //****************************************************************************
    //Variables Generales de Vías.
    //****************************************************************************
    public class T_gxVia
    {
        public short Acepto;
        public string Partys;
        public short Vuelto;  //Indica si es Vuelto.-
        public short destino;  //Destino por Defecto.-
    }
    //****************************************************************************

    //****************************************************************************
    //La siguiente estructura proviene desde el módulo ModyCom.Bas que se han
    //trasladado para acá por problemas de utilización de la función Fn_GetGlosa()
    //****************************************************************************
    //****************************************************************************
    //Variable para Cobro de Comisiones.-
    public class T_xCom
    {
        public short codcom;  //Correlativo de Comisiones.-
        public short TipCom;  //Tipo de Comision.-
        public short CodZon;  //Código de Zona para Courier.-
        public short Estado;  //1:Sin Cobrar;2:Cobrada.-
        public string nomcom;  //Descripción de la Comisión.-
        public short DelSis;  //Comisión del Sistema.-
        public short CodMnd;  //Moneda de la Comisión.-
        public short ConIVA;  //True : Tiene IVA.-
        public double MtoSis;  //Monto del Sistema.-
        public double MtoCom;  //Monto de la Comisión.-
        public double MtoIva;  //Monto IVA.-
        public double MtoTot;  //Monto incluyendo IVA.-
        public double TipCam;  //Tipo de Cambio.-
        public double MtoComp;  //Monto de la Comisión en $.-
        public double MtoIvap;  //Monto IVA en $.-
        public double MtoTotp;  //Monto incluyendo IVA en $.-
        public short PagExt;  //True : Se paga en el Exterior.-
        public short PagME;  //True : Exportador pagó con M/E.-
        public short ConMod;  //True : Modificado por el Esp.-
        public string NemCta;  //Nemónico Cuenta Contable.-
        public string PrtExp;  //Party que paga la Comisión.-
        public short PosPrt;  //Posicion del Partys que paga.-
        public short IndNom;  //Indice del Nombre.-
        public short IndDir;  //Indice de la Dirección.-
        //-----------------------------------------------------------
        public short TCModi;  //TC Modificado por el Esp.-
        public short Estaba;  //Indica si ya existía la Comis.-
        public short Cobrar;  //True : Se cobra la comisión.-
        public string NemMnd;  //Nemónico Moneda.-
        public string NemCta_t;  //Glosa Nemónico Cuenta Contable.-
        public short Status;  //1:Ing;2:Mod;3:Eli.-
        public short PagExtant;  //Indicador pago Exterior Original.-

        
    }

    // Se define estructura propia de los ticket
    // -----------------------------------------
    public class T_Ticket
    {
        public string Nomtic;
        public string Nemtic;
        public string Montic;
        public string Cuetic;
        public short Dehtic;
        public string Contic;
        public bool Demtci;
        public string Glosa;
    }

    public class T_MODXVIA
    {
        public T_MODXVIA()
        {
            VTDme = new T_Tdme[0];
            VxMtoVia = new T_xMtoVia[0];
            VxVia = new T_xVia[0];
            VgxVia = new T_gxVia();
            VgxViaNul = new T_gxVia();
            Vxcom = new T_xCom[0];
            Strtic = new T_Ticket();
        }

        public  T_Tdme[] VTDme;
        public  T_xMtoVia[] VxMtoVia;
        public  T_xVia[] VxVia;
        public  T_gxVia VgxVia;
        public  T_gxVia VgxViaNul;
        
        //Estado Eliminado.-
        public  T_xCom[] Vxcom;
        public  string VBcxCci2 ;
        public  short EsSolBcx;
        public  string BcxEntrada2 ;
       
        //Aviso de Débito.
        public  string Moneda_TRX ;

        public  T_Ticket Strtic;
        public  short IdCtran;


        // UPGRADE_INFO (#0561): The 'MsgxVia' symbol was defined without an explicit "As" clause.
        public const string MsgxVia = "Destino de Fondos";
        //Título de caja de Mensajes.
        // UPGRADE_INFO (#0561): The 'MsgXDoc' symbol was defined without an explicit "As" clause.
        public const string MsgXDoc = "Documentos de Aviso Débito/Crédito";
        // UPGRADE_INFO (#0561): The 'ExVia_Eli' symbol was defined without an explicit "As" clause.
        public const short ExVia_Eli = 3;
        // UPGRADE_INFO (#0561): The 'DocGAdeb' symbol was defined without an explicit "As" clause.
        public const short DocGAdeb = 998;
        //Aviso de Crédito.
        // UPGRADE_INFO (#0561): The 'DocGAcre' symbol was defined without an explicit "As" clause.
        public const short DocGAcre = 999;
    }
}
