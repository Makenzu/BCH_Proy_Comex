using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Utils;
using System;
using System.Collections.Generic;

namespace BCH.Comex.Data.DAL.Services
{
    public static class XCFTServices
    {
        public static decimal ConsultaSaldoCuenta(string cuentaCte, string trxId)
        {
            XCFT.ConsultaSaldoCuenta.datosHeaderRequest header = new XCFT.ConsultaSaldoCuenta.datosHeaderRequest();
            header.consumidor = new XCFT.ConsultaSaldoCuenta.datosConsumidor()
            {
                idApp = "Citidocs1.0",
                usuario = "EJB-COMEX",
            };
            header.transaccion = new XCFT.ConsultaSaldoCuenta.datosTransaccion()
            {
                internalCode = "1",
                canal = "COMEX00001",
                sucursal = "000",
                idTransaccionNegocio = trxId,
                fechaHora = DateTime.Now,
            };
            XCFT.ConsultaSaldoCuenta.cuerpo cuerpo = new XCFT.ConsultaSaldoCuenta.cuerpo();
            cuerpo.numeroCuenta = cuentaCte; //"00000000000");
            XCFT.ConsultaSaldoCuenta.ConsultasSaldoCuentaClient client = new XCFT.ConsultaSaldoCuenta.ConsultasSaldoCuentaClient();
            XCFT.ConsultaSaldoCuenta.cuerpo1 cuerpo1; 
            XCFT.ConsultaSaldoCuenta.datosHeaderResponse response = client.ConsultasSaldoCuenta(header, cuerpo, out cuerpo1);
            return Format.StringToDecimal(cuerpo1.Cuenta.SaldoDisponible.Replace(".", ","));
        }

        public static string ConsultaCuentaCorriente(string ctacte_party)
        {
            XCFT.ConsultaCuentaCorriente.respConsultaCuentaCorriente respConsultaCtaCte;
            XCFT.ConsultaCuentaCorriente.datosHeaderResponse resp;
            var datosHeader = new XCFT.ConsultaCuentaCorriente.datosHeaderRequest();
            var reqConsultaCtaCte = new XCFT.ConsultaCuentaCorriente.reqConsultaCuentaCorriente();
            
            reqConsultaCtaCte.cuenta = ctacte_party;

            XCFT.ConsultaCuentaCorriente.ConsultaCuentaCorrienteClient client = new XCFT.ConsultaCuentaCorriente.ConsultaCuentaCorrienteClient();
            resp = client.ConsultaCuentaCorriente(datosHeader, reqConsultaCtaCte, out respConsultaCtaCte);

            return respConsultaCtaCte.Respuesta.TokenCuenta;
        }

