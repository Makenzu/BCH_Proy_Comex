using BCH.Comex.Core.BL.XGPY.Forms;
using BCH.Comex.Core.BL.XGPY.Modulos;
using BCH.Comex.Core.Entities.Cext01;
//using BCH.Comex.Common.XGPY.Datatypes;
//using BCH.Comex.Common.XGPY.T_Modulos;
//using BCH.Comex.Common.XGPY.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.Datatypes;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.AdminParticipantes.UI_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGPY
{
    public class XgpyService
    {
        private UnitOfWorkCext01 unitOfWork;

        public XgpyService()
        {
            this.unitOfWork = new UnitOfWorkCext01();
        }

        public String ConsultaRazonSocialPorRut(String rutPrty)
        {
            string mensaje = string.Empty;
            return ConsultaRazonSocialPorRut(rutPrty, ref mensaje);
        }

        public String ConsultaRazonSocialPorRut(String rutPrty, ref string mensaje)
        {
            return XGPYServices.ConsultaRazonSocialPorRut(rutPrty, ref mensaje);
        }

        public String ConsultaOficinaPorRut(String rutPrty)
        {
            return XGPYServices.ConsultaOficinaPorRut(rutPrty);
        }

        public List<string> ConsultarDireccionPorRut(string rutPrty)
        {
            return XGPYServices.ConsultarDireccionPorRut(rutPrty);
        }

        public T_PRTGLOB ObtenerDatosWebService(string rutPrty)
        {
            return XGPYServices.ObtenerDatosWebService(rutPrty);
        }

        public IList<Data.DAL.Services.XCFT.ConsultaProductoCliente.producto> ObtenerCuentasPorRut(String rutPrty)
        {
            string mensaje = string.Empty;
            return ObtenerCuentasPorRut(rutPrty, ref mensaje);
        }

        public IList<Data.DAL.Services.XCFT.ConsultaProductoCliente.producto> ObtenerCuentasPorRut(String rutPrty, ref string mensaje)
        {
            return XGPYServices.ObtenerCuentasPorRut(rutPrty, ref mensaje);
        }

        public IList<sce_ini_s01_MS_Result> SyGetIni(string grupo, string eleme)
        {
            return unitOfWork.SceRepository.sce_ini_s01_MS(grupo, eleme);
        }

        public IList<sce_rsa_s06_MS_Result> Sce_Rsa_S06_MS(int id_nombre, int crea_costo, int crea_user)
        {
            return unitOfWork.SceRepository.Sce_Rsa_S06_MS(id_nombre, crea_costo, crea_user);
        }

        public IList<sce_rsa_i01_MS_Result> Sce_Rsa_I01_MS(string idParty, int idNombre, string razonSocial, string nombreFantasia, string contacto, string sortKey, string creaCosto, string creaUser)
        {
            return unitOfWork.SceRepository.Sce_Rsa_I01_MS(idParty, idNombre, razonSocial, nombreFantasia, contacto, sortKey, creaCosto, creaUser);
        }

        public IList<sce_rsa_parti_listRazon_MS_Result> GetLstRazon(string sRazon)
        {
            return unitOfWork.SceRepository.Sce_Rsa_Parti_ListRazon_MS(sRazon);
        }

        public IList<pro_sce_prty_s07_MS_Result> Pro_Sce_Prty_S07_MS(String searchRazonSocial)
        {
            return unitOfWork.SceRepository.Pro_Sce_Prty_S07_MS(searchRazonSocial);
        }

        public IList<sce_rsa_u01_MS_Result> Sce_Rsa_U01_MS(string idParty, int idNombre, int borrado, string razonSocial, string nombreFantasia, string contacto, string sortKey)
        {
            return unitOfWork.SceRepository.Sce_Rsa_U01_MS(idParty, idNombre, borrado, razonSocial, nombreFantasia, contacto, sortKey);
        }

        public IList<sce_rsa_parti_listDir_MS_Result> GetLstDir(string sRazon)
        {
            return unitOfWork.SceRepository.Sce_Rsa_Parti_ListDir_MS(sRazon);
        }

        public IList<LstAcEco_MS_Result> LstAcEco()
        {
            return unitOfWork.SceRepository.LstAcEco_MS();
        }

        public IList<LstRiesgo_MS_Result> LstRiesgo()
        {
            return unitOfWork.SceRepository.LstRiesgo_MS();
        }

        public IList<LstEjec_MS_Result> LstEjec()
        {
            return unitOfWork.SceRepository.LstEjec_MS();
        }

        public IList<sce_rsa_s07_MS_Result> Sce_Rsa_S07_MS(String idParty)
        {
            return unitOfWork.SceRepository.Sce_Rsa_S07_MS(idParty);
        }

        /// <summary>
        /// Participante
        /// </summary>
        /// <param name="searchKeyIdParty"></param>
        /// <returns></returns>
        public sce_prty_s08_MS_Result Sce_Prty_S08_MS(String searchKeyIdParty)
        {
            try
            {
                return unitOfWork.SceRepository.Sce_Prty_S08_MS(searchKeyIdParty);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Marca o Desmarca Participante para ser Eliminado
        /// </summary>
        /// <param name="keyIdParty"></param>
        /// <param name="deletePrty"></param>
        /// <returns></returns>
        public int Sce_Prty_U01_MS(String keyIdParty, Boolean deletePrty)
        {
            return unitOfWork.SceRepository.Sce_Prty_U01_MS(keyIdParty, deletePrty);
        }

        public int pro_sce_aprty_s01_MS(String IdParty, int Sflag)
        {
            /*
            var result = unitOfWork.SceRepository.pro_sce_aprty_s01_MS(IdParty, Sflag);
            if(result!= null)
            {
               if(result[0].HasValue)
                   return result[0].Value;
            }
             * */
            return 0;
        }

        public sce_usr_s05_MS_Result Sce_Usr_S05_MS(String centroCosto, String codigoUsuario)
        {
            return unitOfWork.SceRepository.Sce_Usr_S05_MS(centroCosto, codigoUsuario);
        }

        public List<String> Sce_Usr_S06_MS(String centroCosto, String idEspecialista)
        {
            return unitOfWork.SceRepository.Sce_Usr_S06_MS(centroCosto, idEspecialista);
        }

        /// <summary>
        /// Direcciones asociadas a un participante
        /// </summary>
        /// <param name="idParty"></param>
        /// <returns></returns>
        public IList<sce_dad_s08_MS_Result> Sce_Dad_S08_MS(String idParty)
        {
            return unitOfWork.SceRepository.Sce_Dad_S08_MS(idParty);
        }

        public IList<sgt_ejc_s02_MS_Result> Sgt_ejc_s02_MS(Int32 codigo)
        {
            return unitOfWork.SceRepository.Sgt_ejc_S02_MS(codigo);
        }

        /// <summary>
        /// Especialistas
        /// </summary>
        /// <param name="ejCopImp"></param>
        /// <param name="ejCopExp"></param>
        /// <param name="ejCneGoc"></param>
        /// <returns></returns>
        public IList<sgt_ejc_s03_MS_Result> Sgt_Ejc_S03_MS(String ejCopImp, String ejCopExp, String ejCneGoc)
        {
            return unitOfWork.SgtRepository.Sgt_Ejc_S03_MS(ejCopImp, ejCopExp, ejCneGoc);
        }

        public IList<sgt_pai_s02_MS_Result> Sgt_Pai_S02_MS()
        {
            return unitOfWork.SgtRepository.Sgt_Pai_S02_MS();
        }

        public IList<sgt_suc_s01_MS_Result> Sgt_Suc_S01_MS()
        {
            return unitOfWork.SgtRepository.Sgt_Suc_S01_MS();
        }

        /// <summary>
        /// Ejecutivos
        /// </summary>
        /// <param name="codigoOficina"></param>
        /// <returns></returns>
        public IList<sgt_ejc_s04_MS_Result> Sgt_Ejc_S04_MS(Int16 codigoOficina)
        {
            return unitOfWork.SgtRepository.Sgt_Ejc_S04_MS(codigoOficina);
        }

        public IList<sgt_loc_s01_MS_Result> Sgt_Loc_S01_MS()
        {
            return unitOfWork.SgtRepository.Sgt_Loc_S01_MS();
        }

        public IList<sce_abr_s01_MS_Result> Sce_Abr_S01_MS()
        {
            return unitOfWork.SceRepository.Sce_Abr_S01_MS();
        }

        public IList<sgt_aec_s01_MS_Result> Sgt_aec_s01_MS()
        {
            return unitOfWork.SceRepository.Sgt_aec_s01_MS();
        }

        public IList<sgt_clf_s01_MS_Result> Sgt_Clf_S01_MS()
        {
            return unitOfWork.SgtRepository.Sgt_Clf_S01_MS();
        }

        public void Pro_Sce_Prty_S08_MS(String idParty, Int32 idNombre, Int32 idDir)
        {
            unitOfWork.SceRepository.Pro_Sce_Prty_S08_MS(idParty, idNombre, idDir);
        }

        public IList<sce_tcom_s04_MS_Result> Sce_Tcom_S04_MS(String idParty)
        {
            return unitOfWork.SceRepository.Sce_Tcom_S04_MS(idParty);
        }

        public IList<sce_tgas_s04_MS_Result> Sce_Tgas_S04_MS(String idParty)
        {
            return unitOfWork.SceRepository.Sce_Tgas_S04_MS(idParty);
        }

        public IList<sce_tint_s01_MS_Result> Sce_Tint_S01_MS(String idParty)
        {
            return unitOfWork.SceRepository.Sce_Tint_S01_MS(idParty);
        }

        public IList<sgt_mnd_s02_MS_Result> Sgt_Mnd_S02_MS()
        {
            return unitOfWork.SgtRepository.Sgt_Mnd_S02_MS();
        }

        public IList<sce_prty_i01_MS_Result> Sce_Prty_I01_MS(String keySearchIdParty, String idParty, String createCosto, String createUser, DateTime dateTime)
        {
            return unitOfWork.SceRepository.Sce_Prty_I01_MS(keySearchIdParty, idParty, createCosto, createUser, dateTime);
        }

        #region Index

        /// <summary>
        /// Inicia la aplicacion
        /// </summary>
        /// <returns></returns>
        public InitializationObject AdminParticipantesInit()
        {
            InitializationObject initObj = SetupInitObj();
            //  InitializationObject initObj = new InitializationObject();
            //MdiForm_Load(initObj);
            return initObj;
        }

        public Party PartyInit()
        {
            Party party = new Party();
            return party;
        }


        private InitializationObject SetupInitObj()
        {
            InitializationObject initObject;
            try
            {
                initObject = MODWS.Inicializar(unitOfWork);
            }
            catch (Exception ex)
            {
                initObject = new InitializationObject(true);
                initObject.Mdi_Principal.MESSAGES.Add(new UI_Message
                {
                    Title = "AdminParticipantes",
                    Text = "Error al inciar la aplicación: " + ex.Message,
                    Type = TipoMensaje.Critical
                });
            }

            return initObject;
        }

        private void MdiFormWeb_Load(InitializationObject InitObject)
        {
            PrtEnt01.Form_Load(InitObject, unitOfWork);
        }

        #endregion

        #region "CARACTERISTICAS PARTICIPANTE"

        public void CaracteristicasParticipante_llama0711(InitializationObject initObj)
        {
            //  MODWS.Carga_datos(initObj, unitOfWork, (int)Enums.Pagina.PRTENT07);
            string oficina = string.Empty;
            string ejecutivo = string.Empty;
            string actividad = string.Empty;
            string riesgo = string.Empty;

            switch (initObj.PRTGLOB.Party.tipo)
            {
                case T_PRTGLOB.tipo_cliente:
                    PRTYENT.llama07(initObj, unitOfWork);
                    CaracteristicasParticipanteInit(initObj);
                    break;

                case T_PRTGLOB.tipo_banco:
                    initObj.PaginaWebQueAbrir = "DatosBancoParticipante";
                    return;

                case T_PRTGLOB.individuo:
                    if (initObj.PRTGLOB.Party.sirut != 0)
                    {
                        initObj.PrtEnt07.prtrut.Text = PRTYENT.descero(initObj.PRTGLOB.Party.rut);
                        initObj.PrtEnt07.prtrut.Enabled = false;
                        initObj.PrtEnt07.prtcliente[0].Enabled = true;
                        initObj.PrtEnt07.prtcliente[1].Enabled = true;
                    }
                    else
                    {
                        //initObj.PrtEnt07.prtrut.Text = T_PRTGLOB.formato_rut;
                        initObj.PrtEnt07.prtrut.Enabled = true;
                        initObj.PrtEnt07.prtcliente[0].Enabled = false;
                        initObj.PrtEnt07.prtcliente[1].Enabled = false;
                    }

                    initObj.PrtEnt07.prtcliente[0].Selected = false;
                    // initObj.PrtEnt07.prtcliente[1].Selected = true;    

                    oficina = initObj.PRTGLOB.Party.oficina == null ? string.Empty : initObj.PRTGLOB.Party.oficina;
                    ejecutivo = initObj.PRTGLOB.Party.ejecutivo == null ? string.Empty : initObj.PRTGLOB.Party.ejecutivo;
                    actividad = initObj.PRTGLOB.Party.actividad == null ? string.Empty : initObj.PRTGLOB.Party.actividad;
                    riesgo = initObj.PRTGLOB.Party.riesgo == null ? string.Empty : initObj.PRTGLOB.Party.riesgo;

                    if (!string.IsNullOrEmpty(oficina) || !string.IsNullOrEmpty(ejecutivo) || !string.IsNullOrEmpty(actividad) || !string.IsNullOrEmpty(riesgo))
                    {
                        initObj.PrtEnt07.prtcliente[0].Selected = false;
                        initObj.PrtEnt07.prtcliente[1].Selected = true; //La Lleva 
                        PRTYENT.escribeinfoparty(initObj, unitOfWork);
                        initObj.PrtEnt07.cboOficina.Enabled = true;
                        initObj.PrtEnt07.ejecutivo.Enabled = true;
                        initObj.PrtEnt07.Combo2.Enabled = true;
                        initObj.PrtEnt07.Combo4.Enabled = true;
                        initObj.PrtEnt07.Combo1.ListIndex = 8;
                        initObj.PrtEnt07.Combo1.Enabled = true;
                    }
                    else
                    {
                        initObj.PrtEnt07.prtcliente[0].Selected = true;
                        initObj.PrtEnt07.prtcliente[1].Selected = false;      //La LLeva                 
                        initObj.PrtEnt07.cboOficina.Enabled = false;
                        initObj.PrtEnt07.ejecutivo.Enabled = false;
                        initObj.PrtEnt07.Combo2.Enabled = false;
                        initObj.PrtEnt07.Combo4.Enabled = false;
                        initObj.PrtEnt07.Combo1.ListIndex = 8;
                        initObj.PrtEnt07.Combo1.Enabled = false;
                    }

                    initObj.PrtEnt07.aceptar.Enabled = false;
                    initObj.PrtEnt07.cboOficina.Enabled = true;
                    CaracteristicasParticipanteInit(initObj);
                    break;
            }

            initObj.PrtEnt07.aceptar.Enabled = true;
            initObj.PrtEnt07.cancelar.Enabled = true;
        }

        public void CaracteristicasParticipanteInit(InitializationObject initObject)
        {
            PrtEnt07.Form_Load(initObject, unitOfWork);
            PrtEnt07.listarOficinas_CodNom(initObject);
        }

        public void CaracteristicasParticipanteAceptar(InitializationObject initObject)
        {
            PrtEnt07.Aceptar_Click(initObject, unitOfWork);
        }

        public void CaracteristicasParticipante_Boton_IngImportacion_Click(InitializationObject initObject)
        {
            PrtEnt07.Bot_IngImp_Click(initObject);
        }

        public void CaracteristicasParticipante_Boton_ElimImportacion_Click(InitializationObject initObject)
        {
            PrtEnt07.Bot_EliImp_Click(initObject);
        }

        public void CaracteristicasParticipante_Boton_IngExportacion_Click(InitializationObject initObject)
        {
            PrtEnt07.Bot_IngExp_Click(initObject);
        }

        public void CaracteristicasParticipante_Boton_ElimExportacion_Click(InitializationObject initObject)
        {
            PrtEnt07.Bot_EliExp_Click(initObject);
        }

        public void CaracteristicasParticipante_Boton_IngNegocio_Click(InitializationObject initObject)
        {
            PrtEnt07.Bot_IngNeg_Click(initObject);
        }

        public void CaracteristicasParticipante_Boton_ElimNegocio_Click(InitializationObject initObject)
        {
            PrtEnt07.Bot_EliNeg_Click(initObject);
        }

        //Importacion
        public void CaracteristicasParticipante_Cb_CenCosImportacion_Click(InitializationObject initObject)
        {
            PrtEnt07.Cbo_CenCosImp_Click(initObject);
        }

        public void CaracteristicasParticipante_Cb_CenCosExportacion_Click(InitializationObject initObject)
        {
            PrtEnt07.Cbo_CenCosExp_Click(initObject);
        }

        public void CaracteristicasParticipante_Cb_CenCosNegocio_Click(InitializationObject initObject)
        {
            PrtEnt07.Cbo_CenCosNeg_Click(initObject);
        }


        public void CaracteristicasParticipante_PrtCliente_Click(InitializationObject initObject) // 0:Cliente Comex   1:Cliente Banco
        {
            if (initObject.PrtEnt07.prtcliente[0].Selected)
                PrtEnt07.prtcliente_Click(initObject);
            else
                PrtEnt07.cliente_Click(initObject);
        }

        public void CaracteristicasParticipante_Cb_Ejecutivo_Click(InitializationObject initObject)
        {
            PrtEnt07.ejecutivo_Click(initObject);
        }

        public void CaracteristicasParticipante_Cb_Oficina_Click(InitializationObject initObject, int idcbOficina)
        {
            PrtEnt07.oficina_Click(initObject, unitOfWork, idcbOficina);
        }

        public void CaracteristicasParticipante_Cb_ActividadEconomica_Click(InitializationObject initObject)
        {
            PrtEnt07.Combo2_Click(initObject);
        }

        public void CaracteristicasParticipante_Cb_ClaseRiesgo_Click(InitializationObject initObject)
        {
            PrtEnt07.Combo4_Click(initObject);
        }

        public void CaracteristicasParticipante_Cb_Clasificacion_Click(InitializationObject initObject)
        {
            PrtEnt07.Combo1_Click(initObject);
        }


        public string ValidaRut(string Rut)
        {
            return UTILES.ValidaRut(Rut);
        }

        #endregion

        #region "INSTRUCCIONES PARTICIPANTE"

        public void InstruccionesParticipanteInit(InitializationObject initObject)
        {
            PrtEnt01.llama04(initObject, unitOfWork);
            PrtEnt04.Form_Load(initObject, unitOfWork);
        }
        public void InstruccionesParticipanteAceptar(InitializationObject initObject)
        {
            PrtEnt04.Aceptar_Click(initObject);
        }

        public void InstruccionesParticipanteCmbMemo(InitializationObject initObject)
        {
            PrtEnt04.CmbMemo_Click(initObject);
        }

        //-----------------
        public void InstruccionesParticipante_PrtInstrucciones_Change(InitializationObject initObject)
        {
            PrtEnt04.prtinstruc_Change(initObject);
        }

        public void InstruccionesParticipante_PrtInstrucciones_Keypress(InitializationObject initObject)
        {
            PrtEnt04.prtinstruc_KeyPress(initObject);
        }

        public void InstruccionesParticipante_PrtInstrucciones_LostFocus(InitializationObject initObject)
        {
            PrtEnt04.prtinstruc_KeyPress(initObject);
            PrtEnt04.prtinstruc_LostFocus(initObject);
        }

        #endregion

        #region "TASA ESPECIALES"

        public void TasasEspecialesParticipanteInit(InitializationObject initObject)
        {
            PrtEnt01.llama06(initObject, unitOfWork);
            PrtEnt06.Form_Load(initObject);
        }
        public void TasasEspeciales_Boton_Aceptar_Click(InitializationObject initObject)
        {
            PrtEnt06.Aceptar_Click(initObject);
        }
        public void TasasEspeciales_Boton_Cancelar_Click(InitializationObject initObject)
        {
            PrtEnt06.Cancelar_Click(initObject);
        }
        public void TasasEspeciales_Boton_Agregar_Click(InitializationObject initObject)
        {
            PrtEnt06.Agregar_Click(initObject);
        }
        public void TasasEspeciales_Boton_Eliminar_Click(InitializationObject initObject)
        {
            PrtEnt06.Eliminar_Click(initObject);
        }
        public void TasasEspeciales_lista_Com_dblclick(InitializationObject initObject)
        {
            PrtEnt06.lista_com_dblclick(initObject);
        }
        public void TasasEspeciales_lista_dblclick(InitializationObject initObject)
        {
            PrtEnt06.lista_dblclick(initObject);
        }
        public void TasasEspeciales_Tarifa_Click(InitializationObject initObject)
        {
            PrtEnt06.tarifa_Click(initObject);
        }
        public void TasasEspeciales_TipoInteres_Click(InitializationObject initObject)
        {
            PrtEnt06.prttipoi_Click(initObject);
        }
        public void TasasEspecialesParticipante_Menu_Click(InitializationObject initObject)
        {
            PrtEnt06.menu_Click(initObject);
        }
        public void SeteaArreglos(InitializationObject initObject)
        {

            //initObject.PRTGLOB.rescom = new prtytcom[0];
            //initObject.PRTGLOB.resgas = new prtytgas[0];
            //initObject.PRTGLOB.resint = new prtytint[0];
        }
        public void TasasEspeciales_ImportacionesCobranza_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.etacob_Click(initObject, Etapa);
        }

        public void TasasEspeciales_ImportacionesCobranza_Si_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.etacob_Si_Click(initObject, Etapa);
        }
        public void TasasEspeciales_ImportacionesCartaCredito_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.etacar_Click(initObject, Etapa);
        }

        public void TasasEspeciales_ImportacionesCartaCredito_Si_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.etacar_Si_Click(initObject, Etapa);
        }



        public void TasasEspeciales_Exportaciones_Nivel1_Click(InitializationObject initObject, int Etapa)
        {

            PrtEnt06.prodexp_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Exportaciones_Nivel1_Si_Click(InitializationObject initObject, int Etapa)
        {

            PrtEnt06.prodexp_Si_Click(initObject, Etapa);
        }
        public void TasasEspeciales_Exportaciones_Nivel2_Ingreso_Click(InitializationObject initObject)
        {
            PrtEnt06.etapae_Click(initObject);
        }

        public void TasasEspeciales_Exportaciones_Nivel2_Ingreso_Si_Click(InitializationObject initObject)
        {

            PrtEnt06.etapae_Si_Click(initObject);
        }
        public void TasasEspeciales_Exportaciones_Nivel2_GestionCompraDocumentos_Click(InitializationObject initObject, int Etapa)
        {
            PrtEnt06.etacom_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Exportaciones_Nivel2_GestionCompraDocumentos_Si_Click(InitializationObject initObject, int Etapa)
        {
            PrtEnt06.etacom_Si_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Exportaciones_Nivel2_GestionDescuentoDocumentos_Click(InitializationObject initObject, int Etapa)
        {
            PrtEnt06.etades_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Exportaciones_Nivel2_GestionDescuentoDocumentos_Si_Click(InitializationObject initObject, int Etapa)
        {
            PrtEnt06.etades_Si_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Exportaciones_Nivel2_CartaCredito_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.etacred_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Exportaciones_Nivel2_CartaCredito_Si_Click(InitializationObject initObject, int Etapa)
        {
            PrtEnt06.etacred_Si_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Exportaciones_Nivel2_Cobranza_Click(InitializationObject initObject, int Etapa)
        {
            PrtEnt06.etapa_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Exportaciones_Nivel2_Cobranza_Si_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.etapa_Si_Click(initObject, Etapa);
        }
        public void TasasEspeciales_Exportaciones_Nivel2_Retorno_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.etaret_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Exportaciones_Nivel2_Retorno_Si_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.etaret_Si_Click(initObject, Etapa);
        }
        public void TasasEspeciales_Servicios_Nivel1_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.prodser_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Servicios_Nivel1_Si_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.prodser_Si_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Servicios_Nivel2_Ex_Financiamiento_Click(InitializationObject initObject, int Etapa)
        {
            PrtEnt06.etafin_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Servicios_Nivel2_Ex_Financiamiento_Si_Click(InitializationObject initObject, int Etapa)
        {
            PrtEnt06.etafin_Si_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Servicios_Nivel2_Orden_Pago_Condicionado_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.etaor_Click(initObject, Etapa);
        }

        public void TasasEspeciales_Servicios_Nivel2_Orden_Pago_Condicionado_Si_Click(InitializationObject initObject, int Etapa)
        {
            //SeteaArreglos(initObject);
            PrtEnt06.etaor_Si_Click(initObject, Etapa);
        }
        public void TasasEspeciales_Lista_Si_Click(InitializationObject initObject)
        {
            PrtEnt06.Lista_Si_Click(initObject);
        }
        public void TasasEspeciales_Lista_No_Click(InitializationObject initObject)
        {
            PrtEnt06.Lista_No_Click(initObject);
        }
        //public void prodexp_Si_Click(InitializationObject initObject)
        //{
        //    PrtEnt06.prodexp_Si_Click(initObject);
        //}




        #endregion

        #region "MODAL TASAS ESPECIALES"

        //public void TasasEspeciales_VieneDetalleInit(InitializationObject initObject)
        //{
        //    PrtEnt06.Form_Load_CerrarModal(initObject);
        //}
        public void ModalTasasEspeciales_Aceptar_Click(InitializationObject initObject)
        {
            PrtEnt13.Aceptar_Click(initObject, unitOfWork);
        }

        public void ModalTasasEspeciales_Cancelar_Click(InitializationObject initObject)
        {
            PrtEnt13.Cancelar_Click(initObject);
        }
        public void ModalTasasEspeciales_Manual_click(InitializationObject initObject)
        {
            PrtEnt13.manual_Click(initObject);
        }
        public void ModalTasasEspeciales_Fijo_click(InitializationObject initObject)
        {
            PrtEnt13.fijo_Click(initObject);
        }
        public void minimo_LostFocus(InitializationObject initObject)
        {
            PrtEnt13.minimo_LostFocus(initObject);
        }
        public void maximo_LostFocus(InitializationObject initObject)
        {
            PrtEnt13.maximo_LostFocus(initObject);
        }
        public void tasa_LostFocus(InitializationObject initObject)
        {
            PrtEnt13.tasa_LostFocus(initObject, unitOfWork);
        }
        public void monto_LostFocus(InitializationObject initObject)
        {
            PrtEnt13.monto_LostFocus(initObject);
        }

        #endregion

        public IList<String> Sce_Prty_S07_MS(String idParty)
        {
            return unitOfWork.SceRepository.Sce_Prty_S07_MS(idParty);
        }

        public IList<String> Sce_Bic_S07_MS(String searchIdParty)
        {
            return unitOfWork.SceRepository.Sce_Bic_S07_MS(searchIdParty);
        }

        #region "CUENTAS PARTICIPANTE"
        public void CuentasParticipanteInit(InitializationObject initObject)
        {
            initObject.PrtEnt08.Lista1.Clear();
            initObject.PrtEnt08.Lista2.Clear();
            initObject.PrtEnt08.Lista1.ListIndex = -1;
            initObject.PrtEnt08.Lista2.ListIndex = -1;
            MODWS.Carga_datos(initObject, unitOfWork, (int)Enums.Pagina.PRTENT08);
            PRTYENT.llama08(initObject, unitOfWork);
            PrtEnt08.Form_Load(initObject, unitOfWork);
        }

        public void CuentasParticipanteVieneDetalleInit(InitializationObject initObject)
        {
            PrtEnt08.Form_Load(initObject, unitOfWork);
        }
        public void CuentasParticipante_Aceptar(InitializationObject initObject)
        {
            PrtEnt08.Aceptar_Click(initObject, unitOfWork);
        }
        //public void CuentasParticipante_Cancelar(InitializationObject initObject)
        //{
        //    PrtEnt08.Cancelar_Click(initObject);
        //}
        public void CuentasParticipante_lista1_dblclick(InitializationObject initObject)
        {
            PrtEnt08.lista1_dblclick(initObject);
        }
        #endregion

        #region "DETALLE CUENTAS"

        public void DetalleCuentasInit(InitializationObject InitObject, string lista)//,bool nacional)
        {
            //if (lista == "Lista1")
            //{
            //    PrtEnt08.lista1_dblclick(InitObject);
            //    PrtEnt10.prtcuenta_Change(InitObject);
            //    PrtEnt10.prtcuenta_GotFocus(InitObject);
            //}
            //else
            //{
            //    PrtEnt08.lista2_dblclick(InitObject);
            //    PrtEnt10.prtcuenta_Change(InitObject);
            //    PrtEnt10.prtcuenta_GotFocus(InitObject);
            //}
            PrtEnt10.Form_Load(InitObject);//,nacional);
        }

        public void DetalleCuentas_lista1_ModalSi(InitializationObject initObject)
        {
            PrtEnt08.lista1_ModalSi(initObject);
            DetalleCuentas_lista1_Nacional_dblclick(initObject);
        }

        public void DetalleCuentas_lista1_Nacional_dblclick(InitializationObject initObject)
        {
            PrtEnt08.lista1_dblclick(initObject);
            //PrtEnt08.Lista1_KeyPress(initObject);

            PrtEnt10.prtcuenta_Change(initObject);
            PrtEnt10.prtcuenta_GotFocus(initObject);
        }

        public void DetalleCuentas_lista1_Nacional_TipoCliente_dblclick(InitializationObject initObject)
        {
            PrtEnt08.lista1_CuentaCorriente_tipocliente_Si_dblclick(initObject);
            //PrtEnt08.Lista1_KeyPress(initObject);
            PrtEnt10.prtcuenta_Change(initObject);
            PrtEnt10.prtcuenta_GotFocus(initObject);
        }

        public void DetalleCuentas_lista1_Nacional_TipoBanco_dblclick(InitializationObject initObject)
        {
            PrtEnt08.lista1_CuentaCorriente_tipo_Banco_Si_dblclick(initObject);
            //PrtEnt08.Lista1_KeyPress(initObject);

            PrtEnt10.prtcuenta_Change(initObject);
            PrtEnt10.prtcuenta_GotFocus(initObject);
        }

        public void DetalleCuentas_lista2_ModalSi(InitializationObject initObject)
        {
            PrtEnt08.lista2_ModalSi(initObject);
            DetalleCuentas_lista2_Extranjera_dblclick(initObject);
        }

        public void DetalleCuentas_lista2_Extranjera_dblclick(InitializationObject initObject)
        {
            PrtEnt08.lista2_dblclick(initObject);
            PrtEnt10.Form_Load(initObject);
            PrtEnt10.prtcuenta_Change(initObject);
            PrtEnt10.prtcuenta_GotFocus(initObject);
        }

        public void DetalleCuentas_lista2_Extranjera_CuentaCorriente_Si_dblclick(InitializationObject initObject)
        {
            PrtEnt08.Lista2_CuentaCorriente_Si(initObject);
            PrtEnt10.prtcuenta_Change(initObject);
            PrtEnt10.prtcuenta_GotFocus(initObject);
        }

        public void DetalleCuentas_lista2_Extranjera_LineaCredito_Si_dblclick(InitializationObject initObject)
        {
            PrtEnt08.Lista2_LineaCredito_Si(initObject);
            PrtEnt10.prtcuenta_Change(initObject);
            PrtEnt10.prtcuenta_GotFocus(initObject);
        }


        public void DetalleCuentas_Aceptar_Click(InitializationObject initObject)
        {
            PrtEnt10.Aceptar_Click(initObject);
        }

        public void DetalleCuentas_Cancelar_Click(InitializationObject initObject)
        {
            PrtEnt10.Cancelar_Click(initObject);
        }

        public void DetalleCuentas_Eliminar_Click(InitializationObject initObject)
        {
            PrtEnt10.Eliminar_Click(initObject, unitOfWork);
        }

        public void DetalleCuentas_Prtactiva_Click(InitializationObject initObject)
        {
            PrtEnt10.prtactiva_Click(initObject);
        }

        public void DetalleCuentas_CuentaBae_Click(InitializationObject initObject)
        {
            PrtEnt10.CuentaBae_Click(initObject);
        }
        public void DetalleCuentas_Especial_Click(InitializationObject initObject)
        {
            PrtEnt10.especial_Click(initObject);
        }

        #endregion

        #region "NUEVO PARTICIPANTE"

        public void NuevoParticipanteInit(InitializationObject initObject)
        {
            initObject.PrtEnt00 = new UI_PrtEnt00();
            PrtEnt00.Form_Load(initObject);
        }


        #endregion

        #region "DATOS BANCO"

        public void DatosBancoParticipanteInit(InitializationObject initObject)
        {
            PRTYENT.llama11(initObject);
            PrtEnt11.Form_Load(initObject);

        }

        public void DatosBancoParticipante_Aceptar_Click(InitializationObject initObject)
        {
            PrtEnt11.Aceptar_Click(initObject);
        }

        public void DatosBancoParticipante_Tasa_Click(InitializationObject initObject)
        {
            PrtEnt11.prttasa_Click(initObject);
        }

        public void DatosBancoParticipante_Index_TipoBancoClick(InitializationObject InitObject, string index)
        {
            PrtEnt11.prttipob_Click(InitObject, index);
        }

        public void DatosBancoParticipante_prtaladi_Click(InitializationObject initObject)
        {
            PrtEnt11.prtaladi_Click(initObject);

        }


        public void DatosBancoParticipante_prtcodigo_Change(InitializationObject initObject)
        {
            PrtEnt11.prtcodigo_Change(initObject);
        }

        public void DatosBancoParticipante_prtrut_LostFocus(InitializationObject initObject)
        {
            PrtEnt11.prtrut_LostFocus(initObject);
        }
        public void DatosBancoParticipante_prtrut_Change(InitializationObject initObject)
        {
            PrtEnt11.prtrut_Change(initObject);
        }
        public void DatosBancoParticipante_prtspread_Change(InitializationObject initObject)
        {
            PrtEnt11.prtspread_Change(initObject);
        }
        public void DatosBancoParticipante_prtswif_Change(InitializationObject initObject)
        {
            PrtEnt11.prtswif_Change(initObject);
        }

        public void DatosBancoParticipante_prtplaza_Change(InitializationObject initObject)
        {
            PrtEnt11.prtplaza_Change(initObject);
        }



        #endregion

        #region "ACTIVAR RAZON"

        public int ActivarRazon(InitializationObject initObject, decimal idNombre)
        {
            return PRTYENT.activar_razsoc(initObject, unitOfWork, idNombre);
        }

        //public int AceptaParty(InitializationObject initObject)
        //{
        //    int Prueba = 0;
        //    string argTemp1 = initObject.PRTGLOB.Party.idparty.UCase();
        //    Prueba = PRTYENT.AceptaParty(initObject, unitOfWork, ref argTemp1, true.ToInt());

        //    return Prueba;

        //}

        #endregion

        #region "LIMPIAR OBJETOS MEMORIA"
        public void LimpiarObjetos(InitializationObject initObject)
        {
            //initObject = null;     
            //Moo
            initObject.PRTGLOB.nom = new prtynombre[0];
            initObject.PRTGLOB.direc = new prtydireccion[0];

            //JFM

            //---Caracteristicas
            initObject.PRTGLOB.Party = new prtyprincipal();
            initObject.PRTYENT2.VSGTCliEsp = new CliEsp[0];
            initObject.PRTYENT2.RSGTCliEsp = new CliEsp[0];
            initObject.PRTGLOB.acteco = new tipo_acteco[0];
            initObject.PRTGLOB.riesgo = new tipo_riesgo[0];
            initObject.PRTGLOB.CenCos = new T_Cencos[0];

            //---Datos Banco
            initObject.PRTGLOB.Party.PrtGlob = new PrtGlob();

            //---Instrucciones
            initObject.PRTGLOB.instruccion = new prtyinst[0];
            initObject.PrtEnt04.CmbMemo.Text = string.Empty;

            //---Cuentas
            initObject.MODWS.CtaCCOL = new Cuentas[0];
            initObject.PRTGLOB.ctaclie = new prtyccta[0];
            //initObject.PrtEnt08.Lista1.Clear();
            //initObject.PrtEnt08.Lista2.Clear();

            //--------------Detalle Cuentas
            initObject.PRTGLOB.ctaclie_aux = new cuenta_indice[0];
            initObject.PRTGLOB.ctabancos_aux = new cuenta_indice[0];
            initObject.PRTGLOB.linbancos_aux = new cuenta_indice[0];

            //---Tasas 
            initObject.PRTGLOB.tasacom = new prtytcom[0];
            initObject.PRTGLOB.tasaint = new prtytint[0];
            initObject.PRTGLOB.tasagas = new prtytgas[0];
            initObject.PRTGLOB.rescom = new prtytcom[0];
            initObject.PRTGLOB.resgas = new prtytgas[0];
            initObject.PRTGLOB.resint = new prtytint[0];
            //initObject.PrtEnt06.lista_com.Clear();
            //initObject.PrtEnt06.lista.Clear();

            //PRTGLOB
            T_PRTGLOB.paises = new tipo_paises[0];
            //initObject.PRTGLOB.PrtControl = new PartyParametros();

            //PRTYENT
            //initObject.PRTYENT.VEjc = new T_Especialista[0];
        }


        #endregion

        #region "PRTYENT2"
        public int Habil_SGTCliEje(InitializationObject init)
        {
            int Habil_SGTCliEje = PRTYENT.Habil_SGTCliEje(unitOfWork, init).ToInt();
            return Habil_SGTCliEje;

        }
        #endregion

        public int VSyGet_Cta(InitializationObject initObject, string cuenta, int Flag)
        {
            UnitOfWorkCext01 unitOfWork;
            unitOfWork = new UnitOfWorkCext01();
            return MODWS.SyGet_Cta(initObject, cuenta, 2, unitOfWork);
        }

        public int Sce_Tcom_D01_MS(String id_party)
        {

            return unitOfWork.SceRepository.sce_tcom_d01_MS(id_party);
        }

        public int Sce_Tint_D01_MS(String id_party)
        {
            return unitOfWork.SceRepository.sce_tint_d01_MS(id_party);
        }

        public int Sce_Tgas_D01_MS(String id_party)
        {
            return unitOfWork.SceRepository.sce_tgas_d01_MS(id_party);
        }

        public IList<sce_ctas_s04_MS_Result> Sce_Ctas_S04_MS(String idPrty)
        {
            return unitOfWork.SceRepository.sce_ctas_s04_MS(idPrty);
        }
        public IList<sce_bcta_s01_MS_Result> Sce_Bcta_S01_MS(String idPrty)
        {
            return unitOfWork.SceRepository.sce_bcta_s01_MS(idPrty);
        }

        public string sce_prty_w01_MS(string id_party,
                                      bool borrado,
                                      byte tipo_party,
                                      short flag,
                                      byte clasificac,
                                      bool tiene_rut,
                                      string rut,
                                      string crea_costo,
                                      string crea_user,
                                      string mod_costo,
                                      string mod_user,
                                      bool multiple,
                                      string cod_ofieje,
                                      string cod_eject,
                                      string cod_acteco,
                                      string clase_ries,
                                      short cod_bco,
                                      bool tasa_libor,
                                      bool tasa_prime,
                                      float spread,
                                      string swift,
                                      decimal plaza_alad,
                                      string ejec_corre,
                                      short flagins,
                                      decimal insgen_imp,
                                      decimal insgen_exp,
                                      decimal insgen_ser,
                                      decimal inscob_imp,
                                      decimal inscob_exp,
                                      decimal inscre_imp,
                                      decimal inscre_exp,
                                      string par1)
        {
            return unitOfWork.SceRepository.sce_prty_w01_MS(id_party,
                                        borrado,
                                        tipo_party,
                                        flag,
                                        clasificac,
                                        tiene_rut,
                                        rut,
                                        crea_costo,
                                        crea_user,
                                        mod_costo,
                                        mod_user,
                                        multiple,
                                        cod_ofieje,
                                        cod_eject,
                                        cod_acteco,
                                        clase_ries,
                                        cod_bco,
                                        tasa_libor,
                                        tasa_prime,
                                        spread,
                                        swift,
                                        plaza_alad,
                                        ejec_corre,
                                        flagins,
                                        insgen_imp,
                                        insgen_exp,
                                        insgen_ser,
                                        inscob_imp,
                                        inscob_exp,
                                        inscre_imp,
                                        inscre_exp,
                                        par1);
        }

        public string sce_netd_ejc_clt_w01_MS(string rutcli, decimal odejc, decimal oficina)
        {
            return unitOfWork.SceRepository.sce_netd_ejc_clt_w01_MS(rutcli, odejc, oficina);
        }

        public int? ejc_prty_ejc_i_01_MS(string PrtyRut, decimal Ejcofi, decimal EjcCod, string EjcTpo, DateTime FechaCreate, DateTime FechaMod)
        {
            return unitOfWork.SceRepository.ejc_prty_ejc_i_01_MS(PrtyRut, Ejcofi, EjcCod, EjcTpo, FechaCreate, FechaMod);
        }

        public IList<ejc_prty_ejc_s_01_MS_Result> ejc_prty_ejc_s_01_MS(string prty_rut)
        {
            return unitOfWork.SceRepository.ejc_prty_ejc_s_01_MS(prty_rut);
        }

        public int sce_rsa_i01_MS(
                                    string idparty,
                                    int id_nombre,
                                    string razon_soci,
                                    string nom_fantas,
                                    string contacto,
                                    string sortkey,
                                    string crea_costo,
                                    string crea_user)
        {

            return unitOfWork.SceRepository.sce_rsa_i01_MS(
                                        idparty,
                                        id_nombre,
                                        razon_soci,
                                        nom_fantas,
                                        contacto,
                                        sortkey,
                                        crea_costo,
                                        crea_user);
        }

        public int sce_rsa_u02_MS(string id_party, decimal id_nombre)
        {
            return unitOfWork.SceRepository.sce_rsa_u02_MS(id_party, id_nombre);
        }

        public int sce_rsa_u01_MS(string idparty,
                            int id_nombre,
                            int borrado,
                            string razon_soci,
                            string nom_fantas,
                            string contacto,
                            string sortkey)
        {
            return unitOfWork.SceRepository.sce_rsa_u01_MS(idparty,
                            id_nombre,
                            borrado,
                            razon_soci,
                            nom_fantas,
                            contacto,
                            sortkey);
        }

        public string sce_dad_i01_MS(string id_party,
                                    decimal id_dir,
                                    bool borrado,
                                    string direccion,
                                    string comuna,
                                    decimal cod_comuna,
                                    string cod_postal,
                                    string estado,
                                    string ciudad,
                                    string pais,
                                    decimal cod_pais,
                                    string telefono,
                                    string fax,
                                    string telex,
                                    decimal envio_sce,
                                    decimal recibe_sce,
                                    string cas_postal,
                                    string cas_banco,
                                    string crea_costo,
                                    string crea_user,
                                    string email)
        {
            return unitOfWork.SceRepository.sce_dad_i01_MS(id_party,
                                    id_dir,
                                    borrado,
                                    direccion,
                                    comuna,
                                    cod_comuna,
                                    cod_postal,
                                    estado,
                                    ciudad,
                                    pais,
                                    cod_pais,
                                    telefono,
                                    fax,
                                    telex,
                                    envio_sce,
                                    recibe_sce,
                                    cas_postal,
                                    cas_banco,
                                    crea_costo,
                                    crea_user,
                                    email);
        }


        public string sce_dad_u02_MS(string id_party,
                                    decimal id_dir,
                                    bool borrado,
                                    string direccion,
                                    string comuna,
                                    decimal cod_comuna,
                                    string cod_postal,
                                    string estado,
                                    string ciudad,
                                    string pais,
                                    decimal cod_pais,
                                    string telefono,
                                    string fax,
                                    string telex,
                                    decimal envio_sce,
                                    decimal recibe_sce,
                                    string cas_postal,
                                    string cas_banco,
                                    string email)
        {
            return unitOfWork.SceRepository.sce_dad_u02_MS(id_party,
                                id_dir,
                                borrado,
                                direccion,
                                comuna,
                                cod_comuna,
                                cod_postal,
                                estado,
                                ciudad,
                                pais,
                                cod_pais,
                                telefono,
                                fax,
                                telex,
                                envio_sce,
                                recibe_sce,
                                cas_postal,
                                cas_banco,
                                email);
        }

        public string sce_dad_u03_MS(string id_party, byte id_dir)
        {
            return unitOfWork.SceRepository.sce_dad_u03_MS(id_party, id_dir);
        }
        public IList<sce_blin_s01_MS_Result> Sce_Blin_S01_MS(String idPrty)
        {
            return unitOfWork.SceRepository.sce_blin_s01_MS(idPrty);
        }
        public int Sce_Tgas_I01_MS(String id_party, String sistema, String producto, String etapa, Boolean? borrado, Boolean? m_tarifa, Decimal? monto)
        {
            return unitOfWork.SceRepository.sce_tgas_i01_MS(id_party, sistema, producto, etapa, borrado, m_tarifa, monto);
        }
        public int Sce_Tgas_U02_MS(String id_party, String sistema, String producto, String etapa, Boolean? borrado, Boolean? m_tarifa, Decimal? monto)
        {
            return unitOfWork.SceRepository.sce_tgas_u02_MS(id_party, sistema, producto, etapa, borrado, m_tarifa, monto);
        }
        public int Sce_Tgas_D02_MS(String id_party, String sistema, String producto, String etapa)
        {
            return unitOfWork.SceRepository.sce_tgas_d02_MS(id_party, sistema, producto, etapa);
        }

        public int Sce_Tint_I01_MS(String id_party, String sistema, String producto, String etapa, Boolean? borrado, Boolean? libor, Boolean? prime, Boolean? flotante, Decimal? tasa)
        {
            return unitOfWork.SceRepository.sce_tint_i01_MS(id_party, sistema, producto, etapa, borrado, libor, prime, flotante, tasa);
        }

        public int Sce_Tint_U01_MS(String id_party, String sistema, String producto, String etapa, Boolean? libor, Boolean? prime, Boolean? flotante, Decimal? tasa)
        {
            return unitOfWork.SceRepository.sce_tint_u01_MS(id_party, sistema, producto, etapa, libor, prime, flotante, tasa);
        }

        public int Sce_Tint_D02_MS(String id_party, String sistema, String producto, String etapa)
        {
            return unitOfWork.SceRepository.sce_tint_d02_MS(id_party, sistema, producto, etapa);
        }

        public int? Sce_Tcom_I01_MS(String id_party, String sistema, String producto, String etapa, Decimal? secuencia, Boolean? borrado, Boolean? manual_t, Boolean? monto_fijo, Decimal? tasa, Decimal? hasta_mon, Decimal? minimo, Decimal? maximo, DateTime? fecha)
        {
            return unitOfWork.SceRepository.sce_tcom_i01_MS(id_party, sistema, producto, etapa, secuencia, borrado, manual_t, monto_fijo, tasa, hasta_mon, minimo, maximo, fecha);
        }

        public int Sce_Tcom_U02_MS(String id_party, String sistema, String producto, String etapa, Decimal? secuencia, Boolean? borrado, Boolean? manual_t, Boolean? monto_fijo, Decimal? tasa, Decimal? hasta_mon, Decimal? minimo, Decimal? maximo, DateTime? fecha)
        {
            return unitOfWork.SceRepository.sce_tcom_u02_MS(id_party, sistema, producto, etapa, secuencia, borrado, manual_t, monto_fijo, tasa, hasta_mon, minimo, maximo, fecha);
        }

        public int Sce_Tcom_D02_MS(String id_party, String sistema, String producto, String etapa, Decimal? secuencia)
        {
            return unitOfWork.SceRepository.sce_tcom_d02_MS(id_party, sistema, producto, etapa, secuencia);
        }

        public int Sce_Blin_I01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activa, Decimal? moneda, String linea)
        {
            return unitOfWork.SceRepository.sce_blin_i01_MS(id_party, secuencia, borrado, activa, moneda, linea);
        }

        public int Sce_Blin_U01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activa, Decimal? moneda, String linea)
        {
            return unitOfWork.SceRepository.sce_blin_u01_MS(id_party, secuencia, borrado, activa, moneda, linea);
        }

        public int Sce_Bcta_I01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activa, Decimal? moneda, String cuenta, Boolean? especial)
        {
            return unitOfWork.SceRepository.sce_bcta_i01_MS(id_party, secuencia, borrado, activa, moneda, cuenta, especial);
        }

        public int Sce_Bcta_U01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activa, Decimal? moneda, String cuenta, Boolean? especial)
        {
            return unitOfWork.SceRepository.sce_bcta_u01_MS(id_party, secuencia, borrado, activa, moneda, cuenta, especial);
        }

        public int Sce_Ctas_I01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activabco, Boolean? activace, Boolean? extranjera, Decimal? moneda, String cuenta)
        {
            return unitOfWork.SceRepository.sce_ctas_i01_MS(id_party, secuencia, borrado, activabco, activace, extranjera, moneda, cuenta);
        }

        public int Sce_Ctas_U01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activabco, Boolean? activace, Boolean? extranjera, Decimal? moneda, String cuenta)
        {
            return unitOfWork.SceRepository.sce_ctas_u01_MS(id_party, secuencia, borrado, activabco, activace, extranjera, moneda, cuenta);
        }

        public string Sce_Usr_S09_MS(String cencos, String codusr)
        {
            return unitOfWork.SceRepository.Sce_Usr_S09_MS(cencos, codusr);
        }

        public IList<sce_usr_s04_MS_Result> Sce_Usr_S04_MS(String cencos, String codusr)
        {
            return unitOfWork.SceRepository.sce_usr_s04_MS(cencos, codusr);
        }

        public sce_memg_d01_MS_Result Sce_Memg_D01_MS(String tabla, String codMem)
        {
            return unitOfWork.SceRepository.sce_memg_d01_MS(tabla, codMem);
        }

        public sce_memg_i01_MS_Result Sce_Memg_I01_MS(String tabla, String codMem, String i, String lineas)
        {
            return unitOfWork.SceRepository.sce_memg_i01_MS(tabla, codMem, i, lineas);
        }

        public Int32? Sce_Memg_S03_MS(String Tabla, String m)
        {
            return unitOfWork.SceRepository.sce_memg_s03_MS(Tabla, m);
        }

        public IList<sce_trng_s01_MS_Result> Sce_Trng_S01_List_MS()
        {
            return unitOfWork.SceRepository.sce_trng_s01_list_MS();
        }

        public double Sce_Rng_U01_MS(String codcct, String codesp, String codfun)
        {
            var mensaje = string.Empty;
            return unitOfWork.SceRepository.sce_rng_u01_MS(codcct, codesp, codfun, out mensaje);
        }

        public IList<sce_ctas_s04_MS_Result> Sce_ctas_s04_MS(String id_party)
        {
            return unitOfWork.SceRepository.sce_ctas_s04_MS(id_party);
        }

        public int Sce_rsa_du01_MS(string idparty, string razonSocial, string nombreFantasia, string contacto, string sortKey, string creaCosto, string creaUser)
        {
            return unitOfWork.SceRepository.Sce_rsa_du01_MS(idparty, razonSocial, nombreFantasia, contacto, sortKey, creaCosto, creaUser);
        }

        //INICIO MODIFICACION CNC - ACCENTURE 

        public List<String> ObtenerDatosClasificacionCliente(String rutPrty, String credU, String credP, String kDec)
        {
           
            return XGPYServices.ObtenerDatosClasificacionCliente(rutPrty, credU, credP, kDec).ToList();
        }

        public string sce_cla_cliente_w01(String id_party, String clasif_riesgo, String act_econ, String eval_riesgo, String compos_inst, String tpo_clte_norm)
        {
            return unitOfWork.SceRepository.sce_cla_cliente_w01(id_party, clasif_riesgo, act_econ, eval_riesgo, compos_inst, tpo_clte_norm);
        }

        public IList<sce_campos_cla_cliente_S01_Result> ObtenerDetalleClasifCliente(byte id_campo, string cod_campo, string des_campo)
        {
            return unitOfWork.SgtRepository.sce_campos_cla_cliente_S01(id_campo, cod_campo, des_campo);
        }

        public IList<sce_cred_cla_cliente_S01_Result> ObtenerCredClasifCliente()
        {
            return unitOfWork.SgtRepository.sce_cred_cla_cliente_S01();
        }

        //FIN MODIFICACION CNC - ACCENTURE
    }
}