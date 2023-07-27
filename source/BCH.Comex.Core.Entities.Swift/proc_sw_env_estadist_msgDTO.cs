
namespace BCH.Comex.Core.Entities.Swift
{
    public class proc_sw_env_estadist_msgDTO
    {

        public int casilla { get; set; }
        public string nombre_casilla { get; set; }
        public string tipo_msg { get; set; }
        public string nombre_tipo { get; set; }
        public string cod_banco_em { get; set; }
        public string branch_em { get; set; }
        public string nombre_banco { get; set; }
        public int cantidad { get; set; }
    }
}
