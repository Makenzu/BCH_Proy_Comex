using BCH.Comex.Common.UI_Modulos;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms
{
    public class UI_Frm
    {
        public List<UI_Message> ListaErrores
        {
            set; get;
        }
        public List<UI_Message> ListaConfirmaciones { set; get; }
        public string Text { set; get; }
        public string Tag { set; get; }

        public UI_Frm()
        {
            ListaErrores = new List<UI_Message>();
            ListaConfirmaciones = new List<UI_Message>();
            Text = String.Empty;
            Tag = String.Empty;
        }
    }
}
