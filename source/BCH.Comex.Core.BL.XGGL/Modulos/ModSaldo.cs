using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Services;
using System;
using System.Linq;

using BCH.Comex.Common.Tracing;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class ModSaldo
    {
        public static int SyGet_NodoSal(DatosGlobales Globales)
        {
            T_ModSaldo ModSaldo = Globales.ModSaldo;


            int SyGet_NodoSal = 0;

            string NOM_BASEINI = "";

            string ofici = "";
            int bien = 0;
            string Sql = "";
            string gsQuery = "";
            string gcAux = "";
            string gistatus = "";
            string noddst = "";

            // 

            SyGet_NodoSal = false.ToInt();

            try
            {

                ModSaldo.SerSalCCL =Globales.DatosUsuario.SaldosCtaCte_SerSalCCL;
                if (ModSaldo.SerSalCCL == "")
                {
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type=Common.UI_Modulos.TipoMensaje.Error,
                        Text= "No se pudo encontrar el Servidor STB20 para Consultar Cuenta Corriente en Línea. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema."
                    });
                    return SyGet_NodoSal;
                }

                ModSaldo.VisSalMN = Globales.DatosUsuario.SaldosCtaCte_VisSalMN;
                if (ModSaldo.VisSalMN == "")
                {
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "No se pudo encontrar la Vista '0049' para Consultar Cuenta Corriente en Línea. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema."
                    });
                    return SyGet_NodoSal;
                }

                ModSaldo.VisSalME = Globales.DatosUsuario.SaldosCtaCte_VisSalME;
                if (ModSaldo.VisSalME == "")
                {
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "No se pudo encontrar la Vista '0112' para Consultar Cuenta Corriente en Línea. La aplicación no puede ejecutarse en estas condiciones. Reporte este problema."
                    });
                    return SyGet_NodoSal;
                }

                ModSaldo.NodoSalME = Globales.DatosUsuario.SaldosCtaCte_NodoSalME;
                if (ModSaldo.NodoSalME == "")
                {
                    Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                    {
                        Type = Common.UI_Modulos.TipoMensaje.Error,
                        Text = "No se pudo encontrar el Nodo para Consultar el Saldo de la Cuenta Corriente ME ( NodoSalME ). La aplicación no puede ejecutarse en estas condiciones. Reporte este problema."
                    });
                    return SyGet_NodoSal;
                }

                SyGet_NodoSal = true.ToInt();

                // 
                // 

                return SyGet_NodoSal;

            }
            catch (Exception exc)
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type=Common.UI_Modulos.TipoMensaje.Error,
                    Text= "Cuenta Corriente en Línea"
                });
                SyGet_NodoSal = false.ToInt();
            }
            return SyGet_NodoSal;
        }

        public static double Obtiene_Monto(DatosGlobales Globales,UnitOfWorkCext01 unit, int IdCta_CtaCteMN, int IdCta_CtaCteME, int numcta, string ctacte)
        {
            T_Modswen Modswen = Globales.Modswen;
            T_ModSaldo ModSaldo = Globales.ModSaldo;
            double Obtiene_Monto = 0.0;

            int mto_num = 0;
            string signo_lincre = "";
            string mto_lincre = "";
            int largo = 0;
            int val_sig = 0;
            int val_num = 0;
            string X = "";
            string tiene_lincre = "";
            string signo = "";
            string sal_vis = "";
            string res = "";
            string llamada = "";
            int bien = 0;

            double monto_saldo = 0.0;
            using (Tracer tracer = new Tracer("Obtiene_monto desde xggl"))
            {
                // 
                // Karina Rojas
                try
                {
                    Modswen.RutAis = String.Empty;
                    if (Modswen.RutAis.TrimB() == "")
                    {
                        Modswen.RutAis = "12345678";
                    }

                    if (IdCta_CtaCteMN == numcta)
                    {
                        try
                        {
                            monto_saldo = (double)XCFTServices.ConsultaSaldoCuenta(ctacte, "0");
                            tracer.TraceVerbose("Calculando el saldo CtaCte...");
                            tracer.AddToContext("saldoCtaCte", monto_saldo);
                        }
                        catch (Exception _ex)
                        {
                            tracer.TraceException("Alerta, problemas al leer saldo de la cuenta", _ex);
                            return 0;
                        }

                        if (monto_saldo <= 0)
                        {
                            return 0;
                        }
                        llamada = Modswen.RutAis.TrimB() + ModSaldo.VisSalMN + "L" + MigrationSupport.Utils.Format(ctacte.Str().TrimB(), "000000000000");
                    }
                    else if (IdCta_CtaCteME == numcta || T_MODGCON0.IdCta_ChqCCME == numcta)
                    {
                        try
                        {
                            monto_saldo = (double) XCFTServices.ConsultaSaldoCuenta(ctacte, "0");
                            tracer.TraceVerbose("Calculando el saldo CtaCte...");
                            tracer.AddToContext("saldoCtaCte", monto_saldo);
                        }
                        catch (Exception _ex)
                        {
                            tracer.TraceException("Alerta, problemas al leer saldo de la cuenta", _ex);
                            return 0;
                        }
                        llamada = Modswen.RutAis.TrimB() + ModSaldo.VisSalME + "L" + MigrationSupport.Utils.Format(ctacte.Str().TrimB(), "000000000000");
                    }

                    res = RespuestaSalVis(ctacte);
                    if (res == "-1")
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Error al leer el Saldo de la Cuenta Corriente N° : " + ctacte.Str().TrimB() + " . Reporte este problema."
                        });
                        return Obtiene_Monto;
                    }
                    if (res == "")
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Es probable que la Cuenta Corriente N° " + ctacte.Str().TrimB() + " No Exista ."
                        });
                        return Obtiene_Monto;
                    }


                    if (IdCta_CtaCteMN == numcta)
                    {
                        //if (res.TrimB() != "")
                        //{
                        //    sal_vis = res.Mid(38, 11);
                        //    signo = sal_vis.Mid(11, 1);
                        //    tiene_lincre = res.Mid(337, 2);

                        //    // busca el signo del saldo
                        //    X = SyGet_Signo(unit, signo);
                        //    // val_num% = Mid(X$, 1, 1)
                        //    // val_sig% = Mid(X$, 2, 1)

                        //    if (X.Len() >= 1)
                        //    {
                        //        val_num = (X.Mid(1, 1)).ToInt();
                        //    }

                        //    if (X.Len() >= 2)
                        //    {
                        //        val_sig = (X.Mid(2, X.Len() - 1)).ToInt();
                        //    }


                        //    largo = sal_vis.Len();
                        //    sal_vis = sal_vis.Mid(1, largo - 1) + val_num.Str().TrimB();

                        //    //  D.S.B.
                        //    // sal_num% = Mid(sal_vis$, 1, largo%)
                        //    // monto_saldo# = sal_num% * val_sig%
                        //    monto_saldo = sal_vis.ToVal() * val_sig;


                        //    if (tiene_lincre == "00")
                        //    {
                        //        mto_lincre = res.Mid(374, 11);
                        //        signo_lincre = mto_lincre.Mid(11, 1);
                        //        // busca el signo del monto de la linea
                        //        X = SyGet_Signo(unit, signo_lincre);

                        //        // val_num% = Mid(X$, 1, 1)
                        //        // val_sig% = Mid(X$, 2, 1)

                        //        if (X.Len() >= 1)
                        //        {
                        //            val_num = (X.Mid(1, 1)).ToInt();
                        //        }

                        //        if (X.Len() >= 2)
                        //        {
                        //            val_sig = (X.Mid(2, X.Len() - 1)).ToInt();
                        //        }

                        //        largo = mto_lincre.Len();
                        //        mto_lincre = mto_lincre.Mid(1, largo - 1) + val_num.Str().TrimB();
                        //        mto_num = (mto_lincre.Mid(1, largo)).ToInt();
                        //        monto_saldo = monto_saldo + mto_num * val_sig;
                        //    }

                        //}

                    }
                    else if (IdCta_CtaCteME == numcta || T_MODGCON0.IdCta_ChqCCME == numcta)
                    {
                        //if (res.TrimB() != "")
                        //{
                        //    sal_vis = res.Mid(56, 13);
                        //    signo = sal_vis.Mid(13, 1);

                        //    // busca el signo del saldo
                        //    X = SyGet_Signo(unit, signo);
                        //    if (X.Len() >= 1)
                        //    {
                        //        val_num = (X.Mid(1, 1)).ToInt();
                        //    }

                        //    if (X.Len() >= 2)
                        //    {
                        //        val_sig = (X.Mid(2, X.Len() - 1)).ToInt();
                        //    }


                        //    largo = sal_vis.Len();
                        //    sal_vis = sal_vis.Mid(1, largo - 1) + val_num.Str().TrimB();

                        //    //  D.S.B.
                        //    // sal_num% = Mid(sal_vis$, 1, largo%)
                        //    // sal_num% = Mid(sal_vis$, 1, largo%)
                        //    // monto_saldo# = sal_num% * val_sig%

                        //    monto_saldo = sal_vis.ToInt() * val_sig / 100;

                        //}
                    }

                    Obtiene_Monto = monto_saldo;

                    return Obtiene_Monto;

                }
                catch (Exception exc)
                {
                    Obtiene_Monto = -1;
                    tracer.TraceException("Alerta, problemas al obtener saldo de la cuenta", exc);
                }
            }
            return Obtiene_Monto;
        }

        public static string RespuestaSalVis(string llave)
        {
            try
            {
                var s = XCFTServices.ConsultaCuentaCorriente(llave);
                return s;
            }
            catch (Exception ex)
            {
                return "-1";
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
    }
}
