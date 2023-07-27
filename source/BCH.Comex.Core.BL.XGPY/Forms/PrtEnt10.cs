using BCH.Comex.Core.BL.XGPY.Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XGPY.Forms
{
    public static class PrtEnt10
    {
        public static void Form_Load(InitializationObject InitObject)//, bool nacional)
        {
            int i = 0;
            
            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
                InitObject.PrtEnt10.CuentaBae.Enabled = false;

            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsBanco))
            {
                InitObject.PrtEnt10.cbo_cta.Visible = false;
                InitObject.PrtEnt10.prtcuenta.Visible = true;
            }
            else
            {
                InitObject.PrtEnt10.cbo_cta.Visible = true;
                InitObject.PrtEnt10.prtcuenta.Visible = false;
            }

        }
        public static void Aceptar_Click(InitializationObject InitObject)
        {
            int t = 0;
            string espec = string.Empty;
            int P = 0;
            string s = string.Empty;
            string ind = string.Empty;
            string msj = string.Empty;
            string bae_bch = string.Empty;
            int fi = 0;
            int Flag = 0;
            bool si = false;
            string cuenta = string.Empty;
            string cta = string.Empty;
            string lista = string.Empty;
            int l = 0;
            string li = string.Empty;
            int indice = 0;
            int m = 0;
            string c = string.Empty;
            string est = string.Empty;
            string listas = string.Empty;
            int k = 0;
            string a = string.Empty;
            string ofic = string.Empty;
            int fila = -1;
            ofic = new string((char)0, 255);

            //if (T_PRTGLOB.Pertenece == 0)
            //{
            //    InitObject.Mdi_Principal.Archivo[2].Enabled = false;  // menú salvar
            //    InitObject.Mdi_Principal.Archivo[4].Enabled = false;  // menú eliminar
            //    InitObject.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = false;      // boton
            //}
            //else
            //{
            //    InitObject.Mdi_Principal.Archivo[2].Enabled = true;  // menú salvar
            //    InitObject.Mdi_Principal.Archivo[4].Enabled = true;  // menú eliminar
            //    InitObject.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = true;      // boton salvar o guardar
            //}

            InitObject.PrtEnt10.MarcaMensaje = 0;

            if (string.IsNullOrEmpty(PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text.Trim())) || (PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim()) == "00000000" || PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim()) == "0000000000"))
            {
                InitObject.PrtEnt10.MarcaMensaje = 1;

                InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = " Imposible que la cuenta corriente/línea de crédito sea nula.",
                    Title = T_PRTGLOB.TitCuentas,
                    Type = TipoMensaje.Informacion
                });

                return;
            }
            else
            {
                // Validación Moneda Nacional.-
                if (InitObject.PrtEnt10.CuentaBae.Checked == System.Windows.Forms.CheckState.Unchecked.ToBool())
                {
                    if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && (!string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber)))
                    {
                        //if (InitObject.PrtEnt10.prtcuenta.Text.Length < 9 || InitObject.PrtEnt10.prtcuenta.Text.Length > 12)
                        if (PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text.Trim()).Length < 9 || PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text.Trim()).Length > 12)
                        {
                            InitObject.PrtEnt10.MarcaMensaje = 1;

                            InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = " La Cuenta Global debe contener entre 9 y 12 digitos. Debera ingresar la cuenta nuevamente.",
                                Title = T_PRTGLOB.TitCuentas,
                                Type = TipoMensaje.Informacion
                            });

                            return;
                        }
                    }
                    else
                    {
                        if (PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text.Trim()).Length != 10 && !InitObject.PrtEnt10.Combo1.Enabled)
                        {
                            InitObject.PrtEnt10.MarcaMensaje = 1;

                            InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = " La cuenta debe contener 10 dígitos en total. Deberá ingresar la cuenta nuevamente.",
                                Title = T_PRTGLOB.TitCuentas,
                                Type = TipoMensaje.Informacion
                            });

                            return;
                        }

                        // Validación Moneda Extranjera.-
                        if (InitObject.PrtEnt10.Combo1.Enabled)
                        {
                            if (PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text.Trim()).Length != 11)
                            {
                                InitObject.PrtEnt10.MarcaMensaje = 1;

                                InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = " La cuenta debe contener 11 dígitos en total. Deberá ingresar la cuenta nuevamente.",
                                    Title = T_PRTGLOB.TitCuentas,
                                    Type = TipoMensaje.Informacion
                                });

                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text.Trim()).Length != 10 && !InitObject.PrtEnt10.Combo1.Enabled)
                    {
                        InitObject.PrtEnt10.MarcaMensaje = 1;

                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " La cuenta debe contener 10 dígitos en total. Deberá ingresar la cuenta nuevamente.",
                            Title = T_PRTGLOB.TitCuentas,
                            Type = TipoMensaje.Informacion
                        });
                        return;
                    }

                    if (InitObject.PrtEnt10.Combo1.Enabled && InitObject.PrtEnt10.Combo1.Text == "")
                    {
                        InitObject.PrtEnt10.MarcaMensaje = 1;

                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " Debe elegir una Moneda.",
                            Title = T_PRTGLOB.TitCuentas,
                            Type = TipoMensaje.Informacion
                        });

                        return;
                    }

                    if (PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text.Trim()).Length != 11 && InitObject.PrtEnt10.Combo1.Enabled)
                    {
                        InitObject.PrtEnt10.MarcaMensaje = 1;

                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " La cuenta debe contener 11 dígitos en total. Deberá ingresar la cuenta nuevamente.",
                            Title = T_PRTGLOB.TitCuentas,
                            Type = TipoMensaje.Informacion
                        });

                        return;
                    }
                }

                if (InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente)
                {
                    if (T_PRTGLOB.primeralista != 0)
                    {
                        if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
                            cuenta = "0-" + InitObject.PrtEnt10.prtcuenta.Text;
                        else
                            cuenta = InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim();

                        Flag = 2;
                    }
                    else
                    {
                        if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
                            cuenta = InitObject.PrtEnt10.prtcuenta.Text.Left(1) + "-" + InitObject.PrtEnt10.prtcuenta.Text.Right(12);
                        else
                            cuenta = InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim();

                        Flag = 1;
                    }
                }

                if (InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.individuo)
                {
                    if (T_PRTGLOB.primeralista != 0)
                    {
                        if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
                            cuenta = "0-" + InitObject.PrtEnt10.prtcuenta.Text;
                        else
                            cuenta = InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim();

                        Flag = 2;
                    }
                    else
                    {
                        if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
                            cuenta = InitObject.PrtEnt10.prtcuenta.Text.Left(1) + "-" + InitObject.PrtEnt10.prtcuenta.Text.Right(12);
                        else
                            cuenta = InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim();

                        Flag = 1;
                    }
                }

                if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsBanco))
                {
                    // VALIDA SI EXISTE LA CUENTA
                    if (T_PRTGLOB.primeralista != 0)
                    {
                        fi = MODPRTY.SyGet_Ofi(InitObject, cuenta, Flag);
                        if (fi == 0)
                        {
                            InitObject.PrtEnt10.MarcaMensaje = 1;

                            InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = " No existe Aplicación V_CtaCte.exe. Se asumira que es oficina BCH. Reporte este problema.",
                                Title = T_PRTGLOB.TitCuentas,
                                Type = TipoMensaje.Informacion
                            });

                            bae_bch = "BCH";
                        }

                        if (fi == 1)
                            bae_bch = "BAE";

                        if (fi == 2)
                            bae_bch = "BCH";

                        if (fi == 3)
                        {
                            InitObject.PrtEnt10.MarcaMensaje = 1;

                            InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = " Número de Cuenta Inexistente.",
                                Title = T_PRTGLOB.TitCuentas,
                                Type = TipoMensaje.Informacion
                            });
                            return;
                        }

                        if (InitObject.PrtEnt10.CuentaBae.Checked == false && fi == 1)
                        {
                            InitObject.PrtEnt10.MarcaMensaje = 1;

                            InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = " Número de Cuenta no es BCH.",
                                Title = T_PRTGLOB.TitCuentas,
                                Type = TipoMensaje.Informacion
                            });

                            return;
                        }

                        if (InitObject.PrtEnt10.CuentaBae.Checked == true && fi == 2)
                        {
                            InitObject.PrtEnt10.MarcaMensaje = 1;

                            InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = " Número de Cuenta no es BAE.",
                                Title = T_PRTGLOB.TitCuentas,
                                Type = TipoMensaje.Informacion
                            });

                            return;
                        }
                    }
                }
            }

            if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsBanco))
            {
                //lar_ofi = UTILES.GetPrivateProfileString("Datos", "Oficina", "", ofic, ofic.Len(), "c:\\data\\v_ctacte\\v_ctacte.ini");
                //if (lar_ofi == 0)
                //{
                //    System.Windows.Forms.MessageBox.Show("Error Oficina  no encontrada.", "", MessageBoxButtons.OK);
                //    return;
                //}
                //else
                //{
                //    ofic = ofic.Left(lar_ofi);
                //}

                InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, ofic);
            }

            //cuenta = InitObject.PrtEnt10.prtcuenta.Text;
            cuenta = PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text).Trim();

            switch (InitObject.PRTGLOB.Party.tipo)
            {
                case T_PRTGLOB.tipo_banco:
                    if (T_PRTGLOB.primeralista != 0)
                    {
                        li = InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Value;   //UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Value);

                        if (!string.IsNullOrEmpty(li))
                            li += InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Tag;

                        if (UTILES.copiardestring(li, VB6Helpers.Chr(9), 1) == "")
                        {
                            for (l = 0; l <= InitObject.PrtEnt08.Lista1.Items.Count - 1; l += 1)
                            {
                                lista = InitObject.PrtEnt08.Lista1.Items[l].ToStr();
                                //lista = PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt08.Lista1.Items[l].ToStr());
                                cta = PRTYENT.QuitarMascaraCuenta(UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1));

                                if (cuenta == cta)
                                {
                                    InitObject.PrtEnt10.MarcaMensaje = 1;

                                    InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                    {
                                        Text = " Cuenta Corriente ya existe: Debe ingresar otra cuenta.",
                                        Title = T_PRTGLOB.TitCuentas,
                                        Type = TipoMensaje.Informacion
                                    });

                                    return;
                                }
                            }
                        }
                    }
                    break;

                case T_PRTGLOB.tipo_cliente:
                    if (T_PRTGLOB.primeralista != 0)
                    {
                        if (InitObject.PrtEnt08.Lista1.Items.Count > 1)
                        {
                            //fila = (short)InitObject.PrtEnt08.Lista1.get_ItemData_((short)InitObject.PrtEnt08.Lista1.ListIndex);
                            fila = (short)InitObject.PrtEnt08.Lista1.ListIndex;
                            li = InitObject.PrtEnt08.Lista1.Items[fila].Value;
                            if (!string.IsNullOrEmpty(li))
                                li += InitObject.PrtEnt08.Lista1.Items[fila].Tag; //Esconder columna 26-02-2016
                        }

                        //if (PRTYENT.QuitarMascaraCuenta(UTILES.copiardestring(li, VB6Helpers.Chr(9), 1)) == "")

                        if (string.IsNullOrEmpty(PRTYENT.QuitarMascaraCuenta(UTILES.copiardestring(li, VB6Helpers.Chr(9), 1)).Trim()))
                        {
                            for (l = 0; l <= InitObject.PrtEnt08.Lista1.Items.Count - 1; l += 1)
                            {
                                lista = InitObject.PrtEnt08.Lista1.Items[l].Value.ToStr();
                                cta = PRTYENT.QuitarMascaraCuenta(UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1));

                                if (cuenta.Trim() == cta.Trim())
                                {
                                    InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                    {
                                        Text = " Cuenta Corriente ya existe: Debe ingresar otra cuenta.",
                                        Title = T_PRTGLOB.TitCuentas,
                                        Type = TipoMensaje.Informacion
                                    });

                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (InitObject.PrtEnt08.Lista2.Items.Count > 1)
                        {
                            fila = (short)InitObject.PrtEnt08.Lista2.ListIndex;
                            //fila = (short)InitObject.PrtEnt08.Lista2.get_ItemData_((short)InitObject.PrtEnt08.Lista2.ListIndex);
                            li = UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista2.Items[fila].Value);

                            if (!string.IsNullOrEmpty(li))
                                li += InitObject.PrtEnt08.Lista2.Items[fila].Tag; //Esconder columna 26-02-2016
                        }

                        if (UTILES.copiardestring(li, VB6Helpers.Chr(9), 1) == "")
                        {
                            for (l = 0; l <= InitObject.PrtEnt08.Lista2.Items.Count - 1; l += 1)
                            {
                                lista = InitObject.PrtEnt08.Lista2.Items[l].Value.ToStr();
                                cta = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);

                                if (cuenta.Trim() == cta.Trim())
                                {
                                    InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                    {
                                        Text = " Cuenta Corriente ya existe: Debe ingresar otra cuenta.",
                                        Title = T_PRTGLOB.TitCuentas,
                                        Type = TipoMensaje.Informacion
                                    });

                                    return;
                                }
                            }
                        }
                    }
                    break;
            }

            if (InitObject.PrtEnt10.Combo1.Visible)
            {
                if (InitObject.PrtEnt10.Combo1.SelectedValue == -1)
                {
                    InitObject.PrtEnt10.MarcaMensaje = 1;

                    InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe seleccionar una moneda.",
                        Title = T_PRTGLOB.TitCuentas,
                        Type = TipoMensaje.Informacion
                    });

                    return;
                }
            }
            else
            {
                si = false;

                for (l = 0; l <= InitObject.PrtEnt10.listmon.Items.Count - 1; l += 1)
                {
                    if (InitObject.PrtEnt10.listmon.ListIndex == l)
                    {
                        si = true;
                        break;
                    }
                }
                if (!si)
                {
                    InitObject.PrtEnt10.MarcaMensaje = 1;

                    InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Debe seleccionar por lo menos una moneda.",
                        Title = T_PRTGLOB.TitCuentas,
                        Type = TipoMensaje.Informacion
                    });

                    return;
                }
            }

            if (InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco && !T_PRTGLOB.primeralista.ToBool())
            {
                cta = InitObject.PrtEnt10.prtcuenta.Text;
                indice = (int)InitObject.PrtEnt08.Lista2.ListIndex;

                for (l = 0; l <= InitObject.PrtEnt10.listmon.Items.Count - 1; l += 1)
                {
                    if (InitObject.PrtEnt10.listmon.ListIndex == l)
                    {
                        for (m = 0; m <= InitObject.PrtEnt08.Lista2.Items.Count - 1; m += 1)
                        {
                            lista = InitObject.PrtEnt08.Lista2.Items[m].ToStr();
                            c = UTILES.copiardestring(lista, VB6Helpers.Chr(9), 1);

                            if (cta == c)
                            {
                                if (indice != m)
                                {
                                    if (UTILES.copiardestring(lista, VB6Helpers.Chr(9), 2) == (InitObject.PrtEnt10.Combo1.get_ItemData_(l)).ToString())
                                    {
                                        InitObject.PrtEnt10.MarcaMensaje = 1;
                                        msj = "La línea ya tiene asociada la moneda " + (InitObject.PrtEnt10.Combo1.get_ItemData_(l)).ToString();

                                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                                        {
                                            Text = msj,
                                            Title = T_PRTGLOB.TitCuentas,
                                            Type = TipoMensaje.Informacion
                                        });

                                        InitObject.PrtEnt10.listmon.ListIndex = l;//(l, false);
                                        InitObject.PrtEnt10.Combo1.SelectedValue = InitObject.PrtEnt10.Combo1.get_ItemData_(l);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            switch (InitObject.PRTGLOB.Party.tipo)
            {
                case T_PRTGLOB.tipo_cliente:
                    if (InitObject.PrtEnt10._prtactiva_0.Checked == true)
                    {
                        if (InitObject.PrtEnt10._prtactiva_1.Checked == true)
                            est = "Activa";
                        else
                            est = "No Activa";
                    }
                    else
                        est = "Cerrada";

                    if (!T_PRTGLOB.FlagCuentas.ToBool())
                    {
                        T_PRTGLOB.FlagCuentas = true.ToInt();

                        if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                            InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        
                    }

                    if (T_PRTGLOB.primeralista != 0)
                    {
                        //listas = InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.SelectedValue].Value;
                        if (InitObject.PrtEnt08.Lista1.Items.Count > 1)
                        {
                            //fila = (short)InitObject.PrtEnt08.Lista1.get_ItemData_((short)InitObject.PrtEnt08.Lista1.ListIndex);
                            fila = (short)InitObject.PrtEnt08.Lista1.ListIndex;
                            listas = InitObject.PrtEnt08.Lista1.Items[fila].Value;

                            if (!string.IsNullOrEmpty(listas))
                                listas += InitObject.PrtEnt08.Lista1.Items[fila].Tag;
                        }

                        a = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 4);

                        switch (a.ToInt())
                        {
                            case T_PRTGLOB.eliminado_nuevo:
                                if (string.IsNullOrEmpty(UTILES.copiardestring(listas, VB6Helpers.Chr(9), 1)))
                                {
                                    a = T_PRTGLOB.nuevo.ToString();
                                    ind = PRTYENT.Siguiente(InitObject, "ctaclie").ToString();
                                    k = InitObject.PRTGLOB.ctaclie_aux.GetUpperBound(0);
                                    InitObject.PRTGLOB.ctaclie_aux[k] = new cuenta_indice();
                                    InitObject.PRTGLOB.ctaclie_aux[k].cuenta = InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim();
                                    InitObject.PRTGLOB.ctaclie_aux[k].indice = ind.ToInt();
                                }
                                break;

                            case T_PRTGLOB.leido:
                                a = T_PRTGLOB.modificado.ToString();
                                ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                                break;

                            default:
                                ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                                break;
                        }

                        //s = InitObject.PrtEnt10.prtcuenta.Text + VB6Helpers.Chr(9) + UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, InitObject.PrtEnt10.oficina.Text, 10) + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + bae_bch; //Respaldo original
                        //if (!string.IsNullOrEmpty(InitObject.PrtEnt10.oficina.Text == null ? string.Empty : InitObject.PrtEnt10.oficina.Text.Trim()))
                        //    cantidadEspaciosEnBlanco = 100;
                        //else
                        //    cantidadEspaciosEnBlanco = 200;

                        //s = UTILES.EspaciosAlineadoRight(InitObject.PrtEnt10.prtcuenta.Text, cantidadEspaciosEnBlanco) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoRight(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, InitObject.PrtEnt10.oficina.Text.ToUpper(), 10), 100) + VB6Helpers.Chr(9) + est;//Esconder columna 26-02-2016
                        //s = UTILES.EspaciosAlineado(InitObject.PrtEnt10.prtcuenta.Text, 100) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, (InitObject.PrtEnt10.oficina.Text??string.Empty).ToUpper(), 10), 100) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(est,100);//Esconder columna 26-02-2016
                        s = UTILES.EspaciosAlineadoMonoSpace("",7) + UTILES.EspaciosAlineadoMonoSpace(InitObject.PrtEnt10.prtcuenta.Text, 50) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, (InitObject.PrtEnt10.oficina.Text ?? string.Empty).ToUpper(), 10), 40) + VB6Helpers.Chr(9) + est;//Esconder columna 26-02-2016
                        InitObject.PrtEnt08.Lista1.Items[InitObject.PrtEnt08.Lista1.ListIndex].Value = s;
                        InitObject.PrtEnt08.Lista1.Items[InitObject.PrtEnt08.Lista1.ListIndex].Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + bae_bch;
                        InitObject.PrtEnt08.Lista1.Items[InitObject.PrtEnt08.Lista1.ListIndex].Data = InitObject.PrtEnt10.Combo1.SelectedValue.GetValueOrDefault();
                     
                        if (InitObject.PrtEnt08.Lista1.ListIndex == InitObject.PrtEnt08.Lista1.Items.Count-1)// - 1)
                        {
                            s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                            // InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Value = s, Data = -1 });
                            InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = InitObject.PrtEnt10.Combo1.Items[InitObject.PrtEnt10.Combo1.SelectedValue.GetValueOrDefault()].Data, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                        }
                    }
                    else
                    {
                        //listas = InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.SelectedValue].Value;
                        if (InitObject.PrtEnt08.Lista2.Items.Count > 1)
                        {
                            //fila = (short)InitObject.PrtEnt08.Lista2.get_ItemData_((short)InitObject.PrtEnt08.Lista2.ListIndex);
                            fila = (short)InitObject.PrtEnt08.Lista2.ListIndex;
                            listas = UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista2.Items[fila].Value);
                            if (!string.IsNullOrEmpty(listas))
                                listas += InitObject.PrtEnt08.Lista2.Items[fila].Tag;
                        }

                        a = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                        
                        switch (a.ToInt())
                        {
                            case T_PRTGLOB.eliminado_nuevo:

                                if (PRTYENT.QuitarMascaraCuenta(UTILES.copiardestring(listas, VB6Helpers.Chr(9), 1)) == "")
                                {
                                    a = T_PRTGLOB.nuevo.ToString();
                                    ind = PRTYENT.Siguiente(InitObject, "ctaclie").ToString();
                                    k = InitObject.PRTGLOB.ctaclie_aux.GetUpperBound(0);
                                    InitObject.PRTGLOB.ctaclie_aux[k] = new cuenta_indice();
                                    InitObject.PRTGLOB.ctaclie_aux[k].cuenta = InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim();
                                    InitObject.PRTGLOB.ctaclie_aux[k].indice = ind.ToInt();
                                }
                                break;

                            case T_PRTGLOB.leido:
                                a = T_PRTGLOB.modificado.ToString();
                                ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 6);
                                break;

                            default:
                                a = T_PRTGLOB.modificado.ToString();
                                ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 6);
                                break;
                        }

                        //if (!string.IsNullOrEmpty(InitObject.PrtEnt10.oficina.Text == null ? string.Empty : InitObject.PrtEnt10.oficina.Text.Trim()))
                        //    cantidadEspaciosEnBlanco = 100;
                        //else
                        //    cantidadEspaciosEnBlanco = 200;

                        //s = UTILES.EspaciosEnBlancoRight(InitObject.PrtEnt10.prtcuenta.Text,cantidadEspaciosEnBlanco) + VB6Helpers.Chr(9) + UTILES.EspaciosEnBlancoRight(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, InitObject.PrtEnt10.oficina.Text.ToUpper(), 10),80) + VB6Helpers.Chr(9) + InitObject.PrtEnt10.Combo1.Items[(int)InitObject.PrtEnt10.Combo1.SelectedValue].Value + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind;
                        //s = UTILES.EspaciosAlineado(InitObject.PrtEnt10.prtcuenta.Text, 100) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, (InitObject.PrtEnt10.oficina.Text ?? string.Empty).ToUpper(), 10), 100) + VB6Helpers.Chr(9) + InitObject.PrtEnt10.Combo1.Text + VB6Helpers.Chr(9) + est;
                       
                        s = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(InitObject.PrtEnt10.prtcuenta.Text, 33) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, (InitObject.PrtEnt10.oficina.Text ?? string.Empty).ToUpper(), 10), 35) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(InitObject.PrtEnt10.Combo1.Text.Trim(),37) + VB6Helpers.Chr(9) + est;
                        InitObject.PrtEnt08.Lista2.Items[InitObject.PrtEnt08.Lista2.ListIndex].Value = s;
                        InitObject.PrtEnt08.Lista2.Items[InitObject.PrtEnt08.Lista2.ListIndex].Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind;
                        InitObject.PrtEnt08.Lista2.Items[InitObject.PrtEnt08.Lista2.ListIndex].Data = InitObject.PrtEnt10.Combo1.Value.GetValueOrDefault();
                        InitObject.PrtEnt08.Lista2.ListIndex = InitObject.PrtEnt08.Lista2.ListIndex;

                        if (InitObject.PrtEnt08.Lista2.ListIndex == InitObject.PrtEnt08.Lista2.Items.Count - 1)
                        {
                            s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                            InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = InitObject.PrtEnt10.Combo1.Items[InitObject.PrtEnt10.Combo1.SelectedValue.GetValueOrDefault()].Data, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s, });
                        }
                    }

                    break;

                case T_PRTGLOB.tipo_banco:

                    est = InitObject.PrtEnt10._prtactiva_1.Checked ? "Activa" : "No Activa";
                    cuenta = InitObject.PrtEnt10.prtcuenta.Text;
                    P = cuenta.InStr("_", 1, StringComparison.CurrentCulture);

                    if (P != 0)
                        cuenta = cuenta.Left((P - 1));

                    if (T_PRTGLOB.primeralista != 0)
                    {
                        if (!T_PRTGLOB.FlagCtaBco.ToBool())
                        {
                            T_PRTGLOB.FlagCtaBco = true.ToInt();
                            if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                                InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                        }

                        if (InitObject.PrtEnt08.Lista1.Items.Count > 1)
                        {
                            //fila = (short)InitObject.PrtEnt08.Lista1.get_ItemData_((short)InitObject.PrtEnt08.Lista1.ListIndex);
                            fila = (short)InitObject.PrtEnt08.Lista1.ListIndex;
                            listas = UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista1.Items[fila].Value);
                            if (!string.IsNullOrEmpty(listas))
                                listas += InitObject.PrtEnt08.Lista1.Items[fila].Tag;
                        }

                        a = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 4);
                        
                        switch (a.ToInt())
                        {
                            case T_PRTGLOB.eliminado_nuevo:
                                if (PRTYENT.QuitarMascaraCuenta(UTILES.copiardestring(listas, VB6Helpers.Chr(9), 1)) == "")
                                {
                                    a = T_PRTGLOB.nuevo.ToString();
                                    ind = PRTYENT.Siguiente(InitObject, "ctabancos").ToString();
                                    k = InitObject.PRTGLOB.ctabancos_aux.GetUpperBound(0);
                                    InitObject.PRTGLOB.ctabancos_aux[k] = new cuenta_indice();
                                    InitObject.PRTGLOB.ctabancos_aux[k].cuenta = InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim();
                                    InitObject.PRTGLOB.ctabancos_aux[k].indice = Convert.ToInt32(ind);
                                }
                                break;

                            case T_PRTGLOB.leido:
                                a = T_PRTGLOB.modificado.ToString();
                                ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                                break;

                            default:
                                ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                                break;
                        }

                        espec = InitObject.PrtEnt10.especial.Checked ? "1" : "0";
                        s = cuenta + VB6Helpers.Chr(9) + InitObject.PrtEnt10.Combo1.Items[(int)InitObject.PrtEnt10.Combo1.SelectedValue].Value + VB6Helpers.Chr(9) + est;
                        InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.SelectedValue].Value = s;
                        InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.SelectedValue].Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + espec;
                        InitObject.PrtEnt08.Lista1.Items[InitObject.PrtEnt08.Lista1.ListIndex].Data = InitObject.PrtEnt10.Combo1.SelectedValue.GetValueOrDefault();

                        if (InitObject.PrtEnt08.Lista1.ListIndex == InitObject.PrtEnt08.Lista1.Items.Count - 1)
                        {
                            s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                            InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1" + VB6Helpers.Chr(9) + "0", Value = "" });
                        }
                    }
                    else
                    {
                        for (k = 0; k <= InitObject.PrtEnt10.listmon.Items.Count - 1; k += 1)
                        {
                            if (InitObject.PrtEnt10.listmon.ListIndex == k)
                            {
                                if (!T_PRTGLOB.FlagLineas.ToBool())
                                {
                                    T_PRTGLOB.FlagLineas = 1;
                                    if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                                        InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;

                                }

                                listas = UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Value);

                                if (!string.IsNullOrEmpty(listas))
                                    listas += InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Tag;

                                a = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 4);

                                switch (a.ToInt())
                                {
                                    case T_PRTGLOB.eliminado_nuevo:
                                        if (PRTYENT.QuitarMascaraCuenta(UTILES.copiardestring(listas, VB6Helpers.Chr(9), 1)) == "")
                                        {
                                            a = T_PRTGLOB.nuevo.ToString();
                                            ind = PRTYENT.Siguiente(InitObject, "linbancos").ToString();
                                            t = InitObject.PRTGLOB.linbancos_aux.GetUpperBound(0);
                                            InitObject.PRTGLOB.linbancos_aux[t] = new cuenta_indice();
                                            InitObject.PRTGLOB.linbancos_aux[t].cuenta = InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim();
                                            InitObject.PRTGLOB.linbancos_aux[t].indice = Convert.ToInt32(ind);
                                        }
                                        break;

                                    case T_PRTGLOB.leido:
                                        a = T_PRTGLOB.modificado.ToString();
                                        ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                                        break;

                                    default:
                                        ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                                        break;
                                }

                                s = cuenta + VB6Helpers.Chr(9) + (InitObject.PrtEnt10.listmon.SelectedValue = k).ToString() + VB6Helpers.Chr(9) + est;
                                InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Value = s;
                                InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind;
                                InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Data = InitObject.PrtEnt10.Combo1.Value.GetValueOrDefault();

                                if (InitObject.PrtEnt08.Lista2.SelectedValue == InitObject.PrtEnt08.Lista2.Items.Count - 1)
                                {
                                    s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                                    InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = 0, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                                    InitObject.PrtEnt08.Lista2.SelectedValue = InitObject.PrtEnt08.Lista2.Items.Count - 1;
                                }

                                InitObject.PrtEnt10.listmon.Items[InitObject.PrtEnt08.Lista1.ListIndex].Value = k.ToString();
                                InitObject.PrtEnt10.listmon.get_ItemData_(InitObject.PrtEnt10.listmon.ListIndex);
                            }
                        }
                    }
                    break;


                case T_PRTGLOB.individuo:

                    est = InitObject.PrtEnt10._prtactiva_0.Checked ? InitObject.PrtEnt10._prtactiva_1.Checked ? "Activa" : "No Activa" : "Cerrada";

                    if (!T_PRTGLOB.FlagCuentas.ToBool())
                    {
                        T_PRTGLOB.FlagCuentas = true.ToInt();
                        if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                            InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                    }

                    if (T_PRTGLOB.primeralista != 0)
                    {
                        //listas = InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.SelectedValue].Value;
                        if (InitObject.PrtEnt08.Lista1.Items.Count > 1)
                        {
                            //fila = (short)InitObject.PrtEnt08.Lista1.get_ItemData_((short)InitObject.PrtEnt08.Lista1.ListIndex);
                            fila = (short)InitObject.PrtEnt08.Lista1.ListIndex;
                            listas = InitObject.PrtEnt08.Lista1.Items[fila].Value;
                            if (!string.IsNullOrEmpty(listas))
                                listas += InitObject.PrtEnt08.Lista1.Items[fila].Tag;
                        }

                        a = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 4);

                        switch (a.ToInt())
                        {
                            case T_PRTGLOB.eliminado_nuevo:
                                if (string.IsNullOrEmpty(UTILES.copiardestring(listas, VB6Helpers.Chr(9), 1)))
                                {
                                    a = T_PRTGLOB.nuevo.ToString();
                                    ind = PRTYENT.Siguiente(InitObject, "ctaclie").ToString();
                                    k = InitObject.PRTGLOB.ctaclie_aux.GetUpperBound(0);
                                    InitObject.PRTGLOB.ctaclie_aux[k] = new cuenta_indice();
                                    InitObject.PRTGLOB.ctaclie_aux[k].cuenta = InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim();
                                    InitObject.PRTGLOB.ctaclie_aux[k].indice = ind.ToInt();
                                }
                                break;

                            case T_PRTGLOB.leido:
                                a = T_PRTGLOB.modificado.ToString();
                                ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                                break;

                            default:
                                ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                                break;
                        }

                        //s = InitObject.PrtEnt10.prtcuenta.Text + VB6Helpers.Chr(9) + UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, InitObject.PrtEnt10.oficina.Text, 10) + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + bae_bch; //Respaldo original
                        //if (!string.IsNullOrEmpty(InitObject.PrtEnt10.oficina.Text == null ? string.Empty : InitObject.PrtEnt10.oficina.Text.Trim()))
                        //    cantidadEspaciosEnBlanco = 100;
                        //else
                        //    cantidadEspaciosEnBlanco = 200;

                        //s = UTILES.EspaciosAlineadoRight(InitObject.PrtEnt10.prtcuenta.Text, cantidadEspaciosEnBlanco) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoRight(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, InitObject.PrtEnt10.oficina.Text.ToUpper(), 10), 100) + VB6Helpers.Chr(9) + est;//Esconder columna 26-02-2016
                        //s = UTILES.EspaciosAlineado(InitObject.PrtEnt10.prtcuenta.Text, 100) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, (InitObject.PrtEnt10.oficina.Text??string.Empty).ToUpper(), 10), 100) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(est,100);//Esconder columna 26-02-2016
                        
                        s = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(InitObject.PrtEnt10.prtcuenta.Text, 50) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, (InitObject.PrtEnt10.oficina.Text ?? string.Empty).ToUpper(), 10), 40) + VB6Helpers.Chr(9) + est;//Esconder columna 26-02-2016
                        InitObject.PrtEnt08.Lista1.Items[InitObject.PrtEnt08.Lista1.ListIndex].Value = s;
                        InitObject.PrtEnt08.Lista1.Items[InitObject.PrtEnt08.Lista1.ListIndex].Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + bae_bch;
                        InitObject.PrtEnt08.Lista1.Items[InitObject.PrtEnt08.Lista1.ListIndex].Data = InitObject.PrtEnt10.Combo1.SelectedValue.GetValueOrDefault();

                        if (InitObject.PrtEnt08.Lista1.ListIndex == InitObject.PrtEnt08.Lista1.Items.Count - 1)// - 1)
                        {
                            s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                            // InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Value = s, Data = -1 });
                            InitObject.PrtEnt08.Lista1.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = InitObject.PrtEnt10.Combo1.Items[InitObject.PrtEnt10.Combo1.SelectedValue.GetValueOrDefault()].Data, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s });
                        }
                    }
                    else
                    {
                        //listas = InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.SelectedValue].Value;
                        if (InitObject.PrtEnt08.Lista2.Items.Count > 1)
                        {
                            //fila = (short)InitObject.PrtEnt08.Lista2.get_ItemData_((short)InitObject.PrtEnt08.Lista2.ListIndex);
                            fila = (short)InitObject.PrtEnt08.Lista2.ListIndex;
                            listas = UTILES.QuitaEspaciosEnBlanco(InitObject.PrtEnt08.Lista2.Items[fila].Value);

                            if (!string.IsNullOrEmpty(listas))
                                listas += InitObject.PrtEnt08.Lista2.Items[fila].Tag;
                        }

                        a = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                        
                        switch (a.ToInt())
                        {
                            case T_PRTGLOB.eliminado_nuevo:

                                if (PRTYENT.QuitarMascaraCuenta(UTILES.copiardestring(listas, VB6Helpers.Chr(9), 1)) == "")
                                {
                                    a = T_PRTGLOB.nuevo.ToString();
                                    ind = PRTYENT.Siguiente(InitObject, "ctaclie").ToString();
                                    k = InitObject.PRTGLOB.ctaclie_aux.GetUpperBound(0);
                                    InitObject.PRTGLOB.ctaclie_aux[k] = new cuenta_indice();
                                    InitObject.PRTGLOB.ctaclie_aux[k].cuenta = InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim();
                                    InitObject.PRTGLOB.ctaclie_aux[k].indice = ind.ToInt();
                                }
                                break;

                            case T_PRTGLOB.leido:
                                a = T_PRTGLOB.modificado.ToString();
                                ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 6);
                                break;

                            default:
                                a = T_PRTGLOB.modificado.ToString();
                                ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 6);
                                break;
                        }

                        //if (!string.IsNullOrEmpty(InitObject.PrtEnt10.oficina.Text == null ? string.Empty : InitObject.PrtEnt10.oficina.Text.Trim()))
                        //    cantidadEspaciosEnBlanco = 100;
                        //else
                        //    cantidadEspaciosEnBlanco = 200;

                        //s = UTILES.EspaciosEnBlancoRight(InitObject.PrtEnt10.prtcuenta.Text,cantidadEspaciosEnBlanco) + VB6Helpers.Chr(9) + UTILES.EspaciosEnBlancoRight(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, InitObject.PrtEnt10.oficina.Text.ToUpper(), 10),80) + VB6Helpers.Chr(9) + InitObject.PrtEnt10.Combo1.Items[(int)InitObject.PrtEnt10.Combo1.SelectedValue].Value + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind;
                        //s = UTILES.EspaciosAlineado(InitObject.PrtEnt10.prtcuenta.Text, 100) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, (InitObject.PrtEnt10.oficina.Text ?? string.Empty).ToUpper(), 10), 100) + VB6Helpers.Chr(9) + InitObject.PrtEnt10.Combo1.Text + VB6Helpers.Chr(9) + est;
                        
                        s = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(InitObject.PrtEnt10.prtcuenta.Text, 33) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, (InitObject.PrtEnt10.oficina.Text ?? string.Empty).ToUpper(), 10), 35) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(InitObject.PrtEnt10.Combo1.Text.Trim(), 37) + VB6Helpers.Chr(9) + est;
                        InitObject.PrtEnt08.Lista2.Items[InitObject.PrtEnt08.Lista2.ListIndex].Value = s;
                        InitObject.PrtEnt08.Lista2.Items[InitObject.PrtEnt08.Lista2.ListIndex].Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind;
                        InitObject.PrtEnt08.Lista2.Items[InitObject.PrtEnt08.Lista2.ListIndex].Data = InitObject.PrtEnt10.Combo1.Value.GetValueOrDefault();
                        InitObject.PrtEnt08.Lista2.ListIndex = InitObject.PrtEnt08.Lista2.ListIndex;

                        if (InitObject.PrtEnt08.Lista2.ListIndex == InitObject.PrtEnt08.Lista2.Items.Count - 1)
                        {
                            s = "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "" + VB6Helpers.Chr(9) + "";
                            InitObject.PrtEnt08.Lista2.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem { Data = InitObject.PrtEnt10.Combo1.Items[InitObject.PrtEnt10.Combo1.SelectedValue.GetValueOrDefault()].Data, Tag = VB6Helpers.Chr(9) + "0" + VB6Helpers.Chr(9) + "-1", Value = s, });
                        }
                    }
                    break;
            }
            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsBanco))
            {
            }

            InitObject.PrtEnt10.CuentaBae.Checked = false;
        }

        public static void Cancelar_Click(InitializationObject InitObject)
        {
            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && MODWS.RUTTIT == "")
            {
                if (T_MODWS.FLAGOPEN == "ABIERTO")
                {
                    //string msgreto = string.Empty;
                    //msgreto = MODWS.MensajeWs(InitObject, "SALIR", "");
                    T_MODWS.FLAGOPEN = "CERRADO";
                    MODWS.RUTTIT = "SALIR";
                }
            }
            else
            {
                InitObject.PrtEnt10.CuentaBae.Checked = false;
                if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsBanco))
                {                  
                }               
            }
        }

        public static void cbo_cta_Click(InitializationObject InitObject)
        {
            if (InitObject.PrtEnt10.cbo_cta.Items.Count > 0 && InitObject.PrtEnt10.cbo_cta.Text != "")
                InitObject.PrtEnt10.prtcuenta.Text = InitObject.PrtEnt10.cbo_cta.Text;
        }

        public static void Combo1_Click(InitializationObject InitObject)
        {
            InitObject.PrtEnt10.aceptar.Enabled = true;
        }

        public static void Combo2_Click(InitializationObject InitObject)
        {
            InitObject.PrtEnt10.aceptar.Enabled = true;
        }

        public static void CuentaBae_Click(InitializationObject InitObject)
        {
            if (InitObject.PrtEnt10.CuentaBae.Checked == true)
            {
                ((dynamic)InitObject.PrtEnt10.prtcuenta).Mask = "##\\-######\\-##";
                InitObject.PrtEnt10.prtcuenta.Text = "__-______-__";
            }
            else
            {
                if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && (InitObject.PRTGLOB.Party.Bnumber != "" || InitObject.PRTGLOB.Party.Bnumber != null))
                {
                    ((dynamic)InitObject.PrtEnt10.prtcuenta).Mask = "###########";
                    InitObject.PrtEnt10.prtcuenta.Text = "___________";
                }
                else
                {
                    ((dynamic)InitObject.PrtEnt10.prtcuenta).Mask = "###\\-#####\\-##";
                    InitObject.PrtEnt10.prtcuenta.Text = "___-_____-__";
                }
            }

            //if (Convert.ToInt32((InitObject.PrtEnt10.prtcuenta.Text).ToString().Left(3)) != 0)
            //if (Convert.ToInt32((PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text)).ToString().Left(3)) != 0)
            
            int esNumero = 0;
            
            if(int.TryParse((PRTYENT.QuitarMascaraCuenta(InitObject.PrtEnt10.prtcuenta.Text)).ToString().Left(3), out esNumero))
            {
                if (InitObject.PrtEnt10.CuentaBae.Checked == true)
                    InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, VB6Helpers.Format(PRTYENT.quitaceros(InitObject.PrtEnt10.prtcuenta.Text.ToString().Left(2))));
                else
                    InitObject.PrtEnt10.oficina.Text = PRTYENT.nom_ofi(InitObject, VB6Helpers.Format(PRTYENT.quitaceros(InitObject.PrtEnt10.prtcuenta.Text.ToString().Left(3))));
            }
            else
                InitObject.PrtEnt10.oficina.Text = string.Empty;
        }

        public static void Eliminar_Click(InitializationObject InitObject, UnitOfWorkCext01 unit)
        {
            string bae_bch = string.Empty;
            int fi = 0;
            string cuenta = string.Empty;
            string est = string.Empty;
            string listas = string.Empty;
            string a = string.Empty;
            string ind = string.Empty;
            string espec = string.Empty;
            string s = string.Empty;
            string ofic = string.Empty;
            ofic = new string(0.ToChar(), 255);
            InitObject.PrtEnt10.MarcaMensaje = 0;

            if (Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI))
            {
                if (MODWS.SyGet_Cta(InitObject, InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim(), 1, unit) == 0)
                {
                    InitObject.PrtEnt10.MarcaMensaje = 1;

                    InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = " Esta cuenta no puede ser eliminada debido a que existe un movimiento con esta cuenta.",
                        Title = T_PRTGLOB.TitCuentas,
                        Type = TipoMensaje.Informacion
                    });

                    return;
                }
            }

            switch (InitObject.PRTGLOB.Party.tipo)
            {
                case T_PRTGLOB.tipo_cliente:
                    est = InitObject.PrtEnt10._prtactiva_0.Checked ? InitObject.PrtEnt10._prtactiva_1.Checked ? "Activa" : "No Activa" : "Cerrada";
                    break;

                case T_PRTGLOB.tipo_banco:
                    if (InitObject.PrtEnt10._prtactiva_1.Checked == true)
                        est = "Activa";
                    else
                        est = "No Activa";

                    break;
            }

            cuenta = InitObject.PrtEnt10.prtcuenta.Text.ToString().Trim();
            est = "Eliminada";

            if (T_PRTGLOB.primeralista != 0)
            {
                if (!Convert.ToBoolean(InitObject.PRTGLOB.Party.PrtGlob.EsCITI) && InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco)
                {
                    fi = MODPRTY.SyGet_Ofi(InitObject, cuenta, 2);
                    if (fi == 0)
                    {
                        InitObject.PrtEnt10.MarcaMensaje = 1;

                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " No existe Aplicación V_CtaCte.exe. Se asumira que es oficina BCH. Reporte este problema.",
                            Title = T_PRTGLOB.TitCuentas,
                            Type = TipoMensaje.Informacion
                        });

                        bae_bch = "BCH";
                    }

                    bae_bch = "BCH";
                    //if (fi == 1)
                    //TODO: Queda pendiente revisar SyGet_Ofi que al parecer Beacon no migró
                        //bae_bch = "BAE";
                    //if (fi == 2)
                    //    bae_bch = "BCH";

                    if (fi == 3)
                    {
                        InitObject.PrtEnt10.MarcaMensaje = 1;

                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " Número de Cuenta Inexistente.",
                            Title = T_PRTGLOB.TitCuentas,
                            Type = TipoMensaje.Informacion
                        });

                        return;
                    }

                    if (InitObject.PrtEnt10.CuentaBae.Checked == false && fi == 1)
                    {
                        InitObject.PrtEnt10.MarcaMensaje = 1;

                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " Número de Cuenta no es BCH.",
                            Title = T_PRTGLOB.TitCuentas,
                            Type = TipoMensaje.Informacion
                        });

                        return;
                    }

                    if (InitObject.PrtEnt10.CuentaBae.Checked == true && fi == 2)
                    {
                        InitObject.PrtEnt10.MarcaMensaje = 1;

                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = " Número de Cuenta no es BAE.",
                            Title = T_PRTGLOB.TitCuentas,
                            Type = TipoMensaje.Informacion
                        });

                        return;
                    }
                }

                if (InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco)
                {
                    if (!T_PRTGLOB.FlagCtaBco.ToBool())
                    {
                        T_PRTGLOB.FlagCtaBco = true.ToInt();

                        if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                            InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                    }
                }

                if (InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente)
                {
                    if (!T_PRTGLOB.FlagCuentas.ToBool())
                    {
                        T_PRTGLOB.FlagCuentas = true.ToInt();

                        if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                            InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                    }
                }

                listas = InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Value.ToString();

                if (!string.IsNullOrEmpty(listas))
                    listas += InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Tag;

                var aux = listas.Split(VB6Helpers.Chr(9));
                a = InitObject.PRTGLOB.Party.estado.ToString();

                switch (Convert.ToInt32(a))
                {
                    case T_PRTGLOB.nuevo:
                        a = T_PRTGLOB.eliminado_nuevo.ToString();
                        break;
                    case T_PRTGLOB.leido:
                        a = T_PRTGLOB.eliminado_leido.ToString();
                        break;
                    case T_PRTGLOB.modificado:
                        a = T_PRTGLOB.eliminado_modificado.ToString();
                        break;
                }

                ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                espec = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 6);
                int largo = 50;
                int largo2 = 40;

                if (InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente)
                {
                    s = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(InitObject.PrtEnt10.prtcuenta.Text, largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, InitObject.PrtEnt10.oficina.Text, 10), largo2) + VB6Helpers.Chr(9) + est;
                    InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + bae_bch;
                    //s = InitObject.PrtEnt10.prtcuenta.Text + VB6Helpers.Chr(9) + UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, InitObject.PrtEnt10.oficina.Text, 10) + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + bae_bch;
                }
                else
                {
                    s = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(InitObject.PrtEnt10.prtcuenta.Text, largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista1, InitObject.PrtEnt10.Combo1.Items[(int)InitObject.PrtEnt10.Combo1.ListIndex].Value, 10), largo2) + VB6Helpers.Chr(9) + est;
                    InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind + VB6Helpers.Chr(9) + bae_bch + VB6Helpers.Chr(9) + espec;
                    //s = InitObject.PrtEnt10.prtcuenta.Text + VB6Helpers.Chr(9) + InitObject.PrtEnt10.Combo1.Items[(int)InitObject.PrtEnt10.Combo1.SelectedValue].Value + (VB6Helpers.Chr(9)) + est + (VB6Helpers.Chr(9) + a + (VB6Helpers.Chr(9)) + ind + (VB6Helpers.Chr(9)) + espec + bae_bch);
                }

                InitObject.PrtEnt08.Lista1.Items[(int)InitObject.PrtEnt08.Lista1.ListIndex].Value = s;
            }
            else
            {
                if (InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco)
                {
                    listas = InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Value;

                    if (!string.IsNullOrEmpty(listas))
                        listas += InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Tag;

                    a = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 4);

                    switch (Convert.ToInt32(a))
                    {
                        case T_PRTGLOB.nuevo:
                            a = T_PRTGLOB.eliminado_nuevo.ToString();
                            break;

                        case T_PRTGLOB.leido:
                            a = T_PRTGLOB.eliminado_leido.ToString();
                            break;

                        case T_PRTGLOB.modificado:
                            a = T_PRTGLOB.eliminado_modificado.ToString();
                            break;
                    }

                    ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);
                    int largo = 33;
                    int largo2 = 35;
                    int largo3 = 37;
                    s = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(VB6Helpers.Format(InitObject.PrtEnt10.prtcuenta.Text.ToString(), InitObject.PrtEnt08.cuenta.Mask), largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, "", 10), largo2) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineado(InitObject.PrtEnt10.listmon.SelectedValue.ToString(), largo3) + VB6Helpers.Chr(9) + est;
                    InitObject.PrtEnt08.Lista2.Items[InitObject.PrtEnt08.Lista2.ListIndex].Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind;
                    //s =  InitObject.PrtEnt10.listmon.SelectedValue.ToString() + (VB6Helpers.Chr(9)) + est + VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind;
                    InitObject.PrtEnt08.Lista2.Items[InitObject.PrtEnt08.Lista2.ListIndex].Value = s;

                    if (!T_PRTGLOB.FlagLineas.ToBool())
                    {
                        T_PRTGLOB.FlagLineas = true.ToInt();
                        if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)
                            InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;
                    }
                }

                if (InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_cliente)
                {
                    listas = InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Value;

                    if (!string.IsNullOrEmpty(listas))
                        listas += InitObject.PrtEnt08.Lista2.Items[(int)InitObject.PrtEnt08.Lista2.ListIndex].Tag;

                    a = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 5);

                    switch (a.ToInt())
                    {
                        case T_PRTGLOB.nuevo:
                            a = T_PRTGLOB.eliminado_modificado.ToString();
                            break;

                        case T_PRTGLOB.leido:
                            a = T_PRTGLOB.eliminado_leido.ToString();
                            break;

                        case T_PRTGLOB.modificado:
                            a = T_PRTGLOB.eliminado_modificado.ToString();
                            break;
                    }

                    ind = UTILES.copiardestring(listas, VB6Helpers.Chr(9), 6);
                    InitObject.PrtEnt10.oficina.Text = "";
                    int largo = 33;
                    int largo2 = 35;
                    int largo3 = 37;
                    s = UTILES.EspaciosAlineadoMonoSpace("", 7) + UTILES.EspaciosAlineadoMonoSpace(InitObject.PrtEnt10.prtcuenta.Text, largo) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, InitObject.PrtEnt10.oficina.Text, 10), largo2) + VB6Helpers.Chr(9) + UTILES.EspaciosAlineadoMonoSpace(InitObject.PrtEnt10.Combo1.Items[InitObject.PrtEnt10.Combo1.ListIndex].Value, largo3) + VB6Helpers.Chr(9) + est;
                    InitObject.PrtEnt08.Lista2.Items[InitObject.PrtEnt08.Lista2.ListIndex].Tag = VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind;
                    //s = InitObject.PrtEnt10.prtcuenta.Text + VB6Helpers.Chr(9) + UTILES.RecortaTexto(InitObject.PrtEnt08.Lista2, InitObject.PrtEnt10.oficina.Text, 10) + VB6Helpers.Chr(9) + InitObject.PrtEnt10.Combo1.Items[InitObject.PrtEnt10.Combo1.ListIndex].Value + VB6Helpers.Chr(9) + est + VB6Helpers.Chr(9) + a + VB6Helpers.Chr(9) + ind;
                    InitObject.PrtEnt08.Lista2.Items[InitObject.PrtEnt08.Lista2.ListIndex].Value = s;
                    
                    if (!T_PRTGLOB.FlagCuentas.ToBool())
                    {
                        T_PRTGLOB.FlagCuentas = true.ToInt();

                        if (InitObject.PRTGLOB.Party.estado == T_PRTGLOB.leido)                        
                            InitObject.PRTGLOB.Party.estado = T_PRTGLOB.modificado;                        
                    }
                }
            }

            if (InitObject.PrtEnt10.prtcuenta.Visible)
            {
                //InitObject.PrtEnt10.prtcuenta.Focus();
            }
            //InitObject.PrtEnt10.Hide();
        }

        public static void especial_Click(InitializationObject InitObject)
        {
            InitObject.PrtEnt10.aceptar.Enabled = true;
        }

        public static void listmon_Click(InitializationObject InitObject)
        {
            InitObject.PrtEnt10.aceptar.Enabled = true;
        }

        public static void prtactiva_Click(InitializationObject InitObject)
        {
            InitObject.PrtEnt10.aceptar.Enabled = true;
        }

        public static void prtcuenta_Change(InitializationObject InitObject)
        {
            InitObject.PrtEnt10.aceptar.Enabled = true;
        }

        public static void prtcuenta_GotFocus(InitializationObject InitObject)
        {
            InitObject.PrtEnt10.oficina.Enabled = true;
        }

        public static int VerificaBAE(string cuenta)
        {
            int VerificaBAE = 0;
            double digito = 0.0;
            int i = 0;
            int largo = 0;
            string verif = string.Empty;
            double sum = 0.0;
            int mayor = 0;
            sum = 0;
            verif = (Convert.ToInt32(cuenta.Substring(1, 1)) * 2).ToString().Trim() + (Convert.ToInt32(cuenta.Substring(2, 1)) * 1).ToString().Trim() + (Convert.ToInt32(cuenta.Substring(3, 1)) * 2).ToString().Trim() + (Convert.ToInt32(cuenta.Substring(4, 1)) * 1).ToString().Trim() + (Convert.ToInt32(cuenta.Substring(5, 1)) * 2).ToString().Trim() + (Convert.ToInt32(cuenta.Substring(6, 1)) * 1).ToString().Trim() + (Convert.ToInt32(cuenta.Substring(7, 1)) * 2).ToString().Trim() + (Convert.ToInt32(cuenta.Substring(8, 1)) * 1).ToString().Trim();
            largo = verif.Length - 1;

            for (i = 1; i <= largo; i += 1)
                sum = sum + Convert.ToDouble(verif.Substring(i, 1));
            
            if (sum < 10)
                digito = 10 - sum;
            else
            {
                mayor = (Convert.ToInt32(sum.ToString().Trim().Substring(1, 1)) + 1) * 10;
                digito = Convert.ToDouble(mayor) - sum;
            }

            if (digito == 10)
                digito = 0;
            
            if (digito == Convert.ToDouble(cuenta.Substring(cuenta.Length, 1)))
                VerificaBAE = -1;
            else
                VerificaBAE = 0;
            
            return VerificaBAE;
        }
    }
}
