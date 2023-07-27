
namespace BCH.Comex.Common.XGPY.T_Modulos
{
    public class PrtGlob
    {
        public string llave = "";
        public int Pertenece = 0;
        public int primera = 0;
        public int cambio_a_acreedor = 0;
        public int cambio_a_corresponsal = 0;
        public int ctas_eliminadas = 0;
        public int EsBanco = 0;
        public int EsCITI = 0;
        public int bctas_eliminadas = 0;
        public int MemosLeidos = 0;
        public int FlagParty = 0;
    }

    public class prtyprincipal
    {
        public PrtGlob PrtGlob;
        public string idparty = "";
        public int estado = 0;
        public int tipo = 0;
        public int Flag = 0;
        public int clasificacion = 0;
        public int sirut = 0;
        public string rut = "";
        public string creacosto = "";
        public string creauser = "";
        public string modcosto = "";
        public string moduser = "";
        public int multiple = 0;
        public string oficina = "";
        public string ejecutivo = "";   //los códigos
        public string actividad = "";
        public string riesgo = "";
        public int codbco = 0;
        public int prime = 0;
        public int libor = 0;
        public double spread = 0;
        public string swif = "";
        public int aladi = 0;
        public string ejecorr = "";
        public int flagins = 0;
        //public string insgen_imp = "";
        //public string insgen_exp = "";
        //public string insgen_ser = "";
        //public string inscob_imp = "";
        //public string inscob_exp = "";
        //public string inscre_imp = "";
        //public string inscre_exp = "";
        public int insgen_imp;
        public int insgen_exp;
        public int insgen_ser;
        public int inscob_imp;
        public int inscob_exp;
        public int inscre_imp;
        public int inscre_exp;
        public string Bnumber = "";
    }
    public class prtynombre
    {
        public int estado = 0;
        public int indice = 0;
        public string nombre = "";
        public string fantasia = "";
        public string contacto = "";
        public string sortkey = "";
        public string rut = "";
        public string borrado = "0";
    }
    public class prtydireccion
    {
        public int estado = 0;
        public int indice = 0;
        public string direccion = "";
        public string comuna = "";
        public int CodComuna = 0;
        public string codpostal = "";
        public string region = "";
        public string ciudad = "";
        public string pais = "";
        public int CodPais = 0;
        public string telefono = "";
        public string fax = "";
        public string telex = "";
        public string CasPostal = "";
        public string CasBanco = "";
        public int enviar_a = 0;
        public int recibe = 0;
        public string email = "";
        public string borrado = "0";
    }
    public class prtytcom
    {
        public int manual;
        public int estado;
        public string sistema;
        public string producto;
        public string etapa;
        public int secuencia;
        public int mto_fijo;
        public double tasa;
        public double hasta;
        public double min;
        public double max;
        public string fecha;
    }
    public class reg_monedas
    {
        public string CodMoneda;
        public string NombMoneda;
        public string Nemmoneda;
        public string Swfmoneda;
        public int sin_dec;
    }
    public class prtybcta
    {
        public int estado;
        public int indice;
        public int activa;
        public string moneda;
        public string cuenta;
        public int especial;
    }
    public class prtyblinea
    {
        public int estado;
        public int indice;
        public int activa;
        public string moneda;
        public string linea;
    }
    public class prtyccta
    {
        public int estado;
        public int indice;
        public int activabco;
        public int activace;
        public int extranjera;
        public string moneda;
        public string cuenta;
        public int est2;
    }
    public class prtyinst
    {
        public int codigo;
        public string Memo;
    }
    public struct tipo_riesgo
    {
        public string codigo;
        public string nombre;
    }
    public class codnom
    {
        public string nombre;
        public int codigo;
    }
    public class prtytgas
    {
        public int estado;
        public string sistema;
        public string producto;
        public string etapa;
        public int tarifa;
        public double monto;
    }
    public class prtytint
    {
        public int estado;
        public string sistema;
        public string producto;
        public string etapa;
        public double tasa;
        public int libor;
        public int prime;
        public int flotante;
    }
    public class cuenta_indice
    {
        public string cuenta;
        public int indice;
    }
    public class T_Cencos
    {
        public string Cent_Costo;
    }

    public struct tipo_localidad
    {
        public int codigo;
        public string nombre;
        public int region;
    }
    public struct tipo_paises
    {
        public int codigo;
        public string nombre;
    }

    public struct tipo_acteco
    {
        public double codigo;
        public string nombre;
    }
    public struct tipo_abrev
    {
        public string abrev;
        public string nombre;
    }
    //public class Cuentas
    //{
    //    public string tipo;
    //    public string nrocta;
    //}

    public class PartyParametros
    {
        public string PartyPath;
        public int Red;
        public string Nodo;
        public string Servidor;
        public int Leidos;
        public int Crear;
    }

    public class T_PRTGLOB
    {
        public static int modifico_tasa = 0;
        public const string Master_Titulo = "SCE_Participantes de Comercio Exterior";
        public const string TitDatos = "Datos Participante";
        public const string TitCuentas = "Cuentas Corrientes Participante";
        public const string TitCtaLin = "Cuentas Corrientes/Líneas de Crédito Participante";
        public const string TitInstrucciones = "Instrucciones Especiales Participante";
        public const string TitTasas = "Tasas Especiales Participante";
        public const string TitAbrir = "Lectura de Participante";
        public const string TitIngreso = "Ingreso de Participante";
        public const string Msg_PartyOldFormat = "¡¡Atención!!, el participante no tiene definido el usuario al cual pertenece.  ¿Desea que el participante quede asociado a su cartera?.";
        public const string TitParty = "Administración de Participantes";
        public const int individuo = 0;
        public const int tipo_banco = 1;
        public const int tipo_cliente = 2;

