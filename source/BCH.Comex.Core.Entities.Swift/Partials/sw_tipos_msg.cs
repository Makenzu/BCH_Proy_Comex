
namespace BCH.Comex.Core.Entities.Swift
{
    public partial class sw_tipos_msg
    {
        //retorna la parte numérica del CodTipo (es decir, sacando el "MT")
        public string NroCodTipo
        {
            get
            {
                return this.cod_tipo.Substring(2, 3);
                
            }
        }

        public string NroCodYNombre
        {
            get
            {
                return this.NroCodTipo + " - " + this.nombre_tipo.Trim();
            }
        }
        
    }
}
