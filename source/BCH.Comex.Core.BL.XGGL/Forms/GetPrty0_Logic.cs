using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public static class GetPrty0_Logic
    {
        public static void Form_Load(DatosGlobales Globales)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;

            int listo = 0;
            string abc = "";
            string primero = "";
            int i = 0;
            string LasMarcas = "";
            int aa = 0;
            int[] tavs = null;

            tavs = new int[2];
            tavs[0] = 100;
            tavs[1] = 200;

            LasMarcas = T_SYGETPRT.GPrt_MarcaRequerido + T_SYGETPRT.GPrt_MarcaOtros + T_SYGETPRT.GPrt_MarcaBanco;

            //Se limpia por si trae datos
            GetPrty0.Redireccionar = string.Empty;

            GetPrty0.EnLoad = true.ToInt();
            // Cargamos la Lista y precargamos valores ya ingresados
            for (i = SYGETPRT.PrtControl.LimInf; i <= SYGETPRT.PrtControl.LimSup; i += 1)
            {
                primero = "";
                abc = SYGETPRT.PrtTbl[i].Left(1);
                if ((LasMarcas.InStr(abc, 1, StringComparison.CurrentCulture) > 0) && abc != "")
                {
                    primero = SYGETPRT.PrtTbl[i].Right((SYGETPRT.PrtTbl[i].Len() - 1)) + 9.Char();
                }
                else if (SYGETPRT.PrtTbl[i] == "" && SYGETPRT.PrtControl.Otros != 0 && listo == 0)
                {
                    primero = T_SYGETPRT.GPrt_TxtOtros + 9.Char();
                    listo = 1;
                }
                else if (SYGETPRT.PrtTbl[i] != "")
                {
                    primero = SYGETPRT.PrtTbl[i] + 9.Char();
                }

                if (primero != "")
                {
                    GetPrty0.LstPartys.Items.Add(new UI_ListBoxItem()
                    {
                        Value = primero + SYGETPRT.Partys[i].NombreUsado,
                        Data = i
                    });
                }
            }

            // Lista esta cargada, Deshabilito En Operacion
            GetPrty0._Donde_1.Enabled = SYGETPRT.PrtControl.NoOperacion != 0;
            GetPrty0._Donde_0.Enabled = !GetPrty0._Donde_1.Enabled;

            if (GetPrty0.LstPartys.ListIndex == -1)
            {
                GetPrty0.LstPartys.ListIndex = 0;
                LstPartys_Click(Globales);
            }
            GetPrty0.EnLoad = false.ToInt();

            GetPrty0.Cancelar.Tag = "-1";
            //GetPrty0.Eliminar.Enabled = false;
            // modifica tamaño
            if (GetPrty0.LstPartys.Items.Count <= 4)
            {
                //GetPrty0.LstPartys.Height = 54;
                //GetPrty0.Aceptar.Height = 27;
                //GetPrty0.Cancelar.Top = 52;
                //GetPrty0.Cancelar.Height = 27;
            }
            ShowDir(Globales);
            GetPrty0.LOADED = true;
        }

        // Muestra Dirección del Party seleccionado.-
        private static void ShowDir(DatosGlobales Globales)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;

            string s = "";
            int k = 0;

            k = GetPrty0.LstPartys.ListIndex;
            // Tx_Dir.Text = PartysOpe(k%).DireccionUsado + ", " + PartysOpe(k%).CiudadUsado + ", " + PartysOpe(k%).PaisUsado
            s = SYGETPRT.Partys[k].DireccionUsado;
            if (SYGETPRT.Partys[k].CiudadUsado != "")
            {
                s = s + ", " + SYGETPRT.Partys[k].CiudadUsado;
            }
            if (SYGETPRT.Partys[k].PaisUsado != "")
            {
                s = s + ", " + SYGETPRT.Partys[k].PaisUsado;
            }
            GetPrty0.Tx_Dir.Text = s;

        }

        public static void Bot_Nem_Click(DatosGlobales Globales)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;

            //PrtEnt09.DefInstance.ShowDialog();
            GetPrty0.Llave.Text = SYGETPRT.KeyPrt;
        }

        public static bool Identificar_Click_1_2(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            int ElIndice = 0;
            int aabc = 0;
            string kk = "";
            string ElBco = "";
            string ElSwift = "";
            string ElRut = "";
            int ElFlag = 0;
            int ElTipoParty = 0;
            int Ret1 = 0;
            bool UnBanco = false;
            string s = "";
            string Txt = "";

            // llave es requerida
            if (GetPrty0.Llave.Text.TrimB() == "")
            {
                Txt = "Llave de Participante" + T_SYGETPRT.GPrt_ErrRequerido;
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = Txt,
                    Title = T_SYGETPRT.GPrt_Caption,
                    Type = TipoMensaje.Error,
                    ControlName = "Llave_Text"
                });
                return true;
            }

            SYGETPRT.PrtControl.Indice = SYGETPRT.PrtControl.LimInf + GetPrty0.LstPartys.ListIndex;

            // es modificable
            if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Status > T_SYGETPRT.GPrt_StatDatosLleno)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = T_SYGETPRT.GPrt_ErrModificar,
                    Title = T_SYGETPRT.GPrt_Caption,
                    Type = TipoMensaje.Error,
                    ControlName = "Llave_Text"
                });
                return true;
            }

            // Orieta
            s = GetPrty0.Llave.Text + new string(126.ToChar(), 12 - GetPrty0.Llave.Text.Len());
            GetPrty0.Llave.Tag = s.ToUpper();

            // determinar si necesito un banco
            if (SYGETPRT.PrtTbl[SYGETPRT.PrtControl.Indice].Left(1) == T_SYGETPRT.GPrt_MarcaBanco)
            {
                UnBanco = true;
            }

            // si quiere en operacion y es banco no puede
            if (!GetPrty0._Donde_0.Checked && UnBanco)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = T_SYGETPRT.GPrt_NoPuedeBanco,
                    Title = T_SYGETPRT.GPrt_Caption,
                    Type = TipoMensaje.Error
                });
                return true;
            }

            // En base de Partys
            if (GetPrty0._Donde_0.Checked)
            {
                Ret1 = VerPartySy_1_2(Globales, unit, ref ElTipoParty, ref ElFlag, ref ElRut, ref ElSwift, ref ElBco, UnBanco.ToInt());

                if ((string.IsNullOrEmpty(ElRut) || string.IsNullOrWhiteSpace(ElRut)) && Ret1 == T_SYGETPRT.GPrt_RetExiste)
                {
                    Globales.GetPrty0.Aceptar.Enabled = false;
                    Globales.MESSAGES.Add(new UI_Message
                    {
                        Text = "El participante ingresado no cuenta con RUT. Por favor use el \"Administrador de Participantes\" para ingresarlo, o de lo contrario seleccione otro participante válido.",
                        Type = TipoMensaje.Error,
                        Title = T_MODGCVD.MsgCVD,
                        ControlName = "Llave_Text"
                    });
                    // Limpiamos los datos cuando mostremos el mensaje de error.
                    Globales.GetPrty0.Identificar.Enabled = true;
                    Globales.GetPrty0.Tx_Dir.Text = string.Empty;
                    Globales.GetPrty0.LstPartys.Clear();
                    
                    foreach (var party in SYGETPRT.PrtTbl)
                    {
                        GetPrty0.LstPartys.Items.Add(new UI_ListBoxItem()
                        {
                            Value = party,
                            Data = SYGETPRT.PrtTbl.Length
                        });
                    }
                    return true;

                }else if (Globales.GetPrty0.AbrirIdentParticipantes)
                {
                    Globales.GetPrty0.Aceptar.Enabled = true;
                    return true;
                }
                if (Ret1 != T_SYGETPRT.GPrt_RetExiste)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type=TipoMensaje.Error,
                        Text= "Participante No Existe",
                        Title=T_SYGETPRT.GPrt_Caption,
                        ControlName = "Llave_Text"
                    });
                    return true;
                }
            }
            else
            {
                // en operación
                Ret1 = VerPartyOperacion_1_2(Globales, unit, ref ElIndice);
            }
            return false;
        }

        public static bool Identificar_Click_2_2(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            int ElIndice = 0;
            string kk = "";
            string ElBco = "";
            string ElSwift = "";
            string ElRut = "";
            int ElFlag = 0;
            int ElTipoParty = 0;
            int Ret1 = 0;
            bool UnBanco = false;
            bool ret = true;

            string Accion = string.Empty;
            // En base de Partys
            if (GetPrty0._Donde_0.Checked)
            {
                Ret1 = VerPartySy_2_2(Globales, unit, ref ElTipoParty, ref ElFlag, ref ElRut, ref ElSwift, ref ElBco, UnBanco.ToInt());

                if ((string.IsNullOrEmpty(ElRut) || string.IsNullOrWhiteSpace(ElRut)) && Ret1 == T_SYGETPRT.GPrt_RetExiste)
                {

                    Globales.GetPrty0.Aceptar.Enabled = false;
                    Globales.MESSAGES.Add(new UI_Message
                    {
                        Text = "El participante ingresado no cuenta con RUT. Por favor use el \"Administrador de Participantes\" para ingresarlo, o de lo contrario seleccione otro participante válido.",
                        Type = TipoMensaje.Error,
                        Title = T_MODGCVD.MsgCVD
                    });
                    // Limpiamos los datos cuando mostremos el mensaje de error.
                    Globales.GetPrty0.Identificar.Enabled = true;
                    Globales.GetPrty0.Tx_Dir.Text = string.Empty;
                    Globales.GetPrty0.LstPartys.Clear();

                    foreach (var party in SYGETPRT.PrtTbl)
                    {
                        GetPrty0.LstPartys.Items.Add(new UI_ListBoxItem()
                        {
                            Value = party,
                            Data = SYGETPRT.PrtTbl.Length
                        });
                    }

                    return true;

                }else if (Ret1 == T_SYGETPRT.GPrt_RetExiste)
                {
                    UpdatePartys(Globales, ElTipoParty, T_SYGETPRT.GPrt_EnParty, ElFlag, ElRut, ElSwift, ElBco);
                    Globales.GetPrty0.Aceptar.Enabled = true;
                    GetPrty0.Identificar.Text = "Modificar";
                    GetPrty0.Eliminar.Enabled = true;
                    GetPrty0.Instrucciones.Enabled = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.PrtyFlag(SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].FlagParty, T_SYGETPRT.GPrt_FlagInst) != 0;
                    GetPrty0.Instrucciones.Enabled = false;
                    LstPartys_Click(Globales);
                    ret = false;

                    string rutAux = ElRut.TrimStart('0').PadRight(12, '|');
                    if (string.IsNullOrWhiteSpace(MODXORI.Get_CtaCte(unit, rutAux)))
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "No se encontraron cuentas para el participante",
                            Title = T_SYGETPRT.GPrt_Caption
                        });
                    }
                }
                else
                {
                    ret = true;
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Participante No Existe",
                        Title = T_SYGETPRT.GPrt_Caption
                    });
                }
            }
            else
            {
                // en operación
                Ret1 = VerPartyOperacion_2_2(Globales, ref ElIndice);
                if (Ret1 == T_SYGETPRT.GPrt_RetExiste)
                {
                    UpdatePope(Globales, ElIndice);
                    Globales.GetPrty0.Aceptar.Enabled = true;
                    GetPrty0.Identificar.Text = "Modificar";
                    GetPrty0.Eliminar.Enabled = true;
                    GetPrty0.Instrucciones.Enabled = false;
                    ret = false;
                    Accion = "Identificar";
                }
                if (Ret1 == T_SYGETPRT.GPrt_RetCancelo)
                {
                    ret = true;
                }
            }
            Globales.Action = Accion;//"Identificar";
            Globales.Controller = String.Empty;
            return ret;
        }

        public static void Donde_Click(DatosGlobales globales)
        {
            UI_GetPrty0 GetPrty0 = globales.GetPrty0;
            T_SYGETPRT SYGETPRT = globales.SYGETPRT;
            int Index = GetPrty0._Donde_0.Checked ? 0 : 1;

            switch (Index)
            {
                case 0:
                    GetPrty0.Bot_Nem.Enabled = true;
                    GetPrty0.Llave.Enabled = true;
                    GetPrty0.Llave.Text = MODGPYF0.copiardestring(SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].LlaveArchivo, "~", 1);
                    GetPrty0.Redireccionar = null;

                    if (!GetPrty0.EnLoad.ToBool() && GetPrty0.Llave.Enabled)
                    {
                        
                    }
                    break;
                case 1:
                    GetPrty0.Bot_Nem.Enabled = false;
                    GetPrty0.Llave.Enabled = false;
                    GetPrty0.Llave.Text = "(" + MigrationSupport.Utils.Format(SYGETPRT.PrtControl.Indice, "00") + ")";
                    if (!GetPrty0.EnLoad.ToBool() && GetPrty0.Identificar.Enabled)
                    {
                        
                    }
                    break;
            }
        }

        public static void Identificar_Click_Help(DatosGlobales Globales)
        {
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;
            string kk = Globales.GetPrty2.Tag.ToStr();     // resultado
            Globales.GetPrty2 = null;


                if (!String.IsNullOrEmpty(""))
                {
                    //todo: @emiliano, revisar el archivo HELP

                    //kk = MODGPYF0.GetHelpFile(kk);
                    //aabc = MigrationSupport.Utils.Shell(kk, MigrationSupport.Utils.AppWinStyle.NormalFocus);     // ejecute
                    //System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                }
                // MsgBox "No se encuentra el Participante"
                return;
        }

        public static void LstPartys_Click(DatosGlobales Globales)
        {
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            int IBCOS = 0;
            string s = "";
            string Txt = "";

            SYGETPRT.PrtControl.Indice = SYGETPRT.PrtControl.LimInf + GetPrty0.LstPartys.ListIndex;
            Txt = MODGPYF0.copiardestring(GetPrty0.LstPartys.Items[GetPrty0.LstPartys.ListIndex].Value.ToString(), 9.Char(), 1);

            if (Txt == T_SYGETPRT.GPrt_TxtOtros)
            {
                GetPrty0.Llave.Text = "";
                if (GetPrty0.Frame1.Enabled)
                {
                    GetPrty0.Frame1.Enabled = false;
                }
                return;
            }
            if (!GetPrty0.Frame1.Enabled)
            {
                GetPrty0.Frame1.Enabled = true;
            }

            // Label3.Caption = "Identificar " + txt$

            if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Status == T_SYGETPRT.GPrt_StatVacio)
            {
                GetPrty0.Llave.Text = "";
                GetPrty0._Donde_0.Checked = true;
                GetPrty0.Eliminar.Enabled = false;
                GetPrty0.Instrucciones.Enabled = false;
                GetPrty0.Identificar.Text = "Identificar";
                GetPrty0.Identificar.Enabled = true;
            }
            else
            {
                //es necesario dejar todos en false, antes de seleccionar uno
                for (int i = 0; i < GetPrty0.Donde.Count(); i++)
                {
                    GetPrty0.Donde[i].Checked = false;
                }
                GetPrty0.Donde[SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Ubicacion].Checked = true;
                s = MODGPYF0.copiardestring(SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].LlaveArchivo, "|", 1).TrimB();
                GetPrty0.Llave.Text = MODGPYF0.Componer(s, "~", "");
                //Se debe bloquear si esta seleccionado en operacion
                GetPrty0.Llave.Enabled = !GetPrty0.Donde[1].Checked;
                //GetPrty0.Llave.SelectionStart = Llave.Text.Len();
                // If Trim$(Llave.Text) <> "" Then
                //     Llave.SelStart = Len(Llave.Text)
                // Else
                //     Llave.SelStart = 0
                // End If
                GetPrty0.Identificar.Text = "Modificar";
                GetPrty0.Aceptar.Enabled = true;
                if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Status == T_SYGETPRT.GPrt_StatLleno)
                {
                    GetPrty0.Eliminar.Enabled = true;
                }
                else
                {
                    GetPrty0.Eliminar.Enabled = false;
                }
                if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Status > T_SYGETPRT.GPrt_StatDatosLleno)
                {
                    GetPrty0.Identificar.Enabled = false;
                }
                else
                {
                    GetPrty0.Identificar.Enabled = true;
                }

                GetPrty0.Instrucciones.Enabled = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.PrtyFlag(SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].FlagParty, T_SYGETPRT.GPrt_FlagInst) != 0;
                // Orieta
                GetPrty0.Instrucciones.Enabled = false;
                // Orieta
            }

            // OGG    29/10/1998.-
            if (SYGETPRT.Codop.Id_Product == T_MODGUSR.IdPro_ComExp || SYGETPRT.Codop.Id_Product == T_MODGUSR.IdPro_DesExp)
            {
                if (SYGETPRT.PrtControl.Indice == IBCOS)
                {
                    GetPrty0._Donde_1.Enabled = false;
                }
                else
                {
                    GetPrty0._Donde_1.Enabled = true;
                }
            }
            // OGG    29/10/1998.-

            if (GetPrty0.EnLoad != 0)
            {
                return;
            }

            if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Status <= T_SYGETPRT.GPrt_StatDatosLleno)
            {
                if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Ubicacion == T_SYGETPRT.GPrt_EnParty)
                {
                    
                }
                else
                {
                    
                }
            }
            ShowDir(Globales);
        }

        public static void LstPartys_DblClick(DatosGlobales Globales)
        {
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            int ant = 0;
            string Valor = "";
            int ok = 0;
            string Respa = "";
            bool Modo = false;
            string Texto = "";
            string Que = "";
            string Txt = "";
            int i = 0;
            string Cr = "";

            Cr = 13.Char() + 10.Char();
            i = SYGETPRT.PrtControl.LimInf + GetPrty0.LstPartys.ListIndex;
            Txt = MODGPYF0.copiardestring(GetPrty0.LstPartys.Items[GetPrty0.LstPartys.ListIndex].Value.ToString(), 9.Char(), 1);

            if (Txt != T_SYGETPRT.GPrt_TxtOtros && SYGETPRT.PrtTbl[i].Left(1) != T_SYGETPRT.GPrt_MarcaOtros)
            {
                LstPartys_Click(Globales);
                return;
            }

            // si tiene marca es una edicion
            if (SYGETPRT.PrtTbl[i].Left(1) == T_SYGETPRT.GPrt_MarcaOtros)
            {
                Que = "Para modificar el papel de este participante en la operación debe ingresar el nuevo rol que este juega en la operacion." + Cr + Cr;
                Que = Que + "Ingrese una breve descripción del papel de este participante en la operación.";
                Texto = SYGETPRT.PrtTbl[i].Mid(2);
                Modo = true;
            }
            else
            {
                // uno nuevo
                Que = "Para identificar un nuevo participante, debe primero ingresar el rol que este juega en la operacion." + Cr + Cr;
                Que = Que + "Ingrese una breve descripción del papel de este participante en la operación.";
                Texto = "";
                Modo = false;
            }

            Respa = Texto;
            GetPrty0.MuestraSceInputBox = true;
            GetPrty0.CAPTION = T_SYGETPRT.GPrt_Caption;
            GetPrty0.DESCRIPTION = Que;
            GetPrty0.MODO = Modo;
            //MigrationSupport.Utils.MsgBox(SYGETPRT.GPrt_InputCambio, MODGPYF0.pito(48).Cast<MigrationSupport.MsgBoxStyle>(), SYGETPRT.GPrt_Caption);
        }

        public static void LstPartys_DblClick_Post(DatosGlobales Globales,string Texto)
        {
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            bool Modo = GetPrty0.MODO;
            string Valor = String.Empty;
            int ant = 0;
            int i = SYGETPRT.PrtControl.LimInf + GetPrty0.LstPartys.ListIndex;
            if (Modo)
            {
                Valor = MODGPYF0.copiardestring(GetPrty0.LstPartys.Items[GetPrty0.LstPartys.ListIndex].Value.ToString(), 9.Char(), 2);
                GetPrty0.LstPartys.Items[GetPrty0.LstPartys.ListIndex].Value = Texto + 9.Char() + Valor;
            }
            else
            {
                if (SYGETPRT.PrtControl.Otros - SYGETPRT.PrtControl.PorInsertar > 1)
                {
                    ant = GetPrty0.LstPartys.ListIndex;
                    GetPrty0.LstPartys.Items.Insert(GetPrty0.LstPartys.ListIndex, new UI_ListBoxItem() { Data=GetPrty0.LstPartys.Items.Count, Value= Texto + 9.Char() }  );
                    GetPrty0.LstPartys.ListIndex= ant;
                }
                else
                {
                    GetPrty0.LstPartys.Items[GetPrty0.LstPartys.ListIndex].Value = Texto + 9.Char();
                }
                SYGETPRT.PrtControl.PorInsertar = SYGETPRT.PrtControl.PorInsertar + 1;
            }
            SYGETPRT.PrtTbl[i] = T_SYGETPRT.GPrt_MarcaOtros + Texto;
        }

        private static int VerPartySy_1_2(DatosGlobales Globales, UnitOfWorkCext01 unit, ref int QueTipo, ref int flag, ref string Rut, ref string Swift, ref string Bco, int EsBanco)
        {

            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;

            int VerPartySy = 0;

            double TieneRut = 0.0;
            int Borrado = 0;
            string s = "";

            try
            {

                s = GetPrty0.Llave.Tag.ToStr();

                var res = unit.SceRepository.EjecutarSP<pro_sce_prty_s02_MS_Result>("pro_sce_prty_s02_MS", MODGSYB.dbcharSy(s.ToUpper()), "1");

                if (res.Count == 0)
                {
                    VerPartySy = T_SYGETPRT.GPrt_RetCancelo;
                }
                else
                {
                    var item = res.First();
                    Borrado = item.borrado.ToInt();
                    QueTipo = item.tipo_party.ToInt();
                    flag = item.flag.ToInt();
                    TieneRut = item.tiene_rut.ToDbl();
                    Rut = item.rut;
                    Bco = item.cod_bco.ToString();
                    Swift = item.swift;

                    GetPrty0.BORRADO = Borrado;
                    GetPrty0.QUETIPO = QueTipo;
                    GetPrty0.FLAG = flag;
                    GetPrty0.TIENERUT = TieneRut;
                    GetPrty0.RUT = Rut;
                    GetPrty0.BCO = Bco;
                    GetPrty0.SWIFT = Swift;

                    // Obtiene datos del Party.-
                    VerPartySy = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.EligeSy_1_2(Globales, unit);
                }
            }
            catch (Exception exc)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = "Se ha producido un error al tratar de leer los datos de los Participantes.",
                    Title = T_SYGETPRT.GPrt_Caption,
                    Type = TipoMensaje.Error
                });
                VerPartySy = T_SYGETPRT.GPrt_RetCancelo;
            }
            return VerPartySy;
        }

        private static int VerPartySy_2_2(DatosGlobales Globales, UnitOfWorkCext01 unit, ref int QueTipo, ref int flag, ref string Rut, ref string Swift, ref string Bco, int EsBanco)
        {
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;

            QueTipo = GetPrty0.QUETIPO;
            flag = GetPrty0.FLAG;
            Rut = GetPrty0.RUT;
            Bco = GetPrty0.BCO;
            Swift = GetPrty0.SWIFT;
            return SYGETPRT.EligeSy_2_2(Globales, unit);
        }

        // Modifica la Estructura de Partys
        private static void UpdatePartys(DatosGlobales Globales, int Tipo, int Donde, int flag, string Rut, string Swift, string Bco)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;
            UI_GetPrty1 GetPrty1 = Globales.GetPrty1;

            string Txt = "";
            int n = 0;
            int Cual = 0;

            if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Status == T_SYGETPRT.GPrt_StatVacio)
            {
                SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Status = T_SYGETPRT.GPrt_StatLleno;
            }

            // nombre
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].LlaveArchivo = GetPrty0.Llave.Tag.ToStr();
            Cual = GetPrty1.Nome.ListIndex;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].NombreUsado = GetPrty1.Nome.Items[Cual].Value.ToStr();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].IndNombre = GetPrty1.Nome.get_ItemData_(Cual).ToInt();

            // direccion
            Cual = GetPrty1.Dire.ListIndex;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].DireccionUsado = GetPrty1.Dire.Items[Cual].Value.ToStr();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].IndDireccion = GetPrty1.Dire.get_ItemData_(Cual).ToInt();

            n = 3;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].ComunaUsado = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 1)).TrimR();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].CiudadUsado = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 2)).TrimR();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].PostalUsado = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 3)).TrimR();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].PaisUsado = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 4)).TrimR();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].codpais = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 5)).ToInt();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].EstadoUsado = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 6)).TrimR();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Telefono = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 7)).TrimR();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Fax = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 8)).TrimR();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Telex = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 9)).TrimR();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Enviara = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 10)).TrimR().ToInt();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].CasPostal = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 11)).TrimR();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].CasBanco = MODGPYF0.copiardestring(GetPrty1.Otro.Items[Cual].Value.ToStr(), "~", (short)(n + 12)).TrimR();

            // resetea
            if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Telefono.ToVal() == 0)
            {
                SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Telefono = "";
            }
            if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Fax.ToVal() == 0)
            {
                SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Fax = "";
            }

            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].TipoParty = Tipo;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Ubicacion = Donde;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].FlagParty = flag;
            // INICIO IR46139 RTO 16/03/2012
            if (String.IsNullOrEmpty(Rut))
            {
                string RUT_L = "";
                int Mylen = 0;
                Mylen = SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].LlaveArchivo.Len();
                RUT_L = SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].LlaveArchivo.Mid(1, Mylen - 3);
                RUT_L = MigrationSupport.Utils.Format(RUT_L, "#0000000000");
                if (BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.EsRut(RUT_L) != 0)
                {
                    SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Rut = RUT_L;
                }
                else
                {
                    SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Rut = Rut;
                }
            }
            else
            {
                SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Rut = Rut;
            }
            // Partys(PrtControl.Indice).Rut = Rut
            // TERMINO IR46139 RTO

            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Swift = Swift;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].CodBanco = Bco;

            // update lista
            Txt = MODGPYF0.copiardestring(GetPrty0.LstPartys.Items[GetPrty0.LstPartys.ListIndex].Value.ToString(), 9.Char(), 1);
            GetPrty0.LstPartys.Items[GetPrty0.LstPartys.ListIndex].Value = Txt.TrimB() + 9.Char() + SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].NombreUsado.TrimB();

            Globales.GetPrty1 = null;
        }

        // Pegado a operacion
        private static int VerPartyOperacion_1_2(DatosGlobales Globales, UnitOfWorkCext01 unit, ref int Indice)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;

            int VerPartyOperacion = 0;
            string valor;
            int abc = 0;
            int PopeBase = 0;
            int j = 0;
            int i = 0;
            int Lim = 0;
            double nuestro_pais = 0.0;
            Globales.GetPrty3 = new UI_GetPrty3();
            UI_GetPrty3 GetPrty3 = Globales.GetPrty3;

            PartysPope aux_pope = new PartysPope();

            // cargo pantalla y propiedades
            BorraGetPrty3(Globales);

            nuestro_pais = MODGPYF0.GetSceIni("Pais", "CodPais").ToVal();

            (Globales.GetPrty3.Rut).Mask = UI_GetPrty3.GPrt_RutMask;

            // veamos primero en memoria
            Lim = -1;
            GetPrty0.PopeInd = -1;
            Lim = SYGETPRT.Pope.GetUpperBound(0);
            
            if (Lim >= 0)
            {
                // tiene elementos
                for (i = 0; i <= Lim; i += 1)
                {
                    if (SYGETPRT.Pope[i].Secuencia == SYGETPRT.PrtControl.Indice)
                    {
                        if (SYGETPRT.Pope[i].Status != T_SYGETPRT.GPrt_StatBorro && SYGETPRT.Pope[i].Status != T_SYGETPRT.GPrt_StatVacio)
                        {
                            if (SYGETPRT.Pope[i].EsBanco != 0)
                            {
                                (GetPrty3.Rut).Mask = UI_GetPrty3.GPrt_SwiftMask;
                                GetPrty3.EsBanco.Checked = true;
                                //(GetPrty3.Rut). = SYGETPRT.DesCero(SYGETPRT.Pope[i].RutSwift, GPrt_SwiftMascara, true.ToInt());
                                GetPrty3.Rut.Tag = "1";
                                GetPrty3.Rut.Text = SYGETPRT.Pope[i].RutSwift;
                                GetPrty3.MaskedRut = SYGETPRT.Pope[i].RutSwift.PadLeft(10, '0'); //BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.DesCero(SYGETPRT.Pope[i].RutSwift.PadLeft(10, '0'), UI_GetPrty3.GPrt_SwiftMascara, true.ToInt());
                            }
                            else
                            {
                                (GetPrty3.Rut).Mask = UI_GetPrty3.GPrt_RutMask;
                                GetPrty3.EsBanco.Checked = false;
                                //(GetPrty3.Rut).SelText = SYGETPRT.DesCero(SYGETPRT.Pope[i].RutSwift, GPrt_RutMascara, false.ToInt());
                                GetPrty3.Rut.Tag = "0";
                                GetPrty3.Rut.Text = SYGETPRT.Pope[i].RutSwift;
                                GetPrty3.MaskedRut = SYGETPRT.Pope[i].RutSwift.PadLeft(10, '0');// BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.DesCero(SYGETPRT.Pope[i].RutSwift.PadLeft(10,'0'), UI_GetPrty3.GPrt_RutMascara, false.ToInt());
                            }
                            GetPrty3.Nombre.Text = SYGETPRT.Pope[i].Nombre;
                            GetPrty3.Direccion.Text = SYGETPRT.Pope[i].Direccion;

                            for (j = 0; j <= GetPrty3.Pais.Items.Count - 1; j += 1)
                            {
                                if (GetPrty3.Pais.get_ItemData_(j).ToInt() == -1 && SYGETPRT.Pope[i].codpais != -1)
                                {
                                    GetPrty3.Pais.Items[j].Value = UI_GetPrty3.GPrt_OtroPais;
                                }
                                else if (GetPrty3.Pais.get_ItemData_(j).ToInt() == SYGETPRT.Pope[i].codpais)
                                {
                                    GetPrty3.Pais.ListIndex = j;
                                    if (SYGETPRT.Pope[i].codpais < 0)
                                    {
                                        GetPrty3.Pais.Items[j].Value = UI_GetPrty3.GPrt_OtroPais + SYGETPRT.Pope[i].Pais;
                                    }
                                    break;
                                }
                            }

                            GetPrty3.comuna.Text = SYGETPRT.Pope[i].comuna;
                            GetPrty3.Ciudad.Text = SYGETPRT.Pope[i].Ciudad;
                            GetPrty3.Estado.Text = SYGETPRT.Pope[i].estado;
                            GetPrty3.Postal.Text = SYGETPRT.Pope[i].Postal;

                            (GetPrty3.Telefono).Mask = "";
                            (GetPrty3.Fax).Mask = "";
                            GetPrty3.Telefono.Text = SYGETPRT.Pope[i].Telefono;
                            GetPrty3.Fax.Text = SYGETPRT.Pope[i].Fax;
                            if (GetPrty3.Pais.ListIndex != -1)
                            {
                                if (GetPrty3.Pais.get_ItemData_(GetPrty3.Pais.ListIndex).ToDbl() == nuestro_pais)
                                {
                                    (GetPrty3.Telefono).Mask = UI_GetPrty3.GPrt_FonoMask;
                                    (GetPrty3.Fax).Mask = UI_GetPrty3.GPrt_FonoMask;
                                    //(GetPrty3.Telefono).SelText = SYGETPRT.DesCero(SYGETPRT.Pope[i].Telefono, GPrt_FonoMascara, false.ToInt());
                                    //(GetPrty3.Fax).SelText = SYGETPRT.DesCero(SYGETPRT.Pope[i].Fax, GPrt_FonoMascara, false.ToInt());
                                }
                            }
                            GetPrty3.Telex.Text = SYGETPRT.Pope[i].Telex;
                            GetPrty3.cas_postal.Text = SYGETPRT.Pope[i].CasPostal;
                            GetPrty3.cas_bco.Text = SYGETPRT.Pope[i].CasBanco;
                            GetPrty3.envio[SYGETPRT.Pope[i].Enviara].Checked = true;
                        }
                        GetPrty0.PopeInd = i;
                        break;
                    }
                }
            }

            // Si no esta en memoria buscarlo en disco
            if (GetPrty0.PopeInd < 0)
            {
                // destino bases de datos

                pro_sce_prty_s03_MS_Result pope = null;
                try
                {
                    pope = unit.SceRepository.pro_sce_prty_s03_MS(
                        Globales.SYGETPRT.PrtControl.NumOpe.Cent_costo,
                        Globales.SYGETPRT.PrtControl.NumOpe.Id_Product,
                        Globales.SYGETPRT.PrtControl.NumOpe.Id_Especia,
                        Globales.SYGETPRT.PrtControl.NumOpe.Id_Empresa,
                        Globales.SYGETPRT.PrtControl.NumOpe.Id_Operacion,
                        VB6Helpers.Format(VB6Helpers.CStr(Globales.SYGETPRT.PrtControl.Indice), "00"),
                        1);
                }
                catch (Exception)
                {
                    Globales.MESSAGES.Add(new UI_Message
                    {
                        Text = "Hubo un problema al conectar con sybase para leer Participantes en Operación (sce_pope)",
                        Type = TipoMensaje.Error,
                        Title = T_SYGETPRT.GPrt_Caption
                    });
                    VerPartyOperacion = T_SYGETPRT.GPrt_RetCancelo;
                    return VerPartyOperacion;
                }
                if (pope == null)
                {
                    //no existe
                    PopeBase = T_SYGETPRT.GPrt_StatNuevo;
                }
                else
                {
                    // Traspasamos la lectura a variables auxiliares.
                    //Traspasamos la lectura a variables auxiliares.
                    aux_pope.EsBanco = (short)(pope.esbanco ? -1 : 0);
                    aux_pope.RutSwift = pope.rut;
                    aux_pope.Nombre = pope.razon_soci;
                    aux_pope.Direccion = pope.direccion;
                    aux_pope.comuna = pope.comuna;
                    aux_pope.estado = pope.estado;
                    aux_pope.Ciudad = pope.ciudad;
                    aux_pope.Pais = pope.pais;
                    aux_pope.codpais = (short)pope.cod_pais;
                    aux_pope.Postal = pope.cod_postal;
                    aux_pope.Telefono = pope.telefono;
                    aux_pope.Fax = pope.fax;
                    aux_pope.Telex = pope.telex;
                    aux_pope.Enviara = (short)pope.envio_sce;
                    aux_pope.CasPostal = pope.cas_postal;
                    aux_pope.CasBanco = pope.cas_banco;



                    abc = aux_pope.EsBanco;
                    if (abc != 0)
                    {
                        (GetPrty3.Rut).Mask = UI_GetPrty3.GPrt_SwiftMask;
                        GetPrty3.EsBanco.Checked = true;
                        //((dynamic)GetPrty3Instance.Rut).SelText = SYGETPRT.DesCero(aux_pope.RutSwift.TrimB(), GPrt_SwiftMascara, true.ToInt());
                    }
                    else
                    {
                        (GetPrty3.Rut).Mask = UI_GetPrty3.GPrt_RutMask;
                        GetPrty3.EsBanco.Checked = false;
                        //((dynamic)GetPrty3Instance.Rut).SelText = SYGETPRT.DesCero(aux_pope.RutSwift.TrimB(), GPrt_RutMascara, false.ToInt());
                    }

                    GetPrty3.Nombre.Text = aux_pope.Nombre.TrimR();
                    GetPrty3.Direccion.Text = aux_pope.Direccion.TrimR();
                    GetPrty3.comuna.Text = aux_pope.comuna.TrimR();
                    GetPrty3.Estado.Text = aux_pope.estado.TrimR();
                    GetPrty3.Ciudad.Text = aux_pope.Ciudad.TrimR();

                    abc = aux_pope.codpais;
                    for (j = 0; j <= GetPrty3.Pais.Items.Count - 1; j += 1)
                    {
                        if (GetPrty3.Pais.get_ItemData_(j).ToInt() == -1 && abc != -1)
                        {
                            GetPrty3.Pais.Items[j].Value = UI_GetPrty3.GPrt_OtroPais;
                        }
                        else if (GetPrty3.Pais.get_ItemData_(j).ToInt() == abc)
                        {
                            GetPrty3.Pais.ListIndex = j;
                            if (abc < 0)
                            {
                                GetPrty3.Pais.Items[j].Value = UI_GetPrty3.GPrt_OtroPais + aux_pope.Pais.TrimB();
                            }
                            break;
                        }
                    }

                    GetPrty3.Postal.Text = aux_pope.Postal.TrimR();

                    if (GetPrty3.Pais.get_ItemData_(GetPrty3.Pais.ListIndex).ToDbl() == nuestro_pais)
                    {
                        (GetPrty3.Telefono).Mask = UI_GetPrty3.GPrt_FonoMask;
                        (GetPrty3.Fax).Mask = UI_GetPrty3.GPrt_FonoMask;
                        //(GetPrty3.Telefono).SelText = SYGETPRT.DesCero(aux_pope.Telefono.TrimR(), GPrt_FonoMascara, false.ToInt());
                        //(GetPrty3.Fax).SelText = SYGETPRT.DesCero(aux_pope.Fax.TrimR(), GPrt_FonoMascara, false.ToInt());
                    }
                    else
                    {
                        (GetPrty3.Telefono).Mask = "";
                        (GetPrty3.Fax).Mask = "";
                        GetPrty3.Telefono.Text = aux_pope.Telefono.TrimR();
                        GetPrty3.Fax.Text = aux_pope.Fax.TrimR();
                    }

                    GetPrty3.Telex.Text = aux_pope.Telex.TrimR();
                    GetPrty3.cas_postal.Text = aux_pope.CasPostal.TrimR();
                    GetPrty3.cas_bco.Text = aux_pope.CasBanco.TrimR();
                    GetPrty3.envio[aux_pope.Enviara].Checked = true;

                    valor = "";
                    PopeBase = T_SYGETPRT.GPrt_StatIntacto;
                }
            }

            GetPrty0.Lim = Lim;
            GetPrty0.PopeBase = PopeBase;
            // desplegar los datos o capturar
            //GetPrty3Instance.ShowDialog();
            Globales.Controller = "Participantes";
            Globales.Action = "Crear";

            return 0;
        }

        public static int VerPartyOperacion_2_2(DatosGlobales Globales, ref int Indice)
        {
            UI_GetPrty3 GetPrty3 = Globales.GetPrty3;
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            int GPrt_StatVacion = 0;
            int Lim = GetPrty0.Lim;
            int PopeBase = GetPrty0.PopeBase;

            int PopeInd = GetPrty0.PopeInd;
            var nuestro_pais = MODGPYF0.GetSceIni("Pais", "CodPais").ToVal();
            int kk = 0;
            int intCaseArg = 0;

            // acepto
            if (GetPrty3.Aceptar.Tag.ToVal() == T_SYGETPRT.GPrt_RetExiste)
            {
                if (GetPrty0.PopeInd >= 0)
                {
                    // edito alguno ya en memoria
                    SYGETPRT.Pope[GetPrty0.PopeInd].EsBanco = (GetPrty3.EsBanco.Checked.ToInt());
                    if (SYGETPRT.Pope[PopeInd].EsBanco != 0)
                    {
                        SYGETPRT.Pope[PopeInd].RutSwift = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.FilCero(GetPrty3.Rut.Text, UI_GetPrty3.GPrt_SwiftMascara, true.ToInt());
                    }
                    else
                    {
                        SYGETPRT.Pope[PopeInd].RutSwift = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.FilCero(GetPrty3.Rut.Text, UI_GetPrty3.GPrt_RutMascara, false.ToInt());
                    }
                    SYGETPRT.Pope[PopeInd].Nombre = GetPrty3.Nombre.Text;
                    SYGETPRT.Pope[PopeInd].Direccion = GetPrty3.Direccion.Text;
                    SYGETPRT.Pope[PopeInd].comuna = GetPrty3.comuna.Text;
                    SYGETPRT.Pope[PopeInd].Ciudad = GetPrty3.Ciudad.Text;
                    SYGETPRT.Pope[PopeInd].estado = GetPrty3.Estado.Text;
                    if (GetPrty3.Pais.ListIndex != -1)
                    {
                        SYGETPRT.Pope[PopeInd].codpais = GetPrty3.Pais.get_ItemData_(GetPrty3.Pais.ListIndex).ToInt();
                    }
                    else {
                        SYGETPRT.Pope[PopeInd].codpais = -1;
                    }
                    if (SYGETPRT.Pope[PopeInd].codpais == -1)
                    {
                        SYGETPRT.Pope[PopeInd].Pais = string.Empty;//MODGPYF0.copiardestring(GetPrty3.Pais.Items[GetPrty3.Pais.ListIndex].Value.ToStr(), ")", 2).TrimB();
                    }
                    else
                    {
                        SYGETPRT.Pope[PopeInd].Pais = GetPrty3.Pais.Items[GetPrty3.Pais.ListIndex].Value.ToStr();
                    }
                    SYGETPRT.Pope[PopeInd].Postal = GetPrty3.Postal.Text;

                    SYGETPRT.Pope[PopeInd].Telefono = GetPrty3.Telefono.Text;
                    SYGETPRT.Pope[PopeInd].Fax = GetPrty3.Fax.Text;
                    if (GetPrty3.Pais.ListIndex != -1)
                    {
                        if (GetPrty3.Pais.get_ItemData_(GetPrty3.Pais.ListIndex).ToDbl() == nuestro_pais)
                        {
                            SYGETPRT.Pope[PopeInd].Telefono = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.FilCero(GetPrty3.Telefono.Text, UI_GetPrty3.GPrt_FonoMascara, false.ToInt());
                            SYGETPRT.Pope[PopeInd].Fax = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.FilCero(GetPrty3.Fax.Text, UI_GetPrty3.GPrt_FonoMascara, false.ToInt());
                        }
                    }

                    SYGETPRT.Pope[PopeInd].Telex = GetPrty3.Telex.Text;
                    SYGETPRT.Pope[PopeInd].CasPostal = GetPrty3.cas_postal.Text;
                    SYGETPRT.Pope[PopeInd].CasBanco = GetPrty3.cas_bco.Text;
                    for (kk = 0; kk <= 3; kk += 1)
                    {
                        if (GetPrty3.envio[kk].Checked)
                        {
                            SYGETPRT.Pope[PopeInd].Enviara = kk;
                        }
                    }
                    intCaseArg = SYGETPRT.Pope[PopeInd].Status;
                    if (intCaseArg == T_SYGETPRT.GPrt_StatNuevo)
                    {
                    }
                    else if (intCaseArg == T_SYGETPRT.GPrt_StatCambio)
                    {
                    }
                    else if (intCaseArg == T_SYGETPRT.GPrt_StatBorro)
                    {
                        SYGETPRT.Pope[PopeInd].Status = T_SYGETPRT.GPrt_StatCambio;
                    }
                    else if (intCaseArg == T_SYGETPRT.GPrt_StatIntacto)
                    {
                        SYGETPRT.Pope[PopeInd].Status = T_SYGETPRT.GPrt_StatCambio;
                    }
                    else if (intCaseArg == GPrt_StatVacion)
                    {
                        SYGETPRT.Pope[PopeInd].Status = T_SYGETPRT.GPrt_StatNuevo;
                    }
                    Indice = PopeInd;
                }
                else
                {
                    // desde la base, agregar a memoria
                    Lim = Lim + 1;
                    Array.Resize(ref SYGETPRT.Pope, Lim + 1);
                    SYGETPRT.Pope[Lim] = new PartysPope();
                    SYGETPRT.Pope[Lim].EsBanco = (GetPrty3.EsBanco.Checked).ToInt();
                    if (SYGETPRT.Pope[Lim].EsBanco != 0)
                    {
                        SYGETPRT.Pope[Lim].RutSwift = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.FilCero(GetPrty3.Rut.Text, UI_GetPrty0.GPrt_SwiftMascara, true.ToInt());
                    }
                    else
                    {
                        SYGETPRT.Pope[Lim].RutSwift = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.FilCero(GetPrty3.Rut.Text, UI_GetPrty0.GPrt_RutMascara, false.ToInt());
                    }
                    SYGETPRT.Pope[Lim].Nombre = GetPrty3.Nombre.Text;
                    SYGETPRT.Pope[Lim].Direccion = GetPrty3.Direccion.Text;
                    SYGETPRT.Pope[Lim].comuna = GetPrty3.comuna.Text;
                    SYGETPRT.Pope[Lim].Ciudad = GetPrty3.Ciudad.Text;
                    SYGETPRT.Pope[Lim].estado = GetPrty3.Estado.Text;
                    if (GetPrty3.Pais.ListIndex != -1)
                    {
                        SYGETPRT.Pope[Lim].codpais = GetPrty3.Pais.get_ItemData_(GetPrty3.Pais.ListIndex).ToInt();
                    }
                    else
                    {
                        SYGETPRT.Pope[Lim].codpais = -1;
                    }
                    if (SYGETPRT.Pope[Lim].codpais == -1)
                    {
                        SYGETPRT.Pope[Lim].Pais = string.Empty;//MODGPYF0.copiardestring(GetPrty3.Pais.Items[GetPrty3.Pais.ListIndex].Value.ToStr(), ")", 2).TrimB();
                    }
                    else
                    {
                        SYGETPRT.Pope[Lim].Pais = GetPrty3.Pais.Items[GetPrty3.Pais.ListIndex].Value.ToStr();
                    }
                    SYGETPRT.Pope[Lim].Postal = GetPrty3.Postal.Text;

                    nuestro_pais = MODGPYF0.GetSceIni("Pais", "CodPais").ToVal();

                    if (GetPrty3.Pais.ListIndex != -1)
                    {
                        if (GetPrty3.Pais.get_ItemData_(GetPrty3.Pais.ListIndex).ToDbl() == nuestro_pais)
                        {
                            SYGETPRT.Pope[Lim].Telefono = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.FilCero(GetPrty3.Telefono.Text, UI_GetPrty0.GPrt_FonoMascara, false.ToInt());
                            SYGETPRT.Pope[Lim].Fax = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.FilCero(GetPrty3.Fax.Text, UI_GetPrty0.GPrt_FonoMascara, false.ToInt());
                        }
                        else
                        {
                            SYGETPRT.Pope[Lim].Telefono = GetPrty3.Telefono.Text;
                            SYGETPRT.Pope[Lim].Fax = GetPrty3.Fax.Text;
                        }
                    }
                    else
                    {
                        SYGETPRT.Pope[Lim].Telefono = GetPrty3.Telefono.Text;
                        SYGETPRT.Pope[Lim].Fax = GetPrty3.Fax.Text;
                    }

                    SYGETPRT.Pope[Lim].Telex = GetPrty3.Telex.Text;
                    SYGETPRT.Pope[Lim].CasPostal = GetPrty3.cas_postal.Text;
                    SYGETPRT.Pope[Lim].CasBanco = GetPrty3.cas_bco.Text;
                    for (kk = 0; kk <= 3; kk += 1)
                    {
                        if (GetPrty3.envio[kk].Checked)
                        {
                            SYGETPRT.Pope[Lim].Enviara = kk;
                        }
                    }
                    SYGETPRT.Pope[Lim].Secuencia = SYGETPRT.PrtControl.Indice;
                    SYGETPRT.Pope[Lim].Status = PopeBase;
                    if (PopeBase == T_SYGETPRT.GPrt_StatIntacto)
                    {
                        SYGETPRT.Pope[Lim].Status = T_SYGETPRT.GPrt_StatCambio;
                    }
                    Indice = Lim;
                }
            }
            return GetPrty3.Aceptar.Tag.ToInt();
        }

        private static void UpdatePope(DatosGlobales Globales, int Cual)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;

            string Txt = "";
            int i = 0;

            if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Status == T_SYGETPRT.GPrt_StatVacio)
            {
                SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Status = T_SYGETPRT.GPrt_StatLleno;
            }

            // nombre
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].LlaveArchivo = GetPrty0.Llave.Tag.ToStr();
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].NombreUsado = SYGETPRT.Pope[Cual].Nombre;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].IndNombre = 0;

            // direccion
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].DireccionUsado = SYGETPRT.Pope[Cual].Direccion;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].IndDireccion = 0;

            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].ComunaUsado = SYGETPRT.Pope[Cual].comuna;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].CiudadUsado = SYGETPRT.Pope[Cual].Ciudad;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].PostalUsado = SYGETPRT.Pope[Cual].Postal;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].CasPostal = SYGETPRT.Pope[Cual].CasPostal;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].PaisUsado = SYGETPRT.Pope[Cual].Pais;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].codpais = SYGETPRT.Pope[Cual].codpais;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].EstadoUsado = SYGETPRT.Pope[Cual].estado;

            if (SYGETPRT.Pope[i].EsBanco != 0)
            {
                SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].TipoParty = T_SYGETPRT.GPrt_TipoBcoOperacion;
            }
            else
            {
                SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].TipoParty = T_SYGETPRT.GPrt_TipoEnOperacion;
            }

            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Ubicacion = T_SYGETPRT.GPrt_EnOperacion;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].FlagParty = 0;
            if (SYGETPRT.Pope[Cual].EsBanco != 0)
            {
                SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Rut = "";
                SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Swift = SYGETPRT.Pope[Cual].RutSwift;
            }
            else
            {
                SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Rut = SYGETPRT.Pope[Cual].RutSwift;
                SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Swift = "";
            }
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].CodBanco = "";

            // Más datos.-
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Telefono = SYGETPRT.Pope[Cual].Telefono;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Fax = SYGETPRT.Pope[Cual].Fax;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Telex = SYGETPRT.Pope[Cual].Telex;

            // update lista
            Txt = MODGPYF0.copiardestring(GetPrty0.LstPartys.SelectedItem.ToString(), 9.Char(), 1);
            GetPrty0.LstPartys.Items[GetPrty0.LstPartys.ListIndex].Value = Txt + 9.Char() + SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].NombreUsado;
            GetPrty0.LstPartys.Items[GetPrty0.LstPartys.ListIndex].Data = 0;

        }

        private static void BorraGetPrty3(DatosGlobales Globales)
        {
            UI_GetPrty3 GetPrty3 = Globales.GetPrty3;

            GetPrty3.EsBanco.Checked = false;
            GetPrty3.Rut.Text = UI_GetPrty0.GPrt_RutMascara;
            GetPrty3.Nombre.Text = "";
            GetPrty3.Direccion.Text = "";
            GetPrty3.Pais.ListIndex = -1;
            GetPrty3.comuna.Text = "";
            GetPrty3.Ciudad.Text = "";
            GetPrty3.Estado.Text = "";
            GetPrty3.Postal.Text = "";
            GetPrty3.Telefono.Text = UI_GetPrty0.GPrt_FonoMascara;
            GetPrty3.Fax.Text = UI_GetPrty0.GPrt_FonoMascara;
            GetPrty3.Telex.Text = "";
            GetPrty3.cas_postal.Text = "";
            GetPrty3.cas_bco.Text = "";
            GetPrty3.envio[0].Checked = true;

        }

        public static void Eliminar_Click(DatosGlobales Globales)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MODGLORI MODGLORI = Globales.MODGLORI;
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;

            bool resul = false;
            int i = 0;
            int Lim = 0;
            int Ubica = 0;
            PartyKey BorraParty = new PartyKey();

            if (SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Status >= T_SYGETPRT.GPrt_StatDatos)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = T_SYGETPRT.GPrt_ErrEliminar,
                    Type = TipoMensaje.Error,
                    Title = T_SYGETPRT.GPrt_Caption
                });
                return;
            }

            Ubica = SYGETPRT.Partys[SYGETPRT.PrtControl.Indice].Ubicacion;
            SYGETPRT.Partys[SYGETPRT.PrtControl.Indice] = BorraParty;

            MODGLORI.Titulo = MODGPYF0.copiardestring(GetPrty0.LstPartys.SelectedItem.ToString(), 9.Char(), 1);
            GetPrty0.LstPartys.Items[GetPrty0.LstPartys.ListIndex].Value = MODGLORI.Titulo;

            GetPrty0._Donde_0.Checked = true;
            GetPrty0.Eliminar.Enabled = false;
            GetPrty0.Instrucciones.Enabled = false;

            GetPrty0.Identificar.Text = "Identificar";

            GetPrty0.Llave.Enabled = true;

            Lim = -1;
            if (Ubica == T_SYGETPRT.GPrt_EnOperacion)
            {
                // primero buscamos en memoria
                for (i = 0; i <= Lim; i += 1)
                {
                    if (SYGETPRT.Pope[i].Secuencia == SYGETPRT.PrtControl.Indice)
                    {
                        switch (SYGETPRT.Pope[i].Status)
                        {
                            case T_SYGETPRT.GPrt_StatNuevo:
                                SYGETPRT.Pope[i].Status = T_SYGETPRT.GPrt_StatVacio;
                                break;
                            case T_SYGETPRT.GPrt_StatCambio:
                            case T_SYGETPRT.GPrt_StatIntacto:
                                SYGETPRT.Pope[i].Status = T_SYGETPRT.GPrt_StatBorro;
                                break;
                        }
                        break;
                        resul = true;
                    }
                }

                // si no esta ==> en disco no leida
                if (!resul)
                {
                    Lim = Lim + 1;
                    Array.Resize(ref SYGETPRT.Pope, Lim + 1);
                    SYGETPRT.Pope[Lim].Secuencia = SYGETPRT.PrtControl.Indice;
                    SYGETPRT.Pope[Lim].Status = T_SYGETPRT.GPrt_StatBorro;
                }
            }

            GetPrty0.Llave.Text = "";
            LstPartys_Click(Globales);
        }

        public static void Aceptar_Click(DatosGlobales Globales)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            UI_GetPrty0 GetPrty0 = Globales.GetPrty0;

            bool Party3 = false;
            int IACEP = 0;
            bool Party2 = false;
            int IBCOS = 0;
            int IBCOA = 0;
            bool Party1 = false;
            int IExp = 0;
            string Txt = "";
            int i = 0;

            // buscar si ingreso los requeridos
            for (i = SYGETPRT.PrtControl.LimInf; i <= SYGETPRT.PrtControl.LimSup; i += 1)
            {
                if (SYGETPRT.PrtTbl[i] == "Cliente" && SYGETPRT.Partys[i].Status == T_SYGETPRT.GPrt_StatVacio)
                {
                   
                    Txt = SYGETPRT.PrtTbl[i] + T_SYGETPRT.GPrt_ErrRequerido;
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type=TipoMensaje.Error,
                        Text=Txt,
                        Title= T_SYGETPRT.GPrt_Caption
                    });
                    if (GetPrty0.LstPartys.ListIndex == i - SYGETPRT.PrtControl.LimInf)
                    {
                        LstPartys_Click(Globales);
                    }
                    else
                    {
                        GetPrty0.LstPartys.ListIndex= i - SYGETPRT.PrtControl.LimInf;
                    }
                    return;
                }
            }

            // OGG    30 Julio 1998.-
            if (SYGETPRT.Codop.Id_Product == T_MODGUSR.IdPro_ComExp || SYGETPRT.Codop.Id_Product == T_MODGUSR.IdPro_DesExp)
            {
                if (String.IsNullOrEmpty(SYGETPRT.Partys[IExp].LlaveArchivo))
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe ingresar el Exportador/Vendedor antes de continuar.",
                        Title = T_SYGETPRT.GPrt_Caption
                    });
                    Party1 = false;
                    return;
                }
                else
                {
                    Party1 = true;
                }
                if (String.IsNullOrEmpty(SYGETPRT.Partys[IBCOA].LlaveArchivo))
                {
                    if (SYGETPRT.Partys[IBCOA].TipoParty != T_SYGETPRT.GPrt_TipoBanco && SYGETPRT.Partys[IBCOA].TipoParty != T_SYGETPRT.GPrt_TipoBcoOperacion)
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "El Banco Aval debe ser un partys de tipo banco.",
                            Title = T_SYGETPRT.GPrt_Caption
                        });
                        return;
                    }
                }

                if (SYGETPRT.Partys[IBCOS].LlaveArchivo == "")
                {
                    Party2 = false;
                }
                else
                {
                    Party2 = true;
                    if (SYGETPRT.Partys[IBCOS].TipoParty != T_SYGETPRT.GPrt_TipoBanco && SYGETPRT.Partys[IBCOS].TipoParty != T_SYGETPRT.GPrt_TipoBcoOperacion)
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "El Banco Aceptante/Suscriptor debe ser un partys de tipo banco.",
                            Title = T_SYGETPRT.GPrt_Caption
                        });
                        return;
                    }
                }
                if (SYGETPRT.Partys[IACEP].LlaveArchivo == "")
                {
                    Party3 = false;
                }
                else
                {
                    Party3 = true;
                }
                if (!Party2 && !Party3)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe ingresar el Banco Aceptante/Suscriptor o Aceptante antes de continuar.",
                        Title = T_SYGETPRT.GPrt_Caption
                    });
                    return;
                }
                else if (Party2 && Party3)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe ingresar un sólo partys entre el Banco Aceptante/Suscriptor o Aceptante.",
                        Title = T_SYGETPRT.GPrt_Caption
                    });
                    return;
                }

                // OGG    29/10/1998.-
                switch (SYGETPRT.Codop.Id_Product)
                {
                    case T_MODGUSR.IdPro_ComExp:
                        if (SYGETPRT.Partys[IBCOS].LlaveArchivo != "")
                        {
                            if (SYGETPRT.Partys[IBCOS].Rut == "" || SYGETPRT.Partys[IBCOS].Rut == "0000000000")
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "Debe ingresar el rut del Banco Aceptante/Suscriptor antes de continuar.",
                                    Title = T_SYGETPRT.GPrt_Caption
                                });
                                return;
                            }
                        }
                        else if (SYGETPRT.Partys[IACEP].LlaveArchivo != "")
                        {
                            if (SYGETPRT.Partys[IACEP].Rut == "" || SYGETPRT.Partys[IACEP].Rut == "0000000000")
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "Debe ingresar el rut del Aceptante antes de continuar.",
                                    Title = T_SYGETPRT.GPrt_Caption
                                });
                                return;
                            }
                        }
                        if (SYGETPRT.Partys[IBCOA].LlaveArchivo != "")
                        {
                            if (SYGETPRT.Partys[IBCOA].Rut == "" || SYGETPRT.Partys[IBCOA].Rut == "0000000000")
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "Debe ingresar el rut del Banco Aval antes de continuar.",
                                    Title = T_SYGETPRT.GPrt_Caption
                                });
                                return;
                            }
                        }
                        break;
                    case T_MODGUSR.IdPro_DesExp:
                        if (SYGETPRT.Partys[IExp].LlaveArchivo != "")
                        {
                            if (SYGETPRT.Partys[IExp].Rut == "" || SYGETPRT.Partys[IExp].Rut == "0000000000")
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "Debe ingresar el rut del Exportador/Vendedor antes de continuar.",
                                    Title = T_SYGETPRT.GPrt_Caption
                                });
                                return;
                            }
                        }
                        if (SYGETPRT.Partys[IBCOA].LlaveArchivo != "")
                        {
                            if (SYGETPRT.Partys[IBCOA].Rut == "" || SYGETPRT.Partys[IBCOA].Rut == "0000000000")
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "Debe ingresar el rut del Banco Aval antes de continuar.",
                                    Title = T_SYGETPRT.GPrt_Caption
                                });
                            }
                        }
                        break;
                }
                // OGG    29/10/1998.-
            }
            // OGG    30 Julio 1998.-
            GetPrty0.Aceptar.Tag = "-1";
        }
    }
}
