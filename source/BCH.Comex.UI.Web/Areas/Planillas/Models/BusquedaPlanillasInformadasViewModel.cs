using BCH.Comex.Core.BL.XGPL;
using ExpressiveAnnotations.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public enum TipoPlanilla
    {
        Importacion,
        Exportacion
    }

    public class BusquedaPlanillasInformadasItemViewModel
    {
        public int Correlativo { get; set; }
        public string NumeroPresentacion { get; set; }
        public DateTime FechaPresentacion { get; set; }
        public string NumeroDeclaracion { get; set; }
        public DateTime FechaDeclaracion { get; set; }
        public int CodigoAduana { get; set; }
        public DateTime VtoRet { get; set; } // ???
        public decimal MontoDolaresPlanilla { get; set; }
        public decimal MontoDolaresDeclaracionIngreso { get; set; }
        public decimal MontoInteresDeclaracionIngreso { get; set; }
        public string FechaPresentacionString { get { return FechaPresentacion.FormatoFecha(); } }
        public string VtoRetString { get { return VtoRet.FormatoFecha(); } }
        public string MontoDolaresDeclaracionIngresoString { get { return MontoDolaresDeclaracionIngreso.FormatoDecimal(); } }
        public string MontoInteresDeclaracionIngresoString { get { return MontoInteresDeclaracionIngreso.FormatoDecimal(); } }
        public char TipoPlanilla { get; set; }
    }

    public class BusquedaPlanillasInformadasViewModel
    {
        [Required]
        public TipoPlanilla TipoPlanilla { get; set; }
        [AssertThat("Fecha >= Date(1900, 1, 1)", ErrorMessage = "Debe especificar una fecha válida")]
        public DateTime Fecha { get; set; }
        public IQueryable<BusquedaPlanillasInformadasItemViewModel> Planillas { get; set; }
    }
}