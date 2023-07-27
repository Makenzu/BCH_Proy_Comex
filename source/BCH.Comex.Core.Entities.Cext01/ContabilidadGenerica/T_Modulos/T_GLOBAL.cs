using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class reg_monedas
    {
        public string CodMoneda;
        public string NomMoneda;
        public string NemMoneda;
        public string SwfMoneda;
        public int Sin_dec;
        public string CodMonBC;

        public reg_monedas()
        {
            CodMoneda = String.Empty;
            NomMoneda = String.Empty;
            NemMoneda = String.Empty;
            SwfMoneda = String.Empty;
            CodMonBC = String.Empty;
        }
    }
    public class estr_ovd
    {
        public int id_cuenta;
        public string Glosa;
        public string Nemonico;
        public int nacional;
        public int origen;
        public int via;
        public int Vuelto;

        public estr_ovd()
        {
            Glosa = String.Empty;
            Nemonico = String.Empty;
        }
    }

    public class cuenta_indice
    {
        public string llave;
        public string Cuenta;
        public string Moneda;

        public cuenta_indice()
        {
            llave = String.Empty;
            Cuenta = String.Empty;
            Moneda = String.Empty;
        }
    }

    public class Tipo_Moneda
    {
        public int CodMoneda;   //Código Banco de Chile.-
        public int CodMonBC;   //Código Banco Central.-
        public string NomMoneda;   //Descripción.-
        public string NemMoneda;   //Nemónico de la Moneda.-
        public string SwfMoneda;   //Moneda Swift.-

        public Tipo_Moneda()
        {
            NomMoneda= String.Empty;   //Descripción.-
            NemMoneda= String.Empty;   //Nemónico de la Moneda.-
            SwfMoneda= String.Empty;   //Moneda Swift.-
        }
    }

    public class Operaciones
    {
        public string Texto;
        public string Path;
        public string Tabla;
        public string Campo;
        public string PrtTabla;
        public string PrtCampo;
        public string RefImp;

        public Operaciones()
        {
            Texto= String.Empty;
            Path= String.Empty;
            Tabla= String.Empty;
            Campo= String.Empty;
            PrtTabla= String.Empty;
            PrtCampo= String.Empty;
            RefImp= String.Empty;
        }
    }
    public class T_GLOBAL
    {
        // __________________________________________________________________________________________________
        // Declaración de Variables Global Módulo de Servicios Sistema de Comercio Exterior
        // =========== == ========= ====== ====== == ========= ======= == ======== ========
        // Producto       : Contabilidad Genérica - GL
        // __________________________________________________________________________________________________
        // datos de aplicacion
        public const string Appl_Descripcion = "Contabilidad Genérica";
        public const string Appl_Version = "2.12 - 28/10/2003";
        public const string Master_Titulo = "SCE_Contabilidad Genérica";
        public const string TitMsg = "Selección de Oficinas";
        // Se quita grabación en Sce_Bcx.-
        public int Hdbc = 0;
        public string[] Beneficiario = null;
        public string moneda_nac = "";
        public int cod_monac = 0;
        public int CodBCCh = 0;
        public string Formato_Entrada = "";
        public string Formato_Salida = "";
        public int Tipo_Op = 0;     // guarda índice LasOper
                                           // __________________________________________
                                           // _______________________________________________________
                                           // DATOS RECUPERABLES DE SCE.INI
                                           // SENDEROS
        public string PathTablas = "";
        public string PathTablasSgt = "";
        public string PathExes = "";
        public string PathContab = "";
        public string PathDoc = "";
        public string PathPartys = "";
        public string PathUsuarios = "";
        public string PathSwf = "";
        public string ModSerPath = "";
        public int acepto_docval = 0;
        public int acepto_swift = 0;

        // __________________________________________________________________________________________________
        public CdOper Operacion_aso = new CdOper();
        public int ult_operacion = 0;
        // definición variable para mantener formato
        public const string formato = "##,###,###,###,##0.00";
        public const string formato_aux = "##,###,###,###,##0";
        public const string formato_paridad = "##,###,##0.0000000";
        public const string formato_tipocambio = "#,###,##0.0000";
        public const string sin_decimales = "_____________";
        public const string con_decimales = "_____________.__";
        public string mon_nacional = "";
        public string moneda_aladi = "";

        public reg_monedas[] cod_nom_moneda = new reg_monedas[0];
        public estr_ovd[] ovd = new estr_ovd[0];
        public cuenta_indice datos_cuenta = new cuenta_indice();
        // Constantes índices PartysOpe
        public const int I_Cli = 0;
        public const int I_Imp = 1;
        // constantes monedas de cuentas contables
        public const int Nem_Ext = 1;     // sólo M/E
        public const int Nem_Nac = 2;     // sólo M/N
        public const int Nem_Amb = 3;     // ambas
        public Tipo_Moneda Money = new Tipo_Moneda();     // Variable General de Moneda.-
        public Operaciones[] LasOper = new Operaciones[0];
        public int LasOperLimite = 0;
        public string[] RefImp = new string[0];
        public string Referencia_Imp = "";
    }
}
