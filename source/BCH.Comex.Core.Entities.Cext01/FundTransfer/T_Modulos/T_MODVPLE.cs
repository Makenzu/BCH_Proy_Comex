
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{ 
    public class T_IntPla
    {
        public string codcct;  //Centro de Costo.
        public string codpro;  //Producto.
        public string codesp;  //Especialista.
        public string codofi;  //Oficina.
        public string codope;  //Operacion.
        public int NroPln;  //Número de la Planilla
        public short NroLin;  //Número línea interes
        public string FecPln;
        public short Concep;  //Concepto interes
        public short TipInt;  //Tipo Interes
        public double CapBas;  //Capital base
        public double CodBas;  //Código base tasa
        public double TasInt;  //Tasa interes
        public string FecIni;  //Fecha inicial
        public string FecFin;  //Fecha final
        public short NumDia;  //Número de días
        public double MtoInt;  //Monto interes
        public short FlgEli;  //True si fué eliminado
    }

    //Defino Estructura carga datos planilla estadistica
    public class T_PlnvEs
    {
        public int NroPln;  //Número de la Planilla
        public string CodPla;  //Código de Planilla
        public string FecVta;  //Fecha de Venta
        public short CodPlz;  //Codigo Plaza Banco central
        public string NomImp;  //Nombre del Importador
        public string RutImp;  //Rut del Importador
        public string NumIdi;  //Número del Idi
        public string FecIdi;  //Fecha de Aprobación del Idi
        public string numdec;  //Número de la Declaración
        public string FecDec;  //Fecha de la Declaración
        public short CodPem;  //Código plaza emisión
        public short codfdp;  //Código forma de pago
        public string NumCon;  //Nº Conocimiento de Embarque
        public string FecCon;  //Fecha del Conocimiento del Embarque
        public string FecVop;  //Fecha Vencimiento de la operación
        public int NroCcp;  //Nro. de cuotas cuadro de pago
        public short NroDcp;  //Nro. de la cuota del cuadro de pago
        public string FecAnu;  //Fecha anulación anulación
        public double Valpar;  //Valor paridad
        public double MtoAnu;  //Monto valor total anulación
        public short CanAco;  //Cant. acuerdo que se acoge
        public short Acoge1;
        public short Acoge2;
        public short NroPreCob;  //Nro. presentación cobertura
        public string FechaCob;  //Fecha cobertura
        public short CodbchCob;  //Código plaza banco central cobertura
        public short CodEntCob;  //Código entidad cobertura
        public short NumIdiCob;  //Número informe importación cobertura
        public string FecEmiCob;  //Fecha Emisión Cobertura
        public short PlzEmiCob;  //Fecha Emision informe Cobertura
        public short NroConCob;  //Número conocimiento Cobertura
        public string FecConCob;  //Fecha Conocimiento Cobertura
        public short CodPaiPag;  //Código pais de pago
        public short CodMndBcc;  //Código moneda banco central de chile
        public short CodMndBch;  //Código moneda banco de chile
        public double ValMer;  //Valor de la Mercadería
        public double HasFob;  //Valor gastos hasta fob
        public double FobOri;  //Valor del Fob cubierto en la Planilla
        public double FleOri;  //Valor del Flt cubierto en la Planilla
        public double SegOri;  //Valor del Seg cubierto en la Planilla
        public double CifOri;  //Valor del Cif cubierto en la Planilla
        public double IntOri;  //Valor del Interes cubierto
        public double GasBco;  //Gastos del Banco Cedente
        public double TotOri;  //Total de lo Cubierto
        public double CifDol;  //Valor del Cif en US dollar
        public double TotDol;  //Valor total en us dollar
        public double ParDol;  //Paridad a dolar
        public short UsaConv;  //Usa convenio credito Reciroco
        public string FecAutDeb;  //Fecha Autorización débito Conv.Cre.Rec. en chile
        public string NroDocChi;  //Nro.Documento Convenio Cre. Reciproco en chile
        public string NroDocExt;  //Nro.Documento Convenio Cre. Reciproco en el extranjero
        public double ParDec;  //Paridad de la declaración
        public double ParPla;  //Paridad de la planilla
        public string observ;  //Observaciones generales
        public string ObsDec;  //Observaciones para la Planilla
        public string ObsCob;  //Observaciones para la Planilla
        public short DefDec;  //indica si se definió la declaración
        public short DefInt;  //indica si se definió los intereses
        public short DefPle;  //indica si aprobo la planilla
        public short PagAnt;  //indica si es pago anticipado
                              //Datos del Reemplazo
                              //------------------------------
        public int NumPln_R;
        public string FecPln_R;
        public short CodPlz_R;
        public short CodEnt_R;
        public int NumInf_R;
        public string FecInf_R;
        public short PlzInf_R;
        public string NumCon_R;
        public string FecCon_R;
        //------------------------------
        public short HayAnu;
        public short HayRpl;
    }

    public class T_MODVPLE
    {
        public T_PlnvEs[] PlnVEst;
        public T_IntPla[] IntPla;  //Intereses Planilla de reemplazo
        public T_IntPla[] IntAnu;  //Intereses de de Planilla Anulacion
        
        public T_MODVPLE()
        {
            IntPla = new T_IntPla[0];
            PlnVEst = new T_PlnvEs[0];
            IntAnu = new T_IntPla[0];
        }
    }     
}
