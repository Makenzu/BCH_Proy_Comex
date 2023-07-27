
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //--------------------------------------------------------
    //Estructura para almacenar el Header de la Contabilidad.
    //--------------------------------------------------------
    public class T_IMch
    {
        public string codcct;  //Centro de Costo.
        public string codpro;  //Producto.
        public string codesp;  //Especialista.
        public string codofi;  //Empresa.
        public string codope;  //Operación.
        public int NroRpt;  //Correlativo (id_usr + hora).
        public string fecmov;  //Fecha del Movimiento.
        public string NomCli;  //Nombre Importador.
        public short codfun;  //Código de Función.
        public short estado;  //Estado de la Contabilidad.
        public string DesGen;  //Observaciones Generales.
        public string cencos;  //Usuario.-
        public string codusr;  //Usuario.-
        public string Nombre;  //Nombre Usuario .-
        public string EspOrig;  //Usuario Original.-
    }

    //--------------------------------------------------------
    //Estructura para almacenar el Detalle de la Contabilidad.
    //--------------------------------------------------------
    public class T_IMcd
    {
        public short CodMon;  //Código de la moneda.
        public string NemCta;  //Nemónico Cuenta Contable.
        public string NemMon;  //Nemónico Moneda.
        public string NumCta;  //Número de Cuenta Contable.
        public short IdnCta;  //Indicador del Tipo de Cuenta Contable.
        public string cod_dh;  //<D>ebe, <H>aber.
        public double mtomcd;  //Monto del Movimiento.
        public string PrtCli;  //LLave del Cliente.
        public string rutcli;  //Rut del Cliente.
        public string SwiBco;  //Swif del Banco.
        public string numcct;  //Número de la Cuenta.
        public short OfiDes;  //Número de la Oficina.
        public int NumPar;  //Número de Partida.
        public short TipMov;  //Tipo de Movimiento.
        public string NroRef;  //# Referencia, Vale Vista, Cheque, Reembolso.
        public double TipCam;  //Tipo de Cambio.-
        public string FecVen;  //Fecha Valor.-
    }
    //----------------------------------------------------------------------------
    //Resumen por Moneda del Reporte Contable.
    //----------------------------------------------------------------------------
    public class T_Total
    {
        public short CodMon;  //Código de la moneda.
        public string NemMon;  //Nemónico Moneda.
        public double MtoMov_d;  //Total al Debe.
        public double MtoMov_h;  //Total al Haber.
    }

    //****************************************************************************
    //Tabla de Descripción Función Contable.
    //****************************************************************************
    public class T_Dfc
    {
        public short CodDfc;  //Descripción Función Contable.
        public string DesDfc;

    }
    
}
