
using BCH.Comex.Core.BL.CONTROLINTEGRAL.Forms;
using BCH.Comex.Core.Entities.Custodia;
using BCH.Comex.Core.Entities.Custodia.ControlIntegral.DataTypes;
using BCH.Comex.Data.DAL.Custodia;
using BCH.Comex.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace BCH.Comex.Core.BL.CONTROLINTEGRAL
{
    public class ControlIntegralService
    {
        private UnitOfWorkCustodia unitOfWork;
        public const string AVOID_SEARCH = "AVOID_SEARCH";
        public const string LIMIT_EXCEEDED = "LIMIT_EXCEEDED";
        public const string MSG_ERROR = "Ha ocurrido un error, contacte al administrador: ";



        public ControlIntegralService()
        {
            this.unitOfWork = new UnitOfWorkCustodia();
        }


        #region "FORM LOAD"

        public InitializationObject Inicializar()
        {
            InitializationObject initObj = new InitializationObject();
            Form_Load(initObj);
            return initObj;
        }

        public void Form_Load(InitializationObject initObj)
        {
            // Modo = True
            initObj.ModFunc.Paso_Cliente = false;
            initObj.ModFunc.Paso_Cuentas = false;
            initObj.ModFunc.Paso_Contratos = false;
            initObj.ModFunc.Paso_Recurrencia = false;
            initObj.ModFunc.Paso_Mensajes = false;

        }

        #endregion


        #region "MIFT - DATOS  ORDENANTE"
        public IList<cambios_mift_cliente_00_MS_Result> DatosOrdenante(long? rut, string cuenta)
        {
            return unitOfWork.CambiosRepository.cambios_mift_cliente_00_MS(1, rut, cuenta);
        }

        #endregion

        #region "MIFT - GRABAR LOG"
        public int Cambios_mift_log_insert_2_MS(string Fecha_hora, string Fecha, string Rut, string RutDv, string Cuenta, string NombreClte,
                                                string Segmento, string Ejecutivo, string Moneda, long Monto, string CuentaBnf, string NombreBnf,
                                                string BancoInt, string BancoBnf, string Ind_Con_Otros, string Ind_Con_Mift, string Ind_Con_Fax,
                                               string Ind_Con_Citi, string Ind_Con_Fax_NY, string Ind_Con_Mail, string Txt_Con_Otros, string Resultado,
                                                string Mensaje, string Usuario)
        {
            return unitOfWork.CambiosRepository.cambios_mift_log_insert_2_MS(Fecha_hora, Fecha, Rut, RutDv, Cuenta, NombreClte, Segmento, Ejecutivo, Moneda, Monto, CuentaBnf,
                                                                          NombreBnf, BancoInt, BancoBnf, Ind_Con_Otros, Ind_Con_Mift, Ind_Con_Fax, Ind_Con_Citi, Ind_Con_Fax_NY,
                                                                          Ind_Con_Mail, Txt_Con_Otros, Resultado, Mensaje, Usuario);
        }

        #endregion

        #region "MIFT - Lista FLX TARIFAS"

        public object ListaFlxTarifas(string cuenta, string desMoneda, decimal monto)
        {
            object data;
            try
            {
                if (cuenta == AVOID_SEARCH)
                {
                    data = new List<cambios_mift_tarifas_01_MS_Result>();
                }
                else
                {
                    int cantidad = cambios_mift_tarifas_01_MS(cuenta, desMoneda, monto).ToList().Count();
                    if (cantidad == 0)
                    {
                        data = new List<cambios_mift_tarifas_01_MS_Result>();
                    }
                    else
                    {
                        data = cambios_mift_tarifas_01_MS(cuenta, desMoneda, monto).
                            Select(i => new cambios_mift_tarifas_01_MS_Result
                            {
                                Rut = i.Rut,
                                Tarifas = i.Tarifas,
                                Mon = i.Mon,
                                Valor = i.Valor,
                                Observacion = i.Observacion,
                                Impto = Format.FormatDblFromDB(i.Impto, "#,###,###,###,##0"),
                                Tipo = i.Tipo
                            }).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }
        public IList<cambios_mift_tarifas_01_MS_Result> cambios_mift_tarifas_01_MS(string cuenta, string desMoneda, decimal monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_tarifas_01_MS(cuenta, desMoneda, monto);
        }


        public IList<cambios_mift_tarifas_01_MS_Result> ListaFlxTarifasVerificaTipo(string cuenta, string desMoneda, decimal monto)
        {
            IList<cambios_mift_tarifas_01_MS_Result> data;
            try
            {
                if (cuenta == AVOID_SEARCH)
                {
                    data = new List<cambios_mift_tarifas_01_MS_Result>();
                }
                else
                {
                    int cantidad = cambios_mift_tarifas_01_MS(cuenta, desMoneda, monto).ToList().Count();
                    if (cantidad == 0)
                    {
                        data = new List<cambios_mift_tarifas_01_MS_Result>();
                    }
                    else
                    {
                        data = cambios_mift_tarifas_01_MS(cuenta, desMoneda, monto).
                            Select(i => new cambios_mift_tarifas_01_MS_Result
                            {
                                //Rut = i.Rut,
                                //Tarifas = i.Tarifas,
                                //Mon = i.Mon,
                                //Valor = i.Valor,
                                //Observacion = i.Observacion,
                                //Impto = Format.FormatDblFromDB(i.Impto, "#,###,###,###,##0"),
                                Tipo = i.Tipo
                            }).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                data = null;
            }
            return data;
        }

        public abstract class Observacion
        {
            public string Observaciones;
        }

        //public object ListaFlxPizarra(string cuenta)
        //{          
        //    IList<string> Observaciones = cambios_mift_tarifas_obs_01_MS(cuenta);
        // //   var result = from sec in Observaciones select new[] { Observaciones };

        //    return Observaciones;

        //}
        #endregion

        #region "MIFT - OBSERVACIONES"

        public object ListaFlxPizarra(string cuenta)
        {
            object data;
            try
            {
                if (cuenta == AVOID_SEARCH)
                {
                    data = new List<cambios_mift_tarifas_obs_01_MS_Result>();
                }
                else
                {
                    data = cambios_mift_tarifas_obs_01_MS(cuenta).
                        Select(i => new cambios_mift_tarifas_obs_01_MS_Result
                        {
                            Observaciones = i.Observaciones
                        }).ToList();

                }
            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }

        public IList<cambios_mift_tarifas_obs_01_MS_Result> cambios_mift_tarifas_obs_01_MS(string accCuenta)
        {
            return unitOfWork.CambiosRepository.cambios_mift_tarifas_obs_01_MS(accCuenta);
        }

        #endregion

        #region "MIFT - OBSERVACIONES CLICK"

        public object ListaFlxPizarra_Obs1_Click(string Tipo, string Subtipo, string Moneda, decimal Monto)
        {
            object data;
            try
            {
                if (Tipo == AVOID_SEARCH)
                {
                    data = new List<cambios_mift_tarifas_pizarra_01_MS_Result>();
                }
                else
                {
                    data = cambios_mift_tarifas_pizarra_01_MS(Tipo, Subtipo, Moneda, Monto).
                        Select(i => new cambios_mift_tarifas_pizarra_01_MS_Result
                        {
                            Pizarra = i.Pizarra,
                            USD_Min = i.USD_Min,
                            USD_Max = i.USD_Max,
                            Tasa = i.Tasa,
                            Minimo = i.Minimo,
                            Maximo = i.Maximo
                        }).ToList();

                }
            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }

        public IList<cambios_mift_tarifas_pizarra_01_MS_Result> cambios_mift_tarifas_pizarra_01_MS(string Tipo, string Subtipo, string Moneda, decimal Monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_tarifas_pizarra_01_MS(Tipo, Subtipo, Moneda, Monto).ToList();
        }


        public object ListaFlxPizarra_Obs2_Click(string Tipo, string Subtipo, string Moneda, decimal Monto)
        {
            object data;
            try
            {
                if (Tipo == AVOID_SEARCH)
                {
                    data = new List<cambios_mift_tarifas_pizarra_02_MS_Result>();
                }
                else
                {
                    data = cambios_mift_tarifas_pizarra_02_MS(Tipo, Subtipo, Moneda, Monto).
                        Select(i => new cambios_mift_tarifas_pizarra_02_MS_Result
                        {
                            Pizarra = i.Pizarra,
                            Valor = i.Valor

                        }).ToList();

                }
            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }

        public IList<cambios_mift_tarifas_pizarra_02_MS_Result> cambios_mift_tarifas_pizarra_02_MS(string Tipo, string Subtipo, string Moneda, decimal Monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_tarifas_pizarra_02_MS(Tipo, Subtipo, Moneda, Monto).ToList();
        }

        #endregion

        #region "MIFT - Lista FLX DOCUMENTOS DUPLICADOS"

        public object ListaFlxCitidocDPL(InitializationObject initObj, string accCuenta, decimal monto)
        {
            object data;
            try
            {
                if (accCuenta == "")
                {
                    data = new List<cambios_mift_citidoc_duplicados_01_MS_Result>();
                }
                else
                {
                    int cantidad = CountDocumentosDuplicados(accCuenta, monto);


                    if (cantidad == 0)
                    {
                        data = new List<cambios_mift_citidoc_duplicados_01_MS_Result>();
                    }
                    else
                    {
                        initObj.ModFunc.sIndDpl = "SI";
                        data = cambios_mift_citidoc_duplicados_01_MS(accCuenta, monto).
                            Select(i => new cambios_mift_citidoc_duplicados_01_MS_Result
                            {
                                hora = i.hora,
                                Folder = i.Folder,
                                document_name = i.document_name,
                                referencia = i.referencia,
                                Monto = i.Monto,
                                Document_Type = i.Document_Type

                            }).ToList();
                        //if (contador == 0)
                        //         initObj.ModFunc.sTxtDpl = "Con Duplicidad, ultima carga: " + hora = i.hora +  " Hrs.";
                    }
                }
            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }
        public int CountDocumentosDuplicados(string accCuenta, decimal monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_citidoc_duplicados_01_MS(accCuenta, monto).ToList().Count();
        }
        public IList<cambios_mift_citidoc_duplicados_01_MS_Result> cambios_mift_citidoc_duplicados_01_MS(string accCuenta, decimal monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_citidoc_duplicados_01_MS(accCuenta, monto);
        }

        public IList<string> cambios_mift_citidoc_duplicados_hora_01_MS()
        {
            return unitOfWork.CambiosRepository.cambios_mift_citidoc_duplicados_hora_01_MS();
        }




        #endregion

        #region "MIFT - DATOS ORDENANTE"
        public IList<cambios_mift_cliente_00_MS_Result> cambios_mift_cliente_00_MS(byte? opcion, long? rut, string cuenta)
        {
            return unitOfWork.CambiosRepository.cambios_mift_cliente_00_MS(opcion, rut, cuenta);
        }

        #endregion

        #region "MIFT - CONTRATOS"

        public IList<cambios_mift_contratos_00_MS_Result> cambios_mift_contratos_00_MS(byte? Opcion, long? Rut, string Cuenta)
        {
            return unitOfWork.CambiosRepository.cambios_mift_contratos_00_MS(Opcion, Rut, Cuenta);
        }

        #endregion

        #region "MIFT - DETALLE"

        public object ListaFlxDetalle(short Ord_rut_aux, string Ord_cuenta, string Bnf_swfbco, string Bnf_cuenta, string Bnf_nombre, string Moneda, decimal Monto)
        {
            object data;
            try
            {
                if (Ord_cuenta == "")
                {
                    data = new List<cambios_mift_recurrencia_01b_MS_Result>();
                }
                else
                {
                    int cantidad = CountDetalle(Ord_rut_aux, Ord_cuenta, Bnf_swfbco, Bnf_cuenta, Bnf_nombre, Moneda, Monto);


                    if (cantidad == 0)
                    {
                        data = new List<cambios_mift_recurrencia_01b_MS_Result>();
                    }
                    else
                    {

                        data = cambios_mift_recurrencia_01b_MS(Ord_rut_aux, Ord_cuenta, Bnf_swfbco, Bnf_cuenta, Bnf_nombre, Moneda, Monto).
                            Select(i => new cambios_mift_recurrencia_01b_MS_Result
                            {
                                cantidad = i.cantidad,
                                bnf_cuenta = i.bnf_cuenta,
                                bnf_nombre = i.bnf_nombre,
                                bnf_swbcoint = i.bnf_swbcoint,
                                bnf_swfbco = i.bnf_swfbco,
                                bnf_nombco = i.bnf_nombco

                            }).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }

        public int CountDetalle(short Ord_rut_aux, string Ord_cuenta, string Bnf_swfbco, string Bnf_cuenta, string Bnf_nombre, string Moneda, decimal Monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_recurrencia_01b_MS(Ord_rut_aux, Ord_cuenta, Bnf_swfbco, Bnf_cuenta, Bnf_nombre, Moneda, Monto).ToList().Count();
        }

        public IList<cambios_mift_recurrencia_01b_MS_Result> cambios_mift_recurrencia_01b_MS(short Ord_rut_aux, string Ord_cuenta, string Bnf_swfbco, string Bnf_cuenta, string Bnf_nombre, string Moneda, decimal Monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_recurrencia_01b_MS(Ord_rut_aux, Ord_cuenta, Bnf_swfbco, Bnf_cuenta, Bnf_nombre, Moneda, Monto);
        }
        public IList<short?> cambios_mift_recurrencia_02_MS(byte? Opcion, string ord_Rut, string ord_cuenta, string bnf_swfbco, string bnf_cuenta, string Desmoneda, decimal? Monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_recurrencia_02_MS(Opcion, ord_Rut, ord_cuenta, bnf_swfbco, bnf_cuenta, Desmoneda, Monto).ToList();
        }
                

        #endregion

        #region "MIFT - CARGAR MENSAJES"

        public IList<cambios_mift_callfax_00_MS_Result> cambios_mift_callfax_00_MS(byte Opcion, long Rut, string Cuenta)
        {
            return unitOfWork.CambiosRepository.cambios_mift_callfax_00_MS(Opcion, Rut, Cuenta);
        }

        public IList<string> cambios_mift_mensajes_01_MS(byte Opcion, string Cuenta)
        {
            return unitOfWork.CambiosRepository.cambios_mift_mensajes_01_MS(Opcion, Cuenta);
        }



        public IList<cambios_mift_mesa_cvd_00a_MS_ResultDTO> cambios_mift_mesa_cvd_00a_MS(short Opcion, long Rut, string Cuenta, string Moneda, decimal Monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_mesa_cvd_00a_MS(Opcion, Rut, Cuenta, Moneda, Monto);
        }



        #endregion

        #region "MIFT - DETALLE MESA"

        public object ListaFlxDetalleMesa(short Opcion, long RutCliente, string Cuenta, string Moneda, decimal Monto)
        {
            object data;
            try
            {
                if (Cuenta == "")
                {
                    data = new List<cambios_mift_mesa_cvd_00b_MS_Result>();
                }
                else
                {
                    int cantidad = CantidadDetalleMesa(Opcion, RutCliente, Cuenta, Moneda, Monto);


                    if (cantidad == 0)
                    {
                        data = new List<cambios_mift_mesa_cvd_00b_MS_Result>();
                    }
                    else
                    {

                        data = cambios_mift_recurrencia_01b_MS(Opcion, RutCliente, Cuenta, Moneda, Monto).
                            Select(i => new cambios_mift_mesa_cvd_00b_MS_Result
                            {
                                chk = i.chk,
                                fecha = i.fecha,
                                monto = i.monto,
                                precio = i.precio,
                                mdn_com = i.mdn_com,
                                mnd_vta = i.mnd_vta,
                                tipo = i.tipo,
                                mtoPesos = i.mtoPesos,
                                mtoUSD = i.mtoUSD,
                                FVALOR = i.FVALOR,
                                idComex = i.idComex,
                                estado = i.estado

                            }).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }

        public int CantidadDetalleMesa(short Opcion, long RutCliente, string Cuenta, string Moneda, decimal Monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_mesa_cvd_00b_MS(Opcion, RutCliente, Cuenta, Moneda, Monto).Count();
        }
        public IList<cambios_mift_mesa_cvd_00b_MS_Result> cambios_mift_recurrencia_01b_MS(short Opcion, long RutCliente, string Cuenta, string Moneda, decimal Monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_mesa_cvd_00b_MS(Opcion, RutCliente, Cuenta, Moneda, Monto);
        }

        #endregion

        #region "EREPARO"
        //public void BotonGenerar(InitializationObject initObj, string Datos0, string Datos1, string DocName, string MailEjecutivo, string Usuario)
        //{
        //    frmEReparo.BotonGenerar(initObj, unitOfWork, Datos0, Datos1, DocName, MailEjecutivo, Usuario);
        //}

        public void BotonGenerar(InitializationObject initObj, string Datos0, string Datos1, string DocName, string MailEjecutivo, string Usuario)
        {
            frmEReparo.BotonGenerar(initObj, unitOfWork, Datos0, Datos1, DocName, MailEjecutivo, Usuario);
        }

        public void InitComboDocName(InitializationObject initObj)
        {
            frmEReparo.LlenaComboDocName(initObj, unitOfWork);
        }
        public void LimpiarEReparo(InitializationObject initObj)
        {
            frmEReparo.Limpiar(initObj);
        }

        #endregion


        #region "EMPRESA"
        public object BuscarOrdenantes(byte Opcion, string Nombre)
        {
            object data;
            try
            {
                if (Nombre == AVOID_SEARCH)
                {
                    data = new List<cambios_mift_cliente_01_MS_Result>();
                }
                else
                {
                    int cantidad = CountOrdenantes(Opcion, Nombre);
                 
                    if (cantidad == 0)
                    {
                        data = new List<cambios_mift_cliente_01_MS_Result>();
                    }
                    else
                    {
                        data = cambios_mift_cliente_01_MS(Opcion, Nombre).
                            Select(i => new cambios_mift_cliente_01_MS_Result
                            {
                                accnum = i.accnum,
                                nombre = i.nombre

                            }).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                data = MSG_ERROR + e.Message;
            }
            return data;
        }
        public int CountOrdenantes(byte Opcion, string Nombre)
        {
            return unitOfWork.CambiosRepository.cambios_mift_cliente_01_MS(Opcion, Nombre).ToList().Count();
        }
        public IList<cambios_mift_cliente_01_MS_Result> cambios_mift_cliente_01_MS(byte Opcion, string Nombre)
        {
            return unitOfWork.CambiosRepository.cambios_mift_cliente_01_MS(Opcion, Nombre);
        }

        #endregion


        #region "CHECKLIST CONTROL"

        public void Main(InitializationObject initObj)
        {
            frmChkList.main(initObj);
        }
        public void LimpiarChkList(InitializationObject initObj)
        {
            frmChkList.Limpiar(initObj);
        }


        public int Cambios_Mift_Check_List_Log_01(string cuenta, string desMoneda, decimal monto, string usuario, string checkLista)
        {
            return unitOfWork.CambiosRepository.cambios_mift_check_list_log_01_MS(cuenta, desMoneda, monto, usuario, checkLista);
        }


        public void BotonCopiar(string Cuenta,string DesMoneda,decimal Monto,string Usuario,string ListaChk)
        {
            unitOfWork.CambiosRepository.cambios_mift_check_list_log_01_MS(Cuenta, DesMoneda, Monto, Usuario, ListaChk);
        }


        #endregion




        #region "Lista"
        private IList<cambios_gral_consulta_00_MS_Result> Cambios_gral_consulta_00_MS(byte Opcion)
        {
            return unitOfWork.CambiosRepository.cambios_gral_consulta_00_MS(Opcion);
        }
        public List<SelectListItem> CargarMonedas(byte Opcion)
        {
            List<SelectListItem> listaMonedas = new List<SelectListItem>();

            foreach (cambios_gral_consulta_00_MS_Result moneda in Cambios_gral_consulta_00_MS(Opcion))
                listaMonedas.Add(new SelectListItem()
                {
                    Value = moneda.mnd_mndswf,
                    Text = moneda.mnd_mndswf,
                    Selected = moneda.mnd_mndswf == "USD"
                });
            //listaMonedas.Add(new SelectListItem { Value = "USD", Text = "USD", Selected = true });
            return (listaMonedas);
        }

        public List<SelectListItem> CargarFaxNYM()
        {
            List<SelectListItem> listaFaxNYM = new List<SelectListItem>();
            listaFaxNYM.Add(new SelectListItem { Value = "0", Text = "INSTRUCCIONES VIA FAX CTA. N.YORK", Selected = true });
            listaFaxNYM.Add(new SelectListItem { Value = "1", Text = "INSTRUCCIONES VIA FAX CTA. LONDON" });
            listaFaxNYM.Add(new SelectListItem { Value = "2", Text = "INSTRUCCIONES VIA FAX CTA. N.YORK & LONDON" });
            return (listaFaxNYM);
        }

        public List<SelectListItem> CargarOtrosM()
        {
            List<SelectListItem> listaOtrosM = new List<SelectListItem>();
            listaOtrosM.Add(new SelectListItem { Value = "0", Text = "ANEXO MAIL Y MANDATO HOLD MAIL FIRMADO", Selected = true });
            listaOtrosM.Add(new SelectListItem { Value = "1", Text = "INSTRUCCIONES VIA MAIL FIRMADOS" });
            listaOtrosM.Add(new SelectListItem { Value = "2", Text = "ANEXO MAIL FIRMADO" });
            listaOtrosM.Add(new SelectListItem { Value = "3", Text = "1.- F15, F16, F17, F18, F19, F20 y F21, previa consulta al Depto. Legal en caso de garantizar obligaciones de terceros.2.- F28, No se concede la facultad de entregar documentos de embarque." });
            listaOtrosM.Add(new SelectListItem { Value = "4", Text = "1.- F15 y F20  Previa consulta al Depatamento Legal en caso de garantizar obligaciones de terceros." });
            listaOtrosM.Add(new SelectListItem { Value = "5", Text = "1.- F15, F16, F17, F18, F19, F20 y F21 Previa consulta al Departamento Legal en caso de garantizar obligaciones de terceros." });
            listaOtrosM.Add(new SelectListItem { Value = "6", Text = "LONDON PAYMENT" });
            listaOtrosM.Add(new SelectListItem { Value = "7", Text = "MANDATO HOLD MAIL" });
            listaOtrosM.Add(new SelectListItem { Value = "8", Text = "EURO LIBRA" });
            listaOtrosM.Add(new SelectListItem { Value = "9", Text = "MANDATO ADUANERO//8732303//AGENTE : METROP. VALPO S. ANTONIO" });
            listaOtrosM.Add(new SelectListItem { Value = "10", Text = "Solicitar CallBack por cada instruccion Manual con cargo a las Ctas. Ctes. N° 109210013 y 109210544, que el cliente instruya, independiente el monto y recurrencia”." });
            listaOtrosM.Add(new SelectListItem { Value = "11", Text = "MANDATO ADUANERO" });


            return (listaOtrosM);
        }


        public IList<cambios_mift_citidoc_consulta_01_MS_Result> cambios_mift_citidoc_consulta_01_MS(string accCuenta, decimal monto)
        {
            return unitOfWork.CambiosRepository.cambios_mift_citidoc_consulta_01_MS(accCuenta, monto);
        }

        //public List<SelectListItem> CargarDocName(string accCuenta, decimal monto) //EReparo
        //{
        //    List<SelectListItem> docName = new List<SelectListItem>();

        //    //foreach (cambios_mift_citidoc_consulta_01_MS_Result doc in cambios_mift_citidoc_consulta_01_MS(accCuenta, monto))
        //    //    docName.Add(new SelectListItem()
        //    //    {
        //    //        Value = doc.Column1,
        //    //        Text = doc.document_name
        //    //    });

        //    //docName.Add(new SelectListItem { Value = "0", Text = "Seleccione", Selected = true });
        //    return (docName);
        //}

        #endregion


    }
}
