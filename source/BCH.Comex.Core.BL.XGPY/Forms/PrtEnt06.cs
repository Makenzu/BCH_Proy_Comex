using BCH.Comex.Core.BL.XGPY.Modulos;
//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using CodeArchitects.VB6Library;
using System;
using System.Linq;
using BCH.Comex.Utils;
using System.Collections.Generic;

namespace BCH.Comex.Core.BL.XGPY.Forms
{
    public static class PrtEnt06
    {
        public static int enload = 0;
        public static int nomostrar = 0;
        //T_PRTGLOB.prtytcom rescom = null;
        //PRTGLOB.prtytgas[] resgas = null;
        //PRTGLOB.prtytint[] resint = null;

        public static void Form_Load(InitializationObject initObj)
        {
            int final = 0;
            int[] tabu = null;
            int[] tabs = null;

            int a = 0;
            int i = 0;
            string est = string.Empty;
            enload = 1;

            T_PRTGLOB.modifico_tasa = 0;
            initObj.PrtEnt06.lista.Clear(); //limpiar Lista Modulos

            tabs = new int[5];
            tabs[0] = 44;
            tabs[1] = 121;
            tabs[2] = 197;
            tabs[3] = 450;
            tabs[4] = 500;
            a = UTILES.seteaTabulador(initObj.PrtEnt06.lista_com, tabs);

            tabu = new int[4];
            tabu[0] = 60;
            tabu[1] = 170;
            tabu[2] = 300;
            tabu[3] = 450;
            a = UTILES.seteaTabulador(initObj.PrtEnt06.lista, tabu);

            initObj.PrtEnt06.Eliminar.Enabled = false;
            initObj.PrtEnt06.aceptar.Enabled = false;
            initObj.PrtEnt06.Agregar.Enabled = false;
            initObj.PrtEnt06.prtcomision.Enabled = false;
            prtcomision(initObj, false);
            initObj.PrtEnt06.prtinteres.Enabled = false;
            prtInteres(initObj, false);
            initObj.PrtEnt06.prtgasto.Enabled = false;
            prtGasto(initObj, false);
            initObj.PrtEnt06.tarifa.Enabled = false;
            initObj.PrtEnt06._prttipoi_[0].Enabled = false;
            initObj.PrtEnt06._prttipoi_[1].Enabled = false;
            initObj.PrtEnt06._prttipoi_[2].Enabled = false;
            initObj.PrtEnt06.prtflot.Enabled = false;

            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[0].Tag.ToStr(); //_menu_0.Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prod_[0].Tag.ToStr();//_prod_0.Tag.ToStr();
            T_PRTGLOB.etap = initObj.PrtEnt06._etacob_[0].Tag.ToStr(); //_etacob_0.Tag.ToStr();

            final = -1;
            final = initObj.PRTGLOB.tasacom.GetUpperBound(0);
            if (final != -1)
            {
                initObj.PRTGLOB.rescom = new prtytcom[initObj.PRTGLOB.tasacom.GetUpperBound(0) + 1];

                for (i = 0; i < initObj.PRTGLOB.tasacom.Count(); i += 1)
                    initObj.PRTGLOB.rescom[i] = initObj.PRTGLOB.tasacom[i];

                if (initObj.PRTGLOB.tasacom[0].sistema != "")
                {
                    for (i = 0; i <= initObj.PRTGLOB.tasacom.GetUpperBound(0); i += 1)
                    {
                        if (initObj.PRTGLOB.tasacom[i] != null) //Add el 25-02-2016
                        {
                            switch (initObj.PRTGLOB.tasacom[i].estado)
                            {
                                case T_PRTGLOB.leido:
                                    est = "Leído";
                                    break;
                                case T_PRTGLOB.nuevo:
                                    est = "Nuevo";
                                    break;
                                case T_PRTGLOB.modificado:
                                    est = "Modificado";
                                    break;
                                default:
                                    est = "Eliminado";
                                    break;
                            }
                            escribelista(initObj, initObj.PRTGLOB.tasacom[i].sistema, initObj.PRTGLOB.tasacom[i].producto, initObj.PRTGLOB.tasacom[i].etapa, ref est);
                        }
                    }
                }
            }

            final = -1;
            final = initObj.PRTGLOB.tasaint.GetUpperBound(0);
            if (final != -1)
            {
                initObj.PRTGLOB.resint = new prtytint[initObj.PRTGLOB.tasaint.GetUpperBound(0) + 1];
                for (i = 0; i <= initObj.PRTGLOB.tasaint.GetUpperBound(0); i += 1)
                    initObj.PRTGLOB.resint[i] = initObj.PRTGLOB.tasaint[i];

                if (initObj.PRTGLOB.tasaint[0].sistema != "")
                {
                    for (i = 0; i <= initObj.PRTGLOB.tasaint.GetUpperBound(0); i += 1)
                    {
                        switch (initObj.PRTGLOB.tasaint[i].estado)
                        {
                            case T_PRTGLOB.leido:
                                est = "Leído";
                                break;
                            case T_PRTGLOB.nuevo:
                                est = "Nuevo";
                                break;
                            case T_PRTGLOB.modificado:
                                est = "Modificado";
                                break;
                            default:
                                est = "Eliminado";
                                break;
                        }
                        escribelista(initObj, initObj.PRTGLOB.tasaint[i].sistema, initObj.PRTGLOB.tasaint[i].producto, initObj.PRTGLOB.tasaint[i].etapa, ref est);
                    }
                }
            }

            final = -1;
            final = initObj.PRTGLOB.tasagas.GetUpperBound(0);
            if (final != -1)
            {
                initObj.PRTGLOB.resgas = new prtytgas[initObj.PRTGLOB.tasagas.GetUpperBound(0) + 1];
                for (i = 0; i <= initObj.PRTGLOB.tasagas.GetUpperBound(0); i += 1)
                {
                    initObj.PRTGLOB.resgas[i] = initObj.PRTGLOB.tasagas[i];
                }

                if (initObj.PRTGLOB.tasagas[0].sistema != "")
                {
                    for (i = 0; i <= initObj.PRTGLOB.tasagas.GetUpperBound(0); i += 1)
                    {
                        switch (initObj.PRTGLOB.tasagas[i].estado)
                        {
                            case T_PRTGLOB.leido:
                                est = "Leído";
                                break;
                            case T_PRTGLOB.nuevo:
                                est = "Nuevo";
                                break;
                            case T_PRTGLOB.modificado:
                                est = "Modificado";
                                break;
                            default:
                                est = "Eliminado";
                                break;
                        }
                        escribelista(initObj, initObj.PRTGLOB.tasagas[i].sistema, initObj.PRTGLOB.tasagas[i].producto, initObj.PRTGLOB.tasagas[i].etapa, ref est);
                    }
                }
            }
            enload = 0;
            initObj.PrtEnt06.Titulo.Text = "Tasas Especiales";
        }
        public static void Aceptar_Click(InitializationObject initObj)
        {
            int com = 0;
            int inte = 0;
            int gas = 0;
            int i = 0;
            if (initObj.PRTGLOB.Party.PrtGlob.Pertenece == 0)
            {
                initObj.Mdi_Principal.Archivo[2].Enabled = false;     // menú salvar
                initObj.Mdi_Principal.Archivo[5].Enabled = false;     // menú eliminar
                initObj.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = false;      // boton grabar
            }
            else
            {
                initObj.Mdi_Principal.Archivo[2].Enabled = true;     // menú salvar
                initObj.Mdi_Principal.Archivo[5].Enabled = true;     // menú eliminar
                initObj.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = true;      // boton grabar
            }

            com = -1;
            com = initObj.PRTGLOB.rescom.GetUpperBound(0);

            inte = -1;
            inte = initObj.PRTGLOB.resint.GetUpperBound(0);

            gas = -1;
            gas = initObj.PRTGLOB.resgas.GetUpperBound(0);

            if (com >= 0)
            {
                initObj.PRTGLOB.tasacom = new prtytcom[com + 1];
                i = 0;
                //while (i <= initObj.PRTGLOB.rescom.GetUpperBound(0))
                for (i = 0; i <= initObj.PRTGLOB.rescom.GetUpperBound(0); i++)
                {
                    if (com == 0 && initObj.PRTGLOB.rescom[0].sistema == "")
                    {
                        break;
                    }
                    else
                    {
                        if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagTasas) == 0)
                        {
                            initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag + 2;
                            if (!Convert.ToBoolean(initObj.PRTGLOB.Party.PrtGlob.FlagParty))
                            {
                                initObj.PRTGLOB.Party.PrtGlob.FlagParty = 1;
                                if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                                {
                                    initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                                }
                            }
                        }
                        if (!Convert.ToBoolean(T_PRTGLOB.FlagComision))
                        {
                            T_PRTGLOB.FlagComision = 1;
                            if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                            {
                                initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                            }
                        }
                        initObj.PRTGLOB.tasacom[i] = initObj.PRTGLOB.rescom[i];
                    }
                }
            }

            if (com == -1)
                initObj.PRTGLOB.tasacom = new prtytcom[0];

