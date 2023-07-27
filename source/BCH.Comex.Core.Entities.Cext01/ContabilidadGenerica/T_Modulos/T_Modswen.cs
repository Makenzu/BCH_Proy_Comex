using System;

namespace BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos
{
    public class Fmt_Swf
    {
        public string Status_Campo;
        public string Id_Campo;
        public string Nombre_Campo;
        public int Orden_Campo;
        public int Repeticion;
        public string Formato_Campo;
        public int Largo_Campo;
        public int Linea_Campo;

        public Fmt_Swf()
        {
            Status_Campo= String.Empty;
            Id_Campo= String.Empty;
            Nombre_Campo= String.Empty;
            Formato_Campo= String.Empty;
        }
    }
    public class T_Mtes
    {
        public string fecmsg;
        public int id_mensaje;
        public int NroRpt;

        public T_Mtes()
        {
            fecmsg = String.Empty;
        }
    }
    public class T_Modswen
    {
        public T_Mtes[] VMts = new T_Mtes[0];
        public Fmt_Swf[] VFmt_Swf = new Fmt_Swf[0];
        public string[] Lin_Mts = null;
        public string RutAis = "";
        public int hab_swift = 0;
    }
}
