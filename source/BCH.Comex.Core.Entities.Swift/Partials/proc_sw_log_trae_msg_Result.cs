using System;

namespace BCH.Comex.Core.Entities.Swift
{
    public partial class proc_sw_log_trae_msg_MS_Result
    {
        public string NombrePersonaAis { get; set; }

        public string DescPersonaAis
        {
            get
            {
                if(String.IsNullOrEmpty(this.NombrePersonaAis))
                {
                    if(rutais_log == null)
                    {
                        return null;
                    }
                    else
                    {
                        return rutais_log.Value.ToString("N0");
                    }
                }
                else
                {
                    return this.NombrePersonaAis;
                }
            }
        }
    }
}
