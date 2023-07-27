using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Core.Entities.Portal;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.GeneracionMT300
{
    public class T_ModCarga
    {
        public Archivo archivo { get; set; }
        public IList<ArchivoDetalle> registros { get; set; }
    }
    
}
