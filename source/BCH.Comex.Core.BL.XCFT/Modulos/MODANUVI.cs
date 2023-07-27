using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODANUVI
    {
        public static T_MODANUVI GetMODANUVI()
        {
            return new T_MODANUVI();
        }

        //Refunde montos cuando se trata de la misma cuenta, mismo nemonico de cuenta,
        //misma moneda
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Pr_Refundir(InitializationObject initObj)
        {
            // PROBAR ESTO
            short n = 0;
            short m = 0;
            short i = 0;
            short Encontro = 0;
            short j = 0;
            short x = 0;
            short a = 0;

            n = 0;

            initObj.MODGCON0.VMcdz = new T_Mcd[0];
            n = (short)VB6Helpers.UBound(initObj.MODGCON0.VMcds);
            m = (short)VB6Helpers.UBound(initObj.MODGCON0.VMcdz);

            for (i = 0; i <= (short)n; i++)
            {

                m = (short)VB6Helpers.UBound(initObj.MODGCON0.VMcdz);
                Encontro = (short)(false ? -1 : 0);
                if (VB6Helpers.Instr(VB6Helpers.Trim(initObj.MODGCON0.VMcds[i].NemCta), "CONV") != 0 || VB6Helpers.Instr(VB6Helpers.Trim(initObj.MODGCON0.VMcds[i].NemCta), "CAMBIO") != 0)
                {
                    for (j = 0; j <= (short)m; j++)
                    {
                        ///****************** ACA
                        if ((initObj.MODGCON0.VMcds[i].NumCta == initObj.MODGCON0.VMcdz[j].NumCta) && (initObj.MODGCON0.VMcds[i].cod_dh == initObj.MODGCON0.VMcdz[j].cod_dh) && (initObj.MODGCON0.VMcds[i].CodMon == initObj.MODGCON0.VMcdz[j].CodMon) && (initObj.MODGCON0.VMcds[i].NemCta == initObj.MODGCON0.VMcdz[j].NemCta) && (initObj.MODGCON0.VMcds[i].TipCam == initObj.MODGCON0.VMcdz[j].TipCam))
                        {
                            Encontro = (short)(true ? -1 : 0);
                            initObj.MODGCON0.VMcdz[j].mtomcd += initObj.MODGCON0.VMcds[i].mtomcd;
                            break;
                        }

                    }

                }

                if (Encontro == 0)
                {

                    m = (short)(VB6Helpers.UBound(initObj.MODGCON0.VMcdz));
                    VB6Helpers.RedimPreserve(ref initObj.MODGCON0.VMcdz, 0, ++m);
                    initObj.MODGCON0.VMcdz[m] = initObj.MODGCON0.VMcds[i].Copy();
                }

            }

            initObj.MODGCON0.VMcds = new T_Mcd[1];
            x = (short)(VB6Helpers.UBound(initObj.MODGCON0.VMcdz) + 1);
            initObj.MODGCON0.VMcds = new T_Mcd[x];
            for (a = 0; a < x; a++)
            {
                initObj.MODGCON0.VMcds[a] = initObj.MODGCON0.VMcdz[a].Copy();
            }
        }

        //Imprime Planilla Visible de Anulacion.-
        public static void Pr_ImprPlan(InitializationObject initObject, short Copias_Pln)
        {
            T_MODANUVI MODANUVI = initObject.MODANUVI;
            T_MODVPLE MODVPLE = initObject.MODVPLE;

            short Tot_Anu = 0;
            short i = 0;
            short x = 0;
            short Impresora = 0;
            short k = 0;
            short xx = 0;
            string RR = string.Empty;
            string NroAcs = string.Empty;
            Tot_Anu = (short)VB6Helpers.UBound(MODANUVI.V_PlAnu);


            VB6Helpers.DoEvents();
            for (i = 0; i <= (short)Tot_Anu; i++)
            {
                for (x = 1; x <= (short)Copias_Pln; x++)
                {
                    ImprimirPlanilla pl = new ImprimirPlanilla();
                    //Numero Presentacion y Codigo 205000
                    pl.NumPre = MODANUVI.V_PlAnu[i].NumPre.ToString().PadLeft(6, '0');
                    pl.V_PlAnu_Codigo = MODANUVI.V_PlAnu[i].Codigo;
                    //-------------------------------------
                    //Fecha Venta
                    //-----------------------------------
                    pl.FecVen = DateTime.Parse(MODANUVI.V_PlAnu[i].FecVen).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //Plaza Banco Central y Codigo
                    //-----------------------------------
                    pl.NomPla = MODANUVI.V_PlAnu[i].NomPla;
                    pl.V_PlAnu_CodBch = MODANUVI.V_PlAnu[i].CodBch.ToString().PadLeft(2, '0');
                    //Importador
                    //-----------------------------------
                    pl.V_PlAnu_NomImp = MODANUVI.V_PlAnu[i].NomImp;
                    RR = MODGFYS.Rut_Formateado(MODANUVI.V_PlAnu[i].RutImp);
                    pl.V_PlAnu_RutImp = RR;
                    //Pais de Pago
                    //-----------------------------------
                    if (MODANUVI.V_PlAnu[i].NomPai != string.Empty)
                    {
                        string _tempVar1 = VB6Helpers.Mid(MODANUVI.V_PlAnu[i].NomPai, 1, 17);
                        pl.V_PlAnu_NomPai = MODGFYS.Escribe_Nombre(ref _tempVar1);
                        pl.V_PlAnu_CodPPa = MODANUVI.V_PlAnu[i].CodPPa.ToString().PadLeft(2, '0');
                    }

                    //Moneda
                    //-----------------------------------
                    pl.V_PlAnu_NomMon = MODGFYS.Escribe_Nombre(ref MODANUVI.V_PlAnu[i].NomMon);
                    pl.V_PlAnu_CodMPa = MODANUVI.V_PlAnu[i].CodMPa.ToString().PadLeft(3, '0');
                    //Informe de Importacion
                    //-----------------------------------
                    if (VB6Helpers.Format(VB6Helpers.CStr(MODANUVI.V_PlAnu[i].NumIdi), "000000") != "000000")
                    {
                        pl.V_PlAnu_NumIdi = VB6Helpers.Format(VB6Helpers.CStr(MODANUVI.V_PlAnu[i].NumIdi), "000000");
                        pl.V_PlAnu_FecIdi = DateTime.Parse(MODANUVI.V_PlAnu[i].FecIdi).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        pl.V_PlAnu_CodPla = VB6Helpers.Format(VB6Helpers.CStr(MODANUVI.V_PlAnu[i].CodPla), "00");
                    }

                    //Forma de Pago
                    //-----------------------------------
                    pl.V_PlAnu_CodPag = MODANUVI.V_PlAnu[i].CodPag.ToString().PadLeft(2, '0');
                    //Conocimiento de Embarque
                    //-----------------------------------
                    pl.V_PlAnu_NumCon = MODANUVI.V_PlAnu[i].NumCon;
                    pl.V_PlAnu_FecCon = DateTime.Parse(MODANUVI.V_PlAnu[i].FecCon).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //Vencimiento Operacion
                    //-----------------------------------
                    pl.V_PlAnu_FecVto = DateTime.Parse(MODANUVI.V_PlAnu[i].FecVto).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //Impresion del Detalle de los Montos Cubiertos en la Planilla
                    //-----------------------------------
                    pl.V_PlAnu_MtoFob = MODGFYS.PrintMonto(MODANUVI.V_PlAnu[i].MtoFob);
                    //Acuerdos.-
                    //-----------------------------------
                    if (MODANUVI.V_PlAnu[i].HayAcu != 0)
                    {
                        pl.V_PlAnu_NumAcu = MODANUVI.V_PlAnu[i].NumAcu.ToString().PadLeft(1, '0');
                        NroAcs = "    " + VB6Helpers.Trim(MODANUVI.V_PlAnu[i].Acuer1) + "    ";
                        pl.V_PlAnu_Acuer1 = NroAcs;
                    }

                    //Convenio Credito
                    //-----------------------------------
                    if (MODANUVI.V_PlAnu[i].FecDeb != string.Empty)
                    {
                        pl.V_PlAnu_FecDeb = DateTime.Parse(MODANUVI.V_PlAnu[i].FecDeb).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    if (MODANUVI.V_PlAnu[i].DocChi != string.Empty)
                    {
                        pl.V_PlAnu_DocChi = MODANUVI.V_PlAnu[i].DocChi;
                    }

                    if (MODANUVI.V_PlAnu[i].DocExt != string.Empty)
                    {
                        pl.V_PlAnu_DocExt = MODANUVI.V_PlAnu[i].DocExt;
                    }

                    pl.V_PlAnu_MtoFle = MODGFYS.PrintMonto(MODANUVI.V_PlAnu[i].MtoFle);
                    pl.V_PlAnu_MtoSeg = MODGFYS.PrintMonto(MODANUVI.V_PlAnu[i].MtoSeg);
                    pl.V_PlAnu_MtoCif = MODGFYS.PrintMonto(MODANUVI.V_PlAnu[i].MtoCif);

                    //Cuadro de Pagos.-
                    //-----------------------------------
                    if (MODANUVI.V_PlAnu[i].HayCua != 0)
                    {
                        if (MODANUVI.V_PlAnu[i].NumCua != 0)
                        {
                            pl.V_PlAnu_NumCua = VB6Helpers.Format(VB6Helpers.CStr(MODANUVI.V_PlAnu[i].NumCua), "0");
                        }
                        if (MODANUVI.V_PlAnu[i].numcuo != 0)
                        {
                            pl.V_PlAnu_numcuo = VB6Helpers.Format(VB6Helpers.CStr(MODANUVI.V_PlAnu[i].numcuo), "0");
                        }
                    }

                    pl.V_PlAnu_MtoInt = MODGFYS.PrintMonto(MODANUVI.V_PlAnu[i].MtoInt);
                    pl.V_PlAnu_MtoGas = MODGFYS.PrintMonto(MODANUVI.V_PlAnu[i].MtoGas);

                    //Datos de Anulacion
                    //-----------------------------------
                    pl.V_PlAnu_FecAnu = DateTime.Parse(MODANUVI.V_PlAnu[i].FecAnu).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    pl.V_PlAnu_ParAnu = Format.FormatCurrency(MODANUVI.V_PlAnu[i].ParAnu, "0.0000");
                    pl.V_PlAnu_MtoTot = MODGFYS.PrintMonto(MODANUVI.V_PlAnu[i].MtoTot);
                    pl.V_PlAnu_CifDol = MODGFYS.PrintMonto(MODANUVI.V_PlAnu[i].CifDol);
                    pl.V_PlAnu_TotAnu = Format.FormatCurrency(MODANUVI.V_PlAnu[i].TotAnu, "#,###,###,###,##0.00");
                    pl.V_PlAnu_TotDol = MODGFYS.PrintMonto(MODANUVI.V_PlAnu[i].TotDol);
                    //-----------------------------------
                    //Tipo de cambio y paridad
                    //-----------------------------------
                    pl.V_PlAnu_TipCamo = Format.FormatCurrency(MODANUVI.V_PlAnu[i].TipCamo, "0.0000");
                    pl.V_PlAnu_ParPag = Format.FormatCurrency(MODANUVI.V_PlAnu[i].ParPag, "0.0000");

                    //Observaciones
                    //-----------------------------------
                    if (MODANUVI.V_PlAnu[i].ObsDec != string.Empty)
                    {
                        pl.V_PlAnu_ObsDec = MODANUVI.V_PlAnu[i].ObsDec;
                    }
                    if (MODANUVI.V_PlAnu[i].ObsPar != string.Empty)
                    {
                        pl.V_PlAnu_ObsPar = MODANUVI.V_PlAnu[i].ObsPar;
                    }
                    if (MODANUVI.V_PlAnu[i].ObsMer != string.Empty)
                    {
                        pl.V_PlAnu_ObsMer = MODANUVI.V_PlAnu[i].ObsMer;
                    }
                    if (MODANUVI.V_PlAnu[i].observ != string.Empty)
                    {
                        pl.V_PlAnu_observ = MODANUVI.V_PlAnu[i].observ;
                    }
                    if (MODANUVI.V_PlAnu[i].ObsCob != string.Empty)
                    {
                        pl.V_PlAnu_ObsCob = MODANUVI.V_PlAnu[i].ObsCob;
                    }

                    //Detalle de Intereses de anulacion
                    //-----------------------------------
                    Pr_ImpDetInt(pl, ref MODVPLE.IntAnu, MODANUVI.V_PlAnu[i].NumPre);
                    //-----------------------------------
                    initObject.Planillas.Add(pl);
                    string paramStr = "Impresion/Planillas/ImprimirPlanillaReemplazos?codigoCentroCosto={0}&codigoProducto={1}&codigoEspecialista={2}&codigoEmpresa={3}&codigoCobranza={4}&numeroPlanilla={5}&fechaVenta={6}";
                    string urlStr = string.Format(paramStr, MODANUVI.V_PlAnu[i].codcct, MODANUVI.V_PlAnu[i].codpro, MODANUVI.V_PlAnu[i].codesp, MODANUVI.V_PlAnu[i].codofi, MODANUVI.V_PlAnu[i].codope, pl.NumPre, DateTime.Parse(pl.FecVen).ToString("yyyy-MM-dd"));

                    initObject.DocumentosAImprimir.Add(new DataImpresion()
                    {
                        URL = urlStr,
                        NumeroOperacion = MODANUVI.V_PlAnu[i].codcct + MODANUVI.V_PlAnu[i].codpro + MODANUVI.V_PlAnu[i].codesp + MODANUVI.V_PlAnu[i].codofi + MODANUVI.V_PlAnu[i].codope,
                        nroPresentacion = pl.NumPre,
                        fechaOp = DateTime.Parse(pl.FecVen),
                        tipoDoc = 6,
                        fileName = initObject.MODGCVD.VgCvd.OpeSin
                    });
                }

            }
        }

        //Imprime el detalle de los intereses de anulacion o reemplazo.-
        public static void Pr_ImpDetInt(Planilla pl, ref T_IntPla[] Interes, int NumPla)
        {
            short Impresora = 0;
            short pp = 0;
            dynamic ind = null;
            short k = 0;
            short xx = 0;
            short MaxDet = 0;
            short yy = 0;
            short pos_vert = 0;
            short cont = 0;
            short NDet = 0;
            double tint = 0;
            string Concepto = string.Empty;
            string tip = string.Empty;
            string s = string.Empty;
            string sss = string.Empty;
            string SS = string.Empty;

            pp = VB6Helpers.CShort(ind);


            MaxDet = -1;

            MaxDet = (short)VB6Helpers.UBound(Interes);
            //--- Parametros de Impresion ------
            //xx% = 3
            //yy% = -8
            yy = k;
            //----------------------------------
            pos_vert = (short)(223 + yy);  //antes 225 + yy%
            cont = 0;

            for (NDet = 0; NDet <= (short)MaxDet; NDet++)
            {
                if (Interes[NDet] != null)
                {
                    if (VB6Helpers.Format(VB6Helpers.CStr(NumPla), "000000") == VB6Helpers.Format(VB6Helpers.CStr(Interes[NDet].NroPln), "000000") && Interes[NDet].FlgEli != -1)
                    {
                        cont = (short)(cont + 1);
                        tint += Interes[NDet].MtoInt;
                        pos_vert = (short)(pos_vert + 4);
                        Detalle di = new Detalle();
                        di.Cont = MODGPYF0.forma(cont, "00");

                        short _switchVar1 = Interes[NDet].Concep;
                        if (_switchVar1 == 0)
                        {
                            Concepto = "CIF";
                        }
                        else if (_switchVar1 == 1)
                        {
                            Concepto = "CYF";
                        }
                        else if (_switchVar1 == 2)
                        {
                            Concepto = "CYS";
                        }
                        else if (_switchVar1 == 3)
                        {
                            Concepto = "FLT";
                        }
                        else if (_switchVar1 == 4)
                        {
                            Concepto = "FOB";
                        }
                        else if (_switchVar1 == 5)
                        {
                            Concepto = "SEG";
                        }
                        di.Concepto = Concepto;
                        short _switchVar2 = Interes[NDet].TipInt;
                        if (_switchVar2 == 1)
                        {
                            tip = "PR";
                        }
                        else if (_switchVar2 == 3)
                        {
                            tip = "IC";
                        }
                        else if (_switchVar2 == 4)
                        {
                            tip = "BA";
                        }
                        di.Tip = tip;

                        s = Format.FormatCurrency(Interes[NDet].CapBas, "0.00");
                        sss = VB6Helpers.CStr(MODGFYS.formatnum(VB6Helpers.Trim(s), 0, 15));
                        di.CapBas = sss;
                        di.CodBas = VB6Helpers.Format(VB6Helpers.CStr(Interes[NDet].CodBas), "0");
                        di.Tasa = Format.FormatCurrency(Interes[NDet].TasInt, "0.0000");
                        di.FecIni = DateTime.Parse(Interes[NDet].FecIni).ToString("dd/MM/yyyy");
                        di.FecFin = DateTime.Parse(Interes[NDet].FecFin).ToString("dd/MM/yyyy");
                        di.NumDia = VB6Helpers.Format(VB6Helpers.CStr(Interes[NDet].NumDia), "0");

                        s = Format.FormatCurrency(Interes[NDet].MtoInt, "0.00");
                        SS = VB6Helpers.CStr(MODGFYS.formatnum(VB6Helpers.Trim(s), 0, 15));
                        di.Mto = SS;
                        pl.DetInt.Add(di);
                    }
                }
            }

            if (tint > 0)
            {

                s = Format.FormatCurrency(tint, "0.00");
                SS = VB6Helpers.CStr(MODGFYS.formatnum(VB6Helpers.Trim(s), 0, 15));
                pl.Tint = SS;
            }

        }

        //Actualiza los datos de anulacion : Fecha Anulacion,Paridad y Total Anulacion.-
        public static short Fn_SyPutAnu(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
                T_MODANUVI MODANUVI = initObject.MODANUVI;
                T_MODGUSR MODGUSR = initObject.MODGUSR;

                short _retValue = 0;
                short Tot_Anu = 0;
                short i = 0;
                string Que = string.Empty;

                _retValue = (short)(false ? -1 : 0);
                Tot_Anu = (short)VB6Helpers.UBound(MODANUVI.V_PlAnu);
                try
                {
                    for (i = 0; i <= (short)Tot_Anu; i++)
                    {
                        int indice = i;
                        List<string> parameters = new List<string>();
                        parameters.Add(MODGSYB.dbcharSy(MODANUVI.V_PlAnu[indice].codcct));
                        parameters.Add(MODGSYB.dbcharSy(MODANUVI.V_PlAnu[indice].codpro));
                        parameters.Add(MODGSYB.dbcharSy(MODANUVI.V_PlAnu[indice].codesp));
                        parameters.Add(MODGSYB.dbcharSy(MODANUVI.V_PlAnu[indice].codofi));
                        parameters.Add(MODGSYB.dbcharSy(MODANUVI.V_PlAnu[indice].codope));
                        parameters.Add(MODGSYB.dbnumesy(MODANUVI.V_PlAnu[indice].NumPre));
                        parameters.Add(MODGSYB.dbdatesy(MODANUVI.V_PlAnu[indice].FecAnu));
                        parameters.Add(MODGSYB.dbPardSy(MODANUVI.V_PlAnu[indice].ParAnu));
                        parameters.Add(MODGSYB.dblogisy(-1));  //Hay anulacion
                        parameters.Add(MODGSYB.dbcharSy(MODANUVI.V_PlAnu[indice].observ));
                        parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.CentroCosto));
                        parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.Especialista));

                        //-------------------------------------------
                        //Se ejecuta el Procedimiento Almacenado.
                        //-------------------------------------------
                        if (~Mdl_Funciones_Varias.Cmd_Put_New(initObject.Mdl_Funciones_Varias, () =>
                    {
                        return (short)unit.SceRepository.EjecutarSPConRetorno("sce_plan_u11", String.Empty, parameters.ToArray());
                    }) != 0)
                        {
                            return _retValue;
                        }
                    }
                    _retValue = (short)(true ? -1 : 0);
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Error al actualizar los datos de planillas de anulación",
                        Type = TipoMensaje.Error
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        //Grabamos los datos de autorizacion.-
        public static short Fn_SyPutAut(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
                T_MODANUVI MODANUVI = initObject.MODANUVI;
                short _retValue = 0;
                short Tot_Anu = 0;
                short i = 0;
                string Que = string.Empty;

                _retValue = (short)(false ? -1 : 0);

                Tot_Anu = (short)VB6Helpers.UBound(MODANUVI.V_PlAnu);
                try
                {
                    for (i = 0; i <= (short)Tot_Anu; i++)
                    {
                        int indice = i;
                        List<string> parameters = new List<string>();
                        parameters.Add(MODGSYB.dbnumesy(MODANUVI.V_PlAnu[indice].NumPre));
                        parameters.Add(MODGSYB.dbdatesy(MODANUVI.V_PlAnu[indice].FecVen));
                        parameters.Add(MODGSYB.dbcharSy(MODANUVI.V_PlAnu[indice].TipAut));
                        parameters.Add(MODGSYB.dbnumesy(MODANUVI.V_PlAnu[indice].NumAut));
                        parameters.Add(MODGSYB.dbdatesy(MODANUVI.V_PlAnu[indice].FecAut));
                        parameters.Add(DateTime.Now.ToString("yyyy-MM-dd"));
                        parameters.Add(MODGSYB.dbnumesy(T_MODGCVD.EstadoIng));

                        //-------------------------------------------
                        //Se ejecuta el Procedimiento Almacenado.
                        //-------------------------------------------
                        if (~Mdl_Funciones_Varias.Cmd_Put_New(initObject.Mdl_Funciones_Varias, () =>
                    {
                        return (short)unit.SceRepository.EjecutarSPConRetorno("sce_dabc_w01", String.Empty, parameters.ToArray());
                    }) != 0)
                        {
                            return _retValue;
                        }
                    }

                    _retValue = (short)(true ? -1 : 0);
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Error al grabar los datos de autorizacion de planillas de anulación.",
                        Type = TipoMensaje.Error
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }

        public static short Fn_SyPutReba(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            T_MODCVDIMMM MODCVDIMMM = initObject.MODCVDIMMM;
            T_Module1 Module1 = initObject.Module1;
            T_MODGCON0 MODGCON0 = initObject.MODGCON0;
            T_MODANUVI MODANUVI = initObject.MODANUVI;

            short _retValue = 0;
            short TotReb = 0;
            short i = 0;
            string Que = string.Empty;
            dynamic MsgCvd_Imp = null;

            _retValue = (short)(false ? -1 : 0);


            TotReb = (short)VB6Helpers.UBound(MODCVDIMMM.AnuCob);

            for (i = 0; i <= (short)TotReb; i++)
            {
                int indice = i;
                List<string> parameters = new List<string>();

                parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Cent_Costo));
                parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Product));
                parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Especia));
                parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Empresa));
                parameters.Add(MODGSYB.dbcharSy(Module1.Codop.Id_Operacion));
                parameters.Add(MODGSYB.dbnumesy(VB6Helpers.Val(MODCVDIMMM.AnuCob[indice].NumPla)));
                parameters.Add(DateTime.Now.ToString("yyyy-MM-dd"));
                parameters.Add(MODGSYB.dbnumesy(MODGCON0.VMch.NroRpt));
                parameters.Add(MODGSYB.dbnumesy(MODANUVI.Var_CodMon));
                if (~Mdl_Funciones_Varias.Cmd_Put_New(initObject.Mdl_Funciones_Varias, () =>
                 {
                     return (short)unit.SceRepository.EjecutarSPConRetorno("sce_reb_u02", String.Empty, parameters.ToArray());
                 }) != 0)
                {
                    return _retValue;
                }
            }

            _retValue = (short)(true ? -1 : 0);
            return _retValue;
        }

        public static short Fn_LeePlnSy(InitializationObject initObj, UnitOfWorkCext01 uow, short IndPln)
        {
            using (var tracer = new Tracer())
            {
                short _retValue = 0;
                string NombreUs = string.Empty;
                string RR = string.Empty;
                string Pais = string.Empty;
                string CodP = string.Empty;
                short PosPais = 0;
                short CodPais = 0;
                string NomP = string.Empty;
                short CodMon = 0;
                short ind = 0;
                double Mercader = 0;
                double HastaFob = 0;
                string Acuerdo3 = string.Empty;
                string VenAnu = string.Empty;
                int NumPln_R = 0;
                string FecPln_R = string.Empty;
                int codpln_r = 0;
                short CodEnt_R = 0;
                string NumInf_R = string.Empty;
                string FecInf_R = string.Empty;
                int PlzInf_R = 0;
                string NumCon_R = string.Empty;
                string FecCon_R = string.Empty;
                short HayRpl = 0;

                IList<sce_plan_s17_MS_Result> _rs_plan_s17;
                IList<sce_plan_s16_MS_Result> _rs_plan_s16;
                try
                {
                    _rs_plan_s17 = uow.SceRepository.sce_plan_s17_MS(
                        MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPln].codcct),
                        MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPln].codpro),
                        MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPln].codesp),
                        MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPln].codofi),
                        MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPln].codope),
                        Convert.ToDecimal(MODGSYB.dbnumesy(initObj.MODANUVI.V_PlAnu[IndPln].NumPre))
                        );

                    _rs_plan_s16 = uow.SceRepository.sce_plan_s16_MS(
                        MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPln].codcct),
                        MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPln].codpro),
                        MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPln].codesp),
                        MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPln].codofi),
                        MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPln].codope),
                        Convert.ToInt32(MODGSYB.dbnumesy(initObj.MODANUVI.V_PlAnu[IndPln].NumPre)),
                        Convert.ToDateTime(MODGSYB.dbdatesy(initObj.MODANUVI.V_PlAnu[IndPln].FecVen))
                        ).ToList();

                    if (_rs_plan_s17.Count() < 1 || _rs_plan_s16.Count() < 1)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Error al Leer Datos de Planillas Visibles.",
                            Title = T_MODANUVI.MsgAnuVi,
                            Type = TipoMensaje.Informacion
                        });

                    }

                    if ((_rs_plan_s17.Count() == _rs_plan_s16.Count()) && (_rs_plan_s17.Count() > 0))
                    {
                        initObj.MODANUVI.V_PlAnu[IndPln].codcct = _rs_plan_s17[0].cent_costo;
                        initObj.MODANUVI.V_PlAnu[IndPln].codpro = _rs_plan_s17[0].id_product;
                        initObj.MODANUVI.V_PlAnu[IndPln].codesp = _rs_plan_s17[0].id_especia;
                        initObj.MODANUVI.V_PlAnu[IndPln].codofi = _rs_plan_s17[0].id_empresa;
                        initObj.MODANUVI.V_PlAnu[IndPln].codope = _rs_plan_s17[0].id_cobranz;

                        NombreUs = string.Empty;
                        initObj.MODANUVI.V_PlAnu[IndPln].NumPre = Convert.ToInt32(_rs_plan_s17[0].num_presen);
                        RR = VB6Helpers.Trim(VB6Helpers.CStr(_rs_plan_s17[0].rut));

                        int _switchVar1 = VB6Helpers.Len(RR);
                        if (_switchVar1 == 8)
                        {
                            RR = "00" + RR;
                        }
                        else if (_switchVar1 == 9)
                        {
                            RR = "0" + RR;
                        }

                        initObj.MODANUVI.V_PlAnu[IndPln].RutImp = RR;
                        initObj.MODANUVI.V_PlAnu[IndPln].PrtImp = VB6Helpers.Trim(VB6Helpers.CStr(_rs_plan_s17[0].party));
                        initObj.MODANUVI.V_PlAnu[IndPln].NomImp = VB6Helpers.Trim(VB6Helpers.CStr(_rs_plan_s17[0].nomimport));
                        initObj.MODANUVI.V_PlAnu[IndPln].FecVen = VB6Helpers.CStr(_rs_plan_s17[0].Column1);
                        initObj.MODANUVI.V_PlAnu[IndPln].numdec = VB6Helpers.Trim(VB6Helpers.CStr(_rs_plan_s17[0].num_dec));
                        initObj.MODANUVI.V_PlAnu[IndPln].FecDec = VB6Helpers.CStr(_rs_plan_s17[0].Column2);
                        initObj.MODANUVI.V_PlAnu[IndPln].NumCon = VB6Helpers.Trim(VB6Helpers.CStr(_rs_plan_s17[0].num_con));
                        initObj.MODANUVI.V_PlAnu[IndPln].FecCon = VB6Helpers.CStr(_rs_plan_s17[0].Column3);
                        initObj.MODANUVI.V_PlAnu[IndPln].Codigo = VB6Helpers.CStr(_rs_plan_s17[0].codigo);
                        initObj.MODANUVI.V_PlAnu[IndPln].CodBch = VB6Helpers.CShort(_rs_plan_s17[0].codbcch);  //Bco.Central
                        initObj.MODANUVI.V_PlAnu[IndPln].CodPla = VB6Helpers.CShort(_rs_plan_s17[0].cod_plaza);
                        initObj.MODANUVI.V_PlAnu[IndPln].NomPla = VB6Helpers.Trim(VB6Helpers.CStr(_rs_plan_s17[0].nombplaza));
                        initObj.MODANUVI.V_PlAnu[IndPln].CodPag = VB6Helpers.CShort(_rs_plan_s17[0].forma_pag);
                        initObj.MODANUVI.V_PlAnu[IndPln].CodPPa = VB6Helpers.CShort(_rs_plan_s17[0].codpais);
                        initObj.MODANUVI.V_PlAnu[IndPln].NomPai = VB6Helpers.Trim(VB6Helpers.CStr(_rs_plan_s17[0].nompais));
                        Pais = VB6Helpers.Trim(_rs_plan_s17[0].nompais);

                        if (Pais == string.Empty)
                        {
                            CodP = VB6Helpers.Format(VB6Helpers.CStr(initObj.MODANUVI.V_PlAnu[IndPln].CodPPa), "000");
                            PosPais = MODGTAB0.Get_VPai(initObj, uow, CodPais);
                            NomP = initObj.MODGTAB0.VPai[PosPais].Pai_PaiNom;
                            initObj.MODANUVI.V_PlAnu[IndPln].NomPai = NomP;
                        }
                        else
                        {
                            initObj.MODANUVI.V_PlAnu[IndPln].NomPai = Pais;
                        }

                        initObj.MODANUVI.V_PlAnu[IndPln].CodMon = VB6Helpers.CShort(_rs_plan_s17[0].codmone);
                        initObj.MODANUVI.V_PlAnu[IndPln].CodMPa = initObj.MODGTAB0.VMnd[MODGTAB0.Get_VMnd(initObj.MODGTAB0, uow, initObj.MODANUVI.V_PlAnu[IndPln].CodMon)].Mnd_MndCbc;
                        initObj.MODANUVI.V_PlAnu[IndPln].NomMon = VB6Helpers.Trim(VB6Helpers.CStr(_rs_plan_s17[0].nommone));

                        CodMon = initObj.MODANUVI.V_PlAnu[IndPln].CodMon;
                        ind = MODGTAB0.Get_VMndBC(initObj.MODGTAB0, uow, CodMon);
                        initObj.MODANUVI.V_PlAnu[IndPln].NemMon = initObj.MODGTAB0.VMnd[ind].Mnd_MndNmc;

                        initObj.MODANUVI.V_PlAnu[IndPln].ParPag = (double)(_rs_plan_s17[0].paridad);
                        initObj.MODANUVI.V_PlAnu[IndPln].TipCamo = (double)(_rs_plan_s17[0].tipo_camb);
                        Mercader = (double)(_rs_plan_s17[0].mercaderia);
                        HastaFob = (double)(_rs_plan_s17[0].hasta_fob);
                        initObj.MODANUVI.V_PlAnu[IndPln].MtoFob = (double)(_rs_plan_s17[0].mtofob);
                        initObj.MODANUVI.V_PlAnu[IndPln].MtoFle = (double)(_rs_plan_s17[0].mtoflete);
                        initObj.MODANUVI.V_PlAnu[IndPln].MtoSeg = (double)(_rs_plan_s17[0].mtoseguro);

                        initObj.MODANUVI.V_PlAnu[IndPln].MtoCif = (double)(_rs_plan_s17[0].mtocif);
                        initObj.MODANUVI.V_PlAnu[IndPln].MtoInt = (double)(_rs_plan_s17[0].mtointer);
                        initObj.MODANUVI.V_PlAnu[IndPln].MtoGas = (double)(_rs_plan_s17[0].mtogastos);
                        initObj.MODANUVI.V_PlAnu[IndPln].MtoTot = (double)(_rs_plan_s17[0].mtototal);
                        initObj.MODANUVI.V_PlAnu[IndPln].CifDol = (double)(_rs_plan_s17[0].cifdolar);
                        initObj.MODANUVI.V_PlAnu[IndPln].TotDol = (double)(_rs_plan_s17[0].totaldolar);
                        initObj.MODANUVI.V_PlAnu[IndPln].FecVto = VB6Helpers.CStr(_rs_plan_s17[0].Column4);

                        //FIN 1ª PARTE.
                        //-------------

                        initObj.MODANUVI.V_PlAnu[IndPln].HayCua = VB6Helpers.CShort(_rs_plan_s16[0].haycuadro);
                        initObj.MODANUVI.V_PlAnu[IndPln].NumCua = VB6Helpers.CShort(_rs_plan_s16[0].numcuadro);
                        initObj.MODANUVI.V_PlAnu[IndPln].numcuo = VB6Helpers.CShort(_rs_plan_s16[0].numcuota);

                        initObj.MODANUVI.V_PlAnu[IndPln].HayAcu = VB6Helpers.CShort(_rs_plan_s16[0].hayacuerdo);
                        initObj.MODANUVI.V_PlAnu[IndPln].NumAcu = VB6Helpers.CShort(_rs_plan_s16[0].numacuerdo);
                        initObj.MODANUVI.V_PlAnu[IndPln].Acuer1 = VB6Helpers.CStr(_rs_plan_s16[0].acuerdo1);
                        initObj.MODANUVI.V_PlAnu[IndPln].Acuer2 = VB6Helpers.CStr(_rs_plan_s16[0].acuerdo2);
                        Acuerdo3 = VB6Helpers.CStr(_rs_plan_s16[0].acuerdo3);
                        initObj.MODANUVI.V_PlAnu[IndPln].HayAnu = VB6Helpers.CShort(_rs_plan_s16[0].hayanula);
                        initObj.MODANUVI.V_PlAnu[IndPln].IndAnu = VB6Helpers.CShort(_rs_plan_s16[0].indanula);
                        initObj.MODANUVI.V_PlAnu[IndPln].NumReg = VB6Helpers.CShort(_rs_plan_s16[0].numreg);
                        VenAnu = VB6Helpers.CStr(_rs_plan_s16[0].vencanula);
                        initObj.MODANUVI.V_PlAnu[IndPln].FecAnu = VB6Helpers.CStr(_rs_plan_s16[0].fechaanula);
                        double.TryParse(_rs_plan_s16[0].paranula.ToString(), out initObj.MODANUVI.V_PlAnu[IndPln].ParAnu);
                        double.TryParse(_rs_plan_s16[0].totalanula.ToString(), out initObj.MODANUVI.V_PlAnu[IndPln].TotAnu);
                        initObj.MODANUVI.V_PlAnu[IndPln].FecDeb = VB6Helpers.CStr(_rs_plan_s16[0].fecdebito);
                        initObj.MODANUVI.V_PlAnu[IndPln].DocChi = VB6Helpers.CStr(_rs_plan_s16[0].ndoc1);
                        initObj.MODANUVI.V_PlAnu[IndPln].DocExt = VB6Helpers.CStr(_rs_plan_s16[0].ndoc2);
                        initObj.MODANUVI.V_PlAnu[IndPln].Status = VB6Helpers.CShort(_rs_plan_s16[0].estado);
                        initObj.MODANUVI.V_PlAnu[IndPln].observ = VB6Helpers.Trim(_rs_plan_s16[0].observ);
                        initObj.MODANUVI.V_PlAnu[IndPln].ObsDec = VB6Helpers.Trim(_rs_plan_s16[0].obsdecl);
                        initObj.MODANUVI.V_PlAnu[IndPln].ObsPar = VB6Helpers.Trim(_rs_plan_s16[0].obsparidad);
                        initObj.MODANUVI.V_PlAnu[IndPln].ObsCob = VB6Helpers.Trim(_rs_plan_s16[0].obscobranz);
                        initObj.MODANUVI.V_PlAnu[IndPln].ObsMer = VB6Helpers.Trim(_rs_plan_s16[0].obsmerma);
                        NumPln_R = VB6Helpers.CInt(_rs_plan_s16[0].numpln_r);
                        FecPln_R = VB6Helpers.CStr(_rs_plan_s16[0].fecpln_r);
                        codpln_r = VB6Helpers.CInt(_rs_plan_s16[0].codpln_r);
                        CodEnt_R = VB6Helpers.CShort(_rs_plan_s16[0].codent_r);
                        NumInf_R = VB6Helpers.CStr(_rs_plan_s16[0].numinf_r);
                        FecInf_R = VB6Helpers.CStr(_rs_plan_s16[0].fecinf_r);
                        PlzInf_R = VB6Helpers.CInt(_rs_plan_s16[0].plzinf_r);
                        NumCon_R = VB6Helpers.CStr(_rs_plan_s16[0].numcon_r);
                        FecCon_R = VB6Helpers.CStr(_rs_plan_s16[0].feccon_r);
                        HayRpl = VB6Helpers.CShort(_rs_plan_s16[0].hayrpl);
                        initObj.MODANUVI.V_PlAnu[IndPln].ZonFra = VB6Helpers.CShort(_rs_plan_s16[0].zonfra);

                        initObj.MODCVDIMMM.Ind_Planilla = IndPln;

                        _retValue = (short)(true ? -1 : 0);
                    }
                    return _retValue;
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta", ex);
                    throw;
                }
            }
        }

        /// <summary>
        ///  Lee los Intereses de las planillas seleccionadas en la ventana de anulacion.-
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <param name="IndPlAnu"></param>
        /// <returns></returns>
        public static short Fn_LeeIntPln(InitializationObject initObj, UnitOfWorkCext01 uow, short IndPlAnu)
        {
            using (var tracer = new Tracer())
            {
                short _retValue = 0;
                short nro = 0;
                short cont = 0;
                string ti = string.Empty;

                initObj.MODVPLE.IntAnu = new T_IntPla[1];
                try
                {
                    IList<pro_sce_inpl_s01_MS_Result> _rs;
                    _rs = uow.SceRepository.pro_sce_inpl_s01_MS(MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPlAnu].codcct),
                                                             MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPlAnu].codpro),
                                                             MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPlAnu].codesp),
                                                             MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPlAnu].codofi),
                                                             MODGSYB.dbcharSy(initObj.MODANUVI.V_PlAnu[IndPlAnu].codope),
                                                             Convert.ToDecimal(MODGSYB.dbnumesy(initObj.MODANUVI.V_PlAnu[IndPlAnu].NumPre)));


                    if (_rs.Count() > 0)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Error al leer datos de Interes Planilla Visible.",
                            Title = T_MODANUVI.MsgAnuVi,
                            Type = TipoMensaje.Informacion
                        });

                        return _retValue;
                    }

                    nro = (short)_rs.Count();
                    if (nro > 0)
                    {
                        for (cont = 1; cont <= (short)nro; cont++)
                        {
                            initObj.MODVPLE.IntAnu[cont].codcct = VB6Helpers.CStr(_rs[cont].cent_costo);
                            initObj.MODVPLE.IntAnu[cont].codpro = VB6Helpers.CStr(_rs[cont].id_product);
                            initObj.MODVPLE.IntAnu[cont].codpro = VB6Helpers.CStr(_rs[cont].id_especia);
                            initObj.MODVPLE.IntAnu[cont].codofi = VB6Helpers.CStr(_rs[cont].id_empresa);
                            initObj.MODVPLE.IntAnu[cont].codope = VB6Helpers.CStr(_rs[cont].id_cobranz);
                            initObj.MODVPLE.IntAnu[cont].NroPln = VB6Helpers.CInt(_rs[cont].numplan);
                            initObj.MODVPLE.IntAnu[cont].FecPln = VB6Helpers.CStr(_rs[cont].fecha);
                            initObj.MODVPLE.IntAnu[cont].NroLin = VB6Helpers.CShort(_rs[cont].nro);
                            initObj.MODVPLE.IntAnu[cont].Concep = VB6Helpers.CShort(_rs[cont].concepto);

                            ti = VB6Helpers.Trim(VB6Helpers.CStr(_rs[cont].tipo));
                            switch (ti)
                            {
                                case "PR":
                                    initObj.MODVPLE.IntAnu[cont].TipInt = 1;
                                    break;
                                case "AC":
                                    initObj.MODVPLE.IntAnu[cont].TipInt = 2;
                                    break;
                                case "IC":
                                    initObj.MODVPLE.IntAnu[cont].TipInt = 3;
                                    break;
                                case "BA":
                                    initObj.MODVPLE.IntAnu[cont].TipInt = 4;
                                    break;
                            }

                            initObj.MODVPLE.IntAnu[cont].MtoInt = (double)(_rs[cont].monto);
                            initObj.MODVPLE.IntAnu[cont].CapBas = (double)(_rs[cont].capbas);
                            initObj.MODVPLE.IntAnu[cont].CodBas = (double)(_rs[cont].codbas);
                            initObj.MODVPLE.IntAnu[cont].TasInt = (double)(_rs[cont].tasa);
                            initObj.MODVPLE.IntAnu[cont].FecIni = VB6Helpers.CStr(_rs[cont].fini);
                            initObj.MODVPLE.IntAnu[cont].FecFin = VB6Helpers.CStr(_rs[cont].ffin);
                            initObj.MODVPLE.IntAnu[cont].NumDia = VB6Helpers.CShort(_rs[cont].ndias);
                        }

                    }

                    _retValue = (short)(true ? -1 : 0);
                    return _retValue;
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Error al leer datos de Interes Planilla Visible.",
                        Title = T_MODANUVI.MsgAnuVi,
                        Type = TipoMensaje.Informacion
                    });

                    throw e;
                }
            }
        }

        /// <summary>
        /// Obtenemos el monto total de anulacion de planilla (cambio y conversion)
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        public static void Pr_LlenaMtoAnu(InitializationObject initObj)
        {
            short reg_anu = 0;
            short i = 0;

            reg_anu = (short)VB6Helpers.UBound(initObj.MODANUVI.V_PlAnu);

            for (i = 0; i <= (short)reg_anu; i++)
            {
                if (initObj.MODANUVI.V_PlAnu[i].estado != 9 && initObj.MODANUVI.V_PlAnu[i].HayAnu == 0)
                {
                    initObj.MODANUVI.Vx_AnuReem.CodMon = initObj.MODANUVI.V_PlAnu[i].CodMon;
                    initObj.MODANUVI.Vx_AnuReem.CambAnu = initObj.MODANUVI.Vx_AnuReem.CambAnu + Format.StringToDouble(Format.FormatCurrency((initObj.MODANUVI.V_PlAnu[i].MtoTot * initObj.MODANUVI.V_PlAnu[i].TipCam), "0"));
                    initObj.MODANUVI.Vx_AnuReem.ConvAnu += initObj.MODANUVI.V_PlAnu[i].MtoTot;
                }

            }

        }

        public static short SyGet_PlPrt(InitializationObject initObj, UnitOfWorkCext01 uow, string NumOpe, string FecVen)
        {
            using (var tracer = new Tracer())
            {
                short _retValue = 0;

                try
                {
                    IList<sce_plrm_s02_MS_Result> _rs;
                    _rs = uow.SceRepository.sce_plrm_s02_MS(
                        MODGSYB.dbcharSy(VB6Helpers.Left(NumOpe, 3)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 5, 2)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 2)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 3)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 15, 5)),
                        Convert.ToDateTime(MODGSYB.dbdatesy(FecVen)));

                    if (_rs.Count() > 0)
                    {
                        initObj.MODANUVI.Vx_AnuReem.IndDir = VB6Helpers.CShort(_rs[0].Column1);
                        initObj.MODANUVI.Vx_AnuReem.IndNom = VB6Helpers.CShort(_rs[0].Column2);
                        initObj.MODANUVI.Vx_AnuReem.PrtCli = VB6Helpers.CStr(_rs[0].Column3);
                        _retValue = -1;
                    }
                    else
                    {
                        initObj.MODANUVI.Vx_AnuReem.IndDir = 0;
                        initObj.MODANUVI.Vx_AnuReem.IndNom = 0;
                        initObj.MODANUVI.Vx_AnuReem.PrtCli = "";
                        _retValue = -1;
                    }
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de los datos del party (Sce_iDoc).",
                        Title = T_MODANUVI.MsgAnuVi,
                        Type = TipoMensaje.Informacion
                    });

                    throw e;
                }

                return _retValue;
            }
        }

        /// <summary>
        /// Funcion que rescata las planillas para esa operacion y fecha de venta
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <param name="NumOpe"></param>
        /// <param name="FecVen"></param>
        /// <returns></returns>
        public static short SyGet_PlAnu(InitializationObject initObj, UnitOfWorkCext01 uow, string NumOpe, string FecVen)
        {
            using (var tracer = new Tracer())
            {
                short _retValue = 0;
                short Nro_Reg = 0;
                short i = 0;
                double Mercad = 0;
                double HasFob = 0;

                try
                {
                    IList<sce_plan_s18_MS_Result> _rs;
                    _rs = uow.SceRepository.sce_plan_s18_MS(
                        MODGSYB.dbcharSy(VB6Helpers.Left(NumOpe, 3)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 5, 2)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 2)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 3)),
                        MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 15, 5)),
                        Convert.ToDateTime(MODGSYB.dbdatesy(FecVen))
                        );

                    Nro_Reg = (short)_rs.Count();
                    if (Nro_Reg > 0)
                    {

                        initObj.MODANUVI.V_Plani = _rs.Select(x => new T_AnuVi()
                        {
                            codcct = VB6Helpers.Trim(x.cent_costo),
                            codpro = VB6Helpers.Trim(x.id_product),
                            codesp = VB6Helpers.Trim(x.id_especia),
                            codofi = VB6Helpers.Trim(x.id_empresa),
                            codope = VB6Helpers.Trim(x.id_cobranz),
                            NumPre = VB6Helpers.CInt(x.num_presen),
                            FecVen = VB6Helpers.CStr(x.Column1),
                            numdec = VB6Helpers.CStr(x.num_dec),
                            FecDec = VB6Helpers.CStr(x.Column2),
                            CodMon = VB6Helpers.CShort(x.codmone),
                            CodPag = VB6Helpers.CShort(x.forma_pag),
                            MtoFob = (double)(x.mtofob),
                            MtoFle = (double)(x.mtoflete),
                            MtoSeg = (double)(x.mtoseguro),
                            MtoCif = (double)(x.mtocif),
                            MtoInt = (double)(x.mtointer),
                            MtoGas = (double)(x.mtogastos),
                            MtoTot = (double)(x.mtototal),
                            CifDol = (double)(x.cifdolar),
                            TotDol = (double)(x.totaldolar),
                            ParPag = (double)(x.paridad),
                            TipCam = (double)(x.tipo_camb),
                            IndAnu = VB6Helpers.CShort(x.indanula)
                        }).ToArray();

                        Mercad = (double)(_rs[0].mercaderia);
                        HasFob = (double)(_rs[0].hasta_fob);

                        _retValue = -1;
                    }

                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta", e);

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "No se leyeron los Datos correspondientes a las Planillas",
                        Title = T_MODANUVI.MsgAnuVi,
                        Type = TipoMensaje.Informacion
                    });

                    throw;
                }

                return _retValue;
            }
        }
    }
}
