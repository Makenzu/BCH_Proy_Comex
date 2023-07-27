using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BCH.Comex.UI.Web.Areas.MT300Gestion.Models
{
    public class DetalleViewModel
    {
        public bool EsModificacion { get; set; }
        public bool EsNuevo { get; set; }
        public MensajeViewModel Mensaje { get; set; }
        public IList<UI_Message> ListaMensajes { get; set; }
        public DetalleViewModel()
        {
            ListaMensajes = new List<UI_Message>();
            EsNuevo = false;
            EsModificacion = false;
        }
    }

    public class MensajeViewModel
    {
        public decimal idProcesados { get; set; }
        [Required]
        public string reference { get; set; }
        public string campo22A { get; set; }
        public string campo22C { get; set; }
        [Required]
        public string campo82A { get; set; }
        [Required]
        public string campo87A { get; set; }
        [Required]
        public string bookedBy { get; set; }
        [Required]
        public string valueDate { get; set; }
        [Required]
        public string rate { get; set; }
        [Required]
        public string campo32B { get; set; }
        [Required]
        public string campo53A { get; set; }
        [Required]
        public string campo57A { get; set; }
        [Required]
        public string campo33B { get; set; }
        public string campo98D { get; set; }
    }
}