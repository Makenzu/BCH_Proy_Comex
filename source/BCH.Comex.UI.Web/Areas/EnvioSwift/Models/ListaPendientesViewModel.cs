using BCH.Comex.Core.Entities.Cext01;
using System;
using System.Collections.Generic;

namespace BCH.Comex.UI.Web.Areas.EnvioSwift.Models
{
    public class ListaPendientesViewModel
    {
        public IList<swiftPendiente> listaSwift { get; set; }

        public ListaPendientesViewModel()
        {
            listaSwift = new List<swiftPendiente>();
        }

        public ListaPendientesViewModel(IList<pro_sce_swf_pendientes_s01_MS_Result> listado)
        {
            listaSwift = new List<swiftPendiente>();

            foreach (var item in listado)
            {
                listaSwift.Add(new swiftPendiente() {
                                    archivo = item.archivo,
                                    sistema = item.sistema,
                                    fecha = item.fecha,
                                    tipo = item.tipo,
                                    moneda = item.moneda,
                                    monto = (item.monto.HasValue ? (float)item.monto : 0),
                                    referencia = item.referencia,
                                    esPlantilla = (item.esPlantilla.HasValue ? item.esPlantilla.Value : false)
                                    });
            }
        }
    }

    public class swiftPendiente
    {
        public string archivo { get; set; }
        public string sistema { get; set; }
        public DateTime fecha { get; set; }
        public string tipo { get; set; }
        public string moneda { get; set; }
        public float monto { get; set; }
        public string referencia { get; set; }

        public bool esPlantilla { get; set; }
    }
}