        public static string ReversaCosmos(string idTransaccionNegocio, AbonoCargoResultDTO ac, DateTime fecha)
        {
            Console.WriteLine("Entrando en ReversaCosmos");
            using (Tracer target = new Tracer("ReversaCosmos"))
            {
                target.TraceInformation(string.Format("txid: {0}, txidParaReversar: {1}", idTransaccionNegocio, ac.trx_id));
                Console.WriteLine(string.Format("txid: {0}, txidParaReversar: {1}", idTransaccionNegocio, ac.trx_id));
                try
                {
                    XCFT.ReversaCosmos.ListadataCartola cartola = new XCFT.ReversaCosmos.ListadataCartola();
                    cartola.dataCartola = new XCFT.ReversaCosmos.dataCartola();

                    if (ac.data5 != null && !String.IsNullOrEmpty(ac.data5.Trim()))
                    {
                        cartola.dataCartola.NombreCampo = "DATA5";
                        cartola.dataCartola.ValorCampo = ac.data5.Trim();
                    }

                    string horario = (fecha.Hour < 12 ? "01" : "02");

                    XCFT.ReversaCosmos.datosHeaderRequest headerRequest = new XCFT.ReversaCosmos.datosHeaderRequest()
                    {
                        consumidor = new XCFT.ReversaCosmos.datosConsumidor()
                        {
                            idApp = "Citidocs1.0",
                            usuario = "EJB-COMEX",
                        },
                        transaccion = new XCFT.ReversaCosmos.datosTransaccion()
                        {
                            internalCode = "1",
                            canal = "COMEX00001",
                            sucursal = "000",
                            idTransaccionNegocio = idTransaccionNegocio,
                            fechaHora = fecha,
                        }
                    };


                    XCFT.ReversaCosmos.reqReversaCosmos reqReversaCosmos = new XCFT.ReversaCosmos.reqReversaCosmos()
                    {
                        DatosCabeceraNegocio = new XCFT.ReversaCosmos.DatosCabeceraNegocio()
                        {
                            rutOperadora = "12345678",
                            bancoDestino = "001",
                            bancoOrigen = "001",
                            oficinaOrigenTx = "001",
                            canalOrigenTx = "COMEX00001",
                            fechaContable = fecha.Date,
                            fechaHoraCorrienteTx = fecha,
                            Horario = horario,
                            txid = idTransaccionNegocio,
                            txidParaReversar = ac.trx_id,
                        },
                        DatosNegocio = new XCFT.ReversaCosmos.DatosNegocio()
                        {
                            ListadataCartola = cartola,
                            Cuenta = ac.numcct.Trim(),
                            codigoCartolaCosmos = "0",
                            Monto = ac.mtomcd.ToString("#0.00").Replace(",", "."),
                            Moneda = ac.moneda.Trim(),
                            dataMT942 = new XCFT.ReversaCosmos.dataMT942()
                            {
                                mt942Campo61 = new XCFT.ReversaCosmos.mt942Campo61()
                                {
                                    TxType = "TRF",
                                    ReferenciaCliente = "NONREF",
                                    FechaValuta = fecha.Date.ToString("yyMMdd"),
                                    ReferenciaBCH = ac.data5.Trim(),
                                    MarkDebitCredit = (ac.cod_dh == "D" ? "RD" : "RC")
                                },
                                mt942CodigoTxCosmos = ac.cod_trx_cosmos.Trim(),
                                mt942TipoProductoCosmos = "FT"
                            }
                        }
                    };

                    XCFT.ReversaCosmos.respReversaCosmos respuesta = null;


                    XCFT.ReversaCosmos.ReversaCosmosClient client = new XCFT.ReversaCosmos.ReversaCosmosClient();
                    client.ReversaCosmos(headerRequest, reqReversaCosmos, out respuesta);

                    return respuesta.Respuesta.descripcionRetorno;
                }
                catch (Exception e)
                {
                    target.TraceError(e.ToString());
                    Console.WriteLine("Error en ws: " + e.Message);
                    throw;
                }
            }
        }

