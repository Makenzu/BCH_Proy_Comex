using BCH.Comex.Core.BL.XGPL;
using BCH.Comex.Core.BL.XGPL.Validators;
using ExpressiveAnnotations.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BCH.Comex.UI.Web.Areas.Planillas.Models
{
    public class IngresoVisibleExportacionViewModel
    {
        public string NumeroPresentacion { get; set; }

        [Required]
        [DiaHabil]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Fecha Presentación")]
        public DateTime FechaPresentacion { get; set; }

        public int CodigoPlazaBancoCentralContabilidad { get; set; }

        public string PlazaBancoCentralContabilidad { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal CodigoTipoPlanilla { get; set; }

        public string TipoOperacion { get; set; }

        public string CentroCosto { get; set; }

        public string NombreParty { get; set; }

        public string DireccionParty { get; set; }

        public string RutExportador { get; set; }

        public string Moneda { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:00}")]
        public decimal CodigoMoneda { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}", ApplyFormatInEditMode = true)]
        public decimal MontoBruto { get; set; }

        public decimal Comisiones { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}", ApplyFormatInEditMode = true)]
        public decimal? OtrosGastos { get; set; }

        [AssertThat("MontoBruto - (Comisiones + OtrosGastos) == MontoLiquido",
            ErrorMessage = "No se cumple la lógica: MontoBruto - (Comisiones + OtrosGastos) = MontoLiquido")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}", ApplyFormatInEditMode = true), Display(Name="Valor líquido")]
        public decimal? MontoLiquido { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.0000}", ApplyFormatInEditMode = true)]
        public decimal? ParidadUSD { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.00}", ApplyFormatInEditMode = true)]
        public decimal? MontoDolar { get; set; }

        [RequiredIf("InTipoPlanilla(CodigoTipoPlanilla) == true && PlnEstado == true",
            ErrorMessage = "El Tipo de Cambio de cambio debe tener un valor")]
        [AssertThat("TipoCambio == 0 || (InTipoPlanilla(CodigoTipoPlanilla) == true && PlnEstado == true)",
            ErrorMessage = "Para este tipo de planilla el tipo de cambio no debe llevar valor")]
        [DisplayFormat(DataFormatString = "{0:#,##0.0000}", ApplyFormatInEditMode = true)]
        [DisplayName("Tipo de Cambio")]
        public decimal? TipoCambio { get; set; }

        public string Aduana { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:00}")]
        public decimal? CodigoAduana { get; set; }

        public string NumeroDeclaracion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaVencimiento { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDeclaracion { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal? DFOCodigoEntidadAutorizada { get; set; }

        public string DFOEntidadAutorizada { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal? DFOCodigoTipoFinanciamiento { get; set; }

        public string DFOTipoFinanciamiento { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal? DFOCodigoBancoCentral { get; set; }

        public string DFOBancoCentral { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0000000}")]
        public string DFONumeroPresentacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DFOFechaPresentacion { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:000}")]
        public decimal? AFCodigoMoneda { get; set; }

        public string AFMoneda { get; set; }

        public decimal? AFParidadUSD { get; set; }

        public decimal? AFMonto { get; set; }

        public decimal? AFMontoDolar { get; set; }

        public decimal? AFPlazoVencimiento { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:00}")]
        public decimal? DIECodigoBancoCentral { get; set; }

        public string DIEBancoCentral { get; set; }

        public string DIENumeroEmision { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DIEFechaEmision { get; set; }

        public string Observaciones { get; set; }

        public string Pais { get; set; }

        public bool HabilitarGrupoMonto { get; set; }


        public bool ParentesisComisiones { get; set; }

        public bool InTipoPlanilla(decimal codigoTipoPlanilla)
        {
            return MODXPLN1.PLNLIQ.Contains(codigoTipoPlanilla);           
        }

        [Editable(false)]
        public bool PlnEstado { get; set; }

        [Editable(false)]
        public DateTime FechaAnterior { get; set; }

        public bool PlanillaGuardada { get; set; }
    }
}