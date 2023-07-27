using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.BL.XCFT.Forms;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Core.Entities.Mcambio;
using BCH.Comex.Data.DAL.Mcambio;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Utils;
namespace BCH.Comex.Core.BL.Mcambio
{
    public enum TipoParametro
    {
        Todo = 'A',                 /*Compras, ventas, arbitrajes compra, arbitrajes venta*/
        TodoVenta = 'V',            /*Spot ventas, cruce ventas, arbitraje venta*/
        TodoCompra = 'C',           /*Spot compras, cruce compras, arbitraje compra*/
        SoloArbitraje = 'D',        /*Compras Arbitraje, Ventas Arbitraje*/
        SoloSpotCruce = 'G',        /*Compra, venta sin arbitrajes*/
        SoloSpotCruceCompras = 'H', /*Spot compras, cruce compras*/
        SoloSpotCruceVentas = 'I'   /*Spot ventas, cruce ventas*/
    }
    public enum TipoServicio
    {
        ARBI,
        COMINV,
        PlvSO
    }
    public enum PaginaOrigen 
    {
        COMINV,
        ARBITRAJES,
        PlvSO
    }
    public enum EstadoDeal 
    {
        EnProcesoNI = 2,
        InstruidaNI = 3,
        Liquidada = 9
    }
    public class McambioService
    {
        private const string Mcambio_SP_ConsultaDealChileFX = "ConsultaDealsDisponible";
        private const string Mcambio_SP_Cvd_Consulta_Precios = "pro_cvd_ni_consulta_precios2";
        private const string Mcambio_SP_CreaDealChlFxTeso = "CrearDealChileFxTesoreria";
        private const string Mcambio_SP_IncorpNumOpeCSE = "IncorporacionNumeroOperacionSCE";
        private const string Mcambio_SP_CambioEstCFXT = "CambioDeEstadoCFXT";
        private const string Cext01_spInsRelNumsce = "sp_ins_rel_numsce";
        private const string Cext01_SP_SceRevContable = "sce_reversa_contable";
        private const string Cext01_SP_SceAnulacion_net = "sce_anulacion_net";
        private const string Cext01_SP_SceUpdRelDealNumsce = "sce_Upd_rel_deal_numsce";
        private const string msgOkAnulacionDia = "Se ha anulado exitosamente la Operación de Compra-Venta.";

        private static string ParamGeneralCanal = "";
        private static double ParamGeneralValor_spread = 0;
        private static double ParamGeneralTolerancia_X_Porc = 0;
        private static double ParamGeneralTolerancia_X_USD = 0;
        private static int ParamGeneralGetdateMasXDiasHabiles = 0;
        private static string ParamGeneralResponsable = "";
        private static string ParamGeneralCodFormaPago = "";
        private static string ParamGeneral_PlvSO_CodComBcoCentral = "";

        private static string ParamGeneral_ArbitrajeCompra_CodComBcoCentral = "";
        private static string ParamGeneral_ArbitrajeVenta_CodComBcoCentral = "";

        private static double ParamGeneral_ValorMaximoPizarra = 0;
        private static string ParamGeneral_ValorDefaultSegmento = "";
        private static int ParamGeneral_FlagAnulacion = 0;
        private static string ParamGeneral_otra_condic = "";
        private static double valMontoSegundaMoneda = 0;
        private static double ResPrecioFinal = 0;
        private static System.DateTime FechaValutaFinal;

        public static string ParaMsgSonicBoom = "";
        public static Int64 NewIdChileFx = 0;

