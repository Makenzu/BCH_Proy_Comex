using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.BL.XGGL.Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.UI_Forms;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Forms
{
    public static class Frm_gl
    {
        public static void Form_Load(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            T_MODGOVD MODGOVD = Globales.MODGOVD;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            UI_gl gl = Globales.gl;
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            T_MODGMTA MODGMTA = Globales.MODGMTA;

            gl.prty_cliente = new PartyKey();
            gl.PartyVacio = new PartyKey();
            int iNewIndex = 0;
            int a = 0;
            int[] tabs = null;
            int i = 0;

            MODGOVD.Gvar_NotaCredito = 0;
            ventana_NOTACREDITO(Globales, 0);

            // 
            gl.EnLoad = Convert.ToInt16(true);
            if (MODGUSR.UsrEsp.Tipeje == "O")
            {
                gl.menu_anulaGL.Enabled = true;

            }

            // llena_moneda monedas
            // Carga las monedas en una lista.-
            if (MODGTAB0.VMnd.Any())
            {
                gl.monedas.Items.Clear();
                for (i = 1; i <= MODGTAB0.VMnd.GetUpperBound(0); i++)
                {
                    gl.monedas.AddItem(MODGTAB0.VMnd[i].Mnd_MndCod, MODGPYF1.Minuscula(MODGTAB0.VMnd[i].Mnd_MndNom));
                }
            }
            else
            {
                Globales.MESSAGES.Add(new Common.UI_Modulos.UI_Message()
                {
                    Type = Common.UI_Modulos.TipoMensaje.Error,
                    Text = "Se ha producido un error al tratar de cargar los datos de Monedas, la colección no trae registros.",
                    AutoClose = true
                });
            }

            gl.delista = Convert.ToInt16(false);

            // Deshabilita el boton del Impuesto en caso de que el flag este en 0 ------   JFO  28/03/2007
            if (MODGMTA.impflag == 0)
            {
                gl.Impuesto.Enabled = false;
                gl.Impuesto.Checked = false;
            }
            inicializa(Globales, unit);
            gl.EnLoad = Convert.ToInt16(false);
        }

        public static bool inicializa(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            T_MODGASO MODGASO = Globales.MODGASO;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            T_MODGSWF MODGSWF = Globales.MODGSWF;
            T_MODGOVD MODGOVD = Globales.MODGOVD;
            T_MODCTA MODCTA = Globales.MODCTA;

            UI_gl gl = Globales.gl;
            string ofi = string.Empty;
            int i = 0;
            int fin = 0;
            int a = 0;

            MODGASO.VgAso = MODGASO.VgAsoNul.Copy();

            MODGL.carga_benef(Globales);
            a = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.ResetParty(Globales, GLOBAL.Beneficiario);
            if (Globales.GetPrty0 != null)
            {
                Globales.GetPrty0.LOADED = false;
            }
            SYGETPRT.PartysOpe[1].Status = T_SYGETPRT.GPrt_StatDatos;     // Tercero Opcional.-
            gl.Glosa = "";
            gl.delista = Convert.ToInt16(false);
            gl.prty_cliente = gl.PartyVacio.Copy();

            MOD_ADIC.partidas = new tipo_partidas[1] { new tipo_partidas() };
            CONTAB01.Est_Contab = new Tipo_Contabilidad[1] { new Tipo_Contabilidad() };
            Globales.MODMMT = new T_MODMMT();

            fin = -1;

            fin = MOD_ADIC.Numeros.GetUpperBound(0);


            for (i = 0; i <= fin; i += 1)
            {
                MOD_ADIC.Numeros[i].listo = Convert.ToInt16(true);
            }

            CdOper LaOperacion = new CdOper();
            GLOBAL.Operacion_aso = LaOperacion;
            GLOBAL.Referencia_Imp = "";

            // If SgteNumOperacion(codop.Id_Operacion, CdgOp_CONTgl, False) Then
            //    a% = qedisconnect(hdbc)
            //    End
            // End If
            // Poly
            if (MODGUSR.UsrEsp.Tipeje == "O")
            {
                gl.Bot_Salvar.Enabled = true;
            }
            gl.ya_retorno = Convert.ToInt16(false);
            gl.en_salvar = Convert.ToInt16(false);
            gl.en_swift = Convert.ToInt16(false);
            gl.en_docval = Convert.ToInt16(false);
            GLOBAL.acepto_swift = Convert.ToInt16(false);
            GLOBAL.acepto_docval = Convert.ToInt16(false);
            gl.desde_lista = Convert.ToInt16(false);

            gl.monedas.ListIndex = -1;
            gl.monto.Text = "";
            gl.nemonico.Text = "";
            gl.Tx_NroFac.Text = "";
            gl.Tx_moneda.Text = "";
            gl.Tx_tipo.Text = "";
            gl.Tx_neto.Text = "";
            gl.Tx_iva.Text = "";
            gl.Tx_MtoOri.Text = "";
            gl.Num_Op.Text = "";
            gl.cambiar.Checked = false;
            gl.tipo[0].Checked = true;
            gl.tipo[1].Checked = false;
            gl.Tx_ReferenciaCliente.Text = string.Empty;
            gl.Cliente.Text = "";

            gl.m_e.Items.Clear();
            gl.m_n.Items.Clear();
            gl.me_sort.Items.Clear();
            gl.mn_sort.Items.Clear();
            gl.Dic_MN.Clear();
            gl.Dic_ME.Clear();


            gl.monto.Enabled = false;
            gl.nemonico.Enabled = false;
            gl.ver.Enabled = false;
            gl.aceptar.Enabled = false;
            gl.cancelar.Enabled = false;
            gl.tipo[0].Enabled = false;
            gl.tipo[1].Enabled = false;
            gl.cambiar.Enabled = false;

            gl.m_e.Enabled = false;
            gl.m_n.Enabled = false;
            gl.datos.Enabled = false;

            gl.hab_swift = Convert.ToInt16(false);
            gl.hab_docval = Convert.ToInt16(false);

            //Se limpian las variables de nota de credito
            MODGOVD.Gvar_NotaCredito = 0;
            MODCTA = new T_MODCTA();

            //se limpian los datos de la estructura de los ticket
            Globales.MODGTIC = new T_MODGTIC();

            if (gl.debe_pedir != 0)
            {
                if (MODGRNG.SgteNumOpr(Globales, unit, T_MODGUSR.IdPro_ConGen, T_MODGRNG.Rng_ContGL) == 0)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Para ingresar nuevos movimientos contables, deberá esperar que se restablezca la comunicación con el servidor.",
                        Type = TipoMensaje.Error
                    });
                    return false;
                }
                else
                {
                    // *************************************************
                    // 30/07/98: Modificado para Oficinas (1ºConcepción)
                    // *************************************************
                    ofi = MODGUSR.UsrEsp.OfixUser.TrimB();
                    if (String.IsNullOrEmpty(ofi))
                    {
                        throw new BCH.Comex.Common.Exceptions.ComexApplicationException("No se pudo encontrar la Oficina del Especialista. Comuníquese con el Administrador del Sistema.");
                        //Globales.MESSAGES.Add(new UI_Message()
                        //{
                        //    Type = TipoMensaje.Error,
                        //    Text = "No se pudo encontrar la Oficina del Especialista. Comuníquese con el Administrador del Sistema."
                        //});
                        //return false;
                    }
                    else
                    {
                        if (ofi.InStr(";", 1, StringComparison.CurrentCulture) == 0 && ofi.Length == 3)
                        {
                            SYGETPRT.Codop.Id_Empresa = ofi;
                        }
                        else
                        {
                            Globales.Action = "SeleccionOficina";
                            Globales.Controller = "SeleccionOficina";
                            Globales.VieneDeAction = "Index";
                            Globales.VieneDeController = "Home";
                            //FrmjOfi.DefInstance.ShowDialog();
                            return false;
                        }
                        if (SYGETPRT.Codop.Id_Empresa.TrimB() == "")
                        {
                            SYGETPRT.Codop.Id_Empresa = "000";
                        }
                    }
                }
            }


            return inicializa_2(Globales, unit);


        }

        public static bool inicializa_2(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            UI_gl gl = Globales.gl;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            T_MODGSWF MODGSWF = Globales.MODGSWF;
            string s = string.Empty;

            if (gl.debe_pedir != 0)
            {
                // **********************************************
                s = SYGETPRT.Codop.Cent_costo + "-" + SYGETPRT.Codop.Id_Product + "-" + SYGETPRT.Codop.Id_Especia + "-" + SYGETPRT.Codop.Id_Empresa + "-" + SYGETPRT.Codop.Id_Operacion;
                gl.Text = "Contabilidad Genérica Nº " + s;

                MODGL.PicEnabled(gl.Bot_Partys, Convert.ToInt16(true));
                MODGL.PicEnabled(gl.bot_operacion, Convert.ToInt16(true));
                // mz
                MODGL.PicEnabled(gl.bot_factura, Convert.ToInt16(true));
                if (MODGUSR.UsrEsp.Tipeje == "O")
                {
                    MODGL.PicEnabled(gl.Bot_Salvar, Convert.ToInt16(true));
                }
                gl.Bot_Partys.Enabled = true;
                gl.bot_operacion.Enabled = true;
                gl.bot_factura.Enabled = true;
                gl.debe_pedir = Convert.ToInt16(false);
            }
            else
            {
                MODGL.PicEnabled(gl.Bot_Partys, Convert.ToInt16(false));
                MODGL.PicEnabled(gl.bot_operacion, Convert.ToInt16(false));
                MODGL.PicEnabled(gl.bot_factura, Convert.ToInt16(false));
                MODGL.PicEnabled(gl.Bot_Salvar, Convert.ToInt16(false));
                gl.Bot_Partys.Enabled = false;
                gl.bot_operacion.Enabled = false;
                gl.bot_factura.Enabled = false;
                gl.Bot_Salvar.Enabled = false;
                gl.Text = "Contabilidad Genérica";
            }
            
            gl.Cliente.Text = "";
            gl.retorno_vueltos = Convert.ToInt16(false);
            // If Not enload And bot_operacion.Enabled Then bot_operacion.SetFocus
            if (!gl.EnLoad.ToBool() && gl.Bot_Partys.Enabled)
            {
                //gl.Bot_Partys.Focus();
            }

            int X = MODGTAB0.SyGetn_Pai(Globales, unit);     // Carga Tabla de Países del SGT.-
            if (X == 0)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = "No se pudo cargar correctamente Tabla de Paises.",
                    Title = T_GLOBAL.Appl_Descripcion,
                    Type = TipoMensaje.Error,
                    AutoClose = true
                });
            }

            // 
            // Sybase.-
            MODGCHQ.V_Chq_VVi = new Chq_Vvi[1] { new Chq_Vvi() };     // Cheques y Vales Vistas.-
            MODGSWF.VSwf = new T_Swf[0];//new T_Swf[1] { new T_Swf() };     // Swift's.-
            MODGSWF.VMT103 = new T_mt103[1] { new T_mt103() };
            MODGSWF.VCod = new T_Campo23E[1] { new T_Campo23E() };
            // ------------------------------------------------------------------------
            MODGSWF.VGSwf = new T_GSwf();            

            return true;
        }

        private static void ventana_NOTACREDITO(DatosGlobales Globales, int ind)
        {

            ////  ind = 1 agranda ventana  ....   ind = 2 achica ventana

            //if (ind == 0)
            //{
            //    //  ind = 0  achica ventana
            //    //  Performance
            //    //  08-06-2009
            //    //  Al iniciar el formulario se presentan los objetos basicos, mas abajo se configura
            //    //  nuevamente el formulario dependiendo del parametro "ind"
            //    gl.DefInstance.SetBounds(1005, 1305, 8460, 6060, BoundsSpecified.X | BoundsSpecified.Y);     // ...6460
            //    datos.SetBounds(30, 1200, 8265, 1550);     // .1600..
            //    frame_nac.SetBounds(0, 4000, 8295, 1335);     // .4330..
            //    frame_ext.SetBounds(30, 2760, 8265, 1365);     // .2950..
            //    Frame3D1.SetBounds(30, 600, 8265, 615);     // ...1015
            //                                                //     Factura
            //    Tx_NroFac.Name = "";
            //    Tx_tipo.Name = "";
            //    Tx_moneda.Name = "";

            //    Tx_neto.Name = "";
            //    Tx_iva.Name = "";
            //    Tx_MtoOri.Name = "";

            //    MODCTA.VNotaCreGl.NumFac = "0";
            //    MODCTA.VNotaCreGl.tipofac = "";
            //    MODCTA.VNotaCreGl.netofac = "0";
            //    MODCTA.VNotaCreGl.ivafac = "0";
            //    MODCTA.VNotaCreGl.monto = "0";

            //    MODGOVD.Gvar_NotaCredito = 0;
            //}


            //if (ind == 1)
            //{
            //    //  ind = 1   agranda ventana
            //    //  Performance
            //    //  08-06-2009
            //    //  Para este caso deben aparecer los datos de factura que se encuentran
            //    //  ocultos en el formulario.
            //    gl.DefInstance.SetBounds(1005, 1305, 8460, 7065, BoundsSpecified.X | BoundsSpecified.Y);     // ...7065
            //    datos.SetBounds(30, 2205, 8265, 1550);     // .2205..
            //    frame_nac.SetBounds(0, 5100, 8295, 1335);     // .4935..
            //    frame_ext.SetBounds(30, 3750, 8265, 1365);     // .2950..
            //    Frame3D1.SetBounds(30, 600, 8265, 1575);     // ...1575
            //                                                 //    Factura
            //                                                 //  Tx_NroFac = VNotaCreGl.NumFac
            //                                                 //  Tx_MtoOri = VNotaCreGl.monto

            //    MODGOVD.Gvar_NotaCredito = 1;

            //}
            if (ind == 1)
            {
                Globales.MODGOVD.Gvar_NotaCredito = 1;
            }

        }

        public static bool Grabar_Inicial(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer("Grabar Inicial"))
            {
                T_MODGLORI MODGLORI = Globales.MODGLORI;
                T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
                T_MODGUSR MODGUSR = Globales.MODGUSR;
                UI_gl gl = Globales.gl;
                bool res = true;

                try
                {
                    //  D.S.B.
                    MODGLORI.VxOri = new T_xOri[1];
                    //   ReDim VxVia(0)

                    gl.Bot_Salvar.Enabled = false;
                    gl.delista = false.ToInt();
                    if (gl.m_e.Items.Count != 0 || gl.m_n.Items.Count != 0)
                    {
                        gl.m_n.ListIndex = -1;
                        gl.m_e.ListIndex = -1;
                        gl.monedas.ListIndex = -1;
                        gl.monto.Text = "";
                        gl.nemonico.Text = "";
                        (gl.tipo[0]).Value = -1;
                        (gl.cambiar).Value = 0;
                        gl.en_salvar = true.ToInt();
                        gl.en_swift = false.ToInt();
                        gl.en_docval = false.ToInt();
                        if (cuadra(Globales) == 0)
                        {
                            gl.en_salvar = false.ToInt();
                            if (gl.monto.Enabled)
                            {

                            }
                            tracer.AddToContext("Cuadra", "No Cuadra - SalirSinAceptar");
                            SalirSinAceptar(Globales);
                            throw new Exception();
                        }
                        gl.datos.Enabled = false;
                        gl.frame_ext.Enabled = false;
                        gl.frame_nac.Enabled = false;
                    }
                    else
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Text = "No Existen Movimientos para Grabar",
                            Title = T_GLOBAL.Appl_Descripcion,
                            Type = TipoMensaje.Error
                        });
                        if (SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].LlaveArchivo != "")
                        {
                            if (gl.monedas.Enabled)
                            {

                            }
                        }
                        else
                        {
                            if (gl.bot_operacion.Enabled)
                            {

                            }
                        }
                        tracer.AddToContext("Movimientos", "No Existen Movimientos para Grabar - SalirSinAceptar");
                        SalirSinAceptar(Globales);
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta: ", ex);

                    res = false;
                }
                return res;
            }
        }

        public static bool Grabar_Generar_Swift_1_3(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            UI_gl gl = Globales.gl;
            // ***********************************************
            // Genera Swift's.-
            // ***********************************************
            if (gl.hab_swift != 0)
            {
                //CONTABGL.InterfazSwf(Globales, unit); //MB: ESTO NO ES NECESARIO HACERLO ACA, SE ESTA HACIENDO 2 VECES AL GRABAR.
                Globales.Controller = "Swift";
                Globales.Action = "Emision";
                return true;
            }
            return false;
        }
        /// <summary>
        /// FALSE ES QUE FUE ERRONEO Y TRUE ES QUE SIGO
        /// </summary>
        /// <param name="Globales"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static bool Grabar_Generar_Swift_2_3(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            UI_gl gl = Globales.gl;
            T_MODGSWF MODGSWF = Globales.MODGSWF;
            T_MODMMT MODMMT = Globales.MODMMT;
            int i = 0;

            if (!MODGSWF.VGSwf.Acepto)
            {
                gl.ya_retorno = false.ToInt();
                gl.datos.Enabled = true;
                gl.frame_ext.Enabled = true;
                gl.frame_nac.Enabled = true;
                SalirSinAceptar(Globales);
                return false;
            }
            else
            {
                // Cargar datos en VMT_R()
                foreach (var item in MODGSWF.VSwf)
                {
                    BCH.Comex.Core.BL.XGGL.Modulos.MODMMT.Put_MMT(Globales, 100, item.NroSwf, item.DocSwf);
                }
                return true;
            }
        }

        public static void Grabar_Generar_Swift_3_3(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            UI_gl gl = Globales.gl;
            T_MODGSWF MODGSWF = Globales.MODGSWF;
            T_MODMMT MODMMT = Globales.MODMMT;
            int i = 0;
            int cc;
            for (i = 0; i <= MODMMT.VMT_R.GetUpperBound(0); i += 1)
            {
                MODGSWF.VSwf[i].DocSwf = MODMMT.VMT_R[i].ValAct;
                cc = MODMMT.VMT_R[i].codmt;
                BCH.Comex.Core.BL.XGGL.Modulos.MODGMEM.SyPutn_Mem(Globales, unit, "s", cc, MODGSWF.VSwf[i].DocSwf);
            }
        }

        public static bool Grabar_Generar_Cheque_1_2(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            UI_gl gl = Globales.gl;
            // ***********************************************
            // Genera Cheques.-
            // ***********************************************
            if (gl.hab_docval != 0)
            {
                CONTABGL.InterfazChq(Globales, unit);
                Globales.FrmgChq = new UI_FrmgChq();
                return true;
            }
            return false;
        }

        public static bool Grabar_Generar_Cheque_2_2(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            Globales.FrmgChq = null;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            UI_gl gl = Globales.gl;
            if (MODGCHQ.VGChq.Acepto == 0)
            {
                gl.ya_retorno = false.ToInt();
                gl.datos.Enabled = true;
                gl.frame_ext.Enabled = true;
                gl.frame_nac.Enabled = true;
                SalirSinAceptar(Globales);
                return false;
            }
            return true;
        }

        public static void Grabar_Pre_Ticket(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MODGCON0 MODGCON0 = Globales.MODGCON0;
            UI_gl gl = Globales.gl;

            string Usr = String.Empty;
            string s = String.Empty;
            int Y = 0;
            string re = "";
            // ***********************************************
            // Prepara Estructura.-
            // ***********************************************
            Ordena_Est(Globales);
            Usr = MODGUSR.UsrEsp.CentroCosto + MODGUSR.UsrEsp.Especialista;
            s = SYGETPRT.Codop.Cent_costo + SYGETPRT.Codop.Id_Product + SYGETPRT.Codop.Id_Especia + SYGETPRT.Codop.Id_Empresa + SYGETPRT.Codop.Id_Operacion;
            // ***********************************************
            // Graba Contabilidad.-
            // ***********************************************
            Y = MODABDG.DesActivaBD(Globales, unit, s, "000001", MODGCON0.VMch.NroRpt, MODGCON0.VMch.FecMov);
        }

        public static short Grabar_Ticket(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            short retorno = 0;
            //Tiene que entrar una vez más para que la funcion LisDeCr_1 devuelva el 0
            for (int i = Globales.gl.INDICE_TICKET; i <= Globales.CONTAB01.Est_Contab.Length + 1; i++)
            {
                retorno = LisDeCr_1(Globales, unit, Globales.gl.Impuesto.Checked.ToInt());
                if (retorno != -1)
                {
                    return retorno;
                }
            }
            return retorno;
            //return LisDeCr_1(Globales, unit, Globales.gl.Impuesto.Checked.ToInt());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Globales"></param>
        /// <param name="unit"></param>
        /// <param name="re">ESTE VALOR VIENE DE LA EJECUCION DE LisDeCr en alguna de sus variantes</param>
        public static bool Grabar_Final(DatosGlobales Globales, UnitOfWorkCext01 unit, string re)
        {
            using (var tracer = new Tracer("Grabar Final"))
            {
                T_MODGUSR MODGUSR = Globales.MODGUSR;
                T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
                T_MODGCON0 MODGCON0 = Globales.MODGCON0;
                T_MODGSWF MODGSWF = Globales.MODGSWF;
                T_MODGCHQ MODGCHQ = Globales.MODGCHQ;

                UI_gl gl = Globales.gl;

                string Usr = MODGUSR.UsrEsp.CentroCosto + MODGUSR.UsrEsp.Especialista;
                string s = SYGETPRT.Codop.Cent_costo + SYGETPRT.Codop.Id_Product + SYGETPRT.Codop.Id_Especia + SYGETPRT.Codop.Id_Empresa + SYGETPRT.Codop.Id_Operacion;
                int X = 0;
                int Y = 0;
                int bien = 0;
                int w = 0;
                bool p1, p2 = false;

                if (MODCONGL.SyConGL(Globales, unit, Usr, gl.Impuesto.Checked.ToInt()) == 0)
                {
                    tracer.AddToContext("Grabar Contabilidad", "SalirConError");
                    SalirConError(Globales, unit);
                    return false;
                }
                // Recorre VMcds() y valida código banco <> 0 para ciertos nemónicos.-
                if (BCH.Comex.Core.BL.XGGL.Modulos.MODGCON0.BancosOK(Globales, unit) == 0)
                {
                    tracer.AddToContext("Bancos", "Debe completar la Información requerida de los Bancos asociados.");
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Debe completar la Información requerida de los Bancos asociados.",
                        Title = "Contabilidad Genérica"
                    });

                    gl.datos.Enabled = true;
                    gl.frame_ext.Enabled = true;
                    gl.frame_nac.Enabled = true;
                    SalirSinAceptar(Globales);
                    return false;
                }

                if (BCH.Comex.Core.BL.XGGL.Modulos.MODXSWF.SyPutn_Swf(Globales, unit, s, "000001", Usr) != 0)
                {
                    if (!Modswen.Fn_Save_BaseSwft(Globales, unit, "0", "0", "0", "0", "0"))
                    {
                        tracer.AddToContext("Grabar Swift en base SWIFT", "Problemas al guardar el swift en base SWIFT");
                        SalirConError(Globales, unit);
                        return false;
                    }
                    // Graba ordenes de pago en base swift
                }

                Y = BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.SyPutn_Chq(Globales, unit, s, "000001");
                Y = BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.SyPutn_Vvi(Globales, unit, s, "000001");
                Y = MODABDG.ActivaBases(Globales, unit, s, "000001");
                if (BCH.Comex.Core.BL.XGGL.Modulos.MODGCON0.ValidaContab(Globales, unit, MODGCON0.VMch.NroRpt, MODGCON0.VMch.FecMov) == 0)
                {
                    tracer.AddToContext("Validar Contabilidad", "Debe completar la Información requerida de los Bancos asociados.");
                    SalirConError(Globales, unit);
                    return false;
                }

                //  D.S.B.
                bien = BCH.Comex.Core.BL.XGGL.Modulos.MODGLORI.Traspasa_Contab(Globales);
                if (bien == 0)
                {
                    tracer.AddToContext("Traspasar Contabilidad", "Error al Traspasar Contabilidad a Vias y Origenes.");
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Error al Traspasar Contabilidad a Vias y Origenes",
                        Type = TipoMensaje.Error,
                        Title = " Cuenta Corriente en Línea "
                    });
                    SalirConError(Globales, unit);
                    return false;
                }



                w = MODGSWF.VSwf.GetUpperBound(0);

                for (int i = 0; i <= w; i += 1)
                {
                    if (MODGSWF.VSwf[i].GrabSW == 1)
                    {
                        Modswen.ActivaBD_Swi(Globales, unit, s, 0, 0, 0, 0, 0, MODGSWF.VSwf[i].CorSwi);
                    }
                }

                X = MODXDATA.Cmd_Exe(Globales, unit);
                if (X > 0)
                {

                    Y = MODABDG.DesActivaBD(Globales, unit, s, "000001", MODGCON0.VMch.NroRpt, MODGCON0.VMch.FecMov);
                    MODGSWF.VSwf = new T_Swf[0];//new T_Swf[1];
                    MODGCHQ.V_Chq_VVi = new Chq_Vvi[1];
                    MODGSWF.VMT103 = new T_mt103[1];
                    MODGSWF.VCod = new T_Campo23E[1];
                    SalirSinAceptar(Globales);
                    return false;
                }
                // ***********************************************
                // Graba el log.-
                // ***********************************************
                // Call Graba_log
                // 
                // ***********************************************
                // Se imprimen todos los documentos.-
                // ***********************************************
                //-2 porque sino se va de rango
                p1 = gl.config[T_CONTABGL.Pos_Cartas - 2].Checked;
                p2 = gl.config[T_CONTABGL.Pos_Contabilidad - 2].Checked;
                if (p1 || p2)
                {
                    Globales.FileName = BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.FileName(Globales);
                }
                if (p1)
                {
                    int c = 1;
                    bool t = true;
                    while (t)
                    {
                        string st = MODGPYF0.copiardestring(re, ";", c).TrimB();
                        var esDeb = MODGPYF0.copiardestring(st, " ", 3) == "D";

                        if (st != "")
                        {
                            string Correlativo = MODGPYF0.copiardestring(st, " ", 1);
                            string Copias = String.Empty;
                            if (MODGPYF0.copiardestring(st, " ", 2) == "N")
                            {
                                Copias = "1";
                            }
                            else
                            {
                                Copias = "2";
                            }
                            Globales.DocumentosAImprimir.Add("Impresion/Imprimir?numeroOperacion=" + BCH.Comex.Core.BL.XGGL.Modulos.MODGCHQ.Referencia(Globales) + "&codDocumento=" + (esDeb ? T_MODGADC.DocGAdeb : T_MODGADC.DocGAcre) + "&nroCorrelativo=" + (Correlativo));
                            c = c + 1;
                        }
                        else
                        {
                            t = false;
                        }
                    }
                    BCH.Comex.Core.BL.XGGL.Modulos.MODXSWF.Print_xSwf(Globales, unit, 2);
                }
                if (p2)
                {
                    if (MODGCON0.VMch.NroRpt != 0)
                    {
                        BCH.Comex.Core.BL.XGGL.Modulos.MODGCON1.Pr_Imprime_Contab80(Globales, Globales.MODGCON0.VMch.NroRpt, MODGCON0.VMch.FecMov);
                    }
                }
                gl.Bot_Salvar.Enabled = false;

                // Limpia el correlativo para que se lea el siguiente.-
                SYGETPRT.Codop.Id_Operacion = "";
                inicializa(Globales, unit);
                return true;
            }
        }

        private static void SalirSinAceptar(DatosGlobales Globales)
        {
            T_MODGLORI MODGLORI = Globales.MODGLORI;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MODGUSR MODGUSR = Globales.MODGUSR;
            UI_gl gl = Globales.gl;

            // Cancela la Grabación.-
            gl.frame_ext.Enabled = true;
            gl.frame_nac.Enabled = true;
            gl.m_e.Enabled = true;
            gl.m_n.Enabled = true;
            // Poly
            if (MODGUSR.UsrEsp.Tipeje == "O")
            {
                MODGL.PicEnabled(gl.Bot_Salvar, true.ToInt());
                gl.Bot_Salvar.Enabled = true;
            }
        }

        private static void SalirConError(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MODGSWF MODGSWF = Globales.MODGSWF;
            T_MODGCON0 MODGCON0 = Globales.MODGCON0;
            T_MODGCHQ MODGCHQ = Globales.MODGCHQ;
            int Y = 0;
            string s = SYGETPRT.Codop.Cent_costo + SYGETPRT.Codop.Id_Product + SYGETPRT.Codop.Id_Especia + SYGETPRT.Codop.Id_Empresa + SYGETPRT.Codop.Id_Operacion;

            Y = MODABDG.DesActivaBD(Globales, unit, s, "000001", MODGCON0.VMch.NroRpt, MODGCON0.VMch.FecMov);
            MODGSWF.VSwf = new T_Swf[0]; // new T_Swf[1] { new T_Swf() };
            MODGCHQ.V_Chq_VVi = new Chq_Vvi[1] { new Chq_Vvi() };
            MODGSWF.VMT103 = new T_mt103[1] { new T_mt103() };
            MODGSWF.VCod = new T_Campo23E[1] { new T_Campo23E() };

            SYGETPRT.Codop.Id_Operacion = "";
            inicializa(Globales, unit);
            SalirSinAceptar(Globales);
        }

        private static int cuadra(DatosGlobales Globales)
        {
            using (var tracer = new Tracer("cuadra"))
            {
                UI_gl gl = Globales.gl;
                T_MODGOVD MODGOVD = Globales.MODGOVD;
                T_MODCTA MODCTA = Globales.MODCTA;

                int cuadra = 0;

                double gmonto = 0.0;
                string Msg = "";
                string mone = "";
                double dif = 0.0;
                double haber = 0.0;
                double debe = 0.0;
                string lin = "";
                string Item = "";
                string l = "";
                int i = 0;

                foreach (var key in gl.Dic_ME.Keys)
                {
                    mone = key;
                    var res = ItemsValueForKey(mone, gl.Dic_ME);
                    debe = res.Item1;
                    haber = res.Item2;
                    if (debe != haber)
                    {
                        dif = Math.Abs(debe - haber);
                        if (gl.en_salvar != 0)
                        {
                            tracer.AddToContext("Montos", "Montos Descuadrados en " + mone);
                            Msg = "Imposible grabar: ";
                            Msg = Msg + "Montos debe y haber se encuentran descuadrados en " + mone + " ";
                            Msg = Msg + Format.FormatCurrency(dif, T_GLOBAL.formato);
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = Msg,
                                Type = TipoMensaje.Error,
                                Title = T_GLOBAL.Appl_Descripcion
                            });
                            ubica_moneda(Globales, mone);
                            if (debe < haber)
                            {
                                (gl.tipo[0]).Value = -1;
                            }
                            else
                            {
                                (gl.tipo[1]).Value = -1;
                            }
                        }
                        return cuadra;
                    }
                }

                if (gl.Dic_MN.Count != 0)
                {
                    mone = gl.Dic_MN.Keys.First();
                    var res = ItemsValueForKey(mone, gl.Dic_MN);
                    debe = res.Item1;
                    haber = res.Item2;
                    if (debe != haber)
                    {
                        dif = Math.Abs(debe - haber);
                        if (gl.en_salvar != 0)
                        {
                            tracer.AddToContext("Montos", "Montos Descuadrados en " + mone);
                            Msg = "Imposible grabar: Montos debe y haber ";
                            Msg = Msg + " se encuentran descuadrados en " + mone + " ";
                            Msg = Msg + Format.FormatCurrency(dif, T_GLOBAL.formato);
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = Msg,
                                Type = TipoMensaje.Error,
                                Title = T_GLOBAL.Appl_Descripcion
                            });
                            ubica_moneda(Globales, mone);
                            if (debe < haber)
                            {
                                (gl.tipo[0]).Value = -1;
                            }
                            else
                            {
                                (gl.tipo[1]).Value = -1;
                            }
                        }
                        return cuadra;
                    }
                }

                // '' 17-07-2009
                // '' ADJ INGESYS
                // '' 06-08-2009
                // '' se les quito el comentario a las sgtes 8 líneas
                if (MODGOVD.Gvar_NotaCredito == 1 && MODCTA.VNotaCreGl.monto.ToInt() > 0)
                {
                    gmonto = debe;
                    //Se cambia la valiación ya que validava antes que los montos fueran iguales
                    if (gmonto != MODCTA.VNotaCreGl.monto.ToDbl())
                    {
                        tracer.AddToContext("Montos", " No puede ser Distinto que el Monto de la Factura.");
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Text = "El monto de la nota de crédito, no puede ser distinto que el monto de la factura...",
                            Type = TipoMensaje.Error,
                            Title = T_GLOBAL.Appl_Descripcion
                        });
                        cuadra = false.ToInt();
                        return cuadra;
                    }
                }


                cuadra = true.ToInt();

                return cuadra;
            }
        }

        private static void ubica_moneda(DatosGlobales Globales, string Moneda)
        {
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            UI_gl gl = Globales.gl;

            int k = 0;
            int i = 0;

            for (i = 0; i <= MODGTAB0.VMnd.GetUpperBound(0); i += 1)
            {
                if (MODGTAB0.VMnd[i].Mnd_MndNmc.UCase() == Moneda.UCase())
                {
                    break;
                }
            }

            for (k = 0; k <= gl.monedas.Items.Count - 1; k += 1)
            {
                if (gl.monedas.Items[k].Value.ToStr().LCase() == MODGTAB0.VMnd[i].Mnd_MndNom.LCase())
                {
                    gl.monedas.ListIndex = k;
                    break;
                }
            }

        }

        private static void Ordena_Est(DatosGlobales Globales)
        {
            UI_gl gl = Globales.gl;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            T_GLOBAL GLOBAL = Globales.GLOBAL;

            int iNewIndex = 0;
            int j = 0;
            Tipo_Contabilidad[] aux_est_contab = new Tipo_Contabilidad[0];
            string s = "";
            int i = 0;
            int Total = 0;

            gl.me_sort.Items.Clear();
            gl.mn_sort.Items.Clear();

            Total = CONTAB01.Est_Contab.GetUpperBound(0);
            for (i = 0; i <= Total; i += 1)
            {
                if (CONTAB01.Est_Contab[i].NomMoneda.LCase() == GLOBAL.moneda_nac.LCase())
                {
                    if (CONTAB01.Est_Contab[i].modulo == T_CONTAB01.MODULO_ORIGEN)
                    {
                        s = CONTAB01.Est_Contab[i].NemMoneda + 9.Char() + "D" + 9.Char() + CONTAB01.Est_Contab[i].Nemonico;
                    }
                    else
                    {
                        s = CONTAB01.Est_Contab[i].NemMoneda + 9.Char() + "H" + 9.Char() + CONTAB01.Est_Contab[i].Nemonico;
                    }
                    var item = GridItemFromString(s);
                    item.Data = i;
                    gl.mn_sort.Items.Add(item);
                }
                else
                {
                    if (CONTAB01.Est_Contab[i].modulo == T_CONTAB01.MODULO_ORIGEN)
                    {
                        s = CONTAB01.Est_Contab[i].NemMoneda + 9.Char() + "D" + 9.Char() + CONTAB01.Est_Contab[i].Nemonico;
                    }
                    else
                    {
                        s = CONTAB01.Est_Contab[i].NemMoneda + 9.Char() + "H" + 9.Char() + CONTAB01.Est_Contab[i].Nemonico;
                    }
                    var item = GridItemFromString(s);
                    item.Data = i;
                    gl.me_sort.Items.Add(item);
                }
            }

            aux_est_contab = new Tipo_Contabilidad[Total + 1];
            j = -1;

            for (i = 0; i <= gl.me_sort.Items.Count - 1; i += 1)
            {
                j = j + 1;
                aux_est_contab[j] = CONTAB01.Est_Contab[gl.me_sort.get_ItemData(i).ToInt()];
            }

            for (i = 0; i <= gl.mn_sort.Items.Count - 1; i += 1)
            {
                j = j + 1;
                aux_est_contab[j] = CONTAB01.Est_Contab[gl.mn_sort.get_ItemData(i).ToInt()];
            }

            CONTAB01.Est_Contab = new Tipo_Contabilidad[Total + 1];

            for (i = 0; i <= aux_est_contab.GetUpperBound(0); i += 1)
            {
                CONTAB01.Est_Contab[i] = aux_est_contab[i];
            }

        }

        /// <summary>
        /// ESTA FUNCION SE ENCARGA DE DEFINIR SI SE SIGUE ITERANDO SOBRE LOS TICKETS O NO
        /// </summary>
        /// <param name="Globales"></param>
        /// <param name="Impuesto"></param>
        /// <returns>TRUE ES QUE VA A TICKETS Y FALSE QUE NO</returns>
        private static short LisDeCr_1(DatosGlobales Globales, UnitOfWorkCext01 unit, int Impuesto)
        {
            using (var tracer = new Tracer("LisDeCr_1"))
            {
                T_MODGTIC MODGTIC = Globales.MODGTIC;
                T_CONTAB01 CONTAB01 = Globales.CONTAB01;
                T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
                T_MODGMTA MODGMTA = Globales.MODGMTA;
                T_MODGSCE MODGSCE = Globales.MODGSCE;
                T_MODGUSR MODGUSR = Globales.MODGUSR;
                UI_gl gl = Globales.gl;

                string LisDeCr = "";


                int DoEvents = 0;
                int ff = 0;
                int Oti = 0;
                int a = 0;
                string s = "";
                int i = 0;
                string st = "";
                string Tip = "";

                //MODGTIC.Strtic.Demtci = false.ToInt();
                double[] monto = null;
                monto = new double[2];
                st = Globales.STR_TICKETS;
                i = CONTAB01.Est_Contab.GetUpperBound(0);
                s = SYGETPRT.Codop.Cent_costo + SYGETPRT.Codop.Id_Product + SYGETPRT.Codop.Id_Especia + SYGETPRT.Codop.Id_Empresa + SYGETPRT.Codop.Id_Operacion;
                if (gl.INDICE_TICKET <= i)
                {
                    a = gl.INDICE_TICKET;
                    if (CONTAB01.Est_Contab[a].Borrado == 0)
                    {
                        if (CONTAB01.Est_Contab[a].Nemonico == "CC$" || CONTAB01.Est_Contab[a].Nemonico == "CCE")
                        {
                            if (CONTAB01.Est_Contab[a].debe != 0)
                            {
                                Tip = "D";
                                Oti = 0;
                            }
                            else
                            {
                                Tip = "C";
                                Oti = 1;
                            }
                            monto = new double[2];
                            monto[1] = CONTAB01.Est_Contab[a].Monto.ToVal();
                            if (CONTAB01.Est_Contab[a].Nemonico == "CC$" && CONTAB01.Est_Contab[a].debe.ToBool() && MODGMTA.impflag == 1)
                            {
                                // JFO Modificación al Impuesto 07/02/2007
                                if (Impuesto != 0)
                                {
                                    Array.Resize(ref monto, 3);
                                    monto[2] = MODGSCE.VGen.MtoDeb;
                                }
                            }
                            MODGTIC.Strtic.Nomtic = MODGPYF0.Componer(CONTAB01.Est_Contab[a].Party.LlaveArchivo, "~", "") + " " + CONTAB01.Est_Contab[a].Party.NombreUsado;
                            MODGTIC.Strtic.Nemtic = CONTAB01.Est_Contab[a].NemMoneda;
                            MODGTIC.Strtic.Montic = CONTAB01.Est_Contab[a].Monto;
                            MODGTIC.Strtic.Cuetic = CONTAB01.Est_Contab[a].CtaCte;
                            MODGTIC.Strtic.Dehtic = Oti;
                            if (!MODGTIC.Strtic.Demtci.ToBool())
                            {
                                Globales.Tickets = new UI_Tickets();
                                Globales.Tickets.IMPUESTO = Impuesto;
                                Globales.Tickets.TIP = Tip;
                                Globales.Tickets.S = s;
                                Globales.Tickets.MONTO = monto;
                                Globales.Tickets.A = a;
                                Globales.Tickets.ST = st;
                                gl.INDICE_TICKET++;
                                return 1;
                            }
                            Globales.STR_TICKETS = Frm_gl.LisDeCr_2(Globales, unit, Impuesto, Tip, s, monto, a, st);
                            gl.INDICE_TICKET++;
                            return -1;//tiene que volver a la para la siguiente vuelta
                        }
                        else
                        {
                            gl.INDICE_TICKET++;
                            return -1;//tiene que volver a la para la siguiente vuelta
                        }
                    }
                    else
                    {
                        gl.INDICE_TICKET++;
                        return -1;//tiene que volver a la para la siguiente vuelta
                    }
                }
                return 0;
            }
        }

        /// <summary>
        /// ESTA FUNCION SE LLAMA CUANDO SE TERMINE DE PROCESAR CADA TICKET
        /// </summary>
        /// <param name="Globales"></param>
        /// <param name="unit"></param>
        /// <param name="Impuesto"></param>
        /// <param name="Tip"></param>
        /// <param name="s"></param>
        /// <param name="monto"></param>
        /// <param name="a"></param>
        /// <param name="st"></param>
        /// <returns></returns>
        public static string LisDeCr_2(DatosGlobales Globales, UnitOfWorkCext01 unit, int Impuesto, string Tip, string s, double[] monto, int a, string st)
        {
            using (var tracer = new Tracer("LisDeCr_2"))
            {
                T_MODGTIC MODGTIC = Globales.MODGTIC;
                T_CONTAB01 CONTAB01 = Globales.CONTAB01;
                T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
                T_MODGMTA MODGMTA = Globales.MODGMTA;
                T_MODGSCE MODGSCE = Globales.MODGSCE;
                T_MODGUSR MODGUSR = Globales.MODGUSR;
                UI_gl gl = Globales.gl;
                int X = 0;

                X = MODGADC.Doc_gAdc(Globales, unit, s, ref CONTAB01.Est_Contab[a].Party, Tip, CONTAB01.Est_Contab[a].CtaCte, MODGTIC.Strtic.Contic, CONTAB01.Est_Contab[a].NemMoneda, monto, Globales.gl.txtReferenciaCliente, MODGUSR.UsrEsp.CentroCosto + MODGUSR.UsrEsp.Especialista);
                if (CONTAB01.Est_Contab[a].CodMoneda.ToVal() == T_MODGTAB0.MndNac)
                {
                    st = st + X.Str() + " N " + Tip + ";";
                }
                else
                {
                    st = st + X.Str() + " E " + Tip + ";";
                }
                return st;
            }
        }

        public static void monedas_Click(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            UI_gl gl = Globales.gl;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            int n = 0;
            int m = 0;

            if (gl.monedas.ListIndex == -1)
            {
                gl.monto.Enabled = false;
                gl.nemonico.Enabled = false;
                gl.ver.Enabled = false;
                gl.aceptar.Enabled = false;
                gl.cancelar.Enabled = false;
                gl.tipo[0].Enabled = false;
                gl.tipo[1].Enabled = false;
                gl.cambiar.Enabled = false;
                if (gl.m_n.Items.Count > 0)
                {
                    gl.m_n.Enabled = true;
                }
                if (gl.m_e.Items.Count > 0)
                {
                    gl.m_e.Enabled = true;
                }
            }
            else
            {
                gl.monto.Enabled = true;
                gl.nemonico.Enabled = true;
                gl.ver.Enabled = true;
                gl.aceptar.Enabled = true;
                gl.cancelar.Enabled = false;
                gl.tipo[0].Enabled = true;
                gl.tipo[1].Enabled = true;
                gl.cambiar.Enabled = true;
                if (gl.en_escribe == 0)
                {
                    if (gl.delista == 0)
                    {
                        if (gl.monedas.Items[gl.monedas.ListIndex].Value.ToLower() == GLOBAL.moneda_nac)
                        {
                            gl.m_n.Enabled = true;
                            gl.m_n.ListIndex = gl.m_n.Items.Count - 1;
                            m_n_Click(Globales);
                            gl.m_e.Enabled = false;
                            gl.m_e.ListIndex = -1;
                            m_e_Click(Globales);
                            gl.m_n.ListIndex = gl.m_n.Items.Count - 1;
                            m_n_Click(Globales);
                        }
                        else
                        {
                            gl.m_n.Enabled = false;
                            gl.m_n.ListIndex = -1;
                            m_n_Click(Globales);
                            gl.m_e.Enabled = true;
                            ubica_item(Globales, gl.monedas.Items[gl.monedas.ListIndex].Value.ToStr());
                        }
                    }
                }

            }

            MODGL.Inicializa_formatos(gl.monto, gl.monedas.Items[gl.monedas.ListIndex].Value.ToStr());


            if (gl.monedas.ListIndex != -1)
            {
                m = gl.monedas.get_ItemData_(gl.monedas.ListIndex).ToInt();
                n = MODGTAB0.Get_VMnd(Globales, unit, m);
                if (Globales.MODGTAB0.VMnd[n].Mnd_MndSin != 0)
                {
                    gl.monto.Tag = "____________";
                }
                else
                {
                    gl.monto.Tag = "____________.__";
                }
                gl.monto.Text = Format.FormatCurrency(Format.StringToDouble(gl.monto.Text), MODGPYF1.DecObjeto(gl.monto));
            }

            // 
            // 
            // 

        }

        public static void m_n_dblclick(DatosGlobales Globales)
        {
            UI_gl gl = Globales.gl;
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;

            limpia(Globales);
            gl.delista = false.ToInt();
            if (gl.m_n.ListIndex == -1)
            {
                gl.m_n.ListIndex = -1;
                return;
            }
            else
            {
                gl.delista = true.ToInt();
                MOD_ADIC.ind_partida = gl.m_n.get_IData(gl.m_n.ListIndex);
                gl.mn_sort.ListIndex = gl.m_n.ListIndex;

                escribe_datos(Globales);
                gl.m_e.ListIndex = -1;

                gl.monto.Enabled = true;
                gl.nemonico.Enabled = true;
                gl.ver.Enabled = true;
                gl.aceptar.Enabled = true;
                gl.cancelar.Enabled = false;
                gl.tipo[0].Enabled = true;
                gl.tipo[1].Enabled = true;
                gl.cambiar.Enabled = true;
            }

        }

        public static void m_n_Click(DatosGlobales Globales)
        {

            if (Globales.gl.m_n.ListIndex == -1)
            {
                Globales.gl.mn_sort.ListIndex = -1;

            }

        }

        public static void m_e_dblclick(DatosGlobales Globales)
        {
            UI_gl gl = Globales.gl;
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;

            limpia(Globales);
            gl.delista = false.ToInt();

            if (gl.m_e.ListIndex == -1)
            {
                // l$ = m_e.List(m_e.ListIndex)
                // MsgBox "Item seleccionado corresponde a la suma total de debe y haber para " + copiardestring(l$, Chr$(9), 1), pito(48), Appl_Descripcion
                // m_e.Clear
                gl.m_e.ListIndex = -1;
                m_e_Click(Globales);
                return;
            }
            else
            {
                gl.delista = true.ToInt();
                MOD_ADIC.ind_partida = gl.m_e.get_IData(gl.m_e.ListIndex);
                gl.me_sort.ListIndex = gl.m_e.ListIndex;

                escribe_datos(Globales);
                gl.m_n.ListIndex = -1;
                //m_n_Click(Globales);
                // m_n.Enabled = False

                gl.monto.Enabled = true;
                gl.nemonico.Enabled = true;
                gl.ver.Enabled = true;
                gl.aceptar.Enabled = true;
                gl.cancelar.Enabled = false;
                gl.tipo[0].Enabled = true;
                gl.tipo[1].Enabled = true;
                gl.cambiar.Enabled = true;
            }

        }

        public static void m_e_Click(DatosGlobales Globales)
        {
            UI_gl gl = Globales.gl;
            if (gl.m_e.ListIndex == -1)
            {
                gl.me_sort.ListIndex = -1;
            }

        }

        public static void limpia(DatosGlobales Globales)
        {
            UI_gl gl = Globales.gl;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            Tip_Cuentas limpcuenta = new Tip_Cuentas();
            CONTAB01.Cuenta = limpcuenta;

            gl.nemonico.Text = "";
            gl.monto.Text = "";
            (gl.cambiar).Value = 0;
            gl.m_e.Enabled = true;
            gl.m_n.Enabled = true;
            if (gl.misma_moneda == 0)
            {
                gl.monedas.ListIndex = -1;
                if (gl.monedas.Enabled)
                {

                }
            }
            else
            {
                if (gl.monto.Enabled)
                {

                }
            }
        }

        private static void escribe_datos(DatosGlobales Globales)
        {
            UI_gl gl = Globales.gl;
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            string Mon = "";
            string f = "";
            int m = 0;

            gl.en_escribe = true.ToInt();
            // For i% = 0 To Monedas.ListCount - 1
            //     If Monedas.List(i%) = partidas(ind_partida).nommoneda Then
            //         Monedas.ListIndex = i%
            //         Exit For
            //     End If
            // Next i%
            if (MOD_ADIC.ind_partida != -1)
            {
                m = MOD_ADIC.partidas[MOD_ADIC.ind_partida].CodMoneda.ToInt();
                gl.monedas.ListIndex = gl.monedas.Items.FindIndex(x => x.Data == m);


                f = "0.00";


                Mon = Format.FormatCurrency(MOD_ADIC.partidas[MOD_ADIC.ind_partida].Monto.ToVal(), f);
                gl.monto.Text = Format.FormatCurrency(Mon.ToVal(), MODGPYF1.DecObjeto(gl.monto));

                gl.nemonico.Text = MOD_ADIC.partidas[MOD_ADIC.ind_partida].Nemonico;

                // Performance
                // 09-06-2009
                // si existe valor en numero de partida se muestran los objetos
                gl.txtNumRef.Visible = false;
                gl.LB_Referencia.Visible = false;
                if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].NumPar.TrimB().Len() > 0)
                {
                    gl.txtNumRef.Text = MOD_ADIC.partidas[MOD_ADIC.ind_partida].NumPar;
                    gl.txtNumRef.Visible = true;
                    gl.LB_Referencia.Visible = true;
                }

                if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].debe != 0)
                {
                    (gl.tipo[0]).Checked = true;
                    (gl.tipo[1]).Checked = false;
                }
                else
                {
                    (gl.tipo[0]).Checked = false;
                    (gl.tipo[1]).Checked = true;
                }

                if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].debe == 0)
                {
                    switch (MOD_ADIC.partidas[MOD_ADIC.ind_partida].Ind_Cuenta)
                    {
                        case T_CONTAB01.CHMEBCH:
                        case T_CONTAB01.CHMNBCH:
                        case T_CONTAB01.VVBCH:
                        case T_CONTAB01.OPOP:
                        case T_CONTAB01.OPC:
                            gl.cambiar.Enabled = false;
                            gl.desde_lista = false.ToInt();
                            (gl.cambiar).Value = 0;
                            gl.en_escribe = false.ToInt();
                            return;
                    }
                }

                if (SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].LlaveArchivo != MOD_ADIC.partidas[MOD_ADIC.ind_partida].Party.LlaveArchivo)
                {
                    gl.desde_lista = true.ToInt();
                    (gl.cambiar).Value = 1;
                }
                else
                {
                    gl.desde_lista = false.ToInt();
                    (gl.cambiar).Value = 0;
                }

                if (gl.monedas.Enabled)
                {

                }
                gl.en_escribe = false.ToInt();
            }
        }

        private static void ubica_item(DatosGlobales Globales, string Moneda)
        {
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            UI_gl gl = Globales.gl;

            string mone = "";
            string l = "";
            int i = 0;
            int k = 0;

            for (k = 1; k <= MODGTAB0.VMnd.GetUpperBound(0); k += 1)
            {
                if (MODGTAB0.VMnd[k].Mnd_MndNom.UCase() == Moneda.UCase())
                {
                    break;
                }
            }

            for (i = 0; i <= gl.m_e.Items.Count - 1; i += 1)
            {
                if (gl.m_e.get_ItemData(i).ToInt() == -1)
                {
                    l = (string)gl.m_e.Items[gl.m_e.ListIndex].Tag;
                    mone = MODGPYF0.copiardestring(l, 9.Char(), 1);
                    if (mone.UCase() == MODGTAB0.VMnd[k].Mnd_MndNmc.UCase())
                    {
                        gl.m_e.ListIndex = i;
                        m_e_Click(Globales);
                        return;
                    }
                }
            }

            gl.m_e.ListIndex = -1;
            m_e_Click(Globales);
        }

        public static void nemonico_lostfocus(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            UI_gl gl = Globales.gl;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            int Cual = 0;
            int i = 0;
            string Nem = "";

            if (verifica_nemonico(Globales, unit) == 0)
            {
                if (gl.nemonico.Enabled)
                {

                }
                return;
            }

            Nem = gl.nemonico.Text.TrimB();
            if (Nem != "")
            {
                if (es_ovd(Globales, unit, Nem, ref i) != 0)
                {
                    Cual = GLOBAL.ovd[i].id_cuenta;
                    if (Cual == T_CONTAB01.OPC || Cual == T_CONTAB01.CTACTEBC)
                    {
                        if (gl.monedas.Items[gl.monedas.ListIndex].Value.ToStr().LCase() != GLOBAL.moneda_aladi)
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = "Cuenta contable ingresada sólo trabaja con " + GLOBAL.moneda_aladi,
                                Title = T_GLOBAL.Appl_Descripcion,
                                Type = TipoMensaje.Error
                            });
                            if (gl.monedas.Enabled)
                            {

                            }
                            return;
                        }
                    }
                    if (gl.tipo[1].Checked)
                    {
                        if (Cual == T_CONTAB01.OPC || Cual == T_CONTAB01.OPOP || Cual == T_CONTAB01.CHMEBCH || Cual == T_CONTAB01.CHMNBCH || Cual == T_CONTAB01.VVBCH)
                        {
                            gl.cambiar.Enabled = false;
                        }
                    }
                }
                Globales.gl.NEMO_INI = Globales.gl.nemonico.Text.TrimB();
            }


        }

        public static void tipo_Click(DatosGlobales Globales, UnitOfWorkCext01 unit, int index)
        {
            int Value = Globales.gl.tipo[index].Value;
            int Cual = 0;
            int i = 0;
            string Nem = "";


            if (Globales.gl.tipo[1].Value != 0)
            {
                Nem = Globales.gl.nemonico.Text.TrimB();
                if (Nem != "")
                {
                    if (es_ovd(Globales, unit, Nem, ref i) != 0)
                    {
                        Cual = Globales.GLOBAL.ovd[i].id_cuenta;
                        if (Cual == T_CONTAB01.OPC || Cual == T_CONTAB01.OPOP || Cual == T_CONTAB01.CHMEBCH || Cual == T_CONTAB01.CHMNBCH || Cual == T_CONTAB01.VVBCH)
                        {
                            Globales.gl.cambiar.Enabled = false;
                        }
                    }
                }
            }
        }

        private static int es_ovd(DatosGlobales Globales, UnitOfWorkCext01 unit, string nemo, ref int ind)
        {
            int es_ovd = 0;

            int i = 0;
            int n = 0;

            n = Globales.GLOBAL.ovd.GetUpperBound(0);
            if (n == 0)
            {
                MODGL.lee_ovd(Globales, unit);
            }

            for (i = 0; i <= Globales.GLOBAL.ovd.GetUpperBound(0); i += 1)
            {
                if (Globales.GLOBAL.ovd[i].Nemonico.UCase().TrimB() == nemo.UCase().TrimB())
                {
                    ind = i;
                    es_ovd = true.ToInt();
                    break;
                }
            }
            return es_ovd;
        }

        public static void monto_click(DatosGlobales Globales)
        {
            Globales.gl.monto.Text = Format.FormatCurrency(Format.StringToDouble(Globales.gl.monto.Text), MODGPYF1.DecObjeto(Globales.gl.monto));
        }

        private static int verifica_nemonico(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            using (var tracer = new Tracer())
            {


                UI_gl gl = Globales.gl;
                T_CONTAB01 CONTAB01 = Globales.CONTAB01;
                T_MODGLOV MODGLOV = Globales.MODGLOV;
                T_GLOBAL GLOBAL = Globales.GLOBAL;
                int verifica_nemonico = 0;

                int a = 0;
                string Que = "";

                try
                {

                    Que = gl.nemonico.Text.TrimB();
                    if (Que != "")
                    {
                        if (Que == CONTAB01.NEMO_PAR || Que == CONTAB01.REEM_CHEQUE)
                        {
                            if (gl.delista != 0)
                            {
                                if (gl.NEMO_INI != "" && gl.NEMO_INI != Que)
                                {
                                    Globales.MESSAGES.Add(new UI_Message()
                                    {
                                        Text = "No es posible mover manualmente esta cuenta.",
                                        Title = T_GLOBAL.Appl_Descripcion,
                                        Type = TipoMensaje.Error,
                                        ControlName = "nemonico"
                                    });
                                    if (gl.nemonico.Enabled)
                                    {

                                    }
                                    return verifica_nemonico;
                                }
                            }
                            else
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Text = "No es posible mover manualmente esta cuenta.",
                                    Title = T_GLOBAL.Appl_Descripcion,
                                    Type = TipoMensaje.Error,
                                    ControlName = "nemonico"
                                });
                                if (gl.nemonico.Enabled)
                                {

                                }
                                return verifica_nemonico;
                            }
                        }
                        else
                        {
                            CONTAB01.Cuenta.Nem = Que;
                            a = CONTABGL.Lee_CtaCbe(Globales, unit, Que);
                            MODGLOV.VgOVNul.NomCta = CONTAB01.Cuenta.Nom;
                            if (a == 0)
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Text = "No existe la cuenta contable asociada al nemónico " + Que + ".",
                                    Title = T_GLOBAL.Appl_Descripcion,
                                    Type = TipoMensaje.Error,
                                    ControlName = "nemonico"
                                });
                                if (gl.nemonico.Enabled)
                                {

                                }
                                return verifica_nemonico;
                            }
                            if (CONTAB01.Cuenta.gl == 0)
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Text = "No es posible mover manualmente esta cuenta.",
                                    Title = T_GLOBAL.Appl_Descripcion,
                                    Type = TipoMensaje.Error,
                                    ControlName = "nemonico"
                                });
                                if (gl.nemonico.Enabled)
                                {

                                }
                                return verifica_nemonico;
                            }
                            if (gl.monedas.get_ItemData_(gl.monedas.ListIndex).ToInt() == GLOBAL.cod_monac && CONTAB01.Cuenta.Mon == T_GLOBAL.Nem_Ext)
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Text = "Nemónico ingresado corresponde a Moneda Extranjera",
                                    Title = T_GLOBAL.Appl_Descripcion,
                                    Type = TipoMensaje.Error,
                                    ControlName = "nemonico"
                                });
                                if (gl.nemonico.Enabled)
                                {

                                }
                                return verifica_nemonico;
                            }

                            if (gl.monedas.get_ItemData_(gl.monedas.ListIndex).ToInt() != GLOBAL.cod_monac && CONTAB01.Cuenta.Mon == T_GLOBAL.Nem_Nac)
                            {
                                Globales.MESSAGES.Add(new UI_Message()
                                {
                                    Text = "Nemónico ingresado corresponde a Moneda Nacional",
                                    Title = T_GLOBAL.Appl_Descripcion,
                                    Type = TipoMensaje.Error,
                                    ControlName = "nemonico"
                                });
                                if (gl.nemonico.Enabled)
                                {

                                }
                                return verifica_nemonico;
                            }
                            gl.Glosa = CONTAB01.Cuenta.Nom;

                            //  Performance
                            //  09-06-2009
                            //  indica si permite ingresar numero de referencia
                            if (CONTAB01.Cuenta.Vig == 1)
                            {
                                gl.LB_Referencia.Visible = true;
                                gl.txtNumRef.Visible = true;
                            }
                            else
                            {
                                gl.txtNumRef.Text = "";
                                gl.LB_Referencia.Visible = false;
                                gl.txtNumRef.Visible = false;
                            }
                        }
                    }

                    verifica_nemonico = true.ToInt();
                    return verifica_nemonico;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta", exc);
                }

                return verifica_nemonico;
            }
        }

        public static bool Ver_Click_1_2(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            UI_gl gl = Globales.gl;
            T_MODCTA MODCTA = Globales.MODCTA;
            int X = 0;
            string MsgxCob = "";
            int m = 0;


            m = gl.monedas.get_ItemData_(gl.monedas.ListIndex).ToInt();
            if (m == -1)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = "Debe especificar la moneda sobre la cual desea efectuar la búsqueda de las Cuentas Contables.",
                    Type = TipoMensaje.Error
                });
            }
            if (m == 1)
            {
                MODCTA.VCtaGl.moncta = 2;
            }
            else
            {
                MODCTA.VCtaGl.moncta = 1;
            }
            X = BCH.Comex.Core.BL.XGGL.Modulos.MODCTA.SyGetn_CtaCtb1(Globales, unit);

            if (X == 0)
            {
                return false;
            }
            return true;
        }

        public static void Ver_Click_2_2(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            Globales.gl.nemonico.Text = Globales.MODCTA.VCtaGl.NemCta.UCase();
            nemonico_lostfocus(Globales, unit);
        }

        public static void Aceptar_Click_Original(DatosGlobales Globales, UnitOfWorkCext01 unit, bool saltarValidacionSaldo = false)
        {
            UI_gl gl = Globales.gl;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            T_MODGOVD MODGOVD = Globales.MODGOVD;
            T_MODCTA MODCTA = Globales.MODCTA;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;

            int n = 0;
            int a = 0;
            int debe = 0;
            string Mon = "";
            string nemo = "";
            int codmnd = 0;
            string Moneda = "";
            int Cual = 0;
            int i = 0;
            string Nem = "";

            int ind_ovd = 0;

            //  Performance
            //  05-06-2009
            //  Si el objeto esta visible se debe validar que tenga informacion
            if (gl.txtNumRef.Visible)
            {
                if (gl.txtNumRef.Text.TrimB() == "")
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Es necesario que se ingrese un Número de Partida para poder realizar la operación.",
                        Title = T_MODGLORI.MsgxOri,
                        Type = TipoMensaje.Error
                    });
                    gl.txtNumRef.Enabled = true;
                    return;
                }
            }

            if (verifica_nemonico(Globales, unit) == 0)
            {
                return;
            }

            Nem = gl.nemonico.Text.TrimB();

            if (!string.IsNullOrEmpty(Nem))
            {
                if (es_ovd(Globales, unit, Nem, ref i) != 0)
                {
                    Cual = GLOBAL.ovd[i].id_cuenta;
                    if (Cual == T_CONTAB01.OPC || Cual == T_CONTAB01.CTACTEBC)
                    {
                        if (gl.monedas.Items[gl.monedas.ListIndex].Value.ToStr().LCase() != GLOBAL.moneda_aladi)
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = "Cuenta contable ingresada trabaja sólo con " + GLOBAL.moneda_aladi + ".",
                                Title = T_MODGLORI.MsgxOri,
                                Type = TipoMensaje.Error
                            });
                            return;
                        }
                    }
                }
            }

            Moneda = gl.monedas.Value.ToStr();
            codmnd = gl.monedas.Value.ToInt();
            nemo = gl.nemonico.Text.TrimB();
            Mon = gl.monto.Text;
            debe = (gl.tipo[0]).Value;


            if (string.IsNullOrEmpty(Mon) || Mon.ToVal() == 0)
            {
                if (debe != 0)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Debe ingresar monto a debitar",
                        Title = T_MODGLORI.MsgxOri,
                        Type = TipoMensaje.Error,
                        ControlName = "monto"
                    });
                }
                else
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Debe ingresar monto a acreditar",
                        Title = T_MODGLORI.MsgxOri,
                        Type = TipoMensaje.Error,
                        ControlName = "monto"
                    });
                }
                return;
            }

            if (string.IsNullOrEmpty(nemo))
            {
                if (debe != 0)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Debe ingresar el nemónico de la cuenta que se va a debitar",
                        Title = T_MODGLORI.MsgxOri,
                        Type = TipoMensaje.Error
                    });
                }
                else
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Debe ingresar el nemónico de la cuenta que se va a acreditar",
                        Title = T_MODGLORI.MsgxOri,
                        Type = TipoMensaje.Error
                    });
                }

                return;
            }


            if (MODGOVD.Gvar_NotaCredito == 1 && !string.IsNullOrEmpty(MODCTA.VNotaCreGl.monto) && MODCTA.VNotaCreGl.monto.ToInt() > 0)
            {
                if (Mon.ToDbl() > MODCTA.VNotaCreGl.monto.ToDbl())
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "El Monto de la Nota de Crédito, No puede ser Mayor que el Monto de la Factura...",
                        Title = T_MODGLORI.MsgxOri,
                        Type = TipoMensaje.Error
                    });
                    return;
                }
            }

            if (gl.cambiar.Checked)
            {
                if (gl.delista != 0)
                {
                    if (gl.desde_lista != 0)
                    {
                        gl.prty_cliente = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli];
                        SYGETPRT.PartysOpe[T_GLOBAL.I_Cli] = MOD_ADIC.partidas[MOD_ADIC.ind_partida].Party;
                    }
                    else
                    {
                        gl.prty_cliente = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli];
                        SYGETPRT.PartysOpe[T_GLOBAL.I_Cli] = gl.PartyVacio.Copy();
                    }
                }
                else
                {
                    gl.prty_cliente = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli];
                    SYGETPRT.PartysOpe[T_GLOBAL.I_Cli] = gl.PartyVacio.Copy();
                }

                Globales.Action = "Identificar";
                Globales.Controller = "Participantes";

                n = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.GetParty_1_2(Globales, GLOBAL.Beneficiario, SYGETPRT.Codop, true.ToInt(), false.ToInt());

                //TODO: @hernan resolver esto despues
                if (n == 0)
                {
                    SYGETPRT.PartysOpe[T_GLOBAL.I_Cli] = gl.prty_cliente;
                    return;
                }
            }

            if (CONTAB01.Cuenta.CVT.ToBool() && SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].Ubicacion == T_SYGETPRT.GPrt_EnOperacion)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = "Cuenta contable seleccionada no puede ser asociada a un participante pegado a la operación",
                    Title = T_MODGLORI.MsgxOri,
                    Type = TipoMensaje.Error
                });
                return;
            }

            // 
            // If Not delista Then
            //     ind_partida = Nueva_Partida()
            // End If
            // xxx.-

            if (es_ovd(Globales, unit, nemo, ref ind_ovd) != 0)
            {
                if (debe != 0)
                {
                    bool sigue = cuenta_debe(Globales, unit, Mon, Moneda, codmnd, nemo, ind_ovd, SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].LlaveArchivo, saltarValidacionSaldo);
                    if (!sigue)
                    {
                        return;
                    }
                }
                else
                {
                    bool sigue = cuenta_haber(Globales, unit, Mon, Moneda, codmnd, nemo, ind_ovd, SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].LlaveArchivo);
                    if (!sigue)
                    {
                        return;
                    }
                }

            }
            else
            {
                // CFTB**********************************************
                if (nemo == "OBALALC3" || nemo == "OBALALC" || nemo == "OBNPBCHI" || nemo == "OBNPBCHZ" || nemo == "REFINE" || nemo == "REFINE2" || nemo == "REFINE3")
                {
                    // Llama al formulario Origen/Via.-
                    MODGLOV.VgOV = MODGLOV.VgOVNul.Copy();
                    MODGLOV.VgOV.TipoOV = "V";
                    MODGLOV.VgOV.codmnd = codmnd;
                    MODGLOV.VgOV.MtoMnd = Mon.ToVal();
                    MODGLOV.VgOV.Id_Cta = 0;

                    Globales.FrmGLOV = new UI_FrmGLOV();
                    Globales.Action = "Adicionales";
                    Globales.VieneDeAction = "Aceptar_Despues_Aceptar_Adicionales_1";
                }
                // CFTB**********************************************

                if (nemo == CONTAB01.NEMO_PAR || nemo == CONTAB01.REEM_CHEQUE)
                {
                    if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].envio != 0)
                    {

                        // Llama al formulario Origen/Via.-
                        MODGLOV.VgOV = MODGLOV.VgOVNul.Copy();
                        MODGLOV.VgOV.TipoOV = "";
                        MODGLOV.VgOV.codmnd = codmnd;
                        MODGLOV.VgOV.MtoMnd = Mon.ToVal();
                        MODGLOV.VgOV.Id_Cta = 0;

                        Globales.FrmGLOV = new UI_FrmGLOV();
                        Globales.Action = "Adicionales";
                        Globales.VieneDeAction = "Aceptar_Despues_Aceptar_Adicionales_2";

                    }
                }
                else
                {
                    // *************************************************************
                    // Cambié ahora a una cuenta que NO requiere datos adicionales.-
                    // *************************************************************
                    MODGLOV.VgOV = MODGLOV.VgOVNul.Copy();
                    ingresa_datos(Globales, unit, debe, false.ToInt());
                    escribe_lista(Globales);
                }
            }

            //  Performance
            //  05-06-2009
            //  Limpia los valores y oculta objetos.
            gl.LB_Referencia.Visible = false;
            gl.txtNumRef.Text = "";
            gl.txtNumRef.Visible = false;
            Globales.Action = "Index";
        }

        public static void Aceptar_Click(DatosGlobales Globales, UnitOfWorkCext01 unit, bool saltarValidacionSaldo = false)
        {
            XgglService XgglService = new XGGL.XgglService();
            UI_gl gl = Globales.gl;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            T_MODGOVD MODGOVD = Globales.MODGOVD;
            T_MODCTA MODCTA = Globales.MODCTA;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;

            int i = 0;

            //  Performance
            //  05-06-2009
            //  Si el objeto esta visible se debe validar que tenga informacion
            if (gl.txtNumRef.Visible)
            {
                if (gl.txtNumRef.Text.TrimB() == "")
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Es necesario que se ingrese un Número de Partida para poder realizar la operación.",
                        Title = T_MODGLORI.MsgxOri,
                        Type = TipoMensaje.Error,
                        ControlName = "txtNumRef"
                    });
                    gl.txtNumRef.Enabled = true;
                    return;
                }
            }

            if (verifica_nemonico(Globales, unit) == 0)
            {
                return;
            }

            gl.Nem = gl.nemonico.Text.TrimB();
            if (!string.IsNullOrEmpty(gl.Nem))
            {
                if (es_ovd(Globales, unit, gl.Nem, ref i) != 0)
                {
                    gl.Cual = GLOBAL.ovd[i].id_cuenta;
                    if (gl.Cual == T_CONTAB01.OPC || gl.Cual == T_CONTAB01.CTACTEBC)
                    {
                        if (gl.monedas.Items[gl.monedas.ListIndex].Value.ToStr().LCase() != GLOBAL.moneda_aladi)
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = "Cuenta contable ingresada trabaja sólo con " + GLOBAL.moneda_aladi + ".",
                                Title = T_MODGLORI.MsgxOri,
                                Type = TipoMensaje.Error,
                                ControlName = "nemonico"
                            });
                            return;
                        }
                    }
                }
            }

            gl.Moneda = gl.monedas.Text.ToStr();
            gl.codmnd = gl.monedas.Value.ToInt();
            gl.nemo = gl.nemonico.Text.TrimB();
            gl.Mon = gl.monto.Text;
            gl.debe = (gl.tipo[0]).Value;

            if (string.IsNullOrEmpty(gl.Mon) || gl.Mon.ToVal() == 0)
            {
                if (gl.debe != 0)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Debe ingresar monto a debitar",
                        Title = T_MODGLORI.MsgxOri,
                        Type = TipoMensaje.Error,
                        ControlName = "monto"
                    });
                }
                else
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Debe ingresar monto a acreditar",
                        Title = T_MODGLORI.MsgxOri,
                        Type = TipoMensaje.Error,
                        ControlName = "monto"
                    });
                }
                return;
            }

            if (string.IsNullOrEmpty(gl.nemo))
            {
                if (gl.debe != 0)
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Debe ingresar el nemónico de la cuenta que se va a debitar",
                        Title = T_MODGLORI.MsgxOri,
                        Type = TipoMensaje.Error,
                        ControlName = "nemonico"
                    });
                }
                else
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Debe ingresar el nemónico de la cuenta que se va a acreditar",
                        Title = T_MODGLORI.MsgxOri,
                        Type = TipoMensaje.Error,
                        ControlName = "nemonico"
                    });
                }
                return;
            }

            if (MODGOVD.Gvar_NotaCredito == 1 && !string.IsNullOrEmpty(MODCTA.VNotaCreGl.monto) && MODCTA.VNotaCreGl.monto.ToInt() > 0)
            {
                if (gl.Mon.ToDbl() > MODCTA.VNotaCreGl.monto.ToDbl())
                {
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "El Monto de la Nota de Crédito, No puede ser Mayor que el Monto de la Factura...",
                        Title = "Atención: Mensaje de Advertencia",
                        Type = TipoMensaje.Error,
                        ControlName = "monto"
                    });

                    return;
                }
            }

            if (gl.cambiar.Checked && !saltarValidacionSaldo)
            {
                if (gl.delista != 0)
                {
                    if (gl.desde_lista != 0)
                    {
                        gl.prty_cliente = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli];
                        SYGETPRT.PartysOpe[T_GLOBAL.I_Cli] = MOD_ADIC.partidas[MOD_ADIC.ind_partida].Party;
                    }
                    else
                    {
                        gl.prty_cliente = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli];
                        SYGETPRT.PartysOpe[T_GLOBAL.I_Cli] = gl.PartyVacio.Copy();
                    }
                }
                else
                {
                    gl.prty_cliente = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli];
                    SYGETPRT.PartysOpe[T_GLOBAL.I_Cli] = gl.PartyVacio.Copy();
                }

                Globales.Action = "Identificar";
                Globales.Controller = "Participantes";
                Globales.vieneDeMsg = true;
                return;

                //n = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.GetParty_1_2(Globales, GLOBAL.Beneficiario, SYGETPRT.Codop, true.ToInt(), false.ToInt());
                //TODO: @hernan resolver esto despues
                //if (n == 0)
                //{
                //    SYGETPRT.PartysOpe[T_GLOBAL.I_Cli] = gl.prty_cliente;
                //    return;
                //}
            }

            Aceptar_Click_2(Globales, unit, saltarValidacionSaldo);
        }

        public static void Aceptar_Click_2(DatosGlobales Globales, UnitOfWorkCext01 unit, bool saltarValidacionSaldo = false)
        {
            UI_gl gl = Globales.gl;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            T_MODGOVD MODGOVD = Globales.MODGOVD;
            T_MODCTA MODCTA = Globales.MODCTA;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;



            if (CONTAB01.Cuenta.CVT.ToBool() && SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].Ubicacion == T_SYGETPRT.GPrt_EnOperacion)
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Text = "Cuenta contable seleccionada no puede ser asociada a un participante pegado a la operación",
                    Title = T_MODGLORI.MsgxOri,
                    Type = TipoMensaje.Error
                });
                return;
            }

            if (es_ovd(Globales, unit, gl.nemo, ref gl.ind_ovd) != 0)
            {
                if (gl.debe != 0)
                {
                    bool sigue = cuenta_debe(Globales, unit, gl.Mon, gl.Moneda, gl.codmnd, gl.nemo, gl.ind_ovd, SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].LlaveArchivo, saltarValidacionSaldo);
                    if (!sigue)
                    {
                        return;
                    }
                }
                else
                {
                    bool sigue = cuenta_haber(Globales, unit, gl.Mon, gl.Moneda, gl.codmnd, gl.nemo, gl.ind_ovd, SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].LlaveArchivo);
                    if (!sigue)
                    {
                        return;
                    }
                }
            }
            else
            {
                // CFTB**********************************************
                if (gl.nemo == "OBALALC3" || gl.nemo == "OBALALC" || gl.nemo == "OBNPBCHI" || gl.nemo == "OBNPBCHZ" || gl.nemo == "REFINE" || gl.nemo == "REFINE2" || gl.nemo == "REFINE3")
                {
                    // Llama al formulario Origen/Via.-
                    MODGLOV.VgOV = MODGLOV.VgOVNul.Copy();
                    MODGLOV.VgOV.TipoOV = "V";
                    MODGLOV.VgOV.codmnd = gl.codmnd;
                    MODGLOV.VgOV.MtoMnd = gl.Mon.ToVal();
                    MODGLOV.VgOV.Id_Cta = 0;
                    Globales.FrmGLOV = new UI_FrmGLOV();
                    Globales.Action = "Adicionales";
                    Globales.VieneDeAction = "Aceptar_Despues_Aceptar_Adicionales_1";
                }

                // CFTB**********************************************
                if (gl.nemo == CONTAB01.NEMO_PAR || gl.nemo == CONTAB01.REEM_CHEQUE)
                {
                    if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].envio != 0)
                    {
                        // Llama al formulario Origen/Via.-
                        MODGLOV.VgOV = MODGLOV.VgOVNul.Copy();
                        MODGLOV.VgOV.TipoOV = "";
                        MODGLOV.VgOV.codmnd = gl.codmnd;
                        MODGLOV.VgOV.MtoMnd = gl.Mon.ToVal();
                        MODGLOV.VgOV.Id_Cta = 0;
                        Globales.FrmGLOV = new UI_FrmGLOV();
                        Globales.Action = "Adicionales";
                        Globales.VieneDeAction = "Aceptar_Despues_Aceptar_Adicionales_2";
                    }
                }
                else
                {
                    // *************************************************************
                    // Cambié ahora a una cuenta que NO requiere datos adicionales.-
                    // *************************************************************
                    MODGLOV.VgOV = MODGLOV.VgOVNul.Copy();
                    ingresa_datos(Globales, unit, gl.debe, false.ToInt());
                    escribe_lista(Globales);
                }
            }

            //  Performance
            //  05-06-2009
            //  Limpia los valores y oculta objetos.
            gl.LB_Referencia.Visible = false;
            gl.txtNumRef.Text = "";
            gl.txtNumRef.Visible = false;

            Globales.Action = "Index";
            Globales.Controller = "";
            Globales.vieneDeMsg = false;

            //se agrega para limpiar las variables de seleccion
            //Solo se debe limpiar si no viene ningunmensaje
            if (Globales.MESSAGES.Count() == 0)
            {
                gl.delista = 0;
                gl.m_n.ListIndex = -1;
                gl.m_e.ListIndex = -1;
            }
        }

        private static void ingresa_datos(DatosGlobales Globales, UnitOfWorkCext01 unit, int debe, int llamo)
        {
            UI_gl gl = Globales.gl;
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            T_MODGTAB0 MODGTAB0 = Globales.MODGTAB0;
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            T_MODGLOV MODGLOV = Globales.MODGLOV;

            int k = 0;
            int n = 0;
            int i = 0;
            int m = 0;

            if (gl.delista == 0)
            {
                MOD_ADIC.ind_partida = Nueva_Partida(Globales);
            }
            gl.cambio_tipo = false.ToInt();
            gl.cambio_moneda = false.ToInt();
            gl.nommon_ant = "";
            gl.nemmon_ant = "";
            if (gl.delista != 0)
            {
                if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].NomMoneda.Trim().LCase() == gl.monedas.Items[gl.monedas.ListIndex].Value.ToStr().LCase())
                {
                    if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].debe != debe)
                    {
                        gl.cambio_tipo = true.ToInt();
                        if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].envio != 0)
                        {
                            cancela_envios(Globales, MOD_ADIC.partidas[MOD_ADIC.ind_partida].debe);
                        }
                    }
                }
                else
                {
                    gl.nommon_ant = MOD_ADIC.partidas[MOD_ADIC.ind_partida].NomMoneda;
                    gl.nemmon_ant = MOD_ADIC.partidas[MOD_ADIC.ind_partida].NemMoneda;
                    gl.cambio_moneda = true.ToInt();
                }
            }
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].envio = llamo;
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].debe = debe;
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].Monto = gl.monto.Text;
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].CtaCte = GLOBAL.datos_cuenta.Cuenta;

            //  Performance
            //  01-06-2009
            //  asigna en el arreglo el valor del objeto numero de referencia.
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].NumPar = gl.txtNumRef.Text.TrimB();

            // Datos de las monedas.-
            // Datos_Moneda1 (Monedas.List(Monedas.ListIndex))
            // partidas(ind_partida).codmoneda = Format$(Money.codmoneda)
            // partidas(ind_partida).nommoneda = LCase$(Money.nommoneda)
            // partidas(ind_partida).NemMoneda = Money.NemMoneda
            // partidas(ind_partida).CodMonBC = Format$(Money.CodMonBC)
            // partidas(ind_partida).SwfMoneda = Money.SwfMoneda
            m = gl.monedas.get_ItemData_(gl.monedas.ListIndex).ToInt();
            i = BCH.Comex.Core.BL.XGGL.Modulos.MODGTAB0.Get_VMnd(Globales, unit, m);
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].CodMoneda = MODGTAB0.VMnd[i].Mnd_MndCod.Str().TrimB();
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].NomMoneda = MODGTAB0.VMnd[i].Mnd_MndNom.LCase();
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].NemMoneda = MODGTAB0.VMnd[i].Mnd_MndNmc;
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].CodMonBC = MigrationSupport.Utils.Format(MODGTAB0.VMnd[i].Mnd_MndCbc, String.Empty);
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].SwfMoneda = MODGTAB0.VMnd[i].Mnd_MndSwf;
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].Nemonico = gl.nemonico.Text.TrimB();
            MOD_ADIC.partidas[MOD_ADIC.ind_partida].Glosa = gl.Glosa;
            if (gl.nemonico.Text.TrimB() == CONTAB01.NEMO_PAR)
            {
                MOD_ADIC.partidas[MOD_ADIC.ind_partida].Ind_Cuenta = T_CONTAB01.ONME;
            }
            else
            {
                if (gl.cambio_nemo != 0)
                {
                    MOD_ADIC.partidas[MOD_ADIC.ind_partida].Ind_Cuenta = T_CONTAB01.CHMEBCH;
                    gl.cambio_nemo = false.ToInt();
                }
                else
                {
                    MOD_ADIC.partidas[MOD_ADIC.ind_partida].Ind_Cuenta = que_indice(Globales, MOD_ADIC.partidas[MOD_ADIC.ind_partida].Nemonico, GLOBAL.Money.NomMoneda);
                }
            }
            if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].debe == 0)
            {
                switch (MOD_ADIC.partidas[MOD_ADIC.ind_partida].Ind_Cuenta)
                {
                    case T_CONTAB01.CHMEBCH:
                    case T_CONTAB01.CHMNBCH:
                    case T_CONTAB01.VVBCH:
                    case T_CONTAB01.OPOP:
                    case T_CONTAB01.OPC:
                        MOD_ADIC.partidas[MOD_ADIC.ind_partida].Ind_Benef = 0;
                        MOD_ADIC.partidas[MOD_ADIC.ind_partida].Nom_Benef = "";
                        MOD_ADIC.partidas[MOD_ADIC.ind_partida].Party = gl.PartyVacio.Copy();
                        break;
                    default:
                        MOD_ADIC.partidas[MOD_ADIC.ind_partida].Ind_Benef = T_GLOBAL.I_Cli;
                        MOD_ADIC.partidas[MOD_ADIC.ind_partida].Nom_Benef = GLOBAL.Beneficiario[T_GLOBAL.I_Cli];
                        MOD_ADIC.partidas[MOD_ADIC.ind_partida].Party = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli];
                        break;
                }
            }
            else
            {
                MOD_ADIC.partidas[MOD_ADIC.ind_partida].Ind_Benef = T_GLOBAL.I_Cli;
                MOD_ADIC.partidas[MOD_ADIC.ind_partida].Nom_Benef = GLOBAL.Beneficiario[T_GLOBAL.I_Cli];
                MOD_ADIC.partidas[MOD_ADIC.ind_partida].Party = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli];
            }

            // Se incluye también en el arreglo est_contab.-
            n = CONTAB01.Est_Contab.GetUpperBound(0);

            // If n% = 0 Then ReDim Est_Contab(n%)

            if (MOD_ADIC.ind_partida > n)
            {
                k = n + 1;
                Array.Resize(ref CONTAB01.Est_Contab, k + 1);
                CONTAB01.Est_Contab[k] = new Tipo_Contabilidad();
            }
            else
            {
                k = MOD_ADIC.ind_partida;
            }
            i = MOD_ADIC.ind_partida;
            CONTAB01.Est_Contab[k].Monto = MOD_ADIC.partidas[i].Monto;
            CONTAB01.Est_Contab[k].CodMoneda = MOD_ADIC.partidas[i].CodMoneda;
            CONTAB01.Est_Contab[k].NomMoneda = MOD_ADIC.partidas[i].NomMoneda;
            CONTAB01.Est_Contab[k].CodMonBC = MOD_ADIC.partidas[i].CodMonBC;
            CONTAB01.Est_Contab[k].SwfMoneda = MOD_ADIC.partidas[i].SwfMoneda;
            CONTAB01.Est_Contab[k].NemMoneda = MOD_ADIC.partidas[i].NemMoneda;
            CONTAB01.Est_Contab[k].Nemonico = MOD_ADIC.partidas[i].Nemonico;
            CONTAB01.Est_Contab[k].CtaCte = MOD_ADIC.partidas[i].CtaCte;
            CONTAB01.Est_Contab[k].Glosa = MOD_ADIC.partidas[i].Glosa;
            CONTAB01.Est_Contab[k].Ind_Cuenta = MOD_ADIC.partidas[i].Ind_Cuenta;
            CONTAB01.Est_Contab[k].Ind_Benef = MOD_ADIC.partidas[i].Ind_Benef;
            CONTAB01.Est_Contab[k].Party = MOD_ADIC.partidas[i].Party;
            CONTAB01.Est_Contab[k].debe = MOD_ADIC.partidas[i].debe;
            CONTAB01.Est_Contab[k].Borrado = 0;

            if (CONTAB01.Est_Contab[k].debe != 0)
            {
                CONTAB01.Est_Contab[k].modulo = T_CONTAB01.MODULO_ORIGEN;
            }
            else
            {
                CONTAB01.Est_Contab[k].modulo = T_CONTAB01.MODULO_VIAS;
            }

            //  Performance
            //  01-06-2009
            //  Se almacena en arreglo de cuentas del comprobante el numero de referencia
            //  si es que se especifica, de lo contrario mantiene logica anterior.
            // Est_Contab(k%).Num_Partida = Trim$(Str$(VgOV.NumPar))
            if (gl.txtNumRef.Text.TrimB() != "")
            {
                CONTAB01.Est_Contab[k].Num_Partida = gl.txtNumRef.Text.TrimB();
            }
            else
            {
                CONTAB01.Est_Contab[k].Num_Partida = MODGLOV.VgOV.NumPar.Str().TrimB();
            }

            // Est_Contab(k%).num_op = Num$
            // Est_Contab(k%).modulo = MODULO_ORIGEN
            // Est_Contab(k%).modulo = MODULO_VIAS
            // Est_Contab(k%).modulo = MODULO_VUELTO
            // Est_Contab(k%).modulo = MODULO_ARBITRAJE
            if (CONTAB01.Est_Contab[k].CtaCte == "")
            {
                CONTAB01.Est_Contab[k].CtaCte = MODGLOV.VgOV.CtaCte;
            }

            CONTAB01.Est_Contab[k].Tipo_Mov = MODGLOV.VgOV.TipMov;
            CONTAB01.Est_Contab[k].Of_Origen = MODGLOV.VgOV.CodOfi.Str().TrimB();
            CONTAB01.Est_Contab[k].Swift = MODGLOV.VgOV.CodSwf;
            CONTAB01.Est_Contab[k].CodBco = MODGLOV.VgOV.CodBco;
            CONTAB01.Est_Contab[k].fecVen = MODGLOV.VgOV.fecVen;
            CONTAB01.Est_Contab[k].Num_Ref = MODGLOV.VgOV.numdoc;
            CONTAB01.Est_Contab[k].Num_Cheque = MODGLOV.VgOV.numdoc;
            CONTAB01.Est_Contab[k].Num_Reembolso = MODGLOV.VgOV.numdoc;
            if (MODGLOV.VgOV.Id_Cta > 0)
            {
                CONTAB01.Est_Contab[k].Ind_Cuenta = MODGLOV.VgOV.Id_Cta;
            }
            if (MODGLOV.VgOV.IdPrty != "")
            {
                CONTAB01.Est_Contab[k].Party.LlaveArchivo = MODGLOV.VgOV.IdPrty;
            }

            // Est_Contab(i%).party.LlaveArchivo = s
            // Est_Contab(i%).Glosa = s
            // Est_Contab(i%).nombre_of = s
            // Est_Contab(i%).ind_benef = n
            // Est_Contab(i%).moneda_arb = s
            // Est_Contab(i%).party.IndNombre = n
            // Est_Contab(i%).party.IndDireccion = n

        }

        private static int Nueva_Partida(DatosGlobales Globales)
        {
            tipo_partidas[] partidas = Globales.MOD_ADIC.partidas;

            if (!(partidas.Length == 1 && String.IsNullOrEmpty(partidas[0].Monto))) //esto es pq la 1era vez el length ya es 1 pero no hay ningun monto cargado
            {
                int cantItemsAntesDeAgregar = partidas.Length;
                Array.Resize(ref partidas, cantItemsAntesDeAgregar + 1);
                partidas[cantItemsAntesDeAgregar] = new tipo_partidas();

                Globales.MOD_ADIC.partidas = partidas;
                return cantItemsAntesDeAgregar;
            }

            return 0;
        }

        private static void cancela_envios(DatosGlobales Globales, int debe)
        {
            //T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;
            //T_GLOBAL GLOBAL = Globales.GLOBAL;
            //UI_gl gl = Globales.gl;
            //int nro = 0;

            //nro = BCH.Comex.Core.BL.XGGL.Modulos.MOD_ADIC.ind_numero(Globales,MOD_ADIC.ind_partida);

            //if (debe != 0)
            //{
            //    if (MOD_ADIC.Numeros[nro].origen.ToBool() && gl.origen_link.LinkMode != 0)
            //    {
            //        MOD_ADIC.prepara_servidor(gl.DefInstance.origen_link, MOD_ADIC.Numeros[nro].num_op);
            //        gl.DefInstance.origen_link.LinkExecute "[CANCELAR_ESP]";
            //    }
            //}
            //else
            //{
            //    if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].NomMoneda.LCase() == GLOBAL.moneda_nac.LCase())
            //    {
            //        if (MOD_ADIC.Numeros[nro].Vuelto.ToBool() && gl.DefInstance.vias_link.LinkMode != 0)
            //        {
            //            MOD_ADIC.prepara_servidor_vias(gl.DefInstance.vias_link, CONTAB01.COMO_VUELTOS, MOD_ADIC.Numeros[nro].num_op);
            //            gl.DefInstance.vias_link.LinkExecute "[CANCELAR_ESP]";
            //        }
            //    }
            //    else
            //    {
            //        if (MOD_ADIC.Numeros[nro].via.ToBool() && gl.DefInstance.vias_link.LinkMode != 0)
            //        {
            //            MOD_ADIC.prepara_servidor_vias(gl.DefInstance.vias_link, CONTAB01.COMO_VIAS, MOD_ADIC.Numeros[nro].num_op);
            //            gl.DefInstance.vias_link.LinkExecute "[CANCELAR_ESP]";
            //        }
            //    }
            //}

        }

        private static int que_indice(DatosGlobales Globales, string nemo, string Moneda)
        {
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            int que_indice = 0;

            int i = 0;

            for (i = 0; i < GLOBAL.ovd.GetUpperBound(0); i += 1)
            {
                if (GLOBAL.ovd[i].Nemonico.TrimB() == nemo.TrimB())
                {
                    que_indice = GLOBAL.ovd[i].id_cuenta;
                    return que_indice;
                }
            }

            if (Moneda.LCase() == GLOBAL.moneda_nac.LCase())
            {
                que_indice = T_CONTAB01.ONMN;
            }
            else
            {
                que_indice = T_CONTAB01.ONME;
            }

            return que_indice;
        }

        private static UI_GridItem GridItemFromString(string aux)
        {
            var itemS = new UI_GridItem();
            var arrValsS = aux.Split(9.Char());
            for (var i = 0; i < arrValsS.Length; i++)
            {
                itemS.AddColumn(i.ToString(), arrValsS[i]);
            }
            itemS.Tag = aux;
            return itemS;
        }

        private static Tuple<double, double> ItemsValueForKey(string key, Dictionary<string, List<Tuple<string, double, double, string, int>>> dic)
        {
            decimal montoDebe = 0;
            decimal montoHaber = 0;
            var items = dic[key];
            foreach (var item in items)
            {
                montoDebe += (decimal)item.Item2;
                montoHaber += (decimal)item.Item3;
            }
            return new Tuple<double, double>(montoDebe.ToDbl(), montoHaber.ToDbl());
        }

        private static void escribe_lista(DatosGlobales Globales)
        {
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;
            UI_gl gl = Globales.gl;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

            int I_Clie = 0;
            double t_debe = 0, t_haber = 0;
            int indx = MOD_ADIC.ind_partida;

            verifica_cuentas(Globales);

            //primero genero la tupla
            string nemonicoCuenta = String.Empty;
            double montoDebe = 0;
            double montoHaber = 0;
            string participante = String.Empty;
            string nemonicoMoneda = String.Empty;

            nemonicoCuenta = MOD_ADIC.partidas[MOD_ADIC.ind_partida].Nemonico;
            if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].debe != 0)
            {
                montoDebe = Format.StringToDouble(MOD_ADIC.partidas[MOD_ADIC.ind_partida].Monto);
            }
            else
            {
                montoHaber = Format.StringToDouble(MOD_ADIC.partidas[MOD_ADIC.ind_partida].Monto);
            }
            if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].debe == 0)
            {
                switch (MOD_ADIC.partidas[MOD_ADIC.ind_partida].Ind_Cuenta)
                {
                    case T_CONTAB01.CHMEBCH:
                    case T_CONTAB01.CHMNBCH:
                    case T_CONTAB01.VVBCH:
                        participante = "por def. en doc.";
                        break;
                    case T_CONTAB01.OPOP:
                    case T_CONTAB01.OPC:
                        participante = "por def. en swift";
                        break;
                    default:
                        participante = MODGPYF0.copiardestring(MOD_ADIC.partidas[MOD_ADIC.ind_partida].Party.LlaveArchivo, "~", 1);
                        break;
                }
            }
            else
            {
                participante = MODGPYF0.copiardestring(MOD_ADIC.partidas[MOD_ADIC.ind_partida].Party.LlaveArchivo, "~", 1);
            }
            Tuple<string, double, double, string, int> tupla = new Tuple<string, double, double, string, int>(nemonicoCuenta, montoDebe, montoHaber, participante.Replace("|", ""), indx);
            //agrego la tupla al diccionario correcto
            bool esMN = true;
            nemonicoMoneda = MOD_ADIC.partidas[MOD_ADIC.ind_partida].NemMoneda.Trim();
            if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].NomMoneda.LCase().Trim() == GLOBAL.moneda_nac.Trim())
            {
                if (!gl.Dic_MN.ContainsKey(nemonicoMoneda))
                {
                    gl.Dic_MN.Add(nemonicoMoneda, new List<Tuple<string, double, double, string, int>>());
                }
                if (gl.Dic_MN[nemonicoMoneda].Count() > 0 && gl.m_n.ListIndex != -1)
                {
                    gl.Dic_MN[nemonicoMoneda].RemoveAll(i => i.Item5 == indx);
                }
                gl.Dic_MN[nemonicoMoneda].Add(tupla);
            }
            else
            {
                esMN = false;
                if (!gl.Dic_ME.ContainsKey(nemonicoMoneda))
                {
                    gl.Dic_ME.Add(nemonicoMoneda, new List<Tuple<string, double, double, string, int>>());
                }

                if (gl.Dic_ME[nemonicoMoneda].Count() > 0 && gl.m_e.ListIndex != -1)
                {
                    gl.Dic_ME[nemonicoMoneda].RemoveAll(i => i.Item5 == indx);
                }

                gl.Dic_ME[nemonicoMoneda].Add(tupla);

            }

            if (gl.cambiar.Checked)
            {
                SYGETPRT.PartysOpe[I_Clie] = gl.prty_cliente;
                gl.prty_cliente = gl.PartyVacio.Copy();
            }
            //calcula(Globales,MOD_ADIC.partidas[MOD_ADIC.ind_partida].NomMoneda, MOD_ADIC.partidas[MOD_ADIC.ind_partida].NemMoneda);
            //if (gl.nommon_ant != "")
            //{
            //    calcula(Globales, gl.nommon_ant, gl.nemmon_ant);
            //}

            calcula(Globales);
            var item = ItemsValueForKey(nemonicoMoneda, esMN ? gl.Dic_MN : gl.Dic_ME);

            t_debe = item.Item1;
            t_haber = item.Item2;


            if (t_debe <= t_haber)
            {
                gl.tipo_000.Checked = true;
                gl.tipo_001.Checked = false;
            }
            else
            {
                gl.tipo_000.Checked = false;
                gl.tipo_001.Checked = true;
            }
            if (t_debe == t_haber)
            {
                gl.misma_moneda = false.ToInt();
            }
            else
            {
                gl.misma_moneda = true.ToInt();
            }
            limpia(Globales);

            if (gl.Bot_Partys.Enabled)
            {
                string res = String.Join("||", gl.m_n.Items.Select(x => (string)x.Tag).ToArray());
                MODGL.PicEnabled(gl.Bot_Partys, false.ToInt());
                MODGL.PicEnabled(gl.bot_operacion, false.ToInt());
                MODGL.PicEnabled(gl.bot_factura, false.ToInt());
                // bot_factura.Enabled = False
                gl.Bot_Partys.Enabled = false;
                gl.bot_operacion.Enabled = false;
                gl.bot_factura.Enabled = false;
            }

        }

        private static IEnumerable<UI_GridItem> GenerateGridItemsFrom(Dictionary<string, List<Tuple<string, double, double, string, int>>> dic)
        {
            List<UI_GridItem> items = new List<UI_GridItem>();
            foreach (string key in dic.Keys)
            {
                items.AddRange(GenerateGridItemsFromNemonico(key, dic[key]));
            }
            return (IEnumerable<UI_GridItem>)items;
        }

        private static IEnumerable<UI_GridItem> GenerateGridItemsFromNemonico(string key, List<Tuple<string, double, double, string, int>> list)
        {
            double monto_debe = 0;
            double monto_haber = 0;


            List<UI_GridItem> items = new List<UI_GridItem>();
            foreach (var item in list)
            {
                items.Add(GenerateGridItem(key, item));
                monto_debe += item.Item2;
                monto_haber += item.Item3;
            }
            //creo la ultima row con el total por moneda
            Tuple<string, double, double, string, int> tupleTotal = new Tuple<string, double, double, string, int>("---- Total ----", monto_debe, monto_haber, String.Empty, -1);
            items.Add(GenerateGridItem(key, tupleTotal));
            return (IEnumerable<UI_GridItem>)items;
        }

        private static UI_GridItem GenerateGridItem(string key, Tuple<string, double, double, string, int> item)
        {
            UI_GridItem gridItem = new UI_GridItem();
            gridItem.Data = item.Item5;
            gridItem.AddColumn("0", key);
            gridItem.AddColumn("1", item.Item1);
            gridItem.AddColumn("2", Format.FormatCurrency(item.Item2, T_GLOBAL.formato));
            gridItem.AddColumn("3", Format.FormatCurrency(item.Item3, T_GLOBAL.formato));
            gridItem.AddColumn("4", item.Item4);

            return gridItem;
        }

        //recalcula los totales
        private static void calcula(DatosGlobales Globales)
        {
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;
            UI_gl gl = Globales.gl;
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            int indx = Globales.gl.INDEX_LISTA;

            //escribo en ambas listas 
            gl.m_n.Items.Clear();
            gl.m_e.Items.Clear();
            gl.m_n.Items.AddRange(GenerateGridItemsFrom(gl.Dic_MN));
            gl.m_e.Items.AddRange(GenerateGridItemsFrom(gl.Dic_ME));


        }

        private static void verifica_cuentas(DatosGlobales Globales)
        {
            UI_gl gl = Globales.gl;
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;
            int i = 0;

            gl.hab_swift = false.ToInt();
            gl.hab_docval = false.ToInt();
            for (i = 0; i <= MOD_ADIC.partidas.GetUpperBound(0); i += 1)
            {
                if (((~(MOD_ADIC.partidas[i].debe == 1 ? -1 : 0)) & (~MOD_ADIC.partidas[i].Borrado)) != 0)
                {
                    switch (MOD_ADIC.partidas[i].Ind_Cuenta)
                    {
                        case T_CONTAB01.OPC:
                        case T_CONTAB01.OPOP:
                            gl.hab_swift = true.ToInt();
                            break;
                        case T_CONTAB01.VVBCH:
                        case T_CONTAB01.CHMEBCH:
                            gl.hab_docval = true.ToInt();
                            break;
                    }
                }
            }

        }

        private static bool cuenta_debe(DatosGlobales Globales, UnitOfWorkCext01 unit, string Mon, string Moneda, int codmnd, string nemo, int ind_ovd, string llave, bool saltarValidacionSaldo = false)
        {
            using (Tracer t = new Tracer("XGGL.Frm_gl: cuenta_debe"))
            {
                t.AddToContext("nemo", nemo);

                T_MODGLOV MODGLOV = Globales.MODGLOV;
                T_GLOBAL GLOBAL = Globales.GLOBAL;
                T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

                double Mto = 0.0;
                int leyo = 0;
                bool ext = false;
                //Se debe dejar en false por defecto, y solo cambiarlo a true cuando sea cuenta corriente
                Globales.gl.ValidaMontos = false;

                MODGLOV.VgOV = MODGLOV.VgOVNul.Copy();

                // Se limpian los datos de las cuentas corrientes
                GLOBAL.datos_cuenta = new cuenta_indice();
                Globales.MODCONGL.Cuentas = new T_CtasCtes[0];

                switch (GLOBAL.ovd[ind_ovd].id_cuenta)
                {
                    case T_CONTAB01.CTACTEMN:
                    case T_CONTAB01.CTACTEME:
                    case T_MODGCON0.IdCta_ChqCCME:
                        Globales.gl.ValidaMontos = true;
                        ext = true;
                        if (GLOBAL.ovd[ind_ovd].id_cuenta == T_CONTAB01.CTACTEMN)
                        {
                            ext = false;
                        }
                        leyo = SyLeeCuentas(Globales, unit, SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].LlaveArchivo, ext.ToInt(), "", true);
                        if (leyo == 0)
                        {
                            return true;
                        }
                        else if (leyo == -1)
                        {
                            Globales.gl.MON = Format.StringToDouble(Mon);
                            return false;
                        }

                        ////SE MUEVE A METODO ValidarMontos
                        ////if (GLOBAL.datos_cuenta.Cuenta != string.Empty && !saltarValidacionSaldo)
                        ////{
                        ////    // **************************
                        ////    // Karina Rojas
                        ////    // Valida Monto con Saldo de Ccte
                        ////    Mto = ModSaldo.Obtiene_Monto(Globales,unit, T_MODGCON0.IdCta_CtaCteMN, T_MODGCON0.IdCta_CtaCteME, GLOBAL.ovd[ind_ovd].id_cuenta, MODGPYF0.Componer(GLOBAL.datos_cuenta.Cuenta, "-", ""));
                        ////    if (Mto < Format.StringToDouble(Mon) && !saltarValidacionSaldo)
                        ////    {
                        ////        Globales.MESSAGES.Add(new UI_Message()
                        ////        {
                        ////            Type=TipoMensaje.YesNo,
                        ////            Title="Advertencia",
                        ////            Text= "Saldo disponible de la Cuenta Corriente ( " + MigrationSupport.Utils.Format(Mto, "##,###,###,##0.00") + " ) es menor que el Monto a Cubrir. ¿Desea Continuar?"
                        ////        });


                        ////        return false;
                        ////    }
                        ////}
                        //si al leer la cuenta no encuentra ninguna, no hace nada (comportamiento legacy)
                        if (string.IsNullOrEmpty(GLOBAL.datos_cuenta.Cuenta))
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = "El participante no tiene cuenta corriente en la moneda seleccionada",
                                Title = T_MODGLORI.MsgxOri,
                                Type = TipoMensaje.Informacion
                            });
                            return false;
                        }
                        cuenta_debe_switch(Globales, unit);
                        break;
                    case T_CONTAB01.SCSMN:
                    case T_CONTAB01.SCSME:
                    case T_CONTAB01.CHMEBCH:
                    case T_CONTAB01.OPC:
                    case T_CONTAB01.OPOP:
                    case T_CONTAB01.VVBCH:
                    case T_CONTAB01.VVOB:
                        // Llama al formulario Origen/Via.-
                        MODGLOV.VgOV.TipoOV = "O";
                        MODGLOV.VgOV.codmnd = codmnd;
                        MODGLOV.VgOV.MtoMnd = Mon.ToVal();
                        MODGLOV.VgOV.Id_Cta = GLOBAL.ovd[ind_ovd].id_cuenta;
                        Globales.gl.IND_OVD = ind_ovd;
                        Globales.FrmGLOV = new UI_FrmGLOV();
                        Globales.Action = "Adicionales";
                        Globales.VieneDeAction = "Aceptar_Despues_CuentaDebe_Adicionales";
                        Globales.VieneDeController = "Home";
                        //cuenta_debe_switch(Globales, unit);
                        return false;
                    case T_MODGCON0.IdCta_CHVBNYM:
                    case T_CONTAB01.CTAORD:
                    case T_CONTAB01.CTACTEBC:
                    case T_CONTAB01.VAM:
                    case T_CONTAB01.VAX:
                    case T_CONTAB01.VAMC:
                    case T_CONTAB01.CHMEOBC:
                    case T_CONTAB01.DIVENPEN:
                        // Llama al formulario Origen/Via.-
                        MODGLOV.VgOV.TipoOV = "O";
                        MODGLOV.VgOV.codmnd = codmnd;
                        MODGLOV.VgOV.MtoMnd = Mon.ToVal();
                        MODGLOV.VgOV.Id_Cta = GLOBAL.ovd[ind_ovd].id_cuenta;
                        Globales.gl.IND_OVD = ind_ovd;
                        Globales.FrmGLOV = new UI_FrmGLOV();
                        Globales.Action = "Adicionales";
                        Globales.VieneDeAction = "Aceptar_Despues_CuentaDebe_Adicionales";
                        Globales.VieneDeController = "Home";
                        //cuenta_debe_switch(Globales, unit);
                        return false;
                    default:
                        // Validamos si es una obligacion
                        if(unit.SceRepository.EsObligacion(GLOBAL.ovd[ind_ovd].id_cuenta))
                        {
                            // Llama al formulario Origen/Via.-
                            MODGLOV.VgOV.TipoOV = "O";
                            MODGLOV.VgOV.codmnd = codmnd;
                            MODGLOV.VgOV.MtoMnd = Mon.ToVal();
                            MODGLOV.VgOV.Id_Cta = GLOBAL.ovd[ind_ovd].id_cuenta;
                            Globales.gl.IND_OVD = ind_ovd;
                            Globales.FrmGLOV = new UI_FrmGLOV();
                            Globales.Action = "Adicionales";
                            Globales.VieneDeAction = "Aceptar_Despues_CuentaDebe_Adicionales";
                            Globales.VieneDeController = "Home";
                            return false;
                        }
                        // Guardar Nemonico, Ind_Cuenta, Datos en la Estructura.-
                        cuenta_debe_switch(Globales, unit);
                        break;
                }
                //cuenta_debe_switch(Globales, unit);
                return true;
            }
        }

        private static void cuenta_debe_switch_IdCta_ChqCCME(DatosGlobales Globales, UnitOfWorkCext01 unit, string Mon, string Moneda, int codmnd, string nemo, int ind_ovd, string llave)
        {
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            // ***************************
            if (GLOBAL.datos_cuenta.Moneda.ToVal() == codmnd)
            {
                cuenta_debe_switch(Globales, unit);
            }
        }

        private static void cuenta_debe_switch(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {

            if (Globales.gl.ValidaMontos)
            {
                if (ValidarMontos(Globales, unit, Globales.gl.Mon, Globales.gl.ind_ovd))
                {
                    ingresa_datos(Globales, unit, true.ToInt(), false.ToInt());
                    escribe_lista(Globales);
                    //se agrega para limpiar las variables de seleccion
                    Globales.gl.delista = 0;
                    Globales.gl.m_n.ListIndex = -1;
                    Globales.gl.m_e.ListIndex = -1;
                }
            }
            else
            {
                ingresa_datos(Globales, unit, true.ToInt(), false.ToInt());
                escribe_lista(Globales);
                //se agrega para limpiar las variables de seleccion
                Globales.gl.delista = 0;
                Globales.gl.m_n.ListIndex = -1;
                Globales.gl.m_e.ListIndex = -1;
            }
        }

        private static int SyLeeCuentas(DatosGlobales Globales, UnitOfWorkCext01 unit, string llave, int ext, string mone, bool debe)
        {

            T_GLOBAL GLOBAL = Globales.GLOBAL;
            T_MODCONGL MODCONGL = Globales.MODCONGL;
            T_SYGETPRT SYGETPRT = Globales.SYGETPRT;
            UI_gl gl = Globales.gl;

            int SyLeeCuentas = 0;

            string s = "";
            string Moneda = "";
            string c = "";
            int Activa = 0;
            int estado = 0;
            int i = 0;
            int Total = 0;
            int SiCuentas = 0;
            int flag = 0;
            cuenta_indice limpia_datos = new cuenta_indice();

            using (var tracer = new Tracer("SyLeeCuentas"))
            {

                try
                {

                    GLOBAL.datos_cuenta = limpia_datos;
                    MODCONGL.Cuentas = new T_CtasCtes[1];
                    flag = SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].FlagParty;
                    SiCuentas = BCH.Comex.Core.BL.XGGL.Modulos.SYGETPRT.PrtyFlag(flag, T_SYGETPRT.GPrt_FlagCtas);
                    if (SiCuentas != 0)
                    {
                        var res = unit.SceRepository.EjecutarSP<pro_sce_ctas_s01_MS_Result>("pro_sce_ctas_s01_MS", MODGSYB.dbcharSy(llave), "0", MODGSYB.dblogisy((short)ext));
                        Total = res.Count;
                        if (Total > 0)
                        {
                            MODCONGL.Cuentas = new T_CtasCtes[1];
                            MODCONGL.Cuentas[0] = new T_CtasCtes();
                            for (i = 1; i <= Total; i += 1)
                            {
                                estado = res[i - 1].borrado.ToInt();
                                Activa = res[i - 1].activace.ToInt();
                                c = res[i - 1].cuenta;
                                Moneda = res[i - 1].moneda.ToString();
                                if (estado == 0)
                                {
                                    if (Activa != 0)
                                    {
                                        if (c.Len() > 8)
                                        {
                                            if (ext != 0)
                                            {
                                                c = c.Left(4) + "-" + c.Mid(5, 5) + "-" + c.Right(2);
                                            }
                                            else
                                            {
                                                c = c.Left(3) + "-" + c.Mid(4, 5) + "-" + c.Mid(9, 2);
                                            }
                                        }

                                        Array.Resize(ref MODCONGL.Cuentas, MODCONGL.Cuentas.Length + 1);
                                        //MODCONGL.Cuentas = new T_CtasCtes[i + 1];
                                        GLOBAL.datos_cuenta.Cuenta = c;
                                        GLOBAL.datos_cuenta.llave = llave;
                                        GLOBAL.datos_cuenta.Moneda = Moneda;
                                        MODCONGL.Cuentas[MODCONGL.Cuentas.Length - 1] = new T_CtasCtes();
                                        MODCONGL.Cuentas[MODCONGL.Cuentas.Length - 1].Cuenta = c;
                                        MODCONGL.Cuentas[MODCONGL.Cuentas.Length - 1].llave = llave;
                                        MODCONGL.Cuentas[MODCONGL.Cuentas.Length - 1].Moneda = Moneda.ToInt();
                                    }
                                }
                            }

                            Total = 0;
                            int codMonedaSeleccionada = gl.monedas.get_ItemData_(gl.monedas.ListIndex).ToInt();
                            List<T_CtasCtes> cuentasMonedaSeleccionada = MODCONGL.Cuentas.Where(x => x.Moneda == codMonedaSeleccionada).ToList();
                            Total = cuentasMonedaSeleccionada.Count;

                            if (Total > 1)
                            {
                                Globales.FrmGLOV = new UI_FrmGLOV();
                                Globales.Action = "Adicionales";
                                Globales.VieneDeAction = "Aceptar_Despues_SyLeeCuentas_Adicionales_" + (debe ? "Debe" : "Haber");
                                Globales.VieneDeController = "Home";
                                return -1;
                            }
                            else if (Total == 1) // Este es por si cuentasMonedaSeleccionada tiene datos.
                            {
                                GLOBAL.datos_cuenta.Cuenta = cuentasMonedaSeleccionada[0].Cuenta;
                                GLOBAL.datos_cuenta.llave = cuentasMonedaSeleccionada[0].llave;
                                GLOBAL.datos_cuenta.Moneda = MigrationSupport.Utils.Format(cuentasMonedaSeleccionada[0].Moneda, "00");
                            }
                            else
                            {
                                GLOBAL.datos_cuenta.Cuenta = MODCONGL.Cuentas[0].Cuenta;
                                GLOBAL.datos_cuenta.llave = MODCONGL.Cuentas[0].llave;
                                GLOBAL.datos_cuenta.Moneda = MigrationSupport.Utils.Format(MODCONGL.Cuentas[0].Moneda, "00");
                            }

                            SyLeeCuentas = true.ToInt();
                        }
                        else
                        {
                            if (ext != 0)
                            {
                                if (mone == "")
                                {
                                    s = "moneda extranjera.";
                                }
                                else
                                {
                                    s = mone + ".";
                                }
                            }
                            else
                            {
                                s = "moneda nacional.";
                            }
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = "Participante no tiene cuentas corrientes en " + s,
                                Title = T_SYGETPRT.GPrt_Caption,
                                Type = TipoMensaje.Error
                            });
                        }
                    }
                    else
                    {
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Text = "Participante no tiene cuentas corrientes.",
                            Title = T_SYGETPRT.GPrt_Caption,
                            Type = TipoMensaje.Error
                        });
                    }

                    return SyLeeCuentas;

                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta al leer cuentas", exc);

                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Text = "Se ha producido un error al tratar de leer los datos de los Participantes.",
                        Title = T_SYGETPRT.GPrt_Caption,
                        Type = TipoMensaje.Error
                    });
                }
            }

            return SyLeeCuentas;
        }

        private static bool cuenta_haber(DatosGlobales Globales, UnitOfWorkCext01 unit, string Mon, string Moneda, int codmnd, string nemo, int ind_ovd, string llave)
        {
            using (Tracer t = new Tracer("XGGL.Frm_gl: cuenta_haber"))
            {
                t.AddToContext("nemo", nemo);

                T_MODGLOV MODGLOV = Globales.MODGLOV;
                T_GLOBAL GLOBAL = Globales.GLOBAL;
                T_SYGETPRT SYGETPRT = Globales.SYGETPRT;

                int leyo = 0;

                MODGLOV.VgOV = MODGLOV.VgOVNul.Copy();

                // Se limpian los datos de las cuentas corrientes
                GLOBAL.datos_cuenta = new cuenta_indice();
                Globales.MODCONGL.Cuentas = new T_CtasCtes[0];

                switch (GLOBAL.ovd[ind_ovd].id_cuenta)
                {
                    case T_CONTAB01.CTACTEMN:
                        leyo = SyLeeCuentas(Globales, unit, SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].LlaveArchivo, false.ToInt(), "", false);
                        if (leyo == 0)
                        {
                            return true;
                        }
                        else if (leyo == -1)
                        {
                            Globales.gl.MON = Format.StringToDouble(Mon);
                            return false;
                        }
                        if (GLOBAL.datos_cuenta.Cuenta != "")
                        {
                            if (GLOBAL.datos_cuenta.Moneda.ToVal() == codmnd)
                            {
                                MODGLOV.VgOV.Id_Cta = GLOBAL.ovd[ind_ovd].id_cuenta;
                                // partidas(ind_partida).CtaCte = datos_cuenta.Cuenta
                                cuenta_haber_switch(Globales, unit);
                                return true;
                            }
                        }
                        break;
                    case T_CONTAB01.CTACTEME:
                    case T_MODGCON0.IdCta_ChqCCME:
                        leyo = SyLeeCuentas(Globales, unit, SYGETPRT.PartysOpe[T_GLOBAL.I_Cli].LlaveArchivo, true.ToInt(), Moneda, false);
                        if (leyo == 0)
                        {
                            return true;
                        }
                        else if (leyo == -1)
                        {
                            return false;
                        }
                        if (GLOBAL.datos_cuenta.Cuenta != "")
                        {
                            if (GLOBAL.datos_cuenta.Moneda.TrimB() == MODGL.moneda_reves(Globales, Moneda).TrimB())
                            {
                                // partidas(ind_partida).CtaCte = datos_cuenta.Cuenta
                                cuenta_haber_switch(Globales, unit);
                                return true;
                            }
                        }
                        else
                        {
                            Globales.MESSAGES.Add(new UI_Message()
                            {
                                Text = "El participante no tiene cuenta corriente en la moneda seleccionada",
                                Title = T_MODGLORI.MsgxOri,
                                Type = TipoMensaje.Informacion
                            });
                            return false;
                        }
                        break;
                    case T_CONTAB01.SCSMN:
                    case T_CONTAB01.SCSME:
                    case T_CONTAB01.CTAORD:
                    case T_CONTAB01.CTACTEBC:
                    case T_CONTAB01.VAX:
                    case T_CONTAB01.VAM:
                    case T_CONTAB01.VAMC:
                    case T_CONTAB01.DIVENPEN:
                    case T_MODGCON0.IdCta_CHVBNYM:
                        // Llama al formulario Origen/Via.-
                        MODGLOV.VgOV.TipoOV = "V";
                        MODGLOV.VgOV.codmnd = codmnd;
                        MODGLOV.VgOV.MtoMnd = Mon.ToVal();
                        MODGLOV.VgOV.Id_Cta = GLOBAL.ovd[ind_ovd].id_cuenta;
                        Globales.FrmGLOV = new UI_FrmGLOV();
                        Globales.Action = "Adicionales";
                        Globales.VieneDeAction = "Aceptar_Despues_CuentaHaber_Adicionales";
                        Globales.VieneDeController = "Home";
                        return false;
                    default:
                        // Validamos si es una obligacion
                        if (unit.SceRepository.EsObligacion(GLOBAL.ovd[ind_ovd].id_cuenta))
                        {
                            // Llama al formulario Origen/Via.-
                            MODGLOV.VgOV.TipoOV = "V";
                            MODGLOV.VgOV.codmnd = codmnd;
                            MODGLOV.VgOV.MtoMnd = Mon.ToVal();
                            MODGLOV.VgOV.Id_Cta = GLOBAL.ovd[ind_ovd].id_cuenta;
                            Globales.FrmGLOV = new UI_FrmGLOV();
                            Globales.Action = "Adicionales";
                            Globales.VieneDeAction = "Aceptar_Despues_CuentaHaber_Adicionales";
                            Globales.VieneDeController = "Home";
                            return false;
                        }
                        // Guardar Nemonico, Ind_Cuenta, Datos en la Estructura.-
                        cuenta_haber_switch(Globales, unit);
                        break;
                }

                return true;
            }
        }

        private static void cuenta_haber_switch(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            ingresa_datos(Globales, unit, false.ToInt(), false.ToInt());
            escribe_lista(Globales);
        }

        public static void cuenta_haber_IdCta_CHVBNYM(DatosGlobales Globales, UnitOfWorkCext01 unit, string Mon, string Moneda, int codmnd, string nemo, int ind_ovd, string llave)
        {
            cuenta_haber_switch(Globales, unit);
        }

        public static void Cancelar_Click(DatosGlobales Globales)
        {
            using (var tracer = new Tracer("GL - Cancelar_Click"))
            {
                string monedaD = Globales.gl.MONEDAD;
                bool esMN = Globales.gl.ESMN;
                int index = Globales.gl.INDEX_LISTA;

                UI_gl gl = Globales.gl;
                T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;
                T_CONTAB01 CONTAB01 = Globales.CONTAB01;

                int nro = 0;

                try
                {
                    if (esMN)
                    {
                        gl.Dic_MN[monedaD].RemoveAll(x => x.Item5 == index);
                        if (gl.Dic_MN[monedaD].Count == 0)
                        {
                            gl.Dic_MN.Remove(monedaD);
                        }
                    }
                    else
                    {
                        gl.Dic_ME[monedaD].RemoveAll(x => x.Item5 == index);
                        if (gl.Dic_ME[monedaD].Count == 0)
                        {
                            gl.Dic_ME.Remove(monedaD);
                        }

                    }

                    MOD_ADIC.partidas[MOD_ADIC.ind_partida].Borrado = true.ToInt();
                    CONTAB01.Est_Contab[MOD_ADIC.ind_partida].Borrado = true.ToInt();
                    if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].envio != 0)
                    {
                        nro = BCH.Comex.Core.BL.XGGL.Modulos.MOD_ADIC.ind_numero(Globales, MOD_ADIC.ind_partida);
                        MOD_ADIC.Numeros[nro].Borrado = true.ToInt();
                        if (MOD_ADIC.Numeros[nro].origen != 0)
                        {
                            //if (gl.DefInstance.origen_link.LinkMode != 0)
                            //{
                            //    MOD_ADIC.prepara_servidor(gl.DefInstance.origen_link, MOD_ADIC.Numeros[nro].num_op);
                            //    gl.DefInstance.origen_link.LinkExecute "[CANCELAR_ESP]";
                            //}
                        }
                        if (MOD_ADIC.Numeros[nro].via != 0)
                        {
                            //if (gl.DefInstance.vias_link.LinkMode != 0)
                            //{
                            //    MOD_ADIC.prepara_servidor_vias(gl.DefInstance.vias_link, CONTAB01.COMO_VIAS, MOD_ADIC.Numeros[nro].num_op);
                            //    gl.DefInstance.vias_link.LinkExecute "[CANCELAR_ESP]";
                            //}
                        }
                        if (MOD_ADIC.Numeros[nro].Vuelto != 0)
                        {
                            //if (gl.DefInstance.vias_link.LinkMode != 0)
                            //{
                            //    MOD_ADIC.prepara_servidor_vias(gl.DefInstance.vias_link, CONTAB01.COMO_VUELTOS, MOD_ADIC.Numeros[nro].num_op);
                            //    gl.DefInstance.vias_link.LinkExecute "[CANCELAR_ESP]";
                            //}
                        }
                    }

                    calcula(Globales);
                    verifica_cuentas(Globales);
                    if (gl.m_e.Items.Count == 0 && gl.m_n.Items.Count == 0)
                    {
                        MODGL.PicEnabled(gl.Bot_Partys, true.ToInt());
                        MODGL.PicEnabled(gl.bot_operacion, true.ToInt());
                        MODGL.PicEnabled(gl.bot_factura, true.ToInt());
                        // bot_factura.Enabled = True
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Error en GL al cancelar", ex);
                }
                finally
                {
                    // limpiar
                    limpia(Globales);
                    gl.monedas.ListIndex = -1;

                    //  Performance
                    //  12-06-2009
                    //  Al eliminar debe limpiar y ocultar objetos de numero de partida
                    LimpiaNumPartida(Globales);
                }
            }
        }

        public static void LimpiaNumPartida(DatosGlobales Globales)
        {
            //  Performance
            //  12-06-2009
            //  Limpia los valores y oculta objetos.
            Globales.gl.LB_Referencia.Visible = false;
            Globales.gl.txtNumRef.Text = "";
            Globales.gl.txtNumRef.Visible = false;

        }

        #region LLAMADAS PARA CUANDO VUELVE DE GOV
        public static void Aceptar_Despues_GOV_1(DatosGlobales Globales)
        {
            Globales.gl.LB_Referencia.Visible = false;
            Globales.gl.txtNumRef.Text = "";
            Globales.gl.txtNumRef.Visible = false;
            Globales.Action = "Index";
        }

        public static bool Aceptar_Despues_GOV_2(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            T_CONTAB01 CONTAB01 = Globales.CONTAB01;
            T_MODGLOV MODGLOV = Globales.MODGLOV;
            T_MOD_ADIC MOD_ADIC = Globales.MOD_ADIC;
            UI_gl gl = Globales.gl;

            string nemo = gl.nemonico.Text.TrimB();
            int codmnd = gl.monedas.get_ItemData_(gl.monedas.ListIndex).ToInt();
            string Mon = gl.monto.Text;
            int debe = (gl.tipo[0]).Value;


            if (nemo == CONTAB01.NEMO_PAR || nemo == CONTAB01.REEM_CHEQUE)
            {
                if (MOD_ADIC.partidas[MOD_ADIC.ind_partida].envio != 0)
                {

                    // Llama al formulario Origen/Via.-
                    MODGLOV.VgOV = MODGLOV.VgOVNul.Copy();
                    MODGLOV.VgOV.TipoOV = "";
                    MODGLOV.VgOV.codmnd = codmnd;
                    MODGLOV.VgOV.MtoMnd = Mon.ToVal();
                    MODGLOV.VgOV.Id_Cta = 0;

                    Globales.FrmGLOV = new UI_FrmGLOV();
                    Globales.Action = "Adicionales";
                    Globales.VieneDeAction = "Aceptar_Despues_Aceptar_Adicionales_1";
                    Globales.VieneDeController = "Home";
                    return false;
                }
            }
            else
            {
                // *************************************************************
                // Cambié ahora a una cuenta que NO requiere datos adicionales.-
                // *************************************************************
                MODGLOV.VgOV = MODGLOV.VgOVNul.Copy();
                ingresa_datos(Globales, unit, debe, false.ToInt());
                escribe_lista(Globales);
            }
            Aceptar_Despues_GOV_1(Globales);
            return true;
        }

        public static void CuentaDebe_Despues_GOV(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            cuenta_debe_switch(Globales, unit);
        }

        public static bool SyLeeCuentas_Despues_GOV_Debe(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            double Mon = Globales.gl.MON;
            if (GLOBAL.datos_cuenta.Cuenta != "")
            {
                if (!Globales.vieneDeMsg)
                {
                    // **************************
                    // Karina Rojas
                    // Valida Monto con Saldo de Ccte
                    int ind_ovd = Globales.gl.IND_OVD;
                    double Mto = ModSaldo.Obtiene_Monto(Globales, unit, T_MODGCON0.IdCta_CtaCteMN, T_MODGCON0.IdCta_CtaCteME, GLOBAL.ovd[ind_ovd].id_cuenta, MODGPYF0.Componer(GLOBAL.datos_cuenta.Cuenta, "-", ""));
                    if (Mto < Mon)
                    {
                        //Globales.MESSAGES.Add(new UI_Message()
                        //{
                        //    Type = TipoMensaje.YesNo,
                        //    Title = "Advertencia",
                        //    Text = "Saldo disponible de la Cuenta Corriente ( " + MigrationSupport.Utils.Format(Mto, "##,###,###,##0.00") + " ) es menor que el Monto a Cubrir. ¿Desea Continuar?"
                        //});
                        Globales.Action = "Aceptar_Despues_CuentaDebe_Adicionales";
                        Globales.VieneDeAction = "Aceptar_Despues_SyLeeCuentas_Adicionales_Debe";
                        Globales.VieneDeController = "Home";
                        return false;
                    }

                }
                else
                {
                    if (!Globales.resMsg)
                    {
                        return true;
                    }
                }

            }
            cuenta_debe_switch(Globales, unit);
            return true;
        }

        public static void CuentaHaber_Despues_GOV(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            cuenta_haber_switch(Globales, unit);
            //se agrega para limpiar las variables de seleccion
            Globales.gl.delista = 0;
            Globales.gl.m_n.ListIndex = -1;
            Globales.gl.m_e.ListIndex = -1;
        }

        public static bool SyLeeCuentas_Despues_GOV_Haber(DatosGlobales Globales, UnitOfWorkCext01 unit)
        {
            cuenta_haber_switch(Globales, unit);
            //se agrega para limpiar las variables de seleccion
            Globales.gl.delista = 0;
            Globales.gl.m_n.ListIndex = -1;
            Globales.gl.m_e.ListIndex = -1;
            return true;
        }
        #endregion

        private static bool ValidarMontos(DatosGlobales Globales, UnitOfWorkCext01 unit, string Mon, int ind_ovd)
        {
            T_GLOBAL GLOBAL = Globales.GLOBAL;
            var Mto = ModSaldo.Obtiene_Monto(Globales, unit, T_MODGCON0.IdCta_CtaCteMN, T_MODGCON0.IdCta_CtaCteME, GLOBAL.ovd[ind_ovd].id_cuenta, MODGPYF0.Componer(GLOBAL.datos_cuenta.Cuenta, "-", ""));
            if (Mto < Format.StringToDouble(Mon))
            {
                Globales.MESSAGES.Add(new UI_Message()
                {
                    Type = TipoMensaje.YesNo,
                    Title = "Advertencia",
                    Text = "Saldo disponible de la Cuenta Corriente ( " + MigrationSupport.Utils.Format(Mto, "##,###,###,##0.00") + " ) es menor que el Monto a Cubrir. ¿Desea Continuar?"
                });

                return false;
            }
            return true;
        }
    }
}
