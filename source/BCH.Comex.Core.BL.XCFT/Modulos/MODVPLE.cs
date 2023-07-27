using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public static class MODVPLE
    {
        public static void Pr_PlanillaEstImp(InitializationObject initObject,UnitOfWorkCext01 unit, short Copias)
        {
            T_MODVPLE MODVPLE = initObject.MODVPLE;

            short n = 0;
            short i = 0;
            short j = 0;
            n = (short)VB6Helpers.UBound(MODVPLE.PlnVEst);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            for (i = 0; i <= (short)n; i++)
            {
                for (j = 1; j <= (short)Copias; j++)
                {
                    Imprime_PlnEst(initObject,unit, i);
                }

        }
            MODVPLE.PlnVEst = new T_PlnvEs[0];
        }

        //Graba una planilla estadistica de importación
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyPut_Plan(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {
            T_MODVPLE MODVPLE = initObject.MODVPLE;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_Module1 Module1 = initObject.Module1;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;
            T_MODGUSR MODGUSR = initObject.MODGUSR;

            short _retValue = 0;
            short Fin = 0;
            short Indice = 0;
            string Que = "";
            string R = "";
            short m = 0;
            short n = 0;
            short hay = 0;
            short IndAnula = 0;
            try
            {
                Fin = (short)VB6Helpers.UBound(MODVPLE.PlnVEst);
                // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                // IGNORED: On Error GoTo 0

                if (Fin == 0)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha cancelado la operación. No se definieron Planillas Estadísticas de Importación.",
                        Type = TipoMensaje.Error
                    });

                }
                else
                {
                    for (Indice = 0; Indice <= (short)Fin; Indice++)
                    {
                        List<string> parameters = new List<string>();

                        parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codcct));
                        parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codpro));
                        parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codesp));
                        parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codofi));
                        parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codope));
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].NroPln));
                        parameters.Add(MODGSYB.dbcharSy(MODVPLE.PlnVEst[Indice].RutImp));
                        parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.PrtCli));
                        parameters.Add(MODGSYB.dbcharSy(MODVPLE.PlnVEst[Indice].NomImp));
                        parameters.Add(MODGSYB.dbdatesy(MODVPLE.PlnVEst[Indice].FecVta));
                        parameters.Add(MODGSYB.dbcharSy(MODVPLE.PlnVEst[Indice].numdec));
                        parameters.Add(MODGSYB.dbdatesy(MODVPLE.PlnVEst[Indice].FecDec));
                        parameters.Add(MODGSYB.dbcharSy(MODVPLE.PlnVEst[Indice].NumCon));
                        parameters.Add(MODGSYB.dbdatesy(MODVPLE.PlnVEst[Indice].FecCon));
                        parameters.Add(MODGSYB.dbcharSy(MODVPLE.PlnVEst[Indice].CodPla));
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].CodPlz));
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].CodPem));
                        parameters.Add(MODGSYB.dbcharSy("Santiago"));
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].codfdp));
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].CodPaiPag));
                            m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VPai(initObject, unit, MODVPLE.PlnVEst[Indice].CodPaiPag);
                        parameters.Add(MODGSYB.dbcharSy(MODGTAB0.VPai[m].Pai_PaiNom));
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].CodMndBch));
                            n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMndBC(MODGTAB0, unit, MODVPLE.PlnVEst[Indice].CodMndBcc);
                        parameters.Add(MODGSYB.dbcharSy(MODGTAB0.VMnd[n].Mnd_MndNom));
                        parameters.Add(MODGSYB.dbPardSy(MODVPLE.PlnVEst[Indice].ParPla));
                        parameters.Add(MODGSYB.dbnumesy(0));
                        parameters.Add(MODGSYB.dbmontoSy(MODVPLE.PlnVEst[Indice].ValMer));
                        parameters.Add(MODGSYB.dbmontoSy(MODVPLE.PlnVEst[Indice].HasFob));
                        parameters.Add(MODGSYB.dbmontoSy(MODVPLE.PlnVEst[Indice].FobOri));
                        parameters.Add(MODGSYB.dbmontoSy(MODVPLE.PlnVEst[Indice].FleOri));
                        parameters.Add(MODGSYB.dbmontoSy(MODVPLE.PlnVEst[Indice].SegOri));
                        parameters.Add(MODGSYB.dbmontoSy(MODVPLE.PlnVEst[Indice].CifOri));
                        parameters.Add(MODGSYB.dbmontoSy(MODVPLE.PlnVEst[Indice].IntOri));
                        parameters.Add(MODGSYB.dbmontoSy(MODVPLE.PlnVEst[Indice].GasBco));
                        parameters.Add(MODGSYB.dbmontoSy(MODVPLE.PlnVEst[Indice].TotOri));
                        parameters.Add(MODGSYB.dbmontoSy(MODVPLE.PlnVEst[Indice].CifDol));
                        parameters.Add(MODGSYB.dbmontoSy(MODVPLE.PlnVEst[Indice].TotDol));
                        parameters.Add(MODGSYB.dbdatesy(MODVPLE.PlnVEst[Indice].FecVop));
                        parameters.Add(MODGSYB.dbnumesy(0));  //hay cuadro de pago.-
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].NroCcp));
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].NroDcp));
                        if (MODVPLE.PlnVEst[Indice].CanAco > 0)
                        {
                            hay = 1;
                        }
                        else
                        {
                            hay = 0;
                        }

                        parameters.Add(MODGSYB.dbnumesy(hay));
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].CanAco));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Str(MODVPLE.PlnVEst[Indice].Acoge1)));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Str(MODVPLE.PlnVEst[Indice].Acoge2)));
                        parameters.Add(MODGSYB.dbcharSy("0"));
                        parameters.Add(MODGSYB.dbnumesy(0));
                        if (MODVPLE.PlnVEst[Indice].HayRpl == -1)
                        {
                            IndAnula = 2;
                        }
                        else
                        {
                            IndAnula = 0;
                        }

                        parameters.Add(MODGSYB.dbnumesy(IndAnula));
                        parameters.Add(MODGSYB.dbnumesy(0));
                        parameters.Add("1900-01-01");
                        parameters.Add("1900-01-01");
                        parameters.Add(MODGSYB.dbnumesy(0));
                        parameters.Add(MODGSYB.dbnumesy(0));
                        parameters.Add(MODGSYB.dbdatesy(MODVPLE.PlnVEst[Indice].FecAutDeb));
                        parameters.Add(MODGSYB.dbcharSy(MODVPLE.PlnVEst[Indice].NroDocChi));
                        parameters.Add(MODGSYB.dbcharSy(MODVPLE.PlnVEst[Indice].NroDocExt));
                        parameters.Add(MODGSYB.dbnumesy(8));
                        parameters.Add(MODGSYB.dbcharSy(MODVPLE.PlnVEst[Indice].observ));
                        parameters.Add(MODGSYB.dbcharSy(MODVPLE.PlnVEst[Indice].ObsDec));
                        parameters.Add(MODGSYB.dbcharSy(""));
                        parameters.Add(MODGSYB.dbcharSy(MODVPLE.PlnVEst[Indice].ObsCob));
                        parameters.Add(MODGSYB.dbcharSy(""));
                        parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.CentroCosto));
                        parameters.Add(MODGSYB.dbcharSy(MODGUSR.UsrEsp.Especialista));

                        //datos reemplazo.-
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].NumPln_R));
                        parameters.Add(MODGSYB.dbdatesy(MODVPLE.PlnVEst[Indice].FecPln_R));
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].CodPlz_R));
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].CodEnt_R));
                        parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.PlnVEst[Indice].NumInf_R), "000000")));
                        parameters.Add(MODGSYB.dbdatesy(MODVPLE.PlnVEst[Indice].FecInf_R));
                        parameters.Add(MODGSYB.dbnumesy(MODVPLE.PlnVEst[Indice].PlzInf_R));
                        parameters.Add(MODGSYB.dbcharSy(MODVPLE.PlnVEst[Indice].NumCon_R));
                        parameters.Add(MODGSYB.dbdatesy(MODVPLE.PlnVEst[Indice].FecCon_R));
                        parameters.Add(MODGSYB.dbnumesy(Module1.PartysOpe[T_MODGCVD.ICli].IndNombre));
                        parameters.Add(MODGSYB.dbnumesy(Module1.PartysOpe[T_MODGCVD.ICli].IndDireccion));
                        parameters.Add(MODGSYB.dblogisy(MODVPLE.PlnVEst[Indice].HayRpl));  //Hay reemplazo.-
                        parameters.Add(MODGSYB.dbnumesy(20));  //Tipo Planilla

                        //-------------------------------------------
                        //Se ejecuta el Procedimiento Almacenado.-
                        
                        int res = unit.SceRepository.EjecutarSP<int>("sce_plan_w08", parameters.ToArray()).First();
                        if (res != 0)
                        {
                            throw new Exception();
                        }
                        
                    }
                }
                _retValue = (short)(true ? -1 : 0);
            }
                catch (Exception e)
            {
                    tracer.TraceException("Alerta", e);

                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                        Text = "Se ha producido un error al tratar de grabar la Planilla Visible de Estadística de Importación (Sce_Pla).",
                        Type = TipoMensaje.Error
                });
                _retValue = 0;
            }
            return _retValue;
        }
        }

        //graba intereses planillas estadisticas
        //
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static short SyPut_Inte(InitializationObject initObject,UnitOfWorkCext01 unit)
        {
            T_MODVPLE MODVPLE = initObject.MODVPLE;
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

            short _retValue = 0;
            short Fin = 0;
            short n = 0;
            short a = 0;
            string Que = "";
            short ind = 0;
            string tip = "";
            string Fecha = "";
            string R = "";
            
            Fin = (short)VB6Helpers.UBound(MODVPLE.IntPla);
            n = (short)VB6Helpers.UBound(MODVPLE.PlnVEst);
            try
            {
                for (ind = 0; ind <= (short)Fin; ind++)
                {

                    for (a = 0; a <= (short)n; a++)
                    {
                        if (MODVPLE.PlnVEst[a].NroPln == MODVPLE.IntPla[ind].NroPln)
                        {
                            Fecha = MODVPLE.PlnVEst[a].FecVta;
                        }
                    }
                    List<string> parameters = new List<string>();
                    
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codcct));
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codpro));
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codesp));
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codofi));
                    parameters.Add(MODGSYB.dbcharSy(MODGCVD.VgCvd.codope));
                    parameters.Add(MODGSYB.dbnumesy(MODVPLE.IntPla[ind].NroPln));
                    parameters.Add(MODGSYB.dbdatesy(Fecha));
                    parameters.Add(MODGSYB.dbnumesy(ind));
                    parameters.Add(MODGSYB.dbnumesy(MODVPLE.IntPla[ind].Concep));
                    short _switchVar1 = MODVPLE.IntPla[ind].TipInt;
                    if (_switchVar1 == 1)
                    {
                        tip = "PR";
                    }
                    else if (_switchVar1 == 2)
                    {
                        tip = "AC";
                    }
                    else if (_switchVar1 == 3)
                    {
                        tip = "IC";
                    }
                    else if (_switchVar1 == 4)
                    {
                        tip = "BA";
                    }

                    parameters.Add(MODGSYB.dbcharSy(tip));
                    parameters.Add(MODGSYB.dbmontoSy(MODVPLE.IntPla[ind].MtoInt));
                    parameters.Add(MODGSYB.dbmontoSy(MODVPLE.IntPla[ind].CapBas));
                    parameters.Add(MODGSYB.dbnumesy(MODVPLE.IntPla[ind].CodBas));
                    parameters.Add(MODGSYB.dbtasaSy(MODVPLE.IntPla[ind].TasInt));
                    parameters.Add(MODGSYB.dbdatesy(MODVPLE.IntPla[ind].FecIni));
                    parameters.Add(MODGSYB.dbdatesy(MODVPLE.IntPla[ind].FecFin));
                    parameters.Add(MODGSYB.dbnumesy(MODVPLE.IntPla[ind].NumDia));

                    //----------------------------------------------------

                    //Se ejecuta el Procedimiento Almacenado.
                    int res = unit.SceRepository.EjecutarSP<int>("sce_inpl_w01", parameters.ToArray()).First();
                    if (res != 0)
                    {
                        throw new Exception();
                    }
                    _retValue = -1;
                }
            }
            catch (Exception e)
            {
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Text= "Se ha producido un error al tratar de grabar intereses Planilla Estadistica de Importación (Sce_inpl).",
                    Type=TipoMensaje.Error
                });
                _retValue = 0;
            }
            return _retValue;
        }
        public static T_MODVPLE GetMODVPLE()
        {
            return new T_MODVPLE();
        }

        public static void Imprime_PlnEst(InitializationObject initObject,UnitOfWorkCext01 unit, short pp)
        {
            T_MODVPLE MODVPLE = initObject.MODVPLE;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            short Impresora;
            short k = 0;
            short xx = 0;
            short m = 0;
            string RR = "";
            string Pais = "";
            short n = 0;
            string NroAcs = "";
            short k_Paso = 0;
            short xx_Paso = 0;

            var model = new PlanillaEstadistica();

            model.PlnVEst_NroPln = (MODGPYF0.forma(MODVPLE.PlnVEst[pp].NroPln, "000000"));
            model.PlnVEst_CodPla = (MODVPLE.PlnVEst[pp].CodPla);

            model.PlnVEst_FecVta = (DateTime.Parse(MODVPLE.PlnVEst[pp].FecVta).ToString("dd/MM/yyyy",CultureInfo.InvariantCulture));

            model.PlnVEst_CodPlz = (VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.PlnVEst[pp].CodPlz), "0"));
            model.PlnVEst_NomImp = (MODGFYS.Escribe_Nombre(ref MODVPLE.PlnVEst[pp].NomImp));
            RR = MODGFYS.Rut_Formateado(MODVPLE.PlnVEst[pp].RutImp);
            model.PlnVEst_RutImp = (RR);

            if (MODVPLE.PlnVEst[pp].CodPaiPag != 0)
            {
                m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VPai(initObject,unit, MODVPLE.PlnVEst[pp].CodPaiPag);
                string _tempVar1 = VB6Helpers.Mid(MODGTAB0.VPai[m].Pai_PaiNom, 1, 17);
                Pais = MODGFYS.Escribe_Nombre(ref _tempVar1);
                model.VPai_Pai_PaiNom = (Pais);
                model.PlnVEst_CodPaiPag = (MODGPYF0.forma(MODVPLE.PlnVEst[pp].CodPaiPag, "000"));
            }

            n = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMndBC(MODGTAB0,unit, MODVPLE.PlnVEst[pp].CodMndBcc);
            string monedaNom = String.Empty;
            MODGFYS.Escribe_Nombre(ref monedaNom);
            MODGTAB0.VMnd[n].Mnd_MndNom = monedaNom;
            model.VMnd_Mnd_MndNom = monedaNom;
            model.PlnVEst_CodMndBcc = (MODGPYF0.forma(MODVPLE.PlnVEst[pp].CodMndBcc, "000"));

            if(!string.IsNullOrEmpty(MODVPLE.PlnVEst[pp].NumIdi) && MODVPLE.PlnVEst[pp].NumIdi != "000000")
            {
                model.PlnVEst_NumIdi = (VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Val(MODVPLE.PlnVEst[pp].NumIdi)), "000000"));
                model.PlnVEst_FecIdi = (DateTime.Parse(MODVPLE.PlnVEst[pp].FecIdi).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                model.PlnVEst_CodPem = (VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.PlnVEst[pp].CodPem), "0"));
            }

            model.PlnVEst_codfdp = (MODGPYF0.forma(MODVPLE.PlnVEst[pp].codfdp, "00"));
            model.PlnVEst_NumCon = (MODVPLE.PlnVEst[pp].NumCon);
            model.PlnVEst_FecCon = (DateTime.Parse(MODVPLE.PlnVEst[pp].FecCon).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            model.PlnVEst_FecVop = (DateTime.Parse(MODVPLE.PlnVEst[pp].FecVop).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));

            //Impresion del Detalle de los Montos Cubiertos en la Planilla
            
            model.PlnVEst_ValMer = MODGFYS.PrintMonto(MODVPLE.PlnVEst[pp].ValMer);

            model.PlnVEst_HasFob = MODGFYS.PrintMonto(MODVPLE.PlnVEst[pp].HasFob);

            model.PlnVEst_FobOri = MODGFYS.PrintMonto(MODVPLE.PlnVEst[pp].FobOri);

            model.PlnVEst_FleOri = MODGFYS.PrintMonto(MODVPLE.PlnVEst[pp].FleOri);

            model.PlnVEst_SegOri = MODGFYS.PrintMonto(MODVPLE.PlnVEst[pp].SegOri);

            model.PlnVEst_CifOri = MODGFYS.PrintMonto(MODVPLE.PlnVEst[pp].CifOri);

            model.PlnVEst_IntOri = MODGFYS.PrintMonto(MODVPLE.PlnVEst[pp].IntOri);

            model.PlnVEst_GasBco = MODGFYS.PrintMonto(MODVPLE.PlnVEst[pp].GasBco);

            model.PlnVEst_TotOri = MODGFYS.PrintMonto(MODVPLE.PlnVEst[pp].TotOri);

            model.PlnVEst_CifDol = MODGFYS.PrintMonto(MODVPLE.PlnVEst[pp].CifDol);

            model.PlnVEst_TotDol = MODGFYS.PrintMonto(MODVPLE.PlnVEst[pp].TotDol);

            model.PlnVEst_ParPla = (Format.FormatCurrency(MODVPLE.PlnVEst[pp].ParPla, "0.0000"));

            if (MODVPLE.PlnVEst[pp].NroCcp != 0)
            {
                model.PlnVEst_NroCcp = (VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.PlnVEst[pp].NroCcp), "0"));
            }
            if (MODVPLE.PlnVEst[pp].NroDcp != 0)
            {
                model.PlnVEst_NroDcp = (VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.PlnVEst[pp].NroDcp), "0"));
            }

            if (MODVPLE.PlnVEst[pp].CanAco > 0)
            {
                model.PlnVEst_CanAco = (VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.PlnVEst[pp].CanAco), "0"));
                NroAcs = "    " + VB6Helpers.Trim(VB6Helpers.Str(MODVPLE.PlnVEst[pp].Acoge1)) + "    ";
                
                model.NroAcs = (NroAcs);
                
                model.PlnVEst_FecAutDeb = (DateTime.Parse(MODVPLE.PlnVEst[pp].FecAutDeb).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                model.PlnVEst_NroDocChi = (MODVPLE.PlnVEst[pp].NroDocChi);
                model.PlnVEst_NroDocExt = (MODVPLE.PlnVEst[pp].NroDocExt);
            }

            //Datos planilla reemplazada.-
            //-----------------------------------
            if (MODVPLE.PlnVEst[pp].HayRpl == -1)
            {
                k_Paso = k;
                xx_Paso = xx;
                

                model.PlnVEst_NumPln_R = (MODGPYF0.forma(MODVPLE.PlnVEst[pp].NumPln_R, "000000"));
                model.PlnVEst_FecPln_R = (DateTime.Parse(MODVPLE.PlnVEst[pp].FecPln_R).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                model.PlnVEst_CodPlz_R = (VB6Helpers.Str(MODVPLE.PlnVEst[pp].CodPlz_R));
                model.PlnVEst_CodEnt_R = (VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.PlnVEst[pp].CodEnt_R), "00"));
                model.PlnVEst_NumInf_R = (VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.PlnVEst[pp].NumInf_R), "000000"));
                model.PlnVEst_FecInf_R = (DateTime.Parse(MODVPLE.PlnVEst[pp].FecInf_R).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                model.PlnVEst_PlzInf_R = (VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.PlnVEst[pp].PlzInf_R), "00"));
                model.PlnVEst_NumCon_R = (MODVPLE.PlnVEst[pp].NumCon_R);
                model.PlnVEst_FecCon_R = (DateTime.Parse(MODVPLE.PlnVEst[pp].FecCon_R).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                k = k_Paso;
            }

            if(!string.IsNullOrEmpty(MODVPLE.PlnVEst[pp].ObsDec))
            {
                float _tempVar2 = 149 + k;
                Pr_Imp_ObsPln(model, MODVPLE.PlnVEst[pp].ObsDec, 200, 5 + xx, ref _tempVar2);
            }
            if(!string.IsNullOrEmpty(MODVPLE.PlnVEst[pp].observ))
            {
                float _tempVar3 = 164 + k;
                Pr_Imp_ObsPln(model, MODVPLE.PlnVEst[pp].observ, 200, 5 + xx, ref _tempVar3);
            }
            if(!string.IsNullOrEmpty(MODVPLE.PlnVEst[pp].ObsCob))
            {
                float _tempVar4 = 200 + k;
                Pr_Imp_ObsPln(model, MODVPLE.PlnVEst[pp].ObsCob, 60, 5 + xx, ref _tempVar4);
            }

            Pr_Imprime_det(initObject,model, pp);
            initObject.DocumentosAImprimir.Add(new DataImpresion()
            {
                URL="FundTransfer/PlanillaEstadistica/"+initObject.PlanillasEstadisticas.Count
            });
            initObject.PlanillasEstadisticas.Add(model);
        }

        public static void Pr_Imp_ObsPln(PlanillaEstadistica model, string Obs, short largo, float Columna, ref float Fila)
        {
            string Dato = VB6Helpers.Left(VB6Helpers.Trim(Obs), largo);
            short LD = (short)VB6Helpers.Len(Dato);
            short Indice = 1;
            short UltSpc = 0;
            string linea = "";
            short i = 0;

            while (Indice < LD)
            {
                UltSpc = 0;
                linea = "";
                for (i = (short)Indice; i <= (short)LD; i++)
                {
                    if (VB6Helpers.Mid(Dato, i, 1) != "@")
                    {
                        // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'linea' variable as a StringBuilder6 object.
                        linea += VB6Helpers.Mid(Dato, i, 1);
                    }

                    if (VB6Helpers.Mid(Dato, i, 1) == " ")
                    {
                        UltSpc = i;
                    }

                    if ((linea.Length > 100 || i == LD) || VB6Helpers.Mid(Dato, i, 1) == "@")
                    {
                        if (i >= LD)
                        {
                            Indice = (short)(i + 1);
                            break;
                        }
                        else
                        {
                            if (VB6Helpers.Mid(Dato, i, 1) == "@")
                            {
                                UltSpc = i;
                            }

                            if (UltSpc == 0)
                            {
                                UltSpc = i;
                                linea = VB6Helpers.Left(linea, UltSpc) + "-";
                                Indice = (short)(UltSpc + 1);
                                break;
                            }
                            else
                            {
                                linea = VB6Helpers.Left(linea, UltSpc);
                                Indice = (short)(UltSpc + 1);
                                break;
                            }

                        }

                    }

                }

                model.Linea = (linea);
            }
        }


        //imprime detalle de la planilla estadistica de importación
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Pr_Imprime_det(InitializationObject initObject,PlanillaEstadistica model, short Indice)
        {
            T_MODVPLE MODVPLE = initObject.MODVPLE;

            short Impresora;
            short k = 0;
            short xx = 0;
            short yy = 0;
            short n = 0;
            short pos_vert = 0;
            short cont = 0;
            short i = 0;
            double tint = 0;
            string Concepto = "";
            string Tipo = "";
            string s = "";
            string sss = "";
            string SS = "";

            n = (short)VB6Helpers.UBound(MODVPLE.IntPla);
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            //--- Parametros de Impresion ------

            yy = k;
            //----------------------------------

            pos_vert = (short)(223 + yy);
            cont = 0;

            for (i = 0; i <= (short)n; i++)
            {
                if (MODVPLE.PlnVEst[Indice].NroPln == MODVPLE.IntPla[i].NroPln)
                {
                    Detalle di = new Detalle();

                    cont = (short)(cont + 1);
                    tint += MODVPLE.IntPla[i].MtoInt;
                    pos_vert = (short)(pos_vert + 4);
                    di.Cont = MODGPYF0.forma(cont, "00");
                    short _switchVar1 = MODVPLE.IntPla[i].Concep;
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
                        Concepto = "FLE";
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

                    short _switchVar2 = MODVPLE.IntPla[i].TipInt;
                    if (_switchVar2 == 1)
                    {
                        Tipo = "PR";
                    }
                    else if (_switchVar2 == 2)
                    {
                        Tipo = "AC";
                    }
                    else if (_switchVar2 == 3)
                    {
                        Tipo = "IC";
                    }
                    else if (_switchVar2 == 4)
                    {
                        Tipo = "BA";
                    }
                    di.Tipo = Tipo;

                    s = (Format.FormatCurrency(MODVPLE.IntPla[i].CapBas, "0.00"));
                    sss = VB6Helpers.CStr(MODGFYS.formatnum(VB6Helpers.Trim(s), 0, 15));
                    di.CapBas = sss;
                    
                    di.CodBas = (VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.IntPla[i].CodBas), "0"));
                    di.Tasa = (Format.FormatCurrency(MODVPLE.IntPla[i].TasInt, "0.0000"));
                    di.FecIni = (DateTime.Parse(MODVPLE.IntPla[i].FecIni).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    di.FecFin = (DateTime.Parse(MODVPLE.IntPla[i].FecFin).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                    di.NumDia = (VB6Helpers.Format(VB6Helpers.CStr(MODVPLE.IntPla[i].NumDia), "0"));
                    s = (Format.FormatCurrency(MODVPLE.IntPla[i].MtoInt, "0.00"));
                    SS = VB6Helpers.CStr(MODGFYS.formatnum(VB6Helpers.Trim(s), 0, 15));
                    di.Mto = (SS);
                    model.Detalles.Add(di);

                }

            }

            if (tint > 0)
            {
                s = (Format.FormatCurrency(tint, "0.00"));
                SS = VB6Helpers.CStr(MODGFYS.formatnum(VB6Helpers.Trim(s), 0, 15));
                model.tint = SS;
            }

        }

        public static short SyAnu_ImpCvd(dynamic NumOpe, InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            using (var trace = new Tracer("SyAnu_ImpCvd"))
            {
                string R = "";
                short Codigo = 0;
                string Msg = "";
                var RESULTADO = unit.SceRepository.sce_anu_u12_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(NumOpe, 11, 5)));
                R = RESULTADO.Column1.ToString();
                Msg = RESULTADO.Column2;
                if (R == "-1")
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al anular la Operación de Compra-Venta. El Servidor reporta : [" + Msg + "]. Reporte este problema."
                    });
                    trace.TraceError("Se ha producido un error al anular la Operación de Compra-Venta. El Servidor reporta : [" + Msg + "]. Reporte este problema.");
                    return 0;
                }
                Codigo = (short)VB6Helpers.Val(MODGPYF0.copiardestring(R, "~", 1));
                if (Codigo == -1)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de Anular la Operación de Compra-Venta. El Servidor reporta : [" + Msg + "]. Reporte este problema."
                    });
                    trace.TraceError("Se ha producido un error al tratar de Anular la Operación de Compra-Venta. El Servidor reporta : [" + Msg + "]. Reporte este problema.");
                    return 0;
                }

                if (string.IsNullOrEmpty(R)) return 0;

                return -1;
            }
        }
    }
}
