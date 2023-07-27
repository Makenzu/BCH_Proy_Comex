using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XCFT.Forms;
using BCH.Comex.Core.BL.XCFT.Modulos;
using BCH.Comex.Core.BL.XCFT.Utility;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.UI_Modulos;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Portal;
using BCH.Comex.Data.DAL.Swift;
using CodeArchitects.VB6Library;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;

namespace BCH.Comex.Core.BL.XCFT
{
    public class FundTransferService : IDisposable
    {
        private UnitOfWorkCext01 uow;
        private UnitOfWorkPortal uowPortal;
        private UnitOfWorkSwift uowSwift;

        public FundTransferService()
        {
            uow = new UnitOfWorkCext01();
            uowPortal = new UnitOfWorkPortal();
            uowSwift = new UnitOfWorkSwift();
        }

        public void Dispose()
        {
            if (uow != null)
            {
                uow.Dispose();
            }
            if (uowPortal != null)
            {
                uowPortal.Dispose();
            }
            if (uowSwift != null)
            {
                uowSwift.Dispose();
            }
        }


        #region Index

        /// <summary>
        /// Inicia la aplicacion
        /// </summary>
        /// <returns></returns>
        public InitializationObject FundTransferInit(IDatosUsuario usuario)
        {
            using (Tracer tracer = new Tracer("Inicializacion de Initobj lo load de MdiForm"))
            {
                InitializationObject initObj = SetupInitObj(usuario);
                MdiForm_Load(initObj);

                return initObj;
            }
        }

        /// <summary>
        /// Iniclializa la aplicacion
        /// </summary>
        /// <returns></returns>
        private InitializationObject SetupInitObj(IDatosUsuario usuario)
        {
            using (Tracer tracer = new Tracer("SetupInitObj"))
            {
                InitializationObject initObject;
                try
                {
                    initObject = Mdl_Acceso.Inicializar(uow, usuario);
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "PoliticaBLFundTransfer")) throw;
                    //tracer.TraceError("Error al iniciar la aplicación FundTransfer: {0}",  ex);

                    initObject = new InitializationObject();
                    //initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                    //{
                    //    Title = "FundTransfer",
                    //    Text = "Error al inciar la aplicación: " + ex.Message,
                    //    Type = TipoMensaje.Critical
                    //});
                }

