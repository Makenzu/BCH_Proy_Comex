using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Globalization;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODGFYS
    {
        public static T_MODGFYS GetMODGFYS() {
            return new T_MODGFYS();
        }

        public static string Rut_Formateado(string xrut)
        {
            // Función que formatea un rut (xrut) para desplegarlo de la forma ###.###.###-A

            string xrut1 = "";
            string z = VB6Helpers.Mid(xrut, 1, 3);
            if (VB6Helpers.Mid(z, 1, 3) == "000")
            {
                xrut1 = xrut1;
            }
            else
            {
                if (VB6Helpers.Mid(z, 1, 2) == "00")
                {
                    xrut1 += VB6Helpers.Mid(z, 3, 1);
                }
                else
                {
                    if (VB6Helpers.Mid(z, 1, 1) == "0")
                    {
                        xrut1 += VB6Helpers.Mid(z, 2, 2);
                    }

                }

            }

            xrut1 = xrut1 + "." + VB6Helpers.Mid(xrut, 4, 3) + "." + VB6Helpers.Mid(xrut, 7, 3) + "-" + VB6Helpers.Mid(xrut, 10, 1);
            return xrut1;
        }

        public static string FtoNumCon(string NumEmb)
        {
            string NumBlw = VB6Helpers.Trim(MODCVDIMMM.RetSoloAlfa(NumEmb));
            if (VB6Helpers.Len(NumBlw) > 8)
            {
                NumBlw = VB6Helpers.Mid(NumBlw, VB6Helpers.Len(NumBlw) - 7, 8);
            }
            return NumBlw;
        }

        public static string Escribe_Nombre(ref string Nombre)
        {
            short car = 0;
            short i = 0;
            string nnombre = "";
            Nombre = VB6Helpers.LCase(VB6Helpers.Trim(Nombre));
            car = (short)VB6Helpers.Len(Nombre);
            if (car > 0)
            {
                nnombre = VB6Helpers.UCase(VB6Helpers.Mid(Nombre, 1, 1));
                for (i = 2; i <= (short)car; i++)
                {
                    if (VB6Helpers.Mid(Nombre, i, 1) == " " && VB6Helpers.Mid(Nombre, i + 1, 1) != " ")
                    {
                        // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'nnombre' variable as a StringBuilder6 object.
                        nnombre = nnombre + " " + VB6Helpers.UCase(VB6Helpers.Mid(Nombre, i + 1, 1));
                        i = (short)(i + 1);
                    }
                    else
                    {
                        if (VB6Helpers.Mid(Nombre, i, 1) == "." && VB6Helpers.Mid(Nombre, i + 1, 1) != ".")
                        {
                            nnombre = nnombre + "." + VB6Helpers.UCase(VB6Helpers.Mid(Nombre, i + 1, 1));
                            i = (short)(i + 1);
                        }
                        else
                        {
                            if (VB6Helpers.Mid(Nombre, i, 1) != " ")
                            {
                                nnombre += VB6Helpers.Mid(Nombre, i, 1);
                            }

                        }

                    }

                }

                return nnombre;
            }

            return "";
        }

        public static void FillArrMnd(InitializationObject initObj)
        {
            short i = 0;
            short Mon1 = 0;
            short Exist = 0;
            short Mon2 = 0;
            short MaxMon = 0;
            initObj.MODCVDIMMM.Monedas = new short[1];  //Todas las Monedas de la Transacción. -Nacional.
            initObj.MODCVDIMMM.UltServ = (short)VB6Helpers.UBound(initObj.MODGFYS.VgFyS);

            for (i = 0; i <= (short)initObj.MODCVDIMMM.UltServ; i++)
            {
                if (~initObj.MODGFYS.VgFyS[i].Borrado != 0)
                {
                    short _switchVar1 = initObj.MODGFYS.VgFyS[i].EleTip;
                    if (_switchVar1 == 0 || _switchVar1 == 1 || _switchVar1 == 2)
                    {

                        Mon1 = initObj.MODGFYS.VgFyS[i].CodMon[1];

                        if (Mon1 != T_MODGTAB0.MndNac)
                        {
                            Exist = BuscaEnMonedas(initObj, Mon1);
                            if (~Exist != 0)
                            {
                                MaxMon = (short)(VB6Helpers.UBound(initObj.MODCVDIMMM.Monedas) + 1);
                                VB6Helpers.RedimPreserve(ref initObj.MODCVDIMMM.Monedas, 0, MaxMon);
                                initObj.MODCVDIMMM.Monedas[MaxMon] = Mon1;
                            }

                        }

                        if (initObj.MODGFYS.VgFyS[i].EleTip == 2)
                        {
                            Mon2 = initObj.MODGFYS.VgFyS[i].CodMon[2];
                            if (Mon2 != T_MODGTAB0.MndNac)
                            {
                                Exist = BuscaEnMonedas(initObj, Mon2);
                                if (~Exist != 0)
                                {
                                    MaxMon = (short)(VB6Helpers.UBound(initObj.MODCVDIMMM.Monedas) + 1);
                                    VB6Helpers.RedimPreserve(ref initObj.MODCVDIMMM.Monedas, 0, MaxMon);
                                    initObj.MODCVDIMMM.Monedas[MaxMon] = Mon2;
                                }

                            }

                        }

                    }

                }

            }

            Exist = BuscaEnMonedas(initObj, T_MODGTAB0.MndNac);
            if (~Exist != 0)
            {
                MaxMon = (short)(VB6Helpers.UBound(initObj.MODCVDIMMM.Monedas) + 1);
                VB6Helpers.RedimPreserve(ref initObj.MODCVDIMMM.Monedas, 0, MaxMon);
                initObj.MODCVDIMMM.Monedas[MaxMon] = T_MODGTAB0.MndNac;
            }
        }

        public static short BuscaEnMonedas(InitializationObject initObj, short mon)
        {
            short j = 0;
            for (j = 0; j <= (short)VB6Helpers.UBound(initObj.MODCVDIMMM.Monedas); j++)
            {
                if (mon == initObj.MODCVDIMMM.Monedas[j])
                {
                    return (short)(true ? -1 : 0);
                }

            }

            return (short)(false ? -1 : 0);
        }

        public static string PrintMonto(double mto)
        {
            string SS = VB6Helpers.CStr(formatnum(mto.ToString(), 0, 15));
            return SS;
        }

        public static dynamic formatnum(string P_Numero, short P_Blanco, short P_Largo)
        {
            dynamic _retValue = null;
            string s = P_Numero;
            if (((VB6Helpers.Val(s) == 0 ? -1 : 0) & P_Blanco) != 0)
            {
                _retValue = "";
            }
            else
            {
                s = Format.FormatCurrency(Format.StringToDouble(s), "#,###,###,##0.00");
            }

            s = Derecha(s, P_Largo);
            return s;
        }

        public static string Derecha(string campo, short largo)
        {
            string s = VB6Helpers.Trim(campo);
            short Blancos = (short)(2 * (largo - VB6Helpers.Len(s)) + MODGCON1.Puntos(s));
            short i = 0;
            if (VB6Helpers.Len(s) < largo)
            {
                for (i = 1; i <= (short)Blancos; i++)
                {
                    s = " " + s;
                }

            }
            return s;
        }

        public static void Imp_Planilla(InitializationObject initObject,UnitOfWorkCext01 unit, ref Reg_Planilla[] Pl, short ind, short EsCVD)
        {
            T_MODGFYS MODGFYS = initObject.MODGFYS;

            short Impresora;
            short k = 0;
            short xx = 0;
            string RR = "";
            short pp = ind;
            string NroAcs = "";
            short Planilla_Anulada = 0;

            PlanillaVisibleExportacion model = new PlanillaVisibleExportacion();

            model.Pl_num_planilla = (MODGPYF0.forma(Pl[pp].num_planilla, "000000"));
            model.Pl_Codigo = (Pl[pp].Codigo);
            if (~EsCVD != 0)
            {
                if (Pl[pp].Status != T_MODGFYS.Endoba)
                {
                    model.Pl_fecha_venta = (DateTime.Parse(Pl[pp].fecha_venta).ToString("dd/MM/yyyy",CultureInfo.InvariantCulture));
                }
            }
            else
            {
                model.Pl_fecha_venta = (DateTime.Parse(Pl[pp].fecha_venta).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            }

           
            model.Pl_CodBCCh = (VB6Helpers.Format(VB6Helpers.CStr(Pl[pp].CodBCCh), "0"));
            model.Pl_NomImport = (Pl[pp].NomImport);
            RR = Rut_Formateado(Pl[pp].rut);
            model.Pl_rut = (RR);
            if(!string.IsNullOrEmpty(Pl[pp].Paispago))
            {
                string _tempVar1 = VB6Helpers.Mid(Pl[pp].Paispago, 1, 17);
                model.Pl_Paispago = (Escribe_Nombre(ref _tempVar1));
                model.Pl_Cod_Paispago = (MODGPYF0.forma(Pl[pp].Cod_Paispago, "000"));
            }

            model.Pl_nombre_moneda = ( Escribe_Nombre(ref Pl[pp].nombre_moneda));
            model.Pl_cod_moneda = ( MODGPYF0.forma(Pl[pp].cod_moneda, "000"));

            if (!string.IsNullOrEmpty(Pl[pp].num_idi) && Pl[pp].num_idi != "000000")
            {
                model.Pl_num_idi = ( VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Val(Pl[pp].num_idi)), "000000"));
                model.Pl_fecha_idi = ( DateTime.Parse(Pl[pp].fecha_idi).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                model.Pl_Cod_Plaza = (VB6Helpers.Format(VB6Helpers.CStr(Pl[pp].Cod_Plaza), "0"));
            }

            model.Pl_Cod_FormaPago = (MODGPYF0.forma(Pl[pp].Cod_FormaPago, "00"));

            model.Pl_num_conocimiento = ( Pl[pp].num_conocimiento);
            model.Pl_fecha_conocimiento = (DateTime.Parse(Pl[pp].fecha_conocimiento).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            model.Pl_fecha_vencimiento = (DateTime.Parse(Pl[pp].fecha_vencimiento).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));

            //Impresion del Detalle de los Montos Cubiertos en la Planilla
            
            model.Pl_Mercaderia = PrintMonto(Pl[pp].Mercaderia);

            model.Pl_HastaFob = PrintMonto(Pl[pp].HastaFob);

            model.Pl_Fob_Origen = PrintMonto(Pl[pp].Fob_Origen);

            model.Pl_Flete_Origen = PrintMonto(Pl[pp].Flete_Origen);

            model.Pl_Seguro_Origen = PrintMonto(Pl[pp].Seguro_Origen);

            model.Pl_Cif_Origen = PrintMonto(Pl[pp].Cif_Origen);

            model.Pl_Interes_Origen = PrintMonto(Pl[pp].Interes_Origen);

            model.Pl_Gastos_Banco = PrintMonto(Pl[pp].Gastos_Banco);

            model.Pl_Total_Origen = PrintMonto(Pl[pp].Total_Origen);

            model.Pl_Cif_Dolar = PrintMonto(Pl[pp].Cif_Dolar);

            model.Pl_Total_Dolar = PrintMonto(Pl[pp].Total_Dolar);

            if (~EsCVD != 0)
            {
                if (Pl[pp].Status != T_MODGFYS.Estadis)
                {
                    if (Pl[pp].Status != T_MODGFYS.Endoba)
                    {
                        model.Pl_tipo_cambio = (Format.FormatCurrency(Pl[pp].tipo_cambio, "0.0000"));
                    }
                }

            }
            else
            {
                if (MODGFYS.CVD.Operacion != T_MODGFYS.PLANEST)
                {
                    model.Pl_tipo_cambio = (Format.FormatCurrency(Pl[pp].tipo_cambio, "0.0000"));
                }

            }

            model.Pl_Paridad = (Format.FormatCurrency(Pl[pp].Paridad, "0.0000"));

            if (Pl[pp].HayCuadro != 0)
            {
                if (Pl[pp].Num_Cuadro != 0)
                {
                    model.Pl_Num_Cuadro = (VB6Helpers.Format(VB6Helpers.CStr(Pl[pp].Num_Cuadro), "0"));
                }
                if (Pl[pp].num_cuotas != 0)
                {
                    model.Pl_num_cuotas = (VB6Helpers.Format(VB6Helpers.CStr(Pl[pp].num_cuotas), "0"));
                }
            }

            if (Pl[pp].HayAcuerdo != 0)
            {
                model.Pl_num_acuerdos = (VB6Helpers.Format(VB6Helpers.CStr(Pl[pp].num_acuerdos), "0"));

                NroAcs = "    " + VB6Helpers.Trim(Pl[pp].acuerdo[0]) + "    ";
                
                model.NroAcs = (NroAcs);

                model.Pl_fecha_debito = (DateTime.Parse(Pl[pp].fecha_debito).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                model.Pl_NDoc1 = (Pl[pp].NDoc1);
                model.Pl_NDoc2 = (Pl[pp].NDoc2);
            }

            Planilla_Anulada = (short)(false ? -1 : 0);
            if (Planilla_Anulada != 0)
            {
                model.Pl_fecha_anulacion = (DateTime.Parse(Pl[pp].fecha_anulacion).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                model.Pl_paridad_anulacion = (Format.FormatCurrency(Pl[pp].paridad_anulacion, "0.0000"));
                model.Pl_total_anulacion = (VB6Helpers.Format(VB6Helpers.CStr(Pl[pp].total_anulacion), "0"));
            }

            if(!string.IsNullOrEmpty(Pl[pp].nplar))
            {
                //Imprime_Reemplazo Pl(), pp%
            }

            model.Pl_ObsDecl = (Pl[pp].ObsDecl);

            if(!string.IsNullOrEmpty(Pl[pp].ObsParidad))
            {
                model.Pl_ObsParidad = (Pl[pp].ObsParidad);
            }
            if(!string.IsNullOrEmpty(Pl[pp].ObsMerma))
            {
                model.Pl_ObsMerma = (Pl[pp].ObsMerma);
            }
            if(!string.IsNullOrEmpty(Pl[pp].LineaObs1))
            {
                model.Pl_LineaObs1 = (Pl[pp].LineaObs1);
                model.Pl_LineaObs2 = (Pl[pp].LineaObs2);
                model.Pl_LineaObs3 = (Pl[pp].LineaObs3);
            }

            model.Pl_ObsCobranza = (Pl[pp].ObsCobranza);
            Imprime_Detalle(initObject,model, ref Pl, ref MODGFYS.DetInt, pp);
            initObject.PlanillasVisiblesExportacion.Add(model);

            string paramStr = "Impresion/Planillas/ImprimirPlanillaVisibleExportacion?numeroPresentacion={0}&fechaPresentacion={1}";
            string urlStr = string.Format(paramStr, model.Pl_num_planilla, model.Pl_fecha_venta);

            initObject.DocumentosAImprimir.Add(new DataImpresion()
            {
                URL = urlStr,
                //URL="FundTransfer/ImprimirPlanillaVisibleExportacion/"+initObject.PlanillasVisiblesExportacion.Count
                nroPresentacion = model.Pl_num_planilla,
                fechaOp = DateTime.Parse(model.Pl_fecha_venta),
                tipoDoc = 8,
                fileName = initObject.MODGCVD.VgCvd.OpeSin
            });

            if (~EsCVD != 0)
            {
                if (Pl[pp].Status != T_MODGFYS.Endoba && Pl[pp].Status != T_MODGFYS.Estadis)
                {
                    Pl[pp].Status = T_MODGFYS.Impresa;
                    ActualizaEstPls(initObject,unit, pp, T_MODGFYS.Impresa, -1);
                }

            }

        }

        public static void Imprime_Detalle(InitializationObject initObject,PlanillaVisibleExportacion model, ref Reg_Planilla[] Pl, ref Detalles[] Det, short pp)
        {
            //SE USA INTERES_DETALLE PORQUE SON IGUALES AL DETALLE QUE SE NECESITA
            short Impresora;
            short k = 0;
            short xx = 0;
            short yy = 0;
            double tint = 0;
            short MaxDet = 0;
            short pos_vert = 0;
            short cont = 0;
            short NDet = 0;
            string Concepto = "";
            string s = "";
            string sss = "";
            string SS = "";
            

            MaxDet = -1;
            
            MaxDet = (short)VB6Helpers.UBound(Det);

            //--- Parametros de Impresion ------
            //xx% = 3
            //yy% = -8
            yy = k;
            //----------------------------------
            pos_vert = (short)(223 + yy);
            cont = 0;

            for (NDet = 0; NDet <= (short)MaxDet; NDet++)
            {
                if (VB6Helpers.Val(Pl[pp].num_planilla) == Det[NDet].NumPlan)
                {
                    Detalle det = new Detalle();

                    cont = (short)(cont + 1);
                    tint += Det[NDet].monto;
                    pos_vert = (short)(pos_vert + 4);
                    det.Cont = MODGPYF0.forma(cont, "00");
                    short _switchVar1 = Det[NDet].Concepto;
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

                    det.Concepto = (Concepto);
                    det.Tipo = (Det[NDet].Tipo);
                    s = Format.FormatCurrency(Det[NDet].CapBas, "0.00");
                    sss = VB6Helpers.CStr(formatnum(VB6Helpers.Trim(s), 0, 15));
                    det.CapBas = (sss);
                    det.CodBas = (VB6Helpers.Format(VB6Helpers.CStr(Det[NDet].CodBas), "0"));
                    det.Tasa = (Format.FormatCurrency(Det[NDet].Tasa, "0.0000"));
                    det.FecIni = (DateTime.Parse(Det[NDet].FIni).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    det.FecFin = (DateTime.Parse(Det[NDet].FFin).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    det.NumDia = (VB6Helpers.Format(VB6Helpers.CStr(Det[NDet].ndias), "0"));
                    s = Format.FormatCurrency(Det[NDet].monto, "0.00");
                    SS = VB6Helpers.CStr(formatnum(VB6Helpers.Trim(s), 0, 15));
                    det.Mto = (SS);
                    model.Detalles.Add(det);
                }

            }

            if (tint > 0)
            {
                s = Format.FormatCurrency(tint, "0.00");
                SS = VB6Helpers.CStr(formatnum(VB6Helpers.Trim(s), 0, 15));
                model.tint = SS;
            }

        }

        public static void ActualizaEstPls(InitializationObject initObject,UnitOfWorkCext01 unit, short ind, short estado, short EsVis)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_MODGFYS MODGFYS = initObject.MODGFYS;
            //Actualiza Planillas Visibles
            string Que = "";
            string R = "";
            try
            {
                // IGNORED: On Error GoTo ActualizaErr

                if (EsVis != 0)
                {
                    int res = unit.SceRepository.EjecutarSP<int>("sce_plan_u01",
                        MODGSYB.dbcharSy(MODGCVD.VgCvd.codcct),
                        MODGSYB.dbcharSy(MODGCVD.VgCvd.codpro),
                        MODGSYB.dbcharSy(MODGCVD.VgCvd.codesp),
                        MODGSYB.dbcharSy(MODGCVD.VgCvd.codofi),
                        MODGSYB.dbcharSy(MODGCVD.VgCvd.codope),
                        MODGFYS.Planillas[ind].num_planilla,
                        MODGSYB.dbnumesy(estado)).First();
                    if (res != 0)
                    {
                        throw new Exception();
                    }
                }
                return;
            }
            catch (Exception _ex)
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type= TipoMensaje.Error,
                    Text= "Se ha producido un error de Comunicación al actualizar el estado de la planilla. Acepte este mensaje y continue."
                });
            }
        }
    }
}
