
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //Arreglo para planillas reemplazadas.-
    //----------------------------------------
    public class Plan_Reemp
    {
        public short TipReg;
        public int NumPla;  //Número de la Planilla
        public string FecVen;  //Fecha de Venta
        public string NomImp;  //Nombre del Importador
        public string RutImp;  //Rut del Importador
        public int NumIdi;  //Número del Idi
        public string FecIdi;  //Fecha de Aprobación del Idi
        public short PlzIdi;
        public string numdec;  //Número de la Declaración
        public string FecDec;  //Fecha de la Declaración
        public string NumCon;  //Nº Conocimiento de Embarque
        public string FecCon;  //Fecha del Conocimiento del Embarque
        public string Codigo;  //Código de Planilla 205000
        public short CodBch;  //Codigo del Banco de Chile 15
        public short CodPlz;  //Codigo Plaza Banco Central
        public string NomPlz;  //Nombre de la plaza
        public short CodPag;  //Codigo de la forma de Pago
        public short CodPPa;  //Codigo del País de Pago
        public string NomPai;  //Nombre del País de Pago
        public short CodMPa;  //Codigo de la Moneda del Pago (Banco Central)
        public short CodMon;  //Cod bch
        public string NomMon;  //Nombre de la Moneda de Pago
        public string NemMon;  //Nemonico de la Moneda de Pago
        public double ParPag;  //Paridad del Pago
        public double TipCam;  //Valor tipo de cambio equivalente
        public double TipCamo;  //Valor del tipo de Cambio a la fecha (Origen)
        public double MtoFob;  //Valor del Fob cubierto en la Planilla
        public double MtoFle;  //PCP
        public double MtoSeg;  //PCP
        public double MtoCif;  //PCP
        public double MtoInt;  //PCP
        public double GasBan;  //Gastos del Banco Cedente
        public double TotOri;  //Total de lo Cubierto
        public double CifDol;  //Valor del Cif en US dollar
        public double TotDol;  //Valor total en us dollar
        public string FecVto;
        public short HayCua;
        public int NumCua;
        public short numcuo;
        public short HayAcu;
        public short NumAcu;
        public string Acuer1;
        public string Acuer2;
        public short HayAnu;
        public string VenAnu;
        public short IndAnu;
        public short NumReg;
        public double ParAnu;  //Paridad de Anulacion.-
        public double TotAnu;
        public string FecAnu;
        //Datos del Reemplazo
        //********************************
        public int NumPln_R;
        public string FecPln_R;
        public short CodPlz_R;
        public short CodEnt_R;
        public int NumInf_R;
        public string FecInf_R;
        public short PlzInf_R;
        public string NumCon_R;
        public string FecCon_R;
        //********************************
        public string FecDeb;
        public string DocChi;
        public string DocExt;
        public short Status;
        public string observ;  //Observaciones para la Planilla
        public string ObsDec;  //Observaciones para la Planilla
        public string ObsMer;  //Observaciones para la Planilla
        public string ObsPar;  //Observaciones para la Planilla
        public string ObsCob;  //Observaciones para la Planilla
        public short Estado;
        public short HayRpl;
        public short ClauRo;  //Para Clausula Roja (Planillas S/Op.)
        public short ZonFra;
    }

    //Arreglo de Intereses de Planillas Reemplazada.-
    //-------------------------------------------------
    public class Intereses
    {
        public string Cent_Costo;
        public string Id_Product;
        public string Id_Especia;
        public string Id_Empresa;
        public string Id_Cobranz;
        public int NumPlan;
        public string Fecha;
        public short nro;
        public short Concepto;
        public short Tipo;
        public double monto;
        public double CapBas;
        public short CodBas;
        public double Tasa;
        public string FIni;
        public string FFin;
        public short ndias;
        public short Estado;
    }

    public class TxIdiDec
    {
        public short flag;
        public double OriIdi;
        public double RelIdi;
        public double CubIdi;
        public double DisIdi;
        public short PlzApr;
        public short CodMon;
        public double PaiAdq;
        public double OriFob;
        public double RelFob;
        public double CubFob;
        public double DisFob;
        public double OriFle;
        public double RelFle;
        public double CubFle;
        public double DisFle;
        public double OriSeg;
        public double RelSeg;
        public double CubSeg;
        public double DisSeg;
        public double OriCif;
        public double RelCif;
        public double CubCif;
        public double DisCif;
        public string FecEmb;
        //Montos en la moneda de la planilla a reemplazar
        public double OriMon;
        public double RelMon;
        public double CubMon;
        public double DisMon;
        public double OriFob_M;
        public double RelFob_M;
        public double CubFob_M;
        public double DisFob_M;
        public double OriFle_M;
        public double RelFle_M;
        public double CubFle_M;
        public double DisFle_M;
        public double OriSeg_M;
        public double RelSeg_M;
        public double CubSeg_M;
        public double DisSeg_M;
        public double OriCif_M;
        public double RelCif_M;
        public double CubCif_M;
        public double DisCif_M;
        public short ForPag;
        public short codpai;
    }

    //Variable para Conocimientos de Embarque.-
    //------------------------------------------
    public class Tx_ConEmb
    {
        public string numdec;
        public string FecDec;
        public string NumBlw;
        public string FecBlw;
    }

    public class V_Fdp
    {
        public short codfdp;
        public string NomFdp;
        public string ProFdp;
    }

    public class T_MODPREEM
    {
        public  string FechaSwift;
        public  string NemMoneda ;
        public  double ParIdi;
        public  double ParDec;
        public  short NuevaPlan;
        public  Plan_Reemp[] Vx_PReem;
        public  TxIdiDec VxIdiDec;
        public  TxIdiDec VxIdiDecNull;
        public  Tx_ConEmb[] Vx_ConEmb;
        public  V_Fdp[] Vx_Fdp;
        public T_MODANUVI MODANUVI;
        public T_MODCVDIMMM MODCVDIMMM;

        public T_MODPREEM() {
            Vx_PReem = new Plan_Reemp[0];
            Vx_ConEmb = new Tx_ConEmb[0];
            Vx_Fdp = new V_Fdp[0];
            VxIdiDec = new TxIdiDec();
            VxIdiDecNull = new TxIdiDec();
            MODANUVI = new T_MODANUVI();
            MODCVDIMMM = new T_MODCVDIMMM();
        }        
    }
}
