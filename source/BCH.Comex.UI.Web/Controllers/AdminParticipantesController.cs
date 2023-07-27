﻿using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.XGPY;
using BCH.Comex.Core.BL.XGPY.Modulos;
using BCH.Comex.Core.BL.XGPY.PrtyMod;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using BCH.Comex.UI.Web.Common;
using BCH.Comex.UI.Web.Helpers;
using BCH.Comex.UI.Web.Models.AdminParticipantes;
using System.Web;

namespace BCH.Comex.UI.Web.Controllers
{
    public class AdminParticipantesController : BaseController
    {
        private const string CookieName = "BCHComexXgpy";
        private XgpyService xgpyService;
        private Dictionary<string, Action> initialize;
        private Dictionary<string, Func<Object>> viewModels;
        private const string AdminPartyAppRole = "COMEX_GENERAL_XGPY";

        private ActionResult Rutear(Action Logica, Func<ActionResult> ObtenerRetorno, bool limpiar = true)
        {
            if (InitObject != null && limpiar)
                this.InitObject.Mdi_Principal.MESSAGES.Clear();

            if (Logica != null) //ejecuta la lógica
            {
                try
                {
                    Logica();
                }
                catch (Exception ex)
                {
                    InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = ex.Message,
                        Title = "Excepción: ",
                        Type = TipoMensaje.Critical
                    });
                }
            }

            if (this.InitObject == null)
                throw new Exception("Error! Estado no Inicializado.");

