using System;

namespace BCH.Comex.Core.Entities.Cext01
{
    public partial class sce_mcd
    {
        public string NombreCuenta { get; set; }
        public string DescAdicional { get; set; }

        public string NumCtaConFormato
        {
            get
            {
                if (!String.IsNullOrEmpty(this.numcta))
                {
                    return this.numcta.Substring(0, 3) + "." + this.numcta.Substring(3, 2) + "." + this.numcta.Substring(5, 2) + "-" + this.numcta.Substring(7, 1);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        
    }
}
