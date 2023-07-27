using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class Saldos_Vistas
    {
        public string status1;
        public string status2;
        public string miscelan1;
        public string miscelan2;
        public string moscelan3;
        public int tipocta;
        public int saldoact;

        public Saldos_Vistas()
        {
            status1= String.Empty;
            status2= String.Empty;
            miscelan1= String.Empty;
            miscelan2= String.Empty;
            moscelan3= String.Empty;
        }
    }
    public class T_CodTra
    {
        public string codtra;
        public string nemtra;
        public string destra;

        public T_CodTra()
        {
            codtra = String.Empty;
            nemtra = String.Empty;
            destra = String.Empty;
        }
    }
    public class T_ModSaldo
    {
        public Saldos_Vistas V_SalVis = new Saldos_Vistas();
        public string SerSalCCL = "";
        public string VisSalMN = "";
        public string VisSalME = "";
        public string NodoSalME = "";
        //  Constantes de Nemónicos de Códigos de Cuenta Corriente en Línea
        public const string NEM_ABONOMN = "ABONOMN";
        public const string NEM_CARGOMN = "CARGOMN";
        public const string NEM_ABONOME = "ABONOME";
        public const string NEM_CARGOME = "CARGOME";
        //  Estructura de Códigos de Transacción Cta Cte Línea
        public T_CodTra[] VCodTra = new T_CodTra[0];
    }
}
