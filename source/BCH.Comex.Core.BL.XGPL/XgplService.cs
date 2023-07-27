using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Cext01;
using BCH.Comex.Utils;
using CodeArchitects.VB6Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Configuration;

namespace BCH.Comex.Core.BL.XGPL
{
    public class TablaMonedaItem
    {
        // mnd_mndcod	mnd_mndcbc	mnd_mndnom	mnd_mndnmc	mnd_mndswf
        // 1	999	PESO CHILENO                  	$  	CLP
        public decimal Codigo { get; set; }
        public decimal MndCBC { get; set; }
        public string Nombre { get; set; }
        public string Simbolo { get; set; }
        public string CodigoSwift { get; set; }
    }

    public class XgplService
    {
        private UnitOfWorkCext01 uow;

        //private static XgplService instance;

        // TODO: Ubicar estos datos en el lugar apropiado!
        private IDictionary<decimal, TablaMonedaItem> tablaMoneda = null;
        private IDictionary<decimal, TablaMonedaItem> tablaMonedaBancoCentral = null;

        private UsuarioEspecialista usuarioEstacion = null;
        private IDictionary<decimal, string> tablaPlazaBancoCentral = null;
        private IDictionary<decimal, string> tablaAduana = null;
        private IDatosUsuario datosUsuario;

        //public static XgplService Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new XgplService();
        //        }
        //        return instance;
        //    }
        //}

        public XgplService(IDatosUsuario datosUsuario)
        {
            this.datosUsuario = datosUsuario;
            uow = new UnitOfWorkCext01();
        }

        public XgplService()
        {
            uow = new UnitOfWorkCext01();
        }

        public List<sce_usr_s02_MS_Result> Sce_Usr_S02(string CentroCosto, string CodigoUsuario)
        {
            return uow.SceRepository.sce_usr_s02_MS(CentroCosto, CodigoUsuario);
        }

        public IQueryable<sce_xplv_s08_MS_Result> Sce_Xplv_S08(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return uow.SceRepository.sce_xplv_s08(CentroCosto, CodigoUsuario, FechaIngreso);
        }


        public IQueryable<sce_pli_s05_MS_Result> Sce_Pli_S05(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return uow.SceRepository.sce_pli_s05(CentroCosto, CodigoUsuario, FechaIngreso);
        }

        public sce_pli_s06_MS_Result Sce_Xpli_S06(string NumeroPlanilla, DateTime Fecha)
        {
            return uow.SceRepository.sce_pli_s06(NumeroPlanilla, Fecha).FirstOrDefault();
        }

        public IQueryable<sce_xanu_s01_MS_Result> Sce_XAnu_S01(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return uow.SceRepository.sce_xanu_s01(CentroCosto, CodigoUsuario, FechaIngreso);
        }

        /// <summary>
        /// Planillas Visibles de Importaciones Endosadas
        /// </summary>
        /// <param name="CentroCosto"></param>
        /// <param name="CodigoUsuario"></param>
        /// <returns></returns>
        public IQueryable<sce_plan_s07_MS_Result> Sce_Plan_S07(string CentroCosto, string CodigoUsuario)
        {
            return uow.SceRepository.sce_plan_s07(CentroCosto, CodigoUsuario);
        }

        private IDictionary<decimal, TablaMonedaItem> TablaMoneda()
        {
            if (tablaMoneda == null)
            {
                InicializarTablasMoneda();
            }
            return tablaMoneda;
        }

        private void InicializarTablasMoneda()
        {
            var arr = uow.SgtRepository.Sgt_Mnd_S02().Select(m => new TablaMonedaItem()
            {
                Codigo = m.mnd_mndcod,
                MndCBC = m.mnd_mndcbc,
                Nombre = m.mnd_mndnom,
                Simbolo = m.mnd_mndnmc,
                CodigoSwift = m.mnd_mndswf
            }).ToList();
            tablaMoneda = arr.ToDictionary(m => m.Codigo, m => m);
            tablaMonedaBancoCentral = arr.ToDictionary(m => m.MndCBC, m => m);
        }

        private IDictionary<decimal, TablaMonedaItem> TablaMonedaBancoCentral()
        {
            if (tablaMonedaBancoCentral == null)
            {
                InicializarTablasMoneda();
            }
            return tablaMonedaBancoCentral;
        }

        private TablaMonedaItem GetMoneda(int CodigoMoneda)
        {
            TablaMonedaItem item;
            var arr = uow.SgtRepository.Sgt_Mnd_S02().Select(m => new TablaMonedaItem()
            {
                Codigo = m.mnd_mndcod,
                MndCBC = m.mnd_mndcbc,
                Nombre = m.mnd_mndnom,
                Simbolo = m.mnd_mndnmc,
                CodigoSwift = m.mnd_mndswf
            }).ToList();
            tablaMoneda = arr.ToDictionary(m => m.Codigo, m => m);
            //tablaMonedaBancoCentral = arr.ToDictionary(m => m.MndCBC, m => m);
            if (tablaMoneda.TryGetValue(CodigoMoneda, out item))
            {
                return item;
            }
            return null;
        }

        public string GetNombreMoneda(int CodigoMoneda)
        {
            var item = GetMoneda(CodigoMoneda);
            return item != null ? item.Nombre : "";
        }

        public string GetNombreMonedaBancoCentral(int CodigoMonedaBancoCentral)
        {
            TablaMonedaItem item;
            var arr = uow.SgtRepository.Sgt_Mnd_S02().Select(m => new TablaMonedaItem()
            {
                Codigo = m.mnd_mndcod,
                MndCBC = m.mnd_mndcbc,
                Nombre = m.mnd_mndnom,
                Simbolo = m.mnd_mndnmc,
                CodigoSwift = m.mnd_mndswf
            }).ToList();
            //tablaMoneda = arr.ToDictionary(m => m.Codigo, m => m);
            tablaMonedaBancoCentral = arr.ToDictionary(m => m.MndCBC, m => m);
            if (tablaMonedaBancoCentral.TryGetValue(CodigoMonedaBancoCentral, out item))
            {
                return item.Nombre;
            }
            return "";
        }

        public string GetSimboloMoneda(int CodigoMoneda)
        {
            var item = GetMoneda(CodigoMoneda);
            return item != null ? item.Simbolo : "";
        }

        public string GetCodigoMonedaSwift(int CodigoMoneda)
        {
            var item = GetMoneda(CodigoMoneda);
            return item != null ? item.CodigoSwift : "";
        }

        public static string UsuarioAsignadoAEstacionDeTrabajo()
        {
            return WebConfigurationManager.AppSettings["FundTransfer.Identificacion.CCtUsr"];
        }

        public UsuarioEspecialista ObtenerUsuarioEstacion()
        { 
            //if (usuarioEstacion == null)
            //{
                var usuarioStr = this.datosUsuario.Identificacion_CCtUsr;
                //var usuarioStr = UsuarioAsignadoAEstacionDeTrabajo();
                if (usuarioStr.Length == 5)
                {
                    var CentroCosto = usuarioStr.Substring(0, 3);
                    var CodigoUsuario = usuarioStr.Substring(3, 2);

                    var usuario = this.Sce_Usr_S05_MS(CentroCosto, CodigoUsuario).FirstOrDefault();
                    if (usuario != null)
                    {
                        usuarioEstacion = new UsuarioEspecialista()
                        {
                            rut = usuario.rut,
                            Jerarquia = (short)usuario.jerarquia,
                            CentroCosto = usuario.cent_costo,
                            Especialista = usuario.id_especia,
                            Delegada = (short)(usuario.delegada ? -1 : 0),
                            CostoSuper = usuario.cent_super,
                            Id_Super = usuario.id_super,
                            Nombre = usuario.nombre,
                            Direccion = usuario.direccion,
                            comuna = usuario.comuna,
                            Ciudad = usuario.ciudad,
                            Seccion = usuario.seccion,
                            Oficina = (short)usuario.ofic_orige,
                            Telefono = usuario.telefono,
                            Swift = usuario.swift,
                            Fax = usuario.fax,
                            Tipeje = usuario.tipeje
                        };
                    }

                    /*
                    x = SyGet_OfiUsr(usuario.Left(3), usuario.Right(2));
                    if (x == 0)
                    {
                        Environment.Exit(0);
                    }

                    // Verifica que se haya hecho Inicio de Día Hoy.-
                    if (UsrEsp.Tipeje == "O")
                    {
                        if (Valida != 0)
                        {
                            x = SyGetf_Usr(usuario.Left(3), usuario.Right(2), "I");
                            if (x == 0)
                            {
                                Environment.Exit(0);
                            }
                        }
                    }

                    // Identifica Usuario Original.-
                    UsrOrig = MODGPYF0.GetSceIni("Identificacion", "CCtUsro");
                    UsrEsp.CCtOrig = UsrOrig.Left(3);
                    UsrEsp.EspOrig = UsrOrig.Right(2);

                    // Reemplzaos del Usuario Original.-
                    UsrEsp.RempOrig = SyGet_RempOrig(UsrEsp.CCtOrig, UsrEsp.EspOrig);


                    return VerRegistroUsuario;*/
                }
            //}
            return usuarioEstacion;
        }

        public IQueryable<sce_usr_s05_MS_Result> Sce_Usr_S05_MS(string CentroCosto, string CodigoUsuario)
        {
            return uow.SceRepository.sce_usr_s05_MS(CentroCosto, CodigoUsuario);
        }

        public IQueryable<sce_xplv_s11_MS_Result> Sce_Xplv_S11(string numeroPresentacion, DateTime fechaPresentacion)
        {
            return uow.SceRepository.sce_xplv_s11(numeroPresentacion, fechaPresentacion);
        }

        public IQueryable<string> Sce_Rsa_S03(string partyId, int nombreId)
        {
            return uow.SceRepository.sce_rsa_s03(partyId, nombreId);
        }

        public IQueryable<string> Sce_Dad_S04(string partyId, byte nombreId, string tipo)
        {
            return uow.SceRepository.sce_dad_s04(partyId, nombreId, tipo);
        }

        public IDictionary<decimal, string> ObtenerPlazasBancoCentral()
        {
            if (tablaPlazaBancoCentral == null)
            {
                tablaPlazaBancoCentral = uow.SceRepository.sgt_pbc_s01().ToDictionary(p => p.pbc_pbccod, p => p.pbc_pbcdes);
            }

            return tablaPlazaBancoCentral;

        }

        public IQueryable<sce_xanu_s02_MS_Result> Sce_Xanu_S02(string NumeroPresentacion, DateTime FechaPresentacion)
        {
            return uow.SceRepository.sce_xanu_s02(NumeroPresentacion, FechaPresentacion);
        }

        public IDictionary<decimal, string> ObtenerAduanas()
        {
            if (tablaAduana == null)
            {
                tablaAduana = uow.SceRepository.sce_adn_s01().ToDictionary(a => a.codadn, a => a.nomadn);
            }

            return tablaAduana;
        }

        public List<sce_mcd_s66_MS_Result> sce_mcd_s66_MS(DateTime fecha, string nemonicoCuenta)
        {
            return uow.SceRepository.sce_mcd_s66_MS(fecha, nemonicoCuenta);
        }

        public List<sce_mcd_s65_MS_Result> sce_mcd_s65_MS(DateTime fecha, string cuenta)
        {
            return uow.SceRepository.sce_mcd_s65_MS(fecha, cuenta);
        }

        /// <summary>
        /// Obtiene el nombre de un país mediante su código
        /// </summary>
        /// <param name="CodigoPais"></param>
        /// <returns>Nombre del país</returns>
        /// 
        /// <remarks>Esta función es basada en el funcionamiento del código legacy - claramente debemos crear un
        /// nuevo stored procedure</remarks>
        internal string GetNombrePais(int CodigoPais)
        {
            var match = uow.SgtRepository.Sgt_Pai_S02().Where(p => p.pai_paicod == CodigoPais).SingleOrDefault();
            if (match != null)
            {
                return match.pai_painom;
            }
            return "";
        }

        /// <summary>
        /// Obtiene listado de paises
        /// </summary>        
        /// <returns>sgt_pai_s02_Result</returns>       
        internal IQueryable<sgt_pai_s02_MS_Result> GetPaises()
        {
            return uow.SgtRepository.Sgt_Pai_S02().AsQueryable();
        }

        internal string GetNombreBanco(int CodigoBanco)
        {
            var match = uow.SceRepository.Sce_Bco_S01_MS().Where(b => b.codbco == CodigoBanco).SingleOrDefault();
            if (match != null)
            {
                return match.nombco;
            }
            return "";
        }

        public IQueryable<sce_plia_s01_MS_Result> Sce_Plia_S01(string numeroPlanilla, DateTime fechaPlanilla)
        {
            return uow.SceRepository.sce_plia_s01(numeroPlanilla, fechaPlanilla);
        }

        public IQueryable<DateTime?> GetFeriados()
        {
            return uow.SceRepository.sce_fer_s01();
        }

        /// <summary>
        /// Modifica datos de una planilla
        /// </summary>
        /// <param name="NumeroPlanilla"></param>
        /// <param name="FechaAnterior"></param>
        /// <param name="FechaPlanilla"></param>
        /// <param name="CodigoPais"></param>
        /// <param name="Observaciones"></param>
        /// <param name="ZonaFranca"></param>
        /// <returns>Retorna 0 en caso de éxito, 9 en caso de error (comportamiento legacy)</returns>
        public int? Sce_Pli_W06(string NumeroPlanilla, DateTime FechaAnterior, DateTime FechaPlanilla, int CodigoPais, string Observaciones, bool ZonaFranca)
        {
            var res = uow.SceRepository.sce_pli_w06(NumeroPlanilla, FechaAnterior, FechaPlanilla, CodigoPais, Observaciones, ZonaFranca)
                .FirstOrDefault();
            if (res == null)
            {
                return 9;
            }
            return res;
        }
        
