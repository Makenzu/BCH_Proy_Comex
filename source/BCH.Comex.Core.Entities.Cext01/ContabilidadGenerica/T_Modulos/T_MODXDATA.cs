using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    /// <summary>
    /// Clase creada por @emiliano para ser utilizada para ejecutar el BATCH en el Grabar
    /// Mantiene la estructura de QUERYS para ser procesadas
    /// </summary>
    public class T_MODXDATA
    {
        public List<Func<int>> CmdsQuerysNew = new List<Func<int>>();
    }
}
