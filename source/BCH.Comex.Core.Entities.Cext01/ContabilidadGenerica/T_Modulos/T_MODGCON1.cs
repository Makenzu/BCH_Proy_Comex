using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_IMch
    {
        public string CodCct = String.Empty;   //Centro de Costo.
        public string CodPro = String.Empty;   //Producto.
        public string CodEsp = String.Empty;   //Especialista.
        public string CodOfi = String.Empty;   //Empresa.
        public string CodOpe = String.Empty;   //Operación.
        public int NroRpt;   //Correlativo (id_usr + hora).
        public string FecMov = String.Empty;   //Fecha del Movimiento.
        public string NomCli = String.Empty;   //Nombre Importador.
        public int codfun;   //Código de Función.
        public int estado;   //Estado de la Contabilidad.
        public string DesGen = String.Empty;   //Observaciones Generales.
        public string cencos = String.Empty;   //Usuario.-
        public string codusr = String.Empty;   //Usuario.-
        public string Nombre = String.Empty;   //Nombre Usuario .-
        public string EspOrig = String.Empty;   //Usuario Original.-
    }
    public class T_IMcd
    {
        public int CodMon;   //Código de la moneda.
        public string NemCta = String.Empty;   //Nemónico Cuenta Contable.
        public string NemMon = String.Empty;   //Nemónico Moneda.
        public string numcta = String.Empty;   //Número de Cuenta Contable.
        public int IdnCta;   //Indicador del Tipo de Cuenta Contable.
        public string cod_dh = String.Empty;   //<D>ebe, <H>aber.
        public double mtomcd;   //Monto del Movimiento.
        public string PrtCli = String.Empty;   //LLave del Cliente.
        public string rutcli = String.Empty;   //Rut del Cliente.
        public string SwiBco = String.Empty;   //Swif del Banco.
        public string numcct = String.Empty;   //Número de la Cuenta.
        public int OfiDes;   //Número de la Oficina.
        public int NumPar;   //Número de Partida.
        public int TipMov;   //Tipo de Movimiento.
        public string NroRef = String.Empty;   //# Referencia, Vale Vista, Cheque, Reembolso.
        public double TipCam;   //Tipo de Cambio.-
        public string FecVen = String.Empty;   //Fecha Valor.-
    }
    public class T_Total
    {
        public int CodMon;   //Código de la moneda.
        public string NemMon = String.Empty;   //Nemónico Moneda.
        public double MtoMov_d;   //Total al Debe.
        public double MtoMov_h;   //Total al Haber.
    }
    public class T_Dfc
    {
        public int CodDfc;   //Descripción Función Contable.
        public string DesDfc;//MAX 60
    }
    public class T_MODGCON1
    {
        public T_IMch V_IMch = new T_IMch();     // Header  Contabilidad.
        public T_IMch V_IMchNul = new T_IMch();     // Header  Contabilidad.
        public T_IMcd[] V_IMcd = new T_IMcd[0];     // Arreglo Detalle Contabilidad.
        public T_Total[] V_Total = new T_Total[0];
        public T_Dfc[] VDfc = new T_Dfc[0];
        public int ConLin = 0;
        public int con1pagina = 0;
    }
}
