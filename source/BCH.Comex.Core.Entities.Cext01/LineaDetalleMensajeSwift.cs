using System;
using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01
{
    public class LineaMensajeSwift
    {
        public int CodMT { get; set; }
        public string CodCam { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }
        public bool TieneError { get; set; }
        public short NumLineas { get; set; }
        public short LenLlinea { get; set; }
        public short LenTotal { get; set; }
        public bool EsManual { get; set; }
        public bool Incluido { get; set; }        

        public List<LineaSecundariaMensajeSwift> LineasSecundarias { get; set; } //es necesario que las lineas secundarias sean su propia clase y no simplemente una lista de strings, porque Knockout al bindear a un array de strings no las deja como observables

        public LineaMensajeSwift()
        {
            this.LineasSecundarias = new List<LineaSecundariaMensajeSwift>();
        }

        public LineaMensajeSwift(short cantLineas)
        {
            this.NumLineas = cantLineas;
            this.LineasSecundarias = new List<LineaSecundariaMensajeSwift>();

            for (int i = 0; i < (cantLineas - 1); i++)
            {
                this.LineasSecundarias.Add(new LineaSecundariaMensajeSwift());
            }
        }

        public bool ValidarLongitudTotal()
        {
            int total = 0;
            
            if(!String.IsNullOrEmpty(this.Detalle))
            {
                total += this.Detalle.Trim().Length;

                foreach(LineaSecundariaMensajeSwift lineaS in this.LineasSecundarias)
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

    public class LineaSecundariaMensajeSwift
    {
        public string Detalle { get; set; }
        public IList<SelectListLine> ValorCampo { get; set; }

        public LineaSecundariaMensajeSwift()
        {
            ValorCampo = new List<SelectListLine>();
            Detalle = string.Empty;
        }
    }
    
    public class SelectListLine
    {
        public SelectListLine() { }
        public bool Disabled { get; set; }
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
