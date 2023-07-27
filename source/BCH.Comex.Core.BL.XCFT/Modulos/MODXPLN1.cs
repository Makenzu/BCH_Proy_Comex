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
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class MODXPLN1
    {
        public static T_MODXPLN1 GetMODXPLN1() {
            return new T_MODXPLN1();
        }

        //****************************************************************************
        //   1.  Graba varias Planillas Visibles de Exportación.
        //   2.  Retorno    <> 0 : Grabación Exitosa.
        //                  =  0 : Error o Grabación no Exitosa.
        //****************************************************************************
        public static short SyPutn_xPlv(InitializationObject initObject,UnitOfWorkCext01 unit, string CodAnu, short Estado)
        {
            using (var tracer = new Tracer("Graba varias Planillas Visibles de Exportación - SyPutn_xPlv"))
            {
                T_MODXPLN1 MODXPLN1 = initObject.MODXPLN1;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

                short _retValue = 0;
                short i = 0;
                short ConError = 0;
                try
                {
                    for (i = 0; i <= (short)VB6Helpers.UBound(MODXPLN1.VxPlvs); i++)
                    {
                        if (MODXPLN1.VxPlvs[i].Status != T_MODGCVD.EstadoEli)
                        {
                            List<string> parameters = new List<string>();
                            parameters.Add(MODGSYB.dbcharSy(VB6Helpers.Trim(MODXPLN1.VxPlvs[i].NumPre)));
                            parameters.Add(MODGSYB.dbdatesy(VB6Helpers.Format(VB6Helpers.CStr(MODXPLN1.VxPlvs[i].fecpre), "dd/MM/yyyy")));
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].cencos));
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].codusr));
                            //parameters.Add(DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("hh:mm:ss"));
                            parameters.Add(MODGSYB.dbdatesy(VB6Helpers.Format(VB6Helpers.CStr(DateTime.Now), "dd/MM/yyyy hh:mm:ss")));
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].TipPln));
                            //-------------------------------------------
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].codcct));
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].codpro));
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].codesp));
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].codofi));
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].codope));
                            parameters.Add(MODGSYB.dbcharSy(CodAnu));
                            //-------------------------------------------
                            if (Estado == T_MODXPLN1.ExPlv_Anulada)
                            {
                                parameters.Add(MODGSYB.dbnumesy(T_MODXPLN1.ExPlv_Anulada));
                            }
                            else
                            {
                                parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].Estado));
                            }
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].numdec));
                            //-------------------------------------------
                            parameters.Add(MODGSYB.dbdatesy(VB6Helpers.Format(VB6Helpers.CStr(MODXPLN1.VxPlvs[i].FecDec), "dd/MM/yyyy")));
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].CodAdn));
                            parameters.Add(MODGSYB.dbdatesy(VB6Helpers.Format(VB6Helpers.CStr(MODXPLN1.VxPlvs[i].FecVen), "dd/MM/yyyy")));
                            //-------------------------------------------
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].RutExp));
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].PrtExp));
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].IndNom));
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].IndDir));
                            //-------------------------------------------
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].CodMnd));
                            parameters.Add(MODGSYB.dbmontoSy(MODXPLN1.VxPlvs[i].MtoBru));
                            parameters.Add(MODGSYB.dbmontoSy(MODXPLN1.VxPlvs[i].MtoCom));
                            parameters.Add(MODGSYB.dbmontoSy(MODXPLN1.VxPlvs[i].MtoOtg));
                            parameters.Add(MODGSYB.dbmontoSy(MODXPLN1.VxPlvs[i].MtoLiq));
                            parameters.Add(MODGSYB.dbPardSy(MODXPLN1.VxPlvs[i].Mtopar));
                            parameters.Add(MODGSYB.dbmontoSy(MODXPLN1.VxPlvs[i].MtoDol));
                            parameters.Add(MODGSYB.dbTCamSy(MODXPLN1.VxPlvs[i].TipCam));
                            parameters.Add(MODGSYB.dbTCamSy(MODXPLN1.VxPlvs[i].TipCamo));
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].PlzBcc));
                            //-------------------------------------------
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].DfoCea));
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].DfoCtf));
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].DfoCbc));
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].DfoNpr));
                            parameters.Add(MODGSYB.dbdatesy(VB6Helpers.Format(VB6Helpers.CStr(MODXPLN1.VxPlvs[i].DfoFpr), "dd/MM/yyyy")));
                            //-------------------------------------------
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].AfiMnd));
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].AfiPar));
                            parameters.Add(MODGSYB.dbmontoSy(MODXPLN1.VxPlvs[i].AfiMto));
                            parameters.Add(MODGSYB.dbmontoSy(MODXPLN1.VxPlvs[i].AfiMtoD));
                            parameters.Add(MODGSYB.dbmontoSy(MODXPLN1.VxPlvs[i].AfiVen));
                            //-------------------------------------------
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].DiePbc));
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].DieNum));
                            parameters.Add(MODGSYB.dbdatesy(VB6Helpers.Format(VB6Helpers.CStr(MODXPLN1.VxPlvs[i].DieFec), "dd/MM/yyyy")));
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].ObsPln));
                            parameters.Add(MODGSYB.dblogisy(MODXPLN1.VxPlvs[i].DedCom));
                            parameters.Add(MODGSYB.dblogisy(MODXPLN1.VxPlvs[i].DedFle));
                            parameters.Add(MODGSYB.dblogisy(MODXPLN1.VxPlvs[i].DedSeg));
                            parameters.Add(MODGSYB.dblogisy(MODXPLN1.VxPlvs[i].PlnEst));
                            //-------------------------------------------
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].SecBen));
                            parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].SecInv));
                            parameters.Add(MODGSYB.dbPardSy(MODXPLN1.VxPlvs[i].PrcPar));
                            parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].nomcom));
                            int res = unit.SceRepository.EjecutarSP<int>("sce_xplv_w02", parameters.ToArray()).First();
                            if (res == 9)
                            {
                                Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Text = "Se ha producido un error al tratar de grabar la Planilla Visible de Exportación (Sce_xPlv).",
                                    Type = TipoMensaje.Error
                                });
                                ConError = (short)(true ? -1 : 0);
                            }
                        }
                    }

                    if (~ConError != 0)
                    {
                        _retValue = (short)(true ? -1 : 0);
                    }
                }
                catch (Exception _ex)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error al tratar de grabar la Planilla Visible de Exportación (Sce_xPlv).",
                        Type = TipoMensaje.Error
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }


        //****************************************************************************
        //   1.  Graba los datos adicionales de Estadisticas 408.
        //   2.  Retorno    <> 0 : Grabación Exitosa.
        //                  =  0 : Error o Grabación no Exitosa.
        //****************************************************************************
        public static short SyPutn_xPlva(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer("Graba datos adicionales de Estadisticas 408 - SyPutn_xPlva"))
            {
                T_MODXPLN1 MODXPLN1 = initObject.MODXPLN1;
                UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

                short _retValue = 0;
                short i = 0;
                short n = 0;
                short ConError = 0;


                n = (short)VB6Helpers.UBound(MODXPLN1.VxPlvs);
                // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                // IGNORED: On Error GoTo 0

                for (i = 0; i <= (short)n; i++)
                {
                    if (MODXPLN1.VxPlvs[i].Status != T_MODGCVD.EstadoEli)
                    {
                        List<string> parameters = new List<string>();
                        //Sce_Plia_W01

                        parameters.Add(MODGSYB.dbcharSy(MODXPLN1.VxPlvs[i].NumPre));
                        parameters.Add(MODGSYB.dbdatesy(MODXPLN1.VxPlvs[i].fecpre));
                        parameters.Add(MODGSYB.dbmontoSy(0));
                        parameters.Add(MODGSYB.dbdatesy(MODXPLN1.VxPlvs[i].fecins));
                        parameters.Add(MODGSYB.dbcharSy(""));
                        parameters.Add(MODGSYB.dbdatesy(""));
                        parameters.Add(MODGSYB.dbnumesy(MODXPLN1.VxPlvs[i].codpai));
                        //-----------------------------------------------
                        //Se ejecuta el Procedimiento Almacenado.
                        //-----------------------------------------------
                        int res = unit.SceRepository.EjecutarSP<int>("sce_plia_w01", parameters.ToArray()).First();
                        if (res == 9)
                        {
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Text = "Se ha producido un error de Comunicación al tratar de de grabar la Planilla Invisible (Sce_Pli).",
                                Type = TipoMensaje.Error
                            });
                            ConError = (short)(true ? -1 : 0);
                        }
                    }

                }

                if (~ConError != 0)
                {
                    _retValue = (short)(true ? -1 : 0);
                }
                return _retValue;
            }
        }


        //****************************************************************************
        //   1.  Imprime las n copias de todas las planillas Visible-Export.
        //****************************************************************************
        // UPGRADE_ISSUE (#04E8): Unable to use Try-Catch in current method.
        public static void Pr_Imprime_nxPlv(InitializationObject initObject,UnitOfWorkCext01 unit, short Copias)
        {
            T_MODXPLN1 MODXPLN1 = initObject.MODXPLN1;

            short m = 0;
            short n = 0;
            short i = 0;
            short j = 0;
            
            n = (short)VB6Helpers.UBound(MODXPLN1.VxPlvs);
            
            //------------------------------
            //Verifica Número de Planillas.-
            //------------------------------
            m = 0;
            for (i = 0; i <= (short)n; i++)
            {
                if (MODXPLN1.VxPlvs[i].Status != T_MODGCVD.EstadoEli)
                {
                    m = (short)(m + 1);
                }
            }

            m = 0;
            for (i = 0; i <= (short)n; i++)
            {
                if (MODXPLN1.VxPlvs[i].Status != T_MODGCVD.EstadoEli)
                {
                    m = (short)(m + 1);
                    for (j = 1; j <= (short)Copias; j++)
                    {
                        Pr_Imprime_xPlv(initObject,unit, i);
                    }

                    if (m == 40)
                    {
                        break;
                    }
                }

            }

        }

        public static string ConvRut(string rut)
        {
            string Srut = "";
            string Rfin = "";
            short a = 0;
            for (a = 1; a <= (short)VB6Helpers.Len(rut); a++)
            {
                Srut = VB6Helpers.Mid(rut, a, 1);
                if (Srut == "0")
                {
                    Rfin = VB6Helpers.Right(rut, VB6Helpers.Len(rut) - a);
                }
                else
                {
                    Srut = "";
                    break;
                }
            }
            if(!string.IsNullOrWhiteSpace(Srut))
            {
                return Rfin;
            }
            else
            {
                return rut;
            }
        }

        private static void BEGGINING_Pr_Imprime_xPlv(Planilla_xPlv modelo, InitializationObject initObject,UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXPLN1 MODXPLN1 = initObject.MODXPLN1;
            T_MODGTAB1 MODGTAB1 = initObject.MODGTAB1;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            short copia = 0;
            short i = Indice;
            short num_cop = 0;
            short m = 0;
            string R = "";
            string n = "";

            num_cop = (short)(num_cop + 1);
            copia = (short)(copia + 1);
            //Printer.ScaleLeft = -2
            //Número Presentación....OK¡¡¡
            if(!string.IsNullOrEmpty(MODXPLN1.VxPlvs[i].NumPre))
            {
                n = VB6Helpers.Format(MODXPLN1.VxPlvs[i].NumPre, "0000000");
                modelo.VxPlvs_NumPre = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
            }

            //Plaza Banco Central que Contabiliza...OK¡¡¡
            if (MODXPLN1.VxPlvs[i].PlzBcc != 0)
            {

                //68    '2.25
                m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VPbc(initObject, unit, MODXPLN1.VxPlvs[i].PlzBcc);
                if (m >= 0)
                {
                    modelo.VPbc_Pbc_PbcDes = (VB6Helpers.Trim(MODGTAB1.VPbc[m].Pbc_PbcDes));
                }
                //Código Plaza Banco Central que Contabiliza...OK¡¡¡
                //86
                //68    '2.25
                modelo.VxPlvs_PlzBcc = (VB6Helpers.Format(VB6Helpers.CStr(MODXPLN1.VxPlvs[i].PlzBcc), "00"));
            }

            //Fecha Presentación....OK¡¡¡
            if(!string.IsNullOrWhiteSpace(MODXPLN1.VxPlvs[i].fecpre))
            {


                modelo.VxPlvs_fecpre = (DateTime.Parse(MODXPLN1.VxPlvs[i].fecpre).ToString("dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture));
            }

            //Tipo de Operación....OK¡¡¡
            if (MODXPLN1.VxPlvs[i].TipPln != 0)
            {

                //8    '3.08
                modelo.NomPLn = (VB6Helpers.Trim(VB6Helpers.Mid(GetNomPLn(MODXPLN1.VxPlvs[i].TipPln), 1, 31)));
                //Código Tipo de Operación...OK¡¡¡
                //4
                //8   '3
                modelo.VxPlvs_TipPln = (VB6Helpers.Format(VB6Helpers.CStr(MODXPLN1.VxPlvs[i].TipPln), "000"));
            }

            //Nombre....OK¡¡¡
            if(!string.IsNullOrWhiteSpace(MODXPLN1.VxPlvs[i].PrtExp))
            {

                //4
                modelo.DatPrt = (Mdl_Funciones_Varias.GetDatPrt(initObject, unit, MODXPLN1.VxPlvs[i].PrtExp, MODXPLN1.VxPlvs[i].IndNom, MODXPLN1.VxPlvs[i].IndDir, "N"));
                //Dirección....OK¡¡¡

                //8
                modelo.DatPrtn = (Mdl_Funciones_Varias.GetDatPrtn(unit, MODXPLN1.VxPlvs[i].PrtExp, MODXPLN1.VxPlvs[i].IndNom, MODXPLN1.VxPlvs[i].IndDir, "D", "DC"));
            }

            //Rut....OK¡¡¡
            if(!string.IsNullOrWhiteSpace(MODXPLN1.VxPlvs[i].RutExp))
            {
                R = ConvRut(MODXPLN1.VxPlvs[i].RutExp);
                //20
                //9

                modelo.VxPlvs_RutExp = (VB6Helpers.Mid(R, 1, VB6Helpers.Len(R) - 1) + "-" + VB6Helpers.Mid(R, VB6Helpers.Len(R), 1));
            }
        }

        //****************************************************************************
        //   1.  Imprime la Planilla Nro. 500.
        //****************************************************************************
        public static void Pr_Imprime_xPlv(InitializationObject initObject,UnitOfWorkCext01 unit, short Indice)
        {
            T_MODXPLN1 MODXPLN1 = initObject.MODXPLN1;
            T_MODGTAB1 MODGTAB1 = initObject.MODGTAB1;
            T_MODGTAB0 MODGTAB0 = initObject.MODGTAB0;

            short copia = 0;
            short va = 1;
            short i = Indice;
            short num_cop = 0;
            short m = 0;
            short Impresora;
            float z = 0;
            float Y = 0;
            string n = "";
            string R = "";
            short Pri = 0;
            string Texto = "";
            dynamic lin = null;
            string palabra = "";
            short co = 0;
            string letra = "";
            string pa = "";

            //Se identifica el tipo de planilla a imprimir
            //--------------------------------------------
            if (VB6Helpers.Instr(T_MODXPLN1.PLN400, VB6Helpers.CStr(MODXPLN1.VxPlvs[i].TipPln)) == 0)
            {
                Planilla500 modelo = new Planilla500();
                BEGGINING_Pr_Imprime_xPlv(modelo, initObject, unit, Indice);
                //Moneda....OK¡¡¡MONTO RETORNADO
                if (MODXPLN1.VxPlvs[i].CodMnd != 0)
                {
                    m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, MODXPLN1.VxPlvs[i].CodMnd);
                    if (m != 0)
                    {
                        modelo.VMnd_Mnd_MndNom = (VB6Helpers.Trim(MODGTAB0.VMnd[m].Mnd_MndNom));
                    }
                    //Código Moneda....OK¡¡¡MONTO RETORNADO
                    //5
                    modelo.VMnd_Mnd_MndCbc = (VB6Helpers.Format(VB6Helpers.CStr(MODGTAB0.VMnd[m].Mnd_MndCbc), "000"));
                }

                //Datos de Aduana.(DATOS DECLARACION EXP)...OK¡¡¡
                if (MODXPLN1.VxPlvs[i].CodAdn != 0)
                {
                    //Aduana.
                    //55
                    m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VAdn(MODGTAB1, unit, MODXPLN1.VxPlvs[i].CodAdn);
                    if (m >= 0)
                    {
                        modelo.VAdn_NomAdn = (VB6Helpers.Trim(MODGTAB1.VAdn[m].NomAdn));
                    }
                    //Código Aduana.(DATOS DECLARACION EXP)...OK¡¡¡
                    //6
                    //54
                    modelo.VxPlvs_CodAdn = (VB6Helpers.Format(VB6Helpers.CStr(MODXPLN1.VxPlvs[i].CodAdn), "00"));
                }

                //Entidad Autorizada.(DATOS FIN.ORIGINAL)...OK¡¡¡
                if (MODXPLN1.VxPlvs[i].DfoCea != 0)
                {
                    //Descripción Entidad Autorizada.
                    //5
                    m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_Bco(initObject, unit, MODXPLN1.VxPlvs[i].DfoCea);
                    if (m >= 0)
                    {
                        modelo.VBco_NomBco = (VB6Helpers.Trim(VB6Helpers.Left(MODGTAB0.VBco[m].NomBco, 16)));
                    }
                    //Código Entidad Autorizada.(DATOS FIN.ORIGINAL)...OK¡¡¡
                    //5
                    modelo.VxPlvs_DfoCea = (VB6Helpers.Format(VB6Helpers.CStr(MODXPLN1.VxPlvs[i].DfoCea), "00"));
                }

                //Datos de Plaza Banco Central.(DATOS INF. EXP.)..OK¡¡¡
                if (MODXPLN1.VxPlvs[i].DiePbc != 0)
                {
                    //Die, Código Plaza Banco Central.
                    //6
                    modelo.VxPlvs_DiePbc = (VB6Helpers.Format(VB6Helpers.CStr(MODXPLN1.VxPlvs[i].DiePbc), "00"));
                    //Die, Plaza Banco Central.(DATOS INF. EXP.)..OK¡¡¡
                    //02
                    m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VPbc(initObject, unit, MODXPLN1.VxPlvs[i].DiePbc);
                    if (m >= 0)
                    {
                        modelo.VPbc_Pbc_PbcDes = (VB6Helpers.Trim(MODGTAB1.VPbc[m].Pbc_PbcDes));
                    }
                }

                //Valor Bruto....OK¡¡¡
                if (MODXPLN1.VxPlvs[i].MtoBru != 0)
                {
                    //5
                    n = Format.FormatCurrency(MODXPLN1.VxPlvs[i].MtoBru, "#,###,###,###,##0.00");
                    modelo.VxPlvs_MtoBru = (MODGPYF1.PoneChar(n, " ", "H", 20));
                }

                //Número Aceptación....OK¡¡¡DATOS DEC. EXP.
                if (!string.IsNullOrEmpty(MODXPLN1.VxPlvs[i].numdec))
                {
                    //6
                    n = VB6Helpers.Format(MODXPLN1.VxPlvs[i].numdec, "0000000");
                    modelo.VxPlvs_numdec = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
                }

                //Tipo de finaciamiento(DATOS FIN. ORIGINAL)...OK¡¡¡
                if (MODXPLN1.VxPlvs[i].DfoCtf != 0)
                {
                    //descripción tipo financiamiento
                    //4
                    modelo.NomPLn = (VB6Helpers.Left(VB6Helpers.Trim(GetNomPLn(MODXPLN1.VxPlvs[i].DfoCtf)), 16));

                    //Código Tipo Financiamiento.
                    //4
                    modelo.VxPlvs_DfoCtf = (MODXPLN1.VxPlvs[i].DfoCtf).ToString();
                }

                //Monto Comisión
                if (MODXPLN1.VxPlvs[i].MtoCom != 0)
                {
                    Pri = (short)(true ? -1 : 0);
                    if (MODXPLN1.VxPlvs[i].DedCom != 0)
                    {
                        Pri = (short)(false ? -1 : 0);
                    }

                    n = Format.FormatCurrency(MODXPLN1.VxPlvs[i].MtoCom, "#,###,###,###,##0.00");
                    if (Pri != 0)
                    {
                        modelo.VxPlvs_MtoCom = (MODGPYF1.PoneChar(n, " ", "H", 20));
                    }
                    else
                    {
                        modelo.VxPlvs_MtoCom = ("(" + MODGPYF1.PoneChar(n, " ", "H", 20) + ")");
                    }
                }

                //--------------------------------------------------------------------------
                //Fecha Aceptación.(DATOS DECLARACION DE EXP.)...OK¡¡¡
                if (!string.IsNullOrEmpty(MODXPLN1.VxPlvs[i].FecDec))
                {
                    //4
                    modelo.VxPlvs_FecDec = (DateTime.Parse(MODXPLN1.VxPlvs[i].FecDec).ToString("dd/MM/yyyy"));
                }

                //------------------------------------------------------------------------
                //Plaza Banco Central.(DATOS FIN. ORIGINAL)...OK¡¡¡
                if (MODXPLN1.VxPlvs[i].DfoCbc != 0)
                {
                    //Descripción Entidad Autorizada.(DATOS FIN. ORIGINAL)...OK¡¡¡
                    //3
                    m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VPbc(initObject, unit, MODXPLN1.VxPlvs[i].DfoCbc);
                    if (m >= 0)
                    {
                        modelo.VPbc_Pbc_PbcDes = VB6Helpers.Trim(VB6Helpers.Left(MODGTAB1.VPbc[m].Pbc_PbcDes, 16));
                    }
                    //Código Entidad Autorizada.(DATOS FIN. ORIGINAL)...OK¡¡¡
                    //5
                    //3
                    modelo.VxPlvs_DfoCbc = (VB6Helpers.Format(VB6Helpers.CStr(MODXPLN1.VxPlvs[i].DfoCbc), "00"));
                }

                //Die, Número de Emisión.(DATOS INF. EXPORTACION)...OK¡¡¡
                if (!string.IsNullOrEmpty(MODXPLN1.VxPlvs[i].DieNum))
                {
                    n = MODXPLN1.VxPlvs[i].DieNum;
                    modelo.VxPlvs_DieNum = (VB6Helpers.Mid(n, 1, VB6Helpers.Len(n) - 1) + "-" + VB6Helpers.Right(n, 1));
                }

                //*******************************************************************
                //*******************************************************************
                //Monto Otorgado.
                if (MODXPLN1.VxPlvs[i].MtoOtg != 0)
                {
                    Pri = (short)(true ? -1 : 0);
                    //5
                    n = Format.FormatCurrency(MODXPLN1.VxPlvs[i].MtoOtg, "#,###,###,###,##0.00");
                    if (Pri != 0)
                    {
                        modelo.VxPlvs_MtoOtg = (MODGPYF1.PoneChar(n, " ", "H", 20));
                    }
                }

                //*****************************************************************

                //Fecha Vencimiento Retorno.(DATOS DECLARACION EXP)...OK¡¡¡
                if (!string.IsNullOrEmpty(MODXPLN1.VxPlvs[i].FecVen))
                {
                    //2
                    modelo.VxPlvs_FecVen = (DateTime.Parse(MODXPLN1.VxPlvs[i].FecVen).ToString("dd/MM/yyyy"));
                }

                //Número de Presentación.(DATOS FIN. ORIGINAL)...OK¡¡¡
                if (!string.IsNullOrEmpty(MODXPLN1.VxPlvs[i].DfoNpr))
                {
                    //9
                    n = VB6Helpers.Format(MODXPLN1.VxPlvs[i].DfoNpr, "0000000");
                    modelo.VxPlvs_DfoNpr = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
                }

                //Valor Líquido...OK¡¡¡
                if (MODXPLN1.VxPlvs[i].MtoLiq != 0)
                {
                    //9
                    n = Format.FormatCurrency(MODXPLN1.VxPlvs[i].MtoLiq, "#,###,###,###,##0.00");
                    modelo.VxPlvs_MtoLiq = (MODGPYF1.PoneChar(n, " ", "H", 20));
                }

                //Fecha de Presentación.(DATOS FIN. ORIGINAL)...OK¡¡¡
                if (!string.IsNullOrEmpty(MODXPLN1.VxPlvs[i].DfoFpr))
                {
                    //8
                    modelo.VxPlvs_DfoFpr = (DateTime.Parse(MODXPLN1.VxPlvs[i].DfoFpr).ToString("dd/MM/yyyy"));
                }

                //Plazo Vencimiento del Financiamiento.(ant.financ..)..OK¡¡¡
                if (MODXPLN1.VxPlvs[i].AfiVen != 0)
                {
                    modelo.VxPlvs_AfiVen = (VB6Helpers.Trim(VB6Helpers.Str(MODXPLN1.VxPlvs[i].AfiVen)) + " días.");
                }

                //Die, Fecha de Emisión.(DATOS INF. EXP)..OK¡¡¡
                if (!string.IsNullOrEmpty(MODXPLN1.VxPlvs[i].DieFec))
                {
                    modelo.VxPlvs_DieFec = DateTime.Parse(MODXPLN1.VxPlvs[i].DieFec).ToString("dd/MM/yyyy");
                }

                //Paridad a US$.(MONTO RETORNADO)...OK¡¡¡
                if (MODXPLN1.VxPlvs[i].Mtopar != 0)
                {
                    //7
                    n = Format.FormatCurrency(MODXPLN1.VxPlvs[i].Mtopar, "#,###,##0.0000");
                    modelo.VxPlvs_Mtopar = (MODGPYF1.PoneChar(n, " ", "H", 20));
                }

                //Monto en US$....(MONTO RETORNADO)...OK¡¡¡
                if (MODXPLN1.VxPlvs[i].MtoDol != 0)
                {
                    //5
                    n = Format.FormatCurrency(MODXPLN1.VxPlvs[i].MtoDol, "#,###,###,###,##0.00");
                    modelo.VxPlvs_MtoDol = (MODGPYF1.PoneChar(n, " ", "H", 20));
                }

                //Tipo de Cambio de la Operación.(MONTO RETORNADO)...OK¡¡¡
                if (VB6Helpers.Instr(T_MODXPLN1.PlnEst, VB6Helpers.CStr(MODXPLN1.VxPlvs[i].TipPln)) == 0 && VB6Helpers.Instr(T_MODXPLN1.PLNINF, VB6Helpers.CStr(MODXPLN1.VxPlvs[i].TipPln)) == 0)
                {
                    if (MODXPLN1.VxPlvs[i].TipCam != 0)
                    {
                        n = Format.FormatCurrency(MODXPLN1.VxPlvs[i].TipCam, "#,###,##0.00");
                        modelo.VxPlvs_TipCam = (MODGPYF1.PoneChar(n, " ", "H", 12));
                    }
                }
                END_Pr_Imprime_xPlv(modelo, initObject, i);
                initObject.Planillas500.Add(modelo);

                string paramStr = "Impresion/Planillas/ImprimirPlanillaVisibleExportacion?numeroPresentacion={0}&fechaPresentacion={1}";
                string urlStr = string.Format(paramStr, modelo.VxPlvs_NumPre.Replace("-", ""), DateTime.Parse(modelo.VxPlvs_fecpre).ToString("yyyy-MM-dd"));

                initObject.DocumentosAImprimir.Add(new DataImpresion()
                {
                    URL = urlStr,
                    //URL = "FundTransfer/ImprimirPlanilla500/" + initObject.Planillas500.Count
                    nroPresentacion = modelo.VxPlvs_NumPre.Replace("-", ""),
                    fechaOp = DateTime.Parse(modelo.VxPlvs_fecpre),
                    tipoDoc = 8,
                    fileName = initObject.MODGCVD.VgCvd.OpeSin
                });
            }
            else
            {
                Planilla401 modelo = new Planilla401();
                BEGGINING_Pr_Imprime_xPlv(modelo, initObject, unit, Indice);
                //-----------------------------------------------------------------------

                //SE IMPRIMEN LAS PLANILLAS 401
                //------------------------------
                //------------------------------
                // Antecedentes financiamiento
                //------------------------------

                //*********************ANTECEDENTES FINANCIAMIENTO*******************
                //moneda...OK¡¡¡
                if (MODXPLN1.VxPlvs[i].AfiMnd != 0)
                {
                    m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, MODXPLN1.VxPlvs[i].AfiMnd);
                    if (m != 0)
                    {
                        modelo.VMnd_Mnd_MndNom = (VB6Helpers.LTrim(VB6Helpers.RTrim(MODGTAB0.VMnd[m].Mnd_MndNom))); //...OK¡¡¡
                        modelo.VMnd_Mnd_MndCbc = (VB6Helpers.Format(VB6Helpers.CStr(MODGTAB0.VMnd[m].Mnd_MndCbc), "000")); //...OK¡¡¡
                    }
                }

                //paridad...OK¡¡¡
                if (MODXPLN1.VxPlvs[i].AfiPar != 0)
                {
                    //6.85
                    modelo.VxPlvs_AfiPar = (Format.FormatCurrency(MODXPLN1.VxPlvs[i].AfiPar, "0.0000"));
                }

                //monto...OK¡¡¡
                if (MODXPLN1.VxPlvs[i].AfiMto != 0)
                {
                    //7.68
                    modelo.VxPlvs_AfiMto = (Format.FormatCurrency(MODXPLN1.VxPlvs[i].AfiMto, "0.00"));
                }

                //monto en us$...OK¡¡¡
                if (MODXPLN1.VxPlvs[i].AfiMtoD != 0)
                {
                    //8.55
                    modelo.VxPlvs_AfiMtoD = (Format.FormatCurrency(MODXPLN1.VxPlvs[i].AfiMtoD, "0.00"));
                }

                //Plazo Vencimiento Financiamiento....OK¡¡¡
                if (MODXPLN1.VxPlvs[i].AfiVen != 0)
                {
                    //9.42
                    modelo.VxPlvs_AfiVen = (VB6Helpers.Trim(VB6Helpers.Str(MODXPLN1.VxPlvs[i].AfiVen)) + " días"); //Paola
                }

                //Tipo de Cambio de la Operación....OK¡¡¡(MONTO RETORNADO)
                if (MODXPLN1.VxPlvs[i].TipCam != 0 && MODXPLN1.VxPlvs[i].TipPln != 402)
                {
                    n = Format.FormatCurrency(MODXPLN1.VxPlvs[i].TipCam, "#,###,##0.00");
                    modelo.VxPlvs_TipCam = (MODGPYF1.PoneChar(n, " ", "H", 12));
                }
                END_Pr_Imprime_xPlv(modelo, initObject, i);
                initObject.Planillas401.Add(modelo);

                string paramStr = "Impresion/Planillas/ImprimirPlanillaVisibleExportacion?numeroPresentacion={0}&fechaPresentacion={1}";
                string urlStr = string.Format(paramStr, modelo.VxPlvs_NumPre.Replace("-", ""), DateTime.Parse(modelo.VxPlvs_fecpre).ToString("yyyy-MM-dd")); ;

                initObject.DocumentosAImprimir.Add(new DataImpresion()
                {
                    URL = urlStr,
                    //URL = "FundTransfer/ImprimirPlanilla401/" + initObject.Planillas401.Count
                    nroPresentacion = modelo.VxPlvs_NumPre.Replace("-", ""),
                    fechaOp = DateTime.Parse(modelo.VxPlvs_fecpre),
                    tipoDoc = 8,
                    fileName = initObject.MODGCVD.VgCvd.OpeSin
                });
            }
        }

        private static void END_Pr_Imprime_xPlv(Planilla_xPlv modelo, InitializationObject initObject,int Indice)
        {
            T_MODXPLN1 MODXPLN1 = initObject.MODXPLN1;
            int i = Indice;
            short va = 0;
            string Texto = String.Empty;
            string palabra = String.Empty;
            short co = 0;
            string letra = "";
            //Observaciones.
            //-------------------------
            if (!string.IsNullOrEmpty(MODXPLN1.VxPlvs[i].ObsPln))
            {

                Texto = MODGPYF0.Componer(VB6Helpers.Trim(MODXPLN1.VxPlvs[i].ObsPln), VB6Helpers.Chr(13) + VB6Helpers.Chr(10), " ");
                Texto = Texto + "&";

                for (co = 1; co <= (short)VB6Helpers.Len(Texto); co++)
                {
                    letra = VB6Helpers.Mid(Texto, co, 1);
                    if (letra == " " || letra == "&")
                    {
                        if (VB6Helpers.Len(palabra) >= 60 || letra == "&")
                        {
                            modelo.Palabras.Add(palabra);
                            palabra = "";
                        }
                        else
                        {
                            palabra += " ";
                        }

                    }
                    else
                    {
                        palabra += letra;
                    }

                }

            }
        }

        //Retorna el Nombre de la Planilla.
        public static string GetNomPLn(short TipPln)
        {

            //Tipo de Operación.
            if (TipPln == 401)
            {
                return "ANTIC. COM RET Y LIQUIDADO MCF";
            }
            else if (TipPln == 402)
            {
                return "ANTICIPO DE COMPRADOR RETORNADO";
            }
            else if (TipPln == 403)
            {
                return "CRÉDITO INTERNO";
            }
            else if (TipPln == 407)
            {
                return "CRÉDITO EXTERNO";
            }
            else if (TipPln == 408)
            {
                //agrego la Planilla 408
                return "CRÉDITO EXTERNO RETORNADO";
            }
            else if (TipPln == 500)
            {
                return "DIVISAS RETORNADAS Y LIQUIDADAS";
            }
            else if (TipPln == 511)
            {
                return "DIVISAS RETORNADAS Y NO LIQUIDADAS";
            }
            else if (TipPln == 501)
            {
                return "DIVISAS RETORNADAS";
            }
            else if (TipPln == 502)
            {
                return "DIVISAS NO RETORNADAS 502";
            }
            else if (TipPln == 540)
            {
                return "RET. EMPRESAS";
            }
            else if (TipPln == 570)
            {
                return "RETORNOS POR DEDUCCIÓN";
            }
            else if (TipPln >= 600)
            {
                return "EX-FINANCIAMIENTO";
            }

            return "";
        }

        /// <summary>
        /// Retorna el Número de la Planilla c/ el Dígito Verificador.-
        /// </summary>
        /// <param name="CodsBCH"></param>
        /// <param name="CodBco"></param>
        /// <param name="NumPln"></param>
        /// <param name="Agno"></param>
        /// <returns></returns>
        public static string Fn_DigVer_xPlv(short CodsBCH, short CodBco, int NumPln, short Agno)
        {
            string Agnito = VB6Helpers.Right(VB6Helpers.Format(VB6Helpers.CStr(Agno), "0000"), 2);
            string ns = VB6Helpers.Format(VB6Helpers.CStr(CodsBCH), "00") + VB6Helpers.Format(VB6Helpers.CStr(CodBco), "000") +
                VB6Helpers.Format(VB6Helpers.CStr(NumPln), "000000") + Agnito;
            short n13 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 13, 1));
            short n12 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 12, 1));
            short n11 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 11, 1));
            short n10 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 10, 1));
            short n9 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 9, 1));
            short n8 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 8, 1));
            short n7 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 7, 1));
            short n6 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 6, 1));
            short n5 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 5, 1));
            short n4 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 4, 1));
            short n3 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 3, 1));
            short n2 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 2, 1));
            short n1 = (short)VB6Helpers.Val(VB6Helpers.Mid(ns, 1, 1));
            short suma = (short)((n13 * 2) + (n12 * 3) + (n11 * 4) + (n10 * 5) + (n9 * 6) + (n8 * 7) + (n7 * 2) + (n6 * 3) +
                (n5 * 4) + (n4 * 5) + (n3 * 6) + (n2 * 7) + (n1 * 2));
            short resp = (short)(suma % 11);
            short digito = (short)(11 - resp);

            switch (digito)
            {
                case 11:
                    return VB6Helpers.Format(VB6Helpers.CStr(NumPln), "000000") + "0";
                case 10:
                    return VB6Helpers.Format(VB6Helpers.CStr(NumPln), "000000") + "K";
                default:
                    return VB6Helpers.Format(VB6Helpers.CStr(NumPln), "000000") + VB6Helpers.Format(VB6Helpers.CStr(digito), "0");
            }

        }
    }
}
