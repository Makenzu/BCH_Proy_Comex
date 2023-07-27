using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class ParticipantesViewModel : FundTransferViewModel
    {
        private UI_Frm_Participantes uI_Frm_Participantes;
        private List<UI_Message> list;

        [Display(Name = "Participantes")]
        public int SelectedPartiList { get; set; }
        public List<SelectListItem> PartiList { get; set; }
        [Display(Name = "Llave de identificación")]
        public string KeyText { get; set; }
        public UI_TextBox Llave { get; set; }

        [Display(Name="Dirección")]
        public string Tx_Dir { get; set; }
        
        [Display(Name="Ubicación")]
        public List<UI_OptionItem> Donde { get; set; }
        public int SelectedDonde { get; set; }

        [Display(Name = "Tipo de operación")]
        public List<UI_OptionItem> TipoOperacion { get; set; }
        public int SelectedTipoOperacion { get; set; }

        public UI_Button BtnEliminar { get; set; }
        public UI_Button BtnAceptar { get; set; }
        public UI_Button BtnIdentificar { get; set; }
        public UI_Button BtnBuscar { get; set; }
        /// <summary>
        /// Lo cargo cuando tengo que mostrar el popup de identificar Participante
        /// </summary>
        public bool AbrirIdentParticipantes { get; set; }
        public string Redireccionar { get; set; }
        public string OPE { set; get; }
        public bool AbrirDesdeCargaOperaciones { get; set; }
        public int CargaAutomatica { get; set; }

        public bool ParticipanteCongelado { get; set; }

        public ParticipantesViewModel()
        {
        }

        private ParticipantesViewModel(UI_Frm_Participantes frmState)
        {
            this.Tx_Dir = frmState.Tx_Dir.Text;
            this.Llave = frmState.Llave;
            this.KeyText = frmState.Llave.Text;
            
            this.AbrirIdentParticipantes = frmState.AbrirIdentParticipantes;
            this.AbrirDesdeCargaOperaciones = frmState.AbrirDesdeCargaOperaciones;
            this.CargaAutomatica = frmState.CargaAutomatica;

            this.SelectedPartiList = frmState.LstPartys.ListIndex;
            this.PartiList = new List<SelectListItem>();
            for (int i = 0; i < frmState.LstPartys.Items.Count; i++)
            {
                this.PartiList.Add(new SelectListItem
                {
                    Text = frmState.LstPartys.Items[i].Value,
                    Value = i.ToString(),
                    Selected = frmState.LstPartys.ListIndex == i,
                });
            }

            this.BtnIdentificar = frmState.Identificar;
            this.BtnAceptar = frmState.Aceptar;
            this.BtnEliminar = frmState.Eliminar;
            this.BtnBuscar = frmState.Bot_Nem;

            this.Donde = frmState.Donde;
            this.SelectedDonde = int.Parse(frmState.Donde.First(x => x.Selected).ID);

            this.TipoOperacion = frmState.TipoOperacion;
            this.SelectedTipoOperacion = int.Parse(frmState.TipoOperacion.First(x => x.Selected).ID);
        }

        public ParticipantesViewModel(UI_Frm_Participantes frmState, string accionARedireccionar, List<UI_Message> errores)
            : this(frmState, errores)
        {
            if (!string.IsNullOrEmpty(accionARedireccionar))
            {
                //transformo el Action a URL
                UrlHelper helper = new UrlHelper(System.Web.HttpContext.Current.Request.RequestContext);
                this.Redireccionar = helper.Action(accionARedireccionar);
            }
        }

        public ParticipantesViewModel(UI_Frm_Participantes frmState, List<UI_Message> errores) : this(frmState)
        {
            this.MensajesDeError = errores;
        }
    }
}
