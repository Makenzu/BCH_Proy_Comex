using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms
{
    public class UI_Frm_SeleccionOficina
    {
        public string Oficinas = "";
        public string[] nomsuc;
        public List<T_Suc> VjSuc;

        public UI_Combo Cb_Oficina;
        public UI_Button BAceptar { get; set; }
        /// <summary>
        /// Boton Cancelar
        /// </summary>
        public UI_Button BCancelar { get; set; }

        public UI_Frm_SeleccionOficina()
        {
            //nomsuc = new List<string>();
            VjSuc = new List<T_Suc>();

            Cb_Oficina = new UI_Combo();
            BAceptar = new UI_Button();
            BCancelar = new UI_Button();

        }
    }
}
