using BCH.Comex.Core.BL.XGPY.Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using BCH.Comex.Utils;

namespace BCH.Comex.Core.BL.XGPY.Forms
{
    public static class PrtEnt13
    {
        public static void Aceptar_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            string ff = string.Empty;
            int ind = 0;
            double tas = 0.0;
            string fech_d = string.Empty;
            string fech_m = string.Empty;
            string fech_a = string.Empty;
            string feaux = string.Empty;
            int v = 0;
            string linea = string.Empty;
            int k = 0;
            string f = string.Empty;
            string fech = string.Empty;
            string a = string.Empty;
            string hoy = string.Empty;
            string t = string.Empty;
            string min = string.Empty;
            string max = string.Empty;
            string mon = string.Empty;
            string l = string.Empty;

            initObj.PrtEnt13.MarcaMensaje = 1;

            if (!string.IsNullOrEmpty(UTILES.QuitaEspaciosEnBlanco(initObj.PrtEnt13.Lb_fecing.Text)))
            {
                initObj.PrtEnt13.MarcaMensaje = 0;

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_PRTGLOB.TitTasas,
                    Text = " No se puede modificar una Tasa que ya ha sido ingresada.",
                    Type = TipoMensaje.Informacion
                });
                return;
            }

            if (initObj.PrtEnt13.fecha.Visible)
            {
                if (initObj.PrtEnt13.fecha.Enabled)
                {
                    if (string.IsNullOrEmpty(initObj.PrtEnt13.fecha.Text))
                    {
                        fech = VB6Helpers.Format(DateTime.Now, "ddMMyyyy");
                        f = VB6Helpers.Format(DateTime.Now, "yyyyMMdd");
                    }
                    else
                    {
                        a = initObj.PrtEnt13.fecha.Text;
                        fech = VB6Helpers.Format(initObj.PrtEnt13.fecha.Text, "ddMMyyyy");
                        if (!string.IsNullOrEmpty(fech))
                        {
                            f = VB6Helpers.Format(a, "yyyyMMdd");
                            hoy = VB6Helpers.Format(DateTime.Now, "yyyyMMdd");
                            if (String.CompareOrdinal(f, hoy) < 0)
                            {
                                initObj.PrtEnt13.MarcaMensaje = 0;

                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Title = T_PRTGLOB.TitTasas,
                                    Text = " Debe ingresar una fecha mayor o igual a la actual.",
                                    Type = TipoMensaje.Informacion

                                });
                                initObj.PrtEnt13.fecha.Text = string.Empty;
                                return;
                            }
                        }
                        else
                        {
                            initObj.PrtEnt13.MarcaMensaje = 0;

                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Title = T_PRTGLOB.TitTasas,
                                Text = " Reingresar Fecha: Día o mes no válido.",
                                Type = TipoMensaje.Informacion
                            });
                            return;
                        }
                    }

                    for (k = 0; k <= initObj.PrtEnt06.lista_com.Items.Count - 2; k += 1)
                    {
                        linea = UTILES.QuitaEspaciosEnBlanco(initObj.PrtEnt06.lista_com.Items[k].Value);
                        if (!string.IsNullOrEmpty(linea))
                            linea += VB6Helpers.Chr(9) + initObj.PrtEnt06.lista_com.Items[k].Tag;
                        v = linea.InStr("(", 1, StringComparison.CurrentCulture);
                        if (v != 0)
                        {
                            feaux = linea.Right((linea.Len() - v));
                            feaux = UTILES.copiardestring(feaux, ")", 1);
                        }
                        else
                        {
                            feaux = UTILES.copiardestring(linea, VB6Helpers.Chr(9), 5);
                        }
                        fech_a = feaux.Mid(7, 4);
                        fech_m = feaux.Mid(4, 2);
                        fech_d = feaux.Mid(1, 2);
                        feaux = fech_a + fech_m + fech_d;
                        if (feaux == f && initObj.PrtEnt06.lista_com.ListIndex != k)
                        {
                            initObj.PrtEnt13.MarcaMensaje = 0;

                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Title = T_PRTGLOB.TitTasas,
                                Text = " Alguna Tasa especial ya tiene la fecha de vigencia indicada, por lo tanto es imposible aceptarla.",
                                Type = TipoMensaje.Informacion
                            });
                            return;
                        }
                    }
                }
            }
            else
            {
                fech = VB6Helpers.Format(initObj.PrtEnt13.fecha.Text, "ddMMyyyy"); //initObj.PrtEnt13.fecha.ToStr();
                f = VB6Helpers.Format(DateTime.Now, "yyyyMMdd");
            }

            if (initObj.PrtEnt13.Frame1.Enabled)
            {
                t = initObj.PrtEnt13.tasa.Text;
                if (!string.IsNullOrEmpty(t))
                    t = Format.FormatCurrency(Format.StringToDouble(initObj.PrtEnt13.tasa.Text), T_PRTGLOB.formato_tasa);
                else
                {
                    if (initObj.PrtEnt13.fijo.Checked.ToInt() == 0)
                    {
                        initObj.PrtEnt13.MarcaMensaje = 0;

                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Title = T_PRTGLOB.TitTasas,
                            Text = " De acuerdo a su elección debe ingresar una tasa.",
                            Type = TipoMensaje.Informacion
                        });
                        return;
                    }
                    t = T_PRTGLOB.guiones_tasa;
                }
                tas = Format.StringToDouble(Format.StringToDouble(initObj.PrtEnt13.tasa.Text)); //initObj.PrtEnt13.tasa.Text.ToDbl(); //PRTGLOB.ValTexto(initObj.PrtEnt13.tasa.Text);
                lee_limitesSy(initObj, uow, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, tas);


                mon = initObj.PrtEnt13.monto.Text;
                if (!string.IsNullOrEmpty(mon))
                    mon = Format.FormatCurrency(Format.StringToDouble(mon), T_PRTGLOB.formato);//VB6Helpers.Format(mon, T_PRTGLOB.formato); //UTILES.forma(UTILES.unformat(mon), T_PRTGLOB.formato);
                else
                    mon = T_PRTGLOB.guiones;

                min = initObj.PrtEnt13.minimo.Text;
                if (!string.IsNullOrEmpty(min))
                    min = Format.FormatCurrency(Format.StringToDouble(min), T_PRTGLOB.formato); //VB6Helpers.Format(min, T_PRTGLOB.formato);  //UTILES.forma(UTILES.unformat(min), T_PRTGLOB.formato);               
                else
                    min = T_PRTGLOB.guiones;

                max = initObj.PrtEnt13.maximo.Text;
                if (!string.IsNullOrEmpty(max))
                    max = Format.FormatCurrency(Format.StringToDouble(max), T_PRTGLOB.formato);  //VB6Helpers.Format(max, T_PRTGLOB.formato); //UTILES.forma(UTILES.unformat(max), T_PRTGLOB.formato);
                else
                    max = T_PRTGLOB.guiones;

                //l = UTILES.EspaciosAlineado(t, 50) + VB6Helpers.Chr(9) + UTILES.EspaciosEnBlancoRight(mon, 55);
                //l = String.Format("{0,-35}", t) + VB6Helpers.Chr(9) + String.Format("{0,-35}", mon);
                l = UTILES.EspaciosAlineado(t, 75) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(mon, 75);

                fech_a = f.Mid(1, 4);
                fech_m = f.Mid(5, 2);
                fech_d = f.Mid(7, 2);
                fech = fech_d + "/" + fech_m + "/" + fech_a;
                l = l + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(min, 75) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(max, 75); //+ VB6Helpers.Chr(9) + 
                initObj.PrtEnt06.lista_com.Items[initObj.PrtEnt06.lista_com.ListIndex].Value = l;
                initObj.PrtEnt06.lista_com.Items[initObj.PrtEnt06.lista_com.ListIndex].Tag = fech + VB6Helpers.Chr(9) + VB6Helpers.Format(initObj.PrtEnt13.fijo.Checked.ToInt(), String.Empty);

                ind = initObj.PrtEnt06.lista_com.ListIndex;
                if (ind == initObj.PrtEnt06.lista_com.Items.Count - 1)
                {
                    initObj.PrtEnt06.lista_com.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                    {
                        Data = (ind + 1),
                        ID = (ind + 1).ToStr(),
                        Value = string.Empty
                    });
                }
            }
            else
            {
                ff = fech.Left(2) + "/" + fech.Mid(3, 2) + "/" + fech.Right(4);
                l = "Datos del Manual de Tarifas a partir de (" + ff + ")";
                initObj.PrtEnt06.lista_com.Items[initObj.PrtEnt06.lista_com.ListIndex].Value = l;
                ind = initObj.PrtEnt06.lista_com.ListIndex;
                if (ind == initObj.PrtEnt06.lista_com.Items.Count - 1)
                {
                    initObj.PrtEnt06.lista_com.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                    {
                        Data = ind + 1,
                        Value = string.Empty
                    });
                }
            }
            T_PRTGLOB.modifico_tasa = 1;


        }
        public static bool Frame(InitializationObject initObj, bool EstaHabilitado)
        {
            bool estado = EstaHabilitado;
            initObj.PrtEnt13.fijo.Enabled = estado;
            initObj.PrtEnt13.tasa.Enabled = estado;
            initObj.PrtEnt13.minimo.Enabled = estado;
            initObj.PrtEnt13.monto.Enabled = estado;
            initObj.PrtEnt13.maximo.Enabled = estado;
            return estado;
        }
        public static void Cancelar_Click(InitializationObject initObj)
        {

            initObj.PaginaWebQueAbrir = "TasasEspecialesParticipante";

        }
        public static void fijo_Click(InitializationObject initObj)
        {
            if (initObj.PrtEnt13.fijo.Checked.ToInt() == 1)
            {
                initObj.PrtEnt13.tasa.Text = string.Empty;
                initObj.PrtEnt13.tasa.Enabled = false;
                initObj.PrtEnt13.maximo.Text = string.Empty;
                initObj.PrtEnt13.maximo.Enabled = false;
            }
            else
            {
                initObj.PrtEnt13.tasa.Enabled = true;
                initObj.PrtEnt13.maximo.Enabled = true;
            }
        }
        private static void lee_limitesSy(InitializationObject initObj, UnitOfWorkCext01 uow, string sist, string prod, string etapa, double tasa)
        {
            double tasa_max = 0.0;
            double tasa_min = 0.0;
            int fij = 0;
            initObj.PrtEnt13.MarcaMensaje = 0;
            try
            {
                var R = uow.SceRepository.sce_mta1_s05_MS(sist, prod, etapa);
                MODGSYB.SyContador = 3;

                if (R == null)
                {
                    fij = 0;
                    tasa_min = 0;
                    tasa_max = 0;
                }
                else
                {
                    fij = R.mtofij.ToInt();
                    tasa_min = R.tasmin.ToDbl();
                    tasa_max = R.tasmax.ToDbl();
                }


                if (fij == 0)
                {
                    if (tasa < tasa_min)
                    {
                        initObj.PrtEnt13.MarcaMensaje = 1;

                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Title = T_PRTGLOB.TitTasas,
                            Text = " Asignó una tasa inferior a la del manual de tarifa.",
                            Type = TipoMensaje.Informacion
                        });
                         return;

                    }
                    else if (tasa > tasa_max)
                    {
                        initObj.PrtEnt13.MarcaMensaje = 1;

                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Title = T_PRTGLOB.TitTasas,
                            Text = " Asignó una tasa superior a la del manual de tarifa.",
                            Type = TipoMensaje.Informacion
                        });
                        return;
                    }

                }

                //initObj.PrtEnt13.MarcaMensaje = 1;

            }
            catch
            {
                initObj.PrtEnt13.MarcaMensaje = 1;

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = T_PRTGLOB.TitTasas,
                    Text = " Error en lectura de manual tarifa para comisiones.",
                    Type = TipoMensaje.Informacion
                });

                return;

            }

        }
        public static void manual_Click(InitializationObject initObj)
        {
            if (initObj.PrtEnt13.manual.Checked)
            {
                initObj.PrtEnt13.fijo.Checked = false;
                initObj.PrtEnt13.tasa.Text = string.Empty;
                initObj.PrtEnt13.monto.Text = string.Empty;
                initObj.PrtEnt13.minimo.Text = string.Empty;
                initObj.PrtEnt13.maximo.Text = string.Empty;

                initObj.PrtEnt13.Frame1.Enabled = false;
                Frame(initObj, false);
            }
            else
            {
                initObj.PrtEnt13.Frame1.Enabled = true;
                Frame(initObj, true);
            }
        }
        public static void maximo_LostFocus(InitializationObject initObj)
        {
            string min = string.Empty;
            string max = string.Empty;
            double a = 0;
            initObj.PrtEnt13.idEstadoMonto = 0;

            a = 0;//initObj.PrtEnt13.maximo.Text.ToVal(); //Format.FormatCurrency(Format.StringToDouble(max), T_PRTGLOB.formato); //initObj.PrtEnt13.maximo.Text;//UTILES.mascaralost(initObj.PrtEnt13.maximo.Text);
            if (a == 0)
            {
                if (initObj.PrtEnt13.fijo.Checked.ToInt() == 0)
                {
                    max = initObj.PrtEnt13.maximo.Text;
                    min = initObj.PrtEnt13.minimo.Text;
                    if (!string.IsNullOrEmpty(max) && !string.IsNullOrEmpty(min))
                    {
                        if (max.ToVal() < min.ToVal())
                        {
                            //initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            //{
                            //    Title = T_PRTGLOB.TitTasas,
                            //    Text = " Monto mínimo es mayor que monto máximo.",
                            //    Type = TipoMensaje.Informacion
                            //});
                            initObj.PrtEnt13.idEstadoMonto = 1;
                            return;
                        }
                    }
                }
            }
        }
        public static void minimo_LostFocus(InitializationObject initObj)
        {
            string min = string.Empty;
            string max = string.Empty;
            double a = 0;
            initObj.PrtEnt13.idEstadoMonto = 0;
            a = 0;//initObj.PrtEnt13.minimo.Text.ToVal(); // UTILES.mascaralost(minimo);

            if (a == 0)
            {
                if (initObj.PrtEnt13.fijo.Checked.ToInt() == 1)
                {
                    initObj.PrtEnt13.maximo.Enabled = true;
                    initObj.PrtEnt13.maximo.Text = initObj.PrtEnt13.minimo.Text;
                    initObj.PrtEnt13.maximo.Enabled = false;
                }
                else
                {
                    max = initObj.PrtEnt13.maximo.Text;
                    min = initObj.PrtEnt13.minimo.Text;
                    if (!string.IsNullOrEmpty(max) && !string.IsNullOrEmpty(min))
                    {
                        if (max.ToVal() < min.ToVal())
                        {
                            //initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            //{
                            //    Title = T_PRTGLOB.TitTasas,
                            //    Text = " Monto mínimo es mayor que monto máximo.",
                            //    Type = TipoMensaje.Informacion
                            initObj.PrtEnt13.idEstadoMonto = 1;
                            //});
                            return;
                        }
                    }
                }
            }
        }
        public static void monto_LostFocus(InitializationObject initObj)
        {
            string t = string.Empty;
            string m = string.Empty;
            int a = 0;
            initObj.PrtEnt13.idEstadoMonto = 0;
            a = 0;//initObj.PrtEnt13.monto.Text.ToInt(); //UTILES.mascaralost(monto);
            if (a == 0)
            {
                m = initObj.PrtEnt13.monto.Text;
                if (!string.IsNullOrEmpty(m))
                {
                    //m = UTILES.unformat(m);
                    t = initObj.PrtEnt13.MontoTagFrm.Tag.ToStr();
                    if (m.ToVal() <= t.ToVal())
                    {
                        initObj.PrtEnt13.idEstadoMonto = 1;
                        //initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        //{
                        //    Title = T_PRTGLOB.TitTasas,
                        //    Text = " Monto ingresado fuera de rango",
                        //    Type = TipoMensaje.Informacion
                        //});
                        return;
                    }

                }
            }
        }
        public static void tasa_LostFocus(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            double t = 0.0;
            string tas = string.Empty;
            int a = 0;
            //Se cambia esto, ya que en legacy, la funcion mascaralost devuelve 0 si no se produce error, en nuestro caso no es necesario.
            //a = initObj.PrtEnt13.tasa.Text.ToInt(); //UTILES.mascaralost(tasa);
            ////
            //if (a == 0)
            //{
            if (initObj.PrtEnt13.fijo.Checked.ToInt() == 0)
            {
                if (string.IsNullOrEmpty(initObj.PrtEnt13.tasa.Text))
                {
                    return;
                }
                tas = initObj.PrtEnt13.tasa.Text;
                //Se necesita cambiar el valor que viene con separador de miles y decimales a un número
                //tas = UTILES.unformat(tas);
                tas = float.Parse(tas).ToString();
                if (tas.ToVal() == 0)
                {
                    initObj.PrtEnt13.maximo.Text = string.Empty;
                    initObj.PrtEnt13.minimo.Text = string.Empty;
                    initObj.PrtEnt13.maximo.Enabled = false;
                    initObj.PrtEnt13.minimo.Enabled = false;
                }
                else
                {
                    t = tas.ToVal();
                    lee_limitesSy(initObj, uow, T_PRTGLOB.sistema, T_PRTGLOB.producto, T_PRTGLOB.etap, t);
                    initObj.PrtEnt13.maximo.Enabled = true;
                    initObj.PrtEnt13.minimo.Enabled = true;
                }
                //}
            }
        }

    }
}