        public string formatearRut(string rut, Boolean ConSinDig) 
        {
            string fmtRut = rut;
            if (string.IsNullOrEmpty(fmtRut))
            {
                fmtRut = "";
            }
            if (Regex.IsMatch(fmtRut, "~|"))
            {
                fmtRut = fmtRut.Replace("|", "").Replace("~", "").Trim();
                rut = fmtRut;
            }
            int bl = 0;
            if (fmtRut.Length < 2)
            {
                fmtRut = "";
            }
            else if (fmtRut.Substring(fmtRut.Length - 1, 1).ToUpper() == "K")
            {
                if (int.TryParse(fmtRut.Substring(0, fmtRut.Length - 1), out bl))
                {
                    fmtRut = int.Parse(fmtRut.Substring(0, fmtRut.Length - 1)).ToString();
                    if (ConSinDig)
                    {
                        fmtRut = fmtRut + "-" + rut.Substring(rut.Length - 1, 1);
                    }
                }
                else
                {
                    fmtRut = "";
                }
            }
            else if (int.TryParse(rut, out bl))
            {
                fmtRut = int.Parse(fmtRut).ToString();
                fmtRut = fmtRut.Substring(0, fmtRut.Length - 1);
                if (ConSinDig)
                {
                    fmtRut = fmtRut + "-" + rut.Substring(rut.Length - 1, 1);
                }
            }
            else if (fmtRut.IndexOf("-") != 0 && fmtRut.Substring(fmtRut.Length - 1, 1) != "-")
            {
                if (int.TryParse(fmtRut.Substring(0, fmtRut.Length - 2), out bl))
                {
                    fmtRut = int.Parse(fmtRut.Substring(0, fmtRut.Length - 2)).ToString();
                    if (ConSinDig)
                    {
                        fmtRut = fmtRut + "-" + rut.Substring(rut.Length - 1, 1);
                    }
                }
            }
            else
            {
                fmtRut = "";
            }
            return fmtRut;
        }
        public string RecCodBancoCentral(string val, int queDevolver) 
        {
            string cod = val.Trim();
            if (string.IsNullOrEmpty(val))
            {
                val = "";
            }
            else if (val.Length < 11)
            {
                val = "";
            }
            else
            {
                if (queDevolver == 1)   //codigo lado izquierdo
                {
                    val = cod.Substring(0, cod.IndexOf("-"));
                }
                else if (queDevolver == 2)  //codigo lado derecho
                {
                    val = cod.Substring(cod.IndexOf("-") + 1, (cod.IndexOf(" ") - cod.IndexOf("-")) - 1);
                }
                else    //codigo completo (izquierdo-derecho)
                {
                    val = cod.Substring(0, cod.IndexOf(" "));
                }
            }
            return val;
        }
        public ClassResultConsultDealsDispon Mcambio_GetDealsDisponibles(InitializationObject initObject, string tipoTransaccion, TipoServicio TipServ, Nullable<int> DealPrevSel, Nullable<int> DealActualSel, List<DealsIngresadosParaProcesar> DealsIngParaProces)
        {
            var rutclie = formatearRut(initObject.MODGCVD.VgCvd.rutcli, true);
            var tp = tipoTransaccion;
            valMontoSegundaMoneda = 0;
            ResPrecioFinal = 0;
            GetParamComexgGenerales();
            using (McambioEntities context = new McambioEntities())
            {
                context.Database.Connection.Open();
                SqlParameter[] ParamGetDealDispo = {
                                                    new SqlParameter("@rutCliente", rutclie),
                                                    new SqlParameter("@tipoTransaccion", tp),
                                                    new SqlParameter("@ls_codret", ""),
                                                    new SqlParameter("@codret", SqlDbType.VarChar, 5)
                                                    {
                                                        Direction = System.Data.ParameterDirection.Output
                                                    },
                                                    new SqlParameter("@ls_msgret", ""),
                                                    new SqlParameter("@msgret", SqlDbType.VarChar, 150)
                                                    {
                                                        Direction = System.Data.ParameterDirection.Output
                                                    }
                                                };
                var varMcambioConsultaDealsDisponibles = context.Database.SqlQuery<Mcambio_Consulta_Deals_Disponible>(
                                Mcambio_SP_ConsultaDealChileFX.ToString() + " @rutCliente, @tipoTransaccion, @ls_codret = @codret OUTPUT, @ls_msgret = @msgret OUTPUT",
                                ParamGetDealDispo).ToList();
                context.Database.Connection.Close();
                varMcambioConsultaDealsDisponibles = Mcambio_FormatValues(initObject.ModChVrf, varMcambioConsultaDealsDisponibles, TipServ, initObject.DealsIngParaProces);
                if (varMcambioConsultaDealsDisponibles == null)
                {
                    ClassResultConsultDealsDispon resultVacio = new ClassResultConsultDealsDispon
                    {
                        status = null,
                        MsgSP = null,
                        data = null,
                        lg = 0,
                        dps = null,
                        das = null
                    };
                    return resultVacio;
                }
                if (DealsIngParaProces != null)
                {
                    DealActualSel = 0;
                }
                string msg = "<h4>" + ParamGetDealDispo[5].SqlValue.ToString() + "</h4>";
                if (varMcambioConsultaDealsDisponibles.Count() == 0)
                {
                    msg = "<tr><td style='padding: 4px 5px;'>" + "El saldo del deal ha sido consumido por completo." + "</td></tr>";
                }
                ClassResultConsultDealsDispon resulToView = new ClassResultConsultDealsDispon
                {
                    status = ParamGetDealDispo[3].SqlValue.ToString(),
                    MsgSP = msg,
                    data = varMcambioConsultaDealsDisponibles,
                    lg = varMcambioConsultaDealsDisponibles.Count(),
                    dps = DealPrevSel,
                    das = DealActualSel
                };
                return resulToView;
            }
        }
        public string FormatSinRedondear(Nullable<double> val, int LargoDec, string Ret) 
        {
            string res = Ret;
            string Dec = new String('0', LargoDec);
            if (val != null && val != 0)
            {
                if (val.ToString().IndexOf(",") == -1)
                {
                    if (val < 10)
                    {
                        res = String.Format("{0:,0." + Dec + "}", val);
                    }
                    else
                    {
                        res = String.Format("{0:0,0.00" + "}", val);
                    }
                }
                else
                {
                    if (val < 10)
                    {
                        res = String.Format("{0:,0." + Dec + "}", val);
                    }
                    else
                    {
                        if (Dec.Length == 3)
                        {
                            res = String.Format("{0:0,0." + Dec + "}", (Math.Truncate((decimal)val * 1000) / 1000));
                        }
                        else if (Dec.Length == 4)
                        {
                            res = String.Format("{0:0,0." + Dec + "}", (Math.Truncate((decimal)val * 10000) / 10000));
                        }
                        else
                        {
                            res = String.Format("{0:0,0." + Dec + "}", (Math.Truncate((decimal)val * 100) / 100));
                        }
                    }
                    res = double.Parse(res).ToString();
                }
            }
            return res;
        }
        public List<Mcambio_Consulta_Deals_Disponible> Mcambio_FormatValues(T_ModChVrf ModChVrf, List<Mcambio_Consulta_Deals_Disponible> Mcambio_CDD_Model, TipoServicio TipServ, List<DealsIngresadosParaProcesar> DealsIngParaProces)
        {
            int IdChileFX;
            double DeltaOriginal = 0;
            int MtoBcoRec_o_Delta = 0;
            if (DealsIngParaProces != null)
            {
                if (DealsIngParaProces.Count == 0)
                {
                    DealsIngParaProces = null;
                }
            }
            if (DealsIngParaProces != null)
            {
                MtoBcoRec_o_Delta = 1;
                if (DealsIngParaProces[0].numeroDeal != null && DealsIngParaProces[0].TipoIngreso != "P" && DealsIngParaProces.Count() != 0)
                {
                    IdChileFX = (int)DealsIngParaProces[0].numeroDeal;
                    DeltaOriginal = (double)DealsIngParaProces[0].DeltaOrig;
                    int TotReg = Mcambio_CDD_Model.Count();
                    Mcambio_CDD_Model.RemoveAll(s => s.numeroDeal != IdChileFX);
                    double MtoDelta = 0;
                    double MtoIng1 = 0;
                    double MtoIng2 = 0;
                    foreach (var item in DealsIngParaProces)
                    {
                        if (string.IsNullOrEmpty(Mcambio_CDD_Model[0].delta.ToString()) || Mcambio_CDD_Model[0].delta == 0 || Mcambio_CDD_Model[0].delta == null)
                        {
                            MtoDelta = 0;
                        }
                        else
                        {
                            MtoDelta = double.Parse(FormatSinRedondear(Mcambio_CDD_Model[0].delta, 6, ""));
                        }
                        if (string.IsNullOrEmpty(item.Monto1_Ingresado.ToString()) || item.Monto1_Ingresado == 0 || item.Monto1_Ingresado == null)
                        {
                            MtoIng1 = 0;
                        }
                        else
                        {
                            MtoIng1 = double.Parse(FormatSinRedondear(item.Monto1_Ingresado, 6, ""));
                        }
                        if (string.IsNullOrEmpty(item.Monto2_Ingresado.ToString()) || item.Monto2_Ingresado == 0 || item.Monto2_Ingresado == null)
                        {
                            MtoIng2 = 0;
                        }
                        else
                        {
                            MtoIng2 = double.Parse(FormatSinRedondear(item.Monto2_Ingresado, 6, ""));
                        }
                        if (TipServ == TipoServicio.ARBI)
                        {
                            if (DealsIngParaProces[0].AbrevTipTrans == "V")
                            {
                                MtoDelta = (MtoDelta - MtoIng2);
                            }
                            else
                            {
                                MtoDelta = (MtoDelta - MtoIng1);
                            }
                        }
                        else
                        {
                            MtoDelta = (MtoDelta - MtoIng1);
                        }
                        Mcambio_CDD_Model[0].delta = MtoDelta;
                    }
                    if (MtoDelta <= 0)
                    {
                        Mcambio_CDD_Model.RemoveAll(s => s.numeroDeal != 0);
                        return Mcambio_CDD_Model;
                    }
                }
                else
                {
                    return null;
                }
            }
            for (int i = Mcambio_CDD_Model.Count - 1; i >= 0; i--)
            {
                if (Mcambio_CDD_Model[i].delta <= 0 || Mcambio_CDD_Model[i].delta == null)
                {
                    Mcambio_CDD_Model.RemoveAt(i);
                }
            }


            foreach (var tom in Mcambio_CDD_Model)
            {
                if (DealsIngParaProces == null)
                {
                    tom.DeltaOrig = tom.delta;
                }
                else
                {
                    tom.DeltaOrig = DeltaOriginal;
                }
                //Compra = 0
                //Venta = 1 (spot)
                //compra - Transferencia interna = 2 (arbitraje)
                //venta - Transferencia interna = 3 (arbitraje)
                if (tom.tipoTransaccion == "V")
                {
                    if ((tom.codigoTipoOperacion == 126 || tom.codigoTipoOperacion == 128))
                    {
                        tom.tipoTransaccion = "ArbVta";
                        tom.intCodTipoTransaccion = 3;
                    }
                    else
                    {
                        tom.tipoTransaccion = "Ventas";
                        tom.intCodTipoTransaccion = 1;
                    }
                    tom.stTipoTransaccion = "Ventas";
                    tom.AbrevTipTrans = "V";
                    tom.monedaMuestra = tom.moneda2;
                }
                else if (tom.tipoTransaccion == "C")
                {
                    if ((tom.codigoTipoOperacion == 126 || tom.codigoTipoOperacion == 128))
                    {
                        tom.tipoTransaccion = "ArbCom";
                        tom.intCodTipoTransaccion = 2;
                    }
                    else
                    {
                        tom.tipoTransaccion = "Compras";
                        tom.intCodTipoTransaccion = 0;
                    }
                    tom.stTipoTransaccion = "Compras";
                    tom.AbrevTipTrans = "C";
                    tom.monedaMuestra = tom.moneda1;
                }
                else
                {
                    tom.tipoTransaccion = "Otros";
                    tom.intCodTipoTransaccion = 0;
                    tom.AbrevTipTrans = "C";
                    tom.monedaMuestra = tom.moneda1;
                }
                if (tom.codigoOrigenCarga == null)
                {
                    tom.txtcodigoOrigenCarga = "";
                }
                else if (tom.codigoOrigenCarga == -1)
                {
                    tom.txtcodigoOrigenCarga = "Pizarra";
                }
                else
                {
                    tom.txtcodigoOrigenCarga = "ChileFx";
                }
                tom.stFechaTransaccion = "";
                if (tom.fechaTransaccion != null)
                {
                    tom.stFechaTransaccion = DateTime.Parse(tom.fechaTransaccion.ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                tom.stFechaValuta1 = "";
                if (tom.fechaValuta1 != null)
                {
                    tom.stFechaValuta1 = DateTime.Parse(tom.fechaValuta1.ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                tom.stCodigoEstadoContable = "";
                if (tom.codigoEstadoContable != null)
                {
                    tom.stCodigoEstadoContable = tom.codigoEstadoContable.ToString();
                }
                tom.stultimoNumeroTransitoria = "";
                if (tom.ultimoNumeroTransitoria != null && tom.ultimoNumeroTransitoria != "0")
                {
                    tom.stultimoNumeroTransitoria = tom.ultimoNumeroTransitoria.ToString();
                }
                tom.stultimoNumeroContingente = "";
                if (tom.ultimoNumeroContingente != null && tom.ultimoNumeroContingente != "0")
                {
                    tom.stultimoNumeroContingente = tom.ultimoNumeroContingente.ToString();
                }
                tom.stCodigoBancoCentral = "";
                if (tom.codigoBancoCentral != null && tom.codigoBancoCentral != "0")
                {
                    tom.codigoBancoCentral = tom.codigoBancoCentral.ToString();
                    short n = 0;
                    string s = "";
                    if (ModChVrf.VCcpl != null)
                    {
                        n = (short)(ModChVrf.VCcpl.Count() - 1);
                        for (int i = 0; i <= n; i++)
                        {
                            if ((VB6Helpers.Val(ModChVrf.VCcpl[i].tipope) >= 110 && VB6Helpers.Val(ModChVrf.VCcpl[i].tipope) <= 240) && ModChVrf.VCcpl[i].CodCom == tom.codigoBancoCentral)
                            {
                                s = "";
                                s = s + VB6Helpers.Trim(ModChVrf.VCcpl[i].CodCom + "-" + ModChVrf.VCcpl[i].CptCom) + VB6Helpers.Space(2);
                                s += VB6Helpers.Trim(VB6Helpers.Mid(MODGPYF1.Minuscula(ModChVrf.VCcpl[i].DesCom), 1, 55));
                                tom.stCodigoBancoCentral = s;
                                break;
                            }
                        }
                    }
                }
                tom.stMontoBancoRecibe = FormatSinRedondear(tom.montoBancoRecibe, 2, "0");
                tom.stDelta = FormatSinRedondear(tom.delta, 2, "0");
                tom.stPrecioPoolMoneda2 = FormatSinRedondear(tom.precioPoolMoneda2, 4, "0");
                tom.precioPoolMoneda2 = string.IsNullOrEmpty(tom.stPrecioPoolMoneda2) ? 0 : double.Parse(tom.stPrecioPoolMoneda2);
                double UsarParidad = CalculoParidad(tom.montoBancoRecibe, tom.precioCliente, tom.montoClienteRecibe);
                if (tom.tipoTransaccion == "ArbVta" || tom.tipoTransaccion == "ArbCom")
                {
                    tom.stPrecioCliente = UsarParidad.ToString();
                    tom.Paridad_Ingresada = UsarParidad;
                    double SetMtoClieRec = GetMtoClieRec(UsarParidad, tom.precioCliente, tom.montoBancoRecibe, tom.montoClienteRecibe, tom.delta, MtoBcoRec_o_Delta);

                    string stCalcOriMnd2 = String.Format("{0:#,0.00}", Math.Round((double)(tom.montoBancoRecibe * UsarParidad), 2));
                    tom.stCalcSldMnd2 = String.Format("{0:#,0.00}", stCalcOriMnd2);

                    SetMtoClieRec = Math.Round(SetMtoClieRec, 2) == Math.Round((double)(tom.delta * UsarParidad), 2) ? SetMtoClieRec : Math.Round((double)(tom.delta * UsarParidad), 2);
                    tom.stMontoClienteRecibe = FormatSinRedondear(SetMtoClieRec, 2, "");
                    if (tom.tipoTransaccion == "ArbVta")
                    {
                        tom.stSaldoMoneda1 = String.Format("{0:#,0.00}", SetMtoClieRec);
                        tom.stSaldoMoneda2 = tom.stDelta;
                    }
                    if (tom.tipoTransaccion == "ArbCom")
                    {
                        tom.stSaldoMoneda1 = tom.stDelta;
                        tom.stSaldoMoneda2 = String.Format("{0:#,0.00}", SetMtoClieRec);
                    }
                }
                else
                {
                    tom.stPrecioCliente = FormatSinRedondear(tom.precioCliente, 4, "");
                    tom.Paridad_Ingresada = tom.precioCliente;
                    tom.stMontoClienteRecibe = FormatSinRedondear(tom.delta * tom.precioCliente, 4, "");
                    tom.stMontoClienteRecibe = String.Format("{0:#,0}", Math.Round(double.Parse(tom.stMontoClienteRecibe), 0));
                }
            }
            return Mcambio_CDD_Model;
        }
        public double CalculoParidad(Nullable<double> mtoBcoRec, Nullable<double> precCli, Nullable<double> mtoCliRec) 
        {
            mtoBcoRec = mtoBcoRec == null ? 0 : mtoBcoRec;
            precCli = precCli == null ? 0 : precCli;
            mtoCliRec = mtoCliRec == null ? 0 : mtoCliRec;
            double Resul = 0;
            if (((mtoBcoRec * precCli) == mtoCliRec) || 
                 (Math.Round((double)(mtoBcoRec * precCli), 2) == Math.Round((double)mtoCliRec,2)) ||
                 (Math.Round((double)(mtoBcoRec * precCli), 4) == Math.Round((double)mtoCliRec, 4)) ||
                 (Math.Round((double)(mtoBcoRec * precCli), 0) == Math.Round((double)mtoCliRec,0)) ||
                 ((Math.Truncate((double)(mtoBcoRec * precCli) * 100) / 100) == (Math.Truncate((double)mtoCliRec * 100) / 100)) ||
                 ((Math.Truncate((double)(mtoBcoRec * precCli))) == (Math.Truncate((double)mtoCliRec)))
               )
            {
                Resul = (double)precCli;
            }
            else if (((mtoBcoRec / precCli) == mtoCliRec) || 
                      (Math.Round((double)(mtoBcoRec / precCli), 2) == Math.Round((double)mtoCliRec, 2)) ||
                      (Math.Round((double)(mtoBcoRec / precCli), 4) == Math.Round((double)mtoCliRec, 4)) ||
                      (Math.Round((double)(mtoBcoRec / precCli), 0) == Math.Round((double)mtoCliRec, 0)) ||
                      ((Math.Truncate((double)(mtoBcoRec / precCli) * 100) / 100) == (Math.Truncate((double)mtoCliRec * 100) / 100)) ||
                      ((Math.Truncate((double)(mtoBcoRec / precCli))) == (Math.Truncate((double)mtoCliRec)))
                    )
            {
                Resul = 1 / (double)precCli;
            }
            else if (((mtoBcoRec * precCli) - mtoCliRec) < ((mtoBcoRec / precCli) - mtoCliRec))
            {
                Resul = (double)precCli;
            }
            else
            {
                Resul = 1 / (double)precCli;
            }
            return Resul;
        }
        public double CalculoParidadEnPagina(List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, int idx, string tipo) 
        {
            
            Nullable<double> precCli = datSPMcambioCDD[idx].precioCliente;
            Nullable<double> precCliAjuste = 1 / datSPMcambioCDD[idx].precioCliente;
            Nullable<double> mtoBcoRec = datSPMcambioCDD[idx].montoBancoRecibe;
            Nullable<double> mtoCliRec = datSPMcambioCDD[idx].montoClienteRecibe;
            if (tipo == "C")
            {
                if (mtoBcoRec < mtoCliRec)
                {
                    return (double)precCli;
                }
                else
                {
                    return (double)precCliAjuste;
                }
            }
            else
            {
                if (mtoBcoRec > mtoCliRec)
                {
                    return (double)precCli;
                }
                else
                {
                    return (double)precCliAjuste;
                }
            }
        }
        public double GetMtoClieRec(double UsarParidad, Nullable<double> precCli, Nullable<double> mtoBcoRec, Nullable<double> mtoCliRec, Nullable<double> delta, int MBC_o_D) 
        {
            mtoBcoRec = mtoBcoRec == null ? 0 : mtoBcoRec;
            delta = delta == null ? 0 : delta;
            double mtoCal = MBC_o_D == 0 ? (double)mtoBcoRec : (double)delta;
            precCli = precCli == null ? 0 : precCli;
            double Resul = 0;
            if (UsarParidad == (double)precCli)
            {
                Resul = mtoCal * (double)precCli;
            }
            else
            {
                Resul = mtoCal * (double)UsarParidad;
            }
            return Resul;
        }
        public int idxDealSel(string chk) 
        {
            if (string.IsNullOrEmpty(chk))
            {
                return -1;
            }
            chk = Regex.Replace(chk, @"[^\w\s.!@$%^&*()';\-\/]+", "");
            if (chk.Length == 0)
            {
                return -1;
            }
            else
            {
                chk = chk.Replace("ch_dl_", "");
                int bl;
                if (int.TryParse(chk, out bl))
                {
                    if ((int.Parse(chk) - 1) < 0)
                    {
                        return -1;
                    }
                    else
                    {
                        return int.Parse(chk) - 1;
                    }
                }
                else
                {
                    return -1;
                }
            }
        }
        public void COMINV_SetDivisaDealSelec(InitializationObject initObject, List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, int idx) 
        {
            //estos valores corresponden al listindex del combobox CB_Divisa de la pagina de comercio invisible
            //Compra = 0
            //Venta = 1
            //Transferencia interna = 2 (arbitraje)
            initObject.Frm_Comercio_Invisible.Cb_Divisa.ListIndex = (int)datSPMcambioCDD[idx].intCodTipoTransaccion;
        }
        public void COMINV_SetConceptoDealSelec(InitializationObject initObject, List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, int idx) 
        {
            bool sw = false;
            string CodComercio = "";
            UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible = initObject.Frm_Comercio_Invisible;
            for (int i = 0; i < Frm_Comercio_Invisible.Lt_Tcp.ListCount-1; i++)
            {
                CodComercio = Frm_Comercio_Invisible.Lt_Tcp.Items[i].Value.Substring(0, Frm_Comercio_Invisible.Lt_Tcp.Items[i].Value.IndexOf("-"));
                if (datSPMcambioCDD[idx].stCodigoBancoCentral == Frm_Comercio_Invisible.Lt_Tcp.Items[i].Value)
                {
                    Frm_Comercio_Invisible.Lt_Tcp.ListIndex = i;
                    sw = true;
                    break;
                }
                else if (datSPMcambioCDD[idx].stCodigoBancoCentral.ToString() == "")
                {
                    if (datSPMcambioCDD[idx].codigoBancoCentral.ToString() == CodComercio)
                    {
                        Frm_Comercio_Invisible.Lt_Tcp.ListIndex = i;
                        datSPMcambioCDD[idx].stCodigoBancoCentral = Frm_Comercio_Invisible.Lt_Tcp.Items[i].Value.ToString();
                        sw = true;
                        break;
                    }
                }
            }
            if (!sw)
            {
                Frm_Comercio_Invisible.Lt_Tcp.ListIndex = 0;
                datSPMcambioCDD[idx].stCodigoBancoCentral = Frm_Comercio_Invisible.Lt_Tcp.Items[0].Value.ToString();
                Frm_Comercio_Invisible.Lt_Tcp.Enabled = true;
                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                {
                    Type = TipoMensaje.Critical,
                    Text = " ID ChileFX  " + datSPMcambioCDD[idx].numeroDeal.ToString() + "  →  Código de comercio " + datSPMcambioCDD[idx].codigoBancoCentral.ToString() + " no encontrado.  Validar información del deal."
                });
            }
        }
        public Lista_Moneda_Pais DevolverClassMonedaPais(List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, int idx)
        {
            if (datSPMcambioCDD[idx].tipoTransaccion == "Ventas")
            {
                return GetParamComexMonedaPais(datSPMcambioCDD[idx].moneda2, "SiglaPais");
            }
            else
            {
                return GetParamComexMonedaPais(datSPMcambioCDD[idx].moneda1, "SiglaPais");
            }
        }
        public Lista_Moneda_Pais DevolverClassMonedaPaisUdp(List<DealsIngresadosParaProcesar> DealsIngParaProces, int idx)
        {
            if (DealsIngParaProces[idx].tipoTransaccion == "Ventas")
            {
                return GetParamComexMonedaPais(DealsIngParaProces[idx].moneda2, "SiglaPais");
            }
            else
            {
                return GetParamComexMonedaPais(DealsIngParaProces[idx].moneda1, "SiglaPais");
            }
        }
        public void COMINV_SetValoresSelecDeal(InitializationObject initObject, List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, int idx)
        {
            UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible = initObject.Frm_Comercio_Invisible;
            Lista_Moneda_Pais MonedaPaisDealSeleccionado = DevolverClassMonedaPais(datSPMcambioCDD, idx);
            Frm_Comercio_Invisible.Cb_Pais.ListIndex = Frm_Comercio_Invisible.Cb_Pais.get_Index((int)MonedaPaisDealSeleccionado.CodPaisEquiv);
            Frm_Comercio_Invisible.Cb_Moneda.ListIndex = Frm_Comercio_Invisible.Cb_Moneda.get_Index((int)MonedaPaisDealSeleccionado.CodMoneda);
            Frm_Comercio_Invisible.Tx_MtoCV[0].Text = datSPMcambioCDD[idx].stDelta;
            Frm_Comercio_Invisible.Tx_MtoCV[1].Text = datSPMcambioCDD[idx].stPrecioCliente;
            Frm_Comercio_Invisible.Tx_MtoCV[3].Text = datSPMcambioCDD[idx].stPrecioPoolMoneda2;
            COMING_SetearValores(initObject);
        }
        public void ARBITRAJE_ConsPrec2(InitializationObject initObject) 
        {
            UI_Frm_Arbitrajes FrmArbitraje = initObject.Frm_Arbitrajes;
            Lista_Moneda_Pais MonedaCompra = GetParamComexMonedaPais(FrmArbitraje.Cb_Moneda_Compra.Value.ToString(), "CodMoneda");
            Lista_Moneda_Pais MonedaVenta = GetParamComexMonedaPais(FrmArbitraje.Cb_Moneda_Venta.Value.ToString(), "CodMoneda");
            double MontoCompra = string.IsNullOrEmpty(FrmArbitraje.Tx_Mtoarb[2].Text) ? 0 : double.Parse(FrmArbitraje.Tx_Mtoarb[2].Text);
            double MontoVenta = string.IsNullOrEmpty(FrmArbitraje.Tx_Mtoarb[3].Text) ?  0 : double.Parse(FrmArbitraje.Tx_Mtoarb[3].Text);
            string PagOri = PaginaOrigen.ARBITRAJES.ToString();
            if (MonedaCompra.SiglaMoneda != null && MonedaVenta.SiglaMoneda != null)
            {
                if (MonedaCompra.SiglaMoneda != "USD" && MonedaVenta.SiglaMoneda != "USD")
                {
                    MsgsValidarIngreso(initObject, 9, PagOri, "");
                }
                else
                {
                    valMontoSegundaMoneda = 0;
                    if (MonedaCompra.SiglaMoneda == "USD" && MontoVenta != 0 && valMontoSegundaMoneda == 0)       //Venta
                    {
                        if (MontoVenta > ParamGeneral_ValorMaximoPizarra)
                        {
                            MsgsValidarIngreso(initObject, 6, PagOri, "");
                        }
                        else
                        {
                            string MontoIngresado = MontoVenta.ToString();
                            double ResPrecioFinal = ConsultaPrecios2(initObject, PagOri, MontoIngresado);
                            if (ResPrecioFinal != 0)
                            {
                                FrmArbitraje.Tx_Mtoarb[2].Text = valMontoSegundaMoneda.ToString();
                                double resSinDivision = (double.Parse(MontoIngresado) / ResPrecioFinal);
                                double resConDivision = (double.Parse(MontoIngresado) / (1 / ResPrecioFinal));
                                string valorCalFormat = FormatSinRedondear(resSinDivision, 2, "0");
                                string valorMSMFormat = FormatSinRedondear(valMontoSegundaMoneda, 2, "0");
                                double valorRound2ResSinDiv = Math.Round(resSinDivision, 2);
                                double valorRound2valMtoSMn = Math.Round(valMontoSegundaMoneda, 2);
                                if (valorCalFormat == valorMSMFormat || valorRound2ResSinDiv == valorRound2valMtoSMn)
                                {
                                    FrmArbitraje.Tx_Mtoarb[1].Text = ResPrecioFinal.ToString();
                                }
                                else
                                {
                                    ResPrecioFinal = (1 / ResPrecioFinal);
                                    FrmArbitraje.Tx_Mtoarb[1].Text = ResPrecioFinal.ToString();
                                }

                            }
                            else
                            {
                                FrmArbitraje.Tx_Mtoarb[2].Text = "0";
                                FrmArbitraje.Tx_Mtoarb[1].Text = "0";
                            }
                        }
                        
                        
                    }
                    else if (MonedaVenta.SiglaMoneda == "USD" && MontoCompra != 0 && valMontoSegundaMoneda == 0)  //Compra
                    {
                        if (MontoCompra > ParamGeneral_ValorMaximoPizarra)
                        {
                            MsgsValidarIngreso(initObject, 6, PagOri, "");
                        }
                        else
                        {
                            string MontoIngresado = MontoCompra.ToString();
                            double ResPrecioFinal = ConsultaPrecios2(initObject, PagOri, MontoIngresado);
                            if (ResPrecioFinal != 0)
                            {
                                FrmArbitraje.Tx_Mtoarb[3].Text = valMontoSegundaMoneda.ToString();
                                FrmArbitraje.Tx_Mtoarb[1].Text = ResPrecioFinal.ToString();
                            }
                            else
                            {
                                FrmArbitraje.Tx_Mtoarb[2].Text = "0";
                                FrmArbitraje.Tx_Mtoarb[1].Text = "0";
                            }
                        }
                    }
                    else if (MonedaCompra.SiglaMoneda == "USD" && MontoCompra != 0 && MontoVenta == 0 && valMontoSegundaMoneda == 0)    //Venta
                    {
                        if (FrmArbitraje.Tx_Mtoarb[1].Text != "" && FrmArbitraje.Tx_Mtoarb[1].Text != "0")
                        {
                            MsgsValidarIngreso(initObject, 11, PagOri, "");
                        }
                        FrmArbitraje.Tx_Mtoarb[1].Text = "0";
                    }
                    else if (MonedaVenta.SiglaMoneda == "USD" && MontoVenta != 0 && MontoCompra == 0 && valMontoSegundaMoneda == 0) //Compra
                    {
                        if (FrmArbitraje.Tx_Mtoarb[1].Text != "" && FrmArbitraje.Tx_Mtoarb[1].Text != "0")
                        {
                            MsgsValidarIngreso(initObject, 12, PagOri, "");
                        }
                        FrmArbitraje.Tx_Mtoarb[1].Text = "0";
                    }
                }
            }
        }
        public void COMING_SetearValores(InitializationObject initObject) 
        {
            UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible = initObject.Frm_Comercio_Invisible;
            Frm_Comercio_Invisible_Logic Frm_Comercio_Invisible_Logic = new Frm_Comercio_Invisible_Logic();
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
            short d = 0;
            short k = 0;
            Frm_Comercio_Invisible_Logic.Tx_MtoCV_LostFocus(Frm_Comercio_Invisible, ref d);
            Frm_Comercio_Invisible_Logic.Tx_MtoCV_KeyPress(Mdi_Principal, Frm_Comercio_Invisible, ref d, ref k);
            d = 1;
            Frm_Comercio_Invisible_Logic.Tx_MtoCV_LostFocus(Frm_Comercio_Invisible, ref d);
            Frm_Comercio_Invisible_Logic.Tx_MtoCV_KeyPress(Mdi_Principal, Frm_Comercio_Invisible, ref d, ref k);
            d = 2;
            Frm_Comercio_Invisible_Logic.Tx_MtoCV_LostFocus(Frm_Comercio_Invisible, ref d);
            Frm_Comercio_Invisible_Logic.Tx_MtoCV_KeyPress(Mdi_Principal, Frm_Comercio_Invisible, ref d, ref k);
            d = 3;
            Frm_Comercio_Invisible_Logic.Tx_MtoCV_LostFocus(Frm_Comercio_Invisible, ref d);
            Frm_Comercio_Invisible_Logic.Tx_MtoCV_KeyPress(Mdi_Principal, Frm_Comercio_Invisible, ref d, ref k);
        }
        public void PlvSO_SetValoresSelecDeal(InitializationObject initObject, List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, int idx)
        {
            UI_Frm_PlvSO Frm_PlvSO = initObject.Frm_PlvSO;
            Lista_Moneda_Pais MonedaPaisDealSeleccionado = DevolverClassMonedaPais(datSPMcambioCDD, idx);
            Frm_PlvSO.Tx_CodPag.Text = ParamGeneralCodFormaPago.ToString();
            Frm_PlvSO.Cb_PPago.ListIndex = Frm_PlvSO.Cb_PPago.get_Index((int)MonedaPaisDealSeleccionado.CodPaisEquiv);
            Frm_PlvSO.Cb_Moneda.ListIndex = Frm_PlvSO.Cb_Moneda.get_Index((int)MonedaPaisDealSeleccionado.CodMoneda);
            Frm_PlvSO.Tx_MtoFob.Text = datSPMcambioCDD[idx].stDelta;
            Frm_PlvSO.Pn_ValCif.Text = datSPMcambioCDD[idx].stDelta;
            Frm_PlvSO.Pn_MtoTot.Text = datSPMcambioCDD[idx].stDelta;
            Frm_PlvSO.Pn_CifDol.Text = datSPMcambioCDD[idx].stDelta;
            Frm_PlvSO.Pn_TotDol.Text = datSPMcambioCDD[idx].stDelta;
            Frm_PlvSO.Tx_MtoFle.Text = "0";
            Frm_PlvSO.Tx_MtoSeg.Text = "0";
            Frm_PlvSO.Tx_TipCam.Text = datSPMcambioCDD[idx].stPrecioCliente;
            Frm_PlvSO.Pn_TCDol.Text = datSPMcambioCDD[idx].stPrecioCliente;
            Frm_PlvSO.Tx_Paridad.Text = double.Parse(datSPMcambioCDD[idx].stPrecioPoolMoneda2).ToString();
            Frm_PlvSO.Tx_NumDec.Text = "";
            Frm_PlvSO.Pn_NroPre.Text = "0";
        }
        public void ARBITRAJE_SetValoresSelecDeal(InitializationObject initObject, List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, int idx)
        {
            UI_Frm_Arbitrajes FrmArbitrajes = initObject.Frm_Arbitrajes;
            Lista_Moneda_Pais MonedaPaisDealSeleccionado;
            UnitOfWorkCext01 uow = new UnitOfWorkCext01();
            FrmArbitrajes.nroDeal = datSPMcambioCDD[idx].numeroDeal.ToString();
            //compra
            MonedaPaisDealSeleccionado = GetParamComexMonedaPais(datSPMcambioCDD[idx].moneda1, "SiglaPais");
            FrmArbitrajes.Cb_Moneda_Compra.ListIndex = FrmArbitrajes.Cb_Moneda_Compra.get_Index((int)MonedaPaisDealSeleccionado.CodMoneda);
            Frm_Arbitrajes_Logic.Cb_Moneda_Compra_Click(initObject, uow);
            //venta
            MonedaPaisDealSeleccionado = GetParamComexMonedaPais(datSPMcambioCDD[idx].moneda2, "SiglaPais");
            FrmArbitrajes.Cb_Moneda_Venta.ListIndex = FrmArbitrajes.Cb_Moneda_Venta.get_Index((int)MonedaPaisDealSeleccionado.CodMoneda);
            Frm_Arbitrajes_Logic.Cb_Moneda_Venta_Click(initObject, uow);
            //paridad
            string TipTrans = datSPMcambioCDD[idx].AbrevTipTrans;
            double UsarParidad = CalculoParidadEnPagina(datSPMcambioCDD, idx, TipTrans);
            if (TipTrans == "V")
            {
                FrmArbitrajes.Tx_Mtoarb[1].Text = UsarParidad.ToString();
                datSPMcambioCDD[idx].Paridad_Ingresada = UsarParidad;
            }
            else
            {
                FrmArbitrajes.Tx_Mtoarb[1].Text = UsarParidad.ToString();
                datSPMcambioCDD[idx].Paridad_Ingresada = UsarParidad;
            }
            Frm_Arbitrajes_Logic.Tx_Mtoarb_KeyPress(initObject.Frm_Arbitrajes, 1);
            Frm_Arbitrajes_Logic.Tx_Mtoarb_LostFocus(initObject.MODGARB, initObject.Frm_Arbitrajes, initObject.MODGTAB0, 1);
            //Monto
            if (TipTrans == "V")
            {
                //Monto Venta
                FrmArbitrajes.Tx_Mtoarb[3].Text = double.Parse(datSPMcambioCDD[idx].stDelta).ToString();
                Frm_Arbitrajes_Logic.Tx_Mtoarb_KeyPress(initObject.Frm_Arbitrajes, 3);
                Frm_Arbitrajes_Logic.Tx_Mtoarb_LostFocus(initObject.MODGARB, initObject.Frm_Arbitrajes, initObject.MODGTAB0, 3);
            }
            else
            {
                //Monto Compra
                FrmArbitrajes.Tx_Mtoarb[2].Text = double.Parse(datSPMcambioCDD[idx].stDelta).ToString();
                Frm_Arbitrajes_Logic.Tx_Mtoarb_KeyPress(initObject.Frm_Arbitrajes, 2);
                Frm_Arbitrajes_Logic.Tx_Mtoarb_LostFocus(initObject.MODGARB, initObject.Frm_Arbitrajes, initObject.MODGTAB0, 2);
            }
        }
        public Lista_Moneda_Pais GetParamComexMonedaPais(string dato, string filtro) 
        {
            UnitOfWorkCext01 uow = new UnitOfWorkCext01();
            List<ParametroComex> lsttblMonedas = uow.ParametroComexRepository.GetParametrosComex("SCE.Net: FundTransfer", "EqMonedaPais", "*").ToList();
            Lista_Moneda_Pais MonedaPaisDeal = new Lista_Moneda_Pais();
            string valor = null;
            for (int i = 0; i < lsttblMonedas.Count()-1; i++)
            {
                if (filtro == "SiglaPais")
                {
                    if (dato.Trim() == lsttblMonedas[i].trans_dsc_parametro.Trim())
                    {
                        valor = lsttblMonedas[i].trans_vlr_parametro;
                        MonedaPaisDeal.CodMoneda = int.Parse(string.IsNullOrEmpty(valor) ? "0" : valor);
                        MonedaPaisDeal.SiglaMoneda = lsttblMonedas[i].trans_dsc_parametro;
                        valor = lsttblMonedas[i].trans_nmb_agrupacion_4;
                        MonedaPaisDeal.CodPaisEquiv = int.Parse(string.IsNullOrEmpty(valor) ? "0" : valor);
                        break;
                    }
                }
                else if (filtro == "CodMoneda")
                {
                    if (dato.Trim() == lsttblMonedas[i].trans_vlr_parametro.Trim())
                    {
                        valor = lsttblMonedas[i].trans_vlr_parametro;
                        MonedaPaisDeal.CodMoneda = int.Parse(string.IsNullOrEmpty(valor) ? "0" : valor);
                        MonedaPaisDeal.SiglaMoneda = lsttblMonedas[i].trans_dsc_parametro;
                        valor = lsttblMonedas[i].trans_nmb_agrupacion_4;
                        MonedaPaisDeal.CodPaisEquiv = int.Parse(string.IsNullOrEmpty(valor) ? "0" : valor);
                        break;
                    }
                }
            }
            return MonedaPaisDeal;
        }
        public void GetParamComexgGenerales()
        {
            UnitOfWorkCext01 uow = new UnitOfWorkCext01();
            List<ParametroComex> paramGenerales = uow.ParametroComexRepository.GetParametrosComex("SCE.Net: FundTransfer", "Generales", "*").ToList();
            for (int i = 0; i < paramGenerales.Count(); i++)
            {
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G1")
                {
                    ParamGeneralCanal = paramGenerales[i].trans_vlr_parametro.Trim();
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G2")
                {
                    ParamGeneralValor_spread = double.Parse(string.Format("{0:n}", string.IsNullOrEmpty(paramGenerales[i].trans_vlr_parametro) ? 0 : double.Parse(paramGenerales[i].trans_vlr_parametro.Trim().ToString(), CultureInfo.InvariantCulture)).ToString());
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G3")
                {
                    ParamGeneralTolerancia_X_Porc = double.Parse(string.Format("{0:n}", string.IsNullOrEmpty(paramGenerales[i].trans_vlr_parametro) ? 0 : double.Parse(paramGenerales[i].trans_vlr_parametro.Trim().ToString(), CultureInfo.InvariantCulture)).ToString());
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G4")
                {
                    ParamGeneralTolerancia_X_USD = double.Parse(string.Format("{0:n}", string.IsNullOrEmpty(paramGenerales[i].trans_vlr_parametro) ? 0 : double.Parse(paramGenerales[i].trans_vlr_parametro.Trim().ToString(), CultureInfo.InvariantCulture)).ToString());
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G5")
                {
                    ParamGeneralGetdateMasXDiasHabiles = string.IsNullOrEmpty(paramGenerales[i].trans_vlr_parametro) ? 0 : int.Parse(paramGenerales[i].trans_vlr_parametro.ToString());
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G6")
                {
                    ParamGeneralResponsable = paramGenerales[i].trans_vlr_parametro.Trim();
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G7")
                {
                    ParamGeneralCodFormaPago = paramGenerales[i].trans_vlr_parametro.Trim();
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G8")
                {
                    ParamGeneral_PlvSO_CodComBcoCentral = paramGenerales[i].trans_vlr_parametro.Trim();
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G9")
                {
                    ParamGeneral_ArbitrajeCompra_CodComBcoCentral = paramGenerales[i].trans_vlr_parametro.Trim();
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G10")
                {
                    ParamGeneral_ArbitrajeVenta_CodComBcoCentral = paramGenerales[i].trans_vlr_parametro.Trim();
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G11")
                {
                    ParamGeneral_ValorMaximoPizarra = double.Parse(string.Format("{0:n}", string.IsNullOrEmpty(paramGenerales[i].trans_vlr_parametro) ? 0 : double.Parse(paramGenerales[i].trans_vlr_parametro.Trim().ToString(), CultureInfo.InvariantCulture)).ToString());
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G12")
                {
                    ParamGeneral_ValorDefaultSegmento = paramGenerales[i].trans_vlr_parametro.Trim();
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G13")
                {
                    ParamGeneral_FlagAnulacion = string.IsNullOrEmpty(paramGenerales[i].trans_vlr_parametro) ? 0 : int.Parse(paramGenerales[i].trans_vlr_parametro.ToString());
                }
                if (paramGenerales[i].trans_nmb_agrupacion_4.Trim() == "G14")
                {
                    ParamGeneral_otra_condic = paramGenerales[i].trans_vlr_parametro.Trim();
                }
            }
        }
        private void IgualarRegistros(List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, List<DealsIngresadosParaProcesar> DealsIngParaProces,int newReg,int idx) 
        {
            if (DealsIngParaProces.Count() > 1)
            {
                idx = 0;
            }
            DealsIngParaProces[newReg].rutCliente = datSPMcambioCDD[idx].rutCliente;
            DealsIngParaProces[newReg].nombreCliente = datSPMcambioCDD[idx].nombreCliente;
            DealsIngParaProces[newReg].numeroDeal = datSPMcambioCDD[idx].numeroDeal;
            DealsIngParaProces[newReg].moneda1 = datSPMcambioCDD[idx].moneda1;
            DealsIngParaProces[newReg].moneda2 = datSPMcambioCDD[idx].moneda2;
            DealsIngParaProces[newReg].precioPoolMoneda1 = datSPMcambioCDD[idx].precioPoolMoneda1;
            DealsIngParaProces[newReg].precioPoolMoneda2 = datSPMcambioCDD[idx].precioPoolMoneda2;
            DealsIngParaProces[newReg].precioCliente = datSPMcambioCDD[idx].precioCliente;
            DealsIngParaProces[newReg].fechaValuta1 = datSPMcambioCDD[idx].fechaValuta1;
            DealsIngParaProces[newReg].fechaValuta2 = datSPMcambioCDD[idx].fechaValuta2;
            DealsIngParaProces[newReg].codigoBancoCentral = datSPMcambioCDD[idx].codigoBancoCentral;
            DealsIngParaProces[newReg].montoBancoRecibe = datSPMcambioCDD[idx].montoBancoRecibe;
            DealsIngParaProces[newReg].montoClienteRecibe = datSPMcambioCDD[idx].montoClienteRecibe;
            DealsIngParaProces[newReg].delta = datSPMcambioCDD[idx].delta;
            DealsIngParaProces[newReg].fechaTransaccion = datSPMcambioCDD[idx].fechaTransaccion;
            DealsIngParaProces[newReg].tipoCambioPizarra = datSPMcambioCDD[idx].tipoCambioPizarra;
            DealsIngParaProces[newReg].tipoTransaccion = datSPMcambioCDD[idx].tipoTransaccion;
            DealsIngParaProces[newReg].codigoEstadoDeal = datSPMcambioCDD[idx].codigoEstadoDeal;
            DealsIngParaProces[newReg].codigoFormaPago1 = datSPMcambioCDD[idx].codigoFormaPago1;
            DealsIngParaProces[newReg].codigoFormaPago2 = datSPMcambioCDD[idx].codigoFormaPago2;
            DealsIngParaProces[newReg].codigoEstadoPago = datSPMcambioCDD[idx].codigoEstadoPago;
            DealsIngParaProces[newReg].codigoReferenciaComex = datSPMcambioCDD[idx].codigoReferenciaComex;
            DealsIngParaProces[newReg].codigoOrigenCarga = datSPMcambioCDD[idx].codigoOrigenCarga;
            DealsIngParaProces[newReg].codigoTipoOperacion = datSPMcambioCDD[idx].codigoTipoOperacion;
            DealsIngParaProces[newReg].codigoEstadoContable = datSPMcambioCDD[idx].codigoEstadoContable;
            DealsIngParaProces[newReg].ultimoNumeroTransitoria = datSPMcambioCDD[idx].ultimoNumeroTransitoria;
            DealsIngParaProces[newReg].ultimoNumeroContingente = datSPMcambioCDD[idx].ultimoNumeroContingente;
            DealsIngParaProces[newReg].txtcodigoOrigenCarga = datSPMcambioCDD[idx].txtcodigoOrigenCarga;
            DealsIngParaProces[newReg].stFechaTransaccion = datSPMcambioCDD[idx].stFechaTransaccion;
            DealsIngParaProces[newReg].stFechaValuta1 = datSPMcambioCDD[idx].stFechaValuta1;
            DealsIngParaProces[newReg].stCodigoEstadoContable = datSPMcambioCDD[idx].stCodigoEstadoContable;
            DealsIngParaProces[newReg].stultimoNumeroTransitoria = datSPMcambioCDD[idx].stultimoNumeroTransitoria;
            DealsIngParaProces[newReg].stultimoNumeroContingente = datSPMcambioCDD[idx].stultimoNumeroContingente;
            DealsIngParaProces[newReg].stCodigoBancoCentral = datSPMcambioCDD[idx].stCodigoBancoCentral;
            DealsIngParaProces[newReg].stPrecioCliente = datSPMcambioCDD[idx].stPrecioCliente;
            DealsIngParaProces[newReg].stMontoBancoRecibe = datSPMcambioCDD[idx].stMontoBancoRecibe;
            DealsIngParaProces[newReg].stDelta = datSPMcambioCDD[idx].stDelta;
            DealsIngParaProces[newReg].stMontoClienteRecibe = datSPMcambioCDD[idx].stMontoClienteRecibe;
            DealsIngParaProces[newReg].stPrecioPoolMoneda2 = datSPMcambioCDD[idx].stPrecioPoolMoneda2;
            DealsIngParaProces[newReg].stSaldoMoneda1 = datSPMcambioCDD[idx].stSaldoMoneda1;
            DealsIngParaProces[newReg].stSaldoMoneda2 = datSPMcambioCDD[idx].stSaldoMoneda2;
            DealsIngParaProces[newReg].stTipoTransaccion = datSPMcambioCDD[idx].stTipoTransaccion;
            DealsIngParaProces[newReg].intCodTipoTransaccion = datSPMcambioCDD[idx].intCodTipoTransaccion;
            DealsIngParaProces[newReg].AbrevTipTrans = datSPMcambioCDD[idx].AbrevTipTrans;
            DealsIngParaProces[newReg].monedaMuestra = datSPMcambioCDD[idx].monedaMuestra;
            DealsIngParaProces[newReg].CodMoneda = datSPMcambioCDD[idx].CodMoneda;
            DealsIngParaProces[newReg].moneda1_Ingresada = datSPMcambioCDD[idx].moneda1_Ingresada;
            DealsIngParaProces[newReg].moneda2_Ingresada = datSPMcambioCDD[idx].moneda2_Ingresada;
            DealsIngParaProces[newReg].TipoCambio_Ingresado = datSPMcambioCDD[idx].TipoCambio_Ingresado;
            DealsIngParaProces[newReg].Paridad_Ingresada = datSPMcambioCDD[idx].Paridad_Ingresada;
            DealsIngParaProces[newReg].Monto1_Ingresado = datSPMcambioCDD[idx].Monto1_Ingresado;
            DealsIngParaProces[newReg].Monto2_Ingresado = datSPMcambioCDD[idx].Monto2_Ingresado;
            DealsIngParaProces[newReg].MontoEn_Ingresado = datSPMcambioCDD[idx].MontoEn_Ingresado;
            DealsIngParaProces[newReg].precio_final = datSPMcambioCDD[idx].precio_final;
            DealsIngParaProces[newReg].monto_segunda_moneda = datSPMcambioCDD[idx].monto_segunda_moneda;

            DealsIngParaProces[newReg].DeltaOrig = datSPMcambioCDD[idx].DeltaOrig;

            DealsIngParaProces[newReg].txtCbPais1_Ingresado = datSPMcambioCDD[idx].txtCbPais1_Ingresado;
            DealsIngParaProces[newReg].txtCbPais2_Ingresado = datSPMcambioCDD[idx].txtCbPais2_Ingresado;
            DealsIngParaProces[newReg].txtCbMoneda1_Ingresado = datSPMcambioCDD[idx].txtCbMoneda1_Ingresado;
            DealsIngParaProces[newReg].txtCbMoneda2_Ingresado = datSPMcambioCDD[idx].txtCbMoneda2_Ingresado;

            DealsIngParaProces[newReg].TipoIngreso = datSPMcambioCDD[idx].TipoIngreso;
        }
        public List<Mcambio_Consulta_Deals_Disponible> ClonarClases(List<DealsIngresadosParaProcesar> DealsIngParaProces, string PagOri) 
        {
            List<Mcambio_Consulta_Deals_Disponible> NewdatSPMcambioCDD = new List<Mcambio_Consulta_Deals_Disponible>();
            double TotMonto = 0;
            double MaxVal = 0;
            double ValIng = 0;
            string CodBcoCral_MaxVal = "";
            string StCodBcoCral_MaxVal = "";
            string ValidarCodBcoCral = "";
            double TotMon1 = 0;
            double TotMon2 = 0;
            int pos = 0;
            for (int i = 0; i < DealsIngParaProces.Count; i++)
            {
                if (DealsIngParaProces[i].tipoTransaccion == "Ventas" || DealsIngParaProces[i].tipoTransaccion == "ArbVta")
                {
                    ValIng = (PagOri == "ARBITRAJES" ? (double)DealsIngParaProces[i].Monto2_Ingresado : (double)DealsIngParaProces[i].Monto1_Ingresado);
                }
                else
                {
                    ValIng = (double)DealsIngParaProces[i].Monto1_Ingresado;
                }
                TotMonto = TotMonto + ValIng;
                DealsIngParaProces[i].Monto1_Ingresado = (DealsIngParaProces[i].Monto1_Ingresado == null ? 0 : DealsIngParaProces[i].Monto1_Ingresado);
                DealsIngParaProces[i].Monto2_Ingresado = (DealsIngParaProces[i].Monto2_Ingresado == null ? 0 : DealsIngParaProces[i].Monto2_Ingresado);
                TotMon1 = TotMon1 + (double)DealsIngParaProces[i].Monto1_Ingresado;
                TotMon2 = TotMon2 + (double)DealsIngParaProces[i].Monto2_Ingresado;
                if (ValIng > MaxVal || (ValIng == MaxVal && CodBcoCral_MaxVal == ""))
                {
                    MaxVal = ValIng;
                    CodBcoCral_MaxVal = DealsIngParaProces[i].codigoBancoCentral;
                    StCodBcoCral_MaxVal = DealsIngParaProces[i].stCodigoBancoCentral;
                    ValidarCodBcoCral = RecCodBancoCentral(DealsIngParaProces[i].stCodigoBancoCentral, 1);
                    if (CodBcoCral_MaxVal != ValidarCodBcoCral && ValidarCodBcoCral != "")
                    {
                        CodBcoCral_MaxVal = ValidarCodBcoCral;
                    }
                    pos = i;
                }
            }
            NewdatSPMcambioCDD.Add(new Mcambio_Consulta_Deals_Disponible());
            //actualizamos el registro con los valores finales para poder hacer una sola ejecucion de la clase de grabado
            DealsIngParaProces[pos].delta = DealsIngParaProces[pos].DeltaOrig;
            DealsIngParaProces[pos].stDelta = DealsIngParaProces[pos].DeltaOrig.ToString();
            string stFecHoy = DateTime.Parse(DateTime.Now.ToString()).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime fecHoy = DateTime.Parse(stFecHoy);
            var p_fechaValuta = GetdateMasXDiasHabiles(fecHoy, ParamGeneralGetdateMasXDiasHabiles);
            FechaValutaFinal = p_fechaValuta;
            if (DealsIngParaProces[pos].tipoTransaccion == "Ventas" || DealsIngParaProces[pos].tipoTransaccion == "ArbVta")
            {
                if (PagOri == "ARBITRAJES")
                {
                    if (DealsIngParaProces[pos].TipoIngreso == "P")
                    {
                        DealsIngParaProces[pos].DeltaOrig = TotMon2;
                    }
                    else
                    {
                        double PorcTole = (double)DealsIngParaProces[pos].DeltaOrig * ParamGeneralTolerancia_X_Porc;
                        double DeltaOrig_mas_Tole = (double)DealsIngParaProces[pos].DeltaOrig + PorcTole;
                        TotMon2 = (TotMon2 > DeltaOrig_mas_Tole ? (double)DealsIngParaProces[pos].DeltaOrig : TotMon2);
                    }
                }
                else
                {
                    if (DealsIngParaProces[pos].TipoIngreso != "P")
                    {
                        double PorcTole = (double)DealsIngParaProces[pos].DeltaOrig * ParamGeneralTolerancia_X_Porc;
                        double DeltaOrig_mas_Tole = (double)DealsIngParaProces[pos].DeltaOrig + PorcTole;
                        TotMon1 = (TotMon1 > DeltaOrig_mas_Tole ? (double)DealsIngParaProces[pos].DeltaOrig : TotMon1);
                    }
                    else
                    {
                        DealsIngParaProces[pos].DeltaOrig = TotMon1;
                    }
                }
            }
            else
            {
                if (DealsIngParaProces[pos].TipoIngreso != "P")
                {
                    double PorcTole = (double)DealsIngParaProces[pos].DeltaOrig * ParamGeneralTolerancia_X_Porc;
                    double DeltaOrig_mas_Tole = (double)DealsIngParaProces[pos].DeltaOrig + PorcTole;
                    TotMon1 = (TotMon1 > DeltaOrig_mas_Tole ? (double)DealsIngParaProces[pos].DeltaOrig : TotMon1);
                }
                else
                {
                    DealsIngParaProces[pos].DeltaOrig = TotMon1;
                }
            }
            DealsIngParaProces[pos].Monto1_Ingresado = TotMon1;
            DealsIngParaProces[pos].Monto2_Ingresado = TotMon2;
            DealsIngParaProces[pos].codigoBancoCentral = CodBcoCral_MaxVal;
            DealsIngParaProces[pos].stCodigoBancoCentral = StCodBcoCral_MaxVal;
            pasarValoresEntreClases(NewdatSPMcambioCDD, DealsIngParaProces, 0, pos);
            return NewdatSPMcambioCDD;
        }
        private void pasarValoresEntreClases(List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, List<DealsIngresadosParaProcesar> DealsIngParaProces, int newReg, int idx) 
        {
            datSPMcambioCDD[newReg].rutCliente = DealsIngParaProces[idx].rutCliente;
            datSPMcambioCDD[newReg].nombreCliente = DealsIngParaProces[idx].nombreCliente;
            datSPMcambioCDD[newReg].numeroDeal = DealsIngParaProces[idx].numeroDeal;
            datSPMcambioCDD[newReg].moneda1 = DealsIngParaProces[idx].moneda1;
            datSPMcambioCDD[newReg].moneda2 = DealsIngParaProces[idx].moneda2;
            datSPMcambioCDD[newReg].precioPoolMoneda1 = DealsIngParaProces[idx].precioPoolMoneda1;
            datSPMcambioCDD[newReg].precioPoolMoneda2 = DealsIngParaProces[idx].precioPoolMoneda2;
            datSPMcambioCDD[newReg].precioCliente = DealsIngParaProces[idx].precioCliente;
            datSPMcambioCDD[newReg].fechaValuta1 = DealsIngParaProces[idx].fechaValuta1;
            datSPMcambioCDD[newReg].fechaValuta2 = DealsIngParaProces[idx].fechaValuta2;
            datSPMcambioCDD[newReg].codigoBancoCentral = DealsIngParaProces[idx].codigoBancoCentral;
            datSPMcambioCDD[newReg].montoBancoRecibe = DealsIngParaProces[idx].montoBancoRecibe;
            datSPMcambioCDD[newReg].montoClienteRecibe = DealsIngParaProces[idx].montoClienteRecibe;
            datSPMcambioCDD[newReg].delta = DealsIngParaProces[idx].delta;
            datSPMcambioCDD[newReg].fechaTransaccion = DealsIngParaProces[idx].fechaTransaccion;
            datSPMcambioCDD[newReg].tipoCambioPizarra = DealsIngParaProces[idx].tipoCambioPizarra;
            datSPMcambioCDD[newReg].tipoTransaccion = DealsIngParaProces[idx].tipoTransaccion;
            datSPMcambioCDD[newReg].codigoEstadoDeal = DealsIngParaProces[idx].codigoEstadoDeal;
            datSPMcambioCDD[newReg].codigoFormaPago1 = DealsIngParaProces[idx].codigoFormaPago1;
            datSPMcambioCDD[newReg].codigoFormaPago2 = DealsIngParaProces[idx].codigoFormaPago2;
            datSPMcambioCDD[newReg].codigoEstadoPago = DealsIngParaProces[idx].codigoEstadoPago;
            datSPMcambioCDD[newReg].codigoReferenciaComex = DealsIngParaProces[idx].codigoReferenciaComex;
            datSPMcambioCDD[newReg].codigoOrigenCarga = DealsIngParaProces[idx].codigoOrigenCarga;
            datSPMcambioCDD[newReg].codigoTipoOperacion = DealsIngParaProces[idx].codigoTipoOperacion;
            datSPMcambioCDD[newReg].codigoEstadoContable = DealsIngParaProces[idx].codigoEstadoContable;
            datSPMcambioCDD[newReg].ultimoNumeroTransitoria = DealsIngParaProces[idx].ultimoNumeroTransitoria;
            datSPMcambioCDD[newReg].ultimoNumeroContingente = DealsIngParaProces[idx].ultimoNumeroContingente;
            datSPMcambioCDD[newReg].txtcodigoOrigenCarga = DealsIngParaProces[idx].txtcodigoOrigenCarga;
            datSPMcambioCDD[newReg].stFechaTransaccion = DealsIngParaProces[idx].stFechaTransaccion;
            datSPMcambioCDD[newReg].stFechaValuta1 = DealsIngParaProces[idx].stFechaValuta1;
            datSPMcambioCDD[newReg].stCodigoEstadoContable = DealsIngParaProces[idx].stCodigoEstadoContable;
            datSPMcambioCDD[newReg].stultimoNumeroTransitoria = DealsIngParaProces[idx].stultimoNumeroTransitoria;
            datSPMcambioCDD[newReg].stultimoNumeroContingente = DealsIngParaProces[idx].stultimoNumeroContingente;
            datSPMcambioCDD[newReg].stCodigoBancoCentral = DealsIngParaProces[idx].stCodigoBancoCentral;
            datSPMcambioCDD[newReg].stPrecioCliente = DealsIngParaProces[idx].stPrecioCliente;
            datSPMcambioCDD[newReg].stMontoBancoRecibe = DealsIngParaProces[idx].stMontoBancoRecibe;
            datSPMcambioCDD[newReg].stDelta = DealsIngParaProces[idx].stDelta;
            datSPMcambioCDD[newReg].stMontoClienteRecibe = DealsIngParaProces[idx].stMontoClienteRecibe;
            datSPMcambioCDD[newReg].stPrecioPoolMoneda2 = DealsIngParaProces[idx].stPrecioPoolMoneda2;
            datSPMcambioCDD[newReg].stSaldoMoneda1 = DealsIngParaProces[idx].stSaldoMoneda1;
            datSPMcambioCDD[newReg].stSaldoMoneda2 = DealsIngParaProces[idx].stSaldoMoneda2;
            datSPMcambioCDD[newReg].stTipoTransaccion = DealsIngParaProces[idx].stTipoTransaccion;
            datSPMcambioCDD[newReg].intCodTipoTransaccion = DealsIngParaProces[idx].intCodTipoTransaccion;
            datSPMcambioCDD[newReg].AbrevTipTrans = DealsIngParaProces[idx].AbrevTipTrans;
            datSPMcambioCDD[newReg].monedaMuestra = DealsIngParaProces[idx].monedaMuestra;
            datSPMcambioCDD[newReg].CodMoneda = DealsIngParaProces[idx].CodMoneda;
            datSPMcambioCDD[newReg].moneda1_Ingresada = DealsIngParaProces[idx].moneda1_Ingresada;
            datSPMcambioCDD[newReg].moneda2_Ingresada = DealsIngParaProces[idx].moneda2_Ingresada;
            datSPMcambioCDD[newReg].TipoCambio_Ingresado = DealsIngParaProces[idx].TipoCambio_Ingresado;
            datSPMcambioCDD[newReg].Paridad_Ingresada = DealsIngParaProces[idx].Paridad_Ingresada;
            datSPMcambioCDD[newReg].Monto1_Ingresado = DealsIngParaProces[idx].Monto1_Ingresado;
            datSPMcambioCDD[newReg].Monto2_Ingresado = DealsIngParaProces[idx].Monto2_Ingresado;
            datSPMcambioCDD[newReg].MontoEn_Ingresado = DealsIngParaProces[idx].MontoEn_Ingresado;
            datSPMcambioCDD[newReg].precio_final = DealsIngParaProces[idx].precio_final;
            datSPMcambioCDD[newReg].monto_segunda_moneda = DealsIngParaProces[idx].monto_segunda_moneda;

            datSPMcambioCDD[newReg].DeltaOrig = DealsIngParaProces[idx].DeltaOrig;

            datSPMcambioCDD[newReg].txtCbPais1_Ingresado = DealsIngParaProces[idx].txtCbPais1_Ingresado;
            datSPMcambioCDD[newReg].txtCbPais2_Ingresado = DealsIngParaProces[idx].txtCbPais2_Ingresado;
            datSPMcambioCDD[newReg].txtCbMoneda1_Ingresado = DealsIngParaProces[idx].txtCbMoneda1_Ingresado;
            datSPMcambioCDD[newReg].txtCbMoneda2_Ingresado = DealsIngParaProces[idx].txtCbMoneda2_Ingresado;

            datSPMcambioCDD[newReg].TipoIngreso = DealsIngParaProces[idx].TipoIngreso;
        }
        public int COMEX_DatosIngresados(InitializationObject initObject, List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, string PagOri, int idx, List<DealsIngresadosParaProcesar> DealsIngParaProces) 
        {
            UI_Frm_Comercio_Invisibles FrmCOMINV = initObject.Frm_Comercio_Invisible;
            UI_Frm_PlvSO FrmPlvSO = initObject.Frm_PlvSO;
            UI_Frm_Arbitrajes FrmArbitraje = initObject.Frm_Arbitrajes;
            DealsIngParaProces.Add(new DealsIngresadosParaProcesar());
            int newReg = DealsIngParaProces.Count() - 1;
            int swIngManual;
            if (idx == -1 && newReg == 0)
            {
                swIngManual = 1;
                var rutclie = formatearRut(initObject.MODGCVD.VgCvd.rutcli, true);
                DealsIngParaProces[newReg].rutCliente = rutclie;
                DealsIngParaProces[newReg].TipoIngreso = "P";
                DealsIngParaProces[newReg].numeroDeal = (int)NewIdChileFx;
            }
            else
            {
                swIngManual = 0;
                if (idx == -1)
                {
                    idx = 0;
                }
                IgualarRegistros(datSPMcambioCDD, DealsIngParaProces, newReg, idx);
                DealsIngParaProces[newReg].TipoIngreso = "S";
            }
            if (PagOri == PaginaOrigen.COMINV.ToString())
            {
                if (datSPMcambioCDD != null)
                {
                    if (datSPMcambioCDD.Count() == 1)
                    {
                        idx = 0;
                    }
                }
                Lista_Moneda_Pais MonedaPaisDealSeleccionado = GetParamComexMonedaPais(FrmCOMINV.Cb_Moneda.Value.ToString(), "CodMoneda");
                string tipoTransaccion;
                string AbreTipTrans;
                if (swIngManual == 1)
                {
                    if (FrmCOMINV.Cb_Divisa.ListIndex == 1 || FrmCOMINV.Cb_Divisa.ListIndex == 3)
                    {
                        tipoTransaccion = "Ventas";
                        AbreTipTrans = "V";
                    }
                    else if (FrmCOMINV.Cb_Divisa.ListIndex == 0 || FrmCOMINV.Cb_Divisa.ListIndex == 2)
                    {
                        tipoTransaccion = "Compras";
                        AbreTipTrans = "C";
                    }
                    else
                    {
                        tipoTransaccion = "Compras";
                        AbreTipTrans = "C";
                    }
                    DealsIngParaProces[newReg].tipoTransaccion = tipoTransaccion;
                    DealsIngParaProces[newReg].AbrevTipTrans = AbreTipTrans;
                    int IdxSel = string.IsNullOrEmpty(FrmCOMINV.Lt_Tcp.ListIndex.ToString()) ? 0 : FrmCOMINV.Lt_Tcp.ListIndex;
                    DealsIngParaProces[newReg].codigoBancoCentral = FrmCOMINV.Lt_Tcp.Items[IdxSel].Value.Substring(0, FrmCOMINV.Lt_Tcp.Items[IdxSel].Value.IndexOf("-"));
                }
                else
                {
                    tipoTransaccion = datSPMcambioCDD[idx].tipoTransaccion;
                }
                if (tipoTransaccion == "Ventas") 
                {
                    if (swIngManual == 1)
                    {
                        DealsIngParaProces[newReg].moneda2_Ingresada = MonedaPaisDealSeleccionado.SiglaMoneda;
                        DealsIngParaProces[newReg].moneda1_Ingresada = "CLP";
                    }
                    else
                    {
                        DealsIngParaProces[newReg].moneda2_Ingresada = MonedaPaisDealSeleccionado.SiglaMoneda;
                        DealsIngParaProces[newReg].moneda1_Ingresada = datSPMcambioCDD[idx].moneda1;
                    }
                }
                else
                {
                    if (swIngManual == 1)
                    {
                        DealsIngParaProces[newReg].moneda1_Ingresada = MonedaPaisDealSeleccionado.SiglaMoneda;
                        DealsIngParaProces[newReg].moneda2_Ingresada = "CLP";
                    }
                    else
                    {
                        DealsIngParaProces[newReg].moneda1_Ingresada = MonedaPaisDealSeleccionado.SiglaMoneda;
                        DealsIngParaProces[newReg].moneda2_Ingresada = datSPMcambioCDD[idx].moneda2;
                    }
                }
                if (swIngManual == 1)
                {
                    DealsIngParaProces[newReg].nombreCliente = initObject.Frm_Participantes.LstPartys.SelectedItem.Replace("Cliente\t", "");
                } else if (string.IsNullOrEmpty(datSPMcambioCDD[idx].nombreCliente))
                {
                    DealsIngParaProces[newReg].nombreCliente = initObject.Frm_Participantes.LstPartys.SelectedItem.Replace("Cliente\t", "");
                }
                DealsIngParaProces[newReg].CodMoneda = MonedaPaisDealSeleccionado.CodMoneda;
                DealsIngParaProces[newReg].TipoCambio_Ingresado = double.Parse(FrmCOMINV.Tx_MtoCV[1].Text);
                DealsIngParaProces[newReg].Paridad_Ingresada = double.Parse(FrmCOMINV.Tx_MtoCV[3].Text);
                DealsIngParaProces[newReg].Monto1_Ingresado = double.Parse(FrmCOMINV.Tx_MtoCV[0].Text);
                DealsIngParaProces[newReg].MontoEn_Ingresado = double.Parse(FrmCOMINV.Tx_MtoCV[2].Text);
                DealsIngParaProces[newReg].stCodigoBancoCentral = FrmCOMINV.Lt_Tcp.Text.ToString();
                DealsIngParaProces[newReg].txtCbMoneda1_Ingresado = FrmCOMINV.Cb_Moneda.Text.ToString();
                DealsIngParaProces[newReg].txtCbPais1_Ingresado = FrmCOMINV.Cb_Pais.Text.ToString();
            }

            if (PagOri == PaginaOrigen.PlvSO.ToString())
            {
                Lista_Moneda_Pais MonedaPaisDealSeleccionado = GetParamComexMonedaPais(FrmPlvSO.Cb_Moneda.Value.ToString(), "CodMoneda");
                string tipoTransaccion;
                string AbreTipTrans;
                if (swIngManual == 1)
                {
                    tipoTransaccion = "Ventas";
                    AbreTipTrans = "V";
                    DealsIngParaProces[newReg].tipoTransaccion = tipoTransaccion;
                    DealsIngParaProces[newReg].AbrevTipTrans = AbreTipTrans;
                    DealsIngParaProces[newReg].codigoBancoCentral = ParamGeneral_PlvSO_CodComBcoCentral;
                    DealsIngParaProces[newReg].stCodigoBancoCentral = ParamGeneral_PlvSO_CodComBcoCentral;
                }
                else
                {
                    tipoTransaccion = datSPMcambioCDD[idx].tipoTransaccion;
                    DealsIngParaProces[newReg].stCodigoBancoCentral = datSPMcambioCDD[idx].codigoBancoCentral;
                }
                if (tipoTransaccion == "Ventas")
                {
                    if (swIngManual == 1)
                    {
                        DealsIngParaProces[newReg].moneda1_Ingresada = MonedaPaisDealSeleccionado.SiglaMoneda;
                        if (MonedaPaisDealSeleccionado.SiglaMoneda != "USD")
                        {
                            DealsIngParaProces[newReg].moneda2_Ingresada = "USD";
                        }
                        else
                        {
                            DealsIngParaProces[newReg].moneda2_Ingresada = "CLP";
                        }
                    }
                    else
                    {
                        DealsIngParaProces[newReg].moneda1_Ingresada = datSPMcambioCDD[idx].moneda1;
                        if (datSPMcambioCDD[idx].moneda1 != "USD")
                        {
                            DealsIngParaProces[newReg].moneda2_Ingresada = "USD";
                        }
                        else
                        {
                            DealsIngParaProces[newReg].moneda2_Ingresada = "CLP";
                        }
                    }
                }
                if (swIngManual == 1)
                {
                    DealsIngParaProces[newReg].nombreCliente = initObject.Frm_Participantes.LstPartys.SelectedItem.Replace("Cliente\t", "");
                }
                else if (string.IsNullOrEmpty(datSPMcambioCDD[idx].nombreCliente))
                {
                    DealsIngParaProces[newReg].nombreCliente = initObject.Frm_Participantes.LstPartys.SelectedItem.Replace("Cliente\t", "");
                }
                DealsIngParaProces[newReg].CodMoneda = MonedaPaisDealSeleccionado.CodMoneda;
                DealsIngParaProces[newReg].TipoCambio_Ingresado = double.Parse(FrmPlvSO.Tx_TipCam.Text);
                DealsIngParaProces[newReg].Paridad_Ingresada = double.Parse(FrmPlvSO.Tx_Paridad.Text);
                DealsIngParaProces[newReg].Monto1_Ingresado = double.Parse(FrmPlvSO.Tx_MtoFob.Text);
                DealsIngParaProces[newReg].MontoEn_Ingresado = double.Parse(FrmPlvSO.Tx_MtoFob.Text);
                FrmPlvSO.Tx_CodPag.Text = ParamGeneralCodFormaPago.ToString();
            }

            if (PagOri == PaginaOrigen.ARBITRAJES.ToString())
            {
                Lista_Moneda_Pais MonedaCompra = GetParamComexMonedaPais(FrmArbitraje.Cb_Moneda_Compra.Value.ToString(), "CodMoneda");
                Lista_Moneda_Pais MonedaVenta = GetParamComexMonedaPais(FrmArbitraje.Cb_Moneda_Venta.Value.ToString(), "CodMoneda");
                string tipoTransaccion;
                string AbreTipTrans;
                if (swIngManual == 1)
                {
                    if (MonedaCompra.SiglaMoneda == "USD")
                    {
                        tipoTransaccion = "Ventas";
                        AbreTipTrans = "V";
                        DealsIngParaProces[newReg].tipoTransaccion = tipoTransaccion;
                        DealsIngParaProces[newReg].AbrevTipTrans = AbreTipTrans;
                        DealsIngParaProces[newReg].codigoBancoCentral = ParamGeneral_ArbitrajeVenta_CodComBcoCentral;
                        DealsIngParaProces[newReg].stCodigoBancoCentral = ParamGeneral_ArbitrajeVenta_CodComBcoCentral;
                    }
                    else
                    {
                        tipoTransaccion = "Compras";
                        AbreTipTrans = "C";
                        DealsIngParaProces[newReg].tipoTransaccion = tipoTransaccion;
                        DealsIngParaProces[newReg].AbrevTipTrans = AbreTipTrans;
                        DealsIngParaProces[newReg].codigoBancoCentral = ParamGeneral_ArbitrajeCompra_CodComBcoCentral;
                        DealsIngParaProces[newReg].stCodigoBancoCentral = ParamGeneral_ArbitrajeCompra_CodComBcoCentral;
                    }
                }
                else
                {
                    if (datSPMcambioCDD[idx].tipoTransaccion == "ArbVta")
                    {
                        AbreTipTrans = "V";
                        tipoTransaccion = "Ventas";
                    }
                    else
                    {
                        AbreTipTrans = "C";
                        tipoTransaccion = "Compras";
                    }
                    datSPMcambioCDD[idx].stCodigoBancoCentral = datSPMcambioCDD[idx].codigoBancoCentral;
                }
                if (swIngManual == 1)
                {
                    DealsIngParaProces[newReg].moneda1_Ingresada = MonedaCompra.SiglaMoneda;
                    DealsIngParaProces[newReg].moneda2_Ingresada = MonedaVenta.SiglaMoneda;
                }
                else
                {
                    DealsIngParaProces[newReg].moneda1_Ingresada = datSPMcambioCDD[idx].moneda1;
                    DealsIngParaProces[newReg].moneda2_Ingresada = datSPMcambioCDD[idx].moneda2;
                }
                DealsIngParaProces[newReg].nombreCliente = initObject.Frm_Participantes.LstPartys.SelectedItem.Replace("Cliente\t", "");
                if (MonedaCompra.SiglaMoneda == "USD")
                {
                    DealsIngParaProces[newReg].CodMoneda = MonedaCompra.CodMoneda;
                }
                else
                {
                    DealsIngParaProces[newReg].CodMoneda = MonedaVenta.CodMoneda;
                }
                DealsIngParaProces[newReg].TipoCambio_Ingresado = double.Parse(FrmArbitraje.Tx_Mtoarb[0].Text);
                DealsIngParaProces[newReg].Paridad_Ingresada = double.Parse(FrmArbitraje.Tx_Mtoarb[1].Text);
                DealsIngParaProces[newReg].Monto1_Ingresado = double.Parse(FrmArbitraje.Tx_Mtoarb[2].Text);
                DealsIngParaProces[newReg].Monto2_Ingresado = double.Parse(FrmArbitraje.Tx_Mtoarb[3].Text);
                DealsIngParaProces[newReg].MontoEn_Ingresado = 0;
            }
            if (swIngManual == 1)
            {
                return idx;
            }
            else
            {
                return -1;
            }
        }
        public string TipoIngreso(List<DealsIngresadosParaProcesar> DealsIngParaProces) 
        {
            if (DealsIngParaProces != null)
            {
                if (DealsIngParaProces.Count() >= 1)
                {
                    return DealsIngParaProces[0].TipoIngreso;
                }
                
            }
            return "";
        }
        public double ConsultaPrecios2(InitializationObject initObject, string PagOri, string MontoIngresado) 
        {
            UI_Frm_Comercio_Invisibles FrmCOMINV = initObject.Frm_Comercio_Invisible;
            UI_Frm_PlvSO FrmPlvSO = initObject.Frm_PlvSO;
            UI_Frm_Arbitrajes FrmArbitraje = initObject.Frm_Arbitrajes;
            Lista_Moneda_Pais MonedaPaisDealSeleccionado = new Lista_Moneda_Pais();
            Lista_Moneda_Pais MonedaCompra = new Lista_Moneda_Pais();
            Lista_Moneda_Pais MonedaVenta = new Lista_Moneda_Pais();
            if (PagOri == PaginaOrigen.COMINV.ToString())
            {
                MonedaPaisDealSeleccionado = GetParamComexMonedaPais(FrmCOMINV.Cb_Moneda.Value.ToString(), "CodMoneda");
            }
            if (PagOri == PaginaOrigen.PlvSO.ToString())
            {
                MonedaPaisDealSeleccionado = GetParamComexMonedaPais(FrmPlvSO.Cb_Moneda.Value.ToString(), "CodMoneda");
            }
            if (PagOri == PaginaOrigen.ARBITRAJES.ToString())
            {
                MonedaCompra = GetParamComexMonedaPais(FrmArbitraje.Cb_Moneda_Compra.Value.ToString(), "CodMoneda");
                MonedaVenta = GetParamComexMonedaPais(FrmArbitraje.Cb_Moneda_Venta.Value.ToString(), "CodMoneda");
            }
            
            var rutclie = formatearRut(initObject.MODGCVD.VgCvd.rutcli, false);
            var rutClieConDig = formatearRut(initObject.MODGCVD.VgCvd.rutcli, true);
            string nomClie = initObject.Frm_Participantes.LstPartys.SelectedItem.Replace("Cliente\t", "");
            string codBcoCentral = "";
            using (McambioEntities context = new McambioEntities())
            {
                string mon1="", mon2="";
                string AbreTipTrans="";
                if (PagOri == PaginaOrigen.COMINV.ToString())
                {
                    codBcoCentral = RecCodBancoCentral(FrmCOMINV.Lt_Tcp.Text, 1);
                    FrmCOMINV.Errors.Clear();
                    if (MonedaPaisDealSeleccionado.SiglaMoneda == null)
                    {
                        return 0;
                    }
                    if (FrmCOMINV.Cb_Divisa.ListIndex == 1 || FrmCOMINV.Cb_Divisa.ListIndex == 3)
                    {
                        AbreTipTrans = "V";
                        mon1 = MonedaPaisDealSeleccionado.SiglaMoneda;
                        if (mon1 != "USD")
                        {
                            mon2 = "USD";
                        }
                        else
                        {
                            mon2 = "CLP";
                        }
                    }
                    else if (FrmCOMINV.Cb_Divisa.ListIndex == 0 || FrmCOMINV.Cb_Divisa.ListIndex == 2)
                    {
                        AbreTipTrans = "C";
                        mon1 = MonedaPaisDealSeleccionado.SiglaMoneda;
                        mon2 = "CLP";
                    }
                    else
                    {
                        AbreTipTrans = "C";
                        mon1 = MonedaPaisDealSeleccionado.SiglaMoneda;
                        mon2 = "CLP";
                    }
                }
                if (PagOri == PaginaOrigen.PlvSO.ToString())
                {
                    AbreTipTrans = "V";
                    mon1 = MonedaPaisDealSeleccionado.SiglaMoneda;
                    if (mon1 != "USD")
                    {
                        mon2 = "USD";
                    }
                    else
                    {
                        mon2 = "CLP";
                    }
                    codBcoCentral = ParamGeneral_PlvSO_CodComBcoCentral;
                }
                if (PagOri == PaginaOrigen.ARBITRAJES.ToString())
                {
                    if (MonedaCompra.SiglaMoneda == "USD")  //Venta
                    {
                        AbreTipTrans = "V";
                        mon1 = MonedaVenta.SiglaMoneda;
                        mon2 = MonedaCompra.SiglaMoneda;
                        codBcoCentral = ParamGeneral_ArbitrajeVenta_CodComBcoCentral;
                    }
                    else
                    {
                        AbreTipTrans = "C";
                        mon1 = MonedaCompra.SiglaMoneda;
                        mon2 = MonedaVenta.SiglaMoneda;
                        codBcoCentral = ParamGeneral_ArbitrajeCompra_CodComBcoCentral;
                    }
                    

                }
                if (String.IsNullOrEmpty(MontoIngresado))
                {
                    MontoIngresado = "0";
                }
                //Validacion: Validar vigencia de precio (pro_cvd_ni_consulta_precios2)
                string stFecHoy = DateTime.Parse(DateTime.Now.ToString()).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                DateTime fecHoy = DateTime.Parse(stFecHoy);
                var p_fechaValuta = GetdateMasXDiasHabiles(fecHoy, ParamGeneralGetdateMasXDiasHabiles);
                FechaValutaFinal = p_fechaValuta;
                context.Database.Connection.Open();
                SqlParameter[] Parameters = {
                                                new SqlParameter("@gb_fecha", fecHoy),
                                                new SqlParameter("@gb_compra_venta", AbreTipTrans),
                                                new SqlParameter("@gb_moneda1", mon1),
                                                new SqlParameter("@gb_moneda2", mon2),
                                                new SqlParameter("@gb_monto_cliente", double.Parse(MontoIngresado)),
                                                new SqlParameter("@gb_rut_cliente", rutclie),
                                                new SqlParameter("@gb_canal", ParamGeneralCanal),
                                                new SqlParameter("@gb_segmento", ParamGeneral_ValorDefaultSegmento),
                                                new SqlParameter("@gb_valuta", p_fechaValuta),
                                                new SqlParameter("@ls_codret", ""),
                                                new SqlParameter("@codret", SqlDbType.Char, 5)
                                                {
                                                    Direction = System.Data.ParameterDirection.Output
                                                },
                                                new SqlParameter("@ls_msgret", ""),
                                                new SqlParameter("@msgret", SqlDbType.VarChar, 150)
                                                {
                                                    Direction = System.Data.ParameterDirection.Output
                                                }
                                            };
                var varMcambio_SP_Consulta_Precios = context.Database.SqlQuery<Mcambio_SP_Consulta_Precios2>(
                                Mcambio_SP_Cvd_Consulta_Precios.ToString() + "  @gb_fecha,@gb_compra_venta,@gb_moneda1,@gb_moneda2,@gb_monto_cliente,@gb_rut_cliente,@gb_canal,@gb_segmento,@gb_valuta, @ls_codret = @codret OUTPUT, @ls_msgret = @msgret OUTPUT",
                                Parameters).ToList();
                context.Database.Connection.Close();
                if (Parameters[10].SqlValue.ToString() != "00000")
                {
                    if (PagOri == PaginaOrigen.COMINV.ToString())
                    {
                        MsgsValidarIngreso(initObject, 5, PagOri, Parameters[12].SqlValue.ToString());
                    }
                    else if (PagOri == PaginaOrigen.PlvSO.ToString())
                    {
                        MsgsValidarIngreso(initObject, 5, PagOri, Parameters[12].SqlValue.ToString());
                    }
                    else if (PagOri == PaginaOrigen.ARBITRAJES.ToString())
                    {
                        MsgsValidarIngreso(initObject, 5, PagOri, Parameters[12].SqlValue.ToString());
                    }
                    ParaMsgSonicBoom = Parameters[12].SqlValue.ToString();  //"Tipo de cambio valor pizarra: " +
                    ResPrecioFinal = 0;
                    valMontoSegundaMoneda = 0;
                }
                else
                {
                    ParaMsgSonicBoom = "";
                    ResPrecioFinal = (double)varMcambio_SP_Consulta_Precios[0].precio_final;
                    valMontoSegundaMoneda = (double)varMcambio_SP_Consulta_Precios[0].monto_segunda_moneda;
                    string IdentConsult = varMcambio_SP_Consulta_Precios[0].identificador_consulta;
                    //NewIdChileFx = CrearDealChileFxTesoreria(initObject, rutClieConDig, nomClie, mon1, mon2, ResPrecioFinal, p_fechaValuta,
                    //                              codBcoCentral, fecHoy, valMontoSegundaMoneda, AbreTipTrans, IdentConsult, PagOri);
                    NewIdChileFx = CrearDealChileFxTesoreria(initObject, rutClieConDig, nomClie, mon1, mon2, ResPrecioFinal, p_fechaValuta,
                                                  codBcoCentral, fecHoy, double.Parse(MontoIngresado), AbreTipTrans, IdentConsult, PagOri);
                    if (NewIdChileFx == 0)
                    {
                        ResPrecioFinal = 0;
                    }
                }
            }
            return ResPrecioFinal;
        }
        private Int64 CrearDealChileFxTesoreria(InitializationObject initObject, string rutClieConDig, string nomClie, string mon1, string mon2, double PrecioFinal, System.DateTime p_fechaValuta,
                                                 string codBcoCentral, DateTime fecHoy, double MontoSegundaMoneda, string AbreTipTrans, string IdentConsult, string PagOri) 
        {
            UI_Frm_Comercio_Invisibles FrmCOMINV = initObject.Frm_Comercio_Invisible;
            UI_Frm_PlvSO FrmPlvSO = initObject.Frm_PlvSO;
            UI_Frm_Arbitrajes FrmArbitraje = initObject.Frm_Arbitrajes;
            using (McambioEntities context = new McambioEntities())
            {
                //CrearDealChileFxTesoreria
                context.Database.Connection.Open();
                SqlParameter[] ParamCrearDealChileFxTeso = {
                                            new SqlParameter("@p_rutCliente", rutClieConDig),
                                            new SqlParameter("@p_nombreCliente", nomClie),
                                            new SqlParameter("@p_parMoneda1", mon1),
                                            new SqlParameter("@p_parMoneda2", mon2),
                                            new SqlParameter("@p_precioCliente", PrecioFinal),
                                            new SqlParameter("@p_fechaValuta", p_fechaValuta),
                                            new SqlParameter("@p_codBcoCentral", int.Parse(codBcoCentral)),
                                            new SqlParameter("@p_monto_me", MontoSegundaMoneda),
                                            new SqlParameter("@p_fechaTransaccion", fecHoy),
                                            new SqlParameter("@p_tipoCambioPizarra", PrecioFinal),
                                            new SqlParameter("@p_tipoTransaccion", AbreTipTrans),
                                            new SqlParameter("@p_identificadorConsulta", IdentConsult),
                                            new SqlParameter("@ls_folio", ""),
                                            new SqlParameter("@folio", SqlDbType.Int)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            },
                                            new SqlParameter("@ls_msgret", ""),
                                            new SqlParameter("@msgret", SqlDbType.VarChar, 150)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            },
                                            new SqlParameter("@ls_codret", ""),
                                            new SqlParameter("@codret", SqlDbType.Char, 5)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            }
                                        };
                var varMcambio_SP_CrearDealChlFxTeso = context.Database.SqlQuery<Mcambio_SP_CrearDealChlFxTeso>(
                            Mcambio_SP_CreaDealChlFxTeso.ToString() + " @p_rutCliente,@p_nombreCliente,@p_parMoneda1,@p_parMoneda2,@p_precioCliente,@p_fechaValuta,@p_codBcoCentral,@p_monto_me,@p_fechaTransaccion,@p_tipoCambioPizarra,@p_tipoTransaccion,@p_identificadorConsulta,@ls_folio = @folio OUTPUT,@ls_msgret = @msgret OUTPUT,@ls_codret = @codret OUTPUT",
                            ParamCrearDealChileFxTeso).ToList();
                context.Database.Connection.Close();
                if (ParamCrearDealChileFxTeso[17].SqlValue.ToString() != "00000")
                {
                    if (PagOri == PaginaOrigen.COMINV.ToString())
                    {
                        FrmCOMINV.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Critical,
                            Text = ParamCrearDealChileFxTeso[15].SqlValue.ToString() + " (" + ParamCrearDealChileFxTeso[17].SqlValue.ToString() + ")",
                            Title = "Aviso!:   "
                        });
                    }
                    return 0;
                }
                return Int64.Parse(ParamCrearDealChileFxTeso[13].SqlValue.ToString());
            }
        }
        private string SumarMontosIng(List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, int idCampo) 
        {
            double TotalMontoIng = 0;
            foreach (var item in datSPMcambioCDD)
            {
                TotalMontoIng = TotalMontoIng + (idCampo == 1 ? (double)item.Monto1_Ingresado : (double)item.Monto2_Ingresado) ;
            }
            return TotalMontoIng.ToString();
        }
        public void Ejecutar_GuardarCambioEstado(int? DealManual, int? DealActualSel, InitializationObject initObject, List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, List<DealsIngresadosParaProcesar> DealsIngParaProces, string PagOri, int Flag_transferencia, string stOpeSin, string stPrtCli) 
        {
            string TipoIng;
            int sw;
            if (PagOri == "COMINV" || PagOri == "ARBITRAJES" || (PagOri == "PlvSO" && Flag_transferencia == 0))
            {
                datSPMcambioCDD = ClonarClases(DealsIngParaProces, PagOri);
                int pos;
                if (DealManual != null)
                {
                    pos = (int)DealManual;
                    TipoIng = "M";
                }
                else
                {
                    if (DealActualSel == null)
                    {
                        DealActualSel = 1;
                    }
                    else
                    {
                        DealActualSel = 0;
                    }
                    pos = (int)DealActualSel - 1;
                    TipoIng = "S";
                }
                sw = FundTransfer_Guardar_CambioEstado(initObject, datSPMcambioCDD, DealsIngParaProces, pos, TipoIng, PagOri, stOpeSin, stPrtCli);
                //string msg = "";
                //msg = "Código: " + sw.ToString();
                //initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                //{
                //    Type = TipoMensaje.Informacion,
                //    Text = msg.ToString(),
                //    Title = "Comex"
                //});
            }
        }
        public int FundTransfer_Guardar_CambioEstado(InitializationObject initObject, List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, List<DealsIngresadosParaProcesar> DealsIngParaProces, int idx, string TipoIng, string PagOri, string stOpeSin, string stPrtCli) 
        {
            UI_Frm_Ticket frmTicket = initObject.Frm_Ticket;
            int sw = 0;
            if (datSPMcambioCDD != null)
            {
                if (idx == -1)
                {
                    idx = 0;
                }
                //Nos aseguramos que llego correcto el tipo
                if (datSPMcambioCDD[0].TipoIngreso == "P")
                {
                    TipoIng = "M";
                }
                else
                {
                    TipoIng = "S";
                }
            }
            var rutclie = formatearRut(datSPMcambioCDD[idx].rutCliente, false);
            using (McambioEntities context = new McambioEntities())
            {
                string mon1, mon2;
                string MontoConsulta_Grabar;
                MontoConsulta_Grabar = SumarMontosIng(datSPMcambioCDD,1);
                if (datSPMcambioCDD[idx].tipoTransaccion == "Ventas" || datSPMcambioCDD[idx].tipoTransaccion == "ArbVta")
                {
                    mon1 = datSPMcambioCDD[idx].moneda2_Ingresada;
                    mon2 = datSPMcambioCDD[idx].moneda1_Ingresada;
                    if (PagOri == "ARBITRAJES")
                    {
                        MontoConsulta_Grabar = datSPMcambioCDD[idx].Monto2_Ingresado.ToString();
                    }
                }
                else
                {
                    mon1 = datSPMcambioCDD[idx].moneda1_Ingresada;
                    mon2 = datSPMcambioCDD[idx].moneda2_Ingresada;
                    if (PagOri == "ARBITRAJES")
                    {
                        MontoConsulta_Grabar = datSPMcambioCDD[idx].Monto1_Ingresado.ToString();
                    }
                }
                UnitOfWorkCext01 unit = new UnitOfWorkCext01();
                string stFecHoy = DateTime.Parse(DateTime.Now.ToString()).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                DateTime fecHoy = DateTime.Parse(stFecHoy);
                double montoChileFX = 0;
                int Liquidar = 0;
                if (TipoIng == "M")     //Si es manual, siempre sera consumo completo de Deal
                {
                    montoChileFX = double.Parse(MontoConsulta_Grabar);
                    Liquidar = 1;
                    datSPMcambioCDD[idx].txtcodigoOrigenCarga = "Pizarra";
                    datSPMcambioCDD[idx].delta = double.Parse(MontoConsulta_Grabar);
                    datSPMcambioCDD[idx].stMontoClienteRecibe = ((double)datSPMcambioCDD[idx].delta * (double)datSPMcambioCDD[idx].TipoCambio_Ingresado).ToString();
                }
                if (TipoIng == "S")     //Si es seleccion ventana modal, recuperamos nro chileFx, monto delta y validamos si es consumo completo deal. si es asi, marcamos para liquidar.
                {
                    NewIdChileFx = 0;
                    NewIdChileFx = Int64.Parse(datSPMcambioCDD[idx].numeroDeal.ToString());
                    montoChileFX = (double)datSPMcambioCDD[idx].delta;
                    if (double.Parse(MontoConsulta_Grabar) >= (double)montoChileFX)
                    {
                        Liquidar = 1;
                    }
                }
                if (TipoIng == "M")
                {
                    //IncorporacionNumeroOperacionSCE - Primera Insercion con valores en cero
                    context.Database.Connection.Open();
                    SqlParameter[] ParamIncorpNumOpeCSE = {
                                            new SqlParameter("@rutCliente", datSPMcambioCDD[idx].rutCliente),
                                            new SqlParameter("@numeroDealChileFX", NewIdChileFx),
                                            new SqlParameter("@numeroOperacionSCE", "0"),
                                            new SqlParameter("@montoSCE", "0"),
                                            new SqlParameter("@montoChileFX", (double)montoChileFX),
                                            new SqlParameter("@codigoEstadoContable", 1),
                                            new SqlParameter("@glosaError", DBNull.Value),
                                            new SqlParameter("@exepcionado", DBNull.Value),
                                            new SqlParameter("@vbJefatura", DBNull.Value),
                                            new SqlParameter("@ls_codret", ""),
                                            new SqlParameter("@codret", SqlDbType.Char, 5)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            },
                                            new SqlParameter("@ls_msgret", ""),
                                            new SqlParameter("@msgret", SqlDbType.VarChar, 150)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            }
                                        };
                    var varIncorpNumOpeCSE = context.Database.SqlQuery<Mcambio_SP_IncorporacionNumeroOperacionSCE>(
                                Mcambio_SP_IncorpNumOpeCSE.ToString() + " @rutCliente,@numeroDealChileFX,@numeroOperacionSCE,@montoSCE,@montoChileFX,@codigoEstadoContable,@glosaError,@exepcionado,@vbJefatura,@ls_codret = @codret OUTPUT,@ls_msgret = @msgret OUTPUT",
                                ParamIncorpNumOpeCSE).ToList();
                    context.Database.Connection.Close();
                    if (ParamIncorpNumOpeCSE[10].SqlValue.ToString().Trim() != "000")
                    {
                        sw = 1;
                        frmTicket.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = ParamIncorpNumOpeCSE[12].SqlValue.ToString() + " (" + ParamIncorpNumOpeCSE[10].SqlValue.ToString() + ")",
                            Title = "Aviso!:   "
                        });
                        return sw;
                    }
                }
                //IncorporacionNumeroOperacionSCE - Segunda Insercion con Todos los valores
                string NroSce = stOpeSin.ToString() + new string('0', (19 - stOpeSin.ToString().Length));
                context.Database.Connection.Open();
                SqlParameter[] ParamIncorpNumOpeCSECompletar = {
                                            new SqlParameter("@rutCliente", datSPMcambioCDD[idx].rutCliente),
                                            new SqlParameter("@numeroDealChileFX", NewIdChileFx),
                                            new SqlParameter("@numeroOperacionSCE", NroSce.ToString()),
                                            new SqlParameter("@montoSCE", double.Parse(MontoConsulta_Grabar)),
                                            new SqlParameter("@montoChileFX", (double)montoChileFX),
                                            new SqlParameter("@codigoEstadoContable", 1),
                                            new SqlParameter("@glosaError", DBNull.Value),
                                            new SqlParameter("@exepcionado", DBNull.Value),
                                            new SqlParameter("@vbJefatura", DBNull.Value),
                                            new SqlParameter("@ls_codret", ""),
                                            new SqlParameter("@codret", SqlDbType.Char, 5)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            },
                                            new SqlParameter("@ls_msgret", ""),
                                            new SqlParameter("@msgret", SqlDbType.VarChar, 150)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            }
                                        };
                var varIncorpNumOpeCSECompletar = context.Database.SqlQuery<Mcambio_SP_IncorporacionNumeroOperacionSCE>(
                            Mcambio_SP_IncorpNumOpeCSE.ToString() + " @rutCliente,@numeroDealChileFX,@numeroOperacionSCE,@montoSCE,@montoChileFX,@codigoEstadoContable,@glosaError,@exepcionado,@vbJefatura,@ls_codret = @codret OUTPUT,@ls_msgret = @msgret OUTPUT",
                            ParamIncorpNumOpeCSECompletar).ToList();
                context.Database.Connection.Close();
                if (ParamIncorpNumOpeCSECompletar[10].SqlValue.ToString().Trim() != "000")
                {
                    sw = 1;
                    frmTicket.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = ParamIncorpNumOpeCSECompletar[12].SqlValue.ToString() + " (" + ParamIncorpNumOpeCSECompletar[10].SqlValue.ToString() + ")",
                        Title = "Aviso!:   "
                    });
                    return sw;
                }
                //Cambio estado solo si se consumio por completo deal
                if (Liquidar == 1)
                {
                    int Correcto = CicloQuemarDeal(datSPMcambioCDD[idx].rutCliente, NewIdChileFx, frmTicket);
                    if (Correcto == 1)
                    {
                        return Correcto;    //1 = error al cambiar estado
                    }
                }
                //Registro en sp_ins_rel_numsce
                using (cext01Entities contextCext01 = new cext01Entities())
                {
                    int id_chilefx = (int)NewIdChileFx;                                                 //ID ChileFx
                    string codcct = VB6Helpers.Mid(stOpeSin, 1, 3).ToString();                          //SCE - Centro de costo
                    string codpro = VB6Helpers.Mid(stOpeSin, 4, 2).ToString();                          //SCE - Código de producto
                    string codesp = VB6Helpers.Mid(stOpeSin, 6, 2).ToString();                          //SCE - Código de especialista
                    string codofi = VB6Helpers.Mid(stOpeSin, 8, 3).ToString();                          //SCE - Código de oficina
                    string codope = VB6Helpers.Mid(stOpeSin, 11, 5).ToString();                         //SCE - Código de operación
                    string ref_sce = stOpeSin.ToString();                                               //Referencia SCE
                    string rut_cliente = datSPMcambioCDD[idx].rutCliente.ToString();                    //RUT formato sin puntos y con guión
                    string id_party = stPrtCli.ToString();                                              //RUT formato ID Party
                    int estado = 1;                                                                     //Estado de la operación
                    DateTime fecmov = DateTime.Today;                                                   //Fecha del movimiento
                    double mtomcd = double.Parse(MontoConsulta_Grabar);                                 // (double)datSPMcambioCDD[idx].Monto1_Ingresado;                      //Monto de la operación SCE(Puede ser un monto parcial del deal)
                    string codcom = datSPMcambioCDD[idx].codigoBancoCentral.ToString();                 //Código del BCCH
                    string concep = RecCodBancoCentral(datSPMcambioCDD[idx].stCodigoBancoCentral, 2);   //Concepto del BCCH
                    string origen_deal = datSPMcambioCDD[idx].txtcodigoOrigenCarga.ToString();          //Origen del deal: [CHILEFX, PIZARRA]
                    string tipo_contabilizacion = "ONLINE";                                             //Tipo de contabilización: [ONLINE, BATCH]
                    string chfx_moneda_1_deal = mon1;                                                   // datSPMcambioCDD[idx].moneda1_Ingresada.Trim();          //Moneda que cliente compra(Esto es cliente compra)
                    string chfx_moneda_2_deal = mon2;                                                   // datSPMcambioCDD[idx].moneda2_Ingresada.Trim();          //Moneda que cliente paga(Esto es cliente compra)
                    double chfx_monto_total_deal = (double)montoChileFX;                                //Monto original que el cliente compra o vende(M / E)
                    double chfx_monto_saldo_deal = (double)datSPMcambioCDD[idx].delta;                  //Saldo del deal que el cliente compra o vende(M/ E)  
                    double chfx_monto_peso = double.Parse(datSPMcambioCDD[idx].stMontoClienteRecibe);   //Monto del deal equivalente en pesos(no aplica para arbitrajes)
                    double chfx_tipo_cambio = (double)datSPMcambioCDD[idx].TipoCambio_Ingresado;        //Tipo de cambio de Spot o Cruce que el cliente conoce
                    double chfx_paridad = (double)datSPMcambioCDD[idx].Paridad_Ingresada;               //Paridad de arbitraje que el cliente conoce
                    DateTime chfx_fecha_valuta_deal = FechaValutaFinal;                                 //Valuta del deal
                    string chfx_tipo_operacion = "SPOT";                                                //Tipo de operación:[SPOT, CRUCE, ARBITRAJE]
                    string chfx_tipo_transaccion = datSPMcambioCDD[idx].tipoTransaccion.ToString();     //Tipo de transacción: [COMPRA, VENTA]
                    contextCext01.Database.Connection.Open();
                    SqlParameter[] ParamCext01 = {
                                                new SqlParameter("@id_chilefx", id_chilefx),
                                                new SqlParameter("@codcct", codcct),
                                                new SqlParameter("@codpro", codpro),
                                                new SqlParameter("@codesp", codesp),
                                                new SqlParameter("@codofi", codofi),
                                                new SqlParameter("@codope", codope),
                                                new SqlParameter("@ref_sce", ref_sce),
                                                new SqlParameter("@rut_cliente", rut_cliente),
                                                new SqlParameter("@id_party", id_party),
                                                new SqlParameter("@estado", estado),
                                                new SqlParameter("@fecmov", fecmov),
                                                new SqlParameter("@mtomcd", mtomcd),
                                                new SqlParameter("@codcom", codcom),
                                                new SqlParameter("@concep", concep),
                                                new SqlParameter("@origen_deal", origen_deal),
                                                new SqlParameter("@tipo_contabilizacion", tipo_contabilizacion),
                                                new SqlParameter("@chfx_moneda_1_deal", chfx_moneda_1_deal),
                                                new SqlParameter("@chfx_moneda_2_deal", chfx_moneda_2_deal),
                                                new SqlParameter("@chfx_monto_total_deal", chfx_monto_total_deal),
                                                new SqlParameter("@chfx_monto_saldo_deal", chfx_monto_saldo_deal),
                                                new SqlParameter("@chfx_monto_peso", chfx_monto_peso),
                                                new SqlParameter("@chfx_tipo_cambio", chfx_tipo_cambio),
                                                new SqlParameter("@chfx_paridad", chfx_paridad),
                                                new SqlParameter("@chfx_fecha_valuta_deal", chfx_fecha_valuta_deal),
                                                new SqlParameter("@chfx_tipo_operacion", chfx_tipo_operacion),
                                                new SqlParameter("@chfx_tipo_transaccion", chfx_tipo_transaccion),
                                                new SqlParameter("@dsc_error", ""),
                                                new SqlParameter("@dsc", SqlDbType.VarChar, 200)
                                                {
                                                    Direction = System.Data.ParameterDirection.Output
                                                },
                                                new SqlParameter("@ope_cdg_error", ""),
                                                new SqlParameter("@ope_cdg", SqlDbType.Int)
                                                {
                                                    Direction = System.Data.ParameterDirection.Output
                                                }
                                            };
                    var varParamCext01 = contextCext01.Database.SqlQuery<Cext01_sp_ins_rel_numsce>(
                                    Cext01_spInsRelNumsce.ToString() + " @id_chilefx,@codcct,@codpro,@codesp,@codofi,@codope,@ref_sce,@rut_cliente,@id_party,@estado,@fecmov,@mtomcd,@codcom,@concep,@origen_deal,@tipo_contabilizacion,@chfx_moneda_1_deal,@chfx_moneda_2_deal,@chfx_monto_total_deal,@chfx_monto_saldo_deal,@chfx_monto_peso,@chfx_tipo_cambio,@chfx_paridad,@chfx_fecha_valuta_deal,@chfx_tipo_operacion,@chfx_tipo_transaccion,@dsc_error = @dsc OUTPUT,@ope_cdg_error = @ope_cdg OUTPUT",
                                    ParamCext01).ToList();
                    contextCext01.Database.Connection.Close();
                    if (ParamCext01[29].SqlValue.ToString().Trim() != "0")
                    {
                        sw = 1;
                        frmTicket.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = ParamCext01[27].SqlValue.ToString(),
                            Title = "Aviso!:   "
                        });
                        return sw;
                    }
                    //Area reversa contable
                    if (TipoIng == "S")
                    {
                        string chfxNroSceTransit = string.IsNullOrEmpty(datSPMcambioCDD[idx].ultimoNumeroTransitoria) ? "" : datSPMcambioCDD[idx].ultimoNumeroTransitoria.ToString();
                        string chfxNroSceConting = string.IsNullOrEmpty(datSPMcambioCDD[idx].ultimoNumeroContingente) ? "" : datSPMcambioCDD[idx].ultimoNumeroContingente.ToString();
                        chfxNroSceTransit = chfxNroSceTransit == "0" ? "" : chfxNroSceTransit;
                        chfxNroSceConting = chfxNroSceConting == "0" ? "" : chfxNroSceConting;
                        string chfxNroSceReversa = chfxNroSceConting != "" ? chfxNroSceConting : (chfxNroSceTransit != "" ? chfxNroSceTransit : "");
                        string chfxTipoNroSceRev = chfxNroSceConting != "" ? "C" : (chfxNroSceTransit != "" ? "T" : "");
                        if (chfxNroSceReversa != "")
                        {
                            contextCext01.Database.Connection.Open();
                            SqlParameter[] ParamRevConta = {
                                                            new SqlParameter("@nro_sce", chfxNroSceReversa),
                                                            new SqlParameter("@nro_sce_OpAc", NroSce),
                                                            new SqlParameter("@otra_condic", ParamGeneral_otra_condic),
                                                            new SqlParameter("@tipo", chfxTipoNroSceRev),
                                                            new SqlParameter("@salida", ""),
                                                            new SqlParameter("@sali", SqlDbType.Int)
                                                            {
                                                                Direction = System.Data.ParameterDirection.Output
                                                            }
                                                            };
                            var varParamRevConta = contextCext01.Database.SqlQuery<Cext01_sp_sce_reversa_contable>(
                                    Cext01_SP_SceRevContable.ToString() + " @nro_sce, @nro_sce_OpAc, @otra_condic, @tipo, @salida = @sali OUTPUT",
                                    ParamRevConta).ToList();
                            contextCext01.Database.Connection.Close();
                            if (ParamRevConta[5].SqlValue.ToString().Trim() != "0")
                            {
                                string TipoAlerta = "";
                                switch (ParamRevConta[5].SqlValue.ToString().Trim())
                                {
                                    case "-1":
                                        TipoAlerta = "Tipo no corresponde a T o C al ejecutar reversa contable";
                                        break;
                                    case "-2":
                                        TipoAlerta = "Tipo no corresponde a T o C al ejecutar reversa pli";
                                        break;
                                    case "-3":
                                        TipoAlerta = "Se produjo un error durante ejecución reversa pli";
                                        break;
                                    case "-5":
                                        TipoAlerta = "No se encontro nro. sce";
                                        break;
                                    default:
                                        TipoAlerta = "";
                                        break;
                                }
                                string msg = "Reversa contable: '"+ (chfxTipoNroSceRev == "T" ? "Transitorio":"Contingente") +"' para el Nro.Sce: " + chfxNroSceReversa.ToString() + " - " + TipoAlerta;
                                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = msg,
                                    Title = "Aviso!:   ",
                                    AutoClose = false
                                });
                            }
                        }
                    }
                    
                }

