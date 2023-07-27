
namespace BCH.Comex.Core.Entities.Swift
{
    public partial class sw_monedas
    {
        public string CodYNombre
        {
            get
            {
                return this.cod_moneda_sw + " - " + this.nombre_moneda;
            }
        }
    }
}
