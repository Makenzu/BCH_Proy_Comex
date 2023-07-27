using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public static class MODABDC
    {
        public static T_MODABDC GetMODABDC()
        {
            return new T_MODABDC();
        }

        public static short DesActivaBD(InitializationObject initObject, UnitOfWorkCext01 unit, dynamic OpeSin, string CodAnu)
        {
            using (var trace = new Tracer("DesActivaBD"))
            {
                trace.AddToContext("DesActivaBD", "Desactivar la OP de la Base de datos: SP sce_gen_d02");
                T_MODGCON0 MODGCON0 = initObject.MODGCON0;
                try
                {
                    string a = VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3);
                    string b = VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2);
                    string c = VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2);
                    string d = VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3);
                    string e = VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5);
                    unit.SceRepository.EjecutarSP<object>("sce_gen_d02", a, b, c, d, e, CodAnu, MODGCON0.VMch.NroRpt.ToString(), MODGSYB.dbdatesy(MODGCON0.VMch.fecmov));
                    return -1;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    throw new Exception("Se ha producido un error al tratar de desactivar los registros anulados (Gen_D02).", _ex);
                }
            }
        }

        //Activa todos los Registros de las Bases de Datos.-
        public static short ActivaBases(InitializationObject initObj, UnitOfWorkCext01 unit, string OpeSin, string CodAnu)
        {
            // UPGRADE_INFO (#05B1): The 'x' variable wasn't declared explicitly.
            short x = 0;
            // UPGRADE_INFO (#05B1): The 'n' variable wasn't declared explicitly.
            short n = 0;
            // UPGRADE_INFO (#05B1): The 'i' variable wasn't declared explicitly.
            short i = 0;
            //Contabilidad.-
            if (initObj.MODGCON0.VMch.NroRpt > 0)
            {
                x = ActivaBD_Con(initObj, unit);
                if (~x != 0)
                {
                    return 0;
                }
            }

            //Planillas Visibles de Exportación.-
            n = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODXPLN1.VxPlvs); i++)
            {
                if (initObj.MODXPLN1.VxPlvs[i].Estado == T_MODGCON0.ECC_ING)
                {
                    n = (short)(n + 1);
                }

            }

            if (n > 0)
            {
                x = ActivaBD_xPlv(initObj, unit, OpeSin, CodAnu);
                if (~x != 0)
                {
                    return 0;
                }
            }

            //Planillas Anulación de Exportación.-
            n = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODXANU.VxAnus); i++)
            {
                if (initObj.MODXANU.VxAnus[i].Estado == T_MODGCON0.ECC_ING)
                {
                    n = (short)(n + 1);
                }

            }

            if (n > 0)
            {
                x = ActivaBD_xAnu(initObj,unit, OpeSin, CodAnu);
                if (~x != 0)
                {
                    return 0;
                }
            }

            //Swift's.-
            n = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGSWF.VSwf); i++)
            {
                n = (short)(n + 1);
            }

            if (n > 0)
            {
                x = ActivaBD_Swf(initObj,unit, OpeSin, CodAnu);
                if (~x != 0)
                {
                    return 0;
                }
            }

            //Cheques.-
            n = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGCHQ.V_Chq_VVi); i++)
            {
                if (initObj.MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_CHEQUE)
                {
                    n = (short)(n + 1);
                }

            }

            if (n > 0)
            {
                x = ActivaBD_Chq(initObj, unit, OpeSin, CodAnu);
                if (~x != 0)
                {
                    return 0;
                }
            }

            //Vale Vista.-
            n = 0;
            for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGCHQ.V_Chq_VVi); i++)
            {
                if (initObj.MODGCHQ.V_Chq_VVi[i].TipoDoc == T_MODGCHQ.DOCVAL_VALVIS)
                {
                    n = (short)(n + 1);
                }

            }

            if (n > 0)
            {
                x = ActivaBD_Vvi(initObj, unit, OpeSin, CodAnu);
                if (~x != 0)
                {
                    return 0;
                }
            }

            //-------------------------------------------------------------------------------------------------------------------------------------------------
            //Accenture-Código Nuevo-Inicio
            //Fecha Modificación 22022012
            //Responsable: Angel Donoso Gonzalez.
            //Versión:
            //Descripción : se agrega nueva condición y ciclo que busca si tiene transferencias internas.
            //--------------------------------------------------------------------------------------------------------------------------------------------------
            initObj.MODABDC.Ftin = 0;
            if (initObj.MODGPLI1.Vplis.Length > 0)
            {
                for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGPLI1.Vplis); i++)
                {
                    if (initObj.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_CVD)
                    {
                        //TCvd_CVD = 1 compray venta(invisible)
                        if (initObj.MODGCVD.VgPli[i].TipCVD == "TIN" && initObj.MODGCVD.TIN == true)
                        {
                            initObj.MODABDC.Ftin = 1;
                            initObj.MODGCVD.TIN = false;
                        }

                    }

                }

            }

            if (initObj.MODGCVD.TIN == false)
            {
                if (initObj.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_CVD)
                {
                    //TCvd_CVD = 1 compray venta(invisible)
                    if (initObj.MODABDC.Ftin == 1)
                    {
                        initObj.MODGCVD.TIN = true;
                    }

                }

                //--------------------------------------------------------------------------------------------------
                // Accenture - Código Nuevo - Termino
                //--------------------------------------------------------------------------------------------------
                //--------------------------------------------------------------------------------------------------
                // Accenture - Antiguo Código  - Inicio
                // Fecha Modificación 22022012
                // Responsable: Angel Donoso Gonzalez.
                // Versión:
                // Descripción : se deja como comentario antigua condición
                //--------------------------------------------------------------------------------------------------
                //If TIN = False
                //--------------------------------------------------------------------------------------------------
                // Accenture - Antiguo Código - Termino
                //--------------------------------------------------------------------------------------------------
                //Planillas Invisibles.-
                n = 0;
                for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGPLI1.Vplis); i++)
                {
                    if (initObj.MODGPLI1.Vplis[i].Estado == T_MODGCON0.ECC_ING && initObj.MODGCVD.TIN == false)
                    {
                        n++;
                    }
                }

                //se verifica si hay que activar al menos una planilla
                var activar = initObj.MODGPLI1.Vplis.Any(c => c.Estado == T_MODGCON0.ECC_ING && int.Parse(c.NumPli) > 0);

                if (n > 0 || activar)
                {
                    x = ActivaBD_Pli(initObj, unit, OpeSin, CodAnu);
                    if (~x != 0)
                    {
                        return 0;
                    }
                }

                //Datos Compra/Venta.-
                n = 0;
                for (i = 0; i <= (short)VB6Helpers.UBound(initObj.MODGCVD.VgPli); i++)
                {
                    if (initObj.MODGCVD.VgPli[i].Status == T_MODGCON0.ECC_ING)
                    {
                        n = (short)(n + 1);
                    }

                }

                //Planillas de Reemplazo
                if (~BCH.Comex.Core.BL.XCFT.Modulos.MODPREEM.Fn_ActivaBDPlv(initObj, unit) != 0)
                {
                    return 0;
                }

            }

            if (n > 0)
            {
                x = ActivaBD_Cov(initObj, unit, OpeSin);
                if (~x != 0)
                {
                    return 0;
                }
            }

            return (short)(true ? -1 : 0);
        }

        //Activa Compra/Venta.-
        public static short ActivaBD_Cov(InitializationObject initObj, UnitOfWorkCext01 unit, string OpeSin)
        {
            using (var trace = new Tracer("ActivaBD_Cov: Activa Compra/Venta"))
            {
                short _retValue = 0;
                try
                {
                    string par1 = MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 1, 3));
                    string par2 = MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 4, 2));
                    string par3 = MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 6, 2));
                    string par4 = MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 8, 3));
                    string par5 = MODGSYB.dbcharSy(VB6Helpers.Mid(OpeSin, 11, 5));
                    string par6 = T_MODGCON0.ECC_ING.ToString();

                    trace.TraceInformation("Datos para sce_cov_u01_MS: NroOP: {0}, estado: {1}", OpeSin, par6);
                    _retValue = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(initObj.Mdl_Funciones_Varias, () =>
                    {                        
                        return (short)unit.SceRepository.sce_cov_u01_MS(par1, par2, par3, par4, par5, par6);
                    });
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = "Activación de Registros"
                    });
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }

        //Activa Planillas Invisibles.-        
        public static short ActivaBD_Pli(InitializationObject initObj, UnitOfWorkCext01 unit, dynamic OpeSin, string CodAnu)
        {
            using (var trace = new Tracer("ActivaBD_Pli: Activa Planillas Invisibles"))
            {
                short _retValue = 0;
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObj.Mdl_Funciones_Varias;
                string Que = "";

                try
                {
                    trace.TraceInformation("Datos para sce_pli_u02_MS: NroOP: {0}, CodAnu: {1}, estado: {2}", OpeSin, CodAnu, T_MODGCON0.ECC_ING);
                    _retValue = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(Mdl_Funciones_Varias, () =>
                    {                        
                        return (short)unit.SceRepository.sce_pli_u02_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5)), CodAnu, T_MODGCON0.ECC_ING);
                    });

                    return _retValue;
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = "Activación de Registros"
                    });
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }

        //Activa Vales Vistas.-        
        public static short ActivaBD_Vvi(InitializationObject initObj, UnitOfWorkCext01 unit, dynamic OpeSin, string CodAnu)
        {
            using (var trace = new Tracer("ActivaBD_Vvi: Activa Vales Vistas"))
            {
                short _retValue = 0;
                string Que = "";
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObj.Mdl_Funciones_Varias;
                try
                {
                    trace.TraceInformation("Datos para sce_vvi_u02_MS: NroOP: {0}, CodAnu: {1}, estado: {2}", OpeSin, CodAnu, T_MODGCON0.ECC_ING);
                    _retValue = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(Mdl_Funciones_Varias, () =>
                    {                        
                        return (short)unit.SceRepository.sce_vvi_u02_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5)), CodAnu, T_MODGCON0.ECC_ING);
                    });
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = T_MODGCON0.MsgCon
                    });
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }

        //Activa Cheques.-
        public static short ActivaBD_Chq(InitializationObject initObj, UnitOfWorkCext01 unit, dynamic OpeSin, string CodAnu)
        {
            using (var trace = new Tracer("ActivaBD_Chq: Activa Cheques"))
            {
                short _retValue = 0;
                string Que = "";

                try
                {
                    trace.TraceInformation("Datos para sce_chq_u02_MS: NroOP: {0}, CodAnu: {1}, estado: {2}", OpeSin, CodAnu, T_MODGCON0.ECC_ING);
                    _retValue = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(initObj.Mdl_Funciones_Varias, () =>
                    {                        
                        return (short)unit.SceRepository.sce_chq_u02_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5)), CodAnu, T_MODGCON0.ECC_ING);
                    });
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = T_MODGCON0.MsgCon
                    });
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }

        //Activa los Swift's.-        
        public static short ActivaBD_Swf(InitializationObject initObj,UnitOfWorkCext01 unit, dynamic OpeSin, string CodAnu)
        {
            using (var trace = new Tracer("ActivaBD_Swf: Activa los Swift's"))
            {
                short _retValue = 0;
                string Que = "";

                try
                {
                    trace.TraceInformation("Datos para sce_swf_u01_MS: NroOP: {0}, CodAnu: {1}, estado: {2}", OpeSin, CodAnu, T_MODGCON0.ECC_ING);
                    _retValue = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(initObj.Mdl_Funciones_Varias, () =>
                    {                        
                        return (short)unit.SceRepository.sce_swf_u01_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5)), CodAnu, T_MODGCON0.ECC_ING);
                    });

                    return _retValue;
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = T_MODGCON0.MsgCon
                    });
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }

        //Activa las Planillas de Anulación Exportaciones.-        
        public static short ActivaBD_xAnu(InitializationObject initObj,UnitOfWorkCext01 unit, dynamic OpeSin, string CodAnu)
        {
            using (var trace = new Tracer("ActivaBD_xAnu: Activa las Planillas de Anulación Exportaciones"))
            {
                short _retValue = 0;
                string Que = "";

                try
                {
                    trace.TraceInformation("Datos para sce_xanu_u02_MS: NroOP: {0}, CodAnu: {1}, estado: {2}", OpeSin, CodAnu, T_MODGCON0.ECC_ING);
                    _retValue = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(initObj.Mdl_Funciones_Varias, () =>
                    {                        
                        return (short)unit.SceRepository.sce_xanu_u02_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5)), CodAnu, T_MODGCON0.ECC_ING);
                    });
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = "Activación de Registros"
                    });
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }

        //Activa Planilla Visible Export.-
        public static short ActivaBD_xPlv(InitializationObject initObj, UnitOfWorkCext01 unit, dynamic OpeSin, string CodAnu)
        {
            using (var trace = new Tracer("ActivaBD_xPlv: Activa Planilla Visible Export"))
            {
                short _retValue = 0;
                string Que = "";

                try
                {
                    trace.TraceInformation("Datos para sce_xplv_u02_MS: NroOP: {0}, CodAnu: {1}, estado: {2}", OpeSin, CodAnu, T_MODGCON0.ECC_ING);
                    _retValue = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(initObj.Mdl_Funciones_Varias, () =>
                    {                        
                        return (short)unit.SceRepository.sce_xplv_u02_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(VB6Helpers.CStr(OpeSin), 11, 5)), CodAnu, T_MODGCON0.ECC_ING);
                    });

                    return _retValue;
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = "Activación de Registros"
                    });
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }

        //Activa un Reporte Contable.-
        public static short ActivaBD_Con(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            using (var trace = new Tracer("ActivaBD_Con: Activa un Reporte Contable"))
            {
                short _retValue = 0;
                string Que = "";

                try
                {
                    trace.TraceInformation("Datos para sce_mch_u02: NroRpt: {0}, fecmov: {1}, estado: {2}", initObj.MODGCON0.VMch.NroRpt, MODGSYB.dbdatesy(initObj.MODGCON0.VMch.fecmov), T_MODGCON0.ECC_ING);
                    _retValue = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Put_New(initObj.Mdl_Funciones_Varias, () =>
                    {                        
                        return (short)unit.SceRepository.sce_mch_u02_MS(initObj.MODGCON0.VMch.NroRpt, MODGSYB.dbdatesy(initObj.MODGCON0.VMch.fecmov), T_MODGCON0.ECC_ING);
                    });
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
                        Title = "Activación de Registros"
                    });
                    trace.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }
    }
}
