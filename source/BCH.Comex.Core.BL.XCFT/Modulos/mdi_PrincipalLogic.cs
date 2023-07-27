using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Forms;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.Domain;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public class mdi_PrincipalLogic
    {
        private enum QUE_LISTA
        {
            Lt_CI,
            Lt_CVE,
            Lt_CVI
        }

        /// <summary>
        /// Inicializaciones antes del show del formulario de Destino de Fondos
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="unit"></param>
        /// <param name="volverADefinir"></param>
        public static void Destino_Fondos1_2(InitializationObject initObj, UnitOfWorkCext01 unit, bool hayMensaje, bool respuestaMensaje)
        {
            UI_Mdi_Principal Mdi_Principal = initObj.Mdi_Principal;
            T_MODXVIA MODXVIA = initObj.MODXVIA;
            T_MODGCVD MODGCVD = initObj.MODGCVD;
            T_MODXORI MODXORI = initObj.MODXORI;
            T_ModChVrf ModChVrf = initObj.ModChVrf;
            T_MODGPLI1 MODGPLI1 = initObj.MODGPLI1;
            T_MODGSWF MODGSWF = initObj.MODGSWF;
            T_MODGCHQ MODGCHQ = initObj.MODGCHQ;
            T_Module1 Module1 = initObj.Module1;
            T_MODGMTA MODGMTA = initObj.MODGMTA;
            T_MODGSCE MODGSCE = initObj.MODGSCE;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObj.Mdl_Funciones_Varias;

            if (VB6Helpers.UBound(MODXVIA.VxVia) == -1)
            {
                initObj.Frm_Destino_Fondos = null;
            }

            if (initObj.Frm_Destino_Fondos == null)
            {
                short orig = 0;
                short x = 0;
                //Destino de Fondos.-
                if (VB6Helpers.UBound(MODXVIA.VxVia) >= 0)
                {
                    if (!hayMensaje)
                    {
                        //initObj.Frm_Destino_Fondos.MostrarVolverADefinir = true; //muestro popup
                        initObj.FormularioQueAbrir = "DefinirNuevosDestinos";
                        return;
                    }
                    else
                    {
                        if (respuestaMensaje)
                        {

                        }
                        else
                        {
                            initObj.FormularioQueAbrir = "Index";
                            return;
                        }
                    }

                }
                initObj.Frm_Destino_Fondos = new UI_Frm_Destino_Fondos();
                initObj.Frm_Destino_Fondos.MostrarVolverADefinir = false;
                MODGCVD.VgCvd.Etapa = "VIAORI";
                ModChVrf.VgChV = new T_Chv[0];
                ModChVrf.VPlnChV = new T_PlnChV[0];
                BCH.Comex.Core.BL.XCFT.Modulos.ModChVrf.LimpiaVPlis(MODGPLI1, "E");
                MODXORI.Vx_SCodTran = new S_Codtran[0];
                orig = 0;
                orig = (short)VB6Helpers.UBound(MODXORI.VxOri);

                if (orig >= 0)
                {
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Informacion,
                        Text = "El origen de los fondos ya se encuentra definido, al definir las vias deberá volver a definir los origenes."
                    });
                    MODGCVD.VgCvd.Etapa += "ORI";
                    BCH.Comex.Core.BL.XCFT.Modulos.ModChVrf.LimpiaVPlis(MODGPLI1, "I");
                }

                BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Pr_Init_xVia(MODXVIA, MODGSWF, MODGCHQ);
                BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Pr_Init_xOri(MODXORI);
                Mdi_Principal.BUTTONS["tbr_origfondos"].Enabled = false;
                x = BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.Llena_VxMtoVia(initObj, unit);
                if (~x != 0)
                {
                    return;
                }
                if (BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.HayVxVia(MODXVIA) != 0)
                {
                    MODXVIA.VgxVia.Partys = VB6Helpers.Str(T_MODGCVD.ICli);
                    if (!string.IsNullOrEmpty(Module1.PartysOpe[T_MODGCVD.IOtr].NombreUsado))
                    {
                        MODXVIA.VgxVia.Partys += VB6Helpers.Str(T_MODGCVD.IOtr);
                    }

                    MODXVIA.VgxVia.Vuelto = (short)(false ? -1 : 0);
                    initObj.FormularioQueAbrir = String.Empty;
                }
                else
                {
                    initObj.FormularioQueAbrir = "Index";
                }
            }
            else
            {
                initObj.FormularioQueAbrir = String.Empty;
            }
            return;
        }

        /// <summary>
        /// Acciones despues de cerrar el formulario de Destino de fondos
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="unit"></param>
        /// <param name="volverADefinir"></param>
        public static void Destino_Fondos2_2(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            UI_Mdi_Principal Mdi_Principal = initObj.Mdi_Principal;
            T_MODXVIA MODXVIA = initObj.MODXVIA;
            T_MODGCVD MODGCVD = initObj.MODGCVD;
            T_MODXORI MODXORI = initObj.MODXORI;
            T_ModChVrf ModChVrf = initObj.ModChVrf;
            T_MODGPLI1 MODGPLI1 = initObj.MODGPLI1;
            T_MODGSWF MODGSWF = initObj.MODGSWF;
            T_MODGCHQ MODGCHQ = initObj.MODGCHQ;
            T_Module1 Module1 = initObj.Module1;
            T_MODGMTA MODGMTA = initObj.MODGMTA;
            T_MODGSCE MODGSCE = initObj.MODGSCE;
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObj.Mdl_Funciones_Varias;

            short i = 0;
            short nc = 0;
            short j = 0;
            short a = 0;

            initObj.Frm_Destino_Fondos = null;
            if (~MODXVIA.VgxVia.Acepto != 0)
            {
                return;
            }

            for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxVia); i++)
            {
                if (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CHVRF && MODXVIA.VxVia[i].Status != T_MODXVIA.ExVia_Eli)
                {
                    BCH.Comex.Core.BL.XCFT.Modulos.ModChVrf.SumaPlnInv(initObj, unit, T_MODGCVD.ICli, T_MODGPLI1.EPli_Emi);
                    ModChVrf.VPlnChV = new T_PlnChV[1];
                    ModChVrf.VgChV = new T_Chv[1];
                    break;
                }

            }

            MODGCVD.VgCvd.Etapa = MODGPYF0.Componer(MODGCVD.VgCvd.Etapa, "VIA", "");

            if (BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.TotalChq(MODXVIA) > 0)
            {
                MODGCVD.VgCvd.Etapa += "DVS";
                Mdi_Principal.BUTTONS["tbr_Gchq"].Enabled = true;
                nc = BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.TotalChq(MODXVIA);
                //Deshabilita el boton del Impuesto en caso de que el flag este en 0 ------
                if (T_MODGMTA.impflag == 1)
                {
                    j = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Put_Gcom(Mdl_Funciones_Varias, MODGSCE, Mdi_Principal, 0, "Impuesto de Cheques", "$", MODGSCE.VGen.MtoDeb * nc, 0, 1, "FIJO$");
                }

                //-----------------------------------------------------------------------------------------------
            }
            else
            {
                Mdi_Principal.BUTTONS["tbr_Gchq"].Enabled = false;
            }

            if (BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.GetSwifts(MODXVIA).Any())
            {
                MODGCVD.VgCvd.Etapa += "SWF";
                Mdi_Principal.BUTTONS["tbr_Swift"].Enabled = true;
            }
            else
            {
                Mdi_Principal.BUTTONS["tbr_Swift"].Enabled = false;
            }

            Mdi_Principal.BUTTONS["tbr_origfondos"].Enabled = true;
            MODGCVD.VgCvd.Etapa += "ORI";

            a = 0;
            a = (short)MODGPLI1.Vplis.Length;
            // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            // IGNORED: On Error GoTo 0

            if (a > 0)
            {
                Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = true;
            }
            else
            {
                Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = false;
            }
            initObj.FormularioQueAbrir = "Index";

            // set foco
            var foco = "tbr_origfondos";//Mdi_Principal.BUTTONS["tbr_Gchq"].Enabled ? "tbr_Gchq" : Mdi_Principal.BUTTONS["tbr_Swift"].Enabled ? "tbr_Swift" : "tbr_origfondos";
            initObj.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { initObj.Mdi_Principal.BUTTONS[key].Focus = (key == foco); });

            return;
        }

        public static void NuevaOperacion(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            //valido inicio y fin de dia
            MODGUSR.SyGetf_Usr(initObj.MODGUSR, initObj.Mdi_Principal, unit,
                VB6Helpers.Left(initObj.Usuario.Identificacion_CCtUsr, 3),
                VB6Helpers.Right(initObj.Usuario.Identificacion_CCtUsr, 2), "I");

            short x = ObjetosToVar(initObj);
            if (MODGCVD.HayDiferencia(initObj.MODGCVD) != 0)
            {
                //ver que hacer cuando hay una operacion en curso
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {

                });
            }
            initObj.MODXORI.Vx_SCodTran = new S_Codtran[0];
            MODCVDIM.Inicializa(initObj.MODCVDIM, initObj.MODGFYS, initObj.MODGASO, initObj.MODCVDIMMM, -1);

            if (NuevaCVD_I(initObj, unit, "NuevaOperacion_DesdeSeleccionOficina")) //si no redirecciono a seleccion de oficina
            {
                initObj.ModChVrf.VPlnChV = new T_PlnChV[0];
                initObj.ModChVrf.VgChV = new T_Chv[0];

                //Se limpia matriz para solucionar problema sce_plan
                initObj.MODPREEM.Vx_PReem = new Plan_Reemp[1] { new Plan_Reemp() };
                initObj.FormularioQueAbrir = "Index";
            }
        }

        public static void NuevaOperacion_DesdeSeleccionOficina(InitializationObject Modulos, UnitOfWorkCext01 unit)
        {
            if (NuevaCVD_I(Modulos, unit, "NuevaOperacion_DesdeSeleccionOficina")) //si no redirecciono a seleccion de oficina
            {
                Modulos.ModChVrf.VPlnChV = new T_PlnChV[0];
                Modulos.ModChVrf.VgChV = new T_Chv[0];

                //Se limpia matriz para solucionar problema sce_plan
                Modulos.MODPREEM.Vx_PReem = new Plan_Reemp[1] { new Plan_Reemp() };
                Modulos.FormularioQueAbrir = "Index";
            }
        }

        public static void Participantes(InitializationObject modulos, UnitOfWorkCext01 uow)
        {
            short NumParty = 0;

            modulos.MODGCVD.BotPrt = (short)(true ? -1 : 0); //setearlo a true

            NumParty = Module1.GetParty1_2(modulos.MODGCVD.Beneficiario, -1, 0, modulos, uow);

            if (NumParty != 0)
            {
                modulos.MODGCVD.VgCvd.IndPrt = T_MODGCVD.ICli;
                modulos.MODGCVD.VgCvd.PrtCli = modulos.Module1.PartysOpe[T_MODGCVD.ICli].LlaveArchivo;
                modulos.MODGCVD.VgCvd.rutcli = modulos.Module1.PartysOpe[T_MODGCVD.ICli].rut;

                modulos.Frm_Principal.Tx_NomPrt.Text = modulos.Module1.PartysOpe[T_MODGCVD.ICli].NombreUsado;
            }

            if (modulos.MODGCVD.COMISION == false)
            {
                if (modulos.MODGCVD.VgCvd.TipCVD != T_MODGCVD.TCvd_PlnoBco)
                {
                    if (!string.IsNullOrEmpty(modulos.MODGCVD.VgCvd.PrtCli))
                    {
                        modulos.Mdi_Principal.BUTTONS["tbr_Comercio_invisible"].Enabled = true;
                        modulos.Mdi_Principal.BUTTONS["tbr_vtas_export"].Enabled = true;
                        modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = true;

                        if (modulos.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
                        {
                            modulos.Mdi_Principal.BUTTONS["tbr_arbitrajes"].Enabled = true;
                        }

                    }
                    else
                    {
                        modulos.Mdi_Principal.BUTTONS["tbr_Comercio_invisible"].Enabled = false;
                        modulos.Mdi_Principal.BUTTONS["tbr_arbitrajes"].Enabled = false;
                        modulos.Mdi_Principal.BUTTONS["tbr_vtas_export"].Enabled = false;
                        modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                        modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                    }

                }

            }

        }

        /// <summary>
        /// Se invoca al abrir participantes
        /// </summary>
        /// <param name="modulos"></param>
        /// <param name="uow"></param>
        public static void Participantes1_2(InitializationObject modulos, UnitOfWorkCext01 uow, bool? ultimaOperacionEsCosmos = false)
        {
            short NumParty = 0;

            modulos.MODGCVD.BotPrt = (short)(true ? -1 : 0); //setearlo a true

            NumParty = Module1.GetParty1_2(modulos.MODGCVD.Beneficiario, -1, 0, modulos, uow, ultimaOperacionEsCosmos);

        }

        /// <summary>
        /// Se invoca luego que se cierra la ventana de participantes
        /// </summary>
        /// <param name="modulos"></param>
        /// <param name="uow"></param>
        public static void Participantes2_2(InitializationObject modulos, UnitOfWorkCext01 uow)
        {
            short NumParty = 0;

            NumParty = Module1.GetParty2_2(modulos.MODGCVD.Beneficiario, -1, 0, modulos, uow);

            Participantes2_3(modulos, NumParty != 0);
            if (modulos.Frm_Participantes.Aceptar.Tag.ToString() != "0")
            {
                modulos.Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = true;
                modulos.Mdi_Principal.BUTTONS["tbr_comisiones"].Enabled = true;
                modulos.Mdi_Principal.BUTTONS["tbr_nota"].Enabled = true;
            }
        }

        public static void Participantes2_3(InitializationObject modulos, bool NumParty)
        {
            if (NumParty)
            {
                modulos.MODGCVD.VgCvd.IndPrt = T_MODGCVD.ICli;
                modulos.MODGCVD.VgCvd.PrtCli = modulos.Module1.PartysOpe[T_MODGCVD.ICli].LlaveArchivo;
                modulos.MODGCVD.VgCvd.rutcli = modulos.Module1.PartysOpe[T_MODGCVD.ICli].rut;

                modulos.Frm_Principal.Tx_NomPrt.Text = modulos.Module1.PartysOpe[T_MODGCVD.ICli].NombreUsado;
            }

            if (modulos.MODGCVD.COMISION == false)
            {
                if (modulos.MODGCVD.VgCvd.TipCVD != T_MODGCVD.TCvd_PlnoBco)
                {
                    if (!string.IsNullOrEmpty(modulos.MODGCVD.VgCvd.PrtCli))
                    {
                        modulos.Mdi_Principal.BUTTONS["tbr_Comercio_invisible"].Enabled = true;
                        modulos.Mdi_Principal.BUTTONS["tbr_vtas_export"].Enabled = true;
                        modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = true;

                        if (modulos.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
                        {
                            modulos.Mdi_Principal.BUTTONS["tbr_arbitrajes"].Enabled = true;
                        }
                    }
                    else
                    {
                        modulos.Mdi_Principal.BUTTONS["tbr_Comercio_invisible"].Enabled = false;
                        modulos.Mdi_Principal.BUTTONS["tbr_arbitrajes"].Enabled = false;
                        modulos.Mdi_Principal.BUTTONS["tbr_vtas_export"].Enabled = false;
                        modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                        modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                        modulos.Frm_Participantes.LstPartys.ListIndex = 0;
                        modulos.Frm_Participantes.Tx_Dir.Text = "";
                    }

                }
            }
        }

        public static void Opciones_Botones(InitializationObject Modulos, UnitOfWorkCext01 uow, short index)
        {
            short n = 0;
            short a = 0;
            short operac = 0;

            if (string.IsNullOrEmpty(Modulos.MODGCVD.VgCvd.PrtCli))
            {
                Modulos.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Error,
                    Text = "Antes de realizar una operación debe ingresar los Participantes."
                });
                return;
            }
            string nombreView = String.Empty;
            switch (index)
            {
                case 0:
                    if (Modulos.Frm_Comercio_Invisible == null)
                    {
                        Modulos.MODGCVD.VgCvd.TipCVD = T_MODGCVD.TCvd_CVD;
                        Modulos.MODGCVD.VgCvd.Etapa = "CVD";
                        Modulos.Mdi_Principal.BUTTONS["tbr_arbitrajes"].Enabled = false;
                        Modulos.Mdi_Principal.BUTTONS["tbr_vtas_export"].Enabled = false;
                        Modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                        Modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                        Modulos.Frm_Comercio_Invisible = new UI_Frm_Comercio_Invisibles();
                        nombreView = "ComercioInvisible";
                        return;
                    }
                    else
                    {
                        Modulos.Frm_Comercio_Invisible = null;
                        n = MODGCVD.Count_CVD(Modulos.MODGCVD, Modulos.MODGARB);
                        if (((n > 0 ? -1 : 0) & Modulos.MODGCVD.VgCvd.Acepto) != 0)
                        {
                            Modulos.MODGCVD.VgCvd.Etapa = MODGPYF0.Componer(Modulos.MODGCVD.VgCvd.Etapa, "CVD", "");
                            Pr_Cargar_Listas(Modulos, Modulos.MODGCVD.VgCvd.TipCVD, QUE_LISTA.Lt_CI);
                            Modulos.Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled = true;
                            //selecciono el foco en patalla pricipal
                            var foco = Modulos.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1 ? "tbr_dedfondos" : "tbr_comisiones";
                            Modulos.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { Modulos.Mdi_Principal.BUTTONS[key].Focus = (key == foco ? true : false); });
                        }
                        nombreView = "ComercioInvisible";
                    }
                    //Arbitrajes.
                    break;
                case 1:
                    if (Modulos.Frm_Arbitrajes == null)
                    {
                        Modulos.MODGCVD.VgCvd.TipCVD = T_MODGCVD.TCvd_Arb;
                        Modulos.MODGCVD.VgCvd.Etapa = "ARB";
                        Modulos.Mdi_Principal.BUTTONS["tbr_Comercio_invisible"].Enabled = false;
                        Modulos.Mdi_Principal.BUTTONS["tbr_vtas_export"].Enabled = false;
                        Modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                        Modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                        Modulos.Frm_Arbitrajes = new UI_Frm_Arbitrajes();
                        nombreView = "Arbitrajes";
                        Frm_Arbitrajes_Logic.Form_Load(Modulos, uow);
                        return;
                    }
                    else
                    {
                        Modulos.Frm_Arbitrajes = null;
                        n = MODGCVD.Count_CVD(Modulos.MODGCVD, Modulos.MODGARB);
                        if (((n > 0 ? -1 : 0) & Modulos.MODGCVD.VgCvd.Acepto) != 0)
                        {
                            Modulos.MODGCVD.VgCvd.Etapa = MODGPYF0.Componer(Modulos.MODGCVD.VgCvd.Etapa, "ARB", "");
                            Modulos.MODGCVD.VgCvd.Etapa += "VIA";
                            Modulos.MODGCVD.VgCvd.Etapa += "ORI";
                            Pr_Cargar_Listas(Modulos, Modulos.MODGCVD.VgCvd.TipCVD, QUE_LISTA.Lt_CI);
                            Modulos.Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled = true;
                            //selecciono el foco en patalla pricipal
                            Modulos.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { Modulos.Mdi_Principal.BUTTONS[key].Focus = (key == "tbr_comisiones" ? true : false); });
                        }
                        nombreView = "Arbitrajes";
                    }
                    break;
                case 2:  //comercio visible exportaciones
                    if (Modulos.Frm_VisE == null) //primera vez que entro
                    {
                        Modulos.MODGCVD.VgCvd.TipCVD = T_MODGCVD.TCvd_VisExp;
                        Modulos.MODGCVD.VgCvd.Etapa = "VTA";
                        Modulos.Mdi_Principal.BUTTONS["tbr_Comercio_invisible"].Enabled = false;
                        Modulos.Mdi_Principal.BUTTONS["tbr_arbitrajes"].Enabled = false;
                        Modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                        Modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                        nombreView = "ComercioVisibleExport";
                        Modulos.Frm_VisE = new UI_Frm_VisE();
                        return;
                    }
                    else
                    {
                        if (Modulos.MODXPLN0.VxDatP.Acepto != 0)
                        {
                            Modulos.MODXPLN0.VxDatP.PrtExp = Modulos.MODGCVD.VgCvd.PrtCli;
                            if (Modulos.FrmxPln0 == null)
                            {
                                Modulos.FormularioQueAbrir = "PlanillaVisibleExport";
                                //Modulos.FrmxPln0 = new UI_FrmxPln0();
                                return;
                            }
                            else
                            {
                                //cierro a los dos aca porque ya se que no los voy a precisar
                                Modulos.FrmxPln0 = null;
                                Modulos.Frm_VisE = null;
                                if (Modulos.MODXPLN0.VxDatP.Acepto != 0)
                                {
                                    Pr_Cargar_Listas(Modulos, Modulos.MODGCVD.VgCvd.TipCVD, QUE_LISTA.Lt_CVE);
                                    Modulos.MODGCVD.VgCvd.Etapa = MODGPYF0.Componer(Modulos.MODGCVD.VgCvd.Etapa, "VTA", "");
                                }

                                if (Modulos.MODXPLN0.VxDatP.MtoLiq > 0)
                                {
                                    Modulos.Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled = true;
                                    //selecciono el foco en patalla pricipal
                                    Modulos.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { Modulos.Mdi_Principal.BUTTONS[key].Focus = (key == "tbr_comisiones" ? true : false); });
                                }
                                else
                                {
                                    if (VB6Helpers.UBound(Modulos.MODXPLN1.VxPlvs) >= 0)
                                    {
                                        Modulos.Mdi_Principal.BUTTONS["tbr_planilla2"].Enabled = true;
                                    }
                                }
                            }
                        }
                        nombreView = "PlanillaVisibleExport";
                    }
                    break;
                case 6:
                    if (NuevaCVD_I(Modulos, uow, "PlanillaAnularNumero"))
                    {
                        //----------------------------------------
                        //Realsystems-Código Nuevo-Inicio
                        //Fecha Modificación 20100615
                        //Responsable: Pablo Millan
                        //Versión: 1.0
                        //Descripción : Se genera Transaction ID
                        //----------------------------------------
                        if (string.IsNullOrEmpty(Modulos.Mdl_Funciones_Varias.LC_TRXID_MAN))
                        {
                            Modulos.Mdl_Funciones_Varias.LC_TRXID_MAN = MODGCVD.GeneraTRXID(Modulos.MODGCVD.VgCvd.OpeSin, uow, Modulos.Mdi_Principal.MESSAGES);
                        }

                        //----------------------------------------
                        // RealSystems - Código Nuevo - Termino
                        //----------------------------------------
                        //Realsystems-Código Antiguo-Inicio
                        //----------------------------------------
                        //LC_TRXID_MAN = "CBSCVD" & Format(Date, "yymmdd") & Mid$(VgCvd.OpeSin, 1, 3) & Mid$(VgCvd.OpeSin, 6, 10)
                        //----------------------------------------
                        // RealSystems - Código Antiguo - Termino
                        //----------------------------------------
                        Modulos.MODGCVD.VgCvd.TipCVD = T_MODGCVD.TCvd_PlnoBco;
                        Modulos.MODXANU.VgAnu.codcct = Modulos.MODGCVD.VgCvd.codcct;
                        Modulos.MODXANU.VgAnu.codpro = Modulos.MODGCVD.VgCvd.codpro;
                        Modulos.MODXANU.VgAnu.codesp = Modulos.MODGCVD.VgCvd.codesp;
                        Modulos.MODXANU.VgAnu.codofi = Modulos.MODGCVD.VgCvd.codofi;
                        Modulos.MODXANU.VgAnu.codope = Modulos.MODGCVD.VgCvd.codope;
                    }
                    break;
            }
            //Pr_Cargar_Listas(Modulos, Modulos.MODGCVD.VgCvd.TipCVD, QUE_LISTA.Lt_CVE);
            Modulos.FormularioQueAbrir = "Index";
            bool seguir = CobraComis(Modulos, uow, nombreView);
            if (seguir)
            {
                if (Mdl_Funciones_Varias.TotalComis(Modulos.Mdl_Funciones_Varias) > 0 && !Modulos.Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled)
                {
                    Modulos.Mdi_Principal.BUTTONS["tbr_origfondos"].Enabled = true;
                    Modulos.MODGCVD.VgCvd.Etapa += "ORI";
                }
            }
        }

        public static dynamic Origen_Fondos_1_2(InitializationObject Modulos, UnitOfWorkCext01 unit, bool vieneDeMensaje, bool resMensaje)
        {
            short x = 0;
            short w = 0;
            if (VB6Helpers.UBound(Modulos.MODXORI.VxOri) == -1)
            {
                Modulos.Frm_Origen_Fondos = null;
            }
            //Origen de Fondos.
            if (VB6Helpers.UBound(Modulos.MODXORI.VxOri) >= 0)
            {
                if (!vieneDeMensaje)
                {
                    Modulos.FormularioQueAbrir = "DefinirNuevosOrigenes";
                    return null;
                }
                else
                {
                    if (resMensaje)
                    {

                    }
                    else
                    {
                        Modulos.FormularioQueAbrir = "Index";
                        return null;
                    }
                }
            }
            if (Modulos.Frm_Origen_Fondos == null)
            {
                Modulos.Frm_Origen_Fondos = new UI_Frm_Origen_Fondos();
                Modulos.MODGCVD.VgCvd.Etapa += "ORI";
                Modulos.ModChVrf.VgChV = new T_Chv[0];
                Modulos.ModChVrf.VPlnChV = new T_PlnChV[0];
                ModChVrf.LimpiaVPlis(Modulos.MODGPLI1, "I");
                //Esta accion solo debe realizarce si es cuenta COSMOS
                if (Modulos.MODXORI.gb_esCosmos)
                {
                    //Forma antigua, falla cuando se selecciona una cuenta corriente y despues se modifica los origenes
                    //for (w = 0; w < (short)VB6Helpers.UBound(Modulos.MODXORI.VxOri); w++)
                    //{
                    //    if (Modulos.MODXORI.Vx_SCodTran[w].Via == "ori")
                    //    {
                    //        VB6Helpers.RedimPreserve(ref Modulos.MODXORI.Vx_SCodTran, 0, VB6Helpers.UBound(Modulos.MODXORI.Vx_SCodTran) - 1);
                    //    }

                    //}
                    Modulos.MODXORI.Vx_SCodTran = Modulos.MODXORI.Vx_SCodTran.Where(c => c.Via != "ori").ToArray();
                }
                MODXORI.Pr_Init_xOri(Modulos.MODXORI);
                x = MODGCVD.Llena_VxMtoOri(Modulos, unit);
                if (MODXORI.HayVxOri(Modulos.MODXORI) != 0)
                {
                    Modulos.MODXORI.VgxOri.Partys = VB6Helpers.Str(T_MODGCVD.ICli);
                    if (!String.IsNullOrEmpty(Modulos.Module1.PartysOpe[T_MODGCVD.IOtr].NombreUsado))
                    {
                        Modulos.MODXORI.VgxOri.Partys += VB6Helpers.Str(T_MODGCVD.IOtr);
                    }
                    Modulos.FormularioQueAbrir = String.Empty;
                }
                else
                {
                    Modulos.FormularioQueAbrir = "Index";
                }
            }
            else
            {
                Modulos.FormularioQueAbrir = String.Empty;
            }
            return null;
        }

        public static dynamic Origen_Fondos_2_2(InitializationObject Modulos, UnitOfWorkCext01 unit)
        {
            short i = 0;
            short a = 0;
            short x = 0;
            Modulos.Frm_Origen_Fondos = null;
            //Por defecto habilito el boton grabar
            bool habilitarBotonGrabar = true;
            if (Modulos.MODXORI.VgxOri.Acepto != 0)
            {

                Modulos.MODGCVD.VgCvd.Etapa = MODGPYF0.Componer(Modulos.MODGCVD.VgCvd.Etapa, "ORI", "");

                for (i = 0; i <= (short)VB6Helpers.UBound(Modulos.MODXORI.VxOri); i++)
                {
                    if (Modulos.MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CHVRF && Modulos.MODXORI.VxOri[i].Status != T_MODXORI.ExOri_Eli)
                    {
                        ModChVrf.SumaPlnInv(Modulos, unit, T_MODGCVD.ICli, T_MODGPLI1.EPli_Emi);
                        Modulos.ModChVrf.VPlnChV = new T_PlnChV[1];
                        Modulos.ModChVrf.VgChV = new T_Chv[1];
                        break;
                    }

                }

                short _switchVar2 = Modulos.MODGCVD.VgCvd.TipCVD;
                if (_switchVar2 == T_MODGCVD.TCvd_CVD)
                {
                    Modulos.Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = true;
                }
                else if (_switchVar2 == T_MODGCVD.TCvd_Arb)
                {
                    //x = ;
                    //si CargaPln_Arb false, se produjo un error por lo cual se bloquea el boton grabar
                    if (MODGCVD.CargaPln_Arb(Modulos, unit))
                    {
                        Modulos.Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = true;
                    }
                    else
                    {
                        habilitarBotonGrabar = false;
                    }
                }
                else if (_switchVar2 == T_MODGCVD.TCvd_VisExp)
                {
                    Modulos.Mdi_Principal.BUTTONS["tbr_planilla2"].Enabled = true;
                }
                else if (_switchVar2 == T_MODGCVD.TCvd_Rev)
                {
                    short _switchVar3 = Modulos.MODGCVD.VgCVDo.TipCVD;
                    if (_switchVar3 == T_MODGCVD.TCvd_CVD || _switchVar3 == T_MODGCVD.TCvd_Arb)
                    {
                        Modulos.Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = true;
                    }
                    else if (_switchVar3 == T_MODGCVD.TCvd_VisExp)
                    {
                        Modulos.Mdi_Principal.BUTTONS["tbr_planilla3"].Enabled = true;
                    }

                }
                else if (_switchVar2 == T_MODGCVD.TCvd_RyR)
                {
                    //x = ;
                    if (MODGCVD.CargaPln_Arb(Modulos, unit))
                    {
                        short _switchVar4 = Modulos.MODGCVD.VgCVDo.TipCVD;
                        if (_switchVar4 == T_MODGCVD.TCvd_CVD || _switchVar4 == T_MODGCVD.TCvd_Arb)
                        {
                            Modulos.Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = true;
                        }
                        else if (_switchVar4 == T_MODGCVD.TCvd_VisExp)
                        {
                            Modulos.Mdi_Principal.BUTTONS["tbr_planilla3"].Enabled = true;
                        }
                    }
                    else
                    {
                        habilitarBotonGrabar = false;
                    }

                }

                if (habilitarBotonGrabar)
                {
                    a = 0;

                    a = (short)Modulos.MODGPLI1.Vplis.Length;
                    // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                    // IGNORED: On Error GoTo 0

                    if (a > 0)
                    {
                        Modulos.Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = true;
                    }
                    else
                    {
                        Modulos.Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = false;
                    }

                    //Verifica a donde debe estar el foco
                    var foco = Modulos.MODGCVD.COMISION ? "tbr_grabar" : Modulos.Mdi_Principal.BUTTONS["tbr_vueltos"].Enabled ? "tbr_vueltos" : Modulos.Mdi_Principal.BUTTONS["tbr_Swift"].Enabled ? "tbr_Swift" : Modulos.Mdi_Principal.BUTTONS["tbr_Gchq"].Enabled ? "tbr_Gchq" : "tbr_grabar";
                    Modulos.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { Modulos.Mdi_Principal.BUTTONS[key].Focus = (key == foco ? true : false); });

                
                    Modulos.Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = true;
                }
                else
                {
                    //Si no habilite el boton grabar dejo el foco en el boton nuevo
                    Modulos.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { Modulos.Mdi_Principal.BUTTONS[key].Focus = (key == "tbr_nuevo" ? true : false); });
                }
            }
            Modulos.FormularioQueAbrir = "Index";
            return null;
        }

        public static short Grabar1(InitializationObject Modulos, UnitOfWorkCext01 unit, UnitOfWorkSwift uowSwift)
        {

            using (Tracer trace = new Tracer("GrabarParte1"))
            {
                T_MODGUSR MODGUSR = Modulos.MODGUSR;
                T_MODGCVD MODGCVD = Modulos.MODGCVD;
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = Modulos.Mdl_Funciones_Varias;
                T_MODXORI MODXORI = Modulos.MODXORI;
                T_MODXVIA MODXVIA = Modulos.MODXVIA;
                T_Module1 Module1 = Modulos.Module1;
                T_MODGSWF MODGSWF = Modulos.MODGSWF;

                UI_Mdi_Principal Mdi_Principal = Modulos.Mdi_Principal;
                string Usr = MODGUSR.UsrEsp.CentroCosto + MODGUSR.UsrEsp.Especialista;

                try
                {
                    //********************************************************
                    //Determina Usuario.-
                    //********************************************************
                    if (~Save_Ini(Modulos) != 0)
                    {
                        return 0;
                    }
                    if (~ObjetosToVar(Modulos) != 0)
                    {
                        return 0;
                    }
                    //********************************************************
                    //Validaciones numero de operacion que no sea xxxxx
                    //********************************************************
                    if (MODGCVD.VgCvd.codpro == T_MODGUSR.IdPro_Undefined || MODGCVD.VgCvd.codope == T_MODGUSR.CodOp_Undefined)
                    {
                        trace.AddToContext("Problemas con Codop", MODGCVD.VgCVDNul.codpro + "-" + MODGCVD.VgCVDNul.codope);
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Existe un problema con el número de operación, favor reportar el problema."
                        });
                        return 0;
                    }
                    if (Module1.Codop.Id_Product == T_MODGUSR.IdPro_Undefined || Module1.Codop.Id_Operacion == T_MODGUSR.CodOp_Undefined)
                    {
                        trace.AddToContext("Problemas con Codop", Module1.Codop.Id_Product + "-" + Module1.Codop.Id_Operacion);
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Existe un problema con el número de operación, favor reportar el problema."
                        });
                        return 0;
                    }
                    //********************************************************
                    //Validaciones para ventas visibles de importaciones
                    //********************************************************
                    if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_VisImp)
                    {
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODCVDIMMM.Val_Endo(Modulos, -1) != 0)
                        {
                            return aux_GrabarCorrecto(MODGUSR, Mdi_Principal);
                        }

                        BCH.Comex.Core.BL.XCFT.Modulos.MODCVDIMMM.GenPlAlad(Modulos, unit);
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODCVDIMMM.ValPlan(Modulos) != 0)
                        {
                            MODGCVD.VgCvd.filler = (short)(true ? -1 : 0);
                            return aux_GrabarCorrecto(MODGUSR, Mdi_Principal);
                        }
                    }

                    BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Init(Mdl_Funciones_Varias);
                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODABDC.DesActivaBD(Modulos, unit, MODGCVD.VgCvd.OpeSin, "000000") != 0)
                    {
                        throw new Exception();
                    }


                    //esto es una reingenieria para poder mostrar los tickets
                    if (BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Fn_ExisteCtaCte(Modulos) != 0)
                    {
                        return -1;
                        //cuando retorna true tiene que mostrar los tickets
                    }
                    return 1;
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta, problemas en Grabar1", e);
                    aux_GrabarIncorrecto(Modulos, unit, uowSwift);
                    return 0;
                }
            }
        }

        public static dynamic Grabar(InitializationObject Modulos, UnitOfWorkCext01 unit, UnitOfWorkSwift uowSwift)
        {
            using (var tracer = new Tracer("GrabarParte2"))
            {
                T_MODGUSR MODGUSR = Modulos.MODGUSR;
                T_MODGCVD MODGCVD = Modulos.MODGCVD;
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = Modulos.Mdl_Funciones_Varias;
                T_MODXORI MODXORI = Modulos.MODXORI;
                T_MODXVIA MODXVIA = Modulos.MODXVIA;
                T_Module1 Module1 = Modulos.Module1;
                T_MODGSWF MODGSWF = Modulos.MODGSWF;
                UI_Mdi_Principal Mdi_Principal = Modulos.Mdi_Principal;

                tracer.TraceInformation("Inicio Grabar Op: " + MODGCVD.VgCvd.OpeSin);

                //Se debe limpiar la cola de ejecucion para que se realize un grabado limpio
                Mdl_Funciones_Varias.CmdsQuerysNew.Clear();

                string Usr = MODGUSR.UsrEsp.CentroCosto + MODGUSR.UsrEsp.Especialista;
                short w = 0;
                short i = 0;

                try
                {
                    tracer.AddToContext("Grabar - Save_Doc", "Grabo Documentos");
                    if (~Save_Doc(Modulos, unit, Usr) != 0)
                    {
                        tracer.AddToContext("Save_Doc", "Distinto de Cero");
                        throw new Exception();
                    }
                    tracer.AddToContext("Grabar - Save_Con", "Grabo Contabilidad");
                    if (~Save_Con(Modulos, unit, Usr) != 0)
                    {
                        tracer.AddToContext("Save_Con", "Distinto de Cero");
                        throw new Exception();
                    }
                    tracer.AddToContext("Grabar - Save_Pro", "Grabo Producto de la Operacion");
                    if (~Save_Pro(Modulos, unit) != 0)
                    {
                        tracer.AddToContext("Save_Pro", "Distinto de Cero");
                        throw new Exception();
                    }

                    //Si guardo todo sin problemas, se intenta recuperar el numero de operacion del producto no utilizado
                    BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.RecuperaNumeroOperacionNoUtilizado(Modulos, unit);

                    //SI LA OPERACION ES MANUAL SE DEBE GUARDAR EN LA NUEVA TABLA
                    dynamic Lc_ContaVia = null;
                    dynamic Lc_ContaOri = null;
                    short Lc_Contatot = 0;
                    short IndConta = 0;
                    short j = 0;

                    if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0 && MODXORI.gb_esCosmos == true)
                    {
                        //Se utiliza una lista para trabajar con la lista de origenes y destino. Despues se asigna al arreglo
                        //Mdl_Funciones_Varias.GT_IndViaOri = new TIndViaOri[VB6Helpers.UBound(MODXORI.VxOri) + VB6Helpers.UBound(MODXVIA.VxVia) + 1];
                        List<TIndViaOri> aux_GT_IndViaOri = new List<TIndViaOri>();
                        for (int ind = 0; ind < MODXORI.VxOri.Length + MODXVIA.VxVia.Length; ind++)
                        {
                            aux_GT_IndViaOri.Add(new TIndViaOri());
                        }
                        Lc_ContaVia = 0;
                        Lc_ContaOri = 0;

                        if (MODGCVD.COMISION == true && MODXORI.gb_esCosmos == true)
                        {
                            BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.TraspasoComision(Modulos);
                        }

                        for (IndConta = 0; IndConta <= (short)VB6Helpers.UBound(MODXVIA.VxVia); IndConta++)
                        {
                            if ((MODXVIA.VxVia[IndConta].NumCta == T_MODGCON0.IdCta_CtaCteMANN) || (MODXVIA.VxVia[IndConta].NumCta == T_MODGCON0.IdCta_CtaCteMANE))
                            {
                                if (MODXVIA.VxVia[IndConta].Status != 3)
                                {
                                    aux_GT_IndViaOri[VB6Helpers.CInt(Lc_ContaVia)].ind = IndConta;
                                    aux_GT_IndViaOri[VB6Helpers.CInt(Lc_ContaVia)].OriDes = "Via";
                                    aux_GT_IndViaOri[VB6Helpers.CInt(Lc_ContaVia)].Prty = MODXVIA.VxVia[IndConta].IdPrty;
                                    Lc_ContaVia = Format.StringToDouble(Lc_ContaVia) + 1;
                                }
                            }
                        }

                        for (IndConta = 0; IndConta <= (short)VB6Helpers.UBound(MODXORI.VxOri); IndConta++)
                        {
                            if ((MODXORI.VxOri[IndConta].NumCta == T_MODGCON0.IdCta_CtaCteMANN) || (MODXORI.VxOri[IndConta].NumCta == T_MODGCON0.IdCta_CtaCteMANE))
                            {
                                if (MODXORI.VxOri[IndConta].Status != 3)
                                {
                                    aux_GT_IndViaOri[(int)(Format.StringToDouble(Lc_ContaOri) + Format.StringToDouble(Lc_ContaVia))].ind = IndConta;
                                    aux_GT_IndViaOri[(int)(Format.StringToDouble(Lc_ContaOri) + Format.StringToDouble(Lc_ContaVia))].OriDes = "Ori";
                                    aux_GT_IndViaOri[(int)(Format.StringToDouble(Lc_ContaOri) + Format.StringToDouble(Lc_ContaVia))].Prty = MODXORI.VxOri[IndConta].IdPrty;
                                    Lc_ContaOri = Format.StringToDouble(Lc_ContaOri) + 1;
                                }
                            }
                        }

                        Mdl_Funciones_Varias.GT_IndViaOri = aux_GT_IndViaOri.ToArray<TIndViaOri>();
                        i = 0;
                        j = 0;

                        Lc_Contatot = (short)(Format.StringToDouble(Lc_ContaVia) + Format.StringToDouble(Lc_ContaOri));
                        if (Format.StringToDouble(Lc_ContaVia) > 0 && Format.StringToDouble(Lc_ContaOri) > 0)
                        {
                            for (i = 0; i < VB6Helpers.CShort(Lc_ContaVia); i++)
                            {
                                if (~BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Guarda_Oper_Manual(Modulos, unit, MODGCVD.VgCvd.OpeSin, Mdl_Funciones_Varias.GT_IndViaOri[i].ind, j, 1, Mdl_Funciones_Varias.GT_IndViaOri[i].Prty) != 0)
                                {
                                    tracer.AddToContext("Guarda_Oper_Manual 1", "Distinto de Cero");
                                    throw new Exception();
                                }
                            }

                            for (j = 0; j < VB6Helpers.CShort(Lc_ContaOri); j++)
                            {
                                if (~BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Guarda_Oper_Manual(Modulos, unit, MODGCVD.VgCvd.OpeSin, (short)(Mdl_Funciones_Varias.GT_IndViaOri[i - 1].ind), (short)(Mdl_Funciones_Varias.GT_IndViaOri[i + j].ind), 2, Mdl_Funciones_Varias.GT_IndViaOri[i].Prty) != 0)
                                {
                                    tracer.AddToContext("Guarda_Oper_Manual 2", "Distinto de Cero");
                                    throw new Exception();
                                }
                            }
                        }
                        else if (Format.StringToDouble(Lc_ContaVia) > 0)
                        {
                            for (i = 0; i < VB6Helpers.CShort(Lc_ContaVia); i++)
                            {
                                if (~BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Guarda_Oper_Manual(Modulos, unit, MODGCVD.VgCvd.OpeSin, Mdl_Funciones_Varias.GT_IndViaOri[i].ind, 0, 1, Mdl_Funciones_Varias.GT_IndViaOri[i].Prty) != 0)
                                {
                                    tracer.AddToContext("Guarda_Oper_Manual 3", "Distinto de Cero");
                                    throw new Exception();
                                }
                            }
                        }
                        else if (Format.StringToDouble(Lc_ContaOri) > 0)
                        {
                            for (j = 0; j < VB6Helpers.CShort(Lc_ContaOri); j++)
                            {
                                if (~BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Guarda_Oper_Manual(Modulos, unit, MODGCVD.VgCvd.OpeSin, 0, Mdl_Funciones_Varias.GT_IndViaOri[j].ind, 2, Mdl_Funciones_Varias.GT_IndViaOri[i].Prty) != 0)
                                {
                                    tracer.AddToContext("Guarda_Oper_Manual 4", "Distinto de Cero");
                                    throw new Exception();
                                }
                            }
                        }
                    }

                    //LAS OPRACIONES SE DEBEN GUARDAR EN UNA TABLA DE RELACION
                    if (MODXORI.gb_esCosmos == true)
                    {
                        //TODO GRABAR
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Guarda_Oper_CodTran(Modulos, unit, MODGCVD.VgCvd.OpeSin) != 0)
                        {
                            tracer.AddToContext("Guarda_Oper_CodTran", "Distinto de Cero");
                            throw new Exception();
                        }
                    }

                    if (~BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Guarda_Inf_Adic_CVD(Modulos, unit, MODGCVD.VgCvd.OpeSin) != 0)
                    {
                        tracer.AddToContext("Guarda_Inf_Adic_CVD", "Distinto de Cero");
                        throw new Exception();
                    }

                    w = (short)VB6Helpers.UBound(MODGSWF.VSwf);
                    for (i = 0; i <= (short)w; i++)
                    {
                        if (MODGSWF.VSwf[i].GrabSW == 1)
                        {
                            if (~MODSWENN.ActivaBD_Swi(Modulos, unit, MODGCVD.VgCvd.OpeSin, 0, 0, 0, 0, 0, MODGSWF.VSwf[i].CorSwi) != 0)
                            {
                                tracer.AddToContext("ActivaBD_Swi", "Distinto de Cero");
                                throw new Exception();
                            }
                        }
                    }

                    // es necesario ejecutar esto antes de cdm_exe2 ya que si falla deja la operacion activa en pa bd
                    // solo se debe hacer antes si no es automatica
                    if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
                    {
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Guarda_Oper_Relacion(Modulos, unit, MODGCVD.VgCvd.OpeSin) != 0)
                        {
                            tracer.AddToContext("Guarda_Oper_Relacion Antes Cmd_Exe2", "Distinto de Cero");
                            throw new Exception();
                        }
                    }

                    if (~Cmd_Exe2(Modulos, unit) != 0)
                    {
                        tracer.AddToContext("Cmd_Exe2", "Distinto de Cero");
                        throw new Exception();
                    }

                    //---------------- Fin Codigo Nuevo ----------------
                    //TODO GRABAR
                    if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                    {
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Guarda_Oper_Relacion(Modulos, unit, MODGCVD.VgCvd.OpeSin) != 0)
                        {
                            tracer.AddToContext("Guarda_Oper_Relacion Despues Cmd_Exe2", "Distinto de Cero");
                            throw new Exception();
                        }
                    }

                    if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1 && Modulos.MODCARMAS.CARGA_MASIVA == false)
                    {
                        foreach (var item in Modulos.Frm_Consulta.items)
                        {
                            if (item.Seleccionado == 1)
                            {
                                item.color = "red";
                                item.estado = "3";
                            }
                        }
                        Modulos.FormularioQueAbrir = "CargarOperaciones";
                    }
                    else
                    {
                        Modulos.FormularioQueAbrir = "Index";
                    }
                    Modulos.GrabarOk = 1;
                    if (~Save_Prn(Modulos, unit) != 0)
                    {
                        tracer.AddToContext("Save_Prn", "Distinto de Cero");
                        Modulos.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Informacion,
                            Text = "Hubo un error en la Impresión de Documentos. Reimprima aquellos que falten.",
                            Title = T_MODGCVD.MsgCVD
                        });

                        Module1.Codop.Id_Operacion = "";
                        NuevaCVD_L(Modulos, unit);
                        return false;
                    }
                    return aux_GrabarCorrecto(MODGUSR, Mdi_Principal);
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta al grabar", e);
                    return aux_GrabarIncorrecto(Modulos, unit, uowSwift);
                }
            }
        }

        private static dynamic aux_GrabarCorrecto(T_MODGUSR MODGUSR, UI_Mdi_Principal Mdi_Principal)
        {

            if (MODGUSR.UsrEsp.Tipeje == "O")
            {
                Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = false;
                Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { Mdi_Principal.BUTTONS[key].Focus = (key == "tbr_nuevo" ? true : false); });
            }
            return true;
        }

        private static dynamic aux_GrabarIncorrecto(InitializationObject initObj, UnitOfWorkCext01 unit, UnitOfWorkSwift uowSwift)
        {
            using (Tracer tracer = new Tracer("aux_GrabarIncorrecto"))
            {
                UI_Mdi_Principal Mdi_Principal = initObj.Mdi_Principal;
                T_MODGCVD MODGCVD = initObj.MODGCVD;
                T_MODGUSR MODGUSR = initObj.MODGUSR;
                T_Module1 Module1 = initObj.Module1;
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObj.Mdl_Funciones_Varias;
                short Y = 0;

                tracer.TraceInformation("Nro Ope GrabarIncorrecto: " + MODGCVD.VgCvd.OpeSin);
                Y = MODABDC.DesActivaBD(initObj, unit, MODGCVD.VgCvd.OpeSin, "000000");

                //Se debe desactivar tambien la BD swift, se marcan los swifts como anulados
                if (initObj.MODGSWF != null && initObj.MODGSWF.VSwf != null)
                {
                    foreach (T_Swf swift in initObj.MODGSWF.VSwf)
                    {
                        if (Convert.ToBoolean(swift.GrabSW) && swift.CorSwi > 0)
                        {
                            //el swift ya se grabo en BD swift
                            tracer.AddToContext("aux_GrabarIncorrecto", "el swift ya se grabo en BD swift");
                            MODSWENN.Fn_AnulaSwift(uowSwift, initObj, initObj.Module1.Codop.Cent_Costo, swift.CorSwi, "Anulado por sistema, operación Fund Transfer falló al grabar");
                        }
                    }
                }

                //seteo foco en pantalla principal
                Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { Mdi_Principal.BUTTONS[key].Focus = (key == "tbr_nuevo" ? true : false); });

                if (MODGUSR.UsrEsp.Tipeje == "O")
                {
                    Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = true;
                }
                Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.Informacion,
                    Text = "La Operación ha sido cancelada. "
                });
                Module1.Codop.Id_Operacion = "";
                NuevaCVD_L(initObj, unit);
                initObj.refrescarSesion = true;

                //--------------------------------------------------
                //Modificacion Pedro Faundez
                //Fecha: 30-10-2012
                //------------------ Codigo Nuevo ------------------
                if (Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0 && initObj.MODCARMAS.CARGA_MASIVA == false)
                {
                    initObj.FormularioQueAbrir = "CargarOperaciones";
                }

                //---------------- Fin Codigo Nuevo ----------------
                return false;
            }
        }

        //Realiza LOG e Imprime Cartas.-
        private static short Save_Prn(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            using (Tracer tracer = new Tracer("Save_Prn"))
            {
                initObj.DocumentosAImprimir.Clear();
                short _retValue = 0;
                T_MODGCVD MODGCVD = initObj.MODGCVD;
                T_MODGCON0 MODGCON0 = initObj.MODGCON0;
                T_MODCARMAS MODCARMAS = initObj.MODCARMAS;
                T_Module1 Module1 = initObj.Module1;
                try
                {

                    tracer.TraceVerbose("Generando documentos opr: {0}", MODGCVD.VgCvd.OpeSin);
                    //Se generan archivos log
                    Mdl_Funciones.Log_Cvd(initObj, unit);
                    Mdl_Funciones.Log_Anu(initObj, unit);

                    //Impresión de Documentos.

                    if (initObj.Mdi_Principal.mnu_cartas.Checked)
                    {
                        BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.ImprimeCartas(initObj);
                        MODXSWF.Print_xSwf(initObj, unit, 1); //a pesar de q en la app legacy imprimía 2 copias, no tiene sentido abrirle 2 solapas al usuario con el mismo documento
                        initObj.MODXSWF.VxSwfGen = new List<T_MODXSWF.SwiftGenerado>();
                    }

                    if ((initObj.Mdi_Principal.mnu_conta.Checked && MODGCON0.VMch.NroRpt != 0))
                    {
                        DateTime fecmovAux = DateTime.Parse(MODGCON0.VMch.fecmov);
                        BCH.Comex.Core.BL.XCFT.Modulos.MODGCON1.Pr_Imprime_Contab80(initObj, MODGCON0.VMch.NroRpt, fecmovAux.ToString("yyyy-MM-dd"));
                    }

                    if (initObj.Mdi_Principal.mnu_planillas.Checked)
                    {
                        if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_AnuVisI)
                        {
                            MODANUVI.Pr_ImprPlan(initObj, 1); //Imprime Planillas Visibles de Importacion.-
                            MODPREEM.Pr_ImprRee(initObj, unit, 1); //Poner 4 copias /Imprime Planillas de Reemplazo.-
                        }
                        else
                        {
                            Mdl_Funciones.Pr_Imprime_nPli(initObj, unit, 1); //Imprime Planillas Invisibles.
                            MODXPLN1.Pr_Imprime_nxPlv(initObj, unit, 1); //Imprime Planillas Visibles.
                            MODXANU.Pr_Imprime_nxAnu(initObj, unit, 1); //Imprime Planillas Anuladas.
                            MODCVDIMMM.ImpPlanillasTodas(initObj, unit, 1); //Imprime Planillas Visibles Importaciones.
                            MODVPLE.Pr_PlanillaEstImp(initObj, unit, 1); //Imprime Planillas Estadistica Importaciones .
                        }

                        if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_PlanSO)
                        {
                            MODPREEM.Pr_ImprRee(initObj, unit, 1); //Poner 4 copias /Imprime Planillas Sin Operac.-
                        }
                    }

                    if (initObj.DocumentosAImprimir.Count > 0)
                    {
                        initObj.VieneDe = String.IsNullOrEmpty(initObj.FormularioQueAbrir) ? "Index" : initObj.FormularioQueAbrir;
                        initObj.FormularioQueAbrir = "ImprimirDocumentos";
                    }
                    else if (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
                    {
                        initObj.FormularioQueAbrir = "Index";
                    }

                    if (MODCARMAS.CARGA_MASIVA == true)
                    {
                        //Module1.Codop.Id_Operacion = "";
                        //NuevaCVD("L");
                        //MODCARMAS.Escribe_Msj(("LISTA~CVDFT"));
                        //MODCARMAS.LlamaSatelite();
                        //MODCARMAS.Delay(VB6Helpers.DoubleToDate((5)));
                        //Timer1.Interval = 1;
                    }
                    else
                    {
                        Module1.Codop.Id_Operacion = "";
                        NuevaCVD_L(initObj, unit);
                        //initObj = Mdl_Acceso.Inicializar(unit, initObj.Usuario);
                    }
                    //---------------- Fin Codigo Nuevo ----------------
                    _retValue = -1;
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Alerta, Hubo un problema al generar los documentos",
                        Title = T_MODGCVD.MsgCVD
                    });
                    tracer.TraceException("Alerta", _ex);
                    throw;
                }
            }
        }

        public static short Cmd_Exe2(InitializationObject initObject, UnitOfWorkCext01 unit)//TODO
        {
            //return 0;//QUITAR
            T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
            UI_Mdi_Principal Mdi_Principal = initObject.Mdi_Principal;

            short _retValue = 0;
            //string Que = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Build(Mdl_Funciones_Varias);
            string R = "";
            int i = 0;
            short j = 0;
            short IndCmd = 0;
            //Construye el Comando.-
            if (Mdl_Funciones_Varias.CmdsQuerysNew.Count > 0)
            {
                using (var tracer = new Tracer("Cmd_Exe2"))
                {
                    try
                    {
                        //ejecuto el string 
                        IndCmd = BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones_Varias.Cmd_Execute(Mdl_Funciones_Varias, unit);
                        if (IndCmd > 0)
                        {
                            tracer.AddToContext("Transacción SyBase", "Se ha producido un error al Ejecutar la Transaccion");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Title = "Transacción SyBase",
                                Text = "Se ha producido un error al Ejecutar la Transaccion CmdsQuerysNew[" + IndCmd + "]. Reporte este problema.",
                                Type = TipoMensaje.Error
                            });
                            _retValue = (short)(false ? -1 : 0);
                        }
                        else
                        {
                            _retValue = -1;
                        }
                    }
                    catch (Exception e)
                    {
                        tracer.TraceException("Alerta, error en Cmd_Exe2", e);
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Se ha producido un error al ejecutar la Transacción SyBase.",
                            Title = "Transacción SyBase"
                        });
                        _retValue = 0;
                    }
                }
            }
            else
            {
                _retValue = -1;
            }
            return _retValue;
        }

        #region FUNCIONES PRIVADAS
        //Graba los Datos del Producto de la Operación.-
        private static short Save_Pro(InitializationObject Modulos, UnitOfWorkCext01 unit)
        {
            T_MODGCVD MODGCVD = Modulos.MODGCVD;
            T_Module1 Module1 = Modulos.Module1;
            T_MODGUSR MODGUSR = Modulos.MODGUSR;
            T_MODGSWF MODGSWF = Modulos.MODGSWF;
            T_MODSWENN MODSWENN = Modulos.MODSWENN;
            T_MODXPLN1 MODXPLN1 = Modulos.MODXPLN1;

            short _retValue = 0;
            short a = 0;
            short x = 0;
            short i = 0;
            using (var tracer = new Tracer("Save_Pro"))
            {
                try
                {
                    // LO QUE QUEDA COMENTADO ES PORQUE NO VA AL FLUJO BASICO

                    //********************************************************
                    //Producto.-
                    //*******************************************************
                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.SyPut_CVDAtom(Modulos, unit) != 0)
                    {
                        return _retValue;
                    }
                    short _switchVar1 = MODGCVD.VgCvd.TipCVD;
                    //Compra-Venta.-                
                    if (_switchVar1 == T_MODGCVD.TCvd_CVD)
                    {
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.Mdl_Funciones.Rebaja_xDec_Inv(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("Rebaja_xDec_Inv", "Distinto de Cero");
                            return _retValue;
                        }
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.SyPutn_Cov(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("SyPutn_Cov", "Distinto de Cero");
                            return _retValue;
                        }
                        //Arbitraje.-
                    }
                    else if (_switchVar1 == T_MODGCVD.TCvd_Arb)
                    {
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGARB.SyPutn_ArbAtom(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("SyPutn_ArbAtom", "Distinto de Cero");
                            return _retValue;
                        }
                        //Visible Export.-
                    }
                    else if (_switchVar1 == T_MODGCVD.TCvd_VisExp)
                    {
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.SyPut_Vex(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("SyPut_Vex", "Distinto de Cero");
                            return _retValue;
                        }
                        //Reversar Operación.-
                    }
                    else if (_switchVar1 == T_MODGCVD.TCvd_Rev)
                    {
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGANU.Rebaja_xAnu(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("Rebaja_xAnu", "Distinto de Cero");
                            return _retValue;
                        }
                    }
                    else if (_switchVar1 == T_MODGCVD.TCvd_VisImp)
                    {
                        tracer.AddToContext("SyPut_VisImp", "Planillas Visibles Estadisticas");
                        a = BCH.Comex.Core.BL.XCFT.Modulos.MODCVDIMMM.SyPut_VisImp(Modulos, unit);
                        //Planillas Visibles Estadisticas.-
                    }
                    else if (_switchVar1 == T_MODGCVD.TCvd_PlnVEst)
                    {
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODVPLE.SyPut_Plan(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("SyPut_Plan", "Distinto de Cero");
                            return _retValue;
                        }
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODVPLE.SyPut_Inte(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("SyPut_Inte", "Distinto de Cero");
                            return _retValue;
                        }
                        //Planillas Visibles.-
                    }
                    else if (_switchVar1 == T_MODGCVD.TCvd_AnuVisI)
                    {
                        //Actualizamos campos de anulacion y realizamos las devoluciones
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODANUVI.Fn_SyPutAnu(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("Fn_SyPutAnu", "Distinto de Cero");
                            return _retValue;
                        }
                        //Grabamos los datos de autorizacion
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODANUVI.Fn_SyPutAut(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("Fn_SyPutAut", "Distinto de Cero");
                            return _retValue;
                        }
                        //Grabamos la planilla de reemplazo.-
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODPREEM.Fn_SyPutPl(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("Fn_SyPutPl", "Distinto de Cero");
                            return _retValue;
                        }
                        //Grabamos Intereses.-
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODPREEM.Fn_SyPutIn(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("Fn_SyPutIn", "Distinto de Cero");
                            return _retValue;
                        }
                        //Relacion Dec - Ope.-
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODCVDIMMM.SyPut_SceRdo(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("SyPut_SceRdo", "Distinto de Cero");
                            return _retValue;
                        }
                        //Rebajes.-
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODCVDIMMM.SyPut_SceReb(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("SyPut_SceReb", "Distinto de Cero");
                            return _retValue;
                        }
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODANUVI.Fn_SyPutReba(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("Fn_SyPutReba", "Distinto de Cero");
                            return _retValue;
                        }
                    }
                    else if (_switchVar1 == T_MODGCVD.TCvd_PlanSO)
                    {
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODPREEM.Fn_SyPutPl(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("Fn_SyPutPl", "Distinto de Cero");
                            return _retValue;
                        }
                        //Grabamos Intereses.-
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODPREEM.Fn_SyPutIn(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("Fn_SyPutIn", "Distinto de Cero");
                            return _retValue;
                        }
                        //Relacion Dec - Ope.-
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODCVDIMMM.SyPut_SceRdo(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("SyPut_SceRdo", "Distinto de Cero");
                            return _retValue;
                        }
                        //Rebajes.-
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODCVDIMMM.SyPut_SceReb(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("SyPut_SceReb", "Distinto de Cero");
                            return _retValue;
                        }
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODANUVI.Fn_SyPutReba(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("Fn_SyPutReba", "Distinto de Cero");
                            return _retValue;
                        }
                    }

                    if (MODGCVD.BotPrt != 0)
                    {
                        //Partic. Opcionales
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.SyPutn_Prt(Modulos, unit, MODGCVD.VgCvd.OpeSin) != 0)
                        {
                            tracer.AddToContext("SyPutn_Prt", "Distinto de Cero");
                            return _retValue;
                        }
                        if (BCH.Comex.Core.BL.XCFT.Modulos.Module1.SySalvarGetParty(Modulos, unit, Module1.Codop) != 0)
                        {
                            tracer.AddToContext("SySalvarGetParty", "Distinto de Cero");
                            return _retValue;
                        }
                    }

                    //----------------------------------------------------------------------------------------------------------------------
                    // Accenture - Antiguo código - Termino
                    // Descripcion: se deja como comentario el codigo para que siempre entre a imprimir,
                    //              dentro de la funcion tiene la validación para diferenciar cuando tiene que imprimir planillas y cuando no.
                    //-----------------------------------------------------------------------------------------------------------------------

                    //If TIN = False Then
                    //--------------------------------------------------------------------------------------------------
                    // Accenture - Código Nuevo - Termino
                    //--------------------------------------------------------------------------------------------------
                    //********************************************************
                    //Planillas.-
                    //********************************************************
                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGPLI1.SyPutn_Pli(Modulos, unit, "000000", T_MODXPLN1.ExPlv_Anulada) != 0)
                    {
                        tracer.AddToContext("SyPutn_Pli", "Distinto de Cero");
                        return _retValue;
                        //Graba Pln's Inv.-
                    }
                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXPLN1.SyPutn_xPlv(Modulos, unit, "000000", T_MODXPLN1.ExPlv_Anulada) != 0)
                    {
                        tracer.AddToContext("SyPutn_xPlv", "Distinto de Cero");
                        return _retValue;
                        //Graba Pln's Vis.-
                    }
                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXPLN1.SyPutn_xPlva(Modulos, unit) != 0)
                    {
                        tracer.AddToContext("SyPutn_xPlva", "Distinto de Cero");
                        return _retValue;
                        //Graba Pln's Vis.-
                    }
                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXANU.SyPutn_xAnu(Modulos, unit, T_MODXPLN1.ExPlv_Anulada) != 0)
                    {
                        tracer.AddToContext("SyPutn_xAnu", "Distinto de Cero");
                        return _retValue;
                        //Graba Pln's Anu-Vis.-
                    }

                    //********************************************************
                    //Rebajes.-
                    //********************************************************
                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXPLN0.SyPutn_xDep(Modulos, unit, MODGCVD.VgCvd.OpeSin, 0, 0, 0) != 0)
                    {
                        tracer.AddToContext("SyPutn_xDep", "Distinto de Cero");
                        return _retValue;
                    }

                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODXPLN0.SyPutReb_xDec(Modulos, unit, MODGCVD.VgCvd.OpeSin, 0, 0, 0) != 0)
                    {
                        tracer.AddToContext("SyPutReb_xDec", "Distinto de Cero");
                        return _retValue;
                    }
                    //********************************************************
                    //Documentos Valorados.-
                    //********************************************************
                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.SyPutn_Chq(Modulos, unit, MODGCVD.VgCvd.OpeSin, "000000") != 0)
                    {
                        tracer.AddToContext("SyPutn_Chq", "Distinto de Cero");
                        return _retValue;
                        //Cheques
                    }
                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGCHQ.SyPutn_Vvi(Modulos, unit, MODGCVD.VgCvd.OpeSin, "000000") != 0)
                    {
                        tracer.AddToContext("SyPutn_Vvi", "Distinto de Cero");
                        return _retValue;
                        //Vale Vistas
                    }

                    //'SI ES CARGA AUTOMATICA Y SEGUN OPCION DEBE GENERAR EL SWIFT
                    if (Modulos.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
                    {
                        if (~Genera_Swift_Auto(Modulos, unit) != 0)
                        {
                            tracer.AddToContext("Genera_Swift_Auto", "Distinto de Cero");
                            return _retValue;
                        }
                    }

                    if (BCH.Comex.Core.BL.XCFT.Modulos.MODXSWF.SyPutn_Swf(Modulos, unit, MODGCVD.VgCvd.OpeSin, "000000", MODGUSR.UsrEsp.CentroCosto + MODGUSR.UsrEsp.Especialista) != 0)
                    {
                        x = BCH.Comex.Core.BL.XCFT.Modulos.MODSWENN.Fn_Save_BaseSwft(Modulos, unit, "0", "0", "0", "0", "0");  //Graba ordenes de pago en base swift
                        if (x == 0) //dio error
                        {
                            tracer.AddToContext("Fn_Save_BaseSwft", "Igual a Cero (error)");
                            return 0;
                        }
                    }
                    else
                    {
                        tracer.AddToContext("SyPutn_Swf", "Distinto de Cero");
                        return _retValue;
                        //Swift's.-
                    }

                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODABDC.ActivaBases(Modulos, unit, MODGCVD.VgCvd.OpeSin, "000000") != 0)
                    {
                        tracer.AddToContext("ActivaBases", "Distinto de Cero");
                        return _retValue;
                    }
                    if (MODSWENN.Hab_Swift != 0)
                    {
                        for (i = 0; i <= (short)VB6Helpers.UBound(MODGSWF.VSwf); i++)
                        {
                            if (MODGSWF.VSwf[i].GrabSW == 1)
                            {
                                if (~BCH.Comex.Core.BL.XCFT.Modulos.MODSWENN.ActivaBD_Swi(Modulos, unit, MODGCVD.VgCvd.OpeSin, 0, 0, 0, 0, 0, MODGSWF.VSwf[i].CorSwi) != 0)
                                {
                                    tracer.AddToContext("ActivaBD_Swi", "Distinto de Cero");
                                    return _retValue;
                                }
                            }

                        }

                    }

                    if (MODGCVD.VgCvd.TipCVD != T_MODGCVD.TCvd_VisImp)
                    {
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.ConvPlnOK(Modulos, unit, MODGCVD.VgCvd.OpeSin, "000000") != 0)
                        {
                            tracer.AddToContext("ConvPlnOK", "Distinto de Cero");
                            return _retValue;
                            //Conv v/s Pln.-
                        }
                    }

                    if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_CVD || MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_Arb || MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_VisExp)
                    {
                        if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.ValidaVigente(Modulos, unit, MODGCVD.VgCvd.OpeSin, MODGCVD.VgCvd.TipCVD) != 0)
                        {
                            tracer.AddToContext("ValidaVigente", "Distinto de Cero");
                            return _retValue;
                        }
                    }

                    //********************************************************
                    _retValue = (short)(true ? -1 : 0);
                    return _retValue;
                }
                catch (Exception _ex)
                {
                    _retValue = 0;
                    tracer.TraceError("Error en Save_Pro", _ex);
                }
                return _retValue;
            }
        }

        private static short Save_Con(InitializationObject Modulos, UnitOfWorkCext01 unit, string Usr)
        {
            using (var tracer = new Tracer())
            {
                T_MODGCON0 MODGCON0 = Modulos.MODGCON0;
                short _retValue = 0;

                try
                {
                    tracer.AddToContext("Save_Con - SyConCan", "Grabo la Contabilidad");
                    if (~MODCONT.SyConCan(Modulos, unit, Usr) != 0)
                    {
                        tracer.AddToContext("SyConCan", "Error al Grabar la Contabilidad");
                        return _retValue;
                    }
                    //Valida Contabilidad:
                    tracer.AddToContext("Save_Con - ValidaContab", "Valido la Contabilidad");
                    if (~BCH.Comex.Core.BL.XCFT.Modulos.MODGCON0.ValidaContab(Modulos, unit, MODGCON0.VMch.NroRpt, MODGCON0.VMch.fecmov) != 0)
                    {
                        tracer.AddToContext("ValidaContab", "Error al Validar la Contabilidad");
                        return _retValue;
                    }

                    _retValue = (short)(true ? -1 : 0);
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta: Save_Con Ex:", _ex);
                    _retValue = 0;
                }

                return _retValue;
            }
        }

        //Graba los Documentos de la Operación.-
        private static short Save_Doc(InitializationObject Modulos, UnitOfWorkCext01 unit, string Usr)
        {
            using (var tracer = new Tracer("Save_Doc"))
            {
                T_MODGCVD MODGCVD = Modulos.MODGCVD;
                T_MODXORI MODXORI = Modulos.MODXORI;

                short _retValue = 0;
                short a = 0;
                short b = 0;
                try
                {
                    //Se limpian las variable que indican la cantidad de cartas a impirmir por cada tipo, ya que al venir desde speed por segunda vez, conserva los valores
                    // y puede imprimir documentos que no corresponan a la operación
                    MODGCVD.VgCvd.AvisoDC = string.Empty;
                    //Compra - Venta.
                    MODGCVD.VgCvd.DocCVD = 0;
                    //Planillas.
                    MODGCVD.VgCvd.DocPln = 0;
                    //Arbitraje.
                    MODGCVD.VgCvd.DocArb = 0;
                    //Ventas Visibles Import.
                    MODGCVD.VgCvd.DocCvdI = 0;
                    //*********************************************************
                    //Genera Documentos.-
                    //*********************************************************
                    //Si Existe Débito/Crédito => Grabar Avisos.
                    tracer.AddToContext("Fn_ExisteCtaCte", "Previo a Preguntar si Existe Débito/Crédito");
                    if (MODXVIA.Fn_ExisteCtaCte(Modulos) != 0)
                    {
                        tracer.AddToContext("Fn_ExisteCtaCte True", "Obtengo Avisos de D/C de SyPutn_Adc");
                        MODGCVD.VgCvd.AvisoDC = MODXVIA.SyPutn_Adc(Modulos, unit, MODGCVD.VgCvd.OpeSin, MODGCVD.VgCvd.RefCli, Usr, MODXORI.VgxOri.ImpDeb);
                        if (String.IsNullOrEmpty(MODGCVD.VgCvd.AvisoDC))
                        {
                            return _retValue;
                        }
                    }

                    short _switchVar1 = MODGCVD.VgCvd.TipCVD;
                    if (_switchVar1 == T_MODGCVD.TCvd_CVD || _switchVar1 == 0)
                    {
                        //Compra-Venta.
                        if (MODGCVD.COMISION == false)
                        {
                            tracer.AddToContext("Doc_ComVta", "Obtengo Documento de Compra/Venta");
                            MODGCVD.VgCvd.DocCVD = Mdl_Funciones_Varias.Doc_ComVta(Modulos, unit, MODGCVD.VgCvd.OpeSin);
                            if (MODGCVD.VgCvd.DocCVD == 0)
                            {
                                return _retValue;
                            }
                        }
                        else
                        {
                            tracer.AddToContext("Doc_ComVta", "Obtengo Documento de Compra/Venta");
                            MODGCVD.VgCvd.DocCVD = Mdl_Funciones_Varias.Doc_ComVta(Modulos, unit, MODGCVD.VgCvd.OpeSin);
                            if (MODGCVD.VgCvd.DocCVD == 0)
                            {
                                return _retValue;
                            }
                        }
                    }
                    else if (_switchVar1 == T_MODGCVD.TCvd_Arb)
                    {
                        //Arbitraje.
                        tracer.AddToContext("Doc_ComVta", "Obtengo Documento de Arbitraje");
                        MODGCVD.VgCvd.DocArb = Mdl_Funciones_Varias.Doc_Arbitraje(Modulos, unit, MODGCVD.VgCvd.OpeSin);
                        if (MODGCVD.VgCvd.DocArb == 0)
                        {
                            return _retValue;
                        }

                    }
                    else if (_switchVar1 == T_MODGCVD.TCvd_VisExp)
                    {
                        //Visible Export.-
                        tracer.AddToContext("Doc_xPlvCob", "Obtengo Documento de Visible Export");
                        MODGCVD.VgCvd.DocPln = Mdl_Funciones_Varias.Doc_xPlvCob(Modulos, unit, MODGCVD.VgCvd.OpeSin, "");
                        if (MODGCVD.VgCvd.DocPln == 0)
                        {
                            return _retValue;
                        }

                    }
                    else if (_switchVar1 == T_MODGCVD.TCvd_VisImp)
                    {
                        tracer.AddToContext("Doc_CVDImp", "Obtengo Documento de Visible Import");
                        a = Mdl_Funciones_Varias.Doc_CVDImp(Modulos, unit, MODGCVD.VgCvd.OpeSin);
                    }
                    else if (_switchVar1 == T_MODGCVD.TCvd_PlanSO)
                    {
                        //Visibles Sin Operacion.-
                        tracer.AddToContext("Doc_CVDImp", "Obtengo Documento de Planillas Sin Operacion");
                        b = Mdl_Funciones_Varias.Doc_PlanSO(Modulos, unit, MODGCVD.VgCvd.OpeSin);
                    }

                    //*********************************************************
                    _retValue = (short)(true ? -1 : 0);
                }
                catch (Exception _ex)
                {
                    tracer.TraceException("Alerta: Save_Doc Ex:", _ex);
                    _retValue = 0;
                }
                return _retValue;
            }
        }
        //Verifica que los campos requeridos estén OK.
        //True   :   Exitoso.
        //False  :   Hubo Error.
        private static short ValidaObjetos(InitializationObject Modulos)
        {
            using (var trace = new Tracer("ValidaObjetos"))
            {
                trace.AddToContext("ValidaObjetos", "Verifica que los campos requeridos estén OK.");
                T_MODGCVD MODGCVD = Modulos.MODGCVD;
                T_Module1 Module1 = Modulos.Module1;
                T_MODXANU MODXANU = Modulos.MODXANU;

                UI_Mdi_Principal Mdi_Principal = Modulos.Mdi_Principal;

                short n = 0;
                short i = 0;

                if (String.IsNullOrEmpty(MODGCVD.VgCvd.PrtCli))
                {
                    trace.TraceError("Antes de grabar debe ingresar participantes");
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Antes de grabar debe ingresar participantes"
                    });
                    return 0;
                }

                if (MODGCVD.VgCvd.TipCVD == 0 && MODGCVD.COMISION == false)
                {
                    trace.TraceError("Debe definir el tipo de operación antes de grabar");
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe definir el tipo de operación antes de grabar"
                    });
                    return 0;
                }

                if (string.IsNullOrEmpty(Module1.PartysOpe[T_MODGCVD.ICli].NombreUsado))
                {
                    trace.TraceError("Antes de grabar debe ingresar los datos correspondientes al cliente.");
                    Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Antes de grabar debe ingresar los datos correspondientes al cliente."
                    });
                    return 0;
                }

                if (MODGCVD.COMISION)
                {
                    if (!string.IsNullOrEmpty(MODGCVD.VgCvd.Etapa))
                    {
                        string _switchVar1 = VB6Helpers.Left(MODGCVD.VgCvd.Etapa, 3);
                        if (_switchVar1 == "ORI")
                        {
                            trace.TraceError("Antes de grabar debe definir el Origen de los Fondos.");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Antes de grabar debe definir el Origen de los Fondos."
                            });
                            return 0;
                        }
                    }
                    else
                    {
                        if (Modulos.Mdl_Funciones_Varias.V_gCom == null || Modulos.Mdl_Funciones_Varias.V_gCom.Length == 0)
                        {
                            trace.TraceError("Antes de grabar debe definir las comisiones.");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Antes de grabar debe definir las comisiones."
                            });
                            return 0;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(MODGCVD.VgCvd.Etapa))
                    {
                        string _switchVar1 = VB6Helpers.Left(MODGCVD.VgCvd.Etapa, 3);
                        if (_switchVar1 == "CVD")
                        {
                            trace.TraceError("Antes de grabar debe definir la Operación de Compra/Venta.");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Antes de grabar debe definir la Operación de Compra/Venta."
                            });
                            return 0;
                        }
                        else if (_switchVar1 == "ARB")
                        {
                            trace.TraceError("Antes de grabar debe definir la Operación de Arbitrajes");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Antes de grabar debe definir la Operación de Arbitrajes"
                            });
                            return 0;
                        }
                        else if (_switchVar1 == "VTA")
                        {
                            trace.TraceError("Antes de grabar debe definir la Operación de Ventas Visibles");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Antes de grabar debe definir la Operación de Ventas Visibles"
                            });
                            return 0;
                        }
                        else if (_switchVar1 == "ORI")
                        {
                            trace.TraceError("Antes de grabar debe definir el Origen de los Fondos.");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Antes de grabar debe definir el Origen de los Fondos."
                            });
                            return 0;
                        }
                        else if (_switchVar1 == "VIA")
                        {
                            trace.TraceError("Antes de grabar debe definir el Destino de los Fondos.");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Antes de grabar debe definir el Destino de los Fondos."
                            });
                            Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled = true;
                            Mdi_Principal.BUTTONS["tbr_origfondos"].Enabled = true;
                            return 0;
                        }
                        else if (_switchVar1 == "VUE")
                        {
                            trace.TraceError("Antes de grabar debe definir los Vueltos.");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Antes de grabar debe definir los Vueltos."
                            });
                            Mdi_Principal.BUTTONS["tbr_vueltos"].Enabled = true;
                            Mdi_Principal.BUTTONS["tbr_vueltos"].Focus = true;
                            Mdi_Principal.BUTTONS["tbr_grabar"].Focus = false;
                            return 0;
                        }
                        else if (_switchVar1 == "DVS")
                        {
                            trace.TraceError("Antes de grabar debe definir los Documentos Valorados.");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Antes de grabar debe definir los Documentos Valorados."
                            });
                            return 0;
                        }
                        else if (_switchVar1 == "SWF")
                        {
                            trace.TraceError("Antes de grabar debe definir swift.");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Antes de grabar debe definir swift."
                            });
                            return 0;
                        }
                    }
                }

                n = (short)VB6Helpers.UBound(MODXANU.VxAnus);
                // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                // IGNORED: On Error GoTo 0

                for (i = 0; i <= (short)n; i++)
                {
                    if (MODXANU.VxAnus[i].PlnEst != 1)
                    {
                        if (MODXANU.VxAnus[i].TipCam == 0)
                        {
                            trace.TraceError("Antes de grabar debe Ingresar Tipo de Cambio de las planillas de anulación.");
                            Mdi_Principal.MESSAGES.Add(new UI_Message()
                            {
                                Type = TipoMensaje.Error,
                                Text = "Antes de grabar debe Ingresar Tipo de Cambio de las planillas de anulación."
                            });
                            return 0;
                        }

                        if (!string.IsNullOrEmpty(MODXANU.VxAnus[n].TipAut))
                        {
                            if (MODXANU.VxAnus[n].NroAut == 0 || string.IsNullOrEmpty(MODXANU.VxAnus[n].FecAut))
                            {
                                trace.TraceError("Antes de grabar Ingresar datos de anulación en planillas de anulación.");
                                Mdi_Principal.MESSAGES.Add(new UI_Message()
                                {
                                    Type = TipoMensaje.Error,
                                    Text = "Antes de grabar Ingresar datos de anulación en planillas de anulación."
                                });
                                return 0;
                            }

                        }

                    }

                }

                return (short)(true ? -1 : 0);
            }
        }

        //Graba los Documentos de la Operación.-
        private static short Save_Ini(InitializationObject Modulos)
        {
            using (var trace = new Tracer("Save_Ini"))
            {
                trace.AddToContext("Save_Ini", "Graba los Documentos de la Operación");
                T_Module1 Module1 = Modulos.Module1;
                T_MODGUSR MODGUSR = Modulos.MODGUSR;
                UI_Mdi_Principal Mdi_Principal = Modulos.Mdi_Principal;

                short _retValue = 0;
                short i = 0;
                try
                {
                    // IGNORED: On Error GoTo Save_IniErr

                    //Participante.-
                    if (String.IsNullOrEmpty(Module1.PartysOpe[T_MODGCVD.ICli].NombreUsado))
                    {
                        trace.TraceError("Debe ingresar la Identificación del Participante.");
                        Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Debe ingresar la Identificación del Participante."
                        });
                        return _retValue;
                    }

                    //Botón Grabar.-
                    Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = false;

                    //Valida Objetos.-
                    i = ValidaObjetos(Modulos);
                    if (~i != 0)
                    {
                        if (MODGUSR.UsrEsp.Tipeje == "O")
                        {
                            Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = true;
                        }
                        return _retValue;
                    }

                    _retValue = (short)(true ? -1 : 0);

                }
                catch (Exception _ex)
                {
                    _retValue = 0;
                    trace.TraceException("Alerta: Save_Ini ex:", _ex);
                }
                return _retValue;
            }
        }

        private static short ObjetosToVar(InitializationObject Modulos)
        {
            short _retValue = 0;
            if (Modulos.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_Rev)
            {
                _retValue = -1;
            }
            else
            {
                Modulos.MODGCVD.VgCvd.PrtCli = Modulos.Module1.PartysOpe[T_MODGCVD.ICli].LlaveArchivo;
                Modulos.MODGCVD.VgCvd.IndNomC = Modulos.Module1.PartysOpe[T_MODGCVD.ICli].IndNombre;
                Modulos.MODGCVD.VgCvd.IndDirC = Modulos.Module1.PartysOpe[T_MODGCVD.ICli].IndDireccion;
                Modulos.MODGCVD.VgCvd.PrtOtr = Modulos.Module1.PartysOpe[T_MODGCVD.IOtr].LlaveArchivo;
                Modulos.MODGCVD.VgCvd.IndNomO = Modulos.Module1.PartysOpe[T_MODGCVD.IOtr].IndNombre;
                Modulos.MODGCVD.VgCvd.IndDirO = Modulos.Module1.PartysOpe[T_MODGCVD.IOtr].IndDireccion;
                _retValue = -1;
            }
            return _retValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Modulos"></param>
        /// <param name="unit"></param>
        /// <param name="nombreAccion">Accion que invoca a la funcion. Si tiene que redireccionar a SeleccionOficina, vuleve a
        /// esta accion
        /// </param>
        /// <returns></returns>
        private static bool NuevaCVD_I(InitializationObject Modulos, UnitOfWorkCext01 unit, string nombreAccion)
        {
            using (var trace = new Tracer("NuevaCVD_I"))
            {
                short x;
                string Nro_Disp_FT = "";
                string Nro_Disp_CVD = "";
                string ofi = "";

                if (Modulos.Frm_SeleccionOficina == null) // si no entre en la pantalla aun
                {
                    //Se limpia variable
                    Modulos.Mdl_Funciones_Varias.LC_TRXID_MAN = "";

                    Modulos.MODGCVD.BotPrt = 0;
                    Modulos.Mdl_Funciones_Varias.CARGA_AUTOMATICA = 0;

                    Modulos.Mdl_Funciones_Varias.LC_MONEDA = 0;
                    Modulos.Mdl_Funciones_Varias.LC_MONTO = "";
                    Modulos.Mdl_Funciones_Varias.LC_XREF = "";
                    Modulos.Mdl_Funciones_Varias.LC_CONREFNUM = "";
                    Modulos.Mdl_Funciones_Varias.LC_PRD = "";
                    Modulos.Mdl_Funciones_Varias.LC_OUTGOING = "";
                    Modulos.Mdl_Funciones_Varias.LC_INCOMING = "";
                    Modulos.Mdl_Funciones_Varias.LC_ORD_INST1 = "";  //DESTINO FONDOS
                    Modulos.Mdl_Funciones_Varias.LC_PMNT_DET1 = "";  //DESTINO FONDOS
                    Modulos.Mdl_Funciones_Varias.LC_PMNT_DET2 = "";  //DESTINO FONDOS
                    Modulos.Mdl_Funciones_Varias.LC_PMNT_DET3 = "";  //DESTINO FONDOS
                    Modulos.Mdl_Funciones_Varias.LC_PMNT_DET4 = "";  //DESTINO FONDOS
                    Modulos.Mdl_Funciones_Varias.LC_COD_TRANS = "";  //CODIGO TRANSACCION
                    Modulos.Mdl_Funciones_Varias.Lc_BaseNumber = "";
                    Modulos.Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO = "";
                    Modulos.Mdl_Funciones_Varias.LC_SWFT = "";
                    Modulos.Mdl_Funciones_Varias.LC_SWFT = "";
                    Modulos.Mdl_Funciones_Varias.LC_BEN_INST1 = "";  //BEN_INST1
                    Modulos.Mdl_Funciones_Varias.LC_ULT_BEN1 = "";  //ULT_BEN1
                    Modulos.Mdl_Funciones_Varias.LC_ULT_BEN2 = "";  //ULT_BEN2
                    Modulos.Mdl_Funciones_Varias.LC_ULT_BEN3 = "";  //ULT_BEN3
                    Modulos.Mdl_Funciones_Varias.LC_ULT_BEN4 = "";  //ULT_BEN4
                    Modulos.Mdl_Funciones_Varias.LC_CHG_WHOM = "";  //CHG_WHOM
                    Modulos.Mdl_Funciones_Varias.LC_FCCFT = "";  //FCCFT
                    Modulos.Mdl_Funciones_Varias.LC_DRVALDT = "";  //DRVALDT
                    Modulos.Mdl_Funciones_Varias.LC_NOM_MDA = "";  //NOMBRE MONEDA
                    Modulos.Mdl_Funciones_Varias.LC_INTRMD1 = "";
                    Modulos.Mdl_Funciones_Varias.LC_US_PAY_ID = "";
                    Modulos.Mdl_Funciones_Varias.LC_RECVR_CORRES1 = "";
                    Modulos.Mdl_Funciones_Varias.LC_RECVR_CORRES2 = "";
                    Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1 = "";
                    Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO2 = "";
                    Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO3 = "";
                    Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO4 = "";
                    Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO5 = "";
                    Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO6 = "";

                    Modulos.Frm_Principal.Tx_NomPrt.Text = "";
                    //Modulos.Frm_Principal.Tx_RefCli.Text = "";
                    Modulos.Frm_Principal.Tx_iva.Text = "";
                    Modulos.Frm_Principal.Tx_moneda.Text = "";
                    Modulos.Frm_Principal.Tx_MtoOri.Text = "";
                    Modulos.Frm_Principal.Tx_neto.Text = "";
                    Modulos.Frm_Principal.Tx_NroFac.Text = "";
                    Modulos.Frm_Principal.Tx_tipo.Text = "";
                    Modulos.Frm_Principal.Lt_CI.Clear();
                    Modulos.Frm_Principal.Lt_CVE.Clear();
                    Modulos.Frm_Principal.Lt_CVI.Clear();
                    Modulos.Frm_Principal.Num_Op.Text = "";

                    MODXVIA.Pr_Init_xVia(Modulos.MODXVIA, Modulos.MODGSWF, Modulos.MODGCHQ);
                    MODXORI.Pr_Init_xOri(Modulos.MODXORI);

                    Modulos.Mdl_Funciones_Varias.V_gCom = new T_gCom[0];
                    Modulos.MODGMTA.VVdi = new T_Vdi[1];
                    Modulos.MODGMTA.VVdi[0] = new T_Vdi();
                    Modulos.MODGASO.VgAsoNul = new T_Aso();
                    Modulos.MODGASO.VgAso = Modulos.MODGASO.VgAsoNul.Clone();

                    //------------------------------------------------------------------------
                    x = Module1.ResetParty(Modulos.Module1, Modulos.MODGCVD.Beneficiario);
                    //------------------------------------------------------------------------
                    Modulos.MODGCVD.VgPli = new T_gPli[0];

                    //==>>>  por la diferencia de como se hacen las cosas en la web estas no deberian ir
                    Modulos.MODGARB.VArb = new T_Arb[0];

                    Modulos.MODGPLI1.Vplis = new T_Pli[0];
                    Modulos.MODXPLN1.VxPlvs = new T_xPlv[0];
                    Modulos.MODXANU.VxAnus = new T_xAnu[0];

                    Modulos.MODXPLN0.VxDecP = new T_xDecP[0];
                    Modulos.MODGANU.VAnuPl = new T_AnuPl[0];
                    Modulos.MODXORI.Vx_SCodTran = new S_Codtran[0];
                    //--Comisiones de Planillas.-
                    Modulos.MODXPLN1.VCom_xPlv = Modulos.MODXPLN1.VCom_xPlvNul;
                    Modulos.MODGCVD.VgCVDNul = Modulos.MODGCVD.VgCVDVacia;

                    Modulos.Mdi_Principal.BUTTONS["tbr_Comercio_invisible"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_arbitrajes"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_vtas_export"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_participantes"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_comisiones"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_origfondos"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_Swift"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_planilla2"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_planilla3"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_planilla4"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_operel"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_nota"].Enabled = false;
                    Modulos.Mdi_Principal.BUTTONS["tbr_vueltos"].Enabled = false;

                    //Modo Ingreso.
                    Modulos.MODGCVD.AntOper = 0;
                    Modulos.MODGCVD.Antmon = 0;
                    Modulos.MODGCVD.Anttip = 0;

                    if (Modulos.MODGUSR.UsrEsp.Tipeje == "O")
                    {
                        Modulos.Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = true;
                    }
                    Modulos.Mdi_Principal.BUTTONS["tbr_participantes"].Enabled = true;
                    Modulos.Mdi_Principal.BUTTONS["tbr_comisiones"].Enabled = true;
                    Modulos.Mdi_Principal.BUTTONS["tbr_operel"].Enabled = true;
                    Modulos.Mdi_Principal.BUTTONS["tbr_nota"].Enabled = true;

                    Nro_Disp_FT = MODGRNG.SyGetMax_Rng(unit, Modulos.MODGUSR.UsrEsp.CentroCosto + T_MODGUSR.IdPro_ComVen + Modulos.MODGUSR.UsrEsp.Especialista);
                    Nro_Disp_CVD = MODGRNG.SyGetMax_Rng(unit, Modulos.MODGUSR.UsrEsp.CentroCosto + T_MODGUSR.IdPro_CVD + Modulos.MODGUSR.UsrEsp.Especialista);

                    if (Nro_Disp_FT.Equals("-1"))
                    {
                        trace.TraceError("Error al rescatar el máximo número de operación de un documento con respecto al centro de costo y especialista para FundTransfer");
                        Modulos.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Title = "Error de Acceso a Datos",
                            Type = TipoMensaje.Error,
                            Text = "Error al rescatar el máximo número de operación de un documento con respecto al centro de costo y especialista para FundTransfer"
                        });
                    }
                    else
                    {
                        x = MODGRNG.SgteNumOpr(Modulos.MODGRNG, Modulos.Module1, Modulos.MODGUSR, Modulos.Mdi_Principal, unit, T_MODGUSR.IdPro_ComVen, T_Mdl_Funciones_Varias.cod_producto, true, numMinRequerido: int.Parse(Nro_Disp_FT));
                        if (x != 0)
                            //todo OK
                            Modulos.Module1.Codop_FT = Modulos.Module1.Codop.Clone();
                        else
                            return false;
                    }

                    if (Nro_Disp_CVD.Equals("-1"))
                    {
                        trace.TraceError("Error al rescatar el máximo número de operación de un documento con respecto al centro de costo y especialista para CVD");
                        Modulos.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Title = "Error de Acceso a Datos",
                            Type = TipoMensaje.Error,
                            Text = "Error al rescatar el máximo número de operación de un documento con respecto al centro de costo y especialista para CVD"
                        });
                    }
                    else
                    {
                        x = MODGRNG.SgteNumOpr(Modulos.MODGRNG, Modulos.Module1, Modulos.MODGUSR, Modulos.Mdi_Principal, unit, T_MODGUSR.IdPro_CVD, T_Mdl_Funciones_Varias.cod_producto_CVD, true, numMinRequerido: int.Parse(Nro_Disp_CVD));
                        if (x != 0)
                            //todo OK
                            Modulos.Module1.Codop_CVD = Modulos.Module1.Codop.Clone();
                        else
                            return false;
                    }

                    ofi = VB6Helpers.Trim(Modulos.MODGUSR.UsrEsp.OfixUser);
                    if (string.IsNullOrEmpty(ofi))
                    {
                        Modulos.Module1.Codop.Id_Empresa = "000";
                    }
                    else
                    {
                        if (ofi.IndexOf(";") < 0 && VB6Helpers.Len(ofi) == 3)
                        {
                            Modulos.Module1.Codop.Id_Empresa = ofi;
                            Modulos.FormularioQueAbrir = "SeleccionOficina";
                            Modulos.VieneDe = nombreAccion;
                            return false;
                            //Habilita = True
                        }
                        else
                        {
                            if (Modulos.MODGRNG.Rango_Permitido == true)
                            {
                                //FrmjOfi.DefInstance.Show(1);
                                Modulos.FormularioQueAbrir = "SeleccionOficina";
                                Modulos.VieneDe = nombreAccion;
                                return false;
                            }
                        }
                    }
                }

                Modulos.Frm_SeleccionOficina = null; // "cierro" la pantalla. (Se crea cuando se navega a su URL)

                ofi = VB6Helpers.Trim(Modulos.MODGUSR.UsrEsp.OfixUser);
                if (!string.IsNullOrEmpty(ofi) && string.IsNullOrEmpty(Modulos.Module1.Codop.Id_Empresa))
                {
                    Modulos.Module1.Codop.Id_Empresa = "000";
                }

                Modulos.MODGCVD.VgCVDNul.codcct = Modulos.Module1.Codop.Cent_Costo;
                //Modulos.MODGCVD.VgCVDNul.codpro = Modulos.Module1.Codop.Id_Product;
                Modulos.MODGCVD.VgCVDNul.codpro = T_MODGUSR.IdPro_Undefined;
                Modulos.MODGCVD.VgCVDNul.codesp = Modulos.Module1.Codop.Id_Especia;
                Modulos.MODGCVD.VgCVDNul.codofi = Modulos.Module1.Codop.Id_Empresa;
                Modulos.MODGCVD.VgCVDNul.codope = T_MODGUSR.CodOp_Undefined;//Modulos.Module1.Codop.Id_Operacion;
                Modulos.MODGCVD.VgCVDNul.OpeSin = Modulos.MODGCVD.VgCVDNul.codcct + Modulos.MODGCVD.VgCVDNul.codpro + Modulos.MODGCVD.VgCVDNul.codesp + Modulos.MODGCVD.VgCVDNul.codofi + Modulos.MODGCVD.VgCVDNul.codope;
                Modulos.MODGCVD.VgCVDNul.OpeCon = Modulos.MODGCVD.VgCVDNul.codcct + "-" + Modulos.MODGCVD.VgCVDNul.codpro + "-" + Modulos.MODGCVD.VgCVDNul.codesp + "-" + Modulos.MODGCVD.VgCVDNul.codofi + "-" + Modulos.MODGCVD.VgCVDNul.codope;
                if (Modulos.MODGCVD.COMISION == true)
                {
                    Modulos.Frm_Principal.Caption = "Comisiones Fund Transfer  " + Modulos.MODGCVD.VgCVDNul.OpeCon;
                }
                else
                {
                    Modulos.Frm_Principal.Caption = "Compra Venta de Divisas Fund Transfer  " + Modulos.MODGCVD.VgCVDNul.OpeCon;
                }

                Modulos.MODGCVD.VgCVDNul.estado = T_MODGCVD.ECvd_Ing;

                Modulos.MODGCVD.VgCvd = Modulos.MODGCVD.VgCVDNul.Copy();
                Modulos.MODXANU.VgAnu.AnuSin = 0;

                //Se cargan frases
                Modulos.MODGCVD.VgCvd.InsExp = MODGFRA.Put_InsEsp(Modulos.MODGFRA, "Carta Exportador", -1, "E");
                Modulos.Mdi_Principal.BUTTONS["tbr_planilla4"].Enabled = false;
                //Se inicializa variable tranf. interna
                Modulos.MODGCVD.TIN = false;
                Modulos.MODGCVD.NOTACRED = false;
                //Se inicializa variable de Reversa o Inyeccion
                Modulos.MODCARAB.Rev_o_Iny = "";
                Modulos.MODCARAB.Cuenta_Rev = 0;
                Modulos.MODCVDIM.Gvar_NotaCredito = 0;
                Modulos.MODCARMAS.CARGA_MASIVA = false;

                return true;
            }
        }

        private static void NuevaCVD_L(InitializationObject Modulos, UnitOfWorkCext01 unit)
        {
            short x;

            //Se limpia variable
            Modulos.Mdl_Funciones_Varias.LC_TRXID_MAN = "";

            Modulos.MODGCVD.BotPrt = 0;
            Modulos.Mdl_Funciones_Varias.CARGA_AUTOMATICA = 0;

            Modulos.Mdl_Funciones_Varias.LC_MONEDA = 0;
            Modulos.Mdl_Funciones_Varias.LC_MONTO = "";
            Modulos.Mdl_Funciones_Varias.LC_XREF = "";
            Modulos.Mdl_Funciones_Varias.LC_CONREFNUM = "";
            Modulos.Mdl_Funciones_Varias.LC_PRD = "";
            Modulos.Mdl_Funciones_Varias.LC_OUTGOING = "";
            Modulos.Mdl_Funciones_Varias.LC_INCOMING = "";
            Modulos.Mdl_Funciones_Varias.LC_ORD_INST1 = "";  //DESTINO FONDOS
            Modulos.Mdl_Funciones_Varias.LC_PMNT_DET1 = "";  //DESTINO FONDOS
            Modulos.Mdl_Funciones_Varias.LC_PMNT_DET2 = "";  //DESTINO FONDOS
            Modulos.Mdl_Funciones_Varias.LC_PMNT_DET3 = "";  //DESTINO FONDOS
            Modulos.Mdl_Funciones_Varias.LC_PMNT_DET4 = "";  //DESTINO FONDOS
            Modulos.Mdl_Funciones_Varias.LC_COD_TRANS = "";  //CODIGO TRANSACCION
            Modulos.Mdl_Funciones_Varias.Lc_BaseNumber = "";
            Modulos.Mdl_Funciones_Varias.LC_BASENUMBER_NUEVO = "";
            Modulos.Mdl_Funciones_Varias.LC_SWFT = "";
            Modulos.Mdl_Funciones_Varias.LC_SWFT = "";
            Modulos.Mdl_Funciones_Varias.LC_BEN_INST1 = "";  //BEN_INST1
            Modulos.Mdl_Funciones_Varias.LC_ULT_BEN1 = "";  //ULT_BEN1
            Modulos.Mdl_Funciones_Varias.LC_ULT_BEN2 = "";  //ULT_BEN2
            Modulos.Mdl_Funciones_Varias.LC_ULT_BEN3 = "";  //ULT_BEN3
            Modulos.Mdl_Funciones_Varias.LC_ULT_BEN4 = "";  //ULT_BEN4
            Modulos.Mdl_Funciones_Varias.LC_CHG_WHOM = "";  //CHG_WHOM
            Modulos.Mdl_Funciones_Varias.LC_FCCFT = "";  //FCCFT
            Modulos.Mdl_Funciones_Varias.LC_DRVALDT = "";  //DRVALDT
            Modulos.Mdl_Funciones_Varias.LC_NOM_MDA = "";  //NOMBRE MONEDA
            Modulos.Mdl_Funciones_Varias.LC_INTRMD1 = "";
            Modulos.Mdl_Funciones_Varias.LC_US_PAY_ID = "";
            Modulos.Mdl_Funciones_Varias.LC_RECVR_CORRES1 = "";
            Modulos.Mdl_Funciones_Varias.LC_RECVR_CORRES2 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO1 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO2 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO3 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO4 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO5 = "";
            Modulos.Mdl_Funciones_Varias.LC_SNDR_RECVR_INFO6 = "";

            Modulos.Frm_Principal.Tx_NomPrt.Text = "";
            //Modulos.Frm_Principal.Tx_RefCli.Text = "";
            Modulos.Frm_Principal.Tx_iva.Text = "";
            Modulos.Frm_Principal.Tx_moneda.Text = "";
            Modulos.Frm_Principal.Tx_MtoOri.Text = "";
            Modulos.Frm_Principal.Tx_neto.Text = "";
            Modulos.Frm_Principal.Tx_NroFac.Text = "";
            Modulos.Frm_Principal.Tx_tipo.Text = "";
            Modulos.Frm_Principal.Lt_CI.Clear();
            Modulos.Frm_Principal.Lt_CVE.Clear();
            Modulos.Frm_Principal.Lt_CVI.Clear();
            Modulos.Frm_Principal.Num_Op.Text = "";

            MODXVIA.Pr_Init_xVia(Modulos.MODXVIA, Modulos.MODGSWF, Modulos.MODGCHQ);
            MODXORI.Pr_Init_xOri(Modulos.MODXORI);

            Modulos.Mdl_Funciones_Varias.V_gCom = new T_gCom[0];
            Modulos.MODGMTA.VVdi = new T_Vdi[1];
            Modulos.MODGMTA.VVdi[0] = new T_Vdi();
            Modulos.MODGASO.VgAsoNul = new T_Aso();
            Modulos.MODGASO.VgAso = Modulos.MODGASO.VgAsoNul.Clone();

            //------------------------------------------------------------------------
            x = Module1.ResetParty(Modulos.Module1, Modulos.MODGCVD.Beneficiario);
            //------------------------------------------------------------------------
            Modulos.MODGCVD.VgPli = new T_gPli[0];

            //==>>>  por la diferencia de como se hacen las cosas en la web estas no deberian ir
            Modulos.MODGARB.VArb = new T_Arb[0];

            Modulos.MODGPLI1.Vplis = new T_Pli[0];
            Modulos.MODXPLN1.VxPlvs = new T_xPlv[0];
            Modulos.MODXANU.VxAnus = new T_xAnu[0];

            Modulos.MODXPLN0.VxDecP = new T_xDecP[0];
            Modulos.MODGANU.VAnuPl = new T_AnuPl[0];
            Modulos.MODXORI.Vx_SCodTran = new S_Codtran[0];
            //--Comisiones de Planillas.-
            Modulos.MODXPLN1.VCom_xPlv = Modulos.MODXPLN1.VCom_xPlvNul;
            Modulos.MODGCVD.VgCVDNul = Modulos.MODGCVD.VgCVDVacia;

            Modulos.Mdi_Principal.BUTTONS["tbr_Comercio_invisible"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_arbitrajes"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_vtas_export"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_participantes"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_comisiones"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_origfondos"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_Swift"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_planilla1"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_planilla2"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_planilla3"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_planilla4"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_operel"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_nota"].Enabled = false;
            Modulos.Mdi_Principal.BUTTONS["tbr_vueltos"].Enabled = false;
            Modulos.Frm_Principal.Caption = "Compra Venta de Divisas Fund Transfer";

            Modulos.MODGCVD.VgCvd = Modulos.MODGCVD.VgCVDNul;
            Modulos.MODXANU.VgAnu.AnuSin = 0;

            //Se cargan frases
            Modulos.MODGCVD.VgCvd.InsExp = MODGFRA.Put_InsEsp(Modulos.MODGFRA, "Carta Exportador", -1, "E");
            Modulos.Mdi_Principal.BUTTONS["tbr_planilla4"].Enabled = false;
            //Se inicializa variable tranf. interna
            Modulos.MODGCVD.TIN = false;
            Modulos.MODGCVD.NOTACRED = false;
            //Se inicializa variable de Reversa o Inyeccion
            Modulos.MODCARAB.Rev_o_Iny = "";
            Modulos.MODCARAB.Cuenta_Rev = 0;
            Modulos.MODCVDIM.Gvar_NotaCredito = 0;

            Modulos.MODCARMAS.CARGA_MASIVA = false;
        }

        //****************************************************************************
        //   1.  Carga los datos de las Operaciones realizadas en la Lista que corresponde.
        //****************************************************************************
        private static void Pr_Cargar_Listas(InitializationObject Modulos, short TipoCVD, QUE_LISTA lista)
        {
            short n = 0;
            short m = 0;
            short i = 0;
            StringBuilder s = new StringBuilder();
            //Arreglo de Planillas (Compra/Venta).
            n = (short)VB6Helpers.UBound(Modulos.MODGCVD.VgPli);

            //Arreglo de Arbitrajes.
            VB6Helpers.ClearError();
            m = (short)VB6Helpers.UBound(Modulos.MODGARB.VArb);

            IList<UI_ElementoLista_Frm_Principal> Lista = null;
            if (lista == QUE_LISTA.Lt_CI)
            {
                Lista = Modulos.Frm_Principal.Lt_CI;
            }
            else if (lista == QUE_LISTA.Lt_CVE)
            {
                Lista = Modulos.Frm_Principal.Lt_CVE;
            }
            else if (lista == QUE_LISTA.Lt_CVI)
            {
                Lista = Modulos.Frm_Principal.Lt_CVI;
            }

            switch (TipoCVD)
            {
                case 1:  //Compra - Venta.
                    Lista.Clear();
                    if (n >= 0)
                    {
                        for (i = 0; i <= (short)n; i++)
                        {
                            if (Modulos.MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                            {
                                //Verifica si es de Compra o de Venta.
                                string _switchVar1 = VB6Helpers.Trim(VB6Helpers.UCase(Modulos.MODGCVD.VgPli[i].TipCVD));
                                s.Clear();
                                if (_switchVar1 == "C")
                                {
                                    Lista.Add(new UI_ElementoLista_Frm_Principal()
                                    {
                                        Operacion = "Compra",
                                        Mnd_Compra = VB6Helpers.Trim(Modulos.MODGCVD.VgPli[i].NemMnd),
                                        Mto_Compra = MODGPYF0.forma(Modulos.MODGCVD.VgPli[i].MtoCVD, T_MODGCON0.FormatoConDec),
                                        Mnd_Venta = "-------------",
                                        Mto_Venta = "-----------------------------------"
                                    });
                                }
                                else if (_switchVar1 == "V" || _switchVar1 == "W")
                                {
                                    Lista.Add(new UI_ElementoLista_Frm_Principal()
                                    {
                                        Operacion = "Venta",
                                        Mnd_Compra = "-------------",
                                        Mto_Compra = "-----------------------------------",
                                        Mnd_Venta = VB6Helpers.Trim(Modulos.MODGCVD.VgPli[i].NemMnd),
                                        Mto_Venta = MODGPYF0.forma(Modulos.MODGCVD.VgPli[i].MtoCVD, T_MODGCON0.FormatoConDec)
                                    });
                                }
                                else if (_switchVar1 == "TI")
                                {
                                    Lista.Add(new UI_ElementoLista_Frm_Principal()
                                    {
                                        Operacion = "Transf. ingreso",
                                        Mnd_Compra = VB6Helpers.Trim(Modulos.MODGCVD.VgPli[i].NemMnd),
                                        Mto_Compra = MODGPYF0.forma(Modulos.MODGCVD.VgPli[i].MtoCVD, T_MODGCON0.FormatoConDec),
                                        Mnd_Venta = "-------------",
                                        Mto_Venta = "-----------------------------------"
                                    });
                                }
                                else if (_switchVar1 == "TE")
                                {
                                    Lista.Add(new UI_ElementoLista_Frm_Principal()
                                    {
                                        Operacion = "Transf. egreso",
                                        Mnd_Compra = "-------------",
                                        Mto_Compra = "-----------------------------------",
                                        Mnd_Venta = VB6Helpers.Trim(Modulos.MODGCVD.VgPli[i].NemMnd),
                                        Mto_Venta = MODGPYF0.forma(Modulos.MODGCVD.VgPli[i].MtoCVD, T_MODGCON0.FormatoConDec)
                                    });
                                }
                                else if (_switchVar1 == "TIN")
                                {
                                    Lista.Add(new UI_ElementoLista_Frm_Principal()
                                    {
                                        Operacion = "Transf. interna",
                                        Mnd_Compra = "-------------",
                                        Mto_Compra = "-----------------------------------",
                                        Mnd_Venta = VB6Helpers.Trim(Modulos.MODGCVD.VgPli[i].NemMnd),
                                        Mto_Venta = MODGPYF0.forma(Modulos.MODGCVD.VgPli[i].MtoCVD, T_MODGCON0.FormatoConDec)
                                    });
                                }
                            }
                        }
                    }

                    //Arbitraje.
                    break;
                case 2:
                    Lista.Clear();
                    if (m >= 0)
                    {
                        for (i = 0; i <= (short)m; i++)
                        {
                            if (Modulos.MODGARB.VArb[i].Status != T_MODGCVD.EstadoEli)
                            {
                                Lista.Add(new UI_ElementoLista_Frm_Principal()
                                {
                                    Operacion = "Arbitraje",
                                    Mnd_Compra = VB6Helpers.Trim(Modulos.MODGARB.VArb[i].NemMndC),
                                    Mto_Compra = MODGPYF0.forma(Modulos.MODGARB.VArb[i].MtoCom, T_MODGCON0.FormatoConDec),
                                    Mnd_Venta = VB6Helpers.Trim(Modulos.MODGARB.VArb[i].NemMndV),
                                    Mto_Venta = MODGPYF0.forma(Modulos.MODGARB.VArb[i].MtoVta, T_MODGCON0.FormatoConDec)
                                });
                            }
                        }
                    }

                    break;
                case 3:  //Visible Exportación.
                    //Mto. a Liquidar.
                    Lista.Clear();
                    if (Modulos.MODXPLN0.VxDatP.MtoLiq > 0)
                    {
                        Lista.Add(new UI_ElementoLista_Frm_Principal()
                        {
                            Operacion = "Mto. Liq.",
                            Moneda = VB6Helpers.Trim(Modulos.MODXPLN0.VxDatP.NemMnd),
                            Monto = MODGPYF0.forma(Modulos.MODXPLN0.VxDatP.MtoLiq, T_MODGCON0.FormatoConDec)
                        });
                    }

                    //Mto. a Informar.
                    if (Modulos.MODXPLN0.VxDatP.MtoInf > 0)
                    {
                        Lista.Add(new UI_ElementoLista_Frm_Principal()
                        {
                            Operacion = "Mto. Inf.",
                            Moneda = VB6Helpers.Trim(Modulos.MODXPLN0.VxDatP.NemMnd),
                            Monto = MODGPYF0.forma(Modulos.MODXPLN0.VxDatP.MtoInf, T_MODGCON0.FormatoConDec)
                        });
                    }

                    //Mto. Estadístico.
                    if (Modulos.MODXPLN0.VxDatP.mtotran > 0)
                    {
                        Lista.Add(new UI_ElementoLista_Frm_Principal()
                        {
                            Operacion = "Mto. Tran",
                            Moneda = VB6Helpers.Trim(Modulos.MODXPLN0.VxDatP.NemMnd),
                            Monto = MODGPYF0.forma(Modulos.MODXPLN0.VxDatP.mtotran, T_MODGCON0.FormatoConDec)
                        });
                    }
                    break;
            }
        }

        //Realiza el Cobro de la Comisión de Anulación.-
        private static bool CobraComis(InitializationObject Modulos, UnitOfWorkCext01 unit, string nombreView)
        {
            using (var trace = new Tracer("CobraComis"))
            {
                short m = Modulos.MODXPLN0.VxDatP.CodMnd;
                short a = 0;
                short h = 0;
                short n1 = 0;
                double Valor = 0;
                short x = 0;
                short i = 0;
                short NtcpI = 0;
                short NtcpE = 0;
                short n2 = 0;
                double MtoCom = 0;
                double MtoIva = 0;
                short c = 0;
                short n3 = 0;
                short n4 = 0;
                short Npa = 0;
                short im = 0;
                short hayiva = 0;
                string Cta_Nem = "";

                // UPGRADE_INFO (#0561): The 'TcpCobrar' symbol was defined without an explicit "As" clause.
                const string TcpCobrar = "25111901K;251305014;253103020";

                //Verifica Aceptación de la Operación.-
                if (m == 0)
                {
                    m = Modulos.MODGCVD.CodMonDolar;
                }
                a = MODGTAB0.SyGet_Vmd(Modulos.MODGTAB0, unit, DateTime.Now.ToString("yyyy-MM-dd"), m);
                bool entraAIngresoValores = (Modulos.MODGTAB0.VVmd.VmdPrd == 0 || Modulos.MODGTAB0.VVmd.VmdObs == 0);
                if (entraAIngresoValores && (~a != 0))
                {
                    trace.TraceError("No se han encontrado los valores de las Paridades y Tipos de Cambio para efectuar el Cobro de Comisiones. Debe ingresar los valores manualmente.");
                    Modulos.Mdi_Principal.MESSAGES.Add(new UI_Message()
                    {
                        Text = "No se han encontrado los valores de las Paridades y Tipos de Cambio para efectuar el Cobro de Comisiones. Debe ingresar los valores manualmente.",
                        Type = TipoMensaje.Informacion
                    });
                    Modulos.MODGTAB0.VVmd.VmdCod = m;
                    Modulos.MODGTAB0.VVmd.VmdFec = DateTime.Now.ToString("yyyy-MM-dd");
                    //Frm_Ingreso_Valores.DefInstance.Show(1);
                    Modulos.FormularioQueAbrir = "Ingreso_Valores";
                    Modulos.Frm_Ingreso_Valores = new UI_Frm_Ingreso_Valores();
                    Modulos.Frm_Ingreso_Valores.VieneDe = nombreView;
                    Modulos.Frm_Ingreso_Valores.MensageCancelacion = "Como no ha ingresado los valores de Tipo de Cambio y Paridad, no se puede efectuar el Cobro de Comisiones.";
                    return false;//false indica que no debe seguir
                }

                //Lee las Comisiones.-
                h = MODGMTA.SyGetn_Com(Modulos.MODGMTA, unit);
                n1 = MODGMTA.Find_VCom(Modulos.MODGMTA, "SER", "PLN", "CPE");

                //Comisiones de Planillas.-
                Valor = Modulos.MODXPLN1.VCom_xPlv.MtoCom;
                Modulos.Mdl_Funciones_Varias.V_gCom = new T_gCom[0];
                if (Modulos.MODXPLN1.VCom_xPlv.MtoIva > 0)
                {
                    x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, "Comisión Planillas", "US$", Valor, -1, Modulos.MODGTAB0.VVmd.VmdObs, Modulos.MODGMTA.VCom[n1].cta_mn);
                }
                else
                {
                    x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, "Comisión Planillas", "US$", Valor, 0, Modulos.MODGTAB0.VVmd.VmdObs, Modulos.MODGMTA.VCom[n1].cta_mn);
                }

                //Comisiones de Venta de Divisas.-
                for (i = 0; i <= (short)VB6Helpers.UBound(Modulos.MODGCVD.VgPli); i++)
                {
                    if (Modulos.MODGCVD.VgPli[i].Status != T_MODGCVD.EstadoEli)
                    {
                        if (Modulos.MODGCVD.VgPli[i].TipCVD == "V" && VB6Helpers.Instr(TcpCobrar, Modulos.MODGCVD.VgPli[i].CodTcp) != 0)
                        {
                            NtcpI = (short)(NtcpI + 1);
                        }

                        if (Modulos.MODGCVD.VgPli[i].TipCVD == "W" && VB6Helpers.Instr(TcpCobrar, Modulos.MODGCVD.VgPli[i].CodTcp) != 0)
                        {
                            NtcpE = (short)(NtcpE + 1);
                        }

                    }

                }

                //Venta Divisas Import.-
                n2 = MODGMTA.Find_VCom(Modulos.MODGMTA, "SER", "CFS", "SIN");
                if (NtcpI > 0)
                {
                    MtoCom = 0;
                    MtoIva = 0;
                    Modulos.MODGMTA.VCon = new T_Con[1];
                    MODGMTA.LlenaDatCob(Modulos.MODGMTA, "", "SER", "CFS", "SIN", DateTime.Now.ToString("yyyy-MM-dd"), Modulos.MODGSCE.VGen.MndDol, Modulos.MODGSCE.VGen.MndNac, Modulos.MODGSCE.VGen.MndDol, 0, 0, 0);
                    c = MODGMTA.Cobrar(Modulos.MODGMTA, Modulos.MODGPYF0, Modulos.MODGTAB0, Modulos.MODGCHQ, Modulos.Mdi_Principal, Modulos.Frm_Ingreso_Valores, unit);
                    MtoCom = Modulos.MODGMTA.VCon[VB6Helpers.UBound(Modulos.MODGMTA.VCon)].MtoCob;
                    MtoIva = Modulos.MODGMTA.VCon[VB6Helpers.UBound(Modulos.MODGMTA.VCon)].ivacon;
                }

                if (MtoIva > 0)
                {
                    x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, "Venta Divisas Import.", "US$", MtoCom * NtcpI, -1, Modulos.MODGTAB0.VVmd.VmdObs, Modulos.MODGMTA.VCom[n2].cta_mn);
                }
                else
                {
                    x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, "Venta Divisas Import.", "US$", MtoCom * NtcpI, 0, Modulos.MODGTAB0.VVmd.VmdObs, Modulos.MODGMTA.VCom[n2].cta_mn);
                }

                //Venta Divisas Export.-
                n3 = MODGMTA.Find_VCom(Modulos.MODGMTA, "SER", "EFS", "SIN");
                if (NtcpE > 0)
                {
                    MtoCom = 0;
                    MtoIva = 0;
                    Modulos.MODGMTA.VCon = new T_Con[1];
                    MODGMTA.LlenaDatCob(Modulos.MODGMTA, MODGPYF0.Componer(Modulos.Module1.PartysOpe[0].LlaveArchivo, "~", "|"), "SER", "EFS", "SIN", DateTime.Now.ToString("yyyy-MM-dd"), Modulos.MODGSCE.VGen.MndDol, Modulos.MODGSCE.VGen.MndNac, Modulos.MODGSCE.VGen.MndDol, 0, 0, 0);
                    c = MODGMTA.Cobrar(Modulos.MODGMTA, Modulos.MODGPYF0, Modulos.MODGTAB0, Modulos.MODGCHQ, Modulos.Mdi_Principal, Modulos.Frm_Ingreso_Valores, unit);
                    MtoCom = Modulos.MODGMTA.VCon[VB6Helpers.UBound(Modulos.MODGMTA.VCon)].MtoCob;
                    MtoIva = Modulos.MODGMTA.VCon[VB6Helpers.UBound(Modulos.MODGMTA.VCon)].ivacon;
                }

                if (MtoIva > 0)
                {
                    x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, "Venta Divisas Export.", "US$", MtoCom * NtcpE, -1, Modulos.MODGTAB0.VVmd.VmdObs, Modulos.MODGMTA.VCom[n3].cta_mn);
                }
                else
                {
                    x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, "Venta Divisas Export.", "US$", MtoCom * NtcpE, 0, Modulos.MODGTAB0.VVmd.VmdObs, Modulos.MODGMTA.VCom[n3].cta_mn);
                }

                //Comisiones de Planillas Anuladas.-
                n4 = MODGMTA.Find_VCom(Modulos.MODGMTA, "SER", "PLN", "ANU");
                MtoCom = 0;
                MtoIva = 0;
                for (i = 1; i <= (short)VB6Helpers.UBound(Modulos.MODXANU.VxAnus); i++)
                {
                    if (Modulos.MODXANU.VxAnus[i].Estado != T_MODGCVD.EstadoEli && Modulos.MODXANU.VxAnus[i].MtoAnu > 0)
                    {
                        Npa = (short)(Npa + 1);
                    }

                }

                if (Npa > 0)
                {
                    //Modulos.MODGMTA.VCon = new T_Con[1];
                    VB6Helpers.Redim(ref Modulos.MODGMTA.VCon, 0, 0);
                    MODGMTA.LlenaDatCob(Modulos.MODGMTA, "", "SER", "PLN", "ANU", DateTime.Now.ToString("yyyy-MM-dd"), Modulos.MODGSCE.VGen.MndDol, Modulos.MODGSCE.VGen.MndNac, Modulos.MODGSCE.VGen.MndDol, 0, 0, 0);
                    c = MODGMTA.Cobrar(Modulos.MODGMTA, Modulos.MODGPYF0, Modulos.MODGTAB0, Modulos.MODGCHQ, Modulos.Mdi_Principal, Modulos.Frm_Ingreso_Valores, unit);
                    MtoCom = Modulos.MODGMTA.VCon[VB6Helpers.UBound(Modulos.MODGMTA.VCon)].MtoCob;
                    MtoIva = Modulos.MODGMTA.VCon[VB6Helpers.UBound(Modulos.MODGMTA.VCon)].ivacon;
                }

                if (MtoIva > 0)
                {
                    x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, "Anulación Planillas", "US$", MtoCom * Npa, -1, Modulos.MODGTAB0.VVmd.VmdObs, Modulos.MODGMTA.VCom[n4].cta_mn);
                }
                else
                {
                    x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, "Anulación Planillas", "US$", MtoCom * Npa, 0, Modulos.MODGTAB0.VVmd.VmdObs, Modulos.MODGMTA.VCom[n4].cta_mn);
                }

                //Venta Visibles Divisas Import.-
                if (Modulos.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_VisImp)
                {
                    MtoCom = 0;
                    MtoIva = 0;
                    Modulos.MODGMTA.VCon = new T_Con[1];
                    MODGMTA.LlenaDatCob(Modulos.MODGMTA, MODGPYF0.Componer(Modulos.MODCVDIMMM.VComI.Party, "~", "|"), Modulos.MODCVDIMMM.VComI.codsis, Modulos.MODCVDIMMM.VComI.codpro, Modulos.MODCVDIMMM.VComI.CodEta, DateTime.Now.ToString("yyyy-MM-dd"), Modulos.MODGSCE.VGen.MndDol, Modulos.MODGSCE.VGen.MndNac, Modulos.MODCVDIMMM.VComI.CodMon, Modulos.MODCVDIMMM.VComI.MtoCob, 0, 0);
                    c = MODGMTA.Cobrar(Modulos.MODGMTA, Modulos.MODGPYF0, Modulos.MODGTAB0, Modulos.MODGCHQ, Modulos.Mdi_Principal, Modulos.Frm_Ingreso_Valores, unit);
                    for (im = 0; im <= (short)VB6Helpers.UBound(Modulos.MODGMTA.VCon); im++)
                    {
                        MtoCom = Modulos.MODGMTA.VCon[im].MtoCob;
                        MtoIva = Modulos.MODGMTA.VCon[im].ivacon;
                        if (Modulos.MODGMTA.VCon[im].tipcon != T_MODGMTA.EsVdi && Modulos.MODGMTA.VCon[im].tipcon != T_MODGMTA.EsIva && Modulos.MODGMTA.VCon[im].tipcon != T_MODGMTA.EsSch && Modulos.MODGMTA.VCon[im].tipcon != T_MODGMTA.EsRei)
                        {
                            hayiva = (short)(true ? -1 : 0);
                        }
                        else
                        {
                            hayiva = (short)(false ? -1 : 0);
                        }

                        Modulos.MODCVDIMMM.VComI.TipCnp = Modulos.MODGMTA.VCon[im].glscon;
                        Cta_Nem = MODCVDIMMM.GetCtaCbe(Modulos.MODCVDIMMM, Modulos.MODGTAB0, Modulos.MODGSCE, Modulos.Mdi_Principal, Modulos.MODGMTA.VCon[im].tipcon);
                        x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, Modulos.MODCVDIMMM.VComI.TipCnp, "US$", MtoCom, hayiva, Modulos.MODCVDIMMM.VComI.TipCmC, Cta_Nem);
                    }

                }

                //******************************************************
                //Venta Divisas Import.-
                if (Modulos.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_PlanSO)
                {
                    Modulos.MODCVDIMMM.VComI = new VI_Com();//Cambio 2015-09-14.
                    n2 = MODGMTA.Find_VCom(Modulos.MODGMTA, "SER", "CFS", "SIN");
                    Modulos.MODCVDIMMM.VComI.Party = Modulos.Module1.PartysOpe[T_MODGCVD.ICli].LlaveArchivo;
                    //-------------------------------------------------------------------------------------------------------------------
                    //Código Nuevo-Inicio
                    //Fecha Modificación 05-09-2011
                    //Responsable: Angel Donoso Gonzalez.
                    //Empresa: Accenture
                    //Versión:
                    //Descripción : se deja como constante codigo de moneda dolar para las comisiones de ventas visibles de importación
                    //              ya que estas comisiones siempre son cobradas con esta moneda.
                    //-------------------------------------------------------------------------------------------------------------------
                    Modulos.MODCVDIMMM.VComI.CodMon = T_MODGTAB0.MndDol;  //ARREGLAR
                                                                          //--------------------------------------------------------------------------------------------------
                                                                          // Accenture - Código Nuevo - Termino
                                                                          //--------------------------------------------------------------------------------------------------

                    //--------------------------------------------------------------------------------------------------
                    //Antiguo código - Inicio
                    //Fecha Modificación 20101222
                    //Responsable: Angel Donoso Gonzalez.
                    //Empresa: Accenture
                    //--------------------------------------------------------------------------------------------------
                    // VComI.CodMon = Vx_PReem(1).CodMon     'ARREGLAR
                    //--------------------------------------------------------------------------------------------------
                    // Accenture - Antiguo código - Termino
                    //--------------------------------------------------------------------------------------------------
                    Modulos.MODCVDIMMM.VComI.codsis = "SER";  //ARREGlAR
                    Modulos.MODCVDIMMM.VComI.codpro = "CFS";  //ARREGLAR
                    Modulos.MODCVDIMMM.VComI.CodEta = "SIN";
                    MtoCom = 0;
                    MtoIva = 0;

                    //Modulos.MODGMTA.VCon = new T_Con[1];
                    VB6Helpers.RedimPreserve(ref Modulos.MODGMTA.VCon, 0, 0);// Cambio 2015-09-14.

                    MODGMTA.LlenaDatCob(Modulos.MODGMTA, MODGPYF0.Componer(Modulos.MODCVDIMMM.VComI.Party, "~", "|"), "SER", "CFS", "SIN", DateTime.Now.ToString("yyyy-MM-dd"), Modulos.MODGSCE.VGen.MndDol, Modulos.MODGSCE.VGen.MndNac, Modulos.MODCVDIMMM.VComI.CodMon, Modulos.MODCVDIMMM.VComI.MtoCob, 0, 0);

                    c = MODGMTA.Cobrar(Modulos.MODGMTA, Modulos.MODGPYF0, Modulos.MODGTAB0, Modulos.MODGCHQ, Modulos.Mdi_Principal, Modulos.Frm_Ingreso_Valores, unit);

                    for (im = 0; im <= (short)VB6Helpers.UBound(Modulos.MODGMTA.VCon); im++)
                    {
                        MtoCom = Modulos.MODGMTA.VCon[im].MtoCob;
                        MtoIva = Modulos.MODGMTA.VCon[im].ivacon;
                        if (Modulos.MODGMTA.VCon[im].tipcon != T_MODGMTA.EsVdi && Modulos.MODGMTA.VCon[im].tipcon != T_MODGMTA.EsIva && Modulos.MODGMTA.VCon[im].tipcon != T_MODGMTA.EsSch && Modulos.MODGMTA.VCon[im].tipcon != T_MODGMTA.EsRei)
                        {
                            hayiva = (short)(true ? -1 : 0);
                        }
                        else
                        {
                            hayiva = (short)(false ? -1 : 0);
                        }

                        Modulos.MODCVDIMMM.VComI.TipCnp = Modulos.MODGMTA.VCon[im].glscon;
                        Cta_Nem = MODCVDIMMM.GetCtaCbe(Modulos.MODCVDIMMM, Modulos.MODGTAB0, Modulos.MODGSCE, Modulos.Mdi_Principal, Modulos.MODGMTA.VCon[im].tipcon);
                        if (Modulos.MODCVDIMMM.VComI.TipCnp != null)
                        {
                            x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, Modulos.MODCVDIMMM.VComI.TipCnp, "US$", MtoCom, hayiva, Modulos.MODCVDIMMM.VComI.TipCmC, Cta_Nem);
                        }
                    }

                    if (MtoIva > 0)
                    {
                        x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, "Venta Divisas Import.", "US$", MtoCom * NtcpI, -1, Modulos.MODGTAB0.VVmd.VmdObs, Modulos.MODGMTA.VCom[n2].cta_mn);
                    }
                    else
                    {
                        x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, "Venta Divisas Import.", "US$", MtoCom * NtcpI, 0, Modulos.MODGTAB0.VVmd.VmdObs, Modulos.MODGMTA.VCom[n2].cta_mn);
                    }

                }

                if (Modulos.MODGUSR.UsrEsp.CentroCosto == "753" || Modulos.MODGUSR.UsrEsp.CentroCosto == "714")
                {
                    SyGetComis(Modulos, unit, "SER", "MNE");
                    for (i = 0; i <= (short)VB6Helpers.UBound(Modulos.MODGANU.Comis); i++)
                    {
                        x = Mdl_Funciones_Varias.Put_Gcom(Modulos.Mdl_Funciones_Varias, Modulos.MODGSCE, Modulos.Mdi_Principal, 0, Modulos.MODGANU.Comis[i].glocon, "US$", 0, Modulos.MODGANU.Comis[i].hayiva, Modulos.MODGTAB0.VVmd.VmdObs, Modulos.MODGANU.Comis[i].ctamn);
                    }

                }

                for (i = 0; i <= (short)VB6Helpers.UBound(Modulos.Mdl_Funciones_Varias.V_gCom); i++)
                {
                    if (((Modulos.Mdl_Funciones_Varias.V_gCom[i].MtoCom > 0 || Modulos.Mdl_Funciones_Varias.V_gCom[i].ConIVA > 0 || Modulos.Mdl_Funciones_Varias.V_gCom[i].MtoIva > 0 || Modulos.Mdl_Funciones_Varias.V_gCom[i].MtoIvap > 0 || Modulos.Mdl_Funciones_Varias.V_gCom[i].MtoTot > 0 || Modulos.Mdl_Funciones_Varias.V_gCom[i].MtoComp > 0 ? -1 : 0) | (Modulos.Mdl_Funciones_Varias.V_gCom[i].MtoTotp > 0 ? -1 : 0)) != 0)
                    {
                        Modulos.MODGCVD.VgCvd.Etapa += "ORI";
                        break;
                    }
                }

                //Habilita Botón Comisiones.-
                //Call Pr_HabilitaBot(18, True)
                if (Modulos.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 0)
                {
                    HabilitarOrigenDestinoFondos(Modulos);
                    Modulos.Mdi_Principal.BUTTONS["tbr_comisiones"].Enabled = true;
                }
                return true;
            }
        }

        public static dynamic SyGetComis(InitializationObject Modulos, UnitOfWorkCext01 unit, string CodigoSis, string CodigoPro)
        {
            T_MODGANU MODGANU = Modulos.MODGANU;
            dynamic _retValue = null;
            string Que = "";
            short n = 0;
            string R = "";
            short i = 0;
            MODGANU.Comis = new T_Comis[1];
            try
            {

                MODGANU.Comis = unit.SceRepository.EjecutarSP<sce_mta1_s09_MS_Result>("sce_mta1_s09_MS", CodigoSis, CodigoPro).Select(
                    x => new T_Comis()
                    {
                        glocon = x.glocon,
                        ctamn = x.cta_mn,
                        ctame = x.cta_me,
                        hayiva = (short)(x.hayiva ? -1 : 0)
                    }).ToArray();
                _retValue = true;
            }
            catch (Exception ex)
            {
                _retValue = null;
            }
            return _retValue;
        }
        #endregion

        public static void Ventas_Vis_Import_PosShow(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            using (var trace = new Tracer("Ventas_Vis_Import_PosShow"))
            {
                T_Vdi Aux_Vdi = new T_Vdi();
                short j = 0;
                short Plan = 0;

                initObject.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
                initObject.Mdi_Principal.BUTTONS["tbr_Comercio_invisible"].Enabled = false;
                initObject.Mdi_Principal.BUTTONS["tbr_arbitrajes"].Enabled = false;
                initObject.Mdi_Principal.BUTTONS["tbr_vtas_export"].Enabled = false;
                initObject.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;

                if (initObject.MODANUVI.Vx_AnuReem.AcepRee == -1)
                {
                    var m = initObject.MODXPLN0.VxDatP.CodMnd;
                    if (m == 0)
                    {
                        m = initObject.MODGCVD.CodMonDolar;
                    }
                    var a = MODGTAB0.SyGet_Vmd(initObject.MODGTAB0, uow, DateTime.Now.ToString("yyyy-MM-dd"), m);
                    bool entraAIngresoValores = (initObject.MODGTAB0.VVmd.VmdPrd == 0 || initObject.MODGTAB0.VVmd.VmdObs == 0);
                    if (entraAIngresoValores && (~a != 0))
                    {
                        trace.TraceError("No se han encontrado los valores de las Paridades y Tipos de Cambio para efectuar el Cobro de Comisiones. Debe ingresar los valores manualmente.");
                        initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Text = "No se han encontrado los valores de las Paridades y Tipos de Cambio para efectuar el Cobro de Comisiones. Debe ingresar los valores manualmente.",
                            Type = TipoMensaje.Informacion
                        });
                        initObject.MODGTAB0.VVmd.VmdCod = m;
                        initObject.MODGTAB0.VVmd.VmdFec = DateTime.Now.ToString("yyyy-MM-dd");

                        initObject.FormularioQueAbrir = "Ingreso_Valores";
                        initObject.Frm_Ingreso_Valores = new UI_Frm_Ingreso_Valores();
                        initObject.Frm_Ingreso_Valores.VieneDe = "PlvSO_Finish";
                        return;
                    }
                    Plan = (short)VB6Helpers.UBound(initObject.MODPREEM.Vx_PReem);

                    for (j = 0; j <= (short)Plan; j++)
                    {
                        //Comision de Venta Divisas.-
                        if (initObject.MODPREEM.Vx_PReem[j].Estado != 9 && !string.IsNullOrEmpty(initObject.MODPREEM.Vx_PReem[j].numdec))
                        {
                            Aux_Vdi.numdec = initObject.MODPREEM.Vx_PReem[j].numdec;
                            Aux_Vdi.FecDec = initObject.MODPREEM.Vx_PReem[j].FecDec;
                            Aux_Vdi.MonDec = initObject.MODPREEM.Vx_PReem[j].CodMon;
                            Aux_Vdi.MtoDec = initObject.MODPREEM.Vx_PReem[j].MtoCif;
                            Aux_Vdi.TipCam = initObject.MODPREEM.Vx_PReem[j].TipCamo;
                            MODGMTA.Llena_Vdi(initObject, ref Aux_Vdi);
                        }
                    }

                    if (CobraComis(initObject, uow, "PlvSO_Finish"))
                    {

                        if (initObject.MODANUVI.Vx_AnuReem.TotPln >= 1)
                        {
                            initObject.Mdi_Principal.BUTTONS["tbr_planilla4"].Enabled = true;
                        }

                        if (MODPREEM.Fn_HayViasSO(initObject) != 0)
                        {
                            initObject.MODGCVD.VgCvd.Etapa = "VIA";
                            initObject.Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled = true;
                            initObject.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { initObject.Mdi_Principal.BUTTONS[key].Focus = (key == "tbr_comisiones" ? true : false); });
                        }

                        //Orígenes de Fondos para las Planillas.-
                        if (((MODPREEM.Fn_TotOriSO(initObject) > 0 ? -1 : 0) | (Mdl_Funciones_Varias.TotalComis(initObject.Mdl_Funciones_Varias) > 0 ? -1 : 0)) != 0)
                        {
                            initObject.Mdi_Principal.BUTTONS["tbr_origfondos"].Enabled = true;
                            initObject.MODGCVD.VgCvd.Etapa += "ORI";
                        }
                    }
                }
                initObject.FormularioQueAbrir = "Index";
            }
        }

        public static void Ventas_Vis_Import(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            initObject.MODGCVD.VgCvd.TipCVD = T_MODGCVD.TCvd_PlanSO;
            initObject.Frm_PlvSO = new UI_Frm_PlvSO();
            initObject.Mdi_Principal.BUTTONS["tbr_Comercio_invisible"].Enabled = false;
            initObject.Mdi_Principal.BUTTONS["tbr_arbitrajes"].Enabled = false;
            initObject.Mdi_Principal.BUTTONS["tbr_vtas_export"].Enabled = false;
            initObject.Mdi_Principal.BUTTONS["tbr_vtas_import"].Enabled = false;
        }

        public static void Ventas_Vis_Import_PosCobraComis(InitializationObject initObject, UnitOfWorkCext01 uow)
        {
            using (var trace = new Tracer("Ventas_Vis_Import_PosCobraComis"))
            {
                if (CobraComis(initObject, uow, "Index"))
                {

                    if (initObject.MODANUVI.Vx_AnuReem.TotPln >= 1)
                    {
                        initObject.Mdi_Principal.BUTTONS["tbr_planilla4"].Enabled = true;
                    }

                    if (MODPREEM.Fn_HayViasSO(initObject) != 0)
                    {
                        initObject.MODGCVD.VgCvd.Etapa = "VIA";
                        initObject.Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled = true;
                    }

                    //Orígenes de Fondos para las Planillas.-
                    if (((MODPREEM.Fn_TotOriSO(initObject) > 0 ? -1 : 0) | VB6Helpers.CInt(Mdl_Funciones_Varias.TotalComis(initObject.Mdl_Funciones_Varias))) != 0)
                    {
                        initObject.Mdi_Principal.BUTTONS["tbr_origfondos"].Enabled = true;
                        initObject.MODGCVD.VgCvd.Etapa += "ORI";
                    }
                }
                else
                {
                    trace.TraceError("Ya fue realizado el Cobro de la Comisión de Anulación.");
                    throw new Exception("Ya fue realizado el Cobro de la Comisión de Anulación.");
                }
            }
        }

        /// <summary>
        /// Seleccion a Reversar Operaion Export antes de hacer el show del formulario
        /// </summary>
        /// <param name="initObj"></param>
        /// <param name="uow"></param>
        public static void Opciones_Click_ReversarOperacionExport(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            if (initObj.frmgrev == null)//Entra por primera vez al formulario.
            {
                initObj.MODXORI.Vx_SCodTran = new S_Codtran[0];
                initObj.MODGCVD.VgCvd.TipCVD = T_MODGCVD.TCvd_Rev;

                if (NuevaCVD_I(initObj, uow, "ReversarOperacionExport"))
                {
                    //Descripción : Se genera Transaction ID
                    if (string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.LC_TRXID_MAN))
                    {
                        initObj.Mdl_Funciones_Varias.LC_TRXID_MAN = MODGCVD.GeneraTRXID(initObj.MODGCVD.VgCvd.OpeSin, uow, initObj.Mdi_Principal.MESSAGES);
                    }

                    initObj.MODGCVD.VgCvd.TipCVD = T_MODGCVD.TCvd_Rev;
                    initObj.MODXANU.VgAnu.codcct = initObj.MODGCVD.VgCvd.codcct;
                    initObj.MODXANU.VgAnu.codpro = initObj.MODGCVD.VgCvd.codpro;
                    initObj.MODXANU.VgAnu.codesp = initObj.MODGCVD.VgCvd.codesp;
                    initObj.MODXANU.VgAnu.codofi = initObj.MODGCVD.VgCvd.codofi;
                    initObj.MODXANU.VgAnu.codope = initObj.MODGCVD.VgCvd.codope;

                    initObj.MODXANU.Habilita = true;

                    if (initObj.MODXANU.Habilita)
                    {
                        initObj.frmgrev = new UI_Frmgrev();
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            initObj.frmgrev = null;

            if (initObj.MODGCVD.VgCvd.AceptoRev == -1)
            {
                //Realiza el Cobro de Comisiones.-
                if (CobraComis(initObj, uow, "ReversarOperacionExport_CobraComis"))
                {
                    HabilitarOrigenDestinoFondos(initObj);
                    initObj.FormularioQueAbrir = "Index";
                }
            }
            else
            {
                initObj.FormularioQueAbrir = "Index";
            }
        }

        /// <summary>
        /// Habilitar e inicializar las vias despues de la ejecucion de Reversar Operaciones
        /// </summary>
        /// <param name="initObj"></param>
        public static void HabilitarOrigenDestinoFondos(InitializationObject initObj)
        {
            //Habilitar Vía sólo si es necesario.-
            if (MODCONT.HayPlnConVia(initObj) != 0)
            {
                initObj.MODGCVD.VgCvd.Etapa = "VIA";
                initObj.Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled = true;
                MODXVIA.Pr_Init_xVia(initObj.MODXVIA, initObj.MODGSWF, initObj.MODGCHQ);
            }
            else
            {
                //Origenes de Fondos para las Planillas.-
                if (Mdl_Funciones_Varias.TotalComis(initObj.Mdl_Funciones_Varias) > 0)
                {
                    MODGCVD.Pr_HabilitaBotonMenu(23, -1, initObj);
                    initObj.MODGCVD.VgCvd.Etapa += "ORI";
                    MODXORI.Pr_Init_xOri(initObj.MODXORI);
                }
            }
        }

        public static bool NuevaCVD2(InitializationObject initObj, UnitOfWorkCext01 uow, string Modo)
        {
            using (var trace = new Tracer("NuevaCVD2"))
            {
                var MODGCVD = initObj.MODGCVD;
                var MODGUSR = initObj.MODGUSR;
                var Module1 = initObj.Module1;

                short x = 0;
                string Nro_Disp = "";
                string ofi = "";
                //----------------------------------------
                //Realsystems-Código Nuevo-Inicio
                //Fecha Modificación 20100615
                //Responsable: Pablo Millan
                //Versión: 1.0
                //Descripción : Se limpia variable
                //----------------------------------------
                initObj.Mdl_Funciones_Varias.LC_TRXID_MAN = "";
                //----------------------------------------
                // RealSystems - Código Nuevo - Termino
                //----------------------------------------
                if (Modo == "I")
                {
                    //Modo Ingreso.
                    MODGCVD.AntOper = 0;
                    MODGCVD.Antmon = 0;
                    MODGCVD.Anttip = 0;

                    if (MODGUSR.UsrEsp.Tipeje == "O")
                    {
                        initObj.Mdi_Principal.BUTTONS["tbr_grabar"].Enabled = true;
                    }

                    initObj.Mdi_Principal.BUTTONS["tbr_participantes"].Enabled = true;
                    initObj.Mdi_Principal.BUTTONS["tbr_comisiones"].Enabled = false;

                    //Se limpia el id_operación para que obtenga uno nuevo
                    Module1.Codop.Id_Operacion = string.Empty;

                    Nro_Disp = MODGRNG.SyGetMax_Rng(uow, initObj.MODGUSR.UsrEsp.CentroCosto + T_MODGUSR.IdPro_ComVen + initObj.MODGUSR.UsrEsp.Especialista);

                    if (Nro_Disp.Equals("-1"))
                    {
                        trace.TraceError("Error al rescatar el máximo número de operación de un documento con respecto al centro de costo y especialista para FundTransfer");
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Title = "Error de Acceso a Datos",
                            Type = TipoMensaje.Error,
                            Text = "Error al rescatar el máximo número de operación de un documento con respecto al centro de costo y especialista para FundTransfer"
                        });
                    }
                    else
                    {
                        x = MODGRNG.SgteNumOpr(initObj.MODGRNG, initObj.Module1, initObj.MODGUSR, initObj.Mdi_Principal, uow, T_MODGUSR.IdPro_ComVen, T_Mdl_Funciones_Varias.cod_producto, numMinRequerido: int.Parse(Nro_Disp));
                        if (x != 0)
                            //todo OK
                            initObj.Module1.Codop_FT = initObj.Module1.Codop.Clone();
                        else
                            return false;
                    }

                    ofi = VB6Helpers.Trim(MODGUSR.UsrEsp.OfixUser);
                    if (string.IsNullOrEmpty(ofi))
                    {
                        Module1.Codop.Id_Empresa = "000";
                    }
                    else
                    {
                        if (VB6Helpers.Instr(ofi, ";") == 0 && VB6Helpers.Len(ofi) == 3)
                        {
                            Module1.Codop.Id_Empresa = ofi;
                        }

                        if (string.IsNullOrWhiteSpace(Module1.Codop.Id_Empresa))
                        {
                            Module1.Codop.Id_Empresa = "000";
                        }
                    }

                    MODGCVD.VgCVDNul.codcct = Module1.Codop.Cent_Costo;
                    MODGCVD.VgCVDNul.codpro = Module1.Codop.Id_Product;
                    MODGCVD.VgCVDNul.codesp = Module1.Codop.Id_Especia;
                    MODGCVD.VgCVDNul.codofi = Module1.Codop.Id_Empresa;
                    MODGCVD.VgCVDNul.codope = Module1.Codop.Id_Operacion;
                    MODGCVD.VgCVDNul.OpeSin = MODGCVD.VgCVDNul.codcct + MODGCVD.VgCVDNul.codpro + MODGCVD.VgCVDNul.codesp + MODGCVD.VgCVDNul.codofi + MODGCVD.VgCVDNul.codope;
                    MODGCVD.VgCVDNul.OpeCon = MODGCVD.VgCVDNul.codcct + "-" + MODGCVD.VgCVDNul.codpro + "-" + MODGCVD.VgCVDNul.codesp + "-" + MODGCVD.VgCVDNul.codofi + "-" + MODGCVD.VgCVDNul.codope;
                    //this.Caption = "Compra Venta - Fund Transfer" + MODGCVD.VgCVDNul.OpeCon;
                    initObj.Frm_Principal.Caption = "Compra Venta - Fund Transfer " + MODGCVD.VgCVDNul.OpeCon;
                    MODGCVD.VgCVDNul.estado = T_MODGCVD.ECvd_Ing;
                }
                else
                {
                    //this.Caption = "Compra Venta de Divisas Fund Transfer  ";
                    initObj.Frm_Principal.Caption = "Compra Venta de Divisas Fund Transfer  ";
                }

                MODGCVD.VgCvd = MODGCVD.VgCVDNul;

                initObj.MODXANU.VgAnu.AnuSin = (short)(false ? -1 : 0);
                MODGCVD.TIN = false;
                MODGCVD.NOTACRED = false;
                MODGCVD.VgCvd.InsExp = MODGFRA.Put_InsEsp(initObj.MODGFRA, "Carta Exportador", -1, "E");
                return true;
            }
        }

        public static dynamic Documentos(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            T_MODXVIA MODXVIA = initObj.MODXVIA;
            T_MODGCHQ MODGCHQ = initObj.MODGCHQ;
            T_MODGCVD MODGCVD = initObj.MODGCVD;
            short x = 0;
            //Documentos : Cheque - ValeVista.
            if (MODXVIA.VgxVia.Acepto != 0)
            {
                //Cheques y Vales Vistas.-
                if (MODCHQI.InterfazChq(initObj, unit) >= 0)
                {
                    //FrmgChq.DefInstance.Show(1);
                    //initObj.FormularioQueAbrir = "EmitirCheque";
                    Frm_Chq_Logic.Form_Load(initObj, unit);
                }
            }
            return null;
        }

        public static dynamic Vueltos(InitializationObject initObject, UnitOfWorkCext01 unit, bool vuelve)
        {
            T_MODXVIA MODXVIA = initObject.MODXVIA;
            T_MODXORI MODXORI = initObject.MODXORI;
            T_MODGCVD MODGCVD = initObject.MODGCVD;

            if (!vuelve)
            {
                short i = 0;
                double dif = 0;
                short x = 0;
                //Se eliminan los Vueltos anteriores.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODXVIA.VxMtoVia); i++)
                {
                    if (MODXVIA.VxMtoVia[i].Vuelto != 0)
                    {
                        MODXVIA.VxMtoVia[i].MtoTot = 0;
                    }

                }

                //Calcula los Vueltos.-
                for (i = 0; i <= (short)VB6Helpers.UBound(MODXORI.VxMtoOri); i++)
                {
                    dif = Format.StringToDouble(Utils.Format.FormatCurrency((BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Fn_SumaVxOri(initObject, MODXORI.VxMtoOri[i].CodMon) - MODXORI.VxMtoOri[i].MtoTot), "0.00"));
                    if (dif > 0)
                    {
                        x = VB6Helpers.CShort(BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.Put_xVia(initObject, unit, MODXORI.VxMtoOri[i].CodMon, dif, -1));
                    }

                }

                //Participantes.-
                //VgxVia.Partys = Str$(IExp1)
                MODXVIA.VgxVia.Vuelto = (short)(true ? -1 : 0);
                //VgxVia.Destino = TDme_PagoExp
                initObject.FormularioQueAbrir = "DestinoFondos";
                initObject.VieneDe = "Vueltos";
                return null;
            }
            else
            {
                initObject.Frm_Destino_Fondos = null;
                short n = 0;
                MODXVIA.VgxVia.Vuelto = (short)(false ? -1 : 0);
                if (~MODXVIA.VgxVia.Acepto != 0)
                {
                    return null;
                }
                MODGCVD.VgCvd.Etapa = MODGPYF0.Componer(MODGCVD.VgCvd.Etapa, "VUE", "");

                //Cheques.-
                n = BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.TotalChq(MODXVIA);
                if (n > 0)
                {
                    initObject.Mdi_Principal.BUTTONS["tbr_Gchq"].Enabled = true;
                    MODGCVD.VgCvd.Etapa += "DVS";
                }

                //Swift's.-
                if (BCH.Comex.Core.BL.XCFT.Modulos.MODXVIA.GetSwifts(MODXVIA).Any())
                {
                    initObject.Mdi_Principal.BUTTONS["tbr_Swift"].Enabled = true;
                    MODGCVD.VgCvd.Etapa += "SWF";
                }

                // set foco
                var foco =
                    initObject.Mdi_Principal.BUTTONS["tbr_Swift"].Enabled ? "tbr_Swift" : // Foco en Swift
                    initObject.Mdi_Principal.BUTTONS["tbr_Gchq"].Enabled ? "tbr_Gchq" : // Focos en Cheque                    
                    "tbr_grabar";   // Foco en grabar

                initObject.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { initObject.Mdi_Principal.BUTTONS[key].Focus = (key == foco); });
                initObject.FormularioQueAbrir = "Index";
                return null;
            }
        }

        public static void MDIForm_Load(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            //-------Fin deshabilitar los botones maximizar y minimizar en un formulario MDI Parent -------
            string ic;
            string ir = "";
            string ip = "";
            //Inicializa Objetos y Variables.
            NuevaCVD_L(initObj, uow);
            ic = initObj.Usuario.ConfigImpres_ImprimeCartas;
            if (string.IsNullOrEmpty(ic))
            {
                ic = "-1";
            }

            ir = initObj.Usuario.ConfigImpres_ImprimeReporte;
            if (string.IsNullOrEmpty(ir))
            {
                ir = "-1";
            }

            ip = initObj.Usuario.ConfigImpres_ImprimePlanillas;
            if (string.IsNullOrEmpty(ip))
            {
                ip = "-1";
            }

            initObj.Mdi_Principal.mnu_cartas.Checked = VB6Helpers.CBool(ic);
            initObj.Mdi_Principal.mnu_conta.Checked = VB6Helpers.CBool(ir);
            initObj.Mdi_Principal.mnu_planillas.Checked = VB6Helpers.CBool(ip);

            if (initObj.MODGUSR.UsrEsp.Jerarquia == 1)
            {
                initObj.Mdi_Principal.BUTTONS["tbr_cargo_abono"].Enabled = true;
                initObj.Mdi_Principal.Opciones.Find(x => x.ID == "11").Enabled = true;
            }
            else
            {
                initObj.Mdi_Principal.BUTTONS["tbr_cargo_abono"].Enabled = false;
                initObj.Mdi_Principal.Opciones.Find(x => x.ID == "11").Enabled = false;
            }

            //ESPECIALISTA
            //sbr_Menu.Panels[2].Text = "Especialista: " + initObj.MODGUSR.UsrEsp.Nombre;
            //Frm_Principal.DefInstance.Show();

            //Rango permitido para el ingreso de operaciones
            initObj.MODGRNG.Rango_Permitido = true;
        }

        public static bool Opciones_Click_AnulacionOperacion_1_2(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            initObj.MODGCVD.VgCvd.TipCVD = T_MODGCVD.TCvd_Anu;
            if (NuevaCVD_I(initObj, uow, "AnulacionOperaciones"))
            { //luego de volver de seleccion oficina
                if (initObj.Frmganu == null)
                {
                    // Se genera Transaction ID
                    //----------------------------------------
                    if (string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.LC_TRXID_MAN))
                    {
                        initObj.Mdl_Funciones_Varias.LC_TRXID_MAN = MODGCVD.GeneraTRXID(initObj.MODGCVD.VgCvd.OpeSin, uow, initObj.Mdi_Principal.MESSAGES);
                    }

                    if ((MODGRNG.SgteNumOpr(initObj.MODGRNG, initObj.Module1, initObj.MODGUSR, initObj.Mdi_Principal, uow,
                        T_MODGUSR.IdPro_ComVen, T_Mdl_Funciones_Varias.cod_producto) &
                        (initObj.MODXANU.Habilita ? -1 : 0)) != 0)
                    {
                        //frmganu.DefInstance.Show(1);
                        return true; // continuo ejecutando
                    }
                }
            }

            return false;
        }

        public static void Opciones_Click_AnulacionOperacion_2_2(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            initObj.Frmganu = null;

            initObj.Module1.Codop.Id_Operacion = "";
            NuevaCVD_L(initObj, uow);

            if (initObj.DocumentosAImprimir.Count > 0)
            {
                initObj.VieneDe = String.IsNullOrEmpty(initObj.FormularioQueAbrir) ? "Index" : initObj.FormularioQueAbrir;
                initObj.FormularioQueAbrir = "ImprimirDocumentos";
            }
            else
            {
                initObj.FormularioQueAbrir = "Index";
            }
        }

        public static short Genera_Swift_Auto(InitializationObject initObject, UnitOfWorkCext01 unit)
        {
            using (var trace = new Tracer("Genera_Swift_Auto"))
            {
                T_Mdl_Funciones_Varias Mdl_Funciones_Varias = initObject.Mdl_Funciones_Varias;
                T_MODGSWF MODGSWF = initObject.MODGSWF;
                T_Module1 Module1 = initObject.Module1;


                short _retValue = 0;
                // UPGRADE_INFO (#0501): The 'i' member isn't used anywhere in current application.
                short i = 0;
                string s = "";
                short k = 0;
                short n = -1;
                short m = 0;
                _retValue = (short)(false ? -1 : 0);

                if (Mdl_Funciones_Varias.LC_PRD == "72" || Mdl_Funciones_Varias.LC_PRD == "74")
                {
                    //Se cargan los Documentos según Vías.-
                    //Total Swift's.-
                    n = (short)(n + 1);
                    k = (short)VB6Helpers.UBound(MODGSWF.VSwf);
                    // UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
                    // IGNORED: On Error GoTo 0

                    if (k == -1)
                    {
                        MODGSWF.VSwf = new T_Swf[n + 1];
                        MODGSWF.VSwf[n] = new T_Swf();
                        MODGSWF.VMT103 = new T_mt103[n + 1];
                        MODGSWF.VMT103[n] = new T_mt103();
                    }

                    MODGSWF.VBenSwf = new T_BenSwf[0];
                    m = (short)(VB6Helpers.UBound(MODGSWF.VBenSwf) + 1);
                    VB6Helpers.RedimPreserve(ref MODGSWF.VBenSwf, 0, m);
                    //*********************************************
                    //CAMPO MESSAGE TYPE
                    if (Mdl_Funciones_Varias.LC_PRD == "72")
                    {
                        MODGSWF.VSwf[n].NroSwf = "MT-103";
                    }
                    if (Mdl_Funciones_Varias.LC_PRD == "74")
                    {
                        MODGSWF.VSwf[n].NroSwf = "MT-202";
                    }
                    if (MODGSWF.VSwf[n].NroSwf == "MT-103")
                    {
                        MODGSWF.VSwf[n].BenSwf.EsBanco = false;
                    }
                    if (MODGSWF.VSwf[n].NroSwf == "MT-202")
                    {
                        MODGSWF.VSwf[n].BenSwf.EsBanco = true;
                    }
                    if (MODGSWF.VCliSwf == null)
                    {
                        MODGSWF.VCliSwf = new T_CliSwf();
                    }
                    MODGSWF.VCliSwf.NomCli = Module1.PartysOpe[Mdl_Funciones_Varias.IExp].NombreUsado;
                    MODGSWF.VCliSwf.DirCli1 = Module1.PartysOpe[Mdl_Funciones_Varias.IExp].DireccionUsado;
                    MODGSWF.VCliSwf.DirCli2 = Module1.PartysOpe[Mdl_Funciones_Varias.IExp].CiudadUsado;
                    MODGSWF.VCliSwf.PaiCli = Module1.PartysOpe[Mdl_Funciones_Varias.IExp].PaisUsado;
                    MODGSWF.VCliSwf.rutcli = Module1.PartysOpe[Mdl_Funciones_Varias.IExp].rut;
                    if (!(initObject.MODXORI == null))
                    {
                        var ope = initObject.MODXORI.gs_ctacte_party;
                        if (ope != null)
                        {
                            MODGSWF.VCliSwf.CtaCli = ope;
                        }
                        else
                        {
                            MODGSWF.VCliSwf.CtaCli = "";
                        }
                    }
                    else
                    {
                        MODGSWF.VCliSwf.CtaCli = MODXORI.Get_CtaCte(unit, MODGSWF.VCliSwf.rutcli.TrimStart('0').PadRight(12, '|'));
                    }
                    MODGSWF.VCliSwf.CtaCli = MODGSWF.VCliSwf.CtaCli.TrimStart('0');
                    //************************************
                    //Se Intenta Generar el Swift.-
                    //ASUMO QUE EL INDICE DEBE SER CERO
                    s = BCH.Comex.Core.BL.XCFT.Modulos.MODGSWF.GeneraDocSwf(initObject.MODGSWF, initObject.MODGUSR, initObject.MODGTAB0,
                        initObject.Mdl_Funciones, initObject.Mdl_Funciones_Varias, initObject.MOD_50F, initObject.Mdi_Principal.MESSAGES, initObject.MODGCVD, initObject.Mdl_Funciones_Varias.CARGA_AUTOMATICA, unit, null, 0);

                    if (String.IsNullOrEmpty(s))
                    {
                        trace.TraceError("No se ha podido generar el Swift correctamente. Reporte este problema");
                        initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "No se ha podido generar el Swift correctamente. Reporte este problema."
                        });
                        return (short)(false ? -1 : 0);
                    }
                    else
                    {
                        MODGSWF.VSwf[n].DocSwf = s;
                        return (short)(true ? -1 : 0);
                    }
                }
                else
                {
                    return (short)(true ? -1 : 0);
                }
            }
        }

        public static void Tx_RefCli_LostFocus(InitializationObject initObject)
        {
            initObject.MODGCVD.VgCvd.RefCli = initObject.Frm_Principal.Tx_RefCli.Text;
        }

        public static bool Opciones_Click_AnulacionPlanillaVisibleImport_1_2(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            initObj.MODXORI.Vx_SCodTran = new S_Codtran[0];
            initObj.MODGCVD.VgCvd.TipCVD = T_MODGCVD.TCvd_AnuVisI;

            if (NuevaCVD_I(initObj, uow, "AnulacionPlanillaVisibleImport"))
            {
                // Descripción : Se genera Transaction ID 
                if (string.IsNullOrEmpty(initObj.Mdl_Funciones_Varias.LC_TRXID_MAN))
                {
                    initObj.Mdl_Funciones_Varias.LC_TRXID_MAN = MODGCVD.GeneraTRXID(initObj.MODGCVD.VgCvd.OpeSin, uow, initObj.Mdi_Principal.MESSAGES);
                }

                initObj.MODXANU.VgAnu.codcct = initObj.MODGCVD.VgCvd.codcct;
                initObj.MODXANU.VgAnu.codpro = initObj.MODGCVD.VgCvd.codpro;
                initObj.MODXANU.VgAnu.codesp = initObj.MODGCVD.VgCvd.codesp;
                initObj.MODXANU.VgAnu.codofi = initObj.MODGCVD.VgCvd.codofi;
                initObj.MODXANU.VgAnu.codope = initObj.MODGCVD.VgCvd.codope;
                return true;
            }

            return false;
        }

        public static void Opciones_Click_AnulacionPlanillaVisibleImport_2_2(InitializationObject initObj, UnitOfWorkCext01 uow)
        {
            if (initObj.MODANUVI.Vx_AnuReem.AcepAnu != 0)
            {
                // Carga automática del Participante.-
                Module1.ResetParty(initObj.Module1, initObj.MODGCVD.Beneficiario);
                //InitObj.Module1.ResetParty(InitObj.MODGCVD.Beneficiario);
                initObj.Module1.PartysOpe[0].LlaveArchivo = initObj.MODANUVI.Vx_AnuReem.PrtCli;
                initObj.Module1.PartysOpe[0].IndNombre = initObj.MODANUVI.Vx_AnuReem.IndNom;
                initObj.Module1.PartysOpe[0].IndDireccion = initObj.MODANUVI.Vx_AnuReem.IndDir;
                initObj.Module1.PartysOpe[0].Status = T_Module1.GPrt_StatDatos;
                initObj.Module1.PartysOpe[0].Ubicacion = T_Module1.GPrt_EnParty;

                MODSYGETPRT.SyGet_Prt(ref initObj.Module1.Codop, 1, initObj, uow);

                initObj.Frm_Principal.Tx_NomPrt.Text = initObj.Module1.PartysOpe[T_MODGCVD.ICli].NombreUsado;
                initObj.MODGCVD.VgCvd.IndPrt = T_MODGCVD.ICli;
                initObj.MODGCVD.VgCvd.PrtCli = initObj.Module1.PartysOpe[T_MODGCVD.ICli].LlaveArchivo;
                initObj.MODGCVD.VgCvd.rutcli = initObj.Module1.PartysOpe[T_MODGCVD.ICli].rut;

                //if (InitObj.MODANUVI.Vx_AnuReem.HayRee == 1)
                //{
                //    FrmRemPVI.DefInstance.ShowDialog();
                //}

                // Habilitar Vías sólo si es necesario.-
                if (MODPREEM.Fn_HayViasAnu(initObj))
                {
                    initObj.MODGCVD.VgCvd.Etapa = "VIA";
                    initObj.Mdi_Principal.BUTTONS["tbr_dedfondos"].Enabled = true;
                }
                // Origenes de Fondos para las Planillas.-
                if (MODPREEM.Fn_TotOriAnu(initObj) > 0)
                {
                    initObj.Mdi_Principal.BUTTONS["tbr_origfondos"].Enabled = true;
                    initObj.MODGCVD.VgCvd.Etapa = initObj.MODGCVD.VgCvd.Etapa + "ORI";
                }

                if (initObj.Frm_Anu_Vi.Ch_Reemp.Checked)
                {
                    initObj.MODANUVI.Var_CodMon = initObj.MODANUVI.V_PlAnu[0].CodMon;
                    initObj.MODANUVI.Var_TipCam = Format.StringToDouble((initObj.Frm_Anu_Vi.Tx_TipCam.Text));  //PACP
                    initObj.MODANUVI.Vx_AnuReem.HayRee = (short)(true ? -1 : 0);

                    initObj.Frm_Rem_PVI = new UI_Frm_Rem_PVI();
                    Frm_Rem_PVI.Form_Load(initObj, uow);
                    Frm_Rem_PVI.Pr_CargaPln(initObj, uow);
                    //Frm_Rem_PVI.Bot_Acepta_Click(initObj,);
                    Frm_Rem_PVI.Bot_OkFinal_Click(initObj, uow);
                }
                //short largo_reem = (short)initObj.MODPREEM.Vx_PReem.Length;
                //initObj.MODPREEM.Vx_PReem = new Plan_Reemp[1];
                //initObj.MODPREEM.Vx_PReem[largo_reem] = new Plan_Reemp();
                //MODPREEM.Pr_DatosPl_R(initObj, initObj.MODANUVI.V_PlAnu[largo_reem].NumPre, initObj.MODANUVI.V_PlAnu[largo_reem].FecVen, largo_reem);
                //MODPREEM.Pr_RefundeGrDO(initObj.MODCVDIMMM);
            }
        }
    }
}
