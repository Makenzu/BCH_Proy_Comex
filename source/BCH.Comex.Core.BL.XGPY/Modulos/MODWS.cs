//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Web.Configuration;


namespace BCH.Comex.Core.BL.XGPY.Modulos
{
    public class MODWS
    {
        public static string tipocta = "";
        public static string RUTTIT = "";
        public static T_MODWS GetMODWS()
        {
            return new T_MODWS();
        }
        public static string GetConfigValue(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }
        public static InitializationObject Inicializar(UnitOfWorkCext01 unit)
        {
            InitializationObject res = new InitializationObject(true);
            res.PARTY = PARTY.GetPARTY();
            res.MODGRNG = MODGRNG.GetMODGRNG();
            res.MODGSGT = MODGSGT.GetMODGSGT();
            res.MODWS = MODWS.GetMODWS();
            res.PRTGLOB = PRTGLOB.GetPRTGLOB();
            res.PRTYENT = PRTYENT.GetPRTYENT();
            res.PRTYENT2 = PRTYENT2.GetPRTYENT2();
            res.UTILES = UTILES.GetUTILES();

            //(moo) inicializamos valores
            res.PRTGLOB.Party.PrtGlob = new PrtGlob();
            res.PRTGLOB.Party.PrtGlob.Pertenece = 1;
            res.PRTGLOB.Party.PrtGlob.primera = 1;
            res.PRTGLOB.Party.PrtGlob.cambio_a_acreedor = 0;
            res.PRTGLOB.Party.PrtGlob.cambio_a_corresponsal = 0;
            res.PRTGLOB.Party.PrtGlob.ctas_eliminadas = 1;
            return res;
        }


