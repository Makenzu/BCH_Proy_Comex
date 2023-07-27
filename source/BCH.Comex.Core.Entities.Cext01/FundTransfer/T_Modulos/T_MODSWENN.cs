using System;

namespace BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos
{
    public class Fmt_Swf
    {
        public string Status_Campo;
        public string Id_Campo;
        public string Nombre_Campo;
        public short Orden_Campo;
        public short Repeticion;
        public string Formato_Campo;
        public short Largo_Campo;
        public short Linea_Campo;
    }

    public class T_Mtes
    {
        public string fecmsg;
        public int id_mensaje;
        public int NroRpt;
    }
    public class T_MODSWENN
    {
        public T_Mtes[] VMts;

        public Fmt_Swf[] VFmt_Swf;
        public string[] Lin_Mts;
        public string RutAis = String.Empty;
        public short Hab_Swift;

        public string gb_RutUsuario = String.Empty;
        public string gb_DvUsuario = String.Empty;

        public T_MODSWENN()
        {
            VMts = new T_Mtes[0];
            VFmt_Swf = new Fmt_Swf[0];
        }
    }
}
