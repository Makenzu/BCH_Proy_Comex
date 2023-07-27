
namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    //Estructura para mantener número de la Operación

    public class CdOper
    {
        public string Cent_Costo;
        public string Id_Product;
        public string Id_Especia;
        public string Id_Empresa;
        public string Id_Operacion;
        public string Refnueva;

        public CdOper Clone()
        {
            return (CdOper)this.MemberwiseClone();
        }
    }

    //----
    //Estructuras y Constantes para Partys
    //----

    //Estructura de Control
    public class PartyParametros
    {
        public string PartyPath;
        public short Red;
        public string Nodo;
        public string Servidor;
        public short Leidos;
        public short LimInf;
        public short LimSup;
        public short Otros;
        public short Insertado;
        public short PorInsertar;
        public short Retorno;
        public short Indice;
        public short NoOperacion;
        public CdOper NumOpe;
        public short EsHost;
        public short Modifico;
        public string Cambios;

        #region Initialization

        public PartyParametros()
        {
            NumOpe = new CdOper();
            PartyPath = "";
            Red = 0;
            Nodo = "";
            Servidor = "";
            Leidos = 0;
            LimInf = 0;
            LimSup = 0;
            Otros = 0;
            Insertado = 0;
            PorInsertar = 0;
            Retorno = 0;
            Indice = 0;
            NoOperacion = 0;
            EsHost = 0;
            Modifico = 0;
            Cambios = "";
        }

        #endregion
    }

    //'Definimos el tipo party
    public class PartyKey
    {
        public short Borrado;
        public short TieneRut;
        public short Status;
        public string LlaveArchivo;
        public string NombreUsado;
        public short IndNombre;
        public string DireccionUsado;
        public short IndDireccion;
        public string ComunaUsado;
        public string EstadoUsado;
        public string CiudadUsado;
        public string PostalUsado;
        public string PaisUsado;
        public short CodPais;
        public short TipoParty;
        public short FlagParty;
        public short Ubicacion;
        public string rut;
        public string Swift;
        public string CodBanco;
        public string Telefono;
        public string Fax;
        public string Telex;
        public short Enviara;
        public string CasPostal;
        public string CasBanco;
    }

    public class PartysPope
    {
        public short Status;
        public short Secuencia;
        public short EsBanco;
        public string RutSwift;
        public string Nombre;
        public string Direccion;
        public string comuna;
        public string Ciudad;
        public string estado;
        public string Pais;
        public short CodPais;
        public string Postal;
        public string Telefono;
        public string Fax;
        public string Telex;
        public short Enviara;
        public string CasPostal;
        public string CasBanco;
    }
    //-----
    // UPGRADE_INFO (#0501): The 'estr_ovd' member isn't used anywhere in current application.
    public class estr_ovd
    {
        public short id_cuenta;
        public string Glosa;
        public string Nemonico;
        public short nacional;
        public short origen;
        public short Via;
        public short Vuelto;
    }
    public class T_Module1
    {
        public T_Module1()
        {
            Codop = new CdOper();
            Codop_FT = new CdOper();
            Codop_CVD = new CdOper();
            PartysOpe = new PartyKey[0];
            PrtControl = new PartyParametros();
            Partys = new PartyKey[0];
            PopeOpe = new PartysPope[0];
            Pope = new PartysPope[0];
            PrtTbl = new string[0];
        }

        public  CdOper Codop_FT;
        public  CdOper Codop_CVD;
        public CdOper Codop;
        public  PartyKey[] PartysOpe;
        public  PartyParametros PrtControl;
        public  PartyKey[] Partys;
        public  PartysPope[] PopeOpe;
        public PartysPope[] Pope;
        public  string[] PrtTbl;
        public  string KeyPrt;

        //Declaracion de Constantes party
        public const string GPrt_GetParty = "Captura de Participantes";
        public const string GPrt_NoPath = "Error del path a Bases de Datos";
        //Declaracion de Valores de Retorno de GetParty
        public const short GPrt_RetExiste = 0;
        public const short GPrt_RetCancelo = 1;
        //Declaracion de ubicaciones
        public const short GPrt_EnParty = 0;
        public const short GPrt_EnOperacion = 1;

        //declaraciones de status normales
        public const short GPrt_StatVacio = 0;
        public const short GPrt_StatLleno = 1;
        public const short GPrt_StatDatos = 2;
        public const short GPrt_StatDatosLleno = 3;

        //declaracion status Pope
        public const short GPrt_StatNuevo = -1;
        public const short GPrt_StatCambio = -2;
        public const short GPrt_StatBorro = -3;
        public const short GPrt_StatIntacto = -4;
        public const short GPrt_StatVacion = -5;

        public const short KEY_PRIOR = 0x21;
        public const short KEY_NEXT = 0x22;
        public const short KEY_UP = 0x26;
        public const short KEY_DOWN = 0x28;

        public const short GPrt_TipoBanco = 1;
        public const short GPrt_TipoEnOperacion = 3;
        public const short GPrt_TipoBcoOperacion = 4;

        //tipos de Flag
        public const short GPrt_FlagInst = 1;

        //marcas de lista para operaciones especiales
        public const string GPrt_MarcaRequerido = "&";
        public const string GPrt_MarcaOtros = "#";
        public const string GPrt_MarcaBanco = "@";

        public const string GPrt_TxtOtros = "(Otros)";
        public const string GPrt_InputCambio = "La descripción para el participante es requerida";
        public const string GPrt_Caption = "Identificar Participantes";
        public const string GPrt_ErrGetDbf = "Se ha producido un error en el acceso a las bases de datos de Participantes.";
        public const string GPrt_ErrRequerido = ", esta información es requerida para la operación.";
        public const string GPrt_ErrRut = "Rut Invalido";
        public const string GPrt_NoPuedeBanco = "La operación requiere que este participante sea un banco, por lo tanto este participante no puede estar asociado a la operación.";
        public const string GPrt_ErrEliminar = "El participante posee datos asociados en la operacion, para eliminarlo deberá primero liberar dicha información.";
        public const string GPrt_ErrModificar = "El participante tiene asociada información que impide su modificación, para modificarlo deberá primero liberar dicha información";
    }
}