        //ws 31
        public static string AbonoCuentaCosmos(AbonoCargoResultDTO ac, DateTime fecha, string infoAdicional)
        {
            Console.WriteLine("Entrando en AbonoCuentaCosmos");
            using (Tracer target = new Tracer("AbonoCuentaCosmos"))
            {
                target.TraceInformation(string.Format("txid: {0}", ac.trx_id));
                Console.WriteLine(string.Format("txid: {0}", ac.trx_id));
                try
                {
                    string horario = (fecha.Hour < 12 ? "01" : "02");
                    List<XCFT.AbonoCuentaCosmos.DataCartola> listDataCartola = new List<XCFT.AbonoCuentaCosmos.DataCartola>();

                    if (ac.data1 != null && !String.IsNullOrEmpty(ac.data1.Trim()))
                    {
                        listDataCartola.Add(new XCFT.AbonoCuentaCosmos.DataCartola() { NombreCampo = "DATA1", ValorCampo = ac.data1.Trim() });
                    }

                    if (ac.data2 != null && !String.IsNullOrEmpty(ac.data2.Trim()))
                    {
                        listDataCartola.Add(new XCFT.AbonoCuentaCosmos.DataCartola() { NombreCampo = "DATA2", ValorCampo = ac.data2.Trim() });
                    }

                    if (ac.data3 != null && !String.IsNullOrEmpty(ac.data3.Trim()))
                    {
                        listDataCartola.Add(new XCFT.AbonoCuentaCosmos.DataCartola() { NombreCampo = "DATA3", ValorCampo = ac.data3.Trim() });
                    }

                    if (ac.data4 != null && !String.IsNullOrEmpty(ac.data4.Trim()))
                    {
                        listDataCartola.Add(new XCFT.AbonoCuentaCosmos.DataCartola() { NombreCampo = "DATA4", ValorCampo = ac.data4.Trim() });
                    }

                    if (ac.data5 != null && !String.IsNullOrEmpty(ac.data5.Trim()))
                    {
                        listDataCartola.Add(new XCFT.AbonoCuentaCosmos.DataCartola() { NombreCampo = "DATA5", ValorCampo = ac.data5.Trim() });
                    }


                    XCFT.AbonoCuentaCosmos.datosHeaderRequest headerRequest = new XCFT.AbonoCuentaCosmos.datosHeaderRequest()
                    {
                        consumidor = new XCFT.AbonoCuentaCosmos.datosConsumidor()
                        {
                            idApp = "Citidocs1.0", //Esto tenia la app vieja, habrá que pedir un nuevo id?
                            usuario = "EJB-COMEX",
                        },
                        transaccion = new XCFT.AbonoCuentaCosmos.datosTransaccion()
                        {
                            internalCode = "",
                            canal = "COMEX00001",
                            sucursal = "000",
                            idTransaccionNegocio = ac.trx_id.Trim(),
                            fechaHora = fecha
                        },
                    };

                    XCFT.AbonoCuentaCosmos.reqAbonoCuentaCosmosCodFC reqAbono = new XCFT.AbonoCuentaCosmos.reqAbonoCuentaCosmosCodFC()
                    {
                        DatosCabeceraNegocio = new XCFT.AbonoCuentaCosmos.DatosCabeceraNegocio()
                        {
                            fechaContable = fecha.Date,
                            fechaHoraCorrienteTx = fecha,
                            Horario = horario,
                            rutOperadora = "12345678",
                            bancoDestino = "001",
                            bancoOrigen = "001",
                            oficinaOrigenTx = "000",
                            canalOrigenTx = "COMEX00001",
                            txid = ac.trx_id.Trim(),
                        },
                        DatosNegocio = new XCFT.AbonoCuentaCosmos.DatosNegocio()
                        {
                            ListadataCartola = listDataCartola.ToArray(),
                            Cuenta = ac.numcct.Trim(),
                            CodProductoFC = ac.cod_prod.Trim(),
                            CodTransaccionFC = ac.cod_trx_fc.Trim(),
                            CodExtendidoFC = ac.cod_ext.Trim(),
                            Monto = ac.mtomcd.ToString("0.##").Replace(",", "."),
                            Moneda = ac.moneda,
                        },
                        dataMT942 = new XCFT.AbonoCuentaCosmos.dataMT942()
                        {
                            mt942CodigoTxCosmos = ac.cod_trx_cosmos,
                            mt942InfoAdicional2 = infoAdicional,
                            mt942TipoProductoCosmos = "FT",
                            Mt942Campo61 = new XCFT.AbonoCuentaCosmos.Mt942Campo61()
                            {
                                FechaValuta = fecha.Date.ToString("yyMMdd"),
                                ReferenciaBCH = ac.data5.Trim(),
                                TxType = "TRF",
                                ReferenciaCliente = "NONREF"
                            }
                        }
                    };

                    XCFT.AbonoCuentaCosmos.respAbonoCuentaCosmosCodFC respuesta;
                    XCFT.AbonoCuentaCosmos.AbonoCuentaCosmosCodFCClient client = new XCFT.AbonoCuentaCosmos.AbonoCuentaCosmosCodFCClient();
                    client.AbonoCuentaCosmosCodFC(headerRequest, reqAbono, out respuesta);

                    return respuesta.Respuesta.descripcionRetorno;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error en el ws: " + e.Message);
                    target.TraceException("Alerta en el WS:", e);
                    throw;
                }
            }
        }

