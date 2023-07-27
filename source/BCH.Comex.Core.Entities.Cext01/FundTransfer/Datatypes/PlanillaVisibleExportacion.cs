using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes
{
    public class PlanillaVisibleExportacion
    {
        public string tint;

        public List<Detalle> Detalles { get; set; }
        public string NroAcs { get; set; }
        public string Pl_Cif_Dolar { get; set; }
        public string Pl_Cif_Origen { get; set; }
        public string Pl_CodBCCh { get; set; }
        public string Pl_Codigo { get; set; }
        public string Pl_Cod_FormaPago { get; set; }
        public string Pl_cod_moneda { get; set; }
        public string Pl_Cod_Paispago { get; set; }
        public string Pl_Cod_Plaza { get; set; }
        public string Pl_fecha_anulacion { get; set; }
        public string Pl_fecha_conocimiento { get; set; }
        public string Pl_fecha_debito { get; set; }
        public string Pl_fecha_idi { get; set; }
        public string Pl_fecha_vencimiento { get; set; }
        public string Pl_fecha_venta { get; set; }
        public string Pl_Flete_Origen { get; set; }
        public string Pl_Fob_Origen { get; set; }
        public string Pl_Gastos_Banco { get; set; }
        public string Pl_HastaFob { get; set; }
        public string Pl_Interes_Origen { get; set; }
        public string Pl_LineaObs1 { get; set; }
        public string Pl_LineaObs2 { get; set; }
        public string Pl_LineaObs3 { get; set; }
        public string Pl_Mercaderia { get; set; }
        public string Pl_NDoc1 { get; set; }
        public string Pl_NDoc2 { get; set; }
        public string Pl_nombre_moneda { get; set; }
        public string Pl_NomImport { get; set; }
        public string Pl_num_acuerdos { get; set; }
        public string Pl_num_conocimiento { get; set; }
        public string Pl_Num_Cuadro { get; set; }
        public string Pl_num_cuotas { get; set; }
        public string Pl_num_idi { get; set; }
        public string Pl_num_planilla { get; set; }
        public string Pl_ObsCobranza { get; set; }
        public string Pl_ObsDecl { get; set; }
        public string Pl_ObsMerma { get; set; }
        public string Pl_ObsParidad { get; set; }
        public string Pl_Paispago { get; set; }
        public string Pl_Paridad { get; set; }
        public string Pl_paridad_anulacion { get; set; }
        public string Pl_rut { get; set; }
        public string Pl_Seguro_Origen { get; set; }
        public string Pl_tipo_cambio { get; set; }
        public string Pl_total_anulacion { get; set; }
        public string Pl_Total_Dolar { get; set; }
        public string Pl_Total_Origen { get; set; }
    }
}
