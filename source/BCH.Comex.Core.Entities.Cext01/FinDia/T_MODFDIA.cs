using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.FinDia
{
    public class T_MODFDIA
    {
        public IList<string> VBcoFin;
        public IList<string> VCtb;
        public bool RutOK { get; set; }
        public IList<planillas> Est_Pla { get; set; }

        public IList<ReporteProblemas> ListaReportes { get; set; }
        public IList<DetalleProblemas> DetallesReportes { get; set; }
        public IList<Confirmaciones> ListaConfirmaciones { get; set; }

        public bool ChkImpresionListado { get; set; }
        public string impresionPorInyectar { get; set; }
        public string impresionListado { get; set; }
        
        public T_MODFDIA()
        {
            VBcoFin = new List<string>();
            VCtb = new List<string>();
            ListaReportes = new List<ReporteProblemas>();
            DetallesReportes = new List<DetalleProblemas>();
            ListaConfirmaciones = new List<Confirmaciones>();
            RutOK = false;
            Est_Pla = new List<planillas>();
            impresionPorInyectar = string.Empty;
            impresionListado = string.Empty;
        }
    }


    public class planillas
      {
         public int numpla;
         public string fecpla;
      }

    public class ReporteProblemas
    {
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public IList<DetalleProblemas> DetallesProblemas { get; set; }
        public UI_Message listaConfirmacion { get; set; }
        public FormMostrar formMostrarConfirmacion { get; set; }
        public TipoReporte tipoReporte { get; set; }

        public ReporteProblemas()
        {
            Titulo = string.Empty;
            Mensaje = string.Empty;
            DetallesProblemas = new List<DetalleProblemas>();
            listaConfirmacion = new UI_Message();
            formMostrarConfirmacion = FormMostrar.sinForm;
            tipoReporte = TipoReporte.ReporteCompleto;
        }
    }

    public class DetalleProblemas
    {
        public string NroOperacion { get; set; }
        public string NroReporte { get; set; }
        public string FechaContable { get; set; }
    }

    public enum FormMostrar
    {
        sinForm = 0,
        frmRut = 1,
        frmClave = 2
    }

    public enum TipoReporte
    {
        ReporteCompleto = 0,
        ReportePlanillas = 1
    }

    public class Confirmaciones
    {
        public UI_Message Confirmacion { get; set; }
        public string accionAceptar { get; set; }
        public string accionCancelar { get; set; }
        public FormMostrar formMostrarConfirmacion { get; set; }
    }
}
