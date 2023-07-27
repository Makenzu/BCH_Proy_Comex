using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BCH.Comex.Core.Entities.Swift
{
    public partial class proc_sw_env_trae_files_fecha_string_MS_Result
    {
        public bool PareoCompleto
        {
            get
            {
                return (this.total_mensajes == this.Suma());
            }
        }

        public int? Suma()
        {
            int?[] nums = new int?[] { this.total_rechazos, this.total_envios };
            return nums.Sum();
        }
    }
}