        /// <summary>
        /// Actualiza datos de planilla anulada
        /// </summary>
        /// <param name="NumeroPresentacion">Número de planilla a actualizar</param>
        /// <param name="FechaPresentacion">Fecha actual de planilla</param>
        /// <param name="NuevaFechaPresentacion">Nueva fecha de presentación</param>
        /// <param name="Observaciones">Observaciones</param>
        /// <returns></returns>
        public int Sce_Xanu_U03(string NumeroPresentacion, DateTime FechaPresentacion, DateTime NuevaFechaPresentacion, string Observaciones)
        {
            var res = uow.SceRepository.sce_xanu_u03(NumeroPresentacion, FechaPresentacion, NuevaFechaPresentacion, Observaciones).FirstOrDefault();
            if (res == null)
            {
                return -1; // Procedimiento retorna -1 en caso de error, 0 en caso de éxito
            }
            return res.codigo;
        }

        public void Sce_Xplv_W03(string numeroPresentacion, DateTime fechaAnterior, DateTime fechaPresentacion,
            decimal montoBruto, decimal comisiones, decimal otrosGastos, decimal tipoCambio, string observaciones)
        {
            uow.SceRepository.sce_xplv_w03(numeroPresentacion, fechaAnterior, fechaPresentacion,
                montoBruto, comisiones, otrosGastos, tipoCambio, observaciones);
        }

        public string GetNombreCodigoComercio(string CodigoComercio, string Concepto)
        {
            var res = uow.SceRepository.Sce_Tcp_S01().Where(c => c.codtcp == CodigoComercio + Concepto).FirstOrDefault();
            return res != null ? res.destcp : "";
        }

        public IQueryable<sce_plan_s04_MS_Result> GetPlanillaImportacion(string codigoCentroCosto, string codigoProducto, string codigoEspecialista,
            string codigoEmpresa, string codigoCobranza, int numeroPlanilla, DateTime fechaVenta)
        {
            return uow.SceRepository.sce_plan_s04(codigoCentroCosto, codigoProducto, codigoEspecialista,
            codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta);
        }

        public IQueryable<sce_plan_s05_MS_Result> GetPlanillaEstadisticaImportacion(string codigoCentroCosto, string codigoProducto, string codigoEspecialista,
            string codigoEmpresa, string codigoCobranza, int numeroPlanilla)
        {
            return uow.SceRepository.sce_plan_s05(codigoCentroCosto, codigoProducto, codigoEspecialista,
            codigoEmpresa, codigoCobranza, numeroPlanilla);
        }

        public IQueryable<sce_plan_s16_MS_Result> GetPlanillaVisibleImportacion(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, int numeroPlanilla, DateTime fechaVenta)
        {
            return uow.SceRepository.sce_plan_s16_MS(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta);
        }

        public IQueryable<sce_inpl_s01_MS_Result> GetIntereses(string codigoCentroCosto, string codigoProducto, string codigoEspecialista,
            string codigoOficina, string codigoOperacion, decimal numeroPlanilla)
        {
            return uow.SceRepository.sce_inpl_s01(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoOficina, codigoOperacion, numeroPlanilla);
        }

        public IQueryable<sce_plan_s03_MS_Result> Sce_Plan_S03(DateTime fechaVenta, string codigoCentroCosto, string codigoUsuario, decimal tipo)
        {
            return uow.SceRepository.sce_plan_s03(fechaVenta, codigoCentroCosto, codigoUsuario, tipo);
        }

        public IQueryable<sce_fdp> GetFormasPago()
        {
            return uow.SceRepository.sce_fdp();
        }

        public IQueryable<sce_gpln_s10_MS_Result> ListadoPlanillasUsuarios(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return uow.SceRepository.Sce_Gpln_S10(CentroCosto, CodigoUsuario, FechaIngreso);
        }

        public IQueryable<sce_gpln_s12_MS_Result> ListadoPlanillasVsConversionUsuarios(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return uow.SceRepository.Sce_Gpln_S12(CentroCosto, CodigoUsuario, FechaIngreso);
        }

        public IQueryable<sce_gpln_s11_MS_Result> ListadoResumenPlanillas(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return uow.SceRepository.Sce_Gpln_S11(CentroCosto, CodigoUsuario, FechaIngreso);
        }

        public IQueryable<sce_gpln_s13_MS_Result> InformePosicionCambio(string CentroCosto, DateTime FechaIngreso)
        {
            return uow.SceRepository.Sce_Gpln_S13(CentroCosto, FechaIngreso);
        }

        public IQueryable<sce_gpln_s16_MS_Result> ReporteMovimientosCorresponsales(string CentroCosto, DateTime FechaIngreso)
        {
            return uow.SceRepository.Sce_Gpln_S16_MS(CentroCosto, FechaIngreso);
        }

        public void Sce_Plan_U12(PlanillaVisibleImportacionDTO planilla)
        {
            uow.SceRepository.sce_plan_u12(planilla.CodigoCentroCosto, planilla.CodigoProducto, planilla.CodigoEspecialista, planilla.CodigoEmpresa, planilla.CodigoCobranza,
                       planilla.NumeroPlanilla, planilla.FechaVentaAntigua, planilla.FechaVenta, planilla.NumeroConocimientoEmbarque, planilla.FechaConocimientoEmbarque, planilla.FormaPago,
                       planilla.CodigoPais, planilla.NombrePais, planilla.FechaVencimiento, planilla.HayCuadroCuotas, planilla.NumeroCuotas, planilla.Cuota, planilla.HayAcuerdos,
                       planilla.NumeroAcuerdos, planilla.AcuerdoDesde, planilla.AcuerdoHasta,
                       planilla.FechaAutorizacionDebito, planilla.NumeroDocumentoChile, planilla.NumeroDocumentoExtranjero, planilla.Observaciones, planilla.isZonaFranca);
        }

        public void Sce_Inpl_D01(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, decimal numeroPlanilla)
        {
            uow.SceRepository.sce_inpl_d01(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla);
        }

        public void Sce_Inpl_W01(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, decimal numeroPlanilla, DateTime fechaVenta, decimal numeroLineaInteres, decimal concepto, string tipo, decimal monto, decimal capitalBase, decimal codigoBaseAno, decimal tasaInteres, DateTime fechaInicial, DateTime fechaFinal, decimal numeroDias)
        {
            uow.SceRepository.sce_inpl_w01(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta, numeroLineaInteres, concepto, tipo, monto, capitalBase, codigoBaseAno, tasaInteres, fechaInicial, fechaFinal, numeroDias);
        }

        public string GetNombreUsuario(string CentroCosto, string CodigoUsuario)
        {
            if (CentroCosto != null && CodigoUsuario != null)
            {
                var res = this.Sce_Usr_S02(CentroCosto, CodigoUsuario).FirstOrDefault();
                if (res != null)
                {
                    return res.nombre;
                }
            }
            return "";
        }

        public IQueryable<sce_ref_s01_MS_Result> BuscarReferenciaBAEBCH(string referenciaBAE)
        {
            return uow.SceRepository.sce_ref_s01(referenciaBAE);
        }

        public IQueryable<sce_mcd_s20_MS_Result> Sce_Mcd_S20(DateTime fecha, string numeroCuenta)
        {
            return uow.SceRepository.sce_mcd_s20(fecha, numeroCuenta);
        }

        public IQueryable<sce_mcd_s56_MS_Result> MovimientosDeCanje(DateTime fechaMovimientos)
        {
            return uow.SceRepository.Sce_Mcd_S56(fechaMovimientos);
        }

        public IQueryable<sce_pldc_s01_MS_Result> Sce_Pldc_S01(string deDonde, DateTime? fechaInicio, DateTime? fechaTermino)
        {
            return uow.SceRepository.sce_pldc_s01(deDonde, fechaInicio, fechaTermino);
        }

        public IList<sce_fra_s06_MS_Result> GetFraseCarta(string codigoFrase, string idioma)
        {
            return uow.SceRepository.sce_fra_s06_MS(codigoFrase, idioma);
        }
        public IQueryable<sce_pldc_s02_MS_Result> BusquedaPlanillasInformadas(char tipoPlanilla, DateTime fechaActualizacion)
        {
            return uow.SceRepository.Sce_Pldc_S02(tipoPlanilla, fechaActualizacion);
        }

        public IQueryable<sce_pldc_s03_MS_Result> Sce_Pldc_S03(string tipoPlanilla, string numeroPresentacion, DateTime fechaPresentacion)
        {
            return uow.SceRepository.Sce_Pldc_S03(tipoPlanilla, numeroPresentacion, fechaPresentacion);
        }

        public string FraseCartaBanco(int codigoFrase, string Cadena)
        {
            string s = "";
            int i = 0;
            int n = 0;
         
            var res = uow.SceRepository.Sce_Fra_S06(codigoFrase, SceRepository.IdiomaFrases.Espanol).FirstOrDefault();
            if (res != null)
            {
                // Rescata en campo Memo.
                if (res.numero > 0)
                {
                   res.frase = uow.SceRepository.sce_memg_s01_MS("f", res.numero);
                }
                return res.frase.Trim();
            }
            return "";
        }

        public IQueryable<int?> GuardaDeclaracionPlanilla(DeclaracionPlanillaDTO declaracion)
        {
            return uow.SceRepository.sce_pldc_i01(declaracion.NumeroPresentacion, declaracion.FechaPresentacion, declaracion.Tipo, declaracion.FechaActual, declaracion.DeclaracionImportacion, declaracion.DeclaracionExportacion, declaracion.FechaDeclaracion, declaracion.CodigoAduana, declaracion.MontoDI, declaracion.InteresDI, declaracion.MontoUSD, declaracion.FechaVencimientoRetorno);
        }

        public IQueryable ActualizaFechaDeclaracion(DateTime fechaActual)
        {
            return uow.SceRepository.sce_pldc_u01(fechaActual);
        }

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

        public ReporteContable GetReporteContable(IDatosUsuario datosUsuario, int nroReporte, DateTime fecha, IList<sce_dfc> descripcionesFunciones)
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
                var VCta = new T_Cta[0];
                IList<sce_mcd> lineas = uow.SceRepository.sce_mcd_s07_MS(nroReporte, fecha);
                foreach (sce_mcd linea in lineas)
                {
                    linea.numcct = FormatearNroDeCuenta(linea.numcct, Convert.ToInt16(linea.codmon), datosUsuario);
                    
                    int indiceCuenta = Get_Cta(linea.nemcta, ref VCta);
                    linea.NombreCuenta = VCta[indiceCuenta].Cta_Nom.Value;
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

        private int Get_Cta(string NemCta, ref T_Cta[] VCta)
        {
            int n = 0;
            short i = 0;
            int m = -1;

            n = VCta.Length;

            if (n > 0)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    if (VB6Helpers.UCase(VB6Helpers.Trim(VCta[i].Cta_Nem.Value)) == VB6Helpers.UCase(VB6Helpers.Trim(NemCta)))
                    {
                        m = i;
                        break;
                    }
                }
            }
            else
            {
                VB6Helpers.Redim(ref VCta, 0, 0);
            }

            if (m == -1)
            {
                m = SyGet_Cta(NemCta, ref VCta);
            }

            return m;
        }

