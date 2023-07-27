
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //******************************************************
    //Estructura de Planillas Invisibles.
    //******************************************************
    //********************************************************************************
    //   Autor                      : Accenture - Continuidad Comex
    //   Incidente                  : PBI700000244341
    //   Descripcion                : Problema planillas de comercio invisible operaciones de arbitraje, para solucionar problema
    //                                se cambia la declaración de uno de los campos (CodOci)Antes integer, ahora Double.
    //   Fecha                      : Agosto de 2012
    //   Identificador de Inicio  : ACC-001-I
    //   Identificador de Termino : ACC-001-F
    //********************************************************************************
    public class T_Pli
    {
        public string NumPli;  //# Planilla Invisible.
        public string FecPli;  //Fecha Planilla Invisible.
        public string cencos;  //Centro Costo dueño.
        public string codusr;  //Especialista dueño.
        public string Fecing;  //Fecha de Ingreso.
        public string FecAct;  //Fecha Actualización.
        public string codcct;  //Centro de Costo Ope. asociada.
        public string codpro;  //Producto Operación   asociada.
        public string codesp;  //Especialista Ope.    asociada.
        public string codofi;  //Empresa Operación    asociada.
        public string codope;  //Correlativo Ope.     asociada.
        public string CodAnu;  //Código de Anulación.
        public short Estado;  //Estado Planilla.
        public string CodOper;  //Operacion.
        public short PlzBcc;  //Plaza B. Central Contabiliza.
        public string rutcli;  //Rut del Cliente.
        public string PrtCli;  //Llave del Cliente.
        public short IndNom;  //Indice Nombre.
        public short IndDir;  //Indice Direccion.
        public double CodOci;  //Código de Comercio  ACC-001-I,  ACC-001-F
        public short TipPln;  //Tipo de Planilla.
        public string codcom;  //Código de Comercio.
        public string Concep;  //Concepto.
        public string AnuNum;  //Número de Planilla Anulada.
        public string AnuFec;  //Fecha de Planilla Anulada.
        public short AnuPbc;  //Código de Plaza Banco Central.
        public string ApcTip;  //Tipo de Anulación(Autorización Previa del Banco Central).
        public string ApcNum;  //Número de Planilla Anulada(Autorización Previa del Banco Central).
        public string ApcFec;  //Fecha de Planilla Anulada(Autorización Previa del Banco Central).
        public short ApcPbc;  //Código de Plaza Banco Central(Autorización Previa del Banco Central).
        public string Motivo;
        public short NumAcu;  // numero de acuerdos
        public string Desacu;
        public short codpai;  //País.
        public short CodMnd;  //Código Moneda Planilla.
        public short CodMndBC;  //Código Moneda Banco Central.
        public double MtoOpe;  //Monto Bruto.
        public double Mtopar;  //Monto Paridad.
        public double MtoDol;  //Monto Dólares.
        public double TipCam;  //Tipo de Cambio.
        public double MtoNac;  //Monto Comisiones.
        public string DieNum;  //DIE, Número Emisión.
        public string DieFec;  //DIE, Fecha  Emisión.
        public short DiePbc;  //DIE, Plaza B. Central.
        public string numdec;  //Número Declaración.
        public string FecDec;  //Fecha Declaración.
        public short CodAdn;  //Código Aduana.
        public string CodEOR;  //Código de identificación de una reexportación
        public string FecDeb;  //Fecha Autorización Débito.
        public string DocNac;  //Documento Nacional.
        public string DocExt;  //Documento extranjero.
        public short BcoExt;  //Banco Extranjero.
        public double NumCre;  //Número del Crédito.
        public string FecCre;  //Fecha  del Crédito.
        public short MndCre;  //Moneda del Crédito.
        public double MtoCre;  //Monto  del Crédito.
        public string CodAcu;  //Código Acuerdo.
        public string RegAcu;  //Registro Acuerdo.
        public string RutAcu;  //Rut Acuerdo.
        public string ObsPli;  //Observaciones.
        //-------------------------------------------------------
        public short Status;  //Uso interno x planillas.
        public short Acepto;  //Indica si Acepto el Frm.
        public short IndPrt;  //Indice Party en PartysOpe.
        public short TipMto;  //Uso Interno, 1-2-3 : L,I,E.
        //-------------------------------------------------------
        public double DatImp;  //Datos Impuesto.
        public string fecins;  //Fecha Inscripción.
        public string NomFin;  //Nombre Financista.
        public string fecpre;
        public int NumPre;
        public short codins;
        public int NumCon;
        public string fecsus;
        public string VenOd;
        public string VenOfi;
        public short insuti;
        public double partip;
        public short arecon;
        public int canacu;
        public short afeder;
        public short SecBen;
        public short SecInv;
        public short ZonFra;
        public double PrcPar;
        public short EsTrn;
        public string Cod_IE;

        public T_Pli Copy()
        {
            return (T_Pli)this.MemberwiseClone();
        }
    }

    //******************************************************
    //Arreglo para la Tabla de Planillas.-
    //******************************************************
    public class T_Plinv
    {
        public string NumPli;
        public string FecPli;
        public string codusr;
        public string codope;
        public short Estado;
        public short CodMnd;
        public double MtoOpe;
        public double MtoNac;
        public short TipPln;
    }
    
    public class T_MODGPLI1
    {
        public T_Pli[] Vplis;
        public short IndPli;  //Indica el índice de donde debe comenzar la visualización de la Planilla Invisible.
        // UPGRADE_INFO (#0561): The 'MsgPli' symbol was defined without an explicit "As" clause.
        public const string MsgPli = "Planillas Invisibles";
        //Título de la Caja de Mensajes de esta Pantalla.

        //******************************************************
        //Estado de las Planillas.-
        //******************************************************
        // UPGRADE_INFO (#0561): The 'EPli_Emi' symbol was defined without an explicit "As" clause.
        public const short EPli_Emi = 1;
        // UPGRADE_INFO (#0561): The 'EPli_Rev' symbol was defined without an explicit "As" clause.
        public const short EPli_Rev = 10;

        public T_MODGPLI1()
        {
            Vplis = new T_Pli[0];
        }
    }
}
