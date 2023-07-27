using System;

namespace BCH.Comex.Core.Entities.Swift
{
    public class proc_sw_log_trae_msgsendDTO
    {
        public string NombrePersonaAis { get; set; }

        //public string DescPersonaAis
        //{
        //    get
        //    {
        //        if (String.IsNullOrEmpty(this.NombrePersonaAis))
        //        {
        //            if (rutais_log == null)
        //            {
        //                return null;
        //            }
        //            else
        //            {
        //                return rutais_log;//.Value();//.ToString("N0");
        //            }
        //        }
        //        else
        //        {
        //            return this.NombrePersonaAis;
        //        }
        //    }
        //}
        public DateTime fecha_log { get; set; }
        public int rutais_log { get; set; }
        public int casilla_origen { get; set; }
        public string estado_origen { get; set; }
        public int estado_destino { get; set; }
        //public string estado_destino { get; set; }
        public string nombre_casilla { get; set; }
        public string comentario_log { get; set; }
    }
}
