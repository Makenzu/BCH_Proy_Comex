using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.SWSE;
using BCH.Comex.Data.DAL.Swift;
using BCH.Comex.Data.DAL.Swift.DTO;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.SWG3
{
    public class EnvioSwiftServiceMT300 : EnvioSwiftService
    {
        private readonly UnitOfWorkSwift unitOfWork;

        public EnvioSwiftServiceMT300()
        {
            this.unitOfWork = new UnitOfWorkSwift();
        }

        public bool GuardarMensajeSwift(int idMensaje, string rutUsuario, int centroCosto, string moneda, double monto, string beneficiario, string textoMensajeSwift, out int? idMensajeNuevo, char modoEnvio)
        {
            try
            {
                idMensajeNuevo = null;
                using (Tracer tracer = new Tracer("Inicio grabar mensaje swift"))
                {
                    try
                    {
                        Swi200ServiceMT300 service = new Swi200ServiceMT300();

                        int rutEnFormatoSwift = int.Parse(rutUsuario);

                        int vista = 0;
                        if (idMensaje <= 0)
                        {
                            vista = 1234; //vista para nuevo mensaje
                            SWI300.Swi300Service serviceCorrelativo = new SWI300.Swi300Service();
                            idMensajeNuevo = serviceCorrelativo.GetCorrelativo();
                            idMensaje = idMensajeNuevo.Value;
                        }

                        var msg = new MensajeSwiftSwi200(idMensaje, rutEnFormatoSwift, centroCosto, moneda, monto, modoEnvio, textoMensajeSwift);
                        msg.Beneficiario = beneficiario;
                        return service.IngresaModificaMensajeSwift(vista, msg);
                    }
                    catch (Exception ex)
                    {
                        tracer.TraceException("Alerta, problemas al grabar el mensaje swift ", ex);
                        throw;
                    }
                }
            }
            catch (Exception x)
            {
                throw new Exception("GuardarMensajeSwift: " + x.Message);
            }

        }

        public string ObtieneMontoFormatoSwift(string moneda, decimal monto)
        {
            var registro = unitOfWork.MonedaRepository.Get(m => m.cod_moneda_sw == moneda && m.uso_moneda_banco == "S").FirstOrDefault();
            string separadorDecimal = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            string montoAux = "";

            if (registro != null)
                if (registro.decimales != null)
                    if (registro.decimales >= 0) {
                        montoAux = monto.ToString("0.".PadRight(2 + Convert.ToInt32(registro.decimales), '#'));
                        if (montoAux.Contains(separadorDecimal))
                        {
                            if (separadorDecimal == ",")
                            {
                                return montoAux;
                            }
                            else { 
                                montoAux.Replace('.', ',');
                                return montoAux;
                            }
                        }
                        else
                        {
                            return monto.ToString("0") + ",";
                        }
                    }
                    else {
                        throw new Exception("La cantidad de decimales para el Código de moneda es menor a cero " + moneda + " cant:" + registro.decimales);
                    }

                else
                    throw new Exception("No se pudo obtener la cantidad de decimales para el Código de moneda " + moneda);

            else
                throw new Exception("Código de moneda no existe " + moneda);

        }

    }
}
