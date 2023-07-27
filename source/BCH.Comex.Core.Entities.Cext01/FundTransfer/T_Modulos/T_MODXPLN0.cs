
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //************************************************
    //Variables Generales de generación de Planillas.
    //************************************************
    public class T_xDatP
    {
        public string PrtExp;  //Exportador.-
        public string NumOpe;  //Operación nnn-nn-nn-nn-nnnnnn.-
        public short IndPrt;  //Indice Party en PartysOpe.-
        public short CodMnd;  //Moneda del Producto.
        public short CodMBC;  //Moneda Banco Central.
        public string NemMnd;  //Nemónico de la moneda.
        public double MtoLiq;  //Monto a Liquidar.
        public double MtoInf;  //Monto a Informar.
        public double mtotran;  //Monto Transferencia
        public double TipCam;  //Tipo de Cambio.
        public double Mtopar;  //Paridad.-
        public double MtoLiqs;  //Saldo a Liquidar.
        public double MtoInfs;  //Saldo a Informar.
        public double MtoTrans;  //Saldo Transferencia.
        public short Acepto;  //Indica si se acepto frm.-
    }
    //************************************************
    //Variables Generales Declaración de Exportación.
    //************************************************
    public class T_xDecP
    {
        public string numdec;  //Número Declaración.-
        public string FecDec;  //Fecha  Declaración.-
        public short CodAdn;  //Código Aduana.-
        public short Estado;  //Estado Dec.-
        public short TipDec;  //Tipo   Dec.-
        public short CodCCv;  //Clausula Venta.-
        public string PrtExp1;  //Party      Exp. Ppal.-
        public short IndNom1;  //Nombre     Exp. Ppal.-
        public short IndDir1;  //Dirección  Exp. Ppal.-
        //--------------------------------------------------
        public double ValRet1;  //Valores    Exp. Ppal.-
        public double ValCom1;
        public double ValGas1;
        public double ValLiq1;
        public double ValFle1;
        public double ValSeg1;
        //--------------------------------------------------
        public double ValRet1c;  //Valores    Exp. Ppal.-
        public double ValCom1c;
        public double ValGas1c;
        public double ValLiq1c;
        public double ValFle1c;
        public double ValSeg1c;
        //--------------------------------------------------
        public double ValRet1d;  //Valores    Exp. Ppal.-
        public double ValCom1d;
        public double ValGas1d;
        public double ValLiq1d;
        public double ValFle1d;
        public double ValSeg1d;
        //--------------------------------------------------
        public string PrtExp2;  //Party      Exp. Sec.-
        public short IndNom2;  //Nombre     Exp. Sec.-
        public short IndDir2;  //Dirección  Exp. Sec.-
        //--------------------------------------------------
        public double ValRet2;  //Valores    Exp. Sec.-
        public double ValCom2;
        public double ValGas2;
        public double ValLiq2;
        public double ValFle2;
        public double ValSeg2;
        //--------------------------------------------------
        public double ValRet2c;  //Valores    Exp. Sec.-
        public double ValCom2c;
        public double ValGas2c;
        public double ValLiq2c;
        public double ValFle2c;
        public double ValSeg2c;
        //--------------------------------------------------
        public double ValRet2d;  //Valores    Exp. Sec.-
        public double ValCom2d;
        public double ValGas2d;
        public double ValLiq2d;
        public double ValFle2d;
        public double ValSeg2d;
        //--------------------------------------------------
        public string FecRet;  //Fecha  máxima Retorno.-
        public short CodPbc;  //Plaza  B. Central.-
        public string NumInf;  //Número Informe.-
        public string FecInf;  //Fecha  Informe.-
        //--------------------------------------------------
    }
    
    public class T_MODXPLN0
    {
        public  T_xDatP VxDatP;
        public  T_xDecP[] VxDecP;
        
        public T_MODXPLN0() {
            VxDecP = new T_xDecP[0];
            VxDatP = new T_xDatP();
        }
    }
}
