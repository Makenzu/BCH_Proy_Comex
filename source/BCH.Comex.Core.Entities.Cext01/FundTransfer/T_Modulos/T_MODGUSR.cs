using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class T_MODGUSR
    {

        //Constantes para Id_Product de las Operaciones
        public const string IdPro_CobImp = "03";
        //Declaración Exportaciones
        public const string IdPro_PreExp = "05";
        //Préstamos a Exportadores
        public const string IdPro_CobExp = "06";
        //Cobranza Exportaciones
        public const string IdPro_CreImp = "07";
        //Carta Crédito Importaciones
        public const string IdPro_CreCon = "08";
        //Carta Crédito Contado
        public const string IdPro_CreExp = "09";
        //Carta Crédito Exportaciones
        public const string IdPro_ConGen = "10";
        //Carta Crédito Local
        public const string IdPro_DesExp = "12";
        //Descuentos Documentos Exportaciones.-
        public const string IdPro_ComExp = "13";
        //Compra Documentos Exportaciones.-
        public const string IdPro_AvanEx = "14";
        //Avances Otorgados al Exterior de Exportaciones
        public const string IdPro_InfImp = "15";
        //Informes de Importación.-
        public const string IdPro_DecImp = "16";
        //Declaraciones de Importación.-
        public const string IdPro_RetExp = "17";
        //Retornos Exportación.-
        public const string IdPro_PagCCE = "18";
        //Código Compra Anticipada Carta de Credito
        public const string IdPro_ComVen = "30";
        //Fund Transfer
        public const string IdPro_CVD = "20";
        //CVD

        // SE MERGEA FT Y CVD. SE USA ESTE PRODUCTO CUANDO AUN NO SE TIENE CLARO CUAL SE USARÁ
        public const string IdPro_Undefined = "XX";


        //Contiene Datos del Usuario que se conecta
        public EstructUsuarios UsrEsp ;
        //Contiene Datos de los Especialistas del Usuario Lider
        public EstructUsuarios[] UsrLidEsp;

        public T_MODGUSR() {
            UsrEsp = new EstructUsuarios();
            UsrLidEsp = new EstructUsuarios[0];
        }
    }
}
