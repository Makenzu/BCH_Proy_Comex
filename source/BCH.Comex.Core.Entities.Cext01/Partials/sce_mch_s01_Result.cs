
namespace BCH.Comex.Core.Entities.Cext01
{
    public partial class sce_mch_s01_MS_Result
    {
        public string NombreEspecialista { get; set; }
        public string DescripcionFuncionContable { get; set; }

        public string NroOperacion
        {
            get
            {
                return this.codcct + "-" + this.codpro + "-" + this.codesp + "-" + this.codofi + "-" + this.codope;
            }
        }
    }
}