            if (inte >= 0)
            {
                initObj.PRTGLOB.tasaint = new prtytint[inte + 1];
                i = 0;
                while (i <= initObj.PRTGLOB.resint.GetUpperBound(0))
                {
                    if (inte == 0 && initObj.PRTGLOB.resint[0].sistema == "")
                    {
                        break;
                    }
                    else
                    {
                        if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagTasas) == 0)
                        {
                            initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag + 2;
                            if (!Convert.ToBoolean(initObj.PRTGLOB.Party.PrtGlob.FlagParty))
                            {
                                initObj.PRTGLOB.Party.PrtGlob.FlagParty = 1;
                                if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                                {
                                    initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                                }
                            }
                        }
                        if (!Convert.ToBoolean(T_PRTGLOB.FlagInteres))
                        {
                            T_PRTGLOB.FlagInteres = 1;
                            if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                            {
                                initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                            }
                        }
                        initObj.PRTGLOB.tasaint[i] = initObj.PRTGLOB.resint[i];
                        i = i + 1;
                    }
                }
            }
            if (inte == -1)
                initObj.PRTGLOB.tasaint = new prtytint[0];
            if (gas >= 0)
            {
                initObj.PRTGLOB.tasagas = new prtytgas[gas + 1];
                i = 0;
                while (i <= initObj.PRTGLOB.resgas.GetUpperBound(0))
                {
                    if (gas == 0 && initObj.PRTGLOB.resgas[0].sistema == "")
                    {
                        break;
                    }
                    else
                    {
                        if ((initObj.PRTGLOB.Party.Flag & T_PRTGLOB.Gprt_FlagTasas) == 0)
                        {
                            initObj.PRTGLOB.Party.Flag = initObj.PRTGLOB.Party.Flag + 2;
                            if (!Convert.ToBoolean(initObj.PRTGLOB.Party.PrtGlob.FlagParty))
                            {
                                initObj.PRTGLOB.Party.PrtGlob.FlagParty = 1;
                                if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                                {
                                    initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                                }
                            }
                        }
                        if (!Convert.ToBoolean(T_PRTGLOB.FlagGasto))
                        {
                            T_PRTGLOB.FlagGasto = 1;
                            if (initObj.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                            {
                                initObj.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                            }

                        }
                        initObj.PRTGLOB.tasagas[i] = initObj.PRTGLOB.resgas[i];
                        i = i + 1;
                    }
                }
            }
            if (gas == -1)
                initObj.PRTGLOB.tasagas = new prtytgas[0];

            initObj.PRTGLOB.Party.estado = 5;

            initObj.PaginaWebQueAbrir = "Index";

        }
        public static void Agregar_Click(InitializationObject initObj)
        {
            int salir = 0;
            initObj.PrtEnt06.aceptar.Enabled = true;
            if (initObj.PrtEnt06.prtcomision.Enabled && T_PRTGLOB.modifico_tasa.ToBool())
            {
            }
            if (initObj.PrtEnt06.Agregar.Text == "Agregar")
            {
                respalda(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, 2, ref salir);
                if (salir != 0)
                    return;
                string argTemp1 = "Nuevo";
                escribelista(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, ref argTemp1);
            }
            else
            {
                respalda(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, 3, ref salir);
                if (salir != 0)
                    return;
                string argTemp2 = "Modificado";
                escribelista(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, ref argTemp2);
            }
            limpiacom(initObj);
            limpiagas(initObj);
            limpiaint(initObj);
            initObj.PrtEnt06.lista.ListIndex = -1;
            initObj.PrtEnt06.Titulo.Text = "Tasas Especiales";
            initObj.PrtEnt06.Agregar.Enabled = false;
            initObj.PrtEnt06.Eliminar.Enabled = false;
        }
        public static void Cancelar_Click(InitializationObject initObj)
        {
            initObj.PrtEnt06.lista_com.Clear();
            initObj.PrtEnt06.prttasint.Text = string.Empty;
            initObj.PrtEnt06._prttipoi_[0].Selected = false;
            initObj.PrtEnt06._prttipoi_[1].Selected = false;
            initObj.PrtEnt06._prttipoi_[2].Selected = false;
            initObj.PrtEnt06.prtflot.Checked = false;
            initObj.PrtEnt06.prtmongas.Text = string.Empty;
            initObj.PrtEnt06.tarifa.Checked = false;
            initObj.PrtEnt06.lista.Clear();

            initObj.PaginaWebQueAbrir = "Index";
        }
        public static void Eliminar_Click(InitializationObject initObj)
        {
            string hoy = string.Empty;
            string fecha = string.Empty;
            int i = 0;
            int salir = 0;

            for (i = 0; i <= initObj.PRTGLOB.rescom.GetUpperBound(0); i += 1)
            {
                fecha = VB6Helpers.Format(initObj.PRTGLOB.rescom[i].fecha, "yyyymmdd");
                hoy = VB6Helpers.Format(DateTime.Now, "yyyymmdd");
                if (String.CompareOrdinal(fecha, hoy) <= 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Alguna(s) Tasa(s) especial(es) está(n) vigente(s), por lo tanto es imposible eliminarlas.",
                        Title = T_PRTGLOB.TitTasas,
                        Type = TipoMensaje.Informacion
                    });
                    return;
                }
            }
            initObj.PrtEnt06.aceptar.Enabled = true;
            respalda(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, 4, ref salir);
            string argTemp1 = "Eliminado";
            escribelista(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, ref argTemp1);

            limpiacom(initObj);
            limpiagas(initObj);
            limpiaint(initObj);
            initObj.PrtEnt06.lista.ListIndex = -1;
            initObj.PrtEnt06.Titulo.Text = "Tasas Especiales";
            initObj.PrtEnt06.Agregar.Enabled = false;
            initObj.PrtEnt06.Eliminar.Enabled = false;
        }
        public static void escribecom(InitializationObject initObj, string sist, string prod, string etapa)
        {
            int t = 0;
            string max = string.Empty;
            string min = string.Empty;
            string l = string.Empty;
            string hoy = string.Empty;
            string fecha = string.Empty;
            int final = 0;
            int i = 0;
            int esta = 0;
            T_PRTGLOB.modifico_tasa = 0;
            i = 0;
            initObj.PrtEnt06.prtcomision.Enabled = true;
            prtcomision(initObj, true); //Add por JF
            initObj.PrtEnt06.lista_com.Enabled = true;
            initObj.PrtEnt06.lista_com.Items.Clear();
            initObj.PrtEnt06.lista_com.ListIndex = 0; //Add el 02-02-2016          
            esta = ((false) ? -1 : 0);
            final = -1;
            final = initObj.PRTGLOB.rescom.GetUpperBound(0);
            while (i <= final)
            {
                if (initObj.PRTGLOB.rescom[i].sistema.ToUpper() == sist.ToUpper() && initObj.PRTGLOB.rescom[i].producto.ToUpper() == prod.ToUpper() && initObj.PRTGLOB.rescom[i].etapa.ToUpper() == etapa.ToUpper())
                {
                    esta = ((true) ? -1 : 0);
                    fecha = VB6Helpers.Format(initObj.PRTGLOB.rescom[i].fecha, "yyyymmdd");
                    hoy = VB6Helpers.Format(DateTime.Now, "yyyymmdd");
                    if (initObj.PRTGLOB.rescom[i].manual != 0)
                    {
                        l = "Datos del Manual de Tarifas a partir de (" + initObj.PRTGLOB.rescom[i].fecha + ")";

                        initObj.PrtEnt06.lista_com.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                        {
                            Value = l,
                            Data = i,
                            ID = i.ToString()
                        });
                    }
                    else
                    {
                        if (initObj.PRTGLOB.rescom[i].mto_fijo != 0)
                        {
                            l = T_PRTGLOB.guiones_tasa + VB6Helpers.Chr(9);
                            if (Format.FormatCurrency(initObj.PRTGLOB.rescom[i].hasta, "0.00") == "9999999999999,99")
                                l = l + T_PRTGLOB.guiones + VB6Helpers.Chr(9);
                            else

                                l = l + UTILES.EspaciosAlineado(Format.FormatCurrency(initObj.PRTGLOB.rescom[i].hasta, T_PRTGLOB.formato), 55) + VB6Helpers.Chr(9);

                            l = l + UTILES.EspaciosAlineado(Format.FormatCurrency(initObj.PRTGLOB.rescom[i].min, T_PRTGLOB.formato), 75) + VB6Helpers.Chr(9);
                            l = l + UTILES.EspaciosAlineado(Format.FormatCurrency(initObj.PRTGLOB.rescom[i].max, T_PRTGLOB.formato), 75);// + VB6Helpers.Chr(9);
                            //l = l + initObj.PRTGLOB.rescom[i].fecha.PadLeft(40, ' ').Replace(" ", "&nbsp;") + VB6Helpers.Chr(9) + "1";

                            //Comentariado el 29-02-2016 -->
                            initObj.PrtEnt06.lista_com.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                            {
                                Value = l,
                                Data = i,
                                Tag = initObj.PRTGLOB.rescom[i].fecha + VB6Helpers.Chr(9) + "1",
                                ID = i.ToString()
                            });
                        }
                        else
                        {
                            l = UTILES.EspaciosAlineado(Format.FormatCurrency(initObj.PRTGLOB.rescom[i].tasa, T_PRTGLOB.formato_tasa), 75) + VB6Helpers.Chr(9);
                            if (Format.FormatCurrency(initObj.PRTGLOB.rescom[i].hasta, "0.00") == "9999999999999,99")
                                l = l + T_PRTGLOB.guiones + VB6Helpers.Chr(9);
                            else
                                l = l + UTILES.EspaciosAlineado(Format.FormatCurrency(initObj.PRTGLOB.rescom[i].hasta, T_PRTGLOB.formato), 55) + VB6Helpers.Chr(9);

                            if (initObj.PRTGLOB.rescom[i].tasa != 0)
                            {
                                if (initObj.PRTGLOB.rescom[i].min == 0)
                                    min = T_PRTGLOB.guiones;
                                else
                                    min = UTILES.EspaciosAlineado(Format.FormatCurrency(initObj.PRTGLOB.rescom[i].min, T_PRTGLOB.formato), 75);

                                if (Format.FormatCurrency(initObj.PRTGLOB.rescom[i].max, "0.00") == "9999999999999,99")
                                    max = T_PRTGLOB.guiones;
                                else
                                    max = UTILES.EspaciosAlineado(Format.FormatCurrency(initObj.PRTGLOB.rescom[i].max, T_PRTGLOB.formato), 75);
                            }
                            else
                            {
                                min = T_PRTGLOB.guiones;
                                max = T_PRTGLOB.guiones;
                            }
                            l = l + min + VB6Helpers.Chr(9) + max;
                            //Comentariado el 29-02-2016 -->
                            initObj.PrtEnt06.lista_com.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                            {
                                Value = l,
                                Data = i,
                                Tag = initObj.PRTGLOB.rescom[i].fecha + VB6Helpers.Chr(9) + "0",
                                ID = i.ToString()
                            });
                        }
                    }

                    initObj.PrtEnt06.Agregar.Text = "Modificar";
                    initObj.PrtEnt06.Eliminar.Enabled = true;
                    if (initObj.PRTGLOB.rescom[i].manual == 0)
                    {
                        t = initObj.PrtEnt06.lista_com.Items.Count - 1;
                        if (String.CompareOrdinal(fecha, hoy) < 0)
                        {
                            //lista_com.SetItemData(t, true);
                            initObj.PrtEnt06.lista_com.AddItem(t, "1");
                        }
                    }
                }
                i = i + 1;
            }
            if (esta == 0)
            {
                if (T_PRTGLOB.delista != 0)
                {
                    initObj.PrtEnt06.prtcomision.Enabled = false;
                    prtcomision(initObj, false);
                    initObj.PrtEnt06.lista_com.Enabled = false;
                }
                else
                    initObj.PrtEnt06.lista_com.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { ID = initObj.PrtEnt06.lista_com.Items.Count.ToString(), Tag = "", Data = initObj.PrtEnt06.lista_com.Items.Count, Value = "" });
            }
            else
                initObj.PrtEnt06.lista_com.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { ID = initObj.PrtEnt06.lista_com.Items.Count.ToString(), Tag = "", Data = initObj.PrtEnt06.lista_com.Items.Count, Value = "" });
        }
        public static void prtcomision(InitializationObject initObj, bool pEsHabilitado)
        {
            initObj.PrtEnt06.lista_com.Enabled = pEsHabilitado;
        }
        public static void prtInteres(InitializationObject initObj, bool pEsHabilitado)
        {

            initObj.PrtEnt06.prttasint.Enabled = pEsHabilitado;
            initObj.PrtEnt06._prttipoi_[0].Enabled = pEsHabilitado;
            initObj.PrtEnt06._prttipoi_[1].Enabled = pEsHabilitado;
            initObj.PrtEnt06._prttipoi_[2].Enabled = pEsHabilitado;
            initObj.PrtEnt06.prtflot.Enabled = pEsHabilitado;
        }
        public static void prtGasto(InitializationObject initObj, bool pEsHabilitado)
        {
            initObj.PrtEnt06.prtmongas.Enabled = pEsHabilitado;
            initObj.PrtEnt06.tarifa.Enabled = pEsHabilitado;
        }
        public static void escribegas(InitializationObject initObj, string sist, string prod, string etapa)
        {
            int final = 0;
            bool esta = false;
            int i = 0;
            T_PRTGLOB.modifico_tasa = 0;
            i = 0;
            esta = false;
            initObj.PrtEnt06.tarifa.Enabled = true;
            initObj.PrtEnt06.prtgasto.Enabled = true;
            prtGasto(initObj, true); //Add por JF 
            final = -1;
            final = initObj.PRTGLOB.resgas.GetUpperBound(0);
            while (i <= final && !esta)
            {
                if (initObj.PRTGLOB.resgas[i].sistema.UCase() == sist.UCase() && initObj.PRTGLOB.resgas[i].producto.UCase() == prod.UCase() && initObj.PRTGLOB.resgas[i].etapa.UCase() == etapa.UCase())
                {
                    esta = true;
                    break;
                }
                else
                    i = i + 1;
            }
            if (esta)
            {
                if (initObj.PRTGLOB.resgas[i].tarifa != 0)
                {
                    initObj.PrtEnt06.prtmongas.Text = Format.FormatCurrency(initObj.PRTGLOB.resgas[i].monto, "0.00");
                    initObj.PrtEnt06.tarifa.Checked = true;
                }
                else
                {
                    initObj.PrtEnt06.tarifa.Checked = false;
                    initObj.PrtEnt06.prtmongas.Text = Format.FormatCurrency(initObj.PRTGLOB.resgas[i].monto, "0.00");
                }
                initObj.PrtEnt06.Agregar.Text = "Modificar";
                initObj.PrtEnt06.Eliminar.Enabled = true;
            }
            else
            {
                initObj.PrtEnt06.tarifa.Checked = true;
                initObj.PrtEnt06.prtmongas.Text = string.Empty;
                if (T_PRTGLOB.delista.ToBool() && !nomostrar.ToBool())
                {
                    initObj.PrtEnt06.tarifa.Checked = false;
                    initObj.PrtEnt06.prtgasto.Enabled = false;
                    prtGasto(initObj, false); //Add por JF 
                    initObj.PrtEnt06.tarifa.Enabled = false;
                }
            }
        }
        public static void escribeint(InitializationObject initObj, string sist, string prod, string etapa)
        {
            int final = 0;
            bool esta = false;
            int i = 0;
            T_PRTGLOB.modifico_tasa = 0;
            i = 0;
            esta = false;
            initObj.PrtEnt06.prtinteres.Enabled = true;
            prtInteres(initObj, true);
            initObj.PrtEnt06._prttipoi_[0].Enabled = true;
            initObj.PrtEnt06._prttipoi_[1].Enabled = true;
            initObj.PrtEnt06._prttipoi_[2].Enabled = true;
            initObj.PrtEnt06.prtflot.Enabled = true;

            final = -1;
            final = initObj.PRTGLOB.resint.GetUpperBound(0);
            while (i <= final && !esta)
            {
                if (initObj.PRTGLOB.resint[i].sistema.UCase() == sist.UCase() && initObj.PRTGLOB.resint[i].producto.UCase() == prod.UCase() && initObj.PRTGLOB.resint[i].etapa.UCase() == etapa.UCase())
                {
                    esta = true;
                    break;
                }
                else
                {
                    i = i + 1;
                }
            }
            if (esta)
            {
                initObj.PrtEnt06.prttasint.Text = Format.FormatCurrency(initObj.PRTGLOB.resint[i].tasa, "0.000000");
                initObj.PrtEnt06._prttipoi_[0].Selected = initObj.PRTGLOB.resint[i].libor.ToBool();
                initObj.PrtEnt06._prttipoi_[1].Selected = initObj.PRTGLOB.resint[i].prime.ToBool();
                initObj.PrtEnt06._prttipoi_[2].Selected = (initObj.PRTGLOB.resint[i].prime.ToInt()) + (initObj.PRTGLOB.resint[i].libor.ToInt()) == 0 ? true : false; // (~initObj.PRTGLOB.resint[i].prime.ToInt()).ToStr() + (~initObj.PRTGLOB.resint[i].libor.ToInt()).ToStr();//(~initObj.PRTGLOB.resint[i].prime.ToStr()) + (~initObj.PRTGLOB.resint[i].libor.ToInt()).ToStr();

                if (initObj.PrtEnt06._prttipoi_[2].Selected.ToBool())
                {
                    initObj.PrtEnt06.prtflot.Checked = false;
                    initObj.PrtEnt06.prtflot.Enabled = false;
                }
                else
                {
                    initObj.PrtEnt06.prtflot.Enabled = true;
                    initObj.PrtEnt06.prtflot.Checked = initObj.PRTGLOB.resint[i].flotante.ToBool();
                }
                initObj.PrtEnt06.Agregar.Text = "Modificar";
                initObj.PrtEnt06.Eliminar.Enabled = true;
            }
            else
            {
                initObj.PrtEnt06.prttasint.Text = string.Empty;
                initObj.PrtEnt06._prttipoi_[0].Selected = false; // false  radios permite que al menos uno este seleccionado.
                initObj.PrtEnt06._prttipoi_[1].Selected = false;
                initObj.PrtEnt06._prttipoi_[2].Selected = false;
                initObj.PrtEnt06.prtflot.Checked = false;
                if (T_PRTGLOB.delista != 0)
                {
                    initObj.PrtEnt06.prtinteres.Enabled = false;
                    prtInteres(initObj, false);
                    initObj.PrtEnt06._prttipoi_[0].Enabled = false;
                    initObj.PrtEnt06._prttipoi_[1].Enabled = false;
                    initObj.PrtEnt06._prttipoi_[2].Enabled = false;
                    initObj.PrtEnt06.prtflot.Enabled = false;
                }
            }
        }
        public static void escribelista(InitializationObject initObj, string sist, string prod, string etapa, ref string estado)
        {
            string spe = string.Empty;
            int ind = 0;
            string l = string.Empty;
            bool encontrado = false;
            string linea = string.Empty;

            int i = 0;

            #region "MENU"
            string E = string.Empty;
            string P = string.Empty;
            string s = string.Empty;
            int corS = 0;
            int corP = 0;
            int corE = 0;
            int espaciosEnBlanco = 0;

            for (corS = 0; corS <= initObj.PrtEnt06._menu_.Count - 1; corS++) //MENU
            {
                if (initObj.PrtEnt06._menu_[corS].Tag.ToStr() == sist)
                {
                    s = initObj.PrtEnt06._menu_[i].Text;
                }
            }

            for (corP = 0; corP <= initObj.PrtEnt06._prod_.Count - 1; corP++) //Producto Importaciones
            {
                if (initObj.PrtEnt06._prod_[corP].Tag.ToStr() == prod)
                {
                    P = initObj.PrtEnt06._prod_[corP].Text;
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etacob_.Count - 1; corE++) //Etapa Cobranzas Importaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etacob_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etacob_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etacar_.Count - 1; corE++) //Etapa Carta Credito Importaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etacar_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etacar_[corE].Text;
                    }
                }
            }

            for (corP = 0; corP <= initObj.PrtEnt06._prodexp_.Count - 1; corP++) //Producto Exportaciones
            {
                if (initObj.PrtEnt06._prodexp_[corP].Tag.ToStr() == prod)
                {
                    P = initObj.PrtEnt06._prodexp_[corP].Text;
                }
            }
            if (initObj.PrtEnt06._etapae_0.Tag.ToStr() == etapa) //Etapa Pae Exportaciones
            {
                if (etapa != "SIN")
                {
                    E = initObj.PrtEnt06._etapae_0.Text;
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etacom_.Count - 1; corE++) //Etapa Compra Documentos Exportaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etacom_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etacom_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etades_.Count - 1; corE++) //Etapa Descuento Documentos Exportaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etades_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etades_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etacred_.Count - 1; corE++) //Etapa Carta Credito Exportaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etacred_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etacred_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etapa_.Count - 1; corE++) //Etapa Cobranzas Exportaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etapa_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etapa_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etaret_.Count - 1; corE++) //Etapa Retorno Exportaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etaret_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etaret_[corE].Text;
                    }
                }
            }

            for (corP = 0; corP <= initObj.PrtEnt06._prodser_.Count - 1; corP++) //Producto Servicios
            {
                if (initObj.PrtEnt06._prodser_[corP].Tag.ToStr() == prod)
                {
                    P = initObj.PrtEnt06._prodser_[corP].Text;
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etafin_.Count - 1; corE++) //Etapa Ex Financiamiento
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etafin_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etafin_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etaor_.Count - 1; corE++) //Etapa Orden de Pago Condicionado Financiamiento
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etaor_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etaor_[corE].Text;
                    }
                }
            }

            #endregion

            s = PRTYENT.saca(s, "&");
            P = PRTYENT.saca(P, "&");
            if (etapa != "SIN")
            {
                E = PRTYENT.saca(E, "&");
                //espaciosEnBlanco = 55;
            }
            else
            {
                E = string.Empty;
                //espaciosEnBlanco = 110;
            }
            linea = UTILES.EspaciosAlineado(s, 50) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(P, 100) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(E, 100);

            encontrado = false;
            for (i = 0; i <= initObj.PrtEnt06.lista.Items.Count - 1; i += 1)
            {
                l = initObj.PrtEnt06.lista.Items[i].Value;
                s = UTILES.copiardestring(l, VB6Helpers.Chr(9), 1);
                P = UTILES.copiardestring(l, VB6Helpers.Chr(9), 2);
                E = UTILES.copiardestring(l, VB6Helpers.Chr(9), 3);
                l = s + VB6Helpers.Chr(9) + P + VB6Helpers.Chr(9) + E;
                if (UTILES.QuitaEspaciosEnBlanco(linea) == UTILES.QuitaEspaciosEnBlanco(l))
                {
                    encontrado = true;
                    ind = i;
                    break;
                }
            }
            spe = sist + " " + prod + " " + etapa;
            if (!encontrado)
            {
                //linea = linea + VB6Helpers.Chr(9) + estado + VB6Helpers.Chr(9) + spe;
                linea = linea + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(estado,50);
                if (enload != 0)
                    initObj.PrtEnt06.lista.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = initObj.PrtEnt06.lista.Items.Count, Tag = spe, ID = T_PRTGLOB.leido.ToStr(), Value = linea });
                else
                    initObj.PrtEnt06.lista.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = initObj.PrtEnt06.lista.Items.Count, Tag = spe, ID = T_PRTGLOB.nuevo.ToStr(), Value = linea });


            }
            else
            {
                switch (estado)
                {
                    case "Modificado":
                        if (initObj.PrtEnt06.lista.Items[ind].ID.ToInt() == T_PRTGLOB.leido)
                        {
                            //initObj.PrtEnt06.lista.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = T_PRTGLOB.modificado, Value = linea });
                            initObj.PrtEnt06.lista.Items[ind].ID = T_PRTGLOB.modificado.ToStr();
                        }
                        if (initObj.PrtEnt06.lista.Items[ind].ID.ToInt() == T_PRTGLOB.nuevo)
                            estado = "Nuevo";
                        break;
                    case "Eliminado":
                        switch (initObj.PrtEnt06.lista.Items[ind].ID.ToInt())
                        {
                            case T_PRTGLOB.leido:
                                // initObj.PrtEnt06.lista.SetItemData(ind, T_PRTGLOB.eliminado_leido);                                
                                initObj.PrtEnt06.lista.Items[ind].ID = T_PRTGLOB.eliminado_leido.ToStr();
                                break;
                            case T_PRTGLOB.nuevo:
                                initObj.PrtEnt06.lista.Items[ind].ID = T_PRTGLOB.eliminado_nuevo.ToStr();
                                break;
                            case T_PRTGLOB.modificado:
                                initObj.PrtEnt06.lista.Items[ind].ID = T_PRTGLOB.eliminado_modificado.ToStr();
                                break;
                        }
                        break;
                }
                //linea = linea + 9.Char() + estado + 9.Char() + spe;
                linea = linea + 9.Char() + estado;
                initObj.PrtEnt06.lista.Items[ind].Value = linea;
                initObj.PrtEnt06.lista.Items[ind].Tag = spe;
            }
        }
        public static void escribetitulo(InitializationObject initObj, string sist, string prod, string etapa)
        {
            string linea = string.Empty;
            int i = 0;
            #region "MENU"
            string E = string.Empty;
            string P = string.Empty;
            string s = string.Empty;
            int corS = 0;
            int corP = 0;
            int corE = 0;

            for (corS = 0; corS <= initObj.PrtEnt06._menu_.Count - 1; corS++) //MENU
            {
                if (initObj.PrtEnt06._menu_[corS].Tag.ToStr() == sist)
                {
                    s = initObj.PrtEnt06._menu_[corS].Text;
                }
            }

            for (corP = 0; corP <= initObj.PrtEnt06._prod_.Count - 1; corP++) //Producto Importaciones
            {
                if (initObj.PrtEnt06._prod_[corP].Tag.ToStr() == prod)
                {
                    P = initObj.PrtEnt06._prod_[corP].Text;
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etacob_.Count - 1; corE++) //Etapa Cobranzas Importaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etacob_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etacob_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etacar_.Count - 1; corE++) //Etapa Carta Credito Importaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etacar_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etacar_[corE].Text;
                    }
                }
            }

            for (corP = 0; corP <= initObj.PrtEnt06._prodexp_.Count - 1; corP++) //Producto Exportaciones
            {
                if (initObj.PrtEnt06._prodexp_[corP].Tag.ToStr() == prod)
                {
                    P = initObj.PrtEnt06._prodexp_[corP].Text;
                }
            }
            if (initObj.PrtEnt06._etapae_0.Tag.ToStr() == etapa) //Etapa Pae Exportaciones
            {
                if (etapa != "SIN")
                {
                    E = initObj.PrtEnt06._etapae_0.Text;
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etacom_.Count - 1; corE++) //Etapa Compra Documentos Exportaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etacom_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etacom_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etades_.Count - 1; corE++) //Etapa Descuento Documentos Exportaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etades_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etades_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etacred_.Count - 1; corE++) //Etapa Carta Credito Exportaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etacred_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etacred_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etapa_.Count - 1; corE++) //Etapa Cobranzas Exportaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etapa_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etapa_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etaret_.Count - 1; corE++) //Etapa Retorno Exportaciones
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etaret_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etaret_[corE].Text;
                    }
                }
            }

            for (corP = 0; corP <= initObj.PrtEnt06._prodser_.Count - 1; corP++) //Producto Servicios
            {
                if (initObj.PrtEnt06._prodser_[corP].Tag.ToStr() == prod)
                {
                    P = initObj.PrtEnt06._prodser_[corP].Text;
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etafin_.Count - 1; corE++) //Etapa Ex Financiamiento
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etafin_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etafin_[corE].Text;
                    }
                }
            }
            for (corE = 0; corE <= initObj.PrtEnt06._etaor_.Count - 1; corE++) //Etapa Orden de Pago Condicionado Financiamiento
            {
                if (etapa != "SIN")
                {
                    if (initObj.PrtEnt06._etaor_[corE].Tag.ToStr() == etapa)
                    {
                        E = initObj.PrtEnt06._etaor_[corE].Text;
                    }
                }
            }

            #endregion

            s = PRTYENT.saca(s, "&");
            P = PRTYENT.saca(P, "&");
            if (etapa != "SIN")
            {
                E = PRTYENT.saca(E, "&");
                linea = s + " - " + P + " - " + E;
            }
            else
            {
                E = string.Empty;
                linea = s + " - " + P;
            }
            initObj.PrtEnt06.Titulo.Text = linea;
        }
        public static int esta_eliminado(InitializationObject initObj, string sist, string prod, string etapa)
        {
            int esta_eliminado = 0;

            int i = 0;
            int final = 0;
            bool esta = false;

            esta = false;
            final = -1;

            final = initObj.PRTGLOB.resgas.GetUpperBound(0);
            while (i <= final && !esta)
            {
                if (initObj.PRTGLOB.resgas[i].sistema.UCase() == sist.UCase() && initObj.PRTGLOB.resgas[i].producto.UCase() == prod.UCase() && initObj.PRTGLOB.resgas[i].etapa.UCase() == etapa.UCase())
                {
                    esta = true;
                    break;
                }
                else
                    i = i + 1;
            }

            if (esta)
            {
                if (initObj.PRTGLOB.resgas[i].estado == T_PRTGLOB.eliminado_leido)
                    esta_eliminado = true.ToInt();
            }
            return esta_eliminado;
        }

        //Importaciones -- Carta Credito  
        public static void etacar_Click(InitializationObject initObj, int index)
        {
            string etacar = initObj.PrtEnt06._etacar_[index].Tag.ToString();
            int a = 0;

            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = false.ToInt();
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[0].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prod_[1].Tag.ToStr();
            T_PRTGLOB.etap = etacar;
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;
            a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            if (a != 0)
            {
                initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
                return;
            }

            if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
            {
                switch (index)
                {
                    case 0:
                        escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        break;
                    case 1:
                    case 2:
                        escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        limpiaint(initObj);
                        escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        break;
                    case 3:
                        limpiacom(initObj);
                        escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);

                        break;
                    case 4:
                    case 5:
                        limpiacom(initObj);
                        escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        limpiagas(initObj);
                        escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        break;
                    case 6:
                        escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        limpiaint(initObj);
                        escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        break;
                }
            }
        }

        public static void etacar_Si_Click(InitializationObject initObj, int index)
        {
            string etacar = initObj.PrtEnt06._etacar_[index].Tag.ToString();
            int a = 0;

            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = false.ToInt();
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[0].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prod_[1].Tag.ToStr();
            T_PRTGLOB.etap = etacar;

            switch (index)
            {
                case 0:
                    escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    break;
                case 1:
                case 2:
                    escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    limpiaint(initObj);
                    escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    break;
                case 3:
                    limpiacom(initObj);
                    escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);

                    break;
                case 4:
                case 5:
                    limpiacom(initObj);
                    escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    limpiagas(initObj);
                    escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    break;
                case 6:
                    escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    limpiaint(initObj);
                    escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    break;
            }
        }

        //Importaciones --> Cobranzas
        public static void etacob_Click(InitializationObject initObj, int index)
        {
            string Etacob = initObj.PrtEnt06._etacob_[index].Tag.ToString();
            int a = 0;
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = 0;
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[0].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prod_[0].Tag.ToStr();
            T_PRTGLOB.etap = Etacob;
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;

            a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            if (a != 0)
            {
                initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
                return;
            }
            if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
            {
                escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                limpiaint(initObj);
                escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            }
        }

        public static void etacob_Si_Click(InitializationObject initObj, int index)
        {
            string Etacob = initObj.PrtEnt06._etacob_[index].Tag.ToString();
            int a = 0;

            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = 0;
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[0].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prod_[0].Tag.ToStr();
            T_PRTGLOB.etap = Etacob;

            escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            limpiaint(initObj);
            escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);

        }


        public static void etacom_Click(InitializationObject initObj, int index)
        {
            //System.Windows.Forms.ToolStripMenuItem etacom;          
            string etacom = initObj.PrtEnt06._etacom_[index].Tag.ToString();
            int a = 0;
            string msj = string.Empty;
            System.Windows.Forms.DialogResult resp = 0;
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = 0;
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[1].Tag.ToStr();
            T_PRTGLOB.etap = etacom;
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;

            a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            if (a != 0)
            {
                initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
                return;
            }

            if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
            {
                escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                limpiagas(initObj);
                escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            }
        }

        public static void etacom_Si_Click(InitializationObject initObj, int index)
        {

            string etacom = initObj.PrtEnt06._etacom_[index].Tag.ToString();
            int a = 0;
            string msj = string.Empty;
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = 0;
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[1].Tag.ToStr();
            T_PRTGLOB.etap = etacom;

            escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            limpiagas(initObj);
            escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
        }

        public static void etacred_Click(InitializationObject initObj, int index)
        {
            string etacred = initObj.PrtEnt06._etacred_[index].Tag.ToStr();
            int a = 0;
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = 0;
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[3].Tag.ToStr();
            T_PRTGLOB.etap = etacred;
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;
            a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            if (a != 0)
            {               
                initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
                return;             
            }
            if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
            {
                escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                limpiaint(initObj);
                escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            }

        }


        public static void etacred_Si_Click(InitializationObject initObj, int index)
        {
            string etacred = initObj.PrtEnt06._etacred_[index].Tag.ToStr();
            int a = 0;
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = 0;
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[3].Tag.ToStr();
            T_PRTGLOB.etap = etacred;
            escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            limpiaint(initObj);
            escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);

        }






        public static void etades_Click(InitializationObject initObj, int index)
        {              
            string etades = initObj.PrtEnt06._etades_[index].Tag.ToStr();
            int a = 0;           
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;
            T_PRTGLOB.delista = 0;
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[2].Tag.ToStr();
            T_PRTGLOB.etap = etades;
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;
            a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            if (a != 0)
            {
                initObj.PrtEnt06.idEstadoMsjeExportacion = 1;              
                return;            
            }
            if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
            {
                escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                limpiaint(initObj);
                limpiagas(initObj);
                escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            }
        }

        public static void etades_Si_Click(InitializationObject initObj, int index)
        {
            string etades = initObj.PrtEnt06._etades_[index].Tag.ToStr();
            int a = 0;

            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = 0;
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[2].Tag.ToStr();
            T_PRTGLOB.etap = etades;

            escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            limpiaint(initObj);
            limpiagas(initObj);
            escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
        }

        public static void etafin_Click(InitializationObject initObj, int index)
        {
            string etafin = initObj.PrtEnt06._etafin_[index].Tag.ToStr();
            int a = 0;

            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;
            T_PRTGLOB.delista = false.ToInt();
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[2].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodser_[3].Tag.ToStr();
            T_PRTGLOB.etap = etafin;
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;
            a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            if (a != 0)
            {
                initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
                return;
            }
            if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
            {
                escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                limpiaint(initObj);
                limpiagas(initObj);
                escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            }
        }

        public static void etafin_Si_Click(InitializationObject initObj, int index)
        {
            string etafin = initObj.PrtEnt06._etafin_[index].Tag.ToStr();

            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;
            T_PRTGLOB.delista = false.ToInt();
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[2].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodser_[3].Tag.ToStr();
            T_PRTGLOB.etap = etafin;

            escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            limpiaint(initObj);
            limpiagas(initObj);
            escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
        }

        public static void etaor_Click(InitializationObject initObj, int index)
        {
            string etaor = initObj.PrtEnt06._etaor_[index].Tag.ToStr();
            int a = 0;

            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = 0;
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[2].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodser_[5].Tag.ToStr();
            T_PRTGLOB.etap = etaor;
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;
            a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            if (a != 0)
            {              
                initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
                return;
            }
            if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
            {
                escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                limpiaint(initObj);
                limpiagas(initObj);
                escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            }
        }

        public static void etaor_Si_Click(InitializationObject initObj, int index)
        {         
            string etaor = initObj.PrtEnt06._etaor_[index].Tag.ToStr();
            int a = 0;
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = false.ToInt();
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[2].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodser_[5].Tag.ToStr();
            T_PRTGLOB.etap = etaor;

            escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            limpiaint(initObj);
            limpiagas(initObj);
            escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
        }



        public static void etapa_Click(InitializationObject initObj, int index)
        {
            string etapa = initObj.PrtEnt06._etapa_[index].Tag.ToStr();
            int a = 0;
            string msj = string.Empty;
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = false.ToInt();
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[5].Tag.ToStr();
            T_PRTGLOB.etap = etapa;
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;
            a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            if (a != 0)
            {
                //msj = "Tasa Especial en proceso de Borrado." + 10.Char() + "Si desea reactivar la Tasa Especial" + 10.Char() + "debería elegir SI y podrá acceder a toda su información." + 10.Char() + "Si elige NO volverá a la lista de Tasas Especiales.";
                //resp = MigrationSupport.Utils.MsgBox(msj, MigrationSupport.MsgBoxStyle.YesNo, T_PRTGLOB.TitTasas);
                //if (resp == System.Windows.Forms.DialogResult.No)
                //{
                //    return;
                //}
                initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
                return;
            }

            if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
            {
                if (index == 0)
                {
                    escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    limpiaint(initObj);
                    escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                }
                else
                {
                    limpiacom(initObj);
                    limpiaint(initObj);
                    escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                }
            }
        }

        public static void etapa_Si_Click(InitializationObject initObj, int index)
        {
            string etapa = initObj.PrtEnt06._etapa_[index].Tag.ToStr();
            int a = 0;
            System.Windows.Forms.DialogResult resp = 0;
            string msj = string.Empty;
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = false.ToInt();
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[5].Tag.ToStr();
            T_PRTGLOB.etap = etapa;
           
            if (index == 0)
            {
                escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                limpiaint(initObj);
                escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            }
            else
            {
                limpiacom(initObj);
                limpiaint(initObj);
                escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            }
        }

        public static void etapae_Click(InitializationObject initObj)
        {
            string etapae = initObj.PrtEnt06._etapae_0.Tag.ToStr();
            int a = 0;
            System.Windows.Forms.DialogResult resp = 0;
            string msj = string.Empty;

            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = 0;
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[0].Tag.ToStr();
            T_PRTGLOB.etap = etapae;
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;
            a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            if (a != 0)
            {             
                initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
            }
            if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
            {
                escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                limpiagas(initObj);
                escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            }
        }

        public static void etapae_Si_Click(InitializationObject initObj)
        {

            string etapae = initObj.PrtEnt06._etapae_0.Tag.ToStr();
            int a = 0;
            string msj = string.Empty;

            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = 0;
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[0].Tag.ToStr();
            T_PRTGLOB.etap = etapae;

            escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            limpiagas(initObj);
            escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
        }


        public static void etaret_Click(InitializationObject initObj, int index)
        {
            string etaret = initObj.PrtEnt06._etaret_[index].Tag.ToStr();
            int a = 0;
            string msj = string.Empty;
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = false.ToInt();
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[6].Tag.ToStr();
            T_PRTGLOB.etap = etaret;
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;
            a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            if (a != 0)
            {              
                initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
                return;
            }
            escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            limpiaint(initObj);
            limpiagas(initObj);
            escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
        }

        public static void etaret_Si_Click(InitializationObject initObj, int index)
        {
            string etaret = initObj.PrtEnt06._etaret_[index].Tag.ToStr();
            int a = 0;
            string msj = string.Empty;
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Agregar";
            initObj.PrtEnt06.Eliminar.Enabled = false;

            T_PRTGLOB.delista = false.ToInt();
            T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToStr();
            T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[6].Tag.ToStr();
            T_PRTGLOB.etap = etaret;
            escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            limpiaint(initObj);
            limpiagas(initObj);
            escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
        }

        public static void limpiacom(InitializationObject initObj)
        {
            if (initObj.PrtEnt06.prtcomision.Enabled)
            {
                initObj.PrtEnt06.lista_com.Items.Clear();
                initObj.PrtEnt06.lista_com.Enabled = false;
                initObj.PrtEnt06.prtcomision.Enabled = false;
                prtcomision(initObj, false); //Add por JF
                nomostrar = 0;
            }
        }
        public static void limpiagas(InitializationObject initObj)
        {
            if (initObj.PrtEnt06.prtgasto.Enabled)
            {
                initObj.PrtEnt06.tarifa.Checked = false;
                initObj.PrtEnt06.tarifa.Enabled = false;
                initObj.PrtEnt06.prtmongas.Text = string.Empty;
                initObj.PrtEnt06.prtgasto.Enabled = false;
                prtGasto(initObj, false);
            }
        }
        public static void limpiaint(InitializationObject initObj)
        {
            if (initObj.PrtEnt06.prtinteres.Enabled)
            {
                initObj.PrtEnt06.prttasint.Text = string.Empty;
                initObj.PrtEnt06._prttipoi_[0].Selected = false; //false //por grupo de radios siempre uno tiene que setearse
                initObj.PrtEnt06._prttipoi_[1].Selected = false;
                initObj.PrtEnt06._prttipoi_[2].Selected = false;
                initObj.PrtEnt06.prtflot.Checked = false;
                initObj.PrtEnt06.prtinteres.Enabled = false;
                initObj.PrtEnt06._prttipoi_[0].Enabled = false;
                initObj.PrtEnt06._prttipoi_[1].Enabled = false;
                initObj.PrtEnt06._prttipoi_[2].Enabled = false;
                initObj.PrtEnt06.prtflot.Enabled = false;
            }
        }
        public static void lista_com_dblclick(InitializationObject initObj)
        {
            string l = string.Empty;
            int t = 0;
            string hoy = string.Empty;
            string f = string.Empty;
            int a = 0;
            string linea = string.Empty;
            int valor = 0;
            string tasa = string.Empty;
            string maximo = string.Empty;
            string minimo = string.Empty;
            string monto = string.Empty;

            initObj.PrtEnt13.fecha.Text = string.Empty;

            if (initObj.PrtEnt06.lista_com.Items.Count > 0)
                linea = UTILES.QuitaEspaciosEnBlanco(initObj.PrtEnt06.lista_com.Items[initObj.PrtEnt06.lista_com.ListIndex].Value);
            if (!string.IsNullOrEmpty(linea))
                linea += VB6Helpers.Chr(9) + initObj.PrtEnt06.lista_com.Items[initObj.PrtEnt06.lista_com.ListIndex].Tag;
            else if (linea == null)
                linea = string.Empty;

            if (!string.IsNullOrEmpty(linea))
            {
                a = linea.IndexOf("(", 1, StringComparison.CurrentCulture);
                if (a != -1)
                {
                    initObj.PrtEnt13.manual.Checked = true;
                    f = linea.Substring((linea.Length - a));
                    f = UTILES.copiardestring(f, ")", 1);
                    hoy = DateTime.Now.ToString("dd/mm/yyyy");
                    if (String.CompareOrdinal(VB6Helpers.Format(f, "yyyymmdd"), VB6Helpers.Format(hoy, "yyyymmdd")) < 0)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " Tasa especial está vigente desde " + f + " , imposible modificarla.",
                            Title = T_PRTGLOB.TitTasas,
                            Type = TipoMensaje.Informacion
                        });
                        return;
                    }
                    f = VB6Helpers.Format(f, "ddmmyyyy");
                    initObj.PrtEnt13.fecha.Text = string.Empty;
                    if (!string.IsNullOrEmpty(f))
                        initObj.PrtEnt13.fecha.Text = f;
                }
                else
                {
                    valor = Convert.ToInt32(UTILES.copiardestring(linea, VB6Helpers.Chr(9), 6));
                    if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 1) != T_PRTGLOB.guiones_tasa)
                        tasa = Format.FormatCurrency(Format.StringToDouble(UTILES.copiardestring(linea, VB6Helpers.Chr(9), 1)), "0.000000");
                    else
                        tasa = string.Empty;

                    if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 2) != T_PRTGLOB.guiones)
                        //monto = VB6Helpers.Format(UTILES.copiardestring(linea, VB6Helpers.Chr(9), 2), "0.00"); //
                        monto = Format.FormatCurrency(Format.StringToDouble(UTILES.copiardestring(linea, VB6Helpers.Chr(9), 2)), T_PRTGLOB.formato);
                    else
                        monto = string.Empty;

                    initObj.PrtEnt13.fecha.Text = string.Empty;
                    if (!string.IsNullOrEmpty(VB6Helpers.Format(UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5), "dd/mm/yyyy")))
                        initObj.PrtEnt13.fecha.Text = VB6Helpers.Format(UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5), "dd/mm/yyyy"); //Se cambio de Text a Tag

                    if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5).GetDate() <= DateTime.Now)
                        initObj.PrtEnt13.Lb_fecing.Text = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);
                    else
                        initObj.PrtEnt13.Lb_fecing.Text = string.Empty;

                    if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 3) != T_PRTGLOB.guiones)
                        //minimo = VB6Helpers.Format(UTILES.copiardestring(linea, VB6Helpers.Chr(9), 3), "0.00"); //
                        minimo = Format.FormatCurrency(Format.StringToDouble(UTILES.copiardestring(linea, VB6Helpers.Chr(9), 3)), T_PRTGLOB.formato);
                    else
                        minimo = string.Empty;
                    if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 4) != T_PRTGLOB.guiones)
                        //maximo = VB6Helpers.Format(UTILES.copiardestring(linea, VB6Helpers.Chr(9), 4), "0.00");//
                        maximo = Format.FormatCurrency(Format.StringToDouble(UTILES.copiardestring(linea, VB6Helpers.Chr(9), 4)), T_PRTGLOB.formato); 
                    else
                        maximo = string.Empty;


                }
            }
            else
            {
                valor = 0;
                tasa = string.Empty;
                monto = string.Empty;
                minimo = string.Empty;
                maximo = string.Empty;
                if (initObj.PrtEnt06.lista_com.ListIndex > 0) //COMENTARIADO SEGUN COMO ESTA EL ORIGINAL 
                {
                    t = initObj.PrtEnt06.lista_com.ListIndex;
                    l = initObj.PrtEnt06.lista_com.Items[t].Value;
                    l += VB6Helpers.Chr(9) + initObj.PrtEnt06.lista_com.Items[t].Tag;

                    if (!string.IsNullOrEmpty(l))
                    {
                        a = l.InStr("(", 1, StringComparison.CurrentCulture);
                        if (a != 0)
                        {
                            f = VB6Helpers.Right(l, l.Length - a);
                            f = UTILES.copiardestring(f, ")", 1);

                            hoy = VB6Helpers.Format(DateTime.Now, "dd/mm/yyyy");
                            if (String.CompareOrdinal(VB6Helpers.Format(f, "yyyymmdd"), VB6Helpers.Format(hoy, "yyyymmdd")) >= 0)
                            {
                                initObj.PrtEnt13.fecha.Text = string.Empty;
                                if (!string.IsNullOrEmpty(f))
                                    initObj.PrtEnt13.fecha.Text = f;
                            }
                            f = VB6Helpers.Format(DateTime.Now, "ddmmyyyy");
                        }
                        else
                        {
                            if (!Convert.ToBoolean(initObj.PrtEnt06.lista_com.get_ItemData_(t)))
                            {
                                initObj.PrtEnt13.fecha.Text = string.Empty;
                                if (!string.IsNullOrEmpty(UTILES.copiardestring(l, VB6Helpers.Chr(9), 5)))
                                    initObj.PrtEnt13.fecha.Text = UTILES.copiardestring(l, VB6Helpers.Chr(9), 5);
                            }
                        }
                    }
                    else
                    {
                        initObj.PrtEnt13.fecha.Text = string.Empty;
                    }
                }
            }
            if (initObj.PrtEnt13.Frame1.Enabled)
            {
                initObj.PrtEnt13.fijo.Checked = valor.ToBool();
                initObj.PrtEnt13.tasa.Text = tasa;
                initObj.PrtEnt13.monto.Text = monto;
                initObj.PrtEnt13.minimo.Text = minimo;
                initObj.PrtEnt13.maximo.Text = maximo;
                if (initObj.PrtEnt06.lista_com.ListIndex > 0)
                {
                    l = initObj.PrtEnt06.lista_com.Items[initObj.PrtEnt06.lista_com.ListIndex - 1].Value;
                    monto = UTILES.copiardestring(l, VB6Helpers.Chr(9), 2);//UTILES.unformat(UTILES.copiardestring(l, VB6Helpers.Chr(9), 2));
                    initObj.PrtEnt13.MontoTagFrm.Tag = monto;
                }
                else
                    initObj.PrtEnt13.MontoTagFrm.Tag = 0;
            }
            initObj.PrtEnt06.aceptar.Enabled = false;

        }



        public static void lista_dblclick(InitializationObject initObj)
        {
            string linea = string.Empty;
            string spe = string.Empty;

            if (initObj.PrtEnt06.lista.Items.Count > 0)
                linea = UTILES.QuitaEspaciosEnBlanco(initObj.PrtEnt06.lista.Items[initObj.PrtEnt06.lista.ListIndex].Value);
            if (!string.IsNullOrEmpty(linea))
                linea += VB6Helpers.Chr(9) + initObj.PrtEnt06.lista.Items[initObj.PrtEnt06.lista.ListIndex].Tag.ToString();

            spe = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);
            T_PRTGLOB.sistema = UTILES.copiardestring(spe, " ", 1);
            T_PRTGLOB.producto = UTILES.copiardestring(spe, " ", 2);
            T_PRTGLOB.etap = UTILES.copiardestring(spe, " ", 3);
            T_PRTGLOB.delista = 1;

            initObj.PrtEnt06.EstadoMsjeConfirmacion = 0; /* 0:Todos Estados distinto a Nuevo        1: Eliminado */

            if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 4) == "Eliminado")
            {
                initObj.PrtEnt06.EstadoMsjeConfirmacion = 1;
                return;
            }

            if (initObj.PrtEnt06.EstadoMsjeConfirmacion == 0)
            {
                escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                initObj.PrtEnt06.Agregar.Enabled = true;
                initObj.PrtEnt06.Agregar.Text = "Modificar";
                initObj.PrtEnt06.Eliminar.Enabled = true;
            }

        }


        public static void Lista_No_Click(InitializationObject initObj)
        {
            limpiacom(initObj);
            limpiagas(initObj);
            limpiaint(initObj);
            initObj.PrtEnt06.lista.ListIndex = -1;
            initObj.PrtEnt06.Titulo.Text = "Tasas Especiales";
            initObj.PrtEnt06.Agregar.Enabled = false;
            initObj.PrtEnt06.Eliminar.Enabled = false;
            return;
        }

        public static void Lista_Si_Click(InitializationObject initObj)
        {
            string linea = string.Empty;
            string spe = string.Empty;
            int s = 0;
            int salir = 0;
            if (initObj.PrtEnt06.lista.Items.Count > 0)
                linea = initObj.PrtEnt06.lista.Items[initObj.PrtEnt06.lista.ListIndex].Value;
            if (!string.IsNullOrEmpty(linea))
                linea += VB6Helpers.Chr(9) + initObj.PrtEnt06.lista.Items[initObj.PrtEnt06.lista.ListIndex].Tag;

            spe = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);
            T_PRTGLOB.sistema = UTILES.copiardestring(spe, " ", 1);
            T_PRTGLOB.producto = UTILES.copiardestring(spe, " ", 2);
            T_PRTGLOB.etap = UTILES.copiardestring(spe, " ", 3);
            T_PRTGLOB.delista = 1;
            s = initObj.PrtEnt06.lista.get_ItemData_(initObj.PrtEnt06.lista.ListIndex);
            switch (s)
            {
                case T_PRTGLOB.eliminado_nuevo:
                    s = T_PRTGLOB.nuevo;
                    respalda(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, s, ref salir);
                    string argTemp1 = "Nuevo";
                    escribelista(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, ref argTemp1);
                    break;
                case T_PRTGLOB.eliminado_leido:
                    s = T_PRTGLOB.leido;
                    respalda(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, s, ref salir);
                    string argTemp2 = "Leído";
                    escribelista(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, ref argTemp2);
                    break;
                case T_PRTGLOB.eliminado_modificado:
                    s = T_PRTGLOB.modificado;
                    respalda(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, s, ref salir);
                    string argTemp3 = "Modificado";
                    escribelista(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, ref argTemp3);
                    break;
            }

            escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            escribegas(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.Agregar.Text = "Modificar";
            initObj.PrtEnt06.Eliminar.Enabled = true;

        }

        //public static void lista_KeyPress(InitializationObject initObj)
        //{
        //    int KeyAscii;
        //    KeyAscii = 0;
        //    if (initObj.PrtEnt06.lista.Items.Count != 0)
        //    {
        //        if (KeyAscii == 13)
        //        {
        //            if (initObj.PrtEnt06.lista.Items.Count >= 1 && initObj.PrtEnt06.lista.ListIndex == -1)
        //            {
        //                initObj.PrtEnt06.lista.ListIndex = 0;
        //            }
        //            KeyAscii = 0;
        //            lista_dblclick(initObj);
        //        }
        //        else
        //        {
        //            KeyAscii = 0;
        //            Console.Beep();
        //        }
        //    }
        //    else
        //    {
        //        Console.Beep();
        //    }
        //}

        public static void menu_Click(InitializationObject initObj)
        {
            initObj.PrtEnt06.lista.ListIndex = -1;
        }
        public static string Obtiene_NomEsp(InitializationObject iniObj, string codofi, string codesp)
        {
            string Obtiene_NomEsp = string.Empty;
            int i = 0;
            int fin = 0;
            Obtiene_NomEsp = string.Empty;
            fin = 0;
            fin = iniObj.PRTYENT.VEjc.GetUpperBound(0);
            for (i = 0; i <= fin; i += 1)
            {
                if (iniObj.PRTYENT.VEjc[i].codofi == codofi && iniObj.PRTYENT.VEjc[i].codejc == codesp)
                {
                    Obtiene_NomEsp = PRTYENT2.Minuscula2(iniObj.PRTYENT.VEjc[i].nombre);
                    break;
                }
            }
            return Obtiene_NomEsp;
        }
        public static string PoneChar(string Texto, string Caracter, string DerIzq, int largo)
        {
            string PoneChar = string.Empty;

            string s = string.Empty;
            int i = 0;
            string t = string.Empty;
            t = Texto.Trim();
            if (t.Length >= largo)
            {
                PoneChar = t;
                return PoneChar;
            }
            for (i = 0; i <= largo - t.Length; i += 1)
            {
                s = s + Caracter;
            }
            if (DerIzq == "D")
                PoneChar = t + s;
            else if (DerIzq == "H")
                PoneChar = s + t;
            return PoneChar;
        }
        public static void Pr_Cargar_Espec(InitializationObject initObj, BCH.Comex.Common.UI_Modulos.UI_Combo Cbo_Us, string oficina, string tipejc)
        {
            string s = string.Empty;
            int i = 0;
            int n = 0;
            n = initObj.PRTYENT.VEjc.GetUpperBound(0);
            Cbo_Us.Clear();
            if (n > 0)
            {
                for (i = 0; i <= n; i += 1)
                {
                    if (initObj.PRTYENT.VEjc[i].codofi == oficina && initObj.PRTYENT.VEjc[i].tipo == tipejc)
                    {
                        s = string.Empty;
                        s = s + VB6Helpers.Format(initObj.PRTYENT.VEjc[i].codofi, "000") + "-";
                        s = s + VB6Helpers.Format(initObj.PRTYENT.VEjc[i].codejc, "000") + " : ";
                        s = s + PRTYENT2.Minuscula2(initObj.PRTYENT.VEjc[i].nombre);

                        Cbo_Us.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                        {
                            Data = initObj.PRTYENT.VEjc[i].codofi.ToInt(),
                            Value = s
                        });
                    }
                }
            }
        }
        public static void prodexp_Click(InitializationObject initObj, int index)
        {
            string prodexp = initObj.PrtEnt06._prodexp_[index].Tag.ToString();
            int a = 0;
          
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;
            switch (index)
            {
                case 4:
                    T_PRTGLOB.delista = 0;
                    initObj.PrtEnt06.Agregar.Enabled = true;
                    initObj.PrtEnt06.Agregar.Text = "Agregar";
                    initObj.PrtEnt06.Eliminar.Enabled = false;
                    T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToString();
                    T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[4].Tag.ToStr();
                    T_PRTGLOB.etap = "SIN";
                    a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    if (a != 0)
                    {
                        initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
                        return;
                    }
                    if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
                    {
                        limpiacom(initObj);
                        escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        limpiagas(initObj);
                        escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    }
                    break;
            }
        }

        public static void prodexp_Si_Click(InitializationObject initObj, int index)
        {   
            //limpiacom(initObj);
            //escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            //limpiagas(initObj);
            //escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
            string prodexp = initObj.PrtEnt06._prodexp_[index].Tag.ToString();
            int a = 0;        
            switch (index)
            {
                case 4:
                    T_PRTGLOB.delista = 0;
                    initObj.PrtEnt06.Agregar.Enabled = true;
                    initObj.PrtEnt06.Agregar.Text = "Agregar";
                    initObj.PrtEnt06.Eliminar.Enabled = false;
                    T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[1].Tag.ToString();
                    T_PRTGLOB.producto = initObj.PrtEnt06._prodexp_[4].Tag.ToStr();
                    T_PRTGLOB.etap = "SIN";
                    //a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    //if (a != 0)
                    //{
                    //    initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
                    //    return;
                    //}
                    if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
                    {
                        limpiacom(initObj);
                        escribeint(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        limpiagas(initObj);
                        escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    }
                    break;
            }

        }

        public static void prodser_Click(InitializationObject initObj, int index)
        {
            string prodser = initObj.PrtEnt06._prodser_[index].Tag.ToStr();
            int a = 0;
            initObj.PrtEnt06.idEstadoMsjeExportacion = 0;
            switch (index)
            {
                case 0:
                case 1:
                case 2:
                case 4:
                    T_PRTGLOB.delista = false.ToInt();
                    initObj.PrtEnt06.Agregar.Enabled = true;
                    initObj.PrtEnt06.Agregar.Text = "Agregar";
                    initObj.PrtEnt06.Eliminar.Enabled = false;
                    T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[2].Tag.ToString();
                    T_PRTGLOB.producto = prodser;
                    T_PRTGLOB.etap = "SIN";
                    a = esta_eliminado(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    if (a != 0)
                    {           
                        initObj.PrtEnt06.idEstadoMsjeExportacion = 1;
                        return;
                    }
                    if (initObj.PrtEnt06.idEstadoMsjeExportacion == 0)
                    {
                        escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                        limpiaint(initObj);
                        limpiagas(initObj);
                        escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    }
                    break;
            }
        }

        public static void prodser_Si_Click(InitializationObject initObj, int index)
        {
            string prodser = initObj.PrtEnt06._prodser_[index].Tag.ToStr();

            switch (index)
            {
                case 0:
                case 1:
                case 2:
                case 4:
                    T_PRTGLOB.delista = false.ToInt();
                    initObj.PrtEnt06.Agregar.Enabled = true;
                    initObj.PrtEnt06.Agregar.Text = "Agregar";
                    initObj.PrtEnt06.Eliminar.Enabled = false;
                    T_PRTGLOB.sistema = initObj.PrtEnt06._menu_[2].Tag.Str(); //_menu_2.Tag.ToStr();
                    T_PRTGLOB.producto = prodser;
                    T_PRTGLOB.etap = "SIN";

                    escribecom(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    limpiaint(initObj);
                    limpiagas(initObj);
                    escribetitulo(initObj, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap);
                    break;
            }
        }

        public static void prtflot_Click(InitializationObject initObj)
        {
            initObj.PrtEnt06.Agregar.Enabled = true;
        }

        public static void prttasint_LostFocus(InitializationObject initObj)
        {
            int a = 0;
        }
        public static void prttipoi_Click(InitializationObject initObj)
        {   
            var Index = 0;
            for (int i = 0; i < initObj.PrtEnt06._prttipoi_.Count; i++)
            {
                if (initObj.PrtEnt06._prttipoi_[i].Selected)
                {
                    Index = i;
                    break;
                }
            }
            initObj.PrtEnt06.Agregar.Enabled = true;
            initObj.PrtEnt06.prtflot.Checked = false;
            switch (Index)
            {
                case 2:
                    initObj.PrtEnt06.prtflot.Enabled = false;
                    break;
                default:
                    initObj.PrtEnt06.prtflot.Enabled = true;
                    break;
            }
        }
        public static void respalda(InitializationObject initObj, string sist, string prod, string etapa, int estado, ref int salir)
        {
            string monto = string.Empty;
            bool esta = false;
            int i = 0;
            int final = 0;
            string mto_hasta = string.Empty;
            string max = string.Empty;
            string min = string.Empty;
            string tasa = string.Empty;
            int kk = 0;
            string f = string.Empty;
            int a = 0;
            string linea = string.Empty;
            int k = 0;
            int manual = 0;
            string fecha = string.Empty;
            if (initObj.PrtEnt06.prtcomision.Enabled)
            {
                if (Convert.ToBoolean(T_PRTGLOB.modifico_tasa) || estado == 4)
                {
                    for (k = 0; k <= initObj.PrtEnt06.lista_com.Items.Count - 2; k += 1)
                    {
                        fecha = string.Empty;
                        linea = UTILES.QuitaEspaciosEnBlanco(initObj.PrtEnt06.lista_com.Items[k].Value);
                        if (!string.IsNullOrEmpty(linea))
                            linea += VB6Helpers.Chr(9) + initObj.PrtEnt06.lista_com.Items[k].Tag;


                        a = VB6Helpers.Instr(linea, "(");  // linea.InStr("(", 1, StringComparison.CurrentCulture);
                        if (a != 0)
                        {
                            manual = true.ToInt(); //((true) ? -1 : 0);
                            f = VB6Helpers.Right(linea, linea.Length - a);//linea.Right((linea.Len() - a));
                            f = UTILES.copiardestring(f, ")", 1);
                            fecha = f;
                        }
                        else
                        {
                            manual = false.ToInt(); //((false) ? -1 : 0);
                            kk = k;
                            salir = valores(initObj, kk, ref tasa, ref min, ref max, ref mto_hasta);
                            if (salir != 0)
                            {
                                salir = 1;
                                return;
                            }
                        }
                        final = -1;
                        final = initObj.PRTGLOB.rescom.GetUpperBound(0);
                        if (final != -1)
                        {
                            if (initObj.PRTGLOB.rescom[0].sistema == "")
                            {
                                i = 0;
                                initObj.PRTGLOB.rescom[i] = new prtytcom(); //Add por JFernandez el 12/11/2015
                                initObj.PRTGLOB.rescom[i].estado = T_PRTGLOB.nuevo;
                                initObj.PRTGLOB.rescom[i].secuencia = k + T_PRTYENT.offsec;
                                initObj.PRTGLOB.rescom[i].sistema = sist;
                                initObj.PRTGLOB.rescom[i].producto = prod;
                                initObj.PRTGLOB.rescom[i].etapa = etapa;
                                linea = UTILES.QuitaEspaciosEnBlanco(initObj.PrtEnt06.lista_com.Items[k].Value);
                                if (!string.IsNullOrEmpty(linea))
                                    linea += VB6Helpers.Chr(9) + initObj.PrtEnt06.lista_com.Items[k].Tag;
                            
                                if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 6).ToVal() != 0)
                                    initObj.PRTGLOB.rescom[i].mto_fijo = 1;
                                else
                                    initObj.PRTGLOB.rescom[i].mto_fijo = 0;
                                initObj.PRTGLOB.rescom[i].tasa = Format.StringToDouble(tasa); //Convert.ToDouble(UTILES.unformat(tasa));
                                initObj.PRTGLOB.rescom[i].min = Format.StringToDouble(min);//Convert.ToDouble(UTILES.unformat(min));
                                initObj.PRTGLOB.rescom[i].max = Format.StringToDouble(max);//Convert.ToDouble(UTILES.unformat(max));
                                initObj.PRTGLOB.rescom[i].hasta = Format.StringToDouble(mto_hasta); //Convert.ToDouble(UTILES.unformat(mto_hasta));
                                if (string.IsNullOrEmpty(fecha))
                                    fecha = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);
                                initObj.PRTGLOB.rescom[i].fecha = fecha;
                                initObj.PRTGLOB.rescom[i].manual = manual;
                            }
                            else
                            {
                                i = 0;
                                esta = false;
                                while (i <= final && !esta)
                                {
                                    if (initObj.PRTGLOB.rescom[i].sistema.ToUpper() == sist.ToUpper() && initObj.PRTGLOB.rescom[i].producto.ToUpper() == prod.ToUpper() && initObj.PRTGLOB.rescom[i].etapa.ToUpper() == etapa.ToUpper() && initObj.PRTGLOB.rescom[i].secuencia == k + T_PRTYENT.offsec)
                                    {
                                        esta = true;
                                        break;
                                    }
                                    else
                                        i = i + 1;
                                }
                                if (esta)
                                {
                                    switch (initObj.PRTGLOB.rescom[i].estado)
                                    {
                                        case T_PRTGLOB.leido:
                                            initObj.PRTGLOB.rescom[i].estado = estado;
                                            break;
                                        case T_PRTGLOB.nuevo:
                                            if (estado == T_PRTGLOB.eliminado_leido)
                                                initObj.PRTGLOB.rescom[i].estado = T_PRTGLOB.eliminado_nuevo;
                                            break;
                                        case T_PRTGLOB.modificado:
                                            if (estado == T_PRTGLOB.eliminado_leido)
                                                initObj.PRTGLOB.rescom[i].estado = T_PRTGLOB.eliminado_modificado;
                                            break;
                                        default:
                                            initObj.PRTGLOB.rescom[i].estado = estado;
                                            break;
                                    }
                                    linea = UTILES.QuitaEspaciosEnBlanco(initObj.PrtEnt06.lista_com.Items[k].Value);
                                    if (!string.IsNullOrEmpty(linea))
                                        linea += VB6Helpers.Chr(9) + initObj.PrtEnt06.lista_com.Items[k].Tag;


                                    if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 6).ToVal() != 0)
                                        initObj.PRTGLOB.rescom[i].mto_fijo = 1;
                                    else
                                        initObj.PRTGLOB.rescom[i].mto_fijo = 0;

                                    initObj.PRTGLOB.rescom[i].tasa = Format.StringToDouble(tasa); //Convert.ToDouble(tasa);//Convert.ToDouble(UTILES.unformat(tasa));
                                    initObj.PRTGLOB.rescom[i].min = Format.StringToDouble(min);//Convert.ToDouble(min);
                                    initObj.PRTGLOB.rescom[i].max = Format.StringToDouble(max);//Convert.ToDouble(max);
                                    initObj.PRTGLOB.rescom[i].hasta = Format.StringToDouble(mto_hasta);//Convert.ToDouble(mto_hasta);
                                    if (string.IsNullOrEmpty(fecha))
                                        fecha = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);                                 
                                    initObj.PRTGLOB.rescom[i].fecha = fecha;
                                    initObj.PRTGLOB.rescom[i].manual = manual;
                                }
                                else
                                {
                                    i = final + 1;
                                    Array.Resize(ref  initObj.PRTGLOB.rescom, i + 1);
                                    initObj.PRTGLOB.rescom[i] = new prtytcom();
                                    initObj.PRTGLOB.rescom[i].estado = T_PRTGLOB.nuevo;
                                    initObj.PRTGLOB.rescom[i].secuencia = k + T_PRTYENT.offsec;
                                    initObj.PRTGLOB.rescom[i].sistema = sist;
                                    initObj.PRTGLOB.rescom[i].producto = prod;
                                    initObj.PRTGLOB.rescom[i].etapa = etapa;

                                    linea = UTILES.QuitaEspaciosEnBlanco(initObj.PrtEnt06.lista_com.Items[k].Value);
                                    if (!string.IsNullOrEmpty(linea))
                                        linea += VB6Helpers.Chr(9) + initObj.PrtEnt06.lista_com.Items[k].Tag;
                                
                                    if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 6).ToVal() != 0)
                                        initObj.PRTGLOB.rescom[i].mto_fijo = 1;
                                    else
                                        initObj.PRTGLOB.rescom[i].mto_fijo = 0;

                                    initObj.PRTGLOB.rescom[i].tasa = Format.StringToDouble(tasa);
                                    initObj.PRTGLOB.rescom[i].min = Format.StringToDouble(min);
                                    initObj.PRTGLOB.rescom[i].max = Format.StringToDouble(max);
                                    initObj.PRTGLOB.rescom[i].hasta = Format.StringToDouble(mto_hasta);
                                    if (string.IsNullOrEmpty(fecha))
                                        fecha = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);
                                    initObj.PRTGLOB.rescom[i].fecha = fecha;
                                    initObj.PRTGLOB.rescom[i].manual = manual;
                                }
                            }
                        }
                        else
                        {
                            initObj.PRTGLOB.rescom = new prtytcom[1];
                            initObj.PRTGLOB.rescom[0] = new prtytcom();
                            initObj.PRTGLOB.rescom[0].estado = T_PRTGLOB.nuevo;
                            initObj.PRTGLOB.rescom[0].sistema = sist;
                            initObj.PRTGLOB.rescom[0].producto = prod;
                            initObj.PRTGLOB.rescom[0].etapa = etapa;
                            initObj.PRTGLOB.rescom[0].secuencia = k + T_PRTYENT.offsec;
                            linea = UTILES.QuitaEspaciosEnBlanco(initObj.PrtEnt06.lista_com.Items[k].Value);
                            if (!string.IsNullOrEmpty(linea))
                                linea += VB6Helpers.Chr(9) + initObj.PrtEnt06.lista_com.Items[k].Tag;
                          
                            if (UTILES.copiardestring(linea, VB6Helpers.Chr(9), 6).ToVal() != 0)
                                initObj.PRTGLOB.rescom[0].mto_fijo = 1;
                            else
                                initObj.PRTGLOB.rescom[0].mto_fijo = 0;

                            initObj.PRTGLOB.rescom[0].tasa = Format.StringToDouble(tasa);//Convert.ToDouble(UTILES.unformat(tasa));
                            initObj.PRTGLOB.rescom[0].min = Format.StringToDouble(min);
                            initObj.PRTGLOB.rescom[0].max = Format.StringToDouble(max);
                            initObj.PRTGLOB.rescom[0].hasta = Format.StringToDouble(mto_hasta);
                            if (string.IsNullOrEmpty(fecha))
                                fecha = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);
                            initObj.PRTGLOB.rescom[0].fecha = fecha;
                            initObj.PRTGLOB.rescom[0].manual = manual;
                        }
                    }
                }
                else
                {
                    if (initObj.PrtEnt06.lista_com.Items.Count == 1 && initObj.PrtEnt06.lista_com.Items[0].Value == null)//initObj.PrtEnt06.lista_com.get_ItemData_(0).ToStr() == null)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Title = T_PRTGLOB.TitTasas,
                            Text = " Para agregar ítemes con tasas especiales debe completar la información.",
                            Type = TipoMensaje.Informacion
                        });

                        salir = true.ToInt();
                        return;
                    }
                }
            }

            if (initObj.PrtEnt06.prtinteres.Enabled)
            {
                final = -1;
                final = initObj.PRTGLOB.resint.GetUpperBound(0);
                if (final != -1)
                {
                    if (initObj.PRTGLOB.resint[0].sistema == "")
                    {
                        initObj.PRTGLOB.resint[0].estado = estado;
                        initObj.PRTGLOB.resint[0].sistema = sist;
                        initObj.PRTGLOB.resint[0].producto = prod;
                        initObj.PRTGLOB.resint[0].etapa = etapa;
                        tasa = initObj.PrtEnt06.prttasint.Text;
                        initObj.PRTGLOB.resint[0].tasa = Format.StringToDouble(PRTYENT.cambiasepdec(tasa));
                        initObj.PRTGLOB.resint[0].libor = initObj.PrtEnt06._prttipoi_[0].Selected.ToInt();
                        initObj.PRTGLOB.resint[0].prime = initObj.PrtEnt06._prttipoi_[1].Selected.ToInt();
                        initObj.PRTGLOB.resint[0].flotante = initObj.PrtEnt06.prtflot.ID.ToInt();
                    }
                    else
                    {
                        i = 0;
                        esta = false;
                        while (i <= final && !esta)
                        {
                            if (initObj.PRTGLOB.resint[i].sistema.ToUpper() == sist.ToUpper() && initObj.PRTGLOB.resint[i].producto.ToUpper() == prod.ToUpper() && initObj.PRTGLOB.resint[i].etapa.ToUpper() == etapa.ToUpper())
                            {
                                esta = true;
                                break;
                            }
                            else
                                i = i + 1;
                        }
                        if (esta)
                        {
                            switch (initObj.PRTGLOB.resint[i].estado)
                            {
                                case T_PRTGLOB.leido:
                                    initObj.PRTGLOB.resint[i].estado = estado;
                                    break;
                                case T_PRTGLOB.nuevo:
                                    if (estado == T_PRTGLOB.eliminado_leido)
                                        initObj.PRTGLOB.resint[i].estado = T_PRTGLOB.eliminado_nuevo;
                                    break;
                                case T_PRTGLOB.modificado:
                                    if (estado == T_PRTGLOB.eliminado_leido)
                                        initObj.PRTGLOB.resint[i].estado = T_PRTGLOB.eliminado_modificado;
                                    break;
                                default:
                                    initObj.PRTGLOB.resint[i].estado = estado;
                                    break;
                            }
                            tasa = initObj.PrtEnt06.prttasint.Text;
                            initObj.PRTGLOB.resint[i].tasa = Format.StringToDouble(tasa); //Convert.ToDouble(PRTYENT.cambiasepdec(tasa));
                            initObj.PRTGLOB.resint[i].libor = initObj.PrtEnt06._prttipoi_[0].Selected.ToInt(); //initObj.PrtEnt06._prttipoi_[0].ID.ToInt();
                            initObj.PRTGLOB.resint[i].prime = initObj.PrtEnt06._prttipoi_[1].Selected.ToInt(); //initObj.PrtEnt06._prttipoi_[1].ID.ToInt();
                            initObj.PRTGLOB.resint[i].flotante = initObj.PrtEnt06.prtflot.ID.ToInt();
                        }
                        else
                        {
                            prtytint nuevoParty = new prtytint();
                            nuevoParty.sistema = sist;
                            nuevoParty.producto = prod;
                            nuevoParty.etapa = etapa;
                            nuevoParty.estado = estado;
                            tasa = initObj.PrtEnt06.prttasint.Text;
                            nuevoParty.tasa = tasa.ToDbl();//Convert.ToDouble(PRTYENT.cambiasepdec(tasa));
                            nuevoParty.libor = initObj.PrtEnt06._prttipoi_[0].Selected.ToInt();//initObj.PrtEnt06._prttipoi_[0].ID.ToInt();
                            nuevoParty.prime = initObj.PrtEnt06._prttipoi_[1].Selected.ToInt();//initObj.PrtEnt06._prttipoi_[1].ID.ToInt();
                            nuevoParty.flotante = initObj.PrtEnt06.prtflot.Checked.ToInt();

                            List<prtytint> listaTemp = initObj.PRTGLOB.resint.ToList();
                            listaTemp.Add(nuevoParty);
                            initObj.PRTGLOB.resint = listaTemp.ToArray();
                        }
                    }
                }
                else
                {
                    initObj.PRTGLOB.resint = new prtytint[1];

                    initObj.PRTGLOB.resint[0] = new prtytint();
                    initObj.PRTGLOB.resint[0].estado = estado;
                    initObj.PRTGLOB.resint[0].sistema = sist;
                    initObj.PRTGLOB.resint[0].producto = prod;
                    initObj.PRTGLOB.resint[0].etapa = etapa;
                    tasa = initObj.PrtEnt06.prttasint.Text;
                    initObj.PRTGLOB.resint[0].tasa = Format.StringToDouble(tasa); //Convert.ToDouble(PRTYENT.cambiasepdec(tasa));
                    initObj.PRTGLOB.resint[0].libor = initObj.PrtEnt06._prttipoi_[0].Selected.ToInt(); //((object)((dynamic)PrtEnt06.DefInstance.prttipoi[0]).Value()).ToInt();
                    initObj.PRTGLOB.resint[0].prime = initObj.PrtEnt06._prttipoi_[1].Selected.ToInt();//((object)((dynamic)PrtEnt06.DefInstance.prttipoi[1]).Value()).ToInt();
                    initObj.PRTGLOB.resint[0].flotante = initObj.PrtEnt06.prtflot.Checked.ToInt();//((object)((dynamic)PrtEnt06.DefInstance.prtflot).Value()).ToInt();
                }
            }
            if (initObj.PrtEnt06.prtgasto.Enabled)
            {
                final = -1;
                final = initObj.PRTGLOB.resgas.GetUpperBound(0);
                if (final != -1)
                {
                    if (initObj.PRTGLOB.resgas[0].sistema == "")
                    {
                        initObj.PRTGLOB.resgas[0].estado = estado;
                        initObj.PRTGLOB.resgas[0].sistema = sist;
                        initObj.PRTGLOB.resgas[0].producto = prod;
                        initObj.PRTGLOB.resgas[0].etapa = etapa;
                        if (!string.IsNullOrEmpty(initObj.PrtEnt06.prtmongas.Text))
                            monto = initObj.PrtEnt06.prtmongas.Text;
                        else
                            monto = "0";
                        if (initObj.PrtEnt06.tarifa.Checked.ToInt() == 1)
                            initObj.PRTGLOB.resgas[0].tarifa = 1;
                        else
                            initObj.PRTGLOB.resgas[0].tarifa = 0;
                        initObj.PRTGLOB.resgas[0].monto = Format.StringToDouble(monto);
                    }
                    else
                    {
                        i = 0;
                        esta = false;
                        while (i <= final && !esta)
                        {
                            if (initObj.PRTGLOB.resgas[i].sistema.ToUpper() == sist.ToUpper() && initObj.PRTGLOB.resgas[i].producto.ToUpper() == prod.ToUpper() && initObj.PRTGLOB.resgas[i].etapa.ToUpper() == etapa.ToUpper())
                                esta = true;
                            else
                                i = i + 1;
                        }
                        if (esta)
                        {
                            switch (initObj.PRTGLOB.resgas[i].estado)
                            {
                                case T_PRTGLOB.leido:
                                    initObj.PRTGLOB.resgas[i].estado = estado;
                                    break;
                                case T_PRTGLOB.nuevo:
                                    if (estado == T_PRTGLOB.eliminado_leido)
                                    {
                                        initObj.PRTGLOB.resgas[i].estado = T_PRTGLOB.eliminado_nuevo;
                                    }
                                    break;
                                case T_PRTGLOB.modificado:
                                    if (estado == T_PRTGLOB.eliminado_leido)
                                    {
                                        initObj.PRTGLOB.resgas[i].estado = T_PRTGLOB.eliminado_modificado;
                                    }
                                    break;
                                default:
                                    initObj.PRTGLOB.resgas[i].estado = estado;
                                    break;
                            }
                            monto = "0";
                            if (!string.IsNullOrEmpty(initObj.PrtEnt06.prtmongas.Text))
                                monto = initObj.PrtEnt06.prtmongas.Text;

                            if (initObj.PrtEnt06.tarifa.Checked)
                                initObj.PRTGLOB.resgas[i].tarifa = 1;
                            else
                                initObj.PRTGLOB.resgas[i].tarifa = 0;
                            initObj.PRTGLOB.resgas[i].monto = Format.StringToDouble(monto);
                        }
                        else
                        {
                            Array.Resize(ref initObj.PRTGLOB.resgas, final + 2);
                            initObj.PRTGLOB.resgas[final + 1] = new prtytgas();
                            initObj.PRTGLOB.resgas[final + 1].estado = estado;
                            initObj.PRTGLOB.resgas[final + 1].sistema = sist;
                            initObj.PRTGLOB.resgas[final + 1].producto = prod;
                            initObj.PRTGLOB.resgas[final + 1].etapa = etapa;

                            if (initObj.PrtEnt06.tarifa.Checked.ToInt() == 1)
                                initObj.PRTGLOB.resgas[final + 1].tarifa = 1;
                            else
                                initObj.PRTGLOB.resgas[final + 1].tarifa = 0;
                            monto = "0";
                            if (!string.IsNullOrEmpty(initObj.PrtEnt06.prtmongas.Text))
                                monto = initObj.PrtEnt06.prtmongas.Text;
                            initObj.PRTGLOB.resgas[final + 1].monto = Format.StringToDouble(monto);
                        }
                    }
                }
                else
                {
                    initObj.PRTGLOB.resgas = new prtytgas[1];
                    initObj.PRTGLOB.resgas[0] = new prtytgas();
                    initObj.PRTGLOB.resgas[0].estado = estado;
                    initObj.PRTGLOB.resgas[0].sistema = sist;
                    initObj.PRTGLOB.resgas[0].producto = prod;
                    initObj.PRTGLOB.resgas[0].etapa = etapa;
                    if (initObj.PrtEnt06.tarifa.Checked.ToInt() == 1)
                        initObj.PRTGLOB.resgas[0].tarifa = 1;
                    else
                        initObj.PRTGLOB.resgas[0].tarifa = 0;

                    monto = "0";
                    if (!string.IsNullOrEmpty(initObj.PrtEnt06.prtmongas.Text))
                        monto = initObj.PrtEnt06.prtmongas.Text;

                    initObj.PRTGLOB.resgas[0].monto = Format.StringToDouble(monto);
                }
            }

        }
        public static void tarifa_Click(InitializationObject initObj)
        {
            if (initObj.PrtEnt06.tarifa.Checked.ToInt() == 1)
            {
                initObj.PrtEnt06.prtmongas.Text = string.Empty;
                initObj.PrtEnt06.prtmongas.Enabled = false;
            }
            else
                initObj.PrtEnt06.prtmongas.Enabled = true;
        }
        public static int valores(InitializationObject InitObj, int ind, ref string tasa, ref string minimo, ref string maximo, ref string hasta)
        {
            int valores = 0;

            string max = string.Empty;
            string min = string.Empty;
            string mon = string.Empty;
            string t = string.Empty;
            string l = string.Empty;

            if (InitObj.PrtEnt06.lista_com.Items.Count > 0)
                l = UTILES.QuitaEspaciosEnBlanco(InitObj.PrtEnt06.lista_com.Items[ind].Value);
            if (!string.IsNullOrEmpty(l))
                l += VB6Helpers.Chr(9) + InitObj.PrtEnt06.lista_com.Items[ind].Tag;

            t = UTILES.copiardestring(l, VB6Helpers.Chr(9), 1); //.Replace("&nbsp;", "");
            mon = UTILES.copiardestring(l, VB6Helpers.Chr(9), 2);
            min = UTILES.copiardestring(l, VB6Helpers.Chr(9), 3);
            max = UTILES.copiardestring(l, VB6Helpers.Chr(9), 4);

            switch (Convert.ToInt32(UTILES.copiardestring(l, VB6Helpers.Chr(9), 6)))
            {
                case 0:
                    if (Format.StringToDouble(t) != 0)
                    {
                        if (min == "0" || max == "0")
                        {
                            InitObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = " Si se va a aplicar una tasa, los montos no pueden ser cero.",
                                Title = T_PRTGLOB.TitTasas,
                                Type = TipoMensaje.Informacion
                            });
                            valores = 1;
                            return valores;
                        }
                    }
                    if (min == T_PRTGLOB.guiones)
                        minimo = "0";
                    else
                        minimo = min;
                    if (max == T_PRTGLOB.guiones)
                        maximo = "9999999999999,99";
                    else
                        maximo = max;
                    tasa = t;
                    if (mon == T_PRTGLOB.guiones)
                        hasta = "9999999999999,99";
                    else
                        hasta = mon;
                    break;
                default:
                    tasa = "0";
                    if (mon == T_PRTGLOB.guiones)
                        hasta = "9999999999999,99";
                    else
                        hasta = mon;
                    minimo = min;
                    maximo = max;
                    break;
            }
            return valores;
        }
    }
}
