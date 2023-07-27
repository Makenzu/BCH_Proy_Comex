using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class T_Ticket
    {
        public string Nomtic = String.Empty;
        public string Nemtic = String.Empty;
        public string Montic = String.Empty;
        public string Cuetic = String.Empty;
        public int Dehtic;
        public string Contic = String.Empty;
        public int Demtci;
        public string Glosa = String.Empty;
    }
    public class T_MODGTIC
    {
        public T_Ticket Strtic = new T_Ticket();
    }
}
