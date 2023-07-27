using System;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class TipoAladi
    {
        public string Fecha;
        public string NDoc;
        public short IndECV;
    }

    public class R_Idi
    {
        public string RIdi_numidi;
        public string RIdi_fechaidi;
        public string RIdi_principal;
        public double RIdi_montorel;
        public double RIdi_montocub;
        public double RIdi_dispodec;  //monto disponible para relacionar en las declaraciones asociadas al idi
        public double RIdi_reldec;  //monto ya relacionado en las declaraciones asociadas al idi
        public short Ridi_codmoneda;
        public string Ridi_moneda;
        public string Ridi_nemmoneda;
        public double RIdi_paridad;
        public short RIdi_status;
        public short RIdi_UsoTol;
        public short RIdi_ImpDiv;
    }

    public class Idi
    {
        public string Num_Aproba;
        public string Fecha_Aproba;
        public string Num_Pres;
        public string Fecha_Pres;
        public short estado;
        public string fecha_vcto;
        public double orig;  //orig_cob + orig_otr
        public double Rel;  //rel_cob + rel_otr
        public double Cub;  //cub_cob + cub_otr
        public double Dis;  //dis_cob + dis_otr
        public double orig_cob;
        public double Rel_Cob;
        public double Cub_Cob;
        public double Dis_Cob;
        public double orig_Con;
        public double rel_Con;
        public double cub_Con;
        public double dis_Con;
        public double orig_Cre;
        public double rel_Cre;
        public double cub_Cre;
        public double dis_Cre;
        public double orig_otr;
        public double rel_otr;
        public double cub_otr;
        public double Dis_Otr;
        public double orig_tol;
        public double rel_tol;
        public double reltol;  //Monto recién relacionado
        public double MtoCif;
        public double MtoUsd;
        public double MtoRel;
        public double mtocub;
        public double MtoDis;
        public short cod_moneda;
        public string Nom_Moneda;
        public string nem_moneda;
        public double Paridad;
        public short cambio;
        public short Indice;  //sólo lo usa IdiFin()
        public short PaiAdq;
        public short ForPag;
        public short PlzApr;
        public short OtroPais;
        public short ImpDiv;
    }

    public class Type_Idi
    {
        public string PrtImp;
        public int NumApr;
        public string FecApr;
        public int NumPre;
        public string fecpre;
        public short estado;  //Estados del IDI (ver *) .-
        public short DiaEmb;  //# de días para el embarque.-
        public string FecVto;  //Fecha Vencimiento = FecApr + DiaEmb.-
        public string FecEli;  //Fecha en que se rechaza el Idi.-
        public short IndNom;  //Indice del Nombre del Party.-
        public short IndDir;  //Indice de la Dirección del Party.-
        public short CodNab;  //Código Nab de la Mercadería.-
        public string BcoPre;  //Código Bco. que presenta el IDI.-
        public short PlzPre;  //Plaza  Bco. Central d/ se presenta el IDI.-
        public string BcoApr;  //Código Banco que aprueba el IDI.-
        public short PlzApr;  //Plaza del Bco. Central d/ se aprueba el IDI.-
        public short PaiOri;  //Código de País Origen de la Merc.-
        public short PaiAdq;  //Código de País donde adquirió la Merc.-
        public short EfpCob;  //Existe Forma Pago Cobranza?.-
        public short EfpCre;  //Existe Forma Pago Crédito?.-
        public short EfpCon;  //Existe Forma Pago Contado?.-
        public short EfpOtr;  //Existe Forma Pago Otros?.-
        public short NfpCob;  //# de Forma Pago Cobranza.-
        public short NfpCre;  //# de Forma Pago Crédito.-
        public short NfpCon;  //# de Forma Pago Contado.-
        public short NfpOtr;  //# de Forma Pago Otros.-
        public short DFPCob;  //# de días para pago en Cobranza.-
        public short DFPCre;  //# de días para pago en Crédito.-
        public short DFPCon;  //# de días para pago en Contado.-
        public short DFPOtr;  //# de días para pago en Otros.-
        public double OriCob;  //Monto Original    a Cobertura.-
        public double relcob;  //Monto Relacionado a Cobertura.-
        public double cubcob;  //Monto Cubierto    a Cobertura.-
        public double DisCob;  //Monto Disponible  a Cobertura.-
        public double OriCre;  //Monto Original    a Crédito.-
        public double RelCre;  //Monto Relacionado a Crédito.-
        public double CubCre;  //Monto Cubierto    a Crédito.-
        public double DisCre;  //Monto Disponible  a Crédito.-
        public double OriCon;  //Monto Original    a Contado.-
        public double RelCon;  //Monto Relacionado a Contado.-
        public double CubCon;  //Monto Cubierto    a Contado.-
        public double DisCon;  //Monto Disponible  a Contado.-
        public double OriOtr;  //Monto Original    a Otros.-
        public double relotr;  //Monto Relacionado a Otros.-
        public double cubotr;  //Monto Cubierto    a Otros.-
        public double DisOtr;  //Monto Disponible  a Otros.-
        public double OriTol;  //Monto Original    a Tolerancia.-
        public double reltol;  //Monto Relacionado a Tolerancia.-
        public short CodMon;  //Código de Moneda del IDI.-
        public double Valpar;  //Valor de la Paridad (para que fecha?).-
        public double MtoCif;  //Monto CIF.-
        public double MtoUsd;  //Monto el dólares.-
        public double MtoRel;  //Monto Total Relacionado.-
        public double mtocub;  //Monto Total Cubierto.-
        public double MtoDis;  //Monto Total Disponible.-
        public short NroDec;  //Número de Declaraciones asociadas.-
        public short OtrPai;  //Indica si además existe otro País.-
        public short TraHac;  //Indica si ha sido traspasado hacia otro Banco.-
        public short Trades;  //Indica si ha sido traspasado desde otro Banco.-
        public short EnLock;  //Indica si está lockeado por otro usuario.-
        public string CCosto;  //Centro de Costo del dueño del Idi.-
        public string UsrEsp;  //Especialista dueño del Idi.-
        public short ConTrs;  //Idi Traspasado.-
        public short IdiCom;  //Idi Compartido.-
        public short ImpDiv;  //Impuesto a las divisas.-
    }

    //Estructura Compras, Ventas, Arbitrajes o Anulaciones
    public class estr_cv
    {
        public short EleAmb;  //Importaciones, Exportaciones, Cambios
        public short EleTip;  //Compra, Venta, Arbitraje, Anulacion, Reverso
        public string Codigo;  //Codigo de Comercio
        public string Concep;  //Concepto
        public string subprd;  //Flete = 0, Seguro = 1, Flete y Seguro = 2, endoso recibido = 3
        public short[] CodMon;  //2 para las Ventas, 1 para las Compras en arbitrajes
        public double[] monto;  //2 para las Ventas, 1 para las Compras en arbitrajes
        public string[] Moneda;  //2 para las Ventas, 1 para las Compras en arbitrajes
                                 //Si es una compra o una venta el 1 solamente se usa
        public string PaiOri;  // nombre del pais de origen
        public short codpai;  // código del pais de origen
        public double[] TipCam;
        public double[] Mto_Us;
        public double Tot_Us;
        public double[] mtopss;  //monto pesos de compras y ventas, 1 compra 2 venta
        public double TotPss;
        public double[] ParCvd;
        public double PdrArb;
        public double TCVtUs;  //Tasa de Cambio de Venta Usado
        public short ModMan;  //Cuando los datos de un arbitraje fueron ingresados manualmente
        public string[] ObsCvd;  //Las Mismas para la Planilla de la Venta y de la Compra
        public short EsAlad;  //Nos indica si tiene asociado Información aladi de pago Aladi
        public string FecDeb;
        public string NroDoc;
        public short Borrado;
        public short estado;
        public string ObsPrd;  //Paridad del Arbitraje para Carta del Cliente
        public double MtoCob;  //Mto a Cobertura para Impuesto Vta. Divisas
        public double MtoCob2;  //Mto a Cobertura para Impuesto Vta. Divisas
        public string FecVenc;  //Fecha de Vencimiento de Planillas
        public short CancEndo;  //cancelo la creación de planillas en el endoso.-

        #region Initialization

        public estr_cv(bool dummyArg)
        {
            CodMon = new short[3];
            monto = new double[3];
            Moneda = new string[3];
            TipCam = new double[3];
            Mto_Us = new double[3];
            mtopss = new double[3];
            ParCvd = new double[3];
            ObsCvd = new string[5];
            EleAmb = 0;
            EleTip = 0;
            Codigo = "";
            Concep = "";
            subprd = "";
            PaiOri = "";
            codpai = 0;
            Tot_Us = 0;
            TotPss = 0;
            PdrArb = 0;
            TCVtUs = 0;
            ModMan = 0;
            EsAlad = 0;
            FecDeb = "";
            NroDoc = "";
            Borrado = 0;
            estado = 0;
            ObsPrd = "";
            MtoCob = 0;
            MtoCob2 = 0;
            FecVenc = "";
            CancEndo = 0;
        }

        #endregion

       
    }

    public class Reg_Planilla
    {

        public short Tipo_registro;
        public string Fecha_visacion;
        public string num_planilla;  //Número de la Planilla
        public string fecha_venta;  //Fecha de Venta
        public string NomImport;  //Nombre del Importador
        public string rut;  //Rut del Importador
        public string num_idi;  //Número del Idi
        public string fecha_idi;  //Fecha de Aprobación del Idi
        public string numdec;  //Número de la Declaración
        public string FecDec;  //Fecha de la Declaración
        public string num_conocimiento;  //Nº Conocimiento de Embarque
        public string fecha_conocimiento;  //Fecha del Conocimiento del Embarque
        public string Codigo;  //Código de Planilla
        public short CodBCCh;  //Codigo del Banco de Chile
        public short Cod_Plaza;  //Codigo Plaza Banco
        public string NombrePlaza;
        public short Cod_FormaPago;  //Codigo de la forma de Pago
        public short Cod_Paispago;  //Codigo del País de Pago
        public string Paispago;  //Nombre del País de Pago
        public short cod_moneda;  //Codigo de la Moneda del Pago
        public short Cod_Mon;  //Cod bch
        public string nombre_moneda;  //Nombre de la Moneda de Pago
        public string NemMoneda;  //Nemonico de la Moneda de Pago

        public double Paridad;  //Paridad del Pago
        public double tipo_cambio;  //Valor del tipo de Cambio a la fecha
        public double Mercaderia;  //Valor de la Mercadería
        public double HastaFob;  //Valor de la Mercadería
        public double Fob_Origen;  //Valor del Fob cubierto en la Planilla
        public double Flete_Origen;  //Valor del Flt cubierto en la Planilla
        public double Seguro_Origen;  //Valor del Seg cubierto en la Planilla
        public double Cif_Origen;  //Valor del Cif cubierto en la Planilla
        public double Interes_Origen;  //Valor del Interes cubierto
        public double Gastos_Banco;  //Gastos del Banco Cedente
        public double Total_Origen;  //Total de lo Cubierto
        public double Cif_Dolar;  //Valor del Cif en US dollar
        public double Total_Dolar;  //Valor total en us dollar
        public string fecha_vencimiento;
        public short HayCuadro;
        public short Num_Cuadro;
        public short num_cuotas;
        public short HayAcuerdo;
        public short num_acuerdos;
        public string[] acuerdo;
        public short HayAnula;
        public string Vencanula;
        public short indice_anulacion;
        public short num_registro;
        public double paridad_anulacion;
        public double total_anulacion;
        public string fecha_anulacion;
        //Datos del Reemplazo
        public string nplar;
        public string FPLaR;
        public short CodPlaR;
        public short EntPlaR;
        public int NInfR;
        public string FInfR;
        public short PlaInfR;
        public string NBLR;
        public string FBLR;
        public string fecha_debito;
        public string NDoc1;
        public string NDoc2;
        public short Status;
        public short Todas;  //Indicador de Observaciones para planillas posteriores
        public string Observacion;  //Observaciones para la Planilla
        public string ObsDecl;  //Observaciones para la Planilla
        public string ObsMerma;  //Observaciones para la Planilla
        public string ObsParidad;  //Observaciones para la Planilla
        public string ObsCobranza;  //Observaciones para la Planilla
        public string LineaObs1;  //Observaciones para la Planilla
        public string LineaObs2;  //Observaciones para la Planilla
        public string LineaObs3;  //Observaciones para la Planilla

        public short Lista;
        public short ConInteres;
        public short OkInteres;
        public string NDecs;
        public short Modifico;
        public short ModObs;
        public short ModInt;
        public short Marcada;
        public short ZonFra;

        #region Initialization

        public Reg_Planilla()
        {
            acuerdo = new string[5];
            Tipo_registro = 0;
            Fecha_visacion = "";
            num_planilla = "";
            fecha_venta = "";
            NomImport = "";
            rut = "";
            num_idi = "";
            fecha_idi = "";
            numdec = "";
            FecDec = "";
            num_conocimiento = "";
            fecha_conocimiento = "";
            Codigo = "";
            CodBCCh = 0;
            Cod_Plaza = 0;
            NombrePlaza = "";
            Cod_FormaPago = 0;
            Cod_Paispago = 0;
            Paispago = "";
            cod_moneda = 0;
            Cod_Mon = 0;
            nombre_moneda = "";
            NemMoneda = "";
            Paridad = 0;
            tipo_cambio = 0;
            Mercaderia = 0;
            HastaFob = 0;
            Fob_Origen = 0;
            Flete_Origen = 0;
            Seguro_Origen = 0;
            Cif_Origen = 0;
            Interes_Origen = 0;
            Gastos_Banco = 0;
            Total_Origen = 0;
            Cif_Dolar = 0;
            Total_Dolar = 0;
            fecha_vencimiento = "";
            HayCuadro = 0;
            Num_Cuadro = 0;
            num_cuotas = 0;
            HayAcuerdo = 0;
            num_acuerdos = 0;
            HayAnula = 0;
            Vencanula = "";
            indice_anulacion = 0;
            num_registro = 0;
            paridad_anulacion = 0;
            total_anulacion = 0;
            fecha_anulacion = "";
            nplar = "";
            FPLaR = "";
            CodPlaR = 0;
            EntPlaR = 0;
            NInfR = 0;
            FInfR = "";
            PlaInfR = 0;
            NBLR = "";
            FBLR = "";
            fecha_debito = "";
            NDoc1 = "";
            NDoc2 = "";
            Status = 0;
            Todas = 0;
            Observacion = "";
            ObsDecl = "";
            ObsMerma = "";
            ObsParidad = "";
            ObsCobranza = "";
            LineaObs1 = "";
            LineaObs2 = "";
            LineaObs3 = "";
            Lista = 0;
            ConInteres = 0;
            OkInteres = 0;
            NDecs = "";
            Modifico = 0;
            ModObs = 0;
            ModInt = 0;
            Marcada = 0;
            ZonFra = 0;
        }

        #endregion

        public Reg_Planilla Copy()
        {
            return (Reg_Planilla)this.MemberwiseClone();
        }
    }

    public class Detalles
    {
        public string Cent_Costo;
        public string Id_Product;
        public string Id_Especia;
        public string Id_Empresa;
        public string Id_Operacion;
        public int NumPlan;
        public short Num;
        public short Concepto;
        public string Tipo;
        public double CapBas;
        public short CodBas;
        public double Tasa;
        public string FIni;
        public string FFin;
        public short ndias;
        public double monto;
        public short Modifico;
    }

    //Formas de Pago
    public class T_Fdp
    {
        public short codfdp;
        public string NomFdp;
        public string ProFdp;

        #region Initialization

        public T_Fdp(bool dummyArg)
        {
            NomFdp = String.Empty;
            ProFdp = String.Empty;
            codfdp = 0;
        }

        #endregion
    }

    public class Tipo_CVD
    {
        public short EsVisible;
        public short Operacion;
        public short ConInteres;
        public string BcoEnd;
        public double Int;
        public short otro;
    }

    public class dec
    {
        public short estado;
        public string Num_Acepta;
        public string Fecha_Acepta;
        public string fec_emb;
        public string num_idi;
        public string fecha_idi;
        public double mtomer;
        public double dis_mer;
        public double rel_mer;
        public double cub_mer;
        public short uso_fle;
        public short uso_seg;
        public double orig_fob;
        public double rel_fob;
        public double cub_fob;
        public double dis_fob;
        public double orig_fle;
        public double Rel_Fle;
        public double Cub_Fle;
        public double Dis_Fle;
        public double orig_seg;
        public double Rel_Seg;
        public double Cub_Seg;
        public double Dis_Seg;
        public double orig_cif;
        public double Rel_Cif;
        public double Cub_Cif;
        public double Dis_Cif;
        public short cod_moneda;
        public string Nom_Moneda;
        public string nem_moneda;
        public short cambio;
        public short Indice;  //sólo lo usa DecCob
        public short NumCCo;
        public short FlgEli;
    }

    public class tipo_blw
    {
        public string numdec;
        public string FecDec;
        public string NumBlw;
        public string FecBlw;
    }

    //estructura para paridades en cálculo acceso dispo. en Pago en Chile
    public class paridades
    {
        public short Indice;
        public double Paridad;
        public double monto;
        public short ind_idi;
    }
    public class T_MODGFYS
    {

        // UPGRADE_INFO (#0561): The 'Impresa' symbol was defined without an explicit "As" clause.
        public const short Impresa = 5;
        // UPGRADE_INFO (#0561): The 'Endoba' symbol was defined without an explicit "As" clause.
        public const short Endoba = 6;
        // UPGRADE_INFO (#0561): The 'Estadis' symbol was defined without an explicit "As" clause.
        public const short Estadis = 8;
        // UPGRADE_INFO (#0501): The 'AceptoDec' member isn't used anywhere in current application.
        public static short AceptoDec;
        // UPGRADE_INFO (#0561): The 'FLT' symbol was defined without an explicit "As" clause.
        public const short FLT = 0;
        // UPGRADE_INFO (#0561): The 'SEG' symbol was defined without an explicit "As" clause.
        public const short SEG = 1;
        // UPGRADE_INFO (#0561): The 'FLTSEG' symbol was defined without an explicit "As" clause.
        public const short FLTSEG = 2;
        // UPGRADE_INFO (#0561): The 'ENDREC' symbol was defined without an explicit "As" clause.
        public const short ENDREC = 3;
        // UPGRADE_INFO (#0561): The 'PLANEST' symbol was defined without an explicit "As" clause.
        public const short PLANEST = 4;

        public  R_Idi[] RIdiIni;
        public  R_Idi[] RIdiFin;
        public Detalles[] DetInt;

        public  Idi[] IdiIni;
        public  Idi[] Idifin;
        public  estr_cv[] VgFyS;
        public  estr_cv AuxBase ;
        public  Reg_Planilla[] Planillas;
        public  T_Fdp[] VFdp;
        public  dec[] DecFin;
        public  short TipoAct;
        public  short Es_Nuevo;
        public Tipo_CVD CVD ;

        public T_MODGFYS(){
            AuxBase = new estr_cv(true);
            RIdiIni = new R_Idi[0];
            RIdiFin = new R_Idi[0];
            IdiIni = new Idi[0];
            Idifin = new Idi[0];
            VgFyS = new estr_cv[0];
            Planillas = new Reg_Planilla[0];
            VFdp = new T_Fdp[0];
            DecFin = new dec[0];
            CVD = new Tipo_CVD();
        }

    }
}