                return initObject;
            }
        }

        private void MdiForm_Load(InitializationObject InitObject)
        {
            mdi_PrincipalLogic.MDIForm_Load(InitObject, uow);
        }

        public void Index_ConfigImprimirClick(InitializationObject initObj)
        {
            uow.ActualizarImpresion(initObj.Usuario);
        }

        public void Index_Tx_RefCli_Blur(InitializationObject initObj)
        {
            mdi_PrincipalLogic.Tx_RefCli_LostFocus(initObj);
        }
        #endregion Index

        #region NuevaOperacion

        /// <summary>
        /// Crea una nueva operacion
        /// </summary>
        /// <param name="initObject"></param>
        public void DetectaNuevo(InitializationObject initObject)
        {
            mdi_PrincipalLogic.NuevaOperacion(initObject, uow);
        }

        /// <summary>
        /// Al volver desde la ventana de seleccion de oficina
        /// </summary>
        /// <param name="Modulos"></param>
        /// <param name="unit"></param>
        public void DetectaNuevo_DesdeSeleccionOficina(InitializationObject initObj)
        {
            mdi_PrincipalLogic.NuevaOperacion_DesdeSeleccionOficina(initObj, uow);
        }

        #endregion NuevaOperacion

        public void OpcionBotones(InitializationObject initObject, short opcion)
        {
            mdi_PrincipalLogic.Opciones_Botones(initObject, uow, opcion);
        }

        public void Ventas_Vis_Import(InitializationObject initObject)
        {
            mdi_PrincipalLogic.Ventas_Vis_Import(initObject, uow);
        }

        public void Ventas_Vis_Import_PosShow(InitializationObject initObject)
        {

            mdi_PrincipalLogic.Ventas_Vis_Import_PosShow(initObject, uow);
        }

        public void Ventas_Vis_Import_PosCobraComis(InitializationObject initObject)
        {
            mdi_PrincipalLogic.Ventas_Vis_Import_PosCobraComis(initObject, uow);
        }

        #region "Numero de planillas para anular"
        public void NumeroPlanillasAnularInit(InitializationObject initObject)
        {
            //valido inicio y fin de dia
            MODGUSR.SyGetf_Usr(initObject.MODGUSR, initObject.Mdi_Principal, uow,
                VB6Helpers.Left(initObject.Usuario.Identificacion_CCtUsr, 3),
                VB6Helpers.Right(initObject.Usuario.Identificacion_CCtUsr, 2), "I");

            mdi_PrincipalLogic.Opciones_Botones(initObject, uow, 6);
            frmnroa.Form_Load(initObject, uow);
        }

        public string Cb_Moneda_Click(InitializationObject initObject, short monedaItemData)
        {
            return frmnroa.Cb_Moneda_Click(initObject, uow, monedaItemData);
        }

        public void bot_acep_Click(InitializationObject initObject)
        {
            frmnroa.bot_acep_Click(initObject, uow);
        }

        #endregion


        #region "SELECCION OFICINA"

        public void SeleccionBancoInit(InitializationObject initObject)
        {
            initObject.Frm_SeleccionOficina = new UI_Frm_SeleccionOficina();
            frmOfi.Form_Load(initObject, uow);
        }
        public void Seleccion_Banco_AceptarBanco(InitializationObject initObject)
        {
            frmOfi.BAceptar_Click(initObject);
        }

        public void Seleccion_Banco_CancelarBanco(InitializationObject initObject)
        {
            frmOfi.BCancelar_Click(initObject);
        }

        #endregion

        /// <summary>
        /// Se encarga de la obtención de los destinatarios
        /// </summary>
        /// <returns></returns>
        public IList<sce_tdme> GetDestinatarios()
        {
            return uow.SceRepository.sce_tdme_s01_MS();
        }

        /// <summary>
        /// Obtiene las sucursales
        /// </summary>
        /// <returns></returns>
        public IList<sgt_suc> GetSucursales()
        {
            return uow.SgtRepository.sgt_suc_s01_MS();
        }

        #region PARTICIPANTES
        /// <summary>
        /// Procesa los cambios para mostrar participantes
        /// </summary>
        /// <returns></returns>
        public void ParticipantesInit(InitializationObject initObject, bool? ultimaOperacionEsCosmos)
        {
            if (initObject.Frm_Participantes == null)
            {
                initObject.Frm_Participantes = new UI_Frm_Participantes();
            }

            initObject.Frm_Participantes.LstPartys.Clear();
            mdi_PrincipalLogic.Participantes1_2(initObject, uow, ultimaOperacionEsCosmos);
        }

        /// <summary>
        /// Procesa los cambios para mostrar participantes
        /// </summary>
        /// <returns></returns>
        public void Participantes_Aceptar(InitializationObject initObject)
        {
            Frm_Participantes.Aceptar_Click(initObject);

            if (initObject.Mdi_Principal.MESSAGES.Count == 0)
                mdi_PrincipalLogic.Participantes2_2(initObject, uow);
        }

        public void Participantes_Cancelar(InitializationObject initObject)
        {
            Frm_Participantes.Cancelar_Click(initObject);

            mdi_PrincipalLogic.Participantes2_2(initObject, uow);
        }

        public void Participantes_Identificar(InitializationObject initObject)
        {
            bool seguirEjecutando = Frm_Participantes.Identificar_Click_1(initObject, uow);

            if (seguirEjecutando && string.IsNullOrEmpty(initObject.FormularioQueAbrir))
            {
                Frm_Participantes.Identificar_Click_2(initObject, uow);
            }
        }

        public void Participantes_Eliminar(InitializationObject initObject)
        {
            Frm_Participantes.Eliminar_Click(initObject);
        }

        public void Participantes_LstPartys_Click(InitializationObject initObject)
        {
            Frm_Participantes.LstPartys_Click(initObject);
        }

        public void Participantes_Donde_Click(InitializationObject initObject)
        {
            Frm_Participantes.Donde_Click(initObject);
        }

        public void ParticipantesIdentificar_Aceptar(InitializationObject initObj)
        {
            Frm_Iden_Paticipantes.Aceptar_Click(initObj);
            Frm_Iden_Paticipantes.Dire_Click(initObj);

            if (initObj.Mdl_Funciones_Varias.CARGA_AUTOMATICA == 1)
            {
                //si vengo de carga automatica termino de ejecutar esa logica
                Frm_Participantes.Pr_CargaPARTY2_3(initObj, uow);
            }
            else
            {
                Frm_Participantes.Identificar_Click_2(initObj, uow);
            }
        }

        public void ConsultaParticipantes_Buscar(InitializationObject initObj)
        {
            Frm_Participantes.Bot_Nem_Click(initObj);
            Frm_Con_Participantes.ok_Click(initObj, uow);
        }

        public void ParticipantesCrearInit(InitializationObject initObj)
        {
            Frm_Crea_Participante.Form_Load(initObj);
        }

        public void ParticipantesCrear_Consultar(InitializationObject initObj)
        {
            Frm_Crea_Participante.Consulta_Click(initObj);
        }
        public bool ParticipantesCrear_Aceptar(InitializationObject initObj)
        {
            bool ok = Frm_Crea_Participante.Aceptar_Click(initObj, uow);
            if (ok)
            {
                Frm_Participantes.Identificar_Click_2(initObj, uow);
            }

            return ok;
        }

        public bool ParticipantesCrear_Cancelar(InitializationObject initObj)
        {
            bool ok = Frm_Crea_Participante.Cancelar_Click(initObj, uow);
            if (ok)
            {
                Frm_Participantes.Identificar_Click_2(initObj, uow);
            }

            return ok;
        }
        #endregion

        #region LOAD DE FORMULARIOS
        public void LoadFrmComercioInvisible(InitializationObject Modulos)
        {
            Frm_Comercio_Invisible_Logic.Form_Load(Modulos, uow);
        }
        public void LoadIngresoValores(InitializationObject Modulos)
        {
            Frm_Ingreso_Valores_Logic.FormLoad(Modulos, uow);
        }
        public void LoadFrmArbitrajes(InitializationObject Modulos)
        {
            Frm_Arbitrajes_Logic.Form_Load(Modulos, uow);
        }
        #endregion

        #region INGRESO VALORES
        public void IngresoValores(InitializationObject Modulos)
        {
            Frm_Ingreso_Valores_Logic.IngresarValores(Modulos, uow);
        }
        public void MergeValues(InitializationObject Modulos, string paridad, string cambioObs)
        {
            Modulos.Frm_Ingreso_Valores.Paridad.Text = paridad;
            Modulos.Frm_Ingreso_Valores.TC_Obs.Text = cambioObs;
            Modulos.MODGTAB0.VVmd.VmdPrd = VB6Helpers.Val(MODGPYF0.unformat(Modulos.MODGPYF0, paridad));
            Modulos.MODGTAB0.VVmd.VmdObs = VB6Helpers.Val(MODGPYF0.unformat(Modulos.MODGPYF0, cambioObs));
        }
        public void INGVAL_Cancelar(InitializationObject initObject)
        {
            Frm_Ingreso_Valores_Logic.Cancelar_Click(initObject);
        }
        #endregion

        #region COMERCIO INVISIBLE
        #region EVENTOS
        public void CB_Divisa_Select(InitializationObject initObject)
        {
            Frm_Comercio_Invisible_Logic.Cb_Divisa_click(initObject, uow);
        }
        public void CB_Moneda_Select(InitializationObject initObject)
        {
            Frm_Comercio_Invisible_Logic.Cb_Moneda_Click(initObject, uow);
        }
        public void Lt_Tcp_Select(InitializationObject initObject)
        {
            Frm_Comercio_Invisible_Logic.Lt_Tcp_Click(initObject, uow);
        }
        public void Tx_MtoCV_Key(InitializationObject initObject, short index)
        {
            short d = 0;
            Frm_Comercio_Invisible_Logic.Tx_MtoCV_KeyPress(initObject.Mdi_Principal, initObject.Frm_Comercio_Invisible, ref index, ref d);
        }
        public void Tx_MtoCV_Blur(InitializationObject initObject, short index)
        {
            Frm_Comercio_Invisible_Logic.Tx_MtoCV_LostFocus(initObject.Frm_Comercio_Invisible, ref index);
        }
        public void Ch_Convenio_Click(InitializationObject initObject)
        {
            Frm_Comercio_Invisible_Logic.Ch_Convenio_Click(initObject.MODGCVD, initObject.Frm_Comercio_Invisible);
        }
        public void ok_Click(InitializationObject initObject)
        {
            Frm_Comercio_Invisible_Logic.ok_Click(initObject, uow);
        }
        public void co_boton(InitializationObject initObject, short index)
        {
            Frm_Comercio_Invisible_Logic.Co_Boton_Click(initObject.Module1, initObject.MODGRNG, initObject.MODGUSR, initObject.MODGSCE, initObject.MODGCVD, initObject.Mdl_Funciones, initObject.MODGPLI1, initObject.Mdi_Principal, uow, ref index);
        }
        public void lt_operacion_click(InitializationObject initObject)
        {
            Frm_Comercio_Invisible_Logic.Lt_Operacion_Click(initObject, uow);
        }
        public void no_Click(InitializationObject initObject)
        {
            Frm_Comercio_Invisible_Logic.NO_Click(initObject, uow);
        }
        public void tx_codadn_Blur(InitializationObject initObject)
        {
            Frm_Comercio_Invisible_Logic.Tx_CodAdn_LostFocus(initObject.Mdl_Funciones_Varias, initObject.MODGTAB1, initObject.Mdi_Principal, initObject.Frm_Comercio_Invisible, uow);
        }
        public void tx_docnac_Blur(InitializationObject initObject)
        {
            Frm_Comercio_Invisible_Logic.Tx_DocNac_LostFocus(initObject.Mdi_Principal, initObject.Frm_Comercio_Invisible);
        }
        public void tx_er_Blur(InitializationObject initObject)
        {
            Frm_Comercio_Invisible_Logic.Tx_ER_LostFocus(initObject.Mdi_Principal, initObject.Frm_Comercio_Invisible);
        }
        public void tx_nrodec_Blur(InitializationObject initObject)
        {
            Frm_Comercio_Invisible_Logic.Tx_NroDec_LostFocus(initObject.Mdl_Funciones_Varias, initObject.Frm_Comercio_Invisible, uow);
        }
        #endregion
        #endregion

        #region ARBITRAJES
        public void ARBITRAJES_cb_moneda_compra_Blur(InitializationObject initObject)
        {
            Frm_Arbitrajes_Logic.Cb_Moneda_Compra_Click(initObject, uow);
        }
        public void ARBITRAJES_cb_moneda_venta_Blur(InitializationObject initObject)
        {
            Frm_Arbitrajes_Logic.Cb_Moneda_Venta_Click(initObject, uow);
        }
        public bool ARBITRAJES_co_boton_Click(InitializationObject initObject, short index, bool salioMensaje, bool acepta)
        {
            return Frm_Arbitrajes_Logic.Co_Boton_Click(initObject, uow, index, salioMensaje, acepta);
            //if (termino)
            //{
            //    OpcionBotones(initObject, 1);
            //}
        }
        public void ARBITRAJES_lt_operacion_Click(InitializationObject initObject)
        {
            Frm_Arbitrajes_Logic.Lt_Operacion_Click(initObject, uow);
        }
        public void ARBITRAJES_tx_mtoarb_keypress(InitializationObject initObject, short Index)
        {
            Frm_Arbitrajes_Logic.Tx_Mtoarb_KeyPress(initObject.Frm_Arbitrajes, Index);
        }
        public void ARBITRAJES_tx_mtoarb_lostfocus(InitializationObject initObject, short index)
        {
            Frm_Arbitrajes_Logic.Tx_Mtoarb_LostFocus(initObject.MODGARB, initObject.Frm_Arbitrajes, initObject.MODGTAB0, index);
        }
        public void ARBITRAJES_no_click(InitializationObject initObject)
        {
            Frm_Arbitrajes_Logic.NO_Click(initObject, uow);
        }
        public void ARBITRAJES_ok_Click(InitializationObject initObject, bool acepta)
        {
            bool correcto = Frm_Arbitrajes_Logic.ok_Click(initObject, uow, acepta);
            if (!correcto && initObject.Frm_Arbitrajes.IrAIngresoValores)
            {
                initObject.Frm_Arbitrajes.IrAIngresoValores = true;
                initObject.FormularioQueAbrir = "Ingreso_Valores";
                initObject.Frm_Arbitrajes.VieneDeIngresoValores = true;
            }
        }
        public void ARBITRAJES_ok_2_Click(InitializationObject initObject)
        {
            Frm_Arbitrajes_Logic.ok_2_Click(initObject, uow);
        }

        #endregion

        #region COMERCIO VISIBLE EXPORT
        public void COMVISEXP_Form_Load(InitializationObject Modulos)
        {
            Frm_VisE_Logic.Form_Load(Modulos.MODXPLN0, Modulos.MODGPYF0, Modulos.MODGTAB0, Modulos.MODGCVD, Modulos.Frm_VisE,
                Modulos.Mdl_Funciones_Varias, uow);
        }
        public void COMVISEXP_Cb_Mnd_Blur(InitializationObject Modulos)
        {
            Frm_VisE_Logic.Cb_Mnd_Blur(Modulos.MODGCVD, Modulos.MODGTAB0, Modulos.Mdl_Funciones_Varias, Modulos.Frm_VisE, uow);
        }
        public void COMVISEXP_Tx_MtoVisE_Blur(InitializationObject Modulos, short index)
        {
            Frm_VisE_Logic.Tx_MtoVisE_LostFocus(Modulos.Frm_VisE, index);
        }
        public bool COMVISEXP_Co_Boton_Click(InitializationObject Modulos, short index)
        {
            return Frm_VisE_Logic.Co_Boton_Click(Modulos.MODGTAB0, Modulos.MODXPLN0, Modulos.MODGCVD, Modulos.Frm_VisE, uow, index);
        }

        public void PlanillaVisibleExportInit(InitializationObject initObj)
        {
            if (initObj.FrmxPln0 == null)
            {
                //initObj.Frm_VisE = null;
                initObj.FrmxPln0 = new UI_FrmxPln0();

                FrmxPln0.Form_Load(initObj, uow);
            }
        }

        public bool PlanillaVisibleExport_OK_Click(InitializationObject initObj)
        {
            return FrmxPln0.ok_Click(initObj, uow);
        }

        public void PlanillaVisibleExport_NO_Click(InitializationObject initObj)
        {
            FrmxPln0.NO_Click(initObj);
        }

        public bool PlanillaVisibleExport_OK_Dec_Click(InitializationObject initObj)
        {
            return FrmxPln0.Ok_Dec_Click(initObj, uow);
        }

        public bool PlanillaVisibleExport_Boton_Click(InitializationObject initObj, short indice)
        {

            bool ok = FrmxPln0.Boton_Click(indice, initObj, uow);

            if (ok && (indice == 0 || indice == 2)) //aceptar o cancelar, termino la logica del documento
            {
                mdi_PrincipalLogic.Opciones_Botones(initObj, uow, 2);
            }

            return ok;
        }

        public void PlanillaVisibleExport_Ch_Deduc_Click(InitializationObject initObj, short indice)
        {
            FrmxPln0.Ch_Deduc_Click(indice, initObj);
        }

        public void PlanillaVisibleExport_LtMto_Click(InitializationObject initObj)
        {
            FrmxPln0.LtMto_Click(initObj);
        }

        public void PlanillaVisibleExport_LtPln_Click(InitializationObject initObj)
        {
            FrmxPln0.LtPln_Click(initObj);
        }

        public void PlanillaVisibleExport_LtTPln_Click(InitializationObject initObj)
        {
            FrmxPln0.LtTPln_Click(initObj);
        }

        public void PlanillaVisibleExport_Tx_MtoDec_LostFocus(InitializationObject initObj, short indice)
        {
            FrmxPln0.Tx_MtoDec_LostFocus(indice, initObj);
        }

        public void PlanillaVisibleExport_Tx_NumDec_LostFocus(InitializationObject initObj)
        {
            FrmxPln0.Tx_NumDec_LostFocus(initObj, uow);
        }

        public void PlanillaVisibleExport_LtTPln_LostFocus(InitializationObject initObj)
        {
            FrmxPln0.LtTPln_LostFocus(initObj);
        }

        #endregion

        #region CARGAR OPERACIONES
        public void frm_Consulta_Load(InitializationObject initObj)
        {
            UnitOfWorkCext01 uow = new UnitOfWorkCext01();
            Frm_Consulta.Form_Load(initObj, uow);
        }

        public List<pro_sce_prty_s04_MS_Result> Frm_Consulta_img_Buscar_Click(InitializationObject initObj, string nroTransaccion, string estado)
        {
            return Frm_Consulta.img_Buscar_Click(initObj, uow, nroTransaccion, estado);
        }

        public List<pro_sce_prty_s04_MS_Result> Frm_Consulta_img_Buscar_Click(InitializationObject initObj, string nroTransaccion, string estado, string year)
        {
            return Frm_Consulta.img_Buscar_Click(initObj, uow, nroTransaccion, estado, year);
        }
        public void msg_operaciones_DblClick(InitializationObject initObj, pro_sce_prty_s04_MS_Result data)
        {
            Frm_Consulta.msg_operaciones_DblClick(initObj, uow, data);
        }
        public MemoryStream ExportarOperaciones(InitializationObject initObj, List<pro_sce_prty_s04_MS_Result> data)
        {
            MemoryStream stream = new MemoryStream();
            using (SLDocument doc = new SLDocument())
            {
                doc.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Consulta de Operaciones");
                doc.AddWorksheet("Consulta de Operaciones");

                ExcelOperaciones(data, doc);

                doc.SaveAs(stream);
            }

            stream.Position = 0; //importante, dejar el stream pronto para leer;
            return stream;
        }

        private void ExcelOperaciones(List<pro_sce_prty_s04_MS_Result> data, SLDocument doc)
        {
            SLStyle styleTitulo = doc.CreateStyle();
            styleTitulo.Font.Bold = true;
            styleTitulo.Font.FontSize = 15;
            styleTitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleSubtitulo = doc.CreateStyle();
            styleSubtitulo.Font.Bold = true;
            styleSubtitulo.Font.FontSize = 13;
            styleSubtitulo.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            SLStyle styleEncabezado = doc.CreateStyle();
            styleEncabezado.Font.Bold = true;
            styleEncabezado.SetHorizontalAlignment(HorizontalAlignmentValues.Center);

            doc.SetCellValue("A1", "Exportación Consulta de Operaciones");
            doc.SetCellStyle("A1", styleTitulo);

            doc.SetCellValue("A2", "Fecha Exportación: " + DateTime.Now.ToString("dd/MM/yyyy"));
            doc.SetCellStyle("A2", styleSubtitulo);

            int colIndex = 1;
            int rowIndex = 4;
            doc.SetCellValue(rowIndex, colIndex++, "Nº Transacción");
            doc.SetCellValue(rowIndex, colIndex++, "Fecha de Ingreso");
            doc.SetCellValue(rowIndex, colIndex++, "Razon Social");
            doc.SetCellValue(rowIndex, colIndex++, "Moneda");
            doc.SetCellValue(rowIndex, colIndex++, "Monto");
            doc.SetCellValue(rowIndex, colIndex++, "N° Referencia");
            doc.SetCellValue(rowIndex, colIndex++, "Estado");
            doc.SetCellValue(rowIndex, colIndex++, "Cod. Moneda");
            doc.SetCellValue(rowIndex, colIndex++, "Tipo TRX.");
            doc.SetCellValue(rowIndex, colIndex++, "Beneficiario");
            doc.SetCellValue(rowIndex, colIndex++, "Ordenante");
            doc.SetCellValue(rowIndex, colIndex++, "ORD_INST1");
            doc.SetCellValue(rowIndex, colIndex++, "PMNT_DET1");
            doc.SetCellValue(rowIndex, colIndex++, "PMNT_DET2");
            doc.SetCellValue(rowIndex, colIndex++, "PMNT_DET3");
            doc.SetCellValue(rowIndex, colIndex++, "PMNT_DET4");
            doc.SetCellValue(rowIndex, colIndex++, "DEBIT_REF");
            doc.SetCellValue(rowIndex, colIndex++, "cod_estado");
            doc.SetCellValue(rowIndex, colIndex++, "Observacion");
            doc.SetCellValue(rowIndex, colIndex++, "SWF");
            doc.SetCellValue(rowIndex, colIndex++, "BEN_INST1");
            doc.SetCellValue(rowIndex, colIndex++, "ULT_BEN1");
            doc.SetCellValue(rowIndex, colIndex++, "ULT_BEN2");
            doc.SetCellValue(rowIndex, colIndex++, "ULT_BEN3");
            doc.SetCellValue(rowIndex, colIndex++, "ULT_BEN4");
            doc.SetCellValue(rowIndex, colIndex++, "CHG_WHOM");
            doc.SetCellValue(rowIndex, colIndex++, "DRVALDT");
            doc.SetCellValue(rowIndex, colIndex++, "INTRMD1");
            doc.SetCellValue(rowIndex, colIndex++, "INTRMD2");
            doc.SetCellValue(rowIndex, colIndex++, "US_PAY_ID");
            doc.SetCellValue(rowIndex, colIndex++, "RECVR_CORRES1");
            doc.SetCellValue(rowIndex, colIndex++, "RECVR_CORRES2");
            doc.SetCellValue(rowIndex, colIndex++, "LC_SNDR_RECVR_INFO1");
            doc.SetCellValue(rowIndex, colIndex++, "LC_SNDR_RECVR_INFO2");
            doc.SetCellValue(rowIndex, colIndex++, "LC_SNDR_RECVR_INFO3");
            doc.SetCellValue(rowIndex, colIndex++, "LC_SNDR_RECVR_INFO4");
            doc.SetCellValue(rowIndex, colIndex++, "TRANSACCION ID");
            doc.SetCellValue(rowIndex, colIndex++, "C. R. Number");
            doc.SetCellValue(rowIndex, colIndex++, "LC_SNDR_RECVR_INFO5");
            doc.SetCellValue(rowIndex, colIndex++, "LC_SNDR_RECVR_INFO6");
            doc.SetCellValue(rowIndex, colIndex++, "MONTO ORIGINAL");
            doc.SetCellStyle("A4", "AO4", styleEncabezado);

            foreach (pro_sce_prty_s04_MS_Result d in data)
            {
                colIndex = 1;
                rowIndex++;

                doc.SetCellValue(rowIndex, colIndex++, d.FCCFT);
                doc.SetCellValue(rowIndex, colIndex++, d.fecingreso);
                doc.SetCellValue(rowIndex, colIndex++, d.razonSocial);
                doc.SetCellValue(rowIndex, colIndex++, d.mnd_mndnom);
                doc.SetCellValue(rowIndex, colIndex++, d.DRAMT);
                doc.SetCellValue(rowIndex, colIndex++, d.XREF);
                doc.SetCellValue(rowIndex, colIndex++, d.desc_ft);
                doc.SetCellValue(rowIndex, colIndex++, d.codmnd_bch.Value);
                doc.SetCellValue(rowIndex, colIndex++, d.TIPO_TRX);
                doc.SetCellValue(rowIndex, colIndex++, d.Beneficiario);
                doc.SetCellValue(rowIndex, colIndex++, d.Ordenante);
                doc.SetCellValue(rowIndex, colIndex++, d.ORD_INST1);
                doc.SetCellValue(rowIndex, colIndex++, d.PMNT_DET1);
                doc.SetCellValue(rowIndex, colIndex++, d.PMNT_DET2);
                doc.SetCellValue(rowIndex, colIndex++, d.PMNT_DET3);
                doc.SetCellValue(rowIndex, colIndex++, d.PMNT_DET4);
                doc.SetCellValue(rowIndex, colIndex++, d.DEBIT_REF);
                doc.SetCellValue(rowIndex, colIndex++, d.str_cod_estado);
                doc.SetCellValue(rowIndex, colIndex++, d.desc_ft);
                doc.SetCellValue(rowIndex, colIndex++, d.str_swft);
                doc.SetCellValue(rowIndex, colIndex++, d.BEN_INST1);
                doc.SetCellValue(rowIndex, colIndex++, d.ULT_BEN1);
                doc.SetCellValue(rowIndex, colIndex++, d.ULT_BEN2);
                doc.SetCellValue(rowIndex, colIndex++, d.ULT_BEN3);
                doc.SetCellValue(rowIndex, colIndex++, d.ULT_BEN4);
                doc.SetCellValue(rowIndex, colIndex++, d.CHG_WHOM);
                doc.SetCellValue(rowIndex, colIndex++, d.DRVALDT);
                doc.SetCellValue(rowIndex, colIndex++, d.INTRMD1);
                doc.SetCellValue(rowIndex, colIndex++, d.INTRMD2);
                doc.SetCellValue(rowIndex, colIndex++, d.US_PAY_ID);
                doc.SetCellValue(rowIndex, colIndex++, d.RECVR_CORRES1);
                doc.SetCellValue(rowIndex, colIndex++, d.RECVR_CORRES2);
                doc.SetCellValue(rowIndex, colIndex++, d.SNDR_RECVR_INFO1);
                doc.SetCellValue(rowIndex, colIndex++, d.SNDR_RECVR_INFO2);
                doc.SetCellValue(rowIndex, colIndex++, d.SNDR_RECVR_INFO3);
                doc.SetCellValue(rowIndex, colIndex++, d.SNDR_RECVR_INFO4);
                doc.SetCellValue(rowIndex, colIndex++, d.trxid);
                doc.SetCellValue(rowIndex, colIndex++, d.contract_reference.Value);
                doc.SetCellValue(rowIndex, colIndex++, d.SNDR_RECVR_INFO5);
                doc.SetCellValue(rowIndex, colIndex++, d.SNDR_RECVR_INFO6);
                doc.SetCellValue(rowIndex, colIndex++, d.MONTO_ORIGINAL);
            }

            doc.AutoFitColumn("A", "AO");
        }
        #endregion

        #region PLANILLA VISIBLE IMPORT
        public void PlvSO_LoadFrm(InitializationObject Modulos)
        {
            Frm_PlvSO.Form_Load(Modulos, uow);
        }

        public void PlvSO_Cb_Moneda_Click(InitializationObject Modulos)
        {
            Frm_PlvSO.Cb_Moneda_Click(Modulos, uow);
        }

        public void PlvSO_Bot_OkDec_Click(InitializationObject Modulos)
        {
            Frm_PlvSO.Bot_OkDec_Click(Modulos, uow);
        }

        public void PlvSO_Bot_OkFinal_Click(InitializationObject Modulos)
        {
            Frm_PlvSO.Bot_OkFinal_Click(Modulos, uow);
        }

        public void PlvSO_MtoFob_Blur(InitializationObject Modulos)
        {
            Frm_PlvSO.Tx_MtoFob_LostFocus(Modulos, uow);
        }

        public void PlvSO_MtoFle_Blur(InitializationObject Modulos)
        {
            Frm_PlvSO.Tx_MtoFle_LostFocus(Modulos, uow);
        }

        public void PlvSO_MtoSeg_Blur(InitializationObject Modulos)
        {
            Frm_PlvSO.Tx_MtoSeg_LostFocus(Modulos, uow);
        }

        public void PlvSO_NroPre_Blur(InitializationObject Modulos)
        {
            Frm_PlvSO.Tx_NroPre_LostFocus(Modulos);
        }

        public void PlvSO_TipCam_Blur(InitializationObject Modulos)
        {
            Frm_PlvSO.Tx_TipCam_LostFocus(Modulos);
        }
        public void PlvSO_Paridad_Blur(InitializationObject Modulos)
        {
            Frm_PlvSO.Tx_Paridad_LostFocus(Modulos);
        }
        public void PlvSO_Tx_CodPag_Blur(InitializationObject Modulos)
        {
            Frm_PlvSO.Tx_CodPag_LostFocus(Modulos, uow);
        }
        public void PlvSO_DocChi_Blur(InitializationObject Modulos)
        {
            Frm_PlvSO.Tx_DocChi_LostFocus(Modulos);
        }
        public void PlvSO_Tx_FecRee_Blur(InitializationObject Modulos)
        {
            Frm_PlvSO.Tx_FecRee_LostFocus(Modulos);
        }
        public void PlvSO_Tt_Final_Click(InitializationObject Modulos)
        {
            Frm_PlvSO.Lt_Final_DblClick(Modulos);
        }
        public void PlvSO_Ch_Transf_Click(InitializationObject Modulos)
        {
            Frm_PlvSO.Ch_Transf_Click(Modulos, uow);
        }

        public void PlvSO_Ch_PlanRee_Click(InitializationObject Modulos)
        {
            Frm_PlvSO.Ch_PlanRee_Click(Modulos);
        }

        public void PlvSO_Bot_Cancel_Click(InitializationObject Modulos)
        {
            Frm_PlvSO.Bot_Cancel_Click(Modulos);
        }
        public void PlvSO_Bot_NoFinal_Click(InitializationObject Modulos)
        {
            Frm_PlvSO.Bot_NoFinal_Click(Modulos);
        }
        public void PlvSO_Bot_Acepta_Click(InitializationObject Modulos)
        {
            Frm_PlvSO.Bot_Acepta_Click(Modulos);
        }


        #endregion

        #region RELACIONAR OPERACION
        public void FrmgAso_LoadFrm(InitializationObject initObj)
        {
            FrmgAso.Form_Load(initObj, uow);
        }
        public void FrmgAso_ok_Click(InitializationObject initObj)
        {
            FrmgAso.ok_Click(initObj, uow);
        }
        public void FrmgAso_Aceptar_Click(InitializationObject initObj)
        {
            FrmgAso.Aceptar_Click(initObj, uow);
        }
        public void FrmgAso_Cancelar_Click(InitializationObject initObj)
        {
            FrmgAso.Cancelar_Click(initObj);
        }
        public void FrmgAso_Cb_Producto_Click(InitializationObject initObj)
        {
            FrmgAso.Cb_Producto_Click(initObj, uow);
        }
        public void FrmgAso_Tx_NumOpe_LostFocus(InitializationObject initObj, short index)
        {
            FrmgAso.Tx_NumOpe_LostFocus(ref index, initObj.FrmgAso, initObj);
        }
        #endregion
        #region ORIGENES DE FONDOS
        public void OrigenFondosInit(InitializationObject initObj, bool hayMensaje, bool respuestaMensaje)
        {
            mdi_PrincipalLogic.Origen_Fondos_1_2(initObj, uow, hayMensaje, respuestaMensaje);
            if (initObj.Frm_Origen_Fondos != null && String.IsNullOrEmpty(initObj.FormularioQueAbrir))
            {
                Frm_Origen_Fondos_Logic.Form_Load(initObj, uow);
                //Frm_Origen_Fondos_Logic.Form_Activate(initObj, uow);
            }

        }
        public void ORIFOND_Form_Load(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.Form_Load(Modulos, uow);
        }
        public void ORIFOND_BNem_Click(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.BNem_Click(Modulos, uow);
        }
        public void ORIFOND_Boton_Click(InitializationObject Modulos, short index)
        {
            Frm_Origen_Fondos_Logic.Boton_Click(Modulos, index);
        }
        public void ORIFOND_Bt_PlnTrn_Click(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.Bt_PlnTrn_Click(Modulos);
        }
        public void ORIFOND_cmb_codtran_Click(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.cmb_codtran_Click(Modulos);
        }
        public void ORIFOND_Form_Activate(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.Form_Activate(Modulos, uow);
        }
        public void ORIFOND_L_Cta_Click(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.L_Cta_Click(Modulos, uow);
        }
        public void ORIFOND_L_Cuentas_Click(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.L_Cuentas_Click(Modulos);
        }
        public void ORIFOND_l_mnd_Click(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.l_mnd_Click(Modulos, uow);
        }
        public void ORIFOND_l_mto_Click(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.l_mto_Click(Modulos, uow);
        }
        public void ORIFOND_l_ori_Click(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.l_ori_Click(Modulos, uow);
        }
        public void ORIFOND_L_Partys_Click(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.L_Partys_Click(Modulos, uow);
        }
        public void ORIFOND_NO_Click(InitializationObject Modulos, bool pasoPorPopUp, bool acepta)
        {
            Frm_Origen_Fondos_Logic.NO_Click(Modulos, uow, pasoPorPopUp, acepta);
        }
        public void ORIFOND_Ok_Partys_Click(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.Ok_Partys_Click(Modulos, uow);
        }
        public void ORIFOND_Tx_Datos_KeyPress(InitializationObject Modulos, short index)
        {
            Frm_Origen_Fondos_Logic.Tx_Datos_KeyPress(Modulos, uow, index);
        }
        public void ORIFOND_Tx_Datos_LostFocus(InitializationObject Modulos, short index, int prevValue = 0)
        {
            Frm_Origen_Fondos_Logic.Tx_Datos_LostFocus(Modulos, index);

            if ((Modulos.Frm_Origen_Fondos.L_Cta.Value == 8 && prevValue != 8) && Modulos.MODXORI.VOvd[Modulos.Frm_Origen_Fondos.Indice_Cuenta].NumCta == T_MODGCON0.IdCta_ONME)
            {
                Frm_Origen_Fondos_Logic.L_Cta_Click(Modulos, uow);
            }

        }
        public void ORIFOND_ok_Click(InitializationObject Modulos, bool vieneDeMensaje, bool resMsg)
        {
            short res = Frm_Origen_Fondos_Logic.ok_Click_1(Modulos, uow, vieneDeMensaje, resMsg);
            if (res > 0)
            {
                var res2 = Frm_Origen_Fondos_Logic.ok_Click_2(Modulos, uow, res, vieneDeMensaje, resMsg);
            }
        }
        public void ORIFOND_ok_Click_Final(InitializationObject Modulos)
        {
            Frm_Origen_Fondos_Logic.ok_Click_3(Modulos, uow);
        }

        public void ORIFOND_Aceptar(InitializationObject Modulos)
        {
            mdi_PrincipalLogic.Origen_Fondos_2_2(Modulos, uow);
        }
        #endregion

        #region DESTINOS DE FONDOS
        public void DestinoFondosInit(InitializationObject initObj, bool hayMensaje, bool respuestaMensaje, bool vueltos = false)
        {
            if (!vueltos)
            {
                mdi_PrincipalLogic.Destino_Fondos1_2(initObj, uow, hayMensaje, respuestaMensaje);
                if ((initObj.Frm_Destino_Fondos != null) && String.IsNullOrEmpty(initObj.FormularioQueAbrir))
                {
                    Frm_Destino_Fondos_Logic.Form_Load(initObj, uow);
                }
            }
            else
            {
                initObj.Frm_Destino_Fondos = new UI_Frm_Destino_Fondos();
                Frm_Destino_Fondos_Logic.Form_Load(initObj, uow);
            }

        }
        public void DESTFOND_Form_Load(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.Form_Load(Modulos, uow);
        }
        public void DESTFOND_L_Mnd_Blur(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.l_mnd_Click(Modulos, uow);
        }
        public void DESTFOND_L_Cta_Blur(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.L_Cta_Click(Modulos, uow);
        }
        public void DESTFOND_L_Party_Blur(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.L_Partys_Click(Modulos, uow);
        }
        public void DESTFOND_L_Cuentas_Blur(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.L_Cuentas_Click(Modulos);
        }
        public void DESTFOND_OK_CLick(InitializationObject Modulos, bool vieneDeMsg, bool resMsg)
        {
            Frm_Destino_Fondos_Logic.ok_Click(Modulos, uow, vieneDeMsg, resMsg);
        }
        public void DESTFOND_Aceptar(InitializationObject Modulos)
        {
            mdi_PrincipalLogic.Destino_Fondos2_2(Modulos, uow);
        }
        public void DESTFOND_BNem_Click(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.BNem_Click(Modulos, uow);
        }
        public void DESTFOND_Boton_Click(InitializationObject Modulos, short index)
        {
            Frm_Destino_Fondos_Logic.Boton_Click(Modulos, index);
        }
        public void DESTFOND_Bt_PlnTrn_Click(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.Bt_PlnTrn_Click(Modulos);
        }
        public void DESTFOND_Cb_Destino_Click(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.Cb_Destino_Click(Modulos);
        }
        public void DESTFOND_cmb_codtran_Click(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.cmb_codtran_Click(Modulos);
        }
        public void DESTFOND_l_mto_Click(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.l_mto_Click(Modulos, uow);
        }
        public void DESTFOND_l_via_Click(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.l_via_Click(Modulos, uow);
        }
        public void DESTFOND_NO_Click(InitializationObject Modulos, bool vieneDeMensaje, bool resMensaje)
        {
            Frm_Destino_Fondos_Logic.NO_Click(Modulos, uow, vieneDeMensaje, resMensaje);
        }
        public void DESTFOND_Tx_Datos_Blur(InitializationObject Modulos, short index)
        {
            Frm_Destino_Fondos_Logic.Tx_Datos_KeyPress(Modulos, uow, index);
        }
        public void DESTFOND_txtNumRef_Blur(InitializationObject Modulos)
        {
            Frm_Destino_Fondos_Logic.txtNumRef_LostFocus(Modulos);
        }
        #endregion

        #region Imprimir documentos

        public IList<DocumentoOperacion> BuscarDocumentosOperacionesPorFecha(DateTime fechaOperacion, string centroCosto, string codigoUsr)
        {
            List<DocumentoOperacion> result = new List<DocumentoOperacion>();
            result.AddRange(uow.DocumentosOperacionesRepository.Sce_Mch_s02_MS(fechaOperacion, centroCosto, codigoUsr));
            result.AddRange(uow.DocumentosOperacionesRepository.Sce_Swf_S03(fechaOperacion, centroCosto, codigoUsr));
            result.AddRange(uow.DocumentosOperacionesRepository.Sce_XDoc_S03_MS(fechaOperacion, centroCosto, codigoUsr));
            AgregarDescripcionALosDocumentos(result);
            return result;
        }

        public IList<DocumentoOperacion> BuscarDocumentosOperacionesPorNroOperacion(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            List<DocumentoOperacion> result = new List<DocumentoOperacion>();
            result.AddRange(uow.DocumentosOperacionesRepository.Sce_Mch_s03_MS(codcct, codpro, codesp, codofi, codope));
            result.AddRange(uow.DocumentosOperacionesRepository.Sce_Swf_S04(codcct, codpro, codesp, codofi, codope));
            result.AddRange(uow.DocumentosOperacionesRepository.Sce_XDoc_S04_MS(codcct, codpro, codesp, codofi, codope));
            AgregarDescripcionALosDocumentos(result);
            return result;
        }

        public IList<DocumentoOperacion> BuscarDocumentosOperacionesPorContactReference(string contactReference)
        {
            IList<DocumentoOperacion> temp = uow.DocumentosOperacionesRepository.Pro_sce_relacion_s01_MS(contactReference);
            List<DocumentoOperacion> result = new List<DocumentoOperacion>();
            foreach (DocumentoOperacion doc in temp.GroupBy(x => x.CodCct).Select(y => y.First()))
            {
                result.AddRange(this.BuscarDocumentosOperacionesPorNroOperacion(doc.CodCct, doc.CodPro, doc.CodEsp, doc.CodOfi, doc.CodOpe));
            }
            return result;
        }

        public string GetDetalleMensajeSwift(string nroMensaje)
        {
            string detalle = uow.SceRepository.sce_memg_s01_MS("s", nroMensaje);
            if (!String.IsNullOrEmpty(detalle))
            {
                return detalle.Replace("*", " ");
            }

            return null;
        }

        public ReporteContable GetReporteContable(InitializationObject initObj, int nroReporte, DateTime fecha, IList<sce_dfc> descripcionesFunciones)
        {
            sce_mch_s01_MS_Result cabecera = uow.DocumentosOperacionesRepository.sce_mch_s01_MS(nroReporte, fecha).FirstOrDefault();
            if (cabecera != null)
            {
                cabecera.NombreEspecialista = uow.SceRepository.sce_usr_s07_MS(cabecera.cencos, cabecera.codusr);
                sce_dfc descFuncion = descripcionesFunciones.Where(f => f.coddfc == cabecera.codfun).FirstOrDefault();
                if (descFuncion != null)
                {
                    cabecera.DescripcionFuncionContable = descFuncion.desdfc;
                }
                cabecera.desgen = cabecera.desgen.Trim();
                cabecera.nomcli = cabecera.nomcli.Trim();

                IList<sce_mcd> lineas = uow.SceRepository.sce_mcd_s07_MS(nroReporte, fecha);
                foreach (sce_mcd linea in lineas)
                {
                    linea.numcct = FormatearNroDeCuenta(initObj, linea.numcct, Convert.ToInt16(linea.codmon));
                    int indiceCuenta = MODGNCTA.Get_Cta(linea.nemcta, initObj, uow);
                    linea.NombreCuenta = initObj.MODGNCTA.VCta[indiceCuenta].Cta_Nom.Value;
                    linea.DescAdicional = GetDescripcionAdicionalDeLineaContable(linea);
                }

                ReporteContable reporte = new ReporteContable()
                {
                    Cabecera = cabecera,
                    Lineas = lineas
                };

                return reporte;
            }

            return null;
        }

        public sce_mch_s01_MS_Result GetCabeceraReporteDetalleSwift(InitializationObject initObj, int nroReporte, DateTime fecha)
        {
            sce_mch_s01_MS_Result cabecera = uow.DocumentosOperacionesRepository.sce_mch_s01_MS(nroReporte, fecha).FirstOrDefault();
            if (cabecera != null)
            {
                return cabecera;
            }
            return null;
        }

        public IList<sce_dfc> GetDescripcionesFuncionesContables()
        {
            return uow.SceRepository.GetDescripcionesFuncionesContables();
        }

        private string FormatearNroDeCuenta(InitializationObject initObj, string nroCuenta, short codMoneda)
        {
            if (!String.IsNullOrEmpty(nroCuenta))
            {
                if (nroCuenta.Length > 8 && !initObj.MODXORI.gb_esCosmos)
                {
                    if (codMoneda == initObj.MODGSCE.VGen.MndNac)
                    {
                        return nroCuenta.Substring(0, 3) + "-" + nroCuenta.Substring(3, 5) + "-" + nroCuenta.Substring(8, nroCuenta.Length - 8);
                    }
                    else
                    {
                        return nroCuenta.Substring(0, 4) + "-" + nroCuenta.Substring(4, 5) + "-" + nroCuenta.Substring(9, nroCuenta.Length - 9);
                    }
                }
            }

            return nroCuenta;
        }

        private string GetDescripcionAdicionalDeLineaContable(sce_mcd linea)
        {

            string descBanco, descRef, descCuenta;

            switch ((short)linea.idncta)
            {
                case T_MODGCON0.IdCta_CtaCteMN:
                case T_MODGCON0.IdCta_CtaCteME:
                case T_MODGCON0.IdCta_ChqCCME:
                case T_MODGCON0.IdCta_CtaCteAUTN:
                case T_MODGCON0.IdCta_CtaCteAUTE:
                case T_MODGCON0.IdCta_CtaCteMANN:
                case T_MODGCON0.IdCta_CtaCteMANE:
                case T_MODGCON0.IdCta_GAPMN:
                case T_MODGCON0.IdCta_GAPME:

                    if (!String.IsNullOrEmpty(linea.rutcli) && !String.IsNullOrEmpty(linea.numcct))
                    {
                        return "Rut: " + linea.rutcli + "; Cta: " + linea.numcct;
                    }
                    break;

                case T_MODGCON0.IdCta_VAM:
                case T_MODGCON0.IdCta_VAX:
                case T_MODGCON0.IdCta_VAMC:
                case T_MODGCON0.IdCta_VAMCC:
                case T_MODGCON0.IdCta_VASC:
                    if (!String.IsNullOrEmpty(linea.rutcli))
                    {
                        return "Rut: " + linea.rutcli;
                    }
                    else
                    {
                        return "Rut: " + linea.prtcli;
                    }

                case T_MODGCON0.IdCta_SCSMN:
                case T_MODGCON0.IdCta_SCSME:
                    return "Ofi: " + linea.ofides + "-" + linea.numpar + "/" + linea.tipmov;

                case T_MODGCON0.IdCta_VVOB:
                case T_MODGCON0.IdCta_CHMEOBC:
                case T_MODGCON0.IdCta_CTACTEBC:
                case T_MODGCON0.IdCta_CTAORD:
                case T_MODGCON0.IdCta_DIVENPEN:
                case T_MODGCON0.IdCta_CHMEONY:
                case T_MODGCON0.IdCta_CHMEOBE:
                case T_MODGCON0.IdCta_CHVBNYM:
                case T_MODGCON0.IdCta_BOEREG:
                case T_MODGCON0.IdCta_CHEREG:
                case T_MODGCON0.IdCta_OBLREG:
                case T_MODGCON0.IdCta_OBLARE:
                case T_MODGCON0.IdCta_ACEREG:

                    descBanco = (String.IsNullOrEmpty(linea.swibco) ? String.Empty : "Bco:" + linea.swibco);
                    descRef = (String.IsNullOrEmpty(linea.nroref) ? String.Empty : "Ref:" + linea.nroref);
                    return (descBanco + " " + descRef).Trim();

                case T_MODGCON0.IdCta_VVBCH:
                case T_MODGCON0.IdCta_OPC:
                case T_MODGCON0.IdCta_OPOP:
                case T_MODGCON0.IdCta_CHMEBCH:
                case T_MODGCON0.IdCta_OPEPEND:
                case T_MODGCON0.IdCta_OBLACP:

                    descBanco = (String.IsNullOrEmpty(linea.swibco) ? String.Empty : "Bco: " + linea.swibco);
                    descCuenta = (String.IsNullOrEmpty(linea.numcct) ? String.Empty : "Cta: " + linea.numcct);
                    string descFecha = (linea.fecven == DateTime.MinValue || linea.fecven.ToString("dd/MM/yyyy") == "01-01-1900" ? String.Empty : " Vto: " + linea.fecven.ToString("dd/MM/yyyy"));

                    string result = (descBanco + " " + descCuenta).Trim();
                    if (!String.IsNullOrEmpty(linea.nroref) && linea.idncta != T_MODGCON0.IdCta_OBLACP)
                    {
                        result += (String.IsNullOrEmpty(descCuenta) ? " Ref: " + linea.nroref : "; Ref: " + linea.nroref);

                    }
                    return result += descFecha;
            }

            if (linea.idncta >= 40 && linea.idncta <= 54)
            {
                descBanco = (String.IsNullOrEmpty(linea.swibco) ? String.Empty : "Bco:" + linea.swibco);
                descRef = (String.IsNullOrEmpty(linea.nroref) ? String.Empty : "Ref:" + linea.nroref);
                return (descBanco + " " + descRef).Trim();
            }
            else
            {
                if (String.IsNullOrEmpty(linea.DescAdicional))
                {
                    if (linea.tipcam > 0)
                    {
                        return "T/C: " + Utils.Format.FormatCurrency((double)linea.tipcam, "###,##0.0000");
                    }
                    else if (linea.numpar > 0)
                    {
                        return "N° partida: " + linea.numpar;
                    }
                }
            }

            return null;
        }

        private void AgregarDescripcionALosDocumentos(IList<DocumentoOperacion> documentos)
        {
            foreach (DocumentoOperacion doc in documentos)
            {
                switch (doc.TipoDoc)
                {
                    case DocumentoOperacion.TipoDocEnum.Carta:
                        doc.DescripcionDoc = T_Mdl_Funciones_Varias.GetTipoCartaDesc(short.Parse(doc.CodigoPropio));
                        break;

                    case DocumentoOperacion.TipoDocEnum.Contabilidad:
                        doc.DescripcionDoc = doc.NroRpt + "-" + doc.FechaOperacion.ToString("dd/MM/yyyy");
                        break;

                    case DocumentoOperacion.TipoDocEnum.Swift:
                        doc.DescripcionDoc = "MT-" + doc.TipoSwift.ToString();
                        break;

                }

                doc.DescripcionProducto = T_Mdl_Funciones_Varias.GetProductoDesc(doc.CodPro);
            }
        }

        #endregion

        #region TICKET GRABAR
        public void TICKGR_Form_Load(InitializationObject Modulos)
        {
            Frm_Ticket_Logic.Form_Load(Modulos, uow);
        }
        public void TICKGR_Aceptar_Click(InitializationObject Modulos)
        {
            Frm_Ticket_Logic.Aceptar_Click(Modulos, uow);
        }

        public bool ValidarCheckTicker(InitializationObject Modulos)
        {
            return Frm_Ticket_Logic.ValidarCheckTicker(Modulos, uow);
        }
        public void TICKGR_Cancelar_Click(InitializationObject Modulos)
        {
            Frm_Ticket_Logic.Cancelar_Click(Modulos);
        }
        public void TICKGR_Cb_Ticket_Click(InitializationObject Modulos)
        {
            Frm_Ticket_Logic.Cb_ticket_Click(Modulos);
        }
        public void TICKGR_Otro_Click(InitializationObject Modulos)
        {
            Frm_Ticket_Logic.otro_Click(Modulos);
        }
        #endregion

        #region GRABAR
        public void Grabar1(InitializationObject initObject)
        {
            using (Tracer tracer = new Tracer())
            {
                //valido inicio y fin de dia
                MODGUSR.SyGetf_Usr(initObject.MODGUSR, initObject.Mdi_Principal, uow,
                    VB6Helpers.Left(initObject.Usuario.Identificacion_CCtUsr, 3),
                    VB6Helpers.Right(initObject.Usuario.Identificacion_CCtUsr, 2), "I");

                //-1 entra a ticket, 1 va a grabar3, 0 hubo error
                short resG1 = mdi_PrincipalLogic.Grabar1(initObject, uow, uowSwift);

                tracer.TraceVerbose("Retorno grabra1: {0}", resG1);

                if (resG1 == -1)
                {
                    //si entra aca, entonces tengo que mostrar los tickets
                    initObject.oriLoop = 0;
                    initObject.viaLoop = 0;
                    initObject.SyPutn_Adc_Str = String.Empty;
                    initObject.FormularioQueAbrir = "Grabar2";
                    tracer.AddToContext("Acción Guardar", "Grabar2");
                }
                else if (resG1 == 1)
                {
                    tracer.AddToContext("Acción Guardar", "Grabar3");
                    initObject.FormularioQueAbrir = "Grabar3";
                }
                else
                {
                    tracer.AddToContext("Acción Guardar", "Volvio al Index");
                    initObject.FormularioQueAbrir = "Index";
                }
            }
        }
        //Se modifica para devolver resultado correcto o incorrecto de gragado.
        //public void Grabar(InitializationObject initObject)
        public bool Grabar(InitializationObject initObject)
        {

            using (Tracer tracer = new Tracer())
            {
                try
                {
                    var Resp = mdi_PrincipalLogic.Grabar(initObject, uow, uowSwift);
                    if (Resp == true && !initObject.Mdi_Principal.MantenerMensajes)
                    {
                        initObject.refrescarSesion = true;
                    }
                    return true;    // Resp;    //Se agrega return a original para saber si todo bien o error
                }
                catch (Exception ex)
                {
                     tracer.TraceException("Metodo Grabar - FundTransferServices", ex);
                    if (ExceptionPolicy.HandleException(ex, "PoliticaBLFundTransfer")) throw;
                    return false;   //Se agrega return a original para saber si hubo error
                }
            }
        }

        public bool IrATicket(InitializationObject initObject, string no, string refe, string usu, short id)
        {
            using (Tracer tracer = new Tracer("IrATicket"))
            {
                T_MODXVIA MODXVIA = initObject.MODXVIA;
                T_MODXORI MODXORI = initObject.MODXORI;
                T_Module1 Module1 = initObject.Module1;
                T_MODGUSR MODGUSR = initObject.MODGUSR;

                int i;

                if (initObject.viaLoop <= VB6Helpers.UBound(MODXVIA.VxVia))
                {
                    i = initObject.viaLoop;
                    initObject.viaLoop++;

                    if (((MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteMN) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteME) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteAUTN) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteAUTE) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteMANN) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_CtaCteMANE) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_GAPMN) || (MODXVIA.VxVia[i].NumCta == T_MODGCON0.IdCta_GAPME)) && (MODXVIA.VxVia[i].Status != T_MODXVIA.ExVia_Eli))
                    {
                        tracer.TraceInformation(string.Format("Ticket Destino opt: {0}{1}{2}{3}{4}", Module1.Codop.Cent_Costo, Module1.Codop.Id_Product, Module1.Codop.Id_Especia, Module1.Codop.Id_Empresa, Module1.Codop.Id_Operacion));

                        MODXVIA.Strtic.Nomtic = Module1.PartysOpe[MODXVIA.VxVia[i].PosPrty].NombreUsado;
                        MODXVIA.Strtic.Nemtic = MODXVIA.VxVia[i].NemMon;
                        MODXVIA.Strtic.Montic = Utils.Format.FormatCurrency((MODXVIA.VxVia[i].MtoTot), T_MODGCON0.FormatoConDec);
                        MODXVIA.Strtic.Cuetic = MODXVIA.VxVia[i].CtaCte_t;
                        MODXVIA.Strtic.Dehtic = 1;

                        initObject.Frm_Ticket = new UI_Frm_Ticket(no, refe, usu, id);
                        initObject.Frm_Ticket.esOri = false;
                        // verifica si esta marcado "demas cheques"
                        if (initObject.MODXVIA.Strtic.Demtci)
                        {
                            tracer.TraceInformation("Marca demas check");
                            ValidarCheckTicker(initObject);
                            return false;
                        }
                        return true;
                    }
                }
                else if (initObject.oriLoop <= VB6Helpers.UBound(MODXORI.VxOri))
                {
                    i = initObject.oriLoop;
                    initObject.oriLoop++;

                    if (((MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMN) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteME) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteAUTN) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteAUTE) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMANN) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_CtaCteMANE) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_GAPMN) || (MODXORI.VxOri[i].NumCta == T_MODGCON0.IdCta_GAPME)) && (MODXORI.VxOri[i].Status != T_MODXVIA.ExVia_Eli))
                    {
                        tracer.TraceInformation(string.Format("Ticket Origen opt: {0}{1}{2}{3}{4}", Module1.Codop.Cent_Costo, Module1.Codop.Id_Product, Module1.Codop.Id_Especia, Module1.Codop.Id_Empresa, Module1.Codop.Id_Operacion));
                        MODXVIA.Strtic.Nomtic = Module1.PartysOpe[MODXORI.VxOri[i].PosPrty].NombreUsado;
                        MODXVIA.Strtic.Nemtic = MODXORI.VxOri[i].NemMon;
                        MODXVIA.Strtic.Montic = Utils.Format.FormatCurrency((MODXORI.VxOri[i].MtoTot), T_MODGCON0.FormatoConDec);
                        MODXVIA.Strtic.Cuetic = MODXORI.VxOri[i].CtaCte_t;
                        MODXVIA.Strtic.Dehtic = 0;

                        initObject.Frm_Ticket = new UI_Frm_Ticket(no, refe, usu, id);
                        initObject.Frm_Ticket.esOri = true;
                        // verifica si esta marcado "demas cheques"
                        if (!initObject.MODXVIA.Strtic.Demtci)
                        {
                            if (Module1.Codop.Id_Product == T_MODGUSR.IdPro_CreExp)
                            {
                                MODXVIA.Strtic.Glosa = Modulos.MODXVIA.Fn_GetGlosa(initObject, MODXORI.VxOri[i].PosOri);
                            }
                            else
                            {
                                MODXVIA.Strtic.Glosa = string.Empty;
                            }
                        }
                        else
                        {
                            tracer.TraceInformation("Marca demas check");
                            ValidarCheckTicker(initObject);
                            return false;
                        }
                        return true;
                    }
                }
                else
                {
                    initObject.FormularioQueAbrir = "Grabar3";
                }
                return false;
            }
        }

        public void DecidirSiGrabar(InitializationObject initObject)
        {
            var finDeOri = initObject.viaLoop > VB6Helpers.UBound(initObject.MODXVIA.VxVia);
            var finDeVia = initObject.oriLoop > VB6Helpers.UBound(initObject.MODXORI.VxOri);
            string Usr = initObject.MODGUSR.UsrEsp.CentroCosto + initObject.MODGUSR.UsrEsp.Especialista;
            if (!(finDeVia && finDeOri))
            {
                if (IrATicket(initObject, initObject.MODGCVD.VgCvd.OpeSin, initObject.MODGCVD.VgCvd.RefCli, Usr, initObject.MODXORI.VgxOri.ImpDeb))
                {
                    initObject.FormularioQueAbrir = "Ticket";
                }
                else
                {
                    initObject.FormularioQueAbrir = "Grabar2";
                }
            }
            else
            {
                initObject.FormularioQueAbrir = "Grabar3";
            }

        }
        #endregion

        #region Planilla Invisible Editar

        public void PlanillaInvisibleEditarInit(InitializationObject InitObj)
        {
            InitObj.Frm_Pln_Invisible = new UI_Frm_Pln_Invisible();
            Frm_Pln_Invisible.Form_Activate(InitObj, uow);
            Frm_Pln_Invisible.Form_Load(InitObj, uow);
        }

        public void PlanillaInvisibleEditar_AdelanteClick(InitializationObject InitObj)
        {
            Frm_Pln_Invisible.adelante_Click(InitObj, uow);
        }

        public void PlanillaInvisibleEditar_AtrasClick(InitializationObject InitObj)
        {
            Frm_Pln_Invisible.atras_Click(InitObj, uow);
        }

        public void PlanillaInvisibleEditar_GenerarClick(InitializationObject InitObj)
        {
            //Frm_Pln_Invisible.Generar_Click(InitObj, uow);
        }

        public void PlanillaInvisibleEditar_AceptarClick(InitializationObject InitObj)
        {
            Frm_Pln_Invisible.aceptar_Click(InitObj, uow);
        }

        #endregion

        #region "Eventos Planilla Ingreso Visible Export"

        public void PlanillaIngresoVisibleExportInit(InitializationObject initObject)
        {
            initObject.Frm_Pln_VisExp = new UI_Frm_Pln_VisExp();
            Frm_Pln_VisExp.Form_Load(initObject, uow);
            Frm_Pln_VisExp.Form_Activate(initObject, uow);
        }


        public void PlanillaIngresoVisibleExport_Retroceder(InitializationObject initObject)
        {
            Frm_Pln_VisExp.BotonRetroceder_Click(initObject, uow);

            //mdi_PrincipalLogic.Participantes2_2(initObject, uow);
        }

        public void PlanillaIngresoVisibleExport_Avanzar(InitializationObject initObject)
        {
            Frm_Pln_VisExp.BotonAvanzar_Click(initObject, uow);

            //mdi_PrincipalLogic.Participantes2_2(initObject, uow);
        }

        public void PlanillaIngresoVisibleExport_Modificar(InitializationObject initObject)
        {
            Frm_Pln_VisExp.BotonModificar_Click(initObject, uow);
        }
        public void PlanillaIngresoVisibleExport_Aceptar(InitializationObject initObject)
        {
            Frm_Pln_VisExp.BotonAceptar_Click(initObject, uow);
        }

        public void PlanillaIngresoVisibleExport_Cancelar(InitializationObject initObject)
        {
            //Frm_Participantes.Cancelar_Click(initObject);

            //mdi_PrincipalLogic.Participantes2_2(initObject, uow);
        }



        #endregion

        #region MODULO REVERSAR OPERACION EXPORT
        public void ReversarOperacionExportInit(InitializationObject initObject)
        {
            //valido inicio y fin de dia
            MODGUSR.SyGetf_Usr(initObject.MODGUSR, initObject.Mdi_Principal, uow,
                VB6Helpers.Left(initObject.Usuario.Identificacion_CCtUsr, 3),
                VB6Helpers.Right(initObject.Usuario.Identificacion_CCtUsr, 2), "I");

            mdi_PrincipalLogic.Opciones_Click_ReversarOperacionExport(initObject, uow);
            if (initObject.frmgrev != null)
            {
                Frmgrev_Logic.Form_Load(initObject, uow, initObject.frmgrev);
            }
        }

        public void ReversarOperacion_CobraComis(InitializationObject initObject)
        {
            mdi_PrincipalLogic.HabilitarOrigenDestinoFondos(initObject);
            initObject.FormularioQueAbrir = "Index";
        }

        public void ReversarOperacionExport_Ok_Operacion_Click(InitializationObject initObject)
        {
            Frmgrev_Logic.Ok_Operacion_Click(initObject, uow);
        }

        public void ReversarOperacionExport_Bot_Dec_Click(InitializationObject initObject)
        {
            Frmgrev_Logic.Bot_Dec_Click(initObject);
        }

        public void ReversarOperacionExport_BOT_Obs_Click(InitializationObject initObject)
        {
            Frmgrev_Logic.BOT_Obs_Click(initObject);
        }

        public bool ReversarOperacionExport_Co_Boton_Click(InitializationObject initObject, short index)
        {
            bool correcto = Frmgrev_Logic.Co_Boton_Click(ref index, initObject.MODXORI, initObject, uow, initObject.frmgrev);
            if (correcto)
            {
                mdi_PrincipalLogic.Opciones_Click_ReversarOperacionExport(initObject, uow);
            }
            return correcto;
        }

        public void ReversarOperacionExport_Co_Volver_Click(InitializationObject initObject)
        {
            Frmgrev_Logic.Co_Volver_Click(initObject.frmgrev, initObject);
        }

        public void ReversarOperacionExport_Lt_Pln_Click(InitializationObject initObject)
        {
            Frmgrev_Logic.Lt_Pln_Click(initObject.Mdi_Principal, initObject.MODGANU, initObject.frmgrev);
        }

        public bool ReversarOperacionExport_ok_Click_1(InitializationObject initObject, bool vieneDeMensaje, bool resMensaje)
        {
            #region Lógica
            bool correcto = Frmgrev_Logic.ok_Click_1(initObject.Mdi_Principal, initObject.MODGANU, initObject.frmgrev, initObject, uow, vieneDeMensaje, resMensaje);
            if (correcto && initObject.frmgrev.PopUps.Count > 0)
            {
                correcto = false;
                initObject.frmgrev.PopUps = new List<UI_Message>();
                initObject.frmgrev.Errores = new List<UI_Message>();
            }
            #endregion
            return correcto;
        }

        public void ReversarOperacionExport_CAM_TipCam_KeyPress(InitializationObject initObject, short keyAcii)
        {
            Frmgrev_Logic.CAM_TipCam_KeyPress(ref keyAcii, initObject);
        }

        public void ReversarOperacionExport_CAM_Tipcam_Blur(InitializationObject initObject)
        {
            Frmgrev_Logic.CAM_Tipcam_LostFocus(initObject);
        }

        public void ReversarOperacionExport_Tx_Fecha_Blur(InitializationObject initObject)
        {
            Frmgrev_Logic.Tx_Fecha_LostFocus(initObject.Mdi_Principal, initObject.frmgrev, initObject, initObject.MODCVDIMMM);
        }

        public void ReversarOperacionExport_Tx_NroOpe_Blur(InitializationObject initObject, short index)
        {
            Frmgrev_Logic.Tx_NroOpe_LostFocus(ref index, initObject.frmgrev, initObject);
        }

        public void ReversarOperacionExport_Pr_TabStop(InitializationObject initObject)
        {
            string planilla = "";
            Frmgrev_Logic.Pr_TabStop(planilla, initObject);
        }

        public void ReversarOperacionExport_Pr_Habilitar_Pantalla(InitializationObject initObject, short estado)
        {
            Frmgrev_Logic.Pr_Habilitar_Pantalla(estado, initObject.frmgrev);
        }

        public void ReversarOperacionExport_Tx_TipCam_Blur(InitializationObject initObject)
        {
            Frmgrev_Logic.Tx_TipCam_LostFocus(initObject.frmgrev, initObject);
        }
        #endregion

        #region Modulo Reversa Operacion Export Modal:
        public void ReversarOperacionExportDeclaracion_Acepta_Click(InitializationObject initObj)
        {
            FrmGrev_Declaracion_Logic.CAM_Acepta_Click(initObj);
        }
        public void ReversarOperacionExportDeclaracion_Form_Load(InitializationObject initObj)
        {
            FrmGrev_Declaracion_Logic.Form_Load(initObj);
        }
        public void Tx_CAM_Clausula_Blur(InitializationObject initObject)
        {
            FrmGrev_Declaracion_Logic.CAM_Clausula_LostFocus(initObject);
        }
        public void Tx_CAM_Comisiones_Blur(InitializationObject initObject, short index)
        {
            FrmGrev_Declaracion_Logic.CAM_Comisiones_LostFocus(initObject);
        }
        public void Tx_CAM_Liquido_Blur(InitializationObject initObject)
        {
            FrmGrev_Declaracion_Logic.CAM_Liquido_LostFocus(initObject);
        }
        public void Tx_CAM_Otros_Blur(InitializationObject initObject)
        {
            FrmGrev_Declaracion_Logic.CAM_Otros_LostFocus(initObject);
        }
        #endregion

        #region ANULACION PLANILLA VISIBLE IMPORT
        public void AnulacionPlanillaVisibleImportInit(InitializationObject InitObj)
        {
            //valido inicio y fin de dia
            MODGUSR.SyGetf_Usr(InitObj.MODGUSR, InitObj.Mdi_Principal, uow,
                VB6Helpers.Left(InitObj.Usuario.Identificacion_CCtUsr, 3),
                VB6Helpers.Right(InitObj.Usuario.Identificacion_CCtUsr, 2), "I");

            if (mdi_PrincipalLogic.Opciones_Click_AnulacionPlanillaVisibleImport_1_2(InitObj, uow))
            {
                InitObj.Frm_Anu_Vi = new UI_Frm_Anu_Vi();
                Frm_Anu_Vi.Form_Load(InitObj, uow);
            }
        }

        public void AnulacionPlanillaVisibleImport_OkClick(InitializationObject InitObj)
        {
            Frm_Anu_Vi.Bot_Ok_Click(InitObj, uow);
        }

        public void AnulacionPlanillaVisibleImport_AceptarClick(InitializationObject InitObj)
        {
            Frm_Anu_Vi.Bot_Aceptar_Click(InitObj, uow);
            mdi_PrincipalLogic.Opciones_Click_AnulacionPlanillaVisibleImport_2_2(InitObj, uow);
        }

        public void AnulacionPlanillaVisibleImport_CancelarClick(InitializationObject InitObj)
        {
            Frm_Anu_Vi.Bot_Cancel_Click(InitObj);
        }

        public void AnulacionPlanillaVisibleImport_Tx_FechaPre_Blur(InitializationObject initObject)
        {
            Frm_Anu_Vi.Tx_FecPre_LostFocus(initObject.Mdi_Principal, initObject.Frm_Anu_Vi, initObject, uow);
        }

        public void AnulacionPlanillaVisibleImport_Tx_FechaPre_Keypress(InitializationObject initObject, short KeyAscii, string text)
        {
            Frm_Anu_Vi.Tx_FecPre_KeyPress(ref KeyAscii, initObject.Frm_Anu_Vi, initObject, uow, text);
        }

        public void AnulacionPlanillaVisibleImport_Tx_NroOpe_Blur(InitializationObject initObject, short index)
        {
            Frm_Anu_Vi.Tx_NroOpe_LostFocus(ref index, initObject.Frm_Anu_Vi, initObject);
        }

        public void ReemAnulacionPlanillaVisibleImportInit(InitializationObject InitObj)
        {
            InitObj.Frm_Rem_PVI = new UI_Frm_Rem_PVI();
            Frm_Rem_PVI.Form_Load(InitObj, uow);
        }

        public void AnulacionPlanillaVisibleImport_LtClick(InitializationObject InitObj)
        {
            Frm_Anu_Vi.Lt_PlAnul_Click(InitObj, uow);
        }

        public void ReemAnulacionPlanillaVisibleImport_AceptarClick(InitializationObject InitObj)
        {
            Frm_Rem_PVI.Bot_Acepta_Click(InitObj);
        }

        public void ReemAnulacionPlanillaVisibleImport_CancelClick(InitializationObject InitObj)
        {
            Frm_Rem_PVI.Bot_Cancel_Click(InitObj);
        }

        public void ReemAnulacionPlanillaVisibleImport_OkDecClick(InitializationObject InitObj)
        {
            Frm_Rem_PVI.Bot_OkDec_Click(InitObj, this.uow);
        }

        public void ReemAnulacionPlanillaVisibleImport_NoFinalClick(InitializationObject InitObj)
        {
            Frm_Rem_PVI.Bot_NoFinal_Click(InitObj);
        }

        public void ReemAnulacionPlanillaVisibleImport_OkFinalClick(InitializationObject InitObj)
        {
            Frm_Rem_PVI.Bot_OkFinal_Click(InitObj, this.uow);
        }


        #endregion

        #region "COMISIONES"

        public void ComisionesInit(InitializationObject initObject, bool vieneDeMensaje, bool resMsg)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            T_Module1 Module1 = initObject.Module1;
            T_MODXORI MODXORI = initObject.MODXORI;


            short HabiaOrigen = 0;

            if (MODGCVD.COMISION == true && String.IsNullOrEmpty(Module1.PartysOpe[0].LlaveArchivo))
            {
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message()
                {
                    Text = "Debe definir los Participantes",
                    Type = TipoMensaje.Error
                });
                initObject.FormularioQueAbrir = "Participantes";
                return;
            }

            //Si tiene Orígenes definidos =>
            if (VB6Helpers.UBound(MODXORI.VxOri) > 0)
            {
                HabiaOrigen = (short)(true ? -1 : 0);
                if (!vieneDeMensaje)
                {
                    initObject.FormularioQueAbrir = "DefinirNuevosOrigenes";
                    initObject.VieneDe = "Comisiones";
                    return;
                }
                else
                {
                    if (!resMsg)
                    {
                        initObject.FormularioQueAbrir = "Index";
                        return;
                    }
                }
            }

            BCH.Comex.Core.BL.XCFT.Modulos.MODXORI.Pr_Init_xOri(initObject.MODXORI);

            initObject.Frm_Comisiones = new UI_Frm_Comisiones();
            Frm_Comisiones.Form_Load(initObject, uow);
            //Frm_Comisiones.Form_Activate(initObject, uow);
        }

        public void Comisiones_Ok(InitializationObject initObject)
        {
            Frm_Comisiones.OK_Com_Click(initObject, uow);
            Frm_Comisiones.Tx_Com_LostFocus(initObject, uow, 2);
        }
        public void Comisiones_No(InitializationObject initObject)
        {
            Frm_Comisiones.NO_com_Click(initObject, uow);
            //  Frm_Comisiones.limpiarObjetos(initObject);
        }

        public void Comisiones_Cm_com(InitializationObject initObject)
        {
            Frm_Comisiones.Cm_com_Click(initObject, uow);
        }

        public void Comisiones_Finish(InitializationObject initObject)
        {
            T_MODGCVD MODGCVD = initObject.MODGCVD;
            //Habilita Origen.-
            if (BCH.Comex.Core.BL.XCFT.Modulos.MODGCVD.HayOri(initObject) != 0)
            {
                initObject.Mdi_Principal.BUTTONS["tbr_origfondos"].Enabled = true;
                MODGCVD.VgCvd.Etapa += "ORI";
                //Se setea el focus
                var foco = initObject.MODGCVD.COMISION ? "tbr_origfondos" : "tbr_dedfondos";
                initObject.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { initObject.Mdi_Principal.BUTTONS[key].Focus = (key == foco ? true : false); });
            }
            else
            {
                initObject.Mdi_Principal.BUTTONS["tbr_origfondos"].Enabled = false;
                MODGCVD.VgCvd.Etapa = MODGPYF0.Componer(MODGCVD.VgCvd.Etapa, "ORI", "");
            }
            initObject.FormularioQueAbrir = "Index";
        }


        public void Comisiones_Ls_Com_Click(InitializationObject initObject)
        {
            Frm_Comisiones.Ls_com_Click(initObject, uow);
        }

        public void Comisiones_Tx_Com_Text_Blur(InitializationObject initObject, short index)
        {
            Frm_Comisiones.Tx_Com_LostFocus(initObject, uow, index);
        }
        #endregion

        #region "Anulacion Operaciones"

        public void AnulacionOperacionInit(InitializationObject initObject)
        {
            //valido inicio y fin de dia
            MODGUSR.SyGetf_Usr(initObject.MODGUSR, initObject.Mdi_Principal, uow,
                VB6Helpers.Left(initObject.Usuario.Identificacion_CCtUsr, 3),
                VB6Helpers.Right(initObject.Usuario.Identificacion_CCtUsr, 2), "I");

            if (mdi_PrincipalLogic.Opciones_Click_AnulacionOperacion_1_2(initObject, uow))
            {
                initObject.Frmganu = new UI_frmganu();
                frmganu.Form_Load(initObject);
            }
        }

        public void AnulacionOperacion_Aceptar(InitializationObject initObject)
        {
            frmganu.Co_Boton_Click(initObject, uow, uowSwift);
            mdi_PrincipalLogic.Opciones_Click_AnulacionOperacion_2_2(initObject, uow);
        }

        public void AnulacionOperacion_Cancelar(InitializationObject initObject)
        {
            mdi_PrincipalLogic.Opciones_Click_AnulacionOperacion_2_2(initObject, uow);
        }

        public void AnulacionOperacion_Cmd_ok(InitializationObject initObject)
        {
            frmganu.Cmd_Ok_Click(initObject, uow);
        }

        public void AnulacionOperacion_Tx_NroOpe_Blur(InitializationObject initObject, short index)
        {

            frmganu.Tx_NroOpe_LostFocus(initObject, index);
        }
        #endregion

        #region "Plantilla Anulacion"

        public void PlanillaAnulacionInit(InitializationObject initObject)
        {
            initObject.Frmxanu = new UI_FrmxAnu();
            FrmxAnu.Form_Load(initObject, uow);
            FrmxAnu.Form_Activate(initObject, uow);
            FrmxAnu.Tx_PlAnu_LostFocus(initObject, uow, 4);
        }
        public void PlanillaAnulacion_Retroceder(InitializationObject initObject)
        {
            FrmxAnu.BotonRetroceder_Click(initObject, uow);

            //mdi_PrincipalLogic.Participantes2_2(initObject, uow);
        }
        public void PlanillaAnulacion_Avanzar(InitializationObject initObject)
        {
            FrmxAnu.BotonAvanzar_Click(initObject, uow);

            //mdi_PrincipalLogic.Participantes2_2(initObject, uow);
        }
        public void PlanillaAnulacion_Tickear(InitializationObject initObject)
        {
            FrmxAnu.BotonTickear_Click(initObject, uow);
        }
        public void PlanillaAnulacion_Aceptar(InitializationObject initObject)
        {
            FrmxAnu.BotonAceptar_Click(initObject, uow);
        }
        public void PlanillaAnulacion_Cancelar(InitializationObject initObject)
        {
            //Frm_Participantes.Cancelar_Click(initObject);

            //mdi_PrincipalLogic.Participantes2_2(initObject, uow);
        }

        public void PlanillaAnulada_Blur(InitializationObject initObject, short index)
        {
            FrmxAnu.Tx_PlAnu_LostFocus(initObject, uow, index);
        }

        //public void EntidadAutorizadaPlanillaAnulada_Blur(InitializationObject initObject, short index)
        //{
        //    FrmxAnu.Tx_PlAnu_LostFocus(initObject, uow, index);
        //}

        //public void TipoOperacionAnulada_Blur(InitializationObject initObject, short index)
        //{
        //    FrmxAnu.Tx_PlAnu_LostFocus(initObject, uow, index);
        //}
        //public void PlazaBancoCentralAnulada_Blur(InitializationObject initObject, short index)
        //{
        //    FrmxAnu.Tx_PlAnu_LostFocus(initObject, uow, index);
        //}

        //public void PlazaBancoCentralAnulada_Blur(InitializationObject initObject, short index)
        //{
        //    FrmxAnu.Tx_PlAnu_LostFocus(initObject, uow, index);
        //}


        #endregion

        #region Generar Swift

        public bool InicializarGeneracionDeSwift(InitializationObject io)
        {
            if (io.MODXVIA.VgxVia.Acepto != 0)
            {
                string nroOperacion = MODGCHQ.Referencia(io);
                return Mdl_Funciones.InterfazSwf(io, uow, nroOperacion);
            }

            return false;
        }

        //traspaso alguna info mas a las estructuras esperadas por las siguientes pantallas
        public void FinalizarGeneracionDeSwift(InitializationObject io)
        {
            string fechaPagoHoy = DateTime.Now.ToString("dd/MM/yyyy");
            io.MODPREEM.FechaSwift = fechaPagoHoy;
            for (int i = 0; i < io.MODGSWF.VSwf.Length; i++)
            {
                string fechaPagoSwift = io.MODGSWF.VSwf[i].DatSwf.FecPag;
                if (fechaPagoSwift != fechaPagoHoy)
                {
                    io.MODPREEM.FechaSwift = fechaPagoSwift;
                    break;
                }
            }

            if (io.MODPREEM.FechaSwift != fechaPagoHoy)
            {
                for (int i = 0; i < io.MODPREEM.Vx_PReem.Length; i++)
                {
                    if (io.MODPREEM.Vx_PReem[i].IndAnu == 3)
                    {
                        //esto no tiene sentido, deberia iterar y asignar io.MODGSWF.VSwf[i].DatSwf.FecPag y no io.MODPREEM.FechaSwift;
                        io.MODPREEM.Vx_PReem[i].FecVen = io.MODPREEM.FechaSwift;
                    }
                }

                for (int i = 0; i < io.MODGPLI1.Vplis.Length; i++) //al parecer este arreglo empieza en 0 y no en 1
                {
                    short tipPln = io.MODGPLI1.Vplis[i].TipPln;
                    if (tipPln == T_Mdl_Funciones.TPli_TranIng || tipPln == T_Mdl_Funciones.TPli_TranEg || tipPln == T_Mdl_Funciones.TPli_AnuTranIng || tipPln == T_Mdl_Funciones.TPli_AnuTranEg)
                    {
                        io.MODGPLI1.Vplis[i].FecPli = io.MODPREEM.FechaSwift;
                        io.MODGPLI1.Vplis[i].Fecing = io.MODPREEM.FechaSwift;
                    }
                }
            }

            if (io.MODGSWF.VGSwf.Acepto)
            {
                io.MODGCVD.VgCvd.Etapa = MODGPYF0.Componer(io.MODGCVD.VgCvd.Etapa, "SWF", String.Empty);
                if (io.MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_VisImp)
                {
                    MODCVDIMMM.GenPlAlad(io, uow);
                    if (MODCVDIMMM.ValPlan(io) == 0)
                    {
                        io.MODGCVD.VgCvd.filler = 1;
                    }
                }
                //Verifica a donde debe estar el foco
                var foco = io.Mdi_Principal.BUTTONS["tbr_Gchq"].Enabled ? "tbr_Gchq" : "tbr_grabar";
                io.Mdi_Principal.BUTTONS.Keys.ToList().ForEach(key => { io.Mdi_Principal.BUTTONS[key].Focus = (key == foco ? true : false); });
            }
        }

        public void CargarConDummiesTodasLasEstructurasQueDeberianVenirCargadasDePantallasAnterioresAGenerarSwift(InitializationObject initObject)
        {
            MODGTAB0.SyGetn_Nom(initObject, uow);

            initObject.MODGSWF.VGSwf.NumOpe = "753305700017047";

            Array.Resize(ref initObject.MODGSWF.VSwf, 2);
            //al igual que la aplición legacy, dejo el 1er elemento del array vacío
            initObject.MODGSWF.VSwf[0] = new T_Swf()
            {
                mtoswf = 1500000.91,
                SwfMon = "USD",
                CodMon = 11,
                EsAladi = false,
                DatSwf = new T_DatSwf(),
                BenSwf = new T_BenSwf()
            };

            initObject.MODGSWF.VSwf[1] = new T_Swf()
            {
                mtoswf = 20000.21,
                SwfMon = "EUR",
                CodMon = 96,
                EsAladi = false,
                DatSwf = new T_DatSwf(),
                BenSwf = new T_BenSwf(),
                EstaGen = 0
            };

            initObject.MOD_50F.CHK_50F = true;

            if (initObject.MODGSWF.VCliSwf == null)
            {
                initObject.MODGSWF.VCliSwf = new T_CliSwf()
                {
                    NomCli = "Microsoft Chile S.a.",
                    DirCli1 = "Mariano Fontanecilla S 310 piso 6",
                    DirCli2 = "santiago",
                    PaiCli = "chile",
                    rutcli = "0966337605"
                };
            }

            if (initObject.MODGSWF.VBenSwf == null || initObject.MODGSWF.VBenSwf.Length == 0)
            {
                Array.Resize(ref initObject.MODGSWF.VBenSwf, 2);
                initObject.MODGSWF.VBenSwf[0] = new T_BenSwf()
                {
                    IndBen = 0,
                    FunBen = "Cliente",
                    NomBen = "microsoft chile",
                    DirBen1 = "mariano sanchez",
                    DirBen2 = "santiago",
                    PaiBen = 997,
                };
                initObject.MODGSWF.VBenSwf[1] = new T_BenSwf()
                {
                    IndBen = 1, //necesito esta propiedad, confirmar que vine cargada en el legacy
                    FunBen = "Beneficiario",
                    EsBanco = true
                };
            }

            if (initObject.MODGSWF.VMT103 == null || initObject.MODGSWF.VMT103.Length == 0)
            {
                Array.Resize(ref initObject.MODGSWF.VMT103, 2);
                initObject.MODGSWF.VMT103[0] = new T_mt103()
                {
                    MtoOri = 1500000,
                    MndOri = 11
                };
                initObject.MODGSWF.VMT103[1] = new T_mt103()
                {
                    MtoOri = 20000,
                    MndOri = 96
                };
            }
        }

        public IList<T_Pai> CargarPaisesClientes(InitializationObject initObject)
        {
            if (initObject.MODGTAB0.VPai == null || initObject.MODGTAB0.VPai.Length == 0)
            {
                MODGTAB0.SyGetn_Pai(initObject.MODGTAB0, uow);
            }
            else
            {
                //ya estan cargados los paises, no lo traigo de nuevo de BD
            }
            return initObject.MODGTAB0.VPai.ToList();
        }

        public IList<sce_cpai> CargarPaisesBancos()
        {
            return uow.SceRepository.sce_cpai_s01_MS();
        }

        public IList<T_Mnd> CargarMonedas(InitializationObject initObject)
        {
            if (initObject.MODGTAB0.VMnd == null || initObject.MODGTAB0.VMnd.Length == 0)
            {
                MODGTAB0.SyGetn_Mnd(initObject.MODGTAB0, uow);
                MODGTAB0.SyGetn_MndPai(initObject.MODGTAB0, uow);
            }

            return initObject.MODGTAB0.VMnd.ToList();
        }

        public IList<T_Cor> CargarCorresponsales(InitializationObject initObject)
        {
            MODGTAB0.SyGetn_Cor(initObject.MODGTAB0, uow);
            return initObject.MODGTAB0.VCor;
        }


        /// <summary>
        /// Funcion que trae los datos del banco y también el código del país al que pertenece, unifica las funciones SyGet_VBic y SyGet_Cou del legacy
        /// </summary>
        /// <param name="initObject"></param>
        /// <param name="swiftBanco"></param>
        /// <param name="secuencia"></param>
        /// <returns></returns>
        public T_Bic GetBancoPorSwift(InitializationObject initObject, string swiftBanco, string secuencia)
        {
            var result = Mdl_Funciones.GetBancoPaymentplus(swiftBanco, secuencia, uowSwift, initObject);
            if (result.Count >= 1)
            {
                T_Bic banco = result.First();
                initObject.Mdl_Funciones.VBic = banco;
                if (banco != null)
                {
                    int? couCod = uow.BancoRepository.sce_cou_s03_MS(banco.BicCod);
                    if (couCod.HasValue)
                    {
                        banco.CouCod = couCod.Value;
                    }
                }
                return banco;
            }
            else
            {
                return null;
            }
        }

        public IList<CodigoDeOrdenCampo23Swift> GetCodigosDeOrdenPosiblesCampo23()
        {
            List<CodigoDeOrdenCampo23Swift> lista = new List<CodigoDeOrdenCampo23Swift>();
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.BONL });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.CHQB });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.CORT });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.HOLD, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.INTC });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.PHOB, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.PHOI, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.PHON, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.SDVA });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.TELB, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.TELE, PermiteTextoAdicional = true });
            lista.Add(new CodigoDeOrdenCampo23Swift() { Codigo = CodigoDeOrdenCampo23Swift.CodigoOrden.TELI, PermiteTextoAdicional = true });

            return lista;
        }

        public IDictionary<string, IList<string>> GetReglasCodigosDeOrdenCampo23E()
        {
            Dictionary<string, IList<string>> reglas = new Dictionary<string, IList<string>>();

            //leo mi custom section del web.config
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            XmlElement node = doc.SelectSingleNode("/configuration/FundTransfer.ReglasCodigoDeOrden") as XmlElement;
            if (node != null)
            {
                foreach (XmlNode nodoRegla in node.ChildNodes)
                {
                    string valor1 = nodoRegla.Attributes["Valor1"].Value;
                    string valor2 = nodoRegla.Attributes["Valor2"].Value;

                    IList<string> reglasDeKey = null;
                    if (reglas.ContainsKey(valor1))
                    {
                        reglasDeKey = reglas[valor1];
                    }
                    else
                    {
                        reglasDeKey = new List<string>();
                        reglas.Add(valor1, reglasDeKey);
                    }

                    reglasDeKey.Add(valor2);
                }
            }

            return reglas;
        }

        public DateTime CalcularFechaInicialSwift(T_MODGTAB0 mod)
        {
            return Frm_Swf0.Pr_Fecha_Inicial(mod, uow);
        }

        public bool ValidarFechaPago(T_MODGTAB0 mod, DateTime fechaPago, out string mensajeError)
        {
            if (fechaPago.Date < DateTime.Now.Date)
            {
                mensajeError = "Fecha no puede ser menor a la fecha de hoy";
                return false;
            }
            else return Frm_Swf0.ValidarFechaPago(mod, uow, fechaPago, out mensajeError);
        }

        public bool ValidarCodComp(T_MODGSWF mod, string codComp, out string mensajeError)
        {
            return Frm_Swf0.ValidarCodComp(mod, uow, codComp, out mensajeError);
        }

        public IList<UI_Message> ValidarSwiftCompleto(InitializationObject initObj, T_Swf swiftNuevo, T_CliSwf cliente, T_mt103 montos, IList<LineaMensajeSwift> lineasManuales)
        {
            return Frm_Swf0.ValidarSwiftCompleto(initObj, uow, uowSwift, swiftNuevo, cliente, montos, lineasManuales);
        }

        public bool GenerarSwift(InitializationObject initObj, T_Swf swiftNuevo, T_CliSwf cliente, T_mt103 montos, IList<CodigoDeOrdenCampo23Swift> codigosOrden, IList<LineaMensajeSwift> lineasManuales, short indiceSwift)
        {
            return Frm_Swf0.GenerarSwift(initObj, uow, swiftNuevo, cliente, montos, codigosOrden, lineasManuales, indiceSwift);
        }

        public IList<LineaMensajeSwift> GetCamposManualesSwift(int codMt)
        {
            IList<LineaMensajeSwift> Resultado = new List<LineaMensajeSwift>();
            Resultado = uow.SceRepository.GetCamposManualesSwift(codMt);

            if (Resultado.Any(c => c.CodCam.Trim() == "72"))
            {
                List<Entities.Swift.sw_valor_campos> valoresCampos = uowSwift.ValorCamposRepository.LlenaMatrizValores("MT" + codMt);
                valoresCampos = valoresCampos.Where(c => c.linea_campo == 1 && c.tag_campo.Trim() == "72").ToList();
                if (valoresCampos.Count > 0)
                {
                    List<SelectListLine> fill = valoresCampos.SingleOrDefault().valor_campo.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(c => new SelectListLine { Text = c, Value = c }).ToList<SelectListLine>();
                    Resultado.Where(c => c.CodCam.Trim() == "72").SingleOrDefault().LineasSecundarias.FirstOrDefault().ValorCampo = fill;
                }
            }
            return Resultado;
        }

        public IList<SelectListLine> GetCodigosCampo72(string pais, string moneda, out bool condEspecialesPais)
        {
            List<SelectListLine> result = new List<SelectListLine>();
            List<ParametroComex> resultParams = uow.ParametroComexRepository.GetParametrosComex("CAMPO72", pais, moneda)
                .OrderBy(x => x.trans_nmb_agrupacion_3).ThenBy(x => x.trans_vlr_parametro).ToList();
            condEspecialesPais = resultParams.Exists(x => x.trans_nmb_agrupacion_3 != "*");

            foreach (var param in resultParams)
            {
                var linea = new SelectListLine
                {
                    Disabled = false,
                    Selected = false,
                    Text = string.IsNullOrWhiteSpace(param.trans_dsc_parametro) ? param.trans_vlr_parametro : param.trans_vlr_parametro + " - " + param.trans_dsc_parametro,
                    Value = param.trans_vlr_parametro
                };
                result.Add(linea);
            }
            return result;
        }

        #endregion

        #region Planilla Visible Import Editar
        public void PlanillaVisibleImportEditarInit(InitializationObject InitObj)
        {
            InitObj.Frm_Pln_cob = new UI_Frm_Pln_cob();

            Frm_Pln_cob.Form_Load(InitObj, uow);
        }

        public void PlanillaVisibleImportEditar_Anterior(InitializationObject InitObj)
        {
            Frm_Pln_cob.Bot_Ant_Click(InitObj, uow);
        }

        public void PlanillaVisibleImportEditar_Siguiente(InitializationObject InitObj)
        {
            Frm_Pln_cob.Bot_Sig_Click(InitObj, uow);
        }

        public void PlanillaVisibleImportEditar_Ok(InitializationObject InitObj)
        {
            Frm_Pln_cob.Bot_Okey_Click(InitObj, uow);
        }
        #endregion

        #region EmitirCheque

        public void EMITIRCHEQUE_Documentos(InitializationObject Modulos) { mdi_PrincipalLogic.Documentos(Modulos, uow); }
        public void EMITIRCHEQUE_Co_Aceptar_Click(InitializationObject Modulos) { Frm_Chq_Logic.Co_Aceptar_Click(Modulos); }
        public void EMITIRCHEQUE_Co_Cancelar_Click(InitializationObject Modulos, bool b1, bool b2) { Frm_Chq_Logic.Co_Cancelar_Click(Modulos, b1, b2); }
        public void EMITIRCHEQUE_Co_Generar_Click(InitializationObject Modulos) { Frm_Chq_Logic.Co_Generar_Click(Modulos); }
        //public void EMITIRCHEQUE_Form_Activate(InitializationObject Modulos) { Frm_Chq_Logic.Form_Activate(Modulos); }
        //public void EMITIRCHEQUE_Form_Load(InitializationObject Modulos) { Frm_Chq_Logic.Form_Load(Modulos, uow); }
        public void EMITIRCHEQUE_l_benef_Click(InitializationObject Modulos) { Frm_Chq_Logic.l_benef_Click(Modulos); }
        public void EMITIRCHEQUE_L_Montos_Click(InitializationObject Modulos) { Frm_Chq_Logic.L_Montos_Click(Modulos); }
        public void EMITIRCHEQUE_l_montos_DblClick(InitializationObject Modulos) { Frm_Chq_Logic.l_montos_DblClick(Modulos); }
        public void EMITIRCHEQUE_l_plaza_Click(InitializationObject Modulos) { Frm_Chq_Logic.l_plaza_Click(Modulos); }

        #endregion

        #region Emitir Nota Credito
        public void EmitirNotaCreditoInit(InitializationObject InitObj)
        {
            InitObj.Frmgnota = new UI_Frmgnota();
            InitObj.MODCVDIM.Gvar_NotaCredito = (short)1;
            Frmgnota.Nota(InitObj, uow);

            Frmgnota.Form_Load(InitObj, uow);
        }

        public void EmitirNotaCredito_Producto_Change(InitializationObject InitObj)
        {
            Frmgnota.Cb_Producto_Click(InitObj);
        }

        public void EmitirNotaCredito_Ok_Click(InitializationObject InitObj)
        {
            Frmgnota.ok_Click(InitObj, uow);
        }
        public void EmitirNotaCredito_RetornoFacturasAsociadas(InitializationObject InitObj)
        {
            Frmgnota.RetornoFacturasAsociadas(InitObj);
        }
        public void EmitirNotaCredito_Aceptar_Click(InitializationObject InitObj)
        {
            Frmgnota.Bot_acep_click(InitObj, uow);

            Frmgnota.Nota_Salida(InitObj);
        }

        public void EmitirNotaCredito_Cancelar_Click(InitializationObject InitObj)
        {
            Frmgnota.Bot_canc_click(InitObj);
        }

        public void EmitirNotaCredito_Tx_NroOpe_Blur(InitializationObject initObject, short index)
        {
            Frmgnota.Tx_NroOpe_LostFocus(ref index, initObject.Frmgnota, initObject);
        }
        #endregion

        #region Facturas Asociadas
        public void FacturasAsociadasInit(InitializationObject InitObj)
        {
            InitObj.FrmFact = new UI_FrmFact();

            FrmFact.Form_Load(InitObj, uow);
        }
        public void FacturasAsociadas_bot_acep_Click(InitializationObject InitObj)
        {
            FrmFact.bot_acep_Click(InitObj);
        }

        #endregion

        #region VUELTOS
        public void Vueltos(InitializationObject Modulos)
        {
            bool vuelve = !String.IsNullOrEmpty(Modulos.VieneDe);
            Modulos.VieneDe = String.Empty;
            mdi_PrincipalLogic.Vueltos(Modulos, uow, vuelve);
        }
        #endregion

        #region PLANILLAS DE TRANSFERENCIA
        public void PlanillasTransferenciaInit(InitializationObject initObj)
        {
            Frm_ChVrf.Form_Load(initObj, uow);
        }

        public void PlanillasTransferencia_AceptarClick(InitializationObject initObj)
        {
            Frm_ChVrf.Bt_Aceptar_Click(initObj);
        }

        public void PlanillasTransferencia_CancelarClick(InitializationObject initObj)
        {
            Frm_ChVrf.Bt_Cancelar_Click(initObj);
        }

        public void PlanillasTransferencia_EliminarClick(InitializationObject initObj)
        {
            Frm_ChVrf.Bt_Eliminar_Click(initObj);
        }

        public void PlanillasTransferencia_OKClick(InitializationObject initObj)
        {
            Frm_ChVrf.Bt_OK_Click(initObj);
        }

        public void PlanillasTransferencia_SeleccionarMoneda_Click(InitializationObject initObj)
        {
            Frm_ChVrf.Cbo_Moneda_Click(initObj, uow);
        }

        public void PlanillasTransferencia_ListaPlanillas_Click(InitializationObject initObj)
        {
            Frm_ChVrf.ListaPlanillas_Click(initObj);
        }

        public void PlanillasTransferencia_Tx_Monto_LostFocus_Click(InitializationObject initObj)
        {
            Frm_ChVrf.Tx_Monto_LostFocus(initObj);
        }
        #endregion

        #region NEMONICO DE CUENTAS
        public void NEMCTA_Inicializar(InitializationObject initObject)
        {
            initObject.Frm_Cta = new UI_Frm_Cta();
        }
        #endregion

        #region "Impresion Planillas"

        //"Impresion Planilla Invisible"
        public void ImpresionPlanillaInvisibleInit()
        {
        }
        //"Impresion Planilla Reemplazo"
        public void ImpresionPlanillaReemplazoInit()
        {
        }
        //"Impresion Planilla Visible Exportacion"
        public void ImpresionPlanillaVisibleExportacionInit()
        {
        }

        #endregion

        #region REPORTAR OPERACION
        public List<sce_xdoc_s05_MS_Result> GetCartasOperacion(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (var trace = new Tracer("FundTransferService - GetCartasOperacion"))
            {
                try
                {
                    return uow.SceRepository.Sce_xdoc_s05_MS(codcct, codpro, codesp, codofi, codope).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    return null;
                }
            }
        }

        public List<sce_mch_s16_MS_Result> GetCabeceraContabilidad(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (var trace = new Tracer("FundTransferService - GetCabeceraContabilidad"))
            {
                try
                {
                    return uow.SceRepository.Sce_mch_s16_MS(codcct, codpro, codesp, codofi, codope).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    return null;
                }
            }
        }

        public List<sce_mcd_s78_MS_Result> GetDetalleContabilidad(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (var trace = new Tracer("FundTransferService - GetDetalleContabilidad"))
            {
                try
                {
                    return uow.SceRepository.Sce_mcd_s78_MS(codcct, codpro, codesp, codofi, codope).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    return null;
                }
            }
        }

        public List<sce_plan_s19_MS_Result> GetPlanillasImportacion(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (var trace = new Tracer("FundTransferService - GetPlanillasImportacion"))
            {
                try
                {
                    return uow.SceRepository.Sce_plan_s19_MS(codcct, codpro, codesp, codofi, codope).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    return null;
                }
            }
        }
        public List<sce_pli_s08_MS_Result> GetPlanillasInvisibles(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (var trace = new Tracer("FundTransferService - GetPlanillasInvisibles"))
            {
                try
                {
                    return uow.SceRepository.Sce_pli_s08_MS(codcct, codpro, codesp, codofi, codope).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    return null;
                }
            }
        }

        public List<sce_xanu_s04_MS_Result> GetPlanillasAnulacion(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (var trace = new Tracer("FundTransferService - GetPlanillasAnulacion"))
            {
                try
                {
                    return uow.SceRepository.Sce_xanu_s04_MS(codcct, codpro, codesp, codofi, codope).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    return null;
                }
            }
        }

        public List<sce_xplv_s12_MS_Result> GetPlanillasExportacion(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (var trace = new Tracer("FundTransferService - GetPlanillasExportacion"))
            {
                try
                {
                    return uow.SceRepository.Sce_xplv_s12_MS(codcct, codpro, codesp, codofi, codope).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    return null;
                }
            }
        }
        public List<sce_prty_s10_MS_Result> GetDatosParticipante(string idparty, string rut)
        {
            using (var trace = new Tracer("FundTransferService - GetDatosParticipante"))
            {
                try
                {
                    return uow.SceRepository.Sce_prty_s10_MS(idparty, rut).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    return null;
                }
            }
        }

        public List<sce_rsa_s07_MS_Result> GetRazonSocialParticipante(string idparty)
        {
            using (var trace = new Tracer("FundTransferService - GetRazonSocialParticipante"))
            {
                try
                {
                    return uow.SceRepository.Sce_Rsa_S07_MS(idparty).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    return null;
                }
            }
        }

        public List<sce_dad_s08_MS_Result> GetDireccionParticipante(string idparty)
        {
            using (var trace = new Tracer("FundTransferService - GetDireccionParticipante"))
            {
                try
                {
                    return uow.SceRepository.Sce_Dad_S08_MS(idparty).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    return null;
                }
            }
        }

        public List<tbl_datos_usuario_s01_MS_Result> GetDatosUsuario(string cencos, string codusr)
        {
            using (var trace = new Tracer("FundTransferService - GetDatosUsuario"))
            {
                try
                {
                    return uow.SceRepository.Tbl_datos_usuario_s01_MS(cencos, codusr).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    return null;
                }
            }
        }

        public MemoryStream GenerarArchivoExcelOperacionReportada(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            List<sce_prty_s10_MS_Result> datosParticipante = null;
            List<sce_rsa_s07_MS_Result> razonSocialParticipante = null;
            List<sce_dad_s08_MS_Result> direccionParticipante = null;
            List<tbl_datos_usuario_s01_MS_Result> datosUsuario = null;

            var cartas               = GetCartasOperacion(codcct, codpro, codesp, codofi, codope);
            var cabeceraContabilidad = GetCabeceraContabilidad(codcct, codpro, codesp, codofi, codope);
            var detalleContabilidad  = GetDetalleContabilidad(codcct, codpro, codesp, codofi, codope);
            var planillasImportacion = GetPlanillasImportacion(codcct, codpro, codesp, codofi, codope);
            var planillasInvisibles  = GetPlanillasInvisibles(codcct, codpro, codesp, codofi, codope);
            var planillasAnulacion   = GetPlanillasAnulacion(codcct, codpro, codesp, codofi, codope);
            var planillasExportacion = GetPlanillasExportacion(codcct, codpro, codesp, codofi, codope);

            foreach (var item in cabeceraContabilidad)
            {
                datosParticipante = GetDatosParticipante(item.prtcli, item.rutcli);
                razonSocialParticipante = GetRazonSocialParticipante(item.prtcli);
                direccionParticipante = GetDireccionParticipante(item.prtcli);

                datosUsuario = GetDatosUsuario(item.cencos, item.codusr);
            }

            MemoryStream archivoExcelGenerado = ExcelDataExporter.GenerarArchivoReporteOperacion(cartas, cabeceraContabilidad, detalleContabilidad, planillasImportacion, planillasInvisibles, planillasAnulacion, planillasExportacion, datosParticipante, razonSocialParticipante, direccionParticipante, datosUsuario);

            return archivoExcelGenerado;
        }
        #endregion
    }
}
