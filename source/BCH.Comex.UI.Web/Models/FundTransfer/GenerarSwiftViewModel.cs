using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class GenerarSwiftViewModel
    {

        //info "estatica"
        public IList<CodigoDeOrdenCampo23Swift> CodigosDeOrdenPosiblesCampo23 { get; set; }
        public IDictionary<string, IList<string>> ReglasCodigosDeOrden { get; set; }
        public IList<SelectListItem> TipoBancosSiBeneficiarioEsBanco { get; set; }
        public IList<SelectListItem> TipoBancosSiBeneficiarioNoEsBanco { get; set; }
        public IList<SelectListItem> Monedas { get; set; }
        public IList<SelectListItem> PaisesTPai { get; set; }
        public IList<SelectListItem> PaisesCPai { get; set; }
        
        public IList<T_BenSwf> BeneficiariosIniciales { get; set; }
        
        //info que es una sola, sin importar la cantidad de planillas (o "montos")
        public bool EsCargaAutomatica { get; set; }
        public T_CliSwf Cliente { get; set; }
        public short IndiceP { get; set; }

        //toda la info q se repite segun cuantas planillas tenga
        public IList<PlanillaViewModel> Planillas { get; set; }
        
        public GenerarSwiftViewModel()
        {
            this.BeneficiariosIniciales = new List<T_BenSwf>();
            this.Planillas = new List<PlanillaViewModel>();
        }

        public IList<T_Swf> DatosSwiftDeTodasLasPlanillas
        {
            get{
                return this.Planillas.Select(p => p.DatosSwift).ToList();
            }
        }
    }

    public class PlanillaViewModel
    {
        //aca tengo toda la info q es propia de cada planilla ("monto") que estoy generando
        public List<CodigoDeOrdenCampo23Swift> CodigosDeOrdenCampo23 { get; set; }
        public T_Swf DatosSwift { get; set; }
        public T_mt103 Montos { get; set; }
        public IDictionary<string, T_BcoSwf> Bancos { get; set; }  //puedo tener hasta 5 o 6 bancos (Banco pagador, Banco emisor, banco intermediario, etc.)       
        public List<LineaMensajeSwift> LineasManuales { get; set; } 
       
        public PlanillaViewModel()
        {
            this.CodigosDeOrdenCampo23 = new List<CodigoDeOrdenCampo23Swift>();
            this.DatosSwift = new T_Swf();
            this.Bancos = new Dictionary<string, T_BcoSwf>();
            this.Montos = new T_mt103();
            this.LineasManuales = new List<LineaMensajeSwift>();
        }
    }
}