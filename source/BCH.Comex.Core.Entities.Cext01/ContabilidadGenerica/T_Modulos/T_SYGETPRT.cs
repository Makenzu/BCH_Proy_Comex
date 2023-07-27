using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class CdOper
    {
        public string Cent_costo;
        public string Id_Product;
        public string Id_Especia;
        public string Id_Empresa;
        public string Id_Operacion;
        public string Refnueva;

        public CdOper()
        {
            Cent_costo = String.Empty;
            Id_Product = String.Empty;
            Id_Especia = String.Empty;
            Id_Empresa = String.Empty;
            Id_Operacion = String.Empty;
            Refnueva = String.Empty;
        }
    }
    public class PartyKey
    {
        public int Borrado;   //Orieta
        public int TieneRut;   //Orieta
        public int Status;
        public string LlaveArchivo;
        public string NombreUsado;
        public int IndNombre;
        public string DireccionUsado;
        public int IndDireccion;
        public string ComunaUsado;
        public string EstadoUsado;
        public string CiudadUsado;
        public string PostalUsado;
        public string PaisUsado;
        public int codpais;
        public int TipoParty;
        public int FlagParty;
        public int Ubicacion;
        public string Rut;
        public string Swift;
        public string CodBanco;
        public string Telefono;
        public string Fax;
        public string Telex;
        public int Enviara;
        public string CasPostal;
        public string CasBanco;

        public PartyKey()
        {
            LlaveArchivo = String.Empty;
            NombreUsado = String.Empty;
            DireccionUsado = String.Empty;
            DireccionUsado = String.Empty;
            ComunaUsado = String.Empty;
            EstadoUsado = String.Empty;
            CiudadUsado = String.Empty;
            PostalUsado = String.Empty;
            PaisUsado = String.Empty;
            Rut = String.Empty;
            Swift = String.Empty;
            CodBanco = String.Empty;
            Telefono = String.Empty;
            Fax = String.Empty;
            Telex = String.Empty;
            CasPostal = String.Empty;
            CasBanco = String.Empty;
        }

        public PartyKey Copy()
        {
            return (PartyKey)this.MemberwiseClone();
        }
    }

    public class PartyParametros
    {
        public string PartyPath;
        public int Red;
        public string Nodo;
        public string Servidor;
        public int Leidos;
        public int LimInf;
        public int LimSup;
        public int Otros;
        public int Insertado;
        public int PorInsertar;
        public int Retorno;
        public int Indice;
        public int NoOperacion;
        public CdOper NumOpe;
        public int EsHost;
        public int Modifico;
        public string Cambios;

        public PartyParametros()
        {
            PartyPath = String.Empty;
            Nodo = String.Empty;
            Servidor = String.Empty;
            NumOpe = new CdOper();
            Cambios = String.Empty;
        }
    }

    public class PartysPope
    {
        public int Status;
        public int Secuencia;
        public int EsBanco;
        public string RutSwift;
        public string Nombre;
        public string Direccion;
        public string comuna;
        public string Ciudad;
        public string estado;
        public string Pais;
        public int codpais;
        public string Postal;
        public string Telefono;
        public string Fax;
        public string Telex;
        public int Enviara;
        public string CasPostal;
        public string CasBanco;

        public PartysPope()
        {
            RutSwift = String.Empty;
            Nombre = String.Empty;
            Direccion = String.Empty;
            comuna = String.Empty;
            Ciudad = String.Empty;
            estado = String.Empty;
            Pais = String.Empty;
            Postal = String.Empty;
            Telefono = String.Empty;
            Fax = String.Empty;
            Telex = String.Empty;
            CasPostal = String.Empty;
            CasBanco = String.Empty;
        }

        public PartysPope Copy()
        {
            return (PartysPope)this.Copy();
        }
    }
    public class T_SYGETPRT
    {
        // Declaracion de Constantes party
        public const string GPrt_GetParty = "Captura de Participantes";
        public const string GPrt_NoPath = "Error del path a Bases de Datos";
        public CdOper Codop = new CdOper();
        public PartyKey[] PartysOpe = new PartyKey[0];
        public  PartyParametros PrtControl = new PartyParametros();
        public  PartyKey[] Partys = new PartyKey[0];
        public  PartysPope[] PopeOpe = new PartysPope[0];
        public  PartysPope[] Pope = new PartysPope[0];
        public  string[] PrtTbl = new string[0];
        public  string KeyPrt = "";


        // Declaracion de Valores de Retorno de GetParty
        public const int GPrt_RetExiste = 0;
        public const int GPrt_RetCancelo = 1;
        public const int GPrt_RetNoExiste = 2;
        public const int GPrt_RetErrorPath = 3;
        public const int GPrt_RetErrorLeer = 4;
        public const int GPrt_RetParaBorrar = 5;
        // Declaracion de ubicaciones
        public const int GPrt_EnParty = 0;
        public const int GPrt_EnOperacion = 1;
        // declaraciones de status normales
        public const int GPrt_StatVacio = 0;
        public const int GPrt_StatLleno = 1;
        public const int GPrt_StatDatos = 2;
        public const int GPrt_StatDatosLleno = 3;
        // declaracion de status inamovibles
        public const int GPrt_StatConIdi = 4;
        public const int GPrt_StatConDec = 8;
        public const int GPrt_StatConPago = 16;
        // declaracion status Pope
        public const int GPrt_StatNuevo = -1;
        public const int GPrt_StatCambio = -2;
        public const int GPrt_StatBorro = -3;
        public const int GPrt_StatIntacto = -4;
        // declaración constantes de envio de correspondencia
        public const int GPrt_ADireccion = 0;
        public const int GPrt_AFax = 1;
        public const int GPrt_ACasBanco = 2;
        public const int GPrt_ACasPostal = 3;
        public const int KEY_PRIOR = 0x21;
        public const int KEY_NEXT = 0x22;
        public const int KEY_END = 0x23;
        public const int KEY_HOME = 0x24;
        public const int KEY_UP = 0x26;
        public const int KEY_DOWN = 0x28;
        // tipos de partys
        public const int GPrt_TipoIndividuo = 0;
        public const int GPrt_TipoBanco = 1;
        public const int GPrt_TipoCliente = 2;
        public const int GPrt_TipoEnOperacion = 3;
        public const int GPrt_TipoBcoOperacion = 4;
        // tipos de Flag
        public const int GPrt_FlagInst = 1;
        public const int GPrt_FlagTasas = 2;
        public const int GPrt_FlagCtas = 4;
        public const int GPrt_FlagBcoCorresp = 8;
        public const int GPrt_FlagBcoAcreedor = 16;
        public const int GPrt_FlagSwift = 32;
        public const int GPrt_FlagAladi = 64;
        // flag de instrucciones, solo se usan en GetPrty0 ==> alla se definen
        // estan aqui como documentacion
        // Const Gen_Imp = 1
        // Const Gen_Exp = 2
        // Const Gen_Ser = 4
        // Const Cob_Imp = 8
        // Const Cob_Exp = 16
        // Const Cre_Imp = 32
        // Const Cre_Exp = 64
        // Forma del party en Modulo de administracion de partys
        public const int GPrt_FormaNada = 0;
        public const int GPrt_FormaBancoAlfa = 1;
        public const int GPrt_FormaBancoSwift = 2;
        public const int GPrt_FormaIndAlfa = 3;
        public const int GPrt_FormaIndRut = 4;
        public const int GPrt_FormaCliente = 5;
        // mascara rut y fono
        // Const GPrt_RutMascara = "___.___.___-_"
        // Const GPrt_FonoMascara = "(___) ___-____"
        // Const GPrt_RutMask = "###\.###\.###\-A"
        // Const GPrt_FonoMask = "(###) ###-####"
        // marcas de lista para operaciones especiales
        public const string GPrt_MarcaRequerido = "&";
        public const string GPrt_MarcaOtros = "#";
        public const string GPrt_MarcaBanco = "@";
        // Declaraciones Varias
        private const string GPrt_YaBo = "El participante numero ";
        private const string GPrt_Rado = " o alguna de sus propiedades estan marcadas para eliminación. Por estar en uso no podra ser eliminado.";
        public const string GPrt_Borrado = "El participante esta marcado para eliminación.  Si desea utilizarlo debe habilitarlo en el administrador de participantes.";
        public const string GPrt_NoExiste = "El participante solicitado no existe en las bases de datos.  Para crearlo debe utilizar el administrador de participantes.";
        public const string GPrt_TxtOtros = "(Otros)";
        public const string GPrt_InputOtro = "La descripción no debe ser mayor que 20 caracteres";
        public const string GPrt_InputCambio = "La descripción para el participante es requerida";
        public const string GPrt_Caption = "Identificar Participantes";
        public const string GPrt_ErrGetDbf = "Se ha producido un error en el acceso a las bases de datos de Participantes.";
        public const string GPrt_ErrRecLocked = "El Registro solicitado a la base de datos se encuentra bloqueado.";
        public const string GPrt_ErrRecFatal = "Error en el procesamiento de la Base de Datos";
        public const string GPrt_ErrRecNoExiste = "Registro solicitado no existe en Bases de Datos";
        public const string GPrt_ErrRequerido = ", esta información es requerida para la operación.";
        public const string GPrt_ErrRut = "Rut Invalido";
        public const string GPrt_NoEsBanco = "La operación requiere que este participante sea un banco, sin embargo se ha seleccionado un participante que no es del tipo requerido.";
        public const string GPrt_NoPuedeBanco = "La operación requiere que este participante sea un banco, por lo tanto este participante no puede estar asociado a la operación.";
        public const string GPrt_ErrEliminar = "El participante posee datos asociados en la operacion, para eliminarlo deberá primero liberar dicha información.";
        public const string GPrt_ErrModificar = "El participante tiene asociada información que impide su modificación, para modificarlo deberá primero liberar dicha información";
        public const string GPrt_ErrDbfEliminar = "Error al eliminar el participante en las bases de datos";
        public const string GPrt_Atencion = "¡Atención!";
        public const string GPrt_AtencionCola = "Los participantes son información requerida para la operación, sin ellos no es posible ejecutar la aplicación";
        public const string GPrt_BajarParty = "¡¡Atención!!, el participante [@] existe en la bases de datos del servidor de datos de Comercio Exterior.  ¿Desea recuperar este participante desde el servidor?.";

        public bool HayEnOperacion { get; set; }
        public int CtaPartys { get; set; }
        public string LasMarcas { get; set; }
        public string FileKey { get; set; }
    }
}
