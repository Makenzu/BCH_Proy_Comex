using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Swift
{
    public enum EstadoSwiftEnviado : byte
    {
        Enviado = 0,
        Ingresado = 1,
        Modificado = 2,
        Rechazado = 3,
        EnAprobacion = 4,
        Procesado = 5,
        Devuelto = 6,
        Autorizado = 7,
        Anulado = 8,
        Bloqueado = 9,
        SinSolicitudFirmas = 10
    }

    public partial class ResultadoBusquedaSwift
    {
        public int? total_imp { get; set; }
        public string comentario { get; set; }
        public string fecha_rec {get; set; }
        public string hora_rec {get; set; }

        public string SesionEmi { get; set; }
        public string SecuenciaEmi { get; set; }
        public string FechaEmi { get; set; }
        public string HoraEmi { get; set; }
        
        public string DetalleRaw { get; set; }
        public IList<LineaDetalleMensajeSwift> LineasDetalle { get; set; }
        public IList<LineaEditorMensajeSwift> LineasEditor { get; set; }
        public bool visualizacion { get; set; }
        
        public string PrioridadDesc
        {
            get
            {
                if (String.IsNullOrEmpty(this.prioridad) && tipo_msg != null && tipo_msg == "MT0")
                {
                    return "S";
                }
                else
                {
                    switch (prioridad)
                    {
                        case "N":
                            return "Normal";
                        case "U":
                            return "Urgente";
                        case "S":
                            return "System";
                        default:
                            return "Normal";
                    }
                }
            }
        }

        public string SesionSecuencia
        {
            get
            {
                return this.sesion.ToString() + this.secuencia.ToString();
            }
        }


        public string CodigoYBranchReceptor 
        {
            get
            {
                string desc = (String.IsNullOrEmpty(this.cod_banco_rec) ? String.Empty : cod_banco_rec.Trim());
                desc += (String.IsNullOrEmpty(this.branch_rec) ? String.Empty : branch_rec.Trim());
                return desc;
            }
        }

        public string CodigoYBranchEmisor
        {
            get
            {
                string desc = this.cod_banco_em.Trim();
                if (!String.IsNullOrEmpty(this.branch_em))
                {
                    desc += this.branch_em.Trim();
                }
                return desc;
            }
        }
    }
}