        public static void Carga_datos(InitializationObject InitObject, UnitOfWorkCext01 uow, int pagina)
        {
            string msgreto = "";

            switch (pagina)
            {
                case (int)Enums.Pagina.PRTENT02:
                    break;

                case (int)Enums.Pagina.PRTENT10:
                    tipocta = extraemensaje(T_MODWS.MSJRET, 1);
                    RUTTIT = extraemensaje(T_MODWS.MSJRET, 2);
                    break;

                case (int)Enums.Pagina.PRTENT08:
                    if (InitObject.MODWS.CtaCCOL != null)
                    {
                        if (InitObject.MODWS.CtaCCOL.Length > 0)
                        {
                            InitObject.PrtEnt08.Lista1.Enabled = true;
                            InitObject.PrtEnt08.Lista2.Enabled = true;
                            InitObject.PrtEnt08.aceptar.Enabled = true;
                            InitObject.PrtEnt08.cancelar.Enabled = true;
                        }
                    }

                    break;

                case (int)Enums.Pagina.PRTENT07:
                    int Y = 0;
                    string cod = "";
                    string nome = "";
                    string la_ofi = "";
                    // Oficina
                    la_ofi = InitObject.PrtEnt07.oficina.Text;
                    InitObject.PrtEnt07.oficina.Text = la_ofi; //extraemensaje(T_MODWS.MSJRET, (2.Str()).ToInt());                 
                    InitObject.PrtEnt07.oficina.MaxLength = 0;
                    cod = InitObject.PrtEnt07.oficina.Text;
                    nome = PRTYENT.nom_ofi(InitObject, cod);

                    if (nome == "")
                    {
                        InitObject.PrtEnt07.oficina.Text = "";
                        InitObject.PrtEnt07.oficina.Tag = "";
                        InitObject.PrtEnt07.ejecutivo.Items.Clear();
                        return;
                    }
                    else
                    {
                        InitObject.PrtEnt07.oficina.Text = nome;
                        InitObject.PrtEnt07.oficina.Tag = cod;
                        if (InitObject.PrtEnt07.oficina.Tag.ToStr() != la_ofi)
                        {
                            la_ofi = InitObject.PrtEnt07.oficina.Tag.ToStr();
                            InitObject.PrtEnt07.ejecutivo.ListIndex = -1;
                            PRTYENT.lee_ejecutivosSy(InitObject, uow, cod);
                            PRTYENT.carga_ejecutivos(InitObject, InitObject.PrtEnt07.ejecutivo);

                        }
                    }

                    if (InitObject.PrtEnt07.oficina.Tag.ToStr() != InitObject.PRTGLOB.Party.oficina)
                    {
                        //InitObject.PrtEnt07.aceptar.Enabled = PRTYENT.setaceptar(InitObject.PrtEnt07) != 0;
                    }

                    // Ejecutivos
                    
                    for (Y = 0; Y <= InitObject.PrtEnt07.ejecutivo.Items.Count - 1; Y += 1)
                    {
                        if (InitObject.PrtEnt07.ejecutivo.get_ItemData_(Y).ToStr().TrimB() == (extraemensaje(T_MODWS.MSJRET, 1).ToInt()).Str().TrimB())
                        {
                            InitObject.PrtEnt07.ejecutivo.ListIndex = Y;
                            break;
                        }
                    }

                    // act economica
                    
                    for (Y = 0; Y <= InitObject.PrtEnt07.Combo2.Items.Count - 1; Y += 1)
                    {
                        if (extraemensaje(T_MODWS.MSJRET, 3).TrimB() != "")
                        {
                            if (InitObject.PrtEnt07.Combo2.Items[Y].ToStr().InStr((extraemensaje(T_MODWS.MSJRET, 3).ToDbl()).Str().TrimB(), 1, StringComparison.CurrentCulture) > 0)
                            {
                                InitObject.PrtEnt07.Combo2.ListIndex = Y;
                                break;
                            }
                        }
                        else
                        {
                            InitObject.PrtEnt07.Combo2.ListIndex = Y;
                            break;
                        }
                        // IR68422 20140729
                    }

                    for (Y = 0; Y <= InitObject.PrtEnt07.Combo1.Items.Count - 1; Y += 1)
                    {
                        if (InitObject.PrtEnt07.Combo1.Items[Y].ToStr().TrimB().UCase() == "CLIENTE")
                        {
                            InitObject.PrtEnt07.Combo1.ListIndex = Y;
                            break;
                        }
                    }

                    for (Y = 0; Y <= InitObject.PrtEnt07.Combo4.Items.Count - 1; Y += 1)
                    {
                        if (InitObject.PrtEnt07.Combo4.Items[Y].ToStr().TrimB().UCase() == "SIN CLASIFICACION")
                        {
                            InitObject.PrtEnt07.Combo4.ListIndex = Y;
                            break;
                        }
                    }

                    InitObject.PrtEnt07.aceptar.Enabled = true;
                    InitObject.PrtEnt07.cancelar.Enabled = true;
                    // InitObject.PrtEnt07.Fr_CliEsp.Enabled = true;
                    //InitObject.PrtEnt07.HabilitaDeshabilitaFr_CliEsp()
                    InitObject.PrtEnt07.Combo1.Enabled = true;
                    InitObject.PrtEnt07.Combo4.Enabled = true;
                    InitObject.PrtEnt07.Combo2.Enabled = true;
                    InitObject.PrtEnt07.ejecutivo.Enabled = true;
                    InitObject.PrtEnt07.oficina.Enabled = true;

                    // espera(InitObject.PrtEnt07, "C");
                    // ---------------- Fin Codigo Nuevo ----------------
                    break;
            }

            //if (FLAGOPEN == "ABIERTO")
            //{
            //    //msgreto = MensajeWs("SALIR", "");
            //    FLAGOPEN = "CERRADO";
            //}
        }

        public static string extraemensaje(string msg, int campo)
        {
            string extraemensaje = "";
            int cont = 0;
            int suma = 0;
            int ant = 0;
            int pos = 0;
            int largo = msg.Length;
            ant = 0;
            pos = 0;

            for (cont = 0; cont < largo; cont += 1)
            {
                if (msg.Substring(cont, 1) == "~" || cont == largo)
                {
                    ant = pos;
                    pos = cont;
                    suma = suma + 1;

                    if (suma == campo)
                    {
                        extraemensaje = msg.Substring((ant + 1), pos - 1 - ant);
                        break;
                    }
                }
            }

            return extraemensaje;
        }

