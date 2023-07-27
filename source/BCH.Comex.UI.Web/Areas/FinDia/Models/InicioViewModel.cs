using BCH.Comex.Common.UI_Modulos;
using System;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.FinDia.Models
{
    public class InicioViewModel: FinDiaViewModel
    {
        public UI_CheckBox ChkActualizarNovedades { get; set; }
        public UI_CheckBox ChkImpresionListado { get; set; }
        public IList<InformacionUsuario> ListaUsuarios { get; set; }
        public DateTime FechaControlFinDeDia { get; set; }
        public DateTime? FechaFinDeDiaRealizado { get; set; }


        public InicioViewModel()
        {
            ChkActualizarNovedades = new UI_CheckBox() { Checked = true };
            ChkImpresionListado = new UI_CheckBox() { Checked = true };
            ListaUsuarios = new List<InformacionUsuario>();
        }

        public void update(bool ActualizarNovedades, bool ImpresionListado, IList<UI_Message> ListaErrores)
        {
            ChkActualizarNovedades.Checked = ActualizarNovedades;
            ChkImpresionListado.Checked = ImpresionListado;

            this.ListaErrores = ListaErrores;
        }

        public void addUsuario(string usr, string nombre, string lider, string seccion, string ciudad)
        {
            this.ListaUsuarios.Add(new InformacionUsuario() { Usuario = usr, Ciudad = ciudad, Lider = lider, Nombre = nombre, Seccion = seccion });
        }

    }
}