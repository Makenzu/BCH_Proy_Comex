using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGCN.Forms;
using BCH.Comex.Core.BL.XGCN.Modulos;
using BCH.Comex.Core.Entities.Cext01.Devengo;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.IO;

namespace BCH.Comex.Core.BL.XGCN
{
    public partial class XgcnService: IDisposable
    {
        /// <summary>
        /// Inicia la aplicación con datos iniciales de configuración
        /// </summary>
        /// <param name="datosUsuario"></param>
        /// <returns></returns>
        public DatosGlobales Iniciar(IDatosUsuario datosUsuario)
        {
            using (Tracer tracer = new Tracer("XgcnService - Iniciar"))
            {
                DatosGlobales globales = new DatosGlobales();
                globales.DatosUsuario = datosUsuario;

                string centroCostoUsuario = globales.DatosUsuario.Identificacion_CCtUsr.Substring(0, 3);
                string codigoUsuario = globales.DatosUsuario.Identificacion_CCtUsr.Substring(
                    globales.DatosUsuario.Identificacion_CCtUsr.Length - 2);

                if (!MODGUSR.SyGet_Usr(centroCostoUsuario, codigoUsuario, globales, uow))
                    return null;

                if (!MODGUSR.SyGet_OfiUsr(centroCostoUsuario, codigoUsuario, globales.MODGUSR, uow))
                    return null;

                // Verifica que se haya hecho Inicio de Día Hoy.-
                if (globales.MODGUSR.UsrEsp.tipeje == "O")
                {
                    if (!MODGUSR.SyGetf_Usr(globales.MODGUSR, globales, uow, centroCostoUsuario, codigoUsuario, "I"))
                    {
                        return null;
                    }
                }

                // Identifica Usuario Original.-
                //globales.MODGUSR.UsrEsp.cctorig = datosUsuario.Identificacion_CCtUsro.Left(3);

                //UsrOrig = MODGPYF0.GetSceIni("Identificacion", "CCtUsro");
                //        UsrEsp.CCtOrig = UsrOrig.Left(3);
                //        UsrEsp.EspOrig = UsrOrig.Right(2);

                //        // Reemplzaos del Usuario Original.-
                //        UsrEsp.RempOrig = SyGet_RempOrig(UsrEsp.CCtOrig, UsrEsp.EspOrig);

                return globales;
            }
        }

        private UnitOfWorkCext01 uow;
        public XgcnService()
        {
            uow = new UnitOfWorkCext01();
        }

        //public IEnumerable<string> SyGetn_Trasp(string cCtUsr, string codusr)
        //{
        //    return MODTRASP.SyGetn_Trasp(cCtUsr, codusr, uow);
        //}

        //public int SyPut_Trasp(string cCtUsr, string codusr, string reemplazos)
        //{
        //    return MODTRASP.SyPut_Trasp(cCtUsr, codusr, reemplazos, uow);
        //}

        
        public void Dispose()
        {
            if (uow != null)
            {
                uow.Dispose();
            }
        }

        #region Diferencia Devengamiento

        public void FrmDifDevInit(DatosGlobales globales) 
        {
            FrmDifDev.Form_Load(globales, uow);
        }

        public MemoryStream FrmDifDevFile(List<T_DifDev> datos) 
        {
            return FrmDifDev.getFile(datos);
        }
        #endregion

        #region Consulta Devengamiento

        public void FrmDevengIntReajInit(DatosGlobales globales)
        {
            FrmDevengIntReaj.Form_Load(globales.DatosIntReaj, globales.ListaMensajesError, uow);
        }
        public bool btnBuscar_click(DatosGlobales globales) 
        { 
            return FrmDevengIntReaj.CmdBuscar_Click(globales.DatosIntReaj,globales.ListaMensajesError,uow);
        }

        public MemoryStream FrmDevengIntReajFileConsulta(List<T_DevengInt> datos, string Titulo)
        {
            return FrmDevengIntReaj.getFileConsultaIntereses(datos,Titulo);
        }

        public MemoryStream FrmDevengIntReajFileConsulta(List<T_DevengReaj> datos, string Titulo)
        {
            return FrmDevengIntReaj.getFileConsultaReajustes(datos, Titulo);
        }

        #endregion

        #region Devengamiento Historico

        public void FrmDevHistInit(T_DevHist devHist,IList<UI_Message> listaMensajes)
        {
            FrmDevHist.Form_Load(devHist, listaMensajes, uow);
        }

        public bool FrmDevHist_btnBuscar_click(T_DevHist devHist, IList<UI_Message> listaMensajes)
        {
            return FrmDevHist.CmdBuscar_Click(devHist, listaMensajes, uow);
        }

        public MemoryStream FrmDevHistGetFile(T_DevHist devHist)
        {
            return FrmDevHist.getFileDevengoHistoricos(devHist.Deveng);
        }
        #endregion

        #region Operaciones CDR
        public void FrmCDR_btn_Buscar(T_CDR cdr, IList<UI_Message> listaMensajes)
        {
            FrmCDR.CmdBuscar_Click(cdr, listaMensajes, uow);
        }

        public MemoryStream FrmCDRGetFile(IList<T_DvgCred_Excel> datos, IList<UI_Message> listaMensajes)
        {
            return FrmCDR.getFrmCDRFile(datos, listaMensajes);
        }
        #endregion

        #region Consulta Interfaz CDR

        public void FrmInformeInit(T_INFORME infor, IList<UI_Message> listaMensajes)
        {
            Frminforme.Form_Load(infor, listaMensajes, uow);
        }

        public void FrmInforme_obtenerDias(T_INFORME infor, IList<UI_Message> listaMensajes)
        {
            Frminforme.ObtieneDiasDisponibles(infor, listaMensajes, uow);
        }

        public bool FrmInforme_buscar(T_INFORME infor, IList<UI_Message> listaMensajes)
        {
            return Frminforme.CmdBuscar_Click(infor, listaMensajes,uow);
        }

        public MemoryStream FrmInforme_GetFileCartera(IList<T_CDR_Cartera> datos)
        {
           return Frminforme.getFileCarteraCDR(datos);
        }

        public MemoryStream FrmInforme_GetFileDevengo(IList<T_CDR_Deveng> datos)
        {
            return Frminforme.getFileDevengoCDR(datos);
        }
        #endregion

        #region Consulta Cuentas Contables
        public void FrmCtaCte_Buscar(T_CTACTE ctacte, IList<UI_Message> listaMensajes)
        {
            FrmCtaCte.CmdBuscar_Click(ctacte, listaMensajes, uow);
        }

        public MemoryStream FrmCtaCte_GetFile(IList<T_DvgCta> datos, IList<UI_Message> listaMensajes ) 
        {
            return FrmCtaCte.getFrmCtaCteFile(datos, listaMensajes);
        }
        #endregion
    }
}
