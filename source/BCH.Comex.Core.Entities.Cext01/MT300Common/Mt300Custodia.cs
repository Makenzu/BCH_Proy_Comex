using System;

namespace BCH.Comex.Core.Entities.Cext01.MT300Common
{
    public class Mt300Custodia
    {
        public long id_registro { get; set; }
        public DateTime fecha_carga { get; set; }
        public short estado { get; set; }
        public long id_custodia { get; set; }
        public string base_sk { get; set; }
        public string custodio_global { get; set; }
        public string aba { get; set; }
        public string bankname { get; set; }
        public string benfacct { get; set; }
        public string benfname { get; set; }
        public string ffc { get; set; }
        public string adinfo { get; set; }
        public int activo { get; set; }
        public string usuario { get; set; }
        public DateTime fecha { get; set; }
        public string ind_mt300 { get; set; }
        public string tipo_mt300 { get; set; }
        public string bic_destino_mt300 { get; set; }
        public string bic_origen_mt300 { get; set; }
    }

}
