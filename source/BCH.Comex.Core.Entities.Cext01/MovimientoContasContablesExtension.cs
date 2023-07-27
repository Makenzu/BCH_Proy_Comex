
namespace BCH.Comex.Core.Entities.Cext01
{
    public interface IMovimientoContasContables
    {
        string referencia { get; set; }
        string nemmon { get; set; }
        decimal mtomcd { get; set; }
        string cod_dh { get; set; }
        decimal nrorpt { get; set; }
        string numcta { get; set; }
        string numcct { get; set; }
    }

    public partial class sce_mcd_s66_MS_Result : IMovimientoContasContables { }
    public partial class sce_mcd_s65_MS_Result : IMovimientoContasContables { }

}
