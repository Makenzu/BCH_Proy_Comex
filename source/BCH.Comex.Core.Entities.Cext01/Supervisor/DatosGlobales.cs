using BCH.Comex.Core.Entities.Portal;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.Supervisor
{
    /// <summary>
    /// Contiene los datos globales del módulo Supervisor
    /// </summary>
    public class DatosGlobales
    {
        // Contiene Datos del Usuario que se conecta
        public sce_usr UsrEsp = new sce_usr();
        // Contiene Datos de los Especialistas del Usuario Lider
        public List<sce_usr> UsrLidEsp = null;
        //  D.S.B.
        public sce_usr UsrLid { get; set; }

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

        public FrmgUsrDTO FrmUsr;
        public FrmTraspDTO FrmTrasp;
        public FrmCederDTO FrmCeder;
        
        public FrmAyMDTO FrmAyM;
        public FrmCamClDTO FrmCamCl;
        public FrmgChqDTO FrmgChq;
        
        /// <summary>
        /// Datos de configuracion del usuario logueado
        /// </summary>
        public IDatosUsuario DatosUsuario { get; set; }
        public List<UI_Message> ListaMensajesError { get; set; }

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

        public List<T_Prd> Vprd { get; set; }
        public List<T_Chq> V_Chq { get; set; }
        public List<T_Vvi> V_Vvi { get; set; }
        public List<T_MndIng> VMndIng { get; set; }
        

        public DatosGlobales()
        {
            ListaMensajesError = new List<UI_Message>();
            UsrLid = new sce_usr();
            VUsr1 = new List<Usr1>();
            VUsr = new List<T_Usr>();
            Vprd = new List<T_Prd>();
            
        }
    }

    public enum TipoMensaje
    {
        Nada = 0,
        Correcto = 1,
        Informacion = 2,
        Error = 3,
        Critical = 4,
        YesNo = 5,
        Warning = 6
    }

    public class UI_Message
    {
        public TipoMensaje Type { set; get; }
        public string Text { set; get; }
        public string Title { get; set; }
        public string ControlName { get; set; } //se agrega para hacer referencia a un control determinado
        public bool AutoClose { get; set; }

        public UI_Message()
        {
            this.Type = TipoMensaje.Nada;
            this.Text = String.Empty;
            this.Title = String.Empty;
        }

        public bool IsError
        {
            get
            {
                return (this.Type == TipoMensaje.Error || this.Type == TipoMensaje.Critical);
            }
        }

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

    public class T_Chq
    {
        public int indice { get; set; } //indice aux del cheque en la lista de cheques, agregado para la impresion.

        public string codcct { get; set; }
        public string codpro { get; set; }
        public string codesp { get; set; }
        public string CodEmp { get; set; }
        public string codope { get; set; }
        public int NroCor { get; set; }
        public string FecEmi { get; set; }//ViewModel
        public int estado { get; set; }
        public string CctSup { get; set; }
        public string UsrSup { get; set; }
        public int NroFol { get; set; }
        public string MonSwf { get; set; }//ViewModel
        public double MtoChq { get; set; }//ViewModel
        public string NomBen { get; set; }//ViewModel
        public string NomPag { get; set; }
        public string DirPag { get; set; }
        public string swfpag { get; set; }
        public string CiuPag { get; set; }
        public string PaiPag { get; set; }
        public string numcta { get; set; }
        public string NomCli { get; set; }
        public string MtoChq_t { get; set; }
        public string NomPro { get; set; }

        public string operacion { get { return (codcct.Trim() + '-' + codpro.Trim() + '-' + codesp.Trim() + '-' + CodEmp.Trim() + '-' + codope.Trim()); } }
        public string estimp { get; set; }


        public string DescMontoIngles { get; set; }
        public string MonIngl { get; set; }
        public string FechaIng { get; set; }
    }

    public class T_Vvi
    {
        public string codcct { get; set; }
        public string codpro { get; set; }
        public string codesp { get; set; }
        public string CodEmp { get; set; }
        public string codope { get; set; }
        public string FecEmi { get; set; }
        public int NroCor { get; set; }
        public int estado { get; set; }
        public string CctSup { get; set; }
        public string UsrSup { get; set; }
        public int numfol { get; set; }
        public string NomBen { get; set; }   //Beneficiario.-
        public string RutTom { get; set; }   //Rut Tomador.-
        public string NomTom { get; set; }   //Nombre Tomador.-
        public double MtoVvi { get; set; }
        public string MtoVvi_t { get; set; }



        public string MonSwf { get; set; }//ViewModel
        public string Mto { get { return "$ " + MtoVvi.ToString("#,###,###,###,##0.00"); } }//ViewModel
        public int NroFol { get { return numfol; } }

        public string operacion { get { return (codcct.Trim() + '-' + codpro.Trim() + '-' + codesp.Trim() + '-' + CodEmp.Trim() + '-' + codope.Trim()); } }
        public string estimp { get; set; }

    }

    // Arreglo Para Mantener Nombre de Monedas en Ingles.-
    public class T_MndIng
    {
        public string Mnd_MndNmc;
        public string mnd_mndina;
    }


    public class T_Cliente
    {
        public string rutOriginal { get; set; }
        public string rut { get; set; }
        public string razonSocial { get; set; }
        public string descripcion { get { return rut.PadRight(12, '\xA0') + razonSocial; } }
    }

    public class T_Prod
    {
        public string codPro { get; set; }
        public string desPro { get; set; }
        public int estado { get; set; }
    }

    public struct T_OpeCli
    {
        public int canOpe { get; set; }
        public int codPro { get; set; }
        public string desPro { get; set; }
        public string descripcion { get { return desPro.PadRight(40,'\xA0') + canOpe; } }
    }
      

}
