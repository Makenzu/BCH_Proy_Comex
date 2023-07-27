using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_Tdme
    {
        public int CodDme;   //Código de Destino M/E.
        public string DesDme = String.Empty;   //Descripción de Destino M/E.
    }
    public class T_MODTDME
    {
        public T_Tdme[] VTDme = new T_Tdme[0];
    }
}
