using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Swift
{
    public class proc_rh_swi_001DTO
    {
        public string Poder { get; set; }
        public string fun_atributo { get; set; }
        public string PoderTipoFirma { get; set; }

        public bool RegistraPoder
        {
            get
            {
                return (!String.IsNullOrEmpty(this.Poder) && this.Poder.ToUpper() != "NO REGISTRA PODER");
            }
        }

        public string GetPoderTipoFirma
        {
            get
            {
                if (this.RegistraPoder)
                {
                    if (Poder.Substring(6, 1).Trim() != "")
                    {
                        return this.Poder.Substring(6, 1);
                    }
                    else
                    {
                        return this.Poder.Substring(5, 1);
                    }
                }
                else return "NO REGISTRA PODER";
            }
        }

    }
}
