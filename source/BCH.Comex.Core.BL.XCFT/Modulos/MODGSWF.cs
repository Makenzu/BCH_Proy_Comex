using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public static class MODGSWF
    {
        public static T_MODGSWF GetMODGSWF()
        {

            return new T_MODGSWF();
        }

        private static string GetDescDeCampo(IList<sce_tccs> descripciones, short codMT, string codCam)
        {
            List<sce_tccs> listaFiltrada = descripciones.Where(t => t.codmt == codMT && t.codcam.Trim() == codCam).ToList();
            return listaFiltrada.Select(t => t.descam).FirstOrDefault();
        }

        public static string GeneraDocSwf(T_MODGSWF MODGSWF, T_MODGUSR MODGUSR, T_MODGTAB0 MODGTAB0,
            T_Mdl_Funciones Mdl_Funciones, T_Mdl_Funciones_Varias Mdl_Funciones_Varias, T_MOD_50F MOD_50F, List<UI_Message> MESSAGES,
            T_MODGCVD MODGCVD, short CARGA_AUTOMATICA, UnitOfWorkCext01 uow, IList<LineaMensajeSwift> lineasManuales, short indiceMonto)
        {
            short MT = 0;

            Printer printer = new Printer();
            T_Swf swift = MODGSWF.VSwf[indiceMonto];
            IList<sce_tccs> descripciones = uow.SceRepository.GetDescripcionesDeCodigosDeCamposSwift();


            if (CARGA_AUTOMATICA == 0)
            {
                //----------------------------------------
                //Identifica el Tipo de Swift. (100 - 202)
                if (swift.BenSwf.EsBanco)
                {
                    MT = T_MODGSWF.MT_202;
                }
                else
                {
                    MT = T_MODGSWF.MT_103;
                }

                //----------------------------------------

                Pr_Print_Encabezado(printer, MODGUSR, swift, MT);
                Pr_Print_Blancos(printer, 2);
                if (MT == T_MODGSWF.MT_202)
                {
                    Pr_Print_Cuerpo202(printer, MODGSWF, swift, descripciones, MT);
                }
                else if (MT == T_MODGSWF.MT_103)
                {
                    Pr_Print_Cuerpo(printer, MODGSWF, MODGTAB0, MOD_50F, uow, descripciones, MT, indiceMonto);
                }

                Pr_Print_Referencia(printer, swift, MESSAGES, descripciones, MT);
                if (MT == T_MODGSWF.MT_202)
                {
                    Pr_Print_Gastos202(printer, swift, descripciones, MT);
                }
                else if (MT == T_MODGSWF.MT_103)
                {
                    Pr_Print_Gastos(printer, swift, MODGSWF.VMT103[indiceMonto], descripciones, MT);
                }

                if (swift.EsAladi)
                {
                    Pr_Print_Aladi(printer, swift, descripciones, MT);
                }

                Pr_Print_CamposManuales(printer, lineasManuales);
            }
            else
            {
                //SOLO GENERA MENSAJE SWFT SI ES OUNTGOING
                if (Mdl_Funciones_Varias.LC_PRD == "72" || Mdl_Funciones_Varias.LC_PRD == "74")
                {
                    //CAMPO MESSAGE TYPE
                    if (Mdl_Funciones_Varias.LC_PRD == "72")
                    {
                        MT = T_MODGSWF.MT_103;
                    }
                    if (Mdl_Funciones_Varias.LC_PRD == "74")
                    {
                        MT = T_MODGSWF.MT_202;
                    }

                    Pr_Print_Encabezado_Auto(printer, Mdl_Funciones_Varias, MT);
                    Pr_Print_Blancos(printer, 2);
                    if (MT == T_MODGSWF.MT_202)
                    {
                        Pr_Print_Cuerpo202_Auto(printer, MODGSWF, MODGTAB0, Mdl_Funciones_Varias, swift, descripciones, MT);
                    }
                    else if (MT == T_MODGSWF.MT_103)
                    {
                        Pr_Print_Cuerpo103_Auto(printer, MODGSWF, MODGTAB0, Mdl_Funciones_Varias, MODGCVD, swift, descripciones, MT);
                    }
                }
            }

            return printer.ToString();
        }


        //****************************************************************************
        //   1.  Imprime en pantalla la línea 72 el # Aladi.-
        //****************************************************************************
        public static short Pr_Print_Aladi(Printer printer, T_Swf swift, IList<sce_tccs> descripciones, short MT)
        {
            short _retValue = 0;
            string s = "";
            _retValue = (short)(true ? -1 : 0);
            if (MT == T_MODGSWF.MT_103)
            {
                s = swift.DatSwf.NroAla;
                if (string.IsNullOrEmpty(s))
                {
                    _retValue = (short)(false ? -1 : 0);
                }
                printer.PrintList(": 72", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "72"), Printer.TAB(T_MODGMTS.TabD), "/CONVENIO/" + s);
            }

            return _retValue;
        }

        //****************************************************************************
        //   1.  Determina si es Banco Aladi para colocar la cabecera y datos
        //       correspondiente.
        //****************************************************************************
        public static void Pr_Print_Bco_Aladi(Printer printer, T_Swf swift, short indicador)
        {
            switch (indicador)
            {
                case 1:
                    if (!string.IsNullOrWhiteSpace(swift.BcoAla.SwfBco))
                    {
                        if (VB6Helpers.Mid(VB6Helpers.Trim(swift.BcoAla.SwfBco), 8) == "1")
                        {
                            printer.PrintList(Printer.TAB(27), " T E L E X ");
                        }
                        else
                        {
                            printer.PrintList(Printer.TAB(27), " S W I F T ");
                        }

                    }
                    else
                    {
                        printer.PrintList(Printer.TAB(27), " S W I F T ");
                    }

                    printer.PrintList(Printer.TAB(27), " ========= ");

                    break;
                case 2:
                    if (!string.IsNullOrWhiteSpace(swift.BcoAla.SwfBco))
                    {
                        if (VB6Helpers.Mid(VB6Helpers.Trim(swift.BcoAla.SwfBco), 8) != "1")
                        {
                            if (!string.IsNullOrWhiteSpace(swift.BcoAla.NomBco))
                            {
                                if (!string.IsNullOrWhiteSpace(swift.BcoAla.DirBco1))
                                {
                                    printer.PrintList("RECEIVER", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.SwfBco) + "  " + VB6Helpers.Trim(VB6Helpers.UCase(VB6Helpers.Left(swift.BcoAla.NomBco, 18))) + ", " + VB6Helpers.Trim(VB6Helpers.UCase(VB6Helpers.Left(swift.BcoAla.DirBco1, 14))));
                                }
                                else
                                {
                                    printer.PrintList("RECEIVER", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.SwfBco) + "  " + VB6Helpers.Trim(VB6Helpers.UCase(VB6Helpers.Left(swift.BcoAla.NomBco, 25))));
                                }

                                if ((!string.IsNullOrWhiteSpace(swift.BcoAla.DirBco2)) && (!string.IsNullOrWhiteSpace(swift.BcoAla.PaiBco)))
                                {
                                    printer.PrintList(Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.DirBco2) + "," + VB6Helpers.Trim(swift.BcoAla.PaiBco));
                                }
                                else
                                {
                                    if ((string.IsNullOrWhiteSpace(swift.BcoAla.DirBco2)) && (!string.IsNullOrWhiteSpace(swift.BcoAla.PaiBco)))
                                    {
                                        printer.PrintList(Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.PaiBco));
                                    }

                                    if ((!string.IsNullOrWhiteSpace(swift.BcoAla.DirBco2)) && (string.IsNullOrWhiteSpace(swift.BcoAla.PaiBco)))
                                    {
                                        printer.PrintList(Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.DirBco2));
                                    }

                                }

                            }

                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(swift.BcoAla.NomBco))
                            {
                                if (!string.IsNullOrWhiteSpace(swift.BcoAla.DirBco1))
                                {
                                    printer.PrintList("RECEIVER", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.SwfBco) + "  " + VB6Helpers.Trim(VB6Helpers.UCase(VB6Helpers.Left(swift.BcoAla.NomBco, 18))) + ", " + VB6Helpers.Trim(VB6Helpers.UCase(VB6Helpers.Left(swift.BcoAla.DirBco1, 14))));
                                }
                                else
                                {
                                    printer.PrintList("RECEIVER", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.SwfBco) + " " + VB6Helpers.Trim(VB6Helpers.UCase(VB6Helpers.Left(swift.BcoAla.NomBco, 25))));
                                }

                                if ((!string.IsNullOrWhiteSpace(swift.BcoAla.DirBco2)) && (!string.IsNullOrWhiteSpace(swift.BcoAla.PaiBco)))
                                {
                                    printer.PrintList(Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.DirBco2) + "," + VB6Helpers.Trim(swift.BcoAla.PaiBco));
                                }
                                else
                                {
                                    if ((string.IsNullOrWhiteSpace(swift.BcoAla.DirBco2)) && (!string.IsNullOrWhiteSpace(swift.BcoAla.PaiBco)))
                                    {
                                        printer.PrintList(Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.PaiBco));
                                    }

                                    if ((!string.IsNullOrWhiteSpace(swift.BcoAla.DirBco2)) && (string.IsNullOrWhiteSpace(swift.BcoAla.PaiBco)))
                                    {
                                        printer.PrintList(Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.DirBco2));
                                    }

                                }

                            }

                        }

                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(swift.BcoAla.NomBco))
                        {
                            if (!string.IsNullOrWhiteSpace(swift.BcoAla.NomBco))
                            {
                                if (!string.IsNullOrWhiteSpace(swift.BcoAla.DirBco1))
                                {
                                    printer.PrintList("RECEIVER", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.SwfBco) + "  " + VB6Helpers.Trim(VB6Helpers.UCase(VB6Helpers.Left(swift.BcoAla.NomBco, 18))) + ", " + VB6Helpers.Trim(VB6Helpers.UCase(VB6Helpers.Left(swift.BcoAla.DirBco1, 14))));
                                }
                                else
                                {
                                    printer.PrintList("RECEIVER", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.SwfBco) + " " + VB6Helpers.Trim(VB6Helpers.UCase(VB6Helpers.Left(swift.BcoAla.NomBco, 25))));
                                }

                                if (!string.IsNullOrWhiteSpace(swift.BcoAla.DirBco2) && !string.IsNullOrWhiteSpace(swift.BcoAla.PaiBco))
                                {
                                    printer.PrintList(Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.DirBco2) + "," + VB6Helpers.Trim(swift.BcoAla.PaiBco));
                                }
                                else
                                {
                                    if (string.IsNullOrWhiteSpace(swift.BcoAla.DirBco2) && !string.IsNullOrWhiteSpace(swift.BcoAla.PaiBco))
                                    {
                                        printer.PrintList(Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.PaiBco));
                                    }

                                    if (!string.IsNullOrWhiteSpace(swift.BcoAla.DirBco2) && string.IsNullOrEmpty(swift.BcoAla.PaiBco))
                                    {
                                        printer.PrintList(Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.BcoAla.DirBco2));
                                    }

                                }

                            }

                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(swift.DatSwf.SwfCor))
                            {
                                printer.PrintList("RECEIVER", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(swift.DatSwf.SwfCor));
                            }
                        }

                    }

                    break;
            }

        }

        //****************************************************************************
        //   1.  Determina si existen Bancos Pagadores, Bancos Intermediarios para
        //       imprimir las líneas 56...(A-D) y 57..(A-D).
        //****************************************************************************
        public static void Pr_Print_Bco_PagInt(Printer printer, T_Swf swift, IList<sce_tccs> descripciones, short MT)
        {

            //************************************
            //Para la Línea 56 (A-D).
            //Primero: en el caso de existir Bco Intermediario.
            //----------------------------------------

            if (MT == 103)
            {
                if (!string.IsNullOrWhiteSpace(swift.BcoInt.CodCom))
                {
                    if (!String.IsNullOrEmpty(swift.BcoInt.SwfBco))
                    {
                        printer.PrintList(": 56A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56A"), Printer.TAB(T_MODGMTS.TabD), "//" + VB6Helpers.Trim(swift.BcoInt.CodCom));
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.SwfBco));
                    }
                    else if (String.IsNullOrEmpty(swift.BcoInt.SwfBco) && !String.IsNullOrEmpty(swift.BcoInt.NomBco))
                    {
                        printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), "//" + VB6Helpers.Trim(swift.BcoInt.CodCom));
                        printer.PrintList(VB6Helpers.CStr((T_MODGMTS.TabD)), VB6Helpers.CStr((T_MODGMTS.TabD)), VB6Helpers.Trim(swift.BcoInt.NomBco));
                        if (!string.IsNullOrWhiteSpace(swift.BcoInt.DirBco1))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.DirBco1));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoInt.DirBco2))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.DirBco2));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoInt.PaiBco))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.PaiBco));
                        }
                    }
                    else if (String.IsNullOrEmpty(swift.BcoInt.SwfBco) && String.IsNullOrEmpty(swift.BcoInt.NomBco) && !!string.IsNullOrWhiteSpace(swift.BcoInt.PaiBco))
                    {
                        printer.PrintList(": 56B", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56B"), Printer.TAB(T_MODGMTS.TabD), "//" + VB6Helpers.Trim(swift.BcoInt.CodCom));
                        if (!string.IsNullOrWhiteSpace(swift.BcoInt.PaiBco))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.PaiBco));
                        }
                    }
                    else
                    {
                        printer.PrintList(": 56C", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56C"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.CodCom));
                    }

                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(swift.BcoInt.SwfBco) || !string.IsNullOrWhiteSpace(swift.BcoInt.NomBco))
                    {
                        if (!String.IsNullOrEmpty(swift.BcoPag.SwfBco))
                        {
                            printer.PrintList(": 56A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.SwfBco));
                        }
                        else if (!String.IsNullOrEmpty(swift.BcoInt.NomBco))
                        {
                            printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.NomBco));
                            if (!string.IsNullOrWhiteSpace(swift.BcoInt.DirBco1))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.DirBco1));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BcoInt.DirBco2))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.DirBco2));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BcoInt.PaiBco))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.PaiBco));
                            }
                        }

                    }

                }

                if (!string.IsNullOrWhiteSpace(swift.BcoPag.CodCom))
                {
                    if (!String.IsNullOrEmpty(swift.BcoPag.SwfBco))
                    {
                        printer.PrintList(": 57A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57A"), Printer.TAB(T_MODGMTS.TabD), "//" + VB6Helpers.Trim(swift.BcoPag.CodCom));
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.SwfBco));
                    }
                    else if (String.IsNullOrEmpty(swift.BcoPag.SwfBco) && !String.IsNullOrEmpty(swift.BcoPag.NomBco))
                    {
                        printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), "//" + VB6Helpers.Trim(swift.BcoPag.CodCom));
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), Printer.TAB(T_MODGMTS.TabD), swift.BcoPag.NomBco);
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoPag.DirBco1)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.DirBco1));
                        }
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoPag.DirBco2)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.DirBco2));
                        }
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoPag.PaiBco)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.PaiBco));
                        }
                    }
                    else if (String.IsNullOrEmpty(swift.BcoPag.SwfBco) && String.IsNullOrEmpty(swift.BcoPag.NomBco) && !String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoPag.PaiBco)))
                    {
                        printer.PrintList(": 57B", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57B"), Printer.TAB(T_MODGMTS.TabD), "//" + VB6Helpers.Trim(swift.BcoPag.CodCom));
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoPag.PaiBco)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.PaiBco));
                        }
                    }
                    else
                    {
                        printer.PrintList(": 57C", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57C"), Printer.TAB(T_MODGMTS.TabD), "//" + VB6Helpers.Trim(swift.BcoPag.CodCom));
                    }

                }
                else
                {
                    if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoPag.SwfBco)) || !String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoPag.NomBco)))
                    {
                        if (!String.IsNullOrEmpty(swift.BcoPag.SwfBco))
                        {
                            printer.PrintList(": 57A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.SwfBco));
                        }
                        else if (!String.IsNullOrEmpty(swift.BcoPag.NomBco))
                        {
                            printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.NomBco));
                            if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoPag.DirBco1)))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.DirBco1));
                            }
                            if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoPag.DirBco2)))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.DirBco2));
                            }
                            if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoPag.PaiBco)))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.PaiBco));
                            }
                        }

                    }

                }

            }

        }

        public static void Pr_Print_Bco_CorTer(Printer printer, T_Swf swift, IList<sce_tccs> descripciones, short MT)
        {

            //************************************
            //Para la Línea 53-55 (A-D).
            //----------------------------------------

            if (MT == 103)
            {
                if (!String.IsNullOrEmpty(swift.BcoCoE.SwfBco) || !String.IsNullOrEmpty(swift.BcoCoE.NomBco))
                {
                    if (!String.IsNullOrEmpty(swift.BcoCoE.SwfBco))
                    {
                        printer.PrintList(": 53A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "53A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoCoE.SwfBco));
                    }
                    else if (!String.IsNullOrEmpty(swift.BcoCoE.NomBco))
                    {
                        printer.PrintList(": 53D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "53D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoCoE.NomBco));
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoCoE.DirBco1)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoCoE.DirBco1));
                        }
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoCoE.DirBco2)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoCoE.DirBco2));
                        }
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoCoE.PaiBco)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoCoE.PaiBco));
                        }
                    }

                }

                if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoCoD.SwfBco)) || !String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoCoD.NomBco)))
                {
                    if (!String.IsNullOrEmpty(swift.BcoCoD.SwfBco))
                    {
                        printer.PrintList(": 54A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "54A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoCoD.SwfBco));
                    }
                    else if (!String.IsNullOrEmpty(swift.BcoCoD.NomBco))
                    {
                        printer.PrintList(": 54D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "54D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoCoD.NomBco));
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoCoD.DirBco1)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoCoD.DirBco1));
                        }
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoCoD.DirBco2)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoCoD.DirBco2));
                        }
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoCoD.PaiBco)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoCoD.PaiBco));
                        }
                    }

                }

                if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoTer.SwfBco)) || !String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoTer.NomBco)))
                {
                    if (!String.IsNullOrEmpty(swift.BcoTer.SwfBco))
                    {
                        printer.PrintList(": 55A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "55A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoTer.SwfBco));
                    }
                    else if (!String.IsNullOrEmpty(swift.BcoTer.NomBco))
                    {
                        printer.PrintList(": 55D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "55D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoTer.NomBco));
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoTer.DirBco1)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoTer.DirBco1));
                        }
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoTer.DirBco2)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoTer.DirBco2));
                        }
                        if (!String.IsNullOrEmpty(VB6Helpers.Trim(swift.BcoTer.PaiBco)))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoTer.PaiBco));
                        }
                    }

                }

            }

        }

        //****************************************************************************
        //   1.  Imprime en pantalla n lineas en blanco dadas por los Cuantos
        //       enviados como parámetros.
        //****************************************************************************
        public static void Pr_Print_Blancos(Printer printer, short Cuantos)
        {
            short i = 0;
            for (i = 1; i <= (short)Cuantos; i++)
            {
                printer.PrintList(" ");
            }

        }

        //****************************************************************************
        //   1.  Para la línea 59 (MT-103), si existe Cuenta Corriente se imprime en
        //       pantalla esta cuenta junto con los datos del Beneficiario, en caso
        //       contrario sólo se imprimen los datos del Beneficiario.
        //   2.  Para la línea 58 (TM-202), si existe Cuenta Corriente se imprime en
        //       pantalla esta cuenta junto con los datos del Beneficiario, en caso
        //       contrario sólo se imprimen los datos del Beneficiario.
        //****************************************************************************
        public static void Pr_Print_Cuenta(Printer printer, T_Swf swift, IList<sce_tccs> descripciones, short MT)
        {

            if (MT == T_MODGSWF.MT_103)
            {
                if (!swift.BenSwf.Es59F)
                {
                    if (!string.IsNullOrWhiteSpace(swift.DatSwf.ctacte))
                    {
                        Pr_Print_Blancos(printer, 1);
                        printer.PrintList(": 59", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "59"), Printer.TAB(T_MODGMTS.TabD), "/" + VB6Helpers.Trim(swift.DatSwf.ctacte));
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.NomBen));
                    }
                    else
                    {
                        Pr_Print_Blancos(printer, 1);
                        printer.PrintList(": 59", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "59"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.NomBen));
                    }

                    if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen1))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen1));
                    }
                    if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen2))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen2));
                    }
                    if (!string.IsNullOrWhiteSpace(swift.BenSwf.PaiBen_t))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.PaiBen_t));
                    }
                }
                else
                {
                    Pr_Print_Blancos(printer, 1);
                    int limit = (int)descripciones.Where(t => t.codmt == MT && t.codcam.Trim() == "59F").Select(t => t.lenlin).FirstOrDefault();
                    printer.PrintList(": 59F", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "59F"), Printer.TAB(T_MODGMTS.TabD), "/" + swift.DatSwf.ctacte);
                    bool printLinea2Nombre = false;
                    if (!String.IsNullOrEmpty(swift.BenSwf.NomBen))
                    {
                        string nomBen = "1/" + swift.BenSwf.NomBen.Trim();
                        if (nomBen.Length > limit)
                        {
                            printLinea2Nombre = true;
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(nomBen, 1, limit));
                            string nomBen2 = "1/" + VB6Helpers.Mid(nomBen, limit + 1, limit);
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(nomBen2, 1, limit));
                        }
                        else
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), nomBen);
                        }
                    }

                    if (!String.IsNullOrEmpty(swift.BenSwf.DirBen1))
                    {
                        string dirBen1 = "2/" + swift.BenSwf.DirBen1.Trim();
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(dirBen1, 1, limit));
                        if (dirBen1.Length > limit && !printLinea2Nombre)
                        {
                            string dirBen1b = "2/" + VB6Helpers.Mid(dirBen1, limit + 1, limit);
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(dirBen1b, 1, limit));
                        }
                    }
                    if (!String.IsNullOrEmpty(swift.BenSwf.PaiBen59F))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid("3/" + swift.BenSwf.PaiBen59F + (String.IsNullOrEmpty(swift.BenSwf.DirBen2) ? String.Empty : "/" + swift.BenSwf.DirBen2), 1, limit));
                    }
                }
            }
            else
            {
                if (MT == T_MODGSWF.MT_202)
                {
                    //En el caso del Swift <> BIC.
                    if (VB6Helpers.Mid(VB6Helpers.Trim(swift.BenSwf.SwfBen), 8) != "1")
                    {
                        Pr_Print_Blancos(printer, 1);
                        if (!string.IsNullOrWhiteSpace(swift.DatSwf.ctacte))
                        {
                            printer.PrintList(": 58A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "58A"), Printer.TAB(T_MODGMTS.TabD), @"/" + VB6Helpers.Trim(swift.DatSwf.ctacte));
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.SwfBen));
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.NomBen));
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen1))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen1));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen2))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen2));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.PaiBen_t))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.PaiBen_t));
                            }
                        }
                        else
                        {
                            printer.PrintList(": 58A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "58A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.SwfBen));
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen1))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen1));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen2))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen2));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.PaiBen_t))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.PaiBen_t));
                            }
                        }

                    }
                    else
                    {
                        //En el caso del Swift = BIC.
                        Pr_Print_Blancos(printer, 1);
                        if (!string.IsNullOrWhiteSpace(swift.DatSwf.ctacte))
                        {
                            printer.PrintList(": 58D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "58D"), Printer.TAB(T_MODGMTS.TabD), @"/" + VB6Helpers.Trim(swift.DatSwf.ctacte));
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.NomBen));
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen1))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen1));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen2))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen2));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.PaiBen_t))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.PaiBen_t));
                            }
                        }
                        else
                        {
                            printer.PrintList(": 58D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "58D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.NomBen));
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen1))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen1));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen2))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen2));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.PaiBen_t))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.PaiBen_t));
                            }
                        }

                    }

                }

            }

        }

        //****************************************************************************
        //   1.  Imprime en pantalla todo el cuerpo del Swift que es obligatorio.
        //       Este cuerpo puede variar dependiendo de lo ingresado por el
        //       usuario, por lo tanto es necesario hacer llamadas a distintos
        //       procedimientos que posee en su interior este Pr_Print_Cuerpo.
        //****************************************************************************
        public static void Pr_Print_Cuerpo(Printer printer, T_MODGSWF MODGSWF, T_MODGTAB0 T_MODGTAB0, T_MOD_50F MOD_50F, UnitOfWorkCext01 uow, IList<sce_tccs> descripciones, short MT, int indiceMonto)
        {
            short X = 0;
            string Fecha_Paso1 = "";
            short m = 0;
            string SwfMon = "";
            T_Swf swift = MODGSWF.VSwf[indiceMonto];

            printer.PrintList(": 20", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "20"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VGSwf.NumOpe));
            Pr_Print_Blancos(printer, 1);

            if (MT == T_MODGSWF.MT_202)
            {
                if (!string.IsNullOrWhiteSpace(swift.DatSwf.RefOpe))
                {
                    printer.PrintList(": 21", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "21"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGPYF0.Componer(swift.DatSwf.RefOpe, "/RFB/", "")));
                    Pr_Print_Blancos(printer, 1);
                }

            }

            printer.PrintList(": 23B", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "23B"), Printer.TAB(T_MODGMTS.TabD), "CRED");

            if (MT == T_MODGSWF.MT_103)
            {
                if (VB6Helpers.UBound(MODGSWF.VCod) > 0)
                {
                    for (X = 0; X <= (short)VB6Helpers.UBound(MODGSWF.VCod); X++)
                    {
                        if (MODGSWF.VCod[X].Estado != 9)
                        {
                            if (MODGSWF.VCod[X].numswi == indiceMonto)
                            {
                                printer.PrintList(": 23E", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "23E"), Printer.TAB(T_MODGMTS.TabD), MODGSWF.VCod[X].Codigo);
                            }

                        }
                    }
                }
            }

            Fecha_Paso1 = VB6Helpers.Format(swift.DatSwf.FecPag, "dd/mm/yyyy");
            printer.PrintList(": 32A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "32A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Fecha_Paso1), " ", VB6Helpers.Trim(swift.SwfMon), " ", VB6Helpers.Trim(MODGPYF0.forma(swift.mtoswf, T_MODGSWF.FormatoSwf)));
            Pr_Print_Blancos(printer, 1);

            if (MT == T_MODGSWF.MT_103)
            {
                if (!string.IsNullOrWhiteSpace(VB6Helpers.CStr(MODGSWF.VMT103[indiceMonto].MndOri)) && VB6Helpers.Trim(VB6Helpers.CStr(MODGSWF.VMT103[indiceMonto].MtoOri)) != "0")
                {
                    m = MODGTAB0.Get_VMnd(T_MODGTAB0, uow, MODGSWF.VMT103[indiceMonto].MndOri);
                    SwfMon = T_MODGTAB0.VMnd[m].Mnd_MndSwf;

                    printer.PrintList(": 33B", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "33B"), Printer.TAB(T_MODGMTS.TabD), SwfMon, " ", VB6Helpers.Trim(MODGPYF0.forma(MODGSWF.VMT103[indiceMonto].MtoOri, T_MODGSWF.FormatoSwf)));
                    Pr_Print_Blancos(printer, 1);
                    if (MODGSWF.VMT103[indiceMonto].TipCam != 0)
                    {
                        printer.PrintList(": 36", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "36"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGPYF0.forma(MODGSWF.VMT103[indiceMonto].TipCam, T_MODGSWF.FormatoSwf)));
                        Pr_Print_Blancos(printer, 1);
                    }

                }

                //Flag 50F
                //-------------------------------------------

                //realiza visualizacion de mensaje 50F o 50K
                if (MOD_50F.CHK_50F == true)
                {
                    //if (string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.CtaCli))
                    //{
                    //    printer.PrintList(": 50F", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "50F"), Printer.TAB(T_MODGMTS.TabD), "NIDN/CL/" + VB6Helpers.Trim(MODGSWF.VCliSwf.rutcli));
                    //}
                    //else
                    //{
                    //    printer.PrintList(": 50F", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "50F"), Printer.TAB(T_MODGMTS.TabD), "/" + VB6Helpers.Trim(MODGSWF.VCliSwf.CtaCli));
                    //}

                    printer.PrintList(": 50F", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "50F"), Printer.TAB(T_MODGMTS.TabD), "/" + VB6Helpers.Trim(MODGSWF.VCliSwf.CtaCli));

                    bool printLinea2 = true;
                    if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.NomCli))
                    {
                        string linea1 = VB6Helpers.Trim(MODGSWF.VCliSwf.NomCli);
                        string linea1a = VB6Helpers.Mid(linea1, 1, 33);
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid("1/" + linea1a, 1, 35));

                        if (linea1.Length > 33)
                        {
                            printLinea2 = false;
                            string linea1b = VB6Helpers.Mid(linea1, 34, 33);
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid("1/" + linea1b, 1, 35));
                        }

                    }

                    if (printLinea2 && !string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.DirCli1))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid("2/" + VB6Helpers.Trim(MODGSWF.VCliSwf.DirCli1), 1, 35));
                    }

                    if (!string.IsNullOrWhiteSpace(MOD_50F.VG_50F[indiceMonto, 2]))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid("3/" + VB6Helpers.Trim(MOD_50F.VG_50F[indiceMonto, 2]) + "/" + VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VCliSwf.CiuCli), 1, 35), 1, 35));
                    }

                    if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.rutcli))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid("6/" + VB6Helpers.Trim(MOD_50F.VG_50F[indiceMonto, 2]) + "/BCHICLRM/" + VB6Helpers.Mid(VB6Helpers.Trim(MODGSWF.VCliSwf.rutcli), 1, 35), 1, 35));

                    }

                }
                else
                {
                    printer.PrintList(": 50K", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "50K"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VCliSwf.NomCli));
                    if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.rutcli))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), "Rut:" + VB6Helpers.Trim(MODGSWF.VCliSwf.rutcli));
                    }

                    if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.DirCli1))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VCliSwf.DirCli1));
                    }

                    if (!string.IsNullOrWhiteSpace(MOD_50F.VG_50F[indiceMonto, 2]))
                    {
                        if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.PaiCli))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VCliSwf.DirCli2) + "+" + VB6Helpers.Trim(MOD_50F.VG_50F[indiceMonto, 2]));
                        }
                        else
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VCliSwf.DirCli2));
                        }

                    }

                }

            }

            Pr_Print_Bco_CorTer(printer, swift, descripciones, MT);
            Pr_Print_Bco_PagInt(printer, swift, descripciones, MT);
            Pr_Print_Cuenta(printer, swift, descripciones, MT);
            Pr_Print_Blancos(printer, 1);
        }
        //****************************************************************************
        //   1.  Imprime en pantalla todo el cuerpo del Swift que es obligatorio.
        //       Este cuerpo puede variar dependiendo de lo ingresado por el
        //       usuario, por lo tanto es necesario hacer llamadas a distintos
        //       procedimientos que posee en su interior este Pr_Print_Cuerpo.
        //****************************************************************************
        public static void Pr_Print_Cuerpo103_Auto(Printer printer, T_MODGSWF MODGSWF, T_MODGTAB0 T_MODGTAB0, T_Mdl_Funciones_Varias Mdl_Funciones_Varias, T_MODGCVD MODGCVD, T_Swf swift, IList<sce_tccs> descripciones, short MT)
        {
            string Fecha_Paso1 = "";
            string ls_dato = "";
            short c = 0;
            string MND_SWF = "";

            if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
            {
                printer.PrintList(": 20", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "20"), Printer.TAB(T_MODGMTS.TabD), Mdl_Funciones_Varias.LC_CONREFNUM);
            }
            else
            {
                printer.PrintList(": 20", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "20"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(MODGCVD.VgCvd.OpeSin, 6, 2) + VB6Helpers.Mid(MODGCVD.VgCvd.OpeSin, 8, 3) + VB6Helpers.Mid(MODGCVD.VgCvd.OpeSin, 11, 5));
            }

            Pr_Print_Blancos(printer, 1);

            printer.PrintList(": 23B", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "23B"), Printer.TAB(T_MODGMTS.TabD), "SSTD");

            Fecha_Paso1 = VB6Helpers.Format(swift.DatSwf.FecPag, "dd/mm/yyyy");

            printer.PrintList(": 26T", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "26T"), Printer.TAB(T_MODGMTS.TabD), "103");
            Pr_Print_Blancos(printer, 1);
            //************************************************************
            //Rutina para sacar el codigo de moneda SWIFT
            for (c = 1; c <= (short)VB6Helpers.UBound(T_MODGTAB0.VMnd); c++)
            {
                if (T_MODGTAB0.VMnd[c].Mnd_MndCod == Mdl_Funciones_Varias.LC_MONEDA)
                {
                    MND_SWF = VB6Helpers.Trim(T_MODGTAB0.VMnd[c].Mnd_MndSwf);
                    break;
                }

            }

            //************************************************************
            printer.PrintList(": 32A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "32A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_DRVALDT), 7, 2) + VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_DRVALDT), 3, 2) + VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_DRVALDT), 1, 2), " ", VB6Helpers.Trim(MND_SWF), " ", VB6Helpers.Trim(Mdl_Funciones_Varias.LC_MONTO));
            Pr_Print_Blancos(printer, 1);
            //************************************************************
            printer.PrintList(": 33B", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "33B"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MND_SWF), " ", VB6Helpers.Trim(Mdl_Funciones_Varias.LC_MONTO));
            Pr_Print_Blancos(printer, 1);
            //************************************************************
            printer.PrintList(": 50K", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "50K"), Printer.TAB(T_MODGMTS.TabD), "/", VB6Helpers.LTrim(Mdl_Funciones_Varias.LC_OUTGOING));
            if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.NomCli))
            {
                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VCliSwf.NomCli));
            }

            if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.DirCli1))
            {
                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VCliSwf.DirCli1));
            }

            if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.DirCli2))
            {
                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VCliSwf.DirCli2));
            }

            if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.rutcli))
            {
                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), "RUT-:" + VB6Helpers.Trim(MODGSWF.VCliSwf.rutcli));
            }

            //************************************************************
            if (Mdl_Funciones_Varias.LC_MONEDA == VB6Helpers.CDbl("11"))
            {
                ls_dato = "10999073";
            }
            if (Mdl_Funciones_Varias.LC_MONEDA == VB6Helpers.CDbl("96"))
            {
                ls_dato = "05530296";
            }
            printer.PrintList(": 53D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "53D"), Printer.TAB(T_MODGMTS.TabD), "/", VB6Helpers.Trim(ls_dato));
            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), "Banco de Chile");
            //*********** CAMPO 56a de SWIFT **********************
            // Se emplea TAG US_PAY_ID solamente para FCCFT5 en conjunto con Intermediario.
            // Para este campo se emplea el TAG INTRMD1, INTRMD2, INTRMD3, INTRMD4

            if (Mdl_Funciones_Varias.LC_FCCFT == "FCCFT5")
            {
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 9)
                {
                    printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("//FW") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                    }

                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 8)
                {
                    if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_US_PAY_ID, 1, 1) == "/")
                    {
                        printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    }
                    else
                    {
                        printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("/") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    }

                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                    }

                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 4)
                {
                    printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("//CP") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                    }

                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 0)
                {
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD1) > 0)
                    {
                        if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_INTRMD1, 1, 2) == "A:")
                        {
                            printer.PrintList(": 56", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                        }
                        else
                        {
                            printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                            if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD2) > 0)
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD2));
                            }

                        }

                    }

                }

            }
            else
            {
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD1) > 0)
                {
                    if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_INTRMD1, 1, 2) == "A:")
                    {
                        printer.PrintList(": 56", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                    }
                    else
                    {
                        printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                        if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD2) > 0)
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD2));
                        }

                    }

                }

            }

            //************* FIN Campo 56a *******************************
            //***********************************************************
            Pr_Print_Blancos(printer, 1);

            if (Mdl_Funciones_Varias.LC_FCCFT != "FCCFT5")
            {
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 9)
                {
                    printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("//FW") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                    }

                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 8)
                {
                    if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_US_PAY_ID, 1, 1) == "/")
                    {
                        printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    }
                    else
                    {
                        printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("/") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    }

                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                    }

                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 4)
                {
                    printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("//CP") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                    }

                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 0)
                {
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES1) > 0)
                    {
                        if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_RECVR_CORRES1, 1, 2) == "A:")
                        {
                            printer.PrintList(": 57", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                        }
                        else
                        {
                            printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                            if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES2) > 0)
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES2));
                            }

                        }

                    }

                }

            }
            else
            {
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES1) > 0)
                {
                    if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_RECVR_CORRES1, 1, 2) == "A:")
                    {
                        printer.PrintList(": 57", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                    }
                    else
                    {
                        printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                        if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES2) > 0)
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES2));
                        }

                    }

                }

            }

            //************* FIN Campo 57a *******************************
            //***********************************************************
            Pr_Print_Blancos(printer, 1);
            if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_BEN_INST1) > 0)
            {
                printer.PrintList(": 59", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "59"), Printer.TAB(T_MODGMTS.TabD), "/", VB6Helpers.Trim(Mdl_Funciones_Varias.LC_BEN_INST1));
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_ULT_BEN1) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_ULT_BEN1) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_ULT_BEN2) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_ULT_BEN2) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_ULT_BEN3) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_ULT_BEN3) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_ULT_BEN4) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_ULT_BEN4) + VB6Helpers.Space(35), 1, 35));
                }

            }
            else
            {
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_ULT_BEN1) > 0)
                {
                    if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_ULT_BEN1, 1, 2) == "A:")
                    {
                        printer.PrintList(": 59A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "59A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_ULT_BEN1), 3));
                    }
                    else
                    {
                        printer.PrintList(": 59", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "59"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_ULT_BEN1) + VB6Helpers.Space(35), 1, 35));
                        if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_ULT_BEN2) > 0)
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_ULT_BEN2) + VB6Helpers.Space(35), 1, 35));
                        }

                        if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_ULT_BEN3) > 0)
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_ULT_BEN3) + VB6Helpers.Space(35), 1, 35));
                        }

                        if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_ULT_BEN4) > 0)
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_ULT_BEN4) + VB6Helpers.Space(35), 1, 35));
                        }

                    }

                }

            }

            //***********************************
            Pr_Print_Blancos(printer, 1);
            //*********** CAMPO 70 de SWIFT **********************

            if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_PMNT_DET1) > 0)
            {
                printer.PrintList(": 70", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "70"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_PMNT_DET1) + VB6Helpers.Space(35), 1, 35));
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_PMNT_DET2) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_PMNT_DET2) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_PMNT_DET3) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_PMNT_DET3) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_PMNT_DET4) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_PMNT_DET4) + VB6Helpers.Space(35), 1, 35));
                }

            }

            //**************************
            Pr_Print_Blancos(printer, 1);

            if (Mdl_Funciones_Varias.LC_CHG_WHOM == "8")
            {
                printer.PrintList(": 71A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "71A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("OUR"));
            }
            else if (Mdl_Funciones_Varias.LC_CHG_WHOM != "8")
            {
                printer.PrintList(": 71A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "71A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("BEN"));
            }

            //**************************
            Pr_Print_Blancos(printer, 1);
            if (Mdl_Funciones_Varias.LC_CHG_WHOM != "8")
            {
                printer.PrintList(": 71F", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "71F"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("CLP0"));
            }

            //*****************************
            Pr_Print_Blancos(printer, 1);
            //*********** CAMPO 72a de SWIFT **********************
            if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1) > 0)
            {
                if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1, 1, 3) == "12/")
                {
                    printer.PrintList(": 72", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "72"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim("/ACC/") + VB6Helpers.Trim(VB6Helpers.Mid(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1, 4)) + VB6Helpers.Space(35), 1, 35));
                }
                else
                {
                    printer.PrintList(": 72", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "72"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO2) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO2) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO3) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO3) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO4) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO4) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO5) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO5) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO6) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO6) + VB6Helpers.Space(35), 1, 35));
                }

            }

            Pr_Print_Blancos(printer, 1);
        }

        //****************************************************************************
        //   1.  Imprime en pantalla todo el cuerpo del Swift que es obligatorio.
        //       Este cuerpo puede variar dependiendo de lo ingresado por el
        //       usuario, por lo tanto es necesario hacer llamadas a distintos
        //       procedimientos que posee en su interior este Pr_Print_Cuerpo.
        //****************************************************************************
        public static void Pr_Print_Cuerpo202(Printer printer, T_MODGSWF MODGSWF, T_Swf swift, IList<sce_tccs> descripciones, short MT)
        {
            string Fecha_Paso1 = "";
            printer.PrintList(": 20", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "20"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VGSwf.NumOpe));
            Pr_Print_Blancos(printer, 1);

            if (MT == T_MODGSWF.MT_202)
            {
                if (!string.IsNullOrWhiteSpace(swift.DatSwf.RefOpe))
                {
                    printer.PrintList(": 21", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "21"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGPYF0.Componer(swift.DatSwf.RefOpe, "/RFB/", "")));
                    Pr_Print_Blancos(printer, 1);
                }

            }

            Fecha_Paso1 = VB6Helpers.Format(swift.DatSwf.FecPag, "dd/mm/yyyy");
            printer.PrintList(": 32A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "32A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Fecha_Paso1), " ", VB6Helpers.Trim(swift.SwfMon), " ", VB6Helpers.Trim(MODGPYF0.forma(swift.mtoswf, T_MODGSWF.FormatoSwf)));
            Pr_Print_Blancos(printer, 1);

            if (MT == T_MODGSWF.MT_100)
            {
                printer.PrintList(": 50", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "50"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VCliSwf.NomCli));
                if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.DirCli1))
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VCliSwf.DirCli1));
                }

                if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.DirCli2))
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VCliSwf.DirCli2));
                }

                if (!string.IsNullOrWhiteSpace(MODGSWF.VCliSwf.PaiCli))
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), "(Rut:" + VB6Helpers.Trim(MODGSWF.VCliSwf.rutcli) + ") " + VB6Helpers.Trim(MODGSWF.VCliSwf.PaiCli));
                }

            }

            Pr_Print_Bco_PagInt202(printer, swift, descripciones, MT);
            Pr_Print_Cuenta202(printer, swift, descripciones, MT);
            Pr_Print_Blancos(printer, 1);
        }

        //****************************************************************************
        //   1.  Imprime en pantalla todo el cuerpo del Swift que es obligatorio.
        //       Este cuerpo puede variar dependiendo de lo ingresado por el
        //       usuario, por lo tanto es necesario hacer llamadas a distintos
        //       procedimientos que posee en su interior este Pr_Print_Cuerpo.
        //****************************************************************************
        public static void Pr_Print_Cuerpo202_Auto(Printer printer, T_MODGSWF MODGSWF, T_MODGTAB0 T_MODGTAB0, T_Mdl_Funciones_Varias Mdl_Funciones_Varias, T_Swf swift, IList<sce_tccs> descripciones, short MT)
        {
            string lc_dato = "";
            string LC_MND_SWF = "";
            short c = 0;

            printer.PrintList(": 20", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "20"), Printer.TAB(T_MODGMTS.TabD), Mdl_Funciones_Varias.LC_CONREFNUM); //Trim$(VGSwf.NumOpe)
            Pr_Print_Blancos(printer, 1);
            //************************************************************
            printer.PrintList(": 21", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "21"), Printer.TAB(T_MODGMTS.TabD), Mdl_Funciones_Varias.LC_CONREFNUM); //Trim$(Componer(VSwf(Indice_Monto).DatSwf.RefOpe, "/RFB/", ""))
            Pr_Print_Blancos(printer, 1);
            //************************************************************
            //Rutina para sacar el codigo de moneda SWIFT
            for (c = 1; c <= (short)VB6Helpers.UBound(T_MODGTAB0.VMnd); c++)
            {
                if (T_MODGTAB0.VMnd[c].Mnd_MndNom == VB6Helpers.UCase(Mdl_Funciones_Varias.LC_NOM_MDA))
                {
                    LC_MND_SWF = VB6Helpers.Trim(T_MODGTAB0.VMnd[c].Mnd_MndSwf);
                    break;
                }

            }

            printer.PrintList(": 32A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "32A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_DRVALDT), " ", VB6Helpers.Trim(LC_MND_SWF), " ", VB6Helpers.Trim(Mdl_Funciones_Varias.LC_MONTO));
            Pr_Print_Blancos(printer, 1);
            //************************************************************
            printer.PrintList(": 52D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "52D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGSWF.VCliSwf.NomCli));
            Pr_Print_Blancos(printer, 1);
            //************************************************************
            if (Mdl_Funciones_Varias.LC_MONEDA == VB6Helpers.CDbl("001") || VB6Helpers.Val(Mdl_Funciones_Varias.LC_MONEDA) == VB6Helpers.CDbl("11"))
            {
                lc_dato = "10999073";
            }
            if (Mdl_Funciones_Varias.LC_MONEDA == VB6Helpers.CDbl("151") || VB6Helpers.Val(Mdl_Funciones_Varias.LC_MONEDA) == VB6Helpers.CDbl("96"))
            {
                lc_dato = "05530296";
            }

            printer.PrintList(": 53D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "53D"), Printer.TAB(T_MODGMTS.TabD), "/", VB6Helpers.Trim(lc_dato));
            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), "BANCO DE CHILE");
            Pr_Print_Blancos(printer, 1);
            //************************************************************

            //*********** CAMPO 56a de SWIFT **********************
            // Se emplea TAG US_PAY_ID solamente para FCCFT5 en conjunto con Intermediario.
            // Para este campo se emplea el TAG INTRMD1, INTRMD2, INTRMD3, INTRMD4

            if (Mdl_Funciones_Varias.LC_FCCFT == "FCCFT5")
            {
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 9)
                {
                    printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("//FW") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                    }

                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 8)
                {
                    if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_US_PAY_ID, 1, 1) == "/")
                    {
                        printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    }
                    else
                    {
                        printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("/") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    }

                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                    }

                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 4)
                {
                    printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("//CP") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                    }

                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 0)
                {
                    if (VB6Helpers.CDbl((Mdl_Funciones_Varias.LC_INTRMD1)) > 0)
                    {
                        if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_INTRMD1, 1, 2) == "A:")
                        {
                            printer.PrintList(": 56", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                        }
                        else
                        {
                            printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                            if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD2) > 0)
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD2));
                            }

                        }

                    }

                }

            }
            else
            {
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD1) > 0)
                {
                    if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_INTRMD1, 1, 2) == "A:")
                    {
                        printer.PrintList(": 56", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                    }
                    else
                    {
                        printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD1));
                        if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_INTRMD2) > 0)
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_INTRMD2));
                        }

                    }

                }

            }

            //************* FIN Campo 56a *******************************
            //***********************************************************

            Pr_Print_Blancos(printer, 1);

            //*********** CAMPO 57a de SWIFT **********************
            // Se emplea TAG US_PAY_ID para todos los demás FCCFT exceptuando
            // FCCFT5 en conjunto con Receiver_CORRESP.
            // Para este campo se emplea el TAG RECVR_CORRES1, RECVR_CORRES2, RECVR_CORRES3, RECVR_CORRES4

            if (Mdl_Funciones_Varias.LC_FCCFT != "FCCFT5")
            {
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 9)
                {
                    printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("//FW") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                    }

                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 8)
                {
                    if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_US_PAY_ID, 1, 1) == "/")
                    {
                        printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    }
                    else
                    {
                        printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("/") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    }

                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                    }

                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 4)
                {
                    printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim("//CP") + VB6Helpers.Trim(Mdl_Funciones_Varias.LC_US_PAY_ID));
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES1) > 0)
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                    }

                    //End If
                }
                else if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_US_PAY_ID) == 0)
                {
                    if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES1) > 0)
                    {
                        if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_RECVR_CORRES1, 1, 2) == "A:")
                        {
                            printer.PrintList(": 57", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                        }
                        else
                        {
                            printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                            if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES2) > 0)
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES2));
                            }

                        }

                    }

                }

            }
            else
            {
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES1) > 0)
                {
                    if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_RECVR_CORRES1, 1, 2) == "A:")
                    {
                        printer.PrintList(": 57", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                    }
                    else
                    {
                        printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES1));
                        if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_RECVR_CORRES2) > 0)
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_RECVR_CORRES2));
                        }

                    }

                }

            }

            //************* FIN Campo 57a *******************************

            //*********** CAMPO 58a de SWIFT **********************
            if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_BEN_INST1) > 0)
            {
                printer.PrintList(": 58D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "58D"), Printer.TAB(T_MODGMTS.TabD), "/", VB6Helpers.Trim(Mdl_Funciones_Varias.LC_BEN_INST1));
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_ULT_BEN1) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.LTrim(Mdl_Funciones_Varias.LC_ULT_BEN1));
                }

            }
            else
            {
                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_ULT_BEN1) > 0)
                {
                    if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_ULT_BEN1, 1, 2) == "A:")
                    {
                        printer.PrintList(": 58A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "58A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(VB6Helpers.Mid(Mdl_Funciones_Varias.LC_ULT_BEN1, 3)));
                    }
                    else
                    {
                        printer.PrintList(": 58D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "58D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_ULT_BEN1));
                        if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_ULT_BEN2) > 0)
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.LTrim(Mdl_Funciones_Varias.LC_ULT_BEN2));
                        }

                    }

                }

            }

            Pr_Print_Blancos(printer, 1);

            //************************************************************

            //*********** CAMPO 72 de SWIFT **********************

            if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1) > 0)
            {
                if (VB6Helpers.Mid(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1, 1, 3) == "18/")
                {
                    printer.PrintList(": 72", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "72"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim("/BNF/") + VB6Helpers.Trim(VB6Helpers.Mid(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1, 4)) + VB6Helpers.Space(35), 1, 35));
                }
                else
                {
                    printer.PrintList(": 72", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "72"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO2) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO2) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO3) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO3) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO4) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO4) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO5) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO5) + VB6Helpers.Space(35), 1, 35));
                }

                if (VB6Helpers.Len(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO6) > 0)
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Mid(VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO6) + VB6Helpers.Space(35), 1, 35));
                }

            }

            //*****************************

            Pr_Print_Blancos(printer, 1);
        }
        //****************************************************************************
        //   1.  Para la línea 59 (MT-100), si existe Cuenta Corriente se imprime en
        //       pantalla esta cuenta junto con los datos del Beneficiario, en caso
        //       contrario sólo se imprimen los datos del Beneficiario.
        //   2.  Para la línea 58 (TM-202), si existe Cuenta Corriente se imprime en
        //       pantalla esta cuenta junto con los datos del Beneficiario, en caso
        //       contrario sólo se imprimen los datos del Beneficiario.
        //****************************************************************************
        public static void Pr_Print_Cuenta202(Printer printer, T_Swf swift, IList<sce_tccs> descripciones, short MT)
        {

            if (MT == T_MODGSWF.MT_100)
            {
                if (!string.IsNullOrWhiteSpace(swift.DatSwf.ctacte))
                {
                    Pr_Print_Blancos(printer, 1);
                    printer.PrintList(": 59", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "59"), Printer.TAB(T_MODGMTS.TabD), "/" + VB6Helpers.Trim(swift.DatSwf.ctacte));
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.NomBen));
                }
                else
                {
                    Pr_Print_Blancos(printer, 1);
                    printer.PrintList(": 59", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "59"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.NomBen));
                }

                if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen1))
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen1));
                }
                if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen2))
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen2));
                }
                if (!string.IsNullOrWhiteSpace(swift.BenSwf.PaiBen_t))
                {
                    printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.PaiBen_t));
                }

            }
            else
            {
                if (MT == T_MODGSWF.MT_202)
                {
                    //En el caso del Swift <> BIC.
                    if (VB6Helpers.Mid(VB6Helpers.Trim(swift.BenSwf.SwfBen), 8) != "1")
                    {
                        Pr_Print_Blancos(printer, 1);
                        if (!string.IsNullOrWhiteSpace(swift.DatSwf.ctacte))
                        {
                            printer.PrintList(": 58A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "58A"), Printer.TAB(T_MODGMTS.TabD), @"/" + VB6Helpers.Trim(swift.DatSwf.ctacte));
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.SwfBen));
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.NomBen));
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen1))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen1));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen2))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen2));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.PaiBen_t))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.PaiBen_t));
                            }
                        }
                        else
                        {
                            printer.PrintList(": 58A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "58A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.SwfBen));
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen1))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen1));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen2))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen2));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.PaiBen_t))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.PaiBen_t));
                            }
                        }

                    }
                    else
                    {
                        //En el caso del Swift = BIC.
                        Pr_Print_Blancos(printer, 1);
                        if (!string.IsNullOrWhiteSpace(swift.DatSwf.ctacte))
                        {
                            printer.PrintList(": 58D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "58D"), Printer.TAB(T_MODGMTS.TabD), @"/" + VB6Helpers.Trim(swift.DatSwf.ctacte));
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.NomBen));
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen1))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen1));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen2))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen2));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.PaiBen_t))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.PaiBen_t));
                            }
                        }
                        else
                        {
                            printer.PrintList(": 58D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "58D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.NomBen));
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen1))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen1));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.DirBen2))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.DirBen2));
                            }
                            if (!string.IsNullOrWhiteSpace(swift.BenSwf.PaiBen_t))
                            {
                                printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BenSwf.PaiBen_t));
                            }
                        }

                    }

                }

            }

        }

        //****************************************************************************
        //   1.  Imprime en pantalla la línea 71 dependiendo del tipo de Gasto
        //       elegido por el usuario. Si en 'Sin Gastos' esta línea se omite.
        //****************************************************************************
        public static void Pr_Print_Gastos202(Printer printer, T_Swf swift, IList<sce_tccs> descripciones, short MT)
        {

            if (MT == T_MODGSWF.MT_100)
            {
                short _switchVar1 = swift.DatSwf.TipGas;
                if (_switchVar1 == 1)
                {
                    printer.PrintList(": 71A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "71A"), Printer.TAB(T_MODGMTS.TabD), "BEN");
                }
                else if (_switchVar1 == 2)
                {
                    printer.PrintList(": 71A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "71A"), Printer.TAB(T_MODGMTS.TabD), "OUR");
                }

            }

        }
        //****************************************************************************
        //   1.  Determina si existen Bancos Pagadores, Bancos Intermediarios para
        //       imprimir las líneas 56...(A-D) y 57..(A-D).
        //****************************************************************************
        public static void Pr_Print_Bco_PagInt202(Printer printer, T_Swf swift, IList<sce_tccs> descripciones, short MT)
        {

            //************************************
            //Para la Línea 56 (A-D).
            //Primero: en el caso de existir Bco Intermediario.
            //----------------------------------------
            if (!string.IsNullOrWhiteSpace(swift.BcoInt.NomBco))
            {
                if (!string.IsNullOrWhiteSpace(swift.BcoInt.SwfBco))
                {
                    //En el caso del Swift = BIC.
                    if (VB6Helpers.Mid(VB6Helpers.Trim(swift.BcoInt.SwfBco), 8, 3) == "1  " || (swift.BcoInt.IngMan == -1))
                    {
                        Pr_Print_Blancos(printer, 1);
                        printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.NomBco));
                        if (!string.IsNullOrWhiteSpace(swift.BcoInt.DirBco1))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.DirBco1));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoInt.DirBco2))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.DirBco2));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoInt.PaiBco))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.PaiBco));
                        }
                    }
                    else
                    {
                        //En el caso del Swift <> BIC.
                        Pr_Print_Blancos(printer, 1);
                        printer.PrintList(": 56A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.SwfBco));
                        if (!string.IsNullOrWhiteSpace(swift.BcoInt.DirBco1))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.NomBco));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoInt.DirBco1))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.DirBco1));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoInt.DirBco2))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.DirBco2));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoInt.PaiBco))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.PaiBco));
                        }
                    }

                }
                else
                {
                    Pr_Print_Blancos(printer, 1);
                    printer.PrintList(": 56D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "56D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.NomBco));
                    if (!string.IsNullOrWhiteSpace(swift.BcoInt.DirBco1))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.DirBco1));
                    }
                    if (!string.IsNullOrWhiteSpace(swift.BcoInt.DirBco2))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.DirBco2));
                    }
                    if (!string.IsNullOrWhiteSpace(swift.BcoInt.PaiBco))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoInt.PaiBco));
                    }
                }

            }

            //************************************
            //Para la Línea 57 (A-D).
            //Segundo: en el caso de existir Bco Pagador.
            if (!string.IsNullOrWhiteSpace(swift.BcoPag.NomBco))
            {
                if (!string.IsNullOrWhiteSpace(swift.BcoPag.SwfBco))
                {
                    //********************************.-
                    //Para ser D :   - Ingreso manual de datos.-
                    //               -   Swift terminado en BIC.-
                    //               -   Swift terminado en 1BlancoBlanco.-
                    if ((VB6Helpers.Mid(VB6Helpers.Trim(swift.BcoPag.SwfBco), 8, 3) == "1  ") || (swift.BcoPag.IngMan == -1))
                    {
                        Pr_Print_Blancos(printer, 1);
                        printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.NomBco));
                        if (!string.IsNullOrWhiteSpace(swift.BcoPag.DirBco1))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.DirBco1));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoPag.DirBco2))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.DirBco2));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoPag.PaiBco))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.PaiBco));
                        }
                    }
                    else
                    {
                        //En el caso del Swift <> BIC.
                        Pr_Print_Blancos(printer, 1);
                        printer.PrintList(": 57A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57A"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.SwfBco));
                        if (!string.IsNullOrWhiteSpace(swift.BcoPag.NomBco))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.NomBco));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoPag.DirBco1))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.DirBco1));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoPag.DirBco2))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.DirBco2));
                        }
                        if (!string.IsNullOrWhiteSpace(swift.BcoPag.PaiBco))
                        {
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.PaiBco));
                        }
                    }

                }
                else
                {
                    Pr_Print_Blancos(printer, 1);
                    printer.PrintList(": 57D", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "57D"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.NomBco));
                    if (!string.IsNullOrWhiteSpace(swift.BcoPag.DirBco1))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.DirBco1));
                    }
                    if (!string.IsNullOrWhiteSpace(swift.BcoPag.DirBco2))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.DirBco2));
                    }
                    if (!string.IsNullOrWhiteSpace(swift.BcoPag.PaiBco))
                    {
                        printer.PrintList(Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(swift.BcoPag.PaiBco));
                    }
                }

            }

        }

        //****************************************************************************
        //   1.  Imprime en pantalla el encabezado del Swift.
        //****************************************************************************
        public static void Pr_Print_Encabezado(Printer printer, T_MODGUSR MODGUSR, T_Swf swift, short MT)
        {

            Pr_Print_Bco_Aladi(printer, swift, 1);

            Pr_Print_Blancos(printer, 2);
            printer.PrintList("SENDER", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), MODGUSR.UsrEsp.Swift);

            printer.PrintList("MESSAGE TYPE", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(VB6Helpers.Str(MT)));

            Pr_Print_Bco_Aladi(printer, swift, 2);

            printer.PrintList("DATE ISSUE", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.UCase(DateTime.Now.ToString("dd/MM/yyyy")));
        }

        //****************************************************************************
        //   1.  Imprime en pantalla el encabezado del Swift.
        //****************************************************************************
        public static void Pr_Print_Encabezado_Auto(Printer printer, T_Mdl_Funciones_Varias Mdl_Funciones_Varias, short MT)
        {

            //Call Pr_Print_Bco_Aladi(1)

            printer.PrintList(Printer.TAB(27), " S W I F T ");
            Pr_Print_Blancos(printer, 2);

            printer.PrintList("SENDER", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), "CITICLRSXXX");

            printer.PrintList("MESSAGE TYPE", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(VB6Helpers.Str(MT)));

            printer.PrintList("RECEIVER", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.Trim(Mdl_Funciones_Varias.LC_SWFT));

            printer.PrintList("DATE ISSUE", Printer.TAB(T_MODGMTS.TabH), ":", Printer.TAB(T_MODGMTS.TabH + 2), VB6Helpers.UCase(DateTime.Now.ToString("dd/MM/yyyy")));
        }

        //****************************************************************************
        //   1.  Imprime en pantalla la línea 71 dependiendo del tipo de Gasto
        //       elegido por el usuario. Si en 'Sin Gastos' esta línea se omite.
        //****************************************************************************
        public static void Pr_Print_Gastos(Printer printer, T_Swf swift, T_mt103 montos, IList<sce_tccs> descripciones, short MT)
        {

            if (MT == T_MODGSWF.MT_103)
            {
                short _switchVar1 = swift.DatSwf.TipGas;
                if (_switchVar1 == 1)
                {
                    printer.PrintList(": 71A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "71A"), Printer.TAB(T_MODGMTS.TabD), "BEN");
                    printer.PrintList(": 71F", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "71F"), Printer.TAB(T_MODGMTS.TabD), VB6Helpers.Trim(MODGPYF0.forma(montos.GasEmi, T_MODGSWF.FormatoSwf)));
                }
                else if (_switchVar1 == 2)
                {
                    printer.PrintList(": 71A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "71A"), Printer.TAB(T_MODGMTS.TabD), "OUR");
                }
                else if (_switchVar1 == 3)
                {
                    printer.PrintList(": 71A", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "71A"), Printer.TAB(T_MODGMTS.TabD), "SHA");
                }

            }

        }

        //****************************************************************************
        //   1.  Imprime en pantalla la línea 70 dependiendo de la Referencia
        //       ingresada por el usuario. Si no hay referencia esta línea se omite.
        //****************************************************************************
        public static void Pr_Print_Referencia(Printer printer, T_Swf swift, List<UI_Message> MESSAGES, IList<sce_tccs> descripciones, short MT)
        {
            short i = 0;
            if (MT == T_MODGSWF.MT_103)
            {
                if (!string.IsNullOrWhiteSpace(swift.DatSwf.RefOpe))
                {
                    if (VB6Helpers.Instr(swift.DatSwf.RefOpe, "/RFB/") == 0)
                    {
                        swift.DatSwf.RefOpe = "/RFB/" + swift.DatSwf.RefOpe;
                    }
                    int nLineas = (swift.DatSwf.RefOpe.Length - 1) / 35 + 1;
                    for (i = 0; i < nLineas; i++)
                    {
                        string linea = VB6Helpers.Mid(swift.DatSwf.RefOpe, i * 35 + 1, 35);
                        if (i == 0)
                        {
                            printer.PrintList(": 70", Printer.TAB(6), ":", Printer.TAB(8), GetDescDeCampo(descripciones, MT, "70"), Printer.TAB(T_MODGMTS.TabD), linea);
                        }
                        else
                        {
                            printer.PrintList("    ", Printer.TAB(6), Printer.TAB(8), Printer.TAB(T_MODGMTS.TabD), linea);
                        }
                    }

                    Pr_Print_Blancos(printer, 1);
                }
            }
        }

        public static void Pr_Print_CamposManuales(Printer printer, IList<LineaMensajeSwift> lineasManuales)
        {
            if (lineasManuales != null && lineasManuales.Count > 0)
            {
                Pr_Print_Blancos(printer, 1);

                foreach (LineaMensajeSwift linea in lineasManuales)
                {
                    printer.PrintList(": " + linea.CodCam, Printer.TAB(6), ":", Printer.TAB(8), linea.Descripcion, Printer.TAB(T_MODGMTS.TabD), linea.Detalle);

                    foreach (LineaSecundariaMensajeSwift lineaS in linea.LineasSecundarias)
                    {
                        if (!String.IsNullOrEmpty(lineaS.Detalle))
                        {
                            if (linea.CodCam.Trim() == "72")
                            {
                                lineaS.Detalle = "//" + lineaS.Detalle.Replace("/", "");
                            }
                            printer.PrintList(Printer.TAB(T_MODGMTS.TabD), lineaS.Detalle);
                        }
                    }

                    Pr_Print_Blancos(printer, 1);
                }
            }
        }

    }
}
