using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Services;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public static class ModSaldo
    {
        public static T_ModSaldo GetModSaldo()
        {
            return new T_ModSaldo();
        }
        public static double Obtiene_Monto(InitializationObject initObject, UnitOfWorkCext01 unit, short IdCta_CtaCteMN, short IdCta_CtaCteME, short NumCta, string ctacte)
        {
            using (var tracer = new Tracer())
            {
                T_ModSaldo ModSaldo = initObject.ModSaldo;
                double _retValue = 0;
                double monto_saldo = 0;
                string llamada = "";
                short val_num = 0;
                short val_sig = 0;
                string signo_lincre = "";
                string RutAis = "";
                string res = "";
                string tiene_lincre = "";
                string x = "";
                string mto_lincre = "";
                int mto_num = 0;
                short largo = 0;
                decimal saldoCtaCte = 0;

                try
                {
                    //SE SUPONE QUE AIS DEBE DESAPARECER ASI QUE PONGO EL VALOR POR DEFECTO
                    RutAis = "12345678";
                    if (IdCta_CtaCteMN == NumCta)
                    {
                        try
                        {
                            saldoCtaCte = XCFTServices.ConsultaSaldoCuenta(ctacte, "0");
                            tracer.TraceVerbose("Calculando el saldo CtaCte...");
                            tracer.AddToContext("saldoCtaCte", saldoCtaCte);
                        }
                        catch (Exception _ex)
                        {
                            tracer.TraceException("Alerta", _ex);
                            /*initObject.Frm_Origen_Fondos.Errors.Add(new Common.UI_Modulos.UI_Message()
                            {
                                Text = _ex.Message,
                                Type = Common.UI_Modulos.TipoMensaje.YesNo 
                            });*/
                            return 0;
                        }

                        if (saldoCtaCte <= 0)
                        {
                            return _retValue;
                        }
                        llamada = VB6Helpers.Trim(RutAis) + ModSaldo.VisSalMN + "L" + VB6Helpers.Format(VB6Helpers.Trim(VB6Helpers.Str(ctacte)), "000000000000");
                    }
                    else if (IdCta_CtaCteME == NumCta || T_MODGCON0.IdCta_ChqCCME == NumCta)
                    {
                        try
                        {
                            saldoCtaCte = XCFTServices.ConsultaSaldoCuenta(ctacte, "0");
                            tracer.TraceVerbose("Calculando el saldo CtaCte...");
                            tracer.AddToContext("saldoCtaCte", saldoCtaCte);
                        }
                        catch (Exception _ex)
                        {
                            tracer.TraceException("Alerta", _ex);
                            /*initObject.Frm_Origen_Fondos.Errors.Add(new Common.UI_Modulos.UI_Message()
                            {
                                Text = _ex.Message,
                                Type = Common.UI_Modulos.TipoMensaje.YesNo 
                            });*/
                            return 0;
                        }
                    }

                    res = RespuestaSalVis(ctacte);
                    if (res == "-1")
                    {
                        initObject.Frm_Origen_Fondos.Errors.Add(new Common.UI_Modulos.UI_Message()
                        {
                            Text = "Error al leer el Saldo de la Cuenta Corriente N° : " + VB6Helpers.Trim(VB6Helpers.Str(ctacte)) + " . Reporte este problema.",
                            Type = Common.UI_Modulos.TipoMensaje.Error
                        });
                    }

                    if (res == "")
                    {
                        initObject.Frm_Origen_Fondos.Errors.Add(new Common.UI_Modulos.UI_Message()
                        {
                            Text = "Es probable que la Cuenta Corriente N° : " + VB6Helpers.Trim(VB6Helpers.Str(ctacte)) + " No Exista . . Consulte con el Ejecutivo de Negocio o de Cuenta.",
                            Type = Common.UI_Modulos.TipoMensaje.Error
                        });
                    }

                    if (IdCta_CtaCteMN == NumCta)
                    {
                        monto_saldo = (double)saldoCtaCte;
                        if (tiene_lincre == "00")
                        {
                            mto_lincre = VB6Helpers.Mid(res, 374, 11);
                            signo_lincre = VB6Helpers.Mid(mto_lincre, 11, 1);

                            //busca el signo del monto de la linea
                            x = SyGet_Signo(unit, signo_lincre);

                            if (VB6Helpers.Len(x) >= 1)
                            {
                                val_num = VB6Helpers.CShort(VB6Helpers.Mid(x, 1, 1));
                            }

                            if (VB6Helpers.Len(x) >= 2)
                            {
                                val_sig = VB6Helpers.CShort(VB6Helpers.Mid(x, 2, VB6Helpers.Len(x) - 1));
                            }
                            largo = (short)VB6Helpers.Len(mto_lincre);
                            mto_lincre = VB6Helpers.Mid(mto_lincre, 1, largo - 1) + VB6Helpers.Trim(VB6Helpers.Str(val_num));
                            mto_num = VB6Helpers.CInt(VB6Helpers.Mid(mto_lincre, 1, largo));
                            monto_saldo = monto_saldo + (mto_num * val_sig);
                        }
                    }
                    else if (IdCta_CtaCteME == NumCta || T_MODGCON0.IdCta_ChqCCME == NumCta)
                    {
                        monto_saldo = (double)saldoCtaCte;
                        if (!string.IsNullOrWhiteSpace(res))
                        {
                            monto_saldo = monto_saldo + (mto_num * val_sig);
                        }
                    }
                    _retValue = monto_saldo;
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta", _ex);
                    throw;
                }
                return _retValue;
            }
        }
        public static string SyGet_Signo(UnitOfWorkCext01 unit, string signo)
        {
            try
            {
                sce_ccde_s01_MS_Result res = unit.SceRepository.EjecutarSP<sce_ccde_s01_MS_Result>("sce_ccde_s01_MS", signo).First();
                return res.valor.ToString().Trim() + res.signo.ToString().Trim();
            }
            catch (Exception _ex)
            {
                return String.Empty;
            }
        }
        public static string RespuestaSalVis(string Llave)
        {
            try
            {
                var s = XCFTServices.ConsultaCuentaCorriente(Llave);
                return s;
            }
            catch (Exception ex)
            {
                return "-1";
            }
            //dynamic prms = new { mensaje = Llave, Largo = Llave.Length, Status = "00", function = "01", Contexto = "00", Control = "00" };
            //string sBdd = VB6Helpers.Trim(VB6Helpers.Mid(prms.mensaje, 22, 10));
            ////x = Mdl_SRM.Srmw32(Mdl_SRM.ParamSrm8k.Nodo.Value, Mdl_SRM.ParamSrm8k.Servidor.Value, Mdl_SRM.ParamSrm8k.mensaje.Value, ref _tempVar3, Mdl_SRM.ParamSrm8k.Status.Value, Mdl_SRM.ParamSrm8k.funcion.Value, Mdl_SRM.ParamSrm8k.Contexto.Value, Mdl_SRM.ParamSrm8k.Control.Value);
            //return String.Empty;
        }
    }
}
