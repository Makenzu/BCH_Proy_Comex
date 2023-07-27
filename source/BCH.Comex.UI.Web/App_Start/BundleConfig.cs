using System.Collections.Generic;
using System.Web.Optimization;

namespace BCH.Comex.UI.Web
{
    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/messages_es.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/jquery.inputmask/jquery.inputmask.js",
                        "~/Scripts/jquery.inputmask/jquery.inputmask.date.extensions.js",
                        "~/Scripts/jquery.inputmask/jquery.inputmask.numeric.extensions.js"
                        ));

            var bundleGlobalization = new ScriptBundle("~/bundles/globalization").Include(
                "~/Scripts/globalize/globalize.js",
                "~/Scripts/globalize/cultures/globalize.culture.es-CL.js",
                "~/Scripts/jquery.validate.globalize.js",
                "~/Scripts/init-culture-es.js");
            bundleGlobalization.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(bundleGlobalization);


            bundles.Add(new ScriptBundle("~/bundles/utils").Include(
                        "~/Scripts/numeral.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/moment-with-locales.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Scripts/numeral/languages/es-ES.js",
                        "~/Scripts/Commons.js",
                        "~/Scripts/bootbox/bootbox.min.js",
                        "~/Scripts/string.min.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datetimepicker").Include(
                        "~/Scripts/moment.min.js",
                        "~/Scripts/moment-with-locales.min.js",
                      "~/Scripts/bootstrap-datetimepicker.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table").Include(
                      "~/Scripts/bootstrap-table.js",
                      "~/Scripts/bootstrap-table-es-SP.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap-table-export").Include(
                      "~/Scripts/bootstrap-table/export/libs/FileSaver/FileSaver.min.js",
                      "~/Scripts/bootstrap-table/export/libs/jsPDF/jspdf.min.js",
                      "~/Scripts/bootstrap-table/export/libs/jsPDF-AutoTable/jspdf.plugin.autotable.js",
                      "~/Scripts/bootstrap-table/export/libs/html2canvas/html2canvas.min.js",
                      "~/Scripts/bootstrap-table/export/libs/tableExport.min.js",
                      "~/Scripts/bootstrap-table/export/bootstrap-table-export.js"
                ));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                      "~/Content/css/bootstrap-compiled.css",
                      "~/Content/css/bootstrap-theme.css",
                      "~/Content/css/bootstrap-table.min.css",
                      "~/Content/css/bootstrap-datetimepicker.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/bch-icons.css",
                      "~/Content/css/bootstrap-datetimepicker.css",
                      "~/Content/css/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/JQueryfileDownload").Include(
                "~/Scripts/jquery.fileDownload.js"
                ));

            #region FundTransfer
            /// Actualiza JS FundTransfer.Layout
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/Layout").Include(
                "~/Scripts/FundTransfer/FundTransfer.Layout.js"));

            /// Actualiza JS FundTransfer.Index
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/Index").Include(
                "~/Scripts/FundTransfer/FundTransfer.Index.js"));

            /// Actualiza JS FundTransfer.AnulacionOperacion
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/AnulacionOperacion").Include(
                "~/Scripts/FundTransfer/FundTransfer.AnulacionOperacion.js"));

            /// Actualiza JS FundTransfer.AnulacionPlanillaVisibleImport
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/AnulacionPlanillaVisibleImport").Include(
                "~/Scripts/FundTransfer/FundTransfer.AnulacionPlanillaVisibleImport.js"));

            /// Actualiza JS FundTransfer.Arbitrajes
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/Arbitrajes").Include(
                "~/Scripts/FundTransfer/FundTransfer.Arbitrajes.js"));

            /// Actualiza JS FundTransfer.CargarOperaciones
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/CargarOperaciones").Include(
                "~/Scripts/FundTransfer/FundTransfer.CargarOperaciones.js"));

            /// Actualiza JS FundTransfer.ComercioInvisible
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/ComercioInvisible").Include(
                "~/Scripts/FundTransfer/FundTransfer.ComercioInvisible.js"));

            /// Actualiza JS FundTransfer.ComercioVisibleExport
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/ComercioVisibleExport").Include(
                "~/Scripts/FundTransfer/FundTransfer.ComercioVisibleExport.js"));

            /// Actualiza JS FundTransfer.Comisiones
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/Comisiones").Include(
                "~/Scripts/FundTransfer/FundTransfer.Comisiones.js"));

            /// Actualiza JS FundTransfer.ConsultaParticipantes
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/ConsultaParticipantes").Include(
                "~/Scripts/FundTransfer/FundTransfer.ConsultaParticipantes.js"));

            /// Actualiza JS FundTransfer.DestinoFondos
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/DestinoFondos").Include(
                "~/Scripts/FundTransfer/FundTransfer.DestinoFondos.js"));

            /// Actualiza JS FundTransfer.EmitirCheque
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/EmitirCheque").Include(
                "~/Scripts/FundTransfer/FundTransfer.EmitirCheque.js"));

            /// Actualiza JS FundTransfer.EmitirNotaCredito
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/EmitirNotaCredito").Include(
                "~/Scripts/FundTransfer/FundTransfer.EmitirNotaCredito.js"));

            /// Actualiza JS FundTransfer.FacturasAsociadas
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/FacturasAsociadas").Include(
                "~/Scripts/FundTransfer/FundTransfer.FacturasAsociadas.js"));

            /// Actualiza JS FundTransfer.Frm_Cta
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/Frm_Cta").Include(
                "~/Scripts/FundTransfer/FundTransfer.Frm_Cta.js"));

            /// Actualiza JS FundTransfer.GenerarSwift
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/GenerarSwift").Include(
                "~/Scripts/FundTransfer/FundTransfer.GenerarSwift.js"));

            /// Actualiza JS FundTransfer.ImpresionDeDocumentos
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/ImpresionDeDocumentos").Include(
                "~/Scripts/FundTransfer/FundTransfer.ImpresionDeDocumentos.js"));

            /// Actualiza JS FundTransfer.ImprimirGrabar
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/ImprimirGrabar").Include(
                "~/Scripts/FundTransfer/FundTransfer.ImprimirGrabar.js"));

            /// Actualiza JS FundTransfer.IngresoValores
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/IngresoValores").Include(
                "~/Scripts/FundTransfer/FundTransfer.IngresoValores.js"));

            /// Actualiza JS FundTransfer.NuevoDestino
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/NuevoDestino").Include(
                "~/Scripts/FundTransfer/FundTransfer.NuevoDestino.js"));

            /// Actualiza JS FundTransfer.NuevoOrigen
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/NuevoOrigen").Include(
                "~/Scripts/FundTransfer/FundTransfer.NuevoOrigen.js"));

            /// Actualiza JS FundTransfer.OrigenFondos
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/OrigenFondos").Include(
                "~/Scripts/FundTransfer/FundTransfer.OrigenFondos.js"));

            /// Actualiza JS FundTransfer.Participantes
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/Participantes").Include(
                "~/Scripts/FundTransfer/FundTransfer.Participantes.js"));

            /// Actualiza JS FundTransfer.ParticipantesCrear
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/ParticipantesCrear").Include(
                "~/Scripts/FundTransfer/FundTransfer.ParticipantesCrear.js"));

            /// Actualiza JS FundTransfer.PlanillaAnulacion
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/PlanillaAnulacion").Include(
                "~/Scripts/FundTransfer/FundTransfer.PlanillaAnulacion.js"));

            /// Actualiza JS FundTransfer.PlanillaIngresoVisibleExportacion
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/PlanillaIngresoVisibleExportacion").Include(
                "~/Scripts/FundTransfer/FundTransfer.PlanillaIngresoVisibleExportacion.js"));

            /// Actualiza JS FundTransfer.PlanillaInvisibleEditar
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/PlanillaInvisibleEditar").Include(
                "~/Scripts/FundTransfer/FundTransfer.PlanillaInvisibleEditar.js"));

            /// Actualiza JS FundTransfer.PlanillaTransferencia
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/PlanillaTransferencia").Include(
                "~/Scripts/FundTransfer/FundTransfer.PlanillaTransferencia.js"));

            /// Actualiza JS FundTransfer.PlanillaVisibleExport
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/PlanillaVisibleExport").Include(
                "~/Scripts/FundTransfer/FundTransfer.PlanillaVisibleExport.js"));

            /// Actualiza JS FundTransfer.PlanillaVisibleImport
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/PlanillaVisibleImport").Include(
                "~/Scripts/FundTransfer/FundTransfer.PlanillaVisibleImport.js"));

            /// Actualiza JS FundTransfer.RelacionarOperacion
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/RelacionarOperacion").Include(
                "~/Scripts/FundTransfer/FundTransfer.RelacionarOperacion.js"));

            /// Actualiza JS FundTransfer.ReversaOperacion
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/ReversaOperacion").Include(
                "~/Scripts/FundTransfer/FundTransfer.ReversaOperacion.js"));

            /// Actualiza JS FundTransfer.SeleccionOficina
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/SeleccionOficina").Include(
                "~/Scripts/FundTransfer/FundTransfer.SeleccionOficina.js"));

            /// Actualiza JS FundTransfer.Ticket
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/Ticket").Include(
                "~/Scripts/FundTransfer/FundTransfer.Ticket.js"));

            /// Actualiza JS FundTransfer.NuevoDestino
            bundles.Add(new ScriptBundle("~/bundles/FundTransfer/NuevoDestino").Include(
                "~/Scripts/FundTransfer/NuevoDestino.js"));

            #endregion

            #region Admin Participantes

            /// Actualiza JS AdminParticipantes.Layout
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/Layout").Include(
                "~/Scripts/AdminParticipantes/AdminParticipantes.Layout.js"));

            /// Actualiza JS AdminParticipantes.CaracteristicasParticipante
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/CaracteristicasParticipante").Include(
                "~/Scripts/AdminParticipantes/AdminParticipantes.CaracteristicasParticipante.js"));

            /// Actualiza JS AdminParticipantes.CuentasParticipante
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/CuentasParticipante").Include(
                "~/Scripts/AdminParticipantes/AdminParticipantes.CuentasParticipante.js"));

            /// Actualiza JS AdminParticipantes.DatosBancoParticipante
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/DatosBancoParticipante").Include(
                "~/Scripts/AdminParticipantes/AdminParticipantes.DatosBancoParticipante.js"));

            /// Actualiza JS AdminParticipantes.InstruccionesParticipante
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/InstruccionesParticipante").Include(
                "~/Scripts/AdminParticipantes/AdminParticipantes.InstruccionesParticipante.js"));

            /// Actualiza JS AdminParticipantes.TasasEspeciales
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/TasasEspeciales").Include(
                "~/Scripts/AdminParticipantes/AdminParticipantes.TasasEspeciales.js"));

            /// Actualiza JS AdminParticipantes.custom
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/custom").Include(
                "~/Scripts/AdminParticipantes/app.custom.js"));

            /// Actualiza JS AdminParticipantes.abrir
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/abrir").Include(
                "~/Scripts/AdminParticipantes/admin-prty.abrir.js"));

            /// Actualiza JS AdminParticipantes.base
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/base").Include(
                "~/Scripts/AdminParticipantes/admin-prty.base.js"));

            /// Actualiza JS AdminParticipantes.del
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/del").Include(
                "~/Scripts/AdminParticipantes/admin-prty.del.js"));

            /// Actualiza JS AdminParticipantes.main
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/main").Include(
                "~/Scripts/AdminParticipantes/admin-prty.main.js"));

            /// Actualiza JS AdminParticipantes.new.dir
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/new/dir").Include(
                "~/Scripts/AdminParticipantes/admin-prty.new.dir.js"));

            /// Actualiza JS AdminParticipantes.new
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/new").Include(
                "~/Scripts/AdminParticipantes/admin-prty.new.js"));

            /// Actualiza JS AdminParticipantes.new.rs
            bundles.Add(new ScriptBundle("~/bundles/AdminParticipantes/new/rs").Include(
                "~/Scripts/AdminParticipantes/admin-prty.new.rs.js"));

            #endregion

            #region AutorizacionSwift

            /// Actualiza JS AutorizacionSwift
            bundles.Add(new ScriptBundle("~/bundles/AutorizacionSwift").Include(
                "~/Scripts/AutorizacionSwift/configuraralertas.js"));

            #endregion

            #region BancaMundial

            /// Actualiza JS BancaMundial
            bundles.Add(new ScriptBundle("~/bundles/BancaMundial").Include(
                "~/Scripts/BancaMundial/index.js"));

            #endregion

            #region ConsultaSwift

            /// Actualiza JS ConsultaSwift
            bundles.Add(new ScriptBundle("~/bundles/ConsultaSwift").Include(
                "~/Scripts/ConsultaSwift/index.js"));

            #endregion

            #region ControlIntegral

            /// Actualiza JS ControlIntegral.custom
            bundles.Add(new ScriptBundle("~/bundles/ControlIntegral/custom").Include(
                "~/Scripts/ControlIntegral/app.custom.js"));

            /// Actualiza JS ControlIntegral.frmMift
            bundles.Add(new ScriptBundle("~/bundles/ControlIntegral/frmMift").Include(
                "~/Scripts/ControlIntegral/ControlIntegral.frmMift.js"));
            
            #endregion

            #region GestionControl

            /// Actualiza JS GestionControl
            bundles.Add(new ScriptBundle("~/bundles/GestionControl").Include(
                "~/Scripts/GestionControl/index.js"));

            #endregion

            #region BusquedaPlanilla

            /// Actualiza JS BusquedaPlanilla
            bundles.Add(new ScriptBundle("~/bundles/BusquedaPlanilla").Include(
                "~/Areas/BusquedaPlanilla/Scripts/BusquedaPlanilla.Inicio.js"));

            #endregion

            #region ContabilidadGenerica

            /// Actualiza JS ContabilidadGenerica.Aceptar_Monto
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/AceptaMonto").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.Aceptar_Monto.js"));

            /// Actualiza JS ContabilidadGenerica.Adicionales
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/Adicionales").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.Adicionales.js"));

            /// Actualiza JS ContabilidadGenerica.AnularContabilidad
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/AnularContabilidad").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.AnularContabilidad.js"));

            /// Actualiza JS ContabilidadGenerica.Cheques.Emision
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/ChequesEmision").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.Cheques.Emision.js"));

            /// Actualiza JS ContabilidadGenerica.EmitirNotaCredito
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/EmitirNotaCredito").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.EmitirNotaCredito.js"));

            /// Actualiza JS ContabilidadGenerica.FrmCta
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/FrmCta").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.FrmCta.js"));

            /// Actualiza JS ContabilidadGenerica.Grabar.Imprimir
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/GrabarImprimir").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.Grabar.Imprimir.js"));

            /// Actualiza JS ContabilidadGenerica.GuardarCambiosOperacion
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/GuardarCambiosOperacion").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.GuardarCambiosOperacion.js"));

            /// Actualiza JS ContabilidadGenerica
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.Index.js"));

            /// Actualiza JS ContabilidadGenerica.Layout
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/Layout").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.Layout.js"));

            /// Actualiza JS ContabilidadGenerica.Participante.Consultar
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/ParticipanteConsultar").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.Participante.Consultar.js"));

            /// Actualiza JS ContabilidadGenerica.Participante.Crear
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/ParticipanteCrear").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.Participante.Crear.js"));

            /// Actualiza JS ContabilidadGenerica.Participante.Identificar
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/ParticipanteIdentificar").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.Participante.Identificar.js"));

            /// Actualiza JS ContabilidadGenerica.Participante.Preguntas
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/ParticipantePreguntar").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.Participantes.Preguntar.js"));

            /// Actualiza JS ContabilidadGenerica.RelacionarOperacion
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/RelacionarOperacion").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.RelacionarOperacion.js"));

            /// Actualiza JS ContabilidadGenerica.Ticket
            bundles.Add(new ScriptBundle("~/bundles/ContabilidadGenerica/Ticket").Include(
                "~/Areas/ContabilidadGenerica/Scripts/ContabilidadGenerica.Ticket.js"));

            #endregion

            #region Devengo

            /// Actualiza JS Devengo
            bundles.Add(new ScriptBundle("~/bundles/Devengo").Include(
                "~/Areas/Devengo/Scripts/Devengo.Index.js"));

            /// Actualiza JS Devengo.Common
            bundles.Add(new ScriptBundle("~/bundles/Devengo/Common").Include(
                "~/Areas/Devengo/Scripts/Devengo.Common.js"));

            /// Actualiza JS Devengo.ConsultaCuentasContables
            bundles.Add(new ScriptBundle("~/bundles/Devengo/ConsultaCuentasContables").Include(
                "~/Areas/Devengo/Scripts/Devengo.ConsultaCuentasContables.js"));

            /// Actualiza JS Devengo.ConsultaDevengamiento
            bundles.Add(new ScriptBundle("~/bundles/Devengo/ConsultaDevengamiento").Include(
                "~/Areas/Devengo/Scripts/Devengo.ConsultaDevengamiento.js"));

            /// Actualiza JS Devengo.ConsultaInterfazCDR
            bundles.Add(new ScriptBundle("~/bundles/Devengo/ConsultaInterfazCDR").Include(
                "~/Areas/Devengo/Scripts/Devengo.ConsultaInterfazCDR.js"));

            /// Actualiza JS Devengo.DevengamientoHistorico
            bundles.Add(new ScriptBundle("~/bundles/Devengo/DevengamientoHistorico").Include(
                "~/Areas/Devengo/Scripts/Devengo.DevengamientoHistorico.js"));

            #endregion

            #region EnvioSwift

            /// Actualiza JS EnvioSwift.configuraralertas
            bundles.Add(new ScriptBundle("~/bundles/EnvioSwift/configuraralertas").Include(
                "~/Areas/EnvioSwift/Scripts/configuraralertas.js"));

            /// Actualiza JS EnvioSwift.EditorMensaje
            bundles.Add(new ScriptBundle("~/bundles/EnvioSwift/EditorMensaje").Include(
                "~/Areas/EnvioSwift/Scripts/EditorMensaje.js"));

            /// Actualiza JS EnvioSwift.listado
            bundles.Add(new ScriptBundle("~/bundles/EnvioSwift/listado").Include(
                "~/Areas/EnvioSwift/Scripts/listado.js"));

            /// Actualiza JS EnvioSwift.ListaPendientes
            bundles.Add(new ScriptBundle("~/bundles/EnvioSwift/ListaPendientes").Include(
                "~/Areas/EnvioSwift/Scripts/ListaPendientes.js"));

            #endregion

            #region Fin de Día

            /// Actualiza JS FinDia.Aceptacion
            bundles.Add(new ScriptBundle("~/bundles/FinDia/Aceptacion").Include(
                "~/Areas/FinDia/Scripts/FinDia.Aceptacion.js"));

            /// Actualiza JS FinDia.Inicio
            bundles.Add(new ScriptBundle("~/bundles/FinDia/Inicio").Include(
                "~/Areas/FinDia/Scripts/FinDia.Inicio.js"));

            #endregion

            #region Impresion

            /// Actualiza JS Impresion
            bundles.Add(new ScriptBundle("~/bundles/Impresion").Include(
                "~/Areas/Impresion/Scripts/Impresion.ImpresionDeDocumentos.js"));

            #endregion

            #region InicioDia

            /// Actualiza JS InicioDia
            bundles.Add(new ScriptBundle("~/bundles/InicioDia").Include(
                "~/Areas/InicioDia/Scripts/Index.js"));

            /// Actualiza JS InicioDia
            bundles.Add(new ScriptBundle("~/bundles/InicioDia/Novedades").Include(
                "~/Areas/InicioDia/Scripts/Novedades.js"));

            #endregion

            ///// Actualiza JS Planillas
            //bundles.Add(new ScriptBundle("~/bundles/Planillas").IncludeDirectory(
            //    "~/Areas/Planillas/Scripts/", "*.js"));

            #region Supervisor

            /// Actualiza JS Supervisor.CambioClave
            bundles.Add(new ScriptBundle("~/bundles/Supervisor/CambioClave").Include(
                "~/Areas/Supervisor/Scripts/Supervisor.CambioClave.js"));

            /// Actualiza JS Supervisor.CederCartera
            bundles.Add(new ScriptBundle("~/bundles/Supervisor/CederCartera").Include(
                "~/Areas/Supervisor/Scripts/Supervisor.CederCartera.js"));

            /// Actualiza JS Supervisor.DocumentosValorados
            bundles.Add(new ScriptBundle("~/bundles/Supervisor/DocumentosValorados").Include(
                "~/Areas/Supervisor/Scripts/Supervisor.DocumentosValorados.js"));

            /// Actualiza JS Supervisor.Especialistas
            bundles.Add(new ScriptBundle("~/bundles/Supervisor/Especialistas").Include(
                "~/Areas/Supervisor/Scripts/Supervisor.Especialistas.js"));

            /// Actualiza JS Supervisor.InyectarYReversar
            bundles.Add(new ScriptBundle("~/bundles/Supervisor/InyectarYReversar").Include(
                "~/Areas/Supervisor/Scripts/Supervisor.InyectarYReversar.js"));

            /// Actualiza JS Supervisor.Productividad
            bundles.Add(new ScriptBundle("~/bundles/Supervisor/Productividad").Include(
                "~/Areas/Supervisor/Scripts/Supervisor.Productividad.js"));

            /// Actualiza JS Supervisor.RelacionesIdiDec
            bundles.Add(new ScriptBundle("~/bundles/Supervisor/RelacionesIdiDec").Include(
                "~/Areas/Supervisor/Scripts/Supervisor.RelacionesIdiDec.js"));

            #endregion

            #region ReportarOperacion
            /// Actualiza JS ReportarOperacion.Index
            bundles.Add(new ScriptBundle("~/bundles/ReportarOperacion/Index").Include(
                "~/Scripts/ReportarOperacion/ReportarOperacion.Index.js"));
            #endregion

            #region GeneracionMT300
            bundles.Add(new ScriptBundle("~/bundles/GeneracionMT300/Index").Include(
                "~/Areas/GeneracionMT300/Scripts/GeneracionMT300.Index.js"));
            #endregion

            #region MT300Gestion
            bundles.Add(new ScriptBundle("~/bundles/MT300Gestion/Index").Include(
                "~/Areas/MT300Gestion/Scripts/MT300Gestion.Index.js"));
            bundles.Add(new ScriptBundle("~/bundles/MT300Gestion/Detalle").Include(
                "~/Areas/MT300Gestion/Scripts/MT300Gestion.Detalle.js"));
            #endregion

            #region MT300Planilla
            bundles.Add(new ScriptBundle("~/bundles/MT300Planilla/Index").Include(
                "~/Areas/MT300Planilla/Scripts/MT300Planilla.Index.js"));
            bundles.Add(new ScriptBundle("~/bundles/MT300Planilla/Detalle").Include(
                "~/Areas/MT300Planilla/Scripts/MT300Planilla.Detalle.js"));
            bundles.Add(new ScriptBundle("~/bundles/MT300Planilla/Mensaje").Include(
                "~/Areas/MT300Planilla/Scripts/MT300Planilla.Mensaje.js"));
            #endregion


#if DEBUG
            BundleTable.EnableOptimizations = false;
            #else
                BundleTable.EnableOptimizations = true;
            #endif
        }
    }
}
