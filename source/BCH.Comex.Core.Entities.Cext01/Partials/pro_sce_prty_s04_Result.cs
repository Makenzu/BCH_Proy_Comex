
namespace BCH.Comex.Core.Entities.Cext01
{
    public partial class pro_sce_prty_s04_MS_Result
    {
        public string color { get; set; }
        public int row { get; set; }
        public string razonSocial { get; set; }
        public string TIPO_TRX { get; set; }
        public string Beneficiario { get; set; }
        public string Ordenante { get; set; }
        public string MONTO_ORIGINAL { get; set; }
        public string str_swft { get; set; }
        public string str_cod_estado { get; set; }
        public int Seleccionado { get; set; }

        /// <summary>
        /// Se agrega esta columna para que se pueda buscar por el monto sin tener que poner el separador de miles, ademas se usa la , como separador de decimales, q es lo normal para el usuario
        /// </summary>
        public string MontoParaBusqueda
        {
            get
            {
                if (!string.IsNullOrEmpty(this.DRAMT))
                {
                    return this.DRAMT.Replace(".", ",");
                }
                else return null;
            }
        }
    }
}
