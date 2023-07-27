using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODSWEN1
    {
        // Función que llena un string con el MT
        public static string Fn_GenSwiftEnvio(DatosGlobales Globales,UnitOfWorkCext01 unit, int ind)
        {
            T_MODGSWF MODGSWF = Globales.MODGSWF;
            string Fn_GenSwiftEnvio = "";

            string msgycce = "";

            string sw = "";
            // Campos.-
            if (MODGSWF.VSwf[ind].EstaGen != 0)
            {
                switch (MODGSWF.VSwf[ind].NroSwf)
                {
                    case "MT-103":
                        sw = Fn_Gen103Sw(Globales,unit,ind);
                        break;
                    case "MT-202":
                        sw = Fn_Gen202Sw(Globales, unit, ind);
                        break;
                }
            }

            Fn_GenSwiftEnvio = sw;
            return Fn_GenSwiftEnvio;
        }

        public static string Fn_Gen103Sw(DatosGlobales Globales,UnitOfWorkCext01 unit, int ind)
        {
            T_MODGSWF MODGSWF = Globales.MODGSWF;
            T_Modswen Modswen = Globales.Modswen;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            T_MOD_50F MOD_50F = Globales.MOD_50F;

            string Fn_Gen103Sw = "";

            int linea = 0;
            string SwfMon = "";
            int m = 0;
            string Fecha_Paso1 = "";
            int X = 0;
            int LC = 0;
            int i = 0;
            string sw = "";
            string s = "";
            string Bco = "";

            string[] Arreglo = null;

            if (MODGSWF.VSwf[ind].BcoAla.SwfBco.TrimB() != "")
            {
                Bco = MODGSWF.VSwf[ind].BcoAla.SwfBco;
            }
            else
            {
                Bco = MODGSWF.VSwf[ind].DatSwf.SwfCor;
            }


            //@emiliano se ajusta a lo mismo que esta hecho en FT
            //se reemplaza Fn_Trae_Bancos por BancoIntercambiaClave
            if (!BCH.Comex.Core.BL.XGGL.Modulos.Modswen.BancoIntercambiaClave(Globales, Bco))
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Title="SWIFT",
                    Type=TipoMensaje.Error,
                    Text= "No se podrá emitir el " + MODGSWF.VSwf[ind].NroSwf + " automáticamente, ya que la casilla SWIFT del Banco Receptor (" + Bco + "), no está habilitada. Deberá emitir un Télex, o modificarlo en la aplicacion ENVIO DE SWIFT."
                });
            }

            Fn_Gen103Sw = "";

            sw = "";
            sw = sw + 1.Char() + "{1:F01";
            sw = sw + "BCHICLRMAXXX";
            sw = sw + "          }";
            sw = sw + "{2:I";
            sw = sw + MODGSWF.VSwf[ind].NroSwf.Mid(4, 3);
            if (MODGSWF.VSwf[ind].BcoAla.SwfBco.TrimB() != "")
            {
                sw = sw + MODGSWF.VSwf[ind].BcoAla.SwfBco.TrimB().Mid(1, 8) + "A" + MODGSWF.VSwf[ind].BcoAla.SwfBco.TrimB().Mid(9, 3);
            }
            else
            {
                sw = sw + MODGSWF.VSwf[ind].DatSwf.SwfCor.TrimB().Mid(1, 8) + "A" + MODGSWF.VSwf[ind].DatSwf.SwfCor.TrimB().Mid(9, 3);
            }
            sw = sw + "N";
            sw = sw + "}{4:" + 13.Char() + 10.Char();


            if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
            {
                if (BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_Trae_Fmt_Campos(Globales, "MT103") == 0)
                {
                    return Fn_Gen103Sw;
                }
            }
            for (i = 1; i <= Modswen.VFmt_Swf.GetUpperBound(0); i += 1)
            {
                // msgbox(VFmt_Swf(i%).Id_Campo)
                LC = Modswen.VFmt_Swf[i].Largo_Campo;
                switch (Modswen.VFmt_Swf[i].Id_Campo)
                {
                    case "20":
                        sw = sw + ":20:" + MODGSWF.VGSwf.NumOpe.TrimB() + 13.Char() + 10.Char();
                        break;
                    case "21":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-202")
                        {
                            if (MODGSWF.VSwf[ind].DatSwf.RefOpe == "")
                            {
                                sw = sw + ":21:" + MODGSWF.VGSwf.NumOpe.TrimB() + 13.Char() + 10.Char();
                            }
                            else
                            {
                                sw = sw + ":21:" + MODGPYF0.Componer(MODGSWF.VSwf[ind].DatSwf.RefOpe, "/RFB/", "").TrimB() + 13.Char() + 10.Char();
                            }
                        }
                        break;
                    case "23B":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            sw = sw + ":23B:" + "CRED" + 13.Char() + 10.Char();
                        }
                        break;
                    case "23E":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                            {
                                if (MODGSWF.VCod.GetUpperBound(0) > 0)
                                {
                                    for (X = 0; X <= MODGSWF.VCod.GetUpperBound(0); X += 1)
                                    {
                                        if (MODGSWF.VCod[X].Estado != 9)
                                        {
                                            if (MODGSWF.VCod[X].numswi == ind)
                                            {
                                                sw = sw + ":23E:" + MODGSWF.VCod[X].Codigo + 13.Char() + 10.Char();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "32A":
                        Fecha_Paso1 = MigrationSupport.Utils.Format(MODGSWF.VSwf[ind].DatSwf.FecPag, "yymmdd");
                        sw = sw + ":32A:" + Fecha_Paso1.TrimB() + MODGSWF.VSwf[ind].SwfMon.TrimB() + Format.FormatCurrency(MODGSWF.VSwf[ind].mtoswf, "############0.00").TrimB() + 13.Char() + 10.Char();
                        break;
                    case "33B":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (MODGSWF.VMT103[ind].MndOri.ToStr().TrimB() != "" && MODGSWF.VMT103[ind].MtoOri.ToStr().TrimB() != "0")
                            {
                                m = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VMnd(Globales,unit, (short)MODGSWF.VMT103[ind].MndOri);
                                SwfMon = MODGTAB0.VMnd[m].Mnd_MndSwf;
                                sw = sw + ":33B:" + SwfMon.TrimB() + MODGPYF0.Componer(Format.FormatCurrency(MODGSWF.VMT103[ind].MtoOri, T_MODGSWF.FormatoSwf), ".", "").TrimB() + 13.Char() + 10.Char();
                            }
                        }
                        break;
                    case "36":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (MODGSWF.VMT103[ind].TipCam != 0)
                            {
                                sw = sw + ":36:" + MODGPYF0.Componer(Format.FormatCurrency(MODGSWF.VMT103[ind].TipCam, T_MODGSWF.FormatoSwf), ",", ".").TrimB() + 10.Char();
                            }
                        }
                        // Flag 50F
                        // -------------------------------------------
                        // Codigo insertado 05/102007 por Realsystems
                        // Modificaciones MT-103
                        // realiza generacion de mensaje 50F o 50K
                        break;
                    case "50K":
                        if (MOD_50F.VG_50F[ind, 1] == "0")
                        {
                            if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    string argTemp1 = MODGSWF.VCliSwf.NomCli.TrimB().Left(LC);
                                    sw = sw + ":50K:" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp1, 35) + 13.Char() + 10.Char();
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (MODGSWF.VCliSwf.rutcli.TrimB() != "")
                                    {
                                        string argTemp2 = "Rut:" + MODGSWF.VCliSwf.rutcli.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp2, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (MODGSWF.VCliSwf.DirCli1.TrimB() != "")
                                    {
                                        string argTemp3 = MODGSWF.VCliSwf.DirCli1.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp3, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (MOD_50F.VG_50F[ind, 2].TrimB() != "")
                                    {
                                        string argTemp4 = MODGSWF.VCliSwf.DirCli2.TrimB() + " " + MOD_50F.VG_50F[ind, 2].TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp4, LC) + 13.Char() + 10.Char();
                                    }
                                    else
                                    {
                                        if (MODGSWF.VCliSwf.DirCli2.TrimB() != "")
                                        {
                                            string argTemp5 = MODGSWF.VCliSwf.DirCli2.TrimB();
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp5, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "50F":
                        if (MOD_50F.VG_50F[ind, 1] == "1")
                        {
                            if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                            {
                                //Cambiar al arreglar cuenta
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    string argTemp6 = MODGSWF.VCliSwf.CtaCli.TrimB();
                                    sw = sw + ":50F:/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp6, LC) + 13.Char() + 10.Char();
                                }

                                if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.NomCli))
                                    {
                                        string _tempVar7 = VB6Helpers.Left(VB6Helpers.Trim(MODGSWF.VCliSwf.NomCli), LC);
                                        sw = sw + VB6Helpers.Mid("1/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref _tempVar7, 35), 1, 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }

                                }

                                if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    bool printLinea1b = !string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.NomCli) && MODGSWF.VCliSwf.NomCli.Length > 33;
                                    if (printLinea1b)
                                    {
                                        string _tempVar7 = VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VCliSwf.NomCli), 34, 33);
                                        sw = sw + VB6Helpers.Mid("1/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref _tempVar7, 33), 1, 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                    else if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.DirCli1))
                                    {
                                        string _tempVar8 = VB6Helpers.Trim(MODGSWF.VCliSwf.DirCli1);
                                        sw = sw + VB6Helpers.Mid("2/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref _tempVar8, LC), 1, 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                }

                                if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (!string.IsNullOrWhiteSpace(MOD_50F.VG_50F[ind, 2]))
                                    {
                                        string _tempVar9 = VB6Helpers.Trim(MODGSWF.VCliSwf.CiuCli);
                                        sw = sw + VB6Helpers.Mid("3/" + VB6Helpers.Trim(MOD_50F.VG_50F[ind, 2]) + "/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref _tempVar9, LC), 1, 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }


                                }

                                if (Modswen.VFmt_Swf[i].Linea_Campo == 5)
                                {
                                    if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.rutcli))
                                    {
                                        string _tempVar10 = VB6Helpers.Trim(MODGSWF.VCliSwf.rutcli);
                                        sw = sw + VB6Helpers.Mid("6/" + VB6Helpers.Trim(MOD_50F.VG_50F[ind, 2]) + "/BCHICLRM/" + _tempVar10, 1, 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }

                                }
                            }
                        }
                        // -------------------------------------------------
                        // Codigo original antes de modificacion MT-103
                        // -------------------------------------------------
                        //             Case "50K"
                        //                 If VSwf(ind).NroSwf = "MT-103" Then
                        //                     If VFmt_Swf(i%).Linea_Campo = 1 Then
                        //                         sw = sw & ":50K:" & Fn_FormateaString(Trim$(VCliSwf.NomCli), lc%) & Chr(13) & Chr(10)
                        //                     ElseIf VFmt_Swf(i%).Linea_Campo = 2 Then
                        //                         If Trim$(VCliSwf.rutcli) <> "" Then sw = sw & Fn_FormateaString("Rut:" & Trim$(VCliSwf.rutcli), lc%) & Chr(13) & Chr(10)
                        //                     ElseIf VFmt_Swf(i%).Linea_Campo = 3 Then
                        //                         If Trim$(VCliSwf.DirCli1) <> "" Then sw = sw & Fn_FormateaString(Trim$(VCliSwf.DirCli1), lc%) & Chr(13) & Chr(10)
                        //                     ElseIf VFmt_Swf(i%).Linea_Campo = 4 Then
                        //                         If Trim$(VCliSwf.PaiCli) <> "" Then
                        //                             sw = sw & Fn_FormateaString(Trim$(VCliSwf.DirCli2), lc%) + " " + Fn_FormateaString(Trim$(VCliSwf.PaiCli), lc%) & Chr(13) & Chr(10)
                        //                         Else
                        //                             If Trim$(VCliSwf.DirCli2) <> "" Then sw = sw & Fn_FormateaString(Trim$(VCliSwf.DirCli2), lc%) & Chr(13) & Chr(10)
                        //                         End If
                        //                     End If
                        //                 End If
                        break;
                    case "53A":
                        if (sw.InStr("53A", 1, StringComparison.CurrentCulture) == 0)
                        {
                            if (MODGSWF.VSwf[ind].BcoCoE.SwfBco.TrimB() != "")
                            {
                                if (MODGSWF.VSwf[ind].BcoCoE.SwfBco.TrimB().Mid(8, 3) != "1  " && MODGSWF.VSwf[ind].BcoCoE.SwfBco.TrimB().Mid(8, 3) != "BIC" && !MODGSWF.VSwf[ind].BcoCoE.IngMan.ToBool())
                                {
                                    sw = sw + ":53A:" + MODGSWF.VSwf[ind].BcoCoE.SwfBco.TrimB() + 13.Char() + 10.Char();
                                }
                            }
                        }
                        break;
                    case "53D":
                        // Para ser D :   - Ingreso manual de datos.-
                        //                -   Swift terminado en BIC.-
                        //                -   Swift terminado en 1BlancoBlanco.-
                        // En el caso del Swift = BIC.
                        if (MODGSWF.VSwf[ind].BcoCoE.NomBco.TrimB() != "")
                        {
                            if (MODGSWF.VSwf[ind].BcoCoE.SwfBco.TrimB() == "" || MODGSWF.VSwf[ind].BcoCoE.SwfBco.TrimB().Mid(8, 3) == "1  " || MODGSWF.VSwf[ind].BcoCoE.SwfBco.TrimB().Mid(8, 3) == "BIC" || MODGSWF.VSwf[ind].BcoCoE.IngMan.ToBool())
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    if (MODGSWF.VSwf[ind].BcoCoE.NomBco.TrimB() != "")
                                    {
                                        string argTemp9 = MODGSWF.VSwf[ind].BcoCoE.NomBco.TrimB();
                                        sw = sw + ":53D:" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp9, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (MODGSWF.VSwf[ind].BcoCoE.DirBco1.TrimB() != "")
                                    {
                                        string argTemp10 = MODGSWF.VSwf[ind].BcoCoE.DirBco1.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp10, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (MODGSWF.VSwf[ind].BcoCoE.DirBco2.TrimB() != "")
                                    {
                                        string argTemp11 = MODGSWF.VSwf[ind].BcoCoE.DirBco2.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp11, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (MODGSWF.VSwf[ind].BcoCoE.PaiBco.TrimB() != "")
                                    {
                                        string argTemp12 = MODGSWF.VSwf[ind].BcoCoE.PaiBco.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp12, LC) + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        break;
                    case "54A":
                        if (sw.InStr("54A", 1, StringComparison.CurrentCulture) == 0)
                        {
                            if (MODGSWF.VSwf[ind].BcoCoD.SwfBco.TrimB() != "")
                            {
                                if (MODGSWF.VSwf[ind].BcoCoD.SwfBco.TrimB().Mid(8, 3) != "1  " && MODGSWF.VSwf[ind].BcoCoD.SwfBco.TrimB().Mid(8, 3) != "BIC" && !MODGSWF.VSwf[ind].BcoCoD.IngMan.ToBool())
                                {
                                    sw = sw + ":54A:" + MODGSWF.VSwf[ind].BcoCoD.SwfBco.TrimB() + 13.Char() + 10.Char();
                                }
                            }
                        }
                        break;
                    case "54D":
                        // Para ser D :   - Ingreso manual de datos.-
                        //                -   Swift terminado en BIC.-
                        //                -   Swift terminado en 1BlancoBlanco.-
                        // En el caso del Swift = BIC.
                        if (MODGSWF.VSwf[ind].BcoCoD.NomBco.TrimB() != "")
                        {
                            if (MODGSWF.VSwf[ind].BcoCoD.SwfBco.TrimB() == "" || MODGSWF.VSwf[ind].BcoCoD.SwfBco.TrimB().Mid(8, 3) == "1  " || MODGSWF.VSwf[ind].BcoCoD.SwfBco.TrimB().Mid(8, 3) == "BIC" || MODGSWF.VSwf[ind].BcoCoE.IngMan.ToBool())
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    if (MODGSWF.VSwf[ind].BcoCoD.NomBco.TrimB() != "")
                                    {
                                        string argTemp13 = MODGSWF.VSwf[ind].BcoCoD.NomBco.TrimB();
                                        sw = sw + ":54D:" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp13, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (MODGSWF.VSwf[ind].BcoCoD.DirBco1.TrimB() != "")
                                    {
                                        string argTemp14 = MODGSWF.VSwf[ind].BcoCoD.DirBco1.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp14, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (MODGSWF.VSwf[ind].BcoCoD.DirBco2.TrimB() != "")
                                    {
                                        string argTemp15 = MODGSWF.VSwf[ind].BcoCoD.DirBco2.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp15, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (MODGSWF.VSwf[ind].BcoCoD.PaiBco.TrimB() != "")
                                    {
                                        string argTemp16 = MODGSWF.VSwf[ind].BcoCoD.PaiBco.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp16, LC) + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        break;
                    case "55A":
                        if (MODGSWF.VSwf[ind].BcoTer.SwfBco.TrimB() != "")
                        {
                            if (MODGSWF.VSwf[ind].BcoTer.SwfBco.TrimB().Mid(8, 3) != "1  " && MODGSWF.VSwf[ind].BcoTer.SwfBco.TrimB().Mid(8, 3) != "BIC" && !MODGSWF.VSwf[ind].BcoTer.IngMan.ToBool())
                            {
                                sw = sw + ":55A:" + MODGSWF.VSwf[ind].BcoTer.SwfBco.TrimB() + 13.Char() + 10.Char();
                            }
                        }
                        break;
                    case "55D":
                        // Para ser D :   - Ingreso manual de datos.-
                        //                -   Swift terminado en BIC.-
                        //                -   Swift terminado en 1BlancoBlanco.-
                        // En el caso del Swift = BIC.
                        if (MODGSWF.VSwf[ind].BcoTer.NomBco.TrimB() != "")
                        {
                            if (MODGSWF.VSwf[ind].BcoTer.SwfBco.TrimB() == "" || MODGSWF.VSwf[ind].BcoCoD.SwfBco.TrimB().Mid(8, 3) == "1  " || MODGSWF.VSwf[ind].BcoTer.SwfBco.TrimB().Mid(8, 3) == "BIC" || MODGSWF.VSwf[ind].BcoTer.IngMan.ToBool())
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    if (MODGSWF.VSwf[ind].BcoTer.NomBco.TrimB() != "")
                                    {
                                        string argTemp17 = MODGSWF.VSwf[ind].BcoTer.NomBco.TrimB();
                                        sw = sw + ":55D:" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp17, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (MODGSWF.VSwf[ind].BcoTer.DirBco1.TrimB() != "")
                                    {
                                        string argTemp18 = MODGSWF.VSwf[ind].BcoTer.DirBco1.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp18, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (MODGSWF.VSwf[ind].BcoTer.DirBco2.TrimB() != "")
                                    {
                                        string argTemp19 = MODGSWF.VSwf[ind].BcoTer.DirBco2.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp19, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (MODGSWF.VSwf[ind].BcoTer.PaiBco.TrimB() != "")
                                    {
                                        string argTemp20 = MODGSWF.VSwf[ind].BcoTer.PaiBco.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp20, LC) + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        break;
                    case "56A":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-202")
                        {
                            // Primero: en el caso de existir Bco Intermediario.
                            if (MODGSWF.VSwf[ind].BcoInt.NomBco.TrimB() != "")
                            {
                                if (MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB() != "")
                                {
                                    if (MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB().Mid(8, 3) != "1  " && MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB().Mid(8, 3) != "BIC" && !MODGSWF.VSwf[ind].BcoInt.IngMan.ToBool())
                                    {
                                        sw = sw + ":56A:" + MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB() + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (sw.InStr("56A", 1, StringComparison.CurrentCulture) == 0)
                            {
                                if (MODGSWF.VSwf[ind].BcoInt.CodCom.TrimB() == "")
                                {
                                    if (MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB() != "")
                                    {
                                        if (MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB().Mid(8, 3) != "1  " && MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB().Mid(8, 3) != "BIC" && !MODGSWF.VSwf[ind].BcoInt.IngMan.ToBool())
                                        {
                                            sw = sw + ":56A:" + MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB() + 13.Char() + 10.Char();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "56C":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (MODGSWF.VSwf[ind].BcoInt.CodCom.TrimB() != "" && MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB() == "" && MODGSWF.VSwf[ind].BcoInt.NomBco.TrimB() == "")
                            {
                                sw = sw + ":56C:" + "//" + MODGSWF.VSwf[ind].BcoInt.CodCom.TrimB() + 13.Char() + 10.Char();
                            }
                        }
                        break;
                    case "56D":
                        // If VSwf(ind).NroSwf = "MT-202" Then
                        // Para ser D :   - Ingreso manual de datos.-
                        //                -   Swift terminado en BIC.-
                        //                -   Swift terminado en 1BlancoBlanco.-
                        // En el caso del Swift = BIC.
                        //                   If Trim$(VSwf(ind).BcoInt.NomBco) <> "" Then
                        //                       If Trim$(VSwf(ind).BcoInt.SwfBco) = "" Or Mid$(Trim$(VSwf(ind).BcoInt.SwfBco), 8, 3) = "1  " Or Mid$(Trim$(VSwf(ind).BcoInt.SwfBco), 8, 3) = "BIC" Or (VSwf(ind).BcoInt.IngMan = True) Then
                        //                           If VFmt_Swf(i%).Linea_Campo = 1 Then
                        //                               If Trim$(VSwf(ind).BcoInt.NomBco) <> "" Then sw = sw & ":56D:" & Fn_FormateaString(Trim$(VSwf(ind).BcoInt.NomBco), lc%) & Chr(13) & Chr(10)
                        //                           ElseIf VFmt_Swf(i%).Linea_Campo = 2 Then
                        //                               If Trim$(VSwf(ind).BcoInt.DirBco1) <> "" Then sw = sw & Fn_FormateaString(Trim$(VSwf(ind).BcoInt.DirBco1), lc%) & Chr(13) & Chr(10)
                        //                           ElseIf VFmt_Swf(i%).Linea_Campo = 3 Then
                        //                               If Trim$(VSwf(ind).BcoInt.DirBco2) <> "" Then sw = sw & Fn_FormateaString(Trim$(VSwf(ind).BcoInt.DirBco2), lc%) & Chr(13) & Chr(10)
                        //                           ElseIf VFmt_Swf(i%).Linea_Campo = 4 Then
                        //                               If Trim$(VSwf(ind).BcoInt.PaiBco) <> "" Then sw = sw & Fn_FormateaString(Trim$(VSwf(ind).BcoInt.PaiBco), lc%) & Chr(13) & Chr(10)
                        //                           End If
                        //                       End If
                        //                   End If
                        // End If
                        if (MODGSWF.VSwf[ind].BcoInt.CodCom.TrimB() != "" && MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB() == "" && MODGSWF.VSwf[ind].BcoInt.NomBco.TrimB() != "")
                        {
                            if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                sw = sw + ":56D:" + "//" + MODGSWF.VSwf[ind].BcoInt.CodCom.TrimB() + 13.Char() + 10.Char();
                            }
                            else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                string argTemp21 = MODGSWF.VSwf[ind].BcoInt.NomBco.TrimB().TrimB();
                                sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp21, LC) + 13.Char() + 10.Char();
                            }
                            else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (MODGSWF.VSwf[ind].BcoInt.DirBco1.TrimB() != "")
                                {
                                    string argTemp22 = MODGSWF.VSwf[ind].BcoInt.DirBco1.TrimB();
                                    sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp22, LC) + 13.Char() + 10.Char();
                                }
                            }
                            else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (MODGSWF.VSwf[ind].BcoInt.DirBco2.TrimB() != "")
                                {
                                    string argTemp23 = MODGSWF.VSwf[ind].BcoInt.DirBco2.TrimB();
                                    sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp23, LC) + 13.Char() + 10.Char();
                                }
                            }
                            else if (Modswen.VFmt_Swf[i].Linea_Campo == 5)
                            {
                                if (MODGSWF.VSwf[ind].BcoInt.PaiBco.TrimB() != "")
                                {
                                    string argTemp24 = MODGSWF.VSwf[ind].BcoInt.PaiBco.TrimB();
                                    sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp24, LC) + 13.Char() + 10.Char();
                                }
                            }
                        }
                        if (MODGSWF.VSwf[ind].BcoInt.NomBco.TrimB() != "" && MODGSWF.VSwf[ind].BcoInt.CodCom.TrimB() == "" && MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB() == "")
                        {
                            if (MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB() == "" || MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB().Mid(8, 3) == "1  " || MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB().Mid(8, 3) == "BIC" || MODGSWF.VSwf[ind].BcoInt.IngMan.ToBool())
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    if (MODGSWF.VSwf[ind].BcoInt.NomBco.TrimB() != "")
                                    {
                                        string argTemp25 = MODGSWF.VSwf[ind].BcoInt.NomBco.TrimB();
                                        sw = sw + ":56D:" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp25, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (MODGSWF.VSwf[ind].BcoInt.DirBco1.TrimB() != "")
                                    {
                                        string argTemp26 = MODGSWF.VSwf[ind].BcoInt.DirBco1.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp26, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (MODGSWF.VSwf[ind].BcoInt.DirBco2.TrimB() != "")
                                    {
                                        string argTemp27 = MODGSWF.VSwf[ind].BcoInt.DirBco2.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp27, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (MODGSWF.VSwf[ind].BcoInt.PaiBco.TrimB() != "")
                                    {
                                        string argTemp28 = MODGSWF.VSwf[ind].BcoInt.PaiBco.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp28, LC) + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        break;
                    case "57A":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (MODGSWF.VSwf[ind].BcoPag.CodCom.TrimB() == "")
                            {
                                if (MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB() != "" && MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB().Mid(8, 3) != "1  " && MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB().Mid(8, 3) != "BIC" && !MODGSWF.VSwf[ind].BcoPag.IngMan.ToBool())
                                {
                                    if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                    {
                                        sw = sw + ":57A:" + MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB() + 13.Char() + 10.Char();
                                    }
                                }
                            }
                            else
                            {
                                if (MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB() != "")
                                {
                                    if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                    {
                                        sw = sw + ":57A:" + MODGSWF.VSwf[ind].BcoPag.CodCom.TrimB() + 13.Char() + 10.Char();
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                    {
                                        if (MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB() != "")
                                        {
                                            string argTemp29 = MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB();
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp29, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "57B":
                        if (MODGSWF.VSwf[ind].BcoPag.CodCom.TrimB() != "" && MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB() == "" && MODGSWF.VSwf[ind].BcoPag.NomBco.TrimB() == "" && MODGSWF.VSwf[ind].BcoPag.PaiBco.TrimB() != "")
                        {
                            if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                sw = sw + ":57B:" + "//" + MODGSWF.VSwf[ind].BcoPag.CodCom.TrimB() + 13.Char() + 10.Char();
                            }
                            else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                string argTemp30 = MODGSWF.VSwf[ind].BcoPag.PaiBco.TrimB().TrimB();
                                sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp30, LC) + 13.Char() + 10.Char();
                            }
                        }
                        break;
                    case "57C":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (MODGSWF.VSwf[ind].BcoPag.CodCom.TrimB() != "" && MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB() == "" && MODGSWF.VSwf[ind].BcoPag.NomBco.TrimB() == "" && MODGSWF.VSwf[ind].BcoPag.PaiBco.TrimB() == "")
                            {
                                sw = sw + ":57C:" + "//" + MODGSWF.VSwf[ind].BcoPag.CodCom.TrimB() + 13.Char() + 10.Char();
                            }
                        }
                        break;
                    case "57D":
                        // Para ser D :   - Ingreso manual de datos.-
                        //                -   Swift terminado en BIC.-
                        //                -   Swift terminado en 1BlancoBlanco.-
                        if (MODGSWF.VSwf[ind].BcoPag.CodCom.TrimB() != "" && MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB() == "" && MODGSWF.VSwf[ind].BcoPag.NomBco.TrimB() != "")
                        {
                            if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                sw = sw + ":57D:" + "//" + MODGSWF.VSwf[ind].BcoPag.CodCom.TrimB() + 13.Char() + 10.Char();
                            }
                            else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                string argTemp31 = MODGSWF.VSwf[ind].BcoPag.NomBco.TrimB().TrimB();
                                sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp31, LC) + 13.Char() + 10.Char();
                            }
                            else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                            {
                                if (MODGSWF.VSwf[ind].BcoPag.DirBco1.TrimB() != "")
                                {
                                    string argTemp32 = MODGSWF.VSwf[ind].BcoPag.DirBco1.TrimB();
                                    sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp32, LC) + 13.Char() + 10.Char();
                                }
                            }
                            else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                            {
                                if (MODGSWF.VSwf[ind].BcoPag.DirBco2.TrimB() != "")
                                {
                                    string argTemp33 = MODGSWF.VSwf[ind].BcoPag.DirBco2.TrimB();
                                    sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp33, LC) + 13.Char() + 10.Char();
                                }
                            }
                            else if (Modswen.VFmt_Swf[i].Linea_Campo == 5)
                            {
                                if (MODGSWF.VSwf[ind].BcoPag.PaiBco.TrimB() != "")
                                {
                                    string argTemp34 = MODGSWF.VSwf[ind].BcoPag.PaiBco.TrimB();
                                    sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp34, LC) + 13.Char() + 10.Char();
                                }
                            }
                        }
                        if (MODGSWF.VSwf[ind].BcoPag.NomBco.TrimB() != "" && MODGSWF.VSwf[ind].BcoPag.CodCom.TrimB() == "" && MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB() == "")
                        {
                            if (MODGSWF.VSwf[ind].BcoPag.SwfBco == "" || MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB().Mid(8, 3) == "1  " || MODGSWF.VSwf[ind].BcoPag.IngMan.ToBool())
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    if (MODGSWF.VSwf[ind].BcoPag.NomBco.TrimB() != "")
                                    {
                                        sw = sw + ":57D:" + MODGSWF.VSwf[ind].BcoPag.NomBco.TrimB() + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (MODGSWF.VSwf[ind].BcoPag.DirBco1.TrimB() != "")
                                    {
                                        string argTemp35 = MODGSWF.VSwf[ind].BcoPag.DirBco1.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp35, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (MODGSWF.VSwf[ind].BcoPag.DirBco2.TrimB() != "")
                                    {
                                        string argTemp36 = MODGSWF.VSwf[ind].BcoPag.DirBco2.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp36, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (MODGSWF.VSwf[ind].BcoPag.PaiBco.TrimB() != "")
                                    {
                                        string argTemp37 = MODGSWF.VSwf[ind].BcoPag.PaiBco.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp37, LC) + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        break;
                    case "59":
                        if (!MODGSWF.VSwf[ind].BenSwf.Es59F)
                        {
                            if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                            {
                                if (MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB().TrimB() != "")
                                {
                                    if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                    {
                                        string argTemp38 = MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB();
                                        sw = sw + ":59:" + "/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp38, LC) + 13.Char() + 10.Char();
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.NomBen.TrimB() != "")
                                        {
                                            string argTemp39 = MODGSWF.VSwf[ind].BenSwf.NomBen.TrimB();
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp39, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB() != "")
                                        {
                                            string argTemp40 = MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB();
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp40, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB() != "")
                                        {
                                            if (MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB() != "")
                                            {
                                                string argTemp41 = MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB();
                                                string argTemp42 = MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB();
                                                sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp41, LC) + " " + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp42, LC) + 13.Char() + 10.Char();
                                            }
                                        }
                                        else
                                        {
                                            if (MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB() != "")
                                            {
                                                string argTemp43 = MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB();
                                                sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp43, LC) + 13.Char() + 10.Char();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.NomBen.TrimB() != "")
                                        {
                                            string argTemp44 = MODGSWF.VSwf[ind].BenSwf.NomBen.TrimB();
                                            sw = sw + ":59:" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp44, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB() != "")
                                        {
                                            string argTemp45 = MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB();
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp45, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB() != "")
                                        {
                                            if (MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB() != "")
                                            {
                                                string argTemp46 = MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB();
                                                string argTemp47 = MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB();
                                                sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp46, LC) + " " + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp47, LC) + 13.Char() + 10.Char();
                                            }
                                        }
                                        else
                                        {
                                            if (MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB() != "")
                                            {
                                                string argTemp48 = MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB();
                                                sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp48, LC) + 13.Char() + 10.Char();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "59F":
                        if (MODGSWF.VSwf[ind].BenSwf.Es59F)
                        {
                            if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                            {
                                if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].DatSwf.ctacte.Trim()))
                                {
                                    string _tempVar59f = String.Empty;
                                    if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                    {
                                        _tempVar59f = MODGSWF.VSwf[ind].DatSwf.ctacte.Trim();
                                        sw += ":59F:" + "/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref _tempVar59f, LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                    {
                                        if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.NomBen))
                                        {
                                            _tempVar59f = VB6Helpers.Left(MODGSWF.VSwf[ind].BenSwf.NomBen.Trim(), LC);
                                            sw += "1/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref _tempVar59f, 33) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                    {
                                        if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.NomBen) && MODGSWF.VSwf[ind].BenSwf.NomBen.Trim().Length > 33)
                                        {
                                            _tempVar59f = MODGSWF.VSwf[ind].BenSwf.NomBen.Trim().Substring(33);
                                            sw += "1/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref _tempVar59f, 33) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                        else if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.DirBen1))
                                        {
                                            _tempVar59f = VB6Helpers.Left(MODGSWF.VSwf[ind].BenSwf.DirBen1.Trim(), LC);
                                            sw += "2/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref _tempVar59f, 33) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                    {
                                        if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.NomBen) && MODGSWF.VSwf[ind].BenSwf.NomBen.Trim().Length > 33 && !String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.DirBen1))
                                        {
                                            _tempVar59f = VB6Helpers.Left(MODGSWF.VSwf[ind].BenSwf.DirBen1.Trim(), LC);
                                            sw += "2/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref _tempVar59f, 33) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                        else if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.DirBen1) && MODGSWF.VSwf[ind].BenSwf.DirBen1.Trim().Length > 33) {
                                            _tempVar59f = MODGSWF.VSwf[ind].BenSwf.DirBen1.Trim().Substring(33);
                                            sw += "2/" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref _tempVar59f, 33) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 5)
                                    {
                                        if (!String.IsNullOrEmpty(MODGSWF.VSwf[ind].BenSwf.PaiBen59F))
                                        {
                                            _tempVar59f = VB6Helpers.Trim(MODGSWF.VSwf[ind].BenSwf.DirBen2);
                                            sw += VB6Helpers.Left(("3/" + MODGSWF.VSwf[ind].BenSwf.PaiBen59F.Trim() + (String.IsNullOrEmpty(_tempVar59f) ? String.Empty : "/" + _tempVar59f)), 35) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "58A":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-202")
                        {
                            if (MODGSWF.VSwf[ind].BenSwf.SwfBen.TrimB().Mid(8) != "1")
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    if (MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB() != "")
                                    {
                                        sw = sw + ":58A:" + "/" + MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB() + 13.Char() + 10.Char();
                                    }
                                    else
                                    {
                                        sw = sw + ":58A:" + MODGSWF.VSwf[ind].BenSwf.SwfBen.TrimB() + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB() != "")
                                    {
                                        sw = sw + MODGSWF.VSwf[ind].BenSwf.SwfBen.TrimB() + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        break;
                    case "58D":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-202")
                        {
                            if (MODGSWF.VSwf[ind].BenSwf.SwfBen.TrimB() == "" || MODGSWF.VSwf[ind].BenSwf.SwfBen.TrimB().Mid(8) == "1")
                            {
                                if (MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB() != "")
                                {
                                    if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                    {
                                        sw = sw + ":58D:" + "/" + MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB() + 13.Char() + 10.Char();
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                    {
                                        string argTemp49 = MODGSWF.VSwf[ind].BenSwf.NomBen.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp49, LC) + 13.Char() + 10.Char();
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB() != "")
                                        {
                                            string argTemp50 = MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB();
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp50, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB() != "")
                                        {
                                            string argTemp51 = MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB();
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp51, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 5)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB() != "")
                                        {
                                            string argTemp52 = MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB();
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp52, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                }
                                else
                                {
                                    if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                    {
                                        string argTemp53 = MODGSWF.VSwf[ind].BenSwf.NomBen.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp53, LC) + 13.Char() + 10.Char();
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB() != "")
                                        {
                                            string argTemp54 = MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB();
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp54, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB() != "")
                                        {
                                            string argTemp55 = MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB();
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp55, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                    else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                    {
                                        if (MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB() != "")
                                        {
                                            string argTemp56 = MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB();
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp56, LC) + 13.Char() + 10.Char();
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "70":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            linea = Modswen.VFmt_Swf[i].Linea_Campo;
                            if (linea == 1)
                            {
                                Arreglo = new string[1];
                                if (MODGSWF.VSwf[ind].DatSwf.RefOpe.TrimB() != "")
                                {
                                    if (MODGSWF.VSwf[ind].DatSwf.RefOpe.InStr("/RFB/", 1, StringComparison.CurrentCulture) == 0)
                                    {
                                        MODGSWF.VSwf[ind].DatSwf.RefOpe = "/RFB/" + MODGSWF.VSwf[ind].DatSwf.RefOpe;
                                    }

                                    int nLineas = (MODGSWF.VSwf[ind].DatSwf.RefOpe.Length - 1) / 35 + 1;
                                    List<string> arregloAux = Arreglo.ToList();
                                    for (int nLinea = 0; nLinea < nLineas; nLinea++)
                                    {
                                        arregloAux.Add(VB6Helpers.Mid(MODGSWF.VSwf[ind].DatSwf.RefOpe, nLinea * LC + 1, LC));
                                    }
                                    Arreglo = arregloAux.ToArray();

                                    //MODGPYF1.SeparaL(Globales, MODGSWF.VSwf[ind].DatSwf.RefOpe, ref Arreglo, LC, LC * 4);
                                }
                            }
                            if (linea <= Math.Min(Arreglo.GetUpperBound(0), 4))
                            {
                                if (linea == 1)
                                {
                                    if (Arreglo[linea] != "")
                                    {
                                        sw = sw + ":70:" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref Arreglo[linea], LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else
                                {
                                    if (Arreglo[linea] != "")
                                    {
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref Arreglo[linea], LC) + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        break;
                    case "71A":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                            if (MODGSWF.VSwf[ind].DatSwf.TipGas == 1)
                            {
                                sw = sw + ":71A:" + "BEN" + 13.Char() + 10.Char();
                            }
                            else if (MODGSWF.VSwf[ind].DatSwf.TipGas == 2)
                            {
                                sw = sw + ":71A:" + "OUR" + 13.Char() + 10.Char();
                            }
                            else if (MODGSWF.VSwf[ind].DatSwf.TipGas == 3)
                            {
                                sw = sw + ":71A:" + "SHA" + 13.Char() + 10.Char();
                            }
                        }
                        break;
                    case "71F":
                        if (MODGSWF.VSwf[ind].DatSwf.TipGas == 1)
                        {
                            sw = sw + ":71F:" + MODGSWF.VSwf[ind].SwfMon.TrimB() + Format.FormatCurrency(MODGSWF.VMT103[MODGSWF.Indice_Monto].GasEmi, T_MODGSWF.FormatoSwf).TrimB() + 13.Char() + 10.Char();
                        }
                        else if (MODGSWF.VMT103[MODGSWF.Indice_Monto].GasEmi > 0)
                        {
                            sw = sw + ":71F:" + MODGSWF.VSwf[ind].SwfMon.TrimB() + Format.FormatCurrency(MODGSWF.VMT103[MODGSWF.Indice_Monto].GasEmi, T_MODGSWF.FormatoSwf).TrimB() + 13.Char() + 10.Char();
                        }
                        break;
                    case "71G":
                        if (MODGSWF.VMT103[MODGSWF.Indice_Monto].GasRec > 0)
                        {
                            sw = sw + ":71G:" + MODGSWF.VSwf[ind].SwfMon.TrimB() + Format.FormatCurrency(MODGSWF.VMT103[MODGSWF.Indice_Monto].GasRec, T_MODGSWF.FormatoSwf).TrimB() + 13.Char() + 10.Char();
                        }
                        break;
                    case "72":

                        linea = Modswen.VFmt_Swf[i].Linea_Campo;
                        if (linea == 1)
                        {
                            Arreglo = new string[1];
                            string camp = BCH.Comex.Core.BL.XCFT.Modulos.MODSWENN.Fn_CampoMTManual(MODGSWF.VSwf[ind].DocSwf, Modswen.VFmt_Swf[i].Id_Campo + " ", "INFO DE REMITENTE A DESTINATARIO").Trim();
                            Arreglo = camp.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
                        }
                        if (linea <= Arreglo.Length)
                        {
                            if (linea == 1)
                            {
                                if (!string.IsNullOrEmpty(Arreglo[linea - 1]))
                                {
                                    sw = sw + ":" + Modswen.VFmt_Swf[i].Id_Campo + ":" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref Arreglo[linea - 1], LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Arreglo[linea - 1]))
                                {
                                    if (VB6Helpers.Instr(Arreglo[linea - 1], "//") == 0)
                                    {
                                        Arreglo[linea - 1] = "//" + Arreglo[linea - 1];
                                    }
                                    else
                                    {
                                        Arreglo[linea - 1] = "//" + Arreglo[linea - 1].Replace("/","");
                                    }
                                    sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref Arreglo[linea - 1], LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                }
                            }
                        }
                        break;
                    case "77B":
                        if (MODGSWF.VSwf[ind].NroSwf == "MT-103")
                        {
                                linea = Modswen.VFmt_Swf[i].Linea_Campo;
                                if (linea == 1)
                                {
                                    Arreglo = new string[1];
                                    string camp = BCH.Comex.Core.BL.XCFT.Modulos.MODSWENN.Fn_CampoMTManual(MODGSWF.VSwf[ind].DocSwf, "77B", "INFO EXIGIDA POR REGLAMENTOS").Trim();
                                    Arreglo = camp.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
                                }

                                if (linea <= Arreglo.Length)
                                {
                                    if (linea == 1)
                                    {
                                        if (!string.IsNullOrEmpty(Arreglo[linea - 1]))
                                        {
                                            sw = sw + ":77B:" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref Arreglo[linea - 1], LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(Arreglo[linea - 1]))
                                        {
                                            sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref Arreglo[linea - 1], LC) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
                                        }
                                    }
                                }
                            }
                            break;
                }

            }
            sw = sw + "-}" + 3.Char();
            Fn_Gen103Sw = sw;

            return Fn_Gen103Sw;
        }
        public static string Fn_Gen202Sw(DatosGlobales Globales,UnitOfWorkCext01 unit, int ind)
        {
            T_MODGSWF MODGSWF = Globales.MODGSWF;
            T_Modswen Modswen = Globales.Modswen;

            string Fn_Gen202Sw = "";

            string Fecha_Paso1 = "";
            int LC = 0;
            int i = 0;
            string sw = "";
            string Fn_Gen100Sw = "";
            string s = "";
            string Bco = "";

            string[] Arreglo = null;

            if (MODGSWF.VSwf[ind].BcoAla.SwfBco.TrimB() != "")
            {
                Bco = MODGSWF.VSwf[ind].BcoAla.SwfBco;
            }
            else
            {
                Bco = MODGSWF.VSwf[ind].DatSwf.SwfCor;
            }

            if (!BCH.Comex.Core.BL.XGGL.Modulos.Modswen.BancoIntercambiaClave(Globales,Bco))
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text= "No se podrá emitir el " + MODGSWF.VSwf[ind].NroSwf + " automáticamente, ya que la casilla SWIFT del Banco Receptor (" + Bco + "), no está habilitada. Deberá emitir un Télex.",
                    Type=TipoMensaje.Error,
                    Title="SWIFT"
                });
                Fn_Gen100Sw = "";
                return Fn_Gen202Sw;
            }

            Fn_Gen202Sw = "";

            sw = "";
            sw = sw + 1.Char() + "{1:F01";
            sw = sw + "BCHICLRMAXXX";
            sw = sw + "          }";
            sw = sw + "{2:I";
            sw = sw + MODGSWF.VSwf[ind].NroSwf.Mid(4, 3);
            if (MODGSWF.VSwf[ind].BcoAla.SwfBco.TrimB() != "")
            {
                sw = sw + MODGSWF.VSwf[ind].BcoAla.SwfBco.TrimB().Mid(1, 8) + "A" + MODGSWF.VSwf[ind].BcoAla.SwfBco.TrimB().Mid(9, 3);
            }
            else
            {
                sw = sw + MODGSWF.VSwf[ind].DatSwf.SwfCor.TrimB().Mid(1, 8) + "A" + MODGSWF.VSwf[ind].DatSwf.SwfCor.TrimB().Mid(9, 3);
            }
            sw = sw + "N";
            sw = sw + "}{4:" + 13.Char() + 10.Char();


            if (BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_Trae_Fmt_Campos(Globales, "MT202") == 0)
            {
                return Fn_Gen202Sw;
            }
            for (i = 1; i <= Modswen.VFmt_Swf.GetUpperBound(0); i += 1)
            {
                // msgbox(VFmt_Swf(i%).Id_Campo)
                LC = Modswen.VFmt_Swf[i].Largo_Campo;
                switch (Modswen.VFmt_Swf[i].Id_Campo)
                {
                    case "20":
                        sw = sw + ":20:" + MODGSWF.VGSwf.NumOpe.TrimB() + 13.Char() + 10.Char();
                        break;
                    case "21":
                        if (String.IsNullOrEmpty(MODGSWF.VSwf[ind].DatSwf.RefOpe))
                        {
                            sw = sw + ":21:" + MODGSWF.VGSwf.NumOpe.TrimB() + 13.Char() + 10.Char();
                        }
                        else
                        {
                            sw = sw + ":21:" + MODGPYF0.Componer(MODGSWF.VSwf[ind].DatSwf.RefOpe, "/RFB/", "").TrimB() + 13.Char() + 10.Char();
                        }
                        break;
                    case "32A":
                        // Campo32A
                        Fecha_Paso1 = MigrationSupport.Utils.Format(MODGSWF.VSwf[ind].DatSwf.FecPag, "yymmdd");
                        sw = sw + ":32A:" + Fecha_Paso1.TrimB() + MODGSWF.VSwf[ind].SwfMon.TrimB() + Format.FormatCurrency(MODGSWF.VSwf[ind].mtoswf, "############0.00").TrimB() + 13.Char() + 10.Char();
                        break;
                    case "56A":
                        // Primero: en el caso de existir Bco Intermediario.
                        if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                        {
                            if (MODGSWF.VSwf[ind].BcoInt.NomBco.TrimB() != "")
                            {
                                if (MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB() != "")
                                {
                                    if (MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB().Mid(8, 3) != "1  " && MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB().Mid(8, 3) != "BIC" && !MODGSWF.VSwf[ind].BcoInt.IngMan.ToBool())
                                    {
                                        sw = sw + ":56A:" + MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB() + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        break;
                    case "56D":
                        // Para ser D :   - Ingreso manual de datos.-
                        //                -   Swift terminado en BIC.-
                        //                -   Swift terminado en 1BlancoBlanco.-
                        // En el caso del Swift = BIC.
                        if (MODGSWF.VSwf[ind].BcoInt.NomBco.TrimB() != "")
                        {
                            if (MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB() == "" || MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB().Mid(8, 3) == "1  " || MODGSWF.VSwf[ind].BcoInt.SwfBco.TrimB().Mid(8, 3) == "BIC" || MODGSWF.VSwf[ind].BcoInt.IngMan.ToBool())
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    if (MODGSWF.VSwf[ind].BcoInt.NomBco.TrimB() != "")
                                    {
                                        string argTemp1 = MODGSWF.VSwf[ind].BcoInt.NomBco.TrimB();
                                        sw = sw + ":56D:" + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp1, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (MODGSWF.VSwf[ind].BcoInt.DirBco1.TrimB() != "")
                                    {
                                        string argTemp2 = MODGSWF.VSwf[ind].BcoInt.DirBco1.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp2, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (MODGSWF.VSwf[ind].BcoInt.DirBco2.TrimB() != "")
                                    {
                                        string argTemp3 = MODGSWF.VSwf[ind].BcoInt.DirBco2.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp3, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (MODGSWF.VSwf[ind].BcoInt.PaiBco.TrimB() != "")
                                    {
                                        string argTemp4 = MODGSWF.VSwf[ind].BcoInt.PaiBco.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp4, LC) + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        break;
                    case "57A":
                        if (MODGSWF.VSwf[ind].BcoPag.NomBco.TrimB() != "")
                        {
                            if (MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB() != "" && MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB().Mid(8, 3) != "1  " && MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB().Mid(8, 3) != "BIC" && !MODGSWF.VSwf[ind].BcoPag.IngMan.ToBool())
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    sw = sw + ":57A:" + MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB() + 13.Char() + 10.Char();
                                }
                            }
                        }
                        break;
                    case "57D":
                        if (MODGSWF.VSwf[ind].BcoPag.NomBco.TrimB() != "")
                        {
                            if (MODGSWF.VSwf[ind].BcoPag.SwfBco == "" || MODGSWF.VSwf[ind].BcoPag.SwfBco.TrimB().Mid(8, 3) == "1  " || MODGSWF.VSwf[ind].BcoPag.IngMan.ToBool())
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    if (MODGSWF.VSwf[ind].BcoPag.NomBco.TrimB() != "")
                                    {
                                        sw = sw + ":57D:" + MODGSWF.VSwf[ind].BcoPag.NomBco.TrimB() + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (MODGSWF.VSwf[ind].BcoPag.DirBco1.TrimB() != "")
                                    {
                                        string argTemp5 = MODGSWF.VSwf[ind].BcoPag.DirBco1.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp5, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (MODGSWF.VSwf[ind].BcoPag.DirBco2.TrimB() != "")
                                    {
                                        string argTemp6 = MODGSWF.VSwf[ind].BcoPag.DirBco2.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp6, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (MODGSWF.VSwf[ind].BcoPag.PaiBco.TrimB() != "")
                                    {
                                        string argTemp7 = MODGSWF.VSwf[ind].BcoPag.PaiBco.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp7, LC) + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        break;
                    case "58A":
                        if (MODGSWF.VSwf[ind].BenSwf.SwfBen.TrimB().Mid(8) != "1")
                        {
                            if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                            {
                                if (MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB() != "")
                                {
                                    sw = sw + ":58A:" + "/" + MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB() + 13.Char() + 10.Char();
                                }
                                else
                                {
                                    sw = sw + ":58A:" + MODGSWF.VSwf[ind].BenSwf.SwfBen.TrimB() + 13.Char() + 10.Char();
                                }
                            }
                            else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                            {
                                if (MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB() != "")
                                {
                                    sw = sw + MODGSWF.VSwf[ind].BenSwf.SwfBen.TrimB() + 13.Char() + 10.Char();
                                }
                            }
                        }
                        break;
                    case "58D":
                        if (MODGSWF.VSwf[ind].BenSwf.SwfBen.TrimB() == "" || MODGSWF.VSwf[ind].BenSwf.SwfBen.TrimB().Mid(8) == "1")
                        {
                            if (MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB() != "")
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    sw = sw + ":58D:" + "/" + MODGSWF.VSwf[ind].DatSwf.ctacte.TrimB() + 13.Char() + 10.Char();
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    string argTemp8 = MODGSWF.VSwf[ind].BenSwf.NomBen.TrimB();
                                    sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp8, LC) + 13.Char() + 10.Char();
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB() != "")
                                    {
                                        string argTemp9 = MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp9, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB() != "")
                                    {
                                        string argTemp10 = MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp10, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 5)
                                {
                                    if (MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB() != "")
                                    {
                                        string argTemp11 = MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp11, LC) + 13.Char() + 10.Char();
                                    }
                                }
                            }
                            else
                            {
                                if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                                {
                                    string argTemp12 = MODGSWF.VSwf[ind].BenSwf.NomBen.TrimB();
                                    sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp12, LC) + 13.Char() + 10.Char();
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 2)
                                {
                                    if (MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB() != "")
                                    {
                                        string argTemp13 = MODGSWF.VSwf[ind].BenSwf.DirBen1.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp13, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 3)
                                {
                                    if (MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB() != "")
                                    {
                                        string argTemp14 = MODGSWF.VSwf[ind].BenSwf.DirBen2.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp14, LC) + 13.Char() + 10.Char();
                                    }
                                }
                                else if (Modswen.VFmt_Swf[i].Linea_Campo == 4)
                                {
                                    if (MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB() != "")
                                    {
                                        string argTemp15 = MODGSWF.VSwf[ind].BenSwf.PaiBen_t.TrimB();
                                        sw = sw + BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_FormateaString(ref argTemp15, LC) + 13.Char() + 10.Char();
                                    }
                                }
                            }
                        }
                        break;
                    case "71A":
                        if (MODGSWF.VSwf[ind].DatSwf.TipGas == 1)
                        {
                            sw = sw + ":71A:" + "BEN" + 13.Char() + 10.Char();
                        }
                        else if (MODGSWF.VSwf[ind].DatSwf.TipGas == 2)
                        {
                            sw = sw + ":71A:" + "OUR" + 13.Char() + 10.Char();
                        }
                        break;
                    case "72":
                        if (Modswen.VFmt_Swf[i].Linea_Campo == 1)
                        {
                            s = BCH.Comex.Core.BL.XGGL.Modulos.Modswen.Fn_CampoMT(Globales,"72", MODGSWF.VSwf[ind].DocSwf, "72", "", "", 43, 6);
                            sw = sw + s;
                        }
                        break;
                }

            }
            sw = sw + "-}" + 3.Char();
            Fn_Gen202Sw = sw;

            return Fn_Gen202Sw;
        }

        public static string Fn_EncSwiLoc(DatosGlobales Globales, int ind)
        {
            string s = "";
            s += "SCE";
            s = s + DateTime.Now.ToString("dd/MM/yyyy");
            s += VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.TimeOfDay), "hh:mm:ss");
            s = s + "MT" + VB6Helpers.Mid(Globales.MODGSWF.VSwf[ind].NroSwf, 4, 3);
            s += "0000000000";
            s = s + VB6Helpers.Right("00000000000000000" + VB6Helpers.Trim(Globales.MODGSWF.VGSwf.NumOpe), 16);
            s += Globales.MODGSWF.VSwf[ind].SwfMon;
            s = s + VB6Helpers.Right("0000000000000000000000000000000" + VB6Helpers.Trim(Format.FormatCurrency(Globales.MODGSWF.VSwf[ind].mtoswf, "############0.00")), 30) + VB6Helpers.Chr(13) + VB6Helpers.Chr(10);

            return s;
        }

        public static void Pr_ActSwift(DatosGlobales Globales, int ind, int GrabSW, int CorSwi)
        {
            Globales.MODGSWF.VSwf[ind].GrabSW = GrabSW.ToShort();
            Globales.MODGSWF.VSwf[ind].CorSwi = CorSwi;
        }

        public static string Fn_EncSwi(DatosGlobales Globales, int ind, string rut, string Corr)
        {
            string monto = "0000000000000000000000000000000";
            string sw = "";
            monto = VB6Helpers.Right(monto + VB6Helpers.CStr(Globales.MODGSWF.VSwf[ind].mtoswf), 30);

            sw += rut;
            sw += Globales.MODGUSR.UsrEsp.CentroCosto;
            sw += Globales.MODGSWF.VSwf[ind].SwfMon;
            sw += monto;
            sw += Corr;
            sw += "A";
            return sw;
        }
    }
}