        //ws 32
        public static string CargoCuentaCosmos(AbonoCargoResultDTO ac, DateTime fecha, string infoAdicional)
        {
            Console.Write("Entrando en CargoCuentaCosmos");
            using (Tracer target = new Tracer("CargoCuentaCosmos"))
            {
                target.TraceInformation(string.Format("txid: {0}", ac.trx_id));
                Console.WriteLine(string.Format("txid: {0}", ac.trx_id));
                try
                {
                    string horario = (fecha.Hour < 12 ? "01" : "02");

                    List<XCFT.CargoCuentaCosmos.DataCartola> listDataCartola = new List<XCFT.CargoCuentaCosmos.DataCartola>();

                    if (ac.data1 != null && !String.IsNullOrEmpty(ac.data1.Trim()))
                    {
                        listDataCartola.Add(new XCFT.CargoCuentaCosmos.DataCartola() { NombreCampo = "DATA1", ValorCampo = ac.data1.Trim() });
                    }

                    if (ac.data2 != null && !String.IsNullOrEmpty(ac.data2.Trim()))
                    {
                        listDataCartola.Add(new XCFT.CargoCuentaCosmos.DataCartola() { NombreCampo = "DATA2", ValorCampo = ac.data2.Trim() });
                    }

                    if (ac.data3 != null && !String.IsNullOrEmpty(ac.data3.Trim()))
                    {
                        listDataCartola.Add(new XCFT.CargoCuentaCosmos.DataCartola() { NombreCampo = "DATA3", ValorCampo = ac.data3.Trim() });
                    }

                    if (ac.data4 != null && !String.IsNullOrEmpty(ac.data4.Trim()))
                    {
                        listDataCartola.Add(new XCFT.CargoCuentaCosmos.DataCartola() { NombreCampo = "DATA4", ValorCampo = ac.data4.Trim() });
                    }

                    if (ac.data5 != null && !String.IsNullOrEmpty(ac.data5.Trim()))
                    {
                        listDataCartola.Add(new XCFT.CargoCuentaCosmos.DataCartola() { NombreCampo = "DATA5", ValorCampo = ac.data5.Trim() });
                    }


                    XCFT.CargoCuentaCosmos.datosHeaderRequest headerRequest = new XCFT.CargoCuentaCosmos.datosHeaderRequest()
                    {
                        consumidor = new XCFT.CargoCuentaCosmos.datosConsumidor()
                        {
                            idApp = "Citidocs1.0", //Esto tenia la app vieja, habrá que pedir un nuevo id?
                            usuario = "EJB-COMEX",
                        },
                        transaccion = new XCFT.CargoCuentaCosmos.datosTransaccion()
                        {
                            internalCode = "",
                            canal = "COMEX00001",
                            sucursal = "000",
                            idTransaccionNegocio = ac.trx_id.Trim(),
                            fechaHora = fecha
                        },
                    };

                    XCFT.CargoCuentaCosmos.reqCargoCuentaCosmosCodFC reqAbono = new XCFT.CargoCuentaCosmos.reqCargoCuentaCosmosCodFC()
                    {
                        DatosCabeceraNegocio = new XCFT.CargoCuentaCosmos.DatosCabeceraNegocio()
                        {
                            fechaContable = fecha.Date,
                            fechaHoraCorrienteTx = fecha,
                            Horario = horario,
                            rutOperadora = "12345678",
                            bancoDestino = "001",
                            bancoOrigen = "001",
                            oficinaOrigenTx = "000",
                            canalOrigenTx = "COMEX00001",
                            txid = ac.trx_id.Trim(),
                        },
                        DatosNegocio = new XCFT.CargoCuentaCosmos.DatosNegocio()
                        {
                            ListadataCartola = listDataCartola.ToArray(),
                            Cuenta = ac.numcct.Trim(),
                            CodProductoFC = ac.cod_prod.Trim(),
                            CodTransaccionFC = ac.cod_trx_fc.Trim(),
                            CodExtendidoFC = ac.cod_ext.Trim(),
                            Monto = ac.mtomcd.ToString("0.##").Replace(",", "."),
                            Moneda = ac.moneda,
                        },
                        dataMT942 = new XCFT.CargoCuentaCosmos.dataMT942()
                        {
                            mt942CodigoTxCosmos = ac.cod_trx_cosmos,
                            mt942InfoAdicional2 = infoAdicional,
                            mt942TipoProductoCosmos = "FT",
                            mt942Campo61 = new XCFT.CargoCuentaCosmos.mt942Campo61
                            {
                                FechaValuta = fecha.Date.ToString("yyMMdd"),
                                ReferenciaBCH = ac.data5.Trim(),
                                TxType = "TRF",
                                ReferenciaCliente = "NONREF"
                            }
                        }
                    };

                    XCFT.CargoCuentaCosmos.respCargoCuentaCosmosCodFC respuesta;
                    XCFT.CargoCuentaCosmos.CargoCuentaCosmosCodFCClient client = new XCFT.CargoCuentaCosmos.CargoCuentaCosmosCodFCClient();
                    client.CargoCuentaCosmosCodFC(headerRequest, reqAbono, out respuesta);

                    return respuesta.Respuesta.descripcionRetorno;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error en el ws: " + e.Message);
                    target.TraceException("Alerta en el WS:", e);
                    throw;
                }
            }
        }
    }
}
