using System;

namespace BCH.Comex.Core.Entities.Swift
{
    public class PoderUsuario
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string PoderTipoFirma { get; set; }
        public string Poder { get; set; }
        public string FunAtributo { get; set; }
        
        public bool RegistraPoder
        {
            get
            {
                return (!String.IsNullOrEmpty(this.Poder) && this.Poder.ToUpper() != "NO REGISTRA PODER");
            }
        }
    }
}
