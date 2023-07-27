
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain
{
    public class T_Gen
    {
        public short MndNac{ set; get; }  //Moneda Nacional.-
        public short MndDol{ set; get; }  //Moneda Dolar.-
        public short CodPbc{ set; get; }  //Plaza Baco. Central.-
        public short CodBch{ set; get; }  //Entidad Autoriza.-
        public short SucBCH{ set; get; }  //Sucursal Banco Chile.-
        public short CodBCCh{ set; get; }  //Código Banco Central de Chile.-
        public double MtoDeb{ set; get; }  //Monto del Débito en Cta. Cte.-
        public double MtoIva{ set; get; }  //Monto del IVA.-
    }
}