        public static string ExtraeRut(string rut)
        {
            int cont = 0;
            string retRutFormat = string.Empty, rut_salida = string.Empty, ruttmp = string.Empty;

            if (string.IsNullOrEmpty(rut.Trim()))
                return rut.Trim();

            //(moo) eliminamos los |
            rut = rut.Replace("|", "").Replace("-", "").Trim();

            for (cont = 0; cont < rut.Length; cont++)
            {
                if (rut[cont] != '0')
                {
                    ruttmp = rut.Substring(cont, rut.Length - cont);
                    break;
                }
            }

            for (cont = 0; cont < ruttmp.Length; cont++)
            {
                if (ruttmp[cont] != '~')
                    rut_salida = rut_salida + ruttmp[cont];
            }

            retRutFormat = rut_salida.Substring(0, rut_salida.Length - 1) + "-" + rut_salida[rut_salida.Length - 1];
            return retRutFormat;
        }

        public static void LlamaServerWS()
        {
            int bien = 0;
            string PathExes = "";

            //PathExes = UTILES.GetUbicacion("PathExes");

            //bien = MigrationSupport.Utils.Shell(PathExes + "SCEWSBUS", MigrationSupport.Utils.AppWinStyle.MinimizedFocus);
            if (bien == 0)
            {
                //System.Windows.Forms.MessageBox.Show("Problemas al ejecutar programa SCEWSBUS. Favor interntar de nuevo.", "Error", MessageBoxButtons.AbortRetryIgnore);
                //FLAGOPEN = "CERRADO";
            }
            else
                T_MODWS.FLAGOPEN = "ABIERTO";
        }

