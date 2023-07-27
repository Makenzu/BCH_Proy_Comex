
namespace BCH.Comex.Core.Entities.Swift
{
    public class proc_sw_rec_estadist_msgDTO
    {
        //-- casilla, nombre_casilla, tipo_msg, nombre_tipo, cod_banco_rec, nombre_banco, cantidad 
        public int casilla { get; set; }
        public string nombre_casilla { get; set; }
        public string tipo_msg { get; set; }
        public string nombre_tipo { get; set; }
        public string cod_banco_rec { get; set; }
        public string nombre_banco { get; set; }
        public int cantidad { get; set; }
    }
}