        private int SyGet_Cta(string NemCta, ref T_Cta[] VCta)
        {
            //@emiliano: por aca tiene que pasar antes
            var result = uow.SceRepository.sce_cta_s01_1_MS(NemCta);
            VCta = new T_Cta[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                var item = result[i];
                VCta[i] = new T_Cta();
                VCta[i].Cta_Nem.Value = item.cta_nem;
                VCta[i].Cta_Mon = (short)item.cta_mon;
                VCta[i].Cta_Num.Value = item.cta_num;
                VCta[i].Cta_Nom.Value = item.cta_nom;
                VCta[i].Cta_GL = Convert.ToInt16(item.cta_gl);
                VCta[i].Cta_NroTO = (int)item.cta_nroto;
                VCta[i].Cta_IndTO = (short)item.cta_indto;
                VCta[i].Cta_CIT = Convert.ToInt16(item.cta_cit);
                VCta[i].Cta_CVT = Convert.ToInt16(item.cta_cvt);
                VCta[i].Cta_CAP = Convert.ToInt16(item.cta_cap);
                VCta[i].Cta_CTD = Convert.ToInt16(item.cta_ctd);
                VCta[i].Cta_POS = Convert.ToInt16(item.cta_pos);
                VCta[i].Cta_CDR = Convert.ToInt16(item.cta_cdr);
                VCta[i].Cta_Vig = (short)item.cta_vigtbl;
            }
            return result.Count - 1;
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

        /// <summary>
        /// Formatea el numero SCE (753-XX-29-000-XXXXX)
        /// </summary>
        /// <param name="nroOperacion">Numero de la operacion</param>
        /// <returns>Numero de la operacion formateada con guiones, en caso de no recibir valor apropiado devuelve vacio.</returns>
        public string FormatearNroOperacion(string nroOperacion)
        {
            using (var tracer = new Tracer("FormatearNroOperacion - XgplService"))
            {
                string nroOperacionFormateado = string.Empty;
                try
                {
                    if (!string.IsNullOrWhiteSpace(nroOperacion) && nroOperacion.Length == 15)
                    {
                            nroOperacionFormateado = nroOperacion.Substring(0, 3) + "-" + nroOperacion.Substring(3, 2) + "-" + nroOperacion.Substring(5, 2) + "-" + nroOperacion.Substring(7, 3) + "-" + nroOperacion.Substring(10, 5);
                    }
                    else
                    {
                            tracer.TraceWarning("El largo del número de la operación (" + (nroOperacion ?? "<NULL>") + ") no cumple con el largo establecido (15)");

                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Error al formatear Numero de operacion: " + (nroOperacion ?? "<NULL>"), ex);
                }

                return nroOperacionFormateado;
            }
        }

        private string FormatearNroDeCuenta(string nroCuenta, short codMoneda, IDatosUsuario datosUsuario)
        {
            var usuarioEstacion = this.ObtenerUsuarioEstacion();
            if (!String.IsNullOrEmpty(nroCuenta))
            {
                if (nroCuenta.Length > 8)
                {
                    if (codMoneda == short.Parse(datosUsuario.General_MndNac))
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

        public void EliminarPlanillaVisibleImportacion(decimal numeroPresentacion, DateTime fechaPresentacion)
        {
            uow.SceRepository.Sce_Plan_U14(numeroPresentacion, fechaPresentacion);          
           
        }

        public void EliminarPlanillaInvisibleExportacion(string numeroPresentacion, DateTime fechaPresentacion)
        {
            uow.SceRepository.Sce_Pli_U04(numeroPresentacion, fechaPresentacion);   
        }

        public void EliminarPlanillaAnuladaExportacion(string numeroPresentacion, DateTime fechaPresentacion)
        {
            uow.SceRepository.Sce_Xanu_U04(numeroPresentacion, fechaPresentacion);   
        }

        public void EliminarPlanillaVisibleExportacion(string numeroPresentacion, DateTime fechaPresentacion)
        {
            uow.SceRepository.Sce_Xplv_U03(numeroPresentacion, fechaPresentacion);   
        }

        public PlanillaVisibleExportacion ImprimirPlanillaExportacion(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, int numeroPlanilla, DateTime fechaVenta)
        {
            short Impresora;
            short k = 0;
            short xx = 0;
            string RR = "";
            string NroAcs = "";
            short Planilla_Anulada = 0;
            var planilla = this.GetPlanillaImportacion(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta).FirstOrDefault();
            var planilla2 = this.GetPlanillaVisibleImportacion(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta).FirstOrDefault();
            var model = new PlanillaVisibleExportacion();

            model.Pl_num_planilla = (forma(planilla.num_presen, "000000"));
            model.Pl_Codigo = string.Empty;
            //model.Pl_Codigo = (planilla.Codigo);
            //if (~EsCVD != 0)
            //{
                if (planilla2.estado != T_MODGFYS.Endoba)
                {
                    model.Pl_fecha_venta = planilla.fechaventa.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            //}
            //else
            //{
            //    model.Pl_fecha_venta = (DateTime.Parse(planilla.fecha_venta).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            //}


            model.Pl_CodBCCh = (VB6Helpers.Format(VB6Helpers.CStr(planilla.codbcch), "0"));
            model.Pl_NomImport = (planilla.nomimport);
            RR = Rut_Formateado(planilla.rut);
            model.Pl_rut = (RR);
            if (!string.IsNullOrEmpty(planilla.nompais))
            {
                string _tempVar1 = VB6Helpers.Mid(planilla.nompais, 1, 17);
                model.Pl_Paispago = (Escribe_Nombre(ref _tempVar1));
                model.Pl_Cod_Paispago = (forma(planilla.codpais, "000"));
            }
            var nombreMoneda = planilla.nommone;
            model.Pl_nombre_moneda = (Escribe_Nombre(ref nombreMoneda));
            planilla.nommone = nombreMoneda;
            model.Pl_cod_moneda = (forma(planilla.codmone, "000"));

            if (!string.IsNullOrEmpty(planilla.num_idi) && planilla.num_idi != "000000")
            {
                model.Pl_num_idi = (VB6Helpers.Format(VB6Helpers.CStr(VB6Helpers.Val(planilla.num_idi)), "000000"));
                model.Pl_fecha_idi = planilla.fec_idi.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                model.Pl_Cod_Plaza = (VB6Helpers.Format(VB6Helpers.CStr(planilla.cod_plaza), "0"));
            }

            model.Pl_Cod_FormaPago = (forma(planilla.forma_pag, "00"));

            model.Pl_num_conocimiento = (planilla.num_con);
            model.Pl_fecha_conocimiento = planilla.fec_con.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            model.Pl_fecha_vencimiento = planilla.fechavenc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            //Impresion del Detalle de los Montos Cubiertos en la Planilla

            model.Pl_Mercaderia = PrintMonto((double)planilla.mercaderia);

            model.Pl_HastaFob = PrintMonto((double)planilla.hasta_fob);

            model.Pl_Fob_Origen = PrintMonto((double)planilla.mtofob);

            model.Pl_Flete_Origen = PrintMonto((double)planilla.mtoflete);

            model.Pl_Seguro_Origen = PrintMonto((double)planilla.mtoseguro);

            model.Pl_Cif_Origen = PrintMonto((double)planilla.mtocif);

            model.Pl_Interes_Origen = PrintMonto((double)planilla.mtointer);

            model.Pl_Gastos_Banco = PrintMonto((double)planilla.mtogastos);

            model.Pl_Total_Origen = PrintMonto((double)planilla.mtototal);

            model.Pl_Cif_Dolar = PrintMonto((double)planilla.cifdolar);

            model.Pl_Total_Dolar = PrintMonto((double)planilla.totaldolar);

            //if (~EsCVD != 0)
            //{
                if (planilla2.estado != T_MODGFYS.Estadis)
                {
                    if (planilla2.estado != T_MODGFYS.Endoba)
                    {
                        model.Pl_tipo_cambio = (Format.FormatCurrency((double)planilla.tipo_camb, "0.0000"));
                    }
                }

            //}
            //else
            //{
            //    if (MODGFYS.CVD.Operacion != T_MODGFYS.PLANEST)
            //    {
            //        model.Pl_tipo_cambio = (Format.FormatCurrency(planilla.tipo_cambio, "0.0000"));
            //    }

            //}

            model.Pl_Paridad = (Format.FormatCurrency((double)planilla.paridad, "0.0000"));

            if (planilla2.haycuadro.Value)
            {
                if (planilla2.numcuadro != 0)
                {
                    model.Pl_Num_Cuadro = (VB6Helpers.Format(VB6Helpers.CStr(planilla2.numcuadro), "0"));
                }
                if (planilla2.numcuota != 0)
                {
                    model.Pl_num_cuotas = (VB6Helpers.Format(VB6Helpers.CStr(planilla2.numcuota), "0"));
                }
            }

            if (planilla2.haycuadro.Value)
            {
                model.Pl_num_acuerdos = (VB6Helpers.Format(VB6Helpers.CStr(planilla2.numacuerdo), "0"));

                NroAcs = "    " + VB6Helpers.Trim(planilla2.acuerdo1) + "    ";

                model.NroAcs = (NroAcs);

                model.Pl_fecha_debito = planilla2.fecdebito.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                model.Pl_NDoc1 = (planilla2.ndoc1);
                model.Pl_NDoc2 = (planilla2.ndoc2);
            }

            Planilla_Anulada = (short)(false ? -1 : 0);
            if (Planilla_Anulada != 0)
            {
                model.Pl_fecha_anulacion = planilla2.fechaanula.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                model.Pl_paridad_anulacion = (Format.FormatCurrency((double)planilla2.paranula, "0.0000"));
                model.Pl_total_anulacion = (VB6Helpers.Format(VB6Helpers.CStr(planilla2.totalanula), "0"));
            }

            //if (!string.IsNullOrEmpty(planilla.nplar))
            //{
            //    //Imprime_Reemplazo Pl(), pp%
            //}

            model.Pl_ObsDecl = (planilla2.obsdecl);

            if (!string.IsNullOrEmpty(planilla2.obsparidad))
            {
                model.Pl_ObsParidad = (planilla2.obsparidad);
            }
            if (!string.IsNullOrEmpty(planilla2.obsmerma))
            {
                model.Pl_ObsMerma = (planilla2.obsmerma);
            }
            //if (!string.IsNullOrEmpty(planilla2.LineaObs1))
            //{
            //    model.Pl_LineaObs1 = (planilla.LineaObs1);
            //    model.Pl_LineaObs2 = (planilla.LineaObs2);
            //    model.Pl_LineaObs3 = (planilla.LineaObs3);
            //}

            model.Pl_ObsCobranza = (planilla2.obscobranz);
            var detalles = this.GetIntereses(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla).ToList();
            Imprime_Detalle(model, ref planilla, detalles);

            return model;
        }

        private static void Imprime_Detalle(PlanillaVisibleExportacion model, ref sce_plan_s04_MS_Result Pl, IList<sce_inpl_s01_MS_Result> Det)
        {
            //SE USA INTERES_DETALLE PORQUE SON IGUALES AL DETALLE QUE SE NECESITA
            short Impresora;
            short k = 0;
            short xx = 0;
            short yy = 0;
            double tint = 0;
            short MaxDet = 0;
            short pos_vert = 0;
            short cont = 0;
            short NDet = 0;
            string Concepto = "";
            string s = "";
            string sss = "";
            string SS = "";


            MaxDet = -1;

            MaxDet = (short)VB6Helpers.UBound(Det);

            //--- Parametros de Impresion ------
            //xx% = 3
            //yy% = -8
            //----------------------------------
            cont = 0;

            for (NDet = 0; NDet <= (short)MaxDet; NDet++)
            {
                if (VB6Helpers.Val(Pl.codigo) == (double)Det[NDet].numplan)
                {
                    Detalle det = new Detalle();

                    cont = (short)(cont + 1);
                    tint += (double)Det[NDet].monto;
                    pos_vert = (short)(pos_vert + 4);
                    det.Cont = forma(cont, "00");
                    short _switchVar1 = (short)Det[NDet].concepto;
                    if (_switchVar1 == 0)
                    {
                        Concepto = "CIF";
                    }
                    else if (_switchVar1 == 1)
                    {
                        Concepto = "CYF";
                    }
                    else if (_switchVar1 == 2)
                    {
                        Concepto = "CYS";
                    }
                    else if (_switchVar1 == 3)
                    {
                        Concepto = "FLT";
                    }
                    else if (_switchVar1 == 4)
                    {
                        Concepto = "FOB";
                    }
                    else if (_switchVar1 == 5)
                    {
                        Concepto = "SEG";
                    }

                    det.Concepto = (Concepto);
                    det.Tipo = (Det[NDet].tipo);
                    s = Format.FormatCurrency((double)Det[NDet].capbas, "0.00");
                    sss = VB6Helpers.CStr(formatnum(VB6Helpers.Trim(s), 0, 15));
                    det.CapBas = (sss);
                    det.CodBas = (VB6Helpers.Format(VB6Helpers.CStr(Det[NDet].codbas), "0"));
                    det.Tasa = (Format.FormatCurrency((double)Det[NDet].tasa, "0.0000"));
                    det.FecIni = Det[NDet].fini.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    det.FecFin = Det[NDet].ffin.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    det.NumDia = (VB6Helpers.Format(VB6Helpers.CStr(Det[NDet].ndias), "0"));
                    s = Format.FormatCurrency((double)Det[NDet].monto, "0.00");
                    SS = VB6Helpers.CStr(formatnum(VB6Helpers.Trim(s), 0, 15));
                    det.Mto = (SS);
                    model.Detalles.Add(det);
                }

            }

            if (tint > 0)
            {
                s = Format.FormatCurrency(tint, "0.00");
                SS = VB6Helpers.CStr(formatnum(VB6Helpers.Trim(s), 0, 15));
                model.tint = SS;
            }

        }

        public PlanillaReemplazo ImprimirPlanillaReemplazo(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, int numeroPlanilla, DateTime fechaVenta)
        {
            var planilla = this.GetPlanillaImportacion(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta).FirstOrDefault();
            var planilla2 = this.GetPlanillaVisibleImportacion(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta).FirstOrDefault();

            string mensaje = "";
            short i = 0;
            short k = 0;
            short xx = 0;
            short Tot_Ree = 0;
            short j = 0;
            string NroAcs1 = "";
            string NroAcs2 = "";

            PlanillaReemplazo pr = new PlanillaReemplazo();


            //Tot_Ree = (short)VB6Helpers.UBound(MODPREEM.Vx_PReem);
            //// UPGRADE_ISSUE (#04D8): On Error Goto 0/-1 statements aren't supported.
            //// IGNORED: On Error GoTo 0

            //if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_AnuVisI)
            //{
            //    //Reemplazo.-
            //    mensaje = "Impresión de Planillas de Reemplazo";
            //}
            //else if (MODGCVD.VgCvd.TipCVD == T_MODGCVD.TCvd_PlanSO)
            //{
            //    //Sin Operación.-
            //    mensaje = "Impresión de Planillas sin Operación";
            //}
            //pr.Mensaje = mensaje;



            //for (i = 0; i <= (short)Tot_Ree; i++)
            //{
            //    for (j = 0; j <= (short)copias_ree; j++)
            //    {
            //if (planilla2.estado == 1)
            //{

                //Numero de Presentacion
                //-----------------------------------
                pr.Vx_PReem_NumPla = VB6Helpers.Format(planilla.num_presen, "000000");
                //Codigo (205000)
                //-----------------------------------
                pr.Vx_PReem_Codigo = planilla.codigo;
                //Fecha de Venta
                //-----------------------------------
                pr.Vx_PReem_FecVen = planilla.fechaventa.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                //Plaza Banco Central y Codigo
                //-----------------------------------
                //Printxy 77 + xx%, 31 + k%, "SANTIAGO"

                pr.Vx_PReem_NomPlz = planilla.nombplaza;
                pr.Vx_PReem_CodBch = VB6Helpers.Format(VB6Helpers.CStr(planilla.codbcch), "00");
                //Importador
                //-----------------------------------
                pr.Vx_PReem_NomImp = planilla.nomimport;
                pr.Vx_PReem_RutImp = Rut_Formateado(planilla.rut);
                //Pais de Pago
                //-----------------------------------
                if (!string.IsNullOrEmpty(planilla.nompais))
                {
                    string _tempVar1 = VB6Helpers.Mid(planilla.nompais, 1, 17);
                    pr.Vx_PReem_NomPai = MODGTAB0.GetNombrePais((int)planilla.codpais, this);
                    //pr.Vx_PReem_NomPai = Escribe_Nombre(ref _tempVar1);
                    pr.Vx_PReem_CodPPa = VB6Helpers.Format(planilla.codpais, "000");
                }

                //Moneda
                //-----------------------------------
                string nombreMoneda = planilla.nommone;
                pr.Vx_PReem_NomMon = Escribe_Nombre(ref nombreMoneda);
                planilla.nommone = nombreMoneda;
                pr.Vx_PReem_CodMPa = VB6Helpers.Format(MODGTAB0.GetCodigoMonedaBancoCentral((int)(planilla.codmone),this), "000");
                //Informe de Importacion
                //-----------------------------------
                if (VB6Helpers.Format(VB6Helpers.CStr(planilla.num_idi), "000000") != "000000")
                {
                    pr.Vx_PReem_NumIdi = VB6Helpers.Format(VB6Helpers.CStr(planilla.num_idi), "000000");
                    pr.Vx_PReem_FecIdi = planilla.fec_idi.ToString("dd/MM/yyyy");
                    pr.Vx_PReem_CodPlz = VB6Helpers.Format(VB6Helpers.CStr(planilla.cod_plaza), "00");
                }

                //Forma de Pago
                //-----------------------------------
                pr.Vx_PReem_CodPag = VB6Helpers.Format(planilla.forma_pag, "00");
                //Conocimiento de Embarque
                //-----------------------------------
                pr.Vx_PReem_NumCon = planilla.num_con;
                pr.Vx_PReem_FecCon = planilla.fec_con.ToString("dd/MM/yyyy");
                //Vencimiento de la operacion
                //-----------------------------------
                pr.Vx_PReem_FecVto = planilla.fechavenc.ToString("dd/MM/yyyy");
                //Impresion del Detalle de los Montos Cubiertos en la Planilla
                //-----------------------------------


                pr.Vx_PReem_MtoFob = PrintMonto((double)planilla.mtofob);
                pr.Vx_PReem_MtoFle = PrintMonto((double)planilla.mtoflete);
                pr.Vx_PReem_MtoSeg = PrintMonto((double)planilla.mtoseguro);
                pr.Vx_PReem_MtoCif = PrintMonto((double)planilla.mtocif);
                pr.Vx_PReem_MtoInt = PrintMonto((double)planilla.mtointer);
                pr.Vx_PReem_GasBan = PrintMonto((double)planilla.mtogastos);
                pr.Vx_PReem_TotOri = PrintMonto((double)planilla.mtototal);
                pr.Vx_PReem_CifDol = PrintMonto((double)planilla.cifdolar);
                pr.Vx_PReem_TotDol = PrintMonto((double)planilla.totaldolar);

                //-----------------------------------
                //Tipo cambio y Paridad.-
                //-----------------------------------
                pr.Vx_PReem_TipCam = Format.FormatCurrency((double)planilla.tipo_camb, "0.0000");

                pr.Vx_PReem_ParPag = Format.FormatCurrency((double)planilla.paridad, "0.0000");
                //Cuadro de Pagos.-
                //-----------------------------------
                if (planilla2.haycuadro.Value)
                {
                    if (planilla2.numcuadro != 0)
                    {
                        pr.Vx_PReem_NumCua = planilla2.numcuadro.ToString();
                    }
                    if (planilla2.numcuota != 0)
                    {
                        pr.Vx_PReem_numcuo = planilla2.numcuota.ToString();
                    }
                }

                //Acuerdos.-
                //-----------------------------------
                if (planilla2.hayacuerdo.Value)
                {
                    pr.Vx_PReem_NumAcu = planilla2.numacuerdo.ToString();

                    NroAcs1 = "    " + VB6Helpers.Trim(planilla2.acuerdo1) + "    ";
                    NroAcs2 = "    " + VB6Helpers.Trim(planilla2.acuerdo2) + "    ";
                    pr.Vx_PReem_Acuer1 = NroAcs1;
                    pr.Vx_PReem_Acuer2 = NroAcs2;
                }

                //Convenio Credito Reciproco.-
                //----------------------------
                if (planilla2.fecdebito.HasValue)
                {
                    pr.Vx_PReem_FecDeb = planilla2.fecdebito.Value.ToString("dd/MM/yyyy");
                }

                if (!String.IsNullOrEmpty(planilla2.ndoc1))
                {
                    pr.Vx_PReem_DocChi = planilla2.ndoc1;
                }

                if (!String.IsNullOrEmpty(planilla2.ndoc2))
                {
                    pr.Vx_PReem_DocExt = planilla2.ndoc2;
                }

                //Datos planilla reemplazada.-
                //-----------------------------------
                if (planilla2.hayrpl == -1)
                {
                    pr.Vx_PReem_NumPln_R = VB6Helpers.Format(VB6Helpers.CStr(planilla2.numpln_r), "000000");
                    pr.Vx_PReem_FecPln_R = planilla2.fecpln_r.Value.ToString("dd/MM/yyyy");
                    pr.Vx_PReem_CodPlz_R = VB6Helpers.Format(VB6Helpers.Str(planilla2.codpln_r), "00");
                    pr.Vx_PReem_CodEnt_R = VB6Helpers.Format(VB6Helpers.CStr(planilla2.codent_r), "00");
                    pr.Vx_PReem_NumInf_R = VB6Helpers.Format(VB6Helpers.CStr(planilla2.numinf_r), "000000");
                    pr.Vx_PReem_FecInf_R = planilla2.fecinf_r.Value.ToString("dd/MM/yyyy");
                    pr.Vx_PReem_PlzInf_R = VB6Helpers.Format(VB6Helpers.CStr(planilla2.plzinf_r), "00");
                    pr.Vx_PReem_NumCon_R = VB6Helpers.Format(planilla2.numcon_r, "000000");
                    pr.Vx_PReem_FecCon_R = planilla2.feccon_r.Value.ToString("dd/MM/yyyy");
                }

                pr.Vx_PReem_ObsDec = planilla2.obsdecl;
                pr.Vx_PReem_observ = planilla2.observ;
                pr.Vx_PReem_ObsCob = planilla2.obscobranz;

                //Detalle de Intereses
                //--------------------
                var detalles = this.GetIntereses(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla).ToList();
                Pr_ImpDetInt(pr, planilla, planilla2, ref detalles, (int)planilla.num_presen);
                //initObject.PlanillasReemplazo.Add(pr);

            //}

            //    }

            //}

            return pr;
        }

        //Imprime el detalle de los intereses de anulacion o reemplazo.-
        private static void Pr_ImpDetInt(PlanillaReemplazo pr, sce_plan_s04_MS_Result pl, sce_plan_s16_MS_Result pl2, ref List<sce_inpl_s01_MS_Result> Interes, int NumPla)
        {
            short Impresora = 0;
            short pp = 0;
            dynamic ind = null;
            short k = 0;
            short xx = 0;
            int MaxDet = 0;
            short yy = 0;
            short pos_vert = 0;
            short cont = 0;
            short NDet = 0;
            double tint = 0;
            string Concepto = string.Empty;
            string tip = string.Empty;
            string s = string.Empty;
            string sss = string.Empty;
            string SS = string.Empty;

            pp = VB6Helpers.CShort(ind);


            MaxDet = -1;

            //MaxDet = (short)VB6Helpers.UBound(Interes);
            MaxDet = Interes.Count - 1;
            //--- Parametros de Impresion ------
            //xx% = 3
            //yy% = -8
            yy = k;
            //----------------------------------
            pos_vert = (short)(223 + yy);  //antes 225 + yy%
            cont = 0;

            for (NDet = 0; NDet <= (short)MaxDet; NDet++)
            {
                if (VB6Helpers.Format(VB6Helpers.CStr(NumPla), "000000") == VB6Helpers.Format(VB6Helpers.CStr(Interes[NDet].numplan), "000000")) // && Interes[NDet].FlgEli != -1)
                {
                    cont = (short)(cont + 1);
                    tint += (double)Interes[NDet].monto;
                    pos_vert = (short)(pos_vert + 4);
                    Detalle di = new Detalle();
                    di.Cont = forma(cont, "00");

                    short _switchVar1 = (short)Interes[NDet].concepto;
                    if (_switchVar1 == 0)
                    {
                        Concepto = "CIF";
                    }
                    else if (_switchVar1 == 1)
                    {
                        Concepto = "CYF";
                    }
                    else if (_switchVar1 == 2)
                    {
                        Concepto = "CYS";
                    }
                    else if (_switchVar1 == 3)
                    {
                        Concepto = "FLT";
                    }
                    else if (_switchVar1 == 4)
                    {
                        Concepto = "FOB";
                    }
                    else if (_switchVar1 == 5)
                    {
                        Concepto = "SEG";
                    }
                    di.Concepto = Concepto;
                    short _switchVar2 = short.Parse(Interes[NDet].tipo);
                    if (_switchVar2 == 1)
                    {
                        tip = "PR";
                    }
                    else if (_switchVar2 == 3)
                    {
                        tip = "IC";
                    }
                    else if (_switchVar2 == 4)
                    {
                        tip = "BA";
                    }
                    di.Tip = tip;

                    s = Format.FormatCurrency((double)Interes[NDet].capbas, "0.00");
                    sss = VB6Helpers.CStr(formatnum(VB6Helpers.Trim(s), 0, 15));
                    di.CapBas = sss;
                    di.CodBas = VB6Helpers.Format(VB6Helpers.CStr(Interes[NDet].codbas), "0");
                    di.Tasa = Format.FormatCurrency((double)Interes[NDet].tasa, "0.0000");
                    di.FecIni = Interes[NDet].fini.ToString("dd/MM/yyyy");
                    di.FecFin = Interes[NDet].ffin.ToString("dd/MM/yyyy");
                    di.NumDia = VB6Helpers.Format(VB6Helpers.CStr(Interes[NDet].ndias), "0");

                    s = Format.FormatCurrency((double)Interes[NDet].monto, "0.00");
                    SS = VB6Helpers.CStr(formatnum(VB6Helpers.Trim(s), 0, 15));
                    di.Mto = SS;
                    pr.DetInt.Add(di);
                }

            }

            if (tint > 0)
            {

                s = Format.FormatCurrency(tint, "0.00");
                SS = VB6Helpers.CStr(formatnum(VB6Helpers.Trim(s), 0, 15));
                pr.Tint = SS;
            }

        }

        public PlanillaVisibleAnulada ImprimirPlanillaAnulada(string numeroPresentacion, DateTime fechaPresentacion)
        {
            short va = 1;
            short num_cop = 0;
            short z = 0;
            short copia = 0;
            short a = 0;
            short Impresora;
            float coordy = 0;
            string n = "";
            string R = "";
            short m = 0;
            string Texto = "";
            dynamic lin = null;
            string palabra = "";
            short co = 0;
            string letra = "";

            var planilla = this.Sce_Xanu_S02(numeroPresentacion, fechaPresentacion).FirstOrDefault();

            PlanillaVisibleAnulada model = new PlanillaVisibleAnulada();

            num_cop = (short)(num_cop + 1);
            copia = (short)(copia + 1);

            //Tipo de Anulación....OK¡¡¡
            if (planilla.tipanu != 0)
            {
                model.VxAnus_TipAnu = (VB6Helpers.Format(VB6Helpers.CStr(planilla.tipanu), "000"));
            }

            //Número Presentación....OK¡¡¡
            if (!string.IsNullOrEmpty(planilla.numpre))
            {

                //+ .25
                n = VB6Helpers.Format(planilla.numpre, "0000000");
                model.VxAnus_NumPre = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
            }

            //Fecha Presentación....OK¡¡¡
            //if (!string.IsNullOrWhiteSpace(planilla.fecpre))
            //{
                //+ .15
                model.VxAnus_fecpre = planilla.fecpre.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //}

            //Plaza Banco Central que Contabiliza

            //Código Plaza Banco Central que Contabiliza

            //+ .15
            model.COD_PLAZA_25 = (VB6Helpers.Format("25", "00"));
            //Nombre.
            if (VB6Helpers.Mid(planilla.prtexp, 1, 1) == "0")
            {
                planilla.prtexp = VB6Helpers.Mid(planilla.prtexp, 2) + "~";
            }

            if (!string.IsNullOrWhiteSpace(planilla.prtexp))
            {
                model.DatPrt = MODGTAB0.GetNombreParty(planilla.prtexp, (int)planilla.indnom, this);
                model.DatPrt2 = MODGTAB0.GetDireccionParty(planilla.prtexp, (byte)planilla.inddir, this);
                //model.DatPrt = (Mdl_Funciones_Varias.GetDatPrt(initObject, unit, planilla.PrtExp, planilla.IndNom, planilla.IndDir, "N"));
                //Dirección.

                //.15
                //model.DatPrt2 = (Mdl_Funciones_Varias.GetDatPrt(initObject, unit, planilla.PrtExp, planilla.IndNom, planilla.IndDir, "D"));
            }

            //Rut.
            if (!string.IsNullOrWhiteSpace(planilla.rutexp))
            {
                R = MODXPLN1.ConvRut(VB6Helpers.Trim(planilla.rutexp));
                model.VxAnus_RutExp = (VB6Helpers.Mid(R, 1, VB6Helpers.Len(R) - 1) + "-" + VB6Helpers.Mid(R, VB6Helpers.Len(R), 1));
            }

            //**********************************************************************
            //Entidad Autorizada.

            model.VBco_NomBco = (VB6Helpers.Left(VB6Helpers.Trim(MODGTAB0.GetNombreBanco((int)planilla.entaut, this)), 25));
            //m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_Bco(initObject, unit, planilla.EntAut);
            //if (m >= 0)
            //{
            //    model.VBco_NomBco = (VB6Helpers.Left(VB6Helpers.Trim(MODGTAB0.VBco[m].NomBco), 25));
            //}

            //Código Entidad Autorizada.
            model.VxAnus_EntAut = (VB6Helpers.Format(VB6Helpers.CStr(planilla.entaut), "000"));

            //Datos de Aduana.
            if (planilla.codadn != 0)
            {
                model.VAdn_NomAdn = this.ObtenerAduanas()[planilla.codadn];
                //Aduana.
                //m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VAdn(MODGTAB1, unit, planilla.CodAdn);
                //if (m >= 0)
                //{
                //    model.VAdn_NomAdn = (VB6Helpers.Trim(MODGTAB1.VAdn[m].NomAdn));
                //}

                //Código Aduana.
                model.VxAnus_CodAdn = (VB6Helpers.Format(VB6Helpers.CStr(planilla.codadn), "00"));
            }

            //Moneda.
            if (planilla.codmnd != 0)
            {
                model.VMnd_Mnd_MndNom = MODGTAB0.GetNombreMoneda((int)planilla.codmnd, this);

                //m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, planilla.CodMnd);
                //if (m != 0)
                //{
                //    model.VMnd_Mnd_MndNom = (VB6Helpers.Trim(MODGTAB0.VMnd[m].Mnd_MndNom));
                //}

                //Código Moneda.
                model.VMnd_Mnd_MndCbc = (VB6Helpers.Format(VB6Helpers.CStr(MODGTAB0.GetCodigoMonedaBancoCentral((int)planilla.codmnd,this)), "000"));
            }

            //Número Presentación Original.
            if (!string.IsNullOrEmpty(planilla.numpreo))
            {


                n = VB6Helpers.Format(planilla.numpreo, "0000000");
                model.VxAnus_NumpreO = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
            }

            //Número Aceptación.
            if (!string.IsNullOrEmpty(planilla.numdec))
            {


                n = VB6Helpers.Format(planilla.numdec, "0000000");
                model.VxAnus_numdec = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
            }

            //Monto Anulado.
            if (planilla.mtoanu != 0)
            {
                n = Format.FormatCurrency((double)planilla.mtoanu, "#,###,###,###,##0.00");
                model.VxAnus_MtoAnu = (this.PoneChar(n, " ", "H", 20));
            }

            //Fecha Presentación Original.
            //if (!string.IsNullOrWhiteSpace(planilla.fecpreo))
            //{
                model.VxAnus_FecpreO = planilla.fecpreo.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //}

            //Fecha Aceptación.
            //if (!string.IsNullOrWhiteSpace(planilla.fecdec))
            //{
                model.VxAnus_FecDec = planilla.fecdec.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //}

            //Paridad Anulada.
            if (planilla.mtopara != 0)
            {
                n = Format.FormatCurrency((double)planilla.mtopara, "#,###,##0.0000");
                model.VxAnus_MtoParA = (this.PoneChar(n, " ", "H", 20));
            }

            //Tipo de Operación.
            if (planilla.tippln != 0)
            {
                model.VxAnus_NomTipPln = (VB6Helpers.Trim(VB6Helpers.Mid(this.GetNomPLn((short)planilla.tippln), 1, 17)));
                //model.VxAnus_TipPln = (VB6Helpers.Trim(VB6Helpers.Mid(this.GetNomPLn((short)planilla.tippln), 1, 17)));
                //model.VxAnus_TipPln = (VB6Helpers.Trim(VB6Helpers.Mid(MODXPLN1.GetNomPLn(planilla.tippln), 1, 17)));
                //Código Tipo de Operación.
                model.VxAnus_TipPln = (VB6Helpers.Format(VB6Helpers.CStr(planilla.tippln), "000"));
            }

            //Fecha Vencimiento Retorno.
            //if (!string.IsNullOrWhiteSpace(planilla.fecven))
            //{
                model.VxAnus_FecVen = planilla.fecven.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //}

            //Monto en Dolar Anulado.
            if (planilla.mtodola != 0)
            {
                n = Format.FormatCurrency((double)planilla.mtodola, "#,###,###,###,##0.00");
                model.VxAnus_MtoDolA = (this.PoneChar(VB6Helpers.Format(n, "0.00"), " ", "H", 20));
            }

            //Plaza Banco Central.
            if (planilla.plzbcc != 0)
            {

                model.VPbc_Pbc_PbcDes = MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.plzbcc, this);
                //Descripción Plaza Banco Central.
                //m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VPbc(initObject, unit, planilla.PlzBcc);
                //if (m >= 0)
                //{
                //    model.VPbc_Pbc_PbcDes = (VB6Helpers.Trim(MODGTAB1.VPbc[m].Pbc_PbcDes));
                //}

                //Código Plaza Banco Central.
                model.VxAnus_PlzBcc = (VB6Helpers.Format(VB6Helpers.CStr(planilla.plzbcc) + " 00"));
            }

            //Monto Paridad Original.
            if (planilla.mtodolpo != 0)
            {
                n = Format.FormatCurrency((double)planilla.mtodolpo, "#,###,###,###,##0.00");
                model.VxAnus_MtoDolPo = (this.PoneChar(n, " ", "H", 20));
            }

            //Monto Dolar.
            if (planilla.mtodol != 0)
            {
                n = Format.FormatCurrency((double)planilla.mtodol, "#,###,###,###,##0.00");
                model.VxAnus_MtoDol = (this.PoneChar(n, " ", "H", 20));
            }

            //Monto Paridad.
            if (planilla.mtopar != 0)
            {
                n = Format.FormatCurrency((double)planilla.mtopar, "#,###,##0.0000");
                model.VxAnus_Mtopar = (this.PoneChar(n, " ", "H", 20));
            }

            //-------------------------
            //Observaciones.
            //-------------------------
            if (!string.IsNullOrWhiteSpace(planilla.obspln))
            {
                Texto = this.Componer(VB6Helpers.Trim(planilla.obspln), VB6Helpers.Chr(13) + VB6Helpers.Chr(10), " ");
                Texto = Texto + "&";
                palabra = "";
                for (co = 1; co <= (short)VB6Helpers.Len(Texto); co++)
                {
                    letra = VB6Helpers.Mid(Texto, co, 1);
                    if (letra == " " || letra == "&")
                    {
                        if (VB6Helpers.Len(palabra) >= 60 || letra == "&")
                        {
                            model.Palabras.Add(palabra);
                            lin = Format.StringToDouble(lin) + 0.3;
                            palabra = "";
                        }
                        else
                        {
                            // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'palabra' variable as a StringBuilder6 object.
                            palabra += " ";
                        }

                    }
                    else
                    {
                        palabra += letra;
                    }

                }

            }

            return model;
        }

        public Planilla401 ImpimirPlanilla401(T_xPlv planilla)
        {
            short num_cop = 0;
            short m = 0;
            string R = "";
            string n = "";
            var modelo = new Planilla401();
            if (!string.IsNullOrEmpty(planilla.NumPre))
            {
                n = VB6Helpers.Format(planilla.NumPre, "0000000");
                modelo.VxPlvs_NumPre = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
            }

            //Plaza Banco Central que Contabiliza...OK¡¡¡
            if (planilla.PlzBcc != 0)
            {
                modelo.VPbc_Pbc_PbcDes = MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.PlzBcc, this);
                //if (m >= 0)
                //{
                //    model.VPbc_Pbc_PbcDes = (VB6Helpers.Trim(MODGTAB1.VPbc[m].Pbc_PbcDes));
                //}

                //------------------------------------------
                //Código Plaza Banco Central que Contabiliza
                //------------------------------------------
                modelo.VxPlvs_PlzBcc = (VB6Helpers.Format(planilla.PlzBcc, "00"));
                //68    '2.25
                //m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VPbc(initObject, unit, planilla.PlzBcc);
                //if (m >= 0)
                //{
                //    modelo.VPbc_Pbc_PbcDes = (VB6Helpers.Trim(MODGTAB1.VPbc[m].Pbc_PbcDes));
                //}
                ////Código Plaza Banco Central que Contabiliza...OK¡¡¡
                ////86
                ////68    '2.25
                //modelo.VxPlvs_PlzBcc = (VB6Helpers.Format(VB6Helpers.CStr(planilla.PlzBcc), "00"));
            }

            //Fecha Presentación....OK¡¡¡
            //if (!string.IsNullOrWhiteSpace(planilla.fecpre))
            //{


            modelo.VxPlvs_fecpre = planilla.fecpre.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //}

            //Tipo de Operación....OK¡¡¡
            if (planilla.TipPln != 0)
            {

                //8    '3.08
                modelo.NomPLn = (VB6Helpers.Trim(VB6Helpers.Mid(this.GetNomPLn((short)planilla.TipPln), 1, 31)));
                //Código Tipo de Operación...OK¡¡¡
                //4
                //8   '3
                modelo.VxPlvs_TipPln = (VB6Helpers.Format(VB6Helpers.CStr(planilla.TipPln), "000"));
            }

            //Nombre....OK¡¡¡
            if (!string.IsNullOrWhiteSpace(planilla.PrtExp))
            {
                modelo.DatPrt = MODGTAB0.GetNombreParty(planilla.PrtExp, (int)planilla.IndNom, this);
                modelo.DatPrtn = MODGTAB0.GetDireccionParty(planilla.PrtExp, (byte)planilla.IndDir, this);
                //4
                //modelo.DatPrt = (Mdl_Funciones_Varias.GetDatPrt(initObject, unit, planilla.PrtExp, planilla.IndNom, planilla.IndDir, "N"));
                //Dirección....OK¡¡¡

                //8
                //modelo.DatPrtn = (Mdl_Funciones_Varias.GetDatPrtn(unit, planilla.PrtExp, planilla.IndNom, planilla.IndDir, "D", "DC"));
            }

            //Rut....OK¡¡¡
            if (!string.IsNullOrWhiteSpace(planilla.RutExp))
            {
                R = MODXPLN1.ConvRut(planilla.RutExp);
                //20
                //9

                modelo.VxPlvs_RutExp = (VB6Helpers.Mid(R, 1, VB6Helpers.Len(R) - 1) + "-" + VB6Helpers.Mid(R, VB6Helpers.Len(R), 1));
            }

            //SE IMPRIMEN LAS PLANILLAS 401
            //------------------------------
            //------------------------------
            // Antecedentes financiamiento
            //------------------------------

            //*********************ANTECEDENTES FINANCIAMIENTO*******************
            //moneda...OK¡¡¡
            if (planilla.AfiMnd != 0)
            {
                modelo.VMnd_Mnd_MndNom = MODGTAB0.GetNombreMoneda((int)planilla.AfiMnd, this);
                //if (m != 0)
                //{
                //    model.VMnd_Mnd_MndNom = (VB6Helpers.Trim(MODGPYF1.Minuscula(MODGTAB0.VMnd[m].Mnd_MndNom)));
                //}

                //-------------
                //Código Moneda
                //-------------
                modelo.VMnd_Mnd_MndCbc = (VB6Helpers.Format(VB6Helpers.CStr(planilla.AfiMnd), "000"));
                //m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, planilla.AfiMnd);
                //if (m != 0)
                //{
                //    modelo.VMnd_Mnd_MndNom = (VB6Helpers.LTrim(VB6Helpers.RTrim(MODGTAB0.VMnd[m].Mnd_MndNom))); //...OK¡¡¡
                //    modelo.VMnd_Mnd_MndCbc = (VB6Helpers.Format(VB6Helpers.CStr(MODGTAB0.VMnd[m].Mnd_MndCbc), "000")); //...OK¡¡¡
                //}
            }

            //paridad...OK¡¡¡
            if (planilla.AfiPar != 0)
            {
                //6.85
                modelo.VxPlvs_AfiPar = (Format.FormatCurrency((double)planilla.AfiPar, "0.0000"));
            }

            //monto...OK¡¡¡
            if (planilla.AfiMto != 0)
            {
                //7.68
                modelo.VxPlvs_AfiMto = (Format.FormatCurrency((double)planilla.AfiMto, "0.00"));
            }

            //monto en us$...OK¡¡¡
            if (planilla.AfiMtoD != 0)
            {
                //8.55
                modelo.VxPlvs_AfiMtoD = (Format.FormatCurrency((double)planilla.AfiMtoD, "0.00"));
            }

            //Plazo Vencimiento Financiamiento....OK¡¡¡
            if (planilla.AfiVen != 0)
            {
                //9.42
                modelo.VxPlvs_AfiVen = (VB6Helpers.Trim(VB6Helpers.Str(planilla.AfiVen)) + " días"); //Paola
            }

            //Tipo de Cambio de la Operación....OK¡¡¡(MONTO RETORNADO)
            if (planilla.TipCam != 0 && planilla.TipPln != 402)
            {
                n = Format.FormatCurrency((double)planilla.TipCam, "#,###,##0.00");
                modelo.VxPlvs_TipCam = (this.PoneChar(n, " ", "H", 12));
            }
            string palabra = String.Empty;
            if (!string.IsNullOrEmpty(planilla.ObsPln))
            {
                var Texto = this.Componer(VB6Helpers.Trim(planilla.ObsPln), VB6Helpers.Chr(13) + VB6Helpers.Chr(10), " ");
                Texto = Texto + "&";

                for (int co = 1; co <= (short)VB6Helpers.Len(Texto); co++)
                {
                    var letra = VB6Helpers.Mid(Texto, co, 1);
                    if (letra == " " || letra == "&")
                    {
                        if (VB6Helpers.Len(palabra) >= 60 || letra == "&")
                        {
                            modelo.Palabras.Add(palabra);
                            palabra = "";
                        }
                        else
                        {
                            palabra += " ";
                        }

                    }
                    else
                    {
                        palabra += letra;
                    }
                }
            }
            return modelo;
        }

        public Planilla500 ImprimirPlanilla500(T_xPlv planilla)
        {
            short num_cop = 0;
            short m = 0;
            string R = "";
            string n = "";
            var modelo = new Planilla500();
            if (!string.IsNullOrEmpty(planilla.NumPre))
            {
                n = VB6Helpers.Format(planilla.NumPre, "0000000");
                modelo.VxPlvs_NumPre = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
            }

            //Plaza Banco Central que Contabiliza...OK¡¡¡
            if (planilla.PlzBcc != 0)
            {
                modelo.VPbc_Pbc_PbcDes = MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.PlzBcc, this);
                //if (m >= 0)
                //{
                //    model.VPbc_Pbc_PbcDes = (VB6Helpers.Trim(MODGTAB1.VPbc[m].Pbc_PbcDes));
                //}

                //------------------------------------------
                //Código Plaza Banco Central que Contabiliza
                //------------------------------------------
                modelo.VxPlvs_PlzBcc = (VB6Helpers.Format(planilla.PlzBcc, "00"));
                //68    '2.25
                //m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VPbc(initObject, unit, planilla.PlzBcc);
                //if (m >= 0)
                //{
                //    modelo.VPbc_Pbc_PbcDes = (VB6Helpers.Trim(MODGTAB1.VPbc[m].Pbc_PbcDes));
                //}
                ////Código Plaza Banco Central que Contabiliza...OK¡¡¡
                ////86
                ////68    '2.25
                //modelo.VxPlvs_PlzBcc = (VB6Helpers.Format(VB6Helpers.CStr(planilla.PlzBcc), "00"));
            }

            //Fecha Presentación....OK¡¡¡
            //if (!string.IsNullOrWhiteSpace(planilla.fecpre))
            //{


                modelo.VxPlvs_fecpre = planilla.fecpre.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //}

            //Tipo de Operación....OK¡¡¡
            if (planilla.TipPln != 0)
            {

                //8    '3.08
                modelo.NomPLn = (VB6Helpers.Trim(VB6Helpers.Mid(this.GetNomPLn((short)planilla.TipPln), 1, 31)));
                //Código Tipo de Operación...OK¡¡¡
                //4
                //8   '3
                modelo.VxPlvs_TipPln = (VB6Helpers.Format(VB6Helpers.CStr(planilla.TipPln), "000"));
            }

            //Nombre....OK¡¡¡
            if (!string.IsNullOrWhiteSpace(planilla.PrtExp))
            {
                modelo.DatPrt = MODGTAB0.GetNombreParty(planilla.PrtExp, (int)planilla.IndNom, this);
                modelo.DatPrtn = MODGTAB0.GetDireccionParty(planilla.PrtExp, (byte)planilla.IndDir, this);
                //4
                //modelo.DatPrt = (Mdl_Funciones_Varias.GetDatPrt(initObject, unit, planilla.PrtExp, planilla.IndNom, planilla.IndDir, "N"));
                //Dirección....OK¡¡¡

                //8
                //modelo.DatPrtn = (Mdl_Funciones_Varias.GetDatPrtn(unit, planilla.PrtExp, planilla.IndNom, planilla.IndDir, "D", "DC"));
            }

            //Rut....OK¡¡¡
            if (!string.IsNullOrWhiteSpace(planilla.RutExp))
            {
                R = MODXPLN1.ConvRut(planilla.RutExp);
                //20
                //9

                modelo.VxPlvs_RutExp = (VB6Helpers.Mid(R, 1, VB6Helpers.Len(R) - 1) + "-" + VB6Helpers.Mid(R, VB6Helpers.Len(R), 1));
            }
            //Moneda....OK¡¡¡MONTO RETORNADO
            if (planilla.CodMnd != 0)
            {
                modelo.VMnd_Mnd_MndNom = MODGTAB0.GetNombreMoneda((int)planilla.CodMnd, this);
                //if (m != 0)
                //{
                //    model.VMnd_Mnd_MndNom = (VB6Helpers.Trim(MODGPYF1.Minuscula(MODGTAB0.VMnd[m].Mnd_MndNom)));
                //}

                //-------------
                //Código Moneda
                //-------------
                modelo.VMnd_Mnd_MndCbc = (VB6Helpers.Trim(VB6Helpers.Format(planilla.CodMnd, "000")));

                //m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB0.Get_VMnd(MODGTAB0, unit, planilla.CodMnd);
                //if (m != 0)
                //{
                //    modelo.VMnd_Mnd_MndNom = (VB6Helpers.Trim(MODGTAB0.VMnd[m].Mnd_MndNom));
                //}
                ////Código Moneda....OK¡¡¡MONTO RETORNADO
                ////5
                //modelo.VMnd_Mnd_MndCbc = (VB6Helpers.Format(VB6Helpers.CStr(MODGTAB0.VMnd[m].Mnd_MndCbc), "000"));
            }

            //Datos de Aduana.(DATOS DECLARACION EXP)...OK¡¡¡
            if (planilla.codadn != 0)
            {
                //Aduana.
                //55
                //m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VAdn(MODGTAB1, unit, planilla.codadn);
                //if (m >= 0)
                //{
                //    modelo.VAdn_NomAdn = (VB6Helpers.Trim(MODGTAB1.VAdn[m].NomAdn));
                //}
                modelo.VAdn_NomAdn = this.ObtenerAduanas()[planilla.codadn];
                //Código Aduana.(DATOS DECLARACION EXP)...OK¡¡¡
                //6
                //54
                modelo.VxPlvs_CodAdn = (VB6Helpers.Format(VB6Helpers.CStr(planilla.codadn), "00"));
            }

            //Entidad Autorizada.(DATOS FIN.ORIGINAL)...OK¡¡¡
            if (planilla.DfoCea != 0)
            {
                //Descripción Entidad Autorizada.
                //5
                modelo.VBco_NomBco = (VB6Helpers.Trim(VB6Helpers.Left(MODGTAB0.GetNombreBanco((int)planilla.DfoCea, this), 16)));
                //if (m >= 0)
                //{
                //    modelo.VBco_NomBco = (VB6Helpers.Trim(VB6Helpers.Left(MODGTAB0.VBco[m].NomBco, 16)));
                //}
                //Código Entidad Autorizada.(DATOS FIN.ORIGINAL)...OK¡¡¡
                //5
                modelo.VxPlvs_DfoCea = (VB6Helpers.Format(VB6Helpers.CStr(planilla.DfoCea), "00"));
            }

            //Datos de Plaza Banco Central.(DATOS INF. EXP.)..OK¡¡¡
            if (planilla.DiePbc != 0)
            {
                //Die, Código Plaza Banco Central.
                //6
                modelo.VxPlvs_DiePbc = (VB6Helpers.Format(VB6Helpers.CStr(planilla.DiePbc), "00"));
                //Die, Plaza Banco Central.(DATOS INF. EXP.)..OK¡¡¡
                //02
                modelo.VPbc_Pbc_PbcDes = MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.DiePbc, this);
                //if (m >= 0)
                //{
                //    modelo.VPbc_Pbc_PbcDes = (VB6Helpers.Trim(MODGTAB1.VPbc[m].Pbc_PbcDes));
                //}
            }

            //Valor Bruto....OK¡¡¡
            if (planilla.MtoBru != 0)
            {
                //5
                n = Format.FormatCurrency((double)planilla.MtoBru, "#,###,###,###,##0.00");
                modelo.VxPlvs_MtoBru = (this.PoneChar(n, " ", "H", 20));
            }

            //Número Aceptación....OK¡¡¡DATOS DEC. EXP.
            if (!string.IsNullOrEmpty(planilla.NumDec))
            {
                //6
                n = VB6Helpers.Format(planilla.NumDec, "0000000");
                modelo.VxPlvs_numdec = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
            }

            //Tipo de finaciamiento(DATOS FIN. ORIGINAL)...OK¡¡¡
            if (planilla.DfoCtf != 0)
            {
                //descripción tipo financiamiento
                //4
                modelo.NomPLn = (VB6Helpers.Left(VB6Helpers.Trim(this.GetNomPLn((short)planilla.DfoCtf)), 16));

                //Código Tipo Financiamiento.
                //4
                modelo.VxPlvs_DfoCtf = (planilla.DfoCtf).ToString();
            }

            //Monto Comisión
            if (planilla.MtoCom != 0)
            {
                var Pri = (short)(true ? -1 : 0);
                if (planilla.DedCom)
                {
                    Pri = (short)(false ? -1 : 0);
                }

                n = Format.FormatCurrency((double)planilla.MtoCom, "#,###,###,###,##0.00");
                if (Pri != 0)
                {
                    modelo.VxPlvs_MtoCom = (this.PoneChar(n, " ", "H", 20));
                }
                else
                {
                    modelo.VxPlvs_MtoCom = ("(" + this.PoneChar(n, " ", "H", 20) + ")");
                }
            }

            //--------------------------------------------------------------------------
            //Fecha Aceptación.(DATOS DECLARACION DE EXP.)...OK¡¡¡
            //if (!string.IsNullOrEmpty(planilla.FecDec))
            //{
                //4
                modelo.VxPlvs_FecDec = planilla.FecDec.ToString("dd/MM/yyyy");
            //}

            //------------------------------------------------------------------------
            //Plaza Banco Central.(DATOS FIN. ORIGINAL)...OK¡¡¡
            if (planilla.DfoCbc != 0)
            {
                //Descripción Entidad Autorizada.(DATOS FIN. ORIGINAL)...OK¡¡¡
                //3
                modelo.VPbc_Pbc_PbcDes = VB6Helpers.Trim(VB6Helpers.Left(MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.DfoCbc, this), 16));
                //if (m >= 0)
                //{
                //    modelo.VPbc_Pbc_PbcDes = VB6Helpers.Trim(VB6Helpers.Left(MODGTAB1.VPbc[m].Pbc_PbcDes, 16));
                //}
                //Código Entidad Autorizada.(DATOS FIN. ORIGINAL)...OK¡¡¡
                //5
                //3
                modelo.VxPlvs_DfoCbc = (VB6Helpers.Format(VB6Helpers.CStr(planilla.DfoCbc), "00"));
            }

            //Die, Número de Emisión.(DATOS INF. EXPORTACION)...OK¡¡¡
            if (!string.IsNullOrEmpty(planilla.DieNum))
            {
                n = planilla.DieNum;
                modelo.VxPlvs_DieNum = (VB6Helpers.Mid(n, 1, VB6Helpers.Len(n) - 1) + "-" + VB6Helpers.Right(n, 1));
            }

            //*******************************************************************
            //*******************************************************************
            //Monto Otorgado.
            if (planilla.MtoOtg != 0)
            {
                var Pri = (short)(true ? -1 : 0);
                //5
                n = Format.FormatCurrency((double)planilla.MtoOtg, "#,###,###,###,##0.00");
                if (Pri != 0)
                {
                    modelo.VxPlvs_MtoOtg = (this.PoneChar(n, " ", "H", 20));
                }
            }

            //*****************************************************************

            //Fecha Vencimiento Retorno.(DATOS DECLARACION EXP)...OK¡¡¡
            //if (!string.IsNullOrEmpty(planilla.FecVen))
            //{
                //2
                modelo.VxPlvs_FecVen = planilla.FecVen.ToString("dd/MM/yyyy");
            //}

            //Número de Presentación.(DATOS FIN. ORIGINAL)...OK¡¡¡
            if (!string.IsNullOrEmpty(planilla.DfoNpr))
            {
                //9
                n = VB6Helpers.Format(planilla.DfoNpr, "0000000");
                modelo.VxPlvs_DfoNpr = (VB6Helpers.Trim(VB6Helpers.Left(n, 6) + "-" + VB6Helpers.Right(n, 1)));
            }

            //Valor Líquido...OK¡¡¡
            if (planilla.MtoLiq != 0)
            {
                //9
                n = Format.FormatCurrency((double)planilla.MtoLiq, "#,###,###,###,##0.00");
                modelo.VxPlvs_MtoLiq = (this.PoneChar(n, " ", "H", 20));
            }

            //Fecha de Presentación.(DATOS FIN. ORIGINAL)...OK¡¡¡
            //if (!string.IsNullOrEmpty(planilla.DfoFpr))
            //{
                //8
                modelo.VxPlvs_DfoFpr = planilla.DfoFpr.ToString("dd/MM/yyyy");
            //}

            //Plazo Vencimiento del Financiamiento.(ant.financ..)..OK¡¡¡
            if (planilla.AfiVen != 0)
            {
                modelo.VxPlvs_AfiVen = (VB6Helpers.Trim(VB6Helpers.Str(planilla.AfiVen)) + " días.");
            }

            //Die, Fecha de Emisión.(DATOS INF. EXP)..OK¡¡¡
            //if (!string.IsNullOrEmpty(planilla.DieFec))
            //{
                modelo.VxPlvs_DieFec = planilla.DieFec.ToString("dd/MM/yyyy");
            //}

            //Paridad a US$.(MONTO RETORNADO)...OK¡¡¡
            if (planilla.Mtopar != 0)
            {
                //7
                n = Format.FormatCurrency((double)planilla.Mtopar, "#,###,##0.0000");
                modelo.VxPlvs_Mtopar = (this.PoneChar(n, " ", "H", 20));
            }

            //Monto en US$....(MONTO RETORNADO)...OK¡¡¡
            if (planilla.MtoDol != 0)
            {
                //5
                n = Format.FormatCurrency((double)planilla.MtoDol, "#,###,###,###,##0.00");
                modelo.VxPlvs_MtoDol = (this.PoneChar(n, " ", "H", 20));
            }

            //Tipo de Cambio de la Operación.(MONTO RETORNADO)...OK¡¡¡
            if (VB6Helpers.Instr(T_MODXPLN1.PlnEst, VB6Helpers.CStr(planilla.TipPln)) == 0 && VB6Helpers.Instr(T_MODXPLN1.PLNINF, VB6Helpers.CStr(planilla.TipPln)) == 0)
            {
                if (planilla.TipCam != 0)
                {
                    n = Format.FormatCurrency((double)planilla.TipCam, "#,###,##0.00");
                    modelo.VxPlvs_TipCam = (this.PoneChar(n, " ", "H", 12));
                }
            }
            string palabra = String.Empty;
            if (!string.IsNullOrEmpty(planilla.ObsPln))
            {
                var Texto = this.Componer(VB6Helpers.Trim(planilla.ObsPln), VB6Helpers.Chr(13) + VB6Helpers.Chr(10), " ");
                Texto = Texto + "&";

                for (int co = 1; co <= (short)VB6Helpers.Len(Texto); co++)
                {
                    var letra = VB6Helpers.Mid(Texto, co, 1);
                    if (letra == " " || letra == "&")
                    {
                        if (VB6Helpers.Len(palabra) >= 60 || letra == "&")
                        {
                            modelo.Palabras.Add(palabra);
                            palabra = "";
                        }
                        else
                        {
                            palabra += " ";
                        }

                    }
                    else
                    {
                        palabra += letra;
                    }

                }

            }
            //END_Pr_Imprime_xPlv(modelo, initObject, i);
            return modelo;
        }

        //****************************************************************************
        //   1.  Imprime las n copias de todas las planillas Invisible-Export.-
        //****************************************************************************
        public PlanillaInvisible ImprimirPlanillaInvisibleExportacion(string numeroPresentacion, DateTime fechaPresentacion)
        {
            string n = "";
            short m = 0;
            string rut = "";
            string s = "";
            short a = 0;
            string Texto = "";
            string palabra = "";
            dynamic co = null;
            string letra = "";
            //Recuperamos los datos iniciales de la planilla
            var planilla = this.Sce_Xpli_S06(numeroPresentacion, fechaPresentacion);
            var model = new PlanillaInvisible();
            //-------------------
            //Número Presentación
            //-------------------
            if (!string.IsNullOrWhiteSpace(planilla.numpli))
            {
                model.Vplis_NumPli = (VB6Helpers.Trim(planilla.numpli));
            }

            //-----------
            //Código Pais
            //-----------
            if (planilla.codpai != 0)
            {
                model.VPai_Pai_PaiNom = MODGTAB0.GetNombrePais((int)planilla.codpai, this);
                //if (m != 0)
                //{
                //    model.VPai_Pai_PaiNom = (VB6Helpers.Trim(MODGPYF1.Minuscula(MODGTAB0.VPai[m].Pai_PaiNom)));
                //}
                model.Vplis_codpai = VB6Helpers.Trim(VB6Helpers.Format(planilla.codpai, "000"));
            }

            //-------------------
            //Código de Operación
            //-------------------
            if (planilla.codoci != 0)
            {
                model.Vplis_CodOci = (double)planilla.codoci;
            }

            //------------------
            //Fecha Presentación
            //------------------
            if (planilla.fecpli.HasValue)
            {
                model.Vplis_FecPli = planilla.fecpli.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            //-----------------------------------
            //Plaza Banco Central que Contabiliza
            //-----------------------------------
            if (planilla.plzbcc != 0)
            {
                model.VPbc_Pbc_PbcDes = MODGTAB0.GetNombrePlazaBancoCentral((int)planilla.plzbcc, this);
                //if (m >= 0)
                //{
                //    model.VPbc_Pbc_PbcDes = (VB6Helpers.Trim(MODGTAB1.VPbc[m].Pbc_PbcDes));
                //}

                //------------------------------------------
                //Código Plaza Banco Central que Contabiliza
                //------------------------------------------
                model.Vplis_PlzBcc = (VB6Helpers.Format(planilla.plzbcc, "00"));
            }

            //------
            //Moneda
            //------
            if (planilla.codmnd != 0)
            {
                model.VMnd_Mnd_MndNom = MODGTAB0.GetNombreMoneda((int)planilla.codmnd, this);
                //if (m != 0)
                //{
                //    model.VMnd_Mnd_MndNom = (VB6Helpers.Trim(MODGPYF1.Minuscula(MODGTAB0.VMnd[m].Mnd_MndNom)));
                //}

                //-------------
                //Código Moneda
                //-------------
                model.Vplis_CodMndBC = (VB6Helpers.Trim(VB6Helpers.Format(planilla.codmndbc, "000")));
            }

            //-----------------
            //Tipo de Operación
            //-----------------
            if (planilla.tippln != 0)
            {
                model.Vplis_TipPln = (short)planilla.tippln;
            }

            //---
            //Rut
            //---
            if (!string.IsNullOrWhiteSpace(planilla.rutcli))
            {
                rut = MODXPLN1.ConvRut(VB6Helpers.Trim(planilla.rutcli));
                model.Vplis_rutcli = (VB6Helpers.Mid(rut, 1, VB6Helpers.Len(rut) - 1) + "-" + VB6Helpers.Mid(rut, VB6Helpers.Len(rut), 1));
            }

            //---------------------
            //Nombre del Interesado
            //---------------------
            if (!string.IsNullOrWhiteSpace(planilla.prtcli))
            {
                //if (planilla.indnom != -1)
                //{
                model.DatPrt1 = MODGTAB0.GetNombreParty(planilla.prtcli, (int)planilla.indnom, this);
                //}
                //if (planilla.inddir != -1)
                //{
                //    model.DatPrt1 = VB6Helpers.Trim(SyGet_Dad(unit, p, IndDir));
                //}
                //model.DatPrt1 = (VB6Helpers.Trim(Mdl_Funciones_Varias.GetDatPrt(initObject, unit, planilla.prtcli, planilla.indnom, planilla.inddir, "N")));
            }

            //---------------
            //Monto Operación
            //---------------
            if (planilla.mtoope != 0)
            {
                n = Format.FormatCurrency((double)planilla.mtoope, "#,###,###,###,##0.00");
                model.Vplis_MtoOpe = n;
            }

            //------------------
            //Dirección completa
            //------------------
            if (!string.IsNullOrWhiteSpace(planilla.prtcli))
            {
                model.DatPrt2 = MODGTAB0.GetDireccionParty(planilla.prtcli, (byte)planilla.inddir, this);
                //model.DatPrt2 = (VB6Helpers.Trim(Mdl_Funciones_Varias.GetDatPrt(initObject, unit, planilla.PrtCli, planilla.IndNom, planilla.IndDir, "D")));
            }

            //-------------
            //Valor Paridad
            //-------------
            if (planilla.mtopar != 0)
            {
                n = Format.FormatCurrency((double)planilla.mtopar, "#,###,##0.0000");
                model.Vplis_Mtopar = n;
            }

            //-------------------------
            //Nombre Código de Comercio
            //-------------------------
            s = planilla.codcom + planilla.concep;
            if (!string.IsNullOrWhiteSpace(s))
            {
                //m = BCH.Comex.Core.BL.XCFT.Modulos.MODGTAB1.Get_VTcp(MODGTAB1, unit, s);
                //if (m >= 0)
                //{
                //    if (!string.IsNullOrWhiteSpace(MODGTAB1.VTcp[m].DesTcp))
                //    {
                model.VTcp_DesTcp = this.GetNombreCodigoComercio(planilla.codcom, planilla.concep);
                //    }
                //}
            }

            s = planilla.codcom;
            if (!string.IsNullOrWhiteSpace(s))
            {
                model.Vplis_codcom = (VB6Helpers.Trim(VB6Helpers.Left(s, 2) + "." + VB6Helpers.Mid(s, 3, 2) + "." + VB6Helpers.Right(s, 2)));
            }

            if (!string.IsNullOrWhiteSpace(planilla.concep))
            {
                model.Vplis_Concep = (VB6Helpers.Format(planilla.concep, "000"));
            }

            //----------------
            //Monto en Dolares
            //----------------
            if (planilla.mtodol != 0)
            {
                n = Format.FormatCurrency((double)planilla.mtodol, "#,###,###,###,##0.00");
                model.Vplis_MtoDol = n;
            }

            //-------------------------
            //Datos de Planilla Anulada
            //Nro. Planilla Anulada
            //-------------------------
            if (!string.IsNullOrWhiteSpace(planilla.anunum))
            {
                n = VB6Helpers.Format(planilla.anunum, "0000000");
                model.Vplis_AnuNum = (VB6Helpers.Trim(n));
            }

            //----------------------
            //Fecha Planilla Anulada
            //----------------------
            if (planilla.anufec.HasValue)
            {
                model.Vplis_AnuFec = planilla.anufec.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            //---------------------------------------
            //Plaza Banco Central de Planilla Anulada
            //---------------------------------------
            if (planilla.anupbc != 0)
            {
                n = VB6Helpers.Format(VB6Helpers.CStr(planilla.anupbc), "000");
                model.Vplis_AnuPbc = (VB6Helpers.Trim(n));
            }

            //------------------------------
            //Tipo de Cambio de la Operación
            //------------------------------
            if (planilla.tipcam != 0)
            {
                n = Format.FormatCurrency((double)planilla.tipcam, "#,###,##0.0000");
                model.Vplis_TipCam = n;
            }

            //--------------------
            //Tipo de Autorización
            //--------------------
            if (!string.IsNullOrWhiteSpace(planilla.apctip))
            {
                model.Vplis_ApcTip = (VB6Helpers.Trim(VB6Helpers.UCase(planilla.apctip)));
            }

            //----------------
            //Nro. de Planilla
            //----------------
            if (!string.IsNullOrWhiteSpace(planilla.apcnum))
            {
                n = VB6Helpers.Format(planilla.apcnum, "000000");
                model.VplisApcNum = (VB6Helpers.Trim(n));
            }

            //--------------------
            //Fecha de la Planilla
            //--------------------
            if (planilla.apcfec.HasValue)
            {
                model.Vplis_ApcFec = planilla.apcfec.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            //---------------------------------------
            //Plaza Banco Central de Planilla Anulada
            //---------------------------------------
            if (planilla.apcpbc != 0)
            {
                n = VB6Helpers.Format(VB6Helpers.CStr(planilla.apcpbc), "000");
                model.Vplis_ApcPbc = (VB6Helpers.Trim(n));
            }
            //--------------
            //Monto Nacional
            //--------------
            if (planilla.mtonac != 0)
            {
                //9
                n = Format.FormatCurrency((double)planilla.mtonac, "#,###,###,###,##0.00");
                model.Vplis_MtoNac = n;
            }

            if (!string.IsNullOrWhiteSpace(planilla.desacu))
            {
                m = this.cuentadestring(planilla.desacu, ";");
                model.Vplis_Desacu.Add(VB6Helpers.Trim(VB6Helpers.Str(m)));
                for (a = 1; a <= (short)m; a++)
                {
                    model.Vplis_Desacu.Add(VB6Helpers.Trim(this.copiardestring(planilla.desacu, ";", a)));
                }

            }

            //-----------------------
            //Fecha de Aut. de Débito
            //-----------------------
            if (planilla.fecdeb.HasValue)
            {
                model.Vplis_FecDeb = planilla.fecdeb.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            //------------------
            //Documento Nacional
            //------------------
            if (!string.IsNullOrWhiteSpace(planilla.docnac))
            {
                model.Vplis_DocNac = (VB6Helpers.Trim(planilla.docnac));
            }

            //---------------------------------
            //Número del IDE....INF.EXPORTACION
            //---------------------------------
            if (!string.IsNullOrWhiteSpace(planilla.dienum))
            {
                n = planilla.dienum;
                model.Vplis_DieNum = (VB6Helpers.Trim(VB6Helpers.Mid(n, 1, VB6Helpers.Len(n) - 1) + "-" + VB6Helpers.Mid(n, VB6Helpers.Len(n), 1)));
            }

            //--------------------------------
            //Fecha del IDE....INF.EXPORTACION
            //--------------------------------
            if (planilla.diefec.HasValue)
            {
                model.Vplis_DieFec = planilla.diefec.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            //-----------------------------------
            //PLAZA BCO CENTRAL...INF.EXPORTACION
            //-----------------------------------
            if (planilla.diepbc != 0)
            {
                model.Vplis_DiePbc = (VB6Helpers.Trim(VB6Helpers.Str(planilla.diepbc)));
            }

            //------------------------
            //Documento Extranjero....
            //------------------------
            if (!string.IsNullOrWhiteSpace(planilla.docext))
            {
                model.Vplis_DocExt = (VB6Helpers.Trim(planilla.docext) + " QQ");
            }

            //---------------------------------------------
            // Documento de Reexportación...INF.EXPORTACION
            //---------------------------------------------
            if (!string.IsNullOrEmpty(planilla.codeor))
            {
                model.Vplis_CodEOR = planilla.codeor;
            }

            //-----------------------------------
            //Código de Aduana....INF.EXPORTACION
            //-----------------------------------
            if (planilla.codadn != 0)
            {
                model.Vplis_CodAdn = (VB6Helpers.Trim(VB6Helpers.Str(planilla.codadn)));
            }

            //------------------------------------------
            //Número de la Declaración...INF.EXPORTACION
            //------------------------------------------
            if (!string.IsNullOrWhiteSpace(planilla.numdec))
            {
                model.Vplis_numdec = (VB6Helpers.Trim(VB6Helpers.Left(planilla.numdec, VB6Helpers.Len(planilla.numdec) - 1) + "-" + VB6Helpers.Right(planilla.numdec, 1)));
            }

            //-----------------------------------------
            //Fecha de la Declaración...INF.EXPORTACION
            //-----------------------------------------
            if (planilla.fecdec.HasValue)
            {
                model.Vplis_FecDec = planilla.fecdec.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            //-------------------------------
            //Observaciones...INF.EXPORTACION
            //-------------------------------
            if (!string.IsNullOrWhiteSpace(planilla.obspli))
            {
                Texto = this.Componer(VB6Helpers.Trim(planilla.obspli), VB6Helpers.Chr(13) + VB6Helpers.Chr(10), " ");
                Texto = Texto + "&";
                palabra = "";
                for (double co_Alias = 1; co_Alias <= VB6Helpers.Len(Texto); co_Alias++)
                {
                    co = co_Alias;
                    letra = VB6Helpers.Mid(Texto, VB6Helpers.CInt(co), 1);
                    if (letra == " " || letra == "&")
                    {
                        if (VB6Helpers.Len(palabra) >= 60 || letra == "&")
                        {
                            model.Palabras.Add(palabra);
                            palabra = "";
                        }
                        else
                        {
                            palabra += " ";
                        }
                    }
                    else
                    {
                        palabra += letra;
                    }
                }
            }

            //----------------------
            //Numéro Credito Externo
            //----------------------
            if (planilla.numcre != 0)
            {
                n = VB6Helpers.Str(planilla.numcre);
                model.Vplis_NumCre = (VB6Helpers.Trim(n));
            }

            //--------------------------------
            //Fecha desembolso Credito Externo
            //--------------------------------
            if (planilla.feccre.HasValue)
            {
                model.Vplis_FecCr = planilla.feccre.Value.ToString("dd/MM/yyyy");
            }

            //----------------------------------------
            //Codigo Moneda Desembolso Credito Externo
            //----------------------------------------
            if (planilla.mndcre != 0)
            {
                model.Vplis_MndCre = (VB6Helpers.Trim(VB6Helpers.Str(planilla.mndcre)));
            }

            //----------------------------------------
            //Codigo Moneda Desembolso Credito Externo
            //----------------------------------------
            if (planilla.mndcre != 0)
            {
                model.Vplis_MndCreRepeat = (VB6Helpers.Trim(VB6Helpers.Str(planilla.mndcre)));
            }

            //---------------------------------------------------
            //Monto Equivalente Moneda Desembolso Credito Externo
            //---------------------------------------------------
            if (planilla.mtocre != 0)
            {
                n = Format.FormatCurrency((double)planilla.mtocre, "#,###,###,###,##0.00");
                model.Vplis_MtoCre = (this.PoneChar(n, " ", "H", 20));
            }
            return model;
        }

        public short GetCodigoMonedaBancoCentral(int codigoMoneda)
        {
            var item = GetMoneda(codigoMoneda);
            return (short)item.MndCBC;
        }

        //Determina Cuantos substring separados por Separa hay dentro de
        //EnDonde. El string tiene la forma "----,----,----"
        private short cuentadestring(string EnDonde, string Separa)
        {
            short largo = (short)VB6Helpers.Len(Separa);
            short Inicio = 1;
            short Total = 0;
            short Fin = 0;
            while (Inicio != 0)
            {

                Fin = (short)VB6Helpers.Instr(Inicio, EnDonde, Separa);
                if (Fin != 0)
                {
                    //hay otro separador

                    Total = (short)(Total + 1);
                    Inicio = (short)(Fin + largo);
                }
                else
                {
                    //no hay otro
                    if (Inicio <= VB6Helpers.Len(EnDonde))
                    {
                        Total = (short)(Total + 1);
                    }
                    break;
                }

            }

            return Total;
        }

        /// <summary>
        /// Copia el Cual% elemento de DeDonde$ delimitado por Delim$
        /// la forma del string es "----,----,-----,----"
        /// </summary>
        /// <param name="DeDonde"></param>
        /// <param name="Delim"></param>
        /// <param name="Cual"></param>
        /// <returns></returns>
        private string copiardestring(string DeDonde, string Delim, short Cual)
        {
            short Inicio = 1;
            short Mas = (short)VB6Helpers.Len(Delim);
            short i = 0;
            short Fin = 0;
            double largo = VB6Helpers.Len(DeDonde);

            //primero buscamos el primer delimitador
            //primer elemento no tiene delimitador al inicio

            for (i = 1; i <= (short)(Cual - 1); i++)
            {
                Fin = (short)VB6Helpers.Instr(Inicio, DeDonde, Delim);
                if (Fin == 0)
                {
                    return "";
                    //no existe elemento
                }
                Inicio = (short)(Fin + Mas);
            }

            //en inicio tengo el primer caracter del string
            //busquemos el final

            Fin = (short)VB6Helpers.Instr(Inicio, DeDonde, Delim);
            if (Fin != 0)
            {
                //tiene delim final
                return VB6Helpers.Mid(DeDonde, Inicio, Fin - Inicio);
            }
            else
            {
                //ultimo elemento
                return VB6Helpers.Right(DeDonde, (int)(largo - Inicio + 1));
            }

        }

        // forma un nuevo string reemplazando todas las ocurrencias de "Que" por "En"
        // en "Donde".  Si no encuentra ninguna retorna "Donde"
        //private  string ComponerUna(string Donde, string Que, string En)
        //{
        //    string ComponerUna = "";

        //    int Aqui = 0;
        //    string Sale = "";

        //    Sale = Donde;
        //    Aqui = Sale.InStr(Que, 1, StringComparison.CurrentCulture);
        //    if (Aqui != 0)
        //    {
        //        Sale = Sale.Left((Aqui - 1)) + En + Sale.Mid((Aqui + Que.Len()));
        //    }
        //    ComponerUna = Sale;

        //    return ComponerUna;
        //}
      
        /// <summary>
        /// forma un nuevo string reemplazando todas las ocurrencias de "Que" por "En"
        /// en "Donde".  Si no encuentra ninguna retorna "Donde"
        /// @estanislao: hace un Donde.Replace(Que, En)
        /// </summary>
        /// <param name="Donde"></param>
        /// <param name="Que"></param>
        /// <param name="En"></param>
        /// <returns></returns>
        private string Componer(string Donde, string Que, string En)
        {
            if (!string.IsNullOrEmpty(Donde))
            {
                return Donde.Replace(Que, En);
            }
            else
            {
                return string.Empty;
            }
        }

        //Incluye n caracteres a la izquierad o derecha de un string.-
        // UPGRADE_INFO (#0561): The 'Caracter' symbol was defined without an explicit "As" clause.
        private string PoneChar(string Texto, dynamic Caracter, string DerIzq, short largo)
        {
            string T = VB6Helpers.Trim(Texto);
            short i = 0;
            string s = "";
            if (VB6Helpers.Len(T) >= largo)
            {
                return T;
            }

            for (i = 1; i <= (short)(largo - VB6Helpers.Len(T)); i++)
            {
                // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 's' variable as a StringBuilder6 object.
                // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Caracter'. Consider using the GetDefaultMember6 helper method.
                s += Caracter;
            }

            if (DerIzq == "D")
            {
                return T + s;
            }
            else if (DerIzq == "H")
            {
                return s + T;
            }

            return "";
        }

        //Retorna el Nombre de la Planilla.
        private string GetNomPLn(short TipPln)
        {
            //Tipo de Operación.
            if (TipPln == 401)
            {
                return "ANTIC. COM RET Y LIQUIDADO MCF";
            }
            else if (TipPln == 402)
            {
                return "ANTICIPO DE COMPRADOR RETORNADO";
            }
            else if (TipPln == 403)
            {
                return "CRÉDITO INTERNO";
            }
            else if (TipPln == 407)
            {
                return "CRÉDITO EXTERNO";
            }
            else if (TipPln == 408)
            {
                //agrego la Planilla 408
                return "CRÉDITO EXTERNO RETORNADO";
            }
            else if (TipPln == 500)
            {
                return "DIVISAS RETORNADAS Y LIQUIDADAS";
            }
            else if (TipPln == 511)
            {
                return "DIVISAS RETORNADAS Y NO LIQUIDADAS";
            }
            else if (TipPln == 501)
            {
                return "DIVISAS RETORNADAS";
            }
            else if (TipPln == 502)
            {
                return "DIVISAS NO RETORNADAS 502";
            }
            else if (TipPln == 540)
            {
                return "RET. EMPRESAS";
            }
            else if (TipPln == 570)
            {
                return "RETORNOS POR DEDUCCIÓN";
            }
            else if (TipPln >= 600)
            {
                return "EX-FINANCIAMIENTO";
            }

            return "";
        }

        private static dynamic formatnum(string P_Numero, short P_Blanco, short P_Largo)
        {
            dynamic _retValue = null;
            string s = P_Numero;
            if (((VB6Helpers.Val(s) == 0 ? -1 : 0) & P_Blanco) != 0)
            {
                _retValue = "";
            }
            else
            {
                s = Format.FormatCurrency(Format.StringToDouble(s), "#,###,###,##0.00");
            }

            s = Derecha(s, P_Largo);
            return s;
        }

        private static string Derecha(string campo, short largo)
        {
            string s = VB6Helpers.Trim(campo);
            short Blancos = (short)(2 * (largo - VB6Helpers.Len(s)) + Puntos(s));
            short i = 0;
            if (VB6Helpers.Len(s) < largo)
            {
                for (i = 1; i <= (short)Blancos; i++)
                {
                    s = " " + s;
                }

            }
            return s;
        }

        //Retorna el Número de Puntos de un String.-
        private static short Puntos(string Numero_Str)
        {
            short Contador = 0;
            short i = 0;
            for (i = 1; i <= (short)VB6Helpers.Len(Numero_Str); i++)
            {
                if (VB6Helpers.Mid(Numero_Str, i, 1) == ".")
                {
                    Contador = (short)(Contador + 1);
                }
            }
            return Contador;
        }

        //recibe un string en formato Val() o un dato numerico y lo devuelve en
        //formato despliege windows rellenando con espacios al principio a modo
        //de dejar alineado.
        //Atencion:  Solo funciona si el font con que se despliega es Bold=false
        //
        private static string forma(dynamic Numero, string Mascara)
        {
            string _retValue = "";
            // UPGRADE_INFO (#0561): The 'V_STRING' symbol was defined without an explicit "As" clause.
            const short V_STRING = 8;
            decimal tt = 0;
            // UPGRADE_WARNING (#0354): Unable to read default member of symbol 'Numero'. Consider using the GetDefaultMember6 helper method.
            dynamic ElNum = Numero;
            short dd = 0;
            string ente = "";
            string Deci = "";
            string Temp = "";
            short Lt = 0;
            short lm = 0;
            short EsMenor = (short)(false ? -1 : 0);
            short i = 0;
            short cta = 0;
            //convertir al formato windows
            if ((int)VB6Helpers.VarType(ElNum) == V_STRING)
            {
                if (VB6Helpers.Val(ElNum) < 0)
                {

                    EsMenor = (short)(true ? -1 : 0);
                }

                dd = (short)VB6Helpers.Instr(1, VB6Helpers.CStr(ElNum), ".");
                if (dd != 0)
                {
                    ente = VB6Helpers.Left(VB6Helpers.CStr(ElNum), dd - 1);
                    Deci = "0." + VB6Helpers.Right(VB6Helpers.CStr(ElNum), VB6Helpers.Len(VB6Helpers.CStr(ElNum)) - dd);
                }
                else
                {
                    ente = VB6Helpers.Left(VB6Helpers.CStr(ElNum), VB6Helpers.Len(VB6Helpers.CStr(ElNum)));
                    Deci = "0";
                }

                tt = (decimal)(VB6Helpers.Abs(VB6Helpers.Val(ente)) + VB6Helpers.Val(Deci));
                if (EsMenor != 0)
                {
                    tt = tt * -1;
                }

                Temp = VB6Helpers.Format(VB6Helpers.CStr(tt), Mascara);
            }
            else //si es nro
            {
                double? aux = ElNum as double?;
                if (aux.HasValue)
                    Temp = aux.Value.ToString(Mascara);
            }

            Lt = (short)VB6Helpers.Len(Temp);
            lm = (short)VB6Helpers.Len(Mascara);

            if (Lt > lm)
            {
                return Temp;
                //listo
            }
            else
            {
                _retValue = VB6Helpers.Format(Temp, VB6Helpers.String(lm, "@"));
            }

            //debemos rellenar con espacios los caracteres a la izquierda
            //contemos caracteres y separadores
            for (i = 1; i <= (short)(lm - Lt); i++)
            {
                if (VB6Helpers.Mid(Mascara, i, 1) == "," || VB6Helpers.Mid(Mascara, i, 1) == ".")
                {
                    cta = (short)(cta + 1);
                }
                else
                {
                    cta = (short)(cta + 2);
                }

            }

            return VB6Helpers.Space(cta) + Temp;
        }

        private static string PrintMonto(double mto)
        {
            string SS = VB6Helpers.CStr(formatnum(mto.ToString(), 0, 15));
            return SS;
        }

        private static string Escribe_Nombre(ref string Nombre)
        {
            short car = 0;
            short i = 0;
            string nnombre = "";
            Nombre = VB6Helpers.LCase(VB6Helpers.Trim(Nombre));
            car = (short)VB6Helpers.Len(Nombre);
            if (car > 0)
            {
                nnombre = VB6Helpers.UCase(VB6Helpers.Mid(Nombre, 1, 1));
                for (i = 2; i <= (short)car; i++)
                {
                    if (VB6Helpers.Mid(Nombre, i, 1) == " " && VB6Helpers.Mid(Nombre, i + 1, 1) != " ")
                    {
                        // UPGRADE_INFO (#0571): String concatenation inside a loop. Consider declaring the 'nnombre' variable as a StringBuilder6 object.
                        nnombre = nnombre + " " + VB6Helpers.UCase(VB6Helpers.Mid(Nombre, i + 1, 1));
                        i = (short)(i + 1);
                    }
                    else
                    {
                        if (VB6Helpers.Mid(Nombre, i, 1) == "." && VB6Helpers.Mid(Nombre, i + 1, 1) != ".")
                        {
                            nnombre = nnombre + "." + VB6Helpers.UCase(VB6Helpers.Mid(Nombre, i + 1, 1));
                            i = (short)(i + 1);
                        }
                        else
                        {
                            if (VB6Helpers.Mid(Nombre, i, 1) != " ")
                            {
                                nnombre += VB6Helpers.Mid(Nombre, i, 1);
                            }

                        }

                    }

                }

                return nnombre;
            }

            return "";
        }

        private static string Rut_Formateado(string xrut)
        {
            // Función que formatea un rut (xrut) para desplegarlo de la forma ###.###.###-A

            string xrut1 = "";
            string z = VB6Helpers.Mid(xrut, 1, 3);
            if (VB6Helpers.Mid(z, 1, 3) == "000")
            {
                xrut1 = xrut1;
            }
            else
            {
                if (VB6Helpers.Mid(z, 1, 2) == "00")
                {
                    xrut1 += VB6Helpers.Mid(z, 3, 1);
                }
                else
                {
                    if (VB6Helpers.Mid(z, 1, 1) == "0")
                    {
                        xrut1 += VB6Helpers.Mid(z, 2, 2);
                    }

                }

            }

            xrut1 = xrut1 + "." + VB6Helpers.Mid(xrut, 4, 3) + "." + VB6Helpers.Mid(xrut, 7, 3) + "-" + VB6Helpers.Mid(xrut, 10, 1);
            return xrut1;
        }

        internal sce_usr_s04_MS_Result GetFechasUsuario(string cencos, string codusr)
        {
            var retorno = uow.UsuarioRepository.GetFechasUsuario(cencos, codusr);

            return retorno;
        }
    }
}
