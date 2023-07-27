using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Swift
{
    public interface ILineaMensajeSwift
    {
        string CodCam { get; set; }
        string Descripcion { get; set; }
        string Detalle { get; set; }
        bool TieneErrorDetalle { get; set; }
        string MensajeError { get; set; }
        short LenLinea { get; set; }
        short LenTotal { get; set; }
        string Formato { get; set; }
        int Linea { get; set; }
    }

    public class LineaEditorMensajeSwift: ILineaMensajeSwift
    {
        public string CodMT { get; set; }
        public string CodCam { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }
        public bool TieneErrorDetalle { get; set; }
        public bool TieneErrorVariante { get; set; }
        public string MensajeError { get; set; }
        public short NumLineas { get; set; }
        public short LenLinea { get; set; }
        public short LenTotal { get; set; }
        public bool Incluido { get; set; }
        public bool Obligatorio { get; set; }
        public string Formato { get; set; }
        public int Orden { get; set; }
        public int Linea { get; set; }
        public string Secuencia { get; set; }

        public string VarianteSeleccionada { get; set; }
        public List<string> Variantes { get; set; }

        public bool TieneVariantes
        {
            get
            {
                return (this.Variantes != null && this.Variantes.Count > 1);
            }
        }

        public List<LineaSecundariaEditorMensajeSwift> LineasSecundarias { get; set; } 

        public LineaEditorMensajeSwift()
        {
            this.LineasSecundarias = new List<LineaSecundariaEditorMensajeSwift>();
        }

        public LineaEditorMensajeSwift(short cantLineas)
        {
            this.NumLineas = cantLineas;
            this.LineasSecundarias = new List<LineaSecundariaEditorMensajeSwift>();

            for (int i = 0; i < (cantLineas - 1); i++)
            {
                this.LineasSecundarias.Add(new LineaSecundariaEditorMensajeSwift());
            }
        }

        public bool ValidarLongitudTotal()
        {
            int total = 0;
            
            if(!String.IsNullOrEmpty(this.Detalle))
            {
                total += this.Detalle.Trim().Length;

                foreach (LineaSecundariaEditorMensajeSwift lineaS in this.LineasSecundarias)
                {
                    if (!String.IsNullOrEmpty(lineaS.Detalle))
                    {
                        total += lineaS.Detalle.Trim().Length;
                    }
                }
            }

            return (total <= this.LenTotal);
        }
    }

    public class LineaSecundariaEditorMensajeSwift : ILineaMensajeSwift
    {
        public string CodCam { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }
        public bool TieneErrorDetalle { get; set; }
        public string MensajeError { get; set; }
        public short LenLinea { get; set; }
        public short LenTotal { get; set; }
        public string Formato { get; set; }
        public int Linea { get; set; }
    }
}
