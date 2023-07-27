using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCH.Comex.Core.BL.SWEM.UI_Modulos;
using BCH.Comex.Core.Entities.Swift;
using System.Web.Mvc;

namespace BCH.Comex.UI.Web.Models.ConsultaSwift
{
    public class RecibirMensajeViewModel
    {
        public List<UI_OptionItem> TipoTitulos { get; set; }
        public int SelectedTipoTitulos { get; set; }

        //public List<proc_sw_rec_trae_enc_rango_MS_Result> listaPendiente { get; set; }
        //public List<proc_sw_rec_trae_ree_rango_MS_Result> listaReenviados { get; set; }
        public SelectList TodasLasCasillas { get; set; }
        public List<string> IdsCasillasVisibles { get; set; }
        public string IdCasillaDefault { get; set; }

        private SelectList casillasVisibles;
        public SelectList CasillasVisibles
        {
            get
            {
                if (this.casillasVisibles != null)
                {
                    return casillasVisibles;
                }
                else return this.TodasLasCasillas;
            }
            set
            {
                casillasVisibles = value;
            }
        }


        public RecibirMensajeViewModel()
        {
            TipoTitulos = new List<UI_OptionItem>() {
                new UI_OptionItem { ID="0", Value="Pendientes", Selected=true },
                new UI_OptionItem { ID="1", Value="Confirmados" },
                new UI_OptionItem { ID="2", Value="Impresos" },
                new UI_OptionItem { ID="3", Value="Reenviados" }
            };
            SelectedTipoTitulos = int.Parse(TipoTitulos.First(x => x.Selected).ID);
            this.IdsCasillasVisibles = new List<string>();

            //listaPendiente = new List<proc_sw_rec_trae_enc_rango_MS_Result>();
            //listaReenviados = new List<proc_sw_rec_trae_ree_rango_MS_Result>();


        }




    }
}