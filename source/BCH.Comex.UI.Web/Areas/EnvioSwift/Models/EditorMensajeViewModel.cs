using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.SWSE;
using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.UI.Web.Areas.EnvioSwift.Models
{
    public class EditorMensajeViewModel
    {
        public bool EsSupervisor { get; set; }
        public int IdMensaje { get; set; }
        public IList<sw_tipos_msg> TiposMT { get; set; }
        public IList<sw_monedas> Monedas { get; set; }
        public IList<sw_caracter_error> CaracteresError { get; set; }
        public IList<sw_caracter_error> CaracteresError_Z { get; set; }
        public IList<CampoMonto> CamposMontos { get; set; }

        public string TipoMT { get; set; }
        public string CodMonedaSW { get; set; }
        public double Monto { get; set; }
        public string SwiftBancoReceptor { get; set; }
        public string DescBancoReceptor { get; set; }
        //Se agrega para poder actualizar las firmas
        public int Casilla { get; set; }

        public IList<LineaEditorMensajeSwift> LineasMT { get; set; }

        //se agregan para saber si el mensaje editado esta en la tabla de pendientes
        public bool esPendiente { get; set; }
        public string ctectt { get; set; }
        public string codusr { get; set; }
        public string archivo { get; set; }

        public string nuevoNombreArchivo { get; set; }

        public bool EsPlantilla { get; set; }

        public List<UI_Message> Mensajes { get; set; }

        public string[] CamposMTActivoLibre
        {
            get
            {
                return EnvioSwiftService.CamposMTActivosLibres().Where(i => i != string.Empty).ToArray();
            }
        }

        public EditorMensajeViewModel()
        {
            this.LineasMT = new List<LineaEditorMensajeSwift>();
            Mensajes = new List<UI_Message>();
            CodMonedaSW = string.Empty;
        }
    }
}