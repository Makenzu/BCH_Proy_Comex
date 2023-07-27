
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //--------------------------------------------------------------------
    //Estructura para la Anulacion de Planillas Visibles de  Importacion
    public class T_AnuVi
    {
        public string codcct;  //Centro de Costo.
        public string codpro;  //Producto.
        public string codesp;  //Especialista.
        public string codofi;  //Oficina.
        public string codope;  //Operacion.
        public int NumPre;
        public string RutImp;
        public string PrtImp;
        public string NomImp;
        public string FecVen;
        public int NumIdi;
        public string FecIdi;
        public string numdec;
        public string FecDec;
        public string NumCon;
        public string FecCon;
        public string Codigo;
        public short CodBch;
        public short CodPla;
        public string NomPla;
        public short CodPag;
        public short CodPPa;
        public string NomPai;
        public short CodMPa;
        public short CodMon;
        public string NomMon;
        public string NemMon;
        public double ParPag;
        public double TipCamo;
        public double TipCam;
        public double MtoFob;
        public double MtoFle;
        public double MtoSeg;
        public double MtoCif;
        public double MtoInt;
        public double MtoGas;
        public double MtoTot;
        public double CifDol;
        public double TotDol;
        public string FecVto;
        public short HayCua;
        public short NumCua;
        public short numcuo;
        public short HayAcu;
        public short NumAcu;
        public string Acuer1;
        public string Acuer2;
        public short HayAnu;
        public short IndAnu;
        public short NumReg;
        //VenAnu      As String
        public double ParAnu;  //Paridad de Anulacion.-
        public double TotAnu;
        public string FecAnu;
        public string FecDeb;
        public string DocChi;
        public string DocExt;
        public short Status;
        public string observ;  //Observaciones para la Planilla
        public string ObsDec;  //Observaciones para la Planilla
        public string ObsMer;  //Observaciones para la Planilla
        public string ObsPar;  //Observaciones para la Planilla
        public string ObsCob;  //Observaciones para la Planilla
        public short estado;
        public string TipAut;
        public int NumAut;
        public string FecAut;
        public short ZonFra;
    }

    public class T_PlAnul
    {
        public string nroPresentación;
        public string numIdi;
        public string numnDec;
        public string montoTotal;
    }
    //------------------------------------------------------
    //Global para almacenar algunos datos de planilla de reemplazo.-
    public class Tx_AnuReem
    {
        public string PrtCli;
        public short IndDir;
        public short IndNom;
        public short CodMon;
        public string NemMon;
        public double monto;
        public short HayRee;
        public short TotPln;
        public short AcepAnu;
        public short AcepRee;
        public double ConvRee;
        public double ConvAnu;
        public double CambRee;
        public double CambAnu;
    }
    public class T_MODANUVI
    {
        //Globales para la Anulacion de Planillas Visibles de Importacion
        //------------------------------------------------------------------
        // UPGRADE_INFO (#0561): The 'MsgAnuVi' symbol was defined without an explicit "As" clause.
        public const string MsgAnuVi = "Anulación de Planillas Visibles";
        // UPGRADE_INFO (#0561): The 'MsgRemPv' symbol was defined without an explicit "As" clause.
        public const string MsgRemPv = "Reemplazo de Planillas Visibles";
        // UPGRADE_INFO (#0561): The 'MsgPlaSO' symbol was defined without an explicit "As" clause.
        public const string MsgPlaSO = "Planillas Sin Operación";
        public short Var_CodMon;
        public string FtoSal = "";
        public double Var_TipCam;
        public T_AnuVi[] V_Plani;
        public T_AnuVi[] V_PlAnu;
        public Tx_AnuReem Vx_AnuReem;
        public Tx_AnuReem Vx_AnuReemNull;

        public T_MODANUVI()
        {
            V_Plani = new T_AnuVi[0];
            V_PlAnu = new T_AnuVi[0];
            Vx_AnuReem = new Tx_AnuReem();
            Vx_AnuReemNull = new Tx_AnuReem();

        }
    }
}
