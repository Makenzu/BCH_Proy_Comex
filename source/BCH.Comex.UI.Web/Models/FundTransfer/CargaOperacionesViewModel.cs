using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCH.Comex.UI.Web.Models.FundTransfer
{
    public class CargaOperacionesViewModel
    {
        public List<UI_Message> Messages { get; set; }
        public List<pro_sce_prty_s04_MS_Result> Data { get; set; }

        public CargaOperacionesViewModel()
        {
            this.Messages = new List<UI_Message>();
            this.Data = new List<pro_sce_prty_s04_MS_Result>();
        }
    }
}