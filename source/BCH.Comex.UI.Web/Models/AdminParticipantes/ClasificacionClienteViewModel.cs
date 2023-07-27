//INICIO MODIFICACION CNC - ACCENTURE 

//using BCH.Comex.Common.XGPY.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Common.UI_Modulos;
using System.Collections.Generic;
using System.Linq;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using System;
using Microsoft.Ajax.Utilities;
using BCH.Comex.Core.Entities.Cext01;

namespace BCH.Comex.UI.Web.Models.AdminParticipantes
{
    public class ClasificacionClienteViewModel : AdminParticipantesViewModel
    {
        public UI_TextBox txClasiRiesgo { get; set; }
        public UI_TextBox txActividadEconomica { get; set; }
        public UI_TextBox txTipoEvaluRiesgo { get; set; }
        public UI_TextBox txComposicionInstitucional { get; set; }
        public UI_TextBox txTipoClienteNormativo { get; set; }
        public UI_Button BtnAceptar { get; set; }
        public UI_Button BtnCancelar { get; set; }

        public ClasificacionClienteViewModel()
        {

        }

        public ClasificacionClienteViewModel(UI_PrtEnt14 frmState, InitializationObject initObj)
        {
            MensajesDeError = initObj.Mdi_Principal.MESSAGES;
            this.txClasiRiesgo = frmState.txClasiRiesgo;
            this.txActividadEconomica = frmState.txActividadEconomica;
            this.txTipoEvaluRiesgo = frmState.txTipoEvaluRiesgo;
            this.txComposicionInstitucional = frmState.txComposicionInstitucional;
            this.txTipoClienteNormativo = frmState.txTipoClienteNormativo;
            this.BtnAceptar = frmState.BtnAceptar;
            this.BtnCancelar = frmState.BtnCancelar;
            this.txActividadEconomica.Enabled = false;
            this.txClasiRiesgo.Enabled = false;
            this.txTipoEvaluRiesgo.Enabled = false;
            this.txComposicionInstitucional.Enabled = false;
            this.txTipoClienteNormativo.Enabled = false;
        }

        public ClasificacionClienteViewModel(List<String> lista, List<String> detalle, IList<sgt_clf_s01_MS_Result> tblClfRiesgo, UI_PrtEnt14 frmState, InitializationObject initObj)
        {
            MensajesDeError = initObj.Mdi_Principal.MESSAGES;
            String texto_sin_info = "Sin información";

            this.txClasiRiesgo = frmState.txClasiRiesgo;
            this.txActividadEconomica = frmState.txActividadEconomica;
            this.txTipoEvaluRiesgo = frmState.txTipoEvaluRiesgo;
            this.txComposicionInstitucional = frmState.txComposicionInstitucional;
            this.txTipoClienteNormativo = frmState.txTipoClienteNormativo;
            this.BtnAceptar = frmState.BtnAceptar;
            this.BtnCancelar = frmState.BtnCancelar;
            this.txClasiRiesgo.Text = texto_sin_info;
            this.txActividadEconomica.Text = texto_sin_info;
            this.txTipoEvaluRiesgo.Text = texto_sin_info;
            this.txComposicionInstitucional.Text = texto_sin_info;
            this.txTipoClienteNormativo.Text = texto_sin_info;
            string descripcion = " ";
            string codigo = " ";

            if (lista != null && lista.Count() > 0)
            {

                if (!lista.ElementAt(0).IsNullOrWhiteSpace())
                {
                    //Se agrega descripcion de Clasificacion de riesgo
                        int i = 0;
                        Boolean encontrado = false;

                    if (tblClfRiesgo != null)
                    {
                        for (i = 0; i < tblClfRiesgo.Count(); i++)
                        {
                            if (tblClfRiesgo[i].clf_clfcod.ToString().Trim().Equals(lista.ElementAt(0).ToString()))
                            {
                                descripcion = (" - " + tblClfRiesgo[i].clf_clfdes.ToString());
                                i = tblClfRiesgo.Count() + 1;
                                encontrado = true;
                            }
                        }

                        if (!encontrado)
                        {
                            descripcion = " - Sin información";
                        }
                    }
                    this.txClasiRiesgo.Text = lista.ElementAt(0).ToString() + descripcion;
                }
                //Actividad Economica (VALIDAR)
                if (!lista.ElementAt(1).IsNullOrWhiteSpace())
                {
                    this.txActividadEconomica.Text = lista.ElementAt(2).ToString() + " - " + lista.ElementAt(1).ToString();
                }

                if (!lista.ElementAt(3).IsNullOrWhiteSpace())
                {
                    descripcion = detalle.ElementAt(0).ToString() + " - ";
                    this.txTipoEvaluRiesgo.Text = descripcion + lista.ElementAt(3).ToString();
                }
                if (!lista.ElementAt(4).IsNullOrWhiteSpace())
                {
                    descripcion = detalle.ElementAt(1).ToString() + " - ";
                    this.txComposicionInstitucional.Text = descripcion + lista.ElementAt(4).ToString();
                }
                if (!lista.ElementAt(5).IsNullOrWhiteSpace())
                {
                    descripcion = " - " + detalle.ElementAt(2).ToString();
                    this.txTipoClienteNormativo.Text = lista.ElementAt(5).ToString() + descripcion;
                }

            }

            this.txActividadEconomica.Enabled = false;
            this.txClasiRiesgo.Enabled = false;
            this.txTipoEvaluRiesgo.Enabled = false;
            this.txComposicionInstitucional.Enabled = false;
            this.txTipoClienteNormativo.Enabled = false;
        }
    }
}
//FIN MODIFICACION CNC - ACCENTURE