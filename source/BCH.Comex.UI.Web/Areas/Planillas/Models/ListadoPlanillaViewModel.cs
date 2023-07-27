using BCH.Comex.Core.BL.XGPL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    [Bind(Include = "CentroCosto,CodigoUsuario,FechaIngreso")]
    public class ListadoPlanillasViewModel
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:n3}", ApplyFormatInEditMode = true)]
        public string CentroCosto { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public string CodigoUsuario { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        // Informativos
        public FrmgPlv.TipoListado Tipo { get; set; }
        public string NombreUsuario { get; set; }
        public IEnumerable<ListadoPlanillasItemViewModel> Detalle { get; set; }

        public string Titulo
        {
            get
            {
                return FrmgPlv.NombreModelo(Tipo, NombreUsuario, FechaIngreso);
            }
        }

        public string NombreAccionVisualizar
        {
            get
            {
                switch (Tipo)
                {
                    case FrmgPlv.TipoListado.tplVisibleExportacion:
                        return "VerIngresoVisibleExportacion";
                    case FrmgPlv.TipoListado.tplInvisibleExportacion:
                        return "VerInvisibleExportacion";
                    case FrmgPlv.TipoListado.tplAnuladaExportacion:
                        return "VerVisibleExportacionAnulada";
                    case FrmgPlv.TipoListado.tplVisibleImportacionEndosadas:
                        return "VerEstadisticaImportacion";
                    case FrmgPlv.TipoListado.tplVisibleImportacion:
                        return "VerVisibleImportacion";
                    default:
                        return "";
                }
            }
        }

        public string NombreAccionImprimir
        {
            get
            {
                switch (Tipo)
                {
                    case FrmgPlv.TipoListado.tplVisibleExportacion:
                        return "ImprimirPlanillaVisibleExportacion";
                    case FrmgPlv.TipoListado.tplInvisibleExportacion:
                        return "ImprimirPlanillaInvisibleExportacion";
                    case FrmgPlv.TipoListado.tplAnuladaExportacion:
                        return "ImprimirPlanillaAnulada";
                    case FrmgPlv.TipoListado.tplVisibleImportacionEndosadas:
                        return "ImprimirEstadisticaImportacion";
                    case FrmgPlv.TipoListado.tplVisibleImportacion:
                        return "ImprimirPlanillaReemplazos";
                    default:
                        return "";
                }
            }
        }
    }

    [Serializable]
    public class ListadoPlanillasItemViewModel
    {
        public string Presentacion { get; set; }
        public DateTime FechaPresentacion { get; set; }
        public string CodigoUsuario { get; set; }
        public string Operacion { get; set; }
        public int Estado { get; set; }
        public decimal CodigoMoneda { get; set; }
        public string MontoCIF { get; set; }
        public string Planilla { get; set; }
        public string FechaPresentacionString
        {
            get
            {
                return
                    FechaPresentacion != null ? FechaPresentacion.FormatoFecha() : "";
            }
        }
        public string MonedaString
        {
            get
            {
                if (CodigoMoneda > 0)
                {
                    return MODGTAB0.GetSimboloMoneda((int)CodigoMoneda, new XgplService());
                }
                return "-";
            }
        }

        public string EstadoString { get; set; }

        public object QueryString { get; set; }

        public int? Tipo { get; set; }
        
    }
}