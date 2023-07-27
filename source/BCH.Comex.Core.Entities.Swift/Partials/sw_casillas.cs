
namespace BCH.Comex.Core.Entities.Swift
{
    public partial class sw_casillas
    {
        public string DataTextField
        {
            get
            {
                return cod_casilla + " " + nombre_casilla;
            }
        }
    }
}
