
namespace BCH.Comex.Core.Entities.Cext01.Devengo
{
    // Estructura de Atributos del Usuario.-
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
    }
    // *' Javier Cane
    public class Usr1
    {
        public string CodCct;
        public string CodEsp;
        public string Nombre;
        public int codigo;
        public string Tipeje;
    }
    public class T_MODGUSR
    {
        // Contiene Datos del Usuario que se conecta
        public sce_usr UsrEsp { get; set; }
        // Contiene Datos de los Especialistas del Usuario Lider
        public sce_usr[] UsrLidEsp { get; set; }
        //  D.S.B.
        public static sce_usr UsrLid { get; set; }
        // constantes de numero de operacion posible
        public const string CdgOp_CobImp = "CBI";     // cobranza importaciones
        public const string CdgOp_CobExp = "CBX";     // cobranza exportaciones
        public const string CdgOp_CreImp = "CCI";     // carta credito importaciones
        public const string CdgOp_CreExp = "CCX";     // carta credito exportaciones
        public const string CdgOp_CreLoc = "CCL";     // carta credito local
        public const string CdgOp_PaeExp = "PAE";     // Prestamos exportador
        public const string CdgOp_DesExp = "DEX";     // Código Descuentos de Documentos de Exportaciones.-
        public const string CdgOp_ComExp = "CEX";     // Código Compra de Documentos de Exportaciones.-
        public const string CdgOp_PagCce = "CAC";     // Código Compra Anticipada Carta de Credito
        // Documentos y sus Rangos
        public const string CdgOp_PlaVis = "PLV";     // Planillas visibles
        public const string CdgOp_PlaInv = "PLI";     // Planillas invisibles
        public const string CdgOp_PreIdi = "PID";     // Presentacion de idis
        public const string CdgOp_AprIdi = "IDI";     // Idis aprobados
        public const string CdgOp_CuaPag = "SCP";     // Cuadro de Pagos
        public const string CdgOp_ValVis = "VVS";     // Vale Vista
        public const string CdgOp_OpeAla = "OPA";     // Orden Pago Aladi
        public const string CdgOp_CVTImp = "CVI";     // Compra Venta Importaciones
        public const string CdgOp_CVTExp = "CVE";     // Compra Venta Exportaciones
        public const string CdgOp_CVTCam = "CVC";     // Cambios
        public const string CdgOp_FUTCon = "FCC";     // Código del Contrato para Futuro
        public const string CdgOp_FUTOpe = "FOP";     // Código de las Operaciones de Contrato para Futuro
        public const string CdgOp_CONTgl = "CGL";     // Código del GL
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
        // Constantes de mensajes
        private const string RegUsr_Titulo = "Control de Usuarios";
        private const string RegUsr_NoAis = "Se ha detectado una violación en la seguridad del sistema, los servicios AIS para Windows no estan operativos en la estación de trabajo";
        private const string RegUsr_IniFin = "¡Atención!, no es posible utilizar la aplicación mientras los procedimientos de Inicio o Fin de dia estan activos.";
        private const string RegUsr_NoIniDia = "¡Atención!, no es posible utilizar la aplicación mientras no se realice el procedimiento de inicio de dia de la sesión de comercio exterior de hoy.";
        private const string RegUsr_ErrAccess = "Error(@)=";
        // Usuario NO autorizado.-
        public const string MsgNOUsr = "Se intenta leer una operación sin tener los permisos adecuados. No podrá acceder a esta operación.";
        public const string MsgUsr = "Identificación de Usuario.";
        
        public static Usr1[] VUsr1 { get; set; }

        public static string Nuevo_CCt1 = "";

        public T_MODGUSR() 
        {
            UsrEsp = new sce_usr();
            UsrLid = new sce_usr();
        }
    }
}
