using System;

namespace BCH.Comex.Core.Entities.Cext01
{
    public class CodigoDeOrdenCampo23Swift
    {
        public enum CodigoOrden: short
        {
            BONL = 0,
	        CHQB = 1,
	        CORT = 2,
	        HOLD = 3,
	        INTC = 4,
	        PHON = 5,
	        PHOB = 6,
	        PHOI = 7,
	        SDVA = 8,
	        TELE = 9,
	        TELB = 10,
	        TELI = 11
        }

        public CodigoOrden Codigo { get; set; }
        public string TextoAdicional { get; set; }
        public bool PermiteTextoAdicional { get; set; }


        public string CodigoDesc
        {
            get
            {
                return Enum.GetName(typeof(CodigoOrden), this.Codigo);
            }
        }
        
        public string DescripcionAyuda 
        {
            get
            {
                switch (this.Codigo)
                {
                    case CodigoOrden.BONL:
                        return "El pago debe efectuarse al cliente beneficiario únicamente.";

                    case CodigoOrden.CHQB:
                        return "Páguese al cliente beneficiario sólo mediante un cheque. No debe utilizarse la línea opcional de número de cuenta en el campo 59.";

                    case CodigoOrden.CORT:
                        return "El pago se efectúa como liquidación de un negocio, por ejemplo, acuerdo del tipo de cambio, transacción de valores.";

                    case CodigoOrden.HOLD:
                        return "El cliente beneficiario / acreedor llamará; pague contra su  identificación.";

                    case CodigoOrden.INTC:
                        return "El pago es un pago intra-compañía, es decir un pago entre dos compañías que pertenecen al mismo grupo.";

                    case CodigoOrden.PHON:
                        return "Favor de comunicar a la institución depositaria de la cuenta por teléfono.";

                    case CodigoOrden.PHOB:
                        return "Favor de comunicar / contactarse con el beneficiario / acreedor por teléfono.";

                    case CodigoOrden.PHOI:
                        return "Favor de avisar a la institución intermediaria por teléfono.";

                    case CodigoOrden.SDVA:
                        return "El pago debe efectuarse con la misma fecha valor al beneficiario.";

                    case CodigoOrden.TELE:
                        return "Favor de comunicar a la institución depositaria de la cuenta a través del medio de telecomunicación más eficiente.";

                    case CodigoOrden.TELB:
                        return "Favor de comunicar / contactarse con el beneficiario / acreedor a través del medio de telecomunicación más eficiente.";

                    case CodigoOrden.TELI:
                        return "Favor de comunicar a la instrucción intermediaria a través del medio de telecomunicación más eficiente.";
                    
                    default:
                        return String.Empty;
                }
            }
        }
    }
}
