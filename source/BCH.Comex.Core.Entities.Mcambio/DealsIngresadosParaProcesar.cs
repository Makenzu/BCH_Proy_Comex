using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCH.Comex.Core.Entities.Mcambio
{
    public class DealsIngresadosParaProcesar
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
        public string txtcodigoOrigenCarga { get; set; }
        public string stFechaTransaccion { get; set; }
        public string stFechaValuta1 { get; set; }
        public string stCodigoEstadoContable { get; set; }
        public string stultimoNumeroTransitoria { get; set; }
        public string stultimoNumeroContingente { get; set; }
        public string stCodigoBancoCentral { get; set; }
        public string stPrecioCliente { get; set; }
        public string stMontoBancoRecibe { get; set; }
        public string stDelta { get; set; }
        public string stMontoClienteRecibe { get; set; }
        public string stPrecioPoolMoneda2 { get; set; }
        public string stSaldoMoneda1 { get; set; }
        public string stSaldoMoneda2 { get; set; }
        public string stTipoTransaccion { get; set; }
        public int intCodTipoTransaccion { get; set; }
        public string AbrevTipTrans { get; set; }
        public string monedaMuestra { get; set; }
        //Usar en creación Deal
        public int CodMoneda { get; set; }
        public string moneda1_Ingresada { get; set; }
        public string moneda2_Ingresada { get; set; }
        public Nullable<double> TipoCambio_Ingresado { get; set; }
        public Nullable<double> Paridad_Ingresada { get; set; }
        public Nullable<double> Monto1_Ingresado { get; set; }
        public Nullable<double> Monto2_Ingresado { get; set; }
        public Nullable<double> MontoEn_Ingresado { get; set; }
        public Nullable<double> precio_final { get; set; }
        public Nullable<double> monto_segunda_moneda { get; set; }
        public string txtCbPais1_Ingresado { get; set; }
        public string txtCbPais2_Ingresado { get; set; }

        public string txtCbMoneda1_Ingresado { get; set; }
        public string txtCbMoneda2_Ingresado { get; set; }
        public Nullable<double> DeltaOrig { get; set; }
        public string TipoIngreso { get; set; }
        public string stCalcSldMnd2 { get; set; }
    }
}
