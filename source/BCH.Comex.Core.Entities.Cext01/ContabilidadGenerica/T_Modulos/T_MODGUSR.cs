using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class EstrucUsuarios
    {
        public string Rut;   //rut_usuario
        public int Jerarquia;   //nivel de jerarquia
        public string CentroCosto;   //cent_costo_esp
        public string Especialista;   //id_especialista
        public string CCtOrig;   //Centro Costo Original.-
        public string EspOrig;   //Especialista Original.-
        public int Delegada;   //es del mismo nivel que super
        public string CostoSuper;   //jefe superior
        public string Id_Super;   //jefe superior
        public string Nombre;   //nombre_esp
        public string Direccion;   //direcc_esp
        public string comuna;   //comuna especialista
        public string Ciudad;   //ciudad_esp
        public string Seccion;   //seccion_esp
        public int Oficina;   //codigo de su oficina
        public string Telefono;   //Telefono_esp
        public string Swift;   //swift_esp
        public string Fax;   //fax_esp
        public string Tipeje;   //Tipo Ejecutivo Operativo Negocios XOtro
        public string reemplazos;   //Reemplazos.-
        public string RempOrig;   //Reemplazos Usuario Original.-
        public string OfixUser;   //Oficinas que puede atender el usuario

        public EstrucUsuarios()
        {
            Rut = String.Empty;   //rut_usuario

            CentroCosto = String.Empty;   //cent_costo_esp
            Especialista = String.Empty;   //id_especialista
            CCtOrig = String.Empty;   //Centro Costo Original.-
            EspOrig = String.Empty;   //Especialista Original.-

            CostoSuper = String.Empty;   //jefe superior
            Id_Super = String.Empty;   //jefe superior
            Nombre = String.Empty;   //nombre_esp
            Direccion = String.Empty;   //direcc_esp
            comuna = String.Empty;   //comuna especialista
            Ciudad = String.Empty;   //ciudad_esp
            Seccion = String.Empty;   //seccion_esp

            Telefono = String.Empty;   //Telefono_esp
            Swift = String.Empty;   //swift_esp
            Fax = String.Empty;   //fax_esp
            Tipeje = String.Empty;   //Tipo Ejecutivo Operativo Negocios XOtro
            reemplazos = String.Empty;   //Reemplazos.-
            RempOrig = String.Empty;   //Reemplazos Usuario Original.-
            OfixUser = String.Empty;   //Oficinas que puede atender el usuario
        }
    }
    public class T_MODGUSR
    {
        // Constantes para Id_Product de las Operaciones
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
        public const string IdPro_ComVen = "20";     // Compra Venta Moneda Extranjera

        //Contiene Datos del Usuario que se conecta
        public EstrucUsuarios UsrEsp;
        //Contiene Datos de los Especialistas del Usuario Lider
        public EstrucUsuarios[] UsrLidEsp;

        public T_MODGUSR()
        {
            UsrEsp = new EstrucUsuarios();
            UsrLidEsp = new EstrucUsuarios[0];
        }

    }
}