        // constantes para mantener el estado del participante
        public const int eliminado_nuevo = 0;
        public const int leido = 1;
        public const int nuevo = 2;
        public const int modificado = 3;
        public const int eliminado_leido = 4;
        public const int eliminado_modificado = 5;
        public static string sistema = "";
        public static string producto = "";
        public static string etap = "";
        public static int delista = 0;
        public prtyprincipal Party;
        public prtynombre[] nom;
        public prtydireccion[] direc;
        //public Cuentas[] CtaCCOL; //Aqui no va
        public static string llave = "";
        public static string otro = "";
        public const int MB_ICONEXCLAMATION = 48;     //  Warning message
        public reg_monedas[] cod_nom_moneda;
        public prtyccta[] ctaclie;
        public prtybcta[] ctabancos;
        public prtyblinea[] linbancos;
        public cuenta_indice[] ctabancos_aux;
        public cuenta_indice[] ctaclie_aux;
        public cuenta_indice[] linbancos_aux;

        public codnom[] oficinas;

        public static int FlagCuentas = 0;
        public static int FlagLineas = 0;
        public static int cambio_a_corresponsal = 0;
        public static int cambio_a_acreedor = 0;
        public static int primeralista = 0;
        public static int primera = 0;
        //public static int EsCITI = 0;
        public static int EsBanco = 0;
        public prtyinst[] instruccion;
        public static int FlagInstruccion = 0;
        //public static int FlagParty = 0;
        public static int FlagCtaBco = 0;
        public const int GPRT_FlagInstrucciones = 1;
        public static int Pertenece = 0;
        public tipo_riesgo[] ejecutivos;
        public tipo_riesgo[] riesgo;
        public static tipo_abrev[] abrev = null;
        public const int Gprt_FlagTasas = 2;
        public const int Gprt_FlagCuentas = 4;
        public const int Gprt_FlagCorresponsal = 8;
        public const int Gprt_FlagAcreedor = 16;
        public const int GPRT_FlagSwift = 32;
        public const int GPRT_FlagAladi = 64;
        public const int GPRT_FlagAvisador = 128;
        //public static int ctas_eliminadas = 0;
        public static int bctas_eliminadas = 0;
        public static int blin_eliminadas = 0;
        public prtytcom[] tasacom;
        public static int FlagComision = 0;
        public static int FlagInteres = 0;
        public static int FlagGasto = 0;
        public prtytcom[] rescom;
        public prtytgas[] resgas;
        public prtytint[] resint;
        public prtytint[] tasaint;
        public prtytgas[] tasagas;
        // Constantes para formato en listas
        public const string formato = "#,###,###,###,##0.00";
        public const string guiones = "";     // "********************"
        public const string formato_tasa = "##0.000000";
        public const string guiones_tasa = "";     //  "**********"
        public const string Fono_nac = "(###) ###-####";
        //public T_Cencos[] CenCos;
        public T_Cencos[] CenCos;
        public static tipo_localidad[] localidad = null;
        public static tipo_paises[] paises = null;
        public static string moneda_nac = "PESO CHILENO";
        public static int sig = 0;
        public tipo_acteco[] acteco;
        public const string formato_rut = "___.___.___-_";

        //public static int cambio_a_corresponsal = 0;
        //public static int cambio_a_acreedor = 0;
        public const int MB_ABORTRETRYIGNORE = 2;     //  Abort, Retry, and Ignore buttons
        public const int MB_YESNOCANCEL = 3;     //  Yes, No, and Cancel buttons
        public const int MB_ICONQUESTION = 32;
        //public static int MemosLeidos = 0;
        public static int FlagRazones = 0;
        public static int FlagDireccion = 0;
        public PartyParametros PrtControl;
        public T_PRTGLOB()
        {
            Party = new prtyprincipal();
            Party.PrtGlob = new PrtGlob();
            Party.PrtGlob.llave = "";
            nom = new prtynombre[0];
            direc = new prtydireccion[0];
            cod_nom_moneda = new reg_monedas[0];
            ctabancos = new prtybcta[0];
            linbancos = new prtyblinea[0];
            ctaclie = new prtyccta[0];
            instruccion = new prtyinst[0];
            ejecutivos = new tipo_riesgo[0];
            riesgo = new tipo_riesgo[0];

            oficinas = new codnom[0];
            paises = new tipo_paises[0];
            localidad = new tipo_localidad[0];

            tasacom = new prtytcom[0];
            rescom = new prtytcom[0];
            resgas = new prtytgas[0];
            resint = new prtytint[0];
            tasaint = new prtytint[0];
            tasagas = new prtytgas[0];
            CenCos = new T_Cencos[0];
            ctabancos_aux = new cuenta_indice[0];
            ctaclie_aux = new cuenta_indice[0];
            linbancos_aux = new cuenta_indice[0];
            acteco = new tipo_acteco[0];
            abrev = new tipo_abrev[0];
            PrtControl = new PartyParametros();
        }
    }
}
