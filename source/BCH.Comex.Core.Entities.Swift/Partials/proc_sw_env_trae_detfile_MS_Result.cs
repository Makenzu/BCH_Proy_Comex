using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Swift
{
    public partial class proc_sw_env_trae_detfile_MS_Result
    {
        public string banco_rec
        {
            get
            {
                return (this.cod_banco_rec ?? "").Trim() + (this.branch_rec ?? "").Trim();
            }
        }

        public string fecha_envio_desc
        {
            get
            {
                return this.fecha_envio + " " + this.hora_envio;

            }
        }
    }
}
