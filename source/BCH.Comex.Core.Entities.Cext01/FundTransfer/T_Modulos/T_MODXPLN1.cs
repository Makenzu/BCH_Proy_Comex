
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{

    //*****************************************************
    //Estructura de Planillas Visibles de Exportación.
    //*****************************************************
    public class T_xPlv
    {
        public string NumPre;  //# Presentación.
        public string fecpre;  //Fecha Presentación.
        public short TipPln;  //Tipo de Planilla.
        //-------------------------------------------------
        public string codcct;  //Centro de Costo Ope. asoc
        public string codpro;  //Producto Operación   asoc
        public string codesp;  //Especialista Ope.    asoc
        public string codofi;  //Empresa Operación    asoc
        public string codope;  //Correlativo Ope.     asoc
        public string CodAnu;  //Código de Anulación.
        //-------------------------------------------------
        public short Estado;  //Estado Planilla.
        public string numdec;  //Número Declaración.
        public string FecDec;  //Fecha Declaración.
        public short CodAdn;  //Código Aduana.
        public string FecVen;  //Fecha Ven. Retorno.
        //-------------------------------------------------
        public string RutExp;  //Rut del Exportador.
        public string PrtExp;  //Llave del Exportador.
        public short IndNom;  //Indice Nombre.
        public short IndDir;  //Indice Direccion.
        //-------------------------------------------------
        public short CodMnd;  //Código Moneda Planilla.
        public double MtoBru;  //Monto Bruto.
        public double TotAnu;  //Total de la Anulación.
        public double MtoCom;  //Monto Comisiones.
        public double MtoOtg;  //Monto Otros Gastos.
        public double MtoLiq;  //Monto Líquido.
        public double Mtopar;  //Monto Paridad.
        public double MtoDol;  //Monto Dólares.
        public double TipCam;  //Tipo de Cambio.
        public double TipCamo;  //Tipo de Cambio Operación.
        public short PlzBcc;  //Plaza B. Central Contabil
        //-------------------------------------------------
        public short DfoCea;  //DFO, Código Entidad Autor
        public short DfoCtf;  //DFO, Código Tipo Financia
        public short DfoCbc;  //DFO, Código Plaza B. Cent
        public string DfoNpr;  //DFO, Número Presentación.
        public string DfoFpr;  //DFO, Fecha  Presentación.
        //-------------------------------------------------
        public short AfiMnd;  //AFI, Código Moneda.
        public double AfiPar;  //AFI, Paridad.
        public double AfiMto;  //AFI, Monto.
        public double AfiMtoD;  //AFI, Monto Dolar.
        public short AfiVen;  //AFI, Plazo Vencimiento (d
        //-------------------------------------------------
        public short DiePbc;  //DIE, Plaza B. Central.
        public string DieNum;  //DIE, Número Emisión.
        public string DieFec;  //DIE, Fecha  Emisión.
        public string ObsPln;  //Observaciones.
        //-------------------------------------------------
        public string Fecing;  //Fecha de Ingreso.
        public string FecAct;  //Fecha Actualización.
        public string cencos;  //Centro Costo dueño.
        public string codusr;  //Especialista dueño.
        //-------------------------------------------------
        public short Status;  //Uso interno x planillas.
        public short Acepto;  //Indica si Acepto el Frm.
        public short IndPrt;  //Indice Party en PartysOpe.
        public short TipMto;  //Uso Interno, 1-2-3 : L,I,E.
        public double ValRet;  //Valor Retorno  Dec.
        public double ValCom;  //Valor Comision Dec.
        public double ValGas;  //Valor Gastos   Dec.
        public double ValLiq;  //Valor Liquido  Dec.
        public double ValFle;  //Valor Flete    Dec.
        public double ValSeg;  //Valor Seguro   Dec.

        //--- Se generan campos deducibles

        public short DedCom;
        public short DedFle;
        public short DedSeg;
        public short PlnEst;

        //--- Datos Para CE Retornados
        public double NumCre;  //Número del Crédito.
        public string fecins;  //Fecha Inscripción.
        public short codpai;  //Pais Acreedor

        //--- Sectores Económicos
        public short SecBen;  //Sector Beneficiario
        public short SecInv;  //Sector Inversionista
        public double PrcPar;  //Porcentaje de Participación
        public string nomcom;  //Nombre del Comprador
    }

    public class T_Comxplv
    {
        public double MtoCom;  //indica el monto de las comisiones
        public double MtoIva;  //indica el monto del iva a cobrar por comision
        public double MtoTot;  //indica el monto total de comison mas iva
        public short MonCon;  //Indica la moneda en que se cobro la comison
    }
    //******************************************************
    //Estructura que registra el tipo de Planilla(Visible-Invisible),
    //índice del arreglo en donde se encuentran y el estado.
    public class T_Pln
    {
        public string TipPln;  //V:Visible; I:Invisible.
        public short Indice;  //Indice del arreglo(VxPlv-VPlis).
        public short Status;  //Estado Eli:Ing:Mod.
        public short IndPlv;  //Indice Arreglo Planillas.-
    }

    public class T_MODXPLN1
    {
        public T_MODXPLN1()
        {
            VxPlvs = new T_xPlv[0];
            VCom_xPlv = new T_Comxplv();
            VCom_xPlvNul = new T_Comxplv();
            VPln = new T_Pln[0];
        }

        public  T_xPlv[] VxPlvs;
        public  T_Comxplv VCom_xPlv;
        public  T_Comxplv VCom_xPlvNul;
        public  T_Pln[] VPln;

        public  short IndPlv;  //Indica el índice de donde debe comenzar la visualización de la Planilla Visible.

        //variables que indican tipo de planilla a cobrar comisiones
        public const string PLNLIQ = "401;403;407;500";
        public const string PLNINF = "501;502";
        public const string PlnEst = "402;511";

        public const string PLN400 = "401;403;407;402;408";

        //planilas de que deben tener nuémro de declaración
        public const string PLNCDEC = "500;501;502;511;570;601;603;607";
        public const string PLNSDEC = "401;402;403;407;540;408";

        //Titulo de para definir planillas visibles
        public const string MsgxPlv = "Planillas Visibles de Exportación";

        public const short ExPlv_Anulada = 9;
    }
}
