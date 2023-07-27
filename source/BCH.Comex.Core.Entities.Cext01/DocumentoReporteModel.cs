using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Cext01
{
    public class Letra
    {
        public string Glosa { get; set; }
        public string LetraNro { get; set; }
        public string CodigoMoneda { get; set; }
        public string Moneda { get; set; }
        public string Monto { get; set; }
        public string Vence { get; set; }
    }

    public class DocumentoEmbarque
    {
        public string Nombre { get; set; }
        public string NumeroEmbarque { get; set; }
    }

    public class Embarque
    {
        public string Codigo { get; set; }
        public string Fecha { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
    }

    public class Transferencia
    {
        public string Moneda { get; set; }
        public string Monto { get; set; }
        public string TipoCambio { get; set; }
        public string NroPlanilla { get; set; }
        public string DeclaracionImportacion { get; set; }
    }

    public class ComercioInvisible
    {
        public string CodigoPlanilla { get; set; }
        public string Codigo { get; set; }
        public string Declaracion { get; set; }
        public string Moneda { get; set; }
        public string Monto { get; set; }
    }

    public class MontoRegistro
    {
        public string Glosa { get; set; }
        public string Moneda { get; set; }
        public string Monto { get; set; }
    }

    public class Remesa
    {
        public string Beneficiario { get; set; }
        public string ViaRemesa { get; set; }
        public string Concepto { get; set; }
        public string Moneda { get; set; }
        public string Monto { get; set; }
    }

    public class CargoAbono
    {
        public string DescripcionVia { get; set; }
        public string NombreVia { get; set; }
        public string Moneda { get; set; }
        public string Monto { get; set; }
    }

    public class Divisa
    {
        public string MonedaCompra { get; set; }
        public string MontoCompra { get; set; }
        public string MonedaVenta { get; set; }
        public string MontoVenta { get; set; }
        public string Paridades { get; set; }
    }

    public class ComisionGasto
    {
        public string Tipo { get; set; }
        public string TipoOperacion { get; set; }
        public string Moneda { get; set; }
        public string Monto { get; set; }
    }

    public class DebitoCredito
    {
        public string Moneda { get; set; }
        public string Monto { get; set; }
    }

    public class Impuesto
    {
        public string Detalle { get; set; }
        public string Moneda { get; set; }
        public string Monto { get; set; }
    }

    public class Retorno
    {
        public string Ordenante { get; set; }
        public string Referencia { get; set; }
        public string Moneda { get; set; }
        public string Monto { get; set; }
    }

    public class CompraVenta
    {
        public string MonedaOrigen { get; set; }
        public string Monto { get; set; }
        public string TipoCambio { get; set; }
        public string MonedaDestino { get; set; }
        public string MontoTotal { get; set; }
    }

    public class DocumentoReporteModel
    {
        public string Html { get; set; }
        public string Ciudad { get; set; }
        public string Idioma { get; set; }

        public string TituloPlanillaImport { get; set; }
        public string TituloCabezal { get; set; }
        public string NuestraReferencia {get; set;}
        public string NuestraReferenciaValue { get; set; }
        
        public string SuReferencia {get; set;}
        public string SuReferenciaValue {get; set;}

        public string GiradoTexto { get; set; }
        public string GiradoNombre { get; set; }
        public string GiradoDireccion1 { get; set; }
        public string GiradoDireccion2 { get; set; }
        public string GiradoPais { get; set; }

        public string CobradorReferencia { get; set; }
        public string CobradorReferenciaValue { get; set; }
        public string NombreCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string CiudadCliente { get; set; }
        public string CasillaInternaBanco { get; set; }
        public string CasillaPostal { get; set; }
        public string Tratamiento1 { get; set; }

        public string Direccion1 { get; set; }
        public string SubDireccion1 { get; set; }
        public string Paso1 { get; set; }

        public string Direccion2 { get; set; }
        public string SubDireccion2 { get; set; }
        public string Paso2 { get; set; }
        public string Pais2 { get; set; }

        public string Girado { get; set; }
        public string Girador { get; set; }

        public string Parrafo1 { get; set; }

        public string DetalleAceptacion { get; set; }
        public IList<Letra> Letras { get; set; }
        public IList<ComercioInvisible> LineasComercioInvisible { get; set; }
        public IList<Remesa> LineasRemesa { get; set; }
        public IList<CargoAbono> LineasCargoAbono { get; set; }
        public IList<Divisa> LineasDivisa { get; set; }
        public IList<ComisionGasto> LineasComisionGasto { get; set; }
        public IList<DebitoCredito> LineasDebitoCredito { get; set; }
        public IList<Transferencia> LineasTransferencia { get; set; }
        public IList<Impuesto> LineasImpuesto { get; set; }
        public IList<Retorno> LineasRetorno { get; set; }
        public IList<MontoRegistro> LineasMontoRegistro { get; set; }
        public IList<DocumentoEmbarque> LineasDocumentosEmbarque { get; set; }
        public IList<Embarque> LineasEmbarque { get; set; }
        public IList<CompraVenta> LineasCompra { get; set; }
        public IList<CompraVenta> LineasVenta { get; set; }
        public string LetraNro { get; set; }
        public string Monto { get; set; }
        public string Vence { get; set; }
        public string TotalComisionGasto { get; set; }
        public string MonedaTotal { get; set; }
        public string MonedaCapital { get; set; }
        public string MontoCapital { get; set; }
        public string PiePagina { get; set; }
        public string TotalImpuestos { get; set; }
        public string MonedaTotalMontos { get; set; }
        public string TotalMontos { get; set; }

        public DocumentoReporteModel(string html)
        {
            this.Html = html;
            this.Letras = new List<Letra>();
            this.LineasComercioInvisible = new List<ComercioInvisible>();
            this.LineasRemesa = new List<Remesa>();
            this.LineasCargoAbono = new List<CargoAbono>();
            this.LineasDivisa = new List<Divisa>();
            this.LineasComisionGasto = new List<ComisionGasto>();
            this.LineasDebitoCredito = new List<DebitoCredito>();
            this.LineasTransferencia = new List<Transferencia>();
            this.LineasImpuesto = new List<Impuesto>();
            this.LineasRetorno = new List<Retorno>();
            this.LineasMontoRegistro = new List<MontoRegistro>();
            this.LineasDocumentosEmbarque = new List<DocumentoEmbarque>();
            this.LineasEmbarque = new List<Embarque>();
            this.LineasCompra = new List<CompraVenta>();
            this.LineasVenta = new List<CompraVenta>();
        }
    }
}