            if (String.IsNullOrEmpty(this.InitObject.PaginaWebQueAbrir)) // si no tengo que redireccionar
            {
                if (ObtenerRetorno == null)
                    throw new Exception("No hay que redireccionar, y la funcion que retorna la vista no esta implementada");

                ViewBag.Title = string.IsNullOrEmpty(this.InitObject.PRTGLOB.Party.idparty) ? "Administrador de Participantes" : string.Format("Participante : [{0}]", InitObject.PRTGLOB.Party.idparty.Replace("|", ""));
                ViewBag.MdiPrincipal = MenuPerfil(this.InitObject);
                //ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
                //this.InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message() { Text = "Mensaje de prueba : Object reference null", Type =TipoMensaje.Error });

                this.InitObject.UsrEsp.Jerarquia = UsuarioEspecialista().Jerarquia;
                return ObtenerRetorno();
            }
            else // tengo que redireccionar
            {
                var nuevaAccion = this.InitObject.PaginaWebQueAbrir;
                this.InitObject.PaginaWebQueAbrir = String.Empty;
                return RedirectToAction(nuevaAccion);
            }
        }

        private UI_Mdi_Principal MenuPerfil(InitializationObject init)
        {
            var objetoVerPantalla = new UI_Mdi_Principal();
            objetoVerPantalla.MESSAGES = init.Mdi_Principal.MESSAGES;
            objetoVerPantalla.Opciones = init.Mdi_Principal.Opciones;
            objetoVerPantalla.Archivo = init.Mdi_Principal.Archivo;

            //Primero verificamos si es el inicio de todo el flujo
            if (string.IsNullOrEmpty(init.PRTGLOB.Party.idparty))
            {
                if (UsuarioEspecialista().Jerarquia == 1 || UsuarioEspecialista().Tipeje == "N")
                {
                    objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, true, true, false, false, false, false, false, false);
                    objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, true, true, false, false, false);
                    objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, false, false, false, false, false);
                }
                else if (UsuarioEspecialista().Jerarquia != 1)
                {
                    //                                                                nuevo,abrir,grabar,carac,instr,cuentas,tasas,activar
                    objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, false, true, false, false, false, false, false, false);
                    //objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, true, true, false, false, false, false, false, false);
                    objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, false, true, false, false, false);
                    objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, true, false, false, false, false);
                }

                return objetoVerPantalla;
            }
            //Si tiene datos del participante
            else
            {
                //var varPRTGLOBPertenece = 1;

                ///* Legacy Clase PRTENT05 linea 78
                //   En el codigo original habia una sentencia que evaluaba el retorno de una variable dentro de un metodo, dentro de este metodo
                //habilitaban los botones, los cuales se quedaban en ese estado aun cuando la evaluacion lanzara negativo.
                // * Legacy Clase PRTYENT linea 688
                //  A partir de aqui empezamos las evaluaciones de acuerdo los parametros indicados*/

                bool elim = this.InitObject.PRTGLOB.PrtControl.Red != 0;

                //bool flagUsarConsulta = true;

                //switch (this.InitObject.PRTGLOB.Party.tipo)
                //{
                //    case T_PRTGLOB.tipo_cliente:
                //        /* Se usa el antiguo metodo habilitar con los switchs correspondientes */

                //        //                                                                nuevo,abrir,grabar,carac,instr,cuentas,tasas,activar
                //        objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, false, true, false, true, true, true, true, false);
                //        //                                                                nuevo,abrir,salvar,recup,elimi
                //        objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, false, true, true, false, elim);
                //        //                                                                 carac, inst,  cuent,  tasa, acti
                //        objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, true, true, true, true, false);

                //        break;
                //    case T_PRTGLOB.tipo_banco:

                //        if ((this.InitObject.PRTGLOB.Party.Flag & PRTGLOB.Gprt_FlagCorresponsal) == PRTGLOB.Gprt_FlagCorresponsal &&
                //            (this.InitObject.PRTGLOB.Party.Flag & PRTGLOB.Gprt_FlagAcreedor) == PRTGLOB.Gprt_FlagAcreedor)
                //        {
                //            //                                                                nuevo,abrir,grabar,carac,instr,cuentas,tasas,activar
                //            objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, false, true, false, true, true, true, true, false);
                //            //                                                                nuevo,abrir,salvar,recup,elimi
                //            objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, false, true, true, false, elim);
                //            //                                                                 carac, inst,  cuent,  tasa, acti
                //            objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, true, true, true, true, false);
                //        }
                //        else
                //        {
                //            if ((this.InitObject.PRTGLOB.Party.Flag & PRTGLOB.Gprt_FlagCorresponsal) == PRTGLOB.Gprt_FlagCorresponsal)
                //            {
                //                //Habilitar  + Excluir tasas especiales
                //                //                                                                nuevo,abrir,grabar,carac,instr,cuentas,tasas,activar
                //                objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, false, true, false, true, true, true, false, false);
                //                //                                                                nuevo,abrir,salvar,recup,elimi
                //                objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, false, true, true, false, elim);
                //                //                                                                 carac, inst,  cuent,tasa, acti
                //                objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, true, true, true, false, false);

                //                flagUsarConsulta = false;
                //                break;
                //            }
                //            else
                //            {
                //                //Habilitar + Excluir cuentas corrientes
                //                //                                                                nuevo,abrir,grabar,carac,instr,cuentas,tasas,activar
                //                objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, false, true, false, true, true, false, true, false);
                //                //                                                                nuevo,abrir,salvar,recup,elimi
                //                objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, false, true, true, false, elim);
                //                //                                                                 carac, inst,  cuent,  tasa, acti
                //                objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, true, true, false, true, false);
                //            }

                //            if ((this.InitObject.PRTGLOB.Party.Flag & PRTGLOB.Gprt_FlagAcreedor) == PRTGLOB.Gprt_FlagAcreedor)
                //            {
                //                //Habilitar  + Excluir tasas especiales
                //                //                                                                nuevo,abrir,grabar,carac,instr,cuentas,tasas,activar
                //                objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, false, true, false, true, true, true, false, false);
                //                //                                                                nuevo,abrir,salvar,recup,elimi
                //                objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, false, true, true, false, elim);
                //                //                                                                 carac, inst,  cuent,tasa, acti
                //                objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, true, true, true, false, false);

                //                flagUsarConsulta = false;
                //                break;
                //            }
                //            else
                //            {
                //                //Habilitar + Excluir cuentas corrientes
                //                //                                                                nuevo,abrir,grabar,carac,instr,cuentas,tasas,activar
                //                objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, false, true, false, true, true, false, true, false);
                //                //                                                                nuevo,abrir,salvar,recup,elimi
                //                objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, false, true, true, false, elim);
                //                //                                                                 carac, inst,  cuent,  tasa, acti
                //                objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, true, true, false, true, false);
                //            }

                //            //Excluir Tasas especiales
                //            objetoVerPantalla = DeshabilitarOpcion(objetoVerPantalla, "OpcionTasas", "tbr_Tasas");
                //        }

                //        break;
                //    case T_PRTGLOB.individuo:
                //        /* Se usa el antiguo metodo habilitar con los switchs correspondientes */

                //        //                                                                nuevo,abrir,grabar,carac,instr,cuentas,tasas,activar
                //        objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, false, true, false, true, true, true, true, false);
                //        //                                                                nuevo,abrir,salvar,recup,elimi
                //        objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, false, true, true, false, elim);
                //        //                                                                 carac, inst,  cuent,  tasa, acti
                //        objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, true, true, true, true, false);

                //        //Se deshabilita cuentas
                //        objetoVerPantalla = DeshabilitarOpcion(objetoVerPantalla, "OpcionCuentas", "tbr_Cuentas");

                //        if (string.IsNullOrEmpty(this.InitObject.PRTGLOB.Party.rut))
                //        {
                //            //Deshabilitar tasas y caracteristicas
                //            objetoVerPantalla = DeshabilitarOpcion(objetoVerPantalla, "OpcionCaracteristica", "tbr_Caracteristicas");
                //            objetoVerPantalla = DeshabilitarOpcion(objetoVerPantalla, "OpcionTasas", "tbr_Tasas");
                //        }
                //        else
                //        {
                //            //Habilitar tasas y caracteristicas
                //            objetoVerPantalla = HabilitarOpcion(objetoVerPantalla, "OpcionCaracteristica", "tbr_Caracteristicas");
                //            objetoVerPantalla = HabilitarOpcion(objetoVerPantalla, "OpcionTasas", "tbr_Tasas");
                //        }

                //        break;
                //    default:
                //        break;
                //}

                ///* Legacy PRTYENT linea 792
                // * Se evalua si el usuario tiene acceso y se vuelve a invocar el metodo de solo consulta
                // * Se uso una variable de flag que simular el comportamiento del metodo antes descrito para invocar al metodo de Modoconsulta
                // */

                //if (flagUsarConsulta)
                //{
                //    //                                                                nuevo,abrir,grabar,carac,instr,cuentas,tasas,activar
                //    objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, false, true, false, true, true, true, true, false);
                //    //                                                                nuevo,abrir,salvar,recup,elimi
                //    objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, false, true, true, false, elim);
                //    //                                                                 carac, inst,  cuent,  tasa, acti
                //    objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, true, true, true, true, false);
                //}

                //int fl = 0;
                //if (this.InitObject.PRTGLOB.Party.estado != 0)
                //{
                //    varPRTGLOBPertenece = 0;
                //    fl = 1;
                //}

                //if ((this.InitObject.PRTGLOB.Party.creauser != this.InitObject.UsrEsp.Especialista || this.InitObject.PRTGLOB.Party.creacosto != this.InitObject.UsrEsp.CentroCosto) &&
                //     this.InitObject.UsrEsp.Jerarquia == 0)
                //{
                //    if (this.InitObject.UsrEsp.Tipeje == "O" && this.InitObject.PRTGLOB.Party.tipo == PRTGLOB.tipo_banco ||
                //        this.InitObject.UsrEsp.Tipeje == "O" && this.InitObject.PRTGLOB.Party.tipo == PRTGLOB.individuo)
                //    {
                //        varPRTGLOBPertenece = 0;
                //    }
                //}

                //if (varPRTGLOBPertenece == 0)
                //{
                //    objetoVerPantalla = DeshabilitarOpcion(objetoVerPantalla, "mnuSalvarParticipante", "tbr_Grabar");
                //    objetoVerPantalla = DeshabilitarOpcion(objetoVerPantalla, "mnuEliminarParticipante", string.Empty);
                //    objetoVerPantalla = HabilitarOpcion(objetoVerPantalla, "mnuPrtyActivar", "tbr_Activar");
                //}
                //else
                //{
                //    objetoVerPantalla = HabilitarOpcion(objetoVerPantalla, "mnuSalvarParticipante", "tbr_Grabar");
                //    objetoVerPantalla = HabilitarOpcion(objetoVerPantalla, "mnuEliminarParticipante", string.Empty);
                //    objetoVerPantalla = HabilitarOpcion(objetoVerPantalla, "mnuPrtyActivar", "tbr_Activar");

                //}

                /*LEGACY PRTY05 linea 209
                 * Se vuelve a evaluar si tiene jerarquia
                 * y se agregan las validaciones puestas por los ultimos bugs
                 */
                if (UsuarioEspecialista().Jerarquia == 1 || UsuarioEspecialista().Tipeje == "N")
                {
                    //                                                                nuevo,abrir,grabar,carac,instr,cuentas,tasas,activar
                    objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, true, true, true, true, true, true, true, true);
                    //                                                                nuevo,abrir,salvar,recup,elimi
                    objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, true, true, true, true, true);
                    //                                                                 carac, inst,  cuent,  tasa, acti
                    objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, true, true, true, true, true);
                }
                else if (UsuarioEspecialista().Jerarquia != 1)
                {
                    /* Se usa el antiguo metodo habilitar con los switchs correspondientes */

                    //                                                                nuevo,abrir,grabar,carac,instr,cuentas,tasas,activar
                    objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, false, true, false, true, false, true, false, false);
                    //objetoVerPantalla.BUTTONS = MenuBotonesSwitch(init.Mdi_Principal, true, true, true, true, false, true, false, false);
                    //                                                                nuevo,abrir,salvar,recup,elimi
                    objetoVerPantalla.Archivo = MenuArchivoSwitch(init.Mdi_Principal, false, true, false, false, true);
                    //                                                                 carac, inst,  cuent,  tasa, acti
                    objetoVerPantalla.Opciones = MenuOpcionesSwitch(init.Mdi_Principal, true, false, true, false, false);
                }

                return objetoVerPantalla;
            }
        }


        private Dictionary<string, Comex.Common.UI_Modulos.UI_Button> MenuBotonesSwitch(UI_Mdi_Principal menu,
            bool nuevo, bool abrir, bool grabar, bool carac, bool instr, bool cuentas, bool tasas, bool activar)
        {
            var imageCollection = new Dictionary<string, Comex.Common.UI_Modulos.UI_Button>();

            foreach (var item in this.InitObject.Mdi_Principal.BUTTONS)
            {
                switch (item.Key)
                {
                    case "tbr_nuevoParticipante":
                        item.Value.Enabled = nuevo;
                        item.Value.ImgPath = nuevo ? item.Value.ImgPath.Replace("_F", string.Empty) : item.Value.ImgPath.Contains("Nuevo_F") ? item.Value.ImgPath : item.Value.ImgPath.Replace("Nuevo", "Nuevo_F");
                        break;

                    case "tbr_AbrirParticipante":
                        item.Value.Enabled = abrir;
                        break;

                    case "tbr_Grabar":
                        item.Value.Enabled = grabar;
                        item.Value.ImgPath = grabar ? item.Value.ImgPath.Replace("_F", string.Empty) : item.Value.ImgPath.Contains("Grabar_F") ? item.Value.ImgPath : item.Value.ImgPath.Replace("Grabar", "Grabar_F");
                        item.Value.Tag = "#";
                        break;

                    case "tbr_Caracteristicas":
                        item.Value.Enabled = carac;
                        item.Value.ImgPath = carac ? item.Value.ImgPath.Replace("_F", string.Empty) : item.Value.ImgPath.Contains("Caracteristicas_F") ? item.Value.ImgPath : item.Value.ImgPath.Replace("Caracteristicas", "Caracteristicas_F");
                        break;

                    case "tbr_Instrucciones":
                        item.Value.Enabled = instr;
                        item.Value.ImgPath = instr ? item.Value.ImgPath.Replace("_F", string.Empty) : item.Value.ImgPath.Contains("Instrucciones_F") ? item.Value.ImgPath : item.Value.ImgPath.Replace("Instrucciones", "Instrucciones_F");
                        break;

                    case "tbr_Cuentas":
                        item.Value.Enabled = cuentas;
                        item.Value.ImgPath = cuentas ? item.Value.ImgPath.Replace("_F", string.Empty) : item.Value.ImgPath.Contains("Cuentas_F") ? item.Value.ImgPath : item.Value.ImgPath.Replace("Cuentas", "Cuentas_F");
                        break;

                    case "tbr_Tasas":
                        item.Value.Enabled = tasas;
                        item.Value.ImgPath = tasas ? item.Value.ImgPath.Replace("_F", string.Empty) : item.Value.ImgPath.Contains("TasasEspeciales_F") ? item.Value.ImgPath : item.Value.ImgPath.Replace("TasasEspeciales", "TasasEspeciales_F");
                        break;

                    case "tbr_Activar":
                        item.Value.Enabled = activar;
                        item.Value.ImgPath = activar ? item.Value.ImgPath.Replace("_F", string.Empty) : item.Value.ImgPath.Contains("Activar_F") ? item.Value.ImgPath : item.Value.ImgPath.Replace("Activar", "Activar_F");
                        break;

                    default:
                        break;
                }
                imageCollection.Add(item.Key, item.Value);
            }
            return imageCollection;
        }

        private List<Comex.Common.UI_Modulos.UI_Button> MenuArchivoSwitch(UI_Mdi_Principal menu, bool nuevo, bool abrir, bool salvar, bool recup, bool elimi)
        {
            var buttonCollection = new List<Comex.Common.UI_Modulos.UI_Button>();

            foreach (var item in this.InitObject.Mdi_Principal.Archivo)
            {
                bool flagAgregar = false;

                switch (item.ID)
                {
                    case "mnuNuevoParticipante":
                        item.Enabled = nuevo;

                        if (buttonCollection.Where(x => x.ID == "mnuNuevoParticipante").Count() == 0)
                            flagAgregar = true;

                        break;

                    case "mnuAbrirParticipante":
                        item.Enabled = abrir;

                        if (buttonCollection.Where(x => x.ID == "mnuAbrirParticipante").Count() == 0)
                            flagAgregar = true;

                        break;

                    case "mnuSalvarParticipante":
                        item.Enabled = salvar;

                        if (buttonCollection.Where(x => x.ID == "mnuSalvarParticipante").Count() == 0)
                            flagAgregar = true;

                        break;

                    case "mnuRecuperarParticipante":
                        item.Enabled = recup;

                        if (buttonCollection.Where(x => x.ID == "mnuRecuperarParticipante").Count() == 0)
                            flagAgregar = true;

                        break;

                    case "mnuEliminarParticipante":
                        item.Enabled = elimi;

                        if (buttonCollection.Where(x => x.ID == "mnuEliminarParticipante").Count() == 0)
                            flagAgregar = true;

                        break;

                    case "mnuSalirParticipante":
                        item.Enabled = true;

                        if (buttonCollection.Where(x => x.ID == "mnuSalirParticipante").Count() == 0)
                            flagAgregar = true;

                        break;

                    default:
                        if (buttonCollection.Where(x => x.ID == "separator").Count() < 3)
                            flagAgregar = true;

                        break;
                }

                if (flagAgregar)
                    buttonCollection.Add(item);
            }
            return buttonCollection;
        }

        private List<Comex.Common.UI_Modulos.UI_Button> MenuOpcionesSwitch(UI_Mdi_Principal menu, bool carac, bool inst, bool cuent, bool tasa, bool acti)
        {
            var buttonCollection = new List<Comex.Common.UI_Modulos.UI_Button>();

            foreach (var item in this.InitObject.Mdi_Principal.Opciones)
            {
                bool flagAgregar = false;

                switch (item.ID)
                {
                    case "OpcionCaracteristica":
                        item.Enabled = carac;

                        if (buttonCollection.Where(x => x.ID == "OpcionCaracteristica").Count() == 0)
                            flagAgregar = true;

                        break;

                    case "OpcionInstrucciones":
                        item.Enabled = inst;

                        if (buttonCollection.Where(x => x.ID == "OpcionInstrucciones").Count() == 0)
                            flagAgregar = true;

                        break;

                    case "OpcionCuentas":
                        item.Enabled = cuent;
                        if (buttonCollection.Where(x => x.ID == "OpcionCuentas").Count() == 0)
                        {
                            flagAgregar = true;
                        }
                        break;

                    case "OpcionTasas":
                        item.Enabled = tasa;

                        if (buttonCollection.Where(x => x.ID == "OpcionTasas").Count() == 0)
                            flagAgregar = true;

                        break;

                    case "mnuPrtyActivar":
                        item.Enabled = acti;

                        if (buttonCollection.Where(x => x.ID == "mnuPrtyActivar").Count() == 0)
                            flagAgregar = true;

                        break;

                    default:
                        flagAgregar = true;
                        break;
                }
                if (flagAgregar)
                    buttonCollection.Add(item);
            }
            return buttonCollection;
        }

        private UI_Mdi_Principal DeshabilitarOpcion(UI_Mdi_Principal objetoVerPantalla, string menu, string boton)
        {
            foreach (var item in objetoVerPantalla.Archivo)
            {
                if (item.ID == menu)
                    item.Enabled = false;
            }

            foreach (var item in objetoVerPantalla.Opciones)
            {
                if (item.ID == menu)
                    item.Enabled = false;
            }

            foreach (var item in objetoVerPantalla.BUTTONS)
            {
                if (item.Key == boton)
                    item.Value.Enabled = false;
            }

            return objetoVerPantalla;
        }

        private UI_Mdi_Principal HabilitarOpcion(UI_Mdi_Principal objetoVerPantalla, string menu, string boton)
        {
            foreach (var item in objetoVerPantalla.Archivo)
            {
                if (item.ID == menu)
                    item.Enabled = true;
            }

            foreach (var item in objetoVerPantalla.Opciones)
            {
                if (item.ID == menu)
                    item.Enabled = true;
            }

            foreach (var item in objetoVerPantalla.BUTTONS)
            {
                if (item.Key == boton)
                    item.Value.Enabled = true;
            }

            return objetoVerPantalla;
        }

        private BCH.Comex.Core.BL.XGPL.UsuarioEspecialista usuarioEspecialista = null;
        private BCH.Comex.Core.BL.XGPL.XgplService bl
        {
            get
            {
                return new BCH.Comex.Core.BL.XGPL.XgplService(HttpContext.GetCurrentUser().GetDatosUsuario());
            }
        }

        private BCH.Comex.Core.BL.XGPL.UsuarioEspecialista UsuarioEspecialista()
        {
            if (usuarioEspecialista == null)
                usuarioEspecialista = bl.ObtenerUsuarioEstacion();

            return usuarioEspecialista;
        }

        public void NoRutear()
        {
            this.InitObject.PaginaWebQueAbrir = String.Empty;
        }

        public InitializationObject InitObject
        {
            get
            {
                InitializationObject initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
                return initObject;
            }
            set
            {
                Session[SessionKeys.AdminParty.PartySessionKey] = value;
            }
        }
        static AdminParticipantesController()
        {
            new PortalService().RegisterApp("XGPY", "Administrador de Participantes - Nuevo", "Generales", AdminPartyAppRole, "COMEX_GRP_GENERAL", "AdminParticipantes");
        }
        public AdminParticipantesController()
        {
            this.xgpyService = new XgpyService();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Common.Utils.ValidarEjecucionFinDia(Session);
            base.OnActionExecuting(filterContext);
        }

        [AuthorizeOrForbidden(Roles = AdminPartyAppRole)]
        public ActionResult Index()
        {
            //Esto indica que se cogera la primera razon social de la busqueda en la grilla de razones sociales.
            Int32 partyIdNombre = 0;

            //Cojo los datos del controller principal que contiene la información inicial del usuario
            var datos = this.ControllerContext.HttpContext.Session[SessionKeys.Common.DatosUsuario] as IDatosUsuario;
            UserPrty userPrty = this.LoadUserPrtySession();

            if (String.IsNullOrEmpty(userPrty.centroCosto))
            {
                userPrty.LoadRegUser(this.infoUsuario.Identificacion_CentroDeCostosImpersonado, this.infoUsuario.Identificacion_IdEspecialistaImpersonado);
                this.SaveUserPrtySession(userPrty);
            }

            this.InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            List<UI_Message> Mensaje = new List<UI_Message>();

            return Rutear(() =>
            {
                if (this.InitObject == null) //primera vez que entro al sistema
                {
                    this.InitObject = xgpyService.AdminParticipantesInit();

                    //GVT : Inicializamos la data del usuario que esta consultando - esto noe staba puesto en el desarrollo anterior
                    InitializationObject item = new InitializationObject(true);
                    MODGUSR.SyGet_Usr(datos.Identificacion_CentroDeCostosOriginal, datos.Identificacion_IdEspecialistaOriginal, ref item, xgpyService);
                    this.InitObject.UsrEsp = item.UsrEsp;
                    InitObject.PRTYENT.Hab_SGTCliEje = xgpyService.Habil_SGTCliEje(InitObject);
                    CargarInfo();
                }

                // Valida Acceso según Jerarquia
                //if (UsuarioEspecialista().Jerarquia != 1 && UsuarioEspecialista().Tipeje != "N")
                //{
                //    Mensaje.Add(new UI_Message { Title = "Acceso Denegado", Text = "Usted no tiene Acceso Autorizado para crear nuevos participantes", Type = TipoMensaje.Error });
                //}

                this.InitObject.Mdi_Principal.MESSAGES = Mensaje;
                InitObject.PARTY.IdNombre = partyIdNombre;
                InitObject.PARTY.CreaCosto = this.InitObject.UsrEsp.CentroCosto;
                InitObject.PARTY.CreaUser = this.InitObject.UsrEsp.Especialista;
                this.InitObject.PaginaWebQueAbrir = String.Empty;
            },
           () =>
           {
               return View();
           }, false);
        }

        public ActionResult NuevoParticipante()
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            var iObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (iObject == null)
                return Redirect("~/AdminParticipantes");

            //Se agrego la validación por URL
            if (UsuarioEspecialista().Jerarquia != 1 && UsuarioEspecialista().Tipeje != "N") //if (UsuarioEspecialista().Jerarquia != 1)            
                throw new Exception("Error! Acceso no Autorizado.");

            ModelState.Clear();
            NuevoParticipanteViewModel np = new NuevoParticipanteViewModel();
            iObject.PRTGLOB = new T_PRTGLOB();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = iObject.Mdi_Principal;
            Session[SessionKeys.AdminParty.PartySessionKey] = iObject;
            CargarInfo();
            this.InitObject = iObject;
            return View(np);
        }

        public ActionResult Direccion()
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
            this.xgpyService.NuevoParticipanteInit(this.InitObject);
            NuevoParticipanteViewModel np = new NuevoParticipanteViewModel(this.InitObject.PrtEnt00, this.InitObject);
            return View(np);
        }

        public ActionResult RazonSocial()
        {
            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            foreach (var mensaje in InitObject.PRTGLOB.Party.Mensajes)
            {
                this.InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Text = mensaje,
                    Type = mensaje.Contains("Functional") ? TipoMensaje.Informacion : TipoMensaje.Error,
                    AutoClose = true,
                    Title = mensaje.Contains("Functional") ? "Advertencia" : "Problema con Web Services BCH"
                });
            }

            ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
            this.xgpyService.NuevoParticipanteInit(this.InitObject);
            NuevoParticipanteViewModel np = new NuevoParticipanteViewModel(this.InitObject.PrtEnt00, this.InitObject);
            return View(np);
        }

        /// <summary>
        /// Retorna Lista de Paises
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPais()
        {
            T_PRTGLOB.paises = (tipo_paises[])Session[SessionKeys.AdminParty.PaisSessionKey];

            if (T_PRTGLOB.paises == null)
            {
                var dbSelectPai = xgpyService.Sgt_Pai_S02_MS().OrderBy(x => x.pai_painom).ToList();
                Array.Resize(ref T_PRTGLOB.paises, dbSelectPai.Count);

                for (int i = 0; i < dbSelectPai.Count; i++)
                {
                    T_PRTGLOB.paises[i] = new tipo_paises();
                    T_PRTGLOB.paises[i].codigo = (int)dbSelectPai[i].pai_paicod;
                    T_PRTGLOB.paises[i].nombre = dbSelectPai[i].pai_painom;
                }

                dbSelectPai = null;
                Session[SessionKeys.AdminParty.PaisSessionKey] = T_PRTGLOB.paises;
            }

            return Json(T_PRTGLOB.paises, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Retorna Lista de Localidades/Comunas
        /// </summary>
        /// <returns></returns>
        public JsonResult GetComunasLocalidades()
        {
            T_PRTGLOB.localidad = (tipo_localidad[])Session[SessionKeys.AdminParty.LocalidadSessionKey];

            if (T_PRTGLOB.localidad == null)
            {
                var dbSelectLoc = xgpyService.Sgt_Loc_S01_MS().OrderBy(x => x.loc_locnom).ToList();
                Array.Resize(ref T_PRTGLOB.localidad, dbSelectLoc.Count);

                for (int i = 0; i < dbSelectLoc.Count; i++)
                {
                    T_PRTGLOB.localidad[i] = new tipo_localidad();
                    T_PRTGLOB.localidad[i].codigo = (int)dbSelectLoc[i].loc_loccod;
                    T_PRTGLOB.localidad[i].nombre = dbSelectLoc[i].loc_locnom;
                    T_PRTGLOB.localidad[i].region = (int)dbSelectLoc[i].loc_locreg;
                }

                dbSelectLoc = null;
                Session[SessionKeys.AdminParty.LocalidadSessionKey] = T_PRTGLOB.localidad;
            }

            return Json(T_PRTGLOB.localidad, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Agregar nueva Razón Social, asociada a un participante
        /// </summary>
        /// <param name="idParty"></param>
        /// <param name="idNombre"></param>
        /// <param name="razonSocial"></param>
        /// <param name="nombreFantasia"></param>
        /// <param name="contacto"></param>
        /// <param name="sortKey"></param>
        /// <param name="rutBanco"></param>
        /// <returns></returns>
        public JsonResult AddRazonSocial(string razonSocial, string nombreFantasia, string contacto, string sortKey, string rutBanco)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Json(null);

            if (String.IsNullOrEmpty(InitObject.PARTY.IdParty))
                return Json(null);

            int lenRazonSocial = InitObject.PRTGLOB.nom.Count();
            Array.Resize(ref InitObject.PRTGLOB.nom, lenRazonSocial + 1);
            InitObject.PRTGLOB.nom[lenRazonSocial] = new prtynombre();

            if (razonSocial != null)
                InitObject.PRTGLOB.nom[lenRazonSocial].nombre = razonSocial.ToUpper().Trim();

            if (nombreFantasia != null)
                InitObject.PRTGLOB.nom[lenRazonSocial].fantasia = nombreFantasia.ToUpper().Trim();

            if (contacto != null)
                InitObject.PRTGLOB.nom[lenRazonSocial].contacto = contacto.ToUpper().Trim();

            if (sortKey != null)
                InitObject.PRTGLOB.nom[lenRazonSocial].sortkey = sortKey.ToUpper().Trim();

            if (rutBanco != null)
                InitObject.PRTGLOB.nom[lenRazonSocial].rut = rutBanco.ToUpper().Trim();

            InitObject.PRTGLOB.nom[lenRazonSocial].borrado = "0";
            InitObject.PRTGLOB.nom[lenRazonSocial].estado = T_PRTGLOB.nuevo;
            Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
            return Json(InitObject.PRTGLOB.nom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldRazonSocial"></param>
        /// <param name="razonSocial"></param>
        /// <param name="nombreFantasia"></param>
        /// <param name="contacto"></param>
        /// <param name="sortKey"></param>
        /// <param name="rutBanco"></param>
        /// <returns></returns>
        public JsonResult UpdateRazonSocial(string oldRazonSocial, string razonSocial, string nombreFantasia, string contacto, string sortKey, string rutBanco)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Json(null);

            if (String.IsNullOrEmpty(InitObject.PARTY.IdParty))
                return Json(null);

            for (int i = 0; i < InitObject.PRTGLOB.nom.Count(); i++)
            {
                if (InitObject.PRTGLOB.nom[i].nombre.Equals(oldRazonSocial))
                {
                    if (razonSocial != null)
                        InitObject.PRTGLOB.nom[i].nombre = razonSocial.ToUpper().Trim();

                    if (nombreFantasia != null)
                        InitObject.PRTGLOB.nom[i].fantasia = nombreFantasia.ToUpper().Trim();

                    if (contacto != null)
                        InitObject.PRTGLOB.nom[i].contacto = contacto.ToUpper().Trim();

                    if (sortKey != null)
                        InitObject.PRTGLOB.nom[i].sortkey = sortKey.ToUpper().Trim();

                    if (rutBanco != null)
                        InitObject.PRTGLOB.nom[i].rut = rutBanco.ToUpper().Trim();

                    InitObject.PRTGLOB.nom[i].borrado = "0";
                    InitObject.PRTGLOB.nom[i].estado = T_PRTGLOB.modificado;
                }
            }
            //var resultUpdateRazonSocial = xgpyService.Sce_Rsa_U01_MS(idParty, idNombre, ACTIVA_BORRADO_NO, razonSocial, nombreFantasia, contacto, sortKey);

            Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
            return Json(InitObject.PRTGLOB.direc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyRazonSocial"></param>
        /// <returns></returns>
        public JsonResult DeleteRazonSocial(string keyRazonSocial)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Json(null);

            if (String.IsNullOrEmpty(InitObject.PARTY.IdParty))
                return Json(null);


            for (int i = 0; i < InitObject.PRTGLOB.nom.Count(); i++)
            {
                if (InitObject.PRTGLOB.nom[i].nombre.Equals(keyRazonSocial))
                {
                    InitObject.PRTGLOB.nom[i].borrado = "1";
                    InitObject.PRTGLOB.nom[i].estado = T_PRTGLOB.eliminado_modificado;
                }
            }

            return Json(InitObject.PRTGLOB.direc);
        }

        public JsonResult AddDireccion(string direccion, string comuna, string codComuna, string ciudad, string region, string pais, string codPais, string casillaBancoChile, string casillaPostal, string correoElectronico, string telefono, string telex, string fax, string codPostal, string optionCorrespondencia)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Json(null);

            if (String.IsNullOrEmpty(InitObject.PARTY.IdParty))
                return Json(null);

            int countDireccion = InitObject.PRTGLOB.direc.Count();
            Array.Resize(ref InitObject.PRTGLOB.direc, countDireccion + 1);
            InitObject.PRTGLOB.direc[countDireccion] = new prtydireccion();

            if (direccion != null)
                InitObject.PRTGLOB.direc[countDireccion].direccion = direccion.ToUpper().Trim();

            if (comuna != null)
                InitObject.PRTGLOB.direc[countDireccion].comuna = comuna.ToUpper().Trim();

            if (codComuna != null)
                InitObject.PRTGLOB.direc[countDireccion].CodComuna = Convert.ToInt32(codComuna.Trim());

            if (ciudad != null)
                InitObject.PRTGLOB.direc[countDireccion].ciudad = ciudad.ToUpper().Trim();

            if (region != null)
                InitObject.PRTGLOB.direc[countDireccion].region = region.ToUpper().Trim();

            if (pais != null)
                InitObject.PRTGLOB.direc[countDireccion].pais = pais.ToUpper().Trim();

            if (codPais != null)
                InitObject.PRTGLOB.direc[countDireccion].CodPais = Convert.ToInt32(codPais.Trim());

            if (casillaBancoChile != null)
                InitObject.PRTGLOB.direc[countDireccion].CasBanco = casillaBancoChile.ToUpper().Trim();

            if (casillaPostal != null)
                InitObject.PRTGLOB.direc[countDireccion].CasPostal = casillaPostal.ToUpper().Trim();

            if (correoElectronico != null)
                InitObject.PRTGLOB.direc[countDireccion].email = correoElectronico.ToLower().Trim();

            if (telefono != null)
                InitObject.PRTGLOB.direc[countDireccion].telefono = telefono.ToUpper().Trim();

            if (telex != null)
                InitObject.PRTGLOB.direc[countDireccion].telex = telex.ToUpper().Trim();

            if (fax != null)
                InitObject.PRTGLOB.direc[countDireccion].fax = fax.ToUpper().Trim();

            if (codPostal != null)
                InitObject.PRTGLOB.direc[countDireccion].codpostal = codPostal.ToUpper().Trim();

            switch (optionCorrespondencia)
            {
                case "optionDireccion":
                    InitObject.PRTGLOB.direc[countDireccion].enviar_a = 0;
                    break;

                case "optionFax":
                    InitObject.PRTGLOB.direc[countDireccion].enviar_a = 1;
                    break;

                case "optionCasillaBancoChile":
                    InitObject.PRTGLOB.direc[countDireccion].enviar_a = 2;
                    break;

                case "optionCasillaPostal":
                    InitObject.PRTGLOB.direc[countDireccion].enviar_a = 3;
                    break;

                case "optionEmail":
                    InitObject.PRTGLOB.direc[countDireccion].enviar_a = 4;
                    break;
            }

            InitObject.PRTGLOB.direc[countDireccion].borrado = "0";
            InitObject.PRTGLOB.direc[countDireccion].estado = T_PRTGLOB.nuevo;
            Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
            return Json(InitObject.PRTGLOB.direc);
        }

        public JsonResult UpdateDireccion(string oldDireccion, string direccion, string comuna, string codComuna, string ciudad, string region, string pais, string codPais, string casillaBancoChile, string casillaPostal, string correoElectronico, string telefono, string telex, string fax, string codPostal, string optionCorrespondencia)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Json(null);

            if (String.IsNullOrEmpty(InitObject.PARTY.IdParty))
                return Json(null);

            for (int i = 0; i < InitObject.PRTGLOB.direc.Count(); i++)
            {
                if (InitObject.PRTGLOB.direc[i].direccion.Equals(oldDireccion))
                {
                    if (direccion != null)
                        InitObject.PRTGLOB.direc[i].direccion = direccion.ToUpper().Trim();

                    if (comuna != null)
                        InitObject.PRTGLOB.direc[i].comuna = comuna.ToUpper().Trim();

                    if (codComuna != null)
                        InitObject.PRTGLOB.direc[i].CodComuna = Convert.ToInt32(codComuna.Trim());

                    if (ciudad != null)
                        InitObject.PRTGLOB.direc[i].ciudad = ciudad.ToUpper().Trim();

                    if (region != null)
                        InitObject.PRTGLOB.direc[i].region = region.ToUpper().Trim();

                    if (pais != null)
                        InitObject.PRTGLOB.direc[i].pais = pais.ToUpper().Trim();

                    if (codPais != null)
                        InitObject.PRTGLOB.direc[i].CodPais = Convert.ToInt32(codPais.Trim());

                    if (casillaBancoChile != null)
                        InitObject.PRTGLOB.direc[i].CasBanco = casillaBancoChile.ToUpper().Trim();

                    if (casillaPostal != null)
                        InitObject.PRTGLOB.direc[i].CasPostal = casillaPostal.ToUpper().Trim();

                    if (correoElectronico != null)
                        InitObject.PRTGLOB.direc[i].email = correoElectronico.ToLower().Trim();

                    if (telefono != null)
                        InitObject.PRTGLOB.direc[i].telefono = telefono.ToUpper().Trim();

                    if (telex != null)
                        InitObject.PRTGLOB.direc[i].telex = telex.ToUpper().Trim();

                    if (fax != null)
                        InitObject.PRTGLOB.direc[i].fax = fax.ToUpper().Trim();

                    if (codPostal != null)
                        InitObject.PRTGLOB.direc[i].codpostal = codPostal.ToUpper().Trim();

                    switch (optionCorrespondencia)
                    {
                        case "optionDireccion":
                            InitObject.PRTGLOB.direc[i].enviar_a = 0;
                            break;

                        case "optionFax":
                            InitObject.PRTGLOB.direc[i].enviar_a = 1;
                            break;

                        case "optionCasillaBancoChile":
                            InitObject.PRTGLOB.direc[i].enviar_a = 2;
                            break;

                        case "optionCasillaPostal":
                            InitObject.PRTGLOB.direc[i].enviar_a = 3;
                            break;

                        case "optionEmail":
                            InitObject.PRTGLOB.direc[i].enviar_a = 4;
                            break;
                    }

                    InitObject.PRTGLOB.direc[i].borrado = "0";
                    InitObject.PRTGLOB.direc[i].estado = T_PRTGLOB.modificado;
                }
            }

            Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;

            return Json(InitObject.PRTGLOB.direc);
        }

        public JsonResult DeleteDireccion(string keyDireccion)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Json(null);

            if (String.IsNullOrEmpty(InitObject.PARTY.IdParty))
                return Json(null);

            for (int i = 0; i < InitObject.PRTGLOB.direc.Count(); i++)
            {
                if (InitObject.PRTGLOB.direc[i].direccion.Equals(keyDireccion))
                {
                    InitObject.PRTGLOB.direc[i].borrado = "1";
                    InitObject.PRTGLOB.direc[i].estado = T_PRTGLOB.eliminado_modificado;
                }
            }

            return Json(InitObject.PRTGLOB.direc);
        }

        public JsonResult GetInitObject()
        {
            var iObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (iObject == null)
                return Json(null);

            return Json(iObject);
        }

        public ActionResult AbrirParticipante()
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            ModelState.Clear();

            if (InitObject != null)
            {
                xgpyService.LimpiarObjetos(InitObject);
                ViewBag.MdiPrincipal = MenuPerfil(InitObject);
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
            }
            else
            {
                this.InitObject = xgpyService.AdminParticipantesInit();
                var datos = this.ControllerContext.HttpContext.Session[SessionKeys.Common.DatosUsuario] as IDatosUsuario;
                InitializationObject item = new InitializationObject(true);
                MODGUSR.SyGet_Usr(datos.Identificacion_CentroDeCostosOriginal, datos.Identificacion_IdEspecialistaOriginal, ref item, xgpyService);
                this.InitObject.UsrEsp = item.UsrEsp;
                InitObject.PRTYENT.Hab_SGTCliEje = xgpyService.Habil_SGTCliEje(InitObject);
                CargarInfo();
                ViewBag.MdiPrincipal = MenuPerfil(InitObject);
                InitObject.PARTY.IdNombre = 0;
                InitObject.PARTY.CreaCosto = this.InitObject.UsrEsp.CentroCosto;
                InitObject.PARTY.CreaUser = this.InitObject.UsrEsp.Especialista;
            }

            Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
            return View();
        }

        public int SyGet_Cta(String IdParty, int Sflag)
        {
            //InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            var resultSyGet_Cta = xgpyService.pro_sce_aprty_s01_MS(IdParty, 2);
            return resultSyGet_Cta;
        }

        public JsonResult GetListaDeParticipanteByUser()
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            var resultGetListaDeParticipanteByUser = xgpyService.Sce_Rsa_S06_MS(
                InitObject.PARTY.IdNombre,
                Convert.ToInt32(InitObject.UsrEsp.CostoSuper),
                Convert.ToInt32(InitObject.UsrEsp.Especialista));

            return Json(resultGetListaDeParticipanteByUser.Select(i => new { id_party = i.id_party, razon_soci = i.razon_soci, }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListaDeParticipantesByRazonSocial(String razonSocial)
        {
            var resultGetListaDeParticipantesByRazonSocial = xgpyService.Pro_Sce_Prty_S07_MS(razonSocial);

            return Json(resultGetListaDeParticipantesByRazonSocial.Select(i => new
            {
                id_party = i.id_party,
                id_nombre = i.id_nombre,
                id_dir = i.id_dir,
                razon_soci = i.razon_soci,
                direccion = i.direccion,
                ciudad = i.ciudad,
                pais = i.pais,
            }),

                JsonRequestBehavior.AllowGet);
        }

        public JsonResult NewPrty(String idPrty)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Json(null);

            if (!String.IsNullOrEmpty(idPrty))
            {
                InitObject = xgpyService.AdminParticipantesInit();
                CargarInfo();

                string[] replacements = { "|", ".", "-" };

                foreach (string s in replacements)
                {
                    if (idPrty.IndexOf(s) > 0)
                        idPrty = idPrty.Replace(s, "");
                }

                InitObject.PARTY.IdParty = idPrty;
                InitObject.PARTY.OriginalIdParty = idPrty.PadRight(12, '|');
            }

            Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;

            return Json(idPrty);
        }

        /// <summary>
        /// Inicializa la memoria al momento de salir de Administrador de Participantes
        /// </summary>
        /// <returns></returns>
        public JsonResult CleanPrty()
        {
            XgpyResult xgpyResultSetIdPartyIdNombreIdDir = new XgpyResult();
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            InitObject = xgpyService.AdminParticipantesInit();
            Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;

            //(moo) limpiar objeto principal
            Prty oPrty = this.LoadPrtySession();
            oPrty = new Prty();
            this.SavePrtySession(oPrty);
            return Json(xgpyResultSetIdPartyIdNombreIdDir);
        }

        /// <summary>
        /// Selecciona al Participante y lo carga en variable de Session
        /// </summary>
        /// <param name="idParty"></param>
        /// <param name="idNombre"></param>
        /// <param name="idDir"></param>
        /// <returns></returns>
        public JsonResult SetIdPartyIdNombreIdDir(String idParty, Int32? idNombre, Int32? idDir)
        {
            XgpyResult resLoadPrty = new XgpyResult();
            Prty oPrty = this.LoadPrtySession();

            if (String.IsNullOrEmpty(oPrty.idPrty))
            {
                RespStatus respStatus = oPrty.Load(idParty);

                if (respStatus == RespStatus.PrtyExiste)
                    this.SavePrtySession(oPrty);
            }

            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (!String.IsNullOrEmpty(idParty))
            {
                if (idParty.IndexOf("|") > 0)
                    idParty = idParty.Replace("|", "");

                InitObject.PARTY.IdParty = idParty;
                InitObject.PARTY.OriginalIdParty = idParty.PadRight(12, '|');

                if (idNombre.HasValue)
                    InitObject.PARTY.IdNombre = idNombre.Value;


                if (idDir.HasValue)
                    InitObject.PARTY.IdDir = idDir.Value;


                Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
                var resultParty = xgpyService.Sce_Prty_S08_MS(InitObject.PARTY.OriginalIdParty);

                if (resultParty != null)
                {
                    //(moo) participante encontrado
                    if (resultParty.borrado)
                    {
                        //(moo) participante en proceso de borrado
                        resLoadPrty.ErrorCode = 3;
                        resLoadPrty.Message = "Participante en proceso de Borrado.";
                    }
                    else
                    {
                        //(moo) extraer Razones Sociales
                        var resultPrtyRazonesSociales = xgpyService.Sce_Rsa_S07_MS(InitObject.PARTY.OriginalIdParty);
                        //(moo) extraer Direcciones
                        var resultPrtyDirecciones = xgpyService.Sce_Dad_S08_MS(InitObject.PARTY.OriginalIdParty);
                        Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;

                        if (CargarParticipante(resultParty, resultPrtyRazonesSociales, resultPrtyDirecciones))
                        {
                            resLoadPrty.ErrorCode = 0;
                            resLoadPrty.Message = "";
                            resLoadPrty.iObject = InitObject;
                            Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
                        }
                        else
                        {
                            resLoadPrty.ErrorCode = 900;
                            resLoadPrty.Message = "Error al cargar Participante: " + InitObject.PARTY.IdParty;
                        }
                    }
                }
                else
                {
                    resLoadPrty.ErrorCode = 2;
                    resLoadPrty.Message = "Participante No Existe!";
                }
            }
            else
            {
                resLoadPrty.ErrorCode = 1;
                resLoadPrty.Message = "Sin Participante Asignado!";
            }

            return Json(resLoadPrty, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeDeleteStatusPartyById()
        {
            XgpyResult xgpyResultChangeDelete = new XgpyResult();
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            var resultChangeDelete = xgpyService.Sce_Prty_U01_MS(InitObject.PARTY.OriginalIdParty, false);

            if (resultChangeDelete > 0)
            {
                xgpyResultChangeDelete.ErrorCode = 0;
                xgpyResultChangeDelete.Message = "";

                //(moo) recargamos Participante
                var resultParty = xgpyService.Sce_Prty_S08_MS(InitObject.PARTY.OriginalIdParty);
            }
            else
            {
                xgpyResultChangeDelete.ErrorCode = 100;
                xgpyResultChangeDelete.Message = "No Existe Participante!";
            }

            return Json(xgpyResultChangeDelete, JsonRequestBehavior.AllowGet);
        }

        private void ViewBagLstAcEco()
        {
            var AcEco = xgpyService.LstAcEco();
            List<SelectListItem> AcEcoSelectListItem = new List<SelectListItem>();

            foreach (var item in AcEco)
            {
                AcEcoSelectListItem.Add(new SelectListItem
                {
                    Text = item.aec_aeccod.ToString() + " -- " + item.aec_aecnom.ToString(),
                    Value = item.aec_aeccod.ToString()
                });
            }

            ViewBag.acteco = AcEcoSelectListItem;
        }

        private void ViewBagLstRiesgo()
        {
            var Riesgo = xgpyService.LstRiesgo();
            List<SelectListItem> RiesgoSelectListItem = new List<SelectListItem>();

            foreach (var item in Riesgo)
            {
                RiesgoSelectListItem.Add(new SelectListItem
                {
                    Text = item.clf_clfcod.ToString() + " - " + item.clf_clfdes.ToString(),
                    Value = item.clf_clfcod
                });
            }

            ViewBag.riesgo1 = RiesgoSelectListItem;
        }

        private void ViewBagLstEjec()
        {
            var Ejec = xgpyService.LstEjec();
            List<SelectListItem> EjecSelectListItem = new List<SelectListItem>();

            foreach (var item in Ejec)
            {
                EjecSelectListItem.Add(new SelectListItem
                {
                    Text = item.ejc_ejccod.ToString() + " - " + item.ejc_ejcnom.ToString(),
                    Value = item.ejc_ejccod.ToString()
                });
            }

            ViewBag.Ejec1 = EjecSelectListItem;
        }

        #region "CARACTERISTICAS PARTICIPANTES"

        public ActionResult CaracteristicasParticipante()
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            return Rutear(() =>
            {
                ModelState.Clear();
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
                this.xgpyService.CaracteristicasParticipante_llama0711(this.InitObject);
            }, () =>
            {
                InitObject.PrtEnt07.Cbo_CenCosExp.ListIndex = -1;
                InitObject.PrtEnt07.Cbo_EspecExp.ListIndex = -1;
                InitObject.PrtEnt07.Cbo_CenCosImp.ListIndex = -1;
                InitObject.PrtEnt07.Cbo_EspecImp.ListIndex = -1;
                InitObject.PrtEnt07.Cbo_CenCosNeg.ListIndex = -1;
                InitObject.PrtEnt07.Cbo_EspecNeg.ListIndex = -1;
                InitObject.PrtEnt07.cboOficina.ListIndex = -1;
                CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(InitObject.PrtEnt07, this.InitObject);
                return View(cp);
            });
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult CaracteristicasParticipante(CaracteristicasParticipanteViewModel cp, string Command)
        {
            return Rutear(() =>
            {
                //limpio el modelstate para que use los valoresde InitObj, no los del post
                ModelState.Clear();
                ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                //int idEjecutivo = string.Empty;

                if (Command == "Aceptar")
                {
                    if (cp.cboOficina == null)
                        InitObject.PrtEnt07.cboOficina.ListIndex = 1;
                    else
                        InitObject.PrtEnt07.cboOficina.ListIndex = InitObject.PrtEnt07.cboOficina.Items.FindIndex(x => x.Data == cp.cboOficina.SelectedValue);

                    if (cp.cbEjecutivo == null)
                        InitObject.PrtEnt07.ejecutivo.ListIndex = -1;
                    else
                        InitObject.PrtEnt07.ejecutivo.ListIndex = InitObject.PrtEnt07.ejecutivo.Items.FindIndex(x => x.Data == cp.cbEjecutivo.SelectedValue);

                    if (cp.cbActividadEconomico.SelectedValue == null)
                        InitObject.PrtEnt07.Combo2.ListIndex = -1;
                    else
                        InitObject.PrtEnt07.Combo2.ListIndex = InitObject.PrtEnt07.Combo2.Items.FindIndex(x => x.Data == cp.cbActividadEconomico.SelectedValue);

                    if (cp.cbClaseRiesgo.SelectedValue == null)
                        InitObject.PrtEnt07.Combo4.ListIndex = -1;
                    else
                        InitObject.PrtEnt07.Combo4.ListIndex = InitObject.PrtEnt07.Combo4.Items.FindIndex(x => x.Data == cp.cbClaseRiesgo.SelectedValue);

                    xgpyService.CaracteristicasParticipanteAceptar(this.InitObject);

                    //INICIO MODIFICACION CNC - ACCENTURE 

                    InitObject.PaginaWebQueAbrir = "ClasificacionCliente";
                    Redirect("~/AdminParticipantes/ClasificacionCliente");

                    //FIN MODIFICACION CNC - ACCENTURE 
                }
                else if (Command == "Cancelar")
                {
                    InitObject.PaginaWebQueAbrir = "Index";
                }
            },
            () =>
            {
                cp = new CaracteristicasParticipanteViewModel(InitObject.PrtEnt07, this.InitObject);
                return View(cp);
            });
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Cb_EspecImpIdCbCenCosImportacion(int idcbCenCosImportacion)
        {
            //this.InitObject.Mdi_Principal.MESSAGES.Clear();
            //ModelState.Clear();
            //InitObject.PrtEnt07.Cbo_CenCosImp.ListIndex = InitObject.PrtEnt07.Cbo_CenCosImp.Items.FindIndex(x => x.Data == idcbCenCosImportacion);
            //CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            //cp.Update(InitObject.PrtEnt07);
            //xgpyService.CaracteristicasParticipante_Cb_CenCosImportacion_Click(this.InitObject);
            //if (cp.CbEspecImportacion.Items.Count > 0)
            //    InitObject.PrtEnt07.Cbo_EspecImp.ListIndex = InitObject.PrtEnt07.Cbo_EspecImp.Items.FindIndex(x => x.Data == 0);

            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            if (!string.IsNullOrEmpty(idcbCenCosImportacion.ToString()))
                InitObject.PrtEnt07.Cbo_CenCosImp.ListIndex = InitObject.PrtEnt07.Cbo_CenCosImp.Items.FindIndex(x => x.Data == idcbCenCosImportacion);
            else
                InitObject.PrtEnt07.Cbo_CenCosImp.ListIndex = -1;

            xgpyService.CaracteristicasParticipante_Cb_CenCosImportacion_Click(this.InitObject);

            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            if (cp.CbEspecImportacion.Items.Count > 0)
                InitObject.PrtEnt07.Cbo_EspecImp.ListIndex = InitObject.PrtEnt07.Cbo_EspecImp.Items.FindIndex(x => x.Data == 0);

            return Json(cp);
        }

        public ActionResult Cb_EspecImportacion(int idcbEspecImportacion)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ModelState.Clear();
            InitObject.PrtEnt07.Cbo_EspecImp.ListIndex = InitObject.PrtEnt07.Cbo_EspecImp.Items.FindIndex(x => x.Data == idcbEspecImportacion);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Cb_EspecExpIdCbCenCosExportacion(int idcbCenCosExportacion)
        {
            //this.InitObject.Mdi_Principal.MESSAGES.Clear();
            //ModelState.Clear();
            //InitObject.PrtEnt07.Cbo_CenCosExp.ListIndex = InitObject.PrtEnt07.Cbo_CenCosExp.Items.FindIndex(x => x.Data == idcbCenCosExportacion);
            //CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            //cp.Update(InitObject.PrtEnt07);
            //xgpyService.CaracteristicasParticipante_Cb_CenCosExportacion_Click(this.InitObject);
            //if (cp.CbEspecExportacion.Items.Count > 0)
            //    InitObject.PrtEnt07.Cbo_EspecExp.ListIndex = InitObject.PrtEnt07.Cbo_EspecExp.Items.FindIndex(x => x.Data == 0);

            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            if (!string.IsNullOrEmpty(idcbCenCosExportacion.ToString()))
                InitObject.PrtEnt07.Cbo_CenCosExp.ListIndex = InitObject.PrtEnt07.Cbo_CenCosExp.Items.FindIndex(x => x.Data == idcbCenCosExportacion);
            else
                InitObject.PrtEnt07.Cbo_CenCosExp.ListIndex = -1;

            xgpyService.CaracteristicasParticipante_Cb_CenCosExportacion_Click(this.InitObject);

            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            if (cp.CbEspecExportacion.Items.Count > 0)
                InitObject.PrtEnt07.Cbo_CenCosExp.ListIndex = InitObject.PrtEnt07.Cbo_CenCosExp.Items.FindIndex(x => x.Data == 0);

            return Json(cp);
        }

        public ActionResult Cb_EspecExportacion(int idcbEspecExportacion)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ModelState.Clear();
            InitObject.PrtEnt07.Cbo_EspecExp.ListIndex = InitObject.PrtEnt07.Cbo_EspecExp.Items.FindIndex(x => x.Data == idcbEspecExportacion);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Cb_EspecExpIdCbCenCosNegocio(int idcbCenCosNegocio)
        {
            //this.InitObject.Mdi_Principal.MESSAGES.Clear();
            //ModelState.Clear();
            //InitObject.PrtEnt07.Cbo_CenCosNeg.ListIndex = InitObject.PrtEnt07.Cbo_CenCosNeg.Items.FindIndex(x => x.Data == idcbCenCosNegocio);
            //CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            //cp.Update(InitObject.PrtEnt07);
            //xgpyService.CaracteristicasParticipante_Cb_CenCosNegocio_Click(this.InitObject);
            //if (cp.CbEspecNegocio.Items.Count > 0)
            //    InitObject.PrtEnt07.Cbo_EspecNeg.ListIndex = InitObject.PrtEnt07.Cbo_EspecNeg.Items.FindIndex(x => x.Data == 0);

            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            if (!string.IsNullOrEmpty(idcbCenCosNegocio.ToString()))
                InitObject.PrtEnt07.Cbo_CenCosNeg.ListIndex = InitObject.PrtEnt07.Cbo_CenCosNeg.Items.FindIndex(x => x.Data == idcbCenCosNegocio);
            else
                InitObject.PrtEnt07.Cbo_CenCosNeg.ListIndex = -1;

            xgpyService.CaracteristicasParticipante_Cb_CenCosNegocio_Click(this.InitObject);

            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            if (cp.CbEspecNegocio.Items.Count > 0)
                InitObject.PrtEnt07.Cbo_EspecNeg.ListIndex = InitObject.PrtEnt07.Cbo_EspecNeg.Items.FindIndex(x => x.Data == 0);

            return Json(cp);
        }

        public ActionResult Cb_EspecNegocio(int idcbEspecNegocio)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ModelState.Clear();
            InitObject.PrtEnt07.Cbo_EspecNeg.ListIndex = InitObject.PrtEnt07.Cbo_EspecNeg.Items.FindIndex(x => x.Data == idcbEspecNegocio);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp);
        }


        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Caracteristicas_PrtCliente_Click(int selectedValue)
        {
            try
            {
                CaracteristicasParticipanteViewModel cp;
                this.InitObject.PrtEnt07.prtcliente.ForEach(x => x.Selected = x.ID == selectedValue.ToString());
                xgpyService.CaracteristicasParticipante_PrtCliente_Click(InitObject);
                cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, InitObject);
                return Json(cp, JsonRequestBehavior.AllowGet);
            }
            catch
            {
            }

            return Redirect("~/AdminParticipantes");
        }


        [HttpPost]
        ////[ValidateAntiForgeryToken]8
        public ActionResult Cb_Ejecutivo_Click(int idcbEjecutivo)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt07.ejecutivo.ListIndex = InitObject.PrtEnt07.ejecutivo.Items.FindIndex(x => x.Data == idcbEjecutivo);
            xgpyService.CaracteristicasParticipante_Cb_Ejecutivo_Click(this.InitObject);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Cb_Oficina_Click(int idcbOficina)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt07.cboOficina.ListIndex = InitObject.PrtEnt07.cboOficina.Items.FindIndex(x => x.Data == idcbOficina);
            xgpyService.CaracteristicasParticipante_Cb_Oficina_Click(this.InitObject, idcbOficina);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Cb_ActividadEconomica_Click(int idcbActividadEconomica)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt07.Combo2.ListIndex = InitObject.PrtEnt07.Combo2.Items.FindIndex(x => x.Data == idcbActividadEconomica);
            xgpyService.CaracteristicasParticipante_Cb_ActividadEconomica_Click(this.InitObject);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Cb_ClaseRiesgo_Click(int idcbClaseRiesgo)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt07.Combo4.ListIndex = InitObject.PrtEnt07.Combo4.Items.FindIndex(x => x.Data == idcbClaseRiesgo);
            xgpyService.CaracteristicasParticipante_Cb_ClaseRiesgo_Click(this.InitObject);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Cb_Clasificacion_Click(int idcbClasificacion)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt07.Combo1.ListIndex = InitObject.PrtEnt07.Combo1.Items.FindIndex(x => x.Data == idcbClasificacion);
            xgpyService.CaracteristicasParticipante_Cb_Clasificacion_Click(this.InitObject);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Boton_IngImportacion_Click(int idCbCenCosImportacion, int idCbEspecImportacion)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt07.Cbo_CenCosImp.ListIndex = InitObject.PrtEnt07.Cbo_CenCosImp.Items.FindIndex(x => x.Data == idCbCenCosImportacion);
            InitObject.PrtEnt07.Cbo_EspecImp.ListIndex = InitObject.PrtEnt07.Cbo_EspecImp.Items.FindIndex(x => x.Data == idCbEspecImportacion);
            xgpyService.CaracteristicasParticipante_Boton_IngImportacion_Click(this.InitObject);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Boton_ElimImportacion_Click()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.CaracteristicasParticipante_Boton_ElimImportacion_Click(this.InitObject);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Boton_IngExportacion_Click(int idCbCenCosExportacion, int idCbEspecExportacion)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt07.Cbo_CenCosExp.ListIndex = InitObject.PrtEnt07.Cbo_CenCosExp.Items.FindIndex(x => x.Data == idCbCenCosExportacion);
            InitObject.PrtEnt07.Cbo_EspecExp.ListIndex = InitObject.PrtEnt07.Cbo_EspecExp.Items.FindIndex(x => x.Data == idCbEspecExportacion);
            xgpyService.CaracteristicasParticipante_Boton_IngExportacion_Click(this.InitObject);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Boton_ElimExportacion_Click()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.CaracteristicasParticipante_Boton_ElimExportacion_Click(this.InitObject);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Boton_IngNegocio_Click(int idCbCenCosNegocio, int idCbEspecNegocio)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt07.Cbo_CenCosNeg.ListIndex = InitObject.PrtEnt07.Cbo_CenCosNeg.Items.FindIndex(x => x.Data == idCbCenCosNegocio);
            InitObject.PrtEnt07.Cbo_EspecNeg.ListIndex = InitObject.PrtEnt07.Cbo_EspecNeg.Items.FindIndex(x => x.Data == idCbEspecNegocio);
            xgpyService.CaracteristicasParticipante_Boton_IngNegocio_Click(this.InitObject);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Boton_ElimNegocio_Click()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.CaracteristicasParticipante_Boton_ElimNegocio_Click(this.InitObject);
            CaracteristicasParticipanteViewModel cp = new CaracteristicasParticipanteViewModel(this.InitObject.PrtEnt07, this.InitObject);
            return Json(cp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FormateaRut(string Rut)
        {

            string rutFormateadoValido = "0";
            if (!string.IsNullOrEmpty(Rut))
                rutFormateadoValido = xgpyService.ValidaRut(Rut);

            return Json(new
            {
                rutValido = rutFormateadoValido
            });
        }

        #endregion

        #region CUENTAS PARTICIPANTES

        public void CuentasParticipanteService(string IdParty)
        {
            if (!string.IsNullOrEmpty(IdParty))
            {
                var cuentasPorRut = xgpyService.ObtenerCuentasPorRut(MODWS.ExtraeRut(IdParty));
                if (cuentasPorRut != null)
                {
                    InitObject.MODWS.CtaCCOL = new Cuentas[cuentasPorRut.Count()];
                    for (int i = 0; i < cuentasPorRut.Count(); i++)
                    {
                        InitObject.MODWS.CtaCCOL[i] = new Cuentas();
                        InitObject.MODWS.CtaCCOL[i].nrocta = cuentasPorRut[i].numProducto;
                        InitObject.MODWS.CtaCCOL[i].tipo = cuentasPorRut[i].codigoProducto;
                    }
                }
            }
        }

        public ActionResult CuentasParticipante()
        {
            ////(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;

            if (this.InitObject.PaginaWebQueAbrir != "CuentasParticipante")
            {
                ModelState.Clear();
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                xgpyService.CuentasParticipanteInit(this.InitObject);
            }
            else
                xgpyService.CuentasParticipanteVieneDetalleInit(this.InitObject);

            CuentasParticipanteViewModel ctasparty = new CuentasParticipanteViewModel(InitObject.PrtEnt08, this.InitObject);
            return View(ctasparty);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult CuentasParticipante(CuentasParticipanteViewModel ctasparty, string Command)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            ModelState.Clear();

            if (Command == "Aceptar")
            {
                xgpyService.CuentasParticipante_Aceptar(InitObject);
                Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;

                if (InitObject.PRTGLOB.Party.PrtGlob.FlagIngresoCuentas)
                    return Redirect("~/AdminParticipantes/CaracteristicasParticipante");
                else
                {
                    InitObject.PRTGLOB.Party.PrtGlob.FlagIngresoCuentas = false;
                    return Redirect("~/AdminParticipantes");
                }
            }
            else if (Command == "Cancelar")
            {
                Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
                return new RedirectResult("~/AdminParticipantes");
            }
            else if (Command == "Guardar")
            {
                xgpyService.CuentasParticipante_Aceptar(InitObject);
                Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
                string mensajeError = string.Empty;
                var cuentas = this.InitObject.PRTGLOB.ctaclie;

                string result = GrabarParticipanteCuentas(this.InitObject, this.InitObject.PRTGLOB.Party.idparty, ref mensajeError);

                List<string> ctas = xgpyService.Sce_ctas_s04_MS(this.InitObject.PRTGLOB.Party.idparty).Select(sc => sc.cuenta.Trim()).ToList();

                for (int i = 0; i < cuentas.Length; i++)
                {
                    this.InitObject.PRTGLOB.ctaclie[i].estado = ctas.Contains(this.InitObject.PRTGLOB.ctaclie[i].CuentaSinFormato) ? T_PRTGLOB.modificado : T_PRTGLOB.nuevo;
                    InitObject.PRTGLOB.ctaclie[i].estado = this.InitObject.PRTGLOB.ctaclie[i].estado;
                    // reemplazar tag

                    for (int c = 0; c < InitObject.PrtEnt08.Lista1.ListCount; c++)
                    {
                        var item = InitObject.PrtEnt08.Lista1.Items[c];
                        string acco = item.Value.Replace("\t", "").Replace("-", "").Replace("Activa", "").Replace("Cerrada", "").Replace("Inactiva", "").Replace("Borrada", "").Trim();
                        int tgval = ctas.Contains(acco) ? (int)T_PRTGLOB.modificado : (int)T_PRTGLOB.nuevo;
                        string tgrep = item.Tag.ToString().Replace("\t2", "\t" + tgval.ToString());
                        item.Tag = tgrep;
                    }

                    for (int c = 0; c < InitObject.PrtEnt08.Lista2.ListCount; c++)
                    {
                        var item = InitObject.PrtEnt08.Lista2.Items[c];
                        string acco = item.Value.Replace("\t", "").Replace("-", "").Replace("Activa", "").Replace("Cerrada", "").Replace("Inactiva", "").Replace("Borrada", "").Trim();
                        int tgval = ctas.Contains(acco) ? (int)T_PRTGLOB.modificado : (int)T_PRTGLOB.nuevo;
                        string tgrep = item.Tag.ToString().Replace("\t2", "\t" + tgval.ToString());
                        item.Tag = tgrep;
                    }
                }

                if (!InitObject.PRTGLOB.Party.PrtGlob.FlagIngresoCuentas)
                    InitObject.PRTGLOB.Party.PrtGlob.FlagIngresoCuentas = false;

                Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
            }

            ctasparty = new CuentasParticipanteViewModel(InitObject.PrtEnt08, this.InitObject);
            return View(ctasparty);
        }

        public ActionResult CuentasParticipante_CuentaNacionalVerificaEstado_Click(string nroCuenta)
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();

            if (InitObject.PrtEnt08.Lista1.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(nroCuenta))
                {
                    InitObject.PrtEnt08.cuenta.Text = UTILES.copiardestring(InitObject.PrtEnt08.Lista1.Items[int.Parse(nroCuenta)].Value, "\t", 1);
                    InitObject.PrtEnt08.Lista1.ListIndex = int.Parse(nroCuenta);
                }
                xgpyService.DetalleCuentas_lista1_Nacional_dblclick(InitObject);
            }

            DetalleCuentasViewModel dc = new DetalleCuentasViewModel(InitObject.PrtEnt10, InitObject);
            return Json(dc, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CuentasParticipante_CuentaNacional_Click(string nroCuenta)
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            ViewBag.ReadOnly = InitObject.PrtEnt08.Lista1.Items.Count == 1 ? false : (InitObject.PrtEnt08.Lista1.Items.Count - 1) != int.Parse(nroCuenta);

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();

            if (InitObject.PrtEnt08.Lista1.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(nroCuenta))
                {
                    InitObject.PrtEnt08.cuenta.Text = UTILES.copiardestring(InitObject.PrtEnt08.Lista1.Items[int.Parse(nroCuenta)].Value, "\t", 1);
                    //InitObject.PrtEnt08.cuenta.Text = UTILES.copiardestring(InitObject.PrtEnt08.Lista1.Items[InitObject.PrtEnt08.Lista1.Items.FindIndex(x => x.Data == Convert.ToInt32(nroCuenta))].Value, "\t", 1);
                    //InitObject.PrtEnt08.Lista1.ListIndex = InitObject.PrtEnt08.Lista1.Items.FindIndex(x => x.Data == Convert.ToInt32(nroCuenta));
                    InitObject.PrtEnt08.Lista1.ListIndex = int.Parse(nroCuenta);
                }

                xgpyService.DetalleCuentas_lista1_Nacional_dblclick(InitObject);
            }

            DetalleCuentasViewModel dc = new DetalleCuentasViewModel(InitObject.PrtEnt10, InitObject);
            return PartialView("_DetalleCuentas", dc);
        }

        public ActionResult CuentasParticipante_CuentaNacional_TipoCliente_Click(string nroCuenta)
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            ViewBag.ReadOnly = InitObject.PrtEnt08.Lista1.Items.Count == 1 ? false : (InitObject.PrtEnt08.Lista1.Items.Count - 1) != int.Parse(nroCuenta);

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            //  CuentasParticipanteViewModel cuenta = new CuentasParticipanteViewModel(InitObject.PrtEnt08, this.InitObject);

            if (InitObject.PrtEnt08.Lista1.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(nroCuenta))
                {
                    InitObject.PrtEnt08.cuenta.Text = UTILES.copiardestring(InitObject.PrtEnt08.Lista1.Items[int.Parse(nroCuenta)].Value, "\t", 1);
                    InitObject.PrtEnt08.Lista1.ListIndex = int.Parse(nroCuenta);
                }

                xgpyService.DetalleCuentas_lista1_ModalSi(InitObject);
            }

            DetalleCuentasViewModel dc = new DetalleCuentasViewModel(InitObject.PrtEnt10, InitObject);
            return PartialView("_DetalleCuentas", dc);
        }

        public ActionResult CuentasParticipante_CuentaNacional_TipoBanco_Click(string nroCuenta)
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();

            if (InitObject.PrtEnt08.Lista1.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(nroCuenta))
                {
                    InitObject.PrtEnt08.cuenta.Text = UTILES.copiardestring(InitObject.PrtEnt08.Lista1.Items[InitObject.PrtEnt08.Lista1.Items.FindIndex(x => x.Data == Convert.ToInt32(nroCuenta))].Value, "\t", 1);
                    InitObject.PrtEnt08.Lista1.ListIndex = InitObject.PrtEnt08.Lista1.Items.FindIndex(x => x.Data == Convert.ToInt32(nroCuenta));
                }

                xgpyService.DetalleCuentas_lista1_Nacional_TipoBanco_dblclick(InitObject);
            }

            DetalleCuentasViewModel dc = new DetalleCuentasViewModel(InitObject.PrtEnt10, InitObject);
            return PartialView("_DetalleCuentas", dc);
        }

        public ActionResult CuentasParticipante_CuentaExtranjeraVerificaEstado_Click(string nroCuenta)
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();

            if (InitObject.PrtEnt08.Lista2.Items.Count > 0)
            {
                if (nroCuenta != "")
                {
                    InitObject.PrtEnt08.cuenta.Text = UTILES.copiardestring(InitObject.PrtEnt08.Lista2.Items[int.Parse(nroCuenta)].Value, "\t", 1);
                    InitObject.PrtEnt08.Lista2.ListIndex = int.Parse(nroCuenta);
                }

                xgpyService.DetalleCuentas_lista2_Extranjera_dblclick(InitObject);
            }

            DetalleCuentasViewModel dc = new DetalleCuentasViewModel(InitObject.PrtEnt10, InitObject);
            return Json(dc, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CuentasParticipante_CuentaExtranjera_Click(string nroCuenta)
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            ViewBag.ReadOnly = InitObject.PrtEnt08.Lista2.Items.Count == 1 ? false : (InitObject.PrtEnt08.Lista2.Items.Count - 1) != int.Parse(nroCuenta);

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();

            if (InitObject.PrtEnt08.Lista2.Items.Count > 0)
            {
                if (nroCuenta != "")
                {
                    InitObject.PrtEnt08.cuenta.Text = UTILES.copiardestring(InitObject.PrtEnt08.Lista2.Items[int.Parse(nroCuenta)].Value, "\t", 1);
                    InitObject.PrtEnt08.Lista2.ListIndex = int.Parse(nroCuenta);
                    //InitObject.PrtEnt08.Lista2.ListIndex = InitObject.PrtEnt08.Lista2.Items.FindIndex(x => x.Data == Convert.ToInt32(nroCuenta));
                }

                xgpyService.DetalleCuentas_lista2_Extranjera_dblclick(InitObject);
            }

            DetalleCuentasViewModel dc = new DetalleCuentasViewModel(InitObject.PrtEnt10, InitObject);
            return PartialView("_DetalleCuentas", dc);
        }

        public ActionResult CuentasParticipante_CuentaExtranjera_CuentaCorriente_Si_Click(string nroCuenta)
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            ViewBag.ReadOnly = InitObject.PrtEnt08.Lista2.Items.Count == 1 ? false : (InitObject.PrtEnt08.Lista2.Items.Count - 1) != int.Parse(nroCuenta);

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();

            if (InitObject.PrtEnt08.Lista2.Items.Count > 0)
            {
                if (nroCuenta != "")
                {
                    InitObject.PrtEnt08.cuenta.Text = UTILES.copiardestring(InitObject.PrtEnt08.Lista2.Items[int.Parse(nroCuenta)].Value, "\t", 1);
                    InitObject.PrtEnt08.Lista2.ListIndex = int.Parse(nroCuenta);
                }

                xgpyService.DetalleCuentas_lista2_ModalSi(InitObject);
            }

            DetalleCuentasViewModel dc = new DetalleCuentasViewModel(InitObject.PrtEnt10, InitObject);
            return PartialView("_DetalleCuentas", dc);
        }

        public ActionResult CuentasParticipante_CuentaExtranjera_LineaCredito_Si_Click(string nroCuenta)
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            if (InitObject.PrtEnt08.Lista2.Items.Count > 0)
            {
                if (nroCuenta != "")
                {
                    InitObject.PrtEnt08.cuenta.Text = UTILES.copiardestring(InitObject.PrtEnt08.Lista2.Items[InitObject.PrtEnt08.Lista2.Items.FindIndex(x => x.Data == Convert.ToInt32(nroCuenta))].Value, "\t", 1);
                    InitObject.PrtEnt08.Lista2.ListIndex = InitObject.PrtEnt08.Lista2.Items.FindIndex(x => x.Data == Convert.ToInt32(nroCuenta));
                }
                xgpyService.DetalleCuentas_lista2_Extranjera_LineaCredito_Si_dblclick(InitObject);
            }

            DetalleCuentasViewModel dc = new DetalleCuentasViewModel(InitObject.PrtEnt10, InitObject);
            return PartialView("_DetalleCuentas", dc);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult DetalleCuenta_Aceptar_Click(DetalleCuentasViewModel detalle)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            InitObject.PaginaWebQueAbrir = "CuentasParticipante";
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            detalle.Update(InitObject.PrtEnt10);
            xgpyService.DetalleCuentas_Aceptar_Click(InitObject);
            detalle = new DetalleCuentasViewModel(InitObject.PrtEnt10, InitObject);
            CuentasParticipanteViewModel cuentas = new CuentasParticipanteViewModel(InitObject.PrtEnt08, InitObject);

            return Json(new
            {
                //data = cuentas,
                mensaje = this.InitObject.Mdi_Principal.MESSAGES.Count() == 0 ? string.Empty : this.InitObject.Mdi_Principal.MESSAGES[0].Text,
                marcaMensaje = this.InitObject.PrtEnt10.MarcaMensaje,
                PaginaWebQueAbrir = InitObject.PaginaWebQueAbrir
            });
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult DetalleCuenta_Cancelar_Click()
        {
            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            xgpyService.DetalleCuentas_Cancelar_Click(InitObject);
            return Json(InitObject);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult DetalleCuenta_Eliminar_Click(DetalleCuentasViewModel detalle)
        {
            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            InitObject.PaginaWebQueAbrir = "CuentasParticipante";
            detalle.Update(InitObject.PrtEnt10);
            xgpyService.DetalleCuentas_Eliminar_Click(InitObject);

            // return RedirectToAction(InitObject.PaginaWebQueAbrir, InitObject);
            return Json(new
            {
                mensaje = this.InitObject.Mdi_Principal.MESSAGES.Count() == 0 ? string.Empty : this.InitObject.Mdi_Principal.MESSAGES[0].Text,
                marcaMensaje = this.InitObject.PrtEnt10.MarcaMensaje,
                PaginaWebQueAbrir = InitObject.PaginaWebQueAbrir
            });
        }

        public ActionResult DetalleCuenta_Checked_Click(string elem, bool value)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();

            switch (elem)
            {

                case "prtactiva_1_Checked": //1
                    //InitObject.PrtEnt10._prtactiva_1.Value = 0;
                    InitObject.PrtEnt10._prtactiva_1.Checked = value;
                    xgpyService.DetalleCuentas_Prtactiva_Click(this.InitObject);
                    break;

                case "prtactiva_0_Checked": //0
                    //InitObject.PrtEnt10._prtactiva_0.Value = 0;
                    InitObject.PrtEnt10._prtactiva_0.Checked = value;
                    xgpyService.DetalleCuentas_Prtactiva_Click(this.InitObject);
                    break;

                case "CuentaBae_Checked":   //2
                    //InitObject.PrtEnt10.CuentaBae.Value = 0;
                    InitObject.PrtEnt10.CuentaBae.Checked = value;
                    xgpyService.DetalleCuentas_CuentaBae_Click(this.InitObject);
                    break;

                case "especial_Checked":    //2
                    //InitObject.PrtEnt10.CuentaBae.Value = 0;
                    InitObject.PrtEnt10.CuentaBae.Checked = value;
                    xgpyService.DetalleCuentas_Especial_Click(this.InitObject);
                    break;

            }

            DetalleCuentasViewModel db = new DetalleCuentasViewModel(this.InitObject.PrtEnt10, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region INSTRUCCIONES ESPECIALES

        public ActionResult InstruccionesEspeciales()
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            this.InitObject.PaginaWebQueAbrir = null;
            xgpyService.InstruccionesParticipanteInit(this.InitObject);
            InstruccionesEspecialesParticipanteViewModel iep = new InstruccionesEspecialesParticipanteViewModel(InitObject.PrtEnt04);
            return View(iep);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult InstruccionesEspeciales(InstruccionesEspecialesParticipanteViewModel iep, string Command)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
            ModelState.Clear();

            if (Command == "Aceptar")
            {
                xgpyService.InstruccionesParticipanteAceptar(this.InitObject);
                return new RedirectResult("~/AdminParticipantes");
            }
            else if (Command == "Cancelar")
                return new RedirectResult("~/AdminParticipantes");


            iep = new InstruccionesEspecialesParticipanteViewModel(this.InitObject.PrtEnt04);
            return View(iep);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult InstruccionesEspeciales_CmbMemo_Click(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
            string value = selectedValue ?? string.Empty;

            if (!string.IsNullOrEmpty(value))
            {
                InitObject.PrtEnt04.CmbMemo.ListIndex = InitObject.PrtEnt04.CmbMemo.Items.FindIndex(x => x.Data == int.Parse(selectedValue));
                InitObject.PrtEnt04.CmbMemo.Tag = InitObject.PrtEnt04.CmbMemo.ListIndex;
                InitObject.PrtEnt04.prtinstruc.Enabled = true;
            }
            else
            {
                InitObject.PrtEnt04.CmbMemo.ListIndex = -1;
                InitObject.PrtEnt04.CmbMemo.Tag = -1;
                InitObject.PrtEnt04.prtinstruc.Enabled = false;
                InitObject.PrtEnt04.aceptar.Enabled = false;
            }

            xgpyService.InstruccionesParticipanteCmbMemo(InitObject);
            var data = new InstruccionesEspecialesParticipanteViewModel(this.InitObject.PrtEnt04);
            return Json(data);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult InstruccionesEspeciales_prtinstruc_Change()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.InstruccionesParticipante_PrtInstrucciones_Change(InitObject);
            var data = new InstruccionesEspecialesParticipanteViewModel(this.InitObject.PrtEnt04);
            return Json(data);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult InstruccionesEspeciales_prtinstruc_Keypress()
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.InstruccionesParticipante_PrtInstrucciones_Keypress(InitObject);
            var data = new InstruccionesEspecialesParticipanteViewModel(this.InitObject.PrtEnt04);
            return Json(data);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult InstruccionesEspeciales_prtinstruc_LostFocus(string instrucciones)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();

            if (!string.IsNullOrEmpty(instrucciones.Trim()))
                InitObject.PrtEnt04.prtinstruc.Text = instrucciones;
            else
                InitObject.PrtEnt04.prtinstruc.Text = string.Empty;

            xgpyService.InstruccionesParticipante_PrtInstrucciones_LostFocus(InitObject);
            var data = new InstruccionesEspecialesParticipanteViewModel(this.InitObject.PrtEnt04);
            return Json(data);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult InstruccionesEspeciales_Aceptar(InstruccionesEspecialesParticipanteViewModel iep)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
            ModelState.Clear();
            iep.Update(InitObject.PrtEnt04);
            xgpyService.InstruccionesParticipanteAceptar(this.InitObject);
            iep = new InstruccionesEspecialesParticipanteViewModel(this.InitObject.PrtEnt04);
            //return new RedirectResult("~/AdminParticipantes");
            return Json(iep, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "TASAS ESPECIALES"

        [HttpGet]
        public ActionResult TasasEspecialesParticipante()
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt06 = new UI_PrtEnt06();
            InitObject.PrtEnt13 = new UI_PrtEnt13();
            this.xgpyService.TasasEspecialesParticipanteInit(this.InitObject);
            TasasEspecialesParticipanteViewModel te = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return View(te);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult TasasEspecialesParticipante(TasasEspecialesParticipanteViewModel te, string Command)
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            return Rutear(() =>
            {
                //limpio el modelstate para que use los valoresde InitObj, no los del post
                ModelState.Clear();
                ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                //te.Update(this.InitObject.PrtEnt06);
                this.InitObject.PaginaWebQueAbrir = null;
                xgpyService.TasasEspecialesParticipante_Menu_Click(this.InitObject);

                if (Command == "Aceptar")
                    xgpyService.TasasEspeciales_Boton_Aceptar_Click(this.InitObject);
                else if (Command == "Cancelar")
                    xgpyService.TasasEspeciales_Boton_Cancelar_Click(this.InitObject);
                else if (Command == "Agregar" || Command == "Modificar")
                    xgpyService.TasasEspeciales_Boton_Agregar_Click(this.InitObject);
                else if (Command == "Eliminar")
                    xgpyService.TasasEspeciales_Boton_Eliminar_Click(this.InitObject);

            }, () =>
            {
                te = new TasasEspecialesParticipanteViewModel(InitObject.PrtEnt06, this.InitObject);
                return View(te);
            });
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult TasaEspeciales_ListaComision_Click(string selectedValue)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");


            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            this.InitObject.PaginaWebQueAbrir = null;
            string value = string.Empty;

            if (InitObject.PrtEnt06.lista_com.Items.Count > 0)
            {
                value = selectedValue == null ? string.Empty : selectedValue;
                if (string.IsNullOrEmpty(value))
                    InitObject.PrtEnt06.lista_com.ListIndex = -1;
                else if (selectedValue == "-1")
                    InitObject.PrtEnt06.lista_com.ListIndex = -1;
                else
                    InitObject.PrtEnt06.lista_com.ListIndex = InitObject.PrtEnt06.lista_com.Items.FindIndex(x => x.Data == int.Parse(selectedValue));

                xgpyService.TasasEspeciales_lista_Com_dblclick(InitObject);
            }

            ModalTasaEspecialViewModel te = new ModalTasaEspecialViewModel(InitObject.PrtEnt13, InitObject);
            return PartialView("_TasaEspeciales", te);
        }

        public ActionResult TasaEspeciales_ListaModulo_Click(string selectedValue)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();

            if (InitObject.PrtEnt06.lista.Items.Count > 0)
            {
                if (selectedValue == "")
                    InitObject.PrtEnt06.lista.ListIndex = 0;//-1;
                else if (selectedValue == null)
                    InitObject.PrtEnt06.lista.ListIndex = 0;//-1;
                else if (selectedValue == "-1")
                    InitObject.PrtEnt06.lista.ListIndex = 0;//-1;
                else
                    InitObject.PrtEnt06.lista.ListIndex = InitObject.PrtEnt06.lista.Items.FindIndex(x => x.Data == int.Parse(selectedValue));

                xgpyService.TasasEspeciales_lista_dblclick(InitObject);
            }

            TasasEspecialesParticipanteViewModel te = new TasasEspecialesParticipanteViewModel(InitObject.PrtEnt06, InitObject);
            //return Json(te, JsonRequestBehavior.AllowGet);
            return Json(te, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_ListaModulo_Si_No_Click(bool result)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();

            if (result)
                xgpyService.TasasEspeciales_Lista_Si_Click(InitObject);
            else
                xgpyService.TasasEspeciales_Lista_No_Click(InitObject);

            TasasEspecialesParticipanteViewModel te = new TasasEspecialesParticipanteViewModel(InitObject.PrtEnt06, InitObject);

            //return Json(new
            //{
            //    te,
            //    Estado = result,
            //    JsonRequestBehavior.AllowGet
            //});
            return Json(te, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Tarifa_Click(string elem, bool value)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            //InitObject.PrtEnt06.tarifa.ID = elem;
            //InitObject.PrtEnt06.tarifa.Tag = elem;
            InitObject.PrtEnt06.tarifa.Checked = value;
            xgpyService.TasasEspeciales_Tarifa_Click(this.InitObject);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult TasaEspeciales_TipoInteres_Click(int selectedValue)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            this.InitObject.PrtEnt06._prttipoi_.ForEach(x => x.Selected = x.ID == selectedValue.ToString());
            xgpyService.TasasEspeciales_TipoInteres_Click(this.InitObject);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult TasaEspeciales_TasaInteres_Change(string tasaInteres)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            this.InitObject.PrtEnt06.prttasint.Text = tasaInteres == null ? string.Empty : tasaInteres;
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult TasaEspeciales_MontoGastos_Change(string montoGastos)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            this.InitObject.PrtEnt06.prtmongas.Text = montoGastos == null ? string.Empty : montoGastos;
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult TasaEspeciales_Menu_Exportaciones(TasasEspecialesParticipanteViewModel te)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            //te.Update(InitObject.PrtEnt06);
            xgpyService.TasasEspeciales_Exportaciones_Nivel1_Click(this.InitObject, 4);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Si(TasasEspecialesParticipanteViewModel te)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            // te.Update(InitObject.PrtEnt06);
            xgpyService.TasasEspeciales_Exportaciones_Nivel1_Si_Click(this.InitObject, 4);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_Ingreso() //TasasEspecialesParticipanteViewModel te
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            //te.Update(InitObject.PrtEnt06);
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_Ingreso_Click(InitObject);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);

        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_Ingreso_Si(TasasEspecialesParticipanteViewModel te)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            te.Update(InitObject.PrtEnt06);
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_Ingreso_Si_Click(InitObject);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_GestionCompraDocumentos(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_GestionCompraDocumentos_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_GestionCompraDocumentos_Si(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_GestionCompraDocumentos_Si_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_GestionDescuentoDocumentos(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_GestionDescuentoDocumentos_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_GestionDescuentoDocumentos_Si(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_GestionDescuentoDocumentos_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_CartaCredito(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_CartaCredito_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_CartaCredito_Si(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_CartaCredito_Si_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_Cobranza(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_Cobranza_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_Cobranza_Si(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_Cobranza_Si_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_Retorno(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_Retorno_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Exportaciones_Nivel2_Retorno_Si(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Exportaciones_Nivel2_Retorno_Si_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Servicios_Nivel1(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Servicios_Nivel1_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Servicios_Nivel1_Si(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Servicios_Nivel1_Si_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Servicios_Nivel2_Ex_Financiamiento(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Servicios_Nivel2_Ex_Financiamiento_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Servicios_Nivel2_Ex_Financiamiento_Si(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Servicios_Nivel2_Ex_Financiamiento_Si_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Servicios_Nivel2_Orden_Pago_Condicionado(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Servicios_Nivel2_Orden_Pago_Condicionado_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Servicios_Nivel2_Orden_Pago_Condicionado_Si(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_Servicios_Nivel2_Orden_Pago_Condicionado_Si_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Importaciones_Cobranza(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_ImportacionesCobranza_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Importaciones_Cobranza_Si(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_ImportacionesCobranza_Si_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);

            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Importaciones_CartaCredito(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_ImportacionesCartaCredito_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Menu_Importaciones_CartaCredito_Si(int Etapa)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            xgpyService.TasasEspeciales_ImportacionesCartaCredito_Si_Click(InitObject, Etapa);
            TasasEspecialesParticipanteViewModel db = new TasasEspecialesParticipanteViewModel(this.InitObject.PrtEnt06, this.InitObject);

            return Json(db, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "MODAL TASAS ESPECIALES"

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ModalTasasEspeciales_Manual_Click(string elem, bool value)
        {
            //InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            //ModelState.Clear();                         
            InitObject.PrtEnt13.manual.Checked = value;
            xgpyService.ModalTasasEspeciales_Manual_click(this.InitObject);
            ModalTasaEspecialViewModel mte = new ModalTasaEspecialViewModel(this.InitObject.PrtEnt13, this.InitObject);
            return Json(mte, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ModalTasasEspeciales_Fijo_Click(string elem, bool value)
        {
            //InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            //ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            //case "fijo_Checked":               
            InitObject.PrtEnt13.fijo.Checked = value;
            xgpyService.ModalTasasEspeciales_Fijo_click(this.InitObject);
            ModalTasaEspecialViewModel mte = new ModalTasaEspecialViewModel(this.InitObject.PrtEnt13, this.InitObject);
            return Json(mte, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ModalTasaEspeciales_Aceptar_Click(ModalTasaEspecialViewModel mte)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.PaginaWebQueAbrir = null;
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
            mte.Update(InitObject.PrtEnt13);
            xgpyService.ModalTasasEspeciales_Aceptar_Click(InitObject);
            var data = new ModalTasaEspecialViewModel(this.InitObject.PrtEnt13, InitObject);

            return Json(new
            {
                data = InitObject.PrtEnt06,
                mensaje = this.InitObject.Mdi_Principal.MESSAGES.Count() == 0 ? string.Empty : this.InitObject.Mdi_Principal.MESSAGES[0].Text,
                marcaMensaje = this.InitObject.PrtEnt13.MarcaMensaje,
                PaginaWebQueAbrir = InitObject.PaginaWebQueAbrir
            });
        }

        public ActionResult TasaEspeciales_Maximo_LostFocus(ModalTasaEspecialViewModel mte)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            mte.Update(InitObject.PrtEnt13);
            xgpyService.maximo_LostFocus(this.InitObject);
            //return Json(new
            //{
            //    idEstadoTasa = InitObject.PrtEnt13.idEstadotasa
            //});
            mte = new ModalTasaEspecialViewModel(InitObject.PrtEnt13, InitObject);
            return Json(mte, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Minimo_LostFocus(ModalTasaEspecialViewModel mte)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            mte.Update(InitObject.PrtEnt13);
            xgpyService.minimo_LostFocus(this.InitObject);
            //return Json(new
            //{
            //    idEstadoTasa = InitObject.PrtEnt13.idEstadotasa
            //});
            mte = new ModalTasaEspecialViewModel(InitObject.PrtEnt13, InitObject);
            return Json(mte, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasaEspeciales_Monto_LostFocus(ModalTasaEspecialViewModel mte)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            mte.Update(InitObject.PrtEnt13);
            xgpyService.monto_LostFocus(this.InitObject);
            mte = new ModalTasaEspecialViewModel(InitObject.PrtEnt13, InitObject);
            return Json(mte, JsonRequestBehavior.AllowGet);
            //return Json(new
            //{
            //    idEstadoTasa = InitObject.PrtEnt13.idEstadotasa
            //});
        }

        public ActionResult TasaEspeciales_Tasa_LostFocus(ModalTasaEspecialViewModel mte)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            mte.Update(InitObject.PrtEnt13);
            xgpyService.tasa_LostFocus(this.InitObject);
            mte = new ModalTasaEspecialViewModel(InitObject.PrtEnt13, InitObject);
            return Json(mte, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult ModalTasaEspeciales_Cancelar_Click(ModalTasaEspecialViewModel mte)
        //{
        //    InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
        //    if (InitObject == null)
        //    {
        //        return Redirect("~/AdminParticipantes");
        //    }
        //    ModelState.Clear();
        //    this.InitObject.PaginaWebQueAbrir = null;
        //    this.InitObject.Mdi_Principal.MESSAGES.Clear();
        //    ViewBag.MdiPrincipal = this.InitObject.Mdi_Principal;
        //    xgpyService.ModalTasasEspeciales_Cancelar_Click(InitObject);
        //    //ModalTasaEspecialViewModel mte = new ModalTasaEspecialViewModel(InitObject.PrtEnt13, InitObject);
        //    return RedirectToAction(InitObject.PaginaWebQueAbrir, InitObject);
        //}

        #endregion

        #region "DATOS BANCO"

        public ActionResult DatosBancoParticipante()
        {
            //(moo) 2015-09-25 corrección para evitar caida del sistema en caso que se entre vía URL y no esten los objectos Básicos cargados
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (InitObject == null)
                return Redirect("~/AdminParticipantes");

            //ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
            this.xgpyService.DatosBancoParticipanteInit(this.InitObject);
            DatosBancoParticipanteViewModel cp = new DatosBancoParticipanteViewModel(this.InitObject.PrtEnt11, this.InitObject);
            return View(cp);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DatosBancoParticipante(DatosBancoParticipanteViewModel db, string Command)
        {
            return Rutear(() =>
            {
                //limpio el modelstate para que use los valoresde InitObj, no los del post
                ModelState.Clear();
                ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                db.Update(this.InitObject.PrtEnt11);

                if (Command == "Aceptar")
                    xgpyService.DatosBancoParticipante_Aceptar_Click(this.InitObject);
                else if (Command == "Cancelar")
                    this.InitObject.PaginaWebQueAbrir = "Index";

            }, () => {
                db = new DatosBancoParticipanteViewModel(InitObject.PrtEnt11, this.InitObject);
                // db.Update(this.InitObject.PrtEnt11);
                ViewData["ch_Aladi"] = db.ch_Aladi.Checked ? "" : "disabled = 'disabled'";
                return View(db);
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DatosBanco_PrtTasaRefinanciamiento_Click(int selectedValue)
        {
            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            this.InitObject.PrtEnt11._prttasa_.ForEach(x => x.Selected = x.ID == selectedValue.ToString());
            xgpyService.DatosBancoParticipante_Tasa_Click(this.InitObject);
            DatosBancoParticipanteViewModel db = new DatosBancoParticipanteViewModel(this.InitObject.PrtEnt11, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DatosBanco_Index_TipoBanco_Click(string elem, bool value)
        {
            switch (elem)
            {
                case "ch_Acreedor_Checked":     //0
                    InitObject.PrtEnt11._prttipob_[0].Value = 0;
                    InitObject.PrtEnt11._prttipob_[0].Checked = value;
                    break;

                case "ch_Corresponsal_Checked": //1
                    InitObject.PrtEnt11._prttipob_[1].Value = 1;
                    InitObject.PrtEnt11._prttipob_[1].Checked = value;
                    break;

                case "ch_Avisador_Checked":     //2
                    InitObject.PrtEnt11._prttipob_[2].Value = 2;
                    InitObject.PrtEnt11._prttipob_[2].Checked = value;
                    break;
            }

            xgpyService.DatosBancoParticipante_Index_TipoBancoClick(this.InitObject, elem);
            DatosBancoParticipanteViewModel db = new DatosBancoParticipanteViewModel(this.InitObject.PrtEnt11, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        public ActionResult DatosBanco_prtaladi_Click(string elem, bool value)
        {
            ModelState.Clear();
            InitObject.PrtEnt11.prtaladi.ID = elem;
            InitObject.PrtEnt11.prtaladi.Tag = elem;
            InitObject.PrtEnt11.prtaladi.Checked = value;

            xgpyService.DatosBancoParticipante_prtaladi_Click(this.InitObject);
            DatosBancoParticipanteViewModel db = new DatosBancoParticipanteViewModel(this.InitObject.PrtEnt11, this.InitObject);
            return Json(db, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatosBanco_prtcodigo_Change(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt11.prtcodigo.Text = selectedValue;
            xgpyService.DatosBancoParticipante_prtcodigo_Change(this.InitObject);
            DatosBancoParticipanteViewModel dbp = new DatosBancoParticipanteViewModel(this.InitObject.PrtEnt11, this.InitObject);
            return Json(dbp);
        }

        public ActionResult DatosBanco_prtrut_Blur(string selectedValue) //LostFocus
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt11.prtrut.Text = selectedValue;
            xgpyService.DatosBancoParticipante_prtrut_LostFocus(this.InitObject);
            DatosBancoParticipanteViewModel dbp = new DatosBancoParticipanteViewModel(this.InitObject.PrtEnt11, this.InitObject);
            return Json(dbp);
        }

        public ActionResult DatosBanco_prtrut_Change(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt11.prtrut.Text = selectedValue;
            xgpyService.DatosBancoParticipante_prtrut_Change(this.InitObject);
            DatosBancoParticipanteViewModel dbp = new DatosBancoParticipanteViewModel(this.InitObject.PrtEnt11, this.InitObject);
            return Json(dbp);
        }

        public ActionResult DatosBanco_prtspread_Change(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt11.prtspread.Text = selectedValue;
            xgpyService.DatosBancoParticipante_prtspread_Change(this.InitObject);
            DatosBancoParticipanteViewModel dbp = new DatosBancoParticipanteViewModel(this.InitObject.PrtEnt11, this.InitObject);
            return Json(dbp);
        }

        public ActionResult DatosBanco_prtswif_Change(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt11.prtswif.Text = selectedValue;
            xgpyService.DatosBancoParticipante_prtswif_Change(this.InitObject);
            DatosBancoParticipanteViewModel dbp = new DatosBancoParticipanteViewModel(this.InitObject.PrtEnt11, this.InitObject);
            return Json(dbp);
        }

        public ActionResult DatosBanco_prtplaza_Change(string selectedValue)
        {
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
            InitObject.PrtEnt11.prtplaza.Text = selectedValue;
            xgpyService.DatosBancoParticipante_prtplaza_Change(this.InitObject);
            DatosBancoParticipanteViewModel dbp = new DatosBancoParticipanteViewModel(this.InitObject.PrtEnt11, this.InitObject);
            return Json(dbp);
        }


        #endregion

        #region "ACTIVAR RAZON SOCIAL"

        public ActionResult ActivarRazon(decimal idNombre)
        {
            ActivarRazonViewModel ap = new ActivarRazonViewModel(InitObject);
            ap.IdEstado = xgpyService.ActivarRazon(InitObject, idNombre);
            return Json(ap, JsonRequestBehavior.AllowGet);
        }

        /*
        public ActionResult AceptaParty()
        {
            ModelState.Clear();
            this.InitObject.Mdi_Principal.MESSAGES.Clear();
          //  int idEstado = xgpyService.AceptaParty(InitObject);
            ActivarRazonViewModel ap = new ActivarRazonViewModel(InitObject);
            //ap.IdEstado = idEstado;
            return Json(ap);
        }
         */

        #endregion

        public JsonResult GetPartyDataFromServer()
        {
            XgpyResult xgpyResultSetIdPrtyIdNombreIdDir = new XgpyResult();
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (!String.IsNullOrEmpty(InitObject.PARTY.OriginalIdParty))
            {
                var resultPrty = xgpyService.Sce_Prty_S08_MS(InitObject.PARTY.OriginalIdParty);

                if (resultPrty != null)
                {
                    //(moo) participante encontrado
                    if (resultPrty.borrado)
                    {
                        //(moo) participante en proceso de borrado
                        xgpyResultSetIdPrtyIdNombreIdDir.ErrorCode = 3;
                        xgpyResultSetIdPrtyIdNombreIdDir.Message = "Participante en proceso de Borrado.";
                    }
                    else
                    {
                        //(moo) extraer Razones Sociales
                        var resultPrtyRazonesSociales = xgpyService.Sce_Rsa_S07_MS(InitObject.PARTY.OriginalIdParty);
                        //(moo) extraer Direcciones
                        var resultPrtyDirecciones = xgpyService.Sce_Dad_S08_MS(InitObject.PARTY.OriginalIdParty);

                        if (CargarParticipante(resultPrty, resultPrtyRazonesSociales, resultPrtyDirecciones))
                        {
                            xgpyResultSetIdPrtyIdNombreIdDir.ErrorCode = 0;
                            xgpyResultSetIdPrtyIdNombreIdDir.Message = "";
                        }
                        else
                        {
                            xgpyResultSetIdPrtyIdNombreIdDir.ErrorCode = 900;
                            xgpyResultSetIdPrtyIdNombreIdDir.Message = "Error al cargar Participante: " + InitObject.PARTY.IdParty;
                        }
                    }
                }
                else
                {
                    xgpyResultSetIdPrtyIdNombreIdDir.ErrorCode = 2;
                    xgpyResultSetIdPrtyIdNombreIdDir.Message = "Participante No Existe!";
                }
            }
            else
            {
                xgpyResultSetIdPrtyIdNombreIdDir.ErrorCode = 1;
                xgpyResultSetIdPrtyIdNombreIdDir.Message = "Sin Participante Asignado!";
            }

            return Json(xgpyResultSetIdPrtyIdNombreIdDir, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPrty()
        {
            XgpyResult xgpyResult = new XgpyResult();
            Prty oPrty = this.LoadPrtySession();

            switch (oPrty.nextStatus)
            {
                case NextStatus.DialogPrtyEliminado:
                    xgpyResult.ErrorCode = 111;
                    xgpyResult.Message = "Participante en Proceso de Eliminado";
                    xgpyResult.Prty = oPrty;
                    break;
            }

            if (oPrty.nextStatus == NextStatus.End)
            {
                switch (oPrty.status)
                {
                    case RespStatus.PrtyExisteEnBaseBic:
                        xgpyResult.ErrorCode = 110;
                        xgpyResult.Message = "Participante No Existe!";
                        break;

                    case RespStatus.PrtyNoExiste:
                        xgpyResult.ErrorCode = 100;
                        xgpyResult.Message = "Participante No Existe!";
                        break;

                    case RespStatus.PrtyExiste:
                        xgpyResult.ErrorCode = 0;
                        xgpyResult.Message = "";
                        xgpyResult.Prty = oPrty;
                        break;
                }
            }

            return Json(xgpyResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchIdPrty"></param>
        /// <returns></returns>
        public JsonResult GetPartyByIdParty(String searchIdPrty)
        {
            if (String.IsNullOrEmpty(searchIdPrty))
            {
                return Json(null);
            }


            XgpyResult xgpyResult = new XgpyResult();

            /*
            Prty oPrty = this.LoadPrtySession();

            oPrty = new Prty(searchIdPrty);

            switch (oPrty.nextStatus)
            {
                case NextStatus.DialogPrtyEliminado:
                    xgpyResult.ErrorCode = 111;
                    xgpyResult.Message = "Participante en Proceso de Eliminado";
                    xgpyResult.Prty = oPrty;
                    break;
            }

            if (oPrty.nextStatus == NextStatus.End)
            {
                switch (oPrty.status)
                {
                    case RespStatus.PrtyExisteEnBaseBic:
                        xgpyResult.ErrorCode = 110;
                        xgpyResult.Message = "Participante No Existe!";
                        break;

                    case RespStatus.PrtyNoExiste:
                        xgpyResult.ErrorCode = 100;
                        xgpyResult.Message = "Participante No Existe!";
                        break;

                    case RespStatus.PrtyExiste:
                        xgpyResult.ErrorCode = 0;
                        xgpyResult.Message = "";
                        xgpyResult.Prty = oPrty;
                        break;
                }
            }

            this.SavePrtySession(oPrty);
             */

            #region (moo) 2015-10-09 Código Antiguo

            if (String.IsNullOrEmpty(searchIdPrty))
            {
                return Json(null);
            }

            if (String.IsNullOrEmpty(InitObject.PARTY.OriginalIdParty))
            {
                InitObject.PARTY.OriginalIdParty = searchIdPrty.PadRight(12, '|');
                if (searchIdPrty.IndexOf("|") > 0)
                {
                    searchIdPrty = searchIdPrty.Replace("|", "");
                }
            }

            var resultGetPartyByIdParty = xgpyService.Sce_Prty_S07_MS(InitObject.PARTY.OriginalIdParty);

            if (resultGetPartyByIdParty.Count > 0)
            {
                InitObject.PARTY.IdParty = searchIdPrty;

                xgpyResult.ErrorCode = 0;
                xgpyResult.Message = "";
                xgpyResult.iObject = InitObject;

                Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
            }
            else
            {
                //(moo) Base BIC no utiliza '|' para rellenar
                var resultPartyIsDataBic = xgpyService.Sce_Bic_S07_MS(InitObject.PARTY.IdParty);

                if (resultPartyIsDataBic.Count > 0)
                {
                    //(moo) no existe participante, pero existe en base BIC
                    xgpyResult.ErrorCode = 110;
                    xgpyResult.Message = "Participante No Existe!";
                }
                else
                {
                    //(moo) no existe participante
                    xgpyResult.ErrorCode = 100;
                    xgpyResult.Message = "Participante No Existe!";
                }
            }
            #endregion

            return Json(xgpyResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PrtyReActivar(String searchIdPrty, String iObject)
        {
            XgpyResult xgpyResult = new XgpyResult();

            if (String.IsNullOrEmpty(searchIdPrty))
            {
                return Json(null);
            }

            InitializationObject iO = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            string llaveFormateada = searchIdPrty.PadRight(12, '|').ToUpper();

            iO.PRTGLOB.Party.PrtGlob.Pertenece = 1;
            int nRes = PRTYENT.actualizapartySy(ref iO, xgpyService, llaveFormateada);
            int argTemp1 = 0;
            PRTYENT.lee_infopartySy(ref iO, xgpyService, searchIdPrty, ref argTemp1);

            Session[SessionKeys.AdminParty.PartySessionKey] = iO;

            if (nRes == -1)
            {
                xgpyResult.ErrorCode = 99;
                xgpyResult.Message = "Imposible Reactivar!";
                xgpyResult.iObject = iO;
                return Json(xgpyResult);
            }
            else
            {

                xgpyResult.ErrorCode = 0;
                xgpyResult.Message = "";
                xgpyResult.iObject = iO;
                return Json(xgpyResult);
            }
        }

        public JsonResult AddPartyByDataBic(String idParty)
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            XgpyResult xgpyResult = new XgpyResult();

            if (String.IsNullOrEmpty(idParty))
            {
                return null;
            }

            var keySearchByIdParty = idParty.PadRight(12, '|');
            var dateTime = DateTime.Now;

            var resultAddPartyByDataBic = xgpyService.Sce_Prty_I01_MS(
                idParty,
                keySearchByIdParty,
                InitObject.PARTY.CreaCosto.ToString(),
                InitObject.PARTY.CreaUser.ToString(),
                dateTime);

            return Json(resultAddPartyByDataBic.Select(i => new { code = i.ResponseCode, message = i.ResponseMessage }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SavePrty(String oPrty)
        {
            if (!String.IsNullOrEmpty(oPrty))
            {
                JObject o = JObject.Parse(oPrty);
                Prty prty = o.ToObject<Prty>();
                XgpyResult xgpyRes = new XgpyResult();
                //xgpyRes.ErrorCode = prty.Save();
                xgpyRes.Prty = prty;
                return Json(xgpyRes);
            }
            else
            {
                return Json(null);
            }
        }

        public JsonResult PrtyDelInteres(String idPrty)
        {
            if (String.IsNullOrEmpty(idPrty))
            {
                return Json(null);
            }

            XgpyResult xgpyRes = new XgpyResult();
            int resTint = xgpyService.Sce_Tint_D01_MS(idPrty);
            return Json(xgpyRes);
        }

        public JsonResult PrtyDelComisiones(String idPrty)
        {
            if (String.IsNullOrEmpty(idPrty))
            {
                return Json(null);
            }

            XgpyResult xgpyRes = new XgpyResult();
            int resTcom = xgpyService.Sce_Tcom_D01_MS(idPrty);
            return Json(xgpyRes);
        }

        public JsonResult PrtyDelGastos(String idPrty)
        {
            if (String.IsNullOrEmpty(idPrty))
            {
                return Json(null);
            }

            XgpyResult xgpyRes = new XgpyResult();
            int resTgas = xgpyService.Sce_Tgas_D01_MS(idPrty);
            return Json(xgpyRes);
        }

        private void CargarInfo()
        {
            InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            /*

            //(moo) localidades
            var dbSelectLoc = xgpyService.Sgt_Loc_S01_MS();
            Array.Resize(ref T_PRTGLOB.localidad, dbSelectLoc.Count);
            for (int i = 0; i < dbSelectLoc.Count; i++)
            {
                T_PRTGLOB.localidad[i] = new tipo_localidad();
                T_PRTGLOB.localidad[i].codigo = (int)dbSelectLoc[i].loc_loccod;
                T_PRTGLOB.localidad[i].nombre = dbSelectLoc[i].loc_locnom;
                T_PRTGLOB.localidad[i].region = (int)dbSelectLoc[i].loc_locreg;
            }
            dbSelectLoc = null;

            Session[SessionKeys.AdminParty.LocalidadSessionKey] = T_PRTGLOB.localidad;
             * */


            //(moo) monedas
            var dbSelectMnd = xgpyService.Sgt_Mnd_S02_MS();
            Array.Resize(ref InitObject.PRTGLOB.cod_nom_moneda, dbSelectMnd.Count);
            for (int i = 0; i < dbSelectMnd.Count; i++)
            {
                InitObject.PRTGLOB.cod_nom_moneda[i] = new reg_monedas();
                InitObject.PRTGLOB.cod_nom_moneda[i].CodMoneda = Convert.ToString((int)dbSelectMnd[i].mnd_mndcod);
                InitObject.PRTGLOB.cod_nom_moneda[i].NombMoneda = dbSelectMnd[i].mnd_mndnom;
                InitObject.PRTGLOB.cod_nom_moneda[i].Nemmoneda = dbSelectMnd[i].mnd_mndnmc;
                InitObject.PRTGLOB.cod_nom_moneda[i].Swfmoneda = dbSelectMnd[i].mnd_mndswf;

            }
            dbSelectMnd = null;
            // Session[SessionKeys.AdminParty.PartySessionKey] = InitObject.PRTGLOB.cod_nom_moneda;

            /*

            //(moo) paises
            var dbSelectPai = xgpyService.Sgt_Pai_S02_MS();
            Array.Resize(ref T_PRTGLOB.paises, dbSelectPai.Count);
            for (int i = 0; i < dbSelectPai.Count; i++)
            {
                T_PRTGLOB.paises[i] = new tipo_paises();
                T_PRTGLOB.paises[i].codigo = (int)dbSelectPai[i].pai_paicod;
                T_PRTGLOB.paises[i].nombre = dbSelectPai[i].pai_painom;
            }
            dbSelectPai = null;
            Session[SessionKeys.AdminParty.PaisSessionKey] = T_PRTGLOB.paises;
             * */

            /*
            var dbSelectClf = xgpyService.Sgt_Clf_S01_MS();
            Array.Resize(ref T_PRTGLOB.paises, dbSelectClf.Count);
            for (int i = 0; i < dbSelectClf.Count; i++)
            {
                T_PRTGLOB.riesgo[i] = new tipo_riesgo();
                T_PRTGLOB.riesgo[i].codigo = dbSelectClf[i].clf_clfcod;
                T_PRTGLOB.riesgo[i].nombre = dbSelectClf[i].clf_clfdes;
            }
            dbSelectClf = null;
            Session[SessionKeys.AdminParty.RiesgoSessionKey] = T_PRTGLOB.riesgo;
             * */

            var dbSelectSuc = xgpyService.Sgt_Suc_S01_MS();
            Array.Resize(ref InitObject.PRTGLOB.oficinas, dbSelectSuc.Count);
            for (int i = 0; i < dbSelectSuc.Count; i++)
            {
                InitObject.PRTGLOB.oficinas[i] = new codnom();
                InitObject.PRTGLOB.oficinas[i].codigo = (int)dbSelectSuc[i].suc_succod;
                InitObject.PRTGLOB.oficinas[i].nombre = dbSelectSuc[i].suc_sucnom;
            }
            dbSelectSuc = null;
            Session[SessionKeys.AdminParty.OficinasSessionKey] = InitObject.PRTGLOB.oficinas;

            var dbSelectAbr = xgpyService.Sce_Abr_S01_MS();
            Array.Resize(ref T_PRTGLOB.abrev, dbSelectAbr.Count);
            for (int i = 0; i < dbSelectAbr.Count; i++)
            {
                T_PRTGLOB.abrev[i] = new tipo_abrev();
                T_PRTGLOB.abrev[i].abrev = dbSelectAbr[i].nom_abr;
                T_PRTGLOB.abrev[i].nombre = dbSelectAbr[i].cod_abr;
            }
            dbSelectAbr = null;
            Session[SessionKeys.AdminParty.AbrevSessionKey] = T_PRTGLOB.abrev;
        }

        /// <summary>
        /// Metodo que carga en Objeto Global, datos del participante y sus información adicional
        /// </summary>
        /// <param name="dbSelectPrty"></param>
        /// <param name="dbSelectPrtyRS"></param>
        /// <param name="dbSelectPrtyDI"></param>
        /// <returns></returns>
        private Boolean CargarParticipante(sce_prty_s08_MS_Result dbSelectPrty, IList<sce_rsa_s07_MS_Result> dbSelectPrtyRS, IList<sce_dad_s08_MS_Result> dbSelectPrtyDI)
        {
            using (var tracer = new Tracer("CargarParticipante"))
            {
                Boolean retCarga = true;
                string mensajeWS = string.Empty;

                try
                {
                    InitObject.Mdi_Principal.Archivo[5].Enabled = true; // eliminar

                    InitObject.PRTGLOB.Party.tipo = (int)dbSelectPrty.tipo_party;
                    InitObject.PRTGLOB.Party.Flag = (int)dbSelectPrty.flag;
                    InitObject.PRTGLOB.Party.clasificacion = (int)dbSelectPrty.clasificac;

                    InitObject.PRTGLOB.Party.sirut = dbSelectPrty.tiene_rut ? 1 : 0;
                    InitObject.PRTGLOB.Party.rut = dbSelectPrty.rut;

                    InitObject.PRTGLOB.Party.creacosto = dbSelectPrty.crea_costo;
                    InitObject.PRTGLOB.Party.creauser = dbSelectPrty.crea_user;

                    InitObject.PRTGLOB.Party.modcosto = dbSelectPrty.mod_costo;
                    InitObject.PRTGLOB.Party.moduser = dbSelectPrty.mod_user;
                    InitObject.PRTGLOB.Party.multiple = dbSelectPrty.multiple ? 1 : 0;

                    switch (InitObject.PRTGLOB.Party.tipo)
                    {
                        case T_PRTGLOB.tipo_cliente:
                        case T_PRTGLOB.individuo:
                            InitObject.PRTGLOB.Party.oficina = dbSelectPrty.cod_ofieje;
                            InitObject.PRTGLOB.Party.ejecutivo = dbSelectPrty.cod_eject;
                            InitObject.PRTGLOB.Party.actividad = dbSelectPrty.cod_acteco;
                            InitObject.PRTGLOB.Party.riesgo = dbSelectPrty.clase_ries;
                            break;

                        case T_PRTGLOB.tipo_banco:
                            InitObject.PRTGLOB.Party.codbco = (int)dbSelectPrty.cod_bco;
                            InitObject.PRTGLOB.Party.libor = dbSelectPrty.tasa_libor ? 1 : 0;
                            InitObject.PRTGLOB.Party.prime = dbSelectPrty.tasa_prime ? 1 : 0;
                            InitObject.PRTGLOB.Party.spread = (double)dbSelectPrty.spread;
                            InitObject.PRTGLOB.Party.swif = dbSelectPrty.swift;
                            InitObject.PRTGLOB.Party.aladi = (int)dbSelectPrty.plaza_alad;
                            InitObject.PRTGLOB.Party.ejecorr = dbSelectPrty.ejec_corre;
                            break;
                    }

                    InitObject.PRTGLOB.Party.flagins = (int)dbSelectPrty.flagins;

                    InitObject.PRTGLOB.Party.insgen_imp = Convert.ToInt32(dbSelectPrty.insgen_imp); // ToString
                    InitObject.PRTGLOB.Party.insgen_exp = Convert.ToInt32(dbSelectPrty.insgen_exp);
                    InitObject.PRTGLOB.Party.insgen_ser = Convert.ToInt32(dbSelectPrty.insgen_ser);

                    InitObject.PRTGLOB.Party.inscob_imp = Convert.ToInt32(dbSelectPrty.inscob_imp);
                    InitObject.PRTGLOB.Party.inscob_exp = Convert.ToInt32(dbSelectPrty.inscob_exp);

                    InitObject.PRTGLOB.Party.inscre_imp = Convert.ToInt32(dbSelectPrty.inscre_imp);
                    InitObject.PRTGLOB.Party.inscre_exp = Convert.ToInt32(dbSelectPrty.inscre_exp);

                    InitObject.PRTGLOB.Party.estado = T_PRTGLOB.leido;
                    InitObject.PRTGLOB.Party.idparty = InitObject.PARTY.OriginalIdParty;
                    InitObject.PRTGLOB.Party.Bnumber = InitObject.PRTGLOB.Party.idparty;

                    switch (InitObject.PRTGLOB.Party.tipo)
                    {
                        case 1:
                            InitObject.PRTGLOB.Party.PrtGlob.EsBanco = 1;
                            break;

                        case 2:
                            InitObject.PRTGLOB.Party.PrtGlob.EsCITI = 1;
                            InitObject.PRTGLOB.Party.PrtGlob.EsBanco = 0;
                            break;

                        case 3:
                            InitObject.PRTGLOB.Party.PrtGlob.EsCITI = 1;
                            InitObject.PRTGLOB.Party.PrtGlob.EsBanco = 0;
                            InitObject.PRTGLOB.Party.Bnumber = InitObject.PARTY.OriginalIdParty;
                            InitObject.PRTGLOB.Party.tipo = 2;
                            break;

                        default:
                            InitObject.PRTGLOB.Party.PrtGlob.EsCITI = 0;
                            InitObject.PRTGLOB.Party.PrtGlob.EsBanco = 0;
                            break;
                    }

                    Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;

                    //(moo) razones sociales
                    InitObject.PRTGLOB.nom = new prtynombre[dbSelectPrtyRS.Count];
                    for (int i = 0; i < dbSelectPrtyRS.Count; i++)
                    {
                        InitObject.PRTGLOB.nom[i] = new prtynombre();
                        InitObject.PRTGLOB.nom[i].borrado = dbSelectPrtyRS[i].borrado ? "1" : "0";
                        InitObject.PRTGLOB.nom[i].indice = (int)dbSelectPrtyRS[i].id_nombre;
                        InitObject.PRTGLOB.nom[i].nombre = dbSelectPrtyRS[i].razon_soci.Replace("'", "`");
                        InitObject.PRTGLOB.nom[i].fantasia = dbSelectPrtyRS[i].nom_fantas.Replace("'", "`");
                        InitObject.PRTGLOB.nom[i].contacto = dbSelectPrtyRS[i].contacto.Replace("'", "`");
                        InitObject.PRTGLOB.nom[i].sortkey = dbSelectPrtyRS[i].sortkey.Replace("'", "`");
                        InitObject.PRTGLOB.nom[i].estado = T_PRTGLOB.leido;
                    }

                    Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;

                    //(moo) direcciones
                    InitObject.PRTGLOB.direc = new prtydireccion[dbSelectPrtyDI.Count];
                    for (int i = 0; i < dbSelectPrtyDI.Count; i++)
                    {
                        InitObject.PRTGLOB.direc[i] = new prtydireccion();
                        InitObject.PRTGLOB.direc[i].borrado = dbSelectPrtyDI[i].borrado ? "1" : "0";
                        InitObject.PRTGLOB.direc[i].indice = (int)dbSelectPrtyDI[i].id_dir;
                        InitObject.PRTGLOB.direc[i].indice = (int)dbSelectPrtyDI[i].id_dir;
                        InitObject.PRTGLOB.direc[i].direccion = dbSelectPrtyDI[i].direccion.Replace("'", "`");
                        InitObject.PRTGLOB.direc[i].comuna = dbSelectPrtyDI[i].comuna.Replace("'", "`");
                        InitObject.PRTGLOB.direc[i].CodComuna = (int)dbSelectPrtyDI[i].cod_comuna;
                        InitObject.PRTGLOB.direc[i].codpostal = dbSelectPrtyDI[i].cod_postal.Replace("'", "`");
                        InitObject.PRTGLOB.direc[i].region = dbSelectPrtyDI[i].estado.Replace("'", "`");
                        InitObject.PRTGLOB.direc[i].ciudad = dbSelectPrtyDI[i].ciudad.Replace("'", "`");
                        InitObject.PRTGLOB.direc[i].pais = dbSelectPrtyDI[i].pais.Replace("'", "`");
                        InitObject.PRTGLOB.direc[i].CodPais = (int)dbSelectPrtyDI[i].cod_pais;
                        InitObject.PRTGLOB.direc[i].telefono = Funciones.CleanPhoneNumber(dbSelectPrtyDI[i].telefono);
                        InitObject.PRTGLOB.direc[i].fax = Funciones.CleanPhoneNumber(dbSelectPrtyDI[i].fax);
                        InitObject.PRTGLOB.direc[i].telex = dbSelectPrtyDI[i].telex.Replace("'", "`");
                        InitObject.PRTGLOB.direc[i].enviar_a = (int)dbSelectPrtyDI[i].envio_sce;
                        InitObject.PRTGLOB.direc[i].recibe = (int)dbSelectPrtyDI[i].recibe_sce;
                        InitObject.PRTGLOB.direc[i].CasPostal = dbSelectPrtyDI[i].cas_postal.Replace("'", "`");
                        InitObject.PRTGLOB.direc[i].CasBanco = dbSelectPrtyDI[i].cas_banco.Replace("'", "`");
                        InitObject.PRTGLOB.direc[i].email = dbSelectPrtyDI[i].email.Replace("'", "`");
                        InitObject.PRTGLOB.direc[i].estado = T_PRTGLOB.leido;
                    }

                    Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;

                    //(moo) ejecutivos y  //(moo) oficina
                    try
                    {
                        Int16 codigoOficina;
                        // 
                        // InitObject.PrtEnt01.Link.Text = "162";   
                        if (InitObject.PRTGLOB.Party.tipo == 2)
                        {
                            InitObject.PrtEnt01.Link.Text = xgpyService.ConsultaOficinaPorRut(MODWS.ExtraeRut(InitObject.PARTY.OriginalIdParty));
                            // InitObject.PrtEnt01.Link.Text = "162";
                            //InitObject.PrtEnt07.oficina.Text = "162";
                            T_MODWS.MSJRET = InitObject.PrtEnt01.Link.Text;
                        }
                        else
                        {
                            //Int16 codigoOficina;
                            if (!Int16.TryParse(InitObject.PRTGLOB.Party.oficina, out codigoOficina))
                            {
                                codigoOficina = 0;
                                InitObject.PrtEnt01.Link.Text = codigoOficina.ToString();
                            }
                            InitObject.PrtEnt01.Link.Text = codigoOficina.ToString();
                            //InitObject.PrtEnt07.oficina.Text = codigoOficina.ToString(); 
                        }


                        //if (!Int16.TryParse(InitObject.PRTGLOB.Party.oficina, out codigoOficina))
                        //{
                        //    codigoOficina = 0;
                        //}
                        // codigoOficina = 162;
                        var dbSelectPrtyEjc = xgpyService.Sgt_Ejc_S04_MS(short.Parse(InitObject.PrtEnt01.Link.Text));
                        InitObject.PRTGLOB.ejecutivos = new tipo_riesgo[dbSelectPrtyEjc.Count];
                        for (int i = 0; i < dbSelectPrtyEjc.Count; i++)
                        {
                            InitObject.PRTGLOB.ejecutivos[i] = new tipo_riesgo();
                            InitObject.PRTGLOB.ejecutivos[i].codigo = Convert.ToString(dbSelectPrtyEjc[i].ejc_ejccod);
                            InitObject.PRTGLOB.ejecutivos[i].nombre = dbSelectPrtyEjc[i].ejc_ejcnom;
                        }
                    }
                    catch { }

                    Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;

                    //(moo) especialistas
                    var dbSelectPrtyEsp = xgpyService.Sgt_Ejc_S03_MS(T_PRTYENT.EJCOPIMP, T_PRTYENT.EJCOPEXP, T_PRTYENT.EJCNEGOC);
                    InitObject.PRTYENT.VEjc = new T_Especialista[dbSelectPrtyEsp.Count];
                    for (int i = 0; i < dbSelectPrtyEsp.Count; i++)
                    {
                        InitObject.PRTYENT.VEjc[i] = new T_Especialista();
                        InitObject.PRTYENT.VEjc[i].codofi = string.Format("{0:000}", dbSelectPrtyEsp[i].ejc_ejcofi); //Convert.ToString(dbSelectPrtyEsp[i].ejc_ejcofi); // Se agrega formato "000" a la izquierda, segun fix reportados
                        InitObject.PRTYENT.VEjc[i].codejc = string.Format("{0:000}", dbSelectPrtyEsp[i].ejc_ejccod); //Convert.ToString(dbSelectPrtyEsp[i].ejc_ejccod); // Se agrega formato "000" a la izquierda, segun fix reportados
                        InitObject.PRTYENT.VEjc[i].rut = dbSelectPrtyEsp[i].ejc_ejcrut;
                        InitObject.PRTYENT.VEjc[i].nombre = dbSelectPrtyEsp[i].ejc_ejcnom;
                        InitObject.PRTYENT.VEjc[i].tipo = dbSelectPrtyEsp[i].ejc_ejctpo;
                    }
                    dbSelectPrtyEsp = null;

                    Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;

                    //(moo) tasacom
                    try
                    {
                        var dbSelectTCom = xgpyService.Sce_Tcom_S04_MS(InitObject.PARTY.OriginalIdParty);
                        InitObject.PRTGLOB.tasacom = new prtytcom[dbSelectTCom.Count];
                        for (int i = 0; i < dbSelectTCom.Count; i++)
                        {
                            InitObject.PRTGLOB.tasacom[i] = new prtytcom();
                            InitObject.PRTGLOB.tasacom[i].sistema = dbSelectTCom[i].sistema;
                            InitObject.PRTGLOB.tasacom[i].producto = dbSelectTCom[i].producto;
                            InitObject.PRTGLOB.tasacom[i].etapa = dbSelectTCom[i].etapa;
                            InitObject.PRTGLOB.tasacom[i].estado = T_PRTGLOB.leido;
                            InitObject.PRTGLOB.tasacom[i].secuencia = (int)dbSelectTCom[i].secuencia;
                            InitObject.PRTGLOB.tasacom[i].manual = dbSelectTCom[i].manual_t ? 1 : 0;
                            InitObject.PRTGLOB.tasacom[i].mto_fijo = dbSelectTCom[i].monto_fijo ? 1 : 0;
                            InitObject.PRTGLOB.tasacom[i].tasa = (double)dbSelectTCom[i].tasa;
                            InitObject.PRTGLOB.tasacom[i].hasta = (double)dbSelectTCom[i].hasta_mon;
                            InitObject.PRTGLOB.tasacom[i].min = (double)dbSelectTCom[i].minimo;
                            InitObject.PRTGLOB.tasacom[i].max = (double)dbSelectTCom[i].maximo;
                            InitObject.PRTGLOB.tasacom[i].fecha = Convert.ToString(dbSelectTCom[i].fecha);
                        }
                        dbSelectTCom = null;

                        Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
                    }
                    catch { }

                    // tasagas
                    try
                    {
                        var dbSelectTGas = xgpyService.Sce_Tgas_S04_MS(InitObject.PARTY.OriginalIdParty);
                        InitObject.PRTGLOB.tasagas = new prtytgas[dbSelectTGas.Count];
                        for (int i = 0; i < dbSelectTGas.Count; i++)
                        {
                            InitObject.PRTGLOB.tasagas[i] = new prtytgas();
                            InitObject.PRTGLOB.tasagas[i].sistema = dbSelectTGas[i].sistema;
                            InitObject.PRTGLOB.tasagas[i].producto = dbSelectTGas[i].producto;
                            InitObject.PRTGLOB.tasagas[i].etapa = dbSelectTGas[i].etapa;
                            InitObject.PRTGLOB.tasagas[i].estado = T_PRTGLOB.leido;
                            InitObject.PRTGLOB.tasagas[i].tarifa = dbSelectTGas[i].m_tarifa ? 1 : 0;
                            InitObject.PRTGLOB.tasagas[i].monto = (double)dbSelectTGas[i].monto;
                        }
                        dbSelectTGas = null;

                        Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
                    }
                    catch { }

                    // tasaint
                    try
                    {
                        var dbSelectTInt = xgpyService.Sce_Tint_S01_MS(InitObject.PARTY.OriginalIdParty);
                        InitObject.PRTGLOB.tasaint = new prtytint[dbSelectTInt.Count];
                        for (int i = 0; i < dbSelectTInt.Count; i++)
                        {
                            InitObject.PRTGLOB.tasaint[i] = new prtytint();
                            InitObject.PRTGLOB.tasaint[i].sistema = dbSelectTInt[i].sistema;
                            InitObject.PRTGLOB.tasaint[i].producto = dbSelectTInt[i].producto;
                            InitObject.PRTGLOB.tasaint[i].etapa = dbSelectTInt[i].etapa;
                            InitObject.PRTGLOB.tasaint[i].estado = T_PRTGLOB.leido;
                            InitObject.PRTGLOB.tasaint[i].libor = dbSelectTInt[i].libor ? 1 : 0;
                            InitObject.PRTGLOB.tasaint[i].prime = dbSelectTInt[i].prime ? 1 : 0;
                            InitObject.PRTGLOB.tasaint[i].flotante = dbSelectTInt[i].flotante ? 1 : 0;
                            InitObject.PRTGLOB.tasaint[i].tasa = (double)dbSelectTInt[i].tasa;
                        }
                        dbSelectTInt = null;

                        Session[SessionKeys.AdminParty.PartySessionKey] = InitObject;
                    }
                    catch (Exception e)
                    {
                        tracer.TraceException("Alerta tasaint", e);
                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            AutoClose = true,
                            Text = "Problemas con Sce_Tint_S01_MS" + e.Message,
                            Title = "Advertencia",
                            Type = TipoMensaje.Informacion
                        });
                    }

                    /// Validar la existencia de las cuentas
                    var cuentasPorRut = xgpyService.ObtenerCuentasPorRut(MODWS.ExtraeRut(InitObject.PARTY.OriginalIdParty), ref mensajeWS);
                    if (cuentasPorRut == null)
                    {
                        InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            AutoClose = true,
                            Text = "Problemas con WS ConsultaProductoCliente: " + mensajeWS,
                            Title = "Advertencia",
                            Type = TipoMensaje.Informacion
                        });
                    }
                    else
                    {
                        InitObject.MODWS.CtaCCOL = new Cuentas[cuentasPorRut.Count()];
                        for (int i = 0; i < cuentasPorRut.Count(); i++)
                        {
                            InitObject.MODWS.CtaCCOL[i] = new Cuentas();
                            InitObject.MODWS.CtaCCOL[i].nrocta = cuentasPorRut[i].numProducto;
                            InitObject.MODWS.CtaCCOL[i].tipo = cuentasPorRut[i].codigoProducto;
                        }
                    }

                    //agregago logica botones

                    if ((InitObject.PRTGLOB.Party.creauser != MODGUSR.UsrEsp.Especialista || InitObject.PRTGLOB.Party.creacosto != MODGUSR.UsrEsp.CentroCosto) && MODGUSR.UsrEsp.Jerarquia == 0)
                    {
                        if (MODGUSR.UsrEsp.Tipeje == "O" && InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.tipo_banco || MODGUSR.UsrEsp.Tipeje == "O" && InitObject.PRTGLOB.Party.tipo == T_PRTGLOB.individuo)
                        {
                            InitObject.PRTGLOB.Party.PrtGlob.Pertenece = 0;
                        }

                    }

                    if (InitObject.PRTGLOB.Party.PrtGlob.Pertenece == 0)
                    {
                        InitObject.Mdi_Principal.Archivo[2].Enabled = false;     // menú salvar
                        InitObject.Mdi_Principal.Archivo[4].Enabled = false;     // menú eliminar
                        //InitObject.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = false;    // botón
                        InitObject.Mdi_Principal.BUTTONS["tbr_Activar"].Enabled = true;       // botón activar --Realsystems 03-09-2008 Migración de VB60 a VB30
                    }
                    else
                    {
                        InitObject.Mdi_Principal.Archivo[2].Enabled = true;       // menú salvar
                        InitObject.Mdi_Principal.Archivo[4].Enabled = true;       // menú eliminar
                        InitObject.Mdi_Principal.Archivo[3].Enabled = true;    // menú Activar   --Realsystems 03-09-2008 Migración de VB60 a VB30
                                                                               //InitObject.Mdi_Principal.BUTTONS["tbr_Grabar"].Enabled = true;      // botón
                        InitObject.Mdi_Principal.BUTTONS["tbr_Activar"].Enabled = true;     // botón activar  --Realsystems 03-09-2008 Migración de VB60 a VB30
                    }

                    //T_MODWS.ACCESO = "0";
                    if (UsuarioEspecialista().Jerarquia != 1 && UsuarioEspecialista().Tipeje != "N")
                    //if (T_MODWS.ACCESO == "0")
                    {
                        MODWS.VistaConsulta(InitObject);
                        if (InitObject.Mdi_Principal.BUTTONS["tbr_Cuentas"].Enabled == false)
                        {
                            InitObject.Mdi_Principal.Opciones[2].Enabled = true;
                        }
                        else
                        {
                            InitObject.Mdi_Principal.Opciones[2].Enabled = false;
                        }
                        if (InitObject.Mdi_Principal.BUTTONS["tbr_Caracteristicas"].Enabled == false)
                        {
                            InitObject.Mdi_Principal.Opciones[0].Enabled = true;
                        }
                        else
                        {
                            InitObject.Mdi_Principal.Opciones[0].Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta", ex);
                    retCarga = false;
                }
                return retCarga;
            }
        }

        /// <summary>
        /// Carga Participante en Sesión, si no existe, lo crea!
        /// </summary>
        /// <returns></returns>
        private Prty LoadPrtySession()
        {
            Prty oPrty = (Prty)Session[SessionKeys.AdminParty.PrtyKey];

            if (oPrty == null)
            {
                oPrty = new Prty();
                Session[SessionKeys.AdminParty.PrtyKey] = oPrty;
            }

            return oPrty;
        }

        /// <summary>
        /// Grabar Participate en Sesión
        /// </summary>
        /// <param name="oPrty"></param>
        private void SavePrtySession(Prty oPrty)
        {
            if (oPrty != null)
                Session[SessionKeys.AdminParty.PrtyKey] = oPrty;

        }

        private UserPrty LoadUserPrtySession()
        {
            UserPrty oUsrPrty = (UserPrty)Session[SessionKeys.AdminParty.UserPrtyKey];

            if (oUsrPrty == null)
            {
                oUsrPrty = new UserPrty();
                Session[SessionKeys.AdminParty.UserPrtyKey] = oUsrPrty;
            }

            return oUsrPrty;
        }

        private void SaveUserPrtySession(UserPrty oPrty)
        {
            if (oPrty != null)
                Session[SessionKeys.AdminParty.UserPrtyKey] = oPrty;
        }

        /// Metodos para Abrir Participante 
        ///
        /// Author:     Marco Antonio Orellana O. (marco.orellana@xemantics.com)
        ///             
        ///
        /// Fecha:      2015-10-23
        ///


        /// <summary>
        /// 
        /// </summary>
        /// <param name="LlaveIng"></param>
        /// <param name="modoApertura"> true: Para Abrir Participante - false : Para nuevo Participante</param>
        /// <returns></returns>
        public JsonResult AceptaParty(String LlaveIng, Boolean modo, String iObject)
        {
            XgpyResult xgpyResult = new XgpyResult();
            JObject o = JObject.Parse(iObject);
            InitializationObject initObject = o.ToObject<InitializationObject>();
            InitializationObject iO = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            int iNewIndex = 0;
            string TxtOut = "";
            bool encontrado = false;
            string[] LlaveBaja = null;


            string a = "";
            int s = 0;

            if (iO == null)
                iO = initObject;
            else
            {
                iO.PRTGLOB = initObject.PRTGLOB;
                iO.PRTYENT = initObject.PRTYENT;
            }

            LlaveBaja = new string[1];
            encontrado = false;

            if (String.IsNullOrEmpty(LlaveIng))
            {
                xgpyResult.ErrorCode = 10;
                xgpyResult.Message = "Debe ingresar una llave de identificación.";
                xgpyResult.iObject = iO;

                return Json(xgpyResult);
            }

            if (iO.PRTGLOB.Party.PrtGlob.EsCITI == 1)
                LlaveIng = FormatUtils.TryFormatearRutParticipante(iO.PRTGLOB.Party.PrtGlob.llave, false);
            else
                LlaveIng = FormatUtils.TryFormatearRutParticipante(LlaveIng, false);

            iO.PRTGLOB.Party.PrtGlob.cambio_a_acreedor = 0;
            iO.PRTGLOB.Party.PrtGlob.cambio_a_corresponsal = 0;

            if (modo)
            {
                if (PRTYENT.existesy(ref iO, xgpyService, LlaveIng, 0) != 0)
                    encontrado = true;
                else
                {
                    xgpyResult.ErrorCode = 11;
                    xgpyResult.Message = "El participante solicitado no se encuentra registrado.";
                    xgpyResult.iObject = iO;
                    return Json(xgpyResult);
                }
            }

            PRTYENT.limpia(ref iO);
            iO.PRTGLOB.Party.estado = T_PRTGLOB.leido;
            iO.PRTGLOB.Party.PrtGlob.ctas_eliminadas = 1;
            a = LlaveIng.PadRight(12, '|').ToUpper();

            PRTYENT.lee_infopartySy(ref iO, xgpyService, a, ref s);

            switch (s)
            {
                case 99:
                    xgpyResult.ErrorCode = 10;
                    xgpyResult.Message = "No existe participante";
                    xgpyResult.iObject = iO;

                    return Json(xgpyResult);

                case 100:
                    iO.PRTGLOB.Party.idparty = LlaveIng;
                    xgpyResult.ErrorCode = 100;
                    xgpyResult.Message = "Participante en proceso de Borrado.";
                    xgpyResult.iObject = iO;

                    return Json(xgpyResult);
            }

            if (s != 0)
            {
                xgpyResult.ErrorCode = 98;
                xgpyResult.Message = "Imposible Leer Participante.";
                xgpyResult.iObject = iO;
            }

            iO.PRTGLOB.Party.idparty = a;
            PRTYENT.escribe_llave(iO.PRTGLOB.Party.idparty);
            PRTYENT.lee_razSy(ref iO, xgpyService, iO.PRTGLOB.Party.idparty);
            PRTYENT.lee_dirSy(ref iO, xgpyService, iO.PRTGLOB.Party.idparty);
            PRTYENT.lee_cuentasSy_Inicializar(ref iO, xgpyService, iO.PRTGLOB.Party.idparty);
            PRTYENT.lee_tgasSy_Inicializar(ref iO, xgpyService, iO.PRTGLOB.Party.idparty);
            PRTYENT.lee_instrucSy_Inicializar(ref iO, xgpyService, iO.PRTGLOB.Party.idparty);
            this.xgpyService.CaracteristicasParticipante_llama0711(iO);
            PRTYENT.lee_ejecutivosSy_Initialize(ref iO, xgpyService, iO.PRTGLOB.Party.idparty);

            xgpyResult.ErrorCode = 0;
            xgpyResult.Message = "";
            xgpyResult.iObject = iO;

            Session[SessionKeys.AdminParty.PartySessionKey] = iO;
            return Json(xgpyResult);
        }

        /// <summary>
        /// Valida si el Rut del
        /// </summary>
        /// <param name="llaveSy"></param>
        /// <param name="modo"></param>
        /// <param name="llavePura"></param>
        /// <returns></returns>
        public JsonResult existesy(String llaveSy, Boolean modo, String llavePura)
        {
            using (var tracer = new Tracer("AdminParticipantes - existesy"))
            {
                XgpyResult xgpyResult = new XgpyResult()
                {
                    ErrorCode = 0,
                    Message = string.Empty
                };

                T_PRTGLOB globResult = new T_PRTGLOB()
                {
                    nom = new prtynombre[1]
                };

                var partyList = xgpyService.Sce_Prty_S07_MS(llaveSy);

                if (partyList != null)
                {
                    if (partyList.Count > 0)
                    {
                        globResult.Party.estado = 120;
                        globResult.Party.riesgo = "El participante ya existe en la base de datos";
                    }
                    else
                    {
                        /// Pasamos a traer la información del web service para traer el RUT.
                        var dataRUT = xgpyService.ConsultaRazonSocialPorRut(llavePura, ref globResult.Party.riesgo);

                        /// Inicializamos el nombre
                        globResult.nom[0] = new prtynombre();

                        if (dataRUT != null)
                        {
                            globResult.nom[0].nombre = dataRUT;
                            globResult.Party.estado = 789;
                        }
                        else
                        {
                            if (!globResult.Party.riesgo.Contains("Functional"))
                                globResult.Party.Mensajes.Add("Problemas con Web Services Consulta Razón Social Por Rut: " + globResult.Party.riesgo);
                            else
                                globResult.Party.Mensajes.Add("Información del Servicio: " + globResult.Party.riesgo);
                        }

                        /// Obtenermos las cuenas relacionadas al rut
                        var dataCuentas = xgpyService.ObtenerCuentasPorRut(llavePura, ref globResult.Party.riesgo);

                        if (dataCuentas != null)
                        {
                            globResult.ctaclie = new prtyccta[dataCuentas.Count];

                            for (int i = 0; i < dataCuentas.Count; i++)
                            {
                                globResult.ctaclie[i] = new prtyccta();
                                globResult.ctaclie[i].cuenta = dataCuentas[i].numProducto;
                                globResult.ctaclie[i].moneda = dataCuentas[i].codigoProducto;
                            }
                        }
                        else
                        {
                            if (!globResult.Party.riesgo.Contains("Functional"))
                                globResult.Party.Mensajes.Add("Problemas con Web Services Obtener Cuentas Por Rut: " + globResult.Party.riesgo);
                            else
                                globResult.Party.Mensajes.Add("Información del Servicio: " + globResult.Party.riesgo);
                        }
                    }
                }
                else
                {
                    globResult.Party.estado = 10032;
                    globResult.Party.riesgo = "Error en lectura de participantes en la base de datos.";
                }

                return Json(globResult, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult lee_ejecutivosSy(String codigo, String iObject)
        {
            String oficinaPrty;
            JObject o = JObject.Parse(iObject);
            InitializationObject initObject = o.ToObject<InitializationObject>();
            InitializationObject iO = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            iO.PRTGLOB = initObject.PRTGLOB;
            iO.PRTYENT = initObject.PRTYENT;

            try
            {
                Int16 codigoOficina;
                if (iO.PRTGLOB.Party.tipo == 2)
                    oficinaPrty = xgpyService.ConsultaOficinaPorRut(iO.PRTGLOB.Party.idparty);
                else
                {
                    if (!Int16.TryParse(iO.PRTGLOB.Party.oficina, out codigoOficina))
                    {
                        codigoOficina = 0;
                        oficinaPrty = Convert.ToString(codigoOficina);
                    }
                }

                if (!Int16.TryParse(iO.PRTGLOB.Party.oficina, out codigoOficina))
                    codigoOficina = 0;

                var dbSelectPrtyEjc = xgpyService.Sgt_Ejc_S04_MS(codigoOficina);
                iO.PRTGLOB.ejecutivos = new tipo_riesgo[dbSelectPrtyEjc.Count];

                for (int i = 0; i < dbSelectPrtyEjc.Count; i++)
                {
                    iO.PRTGLOB.ejecutivos[i] = new tipo_riesgo();
                    iO.PRTGLOB.ejecutivos[i].codigo = Convert.ToString(dbSelectPrtyEjc[i].ejc_ejccod);
                    iO.PRTGLOB.ejecutivos[i].nombre = dbSelectPrtyEjc[i].ejc_ejcnom;
                }

                dbSelectPrtyEjc = null;
            }
            catch { }

            Session[SessionKeys.AdminParty.PartySessionKey] = iO;
            return Json(iO);
        }

        public JsonResult Sygetn_Ejecutivos(String iObject)
        {
            JObject o = JObject.Parse(iObject);
            InitializationObject initObject = o.ToObject<InitializationObject>();
            InitializationObject iO = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            iO.PRTGLOB = initObject.PRTGLOB;
            iO.PRTYENT = initObject.PRTYENT;

            try
            {
                var dbSelectPrtyEsp = xgpyService.Sgt_Ejc_S03_MS(T_PRTYENT.EJCOPEXP, T_PRTYENT.EJCOPIMP, T_PRTYENT.EJCNEGOC);
                iO.PRTYENT.VEjc = new T_Especialista[dbSelectPrtyEsp.Count];

                for (int i = 0; i < dbSelectPrtyEsp.Count; i++)
                {
                    iO.PRTYENT.VEjc[i] = new T_Especialista();
                    iO.PRTYENT.VEjc[i].codofi = Convert.ToString(dbSelectPrtyEsp[i].ejc_ejcofi);
                    iO.PRTYENT.VEjc[i].codejc = Convert.ToString(dbSelectPrtyEsp[i].ejc_ejccod);
                    iO.PRTYENT.VEjc[i].rut = dbSelectPrtyEsp[i].ejc_ejcrut;
                    iO.PRTYENT.VEjc[i].nombre = dbSelectPrtyEsp[i].ejc_ejcnom;
                    iO.PRTYENT.VEjc[i].tipo = dbSelectPrtyEsp[i].ejc_ejctpo;
                }

                dbSelectPrtyEsp = null;
                Session[SessionKeys.AdminParty.PartySessionKey] = iO;
                return Json(iO);
            }
            catch { }

            return Json(null);
        }

        public JsonResult lee_tcomSy(String llaveIdPrty)
        {
            InitializationObject iO = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (iO.PRTGLOB.tasacom != null)
            {
                if (iO.PRTGLOB.tasacom.Count() > 0)
                {
                    if (iO.PRTGLOB.tasacom[0] != null)
                    {
                        if (!String.IsNullOrEmpty(iO.PRTGLOB.tasacom[0].sistema))
                            return Json(iO.PRTGLOB.tasacom);
                    }
                }
            }

            try
            {
                var dbSelectTCom = xgpyService.Sce_Tcom_S04_MS(llaveIdPrty);
                InitObject.PRTGLOB.tasacom = new prtytcom[dbSelectTCom.Count];
                for (int i = 0; i < dbSelectTCom.Count; i++)
                {
                    iO.PRTGLOB.tasacom[i] = new prtytcom();
                    iO.PRTGLOB.tasacom[i].sistema = dbSelectTCom[i].sistema;
                    iO.PRTGLOB.tasacom[i].producto = dbSelectTCom[i].producto;
                    iO.PRTGLOB.tasacom[i].etapa = dbSelectTCom[i].etapa;
                    iO.PRTGLOB.tasacom[i].estado = T_PRTGLOB.leido;
                    iO.PRTGLOB.tasacom[i].secuencia = (int)dbSelectTCom[i].secuencia;
                    iO.PRTGLOB.tasacom[i].manual = dbSelectTCom[i].manual_t ? 1 : 0;
                    iO.PRTGLOB.tasacom[i].mto_fijo = dbSelectTCom[i].monto_fijo ? 1 : 0;
                    iO.PRTGLOB.tasacom[i].tasa = (double)dbSelectTCom[i].tasa;
                    iO.PRTGLOB.tasacom[i].hasta = (double)dbSelectTCom[i].hasta_mon;
                    iO.PRTGLOB.tasacom[i].min = (double)dbSelectTCom[i].minimo;
                    iO.PRTGLOB.tasacom[i].max = (double)dbSelectTCom[i].maximo;
                    iO.PRTGLOB.tasacom[i].fecha = Convert.ToString(dbSelectTCom[i].fecha);
                }
                dbSelectTCom = null;
                Session[SessionKeys.AdminParty.PartySessionKey] = iO;
            }
            catch { }
            return Json(iO.PRTGLOB.tasacom);
        }

        public JsonResult lee_tgasSy(String llaveIdPrty)
        {
            InitializationObject iO = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (iO.PRTGLOB.tasagas != null)
            {
                if (iO.PRTGLOB.tasagas.Count() > 0)
                {
                    if (iO.PRTGLOB.tasagas[0] != null)
                    {
                        if (!String.IsNullOrEmpty(iO.PRTGLOB.tasagas[0].sistema))
                        {
                            return Json(iO.PRTGLOB.tasagas);
                        }
                    }
                }
            }

            try
            {
                var dbSelectTGas = xgpyService.Sce_Tgas_S04_MS(llaveIdPrty);
                InitObject.PRTGLOB.tasagas = new prtytgas[dbSelectTGas.Count];
                for (int i = 0; i < dbSelectTGas.Count; i++)
                {
                    iO.PRTGLOB.tasagas[i] = new prtytgas();
                    iO.PRTGLOB.tasagas[i].sistema = dbSelectTGas[i].sistema;
                    iO.PRTGLOB.tasagas[i].producto = dbSelectTGas[i].producto;
                    iO.PRTGLOB.tasagas[i].etapa = dbSelectTGas[i].etapa;
                    iO.PRTGLOB.tasagas[i].estado = T_PRTGLOB.leido;
                    iO.PRTGLOB.tasagas[i].tarifa = dbSelectTGas[i].m_tarifa ? 1 : 0;
                    iO.PRTGLOB.tasagas[i].monto = (double)dbSelectTGas[i].monto;
                }
                dbSelectTGas = null;

                Session[SessionKeys.AdminParty.PartySessionKey] = iO;
            }
            catch { }

            return Json(iO.PRTGLOB.tasagas);
        }

        public JsonResult lee_tintSy(String llaveIdPrty)
        {
            InitializationObject iO = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            if (iO.PRTGLOB.tasaint != null)
            {
                if (iO.PRTGLOB.tasaint.Count() > 0)
                {
                    if (iO.PRTGLOB.tasaint[0] != null)
                    {
                        if (!String.IsNullOrEmpty(iO.PRTGLOB.tasaint[0].sistema))
                            return Json(iO.PRTGLOB.tasaint);
                    }
                }
            }

            try
            {
                var dbSelectTInt = xgpyService.Sce_Tint_S01_MS(llaveIdPrty);
                InitObject.PRTGLOB.tasaint = new prtytint[dbSelectTInt.Count];

                for (int i = 0; i < dbSelectTInt.Count; i++)
                {
                    iO.PRTGLOB.tasaint[i] = new prtytint();
                    iO.PRTGLOB.tasaint[i].sistema = dbSelectTInt[i].sistema;
                    iO.PRTGLOB.tasaint[i].producto = dbSelectTInt[i].producto;
                    iO.PRTGLOB.tasaint[i].etapa = dbSelectTInt[i].etapa;
                    iO.PRTGLOB.tasaint[i].estado = T_PRTGLOB.leido;
                    iO.PRTGLOB.tasaint[i].libor = dbSelectTInt[i].libor ? 1 : 0;
                    iO.PRTGLOB.tasaint[i].prime = dbSelectTInt[i].prime ? 1 : 0;
                    iO.PRTGLOB.tasaint[i].flotante = dbSelectTInt[i].flotante ? 1 : 0;
                    iO.PRTGLOB.tasaint[i].tasa = (double)dbSelectTInt[i].tasa;
                }

                dbSelectTInt = null;
                Session[SessionKeys.AdminParty.PartySessionKey] = iO;
            }
            catch { }

            return Json(iO.PRTGLOB.tasaint);
        }

        /// Metodos para Grabar Participante 
        ///
        /// Author:     Pedro Cesped
        ///             Marco Antonio Orellana O. (marco.orellana@xemantics.com)
        ///
        /// Fecha:      2015-10-21
        ///

        public JsonResult SalvaInfopartySy(String llaveIdPrty, Int32 estadoPrty, String iO)
        {
            bool borrar = false;
            byte tipo_party; ;
            string KeyPrty;
            JObject o = JObject.Parse(iO);
            InitializationObject iObject = o.ToObject<InitializationObject>();
            XgpyResult xgpyResult = new XgpyResult();

            switch (estadoPrty)
            {
                case T_PRTGLOB.nuevo:
                    borrar = false;
                    break;

                case T_PRTGLOB.modificado:
                    borrar = false;
                    break;

                case T_PRTGLOB.eliminado_leido:
                case T_PRTGLOB.eliminado_modificado:
                    borrar = true;
                    break;
            }

            if (String.IsNullOrEmpty(iObject.PRTGLOB.Party.creacosto) || String.IsNullOrEmpty(iObject.PRTGLOB.Party.creauser))
            {
                iObject.PRTGLOB.Party.creacosto = this.LoadUserPrtySession().centroCosto;
                iObject.PRTGLOB.Party.creauser = this.LoadUserPrtySession().especialista;
            }

            iObject.PRTGLOB.Party.modcosto = this.LoadUserPrtySession().centroCosto;
            iObject.PRTGLOB.Party.moduser = this.LoadUserPrtySession().especialista;

            KeyPrty = iObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(iObject.PRTGLOB.Party.Bnumber) ? iObject.PRTGLOB.Party.Bnumber : llaveIdPrty;

            if (iObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(iObject.PRTGLOB.Party.Bnumber))
                KeyPrty = iObject.PRTGLOB.Party.Bnumber;
            else
                KeyPrty = llaveIdPrty;


            if (iObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(iObject.PRTGLOB.Party.Bnumber))
                tipo_party = 3;
            else
                tipo_party = Convert.ToByte(iObject.PRTGLOB.Party.tipo);

            var result_w01 = xgpyService.sce_prty_w01_MS(
                KeyPrty,
                borrar,
                tipo_party,
                Convert.ToInt16(iObject.PRTGLOB.Party.Flag),
                Convert.ToByte(iObject.PRTGLOB.Party.clasificacion),
                Convert.ToBoolean(iObject.PRTGLOB.Party.sirut),
                iObject.PRTGLOB.Party.rut,
                iObject.PRTGLOB.Party.creacosto,
                iObject.PRTGLOB.Party.creauser,
                iObject.PRTGLOB.Party.modcosto,
                iObject.PRTGLOB.Party.moduser,
                Convert.ToBoolean(iObject.PRTGLOB.Party.multiple),
                iObject.PRTGLOB.Party.oficina,
                iObject.PRTGLOB.Party.ejecutivo,
                iObject.PRTGLOB.Party.actividad,
                iObject.PRTGLOB.Party.riesgo,
                Convert.ToInt16(iObject.PRTGLOB.Party.codbco),
                Convert.ToBoolean(iObject.PRTGLOB.Party.libor),
                Convert.ToBoolean(iObject.PRTGLOB.Party.prime),
                Convert.ToSingle(iObject.PRTGLOB.Party.spread),
                iObject.PRTGLOB.Party.swif,
                Convert.ToInt32(iObject.PRTGLOB.Party.aladi),
                iObject.PRTGLOB.Party.ejecorr,
                Convert.ToInt16(iObject.PRTGLOB.Party.flagins),
                iObject.PRTGLOB.Party.insgen_imp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.insgen_imp), //  iObject.PRTGLOB.Party.insgen_imp == "" ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.insgen_imp),
                iObject.PRTGLOB.Party.insgen_exp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.insgen_exp),
                iObject.PRTGLOB.Party.insgen_ser == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.insgen_ser),
                iObject.PRTGLOB.Party.inscob_imp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.inscob_imp),
                iObject.PRTGLOB.Party.insgen_exp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.inscob_exp),
                iObject.PRTGLOB.Party.inscre_imp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.inscre_imp),
                iObject.PRTGLOB.Party.inscre_exp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.inscre_exp),
                "");

            if (result_w01 == "OK")
            {
                xgpyResult.ErrorCode = 0;
                xgpyResult.Message = "";
                xgpyResult.iObject = iObject;
            }
            else
            {
                xgpyResult.ErrorCode = 11000;
                xgpyResult.Message = "Error al grabar Información del Participante [" + result_w01 + "]";
            }

            // (moo) 2016-01-11 | Correción, responder mensaje de Exito al Grabar
            //Session[SessionKeys.AdminParty.PartySessionKey] = iObject;

            return Json(xgpyResult);
        }

        public JsonResult SalvaRelCltEjc(String _iObject)
        {
            string result_w01 = "";
            XgpyResult xgpyResult = new XgpyResult();
            JObject o = JObject.Parse(_iObject);
            InitializationObject initObject = o.ToObject<InitializationObject>();
            InitializationObject iO = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            //if (iO == null)
            //{
            //    iO = initObject;
            //}
            //else
            //{
            //    iO.PRTGLOB = initObject.PRTGLOB;
            //    iO.PRTYENT = initObject.PRTYENT;
            //}

            var iObject = iO;

            // (moo) 2016-01-11 | Correción, responder mensaje de Exito/Error al Grabar
            // por defecto siempre retorna exito, porque no siempre se actualiza esta información
            xgpyResult.ErrorCode = 0;
            xgpyResult.Message = "";
            xgpyResult.iObject = iObject;

            for (int i = 0; i < iObject.PRTYENT2.VSGTCliEsp.Count(); i++)
            {
                if (iObject.PRTYENT2.VSGTCliEsp[i] != null)
                {
                    if (iObject.PRTYENT2.VSGTCliEsp[i].tipo == T_PRTYENT2.SGT_tipnegoc)
                    {
                        result_w01 = xgpyService.sce_netd_ejc_clt_w01_MS(
                            initObject.PRTGLOB.Party.rut,
                            Convert.ToDecimal(iObject.PRTYENT2.VSGTCliEsp[i].codeje),
                            Convert.ToDecimal(iObject.PRTYENT2.VSGTCliEsp[i].ofieje));

                        if (result_w01 == "OK")
                        {
                            xgpyResult.ErrorCode = 0;
                            xgpyResult.Message = "";
                            xgpyResult.iObject = iObject;
                        }
                        else
                        {
                            xgpyResult.ErrorCode = 11000;
                            xgpyResult.Message = "Error al grabar Relación Participante/Ejectutivo [" + result_w01 + "]";
                            xgpyResult.iObject = iObject;
                        }

                        break;
                    }
                }
            }

            return Json(xgpyResult);
        }

        public JsonResult SalvaRazonesSy(String llaveIdPrty, String _iObject)
        {
            int borrar = 0;
            int nomCount = 0;
            string KeyPrty = "";
            XgpyResult xgpyResult = new XgpyResult();
            JObject o = JObject.Parse(_iObject);
            InitializationObject initObject = o.ToObject<InitializationObject>();
            InitializationObject iO = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            //if (iO == null)
            //{
            //    iO = initObject;
            //}
            //else
            //{
            //    iO.PRTGLOB = initObject.PRTGLOB;
            //    iO.PRTYENT = initObject.PRTYENT;
            //}

            //var iObject = iO;

            nomCount = -1;
            nomCount = initObject.PRTGLOB.nom.Count();

            if (nomCount == 0)
            {
                // (moo) 2016-01-11 | El sin información para almacenar, retorna exito, para poder seguir la cadena de funciones grabar participante
                xgpyResult.ErrorCode = 0;
                xgpyResult.Message = "Nada que salvar";
                return Json(xgpyResult);
            }

            for (int i = 0; i < nomCount; i++)
            {
                KeyPrty = initObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(initObject.PRTGLOB.Party.Bnumber) ? initObject.PRTGLOB.Party.Bnumber : llaveIdPrty;

                switch (initObject.PRTGLOB.nom[i].estado)
                {
                    case T_PRTGLOB.nuevo:
                        initObject.PRTGLOB.nom[i].indice = i == 0 ? initObject.PRTGLOB.nom[i].indice : i;
                        var resultValue_i01 = xgpyService.sce_rsa_i01_MS(
                            KeyPrty,
                            initObject.PRTGLOB.nom[i].indice,
                            initObject.PRTGLOB.nom[i].nombre,
                            initObject.PRTGLOB.nom[i].fantasia,
                            initObject.PRTGLOB.nom[i].contacto,
                            initObject.PRTGLOB.nom[i].sortkey,
                            this.LoadUserPrtySession().centroCosto,
                            this.LoadUserPrtySession().especialista);

                        if (resultValue_i01 <= 0)
                        {
                            xgpyResult.ErrorCode = 11002;
                            xgpyResult.Message = "Error al insertar razón social " + i;
                            return Json(xgpyResult);
                        }

                        // (moo) 2016-01-11 | cambiamos el estado despues de grabar
                        initObject.PRTGLOB.nom[i].estado = T_PRTGLOB.leido;

                        if (initObject.PRTGLOB.nom[i].borrado == "1")
                        {
                            var resultValue_u02 = xgpyService.sce_rsa_u02_MS(KeyPrty, initObject.PRTGLOB.nom[i].indice);

                            if (resultValue_u02 <= 0)
                            {
                                xgpyResult.ErrorCode = 11003;
                                xgpyResult.Message = "Error al modificar razón social " + i;
                                return Json(xgpyResult);
                            }
                        }
                        break;

                    case T_PRTGLOB.modificado:
                        borrar = ((false) ? -1 : 0);

                        var resultValue_u01 = xgpyService.sce_rsa_u01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.nom[i].indice,
                            int.Parse(initObject.PRTGLOB.nom[i].borrado),
                            initObject.PRTGLOB.nom[i].nombre,
                            initObject.PRTGLOB.nom[i].fantasia,
                            initObject.PRTGLOB.nom[i].contacto,
                            initObject.PRTGLOB.nom[i].sortkey);

                        if (resultValue_u01 <= 0)
                        {
                            xgpyResult.ErrorCode = 11003;
                            xgpyResult.Message = "Error al modificar razón social " + i;
                            return Json(xgpyResult);
                        }

                        // (moo) 2016-01-11 | cambiamos el estado despues de grabar
                        initObject.PRTGLOB.nom[i].estado = T_PRTGLOB.leido;

                        //if (iObject.PRTGLOB.nom[i].borrado == "1")
                        //{
                        //    var resultValue_u02 = xgpyService.sce_rsa_u02_MS(KeyPrty, iObject.PRTGLOB.nom[i].indice);
                        //    if (resultValue_u02 <= 0)
                        //    {
                        //        xgpyResult.ErrorCode = 11003;
                        //        xgpyResult.Message = "Error al modificar razón social " + i;
                        //        return Json(xgpyResult);
                        //    }
                        //}
                        break;

                    case T_PRTGLOB.eliminado_leido:
                    case T_PRTGLOB.eliminado_modificado:
                        borrar = ((true) ? -1 : 0);

                        var resultValue_u01_1 = xgpyService.sce_rsa_u01_MS(llaveIdPrty,
                            initObject.PRTGLOB.nom[i].indice,
                            borrar,
                            initObject.PRTGLOB.nom[i].nombre,
                            initObject.PRTGLOB.nom[i].fantasia,
                            initObject.PRTGLOB.nom[i].contacto,
                            initObject.PRTGLOB.nom[i].sortkey);

                        if (resultValue_u01_1 <= 0)
                        {
                            xgpyResult.ErrorCode = 11003;
                            xgpyResult.Message = "Error al modificar razón social " + i;
                            return Json(xgpyResult);
                        }

                        // (moo) 2016-01-11 | cambiamos el estado despues de grabar
                        initObject.PRTGLOB.nom[i].estado = T_PRTGLOB.leido;

                        if (initObject.PRTGLOB.nom[i].borrado == "1")
                        {
                            try
                            {
                                xgpyService.sce_rsa_u02_MS(KeyPrty, initObject.PRTGLOB.nom[i].indice);
                            }
                            catch (Exception ex)
                            {
                                xgpyResult.ErrorCode = 11003;
                                xgpyResult.Message = "Error al modificar razón social " + i + " " + ex.Message;
                                return Json(xgpyResult);
                            }
                        }

                        break;
                }
            }

            xgpyResult.ErrorCode = 0;
            xgpyResult.Message = "";
            xgpyResult.iObject = initObject;
            return Json(xgpyResult);
        }

        public JsonResult SalvaDireccionSy(String llaveIdPrty, String _iObject)
        {
            using (var tracer = new Tracer("SalvaDireccionSy"))
            {
                int direcCount = 0;
                string keyIdPrty = string.Empty;

                XgpyResult xgpyResult = new XgpyResult()
                {
                    ErrorCode = 0,
                    Message = ""
                };

                JObject o = JObject.Parse(_iObject);
                InitializationObject iObject = o.ToObject<InitializationObject>();
                InitializationObject iO = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

                var initObject = iO;

                direcCount = iObject.PRTGLOB.direc.Count();

                if (direcCount == 0)
                {
                    xgpyResult.ErrorCode = 0;
                    xgpyResult.Message = "Nada que salvar";
                    return Json(xgpyResult);
                }

                for (int i = 0; i < direcCount; i++)
                {
                    switch (iObject.PRTGLOB.direc[i].estado)
                    {
                        case T_PRTGLOB.nuevo:
                            iObject.PRTGLOB.direc[i].indice = i == 0 ? iObject.PRTGLOB.direc[i].indice : i;
                            keyIdPrty = iObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(iObject.PRTGLOB.Party.Bnumber) ? iObject.PRTGLOB.Party.Bnumber : llaveIdPrty;

                            var result_i01 = xgpyService.sce_dad_i01_MS(
                                keyIdPrty,
                                iObject.PRTGLOB.direc[i].indice,
                                false,
                                iObject.PRTGLOB.direc[i].direccion,
                                iObject.PRTGLOB.direc[i].comuna,
                                iObject.PRTGLOB.direc[i].CodComuna,
                                iObject.PRTGLOB.direc[i].codpostal,
                                iObject.PRTGLOB.direc[i].region,
                                iObject.PRTGLOB.direc[i].ciudad,
                                iObject.PRTGLOB.direc[i].pais,
                                iObject.PRTGLOB.direc[i].CodPais,
                                iObject.PRTGLOB.direc[i].telefono,
                                iObject.PRTGLOB.direc[i].fax,
                                iObject.PRTGLOB.direc[i].telex,
                                iObject.PRTGLOB.direc[i].enviar_a,
                                iObject.PRTGLOB.direc[i].recibe,
                                iObject.PRTGLOB.direc[i].CasPostal,
                                iObject.PRTGLOB.direc[i].CasBanco,
                                this.LoadUserPrtySession().centroCosto,
                                this.LoadUserPrtySession().especialista,
                                iObject.PRTGLOB.direc[i].email);

                            if (result_i01 != "OK")
                            {
                                xgpyResult.ErrorCode = 11002;
                                xgpyResult.Message = "Error al insertar dirección " + i + " [" + result_i01 + "]";
                                xgpyResult.iObject = initObject;
                                return Json(xgpyResult);
                            }

                            // (moo) 2016-01-12 | cambiamos el estado despues de grabar
                            iObject.PRTGLOB.direc[i].estado = T_PRTGLOB.leido;
                            break;

                        case T_PRTGLOB.modificado:
                            var result_u02 = xgpyService.sce_dad_u02_MS(
                                llaveIdPrty,
                                iObject.PRTGLOB.direc[i].indice,
                                false,
                                iObject.PRTGLOB.direc[i].direccion,
                                iObject.PRTGLOB.direc[i].comuna,
                                iObject.PRTGLOB.direc[i].CodComuna,
                                iObject.PRTGLOB.direc[i].codpostal,
                                iObject.PRTGLOB.direc[i].region,
                                iObject.PRTGLOB.direc[i].ciudad,
                                iObject.PRTGLOB.direc[i].pais,
                                iObject.PRTGLOB.direc[i].CodPais,
                                iObject.PRTGLOB.direc[i].telefono,
                                iObject.PRTGLOB.direc[i].fax,
                                iObject.PRTGLOB.direc[i].telex,
                                iObject.PRTGLOB.direc[i].enviar_a,
                                iObject.PRTGLOB.direc[i].recibe,
                                iObject.PRTGLOB.direc[i].CasPostal,
                                iObject.PRTGLOB.direc[i].CasBanco,
                                iObject.PRTGLOB.direc[i].email);

                            if (result_u02 != "OK")
                            {
                                xgpyResult.ErrorCode = 11003;
                                xgpyResult.Message = "Error al modificar dirección " + i + " [" + result_u02 + "]";
                                xgpyResult.iObject = iObject;
                                return Json(xgpyResult);
                            }

                            // (moo) 2016-01-12 | cambiamos el estado despues de grabar
                            iObject.PRTGLOB.direc[i].estado = T_PRTGLOB.leido;
                            break;

                        case T_PRTGLOB.eliminado_leido:
                        case T_PRTGLOB.eliminado_modificado:
                            var result_u03 = xgpyService.sce_dad_u03_MS(llaveIdPrty, Convert.ToByte(iObject.PRTGLOB.direc[i].indice));

                            if (result_u03 != "OK")
                            {
                                xgpyResult.ErrorCode = 11004;
                                xgpyResult.Message = "Error al modificar dirección " + i + " [" + result_u03 + "]";
                                xgpyResult.iObject = iObject;
                                return Json(xgpyResult);
                            }

                            // (moo) 2016-01-12 | cambiamos el estado despues de grabar
                            iObject.PRTGLOB.direc[i].estado = T_PRTGLOB.leido;
                            break;
                    }
                }

                //GVT 16/03/2016 : Agregado para guardar las cuentas
                int ctaCliCount = 0;
                ctaCliCount = initObject.PRTGLOB.ctaclie.Count();
                if (ctaCliCount > 0)
                {
                    for (int i = 0; i < ctaCliCount; i++)
                    {
                        if (initObject.PRTGLOB.ctaclie[i] == null)
                            continue;

                        switch (initObject.PRTGLOB.ctaclie[i].estado)
                        {
                            case T_PRTGLOB.nuevo:

                                string id_Prty = string.Empty;
                                if (initObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(initObject.PRTGLOB.Party.Bnumber))
                                {
                                    id_Prty = initObject.PRTGLOB.Party.Bnumber;
                                }
                                else
                                {
                                    id_Prty = llaveIdPrty;
                                }

                                tracer.AddToContext("Sce_Ctas_I01_MS", "Nuevo");
                                tracer.AddToContext("id_Prty", id_Prty);
                                tracer.AddToContext("moneda", Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()));
                                tracer.AddToContext("CuentaSinFormato", initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                var resValue_i01 = xgpyService.Sce_Ctas_I01_MS(
                                    id_Prty,
                                    initObject.PRTGLOB.ctaclie[i].indice,
                                    false,
                                    initObject.PRTGLOB.ctaclie[i].activabco == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].activace == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].extranjera == 0 ? false : true,
                                    Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()),
                                    initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                if (resValue_i01 < 0)
                                {
                                    xgpyResult.ErrorCode = 11003;
                                    xgpyResult.Message = "Error al modificar cuenta en el participante -  Cuenta : " + initObject.PRTGLOB.ctaclie[i].CuentaSinFormato;
                                    tracer.TraceError("Alerta", xgpyResult.Message);
                                    return Json(xgpyResult);
                                }

                                break;

                            case T_PRTGLOB.modificado:

                                tracer.AddToContext("Sce_Ctas_U01_MS", "Modificado");
                                tracer.AddToContext("id_Prty", llaveIdPrty);
                                tracer.AddToContext("moneda", Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()));
                                tracer.AddToContext("CuentaSinFormato", initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                var resValue_u01 = xgpyService.Sce_Ctas_U01_MS(
                                    llaveIdPrty,
                                    initObject.PRTGLOB.ctaclie[i].indice,
                                    false,
                                    initObject.PRTGLOB.ctaclie[i].activabco == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].activace == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].extranjera == 0 ? false : true,
                                    Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()),
                                    initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                if (resValue_u01 < 0)
                                {
                                    xgpyResult.ErrorCode = 11003;
                                    xgpyResult.Message = "Error al modificar cuenta en el participante -  Cuenta : " + initObject.PRTGLOB.ctaclie[i].CuentaSinFormato;
                                    tracer.TraceError("Alerta", xgpyResult.Message);
                                    return Json(xgpyResult);
                                }
                                break;

                            case T_PRTGLOB.eliminado_leido:
                            case T_PRTGLOB.eliminado_modificado:

                                tracer.AddToContext("Sce_Ctas_U01_MS", "Eliminado/Modificado");
                                tracer.AddToContext("id_Prty", llaveIdPrty);
                                tracer.AddToContext("moneda", Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()));
                                tracer.AddToContext("CuentaSinFormato", initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                var resValue_u01_1 = xgpyService.Sce_Ctas_U01_MS(
                                    llaveIdPrty,
                                    initObject.PRTGLOB.ctaclie[i].indice,
                                    true,
                                    initObject.PRTGLOB.ctaclie[i].activabco == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].activace == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].extranjera == 0 ? false : true,
                                    Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()),
                                    initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                if (resValue_u01_1 < 0)
                                {
                                    xgpyResult.ErrorCode = 11004;
                                    xgpyResult.Message = "Error al eliminar cuenta en el participante - Cuenta : " + initObject.PRTGLOB.ctaclie[i].CuentaSinFormato;
                                    tracer.TraceError("Alerta", xgpyResult.Message);
                                    return Json(xgpyResult);
                                }
                                break;
                        }
                    }
                }

                //GVT 16/03/2016 : Agregado para guardar las tasas            
                int tasaGasCount = initObject.PRTGLOB.tasagas.Count();
                if (tasaGasCount > 0)
                {
                    for (int i = 0; i < tasaGasCount; i++)
                    {
                        if (initObject.PRTGLOB.tasagas[i] == null)
                            continue;

                        switch (initObject.PRTGLOB.tasagas[i].estado)
                        {
                            case T_PRTGLOB.nuevo:

                                var resValue_i01 = xgpyService.Sce_Tgas_I01_MS(
                                    llaveIdPrty,
                                    initObject.PRTGLOB.tasagas[i].sistema,
                                    initObject.PRTGLOB.tasagas[i].producto,
                                    initObject.PRTGLOB.tasagas[i].etapa,
                                    false,
                                    initObject.PRTGLOB.tasagas[i].tarifa == 0 ? false : true,
                                    Convert.ToDecimal(initObject.PRTGLOB.tasagas[i].monto));

                                if (resValue_i01 < 0)
                                {
                                    xgpyResult.ErrorCode = 11002;
                                    xgpyResult.Message = "Error al ingresar gasto monto:" + initObject.PRTGLOB.tasagas[i].monto.ToString();
                                    tracer.TraceError("Alerta", xgpyResult.Message);
                                    return Json(xgpyResult);
                                }
                                break;

                            case T_PRTGLOB.modificado:

                                var resValue_u02 = xgpyService.Sce_Tgas_U02_MS(
                                    llaveIdPrty,
                                    initObject.PRTGLOB.tasagas[i].sistema,
                                    initObject.PRTGLOB.tasagas[i].producto,
                                    initObject.PRTGLOB.tasagas[i].etapa,
                                    false,
                                    initObject.PRTGLOB.tasagas[i].tarifa == 0 ? false : true,
                                    Convert.ToDecimal(initObject.PRTGLOB.tasagas[i].monto));

                                if (resValue_u02 < 0)
                                {
                                    xgpyResult.ErrorCode = 11003;
                                    xgpyResult.Message = "Error al modificar gasto monto:" + initObject.PRTGLOB.tasagas[i].monto.ToString();
                                    tracer.TraceError("Alerta", xgpyResult.Message);
                                    return Json(xgpyResult);
                                }
                                break;

                            case T_PRTGLOB.eliminado_leido:
                            case T_PRTGLOB.eliminado_modificado:

                                var resValue_d02 = xgpyService.Sce_Tgas_D02_MS(
                                    llaveIdPrty,
                                    initObject.PRTGLOB.tasagas[i].sistema,
                                    initObject.PRTGLOB.tasagas[i].producto,
                                    initObject.PRTGLOB.tasagas[i].etapa);

                                if (resValue_d02 < 0)
                                {
                                    xgpyResult.ErrorCode = 11004;
                                    xgpyResult.Message = "Error al eliminar gasto monto:" + initObject.PRTGLOB.tasagas[i].monto.ToString();
                                    tracer.TraceError("Alerta", xgpyResult.Message);
                                    return Json(xgpyResult);
                                }
                                break;
                        }
                    }
                }
                xgpyResult.iObject = initObject;
                return Json(xgpyResult);
            }
        }

        public JsonResult SalvaGastoSy(String llaveIdPrty)
        {
            int tasaGasCount = 0;
            XgpyResult xgpyResult = new XgpyResult();
            var initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            tasaGasCount = -1;
            tasaGasCount = initObject.PRTGLOB.tasagas.Count();

            if (tasaGasCount == 0)
            {
                // (moo) 2016-01-11 | El sin información para almacenar, retorna exito, para poder seguir la cadena de funciones grabar participante
                xgpyResult.ErrorCode = 0;
                xgpyResult.Message = "Nada que salvar";
                return Json(xgpyResult);
            }

            for (int i = 0; i < tasaGasCount; i++)
            {
                switch (initObject.PRTGLOB.tasagas[i].estado)
                {
                    case T_PRTGLOB.nuevo:
                        var resValue_i01 = xgpyService.Sce_Tgas_I01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.tasagas[i].sistema,
                            initObject.PRTGLOB.tasagas[i].producto,
                            initObject.PRTGLOB.tasagas[i].etapa,
                            false,
                            initObject.PRTGLOB.tasagas[i].tarifa == 0 ? false : true,
                            Convert.ToDecimal(initObject.PRTGLOB.tasagas[i].monto));

                        if (resValue_i01 <= 0)
                        {
                            xgpyResult.ErrorCode = 11002;
                            xgpyResult.Message = "Error al ingresar gasto " + i;
                            return Json(xgpyResult);
                        }

                        break;

                    case T_PRTGLOB.modificado:
                        var resValue_u02 = xgpyService.Sce_Tgas_U02_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.tasagas[i].sistema,
                            initObject.PRTGLOB.tasagas[i].producto,
                            initObject.PRTGLOB.tasagas[i].etapa,
                            false,
                            initObject.PRTGLOB.tasagas[i].tarifa == 0 ? false : true,
                            Convert.ToDecimal(initObject.PRTGLOB.tasagas[i].monto));

                        if (resValue_u02 <= 0)
                        {
                            xgpyResult.ErrorCode = 11003;
                            xgpyResult.Message = "Error al modificar gasto " + i;
                            return Json(xgpyResult);
                        }

                        break;

                    case T_PRTGLOB.eliminado_leido:
                    case T_PRTGLOB.eliminado_modificado:
                        var resValue_d02 = xgpyService.Sce_Tgas_D02_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.tasagas[i].sistema,
                            initObject.PRTGLOB.tasagas[i].producto,
                            initObject.PRTGLOB.tasagas[i].etapa);

                        if (resValue_d02 <= 0)
                        {
                            xgpyResult.ErrorCode = 11004;
                            xgpyResult.Message = "Error al eliminar gasto " + i;
                            return Json(xgpyResult);
                        }

                        break;
                }
            }

            xgpyResult.ErrorCode = 0;
            xgpyResult.Message = "";
            return Json(xgpyResult);
        }

        public JsonResult SalvaInteresSy(String llaveIdPrty)
        {
            int tasaIntCount = 0;
            XgpyResult xgpyResult = new XgpyResult();
            var initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            tasaIntCount = initObject.PRTGLOB.tasaint.Count();

            if (tasaIntCount == 0)
            {
                // (moo) 2016-01-11 | El sin información para almacenar, retorna exito, para poder seguir la cadena de funciones grabar participante
                xgpyResult.ErrorCode = 0;
                xgpyResult.Message = "Nada que salvar";
                return Json(xgpyResult);
            }

            for (int i = 0; i < tasaIntCount; i++)
            {
                switch (initObject.PRTGLOB.tasaint[i].estado)
                {
                    case T_PRTGLOB.nuevo:
                        var resValue_i01 = xgpyService.Sce_Tint_I01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.tasaint[i].sistema,
                            initObject.PRTGLOB.tasaint[i].producto,
                            initObject.PRTGLOB.tasaint[i].etapa,
                            false,
                            initObject.PRTGLOB.tasaint[i].libor == 0 ? false : true,
                            initObject.PRTGLOB.tasaint[i].prime == 0 ? false : true,
                            initObject.PRTGLOB.tasaint[i].flotante == 0 ? false : true,
                            Convert.ToDecimal(initObject.PRTGLOB.tasaint[i].tasa));

                        if (resValue_i01 <= 0)
                        {
                            xgpyResult.ErrorCode = 11002;
                            xgpyResult.Message = "Error al ingresar interés " + i;
                            return Json(xgpyResult);
                        }

                        break;

                    case T_PRTGLOB.modificado:
                        var resValue_u01 = xgpyService.Sce_Tint_U01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.tasaint[i].sistema,
                            initObject.PRTGLOB.tasaint[i].producto,
                            initObject.PRTGLOB.tasaint[i].etapa,
                            initObject.PRTGLOB.tasaint[i].libor == 0 ? false : true,
                            initObject.PRTGLOB.tasaint[i].prime == 0 ? false : true,
                            initObject.PRTGLOB.tasaint[i].flotante == 0 ? false : true,
                            Convert.ToDecimal(initObject.PRTGLOB.tasaint[i].tasa));

                        if (resValue_u01 <= 0)
                        {
                            xgpyResult.ErrorCode = 11003;
                            xgpyResult.Message = "Error al modificar interés " + i;
                            return Json(xgpyResult);
                        }

                        break;

                    case T_PRTGLOB.eliminado_leido:
                    case T_PRTGLOB.eliminado_modificado:
                        var resValue_d02 = xgpyService.Sce_Tint_D02_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.tasaint[i].sistema,
                            initObject.PRTGLOB.tasaint[i].producto,
                            initObject.PRTGLOB.tasaint[i].etapa);

                        if (resValue_d02 <= 0)
                        {
                            xgpyResult.ErrorCode = 11004;
                            xgpyResult.Message = "Error al eliminar interés " + i;
                            return Json(xgpyResult);
                        }

                        break;
                }
            }

            xgpyResult.ErrorCode = 0;
            xgpyResult.Message = "";
            return Json(xgpyResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="llaveIdPrty"></param>
        /// <returns></returns>
        public JsonResult SalvaComisionSy(String llaveIdPrty)
        {
            var initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            var mensajes = string.Empty;
            return Json(SalvaComisionParticipante(initObject, llaveIdPrty, ref mensajes));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="llaveIdPrty"></param>
        /// <returns></returns>
        internal XgpyResult SalvaComisionParticipante(InitializationObject initObject, string llaveIdPrty, ref string mensaje)
        {
            using (var trace = new Tracer("SalvaComisionParticipante"))
            {
                XgpyResult xgpyResult = new XgpyResult() { ErrorCode = 0, Message = string.Empty };

                try
                {
                    int tasaComCount = 0, nuevaSecuencia = 0;

                    for (int k = 0; k < initObject.PRTGLOB.tasacom.Count(); k++)
                    {
                        if (initObject.PRTGLOB.tasacom[k].secuencia == 0)
                        {
                            nuevaSecuencia++;
                            initObject.PRTGLOB.tasacom[k].secuencia = nuevaSecuencia;
                        }
                        else
                            nuevaSecuencia = initObject.PRTGLOB.tasacom[k].secuencia;
                    }

                    tasaComCount = initObject.PRTGLOB.tasacom.Count();

                    if (tasaComCount == 0)
                    {
                        // (moo) 2016-01-11 | El sin información para almacenar, retorna exito, para poder seguir la cadena de funciones grabar participante
                        xgpyResult.ErrorCode = 0;
                        xgpyResult.Message = mensaje = "No hay comisiones para guardar en el participante.";
                        return xgpyResult;
                    }

                    for (int i = 0; i < tasaComCount; i++)
                    {
                        switch (initObject.PRTGLOB.tasacom[i].estado)
                        {
                            case T_PRTGLOB.nuevo:
                                var resValue_i01 = xgpyService.Sce_Tcom_I01_MS(
                                    llaveIdPrty,
                                    initObject.PRTGLOB.tasacom[i].sistema,
                                    initObject.PRTGLOB.tasacom[i].producto,
                                    initObject.PRTGLOB.tasacom[i].etapa,
                                    initObject.PRTGLOB.tasacom[i].secuencia,
                                    false,
                                    initObject.PRTGLOB.tasacom[i].manual == 0 ? false : true,
                                    initObject.PRTGLOB.tasacom[i].mto_fijo == 0 ? false : true,
                                    Convert.ToDecimal(initObject.PRTGLOB.tasacom[i].tasa),
                                    Convert.ToDecimal(initObject.PRTGLOB.tasacom[i].hasta),
                                    Convert.ToDecimal(initObject.PRTGLOB.tasacom[i].min),
                                    Convert.ToDecimal(initObject.PRTGLOB.tasacom[i].max),
                                    Convert.ToDateTime(initObject.PRTGLOB.tasacom[i].fecha));

                                if (resValue_i01 < 0)
                                {
                                    xgpyResult.ErrorCode = 11002;
                                    xgpyResult.Message = mensaje = "Error al insertar comisión " + i;
                                    return xgpyResult;
                                }

                                break;

                            case T_PRTGLOB.modificado:
                                var resValue_u02 = xgpyService.Sce_Tcom_U02_MS(
                                    llaveIdPrty,
                                    initObject.PRTGLOB.tasacom[i].sistema,
                                    initObject.PRTGLOB.tasacom[i].producto,
                                    initObject.PRTGLOB.tasacom[i].etapa,
                                    initObject.PRTGLOB.tasacom[i].secuencia,
                                    false,
                                    initObject.PRTGLOB.tasacom[i].manual == 0 ? false : true,
                                    initObject.PRTGLOB.tasacom[i].mto_fijo == 0 ? false : true,
                                    Convert.ToDecimal(initObject.PRTGLOB.tasacom[i].tasa),
                                    Convert.ToDecimal(initObject.PRTGLOB.tasacom[i].hasta),
                                    Convert.ToDecimal(initObject.PRTGLOB.tasacom[i].min),
                                    Convert.ToDecimal(initObject.PRTGLOB.tasacom[i].max),
                                    Convert.ToDateTime(initObject.PRTGLOB.tasacom[i].fecha));

                                if (resValue_u02 < 0)
                                {
                                    xgpyResult.ErrorCode = 11003;
                                    xgpyResult.Message = mensaje = "Error al modificar comisión " + i;
                                    return xgpyResult;
                                }

                                break;

                            case T_PRTGLOB.eliminado_leido:
                            case T_PRTGLOB.eliminado_modificado:
                                var resValue_d02 = xgpyService.Sce_Tcom_D02_MS(
                                    llaveIdPrty,
                                    initObject.PRTGLOB.tasacom[i].sistema,
                                    initObject.PRTGLOB.tasacom[i].producto,
                                    initObject.PRTGLOB.tasacom[i].etapa,
                                    initObject.PRTGLOB.tasacom[i].secuencia);

                                if (resValue_d02 < 0)
                                {
                                    xgpyResult.ErrorCode = 11004;
                                    xgpyResult.Message = mensaje = "Error al eliminar comisión " + i;
                                    return xgpyResult;
                                }
                                break;
                        }
                    }

                    return xgpyResult;
                }
                catch (Exception _ex)
                {
                    trace.TraceException("Alerta", _ex);
                    return xgpyResult;
                }
            }
        }

        public JsonResult SalvaLineasSy(String llaveIdPrty)
        {
            int linBancosCount = 0;
            XgpyResult xgpyResult = new XgpyResult();
            var initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            linBancosCount = initObject.PRTGLOB.linbancos.Count();

            if (linBancosCount == 0)
            {
                // (moo) 2016-01-11 | El sin información para almacenar, retorna exito, para poder seguir la cadena de funciones grabar participante
                xgpyResult.ErrorCode = 0;
                xgpyResult.Message = "Nada que salvar";
                return Json(xgpyResult);
            }

            for (int i = 0; i < linBancosCount; i++)
            {
                switch (initObject.PRTGLOB.linbancos[i].estado)
                {
                    case T_PRTGLOB.nuevo:
                        var resValue_i01 = xgpyService.Sce_Blin_I01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.linbancos[i].indice,
                            false,
                            initObject.PRTGLOB.linbancos[i].activa == 0 ? false : true,
                            Convert.ToDecimal(initObject.PRTGLOB.linbancos[i].moneda.Trim()),
                            initObject.PRTGLOB.linbancos[i].linea);

                        if (resValue_i01 <= 0)
                        {
                            xgpyResult.ErrorCode = 11002;
                            xgpyResult.Message = "Error al insertar participante " + i;
                            return Json(xgpyResult);
                        }

                        break;

                    case T_PRTGLOB.modificado:
                        var resValue_u01 = xgpyService.Sce_Blin_U01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.linbancos[i].indice,
                            false,
                            initObject.PRTGLOB.linbancos[i].activa == 0 ? false : true,
                            Convert.ToDecimal(initObject.PRTGLOB.linbancos[i].moneda.Trim()),
                            initObject.PRTGLOB.linbancos[i].linea);

                        if (resValue_u01 <= 0)
                        {
                            xgpyResult.ErrorCode = 11003;
                            xgpyResult.Message = "Error al modificar participante " + i;
                            return Json(xgpyResult);
                        }

                        break;

                    case T_PRTGLOB.eliminado_leido:
                    case T_PRTGLOB.eliminado_modificado:
                        var resValue_u01_1 = xgpyService.Sce_Blin_U01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.linbancos[i].indice,
                            true,
                            initObject.PRTGLOB.linbancos[i].activa == 0 ? false : true,
                            Convert.ToDecimal(initObject.PRTGLOB.linbancos[i].moneda.Trim()),
                            initObject.PRTGLOB.linbancos[i].linea);

                        if (resValue_u01_1 <= 0)
                        {
                            xgpyResult.ErrorCode = 11003;
                            xgpyResult.Message = "Error al modificar participante " + i;
                            return Json(xgpyResult);
                        }

                        break;
                }
            }

            xgpyResult.ErrorCode = 0;
            xgpyResult.Message = "";
            return Json(xgpyResult);
        }

        public JsonResult SalvaCtaBcoSy(String llaveIdPrty)
        {
            int ctaBancosCount = 0;
            XgpyResult xgpyResult = new XgpyResult();
            var initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            ctaBancosCount = initObject.PRTGLOB.ctabancos.Count();

            if (ctaBancosCount == 0)
            {
                // (moo) 2016-01-11 | El sin información para almacenar, retorna exito, para poder seguir la cadena de funciones grabar participante
                xgpyResult.ErrorCode = 0;
                xgpyResult.Message = "Nada que salvar";
                return Json(xgpyResult);
            }

            for (int i = 0; i < ctaBancosCount; i++)
            {
                switch (initObject.PRTGLOB.ctabancos[i].estado)
                {
                    case T_PRTGLOB.nuevo:
                        var resValue_i01 = xgpyService.Sce_Bcta_I01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.ctabancos[i].indice,
                            false,
                            initObject.PRTGLOB.ctabancos[i].activa == 0 ? false : true,
                            Convert.ToDecimal(initObject.PRTGLOB.ctabancos[i].moneda.Trim()),
                            initObject.PRTGLOB.ctabancos[i].cuenta,
                            initObject.PRTGLOB.ctabancos[i].especial == 0 ? false : true);

                        if (resValue_i01 <= 0)
                        {
                            xgpyResult.ErrorCode = 11002;
                            xgpyResult.Message = "Error al insertar participante " + i;
                            return Json(xgpyResult);
                        }

                        break;

                    case T_PRTGLOB.modificado:
                        var resValue_u01 = xgpyService.Sce_Bcta_U01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.ctabancos[i].indice,
                            false,
                            initObject.PRTGLOB.ctabancos[i].activa == 0 ? false : true,
                            Convert.ToDecimal(initObject.PRTGLOB.ctabancos[i].moneda.Trim()),
                            initObject.PRTGLOB.ctabancos[i].cuenta,
                            initObject.PRTGLOB.ctabancos[i].especial == 0 ? false : true);

                        if (resValue_u01 <= 0)
                        {
                            xgpyResult.ErrorCode = 11003;
                            xgpyResult.Message = "Error al modificar participante " + i;
                            return Json(xgpyResult);
                        }

                        break;

                    case T_PRTGLOB.eliminado_leido:
                    case T_PRTGLOB.eliminado_modificado:
                        var resValue_u01_1 = xgpyService.Sce_Bcta_U01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.ctabancos[i].indice,
                            true,
                            initObject.PRTGLOB.ctabancos[i].activa == 0 ? false : true,
                            Convert.ToDecimal(initObject.PRTGLOB.ctabancos[i].moneda.Trim()),
                            initObject.PRTGLOB.ctabancos[i].cuenta,
                            initObject.PRTGLOB.ctabancos[i].especial == 0 ? false : true);

                        if (resValue_u01_1 <= 0)
                        {
                            xgpyResult.ErrorCode = 11003;
                            xgpyResult.Message = "Error al modificar participante " + i;
                            return Json(xgpyResult);
                        }

                        break;
                }
            }

            xgpyResult.ErrorCode = 0;
            xgpyResult.Message = "";
            return Json(xgpyResult);
        }

        public JsonResult SalvaCuentasSy(String llaveIdPrty)
        {
            using (var tracer = new Tracer("SalvaCuentasSy"))
            {
                int ctaCliCount = 0, indiceCta = 0;

                XgpyResult xgpyResult = new XgpyResult()
                {
                    ErrorCode = 0,
                    Message = string.Empty
                };

                var initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
                ctaCliCount = initObject.PRTGLOB.ctaclie.Count();

                if (ctaCliCount == 0)
                {
                    xgpyResult.ErrorCode = 0;
                    xgpyResult.Message = "Nada que salvar";
                    return Json(xgpyResult);
                }

                for (int i = 0; i < ctaCliCount; i++)
                {
                    switch (initObject.PRTGLOB.ctaclie[i].estado)
                    {
                        case T_PRTGLOB.nuevo:
                            String id_Prty;
                            id_Prty = initObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(initObject.PRTGLOB.Party.Bnumber) ? initObject.PRTGLOB.Party.Bnumber : llaveIdPrty;
                            indiceCta = 0;

                            for (int k = 0; k <= initObject.PRTGLOB.ctaclie.GetUpperBound(0); k++)
                            {
                                if (initObject.PRTGLOB.ctaclie[k].indice > indiceCta)
                                    indiceCta = initObject.PRTGLOB.ctaclie[k].indice;
                            }

                            indiceCta++;

                            tracer.AddToContext("Sce_Ctas_I01_MS", "Nuevo");
                            tracer.AddToContext("id_Prty", id_Prty);
                            tracer.AddToContext("moneda", Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()));
                            tracer.AddToContext("CuentaSinFormato", initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                            var resValue_i01 = xgpyService.Sce_Ctas_I01_MS(
                                id_Prty,
                                initObject.PRTGLOB.ctaclie[i].indice, // indicecta++
                                false,
                                initObject.PRTGLOB.ctaclie[i].activabco == 0 ? false : true,
                                initObject.PRTGLOB.ctaclie[i].activace == 0 ? false : true,
                                initObject.PRTGLOB.ctaclie[i].extranjera == 0 ? false : true,
                                Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()),
                                initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                            if (resValue_i01 <= 0)
                            {
                                xgpyResult.ErrorCode = 11002;
                                xgpyResult.Message = "Error al insertar participante " + i;
                                tracer.TraceError("Alerta", xgpyResult.Message);
                                return Json(xgpyResult);
                            }

                            break;

                        case T_PRTGLOB.modificado:

                            tracer.AddToContext("Sce_Ctas_U01_MS", "Modificado");
                            tracer.AddToContext("id_Prty", llaveIdPrty);
                            tracer.AddToContext("moneda", Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()));
                            tracer.AddToContext("CuentaSinFormato", initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                            var resValue_u01 = xgpyService.Sce_Ctas_U01_MS(
                                llaveIdPrty,
                                initObject.PRTGLOB.ctaclie[i].indice,
                                false,
                                initObject.PRTGLOB.ctaclie[i].activabco == 0 ? false : true,
                                initObject.PRTGLOB.ctaclie[i].activace == 0 ? false : true,
                                initObject.PRTGLOB.ctaclie[i].extranjera == 0 ? false : true,
                                Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()),
                                initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                            if (resValue_u01 <= 0)
                            {
                                xgpyResult.ErrorCode = 11003;
                                xgpyResult.Message = "Error al modificar participante " + i;
                                tracer.TraceError("Alerta", xgpyResult.Message);
                                return Json(xgpyResult);
                            }

                            break;

                        case T_PRTGLOB.eliminado_leido:
                        case T_PRTGLOB.eliminado_modificado:

                            tracer.AddToContext("Sce_Ctas_U01_MS", "Eliminado/Modificado");
                            tracer.AddToContext("id_Prty", llaveIdPrty);
                            tracer.AddToContext("moneda", Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()));
                            tracer.AddToContext("CuentaSinFormato", initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                            var resValue_u01_1 = xgpyService.Sce_Ctas_U01_MS(
                                llaveIdPrty,
                                initObject.PRTGLOB.ctaclie[i].indice,
                                true,
                                initObject.PRTGLOB.ctaclie[i].activabco == 0 ? false : true,
                                initObject.PRTGLOB.ctaclie[i].activace == 0 ? false : true,
                                initObject.PRTGLOB.ctaclie[i].extranjera == 0 ? false : true,
                                Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()),
                                initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                            if (resValue_u01_1 <= 0)
                            {
                                xgpyResult.ErrorCode = 11004;
                                xgpyResult.Message = "Error al modificar participante " + i;
                                tracer.TraceError("Alerta", xgpyResult.Message);
                                return Json(xgpyResult);
                            }

                            break;
                    }
                }

                return Json(xgpyResult);
            }
        }

        public JsonResult SalvaInstruccionSy()
        {
            int res = 0;
            string mem = "";
            Int32 cod;
            XgpyResult xgpyResult = new XgpyResult();
            var initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

            for (int i = 0; i <= 6; i++)
            {
                cod = initObject.PRTGLOB.instruccion[i].codigo;
                mem = initObject.PRTGLOB.instruccion[i].Memo;

                if (!String.IsNullOrEmpty(mem))
                {
                    // Si la instruccion es nueva
                    if (cod == 0)
                    {
                        res = MODGMEM.SyPutn_Mem("p", 0, mem, ref initObject, xgpyService);
                        if (res == 0)
                        {
                            xgpyResult.ErrorCode = 2001;
                            xgpyResult.Message = "Ha ocurrido un error al grabar memos de instrucciones";
                            xgpyResult.iObject = initObject;
                            return Json(xgpyResult);
                        }
                        else
                            initObject.PRTGLOB.instruccion[i].codigo = res;
                    }
                    else
                    {
                        // Si la instruccion fue modificada
                        res = MODGMEM.SyPutn_Mem("p", cod, mem, ref initObject, xgpyService);
                        if (res == 0)
                        {
                            xgpyResult.ErrorCode = 2001;
                            xgpyResult.Message = "Ha ocurrido un error al grabar memos de instrucciones";
                            xgpyResult.iObject = initObject;
                            return Json(xgpyResult);
                        }
                    }
                }
                else
                {
                    if (cod != 0)
                    {
                        res = MODGMEM.SyPutn_Mem("p", cod, "", ref initObject, xgpyService);
                        if (res == 0)
                        {
                            xgpyResult.ErrorCode = 2001;
                            xgpyResult.Message = "Ha ocurrido un error al grabar memos de instrucciones";
                            xgpyResult.iObject = initObject;
                            return Json(xgpyResult);
                        }

                        initObject.PRTGLOB.instruccion[i].codigo = 0;
                    }
                }
            }

            initObject.PRTGLOB.Party.insgen_imp = Convert.ToInt32(initObject.PRTGLOB.instruccion[0].codigo); //ToString aa todos
            initObject.PRTGLOB.Party.insgen_exp = Convert.ToInt32(initObject.PRTGLOB.instruccion[1].codigo);
            initObject.PRTGLOB.Party.insgen_ser = Convert.ToInt32(initObject.PRTGLOB.instruccion[2].codigo);
            initObject.PRTGLOB.Party.inscob_imp = Convert.ToInt32(initObject.PRTGLOB.instruccion[3].codigo);
            initObject.PRTGLOB.Party.inscob_exp = Convert.ToInt32(initObject.PRTGLOB.instruccion[4].codigo);
            initObject.PRTGLOB.Party.inscre_imp = Convert.ToInt32(initObject.PRTGLOB.instruccion[5].codigo);
            initObject.PRTGLOB.Party.inscre_exp = Convert.ToInt32(initObject.PRTGLOB.instruccion[6].codigo);
            xgpyResult.ErrorCode = 0;
            xgpyResult.Message = "";
            xgpyResult.iObject = initObject;
            Session[SessionKeys.AdminParty.PartySessionKey] = initObject;
            return Json(xgpyResult);
        }

        /*public int PrtyVerificaFlag(String llaveIdPrty, InitializationObject iObject)
        {
            int flag = iObject.PRTGLOB.Party.Flag;
            if (flag == 0)
            {
                flag = 4;
                return xgpyService.Sce_Prty_Flag_e01_MS(llaveIdPrty, flag);
            }
            return 0;
        }*/

        public string PrtyLimpiarRazonSocial(InitializationObject initObject, string llaveIdPrty, ref string mensajeError)
        {
            if (initObject.PRTGLOB.nom.Length == 0)
                mensajeError = "Advertencia : No hay nombres a guardar.";
            else
            {
                prtynombre razonSocial = initObject.PRTGLOB.nom[0];
                int resultado = xgpyService.Sce_rsa_du01_MS(llaveIdPrty, razonSocial.nombre, razonSocial.fantasia, razonSocial.contacto, razonSocial.sortkey, this.LoadUserPrtySession().centroCosto, this.LoadUserPrtySession().especialista);

                if (resultado >= 0)
                    mensajeError = "OK";
                else
                {
                    switch (resultado)
                    {
                        case -1: mensajeError = "Error limpiando las razones sociales repetidas"; break;
                        case -2: mensajeError = "Error actualizando la razón social"; break;
                        default: break;
                    }
                }

            }
            return HttpUtility.HtmlEncode(mensajeError);
        }

        public JsonResult GuardaParticipanteGlobal(String llaveIdPrty, Int32 estadoPrty, String iO)
        {
            JObject o = JObject.Parse(iO); //Convertimos en objeto lo enviado en cadena
            InitializationObject iObject = o.ToObject<InitializationObject>(); //Inicializamos la clase global de la aplicacion con la información
            XgpyResult xgpyResult = new XgpyResult(); //Instanciamos el retorno del guardado
            xgpyResult.ErrorCode = 0;
            xgpyResult.Message = "";
            string mensajeError = string.Empty;

            //Para cuentas, tasas, instrucciones
            InitializationObject objectSesion = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            xgpyResult.iObject = iObject;

            //Grabamos la informacion del participante
            if (GrabarParticipante(xgpyResult, llaveIdPrty, estadoPrty, iObject, objectSesion) != "OK")
            {
                xgpyResult.ErrorCode = 11000;
                xgpyResult.Message = "Error al grabar Información del Participante [" + llaveIdPrty + "]";
                return Json(xgpyResult);
            }

            if (GrabarParticipanteRelacionEjecutivo(objectSesion, iObject, ref mensajeError) < 0)
            {
                xgpyResult.ErrorCode = 11000;
                xgpyResult.Message = mensajeError;
                return Json(xgpyResult);
            }

            if (GrabarParticipanteRazonesSociales(iObject, llaveIdPrty, ref mensajeError) != "OK")
            {
                xgpyResult.ErrorCode = 11000;
                xgpyResult.Message = mensajeError;
                return Json(xgpyResult);
            }

            if (GrabarParticipanteDirecciones(iObject, llaveIdPrty, ref mensajeError) != "OK")
            {
                xgpyResult.ErrorCode = 11000;
                xgpyResult.Message = mensajeError;
                return Json(xgpyResult);
            }

            //Verifica en caso se decida grabar sin cuentas en el nuevo participante
            // Modal : desea grabar participante sin cuentas ? Cliente (true) / Individuo (false)
            ///bool flagIndividuo = iObject.PRTGLOB.Party.tipo == 0 ? false : true;
            if (iObject.PRTGLOB.ctaclie.Length > 0)
            {
                if (GrabarParticipanteCuentas(objectSesion, llaveIdPrty, ref mensajeError) != "OK")
                {
                    xgpyResult.ErrorCode = 11000;
                    xgpyResult.Message = mensajeError;
                    return Json(xgpyResult);
                }
            }

            if (GrabarParticipanteTasas(objectSesion, llaveIdPrty, ref mensajeError) != "OK")
            {
                xgpyResult.ErrorCode = 11000;
                xgpyResult.Message = mensajeError;
                return Json(xgpyResult);
            }

            if (SalvaComisionParticipante(objectSesion, llaveIdPrty, ref mensajeError).ErrorCode != 0)
            {
                xgpyResult.ErrorCode = 11000;
                xgpyResult.Message = mensajeError;
                return Json(xgpyResult);
            }

            if (GrabarParticipanteInstrucciones(objectSesion, ref mensajeError) != "OK")
            {
                xgpyResult.ErrorCode = 11000;
                xgpyResult.Message = mensajeError;
                return Json(xgpyResult);
            }

            /* característica no implementada
            if (PrtyVerificaFlag(llaveIdPrty, objectSesion) != 0)
            {
                xgpyResult.ErrorCode = 11000;
                xgpyResult.Message = "Error al grabar Información del Participante [" + llaveIdPrty + "]";
                return Json(xgpyResult);
            }
            */

            // eliminación para publicación de encasillamiento
            /*if (PrtyLimpiarRazonSocial(objectSesion, llaveIdPrty, ref mensajeError) != "OK")
            {
                xgpyResult.ErrorCode = 11000;
                xgpyResult.Message = mensajeError;
                return Json(xgpyResult);
            }*/


            if (iObject.PRTYENT.Hab_SGTCliEje != 0)
            {
                if (T_PRTYENT.Cliente_SGT == 0)
                {
                    xgpyResult.ErrorCode = 0;
                    xgpyResult.Message = "El Cliente no se encuentra registrado en el Sistema General de Tablas del Banco. No se podran guardar los datos del Ejecutivo Comex.";
                    return Json(xgpyResult);
                }
            }

            //INICIO MODIFICACION CNC - ACCENTURE 

            //Se graba informacion de Clasificacion del cliente
            if (GrabarClasificacionCliente(iObject, objectSesion) != "OK")
            {
                xgpyResult.ErrorCode = 11000;
                xgpyResult.Message = "Error al grabar Clasificacion del Cliente [" + llaveIdPrty + "]";
                return Json(xgpyResult);
            }
            //FIN MODIFICACION CNC - ACCENTURE 

            xgpyResult.iObject = iObject;
            return Json(xgpyResult);
        }

        public string GrabarParticipante(XgpyResult xgpyResult, String llaveIdPrty, Int32 estadoPrty, InitializationObject iObject, InitializationObject objectSesion)
        {
            string KeyPrty;
            byte tipo_party;
            var borrar = EstadoRegistro(estadoPrty); //Asignamos el flag al registro de participante que viene

            if (iObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(iObject.PRTGLOB.Party.Bnumber))
            {
                KeyPrty = iObject.PRTGLOB.Party.Bnumber.PadRight(12, '|');
            }
            else
            {
                KeyPrty = llaveIdPrty;
            }

            if (iObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(iObject.PRTGLOB.Party.Bnumber))
            {
                tipo_party = 3;
            }
            else
            {
                tipo_party = Convert.ToByte(iObject.PRTGLOB.Party.tipo);
            }

            // Evitamos que al tener un rut vacío, se guarde con ceros.
            if (!string.IsNullOrWhiteSpace(iObject.PRTGLOB.Party.rut))
            {
                // En caso de venir un rut, lo limpiamos, rellenamos con ceros y dejamos la posible k en mayúscula.
                iObject.PRTGLOB.Party.rut = iObject.PRTGLOB.Party.rut.ToUpper().Trim().PadLeft(10, '0');
            }
            else
            {
                iObject.PRTGLOB.Party.rut = string.Empty;
            }


            iObject.PRTGLOB.Party.rut = FormatUtils.TryFormatearRutParticipante(iObject.PRTGLOB.Party.rut);

            var result_w01 = xgpyService.sce_prty_w01_MS(
                KeyPrty,
                borrar,
                tipo_party,
                Convert.ToInt16(iObject.PRTGLOB.Party.Flag),
                Convert.ToByte(objectSesion.PRTGLOB.Party.clasificacion),
                Convert.ToBoolean(iObject.PRTGLOB.Party.sirut),
                iObject.PRTGLOB.Party.rut,
                this.infoUsuario.Identificacion_CentroDeCostosImpersonado,
                this.infoUsuario.Identificacion_IdEspecialistaImpersonado,
                this.infoUsuario.Identificacion_CentroDeCostosImpersonado,
                this.infoUsuario.Identificacion_IdEspecialistaImpersonado,
                Convert.ToBoolean(iObject.PRTGLOB.Party.multiple),
                objectSesion.PRTGLOB.Party.oficina,
                objectSesion.PRTGLOB.Party.ejecutivo,
                objectSesion.PRTGLOB.Party.actividad,
                objectSesion.PRTGLOB.Party.riesgo,
                Convert.ToInt16(iObject.PRTGLOB.Party.codbco),
                Convert.ToBoolean(iObject.PRTGLOB.Party.libor),
                Convert.ToBoolean(iObject.PRTGLOB.Party.prime),
                Convert.ToSingle(iObject.PRTGLOB.Party.spread),
                (!string.IsNullOrEmpty(iObject.PRTGLOB.Party.swif) ? iObject.PRTGLOB.Party.swif.ToUpper().Trim() : string.Empty),
                Convert.ToInt32(iObject.PRTGLOB.Party.aladi),
                iObject.PRTGLOB.Party.ejecorr,
                Convert.ToInt16(iObject.PRTGLOB.Party.flagins),
                iObject.PRTGLOB.Party.insgen_imp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.insgen_imp),
                iObject.PRTGLOB.Party.insgen_exp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.insgen_exp),
                iObject.PRTGLOB.Party.insgen_ser == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.insgen_ser),
                iObject.PRTGLOB.Party.inscob_imp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.inscob_imp),
                iObject.PRTGLOB.Party.insgen_exp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.inscob_exp),
                iObject.PRTGLOB.Party.inscre_imp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.inscre_imp),
                iObject.PRTGLOB.Party.inscre_exp == 0 ? 0 : Convert.ToInt32(iObject.PRTGLOB.Party.inscre_exp),
                "");

            return result_w01;
        }

        public int GrabarParticipanteRelacionEjecutivo(InitializationObject objectSession, InitializationObject iObject, ref string mensaje)
        {
            for (int i = 0; i < objectSession.PRTYENT2.VSGTCliEsp.Count(); i++)
            {
                if (objectSession.PRTYENT2.VSGTCliEsp[i] != null)
                {
                    if (objectSession.PRTYENT2.VSGTCliEsp[i].estado == T_PRTGLOB.nuevo)
                    {
                        var result_w01 = xgpyService.ejc_prty_ejc_i_01_MS(iObject.PRTGLOB.Party.rut,
                                                                         decimal.Parse(objectSession.PRTYENT2.VSGTCliEsp[i].ofieje),
                                                                         decimal.Parse(objectSession.PRTYENT2.VSGTCliEsp[i].codeje),
                                                                         objectSession.PRTYENT2.VSGTCliEsp[i].tipo,
                                                                         DateTime.Now,
                                                                         DateTime.Now);
                        if (result_w01 < 0)
                        {
                            mensaje = "Error al grabar ejecutivo con código : " + objectSession.PRTYENT2.VSGTCliEsp[i].codeje;
                            return result_w01 == null ? -1 : result_w01.Value;
                        }
                    }
                }
            }

            return 0;
        }

        public string GrabarParticipanteRazonesSociales(InitializationObject initObject, string llaveIdPrty, ref string mensajeError)
        {
            string KeyPrty = "";

            if (initObject.PRTGLOB.nom.Length == 0)
            {
                mensajeError = "Advertencia : No hay nombres a guardar.";
                return HttpUtility.HtmlEncode(mensajeError);
            }

            KeyPrty = initObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(initObject.PRTGLOB.Party.Bnumber) ? initObject.PRTGLOB.Party.Bnumber.PadRight(12, '|') : llaveIdPrty;

            for (int i = 0; i < initObject.PRTGLOB.nom.Length; i++)
            {
                switch (initObject.PRTGLOB.nom[i].estado)
                {
                    case T_PRTGLOB.nuevo:
                        var resultValue_i01 = xgpyService.sce_rsa_i01_MS(
                            KeyPrty,
                            initObject.PRTGLOB.nom[i].indice,
                            initObject.PRTGLOB.nom[i].nombre,
                            initObject.PRTGLOB.nom[i].fantasia,
                            initObject.PRTGLOB.nom[i].contacto,
                            initObject.PRTGLOB.nom[i].sortkey,
                            this.LoadUserPrtySession().centroCosto,
                            this.LoadUserPrtySession().especialista);

                        if (resultValue_i01 < 0)
                        {
                            mensajeError = "Advertencia : Sucedio un error al grabar razón social : " + initObject.PRTGLOB.nom[i].nombre;
                            return HttpUtility.HtmlEncode(mensajeError);
                        }

                        //En caso lo haya ingresado como inactivo
                        if (initObject.PRTGLOB.nom[i].borrado == "1")
                        {
                            var resultValue_u02 = xgpyService.sce_rsa_u02_MS(KeyPrty, initObject.PRTGLOB.nom[i].indice);
                            if (resultValue_u02 < 0)
                            {
                                mensajeError = "Advertencia : Error al modificar la razón social ingresada como inactiva : " + initObject.PRTGLOB.nom[i].nombre;
                                return HttpUtility.HtmlEncode(mensajeError);
                            }
                        }

                        initObject.PRTGLOB.nom[i].estado = T_PRTGLOB.leido;
                        break;

                    case T_PRTGLOB.modificado:

                        var resultValue_u01 = xgpyService.sce_rsa_u01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.nom[i].indice,
                            int.Parse(initObject.PRTGLOB.nom[i].borrado),
                            initObject.PRTGLOB.nom[i].nombre,
                            initObject.PRTGLOB.nom[i].fantasia,
                            initObject.PRTGLOB.nom[i].contacto,
                            initObject.PRTGLOB.nom[i].sortkey);

                        if (resultValue_u01 < 0)
                        {
                            mensajeError = "Advertencia : Error al modificar la razon social :" + initObject.PRTGLOB.nom[i].nombre;
                            return HttpUtility.HtmlEncode(mensajeError);
                        }

                        initObject.PRTGLOB.nom[i].estado = T_PRTGLOB.leido;
                        break;

                    case T_PRTGLOB.eliminado_leido:
                    case T_PRTGLOB.eliminado_modificado:

                        var resultValue_u01_1 = xgpyService.sce_rsa_u01_MS(
                            llaveIdPrty,
                            initObject.PRTGLOB.nom[i].indice,
                            1, // se pone 1 por que en la base de datos , 1 es el flag de borrado
                            initObject.PRTGLOB.nom[i].nombre,
                            initObject.PRTGLOB.nom[i].fantasia,
                            initObject.PRTGLOB.nom[i].contacto,
                            initObject.PRTGLOB.nom[i].sortkey);

                        if (resultValue_u01_1 < 0)
                        {
                            mensajeError = "Advertencia : Error al modificar la razon social :" + initObject.PRTGLOB.nom[i].nombre;
                            return HttpUtility.HtmlEncode(mensajeError);
                        }

                        break;
                }
            }
            return HttpUtility.HtmlEncode("OK");
        }

        public string GrabarParticipanteDirecciones(InitializationObject iObject, string llaveIdPrty, ref string mensajeError)
        {
            string keyIdPrty = "";
            if (iObject.PRTGLOB.direc.Length == 0)
            {
                mensajeError = "Advertencia : No hay direcciones a guardar.";
                return HttpUtility.HtmlEncode(mensajeError);
            }

            keyIdPrty = iObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(iObject.PRTGLOB.Party.Bnumber) ? iObject.PRTGLOB.Party.Bnumber.PadRight(12, '|') : llaveIdPrty;

            for (int i = 0; i < iObject.PRTGLOB.direc.Length; i++)
            {
                switch (iObject.PRTGLOB.direc[i].estado)
                {
                    case T_PRTGLOB.nuevo:
                        var result_i01 = xgpyService.sce_dad_i01_MS(
                            keyIdPrty,
                            iObject.PRTGLOB.direc[i].indice,
                            false,
                            iObject.PRTGLOB.direc[i].direccion,
                            iObject.PRTGLOB.direc[i].comuna,
                            iObject.PRTGLOB.direc[i].CodComuna,
                            iObject.PRTGLOB.direc[i].codpostal,
                            iObject.PRTGLOB.direc[i].region,
                            iObject.PRTGLOB.direc[i].ciudad,
                            iObject.PRTGLOB.direc[i].pais,
                            iObject.PRTGLOB.direc[i].CodPais,
                            iObject.PRTGLOB.direc[i].telefono,
                            iObject.PRTGLOB.direc[i].fax,
                            iObject.PRTGLOB.direc[i].telex,
                            iObject.PRTGLOB.direc[i].enviar_a,
                            iObject.PRTGLOB.direc[i].recibe,
                            iObject.PRTGLOB.direc[i].CasPostal,
                            iObject.PRTGLOB.direc[i].CasBanco,
                            this.LoadUserPrtySession().centroCosto,
                            this.LoadUserPrtySession().especialista,
                            iObject.PRTGLOB.direc[i].email);

                        if (result_i01 != "OK")
                        {
                            mensajeError = "Error al insertar dirección : " + " [" + iObject.PRTGLOB.direc[i].direccion + "]";
                            return HttpUtility.HtmlEncode(mensajeError);
                        }

                        iObject.PRTGLOB.direc[i].estado = T_PRTGLOB.leido;
                        break;

                    case T_PRTGLOB.modificado:
                        var result_u02 = xgpyService.sce_dad_u02_MS(
                            llaveIdPrty,
                            iObject.PRTGLOB.direc[i].indice,
                            int.Parse(iObject.PRTGLOB.direc[i].borrado) == 0 ? false : true,
                            iObject.PRTGLOB.direc[i].direccion,
                            iObject.PRTGLOB.direc[i].comuna,
                            iObject.PRTGLOB.direc[i].CodComuna,
                            iObject.PRTGLOB.direc[i].codpostal,
                            iObject.PRTGLOB.direc[i].estado.ToString(),
                            iObject.PRTGLOB.direc[i].ciudad,
                            iObject.PRTGLOB.direc[i].pais,
                            iObject.PRTGLOB.direc[i].CodPais,
                            iObject.PRTGLOB.direc[i].telefono,
                            iObject.PRTGLOB.direc[i].fax,
                            iObject.PRTGLOB.direc[i].telex,
                            iObject.PRTGLOB.direc[i].enviar_a,
                            iObject.PRTGLOB.direc[i].recibe,
                            iObject.PRTGLOB.direc[i].CasPostal,
                            iObject.PRTGLOB.direc[i].CasBanco,
                            iObject.PRTGLOB.direc[i].email);

                        if (result_u02 != "OK")
                        {
                            mensajeError = "Error al modificar dirección : " + " [" + iObject.PRTGLOB.direc[i].direccion + "]";
                            return HttpUtility.HtmlEncode(mensajeError);
                        }

                        iObject.PRTGLOB.direc[i].estado = T_PRTGLOB.leido;
                        break;

                    case T_PRTGLOB.eliminado_leido:
                    case T_PRTGLOB.eliminado_modificado:
                        var result_u03 = xgpyService.sce_dad_u03_MS(llaveIdPrty, Convert.ToByte(iObject.PRTGLOB.direc[i].indice));

                        if (result_u03 != "OK")
                        {
                            mensajeError = "Error al modificar dirección : " + " [" + iObject.PRTGLOB.direc[i].direccion + "]";
                            return HttpUtility.HtmlEncode(mensajeError);
                        }

                        iObject.PRTGLOB.direc[i].estado = T_PRTGLOB.leido;
                        break;
                }
            }

            return HttpUtility.HtmlEncode("OK");
        }

        public string GrabarParticipanteCuentas(InitializationObject initObject, string llaveIdPrty, ref string mensajeError)
        {
            using (var tracer = new Tracer("GrabarParticipanteCuentas"))
            {
                string id_Prty = string.Empty;
                id_Prty = initObject.PRTGLOB.Party.PrtGlob.EsCITI != 0 && !String.IsNullOrEmpty(initObject.PRTGLOB.Party.Bnumber) ? initObject.PRTGLOB.Party.Bnumber.PadRight(12, '|') : llaveIdPrty;

                if (initObject.PRTGLOB.ctaclie.Count() > 0)
                {
                    for (int i = 0; i < initObject.PRTGLOB.ctaclie.Count(); i++)
                    {
                        if (initObject.PRTGLOB.ctaclie[i] == null)
                            continue;

                        switch (initObject.PRTGLOB.ctaclie[i].estado)
                        {
                            case T_PRTGLOB.nuevo:

                                tracer.AddToContext("Sce_Ctas_I01_MS", "Nuevo");
                                tracer.AddToContext("id_Prty", id_Prty);
                                tracer.AddToContext("moneda", Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()));
                                tracer.AddToContext("CuentaSinFormato", initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                var resValue_i01 = xgpyService.Sce_Ctas_I01_MS(
                                    id_Prty,
                                    initObject.PRTGLOB.ctaclie[i].indice,
                                    false,
                                    initObject.PRTGLOB.ctaclie[i].activabco == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].activace == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].extranjera == 0 ? false : true,
                                    Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()),
                                    initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                if (resValue_i01 < 0)
                                {
                                    mensajeError = "Error al ingresar cuenta en el participante -  Cuenta : " + initObject.PRTGLOB.ctaclie[i].CuentaSinFormato;
                                    tracer.TraceError("Alerta", mensajeError);
                                    return HttpUtility.HtmlEncode(mensajeError);
                                }

                                break;

                            case T_PRTGLOB.modificado:

                                tracer.AddToContext("Sce_Ctas_U01_MS", "Modificado");
                                tracer.AddToContext("id_Prty", id_Prty);
                                tracer.AddToContext("moneda", Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()));
                                tracer.AddToContext("CuentaSinFormato", initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                var resValue_u01 = xgpyService.Sce_Ctas_U01_MS(
                                    llaveIdPrty,
                                    initObject.PRTGLOB.ctaclie[i].indice,
                                    false,
                                    initObject.PRTGLOB.ctaclie[i].activabco == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].activace == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].extranjera == 0 ? false : true,
                                    Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()),
                                    initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                if (resValue_u01 < 0)
                                {
                                    mensajeError = "Error al modificar cuenta en el participante -  Cuenta : " + initObject.PRTGLOB.ctaclie[i].CuentaSinFormato;
                                    tracer.TraceError("Alerta", mensajeError);
                                    return HttpUtility.HtmlEncode(mensajeError);
                                }

                                break;

                            case T_PRTGLOB.eliminado_leido:
                            case T_PRTGLOB.eliminado_modificado:

                                tracer.AddToContext("Sce_Ctas_U01_MS", "Eliminado/Modificado");
                                tracer.AddToContext("id_Prty", id_Prty);
                                tracer.AddToContext("moneda", Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()));
                                tracer.AddToContext("CuentaSinFormato", initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                var resValue_u01_1 = xgpyService.Sce_Ctas_U01_MS(
                                    llaveIdPrty,
                                    initObject.PRTGLOB.ctaclie[i].indice,
                                    true,
                                    initObject.PRTGLOB.ctaclie[i].activabco == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].activace == 0 ? false : true,
                                    initObject.PRTGLOB.ctaclie[i].extranjera == 0 ? false : true,
                                    Convert.ToDecimal(initObject.PRTGLOB.ctaclie[i].moneda.Trim()),
                                    initObject.PRTGLOB.ctaclie[i].CuentaSinFormato);

                                if (resValue_u01_1 < 0)
                                {
                                    mensajeError = "Error al eliminar cuenta en el participante - Cuenta : " + initObject.PRTGLOB.ctaclie[i].CuentaSinFormato;
                                    tracer.TraceError("Alerta", mensajeError);
                                    return HttpUtility.HtmlEncode(mensajeError);
                                }

                                break;
                        }
                    }
                }

                return HttpUtility.HtmlEncode("OK");
            }
        }

        public string GrabarParticipanteTasas(InitializationObject initObject, string llaveIdPrty, ref string mensajeError)
        {
            if (initObject.PRTGLOB.tasagas == null)
                return HttpUtility.HtmlEncode("OK");


            if (initObject.PRTGLOB.tasagas.Count() > 0)
            {
                for (int i = 0; i < initObject.PRTGLOB.tasagas.Count(); i++)
                {
                    if (initObject.PRTGLOB.tasagas[i] == null)
                        continue;

                    switch (initObject.PRTGLOB.tasagas[i].estado)
                    {
                        case T_PRTGLOB.nuevo:
                            var resValue_i01 = xgpyService.Sce_Tgas_I01_MS(
                                llaveIdPrty,
                                initObject.PRTGLOB.tasagas[i].sistema,
                                initObject.PRTGLOB.tasagas[i].producto,
                                initObject.PRTGLOB.tasagas[i].etapa,
                                false,
                                initObject.PRTGLOB.tasagas[i].tarifa == 0 ? false : true,
                                Convert.ToDecimal(initObject.PRTGLOB.tasagas[i].monto));

                            if (resValue_i01 < 0)
                            {
                                mensajeError = "Error al ingresar gasto monto:" + initObject.PRTGLOB.tasagas[i].monto.ToString();
                                return HttpUtility.HtmlEncode(mensajeError);
                            }
                            break;

                        case T_PRTGLOB.modificado:
                            var resValue_u02 = xgpyService.Sce_Tgas_U02_MS(
                                llaveIdPrty,
                                initObject.PRTGLOB.tasagas[i].sistema,
                                initObject.PRTGLOB.tasagas[i].producto,
                                initObject.PRTGLOB.tasagas[i].etapa,
                                false,
                                initObject.PRTGLOB.tasagas[i].tarifa == 0 ? false : true,
                                Convert.ToDecimal(initObject.PRTGLOB.tasagas[i].monto));

                            if (resValue_u02 < 0)
                            {
                                mensajeError = "Error al modificar gasto monto:" + initObject.PRTGLOB.tasagas[i].monto.ToString();
                                return HttpUtility.HtmlEncode(mensajeError);
                            }

                            break;

                        case T_PRTGLOB.eliminado_leido:
                        case T_PRTGLOB.eliminado_modificado:
                            var resValue_d02 = xgpyService.Sce_Tgas_D02_MS(
                                llaveIdPrty,
                                initObject.PRTGLOB.tasagas[i].sistema,
                                initObject.PRTGLOB.tasagas[i].producto,
                                initObject.PRTGLOB.tasagas[i].etapa);

                            if (resValue_d02 < 0)
                            {
                                mensajeError = "Error al eliminar gasto monto:" + initObject.PRTGLOB.tasagas[i].monto.ToString();
                                return HttpUtility.HtmlEncode(mensajeError);
                            }

                            break;
                    }
                }
            }

            return HttpUtility.HtmlEncode("OK");
        }

        public string GrabarParticipanteInstrucciones(InitializationObject initObject, ref string mensajeError)
        {
            int codigoInstruccion = 0;
            string mem = "";
            int cod;

            for (int i = 0; i <= 6; i++)
            {
                if (i + 1 > initObject.PRTGLOB.instruccion.Length)
                    continue;
                else if (initObject.PRTGLOB.instruccion[i] == null)
                    continue;

                cod = initObject.PRTGLOB.instruccion[i].codigo;
                mem = initObject.PRTGLOB.instruccion[i].Memo;

                if (!String.IsNullOrEmpty(mem))
                {
                    // Si la instruccion es nueva
                    if (cod == 0)
                    {
                        codigoInstruccion = MODGMEM.SyPutn_Mem_New("p", 0, mem, ref initObject, xgpyService);
                        if (codigoInstruccion <= 0)
                        {
                            mensajeError = "Ha ocurrido un error al grabar memos de instrucciones";
                            return HttpUtility.HtmlEncode(mensajeError);
                        }
                        else
                            initObject.PRTGLOB.instruccion[i].codigo = codigoInstruccion;
                    }
                    else
                    {
                        // Si la instruccion fue modificada
                        codigoInstruccion = MODGMEM.SyPutn_Mem_New("p", cod, mem, ref initObject, xgpyService);

                        if (codigoInstruccion <= 0)
                        {
                            mensajeError = "Ha ocurrido un error al grabar memos de instrucciones";
                            return HttpUtility.HtmlEncode(mensajeError);
                        }
                    }
                }
                else
                {
                    if (cod != 0)
                    {
                        codigoInstruccion = MODGMEM.SyPutn_Mem_New("p", cod, "", ref initObject, xgpyService);
                        if (codigoInstruccion <= 0)
                        {
                            mensajeError = "Ha ocurrido un error al grabar memos de instrucciones";
                            return HttpUtility.HtmlEncode(mensajeError);
                        }

                        initObject.PRTGLOB.instruccion[i].codigo = 0;
                    }
                }
            }

            return HttpUtility.HtmlEncode("OK");
        }

        private bool EstadoRegistro(int estadoRegistro)
        {
            switch (estadoRegistro)
            {
                case T_PRTGLOB.nuevo:
                    return false;

                case T_PRTGLOB.modificado:
                    return false;

                case T_PRTGLOB.eliminado_leido:
                    return true;

                case T_PRTGLOB.eliminado_modificado:
                    return true;

                default:
                    return false;
            }
        }

        public JsonResult EliminarParticipante()
        {
            var initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            string llave = initObject.PRTGLOB.Party.idparty;
            XgpyResult xgpyResult = new XgpyResult(); //Instanciamos el retorno del guardado
            xgpyResult.ErrorCode = 0;
            xgpyResult.Message = "Participante eliminado correctamente";

            if (xgpyService.VSyGet_Cta(initObject, llave, 2) == 0)
            {
                xgpyResult.ErrorCode = 1;
                xgpyResult.Message = "Este Participante no puede ser eliminado debido a que existe un movimiento con una de sus cuentas.";
            }
            else
                xgpyService.Sce_Prty_U01_MS(llave, true);

            return Json(xgpyResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene el número de cuentas para saber si le va mostrar la modal o no
        /// </summary>
        /// <returns></returns>
        public JsonResult GetNumeroCuentas()
        {
            var initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            return Json(new { NumeroCuentas = initObject == null ? 0 : (initObject.PRTGLOB == null ? 0 : (initObject.PRTGLOB.ctaclie == null ? 0 : initObject.PRTGLOB.ctaclie.Length)) }, JsonRequestBehavior.AllowGet);
        }

        /// Metodos sistema FrontEnd
        ///
        /// Author:     Marco Antonio Orellana O. (marco.orellana@xemantics.com)
        ///             
        /// Fecha:      2015-10-21
        ///
        public JsonResult DataObject(String iO)
        {
            if (iO == null)
            {
                var initObject = xgpyService.AdminParticipantesInit();
                Session[SessionKeys.AdminParty.PartySessionKey] = initObject;
                return Json(initObject);
            }
            else
            {
                if (String.IsNullOrEmpty(iO))
                {
                    var iObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

                    if (iObject == null)
                        iObject = xgpyService.AdminParticipantesInit();

                    return Json(iObject);
                }
                else
                {
                    JObject o = JObject.Parse(iO);
                    InitializationObject initObject = o.ToObject<InitializationObject>();
                    InitializationObject iObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
                    if (iObject == null)
                    {
                        iObject = initObject;
                    }
                    else
                    {
                        iObject.PRTGLOB = initObject.PRTGLOB;
                        iObject.PRTYENT = initObject.PRTYENT;
                        iObject.MODWS = initObject.MODWS;
                    }

                    Session[SessionKeys.AdminParty.PartySessionKey] = iObject;
                    return Json(iObject);
                }
            }
        }

        public JsonResult DataObjectSave(String iO, string idParty)
        {
            if (iO == null)
            {
                var initObject = xgpyService.AdminParticipantesInit();
                Session[SessionKeys.AdminParty.PartySessionKey] = initObject;
                return Json(initObject);
            }
            else
            {
                XgpyResult xgpyResult = new XgpyResult(); //Instanciamos el retorno del guardado
                xgpyResult.ErrorCode = 0;
                xgpyResult.Message = "";

                if (String.IsNullOrEmpty(iO))
                {
                    var iObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

                    if (iObject == null)
                        iObject = xgpyService.AdminParticipantesInit();

                    return Json(iObject);
                }
                else
                {
                    JObject o = JObject.Parse(iO);
                    InitializationObject initObject = o.ToObject<InitializationObject>();
                    InitializationObject iObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

                    if (iObject == null)
                        iObject = initObject;
                    else
                    {
                        iObject.PRTGLOB = initObject.PRTGLOB;
                        iObject.PRTYENT = initObject.PRTYENT;
                        iObject.MODWS = initObject.MODWS;

                        string mensajeError = string.Empty;
                        string res = GrabarParticipanteRazonesSociales(initObject, idParty, ref mensajeError);

                        if (res != "OK")
                        {
                            xgpyResult.ErrorCode = 11000;
                            xgpyResult.Message = mensajeError;
                            xgpyResult.iObject = iObject;
                            Session[SessionKeys.AdminParty.PartySessionKey] = iObject;
                            return Json(xgpyResult);
                        }
                        else
                        {
                            for (int i = 0; i < initObject.PRTGLOB.nom.Length; i++)
                                initObject.PRTGLOB.nom[i].estado = T_PRTGLOB.modificado;

                            for (int i = 0; i < iObject.PRTGLOB.nom.Length; i++)
                                iObject.PRTGLOB.nom[i].estado = T_PRTGLOB.modificado;
                        }
                    }

                    Session[SessionKeys.AdminParty.PartySessionKey] = iObject;
                    xgpyResult.iObject = iObject;
                    return Json(xgpyResult);
                }
            }
        }

        public JsonResult DataObjectSaveAddress(String iO, string idParty)
        {
            if (iO == null)
            {
                var initObject = xgpyService.AdminParticipantesInit();
                Session[SessionKeys.AdminParty.PartySessionKey] = initObject;
                return Json(initObject);
            }
            else
            {
                XgpyResult xgpyResult = new XgpyResult(); //Instanciamos el retorno del guardado
                xgpyResult.ErrorCode = 0;
                xgpyResult.Message = "";

                if (String.IsNullOrEmpty(iO))
                {
                    var iObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

                    if (iObject == null)
                        iObject = xgpyService.AdminParticipantesInit();

                    return Json(iObject);
                }
                else
                {
                    JObject o = JObject.Parse(iO);
                    InitializationObject initObject = o.ToObject<InitializationObject>();
                    InitializationObject iObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

                    if (iObject == null)
                        iObject = initObject;
                    else
                    {
                        iObject.PRTGLOB = initObject.PRTGLOB;
                        iObject.PRTYENT = initObject.PRTYENT;
                        iObject.MODWS = initObject.MODWS;

                        string mensajeError = string.Empty;
                        string res = GrabarParticipanteDirecciones(initObject, idParty, ref mensajeError);

                        if (res != "OK")
                        {
                            xgpyResult.ErrorCode = 11000;
                            xgpyResult.Message = mensajeError;
                            xgpyResult.iObject = iObject;
                            Session[SessionKeys.AdminParty.PartySessionKey] = iObject;
                            return Json(xgpyResult);
                        }
                        else
                        {
                            for (int i = 0; i < initObject.PRTGLOB.direc.Length; i++)
                                initObject.PRTGLOB.direc[i].estado = T_PRTGLOB.modificado;

                            for (int i = 0; i < iObject.PRTGLOB.direc.Length; i++)
                                iObject.PRTGLOB.direc[i].estado = T_PRTGLOB.modificado;
                        }
                    }

                    Session[SessionKeys.AdminParty.PartySessionKey] = iObject;
                    xgpyResult.iObject = iObject;
                    return Json(xgpyResult);
                }
            }
        }

        public JsonResult ReloadSessionObject()
        {
            this.InitObject = null;
            return Json(new { success = true });
        }

        public JsonResult GetDireccionServer(string rut)
        {
            var data = xgpyService.ConsultarDireccionPorRut(rut);
            var dbSelectLoc = xgpyService.Sgt_Loc_S01_MS().OrderBy(x => x.loc_locnom).ToList();
            string comuna = string.Empty;
            string region = string.Empty;
            ObtenerCodigoComuna(dbSelectLoc, data[1], ref comuna, ref region);
            data[1] = comuna;
            data[2] = region;
            // agregamos el país
            data.Add(ObtenerNombrePais(xgpyService.Sgt_Pai_S02_MS(), (string.IsNullOrEmpty(data[4]) ? 997 : Convert.ToInt32(data[4]))));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDireccionWebServer(string rut)
        {
            var data = xgpyService.ObtenerDatosWebService(rut);
            string comuna = string.Empty;
            string region = string.Empty;

            if (data == null)
                data = new T_PRTGLOB();
            else
            {
                var dbSelectLoc = xgpyService.Sgt_Loc_S01_MS().OrderBy(x => x.loc_locnom).ToList();
                ObtenerCodigoComuna(dbSelectLoc, data.direc[0].comuna, ref comuna, ref region);
                // la variable comuna obtiene el código de la comuna no el nombre, la region obtiene el nombre
                data.direc[0].CodComuna = string.IsNullOrEmpty(comuna) ? 0 : Convert.ToInt32(comuna);
                data.direc[0].region = region;
                // agregamos el país
                data.direc[0].pais = ObtenerNombrePais(xgpyService.Sgt_Pai_S02_MS(), data.direc[0].CodPais);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public string ObtenerNombrePais(IList<sgt_pai_s02_MS_Result> listaPaises, int codPais)
        {
            using (Tracer tracer = new Tracer())
            {
                try
                {
                    sgt_pai_s02_MS_Result pais = listaPaises.Where(x => ((int)x.pai_paicod) == codPais).FirstOrDefault();
                    return string.IsNullOrEmpty(pais.pai_painom) ? "CHILE" : pais.pai_painom;
                }
                catch (Exception ex)
                {
                    tracer.TraceException(string.Format("Error: No se encontró el nombre del país asociado al código {0}", codPais), ex);
                    return HttpUtility.HtmlEncode("CHILE"); // dejamos por defecto chile
                }
            }
        }

        public void ObtenerCodigoComuna(List<sgt_loc_s01_MS_Result> listaComunas, string nombreComuna, ref string codComuna, ref string codRegion)
        {
            foreach (var item in listaComunas)
            {
                if (item.loc_locnom == nombreComuna.ToUpper())
                {
                    codComuna = item.loc_loccod.ToString();
                    codRegion = ObtenerCodigoRegion(item.loc_locreg.ToString());
                }
            }
            var comunaSantiago = "COMUNA " + nombreComuna.ToUpper();
            foreach (var item in listaComunas)
            {
                if (item.loc_locnom == comunaSantiago)
                {
                    codComuna = item.loc_loccod.ToString();
                    codRegion = ObtenerCodigoRegion(item.loc_locreg.ToString());
                }
            }
        }

        public string ObtenerCodigoRegion(string codigoRegion)
        {
            string[] nums = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
            string[] regiones = { "PRIMERA", "SEGUNDA", "TERCERA", "CUARTA", "QUINTA", "SEXTA", "SEPTIMA", "OCTAVA", "NOVENA", "DECIMA", "UNDECIMA", "DUODECIMA", "METROPOLITANA" };
            int puntero = nums.Contains(codigoRegion) ? Array.FindIndex(nums, i => i == codigoRegion) : 12;
            return regiones[puntero];
        }

        public JsonResult getCurrentUICuentasData()
        {
            var initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            return Json(initObject.PRTGLOB.ctaclie, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIfHasRazonesSociales(string key)
        {
            int estado = xgpyService.Sce_Rsa_S07_MS(key).Count == 0 ? (int)T_PRTGLOB.nuevo : (int)T_PRTGLOB.modificado;
            return Json(new { Estado = estado }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIfHasDirecciones(string key)
        {
            int estado = xgpyService.Sce_Dad_S08_MS(key).Count == 0 ? (int)T_PRTGLOB.nuevo : (int)T_PRTGLOB.modificado;
            return Json(new { Estado = estado }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPreviousRazonSocial(string key)
        {
            var rzsocs = xgpyService.Sce_Rsa_S07_MS(key);

            if (rzsocs.Count > 0)
            {
                var rzscs = rzsocs.First();
                return Json(new { Nombre = rzscs.razon_soci, Fantasia = rzscs.nom_fantas, SortKey = rzscs.sortkey }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Nombre = string.Empty, Fantasia = string.Empty, SortKey = string.Empty }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPreviousDireccion(string key)
        {
            var direcciones = xgpyService.Sce_Dad_S08_MS(key);

            if (direcciones.Count > 0)
            {
                var dir = direcciones.First();

                return Json(new
                {
                    Direccion = dir.direccion,
                    Comuna = dir.comuna,
                    CodComuna = dir.cod_comuna,
                    CodPostal = dir.cod_postal,
                    Region = dir.estado,
                    Ciudad = dir.ciudad,
                    Pais = dir.pais,
                    CodPais = dir.cod_pais,
                    Telefono = dir.telefono,
                    Fax = dir.fax,
                    Telex = dir.telex,
                    CasPostal = dir.cas_postal,
                    CasBanco = dir.cas_banco,
                    EnviarA = dir.envio_sce,
                    Recibe = dir.recibe_sce,
                    Email = dir.email,
                    Borrado = dir.borrado
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Direccion = string.Empty,
                Comuna = string.Empty,
                CodComuna = 0,
                CodPostal = string.Empty,
                Region = string.Empty,
                Ciudad = string.Empty,
                Pais = string.Empty,
                CodPais = 0,
                Telefono = string.Empty,
                Fax = string.Empty,
                Telex = string.Empty,
                CasPostal = string.Empty,
                CasBanco = string.Empty,
                EnviarA = 0,
                Recibe = 0,
                Email = string.Empty,
                Borrado = "0"
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCurrentUITipClieData()
        {
            var initObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];
            return Json(initObject.PrtEnt07.prtcliente, JsonRequestBehavior.AllowGet);
        }

        public void ActualizarRazonSocial(string idparty)
        {

        }

        //INICIO MODIFICACION CNC - ACCENTURE
        public ActionResult ClasificacionCliente()
        {
            try
            {
                //ModelState.Clear();
                this.InitObject.Mdi_Principal.MESSAGES.Clear();
                //InitObject = (InitializationObject)Session[SessionKeys.AdminParty.PartySessionKey];

                if (InitObject == null)
                {
                    return Redirect("~/AdminParticipantes");
                }

                foreach (var mensaje in InitObject.PRTGLOB.Party.Mensajes)
                {
                    this.InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = mensaje,
                        Type = mensaje.Contains("Functional") ? TipoMensaje.Informacion : TipoMensaje.Error,
                        AutoClose = true,
                        Title = mensaje.Contains("Functional") ? "Advertencia" : "Problema con Web Services BCH"
                    });
                }

                ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
                var rut = MODWS.ExtraeRut(InitObject.PRTGLOB.Party.rut);
                if (rut == "")
                {
                    ClasificacionClienteViewModel cc = new ClasificacionClienteViewModel(this.InitObject.PrtEnt14, this.InitObject);
                    return View(cc);
                }
                else
                {
                    string credU = "";
                    string credP = "";
                    string kDec = "";

                    //obtener usuario y contraseña
                    var varCre = this.xgpyService.ObtenerCredClasifCliente();
                    if (varCre != null && varCre.Count() > 0)
                    {
                        credU = varCre.ElementAt(0).trans_vlr_parametro;
                        credP = varCre.ElementAt(1).trans_vlr_parametro;
                        kDec = varCre.ElementAt(1).kDec;
                    }


                    var result = this.xgpyService.ObtenerDatosClasificacionCliente(rut, credU, credP, kDec);
                    List<string> detalle = new List<string>();
                    var evalRiesgo = this.xgpyService.ObtenerDetalleClasifCliente(1, " ", result.ElementAt(3).Trim());
                    if (evalRiesgo != null && evalRiesgo.Count() > 0)
                    {
                        detalle.Add(evalRiesgo.ElementAt(0).cod_campo);
                    }
                    else
                    {
                        detalle.Add("Sin Información");
                    }
                    var tpoCliNorm = this.xgpyService.ObtenerDetalleClasifCliente(2, " ", result.ElementAt(4).Trim());
                    if (tpoCliNorm != null && tpoCliNorm.Count() > 0)
                    {
                        detalle.Add(tpoCliNorm.ElementAt(0).cod_campo);
                    }
                    else
                    {
                        detalle.Add("Sin Información");
                    }
                    var compInst = this.xgpyService.ObtenerDetalleClasifCliente(3, " ", result.ElementAt(5).Trim());
                    if (compInst != null && compInst.Count() > 0)
                    {
                        detalle.Add(compInst.ElementAt(0).des_campo);
                    }
                    else
                    {
                        detalle.Add("Sin Información");
                    }

                    var tblClfRiesgo = this.xgpyService.Sgt_Clf_S01_MS();
                    ClasificacionClienteViewModel cc = new ClasificacionClienteViewModel(result, detalle, tblClfRiesgo, this.InitObject.PrtEnt14, this.InitObject);
                    return View(cc);
                }

            }
            catch
            {
                this.InitObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Text = "Problema con Web Services BCH",
                    Type = TipoMensaje.Error,
                    Title = "Error "
                });

                ClasificacionClienteViewModel cc = new ClasificacionClienteViewModel(null, null, null, this.InitObject.PrtEnt14, this.InitObject);
                return View(cc);
            }

        }

        public string GrabarClasificacionCliente(InitializationObject iObject, InitializationObject objectSesion)
        {
            // Evitamos que al tener un rut vacío, se guarde con ceros.
            if (!string.IsNullOrWhiteSpace(iObject.PRTGLOB.Party.rut))
            {
                // En caso de venir un rut, lo limpiamos, rellenamos con ceros y dejamos la posible k en mayúscula.
                iObject.PRTGLOB.Party.rut = iObject.PRTGLOB.Party.rut.ToUpper().Trim().PadLeft(10, '0');
            }
            else
            {
                iObject.PRTGLOB.Party.rut = string.Empty;
            }

            List<string> lista = new List<string>();
            string clasif_riesgo;
            string act_econ;
            string eval_riesgo;
            string compos_inst;
            string tpo_clte_norm;

            //Se obtiene el texto de los campos
            clasif_riesgo = objectSesion.PrtEnt14.txClasiRiesgo.Text;
            act_econ = objectSesion.PrtEnt14.txActividadEconomica.Text;
            eval_riesgo = objectSesion.PrtEnt14.txTipoEvaluRiesgo.Text;
            compos_inst = objectSesion.PrtEnt14.txComposicionInstitucional.Text;
            tpo_clte_norm = objectSesion.PrtEnt14.txTipoClienteNormativo.Text;

            //Se obtiene el codigo de los campos
            clasif_riesgo = (clasif_riesgo.IndexOf(" - ") >= 0) ? clasif_riesgo = clasif_riesgo.Substring(0, clasif_riesgo.IndexOf(" - ")) : clasif_riesgo = " ";
            act_econ = (act_econ.IndexOf(" - ") >= 0) ? act_econ = act_econ.Substring(0, act_econ.IndexOf(" - ")) : act_econ = " ";
            eval_riesgo = (eval_riesgo.IndexOf(" - ") >= 0) ? eval_riesgo = eval_riesgo.Substring(0, eval_riesgo.IndexOf(" - ")) : eval_riesgo = " ";
            compos_inst = (compos_inst.IndexOf(" - ") >= 0) ? compos_inst = compos_inst.Substring(0, compos_inst.IndexOf(" - ")) : compos_inst = " ";
            tpo_clte_norm = (tpo_clte_norm.IndexOf(" - ") >= 0) ? tpo_clte_norm = tpo_clte_norm.Substring(0, tpo_clte_norm.IndexOf(" - ")) : tpo_clte_norm = " ";

            //formatear codigos
            List<string> list_codigo = new List<string>();
            list_codigo.Add(clasif_riesgo.Trim());
            list_codigo.Add(act_econ.Trim());
            list_codigo.Add(eval_riesgo.Trim());
            list_codigo.Add(compos_inst.Trim());
            list_codigo.Add(tpo_clte_norm.Trim());

            for (int i = 0; i < list_codigo.Count(); i++)
            {
                if (list_codigo.ElementAt(i).Equals("Sin Información"))
                {
                    lista.Add("");
                }
                else
                {
                    lista.Add(list_codigo.ElementAt(i));
                }
            }

            iObject.PRTGLOB.Party.rut = FormatUtils.TryFormatearRutParticipante(iObject.PRTGLOB.Party.rut);
            var result_w01 = xgpyService.sce_cla_cliente_w01(iObject.PRTGLOB.Party.rut, lista.ElementAt(0).ToString(), lista.ElementAt(1).ToString(), lista.ElementAt(2).ToString(), lista.ElementAt(3).ToString(), lista.ElementAt(4).ToString());
            return result_w01;

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ClasificacionCliente(string Command)
        {
            return Rutear(() =>
            {
                //limpio el modelstate para que use los valoresde InitObj, no los del post

                ModelState.Clear();
                ViewBag.MdiPrincipal = InitObject.Mdi_Principal;
                this.InitObject.Mdi_Principal.MESSAGES.Clear();

                if (Command == "Aceptar")
                {
                    InitObject.PaginaWebQueAbrir = "Index";
                }
                else if (Command == "Cancelar")
                {
                    InitObject.PaginaWebQueAbrir = "Index";
                }
            },
            () =>
            {
                var cc = new ClasificacionClienteViewModel(InitObject.PrtEnt14, this.InitObject);
                return View(cc);
            });
        }

        //FIN MODIFICACION CNC - ACCENTURE
    }
}