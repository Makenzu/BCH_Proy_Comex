
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class T_xAnu
    {
        public string NumPre;  //# Presentación.
        public string fecpre;  //Fecha Presentación.
        public string cencos;  //Centro Costo dueño.
        public string codusr;  //Especialista dueño.
        public string Fecing;  //Fecha de Ingreso.
        public short Estado;  //Estado Planilla.
        //-------------------------------------------------------
        public string codcct;  //Centro de Costo Ope. asociada.
        public string codpro;  //Producto Operación   asociada.
        public string codesp;  //Especialista Ope.    asociada.
        public string codofi;  //Empresa Operación    asociada.
        public string codope;  //Correlativo Ope.     asociada.
        //-------------------------------------------------------
        public short TipAnu;  //Tipo de Anulación.
        public short PlzBcc;  //Plaza B. Central Contabiliza.
        //-------------------------------------------------------
        public string RutExp;  //Rut del Exportador.
        public string PrtExp;  //Llave del Exportador.
        public short IndNom;  //Indice Nombre.
        public short IndDir;  //Indice Direccion.
        //-------------------------------------------------------
        public short EntAut;  //Entidad que Autoriza.
        public string NumpreO;  //# Presentación Original.
        public string FecpreO;  //Fecha Presentación Original.
        public short TipPln;  //Tipo de Planilla.
        //-------------------------------------------------------
        public short CodPbc;  //Código de Plaza Banco Central.
        public string numdec;  //Número Declaración.
        public string FecDec;  //Fecha Declaración.
        public short CodAdn;  //Código Aduana.
        public string FecVen;  //Fecha Ven. Retorno.
        //-------------------------------------------------------
        public short CodMnd;  //Código Moneda Planilla.
        public double MtoDol;  //Monto Dólares.
        public double Mtopar;  //Monto Paridad.
        public double MtoAnu;  //Monto Anulación.
        public double MtoParA;  //Monto Paridad de Anulación.
        public double MtoDolA;  //Monto Dólares de Anulación.
        public double MtoDolPo;  //Monto Dólares Paridad Original.
        //-------------------------------------------------------
        public string ObsPln;  //Observaciones.
        public short Acepto;  //Aceptar
        public short PlnEst;  //indice si la planilla es estadistica
        //-------------------------------------------------------
        public string TipAut;  //Tipo de Autorización.-
        public double NroAut;  //Nro. de Autorización.-
        public string FecAut;  //Fecha de Autorización.-
        public double TipCam;  //Tipo de Cambio.-
        public short PlnOK;  //Flag que indica que la planilla fue validada.-
    }

    //**********Arreglo para la Tabla de Planillas Anuladas.**********
    public class T_xAnu1
    {
        public string NumPre;
        public string fecpre;
        public string codusr;
        public string codope;
        public short Estado;
        public short CodMnd;
        public double MtoAnu;
        public short TipPln;
    }

    public class T_GAnu
    {
        public string codcct;
        public string codpro;
        public string codesp;
        public string codofi;
        public string codope;
        public short AnuSin;
        public short AnuSinOK;
    }
    
    public class T_MODXANU
    {
        public T_xAnu[] VxAnus;
        public T_GAnu VgAnu;
        //*************************************************************
        // UPGRADE_INFO (#0561): The 'MsgxAnu' symbol was defined without an explicit "As" clause.
        public const string MsgxAnu = "Anulación de Planillas Visibles";

        public bool Habilita { set; get; }

        public T_MODXANU()
        {
            VxAnus = new T_xAnu[0];
            VgAnu = new T_GAnu();
        }
    }
}
