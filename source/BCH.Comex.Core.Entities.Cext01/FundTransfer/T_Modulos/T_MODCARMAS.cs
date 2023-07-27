
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //--------------------------------------------------
    //Nuevo Modulo de  Carga Masiva - Realsystems
    //Fecha: 21-09-2012
    //Declaraciones y funciones varias

    public class SPOT
    {
        public double Id_Operacion;
        public string rut_cliente;
        public string id_party;
        public string rut_beneficiario;
        public string id_prty_ben;
        //    rut_otro          As String
        //    id_prty_otro      As String
        public string tipo_cuenta;
        public string cod_planilla;
        public double pais_planilla;
        public string mnd_planilla;
        public double monto_planilla;
        //   tip_cambio_pla    As Double
        public string cta_cte_origen;
        public string mnd_origen;
        //    monto_origen      As Double
        public string beneficia_origen;
        public string det1_origen;
        public string det2_origen;
        public string det3_origen;
        public string beneficia_destino;
        public string cta_cte_destino;
        public string mnd_destino;
        //    monto_destino     As Double
        public string por_orden_de;
        public string det1_destino;
        public string det2_destino;
        public string det3_destino;
        public short estado;
    }

    public class OrdenPago
    {
        public double Id_Operacion;
        public string rut_cliente;
        public string id_party;
        //rut_beneficiario  As String
        //id_prty_ben       As String
        //rut_otro          As String
        //id_prty_otro      As String
        public string tipo_cuenta;
        public string cod_planilla;
        public double pais_planilla;
        public string mnd_planilla;
        public double monto_planilla;
        public double tip_cambio_pla;
        public string cta_cte_origen;
        //    mnd_origen        As String
        //    monto_origen      As Double
        public string Beneficiario;
        public string det1_origen;
        public string det2_origen;
        public string det3_origen;
        public string gastos;
        public string corresponsal;
        public string fecha_valuta;
        public string nombre_benefi;
        public string direccion_benefi;
        public double pais_benefi;
        public string cuenta_benefi;
        public string referencia_benefi;
        public string bco_inter_57;
        public string bco_pais_57a;
        public short estado;
    }
    public class T_MODCARMAS
    {
        public  SPOT[] Ope_SPOT;
        public  OrdenPago[] Ope_OP;
        public  bool CARGA_MASIVA;
        // UPGRADE_INFO (#0561); The 'RUTA_CARMAS' symbol was defined without an explicit "As" clause.
        public const string RUTA_CARMAS = @"C;\DATA\SCE\CARMAS.txt";
        // UPGRADE_INFO (#0561); The 'SPOT_OP' symbol was defined without an explicit "As" clause.
        public dynamic SPOT_OP;
        // UPGRADE_INFO (#0561); The 'Index' symbol was defined without an explicit "As" clause.
        public dynamic Index;
        // UPGRADE_INFO (#0561); The 'TIPOCVDCM' symbol was defined without an explicit "As" clause.
        public  dynamic TIPOCVDCM;
        public  string SPOT_OP_TMP;

        public T_MODCARMAS()
        {
            Ope_SPOT = new SPOT[0];
            Ope_OP = new OrdenPago[0];
        }
    }
}
