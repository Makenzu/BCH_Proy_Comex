
namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    // Variables Generales Comercio Exterior.-
    public class T_Gen
    {
        public int MndNac;   //Moneda Nacional.-
        public int MndDol;   //Moneda Dolar.-
        public int CodPbc;   //Plaza Baco. Central.-
        public int CodBCH;   //Entidad Autoriza.-
        public int SucBCH;   //Sucursal Banco Chile.-
        public int CodBCCh;   //Código Banco Central de Chile.-
        public double MtoDeb;   //Monto del Débito en Cta. Cte.-
        public double MtoIva;   //Monto del IVA.-
    }
    public class T_MODGSCE
    {
        public T_Gen VGen = new T_Gen();
    }
}
