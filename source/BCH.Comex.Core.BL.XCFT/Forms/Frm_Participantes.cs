using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public class Frm_Participantes
    {
        //mascara rut y fono
        private const string GPrt_RutMascara = "___.___.___-_";
        private const string GPrt_FonoMascara = "(___) ___-____";
        private const string GPrt_SwiftMascara = "____________";
        private const string GPrt_RutMask = @"###\.###\.###\-A";
        private const string GPrt_FonoMask = "(###) ###-####";
        private const string GPrt_SwiftMask = "&&&&&&&&&&&&";
        private const string GPrt_OtroPais = "(Otro pais) ";

        public static void Aceptar_Click(InitializationObject initObj)
        {
            short i = 0;
            string Txt = "";
            short Party1 = 0;
            short IBCOA = 0;
            short IBCOS = 0;
            short Party2 = 0;
            short IACEP = 0;
            short Party3 = 0;

            using (Tracer tracer = new Tracer("Aceptar Participantes"))
            {
                //Se agrega valicacion para detener en este punto posibles problemas con el numero de operacion y el codigo producto

                if (initObj.MODGCVD.VgCvd.codpro == T_MODGUSR.IdPro_Undefined || initObj.MODGCVD.VgCvd.codope == T_MODGUSR.CodOp_Undefined
                    //Se agrega una validación para segurarnos que el nro de operacion no venga en blanco (bug 1766)
                    || string.IsNullOrEmpty(initObj.MODGCVD.VgCvd.codpro) || string.IsNullOrEmpty(initObj.MODGCVD.VgCvd.codope))
                {
                    tracer.AddToContext("Problemas en determinar cod producto o num operacion", initObj.Module1.Codop.Id_Product + "-" + initObj.Module1.Codop.Id_Operacion);
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se detecto un problema al intentar determinar el número de operación o el código del producto, favor volver a identificar el participante"
                    });
                    return;
                }

                //buscar si ingreso los requeridos

                for (i = (short)initObj.Module1.PrtControl.LimInf; i <= (short)initObj.Module1.PrtControl.LimSup; i++)
                {
                    if (VB6Helpers.Left(initObj.Module1.PrtTbl[i], 1) == T_Module1.GPrt_MarcaRequerido &&
                        initObj.Module1.Partys[i].Status == T_Module1.GPrt_StatVacio)
                    {
                        Txt = VB6Helpers.Right(initObj.Module1.PrtTbl[i], VB6Helpers.Len(initObj.Module1.PrtTbl[i]) - 1)
                            + T_Module1.GPrt_ErrRequerido;

                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = Txt,
                            Type = TipoMensaje.Informacion,
                            Title = T_Module1.GPrt_Caption
                        });

                        if (initObj.Frm_Participantes.LstPartys.ListIndex == i - initObj.Module1.PrtControl.LimInf)
                        {
                            LstPartys_Click(initObj);
                        }
                        else
                        {
                            initObj.Frm_Participantes.LstPartys.ListIndex = i - initObj.Module1.PrtControl.LimInf;
                        }

                        return;
                    }
                }

                if (initObj.Module1.Codop.Id_Product == T_MODGUSR.IdPro_ComExp ||
                    initObj.Module1.Codop.Id_Product == T_MODGUSR.IdPro_DesExp)
                {
                    if (string.IsNullOrEmpty(initObj.Module1.Partys[initObj.Mdl_Funciones_Varias.IExp].LlaveArchivo))
                    {
                        Party1 = (short)(false ? -1 : 0);
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Debe ingresar el Exportador/Vendedor antes de continuar.",
                            Type = TipoMensaje.Informacion,
                            Title = T_Module1.GPrt_Caption
                        });

                        return;
                    }
                    else
                    {
                        Party1 = (short)(true ? -1 : 0);
                    }

                    if (!string.IsNullOrEmpty(initObj.Module1.Partys[IBCOA].LlaveArchivo))
                    {
                        if (initObj.Module1.Partys[IBCOA].TipoParty != T_Module1.GPrt_TipoBanco &&
                            initObj.Module1.Partys[IBCOA].TipoParty != T_Module1.GPrt_TipoBcoOperacion)
                        {
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = "El Banco Aval debe ser un partys de tipo banco.",
                                Type = TipoMensaje.Informacion,
                                Title = T_Module1.GPrt_Caption
                            });

                            return;
                        }

                    }

                    if (string.IsNullOrEmpty(initObj.Module1.Partys[IBCOS].LlaveArchivo))
                    {
                        Party2 = (short)(false ? -1 : 0);
                    }
                    else
                    {
                        Party2 = (short)(true ? -1 : 0);
                        if (initObj.Module1.Partys[IBCOS].TipoParty != T_Module1.GPrt_TipoBanco &&
                            initObj.Module1.Partys[IBCOS].TipoParty != T_Module1.GPrt_TipoBcoOperacion)
                        {
                            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                            {
                                Text = "El Banco Aceptante/Suscriptor debe ser un partys de tipo banco.",
                                Type = TipoMensaje.Informacion,
                                Title = T_Module1.GPrt_Caption
                            });

                            return;
                        }

                    }

                    if (string.IsNullOrEmpty(initObj.Module1.Partys[IACEP].LlaveArchivo))
                    {
                        Party3 = (short)(false ? -1 : 0);
                    }
                    else
                    {
                        Party3 = (short)(true ? -1 : 0);
                    }

                    if (Party2 == 0 && Party3 == 0)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Debe ingresar el Banco Aceptante/Suscriptor o Aceptante antes de continuar.",
                            Type = TipoMensaje.Informacion,
                            Title = T_Module1.GPrt_Caption
                        });

                        return;
                    }
                    else if (Party2 == -1 && Party3 == -1)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Debe ingresar un sólo partys entre el Banco Aceptante/Suscriptor o Aceptante.",
                            Type = TipoMensaje.Informacion,
                            Title = T_Module1.GPrt_Caption
                        });

                        return;
                    }

                    string _switchVar1 = initObj.Module1.Codop.Id_Product;
                    if (_switchVar1 == T_MODGUSR.IdPro_ComExp)
                    {
                        if (!string.IsNullOrEmpty(initObj.Module1.Partys[IBCOS].LlaveArchivo))
                        {
                            if (string.IsNullOrEmpty(initObj.Module1.Partys[IBCOS].rut) || initObj.Module1.Partys[IBCOS].rut == "0000000000")
                            {
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = "Debe ingresar el rut del Banco Aceptante/Suscriptor antes de continuar.",
                                    Type = TipoMensaje.Informacion,
                                    Title = T_Module1.GPrt_Caption
                                });

                                return;
                            }

                        }
                        else if (!string.IsNullOrEmpty(initObj.Module1.Partys[IACEP].LlaveArchivo))
                        {
                            if (string.IsNullOrEmpty(initObj.Module1.Partys[IACEP].rut) || initObj.Module1.Partys[IACEP].rut == "0000000000")
                            {
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = "Debe ingresar el rut del Aceptante antes de continuar.",
                                    Type = TipoMensaje.Informacion,
                                    Title = T_Module1.GPrt_Caption
                                });

                                return;
                            }

                        }

                        if (!string.IsNullOrEmpty(initObj.Module1.Partys[IBCOA].LlaveArchivo))
                        {
                            if (string.IsNullOrEmpty(initObj.Module1.Partys[IBCOA].rut) || initObj.Module1.Partys[IBCOA].rut == "0000000000")
                            {
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = "Debe ingresar el rut del Banco Aval antes de continuar.",
                                    Type = TipoMensaje.Informacion,
                                    Title = T_Module1.GPrt_Caption
                                });

                                return;
                            }

                        }

                    }
                    else if (_switchVar1 == T_MODGUSR.IdPro_DesExp)
                    {
                        if (!string.IsNullOrEmpty(initObj.Module1.Partys[initObj.Mdl_Funciones_Varias.IExp].LlaveArchivo))
                        {
                            if (string.IsNullOrEmpty(initObj.Module1.Partys[initObj.Mdl_Funciones_Varias.IExp].rut) ||
                                initObj.Module1.Partys[initObj.Mdl_Funciones_Varias.IExp].rut == "0000000000")
                            {
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = "Debe ingresar el rut del Exportador/Vendedor antes de continuar.",
                                    Type = TipoMensaje.Informacion,
                                    Title = T_Module1.GPrt_Caption
                                });

                                return;
                            }

                        }

                        if (!string.IsNullOrEmpty(initObj.Module1.Partys[IBCOA].LlaveArchivo))
                        {
                            if (string.IsNullOrEmpty(initObj.Module1.Partys[IBCOA].rut) || initObj.Module1.Partys[IBCOA].rut == "0000000000")
                            {
                                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                                {
                                    Text = "Debe ingresar el rut del Banco Aval antes de continuar.",
                                    Type = TipoMensaje.Informacion,
                                    Title = T_Module1.GPrt_Caption
                                });

                                return;
                            }

                        }

                    }

                }

                initObj.Frm_Participantes.Aceptar.Tag = -1;
                //Se setea el focus
                var foco = initObj.MODGCVD.COMISION ? "tbr_comisiones" : Convert.ToBoolean(initObj.MODANUVI.V_PlAnu.Length + initObj.MODGANU.VAnuPl.Length) ? "tbr_dedfondos" : "tbr_Comercio_invisible";
                initObj.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { initObj.Mdi_Principal.BUTTONS[key].Focus = (key == foco ? true : false); });
            }
        }

        private static void BorraFrm_Crea_Participante(InitializationObject initObj)
        {
            initObj.Frm_Crea_Participante = new UI_Frm_Crea_Participante();

            initObj.Frm_Crea_Participante.EsBanco.Value = (false ? -1 : 0);
            initObj.Frm_Crea_Participante.rut.Text = GPrt_RutMascara;
            initObj.Frm_Crea_Participante.Nombre.Text = "";
            initObj.Frm_Crea_Participante.Direccion.Text = "";
            initObj.Frm_Crea_Participante.Pais.ListIndex = -1;
            initObj.Frm_Crea_Participante.comuna.Text = "";
            initObj.Frm_Crea_Participante.Ciudad.Text = "";
            initObj.Frm_Crea_Participante.Estado.Text = "";
            initObj.Frm_Crea_Participante.Postal.Text = "";
            initObj.Frm_Crea_Participante.Telefono.Text = GPrt_FonoMascara;
            initObj.Frm_Crea_Participante.Fax.Text = GPrt_FonoMascara;
            initObj.Frm_Crea_Participante.Telex.Text = "";
            initObj.Frm_Crea_Participante.cas_postal.Text = "";
            initObj.Frm_Crea_Participante.cas_bco.Text = "";
            initObj.Frm_Crea_Participante.envio[0].Selected = true;
        }

        public static void Bot_Nem_Click(InitializationObject initObj)
        {
            initObj.Frm_Participantes.Llave.Text = initObj.Module1.KeyPrt;
        }

        public static void Cancelar_Click(InitializationObject initObj)
        {
            if (initObj.Frm_Participantes.Aceptar.Tag.ToString() == "")
            {
                initObj.Frm_Participantes.Aceptar.Tag = 0;
            }

            if (Convert.ToInt32(initObj.Frm_Participantes.Aceptar.Tag) != -1)
            {
                short Partys = (short)VB6Helpers.UBound(initObj.Module1.Partys);
                for (var i = 0; i <= Partys; i++)
                {
                    initObj.Module1.Partys[i].NombreUsado = "";
                    initObj.Module1.Partys[i].PaisUsado = "";
                    initObj.Module1.Partys[i].DireccionUsado = "";
                    initObj.Module1.Partys[i].CiudadUsado = "";
                    initObj.Module1.Partys[i].LlaveArchivo = "";
                    initObj.Module1.Partys[i].Status = 0;
                }
            }
            initObj.Frm_Participantes.Aceptar.Tag = 0;
        }

        public static void Donde_Click(InitializationObject initObj)
        {
            var Index = 0;
            for (int i = 0; i < initObj.Frm_Participantes.Donde.Count; i++)
            {
                if (initObj.Frm_Participantes.Donde[i].Selected)
                {
                    Index = i;
                    break;
                }
            }

            switch (Index)
            {
                case 0:
                    initObj.Frm_Participantes.Bot_Nem.Enabled = true;
                    initObj.Frm_Participantes.Llave.Enabled = true;
                    initObj.Frm_Participantes.Llave.Text = MODGPYF0.copiardestring(initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].LlaveArchivo, "~", 1);
                    //initObj.Frm_Participantes.Llave.SelStart = VB6Helpers.Len(Llave.Text);
                    //if ((~EnLoad & (Llave.Enabled ? -1 : 0)) != 0)
                    //{
                    //    Llave.SetFocus();
                    //}
                    break;
                case 1:
                    initObj.Frm_Participantes.Bot_Nem.Enabled = false;
                    initObj.Frm_Participantes.Llave.Enabled = false;
                    initObj.Frm_Participantes.Llave.Text = "(" + VB6Helpers.Format(VB6Helpers.CStr(initObj.Module1.PrtControl.Indice), "00") + ")";
                    //if ((~EnLoad & (Identificar.Enabled ? -1 : 0)) != 0)
                    //{
                    //    Identificar.SetFocus();
                    //}
                    break;
            }
        }

        public static void Eliminar_Click(InitializationObject initObj)
        {
            PartyKey BorraParty = new PartyKey();
            short Ubica = 0;
            string Titulo = "";
            short Lim = 0;
            short i = 0;
            short resul = 0;

            if (initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status >= T_Module1.GPrt_StatDatos)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = T_Module1.GPrt_ErrEliminar,
                    Type = TipoMensaje.Informacion,
                    Title = T_Module1.GPrt_Caption
                });

                return;
            }

            Ubica = initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Ubicacion;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice] = BorraParty;
            initObj.Module1.PartysOpe[initObj.Module1.PrtControl.Indice] = BorraParty;

            Titulo = MODGPYF0.copiardestring(
                (initObj.Frm_Participantes.LstPartys.Items[initObj.Frm_Participantes.LstPartys.ListIndex].Value),
                VB6Helpers.Chr(9), 1);

            initObj.Frm_Participantes.LstPartys.Items[initObj.Frm_Participantes.LstPartys.ListIndex].Value = Titulo;

            initObj.Frm_Participantes.Donde[0].Selected = true;
            initObj.Frm_Participantes.Eliminar.Enabled = false;
            initObj.Frm_Participantes.Instrucciones.Enabled = false;

            initObj.Frm_Participantes.Identificar.Text = "Identificar";

            initObj.Frm_Participantes.Llave.Enabled = true;

            Lim = -1;

            if (Ubica == T_Module1.GPrt_EnOperacion)
            {
                //primero buscamos en memoria
                for (i = 0; i <= (short)Lim; i++)
                {
                    if (initObj.Module1.Pope[i].Secuencia == initObj.Module1.PrtControl.Indice)
                    {
                        short _switchVar1 = initObj.Module1.Pope[i].Status;
                        if (_switchVar1 == T_Module1.GPrt_StatNuevo)
                        {
                            initObj.Module1.Pope[i].Status = T_Module1.GPrt_StatVacio;
                        }
                        else if (_switchVar1 == T_Module1.GPrt_StatCambio || _switchVar1 == T_Module1.GPrt_StatIntacto)
                        {
                            initObj.Module1.Pope[i].Status = T_Module1.GPrt_StatBorro;
                        }

                        break;
                        // UPGRADE_INFO (#0521): Unreachable code detected.
                        resul = (short)(true ? -1 : 0);
                    }
                }

                //si no esta ==> en disco no leida
                if (~resul != 0)
                {
                    Lim = (short)(Lim + 1);
                    VB6Helpers.RedimPreserve(ref initObj.Module1.Pope, 0, Lim);
                    initObj.Module1.Pope[Lim].Secuencia = initObj.Module1.PrtControl.Indice;
                    initObj.Module1.Pope[Lim].Status = T_Module1.GPrt_StatBorro;
                }

            }

            initObj.Frm_Participantes.Llave.Text = "";
            LstPartys_Click(initObj);
            initObj.Frm_Participantes.Eliminar.Tag = -1;
        }

        //Cargamos la lista de participantes
        public static void Form_Load(InitializationObject initObj, UnitOfWorkCext01 uow, bool? ultimaOperacionEsCosmos = false)
        {
            short[] tavs = null;
            short aa;
            string LasMarcas = T_Module1.GPrt_MarcaRequerido + T_Module1.GPrt_MarcaOtros + T_Module1.GPrt_MarcaBanco;
            short i = 0;
            string primero = "";
            string abc = "";
            short listo = 0;
            tavs = new short[2];
            tavs[0] = 100;
            tavs[1] = 200;

            initObj.Frm_Participantes.EnLoad = (short)(true ? -1 : 0);

            //Cargamos la Lista y precargamos valores ya ingresados
            for (i = (short)initObj.Module1.PrtControl.LimInf; i <= (short)initObj.Module1.PrtControl.LimSup; i++)
            {
                primero = "";
                abc = VB6Helpers.Left(initObj.Module1.PrtTbl[i], 1);
                if ((VB6Helpers.Instr(1, LasMarcas, abc) & (!string.IsNullOrEmpty(abc) ? -1 : 0)) != 0)
                {
                    primero = VB6Helpers.Right(initObj.Module1.PrtTbl[i], VB6Helpers.Len(initObj.Module1.PrtTbl[i]) - 1)
                        + VB6Helpers.Chr(9);
                }
                else if (string.IsNullOrEmpty(initObj.Module1.PrtTbl[i]) && initObj.Module1.PrtControl.Otros != 0 && listo == 0)
                {
                    primero = T_Module1.GPrt_TxtOtros + VB6Helpers.Chr(9);
                    listo = 1;
                }
                else if (!string.IsNullOrEmpty(initObj.Module1.PrtTbl[i]))
                {
                    primero = initObj.Module1.PrtTbl[i] + VB6Helpers.Chr(9);
                }

                if (!string.IsNullOrEmpty(primero))
                {
                    initObj.Frm_Participantes.LstPartys.Items.Add(new UI_ListBoxItem
                    {
                        Value = primero + initObj.Module1.Partys[i].NombreUsado
                    });
                }
            }
            initObj.Frm_Participantes.LstPartys.ListIndex = 0;

            //Lista esta cargada, Deshabilito En Operacion
            initObj.Frm_Participantes.Donde[1].Enabled = initObj.Module1.PrtControl.NoOperacion != 0;
            initObj.Frm_Participantes.LstPartys.ListIndex = 0;
            LstPartys_Click(initObj);
            initObj.Frm_Participantes.EnLoad = (short)(false ? -1 : 0);
            initObj.Frm_Participantes.Cancelar.Tag = -1;
            ShowDir(initObj);

            int producto = 20;
            
            //---------------- Fin Codigo Nuevo ----------------
            if (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
            {
                //TODO:@estanislao Carga automatica
                Pr_CargaPARTY1_3(initObj, uow);

                //si es carga automatica entonces el producto que voy a marcar es el de la operacion
                if(initObj.MODXORI.gb_esCosmos)
                {
                    producto = 30;
                }
            }
            else
            {
                //si no es carga automatica, sugiero para el producto el mismo producto de la operacion anterior, y si es la primera de la sesion, entonces sugiero Banco de Chile (prod 20)
                if (ultimaOperacionEsCosmos.HasValue && ultimaOperacionEsCosmos.Value)
                {
                    producto = 30;
                }
                
                //----------------------------------------
                //Realsystems-Código Nuevo-Inicio
                //Fecha Modificación 20100615
                //Responsable: Pablo Millan
                //Versión: 1.0
                //Descripción : Se genera Transaction ID
                //----------------------------------------
                if (string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.LC_TRXID_MAN))
                {
                    initObj.Mdl_Funciones_Varias.LC_TRXID_MAN = MODGCVD.GeneraTRXID(initObj.MODGCVD.VgCvd.OpeSin, uow, initObj.Mdi_Principal.MESSAGES);
                }
            }

            //precargo el radio de TipoOperacion segun lo que corresponde.
            initObj.Frm_Participantes.TipoOperacion.ForEach(x => x.Selected = false);
            initObj.Frm_Participantes.TipoOperacion.Where(x => x.ID == producto.ToString()).FirstOrDefault().Selected = true;
        }

        public static void Form_Unload(InitializationObject initObj)
        {
            if (VB6Helpers.Val(initObj.Frm_Participantes.Cancelar.Tag) != 0)
            {
                Cancelar_Click(initObj);
            }

        }

        //public static void Identificar_Click(InitializationObject initObj, UnitOfWorkCext01 uow)
        //{
        //    string Txt = "";
        //    string s = "";
        //    short UnBanco = 0;
        //    short ElTipoParty = 0;
        //    short ElFlag = 0;
        //    string ElRut = "";
        //    string ElSwift = "";
        //    string ElBco = "";
        //    short Ret1 = 0;
        //    string kk = "";
        //    short aabc = 0;
        //    short ElIndice = 0;

        //    //llave es requerida
        //    if (VB6Helpers.Trim(initObj.Frm_Participantes.Llave.Text) == "")
        //    {
        //        Txt = "Llave de Participante" + T_Module1.GPrt_ErrRequerido;

        //        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
        //        {
        //            Text = Txt,
        //            Type = TipoMensaje.Informacion,
        //            Title = T_Module1.GPrt_Caption
        //        });

        //        return;
        //    }

        //    initObj.Module1.PrtControl.Indice = (short)(initObj.Module1.PrtControl.LimInf + 
        //        initObj.Frm_Participantes.LstPartys.ListIndex);

        //    //es modificable
        //    if (initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status > T_Module1.GPrt_StatDatosLleno)
        //    {
        //        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
        //        {
        //            Text = T_Module1.GPrt_ErrModificar,
        //            Type = TipoMensaje.Informacion,
        //            Title = T_Module1.GPrt_Caption
        //        });

        //        return;
        //    }

        //    //Le agrega el caracter ~ hasta completar el largo de 12
        //    s = initObj.Frm_Participantes.Llave.Text + VB6Helpers.String(12 - VB6Helpers.Len(initObj.Frm_Participantes.Llave.Text), 126);
        //    initObj.Frm_Participantes.Llave.Tag = s;

        //    //determinar si necesito un banco
        //    if (VB6Helpers.Left(initObj.Module1.PrtTbl[initObj.Module1.PrtControl.Indice], 1) == T_Module1.GPrt_MarcaBanco)
        //    {
        //        UnBanco = (short)(true ? -1 : 0);
        //    }

        //    //si quiere en operacion y es banco no puede
        //    if (((initObj.Frm_Participantes.Donde[0].Selected == false ? -1 : 0) & UnBanco) != 0)
        //    {
        //        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
        //        {
        //            Text = T_Module1.GPrt_NoPuedeBanco,
        //            Type = TipoMensaje.Informacion,
        //            Title = T_Module1.GPrt_Caption
        //        });

        //        return;
        //    }

        //    //En base de Partys
        //    if (initObj.Frm_Participantes.Donde[0].Selected == true)
        //    {
        //        //muestra pantalla de ID participantes si tiene multiples direcciones o raz. sociales
        //        Ret1 = VerPartySy(ref ElTipoParty, ref ElFlag, ref ElRut, ref ElSwift, ref ElBco, UnBanco, initObj, uow);
        //        if (Ret1 == T_Module1.GPrt_RetExiste)
        //        {

        //            initObj.Frm_Participantes.Identificar.Enabled = false;
        //            UpdatePartys(ElTipoParty, T_Module1.GPrt_EnParty, ElFlag, ElRut, ElSwift, ElBco, initObj);
        //            initObj.Frm_Participantes.Identificar.Text = "Modificar";
        //            initObj.Frm_Participantes.Eliminar.Enabled = true;
        //            initObj.Frm_Participantes.Instrucciones.Enabled = false;

        //            LstPartys_Click(initObj);

        //            initObj.MODXORI.gb_esCosmos = false;
        //            if (MODXORI.SyGet_CtaCte(s, initObj, uow))
        //            {
        //                initObj.Frm_Participantes.Aceptar.Enabled = false;
        //                //Llama al WS
        //                string token = MODXORI.SrvGetCtaCos(initObj.MODXORI.gs_ctacte_party);
        //                Inet1_StateChanged(token, initObj);
        //            }

        //            return;
        //        }

        //        //si estamos por aqui, es porque no existe el participante
        //        //mostrar pantalla preguntando que hacer
        //        initObj.Frm_Participantes.Label1.Tag = VB6Helpers.Format(VB6Helpers.CStr(Ret1));

        //        //Confirmado con Jose Acuña - este formulario no se utiliza
        //        //Frm_Parti_No.Show(1); //muestro

        //        return;
        //    }
        //    else
        //    {
        //        //en operación
        //        Ret1 = VerPartyOperacion(ref ElIndice, initObj, uow);
        //        if (Ret1 == T_Module1.GPrt_RetExiste)
        //        {
        //            UpdatePope(ElIndice, initObj);
        //            initObj.Frm_Participantes.Identificar.Text = "Modificar";
        //            initObj.Frm_Participantes.Eliminar.Enabled = true;
        //            initObj.Frm_Participantes.Instrucciones.Enabled = false;

        //            return;
        //        }

        //        if (Ret1 == T_Module1.GPrt_RetCancelo)
        //        {
        //            return;
        //        }
        //    }

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <returns>True si debe seguir ejecutando Identificar_Click_2</returns>
        public static bool Identificar_Click_1(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            string Txt = "";
            string s = "";
            short UnBanco = 0;
            short ElTipoParty = 0;
            short ElFlag = 0;
            string ElRut = "";
            string ElSwift = "";
            string ElBco = "";
            short Ret1 = 0;
            string kk = "";
            short aabc = 0;
            short ElIndice = 0;

            //llave es requerida
            if (string.IsNullOrWhiteSpace(initObj.Frm_Participantes.Llave.Text.ToUpper()))
            {
                Txt = "Llave de Participante" + T_Module1.GPrt_ErrRequerido;

                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = Txt,
                    Type = TipoMensaje.Informacion,
                    Title = T_Module1.GPrt_Caption,
                    ControlName = "KeyText"
                });

                return false;
            }

            initObj.Module1.PrtControl.Indice = (short)(initObj.Module1.PrtControl.LimInf +
                initObj.Frm_Participantes.LstPartys.ListIndex);

            //es modificable
            if (initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status > T_Module1.GPrt_StatDatosLleno)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = T_Module1.GPrt_ErrModificar,
                    Type = TipoMensaje.Informacion,
                    Title = T_Module1.GPrt_Caption,
                    ControlName = "KeyText"
                });

                return false;
            }

            //Le agrega el caracter ~ hasta completar el largo de 12
            s = initObj.Frm_Participantes.Llave.Text.ToUpper() + VB6Helpers.String(12 - VB6Helpers.Len(initObj.Frm_Participantes.Llave.Text.ToUpper()), 126);
            initObj.Frm_Participantes.Llave.Tag = s;

            //determinar si necesito un banco
            if (VB6Helpers.Left(initObj.Module1.PrtTbl[initObj.Module1.PrtControl.Indice], 1) == T_Module1.GPrt_MarcaBanco)
            {
                UnBanco = (short)(true ? -1 : 0);
            }

            //si quiere en operacion y es banco no puede
            if (((initObj.Frm_Participantes.Donde[0].Selected == false ? -1 : 0) & UnBanco) != 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = T_Module1.GPrt_NoPuedeBanco,
                    Type = TipoMensaje.Informacion,
                    Title = T_Module1.GPrt_Caption
                });

                return false;
            }

            //En base de Partys
            if (initObj.Frm_Participantes.Donde[0].Selected == true)
            {
                //muestra pantalla de ID participantes si tiene multiples direcciones o raz. sociales
                Ret1 = VerPartySy1_2(ref ElTipoParty, ref ElFlag, ref ElRut, ref ElSwift, ref ElBco, UnBanco, initObj, uow);

                // validamos que el rut no venga vacío
                if ((string.IsNullOrEmpty(ElRut) || string.IsNullOrWhiteSpace(ElRut)) && Ret1 == T_Module1.GPrt_RetExiste)
                {

                    initObj.Frm_Participantes.Aceptar.Enabled = false;
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "El participante ingresado no cuenta con RUT. Por favor use el \"Administrador de Participantes\" para ingresarlo, o de lo contrario seleccione otro participante válido.",
                        Type = TipoMensaje.Error,
                        Title = T_MODGCVD.MsgCVD,
                        ControlName = "KeyText"
                    });

                    initObj.Frm_Participantes.Identificar.Enabled = true;
                    return false;
                }
            }
            else
            {
                //en operación
                Ret1 = VerPartyOperacion1_2(ref ElIndice, initObj, uow);
            }

            return true;
        }

        public static void Identificar_Click_2(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short UnBanco = 0;
            short ElTipoParty = 0;
            short ElFlag = 0;
            string ElRut = "";
            string ElSwift = "";
            string ElBco = "";
            short Ret1 = 0;
            short ElIndice = 0;

            string s = "";
            s = initObj.Frm_Participantes.Llave.Text.ToUpper() + VB6Helpers.String(12 - VB6Helpers.Len(initObj.Frm_Participantes.Llave.Text.ToUpper()), 126);

            int tipoOperacion = int.Parse(initObj.Frm_Participantes.TipoOperacion.Where(x => x.Selected).FirstOrDefault().ID);

            //En base de Partys
            if (initObj.Frm_Participantes.Donde[0].Selected == true)
            {
                //muestra pantalla de ID participantes si tiene multiples direcciones o raz. sociales
                Ret1 = VerPartySy2_2(ref ElTipoParty, ref ElFlag, ref ElRut, ref ElSwift, ref ElBco, UnBanco, initObj, uow);
                
                if ((string.IsNullOrEmpty(ElRut) || string.IsNullOrWhiteSpace(ElRut)) && Ret1 == T_Module1.GPrt_RetExiste)
                {

                    initObj.Frm_Participantes.Aceptar.Enabled = false;
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "El participante ingresado no cuenta con RUT. Por favor use el \"Administrador de Participantes\" para ingresarlo, o de lo contrario seleccione otro participante válido.",
                        Type = TipoMensaje.Error,
                        Title = T_MODGCVD.MsgCVD,
                        ControlName = "KeyText"
                    });

                    initObj.Frm_Participantes.Identificar.Enabled = true;
                    return;
                } else if (Ret1 == T_Module1.GPrt_RetExiste) //si no presiono Cancel
                {
                    initObj.Frm_Participantes.Identificar.Enabled = false;
                    UpdatePartys(ElTipoParty, T_Module1.GPrt_EnParty, ElFlag, ElRut, ElSwift, ElBco, initObj);
                    initObj.Frm_Participantes.Identificar.Text = "Modificar";
                    initObj.Frm_Participantes.Eliminar.Enabled = true;
                    initObj.Frm_Participantes.Instrucciones.Enabled = false;
                    LstPartys_Click(initObj);
                    initObj.MODXORI.gb_esCosmos = false;

                    //Valida si el participante tiene cta. cte. -> Los participantes que son Banco no tienen cuenta corriente.
                    if (MODXORI.SyGet_CtaCte(s.ToUpper(), initObj, uow))
                    {
                        initObj.Frm_Participantes.Aceptar.Enabled = false;
                        //Método que asigna los numero de operación:
                        Inet1_StateChanged(tipoOperacion, initObj);
                    }
                    else
                    {
                        /*Sino posee cuenta corriente se asume que el participante es Banco.*/
                        //Método que asigna los numero de operación:
                        initObj.Frm_Participantes.Aceptar.Enabled = false;
                        Inet2_StateChanged(initObj, tipoOperacion);
                    }

                    initObj.Frm_Participantes.Identificar.Enabled = true;
                    return;
                }
                
                //si estamos por aqui, es porque no existe el participante
                initObj.Frm_Participantes.Label1.Tag = VB6Helpers.Format(VB6Helpers.CStr(Ret1));
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "Atención: Participante No Existe.",
                    Type = TipoMensaje.Error,
                    Title = T_MODGCVD.MsgCVD,
                    ControlName = "KeyText"
                });
                return;
            }
            else
            {
                //en operación
                Ret1 = VerPartyOperacion2_2(ref ElIndice, initObj, uow);
                if (Ret1 == T_Module1.GPrt_RetExiste)
                {
                    UpdatePope(ElIndice, initObj);
                    initObj.Frm_Participantes.Identificar.Text = "Modificar";
                    initObj.Frm_Participantes.Eliminar.Enabled = true;
                    initObj.Frm_Participantes.Instrucciones.Enabled = false;

                    Inet2_StateChanged(initObj, tipoOperacion);

                    return;
                }
                if (Ret1 == T_Module1.GPrt_RetCancelo)
                {
                    return;
                }
            }
        }

        private static void Inet1_StateChanged(int tipoOperacion, InitializationObject initObj)
        {
            if (tipoOperacion == 30)
            {
                initObj.MODXORI.gb_esCosmos = true;
                initObj.Module1.Codop = initObj.Module1.Codop_FT.Clone();
                initObj.CaptionAddition = "Cuenta Citi";
                initObj.Frm_Participantes.Identificar.Enabled = true;
                initObj.Frm_Participantes.Aceptar.Enabled = true;

            }
            else 
            {
                initObj.MODXORI.gb_esCosmos = false;
                initObj.Module1.Codop = initObj.Module1.Codop_CVD.Clone();
                initObj.CaptionAddition = "Cuenta BCH";
                initObj.Frm_Participantes.Identificar.Enabled = true;
                initObj.Frm_Participantes.Aceptar.Enabled = true;
            }

            //Se llena esta estructura para poder generar el mensaje del correlativo al guardar.
            initObj.Module1.Codop.Cent_Costo = initObj.MODGCVD.VgCvd.codcct;
            initObj.Module1.Codop.Id_Product = initObj.Module1.Codop.Id_Product != null ? initObj.Module1.Codop.Id_Product : "30";
            initObj.Module1.Codop.Id_Especia = initObj.MODGCVD.VgCvd.codesp;
            initObj.Module1.Codop.Id_Empresa = initObj.MODGCVD.VgCvd.codofi;
            initObj.Module1.Codop.Id_Operacion = initObj.Module1.Codop.Id_Operacion;//initObj.MODGCVD.VgCvd.codope;


            initObj.MODGCVD.VgCvd.codpro = initObj.Module1.Codop.Id_Product;
            initObj.MODGCVD.VgCvd.codope = initObj.Module1.Codop.Id_Operacion;

            initObj.MODGCVD.VgCvd.OpeSin = initObj.MODGCVD.VgCvd.codcct + initObj.MODGCVD.VgCvd.codpro + initObj.MODGCVD.VgCvd.codesp + initObj.MODGCVD.VgCvd.codofi + initObj.MODGCVD.VgCvd.codope;
            initObj.MODGCVD.VgCvd.OpeCon = initObj.MODGCVD.VgCvd.codcct + "-" + initObj.MODGCVD.VgCvd.codpro + "-" + initObj.MODGCVD.VgCvd.codesp + "-" + initObj.MODGCVD.VgCvd.codofi + "-" + initObj.MODGCVD.VgCvd.codope;
            if (initObj.MODGCVD.COMISION == true)
            {
                initObj.Frm_Principal.Caption = "Comisiones Fund Transfer  " + initObj.MODGCVD.VgCvd.OpeCon + " " + initObj.CaptionAddition;
            }
            else
            {
                initObj.Frm_Principal.Caption = "Compra Venta de Divisas Fund Transfer  " + initObj.MODGCVD.VgCvd.OpeCon + " " + initObj.CaptionAddition;
            }

        }

        private static void Inet2_StateChanged(InitializationObject initObj, int tipoOperacion)
        {
            //Se llena esta estructura para poder generar el mensaje del correlativo al guardar.
            initObj.Module1.Codop.Cent_Costo = initObj.MODGCVD.VgCvd.codcct;
            initObj.Module1.Codop.Id_Product = tipoOperacion.ToString();
            initObj.Module1.Codop.Id_Especia = initObj.MODGCVD.VgCvd.codesp;
            initObj.Module1.Codop.Id_Empresa = initObj.MODGCVD.VgCvd.codofi;

            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
            {
                Text = "No se encontraron cuentas para el participante",
                Type = TipoMensaje.Informacion,
                Title = T_MODGCVD.MsgCVD
            });
            
            if(tipoOperacion == 20)
            {
                initObj.Module1.Codop.Id_Operacion = initObj.Module1.Codop_CVD.Id_Operacion;
                initObj.CaptionAddition = "Cuenta BCH";
            }
            else if(tipoOperacion == 30)
            {
                initObj.Module1.Codop.Id_Operacion = initObj.Module1.Codop_FT.Id_Operacion;
                initObj.CaptionAddition = "Cuenta Cosmos";
            }

            
            initObj.MODGCVD.VgCvd.codpro = initObj.Module1.Codop.Id_Product;
            initObj.MODGCVD.VgCvd.codope = initObj.Module1.Codop.Id_Operacion;

           
            initObj.MODGCVD.VgCvd.OpeSin = initObj.MODGCVD.VgCvd.codcct + initObj.MODGCVD.VgCvd.codpro + initObj.MODGCVD.VgCvd.codesp + initObj.MODGCVD.VgCvd.codofi + initObj.MODGCVD.VgCvd.codope;
            initObj.MODGCVD.VgCvd.OpeCon = initObj.MODGCVD.VgCvd.codcct + "-" + initObj.MODGCVD.VgCvd.codpro + "-" + initObj.MODGCVD.VgCvd.codesp + "-" + initObj.MODGCVD.VgCvd.codofi + "-" + initObj.MODGCVD.VgCvd.codope;
            
            if (initObj.MODGCVD.COMISION == true)
            {
                initObj.Frm_Principal.Caption = "Comisiones Fund Transfer  " + initObj.MODGCVD.VgCvd.OpeCon + " " + initObj.CaptionAddition;
            }
            else
            {
                initObj.Frm_Principal.Caption = "Compra Venta de Divisas Fund Transfer  " + initObj.MODGCVD.VgCvd.OpeCon + " " + initObj.CaptionAddition;
            }
            initObj.Frm_Participantes.Identificar.Enabled = true;
            initObj.Frm_Participantes.Aceptar.Enabled = true;

        }

        public static void LstPartys_Click(InitializationObject initObj)
        {
            string Txt;
            string s = "";
            short IBCOS = 0;

            initObj.Module1.PrtControl.Indice = (short)(initObj.Module1.PrtControl.LimInf + initObj.Frm_Participantes.LstPartys.ListIndex);

            Txt = MODGPYF0.copiardestring(
                (initObj.Frm_Participantes.LstPartys.Items[initObj.Frm_Participantes.LstPartys.ListIndex].Value),
                VB6Helpers.Chr(9), 1);

            //@estanislao no existe la opcion de "(Otros)", en vez de "Otros" por codigo nunca
            //if (Txt == T_Module1.GPrt_TxtOtros)
            //{
            //    initObj.Frm_Participantes.Llave.Text = "";
            //    if (Frame1.Enabled)
            //    {
            //        Frame1.Enabled = false;
            //    }
            //    return;
            //}
            //if (!Frame1.Enabled)
            //{
            //    Frame1.Enabled = true;
            //}

            if (initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status == T_Module1.GPrt_StatVacio)
            {
                initObj.Frm_Participantes.Llave.Text = "";

                initObj.Frm_Participantes.Donde.ForEach(x => x.Selected = false);
                initObj.Frm_Participantes.Donde[0].Selected = true;
                Frm_Participantes.Donde_Click(initObj);

                initObj.Frm_Participantes.Eliminar.Enabled = false;
                initObj.Frm_Participantes.Instrucciones.Enabled = false;
                initObj.Frm_Participantes.Identificar.Text = "Identificar";
            }
            else
            {
                initObj.Frm_Participantes.Donde.ForEach(x => x.Selected = false);
                initObj.Frm_Participantes.Donde[initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Ubicacion].Selected = true;
                Frm_Participantes.Donde_Click(initObj);

                s = VB6Helpers.Trim(MODGPYF0.copiardestring(initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].LlaveArchivo, "|", 1));
                initObj.Frm_Participantes.Llave.Text = MODGPYF0.Componer(s, "~", "");
                initObj.Frm_Participantes.Identificar.Text = "Modificar";
                if (initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status == T_Module1.GPrt_StatLleno)
                {
                    initObj.Frm_Participantes.Eliminar.Enabled = true;
                }
                else
                {
                    initObj.Frm_Participantes.Eliminar.Enabled = false;
                }

                if (initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status > T_Module1.GPrt_StatDatosLleno)
                {
                    initObj.Frm_Participantes.Identificar.Enabled = false;
                }
                else
                {
                }
                initObj.Frm_Participantes.Instrucciones.Enabled = false;
            }

            if (initObj.Module1.Codop.Id_Product == T_MODGUSR.IdPro_ComExp || initObj.Module1.Codop.Id_Product == T_MODGUSR.IdPro_DesExp)
            {
                short _switchVar1 = initObj.Module1.PrtControl.Indice;
                if (_switchVar1 == IBCOS)
                {
                    initObj.Frm_Participantes.Donde[1].Enabled = false;
                }
                else
                {
                    initObj.Frm_Participantes.Donde[1].Enabled = true;
                }
            }

            if (initObj.Frm_Participantes.EnLoad != 0)
            {
                return;
            }

            ShowDir(initObj);
        }

        //@estanislao: este flujo nunca entra en otro que no sea LstPartys_Click();
        //private void LstPartys_DblClick()
        //{
        //    string Cr = VB6Helpers.Chr(13) + VB6Helpers.Chr(10);
        //    short i = (short)(initObj.Module1.PrtControl.LimInf + LstPartys.ListIndex);
        //    string Txt = MODGPYF0.copiardestring((LstPartys.get_List((short)LstPartys.ListIndex)), VB6Helpers.Chr(9), 1);
        //    string Que = "";
        //    string Texto = "";
        //    short Modo = 0;
        //    string Respa = "";
        //    short OK = 0;
        //    string Valor = "";
        //    short ant = 0;

        //    if (Txt != initObj.Module1.GPrt_TxtOtros && VB6Helpers.Left(initObj.Module1.PrtTbl[i], 1) != initObj.Module1.GPrt_MarcaOtros)
        //    {
        //        LstPartys_Click();
        //        return;
        //    }

        //    //si tiene marca es una edicion
        //    if (VB6Helpers.Left(initObj.Module1.PrtTbl[i], 1) == initObj.Module1.GPrt_MarcaOtros)
        //    {
        //        Que = "Para modificar el papel de este participante en la operación debe ingresar el nuevo rol que este juega en la operacion." + Cr + Cr;
        //        Que += "Ingrese una breve descripción del papel de este participante en la operación.";
        //        Texto = VB6Helpers.Mid(initObj.Module1.PrtTbl[i], 2);
        //        Modo = (short)(true ? -1 : 0);
        //    }
        //    else
        //    {
        //        //uno nuevo
        //        Que = "Para identificar un nuevo participante, debe primero ingresar el rol que este juega en la operacion." + Cr + Cr;
        //        Que += "Ingrese una breve descripción del papel de este participante en la operación.";
        //        Texto = "";
        //        Modo = (short)(false ? -1 : 0);
        //    }

        //    Respa = Texto;
        //    while (OK == 0)
        //    {

        //        OK = initObj.Module1.SceBoxInput(Que, initObj.Module1.GPrt_Caption, ref Texto, 20);
        //        if (OK == 0)
        //        {
        //            return;
        //        }
        //        if (Texto == "")
        //        {
        //            VB6Helpers.MsgBox(initObj.Module1.GPrt_InputCambio, MsgBoxStyle.Information, initObj.Module1.GPrt_Caption);
        //            Texto = Respa;
        //            OK = (short)(false ? -1 : 0);
        //        }

        //    }

        //    if (Modo != 0)
        //    {
        //        Valor = MODGPYF0.copiardestring((LstPartys.get_List((short)LstPartys.ListIndex)), VB6Helpers.Chr(9), 2);
        //        LstPartys.set_List((short)LstPartys.ListIndex, Texto + VB6Helpers.Chr(9) + Valor);
        //    }
        //    else
        //    {
        //        if (initObj.Module1.PrtControl.Otros - initObj.Module1.PrtControl.PorInsertar > 1)
        //        {
        //            ant = (short)LstPartys.ListIndex;
        //            LstPartys.AddItem(Texto + VB6Helpers.Chr(9), (short)LstPartys.ListIndex);
        //            LstPartys.ListIndex = ant;
        //        }
        //        else
        //        {
        //            LstPartys.set_List((short)LstPartys.ListIndex, Texto + VB6Helpers.Chr(9));
        //        }

        //        initObj.Module1.PrtControl.PorInsertar = (short)(initObj.Module1.PrtControl.PorInsertar + 1);
        //    }

        //    initObj.Module1.PrtTbl[i] = initObj.Module1.GPrt_MarcaOtros + Texto;
        //}

        //private void LstPartys_KeyPress(ref short KeyAscii)
        //{
        //    if (KeyAscii == 13)
        //    {
        //        LstPartys_DblClick();
        //    }
        //}

        //private void Llave_KeyDown(ref short KeyCode, ref short Shift)
        //{

        //    if ((Shift & 4) == 4)
        //    {
        //        return;
        //    }

        //    switch (KeyCode)
        //    {
        //        case initObj.Module1.KEY_UP:
        //            if (LstPartys.ListIndex == 0)
        //            {
        //                return;
        //            }
        //            LstPartys.ListIndex--;
        //            break;
        //        case initObj.Module1.KEY_DOWN:
        //            if (LstPartys.ListIndex == LstPartys.ListCount - 1)
        //            {
        //                return;
        //            }
        //            LstPartys.ListIndex++;
        //            break;
        //        case initObj.Module1.KEY_PRIOR:
        //            if (LstPartys.ListIndex - 5 < 0)
        //            {
        //                LstPartys.ListIndex = 0;
        //            }
        //            else
        //            {
        //                LstPartys.ListIndex -= 5;
        //            }

        //            break;
        //        case initObj.Module1.KEY_NEXT:
        //            if (LstPartys.ListIndex + 5 > LstPartys.ListCount - 1)
        //            {
        //                LstPartys.ListIndex = LstPartys.ListCount - 1;
        //            }
        //            else
        //            {
        //                LstPartys.ListIndex += 5;
        //            }

        //            break;
        //    }

        //}

        //private void Llave_KeyPress(ref short KeyAscii)
        //{

        //    //Solo mayusculas
        //    if (KeyAscii == 13)
        //    {

        //        if (Identificar.Enabled == false)
        //        {
        //            return;
        //        }

        //        Identificar.SetFocus();
        //        KeyAscii = 0;
        //        return;
        //    }

        //    KeyAscii = (short)VB6Helpers.Asc(VB6Helpers.UCase(VB6Helpers.Chr(KeyAscii)));
        //}

        /// <summary>
        /// Muestra Dirección del Party seleccionado.-
        /// Concatena la ciudad y el pais a la direccion del participante, y lo asigna al textbox de direcion tx_Dir
        /// </summary>
        /// <param name="initObj"></param>
        private static void ShowDir(InitializationObject initObj)
        {
            short k = (short)initObj.Frm_Participantes.LstPartys.ListIndex;
            if (k >= 0)
            {
                string s = initObj.Module1.Partys[k].DireccionUsado;

                if (!string.IsNullOrEmpty(initObj.Module1.Partys[k].CiudadUsado))
                {
                    s = s + ", " + initObj.Module1.Partys[k].CiudadUsado;
                }

                if (!string.IsNullOrEmpty(initObj.Module1.Partys[k].PaisUsado))
                {
                    s = s + ", " + initObj.Module1.Partys[k].PaisUsado;
                }

                initObj.Frm_Participantes.Tx_Dir.Text = s;
            }
        }

        /// <summary>
        /// Modifica la Estructura de Partys
        /// </summary>
        /// <param name="Tipo"></param>
        /// <param name="Donde"></param>
        /// <param name="flag"></param>
        /// <param name="rut"></param>
        /// <param name="Swift"></param>
        /// <param name="Bco"></param>
        /// <param name="initObj"></param>
        private static void UpdatePartys(short Tipo, short Donde, short flag, string rut, string Swift, string Bco,
            InitializationObject initObj)
        {
            short Cual = 0;
            short n = 0;
            string Txt = "";
            if (initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status == T_Module1.GPrt_StatVacio)
            {
                initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status = T_Module1.GPrt_StatLleno;
            }

            //nombre
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].LlaveArchivo =
                VB6Helpers.CStr(initObj.Frm_Participantes.Llave.Tag);

            Cual = (short)initObj.Frm_Iden_Participantes.Nome.ListIndex;

            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].NombreUsado =
                initObj.Frm_Iden_Participantes.Nome.Items[Cual].Value;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].IndNombre =
                (short)initObj.Frm_Iden_Participantes.Nome.Items[Cual].Data;

            //direccion
            Cual = (short)initObj.Frm_Iden_Participantes.Dire.ListIndex;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].DireccionUsado =
                initObj.Frm_Iden_Participantes.Dire.Items[Cual].Value;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].IndDireccion =
                (short)initObj.Frm_Iden_Participantes.Dire.Items[Cual].Data;

            n = 3;
            pro_sce_prty_s02_MS_3_Result datosParti = initObj.Frm_Iden_Participantes.Otro.Items[Cual].Tag as pro_sce_prty_s02_MS_3_Result;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].ComunaUsado = datosParti.comuna.ToString();
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].CiudadUsado = datosParti.ciudad;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].PostalUsado = datosParti.cod_postal;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].PaisUsado = datosParti.pais;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].CodPais = (short)datosParti.cod_pais;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].EstadoUsado = datosParti.estado;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Telefono = datosParti.telefono;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Fax = datosParti.fax;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Telex = datosParti.telex;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Enviara = (short)datosParti.envio_sce;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].CasPostal = datosParti.cas_postal;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].CasBanco = datosParti.cas_banco;

            //resetea
            if (VB6Helpers.Val(initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Telefono) == 0)
            {
                initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Telefono = "";
            }

            if (VB6Helpers.Val(initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Fax) == 0)
            {
                initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Fax = "";
            }

            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].TipoParty = Tipo;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Ubicacion = Donde;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].FlagParty = flag;

            //INICIO IR46139 RTO 16/03/2012
            string RUT_L = "";
            short Mylen = 0;
            if (string.IsNullOrEmpty(rut))
            {
                Mylen = (short)VB6Helpers.Len(initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].LlaveArchivo);
                RUT_L = VB6Helpers.Mid(initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].LlaveArchivo, 1, Mylen - 3);
                RUT_L = VB6Helpers.Format(RUT_L, "#0000000000");
                if (Module1.EsRut(RUT_L) != 0)
                {
                    initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].rut = RUT_L;
                }
                else
                {
                    initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].rut = rut;
                }

            }
            else
            {
                initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].rut = rut;
            }

            //Partys(PrtControl.Indice).Rut = Rut
            //TERMINO IR46139 RTO 16/03/2012

            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].rut = rut;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Swift = Swift;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].CodBanco = Bco;

            //update lista
            Txt = MODGPYF0.copiardestring(initObj.Frm_Participantes.LstPartys.Items[
                initObj.Frm_Participantes.LstPartys.ListIndex].Value, VB6Helpers.Chr(9), 1);
            initObj.Frm_Participantes.LstPartys.Items[(short)initObj.Frm_Participantes.LstPartys.ListIndex].Value =
                VB6Helpers.Trim(Txt) + VB6Helpers.Chr(9) +
                VB6Helpers.Trim(initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].NombreUsado);

            //ignorado
            //VB6Helpers.Unload(Frm_Iden_Paticipantes.DefInstance);
        }

        private static void UpdatePope(short Cual, InitializationObject initObj)
        {
            short i = 0;
            string Txt = "";
            if (initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status == T_Module1.GPrt_StatVacio)
            {
                initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Status = T_Module1.GPrt_StatLleno;
            }

            //nombre
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].LlaveArchivo = VB6Helpers.CStr(initObj.Frm_Participantes.Llave.Tag);
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].NombreUsado = initObj.Module1.Pope[Cual].Nombre;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].IndNombre = 0;

            //direccion
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].DireccionUsado = initObj.Module1.Pope[Cual].Direccion;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].IndDireccion = 0;

            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].ComunaUsado = initObj.Module1.Pope[Cual].comuna;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].CiudadUsado = initObj.Module1.Pope[Cual].Ciudad;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].PostalUsado = initObj.Module1.Pope[Cual].Postal;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].CasPostal = initObj.Module1.Pope[Cual].CasPostal;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].CasBanco = initObj.Module1.Pope[Cual].CasBanco;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].PaisUsado = initObj.Module1.Pope[Cual].Pais;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].CodPais = initObj.Module1.Pope[Cual].CodPais;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].EstadoUsado = initObj.Module1.Pope[Cual].estado;

            if (initObj.Module1.Pope[i].EsBanco != 0)
            {
                initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].TipoParty = T_Module1.GPrt_TipoBcoOperacion;
            }
            else
            {
                initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].TipoParty = T_Module1.GPrt_TipoEnOperacion;
            }

            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Ubicacion = T_Module1.GPrt_EnOperacion;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].FlagParty = 0;
            if (initObj.Module1.Pope[Cual].EsBanco != 0)
            {
                initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].rut = "";
                initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Swift = initObj.Module1.Pope[Cual].RutSwift;
            }
            else
            {
                initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].rut = initObj.Module1.Pope[Cual].RutSwift;
                initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Swift = "";
            }

            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].CodBanco = "";

            //Más datos.-
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Telefono = initObj.Module1.Pope[Cual].Telefono;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Fax = initObj.Module1.Pope[Cual].Fax;
            initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].Telex = initObj.Module1.Pope[Cual].Telex;

            //update lista
            Txt = MODGPYF0.copiardestring(initObj.Frm_Participantes.LstPartys.Items[
                initObj.Frm_Participantes.LstPartys.ListIndex].Value, VB6Helpers.Chr(9), 1);
            initObj.Frm_Participantes.LstPartys.Items[initObj.Frm_Participantes.LstPartys.ListIndex].Value = Txt + VB6Helpers.Chr(9)
                + initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].NombreUsado;
            initObj.Frm_Participantes.LstPartys.Items[initObj.Frm_Participantes.LstPartys.ListIndex].Data = 0;
        }

        ////Pegado a operacion
        ///// <summary>
        ///// Muestra la pantalla de crear participantes (Frm_Crea_Participantes)
        ///// </summary>
        ///// <param name="Indice"></param>
        ///// <param name="initObj"></param>
        ///// <param name="uow"></param>
        ///// <returns></returns>
        //private static short VerPartyOperacion(ref short Indice, InitializationObject initObj, UnitOfWorkCext01 uow)
        //{
        //    short _retValue = 0;
        //    PartysPope aux_pope = new PartysPope();
        //    short nuestro_pais;
        //    short Lim;
        //    short PopeInd;
        //    short i = 0;
        //    short j = 0;
        //    short PopeBase = 0;
        //    short abc = 0;
        //    string Valor = "";
        //    short kk = 0;
        //    string KeyOper;

        //    //cargo pantalla y propiedades
        //    //el formulario no maneja load
        //    //VB6Helpers.LoadForm(Frm_Crea_Participante.DefInstance); 
        //    BorraFrm_Crea_Participante(initObj);

        //    nuestro_pais = (short)VB6Helpers.Val(Mdl_Acceso.GetSceIni("FundTransfer.Pais.CodPais"));

        //    initObj.Frm_Crea_Participante.rut.Mask = GPrt_RutMask;

        //    KeyOper = "";
        //    KeyOper = initObj.Module1.PrtControl.NumOpe.Cent_Costo + "-";
        //    KeyOper = KeyOper + initObj.Module1.PrtControl.NumOpe.Id_Product + "-";
        //    KeyOper = KeyOper + initObj.Module1.PrtControl.NumOpe.Id_Especia + "-";
        //    KeyOper = KeyOper + initObj.Module1.PrtControl.NumOpe.Id_Empresa + "-";
        //    KeyOper += initObj.Module1.PrtControl.NumOpe.Id_Operacion;

        //    initObj.Frm_Crea_Participante.Caption = T_Module1.GPrt_Caption + " (" + KeyOper + ")";

        //    //veamos primero en memoria
        //    Lim = -1;
        //    PopeInd = -1;
        //    
        //    Lim = (short)VB6Helpers.UBound(initObj.Module1.Pope);

        //    if (Lim >= 0)
        //    {
        //        //tiene elementos
        //        for (i = 0; i <= (short)Lim; i++)
        //        {
        //            if (initObj.Module1.Pope[i].Secuencia == initObj.Module1.PrtControl.Indice)
        //            {
        //                if (initObj.Module1.Pope[i].Status != T_Module1.GPrt_StatBorro && initObj.Module1.Pope[i].Status != T_Module1.GPrt_StatVacio)
        //                {
        //                    if (initObj.Module1.Pope[i].EsBanco != 0)
        //                    {
        //                        initObj.Frm_Crea_Participante.rut.Mask = GPrt_SwiftMask;
        //                        initObj.Frm_Crea_Participante.EsBanco.Value = 1;
        //                        //ignorado
        //                        initObj.Frm_Crea_Participante.rut.Text += Module1.DesCero(initObj.Module1.Pope[i].RutSwift, GPrt_SwiftMascara, -1);
        //                    }
        //                    else
        //                    {
        //                        initObj.Frm_Crea_Participante.rut.Mask = GPrt_RutMask;
        //                        initObj.Frm_Crea_Participante.EsBanco.Value = 0;
        //                        //ignorado
        //                        initObj.Frm_Crea_Participante.rut.Text += Module1.DesCero(initObj.Module1.Pope[i].RutSwift, GPrt_RutMascara, 0);
        //                    }

        //                    initObj.Frm_Crea_Participante.Nombre.Text = initObj.Module1.Pope[i].Nombre;
        //                    initObj.Frm_Crea_Participante.Direccion.Text = initObj.Module1.Pope[i].Direccion;

        //                    for (j = 0; j <= (short)(initObj.Frm_Crea_Participante.Pais.ListCount - 1); j++)
        //                    {
        //                        if (initObj.Frm_Crea_Participante.Pais.Items[j].Data == -1 && initObj.Module1.Pope[i].CodPais != -1)
        //                        {
        //                            initObj.Frm_Crea_Participante.Pais.Items[j].Value = GPrt_OtroPais;
        //                        }
        //                        else if (initObj.Frm_Crea_Participante.Pais.Items[j].Data == initObj.Module1.Pope[i].CodPais)
        //                        {
        //                            initObj.Frm_Crea_Participante.Pais.ListIndex = j;
        //                            if (initObj.Module1.Pope[i].CodPais < 0)
        //                            {
        //                                initObj.Frm_Crea_Participante.Pais.Items[j].Value = GPrt_OtroPais + initObj.Module1.Pope[i].Pais;
        //                            }

        //                            break;
        //                        }

        //                    }

        //                    initObj.Frm_Crea_Participante.comuna.Text = initObj.Module1.Pope[i].comuna;
        //                    initObj.Frm_Crea_Participante.Ciudad.Text = initObj.Module1.Pope[i].Ciudad;
        //                    initObj.Frm_Crea_Participante.Estado.Text = initObj.Module1.Pope[i].estado;
        //                    initObj.Frm_Crea_Participante.Postal.Text = initObj.Module1.Pope[i].Postal;

        //                    initObj.Frm_Crea_Participante.Telefono.Mask = "";
        //                    initObj.Frm_Crea_Participante.Fax.Mask = "";
        //                    initObj.Frm_Crea_Participante.Telefono.Text = initObj.Module1.Pope[i].Telefono;
        //                    initObj.Frm_Crea_Participante.Fax.Text = initObj.Module1.Pope[i].Fax;
        //                    if (initObj.Frm_Crea_Participante.Pais.ListIndex != -1)
        //                    {
        //                        if (initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Data == nuestro_pais)
        //                        {
        //                            initObj.Frm_Crea_Participante.Telefono.Mask = GPrt_FonoMask;
        //                            initObj.Frm_Crea_Participante.Fax.Mask = GPrt_FonoMask;
        //                            initObj.Frm_Crea_Participante.Telefono.Text += Module1.DesCero(initObj.Module1.Pope[i].Telefono, GPrt_FonoMascara, 0);
        //                            initObj.Frm_Crea_Participante.Fax.Text += Module1.DesCero(initObj.Module1.Pope[i].Fax, GPrt_FonoMascara, 0);
        //                        }

        //                    }

        //                    initObj.Frm_Crea_Participante.Telex.Text = initObj.Module1.Pope[i].Telex;
        //                    initObj.Frm_Crea_Participante.cas_postal.Text = initObj.Module1.Pope[i].CasPostal;
        //                    initObj.Frm_Crea_Participante.cas_bco.Text = initObj.Module1.Pope[i].CasBanco;
        //                    initObj.Frm_Crea_Participante.envio[initObj.Module1.Pope[i].Enviara].Selected = true;
        //                }

        //                PopeInd = i;
        //                break;
        //            }

        //        }

        //    }

        //    //Si no esta en memoria buscarlo en disco
        //    if (PopeInd < 0)
        //    {
        //        sce_pope pope = null;
        //        try
        //        {
        //            pope = uow.SceRepository.pro_sce_prty_s03_MS(
        //                initObj.Module1.PrtControl.NumOpe.Cent_Costo,
        //                initObj.Module1.PrtControl.NumOpe.Id_Product,
        //                initObj.Module1.PrtControl.NumOpe.Id_Especia,
        //                initObj.Module1.PrtControl.NumOpe.Id_Empresa,
        //                initObj.Module1.PrtControl.NumOpe.Id_Operacion,
        //                VB6Helpers.Format(VB6Helpers.CStr(initObj.Module1.PrtControl.Indice), "00"),
        //                1);
        //        }
        //        catch (Exception)
        //        {
        //            initObj.Mdi_Principal.MESSAGES.Add(new UI_Message{
        //                Text = "Hubo un problema al conectar con sybase para leer Participantes en Operación (sce_pope)",
        //                Type = TipoMensaje.Informacion,
        //                Title = T_Module1.GPrt_Caption

        //            });
        //            _retValue = T_Module1.GPrt_RetCancelo;
        //        }

        //        if (pope == null)
        //        {
        //            //no existe
        //            PopeBase = T_Module1.GPrt_StatNuevo;
        //        }
        //        else
        //        {
        //            //Traspasamos la lectura a variables auxiliares.
        //            aux_pope.EsBanco = (short)(pope.esbanco ? -1 : 0);
        //            aux_pope.RutSwift = pope.rut;
        //            aux_pope.Nombre = pope.razon_soci;
        //            aux_pope.Direccion = pope.direccion;
        //            aux_pope.comuna = pope.comuna;
        //            aux_pope.estado = pope.estado;
        //            aux_pope.Ciudad = pope.ciudad;
        //            aux_pope.Pais = pope.pais;
        //            aux_pope.CodPais = (short)pope.cod_pais;
        //            aux_pope.Postal = pope.cod_postal;
        //            aux_pope.Telefono = pope.telefono;
        //            aux_pope.Fax = pope.fax;
        //            aux_pope.Telex = pope.telex;
        //            aux_pope.Enviara = (short)pope.envio_sce;
        //            aux_pope.CasPostal = pope.cas_postal;
        //            aux_pope.CasBanco = pope.cas_banco;

        //            abc = aux_pope.EsBanco;
        //            if (abc != 0)
        //            {
        //                initObj.Frm_Crea_Participante.rut.Mask = GPrt_SwiftMask;
        //                initObj.Frm_Crea_Participante.EsBanco.Value = 1;
        //                initObj.Frm_Crea_Participante.rut.Text += Module1.DesCero(VB6Helpers.Trim(aux_pope.RutSwift), GPrt_SwiftMascara, -1);
        //            }
        //            else
        //            {
        //                initObj.Frm_Crea_Participante.rut.Mask = GPrt_RutMask;
        //                initObj.Frm_Crea_Participante.EsBanco.Value = 0;
        //                initObj.Frm_Crea_Participante.rut.Text += Module1.DesCero(VB6Helpers.Trim(aux_pope.RutSwift), GPrt_RutMascara, 0);
        //            }

        //            initObj.Frm_Crea_Participante.Nombre.Text = VB6Helpers.RTrim(aux_pope.Nombre);
        //            initObj.Frm_Crea_Participante.Direccion.Text = VB6Helpers.RTrim(aux_pope.Direccion);
        //            initObj.Frm_Crea_Participante.comuna.Text = VB6Helpers.RTrim(aux_pope.comuna);
        //            initObj.Frm_Crea_Participante.Estado.Text = VB6Helpers.RTrim(aux_pope.estado);
        //            initObj.Frm_Crea_Participante.Ciudad.Text = VB6Helpers.RTrim(aux_pope.Ciudad);

        //            abc = aux_pope.CodPais;
        //            for (j = 0; j <= (short)(initObj.Frm_Crea_Participante.Pais.Items.Count - 1); j++)
        //            {
        //                if (initObj.Frm_Crea_Participante.Pais.Items[j].Data == -1 && abc != -1)
        //                {
        //                    initObj.Frm_Crea_Participante.Pais.Items[j].Value = GPrt_OtroPais;
        //                }
        //                else if (initObj.Frm_Crea_Participante.Pais.Items[j].Data == abc)
        //                {
        //                    initObj.Frm_Crea_Participante.Pais.ListIndex = j;
        //                    if (abc < 0)
        //                    {
        //                        initObj.Frm_Crea_Participante.Pais.Items[j].Value = GPrt_OtroPais + VB6Helpers.Trim(aux_pope.Pais);
        //                    }

        //                    break;
        //                }

        //            }

        //            initObj.Frm_Crea_Participante.Postal.Text = VB6Helpers.RTrim(aux_pope.Postal);

        //            if (initObj.Frm_Crea_Participante.Pais.Items[(short)initObj.Frm_Crea_Participante.Pais.ListIndex].Data == nuestro_pais)
        //            {
        //                initObj.Frm_Crea_Participante.Telefono.Mask = GPrt_FonoMask;
        //                initObj.Frm_Crea_Participante.Fax.Mask = GPrt_FonoMask;
        //                initObj.Frm_Crea_Participante.Telefono.Text += Module1.DesCero(VB6Helpers.RTrim(aux_pope.Telefono), GPrt_FonoMascara, 0);
        //                initObj.Frm_Crea_Participante.Fax.Text += Module1.DesCero(VB6Helpers.RTrim(aux_pope.Fax), GPrt_FonoMascara, 0);
        //            }
        //            else
        //            {
        //                initObj.Frm_Crea_Participante.Telefono.Mask = "";
        //                initObj.Frm_Crea_Participante.Fax.Mask = "";
        //                initObj.Frm_Crea_Participante.Telefono.Text = VB6Helpers.RTrim(aux_pope.Telefono);
        //                initObj.Frm_Crea_Participante.Fax.Text = VB6Helpers.RTrim(aux_pope.Fax);
        //            }

        //            initObj.Frm_Crea_Participante.Telex.Text = VB6Helpers.RTrim(aux_pope.Telex);
        //            initObj.Frm_Crea_Participante.cas_postal.Text = VB6Helpers.RTrim(aux_pope.CasPostal);
        //            initObj.Frm_Crea_Participante.cas_bco.Text = VB6Helpers.RTrim(aux_pope.CasBanco);
        //            initObj.Frm_Crea_Participante.envio[aux_pope.Enviara].Selected = true;

        //            Valor = "";
        //            PopeBase = T_Module1.GPrt_StatIntacto;
        //        }
        //    }

        //    //desplegar los datos o capturar
        //    //Frm_Crea_Participante.Show(1);

        //    //acepto
        //    if (VB6Helpers.Val(initObj.Frm_Crea_Participante.Aceptar.Tag) == T_Module1.GPrt_RetExiste)
        //    {
        //        if (PopeInd >= 0)
        //        {
        //            //edito alguno ya en memoria
        //            initObj.Module1.Pope[PopeInd].EsBanco = initObj.Frm_Crea_Participante.EsBanco.Value;
        //            if (initObj.Module1.Pope[PopeInd].EsBanco != 0)
        //            {
        //                initObj.Module1.Pope[PopeInd].RutSwift = Module1.FilCero((initObj.Frm_Crea_Participante.rut.Text), 
        //                    GPrt_SwiftMascara, -1);
        //            }
        //            else
        //            {
        //                initObj.Module1.Pope[PopeInd].RutSwift = Module1.FilCero((initObj.Frm_Crea_Participante.rut.Text), 
        //                    GPrt_RutMascara, 0);
        //            }

        //            initObj.Module1.Pope[PopeInd].Nombre = initObj.Frm_Crea_Participante.Nombre.Text;
        //            initObj.Module1.Pope[PopeInd].Direccion = initObj.Frm_Crea_Participante.Direccion.Text;
        //            initObj.Module1.Pope[PopeInd].comuna = initObj.Frm_Crea_Participante.comuna.Text;
        //            initObj.Module1.Pope[PopeInd].Ciudad = initObj.Frm_Crea_Participante.Ciudad.Text;
        //            initObj.Module1.Pope[PopeInd].estado = initObj.Frm_Crea_Participante.Estado.Text;
        //            if (initObj.Frm_Crea_Participante.Pais.ListIndex != -1)
        //            {
        //                initObj.Module1.Pope[PopeInd].CodPais = 
        //                    (short)initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Data;
        //            }

        //            if (initObj.Module1.Pope[PopeInd].CodPais == -1)
        //            {
        //                initObj.Module1.Pope[PopeInd].Pais = 
        //                    VB6Helpers.Trim(MODGPYF0.copiardestring(initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Value, ")", 2));
        //            }
        //            else
        //            {
        //                initObj.Module1.Pope[PopeInd].Pais = initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Value;
        //            }

        //            initObj.Module1.Pope[PopeInd].Postal = initObj.Frm_Crea_Participante.Postal.Text;

        //            initObj.Module1.Pope[PopeInd].Telefono = initObj.Frm_Crea_Participante.Telefono.Text;
        //            initObj.Module1.Pope[PopeInd].Fax = initObj.Frm_Crea_Participante.Fax.Text;
        //            if (initObj.Frm_Crea_Participante.Pais.ListIndex != -1)
        //            {
        //                if (initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Data == nuestro_pais)
        //                {
        //                    initObj.Module1.Pope[PopeInd].Telefono = Module1.FilCero((initObj.Frm_Crea_Participante.Telefono.Text), GPrt_FonoMascara, 0);
        //                    initObj.Module1.Pope[PopeInd].Fax = Module1.FilCero((initObj.Frm_Crea_Participante.Fax.Text), GPrt_FonoMascara, 0);
        //                }

        //            }

        //            initObj.Module1.Pope[PopeInd].Telex = initObj.Frm_Crea_Participante.Telex.Text;
        //            initObj.Module1.Pope[PopeInd].CasPostal = initObj.Frm_Crea_Participante.cas_postal.Text;
        //            initObj.Module1.Pope[PopeInd].CasBanco = initObj.Frm_Crea_Participante.cas_bco.Text;
        //            for (kk = 0; kk <= 3; kk++)
        //            {
        //                if (initObj.Frm_Crea_Participante.envio[kk].Selected)
        //                {
        //                    initObj.Module1.Pope[PopeInd].Enviara = kk;
        //                }
        //            }

        //            short _switchVar1 = initObj.Module1.Pope[PopeInd].Status;
        //            if (_switchVar1 == T_Module1.GPrt_StatNuevo)
        //            {
        //            }
        //            else if (_switchVar1 == T_Module1.GPrt_StatCambio)
        //            {
        //            }
        //            else if (_switchVar1 == T_Module1.GPrt_StatBorro)
        //            {
        //                initObj.Module1.Pope[PopeInd].Status = T_Module1.GPrt_StatCambio;
        //            }
        //            else if (_switchVar1 == T_Module1.GPrt_StatIntacto)
        //            {
        //                initObj.Module1.Pope[PopeInd].Status = T_Module1.GPrt_StatCambio;
        //            }
        //            else if (_switchVar1 == T_Module1.GPrt_StatVacion)
        //            {
        //                initObj.Module1.Pope[PopeInd].Status = T_Module1.GPrt_StatNuevo;
        //            }

        //            Indice = PopeInd;
        //        }
        //        else
        //        {
        //            //desde la base, agregar a memoria
        //            Lim = (short)(Lim + 1);
        //            VB6Helpers.RedimPreserve(ref initObj.Module1.Pope, 0, Lim);
        //            initObj.Module1.Pope[Lim].EsBanco = (short)initObj.Frm_Crea_Participante.EsBanco.Value;
        //            if (initObj.Module1.Pope[Lim].EsBanco != 0)
        //            {
        //                initObj.Module1.Pope[Lim].RutSwift = Module1.FilCero((initObj.Frm_Crea_Participante.rut.Text), GPrt_SwiftMascara, -1);
        //            }
        //            else
        //            {
        //                if (initObj.Mdl_Funciones_Varias.CARGA_PARTY == 0)
        //                {
        //                    initObj.Module1.Pope[Lim].RutSwift = Module1.FilCero((initObj.Frm_Crea_Participante.rut.Text), GPrt_RutMascara, 0);
        //                }
        //                if (initObj.Mdl_Funciones_Varias.CARGA_PARTY == 1)
        //                {
        //                    //@estanislao: se usaba txt_rut
        //                    initObj.Module1.Pope[Lim].RutSwift = Module1.FilCero((initObj.Frm_Crea_Participante.rut.Text), GPrt_RutMascara, 0);
        //                }
        //            }

        //            initObj.Module1.Pope[Lim].Nombre = initObj.Frm_Crea_Participante.Nombre.Text;
        //            initObj.Module1.Pope[Lim].Direccion = initObj.Frm_Crea_Participante.Direccion.Text;
        //            initObj.Module1.Pope[Lim].comuna = initObj.Frm_Crea_Participante.comuna.Text;
        //            initObj.Module1.Pope[Lim].Ciudad = initObj.Frm_Crea_Participante.Ciudad.Text;
        //            initObj.Module1.Pope[Lim].estado = initObj.Frm_Crea_Participante.Estado.Text;
        //            if (initObj.Frm_Crea_Participante.Pais.ListIndex != -1)
        //            {
        //                initObj.Module1.Pope[Lim].CodPais = (short)initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Data;
        //            }
        //            else
        //            {
        //                initObj.Module1.Pope[Lim].CodPais = 0;
        //            }

        //            if (initObj.Module1.Pope[Lim].CodPais == -1)
        //            {
        //                initObj.Module1.Pope[Lim].Pais = 
        //                    VB6Helpers.Trim(MODGPYF0.copiardestring((initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Value), ")", 2));
        //            }
        //            else
        //            {
        //                initObj.Module1.Pope[Lim].Pais = 
        //                    initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Value;
        //            }

        //            initObj.Module1.Pope[Lim].Postal = initObj.Frm_Crea_Participante.Postal.Text;

        //            nuestro_pais = (short)VB6Helpers.Val(Mdl_Acceso.GetSceIni("FundTransfer.Pais.CodPais"));

        //            if (initObj.Frm_Crea_Participante.Pais.ListIndex != -1)
        //            {
        //                if (initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Data == nuestro_pais)
        //                {
        //                    initObj.Module1.Pope[Lim].Telefono = Module1.FilCero((initObj.Frm_Crea_Participante.Telefono.Text), GPrt_FonoMascara, 0);
        //                    initObj.Module1.Pope[Lim].Fax = Module1.FilCero((initObj.Frm_Crea_Participante.Fax.Text), GPrt_FonoMascara, 0);
        //                }
        //                else
        //                {
        //                    initObj.Module1.Pope[Lim].Telefono = initObj.Frm_Crea_Participante.Telefono.Text;
        //                    initObj.Module1.Pope[Lim].Fax = initObj.Frm_Crea_Participante.Fax.Text;
        //                }

        //            }
        //            else
        //            {
        //                initObj.Module1.Pope[Lim].Telefono = initObj.Frm_Crea_Participante.Telefono.Text;
        //                initObj.Module1.Pope[Lim].Fax = initObj.Frm_Crea_Participante.Fax.Text;
        //            }

        //            initObj.Module1.Pope[Lim].Telex = initObj.Frm_Crea_Participante.Telex.Text;
        //            initObj.Module1.Pope[Lim].CasPostal = initObj.Frm_Crea_Participante.cas_postal.Text;
        //            initObj.Module1.Pope[Lim].CasBanco = initObj.Frm_Crea_Participante.cas_bco.Text;
        //            for (kk = 0; kk <= 3; kk++)
        //            {
        //                if (initObj.Frm_Crea_Participante.envio[kk].Selected)
        //                {
        //                    initObj.Module1.Pope[Lim].Enviara = kk;
        //                }
        //            }

        //            initObj.Module1.Pope[Lim].Secuencia = initObj.Module1.PrtControl.Indice;
        //            initObj.Module1.Pope[Lim].Status = PopeBase;
        //            if (PopeBase == T_Module1.GPrt_StatIntacto)
        //            {
        //                initObj.Module1.Pope[Lim].Status = T_Module1.GPrt_StatCambio;
        //            }
        //            Indice = Lim;
        //        }

        //    }

        //    return (short)VB6Helpers.Val(initObj.Frm_Crea_Participante.Aceptar.Tag);
        //}

        //Pegado a operacion

        /// <summary>
        /// Muestra la pantalla de crear participantes (Frm_Crea_Participantes)
        /// Pre mostrar formulario
        /// </summary>
        /// <param name="Indice"></param>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        private static short VerPartyOperacion1_2(ref short Indice, InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short _retValue = 0;
            PartysPope aux_pope = new PartysPope();
            short nuestro_pais;
            short Lim;
            short PopeInd;
            short i = 0;
            short j = 0;
            short PopeBase = 0;
            short abc = 0;
            string Valor = "";
            short kk = 0;
            string KeyOper;

            //cargo pantalla y propiedades
            //el formulario no maneja load
            //VB6Helpers.LoadForm(Frm_Crea_Participante.DefInstance); 
            BorraFrm_Crea_Participante(initObj);
            nuestro_pais = (short)VB6Helpers.Val(Mdl_Acceso.GetSceIni("FundTransfer.Pais.CodPais"));
            initObj.Frm_Crea_Participante.rut.Mask = GPrt_RutMask;

            //Método que asigna los numero de operación:
            var tipoOperacion = int.Parse(initObj.Frm_Participantes.TipoOperacion.Where(x => x.Selected).FirstOrDefault().ID); 
            Inet1_StateChanged(tipoOperacion, initObj);
            initObj.Module1.PrtControl.NumOpe = initObj.Module1.Codop.Clone();

            KeyOper = "";
            KeyOper = initObj.Module1.PrtControl.NumOpe.Cent_Costo + "-";
            KeyOper = KeyOper + initObj.Module1.PrtControl.NumOpe.Id_Product + "-";
            KeyOper = KeyOper + initObj.Module1.PrtControl.NumOpe.Id_Especia + "-";
            KeyOper = KeyOper + initObj.Module1.PrtControl.NumOpe.Id_Empresa + "-";
            KeyOper += initObj.Module1.PrtControl.NumOpe.Id_Operacion;

            initObj.Frm_Crea_Participante.Caption = T_Module1.GPrt_Caption + " (" + KeyOper + ")";

            //veamos primero en memoria
            Lim = -1;
            PopeInd = -1;
            Lim = (short)VB6Helpers.UBound(initObj.Module1.Pope);

            if (Lim >= 0)
            {
                //tiene elementos
                for (i = 0; i <= (short)Lim; i++)
                {
                    if (initObj.Module1.Pope[i].Secuencia == initObj.Module1.PrtControl.Indice)
                    {
                        if (initObj.Module1.Pope[i].Status != T_Module1.GPrt_StatBorro && initObj.Module1.Pope[i].Status != T_Module1.GPrt_StatVacio)
                        {
                            if (initObj.Module1.Pope[i].EsBanco != 0)
                            {
                                initObj.Frm_Crea_Participante.rut.Mask = GPrt_SwiftMask;
                                initObj.Frm_Crea_Participante.EsBanco.Value = 1;
                                //ignorado
                                initObj.Frm_Crea_Participante.rut.Text += Module1.DesCero(initObj.Module1.Pope[i].RutSwift, GPrt_SwiftMascara, -1);
                            }
                            else
                            {
                                initObj.Frm_Crea_Participante.rut.Mask = GPrt_RutMask;
                                initObj.Frm_Crea_Participante.EsBanco.Value = 0;
                                //ignorado
                                initObj.Frm_Crea_Participante.rut.Text += Module1.DesCero(initObj.Module1.Pope[i].RutSwift, GPrt_RutMascara, 0);
                            }

                            initObj.Frm_Crea_Participante.Nombre.Text = initObj.Module1.Pope[i].Nombre;
                            initObj.Frm_Crea_Participante.Direccion.Text = initObj.Module1.Pope[i].Direccion;

                            for (j = 0; j <= (short)(initObj.Frm_Crea_Participante.Pais.ListCount - 1); j++)
                            {
                                if (initObj.Frm_Crea_Participante.Pais.Items[j].Data == -1 && initObj.Module1.Pope[i].CodPais != -1)
                                {
                                    initObj.Frm_Crea_Participante.Pais.Items[j].Value = GPrt_OtroPais;
                                }
                                else if (initObj.Frm_Crea_Participante.Pais.Items[j].Data == initObj.Module1.Pope[i].CodPais)
                                {
                                    initObj.Frm_Crea_Participante.Pais.ListIndex = j;
                                    if (initObj.Module1.Pope[i].CodPais < 0)
                                    {
                                        initObj.Frm_Crea_Participante.Pais.Items[j].Value = GPrt_OtroPais + initObj.Module1.Pope[i].Pais;
                                    }

                                    break;
                                }

                            }

                            initObj.Frm_Crea_Participante.comuna.Text = initObj.Module1.Pope[i].comuna;
                            initObj.Frm_Crea_Participante.Ciudad.Text = initObj.Module1.Pope[i].Ciudad;
                            initObj.Frm_Crea_Participante.Estado.Text = initObj.Module1.Pope[i].estado;
                            initObj.Frm_Crea_Participante.Postal.Text = initObj.Module1.Pope[i].Postal;

                            initObj.Frm_Crea_Participante.Telefono.Mask = "";
                            initObj.Frm_Crea_Participante.Fax.Mask = "";
                            initObj.Frm_Crea_Participante.Telefono.Text = initObj.Module1.Pope[i].Telefono;
                            initObj.Frm_Crea_Participante.Fax.Text = initObj.Module1.Pope[i].Fax;
                            if (initObj.Frm_Crea_Participante.Pais.ListIndex != -1)
                            {
                                if (initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Data == nuestro_pais)
                                {
                                    initObj.Frm_Crea_Participante.Telefono.Mask = GPrt_FonoMask;
                                    initObj.Frm_Crea_Participante.Fax.Mask = GPrt_FonoMask;
                                    initObj.Frm_Crea_Participante.Telefono.Text += Module1.DesCero(initObj.Module1.Pope[i].Telefono, GPrt_FonoMascara, 0);
                                    initObj.Frm_Crea_Participante.Fax.Text += Module1.DesCero(initObj.Module1.Pope[i].Fax, GPrt_FonoMascara, 0);
                                }

                            }

                            initObj.Frm_Crea_Participante.Telex.Text = initObj.Module1.Pope[i].Telex;
                            initObj.Frm_Crea_Participante.cas_postal.Text = initObj.Module1.Pope[i].CasPostal;
                            initObj.Frm_Crea_Participante.cas_bco.Text = initObj.Module1.Pope[i].CasBanco;
                            initObj.Frm_Crea_Participante.envio[initObj.Module1.Pope[i].Enviara].Selected = true;
                        }

                        PopeInd = i;
                        break;
                    }

                }

            }

            //Si no esta en memoria buscarlo en disco
            if (PopeInd < 0)
            {
                pro_sce_prty_s03_MS_Result pope = null;
                try
                {
                    pope = uow.SceRepository.pro_sce_prty_s03_MS(
                        initObj.Module1.PrtControl.NumOpe.Cent_Costo,
                        initObj.Module1.PrtControl.NumOpe.Id_Product,
                        initObj.Module1.PrtControl.NumOpe.Id_Especia,
                        initObj.Module1.PrtControl.NumOpe.Id_Empresa,
                        initObj.Module1.PrtControl.NumOpe.Id_Operacion,
                        VB6Helpers.Format(VB6Helpers.CStr(initObj.Module1.PrtControl.Indice), "00"),
                        1);
                }
                catch (Exception)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Hubo un problema al conectar con sybase para leer Participantes en Operación (sce_pope)",
                        Type = TipoMensaje.Informacion,
                        Title = T_Module1.GPrt_Caption

                    });
                    _retValue = T_Module1.GPrt_RetCancelo;
                }

                if (pope == null)
                {
                    //no existe
                    PopeBase = T_Module1.GPrt_StatNuevo;
                }
                else
                {
                    //Traspasamos la lectura a variables auxiliares.
                    aux_pope.EsBanco = (short)(pope.esbanco ? -1 : 0);
                    aux_pope.RutSwift = pope.rut;
                    aux_pope.Nombre = pope.razon_soci;
                    aux_pope.Direccion = pope.direccion;
                    aux_pope.comuna = pope.comuna;
                    aux_pope.estado = pope.estado;
                    aux_pope.Ciudad = pope.ciudad;
                    aux_pope.Pais = pope.pais;
                    aux_pope.CodPais = (short)pope.cod_pais;
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
                        initObj.Frm_Crea_Participante.rut.Mask = GPrt_SwiftMask;
                        initObj.Frm_Crea_Participante.EsBanco.Value = 1;
                        initObj.Frm_Crea_Participante.rut.Text += Module1.DesCero(VB6Helpers.Trim(aux_pope.RutSwift), GPrt_SwiftMascara, -1);
                    }
                    else
                    {
                        initObj.Frm_Crea_Participante.rut.Mask = GPrt_RutMask;
                        initObj.Frm_Crea_Participante.EsBanco.Value = 0;
                        initObj.Frm_Crea_Participante.rut.Text += Module1.DesCero(VB6Helpers.Trim(aux_pope.RutSwift), GPrt_RutMascara, 0);
                    }

                    initObj.Frm_Crea_Participante.Nombre.Text = VB6Helpers.RTrim(aux_pope.Nombre);
                    initObj.Frm_Crea_Participante.Direccion.Text = VB6Helpers.RTrim(aux_pope.Direccion);
                    initObj.Frm_Crea_Participante.comuna.Text = VB6Helpers.RTrim(aux_pope.comuna);
                    initObj.Frm_Crea_Participante.Estado.Text = VB6Helpers.RTrim(aux_pope.estado);
                    initObj.Frm_Crea_Participante.Ciudad.Text = VB6Helpers.RTrim(aux_pope.Ciudad);

                    abc = aux_pope.CodPais;
                    for (j = 0; j <= (short)(initObj.Frm_Crea_Participante.Pais.Items.Count - 1); j++)
                    {
                        if (initObj.Frm_Crea_Participante.Pais.Items[j].Data == -1 && abc != -1)
                        {
                            initObj.Frm_Crea_Participante.Pais.Items[j].Value = GPrt_OtroPais;
                        }
                        else if (initObj.Frm_Crea_Participante.Pais.Items[j].Data == abc)
                        {
                            initObj.Frm_Crea_Participante.Pais.ListIndex = j;
                            if (abc < 0)
                            {
                                initObj.Frm_Crea_Participante.Pais.Items[j].Value = GPrt_OtroPais + VB6Helpers.Trim(aux_pope.Pais);
                            }

                            break;
                        }

                    }

                    initObj.Frm_Crea_Participante.Postal.Text = VB6Helpers.RTrim(aux_pope.Postal);

                    if (initObj.Frm_Crea_Participante.Pais.Items[(short)initObj.Frm_Crea_Participante.Pais.ListIndex].Data == nuestro_pais)
                    {
                        initObj.Frm_Crea_Participante.Telefono.Mask = GPrt_FonoMask;
                        initObj.Frm_Crea_Participante.Fax.Mask = GPrt_FonoMask;
                        initObj.Frm_Crea_Participante.Telefono.Text += Module1.DesCero(VB6Helpers.RTrim(aux_pope.Telefono), GPrt_FonoMascara, 0);
                        initObj.Frm_Crea_Participante.Fax.Text += Module1.DesCero(VB6Helpers.RTrim(aux_pope.Fax), GPrt_FonoMascara, 0);
                    }
                    else
                    {
                        initObj.Frm_Crea_Participante.Telefono.Mask = "";
                        initObj.Frm_Crea_Participante.Fax.Mask = "";
                        initObj.Frm_Crea_Participante.Telefono.Text = VB6Helpers.RTrim(aux_pope.Telefono);
                        initObj.Frm_Crea_Participante.Fax.Text = VB6Helpers.RTrim(aux_pope.Fax);
                    }

                    initObj.Frm_Crea_Participante.Telex.Text = VB6Helpers.RTrim(aux_pope.Telex);
                    initObj.Frm_Crea_Participante.cas_postal.Text = VB6Helpers.RTrim(aux_pope.CasPostal);
                    initObj.Frm_Crea_Participante.cas_bco.Text = VB6Helpers.RTrim(aux_pope.CasBanco);
                    initObj.Frm_Crea_Participante.envio[aux_pope.Enviara].Selected = true;

                    Valor = "";
                    PopeBase = T_Module1.GPrt_StatIntacto;
                }
            }

            //desplegar los datos o capturar
            //Frm_Crea_Participante.Show(1);
            initObj.FormularioQueAbrir = FormulariosEnum.ParticipantesCrear.ToString();

            return (short)VB6Helpers.Val(initObj.Frm_Crea_Participante.Aceptar.Tag);
        }

        //Pegado a operacion
        /// <summary>
        /// Muestra la pantalla de crear participantes (Frm_Crea_Participantes)
        /// Post mostrar formulario
        /// </summary>
        /// <param name="Indice"></param>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        private static short VerPartyOperacion2_2(ref short Indice, InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short _retValue = 0;
            PartysPope aux_pope = new PartysPope();
            short nuestro_pais;
            short Lim;
            short PopeInd;
            short i = 0;
            short j = 0;
            short PopeBase = 0;
            short abc = 0;
            string Valor = "";
            short kk = 0;
            string KeyOper;

            Lim = -1;
            PopeInd = -1;
            Lim = (short)VB6Helpers.UBound(initObj.Module1.Pope);
            nuestro_pais = (short)VB6Helpers.Val(Mdl_Acceso.GetSceIni("FundTransfer.Pais.CodPais"));

            if (Lim >= 0)
            {
                //tiene elementos
                for (i = 0; i <= (short)Lim; i++)
                {
                    if (initObj.Module1.Pope[i].Secuencia == initObj.Module1.PrtControl.Indice)
                    {
                        PopeInd = i;
                        break;
                    }
                }
            }


            //acepto
            if (VB6Helpers.Val(initObj.Frm_Crea_Participante.Aceptar.Tag) == T_Module1.GPrt_RetExiste)
            {
                if (PopeInd >= 0)
                {
                    //edito alguno ya en memoria
                    initObj.Module1.Pope[PopeInd].EsBanco = initObj.Frm_Crea_Participante.EsBanco.Value;
                    if (initObj.Module1.Pope[PopeInd].EsBanco != 0)
                    {
                        initObj.Module1.Pope[PopeInd].RutSwift = Module1.FilCero((initObj.Frm_Crea_Participante.rut.Text),
                            GPrt_SwiftMascara, -1);
                    }
                    else
                    {
                        initObj.Module1.Pope[PopeInd].RutSwift = Module1.FilCero((initObj.Frm_Crea_Participante.rut.Text),
                            GPrt_RutMascara, 0);
                    }

                    initObj.Module1.Pope[PopeInd].Nombre = initObj.Frm_Crea_Participante.Nombre.Text;
                    initObj.Module1.Pope[PopeInd].Direccion = initObj.Frm_Crea_Participante.Direccion.Text;
                    initObj.Module1.Pope[PopeInd].comuna = initObj.Frm_Crea_Participante.comuna.Text ?? string.Empty;
                    initObj.Module1.Pope[PopeInd].Ciudad = initObj.Frm_Crea_Participante.Ciudad.Text ?? string.Empty;
                    initObj.Module1.Pope[PopeInd].estado = initObj.Frm_Crea_Participante.Estado.Text ?? string.Empty;
                    if (initObj.Frm_Crea_Participante.Pais.ListIndex != -1)
                    {
                        initObj.Module1.Pope[PopeInd].CodPais =
                            (short)initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Data;
                    }

                    if (initObj.Module1.Pope[PopeInd].CodPais == -1)
                    {
                        initObj.Module1.Pope[PopeInd].Pais = string.Empty;
                            //VB6Helpers.Trim(MODGPYF0.copiardestring(initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Value, ")", 2));
                    }
                    else
                    {
                        initObj.Module1.Pope[PopeInd].Pais = initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Value;
                    }

                    initObj.Module1.Pope[PopeInd].Postal = initObj.Frm_Crea_Participante.Postal.Text;

                    initObj.Module1.Pope[PopeInd].Telefono = initObj.Frm_Crea_Participante.Telefono.Text;
                    initObj.Module1.Pope[PopeInd].Fax = initObj.Frm_Crea_Participante.Fax.Text;
                    if (initObj.Frm_Crea_Participante.Pais.ListIndex != -1)
                    {
                        if (initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Data == nuestro_pais)
                        {
                            initObj.Module1.Pope[PopeInd].Telefono = Module1.FilCero((initObj.Frm_Crea_Participante.Telefono.Text), GPrt_FonoMascara, 0);
                            initObj.Module1.Pope[PopeInd].Fax = Module1.FilCero((initObj.Frm_Crea_Participante.Fax.Text), GPrt_FonoMascara, 0);
                        }

                    }

                    initObj.Module1.Pope[PopeInd].Telex = initObj.Frm_Crea_Participante.Telex.Text;
                    initObj.Module1.Pope[PopeInd].CasPostal = initObj.Frm_Crea_Participante.cas_postal.Text ?? string.Empty;
                    initObj.Module1.Pope[PopeInd].CasBanco = initObj.Frm_Crea_Participante.cas_bco.Text ?? string.Empty;
                    for (kk = 0; kk <= 3; kk++)
                    {
                        if (initObj.Frm_Crea_Participante.envio[kk].Selected)
                        {
                            initObj.Module1.Pope[PopeInd].Enviara = kk;
                        }
                    }

                    short _switchVar1 = initObj.Module1.Pope[PopeInd].Status;
                    if (_switchVar1 == T_Module1.GPrt_StatNuevo)
                    {
                    }
                    else if (_switchVar1 == T_Module1.GPrt_StatCambio)
                    {
                    }
                    else if (_switchVar1 == T_Module1.GPrt_StatBorro)
                    {
                        initObj.Module1.Pope[PopeInd].Status = T_Module1.GPrt_StatCambio;
                    }
                    else if (_switchVar1 == T_Module1.GPrt_StatIntacto)
                    {
                        initObj.Module1.Pope[PopeInd].Status = T_Module1.GPrt_StatCambio;
                    }
                    else if (_switchVar1 == T_Module1.GPrt_StatVacion)
                    {
                        initObj.Module1.Pope[PopeInd].Status = T_Module1.GPrt_StatNuevo;
                    }

                    Indice = PopeInd;
                }
                else
                {
                    //desde la base, agregar a memoria
                    Lim = (short)(Lim + 1);
                    VB6Helpers.RedimPreserve(ref initObj.Module1.Pope, 0, Lim);
                    initObj.Module1.Pope[Lim].EsBanco = (short)initObj.Frm_Crea_Participante.EsBanco.Value;
                    if (initObj.Module1.Pope[Lim].EsBanco != 0)
                    {
                        initObj.Module1.Pope[Lim].RutSwift = Module1.FilCero((initObj.Frm_Crea_Participante.rut.Text), GPrt_SwiftMascara, -1);
                    }
                    else
                    {
                        if (initObj.Mdl_Funciones_Varias.CARGA_PARTY == 0)
                        {
                            initObj.Module1.Pope[Lim].RutSwift = Module1.FilCero((initObj.Frm_Crea_Participante.rut.Text), GPrt_RutMascara, 0);
                        }
                        if (initObj.Mdl_Funciones_Varias.CARGA_PARTY == 1)
                        {
                            //@estanislao: se usaba txt_rut
                            initObj.Module1.Pope[Lim].RutSwift = Module1.FilCero((initObj.Frm_Crea_Participante.rut.Text), GPrt_RutMascara, 0);
                        }
                    }

                    initObj.Module1.Pope[Lim].Nombre = initObj.Frm_Crea_Participante.Nombre.Text;
                    initObj.Module1.Pope[Lim].Direccion = initObj.Frm_Crea_Participante.Direccion.Text;
                    initObj.Module1.Pope[Lim].comuna = initObj.Frm_Crea_Participante.comuna.Text ?? string.Empty;
                    initObj.Module1.Pope[Lim].Ciudad = initObj.Frm_Crea_Participante.Ciudad.Text ?? string.Empty;
                    initObj.Module1.Pope[Lim].estado = initObj.Frm_Crea_Participante.Estado.Text ?? string.Empty;
                    if (initObj.Frm_Crea_Participante.Pais.ListIndex != -1)
                    {
                        initObj.Module1.Pope[Lim].CodPais = (short)initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Data;
                    }
                    else
                    {
                        initObj.Module1.Pope[Lim].CodPais = -1;
                    }

                    if (initObj.Module1.Pope[Lim].CodPais == -1)
                    {
                        initObj.Module1.Pope[Lim].Pais = string.Empty;//Si no tiene pais seleccionado e intentar ingresar a la lista da error, en legacy deja en blanco este valor porque la posición 0 que es la que asigna, tiene en blanco el valor
                           // VB6Helpers.Trim(MODGPYF0.copiardestring((initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Value), ")", 2));
                    }
                    else
                    {
                        initObj.Module1.Pope[Lim].Pais =
                            initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Value;
                    }

                    initObj.Module1.Pope[Lim].Postal = initObj.Frm_Crea_Participante.Postal.Text ?? string.Empty;

                    nuestro_pais = (short)VB6Helpers.Val(Mdl_Acceso.GetSceIni("FundTransfer.Pais.CodPais")); ;

                    if (initObj.Frm_Crea_Participante.Pais.ListIndex != -1)
                    {
                        if (initObj.Frm_Crea_Participante.Pais.Items[initObj.Frm_Crea_Participante.Pais.ListIndex].Data == nuestro_pais)
                        {
                            initObj.Module1.Pope[Lim].Telefono = Module1.FilCero((initObj.Frm_Crea_Participante.Telefono.Text), GPrt_FonoMascara, 0);
                            initObj.Module1.Pope[Lim].Fax = Module1.FilCero((initObj.Frm_Crea_Participante.Fax.Text), GPrt_FonoMascara, 0);
                        }
                        else
                        {
                            initObj.Module1.Pope[Lim].Telefono = initObj.Frm_Crea_Participante.Telefono.Text;
                            initObj.Module1.Pope[Lim].Fax = initObj.Frm_Crea_Participante.Fax.Text;
                        }

                    }
                    else
                    {
                        initObj.Module1.Pope[Lim].Telefono = initObj.Frm_Crea_Participante.Telefono.Text;
                        initObj.Module1.Pope[Lim].Fax = initObj.Frm_Crea_Participante.Fax.Text;
                    }

                    initObj.Module1.Pope[Lim].Telex = initObj.Frm_Crea_Participante.Telex.Text;
                    initObj.Module1.Pope[Lim].CasPostal = initObj.Frm_Crea_Participante.cas_postal.Text ?? string.Empty;
                    initObj.Module1.Pope[Lim].CasBanco = initObj.Frm_Crea_Participante.cas_bco.Text ?? string.Empty;
                    for (kk = 0; kk <= 3; kk++)
                    {
                        if (initObj.Frm_Crea_Participante.envio[kk].Selected)
                        {
                            initObj.Module1.Pope[Lim].Enviara = kk;
                        }
                    }

                    initObj.Module1.Pope[Lim].Secuencia = initObj.Module1.PrtControl.Indice;
                    initObj.Module1.Pope[Lim].Status = PopeBase;
                    if (PopeBase == T_Module1.GPrt_StatIntacto)
                    {
                        initObj.Module1.Pope[Lim].Status = T_Module1.GPrt_StatCambio;
                    }
                    Indice = Lim;
                }

            }

            return (short)VB6Helpers.Val(initObj.Frm_Crea_Participante.Aceptar.Tag);
        }

        /// <summary>
        /// Pre llamado al formulario.
        /// Carga los datos del participante, y si tiene mas de una direccion o nombre (razo social), marca para
        /// mostrar la pantalla de seleccion
        /// </summary>
        /// <param name="QueTipo"></param>
        /// <param name="flag"></param>
        /// <param name="rut"></param>
        /// <param name="Swift"></param>
        /// <param name="Bco"></param>
        /// <param name="EsBanco"></param>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        private static short VerPartySy1_2(ref short QueTipo, ref short flag, ref string rut, ref string Swift,
            ref string Bco, short EsBanco, InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            using (var tracer = new Tracer())
            {
                short _retValue = 0;
                string s = "";
                short Borrado = 0;
                bool TieneRut;

                try
                {
                    pro_sce_prty_s02_MS_Result queryResult = null;
                    s = VB6Helpers.CStr(initObj.Frm_Participantes.Llave.Tag);
                    s = s.Replace("~", "|");
                    try
                    {
                        queryResult = uow.SceRepository.EjecutarSP<pro_sce_prty_s02_MS_Result>("pro_sce_prty_s02_MS", s.ToUpper(), "1").FirstOrDefault();
                    }
                    catch (Exception)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Se ha producido un error al tratar de leer los datos de los Participantes",
                            Type = TipoMensaje.Informacion,
                            Title = T_Module1.GPrt_Caption
                        });

                        return T_Module1.GPrt_RetCancelo;
                    }

                    if (queryResult == null)
                    {
                        return T_Module1.GPrt_RetCancelo;
                    }

                    Borrado = (short)(queryResult.borrado ? -1 : 0);
                    QueTipo = (short)queryResult.tipo_party;
                    flag = (short)queryResult.flag;

                    TieneRut = queryResult.tiene_rut;
                    rut = queryResult.rut;
                    Bco = queryResult.cod_bco.ToString();
                    Swift = queryResult.swift;

                    //Obtiene datos del Party.-
                    Module1.EligeSy1_2(initObj, uow);
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta en VerPartySy1_2: ", ex);

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Error: " + ex.HResult,
                        Type = TipoMensaje.Informacion,
                        Title = T_Module1.GPrt_Caption
                    });
                }

                return _retValue;
            }
        }

        /// <summary>
        /// Post mostrar el formulario
        /// </summary>
        /// <param name="QueTipo"></param>
        /// <param name="flag"></param>
        /// <param name="rut"></param>
        /// <param name="Swift"></param>
        /// <param name="Bco"></param>
        /// <param name="EsBanco"></param>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        private static short VerPartySy2_2(ref short QueTipo, ref short flag, ref string rut, ref string Swift,
            ref string Bco, short EsBanco, InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            using (var tracer = new Tracer())
            {
                short _retValue = 0;
                string s = "";
                short Borrado = 0;
                bool TieneRut;

                try
                {
                    pro_sce_prty_s02_MS_Result queryResult = null;
                    s = VB6Helpers.CStr(initObj.Frm_Participantes.Llave.Tag);
                    s = s.Replace("~", "|").Trim();
                    try
                    {
                        queryResult = uow.SceRepository.EjecutarSP<pro_sce_prty_s02_MS_Result>("pro_sce_prty_s02_MS", s.ToUpper(), "1").FirstOrDefault();
                    }
                    catch (Exception)
                    {
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Se ha producido un error al tratar de leer los datos de los Participantes",
                            Type = TipoMensaje.Informacion,
                            Title = T_Module1.GPrt_Caption
                        });

                        return T_Module1.GPrt_RetCancelo;
                    }

                    if (queryResult == null)
                    {
                        return T_Module1.GPrt_RetCancelo;
                    }

                    Borrado = (short)(queryResult.borrado ? -1 : 0);
                    QueTipo = (short)queryResult.tipo_party;
                    flag = (short)queryResult.flag;

                    // aca es donde se ve que el participante BNARESMMXXX no cuenta con rut asociado.
                    TieneRut = queryResult.tiene_rut;
                    rut = queryResult.rut;
                    Bco = queryResult.cod_bco.ToString();
                    Swift = queryResult.swift;
                    
                    //Obtiene datos del Party.-
                    if (Module1.EligeSy2_2(initObj, uow) != 0) //el usuario cancelo la pantalla de iden_Participantes
                    {
                        _retValue = 1;
                    }
                    else // el usuario aceptó la selección de raz soc y dir
                    {
                        _retValue = 0;
                    }
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta en VerPartySy2_2: ", _ex);

                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "Error: " + _ex.HResult,
                        Type = TipoMensaje.Informacion,
                        Title = T_Module1.GPrt_Caption
                    });
                }

                return _retValue;
            }
        }

        public static void Pr_CargaPARTY1_3(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            string s = "";
            short ElTipoParty = 0;
            short ElFlag = 0;
            string ElRut = "";
            string ElSwift = "";
            string ElBco = "";
            short Ret1 = 0;
            short UnBanco = 0;
            short ElIndice = 0;

            if (VB6Helpers.Int(initObj.Mdl_Funciones_Varias.LC_PRD) == Format.StringToDouble("72")
                || VB6Helpers.Int(initObj.Mdl_Funciones_Varias.LC_PRD) == Format.StringToDouble("74"))
            {
                initObj.Mdl_Funciones_Varias.Lc_BaseNumber = initObj.Mdl_Funciones_Varias.LC_OUTGOING;  //DRACC
            }

            if (VB6Helpers.Int(initObj.Mdl_Funciones_Varias.LC_PRD) == Format.StringToDouble("62"))
            {
                initObj.Mdl_Funciones_Varias.Lc_BaseNumber = initObj.Mdl_Funciones_Varias.LC_INCOMING;  //CRACC
            }

            //LC_BASENUMBER TIENE LARGO 12
            //HAY QUE DEJARLO DE LARGO 6
            if (VB6Helpers.Len(initObj.Mdl_Funciones_Varias.Lc_BaseNumber) == 10)
            {
                initObj.Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO =
                    VB6Helpers.Mid(initObj.Mdl_Funciones_Varias.Lc_BaseNumber, 1, 6);
            }
            else if (VB6Helpers.Len(initObj.Mdl_Funciones_Varias.Lc_BaseNumber) == 9)
            {
                initObj.Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO =
                    VB6Helpers.Mid(initObj.Mdl_Funciones_Varias.Lc_BaseNumber, 1, 6);
            }

            s = initObj.Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO +
                VB6Helpers.String(12 - VB6Helpers.Len(initObj.Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO), 126);
            initObj.Frm_Participantes.Llave.Tag = s;
            initObj.Frm_Participantes.Llave.Text = initObj.Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO;

            initObj.Frm_Participantes.CargaAutomatica = initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA;

            ////DEBE IR A LA BASE DE DATOS A VALIDAR SI ESXISTE RUT Y CLIENTE ASOCIADO AL BASENUMBER
            //Ret1 = VerPartySy(ref ElTipoParty, ref ElFlag, ref ElRut, ref ElSwift, ref ElBco, UnBanco, initObj, uow);
            VerPartySy1_2(ref ElTipoParty, ref ElFlag, ref ElRut, ref ElSwift, ref ElBco, UnBanco, initObj, uow);
            if (!initObj.Frm_Participantes.AbrirIdentParticipantes)
            {
                Pr_CargaPARTY2_3(initObj, uow);
            }
            else
            {
                initObj.Frm_Participantes.AbrirDesdeCargaOperaciones = (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1);
            }
        }

        public static void Pr_CargaPARTY2_3(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            string s = "";
            short ElTipoParty = 0;
            short ElFlag = 0;
            string ElRut = "";
            string ElSwift = "";
            string ElBco = "";
            short Ret1 = 0;
            short UnBanco = 0;
            short ElIndice = 0;

            Ret1 = VerPartySy2_2(ref ElTipoParty, ref ElFlag, ref ElRut, ref ElSwift, ref ElBco, UnBanco, initObj, uow);

            if (Ret1 == T_Module1.GPrt_RetExiste)
            {
                //EXISTE PARTICIPANTE
                UpdatePartys(ElTipoParty, T_Module1.GPrt_EnParty, ElFlag, ElRut, ElSwift, ElBco, initObj);
                initObj.Frm_Participantes.Identificar.Text = "Modificar";
                initObj.Frm_Participantes.Eliminar.Enabled = true;
                //Esto no tiene sentido si en la linea siguiente se pone en false
                //initObj.Frm_Participantes.Instrucciones.Enabled = Module1.PrtyFlag(initObj.Module1.Partys[initObj.Module1.PrtControl.Indice].FlagParty, 
                //    T_Module1.GPrt_FlagInst) != 0;
                initObj.Frm_Participantes.Instrucciones.Enabled = false;

                LstPartys_Click(initObj);
                Aceptar_Click(initObj);

                return;
            }
            else
            {
                //DEBE IR AL SERVICIO
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "El Basenumber no existe en la BD, debe realizar el ingreso del participante manualmente",
                    Type = TipoMensaje.Informacion,
                    Title = T_Module1.GPrt_Caption
                });

                //TODO:@estanislao repensar esto
                //LUEGO DEBE MOSTRAR PANTALLA DE INGRESO DE PARTICIPANTES
                Ret1 = VerPartyOperacion1_2(ref ElIndice, initObj, uow);

                return;
            }

        }

        /// <summary>
        /// 3era parte de la lgica de carga automatica
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        public static void Pr_CargaPARTY3_3(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            short ElIndice = 0;

            var Ret1 = VerPartyOperacion2_2(ref ElIndice, initObj, uow);

            if (Ret1 == T_Module1.GPrt_RetExiste && initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1
                && initObj.Mdl_Funciones_Varias.CARGA_PARTY == 1)
            {
                // simulo el post de identificar
                Identificar_Click_1(initObj, uow);
                return;
            }

            if (Ret1 == T_Module1.GPrt_RetExiste)
            {
                UpdatePope(ElIndice, initObj);
                initObj.Frm_Participantes.Identificar.Text = "Modificar";
                initObj.Frm_Participantes.Eliminar.Enabled = true;
                initObj.Frm_Participantes.Instrucciones.Enabled = false;
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Text = "La operacion no puede ser cursada ya que el participante ingresado debe ser creado",
                    Type = TipoMensaje.Informacion,
                    Title = T_Module1.GPrt_Caption
                });

                //if (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                //{
                //Frm_Participantes.Form_Unload(initObj);
                initObj.FormularioQueAbrir = FormulariosEnum.Index.ToString();
                //}

                return;
            }

            if (Ret1 == T_Module1.GPrt_RetCancelo)
            {
                //if (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                //{
                //Frm_Participantes.Form_Unload(initObj);
                initObj.FormularioQueAbrir = FormulariosEnum.Index.ToString();
                //}

                return;
            }
        }
    }

}