        public static void Llena_Cuentas(InitializationObject InitObject, BCH.Comex.Common.UI_Modulos.UI_Combo lista)
        {
            int i = 0;
            lista.Clear();

            for (i = 0; i <= InitObject.MODWS.CtaCCOL.GetUpperBound(0); i += 1)
            {
                if (InitObject.PrtEnt10.Tag.Tag.ToStr() == "MN")
                {
                    if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                    {
                        if (InitObject.MODWS.CtaCCOL[i].tipo == "YTD")
                        {
                            lista.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                            {
                                ID = i.ToString(),
                                Data = i,
                                Value = VB6Helpers.Format(InitObject.MODWS.CtaCCOL[i].nrocta + new string('_', 12 - InitObject.MODWS.CtaCCOL[i].nrocta.TrimB().Len()).TrimB(), "____________")
                            });

                            //lista.AddItem(VB6Helpers.Format(InitObject.MODWS.CtaCCOL[i].nrocta + new string('_', 12 - InitObject.MODWS.CtaCCOL[i].nrocta.TrimB().Len()).TrimB(), "____________"));
                        }
                    }
                    else
                    {
                        if (InitObject.MODWS.CtaCCOL[i].tipo == "CTD")
                        {
                            //((dynamic)lista).AddItem(VB6Helpers.Format(new string('0', 11 - InitObject.MODWS.CtaCCOL[i].nrocta.TrimB().Len()) + InitObject.MODWS.CtaCCOL[i].nrocta.TrimB(), "000-00000-00"));
                            lista.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                            {
                                ID = i.ToString(),
                                Data = i,
                                Value = VB6Helpers.Format(InitObject.MODWS.CtaCCOL[i].nrocta.PadLeft(10, '0'), "000-00000-00")
                            });
                        }
                    }
                }
                else
                {
                    if (InitObject.PRTGLOB.Party.PrtGlob.EsCITI.ToBool() && !string.IsNullOrEmpty(InitObject.PRTGLOB.Party.Bnumber))
                    {
                        if (InitObject.MODWS.CtaCCOL[i].tipo == "YEX")
                        {
                            // ((dynamic)lista).AddItem(VB6Helpers.Format(InitObject.MODWS.CtaCCOL[i].nrocta + new string('_', 12 - InitObject.MODWS.CtaCCOL[i].nrocta.TrimB().Len()).TrimB(), "____________"));
                            lista.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                            {
                                ID = i.ToString(),
                                Data = i,
                                Value = InitObject.MODWS.CtaCCOL[i].nrocta.PadRight(12, '_')
                            });
                        }
                    }
                    else
                    {
                        if (InitObject.MODWS.CtaCCOL[i].tipo == "CEX")
                        {
                            //((dynamic)lista).AddItem(VB6Helpers.Format(new string('0', 11 - InitObject.MODWS.CtaCCOL[i].nrocta.TrimB().Len()) + InitObject.MODWS.CtaCCOL[i].nrocta.TrimB(), "0000-00000-00"));

                            lista.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                            {
                                ID = i.ToString(),
                                Data = i,
                                Value = VB6Helpers.Format(new string('0', 11 - InitObject.MODWS.CtaCCOL[i].nrocta.TrimB().Len()) + InitObject.MODWS.CtaCCOL[i].nrocta.TrimB(), "0000-00000-00")
                            });
                        }
                    }
                }
            }

            //if (InitObject.PrtEnt10.Tag.Text == "MN")
            //{
            //    foreach (BCH.Comex.Common.UI_Modulos.UI_ComboItem cuenta in InitObject.PrtEnt08.Lista1.Items)
            //    {
            //        if (cuenta.Value != null)
            //            lista.AddItem(i, UTILES.copiardestring(cuenta.Value, "\t", 1));
            //    }
            //}
            //else
            //{
            //    foreach (BCH.Comex.Common.UI_Modulos.UI_ComboItem cuenta in InitObject.PrtEnt08.Lista2.Items)
            //    {
            //        if (cuenta.Value != null)
            //            lista.AddItem(i, UTILES.copiardestring(cuenta.Value, "\t", 1));
            //    }
            //}

            //i = 0;
            //foreach (UI_ComboItem cuenta in InitObject.PrtEnt08.Lista1.Items)
            //{
            //    if (cuenta.Value != null)
            //    {
            //        lista.Items.Add(new UI_ComboItem
            //        {
            //            ID = i.ToString(),
            //            Data = i,
            //            Value = UTILES.copiardestring(cuenta.Value, "\t", 1)
            //        });
            //        i++;
            //    }
            //}

            //i = 0;
            //foreach (UI_ComboItem cuenta in InitObject.PrtEnt08.Lista2.Items)
            //{
            //    if (cuenta.Value != null)
            //    {
            //        lista.Items.Add(new UI_ComboItem
            //               {
            //                   ID = i.ToString(),
            //                   Data = i,
            //                   Value = UTILES.copiardestring(cuenta.Value, "\t", 1)
            //               });
            //        i++;

            //    }

            //}
        }

        //public static void Llena_Cuentas(InitializationObject InitObject, BCH.Comex.Common.UI_Modulos.UI_Combo listaDestino, BCH.Comex.Common.UI_Modulos.UI_Combo listaOrigen)
        //{
        //    int i = 0;
        //    listaDestino.Clear();        
        //    foreach (BCH.Comex.Common.UI_Modulos.UI_ComboItem cuenta in listaOrigen.Items)
        //    {
        //        if (cuenta.Value != "")
        //        {
        //            listaDestino.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
        //            {
        //                ID = i.ToString(),
        //                Data = i,
        //                Value = UTILES.copiardestring(cuenta.Value, "\t", 1)
        //            });
        //            i++;
        //        }
        //    }
        //}

        public static void Llena_Cuentas(InitializationObject InitObject, BCH.Comex.Common.UI_Modulos.UI_Combo listaDestino, BCH.Comex.Common.UI_Modulos.UI_Combo listaOrigen)
        {
            int i = 0;
            listaDestino.Clear();

            foreach (BCH.Comex.Common.UI_Modulos.UI_ComboItem cuenta in listaOrigen.Items)
            {
                if (cuenta.Value != "" && cuenta.Value != "\t\t")
                {
                    listaDestino.Items.Add(new BCH.Comex.Common.UI_Modulos.UI_ComboItem
                    {
                        ID = i.ToString(),
                        Data = i,
                        Value = UTILES.copiardestring(cuenta.Value, "\t", 1)
                    });
                    i++;
                }
            }

            listaDestino.ListIndex = (listaOrigen.Items[listaOrigen.ListIndex].Value.Replace("\t", string.Empty) != string.Empty ? listaOrigen.ListIndex : -1);
        }

        public static string MensajeWs(InitializationObject initObj, string servicio, string Mensaje)
        {
            string MensajeWs = "";

            try
            {
                initObj.PrtEnt01.Link.Text = servicio + "~" + Mensaje + "~";
                return MensajeWs;
            }
            catch (Exception exc)
            {
                //MigrationSupport.GlobalException.Initialize(exc);
                //switch (MigrationSupport.GlobalException.Instance.Number)
                //{
                //    case 284:
                //        return MensajeWs;
                //    case 286:
                //        return MensajeWs;
                //    case 19:
                //        return MensajeWs;
                //}
            }
            return MensajeWs;
        }

        public static int SyGet_Cta(InitializationObject initObj, string cuenta, int flag, UnitOfWorkCext01 unit)
        {
            int SyGet_Cta = 0;
            string R = "";
            int ExisteConta = 0;

            try
            {
                //try
                //{

                //    unit.SceRepository.ReadQuerySP((reader) =>
                //     {
                //         while (reader.Read())
                //         {
                //             R = Convert.ToString(reader.GetInt32(0));
                //         }
                //     }, "pro_sce_aprty_s01", MODGSYB.dbcharSy(cuenta), Convert.ToString(Flag));

                //}
                //catch (Exception e)
                //{
                //    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                //    {
                //        Text = " Se ha producido un error al tratar de leer los datos de los Usuarios.",
                //        Title = T_PRTGLOB.TitCuentas,
                //        Type = TipoMensaje.Informacion
                //    });

                //}

                //ExisteConta = Convert.ToInt32(MODGSYB.GetPosSy(MODGSYB.NumIni(), "N", R));
                ExisteConta = unit.SceRepository.Pro_Sce_Aprty_S01(cuenta, flag);
                SyGet_Cta = Convert.ToInt32(ExisteConta == 0);

                return SyGet_Cta;
            }
            catch (Exception exc)
            {
                string msg = "[" + MigrationSupport.Utils.Format(MigrationSupport.GlobalException.Instance.Number, String.Empty) + "] " + MigrationSupport.GlobalException.Instance.Number;

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = msg,
                    Title = T_PRTGLOB.TitCuentas,
                    Type = TipoMensaje.Informacion
                });
            }
            return SyGet_Cta;
        }

        public static void VistaConsulta(InitializationObject initObj)
        {
            // nuevo
            initObj.Mdi_Principal.Archivo[0].Enabled = false;    // menu nuevo
            initObj.Mdi_Principal.BUTTONS["tbr_nuevoParticipante"].Enabled = false;    // boton nuevo

            // salvar   
            initObj.Mdi_Principal.Archivo[2].Enabled = false;
            initObj.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = false; // boton     
            // Instrucciones
            initObj.Mdi_Principal.BUTTONS["tbr_Instrucciones"].Enabled = false;     // Instrucciones

            // Tasas
            initObj.Mdi_Principal.BUTTONS["tbr_Tasas"].Enabled = false;

            // Activar Razon
            initObj.Mdi_Principal.BUTTONS["tbr_Activar"].Enabled = false;

            // recuperar
            initObj.Mdi_Principal.Archivo[3].Enabled = false;

            // eliminar
            initObj.Mdi_Principal.Archivo[4].Enabled = false;

            // menu opciones
            initObj.Mdi_Principal.Opciones[1].Enabled = false;     // Instrucciones
            initObj.Mdi_Principal.Opciones[3].Enabled = false;    //Tasas
            initObj.Mdi_Principal.Opciones[0].Enabled = false;   // Caracteristicas
            initObj.Mdi_Principal.Opciones[2].Enabled = false;  // Cuenta
        }
    }
}
