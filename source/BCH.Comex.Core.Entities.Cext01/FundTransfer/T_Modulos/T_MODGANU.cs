
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //Variable para Anulación de Compra-Venta.
    public class T_Anu
    {
        public string codcct;  //Centro de Costo.
        public string codpro;  //Producto.
        public string codesp;  //Especialista.
        public string codofi;  //Oficina.
        public string codope;  //Operacion.
        public string CodOpe_t;  //Operacion.
        public string PrtCli;  //Participante.
        public short IndNom;  //Nombre.
    }

    //APC : Autorización Previa del Banco Central
    //Planillas asociadas a una Compra Venta.
    public class T_AnuPl
    {
        public short TipDoc;  //1: Ing, 2: Egr, 3:Anu I, 4: Anu E, 8: Trn I, 9: Trn E, 10: Anu TI, 11: Anu TE.
        public short TipAnu;  //Tipo de Anulación
        public string NumPln;  //# Planilla.
        public string FecPln;  //Fecha Planilla.
        public string VisInv;  //Tipo : VIS/INV.
        public short TipPln;  //1:Ingreso;2:Egreso.-
        public short CodMnd;  //Moneda.
        public short CodMndBC;  //Moneda Banco Central.
        public double MtoPln;  //Monto Líquido.
        public double TipCam;  //Tipo de Cambio US$(Inv).-
        public double TipCamo;  //Tipo de Cambio MO (Inv).-
        public string Motivo;
        public string NemMnd;
        public string MtoPln_t;
        public double MtoAnu;
        public string ObsPln;
        public string ApcTip;  //Tipo.-
        public string ApcNum;  //Número.-
        public string ApcFec;  //Fecha.-
        public short PlzBcc;  //Plaza B. Central.-
        public string numdec;  //Número Declaración.-
        public string FecDec;  //Fecha  Declaración.-
        public short CodAdn;  //Aduana.-
        public string PrtExp;  //Exportador.-
        public string codcom;  //Código de Comercio.-
        public short estado;  //Estado de la Planilla.
        //-------------------------------------------------
        public double ValCla;  //V. Claúsula Declaración.-
        public double ValCom;  //V. Comisiones Declaración.-
        public double OtrGas;  //V. Otros Gastos Declaración.-
        public double ValLiq;  //V. Líquido Declaración.-
    }

    //Datos de Vías y Orígenes de una Operación.-
    public class T_ViaOri
    {
        public short CodMnd;
        public double MtoMnd;
        public string ViaOri;
    }

    public class T_Comis
    {
        public string glocon;
        public string ctamn;
        public string ctame;
        public short hayiva;
    }

    public class T_MODGANU
    {
        public T_Anu VAnu;
        public T_AnuPl[] VAnuPl;
        public T_Comis[] Comis;

        // UPGRADE_INFO (#0561): The 'LstMarca' symbol was defined without an explicit "As" clause.
        public const string LstMarca = "»";

        public T_MODGANU()
        {
            VAnu = new T_Anu();
            VAnuPl= new T_AnuPl[0];
            Comis = new T_Comis[0];
        }
    }
}
