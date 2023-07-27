
namespace BCH.Comex.Core.Entities.Swift
{
    public class proc_sw_rec_controlDTO
    {
        private int _num;
        public int sesion { get; set; }
        public int secuencia { get; set; }
        public int casilla { get; set; }
        public string estado_msg { get; set; }
        public string fecha1 { get; set; }
        public string fecha2 { get; set; }
        public string cod_banco_rec { get; set; }
        public string branch_rec { get; set; }
        public int num { get; set; }
    }
}