                using (McambioEntities ctxValidar = new McambioEntities())
                {
                    try
                    {
                        //Consulta tbl_chilefx_deal_sce (Esto es para validar que se haya grabado y evitar que misteriosamente digan que no se grabo)
                        ctxValidar.Database.Connection.Open();
                        var ValExiste = (from cfx in ctxValidar.tbl_chilefx_deal_sce
                                         where cfx.id_chilefx == NewIdChileFx && cfx.id_sce == NroSce.ToString()
                                         orderby cfx.fecha_hora_registro descending
                                         select cfx).FirstOrDefault();
                        ctxValidar.Database.Connection.Close();
                        if (ValExiste == null)
                        {
                            sw = 1;
                            initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Warning,
                                Text = "Id SCE: " + NroSce.ToString() + " - Id ChileFx: " + NewIdChileFx.ToString() + ", no grabado. Póngase en contacto con el área de sistemas.",
                                Title = "Aviso!:   ",
                                AutoClose = false
                            });
                            return sw;
                        }
                        else
                        {
                            initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Correcto,
                                Text = "Id SCE: " + NroSce.ToString() + " - Id ChileFx: " + NewIdChileFx.ToString() + ", grabado correctamente.",
                                Title = "Aviso!:   ",
                                AutoClose = false
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                //realizo todo y marcamos con 9 para que no vuelva a entrar a grabar
                sw = 9;
                
                return sw;
            }
        }
        public double PlvSO_ValidarIngresoPiz(InitializationObject initObject, dynamic jsonModel, List<DealsIngresadosParaProcesar> DealsIngParaProces, string PagOri) 
        {
            UI_Frm_PlvSO FrmPlvSO = initObject.Frm_PlvSO;
            initObject.Mdi_Principal.MESSAGES.Clear();
            if (FrmPlvSO.Tx_MtoFob.Text != null)
            {
                string TipIng = TipoIngreso(DealsIngParaProces);
                if (TipIng == "" || TipIng == "P")
                {
                    if (double.Parse(FrmPlvSO.Tx_MtoFob.Text) > ParamGeneral_ValorMaximoPizarra)
                    {
                        MsgsValidarIngreso(initObject, 6, PagOri, "");
                        return ParamGeneral_ValorMaximoPizarra;
                    }
                }
            }
            if (valMontoSegundaMoneda == 0)
            {
                if (ParaMsgSonicBoom != "")
                {
                    MsgsValidarIngreso(initObject, 5, PagOri, ParaMsgSonicBoom + " - " + T_MODANUVI.MsgPlaSO.ToString());
                }
                return -1;
            }
            else
            {
                return 0;
            }
        }
        public double COMINV_ValidarIngresoPiz(InitializationObject initObject, string PagOri, List<DealsIngresadosParaProcesar> DealsIngParaProces) 
        {
            UI_Frm_Comercio_Invisibles FrmCOMINV = initObject.Frm_Comercio_Invisible;
            FrmCOMINV.Errors.Clear();
            if (FrmCOMINV.Tx_MtoCV[0].Text != null)
            {
                string TipIng = TipoIngreso(DealsIngParaProces);
                if (TipIng == "" || TipIng == "P")
                {
                    if (double.Parse(FrmCOMINV.Tx_MtoCV[0].Text) > ParamGeneral_ValorMaximoPizarra)
                    {
                        MsgsValidarIngreso(initObject, 6, PagOri, "");
                        return ParamGeneral_ValorMaximoPizarra;
                    }
                }
            }
            if (valMontoSegundaMoneda == 0)
            {
                if (ParaMsgSonicBoom != "")
                {
                    FrmCOMINV.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Critical,
                        Text = ParaMsgSonicBoom,
                        Title = "Aviso!:   "
                    });
                }
                return -1;
            }
            else
            {
                return 0;
            }
        }
        public void MsgsValidarIngreso(InitializationObject initObject, int IdMsg, string PagOri, string Msg)
        {
            UI_Frm_Comercio_Invisibles FrmCOMINV = initObject.Frm_Comercio_Invisible;
            UI_Mdi_Principal MdiPrinc = initObject.Mdi_Principal;
            UI_Frm_Arbitrajes FrmArbi = initObject.Frm_Arbitrajes;
            string txtMsg = "";
            TipoMensaje TipMsg = TipoMensaje.Nada;
            if (IdMsg == 1)
            {
                TipMsg = TipoMensaje.Informacion;
                txtMsg = "Debe respetar el tipo de operación (compra/venta) y la moneda de uso del primer deal seleccionado.";
            }
            else if (IdMsg == 2)
            {
                TipMsg = TipoMensaje.Critical;
                txtMsg = "Cuando el primer registro se crea con tipo de cambio pizarra, no puede ingresar más.";
            }
            else if (IdMsg == 3)
            {
                TipMsg = TipoMensaje.Critical;
                txtMsg = "Cuando el primer registro se crea a partir de un deal seleccionado, no puede ingresar un monto con tipo de cambio pizarra.";
            }
            else if (IdMsg == 4)
            {
                TipMsg = TipoMensaje.Critical;
                txtMsg = "Tipo de cambio no corresponde al establecido para el deal seleccionado.";
            }
            else if (IdMsg == 5)
            {
                TipMsg = TipoMensaje.Critical;
                txtMsg = Msg;
            }
            else if (IdMsg == 6)
            {
                TipMsg = TipoMensaje.Critical;
                txtMsg = "El monto ingresado excede el máximo autorizado de " + ParamGeneral_ValorMaximoPizarra + " para tipo de cambio pizarra.";
            }
            else if (IdMsg == 7)
            {
                TipMsg = TipoMensaje.Error;
                txtMsg = "El identificador del Deal seleccionado es incorrecto.<br /> Pulse la tecla F5 o click en botón refrescar página.";
            }
            else if (IdMsg == 8)
            {
                TipMsg = TipoMensaje.Error;
                txtMsg = "El valor ingresado supera el saldo restante del Deal.";
            }
            else if (IdMsg == 9)
            {
                TipMsg = TipoMensaje.Error;
                txtMsg = "No ha seleccionado ninguna moneda de tipo dólar.";
            }
            else if (IdMsg == 10)
            {
                TipMsg = TipoMensaje.Error;
                txtMsg = "La suma de los montos ha alcanzado el total del saldo. No puede ingresar más.";
            }
            else if (IdMsg == 11)
            {
                TipMsg = TipoMensaje.Informacion;
                txtMsg = "Moneda Compra es USD, para obtener la Paridad Arbitraje debe ingresar el Monto Venta.";
            }
            else if (IdMsg == 12)
            {
                TipMsg = TipoMensaje.Informacion;
                txtMsg = "Moneda Venta es USD, para obtener la Paridad Arbitraje debe ingresar el Monto Compra.";
            }
            else if (IdMsg == 13)
            {
                TipMsg = TipoMensaje.Critical;
                txtMsg = "El monto ingresado sumado a los ingresos previos, supera el saldo total del deal más la tolerancia permitida.";
            }
            else
            {
                TipMsg = TipoMensaje.Critical;
                txtMsg = "Algo extraño pasa!!, seguramente proviene de la dimensión desconocida.";
            }
            if (PagOri == PaginaOrigen.COMINV.ToString())
            {
                FrmCOMINV.Errors.Clear();
                FrmCOMINV.Errors.Add(new UI_Message()
                {
                    Type = TipMsg,
                    Text = txtMsg,
                    Title = "Aviso!:   "
                });
            }
            else if (PagOri == PaginaOrigen.PlvSO.ToString())
            {
                MdiPrinc.MESSAGES.Clear();
                MdiPrinc.MESSAGES.Add(new UI_Message()
                {
                    Type = TipMsg,
                    Text = txtMsg,
                    Title = "Aviso!:   "
                });
            }
            else if (PagOri == PaginaOrigen.ARBITRAJES.ToString())
            {
                FrmArbi.Errors.Clear();
                FrmArbi.Errors.Add(new UI_Message()
                {
                    Type = TipMsg,
                    Text = txtMsg,
                    Title = "Aviso!:   "
                });
            }
        }
        public int ValidarIngreso(InitializationObject initObject, List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, List<DealsIngresadosParaProcesar> DealsIngParaProces, string PagOri, int pos, Nullable<int> DealActSel) 
        {
            int sw = 0;
            UI_Frm_Comercio_Invisibles FrmCOMINV = initObject.Frm_Comercio_Invisible;
            UI_Frm_PlvSO Frm_PlvSO = initObject.Frm_PlvSO;
            UI_Frm_Arbitrajes FrmArbitrajes = initObject.Frm_Arbitrajes;
            Lista_Moneda_Pais MonedaPaisDealSeleccionado = new Lista_Moneda_Pais();
            string tipoTransaccion;
            bool PrimerIngreso = false;
            if (DealsIngParaProces == null)
            {
                PrimerIngreso = true;
            }
            if (PagOri == PaginaOrigen.COMINV.ToString())
            {
                T_MODGPYF0 MODGPFY0 = initObject.MODGPYF0;
                T_ModChVrf ModChVrf = initObject.ModChVrf;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
                short a = Fn_Validar_Campos_COMINV(MODGPFY0, ModChVrf, Mdi_Principal, initObject.Frm_Comercio_Invisible, 1, 6);
                if (a == 0)
                {
                    return 5;
                }
                if (FrmCOMINV.Cb_Divisa.ListIndex == 1 || FrmCOMINV.Cb_Divisa.ListIndex == 3)
                {
                    tipoTransaccion = "Ventas";
                }
                else if (FrmCOMINV.Cb_Divisa.ListIndex == 0 || FrmCOMINV.Cb_Divisa.ListIndex == 2)
                {
                    tipoTransaccion = "Compras";
                }
                else
                {
                    tipoTransaccion = "Compras";
                }
                if (PrimerIngreso)
                {
                    if (datSPMcambioCDD[pos].TipoIngreso == "P")
                    {
                        MsgsValidarIngreso(initObject, 2, PagOri, "");
                        return 2;
                    }
                    if (DealActSel == -1 && datSPMcambioCDD[pos].TipoIngreso == "S")
                    {
                        MsgsValidarIngreso(initObject, 3, PagOri, "");
                        return 3;
                    }
                    if (datSPMcambioCDD[pos].tipoTransaccion != tipoTransaccion)
                    {
                        MsgsValidarIngreso(initObject, 1, PagOri, "");
                        return 1;
                    }
                    MonedaPaisDealSeleccionado = DevolverClassMonedaPais(datSPMcambioCDD, pos);
                    if (FrmCOMINV.Cb_Moneda.ListIndex != FrmCOMINV.Cb_Moneda.get_Index((int)MonedaPaisDealSeleccionado.CodMoneda))
                    {
                        MsgsValidarIngreso(initObject, 1, PagOri, "");
                        return 1;
                    }
                } 
                else if (!PrimerIngreso && DealsIngParaProces.Count() >= 1)
                {
                    if (DealsIngParaProces[0].TipoIngreso == "P")
                    {
                        MsgsValidarIngreso(initObject, 2, PagOri, "");
                        return 2;
                    }
                    if (DealActSel == -1 && DealsIngParaProces[0].TipoIngreso == "S")
                    {
                        MsgsValidarIngreso(initObject, 3, PagOri, "");
                        return 3;
                    }
                    if (DealsIngParaProces[0].tipoTransaccion != tipoTransaccion)
                    {
                        MsgsValidarIngreso(initObject, 1, PagOri, "");
                        return 1;
                    }
                    if (FrmCOMINV.Cb_Moneda.Text.ToString() != DealsIngParaProces[0].txtCbMoneda1_Ingresado)
                    {
                        MsgsValidarIngreso(initObject, 1, PagOri, "");
                        return 1;
                    }
                    double tc_frm = string.IsNullOrEmpty(FrmCOMINV.Tx_MtoCV[1].Text) ? 0 : double.Parse(FrmCOMINV.Tx_MtoCV[1].Text);
                    double tc_ing = (double)DealsIngParaProces[0].TipoCambio_Ingresado == null ? 0 : (double)DealsIngParaProces[0].TipoCambio_Ingresado;
                    if (DealsIngParaProces[0].TipoIngreso == "S" && (tc_frm != tc_ing))
                    {
                        MsgsValidarIngreso(initObject, 4, PagOri, "");
                        return 3;
                    }
                }
                if (PrimerIngreso)
                {

                }
                else if (!PrimerIngreso)
                {

                }
            }
            if (PagOri == PaginaOrigen.PlvSO.ToString())
            {
                tipoTransaccion = "Ventas";
                if (PrimerIngreso)
                {
                    if (DealActSel == null || DealActSel == -1) //validar ingreso pizarra
                    {
                        if (double.Parse(Frm_PlvSO.Tx_TipCam.Text.ToString()) > ParamGeneral_ValorMaximoPizarra)
                        {

                            MsgsValidarIngreso(initObject, 6, PagOri, "");
                            return 1;
                        }
                    }
                    else                    //validar ingreso seleccion
                    {
                        pos = ((int)DealActSel - 1) < 0 ? 0 : ((int)DealActSel - 1);
                        MonedaPaisDealSeleccionado = DevolverClassMonedaPais(datSPMcambioCDD, pos);
                        if (MonedaPaisDealSeleccionado.CodMoneda != Frm_PlvSO.Cb_Moneda.Value)
                        {
                            MsgsValidarIngreso(initObject, 1, PagOri, "");
                            return 1;
                        }
                        if (double.Parse(Frm_PlvSO.Tx_TipCam.Text.ToString()) != double.Parse(datSPMcambioCDD[pos].precioCliente.ToString()))
                        {
                            MsgsValidarIngreso(initObject, 4, PagOri, "");
                            return 1;
                        }
                    }
                }
                else
                {
                    pos = (int)DealActSel >= 0 ? 0 : -1;
                    if (pos == - 1)     //validar ingreso pizarra
                    {
                        if (DealsIngParaProces[0].TipoIngreso == "P")
                        {
                            MsgsValidarIngreso(initObject, 2, PagOri, "");
                            return 1;
                        }
                        if (pos == -1 && DealsIngParaProces[0].TipoIngreso == "S")
                        {
                            MsgsValidarIngreso(initObject, 3, PagOri, "");
                            return 1;
                        }
                    }
                    else                //validar ingreso seleccion
                    {
                        MonedaPaisDealSeleccionado = DevolverClassMonedaPais(datSPMcambioCDD, pos);
                        if (MonedaPaisDealSeleccionado.CodMoneda != Frm_PlvSO.Cb_Moneda.Value)
                        {
                            MsgsValidarIngreso(initObject, 1, PagOri, "");
                            return 1;
                        }
                        double tc_frm = string.IsNullOrEmpty(Frm_PlvSO.Tx_TipCam.Text) ? 0 : double.Parse(Frm_PlvSO.Tx_TipCam.Text);
                        double tc_ing = DealsIngParaProces[0].TipoCambio_Ingresado == null ? 0 : (double)DealsIngParaProces[0].TipoCambio_Ingresado;
                        if (DealsIngParaProces[0].TipoIngreso == "S" && (tc_frm != tc_ing))
                        {
                            MsgsValidarIngreso(initObject, 4, PagOri, "");
                            return 1;
                        }
                        double MtoFob = string.IsNullOrEmpty(Frm_PlvSO.Tx_MtoFob.Text) ? 0 : double.Parse(Frm_PlvSO.Tx_MtoFob.Text);
                        double MtoDelta = datSPMcambioCDD[0].delta == null ? 0 : (double)datSPMcambioCDD[0].delta;
                        if (MtoFob > MtoDelta)
                        {
                            MsgsValidarIngreso(initObject, 8, PagOri, "");
                            Frm_PlvSO.Tx_MtoFob.Text = MtoDelta.ToString();
                            return 1;
                        }
                    }
                }
            }
            if (PagOri == PaginaOrigen.ARBITRAJES.ToString())
            {
                short a = Fn_Validar_Campos_ARBITRAJE(FrmArbitrajes, 1, 6);
                if (a == 0)
                {
                    return 5;
                }
                Lista_Moneda_Pais MonedaCompra = new Lista_Moneda_Pais();
                Lista_Moneda_Pais MonedaVenta = new Lista_Moneda_Pais();
                MonedaCompra = GetParamComexMonedaPais(FrmArbitrajes.Cb_Moneda_Compra.Value.ToString(), "CodMoneda");
                MonedaVenta = GetParamComexMonedaPais(FrmArbitrajes.Cb_Moneda_Venta.Value.ToString(), "CodMoneda");
                if (PrimerIngreso)
                {
                    if (DealActSel == null || DealActSel == -1) //validar ingreso pizarra
                    {
                        if (MonedaCompra.SiglaMoneda != "USD" && MonedaVenta.SiglaMoneda != "USD")
                        {
                            MsgsValidarIngreso(initObject, 9, PagOri, "");
                        }
                        if (MonedaCompra.SiglaMoneda == "USD" && (double.Parse(FrmArbitrajes.Tx_Mtoarb[3].Text)) > ParamGeneral_ValorMaximoPizarra)  //Venta
                        {
                            MsgsValidarIngreso(initObject, 6, PagOri, "");
                            return 1;
                        }
                        if (MonedaVenta.SiglaMoneda == "USD" && (double.Parse(FrmArbitrajes.Tx_Mtoarb[2].Text)) > ParamGeneral_ValorMaximoPizarra)  //Venta
                        {
                            MsgsValidarIngreso(initObject, 6, PagOri, "");
                            return 1;
                        }
                    }
                    else
                    {
                        pos = ((int)DealActSel - 1) < 0 ? 0 : ((int)DealActSel - 1);
                        if (datSPMcambioCDD[pos].moneda1.Trim() != MonedaCompra.SiglaMoneda)
                        {
                            MsgsValidarIngreso(initObject, 1, PagOri, "");
                            return 1;
                        }
                        if (datSPMcambioCDD[pos].moneda2.Trim() != MonedaVenta.SiglaMoneda)
                        {
                            MsgsValidarIngreso(initObject, 1, PagOri, "");
                            return 1;
                        }
                        double ParidadIng = double.Parse(FrmArbitrajes.Tx_Mtoarb[1].Text.ToString());
                        ParidadIng = Math.Round(ParidadIng, 2);
                        double ParidadDividida = 1 / double.Parse(FrmArbitrajes.Tx_Mtoarb[1].Text.ToString());
                        ParidadDividida = Math.Round(ParidadDividida, 2);
                        double ParidadSel = double.Parse(datSPMcambioCDD[pos].precioCliente.ToString());
                        ParidadSel = Math.Round(ParidadSel, 2);
                        if (ParidadIng != ParidadSel && ParidadDividida != ParidadSel)
                        {
                            MsgsValidarIngreso(initObject, 4, PagOri, "");
                            return 1;
                        }
                        if (MonedaCompra.SiglaMoneda == "USD")
                        {
                            double Monto = string.IsNullOrEmpty(FrmArbitrajes.Tx_Mtoarb[3].Text) ? 0 : double.Parse(FrmArbitrajes.Tx_Mtoarb[3].Text);
                            double MtoDelta = datSPMcambioCDD[pos].delta == null ? 0 : (double)datSPMcambioCDD[pos].delta;
                            double PorcTole = MtoDelta * ParamGeneralTolerancia_X_Porc;
                            double DeltaOrig_mas_Tole = MtoDelta + PorcTole;
                            if (Monto > DeltaOrig_mas_Tole)
                            {
                                MsgsValidarIngreso(initObject, 8, PagOri, "");
                                FrmArbitrajes.Tx_Mtoarb[3].Text = MtoDelta.ToString();
                                return 1;
                            }
                        }
                        if (MonedaVenta.SiglaMoneda == "USD")
                        {
                            double Monto = string.IsNullOrEmpty(FrmArbitrajes.Tx_Mtoarb[2].Text) ? 0 : double.Parse(FrmArbitrajes.Tx_Mtoarb[2].Text);
                            double MtoDelta = datSPMcambioCDD[pos].delta == null ? 0 : (double)datSPMcambioCDD[pos].delta;
                            double PorcTole = MtoDelta * ParamGeneralTolerancia_X_Porc;
                            double DeltaOrig_mas_Tole = MtoDelta + PorcTole;
                            if (Monto > DeltaOrig_mas_Tole)
                            {
                                MsgsValidarIngreso(initObject, 8, PagOri, "");
                                FrmArbitrajes.Tx_Mtoarb[2].Text = MtoDelta.ToString();
                                return 1;
                            }
                        }

                    }
                }
                else
                {
                    if (DealsIngParaProces.Count > 0)
                    {
                        if (DealsIngParaProces[0].TipoIngreso == "P")
                        {
                            MsgsValidarIngreso(initObject, 2, PagOri, "");
                            return 1;
                        }
                        else
                        {
                            if (DealActSel == null)
                            {
                                MsgsValidarIngreso(initObject, 3, PagOri, "");
                                return 1;
                            }
                            pos = ((int)DealActSel - 1) < 0 ? 0 : ((int)DealActSel - 1);
                            if (pos > (int)DealsIngParaProces.Count)
                            {
                                pos = 0;
                            }
                            if (DealsIngParaProces[pos].moneda1.Trim() != MonedaCompra.SiglaMoneda)
                            {
                                MsgsValidarIngreso(initObject, 1, PagOri, "");
                                return 1;
                            }
                            if (DealsIngParaProces[pos].moneda2.Trim() != MonedaVenta.SiglaMoneda)
                            {
                                MsgsValidarIngreso(initObject, 1, PagOri, "");
                                return 1;
                            }
                            double ParidadIng = double.Parse(FrmArbitrajes.Tx_Mtoarb[1].Text.ToString());
                            ParidadIng = Math.Round(ParidadIng, 2);
                            double ParidadDividida = 1 / double.Parse(FrmArbitrajes.Tx_Mtoarb[1].Text.ToString());
                            ParidadDividida = Math.Round(ParidadDividida, 2);
                            double ParidadSel = double.Parse(DealsIngParaProces[pos].precioCliente.ToString());
                            ParidadSel = Math.Round(ParidadSel, 2);
                            if (ParidadIng != ParidadSel && ParidadDividida != ParidadSel)
                            {
                                MsgsValidarIngreso(initObject, 4, PagOri, "");
                                return 1;
                            }
                            if (MonedaCompra.SiglaMoneda == "USD")
                            {
                                double Monto = string.IsNullOrEmpty(FrmArbitrajes.Tx_Mtoarb[3].Text) ? 0 : double.Parse(FrmArbitrajes.Tx_Mtoarb[3].Text);
                                double MtoDelta = DealsIngParaProces[0].delta == null ? 0 : (double)DealsIngParaProces[0].delta;
                                double PorcTole = MtoDelta * ParamGeneralTolerancia_X_Porc;
                                double DeltaOrig_mas_Tole = MtoDelta + PorcTole;
                                if (Monto > DeltaOrig_mas_Tole)
                                {
                                    MsgsValidarIngreso(initObject, 8, PagOri, "");
                                    FrmArbitrajes.Tx_Mtoarb[3].Text = MtoDelta.ToString();
                                    return 1;
                                }
                            }
                            if (MonedaVenta.SiglaMoneda == "USD")
                            {
                                double Monto = string.IsNullOrEmpty(FrmArbitrajes.Tx_Mtoarb[2].Text) ? 0 : double.Parse(FrmArbitrajes.Tx_Mtoarb[2].Text);
                                double MtoDelta = DealsIngParaProces[0].delta == null ? 0 : (double)DealsIngParaProces[0].delta;
                                double PorcTole = MtoDelta * ParamGeneralTolerancia_X_Porc;
                                double DeltaOrig_mas_Tole = MtoDelta + PorcTole;
                                if (Monto > DeltaOrig_mas_Tole)
                                {
                                    MsgsValidarIngreso(initObject, 8, PagOri, "");
                                    FrmArbitrajes.Tx_Mtoarb[2].Text = MtoDelta.ToString();
                                    return 1;
                                }
                            }
                            if (DealsIngParaProces[0].TipoIngreso == "S" && datSPMcambioCDD.Count() == 0)
                            {
                                MsgsValidarIngreso(initObject, 10, PagOri, "");
                                return 1;
                            }
                        }
                    }
                }
                
            }
            return sw;
        }

        public int ValidarIngresoUdp(InitializationObject initObject, List<DealsIngresadosParaProcesar> DealsIngParaProces, string PagOri, int pos, Nullable<int> DealActSel)
        {
            int sw = 0;
            UI_Frm_Comercio_Invisibles FrmCOMINV = initObject.Frm_Comercio_Invisible;
            UI_Frm_PlvSO Frm_PlvSO = initObject.Frm_PlvSO;
            UI_Frm_Arbitrajes FrmArbitrajes = initObject.Frm_Arbitrajes;
            Lista_Moneda_Pais MonedaPaisDealSeleccionado = new Lista_Moneda_Pais();
            string tipoTransaccion;
            bool PrimerIngreso = false;
            if (DealsIngParaProces == null)
            {
                PrimerIngreso = true;
            }
            if (PagOri == PaginaOrigen.COMINV.ToString())
            {
                T_MODGPYF0 MODGPFY0 = initObject.MODGPYF0;
                T_ModChVrf ModChVrf = initObject.ModChVrf;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
                short a = Fn_Validar_Campos_COMINV(MODGPFY0, ModChVrf, Mdi_Principal, initObject.Frm_Comercio_Invisible, 1, 6);
                if (a == 0)
                {
                    return 5;
                }
                if (FrmCOMINV.Cb_Divisa.ListIndex == 1 || FrmCOMINV.Cb_Divisa.ListIndex == 3)
                {
                    tipoTransaccion = "Ventas";
                }
                else if (FrmCOMINV.Cb_Divisa.ListIndex == 0 || FrmCOMINV.Cb_Divisa.ListIndex == 2)
                {
                    tipoTransaccion = "Compras";
                }
                else
                {
                    tipoTransaccion = "Compras";
                }
                if (PrimerIngreso)
                {
                    if (DealsIngParaProces[pos].TipoIngreso == "P")
                    {
                        MsgsValidarIngreso(initObject, 2, PagOri, "");
                        return 2;
                    }
                    if (DealActSel == -1 && DealsIngParaProces[pos].TipoIngreso == "S")
                    {
                        MsgsValidarIngreso(initObject, 3, PagOri, "");
                        return 3;
                    }
                    if (DealsIngParaProces[pos].tipoTransaccion != tipoTransaccion)
                    {
                        MsgsValidarIngreso(initObject, 1, PagOri, "");
                        return 1;
                    }
                    MonedaPaisDealSeleccionado = DevolverClassMonedaPaisUdp(DealsIngParaProces, pos);
                    if (FrmCOMINV.Cb_Moneda.ListIndex != FrmCOMINV.Cb_Moneda.get_Index((int)MonedaPaisDealSeleccionado.CodMoneda))
                    {
                        MsgsValidarIngreso(initObject, 1, PagOri, "");
                        return 1;
                    }
                }
                else if (!PrimerIngreso && DealsIngParaProces.Count() >= 1)
                {
                    if (DealsIngParaProces[0].TipoIngreso == "P")
                    {
                        MsgsValidarIngreso(initObject, 2, PagOri, "");
                        return 2;
                    }
                    if (DealActSel == -1 && DealsIngParaProces[0].TipoIngreso == "S")
                    {
                        MsgsValidarIngreso(initObject, 3, PagOri, "");
                        return 3;
                    }
                    if (DealsIngParaProces[0].tipoTransaccion != tipoTransaccion)
                    {
                        MsgsValidarIngreso(initObject, 1, PagOri, "");
                        return 1;
                    }
                    if (FrmCOMINV.Cb_Moneda.Text.ToString() != DealsIngParaProces[0].txtCbMoneda1_Ingresado)
                    {
                        MsgsValidarIngreso(initObject, 1, PagOri, "");
                        return 1;
                    }
                    double tc_frm = string.IsNullOrEmpty(FrmCOMINV.Tx_MtoCV[1].Text) ? 0 : double.Parse(FrmCOMINV.Tx_MtoCV[1].Text);
                    double tc_ing = (double)DealsIngParaProces[0].TipoCambio_Ingresado == null ? 0 : (double)DealsIngParaProces[0].TipoCambio_Ingresado;
                    if (DealsIngParaProces[0].TipoIngreso == "S" && (tc_frm != tc_ing))
                    {
                        MsgsValidarIngreso(initObject, 4, PagOri, "");
                        return 3;
                    }
                }
                if (PrimerIngreso)
                {

                }
                else if (!PrimerIngreso)
                {

                }
            }
            if (PagOri == PaginaOrigen.PlvSO.ToString())
            {
                tipoTransaccion = "Ventas";
                if (PrimerIngreso)
                {
                    if (DealActSel == null || DealActSel == -1) //validar ingreso pizarra
                    {
                        if (double.Parse(Frm_PlvSO.Tx_TipCam.Text.ToString()) > ParamGeneral_ValorMaximoPizarra)
                        {

                            MsgsValidarIngreso(initObject, 6, PagOri, "");
                            return 1;
                        }
                    }
                    else                    //validar ingreso seleccion
                    {
                        pos = ((int)DealActSel - 1) < 0 ? 0 : ((int)DealActSel - 1);
                        MonedaPaisDealSeleccionado = DevolverClassMonedaPaisUdp(DealsIngParaProces, pos);
                        if (MonedaPaisDealSeleccionado.CodMoneda != Frm_PlvSO.Cb_Moneda.Value)
                        {
                            MsgsValidarIngreso(initObject, 1, PagOri, "");
                            return 1;
                        }
                        if (double.Parse(Frm_PlvSO.Tx_TipCam.Text.ToString()) != double.Parse(DealsIngParaProces[pos].precioCliente.ToString()))
                        {
                            MsgsValidarIngreso(initObject, 4, PagOri, "");
                            return 1;
                        }
                    }
                }
                else
                {
                    pos = (int)DealActSel >= 0 ? 0 : -1;
                    if (pos == -1)     //validar ingreso pizarra
                    {
                        if (DealsIngParaProces[0].TipoIngreso == "P")
                        {
                            MsgsValidarIngreso(initObject, 2, PagOri, "");
                            return 1;
                        }
                        if (pos == -1 && DealsIngParaProces[0].TipoIngreso == "S")
                        {
                            MsgsValidarIngreso(initObject, 3, PagOri, "");
                            return 1;
                        }
                    }
                    else                //validar ingreso seleccion
                    {
                        MonedaPaisDealSeleccionado = DevolverClassMonedaPaisUdp(DealsIngParaProces, pos);
                        if (MonedaPaisDealSeleccionado.CodMoneda != Frm_PlvSO.Cb_Moneda.Value)
                        {
                            MsgsValidarIngreso(initObject, 1, PagOri, "");
                            return 1;
                        }
                        double tc_frm = string.IsNullOrEmpty(Frm_PlvSO.Tx_TipCam.Text) ? 0 : double.Parse(Frm_PlvSO.Tx_TipCam.Text);
                        double tc_ing = DealsIngParaProces[0].TipoCambio_Ingresado == null ? 0 : (double)DealsIngParaProces[0].TipoCambio_Ingresado;
                        if (DealsIngParaProces[0].TipoIngreso == "S" && (tc_frm != tc_ing))
                        {
                            MsgsValidarIngreso(initObject, 4, PagOri, "");
                            return 1;
                        }
                        double MtoFob = string.IsNullOrEmpty(Frm_PlvSO.Tx_MtoFob.Text) ? 0 : double.Parse(Frm_PlvSO.Tx_MtoFob.Text);
                        double MtoDelta = DealsIngParaProces[0].delta == null ? 0 : (double)DealsIngParaProces[0].delta;
                        if (MtoFob > MtoDelta)
                        {
                            MsgsValidarIngreso(initObject, 8, PagOri, "");
                            Frm_PlvSO.Tx_MtoFob.Text = MtoDelta.ToString();
                            return 1;
                        }
                    }
                }
            }
            if (PagOri == PaginaOrigen.ARBITRAJES.ToString())
            {
                short a = Fn_Validar_Campos_ARBITRAJE(FrmArbitrajes, 1, 6);
                if (a == 0)
                {
                    return 5;
                }
                Lista_Moneda_Pais MonedaCompra = new Lista_Moneda_Pais();
                Lista_Moneda_Pais MonedaVenta = new Lista_Moneda_Pais();
                MonedaCompra = GetParamComexMonedaPais(FrmArbitrajes.Cb_Moneda_Compra.Value.ToString(), "CodMoneda");
                MonedaVenta = GetParamComexMonedaPais(FrmArbitrajes.Cb_Moneda_Venta.Value.ToString(), "CodMoneda");
                if (PrimerIngreso)
                {
                    if (DealActSel == null || DealActSel == -1) //validar ingreso pizarra
                    {
                        if (MonedaCompra.SiglaMoneda != "USD" && MonedaVenta.SiglaMoneda != "USD")
                        {
                            MsgsValidarIngreso(initObject, 9, PagOri, "");
                        }
                        if (MonedaCompra.SiglaMoneda == "USD" && (double.Parse(FrmArbitrajes.Tx_Mtoarb[3].Text)) > ParamGeneral_ValorMaximoPizarra)  //Venta
                        {
                            MsgsValidarIngreso(initObject, 6, PagOri, "");
                            return 1;
                        }
                        if (MonedaVenta.SiglaMoneda == "USD" && (double.Parse(FrmArbitrajes.Tx_Mtoarb[2].Text)) > ParamGeneral_ValorMaximoPizarra)  //Venta
                        {
                            MsgsValidarIngreso(initObject, 6, PagOri, "");
                            return 1;
                        }
                    }
                    else
                    {
                        pos = ((int)DealActSel - 1) < 0 ? 0 : ((int)DealActSel - 1);
                        if (DealsIngParaProces[pos].moneda1.Trim() != MonedaCompra.SiglaMoneda)
                        {
                            MsgsValidarIngreso(initObject, 1, PagOri, "");
                            return 1;
                        }
                        if (DealsIngParaProces[pos].moneda2.Trim() != MonedaVenta.SiglaMoneda)
                        {
                            MsgsValidarIngreso(initObject, 1, PagOri, "");
                            return 1;
                        }
                        double ParidadIng = double.Parse(FrmArbitrajes.Tx_Mtoarb[1].Text.ToString());
                        ParidadIng = Math.Round(ParidadIng, 2);
                        double ParidadDividida = 1 / double.Parse(FrmArbitrajes.Tx_Mtoarb[1].Text.ToString());
                        ParidadDividida = Math.Round(ParidadDividida, 2);
                        double ParidadSel = double.Parse(DealsIngParaProces[pos].precioCliente.ToString());
                        ParidadSel = Math.Round(ParidadSel, 2);
                        if (ParidadIng != ParidadSel && ParidadDividida != ParidadSel)
                        {
                            MsgsValidarIngreso(initObject, 4, PagOri, "");
                            return 1;
                        }
                        if (MonedaCompra.SiglaMoneda == "USD")
                        {
                            double Monto = string.IsNullOrEmpty(FrmArbitrajes.Tx_Mtoarb[3].Text) ? 0 : double.Parse(FrmArbitrajes.Tx_Mtoarb[3].Text);
                            double MtoDelta = DealsIngParaProces[pos].delta == null ? 0 : (double)DealsIngParaProces[pos].delta;
                            double PorcTole = MtoDelta * ParamGeneralTolerancia_X_Porc;
                            double DeltaOrig_mas_Tole = MtoDelta + PorcTole;
                            if (Monto > DeltaOrig_mas_Tole)
                            {
                                MsgsValidarIngreso(initObject, 8, PagOri, "");
                                FrmArbitrajes.Tx_Mtoarb[3].Text = MtoDelta.ToString();
                                return 1;
                            }
                        }
                        if (MonedaVenta.SiglaMoneda == "USD")
                        {
                            double Monto = string.IsNullOrEmpty(FrmArbitrajes.Tx_Mtoarb[2].Text) ? 0 : double.Parse(FrmArbitrajes.Tx_Mtoarb[2].Text);
                            double MtoDelta = DealsIngParaProces[pos].delta == null ? 0 : (double)DealsIngParaProces[pos].delta;
                            double PorcTole = MtoDelta * ParamGeneralTolerancia_X_Porc;
                            double DeltaOrig_mas_Tole = MtoDelta + PorcTole;
                            if (Monto > DeltaOrig_mas_Tole)
                            {
                                MsgsValidarIngreso(initObject, 8, PagOri, "");
                                FrmArbitrajes.Tx_Mtoarb[2].Text = MtoDelta.ToString();
                                return 1;
                            }
                        }

                    }
                }
                else
                {
                    if (DealsIngParaProces.Count > 0)
                    {
                        if (DealsIngParaProces[0].TipoIngreso == "P")
                        {
                            MsgsValidarIngreso(initObject, 2, PagOri, "");
                            return 1;
                        }
                        else
                        {
                            if (DealActSel == null)
                            {
                                MsgsValidarIngreso(initObject, 3, PagOri, "");
                                return 1;
                            }
                            pos = ((int)DealActSel - 1) < 0 ? 0 : ((int)DealActSel - 1);
                            if (pos > (int)DealsIngParaProces.Count)
                            {
                                pos = 0;
                            }
                            if (DealsIngParaProces[pos].moneda1.Trim() != MonedaCompra.SiglaMoneda)
                            {
                                MsgsValidarIngreso(initObject, 1, PagOri, "");
                                return 1;
                            }
                            if (DealsIngParaProces[pos].moneda2.Trim() != MonedaVenta.SiglaMoneda)
                            {
                                MsgsValidarIngreso(initObject, 1, PagOri, "");
                                return 1;
                            }
                            double ParidadIng = double.Parse(FrmArbitrajes.Tx_Mtoarb[1].Text.ToString());
                            ParidadIng = Math.Round(ParidadIng, 2);
                            double ParidadDividida = 1 / double.Parse(FrmArbitrajes.Tx_Mtoarb[1].Text.ToString());
                            ParidadDividida = Math.Round(ParidadDividida, 2);
                            double ParidadSel = double.Parse(DealsIngParaProces[pos].precioCliente.ToString());
                            ParidadSel = Math.Round(ParidadSel, 2);
                            if (ParidadIng != ParidadSel && ParidadDividida != ParidadSel)
                            {
                                MsgsValidarIngreso(initObject, 4, PagOri, "");
                                return 1;
                            }
                            double SaldoRestante = 0;
                            double TotalIng = 0;
                            if (MonedaCompra.SiglaMoneda == "USD")
                            {
                                double Monto = string.IsNullOrEmpty(FrmArbitrajes.Tx_Mtoarb[3].Text) ? 0 : double.Parse(FrmArbitrajes.Tx_Mtoarb[3].Text);
                                double MtoDelta = DealsIngParaProces[0].delta == null ? 0 : (double)DealsIngParaProces[0].delta;
                                double PorcTole = MtoDelta * ParamGeneralTolerancia_X_Porc;
                                double DeltaOrig_mas_Tole = MtoDelta + PorcTole;
                                double SumaIng = 0;
                                if (DealsIngParaProces.Count() > 0)
                                {
                                    for (int i = 0; i < DealsIngParaProces.Count(); i++)
                                    {
                                        if (pos != i)
                                        {
                                            SumaIng = SumaIng + (double)DealsIngParaProces[i].Monto2_Ingresado;
                                        }
                                    }
                                }
                                TotalIng = SumaIng + Monto;
                                SaldoRestante = DeltaOrig_mas_Tole - SumaIng;
                                if (TotalIng > DeltaOrig_mas_Tole)
                                {
                                    MsgsValidarIngreso(initObject, 13, PagOri, "");
                                    FrmArbitrajes.Tx_Mtoarb[3].Text = SaldoRestante.ToString();
                                    return 1;
                                }
                            }
                            if (MonedaVenta.SiglaMoneda == "USD")
                            {
                                double Monto = string.IsNullOrEmpty(FrmArbitrajes.Tx_Mtoarb[2].Text) ? 0 : double.Parse(FrmArbitrajes.Tx_Mtoarb[2].Text);
                                double MtoDelta = DealsIngParaProces[0].delta == null ? 0 : (double)DealsIngParaProces[0].delta;
                                double PorcTole = MtoDelta * ParamGeneralTolerancia_X_Porc;
                                double DeltaOrig_mas_Tole = MtoDelta + PorcTole;
                                double SumaIng = 0;
                                if (DealsIngParaProces.Count() > 0)
                                {
                                    for (int i = 0; i < DealsIngParaProces.Count(); i++)
                                    {
                                        if (pos != i)
                                        {
                                            SumaIng = SumaIng + (double)DealsIngParaProces[i].Monto1_Ingresado;
                                        }
                                    }
                                }
                                TotalIng = SumaIng + Monto;
                                SaldoRestante = DeltaOrig_mas_Tole - SumaIng;
                                if (TotalIng > DeltaOrig_mas_Tole)
                                {
                                    MsgsValidarIngreso(initObject, 13, PagOri, "");
                                    FrmArbitrajes.Tx_Mtoarb[2].Text = SaldoRestante.ToString();
                                    return 1;
                                }
                            }
                            if (DealsIngParaProces[0].TipoIngreso == "S" && DealsIngParaProces.Count() == 0)
                            {
                                MsgsValidarIngreso(initObject, 10, PagOri, "");
                                return 1;
                            }
                        }
                    }
                }

            }
            return sw;
        }


        public bool COMINV_Validar(InitializationObject initObject, string T) {
            UI_Frm_Comercio_Invisibles Frm_ComInv = initObject.Frm_Comercio_Invisible;
            if (Frm_ComInv.Cb_Moneda.Value == null || Frm_ComInv.Cb_Pais.Value == null)
            {
                return false;
            }
            for (int i = 0; i < 4; i++)
            {
                if ((ResPrecioFinal != 0 && i == 1 && Frm_ComInv.Tx_MtoCV[1].Enabled == true) && (T == "" || T == "P"))
                {
                    Frm_ComInv.Tx_MtoCV[i].Text = ResPrecioFinal.ToString();
                }
                if (string.IsNullOrEmpty(Frm_ComInv.Tx_MtoCV[i].Text))
                {
                    return false;
                }
                else if (double.Parse(Frm_ComInv.Tx_MtoCV[i].Text) == 0 && i != 3)
                {
                    return false;
                }
            }
            T_MODGPYF0 MODGPFY0 = initObject.MODGPYF0;
            T_ModChVrf ModChVrf = initObject.ModChVrf;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
            short a = Fn_Validar_Campos_COMINV(MODGPFY0, ModChVrf, Mdi_Principal, initObject.Frm_Comercio_Invisible, 1, 6);
            if (a == 0)
            {
                return false;
            }
            return true;
        }
        public DateTime GetdateMasXDiasHabiles(DateTime fecha, int AddDias)
        {
            UnitOfWorkCext01 uow = new UnitOfWorkCext01();
            //recuperar feriados desde tb sce_fer desde fecha hasta fecha + 1 mes
            IList<DateTime> feriados = uow.SceRepository.GetFeriadosEntreFechas(fecha, fecha.AddMonths(+1));
            DateTime DiaHabil = fecha;
            for (int i = 0; i < AddDias; i++)
            {
                bool esFechaValida = false;
                while (!esFechaValida)
                {
                    DiaHabil = DiaHabil.AddDays(+1);
                    if (DiaHabil.DayOfWeek != DayOfWeek.Saturday && DiaHabil.DayOfWeek != DayOfWeek.Sunday)
                    {
                        esFechaValida = !feriados.Where(f => f == DiaHabil).Any();
                    }
                }
            }
            return DiaHabil;
        }
        public int CicloQuemarDeal(string rutCliente, Int64 folio, UI_Frm_Ticket frmTicket) 
        {
            int correcto = 0;
            foreach (int CodEst in Enum.GetValues(typeof(EstadoDeal)))
            {
                using (McambioEntities McambioContexto = new McambioEntities())
                {
                    McambioContexto.Database.Connection.Open();
                    SqlParameter[] ParamCambioDeEstadoCFXT = {
                                            new SqlParameter("@rut", rutCliente),
                                            new SqlParameter("@gb_folio", folio),
                                            new SqlParameter("@gb_estado", CodEst),
                                            new SqlParameter("@gb_cod_estado_contable", 1),
                                            new SqlParameter("@gb_canal", ParamGeneralCanal),
                                            new SqlParameter("@gb_responsable", ParamGeneralResponsable),
                                            new SqlParameter("@ls_ejecucion", ""),
                                            new SqlParameter("@eje", SqlDbType.Int)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            },
                                            new SqlParameter("@ls_codret", ""),
                                            new SqlParameter("@codret", SqlDbType.Char, 5)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            },
                                            new SqlParameter("@ls_msgret", ""),
                                            new SqlParameter("@msgret", SqlDbType.VarChar, 150)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            }
                                            
                                        };
                    var varMcambio_SP_CambioDeEstadoCFXT = McambioContexto.Database.SqlQuery<Mcambio_SP_CambioDeEstadoCFXT>(
                                Mcambio_SP_CambioEstCFXT.ToString() + " @rut,@gb_folio,@gb_estado,@gb_cod_estado_contable,@gb_canal,@gb_responsable,@ls_ejecucion = @eje OUTPUT,@ls_codret = @codret OUTPUT,@ls_msgret = @msgret OUTPUT",
                                ParamCambioDeEstadoCFXT).ToList();
                    McambioContexto.Database.Connection.Close();
                    if (ParamCambioDeEstadoCFXT[9].SqlValue.ToString() != "00000")
                    {
                        correcto = 1;
                        frmTicket.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = ParamCambioDeEstadoCFXT[11].SqlValue.ToString(),
                            Title = "Aviso!:   "
                        });
                        break;
                    }
                }
            }
            return correcto;
        }
        /// <summary>
        /// Valida los campos referentes a la Compra Venta.
        /// </summary>
        /// <param name="MODGPYF0"></param>
        /// <param name="ModChVrf"></param>
        /// <param name="Mdi_Principal"></param>
        /// <param name="Frm_Comercio_Invisible"></param>
        /// <param name="CampoInicial"></param>
        /// <param name="CampoFinal"></param>
        /// <returns></returns>
        private static short Fn_Validar_Campos_COMINV(T_MODGPYF0 MODGPYF0, T_ModChVrf ModChVrf, UI_Mdi_Principal Mdi_Principal, UI_Frm_Comercio_Invisibles Frm_Comercio_Invisible, short CampoInicial, short CampoFinal)
        {
            using (var trace = new Tracer("Fn_Validar_Campos_COMINV"))
            {
                trace.AddToContext("Fn_Validar_Campos_COMINV", "Valida los campos referentes a la Comercio invisible.");
                short HayDec = 0, m = 0, HayIde = 0, i = 0;
                string Con = string.Empty, Paso = string.Empty, Msg = string.Empty;

                /// 
                /// Valida Declaración e Informes.
                /// 
                if (Frm_Comercio_Invisible.Fr_Declaracion.Enabled)
                {
                    if (!string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_NroDec.Text) || !string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_FecDec.Text) || !string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_CodAdn.Text))
                    {
                        HayDec = (short)(true ? -1 : 0);
                    }

                }

                m = short.Parse(Frm_Comercio_Invisible.Lt_Tcp.Items.ElementAt(Frm_Comercio_Invisible.Lt_Tcp.ListIndex).ID);
                if (ModChVrf.VCcpl[m].dataut == -1)
                {
                    if (Frm_Comercio_Invisible.Cb_TipAut.Value == -1 && Frm_Comercio_Invisible.Cb_TipAut.Visible == true)
                    {
                        Msg = "Atención: Falta selecionar el tipo de autorización.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Cb_TipAut"
                        });
                        return 0;
                    }

                    if (Frm_Comercio_Invisible.Cb_TipAut.Visible == true)
                    {
                        if (Frm_Comercio_Invisible.Cb_TipAut.Value != 6)
                        {
                            if (string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_NroAut.Text))
                            {
                                Msg = "Atención: Falta el Número de la Autorización del Banco Central.";
                                trace.TraceInformation(Msg);
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = Msg,
                                    ControlName = "Tx_NroAut"
                                });
                                return 0;
                            }

                            if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecAut.Text))
                            {
                                Msg = "Atención: Falta la Fecha de la Autorización del Banco Central.";
                                trace.TraceInformation(Msg);
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = Msg,
                                    ControlName = "Tx_FecAut"
                                });
                                return 0;
                            }

                            if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_SucBcch.Text))
                            {
                                Msg = "Atención: Falta Ingresar el código de la sucursal del Banco Central.";
                                trace.TraceInformation(Msg);
                                Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Informacion,
                                    Text = Msg,
                                    ControlName = "Tx_SucBcch"
                                });
                                return 0;
                            }

                        }

                    }

                }

                ///
                /// No hay datos de Declaración ni de Informe.
                /// 
                if (((Frm_Comercio_Invisible.Fr_Declaracion.Enabled ? -1 : 0) & (~HayDec & ~HayIde)) != 0)
                {
                    Msg = "Debe ingresar datos de la Declaración.";
                    trace.TraceInformation(Msg);
                    Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = Msg
                    });
                    return 0;
                }

                /// 
                /// Hay datos inconclusos de Declaraciones.
                /// 
                if (ModChVrf.VCcpl[m].decexp == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NroDec.Text))
                    {
                        Msg = "Atención: Falta Ingresar el Número de la Declaración.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_NroDec"
                        });
                        return 0;
                    }

                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecDec.Text))
                    {
                        Msg = "Atención: Falta Ingresar la Fecha de la Declaración de Exportación.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_FecDec"
                        });
                        return 0;
                    }

                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_CodAdn.Text))
                    {
                        Msg = "Atención: Falta Ingresar el Código de la Aduana.";
                        trace.TraceInformation(Msg);
                        Frm_Comercio_Invisible.Errors.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = Msg,
                            ControlName = "Tx_CodAdn"
                        });
                        return 0;
                    }

                }

                ///
                /// Hay datos inconclusos de Declaraciones.
                /// Valida el resto de los campos.
                /// 
                for (i = (short)CampoInicial; i <= (short)CampoFinal; i++)
                {
                    switch (i)
                    {

                        case 1:  //Cb_Pais
                            Con = VB6Helpers.Mid(Frm_Comercio_Invisible.Lt_Tcp.Text, 1, 6);
                            if (VB6Helpers.Instr(UI_Frm_Comercio_Invisibles.Concepai, Con) == 0)
                            {
                                if (Frm_Comercio_Invisible.Cb_Pais.Enabled && Frm_Comercio_Invisible.Cb_Pais.ListIndex == -1)
                                {
                                    return 0;
                                }

                            }

                            break;
                        case 2:  //Tx_MtoCV(0).text
                            if (Format.StringToDouble((Frm_Comercio_Invisible.Tx_MtoCV[0].Text)) == 0)
                            {
                                return 0;
                            }

                            break;
                        case 3:  //Tx_MtoCV(1).text
                            if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("0") || Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("1"))
                            {
                                if (Format.StringToDouble((Frm_Comercio_Invisible.Tx_MtoCV[1].Text)) == 0)
                                {
                                    return 0;
                                }

                            }

                            break;
                        case 4:  //Tx_MtoCV(2).text
                            if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("0") || Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("1"))
                            {
                                if (Format.StringToDouble((Frm_Comercio_Invisible.Tx_MtoCV[2].Text)) == 0)
                                {
                                    return 0;
                                }

                            }

                            break;
                        case 5:  //Lt_Tcp
                            if (Frm_Comercio_Invisible.Lt_Tcp.ListIndex == -1)
                            {
                                return 0;
                            }

                            break;
                        case 6:
                            if (string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_MtoCV[3].Text))
                            {
                                return 0;
                            }

                            break;
                    }

                }

                /// 
                /// Valida que haya seleccionado alguna moneda.-
                /// 
                if (Frm_Comercio_Invisible.Cb_Moneda.ListIndex == -1)
                {
                    return 0;
                }

                if (ModChVrf.VCcpl[m].operel == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecPre.Text))
                    {
                        return 0;
                    }

                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NumPre.Text))
                    {
                        return 0;
                    }

                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_CodIns.Text))
                    {
                        return 0;
                    }

                }

                /// 
                /// Operaciones Financieras Internacionales
                /// 
                if (ModChVrf.VCcpl[m].numins == -1 || ModChVrf.VCcpl[m].vtocic == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NumIns.Text))
                    {
                        return 0;
                    }
                }

                if (ModChVrf.VCcpl[m].fecins == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecIns.Text))
                    {
                        return 0;
                    }
                }

                if (ModChVrf.VCcpl[m].finext == -1 || ModChVrf.VCcpl[m].vtocic == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NomFin.Text))
                    {
                        return 0;
                    }
                    if (!(Regex.Match(Frm_Comercio_Invisible.Tx_NomFin.Text, @"^[a-zA-Z\s]{8,}$")).Success)
                    {
                        return 0;
                    }
                }
                if (ModChVrf.VCcpl[m].vtocic == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecVC.Text))
                    {
                        return 0;
                    }
                }
                if (ModChVrf.VCcpl[m].fecdes == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_Fecha.Text))
                    {
                        return 0;
                    }
                }
                if (ModChVrf.VCcpl[m].mondes == -1)
                {
                    if (Frm_Comercio_Invisible.Cb_MonDes.ListIndex == -1)
                    {
                        return 0;
                    }
                }
                if (ModChVrf.VCcpl[m].mtodes == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_Mto.Text))
                    {
                        return 0;
                    }
                }
                /// 
                /// Sectores
                /// 
                if (ModChVrf.VCcpl[m].infimp == -1)
                {
                    if (Frm_Comercio_Invisible.Cb_SecEcBen.ListIndex == -1)
                    {
                        return 0;
                    }
                }
                if (ModChVrf.VCcpl[m].infexp == -1)
                {
                    if (Frm_Comercio_Invisible.Cb_SecEcIn.ListIndex == -1)
                    {
                        return 0;
                    }
                }
                if (ModChVrf.VCcpl[m].datint == -1)
                {
                    if (string.IsNullOrEmpty(Frm_Comercio_Invisible.Tx_PrcPar.Text))
                    {
                        return 0;
                    }
                }
                if (ModChVrf.VCcpl[m].datder == -1)
                {
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecSus.Text))
                    {
                        return 0;
                    }
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_NumCon.Text))
                    {
                        return 0;
                    }
                    if (Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("0") || Frm_Comercio_Invisible.Cb_Divisa.Items.ElementAt(Frm_Comercio_Invisible.Cb_Divisa.ListIndex).ID.Equals("1"))
                    {
                        if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_ParTip.Text))
                        {
                            return 0;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(Frm_Comercio_Invisible.Tx_FecVen.Text))
                    {
                        return 0;
                    }
                    if (Frm_Comercio_Invisible.Cb_InsUt.ListIndex == -1)
                    {
                        return 0;
                    }
                    if (Frm_Comercio_Invisible.Cb_ArCon.ListIndex == -1)
                    {
                        return 0;
                    }
                }
                return 1;
            }
        }
        private static short Fn_Validar_Campos_ARBITRAJE(UI_Frm_Arbitrajes Frm_Arbitraje, short CampoInicial, short CampoFinal)
        {
            short i = 0;
            string Paso = "";
            for (i = (short)CampoInicial; i <= (short)CampoFinal; i++)
            {
                switch (i)
                {

                    case 0:  //Tipo de Cambio
                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[0].Text) == 0)
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta Ingresar el tipo de cambio.",
                                ControlName = "TipoC"
                            });
                            return 0;
                        }

                        break;
                    case 1:  //Cb_Moneda_Compra
                        if (Frm_Arbitraje.Cb_Moneda_Compra.ListIndex == -1)
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta Seleccionar una Moneda de Compra.",
                                ControlName = "Cb_Moneda_Compra"
                            });
                            return 0;
                        }

                        break;
                    case 2:  //Cb_Moneda_Venta
                        if (Frm_Arbitraje.Cb_Moneda_Venta.ListIndex == -1)
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta Seleccionar una Moneda de Venta.",
                                ControlName = "Cb_Moneda_Venta"
                            });
                            return 0;
                        }

                        break;
                    case 3:  //Tx_Mtoarb(1).text
                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[1].Text) == 0)
                        {
                            Frm_Arbitraje.Tx_Mtoarb[1].Enabled = true;
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta Ingresar la Paridad del Arbitraje.",
                                ControlName = "Tx_Paridad_Arbitraje"
                            });
                            return 0;
                        }

                        break;
                    case 4:  //Tx_Mtoarb(2).text
                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[2].Text) == 0)
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta Ingresar el Monto de Compra.",
                                ControlName = "Tx_Monto_Compra"
                            });
                            return 0;
                        }

                        break;
                    case 5:  //Tx_Mtoarb(3).text
                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[3].Text) == 0)
                        {
                            Frm_Arbitraje.Tx_Mtoarb[0].Enabled = true;
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Falta el Monto de Venta.",
                                ControlName = "Tx_Monto_Venta"
                            });
                            return 0;
                        }

                        break;
                    case 6:  //Valida Moneda Iguales en el Arbitraje.-
                        if ((Frm_Arbitraje.Cb_Moneda_Compra.ListIndex == Frm_Arbitraje.Cb_Moneda_Venta.ListIndex) && (Frm_Arbitraje.Cb_Moneda_Compra.ListIndex != -1))
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Para efectuar un Arbitraje debe seleccionar monedas diferentes.",
                                ControlName = "Cb_Moneda_Compra"
                            });
                            return 0;
                        }

                        break;
                    case 7:  //Tx_Mtoarb(4).text
                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[4].Text) == 0)
                        {
                            Frm_Arbitraje.Tx_Mtoarb[4].Enabled = true;
                            Frm_Arbitraje.Tx_Mtoarb[5].Enabled = true;
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Debe Ingresar la Paridad de la Moneda de Compra " + Paso,
                                ControlName = "Tx_Paridad_Plan_Compra"
                            });
                            return 0;
                        }

                        if (Format.StringToDouble(Frm_Arbitraje.Tx_Mtoarb[5].Text) == 0)
                        {
                            Frm_Arbitraje.Tx_Mtoarb[5].Enabled = true;
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Debe Ingresar la Paridad de la Moneda de Venta. " + Paso,
                                ControlName = "Tx_Paridad_Plan_Venta"
                            });
                            return 0;
                        }
                        else
                        {
                            Frm_Arbitraje.Tx_Mtoarb[5].Enabled = false;
                        }

                        break;
                    case 8:  //Lista de pais
                        if (Frm_Arbitraje.Cb_Pais.ListIndex == -1)
                        {
                            Frm_Arbitraje.Errors.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Para efectuar un Arbitraje debe seleccionar pais.",
                                ControlName = "Cb_Pais"
                            });
                            return 0;
                        }
                        break;
                }
            }
            return 1;
        }
        public static bool Fn_ParidArbiSis(InitializationObject initObject) 
        {
            double div = 0;
            UI_Frm_Arbitrajes FrmArbitrajes = initObject.Frm_Arbitrajes;
            if (VB6Helpers.Val(FrmArbitrajes.Tx_Mtoarb[5].Text) != 0 && VB6Helpers.Val(FrmArbitrajes.Tx_Mtoarb[4].Text) != 0)
            {
                div = Format.StringToDouble((FrmArbitrajes.Tx_Mtoarb[5].Text)) / Format.StringToDouble((FrmArbitrajes.Tx_Mtoarb[4].Text));
                if (Math.Abs(Format.StringToDouble((FrmArbitrajes.Tx_Mtoarb[1].Text)) - div) > div * 0.03 )
                {
                    return true;
                }
            }
            return false;
        }
        public void Fn_Anulacion(InitializationObject initObject, string NroSce) 
        {
            string msgMdiPrinc = "";
            if (initObject.Mdi_Principal.MESSAGES.Count() > 0)
            {
                msgMdiPrinc = initObject.Mdi_Principal.MESSAGES[0].Text.ToString();
            }
            GetParamComexgGenerales();
            if (ParamGeneral_FlagAnulacion == 1 && (msgMdiPrinc == msgOkAnulacionDia || msgMdiPrinc == ""))
            {
                string stRut = "";  // formatearRut(Rut, true);
                double dbMto = 0;
                double dbMtoNegativo = 0;   // Mto * -1;
                string stNroSce = NroSce;
                int Id_chileFx = 0;
                int error = 0;
                stNroSce = NroSce + new string('0', (19 - NroSce.Length));
                //llamar a SP [sce_anulacion] para recuperar IdChileFx, monto Y RUT
                using (cext01Entities ctxSceAnulCext01 = new cext01Entities())
                {
                    ctxSceAnulCext01.Database.Connection.Open();
                    SqlParameter[] ParamAnulacion = {
                                                    new SqlParameter("@nro_sce", stNroSce),
                                                    new SqlParameter("@id_chilefx", ""),
                                                    new SqlParameter("@idChFx", SqlDbType.Int)
                                                    {
                                                        Direction = System.Data.ParameterDirection.Output
                                                    },

                                                    new SqlParameter("@mto_chilefx", ""),
                                                    new SqlParameter("@mtoChFx", SqlDbType.Float)
                                                    {
                                                        Direction = System.Data.ParameterDirection.Output
                                                    },

                                                    new SqlParameter("@rut_chilefx", ""),
                                                    new SqlParameter("@rutchilefx", SqlDbType.VarChar, 12)
                                                    {
                                                        Direction = System.Data.ParameterDirection.Output
                                                    },

                                                    new SqlParameter("@cod_error", ""),
                                                    new SqlParameter("@codErr", SqlDbType.Int)
                                                    {
                                                        Direction = System.Data.ParameterDirection.Output
                                                    },
                                                    new SqlParameter("@msg_error", ""),
                                                    new SqlParameter("@msgErr", SqlDbType.VarChar, 100)
                                                    {
                                                        Direction = System.Data.ParameterDirection.Output
                                                    }
                                                };
                    var varParamAnulacion = ctxSceAnulCext01.Database.SqlQuery<Cext01_sp_sce_anulacion_net>(
                            Cext01_SP_SceAnulacion_net.ToString() + " @nro_sce,@id_chilefx = @idChFx OUTPUT,@mto_chilefx = @mtoChFx OUTPUT,@rut_chilefx = @rutchilefx OUTPUT,@cod_error = @codErr OUTPUT,@msg_error = @msgErr OUTPUT",
                            ParamAnulacion).ToList();
                    ctxSceAnulCext01.Database.Connection.Close();
                    if (ParamAnulacion[8].SqlValue.ToString().Trim() != "0")
                    {
                        initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = ParamAnulacion[10].SqlValue.ToString(),
                            Type = TipoMensaje.Informacion,
                            AutoClose = true
                        });
                        error = 1;
                    }
                    else
                    {
                        Id_chileFx = int.Parse(ParamAnulacion[2].SqlValue.ToString());
                        dbMto = double.Parse(ParamAnulacion[4].SqlValue.ToString());
                        dbMtoNegativo = double.Parse(ParamAnulacion[4].SqlValue.ToString()) * -1;
                        stRut = formatearRut(ParamAnulacion[6].SqlValue.ToString(), true);
                    }
                }
                if (error == 0)
                {
                    //llamar a SP [IncorporacionNumeroOperacionSCE] con valor en negativo
                    using (McambioEntities contextMcambio = new McambioEntities())
                    {
                        contextMcambio.Database.Connection.Open();
                        SqlParameter[] ParamAnularIncorpNumOpeCSECompletar = {
                                            new SqlParameter("@rutCliente", stRut.ToString()),
                                            new SqlParameter("@numeroDealChileFX", Id_chileFx),
                                            new SqlParameter("@numeroOperacionSCE", stNroSce.ToString()),
                                            new SqlParameter("@montoSCE", (double)dbMtoNegativo),  //(double)datSPMcambioCDD[idx].Monto1_Ingresado),
                                            new SqlParameter("@montoChileFX", (double)dbMtoNegativo),
                                            new SqlParameter("@codigoEstadoContable", 7),
                                            new SqlParameter("@glosaError", "anual deal"),
                                            new SqlParameter("@exepcionado", DBNull.Value),
                                            new SqlParameter("@vbJefatura", DBNull.Value),
                                            new SqlParameter("@ls_codret", ""),
                                            new SqlParameter("@codret", SqlDbType.Char, 5)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            },
                                            new SqlParameter("@ls_msgret", ""),
                                            new SqlParameter("@msgret", SqlDbType.VarChar, 150)
                                            {
                                                Direction = System.Data.ParameterDirection.Output
                                            }
                                        };
                        var varIncorpNumOpeCSECompletar = contextMcambio.Database.SqlQuery<Mcambio_SP_IncorporacionNumeroOperacionSCE>(
                                    Mcambio_SP_IncorpNumOpeCSE.ToString() + " @rutCliente,@numeroDealChileFX,@numeroOperacionSCE,@montoSCE,@montoChileFX,@codigoEstadoContable,@glosaError,@exepcionado,@vbJefatura,@ls_codret = @codret OUTPUT,@ls_msgret = @msgret OUTPUT",
                                    ParamAnularIncorpNumOpeCSECompletar).ToList();
                        contextMcambio.Database.Connection.Close();
                        if (ParamAnularIncorpNumOpeCSECompletar[10].SqlValue.ToString().Trim() != "000")
                        {
                            initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = ParamAnularIncorpNumOpeCSECompletar[12].SqlValue.ToString() + " (" + ParamAnularIncorpNumOpeCSECompletar[10].SqlValue.ToString() + ")",
                                Type = TipoMensaje.Informacion,
                                Title = "Aviso!:   ",
                                AutoClose = true
                            });
                            error = 1;
                        }
                    }
                    if (error == 0)
                    {
                        //llamar a SP [sce_Upd_rel_deal_numsce] para actualizar estado a 9 en tabla log [rel_deal_numsce]
                        using (cext01Entities ctxSceUpdRelDealNSce = new cext01Entities())
                        {
                            ctxSceUpdRelDealNSce.Database.Connection.Open();
                            SqlParameter[] ParamUpdRelDealNSce = {
                                                    new SqlParameter("@nro_sce", stNroSce.ToString()),
                                                    new SqlParameter("@id_chilefx", Id_chileFx),
                                                    new SqlParameter("@mto_liberar", dbMto),
                                                    new SqlParameter("@cod_error", ""),
                                                    new SqlParameter("@codErr", SqlDbType.Int)
                                                    {
                                                        Direction = System.Data.ParameterDirection.Output
                                                    },
                                                    new SqlParameter("@msg_error", ""),
                                                    new SqlParameter("@msgErr", SqlDbType.VarChar, 100)
                                                    {
                                                        Direction = System.Data.ParameterDirection.Output
                                                    }
                                                };
                            var varParamUpdRelDealNSce = ctxSceUpdRelDealNSce.Database.SqlQuery<Cext01_sp_sce_Upd_rel_deal_numsce>(
                                    Cext01_SP_SceUpdRelDealNumsce.ToString() + " @nro_sce,@id_chilefx,@mto_liberar,@cod_error = @codErr OUTPUT,@msg_error = @msgErr OUTPUT",
                                    ParamUpdRelDealNSce).ToList();
                            ctxSceUpdRelDealNSce.Database.Connection.Close();
                            if (ParamUpdRelDealNSce[4].SqlValue.ToString().Trim() != "0")
                            {
                                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = ParamUpdRelDealNSce[6].SqlValue.ToString(),
                                    Type = TipoMensaje.Informacion,
                                    AutoClose = true
                                });
                                error = 1;
                            }
                            else
                            {
                                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = ParamUpdRelDealNSce[6].SqlValue.ToString(),
                                    Type = TipoMensaje.Correcto,
                                    AutoClose = true
                                });
                            }
                        }
                    }
                }
            }
        }
        public string ObtenerDato(
                                    List<Mcambio_Consulta_Deals_Disponible> datSPMcambioCDD, 
                                    List<DealsIngresadosParaProcesar> DealsIngParaProces,
                                    string PagOri, int pos, string Dato) 
        {
            string valor = "";
            //se consumio por completo el deal 
            if (datSPMcambioCDD.Count == 0)
            {
                if (DealsIngParaProces.Count > 0)
                {
                    switch (Dato)
                    {
                        case "m1":
                            valor = DealsIngParaProces[0].moneda1.Trim();
                            break;
                        case "m2":
                            valor = DealsIngParaProces[0].moneda2.Trim();
                            break;
                        case "pi":
                            valor = DealsIngParaProces[0].Paridad_Ingresada.ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                switch (Dato)
                {
                    case "m1":
                        valor = datSPMcambioCDD[pos].moneda1.Trim();
                        break;
                    case "m2":
                        valor = datSPMcambioCDD[pos].moneda2.Trim();
                        break;
                    case "pi":
                        valor = datSPMcambioCDD[pos].Paridad_Ingresada.ToString();
                        break;
                    default:
                        break;
                }
            }
            return valor;
        }
    }
}