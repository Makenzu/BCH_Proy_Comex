
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class Saldos_Vistas
    {
        public string status1;
        public string status2;
        public string miscelan1;
        public string miscelan2;
        public string moscelan3;
        public short tipocta;
        public int saldoact;
    }

    // Estructura de Códigos de Transacción Cta Cte Línea

    public class T_CodTra
    {
        public string codtra;
        public string nemtra;
        public string destra;
    }
    public class T_ModSaldo
    {
        public  Saldos_Vistas V_SalVis;
        public  string SerSalCCL = "";
        public  string VisSalMN = "";
        public  string VisSalME = "";
        public  string NodoSalME = "";
        public  T_CodTra[] VCodTra;

        public T_ModSaldo()
        {
            V_SalVis = new Saldos_Vistas();
            VCodTra = new T_CodTra[0];
        }
    }
}
