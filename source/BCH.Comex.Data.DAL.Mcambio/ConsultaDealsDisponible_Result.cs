//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BCH.Comex.Data.DAL.Mcambio
{
    using System;
    
    public partial class ConsultaDealsDisponible_Result
    {
        public string rutCliente { get; set; }
        public string nombreCliente { get; set; }
        public Nullable<int> numeroDeal { get; set; }
        public string moneda1 { get; set; }
        public string moneda2 { get; set; }
        public Nullable<double> precioPoolMoneda1 { get; set; }
        public Nullable<double> precioPoolMoneda2 { get; set; }
        public Nullable<double> precioCliente { get; set; }
        public Nullable<System.DateTime> fechaValuta1 { get; set; }
        public Nullable<System.DateTime> fechaValuta2 { get; set; }
        public string codigoBancoCentral { get; set; }
        public Nullable<double> montoBancoRecibe { get; set; }
        public Nullable<double> montoClienteRecibe { get; set; }
        public Nullable<double> delta { get; set; }
        public Nullable<System.DateTime> fechaTransaccion { get; set; }
        public Nullable<double> tipoCambioPizarra { get; set; }
        public string tipoTransaccion { get; set; }
        public Nullable<int> codigoEstadoDeal { get; set; }
        public Nullable<int> codigoFormaPago1 { get; set; }
        public Nullable<int> codigoFormaPago2 { get; set; }
        public Nullable<int> codigoEstadoPago { get; set; }
        public Nullable<int> codigoReferenciaComex { get; set; }
        public Nullable<int> codigoOrigenCarga { get; set; }
        public Nullable<int> codigoTipoOperacion { get; set; }
        public Nullable<int> codigoEstadoContable { get; set; }
        public string ultimoNumeroTransitoria { get; set; }
        public string ultimoNumeroContingente { get; set; }
    }
}
