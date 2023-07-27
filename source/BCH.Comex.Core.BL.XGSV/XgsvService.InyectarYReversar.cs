using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XGSV.Modulos;
/*using BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain;*/
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Supervisor;
using BCH.Comex.Data.DAL.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BCH.Comex.Core.BL.XGSV
{
    partial class XgsvService
    {
        private const string NombreWorksheetParaInyectar = "Para inyectar";
        private const string NombreWorksheetParaReversar = "Para reversar";

        public enum OperacionInyeccion : byte
        {
            Inyectar = 0,
            Reversar = 1
        }

        #region Metodos Públicos

        public IList<AbonoCargoResultDTO> GetAbonosCargos(int centroCostoSupervisor, string id_Super, OperacionInyeccion op)
        {
            var especialistas = uow.SceRepository.sce_usr_s10_MS(centroCostoSupervisor.ToString(), id_Super);
            return GetAbonosCargos(especialistas, op);
        }

        public List<UI_Message> InyectarOReversarCargoAbono(AbonoCargoResultDTO ac, OperacionInyeccion op, string rutEspecialista)
        {
            using (Tracer tracer = new Tracer("InyectarOReversarCargoAbono"))
            {
                var mjs = ValidarEstadoOperacion(ac, op);

                if (mjs.Count() > 0)
                {
                    return mjs;
                }

                if (ac.TipoCuentaAsEnum == AbonoCargoResultDTO.TipoCuenta.Citi)
                {
                    return InyectarOReversarCuentaCITI(ac, op);
                }
                else if (ac.TipoCuentaAsEnum == AbonoCargoResultDTO.TipoCuenta.Bch)
                {
                    return InyectarOReversarCuentaBCH(ac, op, rutEspecialista);
                }
                else
                {
                    tracer.TraceError("El tipo de cuenta no es CITI ni BCH");
                    throw new ArgumentException("El tipo de cuenta no es CITI ni BCH");
                }
            }
        }

        public MemoryStream GetExcelDeAbonosYCargos(IList<AbonoCargoResultDTO> acsParaInyectar, IList<AbonoCargoResultDTO> acsParaReversar)
        {
            using (Tracer tracer = new Tracer("GetExcelDeAbonosYCargos"))
            {
                MemoryStream stream = new MemoryStream();
                using (SLDocument doc = new SLDocument())
                {
                    doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, NombreWorksheetParaInyectar);
                    doc.AddWorksheet(NombreWorksheetParaReversar);

                    doc.SelectWorksheet(NombreWorksheetParaInyectar);
                    CargarHojaExcelConAbonosYCargos(acsParaInyectar, doc, "inyectar");
                    doc.SelectWorksheet(NombreWorksheetParaReversar);
                    CargarHojaExcelConAbonosYCargos(acsParaReversar, doc, "reversar");
                    doc.SelectWorksheet(NombreWorksheetParaInyectar);

                    doc.SaveAs(stream);
                }

                stream.Position = 0; //importante, dejar el stream pronto para leer;
                return stream;
            }
        }

        /// <summary>
        /// Se actuliza el registro de la cache según lo que esta en la base de datos.
        /// </summary>
        /// <param name="abonosCargos"></param>
        public void UpdateCargoYAbono(ref AbonoCargoResultDTO abonosCargos)
        {
            using (Tracer tracer = new Tracer("UpdateCargoYAbonoDTO"))
            {
                var resultado = uow.SceRepository.GetUpdateCargoYAbono(abonosCargos.codcct, abonosCargos.codpro, abonosCargos.codesp, abonosCargos.codofi, abonosCargos.codope, abonosCargos.nroimp.ToString(), abonosCargos.trx_id, abonosCargos.nrorpt.ToString());
                if (resultado != null)
                {
                    if (!string.IsNullOrEmpty(resultado.transaction_id))
                    {
                        tracer.TraceInformation("Operación: {0}-{1}-{2}-{3}-{4}", resultado.codcct, resultado.codpro, resultado.codesp, resultado.codofi, resultado.codope);
                        tracer.TraceInformation("Se actualiza TRXID: original {0}, nuevo {1}", abonosCargos.trx_id, resultado.transaction_id);
                        abonosCargos.trx_id = resultado.transaction_id;
                    }
                }
            }
        }

        #endregion

        #region Metodos Privados

        private IList<AbonoCargoResultDTO> GetAbonosCargos(IList<sce_usr> especialistas, OperacionInyeccion op)
        {
            using (Tracer tracer = new Tracer("GetAbonosCargos"))
            {
                var result = new List<AbonoCargoResultDTO>();
                if (op == OperacionInyeccion.Reversar)
                {
                    foreach (var item in especialistas)
                    {
                        var lista = uow.SceRepository.pro_sce_rev_abocar_s03_MS(item.cent_costo, item.id_especia);
                        try
                        {
                            //Se buscan los registros que tengan el TrxId Duplicado
                            var duplicados = lista.GroupBy(c => c.trx_id, (key, g) => { return g.Count() > 1 ? key : null; }).ToList();
                            //Se marcan los registros repetidos
                            lista.Where(c => duplicados.Contains(c.trx_id)).ToList()
                                 .ForEach(c =>
                                 {
                                     c.TrxIdRepetido = true;
                                     tracer.TraceWarning("El txrId " + c.trx_id + " esta repetido y se reporto para la operacion " + c.NroOperacionSinRaya);
                                 });
                        }
                        catch (Exception ex)
                        {
                            tracer.TraceError("Alerta, se presento un error al intentar buscar duplicados para el especialista " + item.cent_costo + item.id_especia, ex);
                        }
                        result.AddRange(lista);
                    }
                }
                else
                {
                    foreach (var item in especialistas)
                    {
                        result.AddRange(uow.SceRepository.pro_sce_abo_car_s03_MS(item.cent_costo, item.id_especia));
                    }
                }

                short idAux = 1;
                foreach (AbonoCargoResultDTO ac in result)
                {
                    ac.PosEnLista = idAux++;
                    ac.IdAux = Guid.NewGuid();
                }

                return result;
            }
        }

        private List<UI_Message> InyectarOReversarCuentaCITI(AbonoCargoResultDTO ac, OperacionInyeccion op)
        {
            using (Tracer target = new Tracer("InyectarOReversarCuentaCITI"))
            {
                target.TraceInformation(string.Format("NroOpe: {0}, Cta: {1}, NroImp: {2}, NroReporte: {3}, Monto: {4}", ac.NroOperacionSinRaya, ac.numcct, ac.nroimp, ac.nrorpt, ac.mtomcd));
                List<UI_Message> messages = new List<UI_Message>();

                try
                {
                    IList<tbl_sce_fts> resultado = uow.SceRepository.pro_sce_abo_car_s02_MS(ac);

                    if (resultado != null && resultado.Count > 0)
                    {
                        tbl_sce_fts l = resultado.Last();

                        string transferAmount, equivalentAmount = String.Empty;

                        if (l.currency_code == 1)
                        {
                            transferAmount = (l.transfer_amount.HasValue ? l.transfer_amount.Value.ToString("00000000000000000") : "00000000000000000");
                        }
                        else
                        {
                            transferAmount = (l.transfer_amount.HasValue ? l.transfer_amount.Value.FormatAsCD(17) : "00000000000000000");
                        }

                        if (l.currency_code_equi == 1)
                        {

                            equivalentAmount = (l.equivalent_amount.HasValue ? l.equivalent_amount.Value.ToString("00000000000000000") : "00000000000000000");
                        }
                        else
                        {
                            equivalentAmount = (l.equivalent_amount.HasValue ? l.equivalent_amount.Value.FormatAsCD(17) : "00000000000000000");
                        }

                        string infoAdicional = l.record_type + l.branch_number + l.contract_reference.ToString("0000000000") +
                            l.ordering_customer.PadRight(11, '0') +
                            l.act_hist_indicator + (l.input_date1.HasValue ? l.input_date1.Value.ToString("000000") : "000000") +
                            l.receiver.Trim().PadRight(11, '0') +
                            (l.credit_entry_date.HasValue ? l.credit_entry_date.Value.ToString("000000") : "000000") +
                            (l.order_cost_account.HasValue ? l.order_cost_account.Value.ToString("00000000000") : "00000000000") +
                            (l.receiver_account.HasValue ? l.receiver_account.Value.ToString("00000000000") : "00000000000") +
                            l.authorization_stat + l.transac_type_code + l.exe_type_code_tran + l.product_type +
                            l.swf_currency_code.PadRight(4) +
                            (l.currency_code.HasValue ? l.currency_code.Value.ToString("000") : "000") +
                            (l.charges_debit.HasValue ? l.charges_debit.Value.ToString("00000000000") : "00000000000") +
                            transferAmount +
                            l.sign_transfer +
                            (l.legal_vehicle_code.HasValue ? l.legal_vehicle_code.Value.ToString("00") : "00") +
                            l.debit_value_date + l.data_entry_date + l.credit_value_date +
                            l.texto.PadRight(24) +
                            l.status.PadRight(1) +
                            l.by_order_of.PadRight(35) +
                            l.beneficiary.PadRight(35) +
                            l.last_inp_date +
                            l.transfer_charged.PadRight(1) + l.operator_id.PadRight(3) +
                            l.input_date2 +
                            (l.input_time.HasValue ? l.input_time.Value.ToString("0000000") : "0000000") +
                            l.authorizer_id.PadRight(3) +
                            (l.authorizer_time.HasValue ? l.authorizer_time.Value.ToString("0000000") : "0000000") +
                            (l.order_cust_account.HasValue ? l.order_cust_account.Value.ToString("00000000000") : "00000000000") +
                            (l.input_date3.HasValue ? l.input_date3.Value.ToString("00000") : "00000") +
                            l.alpha_number.PadRight(12) + l.swf_currency_equi.PadRight(4) +
                            (l.currency_code_equi.HasValue ? l.currency_code_equi.Value.ToString("000") : "000") +
                            equivalentAmount +
                            l.signo_equivalent.PadRight(1) +
                            (l.fcy_exchange_rate.HasValue ? l.fcy_exchange_rate.Value.ToString("00000000000") : "00000000000") +
                            (l.receiver_account2.HasValue ? l.receiver_account2.Value.ToString("00000000000") : "00000000000") +
                            (l.input_date4.HasValue ? l.input_date4.Value.ToString("00000") : "00000") +
                            l.short_benefic_bank.PadRight(32) + l.alpha_reference.PadRight(24) + l.lto_indicator.PadRight(1) +
                            l.benefi_account_num.PadRight(16) +
                            (l.commission_rate.HasValue ? l.commission_rate.Value.ToString("000000000") : "000000000") +
                            (l.commission_amount.HasValue ? l.commission_amount.Value.ToString("00000000000000000") : "00000000000000000") +
                            l.sing_commssion.PadRight(1) +
                            (l.courtage_rate.HasValue ? l.courtage_rate.Value.ToString("00000") : "00000") +
                            (l.courtage_amount.HasValue ? l.courtage_amount.Value.ToString("00000000000000000") : "00000000000000000") +
                            l.sign_courtage.PadRight(1) +
                            (l.postage_amount.HasValue ? l.postage_amount.Value.ToString("00000000000000000") : "00000000000000000") +
                            l.sign_postage.PadRight(1) + l.swf_currency_charg.PadRight(4) +
                            (l.currency_code_chan.HasValue ? l.currency_code_chan.Value.ToString("000") : "000") +
                            (l.chrg_base_nbr.HasValue ? l.chrg_base_nbr.Value.ToString("00000000000") : "00000000000") +
                            l.short_charges_acou.PadRight(32) + l.reference_number.PadRight(16) +
                            (l.central_bank_code.HasValue ? l.central_bank_code.Value.ToString("00000000000") : "00000000000") +
                            l.num_order_customer + l.num_receiver + l.num_beneficia_bank +
                            l.num_beneficiary + l.num_reason + l.num_bank_to_bank + l.num_charges +
                            (l.total_number.HasValue ? l.total_number.Value.ToString("00") : "00") +
                            l.text_line + l.text_line2 + l.text_line3;

                        if (op == OperacionInyeccion.Reversar)
                        {
                            ReversarOperacion(ac, messages);
                        }
                        else //Inyectar
                        {
                            string respuestaTransaccion = String.Empty;
                            try
                            {
                                if (ac.cod_dh == "H") //Abono
                                {
                                    respuestaTransaccion = XCFTServices.AbonoCuentaCosmos(ac, DateTime.Now, infoAdicional);
                                }
                                else //Cargo
                                {
                                    respuestaTransaccion = XCFTServices.CargoCuentaCosmos(ac, DateTime.Now, infoAdicional);
                                }

                                if (ActualizaEncabezados(ac, op, respuestaTransaccion, null, messages))
                                {
                                    //todo OK
                                    messages.Add(new UI_Message
                                    {
                                        Title = "Operación exitosa",
                                        Text = string.Format("El {0} se inyectó correctamente", ac.cod_dh == "H" ? "abono" : "cargo"),
                                        Type = TipoMensaje.Correcto,
                                        AutoClose = true
                                    });
                                }
                            }
                            catch (Exception ex)
                            {
                                //aunque me haya dado error, llamo a este ws para poder reintentar eventaualmente para este abono/cargo,
                                //sino el ws siempre me va a devolver "Error en ID Transaccion Duplicada"
                                IncrementarIdTransaccion(ac, OperacionInyeccion.Inyectar, messages);
                                
                                messages.Add(new UI_Message
                                {
                                    Title = "Error al inyectar",
                                    Text = "Ocurrió un error al inyectar la operación. Detalles: " + ex.Message,
                                    Type = TipoMensaje.Error
                                });
                                target.TraceException("Error al inyectar - Ocurrió un error al inyectar la operación.", ex);
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("El cargo/abono no existe en la BD");
                    }
                }
                catch (Exception ex)
                {
                    target.TraceException("Error al inyectar o reversar", ex);
                    messages.Add(new UI_Message
                    {
                        Title = "Errro al inyectar o reversar",
                        Text = "Ocurrió un error al inyectar o reversar la operación. Detalles: " + ex.Message,
                        Type = TipoMensaje.Error
                    });
                }

                return messages;
            }
        }

        private List<UI_Message> InyectarOReversarCuentaBCH(AbonoCargoResultDTO ac, OperacionInyeccion op, string rutEspecialista)
        {
            using (Tracer tracer = new Tracer())
            {
                List<UI_Message> messages = new List<UI_Message>();

                try
                {
                    if (String.IsNullOrEmpty(ac.trx_id))
                    {
                        GenerarPrimerTrxId(ac, messages);
                    }

                    string respuestaTransaccion = String.Empty;
                    if (op == OperacionInyeccion.Inyectar)
                    {

                        if (ac.cod_dh == "H") //Abono
                        {
                            if (String.IsNullOrEmpty(ac.numcct) || (ac.nemcta != "CC$" && ac.nemcta != "CCE"))
                            {
                                messages.Add(new UI_Message
                                {
                                    Title = "Operación no válida",
                                    Text = "La cuenta BCH que se quiere abonar no es de los nemónicos que corresponden o el número de cuenta está vacío",
                                    Type = TipoMensaje.Error
                                });

                                tracer.TraceError("Operación no válida - La cuenta BCH que se quiere abonar no es de los nemónicos que corresponden o el número de cuenta está vacío");
                                return messages;
                            }

                            try
                            {
                                respuestaTransaccion = XCFTServices.AbonoCuentaCosmos(ac, DateTime.Now, String.Empty);
                            }
                            catch (Exception ex)
                            {
                                //aunque me haya dado error, llamo a este ws para poder reinetntar eventaualmente para este abono/cargo,
                                //sino el ws siempre me va a devolver "Error en ID Transaccion Duplicada"
                                IncrementarIdTransaccion(ac, OperacionInyeccion.Inyectar, messages);

                                messages.Add(new UI_Message
                                {
                                    Title = "Error al inyectar",
                                    Text = "Ocurrió un error al inyectar la operación. Detalles: " + ex.Message,
                                    Type = TipoMensaje.Error
                                });
                                tracer.TraceException("Error al inyectar - Ocurrió un error al inyectar la operación.", ex);
                                return messages;
                            }

                            if (!ActualizaEncabezados(ac, op, respuestaTransaccion, rutEspecialista, messages))
                            {
                                //el ws retornó OK pero no pude actualizar el registro en la BD, así que tengo que reversar la inyección
                                messages.Add(new UI_Message
                                {
                                    Title = "Error al actualizar BD",
                                    Text = "Intentando reversar la operación ...",
                                    Type = TipoMensaje.Informacion
                                });
                                tracer.TraceError("Error al actualizar BD - Intentando reversar la operación ...");
                                ReversarOperacion(ac, messages);
                            }
                            else
                            {
                                //todo OK
                                messages.Add(new UI_Message
                                {
                                    Title = "Operación exitosa",
                                    Text = string.Format("El abono se inyectó correctamente"),
                                    Type = TipoMensaje.Correcto,
                                    AutoClose = true
                                });
                            }
                        }
                        else //Cargo
                        {
                            if (ac.codmon == 11)
                            {
                                if (!ValidarSaldoOperacion(ac, messages))
                                {
                                    return messages;
                                }
                            }

                            try
                            {
                                respuestaTransaccion = XCFTServices.CargoCuentaCosmos(ac, DateTime.Now, String.Empty);
                            }
                            catch (Exception ex)
                            {
                                IncrementarIdTransaccion(ac, OperacionInyeccion.Inyectar, messages);

                                messages.Add(new UI_Message
                                {
                                    Title = "Error al inyectar",
                                    Text = "Ocurrió un error al inyectar la operación. Detalles: " + ex.Message,
                                    Type = TipoMensaje.Error
                                });
                                tracer.TraceException("Error al inyectar - Ocurrió un error al inyectar la operación.", ex);
                                return messages;
                            }

                            if (ActualizaEncabezados(ac, op, respuestaTransaccion, rutEspecialista, messages))
                            {
                                messages.Add(new UI_Message
                                {
                                    Title = "Operación exitosa",
                                    Text = "El cargo se inyectó correctamente",
                                    Type = TipoMensaje.Correcto,
                                    AutoClose = true
                                });
                            }
                        }
                    }
                    else // Reversa
                    {
                        ReversarOperacion(ac, messages);
                    }
                }
                catch (Exception ex) {
                    tracer.TraceException("Error al inyectar o reversar", ex);
                    messages.Add(new UI_Message
                    {
                        Title = "Error al inyectar o reversar",
                        Text = "Ocurrió un error al inyectar o reversar la operación. Detalles: " + ex.Message,
                        Type = TipoMensaje.Error
                    });
                }

                return messages;
            }
        }

        private bool ReversarOperacion(AbonoCargoResultDTO ac, List<UI_Message> messages)
        {
            using (Tracer tracer = new Tracer())
            {
                if (ac.cod_dh == "H") //Abono
                {
                    if (!ValidarSaldoOperacion(ac, messages))
                    {
                        return false;
                    }
                }

                IncrementarIdTransaccion(ac, OperacionInyeccion.Reversar, messages);
                string idTransaccionNegocio = LeerTrxReversa(ac);
                string respuestaTransaccion = XCFTServices.ReversaCosmos(idTransaccionNegocio, ac, DateTime.Now);

                if (!String.IsNullOrEmpty(respuestaTransaccion))
                {
                    try
                    {
                        //El orden de los factores si altera el producto. Tanto Citi como Bch actualizan la contabilidad y aumenta el id de transaccion
                        //pero lo hacen en el orden inverso!!!

                        bool result2ndaOperacion = false;
                        if (ac.TipoCuentaAsEnum == AbonoCargoResultDTO.TipoCuenta.Citi)
                        {
                            if (ActualizaEncabezados(ac, OperacionInyeccion.Reversar, respuestaTransaccion, null, messages))
                            {
                                result2ndaOperacion = IncrementarIdTransaccion(ac, OperacionInyeccion.Inyectar, messages);
                            }
                            else return false;
                        }
                        else if (ac.TipoCuentaAsEnum == AbonoCargoResultDTO.TipoCuenta.Bch)
                        {
                            if (IncrementarIdTransaccion(ac, OperacionInyeccion.Inyectar, messages))
                            {
                                result2ndaOperacion = ActualizaEncabezados(ac, OperacionInyeccion.Reversar, respuestaTransaccion, null, messages);
                            }
                            else return false;
                        }

                        if (result2ndaOperacion)
                        {
                            messages.Add(new UI_Message
                            {
                                Title = "Operación exitosa",
                                Text = string.Format("El {0} se reversó correctamente", ac.cod_dh == "H" ? "abono" : "cargo"),
                                Type = TipoMensaje.Correcto,
                                AutoClose = true
                            });

                            return true;
                        }
                        else return false;
                    }
                    catch (Exception ex)
                    {
                        messages.Add(new UI_Message
                        {
                            Title = "Error en la operación",
                            Text = "Ocurrió un error al intentar actualizar la contabilidad de la reversa.",
                            Type = TipoMensaje.Critical
                        });
                        tracer.TraceException("Error en la operación - Ocurrió un error al intentar actualizar la contabilidad de la reversa.", ex);
                        return false;
                    }
                }
                else
                {
                    messages.Add(new UI_Message
                    {
                        Title = "Error al reversar",
                        Text = "La operación no se pudo reversar",
                        Type = TipoMensaje.Error
                    });
                    tracer.TraceError("Error al reversar - La operación no se pudo reversar");

                    return false;
                }
            }
        }

        private bool ValidarSaldoOperacion(AbonoCargoResultDTO ac, List<UI_Message> messages)
        {
            if (ac.codmon == 1 || ac.codmon == 11) //CLP o USD
            {
                decimal saldo = XCFTServices.ConsultaSaldoCuenta(ac.numcct, ac.trx_id);
                if (saldo < ac.mtomcd)
                {
                    if (ac.codmon == 11)
                    {
                        messages.Add(new UI_Message
                        {
                            Title = "Saldo insuficiente",
                            Text = string.Format(
                                "El saldo disponible de la Cuenta Corriente {1} es de US$ {0}, menor al  monto a cargar de US$ {2}.",
                                saldo.ToString("0,0.00"),
                                ac.numcct,
                                ac.mtomcd.ToString("0,0.00")
                            ),
                            Type = TipoMensaje.Error
                        });
                    }
                    else if (ac.codmon == 1)
                    {
                        messages.Add(new UI_Message
                        {
                            Title = "Saldo insuficiente",
                            Text = string.Format(
                                "El saldo disponible de la Cuenta Corriente {1} es de $ {0}, menor al  monto a cargar de $ {2}.",
                                saldo.ToString("0,0"),
                                ac.numcct,
                                ac.mtomcd.ToString("0,0")
                            ),
                            Type = TipoMensaje.Error
                        });
                    }


                    return false;
                }
                else return true;
            }
            else return true; //no se validan otras monedas
        }

        //Esta funcion actualiza la contabilidad o "encabezados", setea enLinea en 0 o 1 de la tabla sce_mcd dependiendo de inyeccion o reversa
        private bool ActualizaEncabezados(AbonoCargoResultDTO ac, OperacionInyeccion op, string respuestaTransaccion, string rutEspecialista, List<UI_Message> messages)
        {
            bool result = true;
            using (Tracer target = new Tracer("ActualizaEncabezados"))
            {
                target.TraceInformation(string.Format("NroOpe: {0}, Cta: {1}, NroImp: {2}, NroReporte: {3}, Monto: {4}", ac.NroOperacionSinRaya, ac.numcct, ac.nroimp, ac.nrorpt, ac.mtomcd));
                try
                {
                    string codigo, mensaje; //no los utilizo

                    if (op == OperacionInyeccion.Inyectar)
                    {
                        uow.SceRepository.pro_sce_abo_car_u01_MS(ac, respuestaTransaccion, rutEspecialista, out codigo, out mensaje);
                    }
                    else
                    {
                        //if (ac.TipoCuentaAsEnum == AbonoCargoResultDTO.TipoCuenta.Bch)
                        //{
                        //    result = uow.SceRepository.Sce_mcd_u70(ac.nrorpt, ac.fecmov, ac.nroimp, ac.enlinea, rutEspecialista);
                        //}
                        //else
                        //{
                            uow.SceRepository.pro_sce_rev_abocar_u02_MS(ac, out codigo, out mensaje);
                        //}
                    }

                    if (!result)
                    {
                        string detalle = "Error al modificar 'enlinea' de sce_mcd";
                        messages.Add(new UI_Message
                        {
                            Title = "Error en la operación",
                            Text = string.Format(
                                "Ocurrió un error al intentar actualizar la contabilidad de la {0}. Detalles: {1}",
                                (op == OperacionInyeccion.Inyectar ? "inyección" : "reversa"),
                                detalle),
                            Type = TipoMensaje.Critical
                        });

                        target.TraceError(detalle);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    messages.Add(new UI_Message
                    {
                        Title = "Error en la operación",
                        Text = string.Format(
                            "Ocurrió un error al intentar actualizar la contabilidad de la {0}. Detalles: {1}",
                            (op == OperacionInyeccion.Inyectar ? "inyección" : "reversa"),
                            ex.Message),
                        Type = TipoMensaje.Critical
                    });

                    target.TraceError("Error en la operacion", ex);
                    return false;
                }
            }
        }

        private void GenerarPrimerTrxId(AbonoCargoResultDTO ac, List<UI_Message> messages)
        {
            using (Tracer trace = new Tracer("GenerarPrimerTrxId"))
            {
                trace.TraceInformation(string.Format("NroOpe: {0}, Cta: {1}, NroImp: {2}, NroReporte: {3}, Monto: {4}", ac.NroOperacionSinRaya, ac.numcct, ac.nroimp, ac.nrorpt, ac.mtomcd));
                try
                {
                    string trxId = MODGCVD.GeneraTRXID(ac.NroOperacionSinRaya, uow, messages);
                    if (!String.IsNullOrEmpty(trxId))
                    {
                        //Ademas de la TrxId que me generé, tengo que obtener dos digitos mas correlativos para esta operacion
                        string correlativoRelacionado = uow.SceRepository.pro_sce_sup_s04(ac);
                        ac.trx_id = trxId + correlativoRelacionado;
                        trace.TraceInformation("TrxId generado: " + ac.trx_id); 
                        //ahora si que tengo la trxid completa, la actualizo en tabla tbl_sce_relacion_ft:
                        uow.SceRepository.pro_sce_relacion_i01_MS(ac);
                        trace.TraceInformation("pro_sce_relacion_i01_MS ejecutado"); 
                    }
                }
                catch (Exception ex)
                {
                    trace.TraceError("Alerta: ", ex);
                    throw;
                }
            }
        }

        //aumenta en 1 el trxid o trxIdReversa respectivamente despendiendo del tipo de operacion
        private bool IncrementarIdTransaccion(AbonoCargoResultDTO ac, OperacionInyeccion op, List<UI_Message> messages)
        {
            using (Tracer trace = new Tracer("IncrementarIdTransaccion"))
            {
                try
                {
                    string codigo, mensaje;
                    if (op == OperacionInyeccion.Inyectar)
                    {
                        trace.TraceInformation("Se incrementará para inyección. TrxId antes: " + ac.trx_id);
                        uow.SceRepository.pro_sce_abo_car_u02_MS(ac, out codigo, out mensaje);
                    }
                    else
                    {
                        trace.TraceInformation("Se incrementará para reversa. TrxId antes: " + ac.trx_id);
                        uow.SceRepository.pro_sce_rev_abocar_u01_MS(ac, out codigo, out mensaje);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    messages.Add(new UI_Message
                    {
                        Title = "Error al incrementar el Id de Transacción",
                        Text = "Ocurrió un error al inyectar la operación. Detalles: " + ex.Message,
                        Type = TipoMensaje.Error
                    });

                    trace.TraceException("Error al incrementar el Id de Transacción", ex);
                    return false;
                }
            }
        }

        private string LeerTrxReversa(AbonoCargoResultDTO abonoCargo)
        {
            using (Tracer trace = new Tracer("LeerTrxReversa"))
            {
                try
                {
                    trace.TraceInformation("Se leerá la TrxReversa para TrxId: " + abonoCargo.trx_id);
                    string result = uow.SceRepository.pro_sce_rev_abocar_s02_MS_OpcionIdTrx(abonoCargo);
                    trace.TraceInformation("TrxId reversa leído: " + result);
                    return result;
                }
                catch (Exception ex)
                {
                    trace.TraceException("Alerta: ", ex);
                    throw;
                }
            }
        }

        private void CargarHojaExcelConAbonosYCargos(IList<AbonoCargoResultDTO> acs, SLDocument doc, string verbo)
        {
            SLStyle styleTitulo = doc.CreateStyle();
            styleTitulo.Font.Bold = true;
            styleTitulo.Font.FontSize = 15;
            styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleSubtitulo = doc.CreateStyle();
            styleSubtitulo.Font.Bold = true;
            styleSubtitulo.Font.FontSize = 13;
            styleSubtitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleEncabezado = doc.CreateStyle();
            styleEncabezado.Font.Bold = true;
            styleEncabezado.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleBold = doc.CreateStyle();
            styleBold.Font.Bold = true;
            styleBold.SetHorizontalAlignment(HorizontalAlignmentValues.Left);

            SLStyle styleFechaBold = doc.CreateStyle();
            styleFechaBold.Font.Bold = true;
            styleFechaBold.FormatCode = "dd-mm-yyyy";

            SLStyle styleFecha = doc.CreateStyle();
            styleFecha.FormatCode = "dd-mm-yyyy";

            SLStyle styleDecimalMonedaExtranjera = doc.CreateStyle();
            styleDecimalMonedaExtranjera.FormatCode = "#,##0.00";

            SLStyle styleDecimalPesosChilenos = doc.CreateStyle();
            styleDecimalPesosChilenos.FormatCode = "#,##0";


            doc.MergeWorksheetCells("A1", "I1");
            doc.MergeWorksheetCells("A2", "I2");
            doc.MergeWorksheetCells("A4", "B4");

            doc.SetCellValue("A1", "Exportación Consulta de Operaciones");
            doc.SetCellStyle("A1", styleTitulo);
            doc.SetCellValue("A2", "Cargos y Abonos para " + verbo);
            doc.SetCellStyle("A2", styleSubtitulo);

            doc.SetCellValue("A4", "Fecha Exportación:");
            doc.SetCellStyle("A4", styleBold);

            doc.SetCellValue("C4", DateTime.Now);
            doc.SetCellStyle("C4", styleFechaBold);

            int colIndex = 1;
            int rowIndex = 6;
            doc.SetCellValue(rowIndex, colIndex++, "Cta. Cte.");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo Cta.");
            doc.SetCellValue(rowIndex, colIndex++, "Nombre");
            doc.SetCellValue(rowIndex, colIndex++, "Abono / Cargo");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda");
            doc.SetCellValue(rowIndex, colIndex++, "Monto");
            doc.SetCellValue(rowIndex, colIndex++, "Nro. Reporte");
            doc.SetCellValue(rowIndex, colIndex++, "Fecha Movimiento");
            doc.SetCellValue(rowIndex, colIndex++, "Transaction ID");
            doc.SetCellStyle("A6", "I6", styleEncabezado);

            foreach (AbonoCargoResultDTO ac in acs)
            {
                colIndex = 1;
                rowIndex++;

                doc.SetCellValueNumeric(rowIndex, colIndex++, ac.codcct);
                doc.SetCellValueNumeric(rowIndex, colIndex++, ac.tip_cta);
                doc.SetCellValue(rowIndex, colIndex++, ac.nomcli);
                doc.SetCellValue(rowIndex, colIndex++, (ac.cod_dh == "H" ? "Abono" : "Cargo"));
                doc.SetCellValue(rowIndex, colIndex++, ac.moneda);
                doc.SetCellValue(rowIndex, colIndex, ac.mtomcd);
                doc.SetCellStyle(rowIndex, colIndex++, (ac.moneda == "CLP" ? styleDecimalPesosChilenos : styleDecimalMonedaExtranjera));
                doc.SetCellValue(rowIndex, colIndex++, ac.nrorpt);
                doc.SetCellValue(rowIndex, colIndex, ac.fecmov);
                doc.SetCellStyle(rowIndex, colIndex++, styleFecha);
                doc.SetCellValue(rowIndex, colIndex++, ac.trx_id);
            }

            doc.AutoFitColumn("A", "I");
        }
        
        private List<UI_Message> ValidarEstadoOperacion(AbonoCargoResultDTO ac, OperacionInyeccion op)
        {
            using (Tracer tracer = new Tracer("ValidaEstadoOperacion"))
            {
                List<UI_Message> mensajes = new List<UI_Message>();
                var operacion = uow.SceRepository.GetContabilidadPorNroReporte(ac.codcct, ac.codpro, ac.codesp, ac.codofi, ac.codope, ac.nrorpt);
                var estadoAbonoCargo = uow.SceRepository.sce_mcd_s77_MS(ac.codcct, ac.codpro, ac.codesp, ac.codofi, ac.codope, ac.nemcta, ac.nrorpt, ac.nroimp);
                // preguntamos si es cargo o abono para mostrar el mensaje correctamente
                string cod_dh = ac.cod_dh == "D" ? "cargo" : "abono";
                if (operacion != null && estadoAbonoCargo != null)
                {
                    // discriminamos si es inyeccion o reversa
                    if (op == OperacionInyeccion.Inyectar)
                    {

                        if(estadoAbonoCargo.enlinea == 1)
                        {
                            mensajes.Add(new UI_Message()
                            {
                                Title = "Error al inyectar",
                                Text = string.Format("No es posible realizar la inyección, ya que el {0} ya ha sido inyectado.", cod_dh),
                                Type = TipoMensaje.Error
                            });
                            return mensajes;
                        }

                        if (operacion.estado == 9)
                        {
                            mensajes.Add(new UI_Message()
                            {
                                Title = "Error al inyectar",
                                Text = "No es posible realizar la inyección, ya que la operación ha sido anulada.",
                                Type = TipoMensaje.Error
                            });
                            return mensajes;
                        }
                    }
                    else // reversa
                    {
                        if (estadoAbonoCargo.enlinea == 0)
                        {
                            mensajes.Add(new UI_Message()
                            {
                                Title = "Error al inyectar",
                                Text = string.Format("No es posible realizar la reversa, ya que el {0} ya ha sido reversado.", cod_dh),
                                Type = TipoMensaje.Error
                            });
                            return mensajes;
                        }
                    }
                }
                else
                {
                    tracer.TraceError("sce_mch_s15_MS o sce_mcd_s77_MS no retorno información");
                }

                return mensajes;
            }
        }
        #endregion
    }
}
