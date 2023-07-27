using System;

namespace BCH.Comex.Core.Entities.Swift
{
    public partial class sw_msgsend_firma
    {
        public string NombrePersonaSolicita { get; set; }
        public string NombrePersonaFirma { get; set; }
        public string RutFirmaConDigitoVerificador { get; set; }

        public string DescPersonaSolicita
        {
            get
            {
                return (!String.IsNullOrEmpty(this.NombrePersonaSolicita) ? this.NombrePersonaSolicita : this.rut_solic.ToString());
            }
        }

        public string DescPersonaFirma
        {
            get
            {
                return (!String.IsNullOrEmpty(this.NombrePersonaFirma) ? this.NombrePersonaFirma : this.rut_firma.ToString());
            }
        }



        public string EstadoDesc
        {
            get
            {
                switch (this.estado_firma)
                {
                    case "N":
                        return "Nueva";

                    case "F":
                        return "Realizado";

                    case "D":
                        return "Devuelta";

                    case "P":
                        return "Pendiente";

                    default:
                        return this.estado_firma;
                }
            }
        }

        public sw_msgsend_firma()
        {
            this.estado_firma = "N";
        }
    }
}
