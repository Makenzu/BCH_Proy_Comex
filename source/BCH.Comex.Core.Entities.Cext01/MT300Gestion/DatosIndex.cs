using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Cext01.MT300Gestion
{
    public class DatosIndex
    {
		public string Referencia { get; set; }
		public string Destino { get; set; }
		public string Cuenta { get; set; }
		public DateTime? Fecha { get; set; }
		public bool UsarFiltros { get; set; }
		public int PageSize { get; set; }
		public int RowOffset { get; set; }
		public DatosIndex()
		{
			Referencia = "";
			Destino = "";
			Cuenta = "";
			Fecha = null;
			UsarFiltros = false;
			PageSize = 25;
			RowOffset = 0;
		}
	}
}
