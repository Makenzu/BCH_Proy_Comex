
namespace BCH.Comex.Core.Entities.Cext01.Common
{
    public class T_MODGUSR
    {
        public const string IdPro_CobImp = "03";     // Cobranza Importaciones
        public const string IdPro_DecExp = "04";     // Declaración Exportaciones
        public const string IdPro_PreExp = "05";     // Préstamos a Exportadores
        public const string IdPro_CobExp = "06";     // Cobranza Exportaciones
        public const string IdPro_CreImp = "07";     // Carta Crédito Importaciones
        public const string IdPro_CreCon = "08";     // Carta Crédito Contado
        public const string IdPro_CreExp = "09";     // Carta Crédito Exportaciones
        public const string IdPro_ConGen = "10";     // Contabilidad Genérica (G/L)
        public const string IdPro_CreLoc = "11";     // Carta Crédito Local
        public const string IdPro_DesExp = "12";     // Descuentos Documentos Exportaciones.-
        public const string IdPro_ComExp = "13";     // Compra Documentos Exportaciones.-
        public const string IdPro_AvanEx = "14";     // Avances Otorgados al Exterior de Exportaciones
        public const string IdPro_InfImp = "15";     // Informes de Importación.-
        public const string IdPro_DecImp = "16";     // Declaraciones de Importación.-
        public const string IdPro_RetExp = "17";     // Retornos Exportación.-
        public const string IdPro_PagCCE = "18";     // Código Compra Anticipada Carta de Credito
        public const string IdPro_ComVen = "30";     // Compra Venta Moneda Extranjera
        public const string IdPro_CVD = "20";
        public const string IdPro_PreImp = "23";     // Prestamos a Impotadores
        // SE MERGEA FT Y CVD. SE USA ESTE PRODUCTO CUANDO AUN NO SE TIENE CLARO CUAL SE USARÁ
        public const string IdPro_Undefined = "XX";
        //Se usa cuando no se sabe que numero se debe usar
        public const string CodOp_Undefined = "XXXXX";
        //Contiene Datos del Usuario que se conecta
        public EstructUsuario UsrEsp;
        //Contiene Datos de los Especialistas del Usuario Lider
        public EstructUsuario[] UsrLidEsp;

        public T_MODGUSR()
        {
            UsrEsp = new EstructUsuario();
            UsrLidEsp = new EstructUsuario[0];
        }
    }
}
