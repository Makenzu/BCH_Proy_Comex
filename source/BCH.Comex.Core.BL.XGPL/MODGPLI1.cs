
namespace BCH.Comex.Core.BL.XGPL
{
    public static class MODGPLI1
    {
        public struct T_Pli
        {
            public string NumPli;   //# Planilla Invisible.
            public string FecPli;   //Fecha Planilla Invisible.
            public string FecAnt;   //Fecha Planilla (FecPli) antes de ser modificada.
            public string Cencos;   //Centro Costo dueño.
            public string CodUsr;   //Especialista dueño.
            public string Fecing;   //Fecha de Ingreso.
            public string fecact;   //Fecha Actualización.
            public string codcct;   //Centro de Costo Ope. asociada.
            public string CodPro;   //Producto Operación   asociada.
            public string codesp;   //Especialista Ope.    asociada.
            public string codofi;   //Empresa Operación    asociada.
            public string codope;   //Correlativo Ope.     asociada.
            public string CodAnu;   //Código de Anulación.
            public int estado;   //Estado Planilla.
            public string CodOper;   //Operacion.
            public int PlzBcc;   //Plaza B. Central Contabiliza.
            public string RutCli;   //Rut del Cliente.
            public string PrtCli;   //Llave del Cliente.
            public int IndNom;   //Indice Nombre.
            public int IndDir;   //Indice Direccion.
            public int CodOci;   //Código de Comercio
            public int TipPln;   //Tipo de Planilla.
            public string CodCom;   //Código de Comercio.
            public string ConCep;   //Concepto.
            public string AnuNum;   //Número de Planilla Anulada.
            public string AnuFec;   //Fecha de Planilla Anulada.
            public int AnuPbc;   //Código de Plaza Banco Central.
            public string ApcTip;   //Tipo de Anulación(Autorización Previa del Banco Central).
            public string ApcNum;   //Número de Planilla Anulada(Autorización Previa del Banco Central).
            public string ApcFec;   //Fecha de Planilla Anulada(Autorización Previa del Banco Central).
            public int ApcPbc;   //Código de Plaza Banco Central(Autorización Previa del Banco Central).
            public string Motivo;
            public int NumAcu;   // numero de acuerdos
            public string Desacu;
            public int CodPai;   //País.
            public int CodMnd;   //Código Moneda Planilla.
            public int CodMndBC;   //Código Moneda Banco Central.
            public double MtoOpe;   //Monto Bruto.
            public double Mtopar;   //Monto Paridad.
            public double MtoDol;   //Monto Dólares.
            public double TipCam;   //Tipo de Cambio.
            public double MtoNac;   //Monto Comisiones.
            public string DieNum;   //DIE, Número Emisión.
            public string DieFec;   //DIE, Fecha  Emisión.
            public int DiePbc;   //DIE, Plaza B. Central.
            public string NumDec;   //Número Declaración.
            public string FecDec;   //Fecha Declaración.
            public int codadn;   //Código Aduana.
            public string CodEOR;   //Código de identificación de una reexportación
            public string FecDeb;   //Fecha Autorización Débito.
            public string DocNac;   //Documento Nacional.
            public string DocExt;   //Documento extranjero.
            public int BcoExt;   //Banco Extranjero.
            public int NumCre;   //Número del Crédito.
            public string FecCre;   //Fecha  del Crédito.
            public int MndCre;   //Moneda del Crédito.
            public double MtoCre;   //Monto  del Crédito.
            public string CodAcu;   //Código Acuerdo.
            public string RegAcu;   //Registro Acuerdo.
            public string RutAcu;   //Rut Acuerdo.
            public string ObsPli;   //Observaciones.
            public int Status;   //Uso interno x planillas.
            public int Acepto;   //Indica si Acepto el Frm.
            public int IndPrt;   //Indice Party en PartysOpe.
            public int TipMto;   //Uso Interno, 1-2-3 : L,I,E.
            public double DatImp;   //Datos Impuesto.
            public string FecIns;   //Fecha Inscripción.
            public string NomFin;   //Nombre Financista.
            public string FecVen;   //Fecha Vencimiento Capital.
            public int ZonFra;
            public int SecBen;
            public int SecInv;
            public double PrcPar;
        }
    }
}
