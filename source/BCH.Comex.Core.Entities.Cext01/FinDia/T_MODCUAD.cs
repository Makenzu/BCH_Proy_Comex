
namespace BCH.Comex.Core.Entities.Cext01.FinDia
{
   public class T_MODCUAD
    {
       public No_Ele[] Elementos = null;
        public decimal ncorr {get; set;}
        public string glosa_estado {get; set;}
        public string mensaje_error { get; set; }
        public string tipo_mt { get; set; }
        public decimal tipo_mt_decimal { get; set; }
        public string referencia { get; set; }
        public string codcct { get; set; }
        public string codpro { get; set; }
        public string codesp { get; set; }
        public string codofi { get; set; }
        public string codope { get; set; }
    }
   public struct No_Ele
   {
       public decimal ncorr;
       public string glosa_estado;
       public string mensaje_error;
       public string tipo_mt;
       public decimal tipo_mt_decimal;
       public string referencia;
       public string codcct;
       public string codpro;
       public string codesp;
       public string codofi;
       public string codope;
   }
}
