using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XCFT.Forms
{
    public class frmganu
    {
        public static short RowCount;


        private static short Lee_RepCon(string Numero, UnitOfWorkCext01 unit, InitializationObject initObj)
        {
            short _retValue = 0;
            string R = "";
            int NumPln = 0;
            string FecPln = "";

            //try
            //{
            var RESULTADO = unit.SceRepository.sce_mch_s11_MS(MODGSYB.dbcharSy(VB6Helpers.Mid(Numero, 1, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(Numero, 4, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(Numero, 6, 2)), MODGSYB.dbcharSy(VB6Helpers.Mid(Numero, 8, 3)), MODGSYB.dbcharSy(VB6Helpers.Mid(Numero, 11, 5)));
            if (RESULTADO != null)
            {
                NumPln = (int)RESULTADO.nrorpt;
                FecPln = RESULTADO.fecmov.ToString("yyyy-MM-dd");
                MODGCON1.Pr_Imprime_Contab80(initObj, NumPln, FecPln, Numero);
                _retValue = -1;
            }

            return _retValue;

            //}
            //catch (Exception _ex)
            //{
            //    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message()
            //    {
            //        Type = TipoMensaje.Error,
            //        Text = "[" + VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Err.Number)) + "] " + VB6Helpers.ErrorToString(VB6Helpers.Err.Number),
            //        Title = T_MODGCVD.MsgCVD
            //    });
            //    _retValue = 0;
            //}
            //return _retValue;
        }

        /// <summary>
        /// Obtiene información de la contabilidad
        /// </summary>
        /// <param name="NumeroOperacion">Número completo de la operación</param>
        /// <param name="unit">Conexión a la base de datos</param>
        /// <param name="initObj">Varible trasversal al sistema</param>
        /// <param name="TipoCVD">Retorna el tipo de CVD correspondiente de la Operación</param>
        /// <returns></returns>
        private static bool Leer_DatosContabilidad_esAUTOMATICA(string NumeroOperacion, UnitOfWorkCext01 unit, InitializationObject initObj, ref string TipoCVD, ref bool Inyectado)
        {
            using (Tracer trace = new Tracer("Leer_DatosContabilidad"))
            {
                var devuelvo = false;
                var datos =
                unit.SceRepository.sce_mch_s11_MS(
                    NumeroOperacion.Substring(0, 3),
                    NumeroOperacion.Substring(3, 2),
                    NumeroOperacion.Substring(5, 2),
                    NumeroOperacion.Substring(7, 3),
                    NumeroOperacion.Substring(10, 5));
                if (datos != null)
                {
                    var Completar = unit.DocumentosOperacionesRepository.sce_mch_s01_MS(int.Parse(datos.nrorpt.ToString()), datos.fecmov).FirstOrDefault();
                    if (Completar != null)
                    {
                        devuelvo = Completar.desgen.Contains("AUTOMATICA");
                        var resultados = unit.SceRepository.sce_mcd_s07_MS(int.Parse(datos.nrorpt.ToString()), datos.fecmov);
                        if (devuelvo && resultados != null)
                        {
                            TipoCVD =
                                // VENTA (OPE|OPEPEND)
                                resultados.Count(c => c.nemcta.StartsWith("OPE")) > 0 ? "TE" :
                                // COMPRA (ACE|COE)
                                resultados.Count(c => c.nemcta.StartsWith("ACE")) > 0 ? "TI" :
                                resultados.Count(c => c.nemcta.StartsWith("COE")) > 0 ? "TI" : "";

                            Inyectado = unit.SceRepository.proc_sel_validaOperacionSiTieneInyeccion_MS(
                                NumeroOperacion.Substring(0, 3),
                                NumeroOperacion.Substring(3, 2),
                                NumeroOperacion.Substring(5, 2),
                                NumeroOperacion.Substring(7, 3),
                                NumeroOperacion.Substring(10, 5));
                        }
                    }
                }
                return devuelvo;
            }
        }

        public static void Cmd_Ok_Click(InitializationObject initObj, UnitOfWorkCext01 unit)
        {
            short x = 0;
            short i = 0;
            string s = "";
            initObj.DocumentosAImprimir = new List<DataImpresion>();

            if (initObj.Frmganu == null)
            {
                initObj.Frmganu = new UI_frmganu();
                initObj.Frmganu.Tx_NroOpe[0].Text = "000";
                initObj.Frmganu.Tx_NroOpe[0].Text = "00";
                initObj.Frmganu.Tx_NroOpe[0].Text = "00";
                initObj.Frmganu.Tx_NroOpe[0].Text = "000";
                initObj.Frmganu.Tx_NroOpe[0].Text = "00000";
            }

            /// Limpia la Pantalla.
            initObj.Frmganu.Lt_Pln.Text = "";
            initObj.Frmganu.Tx_Prty.Text = "";

            /// Rescata el Partys de la Operación.
            Tx_NroOpe_LostFocus(initObj, 0);
            initObj.MODGANU.VAnu.CodOpe_t = initObj.Frmganu.Tx_NroOpe[0].Text + initObj.Frmganu.Tx_NroOpe[1].Text + initObj.Frmganu.Tx_NroOpe[2].Text + initObj.Frmganu.Tx_NroOpe[3].Text + initObj.Frmganu.Tx_NroOpe[4].Text;

            /// Busca la Operación en las bases.
            x = MODGANU.SyGet_CVD(initObj.MODGANU.VAnu.CodOpe_t, initObj, unit);

            /// Limpiamos la variable de los mensajes en pantalla
            initObj.Mdi_Principal.MESSAGES.Clear();

            /// Validamos si la  operación existe en la base.
            if (x == 0)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "La operación de Compra-Venta no existe.",
                    Type = TipoMensaje.Informacion
                });
                initObj.Frmganu.Co_Boton_000.Enabled = false;
                return;
            }

            /// Validamos si la  operación existe en la base.
            if (string.IsNullOrEmpty(initObj.MODGCVD.VgCVDo.codope))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "La operación de Compra-Venta no existe.",
                    Type = TipoMensaje.Error
                });
                initObj.Frmganu.Co_Boton_000.Enabled = false;
                return;
            }

            /// Valida si la operación tiene inyeccion en "tbl_sce_cvd_ft"
            if (Convert.ToBoolean(Mdl_Funciones_Varias.Valida_Anulacion(initObj.MODGANU.VAnu.CodOpe_t, initObj, unit)))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "No se puede Anular una operación, que tiene realizado el Abono/Cargo.",
                    Type = TipoMensaje.Error
                });
                initObj.Frmganu.Co_Boton_000.Enabled = false;
            }           

            /// Validación de la fecha actual para el número de operación ingresado.
            initObj.MODCVDIMMM.FechaHoy = DateTime.Now.ToString("dd/MM/yyyy");
            if (VB6Helpers.Format(initObj.MODCVDIMMM.FechaHoy, "yyyyMMdd") != VB6Helpers.Format(initObj.MODGCVD.VgCVDo.Fecing, "yyyyMMdd"))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "No podrá anular esta Operación debido a que NO fue creada con fecha de Hoy.",
                    Type = TipoMensaje.Error
                });
                initObj.Frmganu.Co_Boton_000.Enabled = false;
            }

            /// Validamos si la operación ingresada, no este ya anulada.
            if (initObj.MODGCVD.VgCVDo.estado == initObj.MODGCVD.ECvd_Anu)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "No podrá anular esta Operación debido a que ya fue anulada.",
                    Type = TipoMensaje.Error
                });
                initObj.Frmganu.Co_Boton_000.Enabled = false;
            }

            /// Busca el tipo CVD de la Operación 
            string TipoCVD = VB6Helpers.CStr(Mdl_Funciones_Varias.Busca_TipCVD(initObj.MODGANU.VAnu.CodOpe_t, unit, initObj));

            /// Validamos si no tiene TipoCVD, que es una operación AOPR o APOE
            /// Cargamos en la variable para saber si, esta fue inyectado
            bool estaInyectado = false;
            bool esAuto = Leer_DatosContabilidad_esAUTOMATICA(initObj.MODGANU.VAnu.CodOpe_t, unit, initObj, ref TipoCVD, ref estaInyectado);

            /// Si la Operación tiene alguna inyección en la tabla sce_mcd, no deja continuar, en especial si
            /// La operación es AOPR | APOE
            if (estaInyectado)
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "No se puede Anular una operación " + (esAuto == true ? "Automatica" : "") + ", debido a que tiene Abono/Cargo.",
                    Type = TipoMensaje.Error
                });
                initObj.Frmganu.Co_Boton_000.Enabled = false;
            }

            /// Si la Operación ingresada no cuenta una definición de tipo CVD, este no continua.
            if (string.IsNullOrEmpty(TipoCVD))
            {
                initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Text = "No podrá anular esta Operación debido a que no existe registro de Tipo CVD (en tbl_sce_cvd_ft).",
                    Type = TipoMensaje.Error
                });
                initObj.Frmganu.Co_Boton_000.Enabled = false;
            }

            /// Nombre del Cliente
            initObj.Frmganu.Tx_Prty.Text = VB6Helpers.Trim(Mdl_Funciones_Varias.GetDatPrt(initObj, unit, initObj.MODGCVD.VgCVDo.PrtCli, initObj.MODGCVD.VgCVDo.IndNomC, initObj.MODGCVD.VgCVDo.IndDirC, "N"));

            /// Datos de la Planilla
            if ((TipoCVD.Trim() != ("TIN") && TipoCVD.Trim() != ("AV")))
            {
                x = MODGANU.SyGetPlanCVD_Anu(initObj.MODGANU.VAnu.CodOpe_t, unit, initObj);
                if (~x != 0)
                {
                    initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                    {
                        Text = "No se han encontrado planillas para la operación de Compra-Venta.",
                        Type = TipoMensaje.Warning
                    });
                }

                initObj.Frmganu.Lt_Pln.Text = "";
                for (i = 0; i < initObj.MODGANU.VAnuPl.Count(); i++)
                {
                    s = "";
                    s = s + initObj.MODGANU.VAnuPl[i].NumPln + VB6Helpers.Chr(9);
                    s = s + VB6Helpers.Format(initObj.MODGANU.VAnuPl[i].FecPln, "dd/MM/yyyy") + VB6Helpers.Chr(9);
                    s = s + initObj.MODGANU.VAnuPl[i].VisInv + VB6Helpers.Chr(9);
                    s = s + initObj.MODGANU.VAnuPl[i].NemMnd + VB6Helpers.Chr(9);
                    s += initObj.MODGANU.VAnuPl[i].MtoPln_t;
                    initObj.Frmganu.Lt_Pln.Text = s;
                }
            }
            else
            {
                string monto = MODGANU.SyGet_montoOperacion(initObj.MODGANU.VAnu.CodOpe_t, unit);
                if (TipoCVD.Trim() == "TIN")
                {
                    s = "";
                    s += "Transferencia Interna " + monto;
                    initObj.Frmganu.Lt_Pln.Text = s;
                }
                else
                {
                    s = "";
                    s += "Comisiones " + monto;
                    initObj.Frmganu.Lt_Pln.Text = s;
                }
            }
            return;
        }

        public static void Co_Boton_Click(InitializationObject initObj, UnitOfWorkCext01 unit, UnitOfWorkSwift uow)
        {
            short p = 0;
            short n = 0;
            short x = 0;
            short i = 0;
            short Index = 0;

            switch (Index)
            {
                case 0:
                    //if (initObj.Frmganu.Lt_Pln.Items.Count > 0)
                    if (initObj.Frmganu.Lt_Pln.Text.Length > 0)
                    {
                        initObj.Module1.Codop.Cent_Costo = initObj.MODGANU.VAnu.CodOpe_t.Substring(0, 3);
                        initObj.Module1.Codop.Id_Product = initObj.MODGANU.VAnu.CodOpe_t.Substring(3, 2);
                        initObj.Module1.Codop.Id_Especia = initObj.MODGANU.VAnu.CodOpe_t.Substring(5, 2);
                        initObj.Module1.Codop.Id_Empresa = initObj.MODGANU.VAnu.CodOpe_t.Substring(7, 3);
                        initObj.Module1.Codop.Id_Operacion = initObj.MODGANU.VAnu.CodOpe_t.Substring(10, 5);

                        p = MODSWENN.Fn_GetMts(initObj, unit, 1, "R", "0", "0", "0", "0", n.ToString());
                        // Fn_GetMts dudu
                        if (!(initObj.MODGCVD.VgCVDo.TipCVD == T_MODGCVD.TCvd_VisImp) && !(initObj.MODGCVD.VgCVDo.TipCVD == T_MODGCVD.TCvd_PlanSO) && !(initObj.MODGCVD.VgCVDo.TipCVD == T_MODGCVD.TCvd_AnuVisI))
                        {
                            x = MODGANU.SyPutr_Cvd(initObj.MODGANU.VAnu.CodOpe_t, unit, initObj);
                        }
                        else
                        {
                            x = MODVPLE.SyAnu_ImpCvd(initObj.MODGANU.VAnu.CodOpe_t, initObj, unit);
                        }

                        if (~x != 0)
                        {
                            return;
                        }

                        if (x != 0)
                        {
                            p = MODGEX40.Fn_GetMtsCV(initObj.MODGANU.VAnu.CodOpe_t, 9, "R", "0", "0", "0", "0", "0");
                            //Fn_GetMtsCV dudu
                            initObj.MODSWENN.RutAis = initObj.Usuario.Identificacion_Rut; //Rut del especialista
                            if (initObj.MODSWENN.VMts != null)
                            {
                                if (initObj.MODSWENN.VMts.Length > 0)
                                {
                                    for (i = 0; i < initObj.MODSWENN.VMts.Length; i++)
                                    {
                                        //Fn_AnulaSwift dudu
                                        x = MODSWENN.Fn_AnulaSwift(uow, initObj, VB6Helpers.Mid(initObj.MODGANU.VAnu.CodOpe_t, 1, 3), initObj.MODSWENN.VMts[i].id_mensaje, "Anulación de Compra-Venta");
                                        x = MODSWENN.Fn_BorraSwiCo(unit, initObj, initObj.MODGANU.VAnu.CodOpe_t, 0, 0, 0, 0, 0, initObj.MODSWENN.VMts[i].id_mensaje);
                                        //Fn_BorraSwiCo dudu
                                    }
                                }
                            }
                        }
                        initObj.Mdi_Principal.MESSAGES.Add(new UI_Message
                        {
                            Text = "Se ha anulado exitosamente la Operación de Compra-Venta.",
                            Type = TipoMensaje.Informacion,
                            AutoClose = true
                        });
                        x = Lee_RepCon(initObj.MODGANU.VAnu.CodOpe_t, unit, initObj);
                        return;
                    }

                    initObj.Frmganu.Tx_NroOpe[4].Text = "";
                    //initObj.Frmganu.Lt_Pln.Items.Clear();
                    initObj.Frmganu.Lt_Pln.Text = "";

                    break;
                case 1:
                    break;
            }
        }

        public static void Form_Load(InitializationObject initObj)
        {
            initObj.Frmganu.Tx_NroOpe[0].Text = initObj.MODGUSR.UsrEsp.CentroCosto;
            initObj.Frmganu.Tx_NroOpe[1].Text = T_MODGUSR.IdPro_ComVen;
            initObj.Frmganu.Tx_NroOpe[2].Text = initObj.MODGUSR.UsrEsp.Especialista;
            initObj.Frmganu.Tx_NroOpe[3].Text = initObj.Module1.Codop.Id_Empresa;
            initObj.Frmganu.Tx_NroOpe[4].Text = "";
        }

        private void Tx_NroOpe_GotFocus(ref short Index, InitializationObject initObj)
        {
            MODGPYF1.selTexto(initObj.Frmganu.Tx_NroOpe[Index].Text);
        }

        public static void Tx_NroOpe_LostFocus(InitializationObject initObj, short Index)
        {
            T_MODGPYF0 MODGPYF0 = initObj.MODGPYF0;
            int n = 0;
            if (string.IsNullOrEmpty(initObj.Frmganu.Tx_NroOpe[Index].Text))
            {
                n = 0;
            }
            else
            {
                n = (int)Format.StringToDouble(initObj.Frmganu.Tx_NroOpe[Index].Text);
            }

            switch (Index)
            {
                case 0:
                    initObj.Frmganu.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "000");
                    break;
                case 1:
                    initObj.Frmganu.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;
                case 2:
                    initObj.Frmganu.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00");
                    break;
                case 3:
                    initObj.Frmganu.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "000");
                    break;
                case 4:
                    initObj.Frmganu.Tx_NroOpe[Index].Text = VB6Helpers.Format(VB6Helpers.CStr(n), "00000");
                    break;
            }
        }
    }
}

