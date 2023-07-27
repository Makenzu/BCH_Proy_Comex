
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //Estructura de Arbitrajes.
    public class T_Arb
    {
        public string codope;  //Código de la operación
        public short codpai;  //País de Origen.
        public short MndCom;  //Moneda Compra.
        public string NemMndC;  //Nemónico Moneda Compra.
        public short MndVta;  //Moneda Venta.
        public string NemMndV;  //Nemónico Moneda Venta.
        public double MtoCom;  //Monto de Compra.
        public double MtoVta;  //Monto de Venta.
        public double PrdCom;  //Paridad Compra.
        public double PrdVta;  //Paridad Venta.
        public double PrdArb;  //Paridad Arbitraje.
        public double TipCam;  //Tipo de Cambio.
        public double MtoDol;  //Monto en Dolares.
        public double MtoPes;  //Monto en Pesos.
        public short Conven;  //Convenio?.
        public short Status;  //Status Registro.
        // se agregan otrso registros
        public double DolCom;  //monto en dolares de la compra
        public double DolVta;  //monto en dolares de la venta
        public double CamCom;  //Tipo cambio de la compra
        public double CamVta;  //Tipo Cambio de la venta
    }

    public class T_MODGARB
    {
        public T_Arb[] VArb;

        public T_MODGARB()
        {
            VArb = new T_Arb[0];
        }
    }
}
