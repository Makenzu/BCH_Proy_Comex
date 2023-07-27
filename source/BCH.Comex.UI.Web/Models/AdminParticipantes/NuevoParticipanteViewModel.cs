using BCH.Comex.Common.UI_Modulos;
//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BCH.Comex.UI.Web.Models.AdminParticipantes
{
    public class NuevoParticipanteViewModel
    {

        public List<UI_OptionItem> TipoParticipante { get; set; }
        public int SelectedTipoParticipante { get; set; }

        public List<UI_OptionItem> OpcionFormato { get; set; }
        public int SelectedOpcionFormato { get; set; }
        public UI_Button BotonAceptar { get; set; }
        public UI_Button BotonCancelar { get; set; }

        //Llave de Identificacion
        public UI_TextBox txt_LlaveIdentificacion { get; set; }

        //Situacion
        [Display(Name = "Cliente Comex")]
        public UI_CheckBox OpcionSituacion { get; set; }
        [Display(Name = "Acreedor")]
        public UI_CheckBox OpcionTipob_0 { get; set; }
        [Display(Name = "Corresponsal")]
        public UI_CheckBox OpcionTipob_1 { get; set; }
        [Display(Name = "Avisador")]
        public UI_CheckBox OpcionTipob_2 { get; set; }



        public NuevoParticipanteViewModel()
        {



        }

        public NuevoParticipanteViewModel(UI_PrtEnt00 frmState, InitializationObject initObj)
        {

            this.TipoParticipante = frmState._PrtTipo;
            SelectedTipoParticipante = int.Parse(frmState._PrtTipo.First(x => x.Selected).ID);

            this.OpcionFormato = frmState._PrtFormato;
            SelectedOpcionFormato = int.Parse(frmState._PrtFormato.First(x => x.Selected).ID);

            BotonAceptar = frmState.prtaceptar;
            BotonCancelar = frmState.prtcancelar;
            txt_LlaveIdentificacion = frmState.txt_BNumber;

            OpcionSituacion = frmState.prtsituacion;
            OpcionTipob_0 = frmState._prttipob_0;
            OpcionTipob_1 = frmState._prttipob_1;
            OpcionTipob_2 = frmState._prttipob_2;

        }

    }
}