using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGGL.Forms;
using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Data.DAL.Swift;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL
{
    public partial class XgglService : IDisposable
    {
        private UnitOfWorkCext01 uow;
        private UnitOfWorkSwift uowSwift;
        private int num_participantes = 0;
        public XgglService()
        {
            uow = new UnitOfWorkCext01();
            uowSwift = new UnitOfWorkSwift();
        }

        public void Dispose()
        {
            if (uow != null)
            {
                uow.Dispose();
            }
            if (uowSwift != null)
            {
                uowSwift.Dispose();
            }

        }

        /// <summary>
        /// Inicia la aplicación con datos iniciales de configuración
        /// </summary>
        /// <param name="datosUsuario"></param>
        /// <returns></returns>
        public DatosGlobales Iniciar(IDatosUsuario datosUsuario)
        {
            using (Tracer tracer = new Tracer("XgglService - Iniciar"))
            {
                //DatosGlobales globales = new DatosGlobales();
                //globales.DatosUsuario = datosUsuario;

                //string centroCostoUsuario = globales.DatosUsuario.Identificacion_CCtUsr.Substring(0, 3);
                //string codigoUsuario = globales.DatosUsuario.Identificacion_CCtUsr.Substring(
                //    globales.DatosUsuario.Identificacion_CCtUsr.Length - 2);

                //if (!MODGSUP.SyGet_Usr(centroCostoUsuario, codigoUsuario, globales, uow))
                //    return null;

                //if (!MODGUSR.SyGet_OfiUsr(centroCostoUsuario, codigoUsuario, globales, uow))
                //    return null;
                var res = MODGL.Main(this.uow, datosUsuario);
                res.gl = new Entities.Cext01.ContabilidadGenerica.UI_Forms.UI_gl();
                Frm_gl.Form_Load(res, this.uow);
                return res;
            }
        }

        #region gl
        public void NuevaOperacion(DatosGlobales Globales)
        {

            UI_gl gl = Globales.gl;
            if (gl.m_e.Items.Count != 0 || gl.m_n.Items.Count != 0)
            {
                //a = MigrationSupport.Utils.MsgBox("Desea grabar los cambios ?", MODGPYF0.pito(35).Cast<MigrationSupport.MsgBoxStyle>(), GLOBAL.Appl_Descripcion);
                if (!Globales.vieneDeMsg)
                {
                    Globales.Action = "GuardarCambiosOperacion";
                    Globales.Controller = "Mensajes";
                    Globales.VieneDeAction = "Operacion";
                    Globales.VieneDeController = "Nueva";
                    Globales.vieneDeMsg = true;
                    return;
                }
                else
                {
                    Globales.vieneDeMsg = false;
                    if (Globales.resMsg)
                    {
                        Globales.resMsg = false;
                        //Grabar_Inicial(Globales);
                    }
                    else
                    {
                        Globales.Action = "Index";
                        Globales.Controller = "Home";
                        return;
                    }
                }
            }

            //reinicio este porque me quedan los swifts colgados
            Globales.MODXSWF = new T_MODXSWF();


            // limpia_servidores True
            gl.debe_pedir = true.ToInt();
            if (Frm_gl.inicializa(Globales, this.uow))
            {
                Globales.Action = "Index";
                Globales.Controller = "Home";
                Globales.VieneDeAction = "";
                Globales.VieneDeController = "";
                Globales.vieneDeMsg = false;
            }

        }

        private void LimpiarGlobales(DatosGlobales globales)
        {
            throw new NotImplementedException();
        }

        public void NuevaOperacion_2(DatosGlobales Globales)
        {
            if (Frm_gl.inicializa_2(Globales, this.uow))
            {
                Globales.Action = "Index";
                Globales.Controller = "Home";
                Globales.VieneDeAction = "";
                Globales.VieneDeController = "";
                Globales.vieneDeMsg = false;
            }
        }

        public void GL_Moneda_Click(DatosGlobales Globales)
        {
            Frm_gl.monedas_Click(Globales, this.uow);
        }

        public void GL_Monto_Blur(DatosGlobales Globales)
        {
            Frm_gl.monto_click(Globales);
        }

        public void GL_DebeHaber_Change(DatosGlobales Globales, int index)
        {
            Frm_gl.tipo_Click(Globales, this.uow, index);
        }

        public void GL_Nemonico_Blur(DatosGlobales Globales)
        {
            Frm_gl.nemonico_lostfocus(Globales, this.uow);
        }

        public void GL_Ver_1_2(DatosGlobales Globales)
        {
            Globales.Frm_Cta = new UI_Frm_Cta();
            var irAVer = Frm_gl.Ver_Click_1_2(Globales, this.uow);
            if (irAVer)
            {
                Globales.Controller = "Plan_De_Cuentas";
                Globales.Action = "Ver";
                Globales.VieneDeController = "Home";
                Globales.VieneDeAction = "Ver_2";
            }
            else
            {
                Globales.Action = "Index";
            }
        }

        public void GL_Ver_2_2(DatosGlobales Globales)
        {
            Frm_gl.Ver_Click_2_2(Globales, this.uow);
            //Frm_gl.nemonico_lostfocus(Globales, this.uow);
            Globales.Action = "Index";
            Globales.VieneDeController = String.Empty;
            Globales.VieneDeAction = String.Empty;
        }

        public void GL_OK_Click(DatosGlobales Globales, bool saltarValidacionSaldo = false)
        {
            Frm_gl.Aceptar_Click(Globales, this.uow, saltarValidacionSaldo);
            if (String.IsNullOrEmpty(Globales.Action))
            {
                Globales.Action = "Index";
            }
        }

        public void GL_OK_Click_DespuesDeParticipantes(DatosGlobales Globales)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            int n = 0;
            n = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.GetParty_1_2(Globales, GLOBAL.Beneficiario, SYGETPRT.Codop, true.ToInt(), false.ToInt());
            if (n == 0)
            {
                Globales.SYGETPRT.PartysOpe[T_GLOBAL.I_Cli] = Globales.gl.prty_cliente;
            }
            Frm_gl.Aceptar_Click_2(Globales, this.uow);
        }

        public void GL_Aceptar_GOV_1(DatosGlobales Globales)
        {
            Frm_gl.Aceptar_Despues_GOV_1(Globales);
            Globales.Action = "Index";
        }

        public void GL_Aceptar_GOV_2(DatosGlobales Globales)
        {
            Frm_gl.Aceptar_Despues_GOV_2(Globales, this.uow);
        }

        public void GL_CuentaDebe_GOV(DatosGlobales Globales)
        {
            Frm_gl.CuentaDebe_Despues_GOV(Globales, this.uow);
            Globales.Action = "Index";
        }

        public void GL_SyLeeCuentas_GOV_Debe(DatosGlobales Globales)
        {
            bool estaBien = Frm_gl.SyLeeCuentas_Despues_GOV_Debe(Globales, this.uow);
            if (estaBien)
            {
                Globales.Action = "Index";
            }
            else
            {
                Globales.Action = "Aceptar_Monto";
            }
        }

        public void GL_CuentaHaber_GOV(DatosGlobales Globales)
        {
            Frm_gl.CuentaHaber_Despues_GOV(Globales, this.uow);
            Globales.Action = "Index";
        }

        public void GL_SyLeeCuentas_GOV_Haber(DatosGlobales Globales)
        {
            bool estaBien = Frm_gl.SyLeeCuentas_Despues_GOV_Haber(Globales, this.uow);
            if (estaBien)
            {
                Globales.Action = "Index";
            }
            else
            {
                Globales.Action = "Aceptar_Monto";
            }
        }

        public void GL_m_n_DoubleClick(DatosGlobales Globales)
        {

            Frm_gl.m_n_dblclick(Globales);
        }

        public void GL_m_e_DoubleClick(DatosGlobales Globales)
        {

            Frm_gl.m_e_dblclick(Globales);
        }

        public void GL_m_n_Click(DatosGlobales Globales)
        {
            Frm_gl.m_n_Click(Globales);
        }

        public void GL_m_e_Click(DatosGlobales Globales)
        {
            Frm_gl.m_e_Click(Globales);
        }

        public void GL_Cancelar(DatosGlobales Globales)
        {
            Frm_gl.Cancelar_Click(Globales);
            Globales.Action = "Index";
        }

        #endregion

        #region PARTYS
        public void Partys_1_2(DatosGlobales Globales)
        {
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            if (Globales.GetPrty0 != null && Globales.GetPrty0.LOADED)
            {
                if (Globales.RAZON_SOCIAL != null)
                    Globales.GetPrty0.Llave.Text = Globales.RAZON_SOCIAL;
                return;
            }
            num_participantes = 0;

            if (GLOBAL.Operacion_aso.Cent_costo != "")
            {
                if (SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].Status < T_SYGETPRT.GPrt_StatConPago)
                {
                    SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].Status = T_SYGETPRT.GPrt_StatConPago;
                }
            }

            int res = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.GetParty_1_2(Globales, GLOBAL.Beneficiario, SYGETPRT.Codop, true.ToInt(), false.ToInt());
            GetPrty0_Logic.Form_Load(Globales);
        }

        public void Partys_2_2(DatosGlobales Globales)
        {
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            num_participantes = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.GetParty_2_2(Globales, GLOBAL.Beneficiario, SYGETPRT.Codop, true.ToInt(), false.ToInt());
            if (GLOBAL.Operacion_aso.Cent_costo == "")
            {
                Globales.gl.Num_Op.Text = "";
            }

            if (num_participantes > 0)
            {
                string nombreCliente = (string.IsNullOrWhiteSpace(Globales.gl.prty_cliente.NombreUsado) ? SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].NombreUsado : Globales.gl.prty_cliente.NombreUsado);
                Globales.gl.Cliente.Text = nombreCliente; // se sustituye por usuario inicial SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].NombreUsado;
                Globales.gl.datos.Enabled = true;
                Globales.gl.frame_ext.Enabled = true;
                Globales.gl.frame_nac.Enabled = true;
                //Variable que viene del botón ticket del Home de Contabilidad Genérica.
                if (Globales.vieneDeMsg == true)
                {
                    GL_OK_Click_DespuesDeParticipantes(Globales);
                }
            }
            else
            {
                Globales.gl.Cliente.Text = "";
                Globales.gl.datos.Enabled = false;
                Globales.gl.frame_ext.Enabled = false;
                Globales.gl.frame_nac.Enabled = false;
            }
            if (Globales.Action != "Adicionales")
            {

                Globales.Action = "Index";
                Globales.Controller = "Home";
                Globales.GetPrty0 = null;
            }
            else
            {
                Globales.Action = "Adicionales";
                Globales.Controller = "Home";
                Globales.VieneDeAction = Globales.gl.tipo_000.Checked ?
                    "Aceptar_Despues_CuentaDebe_Adicionales" : "Aceptar_Despues_CuentaHaber_Adicionales";
                Globales.GetPrty0 = null;
            }
        }

        public void Partys_Bot_Nem_Click(DatosGlobales Globales)
        {
            Globales.PrtEnt09 = new UI_PrtEnt09();
        }
        public void Partys_Bot_Nem_Post_Click(DatosGlobales Globales)
        {

        }

        public void Partys_Identificar_Click_1_2(DatosGlobales Globales)
        {
            bool seguirEjecutando = GetPrty0_Logic.Identificar_Click_1_2(Globales, this.uow);
            if (!seguirEjecutando && string.IsNullOrEmpty(Globales.Action))
            {
                GetPrty0_Logic.Identificar_Click_2_2(Globales, this.uow);
            }
            else if (Globales.Action == "Preguntar")
            {
                Globales.GetPrty2 = new UI_GetPrty2();
                Globales.GetPrty0.Redireccionar = "Participantes/Preguntar";
            }
            else if (Globales.Action == "Crear")
            {
                Globales.GetPrty0.Redireccionar = "Participantes/Crear";
            }
            else if (Globales.GetPrty0.AbrirIdentParticipantes)
            {

            }
        }

        public void Partys_Preguntar_Load(DatosGlobales Globales)
        {
            Globales.GetPrty2.Text = T_SYGETPRT.GPrt_Caption + "[" + Globales.GetPrty0.Llave.Text + "]";
        }
        public void Partys_Consultar_Participante(DatosGlobales obj)
        {
            this.Partys_Bot_Nem_Click(obj);
            this.Partys_Consultar_Load(obj);
        }

        public void Partys_Crear_Participante(DatosGlobales Globales)
        {
            if (Globales.GetPrty3 == null)
                Globales.GetPrty3 = new UI_GetPrty3();
            GetPrty3_Logic.Form_Load(Globales, this.uow);
        }

        public void partys_crear_aceptar(DatosGlobales Globales)
        {
            GetPrty3_Logic.Aceptar_Click(Globales);
        }

        public void Partys_Crear_Aceptar_Post(DatosGlobales Globales)
        {
            GetPrty0_Logic.Identificar_Click_2_2(Globales, this.uow);
        }

        public void partys_crear_es_banco_checked(DatosGlobales Globales)
        {
            GetPrty3_Logic.EsBanco_Click(Globales);
        }

        public void Partys_LstPartys_Click(DatosGlobales globales)
        {
            GetPrty0_Logic.LstPartys_Click(globales);
        }

        public void Partys_Eliminar(DatosGlobales Globales)
        {
            GetPrty0_Logic.Eliminar_Click(Globales);
        }

        public void Partys_Donde_Click(DatosGlobales Globales)
        {
            GetPrty0_Logic.Donde_Click(Globales);
        }

        public void Partys_Consultar_Load(DatosGlobales Globales)
        {
            Globales.SYGETPRT.KeyPrt = String.Empty;
        }

        public void Partys_Consultar_Buscar(DatosGlobales Globales)
        {
            PrtEnt09_Logic.OK_Click(Globales, this.uow);
        }

        public void Partys_Submit(DatosGlobales Globales)
        {
            if (Globales.Tag == "Cancelar")
            {
                Globales.GetPrty0.Aceptar.Tag = "0";
            }
            else if (Globales.Tag == "Aceptar")
            {
                GetPrty0_Logic.Aceptar_Click(Globales);
                if (Globales.MESSAGES.Count > 0)
                {
                    Globales.GetPrty0.ListaErrores = new List<Comex.Common.UI_Modulos.UI_Message>(Globales.MESSAGES);
                    Globales.MESSAGES.Clear();

                    return;
                }
            }
            this.Partys_2_2(Globales);
        }

        public void Partys_ParticipantesIdentificar_Aceptar(DatosGlobales Globales)
        {
            GetPrty1_Logic.Aceptar_Click(Globales);
            GetPrty0_Logic.Identificar_Click_2_2(Globales, this.uow);
        }
        #endregion

        #region GRABAR

        public ReporteContable GetReporteContable(DatosGlobales Globales, int nroReporte, DateTime fecha, IList<sce_dfc> descripcionesFunciones)
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
                    linea.numcct = FormatearNroDeCuenta(Globales, linea.numcct, Convert.ToInt16(linea.codmon));
                    int indiceCuenta = MODGNCTA.Get_Cta(linea.nemcta, Globales, uow);
                    linea.NombreCuenta = Globales.MODGNCTA.VCta[indiceCuenta].Cta_Nom;
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

        private string FormatearNroDeCuenta(DatosGlobales Globales, string nroCuenta, short codMoneda)
        {
            if (!String.IsNullOrEmpty(nroCuenta))
            {
                if (nroCuenta.Length > 8 && !Globales.MODXORI.gb_esCosmos)
                {
                    if (codMoneda == Globales.MODGSCE.VGen.MndNac)
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
                    string descFecha = (linea.fecven == DateTime.MinValue ? String.Empty : " Vto: " + linea.fecven.ToString("dd/MM/yyyy"));

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

        public IList<sce_dfc> GetDescripcionesFuncionesContables()
        {
            return uow.SceRepository.GetDescripcionesFuncionesContables();
        }

        public void Grabar_Inicial(DatosGlobales Globales)
        {
            var vaASwift = Frm_gl.Grabar_Inicial(Globales, this.uow);
            if (vaASwift)
            {
                Globales.Action = "Swift";
                Globales.Controller = "Grabar";
            }
            else
            {
                Globales.Action = "Index";
                Globales.Controller = "Home";

            }
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

        public void Grabar_Generar_Swift_1(DatosGlobales Globales)
        {
            var vaASwift = Frm_gl.Grabar_Generar_Swift_1_3(Globales, this.uow);
            if (!vaASwift)
            {
                Globales.Action = "Cheques";
            }
            else
            {
                Globales.Action = "GenerarSwift";
                Globales.Controller = "GenerarSwift";
            }
        }

        public void Grabar_Generar_Swift_2(DatosGlobales Globales)
        {
            var vaAMT = Frm_gl.Grabar_Generar_Swift_2_3(Globales, this.uow);
            if (vaAMT)
            {
                Globales.Controller = "";
                Globales.Action = "Despues_De_MT";
            }
            else
            {
                Globales.Controller = "Home";
                Globales.Action = "Index";
            }
        }

        public void Grabar_Generar_Swift_3(DatosGlobales Globales)
        {
            Frm_gl.Grabar_Generar_Swift_3_3(Globales, this.uow);
            Globales.Action = "Cheques";
        }

        public void Grabar_Generar_Cheques_1_2(DatosGlobales Globales)
        {
            var vaACheques = Frm_gl.Grabar_Generar_Cheque_1_2(Globales, this.uow);
            if (vaACheques)
            {
                Globales.Controller = "Cheque";
                Globales.Action = "Emision";
            }
            else
            {
                Globales.Action = "Tickets";
            }
        }

        public void Grabar_Generar_Cheques_2_2(DatosGlobales Globales)
        {
            var vaATickets = Frm_gl.Grabar_Generar_Cheque_2_2(Globales, this.uow);
            if (vaATickets)
            {
                Globales.Action = "Tickets";
            }
            else
            {
                Globales.Controller = "Home";
                Globales.Action = "Index";
            }
        }

        public void Grabar_Ticket(DatosGlobales Globales)
        {
            short vaATicket = Frm_gl.Grabar_Ticket(Globales, this.uow);
            if (vaATicket == 1)
            {
                Globales.Controller = "Tickets";
                Globales.Action = "Ingreso";
            }
            else if (vaATicket == 0)
            {
                Globales.Action = "Final";
            }
            else if (vaATicket == -1)
            {
                Globales.Action = "Tickets";
            }
        }

        public void Grabar_Final(DatosGlobales Globales)
        {
            string re = Globales.STR_TICKETS;
            Globales.STR_TICKETS = String.Empty;
            Globales.gl.INDICE_TICKET = 0;
            var ok = Frm_gl.Grabar_Final(Globales, this.uow, re);
            if (ok && Globales.DocumentosAImprimir.Count > 0)
            {
                Globales.Action = "Imprimir";
                Globales.Controller = "Grabar";
            }
            else
            {
                Globales.Controller = "Home";
                Globales.Action = "Index";
            }
        }

        #endregion

        #region TICKETS
        public void Ticket_Load(DatosGlobales Globales)
        {
            Tickets_Logic.Form_Load(Globales, this.uow);
        }
        public void Ticket_Aceptar(DatosGlobales Globales)
        {
            Tickets_Logic.Aceptar_Click(Globales, this.uow);
        }
        public void Ticket_Cancelar(DatosGlobales Globales)
        {
            Tickets_Logic.Cancelar_Click(Globales, this.uow);
        }
        public void Ticket_Otro_Click(DatosGlobales Globales)
        {
            Tickets_Logic.otro_Click(Globales);
        }
        #endregion

        #region FRM_CTA
        public void CTA_Form_Load(DatosGlobales Globales)
        {
            Frm_Cta_Logic.Form_Load(Globales);
        }

        public void CTA_Aceptar(DatosGlobales Globales)
        {
            Frm_Cta_Logic.bot_acep_Click(Globales);
        }

        public void CTA_Cancelar(DatosGlobales Globales)
        {
            Globales.Controller = "Home";
            Globales.Action = "Index";
        }
        #endregion

        #region GLOV
        public void GLOV_FormLoad(DatosGlobales Globales)
        {
            FrmGLOV_Logic.Form_Load(Globales, this.uow);
        }

        public void GLOV_Tx_Datos_Blur(DatosGlobales Globales, int index)
        {
            FrmGLOV_Logic.Tx_Datos_KeyPress(Globales, this.uow, index);
            FrmGLOV_Logic.Tx_Datos_LostFocus(Globales, this.uow, index);
        }

        public void GLOV_Boton(DatosGlobales Globales, int index)
        {
            FrmGLOV_Logic.Boton_Click(Globales, this.uow, index);
        }

        public void GLOV_Fin(DatosGlobales Globales)
        {
            Globales.Action = Globales.VieneDeAction;
            Globales.Controller = Globales.VieneDeController;
            Globales.VieneDeAction = String.Empty;
            Globales.VieneDeController = String.Empty;
        }
        #endregion

        #region Actualizar Impresion Contabilidad Generica

        /// <summary>
        /// Mapea la configuracion de impresion de cartas y contabilidad a los controles de la pantalla
        /// </summary>
        /// <param name="globales"></param>
        public void IndexMapearConfigImprimirAControles(DatosGlobales globales)
        {
            if (globales.DatosUsuario.ConfigImpres_ContabilidadGenerica != null &&
                globales.DatosUsuario.ConfigImpres_ContabilidadGenerica.Split(';').Length == 2)
            {
                globales.gl.ChkImpresionCartas.Checked = globales.DatosUsuario.ConfigImpres_ContabilidadGenerica.Split(';')[0] == "-1";
                globales.gl.ChkImpresionContabilidad.Checked = globales.DatosUsuario.ConfigImpres_ContabilidadGenerica.Split(';')[1] == "-1";
            }
        }

        /// <summary>
        /// Mapea los datos de impresiond e cartas y contabilidad desde los contorls a DatosUsuario y a BD
        /// </summary>
        /// <param name="datosGlobales"></param>
        public void IndexMapearConfigImprimirADatosUsuario(DatosGlobales datosGlobales)
        {
            string ConfigImpres_ContabilidadGenerica_Cartas = datosGlobales.gl.ChkImpresionCartas.Checked ? "-1" : "0";
            string ConfigImpres_ContabilidadGenerica_Contabilidad = datosGlobales.gl.ChkImpresionContabilidad.Checked ? "-1" : "0";
            datosGlobales.DatosUsuario.ConfigImpres_ContabilidadGenerica = ConfigImpres_ContabilidadGenerica_Cartas + ";" + ConfigImpres_ContabilidadGenerica_Contabilidad;

            uow.ActualizarImpresionContabilidadGenerica(datosGlobales.DatosUsuario);
        }

        #endregion
    }
}
