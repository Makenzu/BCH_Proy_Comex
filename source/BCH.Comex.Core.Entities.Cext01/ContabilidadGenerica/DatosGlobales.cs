using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.Core.Entities.Portal;
using System;
using System.Collections.Generic;


namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica
{

    /// <summary>
    /// Contiene los datos Globales de Contabilidad Generica
    /// </summary>
    public class DatosGlobales
    {
        // Contiene Datos del Usuario que se conecta
        public sce_usr UsrEsp = new sce_usr();
        // Contiene Datos de los Especialistas del Usuario Lider
        public List<sce_usr> UsrLidEsp = null;
        //  D.S.B.
        public sce_usr UsrLid { get; set; }
        /// <summary>
        /// Datos de configuracion del usuario logueado
        /// </summary>
        public IDatosUsuario DatosUsuario { get; set; }

        public T_MODGUSR MODGUSR { set; get; }//emula las propiedades del modulo MODGUSR
        public T_MODGTAB0 MODGTAB0 { get; set; }
        public T_MODGASO MODGASO { get; set; }
        public T_MODXPLN0 MODXPLN0 { get; set; }
        public T_MODGCVD MODGCVD { get; set; }
        public T_MODXORI MODXORI { get; set; }
        public T_MODGOVD MODGOVD { set; get; }
        public T_SYGETPRT SYGETPRT { set; get; }
        public T_MODGMTA MODGMTA { set; get; }
        public T_GLOBAL GLOBAL { set; get; }

        public T_MODGPYF0 MODGPYF0 { set; get; }
        public T_MODGL MODGL { set; get; }

        public T_MODGNPRT MODGNPRT { get; set; }
        public T_MOD_ADIC MOD_ADIC { set; get; }

        public T_CONTAB01 CONTAB01 { set; get; }
        public T_MODGRNG MODGRNG { set; get; }
        public T_MODGCHQ MODGCHQ { set; get; }
        public T_MODGSWF MODGSWF { set; get; }
        public T_MODGSCE MODGSCE { set; get; }
        public T_ModSaldo ModSaldo { set; get; }
        public T_MODXPRN1 MODXPRN1 { set; get; }

        public T_MODGBIC MODGBIC { set; get; }

        public T_Modswen Modswen { set; get; }
        //public T_Module1 Module1 { get; set; }
        public T_ModNotc ModNotc { get; set; }
        public T_MODCTA MODCTA { get; set; }

        public T_MODGLORI MODGLORI { set; get; }
        public T_CONTABGL CONTABGL { set; get; }
        public T_MODMMT MODMMT { set; get; }
        public T_MODGCON0 MODGCON0 { set; get; }

        public T_MODGADC MODGADC { set; get; }

        public T_MODXDOC0 MODXDOC0 { set; get; }
        public T_MODXDATA MODXDATA { set; get; }

        public T_MODGTIC MODGTIC { set; get; }
        public T_MODCONGL MODCONGL { set; get; }
        public T_MODGNCTA MODGNCTA { set; get; }

        public T_MODXSWF MODXSWF { set; get; }

        public T_MOD_50F MOD_50F { set; get; }

        public T_MODGCON1 MODGCON1 { set; get; }

        public T_MODTDME MODTDME { set; get; }
        public T_MODGLOV MODGLOV { set; get; }

        public FrmgAsoDTO FrmgAso { set; get; }
        public FrmgAsoNCDTO FrmgAsoNC { set; get; }
        public List<UI_Message> MESSAGES { set; get; }  

        public string Action { set; get; }//Metodo del Controller al que vamos a Rutear
        public string Controller { set; get; }//Controller al que vamos a rutear
        public string VieneDeAction { set; get; }
        public string VieneDeController { set; get; }
        public bool vieneDeMsg { set; get; }
        public bool resMsg { set; get; }
        public string FileName { get; set; }


        #region FORMULARIOS UI
        public UI_gl gl { set; get; }
        public UI_GetPrty0 GetPrty0 { set; get; }
        public UI_GetPrty1 GetPrty1 { set; get; }
        public UI_GetPrty2 GetPrty2 { set; get; }
        public UI_GetPrty3 GetPrty3 { set; get; }
        public UI_PrtEnt09 PrtEnt09 { set; get; }
        public UI_Tickets Tickets { set; get; }
        public UI_Frm_Cta Frm_Cta { set; get; }
        public UI_FrmGLOV FrmGLOV { set; get; }
        public UI_FrmGlanu FrmGlanu { get; set; } 
        public UI_FrmgChq FrmgChq { set; get; }
        public UI_Frm_SeleccionOficina Frm_SeleccionOficina { set; get; }
        #endregion


        public struct Usr1
        {
            public string codcct;
            public string codesp;
            public string nombre;
            public int codigo;
            public string Tipeje;
        }
        public static List<Usr1> VUsr1 { get; set; }
        public List<T_Usr> VUsr { get; set; }
        public List<T_Prd> Vprd { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string Tag { get; set; }
        public T_MODGMTS MODGMTS { get; set; }
        public List<string> DocumentosAImprimir { get; set; }
        public string STR_TICKETS { get; set; }

        public DatosGlobales()
        {   
            MESSAGES = new List<UI_Message>();
            UsrLid = new sce_usr();
            VUsr1 = new List<Usr1>();
            VUsr = new List<T_Usr>();
            Vprd = new List<T_Prd>();

            MODGUSR = new T_MODGUSR();
            MODGASO = new T_MODGASO();
            MODGOVD = new T_MODGOVD();
            SYGETPRT = new T_SYGETPRT();
            MODGMTA = new T_MODGMTA();
            MODGL = new T_MODGL();
            GLOBAL = new T_GLOBAL();
            MOD_ADIC = new T_MOD_ADIC();
            CONTAB01 = new T_CONTAB01();
            MODGRNG = new T_MODGRNG();
            MODGCHQ = new T_MODGCHQ();
            MODGSWF = new T_MODGSWF();
            MOD_50F = new T_MOD_50F();
            MODGSCE = new T_MODGSCE();
            ModSaldo = new T_ModSaldo();
            MODXPRN1 = new T_MODXPRN1();
            Modswen = new T_Modswen();
            MODGTAB0 = new T_MODGTAB0();
            MODGCVD = new T_MODGCVD();
            MODGLORI = new T_MODGLORI();
            MODCTA = new T_MODCTA();
            CONTABGL = new T_CONTABGL();
            MODGMTS = new T_MODGMTS();
            MODMMT = new T_MODMMT();
            MODGCON0 = new T_MODGCON0();
            MODGTIC = new T_MODGTIC();
            MODGADC = new T_MODGADC();
            MODXDOC0 = new T_MODXDOC0();
            MODXDATA = new T_MODXDATA();
            MODCONGL = new T_MODCONGL();
            MODGNCTA = new T_MODGNCTA();
            MODXSWF = new T_MODXSWF();
            MOD_50F = new T_MOD_50F();
            MODGCON1 = new T_MODGCON1();
            MODTDME = new T_MODTDME();
            MODGLOV = new T_MODGLOV();
            MODGBIC = new T_MODGBIC();
            MODGPYF0 = new T_MODGPYF0();

            ModNotc = new T_ModNotc();
            Action = String.Empty;
            Controller = String.Empty;
            VieneDeAction = String.Empty;
            VieneDeController = String.Empty;
            vieneDeMsg = false;
            resMsg = false;

            MODGNPRT = new T_MODGNPRT();
            //Module1 = new T_Module1();

            FrmgAsoNC = new FrmgAsoNCDTO() { Facturas = new List<FrmgNCFacturasDTO>() };
            MODCTA = new T_MODCTA();
            DocumentosAImprimir = new List<string>();
        }

       
    }

    /// <summary>
    /// Estructura General de Productividad.-
    /// </summary>
    public class T_Prd
    {
        public string CodCct;   //Centro Costo Especialista.-
        public string CodUsr;   //Código Usuario Especialista.-
        public string nombre_esp;   //Nombre Especialista.-
        public int numcob;   //Cobranzas.-
        public int numret;   //Retornos.-
        public int numpli;   //Planillas Invisibles.-
        public int numplv;   //Planillas Visibles.-
        public int numdec;   //Declaraciones.-
        public int num_gl;   //GL.-
        public int numcce;   //Crédito.-
    }
    
    public class T_Usr
    {
        public string CenCos { get; set; }
        public string CodEsp { get; set; }
        public string CenSup { get; set; }
        public string CodSup { get; set; }
        public string NomEsp { get; set; }
        public int Jerarquia { get; set; }
        public string FecIni { get; set; }
        public string FecFin { get; set; }
        public string FecOut { get; set; }
        public int ConFin { get; set; }

        public string CecoEsp { get { return CenCos + '-' + CodEsp; } }
    }

        

}
