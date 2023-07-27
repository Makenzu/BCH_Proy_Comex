using System;

namespace BCH.Comex.UI.Web.Common
{
    public static class SessionKeys
    {
        public static class FundTransfer
        {
            public const string InitializationObjectKey = "FundTransfer.InitializationObject";
            public const string CargosYAbonosAInyectarKey = "FundTransfer.CargosYAbonosAInyectar";
            public const string CargosYAbonosAReversarKey = "FundTransfer.CargosYAbonosAReversar";
            public const string DescripcionFuncionesContablesKey = "FundTransfer.DescripcionFuncionesContables";
            public const string ImpresionDocumentosReporteContableKey = "FundTransfer.ImpresionDocumentos.ReporteContable";
            public const string ImpresionDocumentosDetalleUltimoSwiftKey = "FundTransfer.ImpresionDocumentos.DetalleUltimoSwift";
            public const string ImpresionDocumentosNroMensajeUltimoSwiftKey = "FundTransfer.ImpresionDocumentos.NroMensajeUltimoSwift";
            public const string UltimaOperacionEsCosmosKey = "FundTransfer.UltimaOperacionEsCosmos";
        }

        public static class AdminParty
        {
            public const String PrtyKey = "AdminParty.Prty";
            public const String UserPrtyKey = "AdminParty.UserPrty";
            public const string PartySessionKey = "AdminParty.Party";
            public const string OficinasSessionKey = "AdminParty.Oficinas";
            public const string PaisSessionKey = "AdminParty.Pais";
            public const string LocalidadSessionKey = "AdminParty.Localidad";
            public const string PrtGlobSessionKey = "AdminParty.PrtGlob";
            public const string MonedasSessionKey = "AdminParty.Monedas";
            public const string AbrevSessionKey = "AdminParty.Abrev";
            public const string RiesgoSessionKey = "AdminParty.Riesgo";
        }
        public static class Supervisor
        {
            public const string DatosGlobalesKey = "Supervisor.DatosGlobales";
            public const string UltimosChequesBuscadosKey = "Supervisor.UltimosChequesBuscados";
        }

        public static class InicioDia
        {
            public const string DatosUsuario = "InicioDia.DatosUsuario";
        }

        public static class Common
        {
            public const string DatosUsuario = "Common.DatosUsuario";
            public const string ConfigAlertas = "Common.ConfigAlertas";
            public const string Alertas = "Common.Alertas";
            public const string FinDiaEjecutado = "Common.FinDiaEjecutado";
        }
        public static class ContabilidadGenerica
        {
            public const string DatosGlobalesKey = "ContabilidadGenerica.DatosGlobales";
        }

        public static class Devengo
        {
            public const string DatosGlobalesKey = "Devengo.DatosGlobales";
        }

        public static class FinDia
        {
            public const string DatosGlobalesKey = "FinDia.DatosGlobales";
        }

        public static class ConsultaSwift
        {
            public const string InitializationBotonesKey = "ConsultaSwift.InitializationBotones";
        }

        public static class Impresion
        {
            public const string DescripcionFuncionesContablesKey = "Impresion.DescripcionFuncionesContables";
            public const string ImpresionDocumentosReporteContableKey = "Impresion.ImpresionDocumentos.ReporteContable";
            public const string ImpresionDocumentosDetalleUltimoSwiftKey = "Impresion.ImpresionDocumentos.DetalleUltimoSwift";
            public const string ImpresionDocumentosNroMensajeUltimoSwiftKey = "Impresion.ImpresionDocumentos.NroMensajeUltimoSwift";
        }

         public static class ControlIntegral
         {
             public const string InitializationObjectKey = "ControlIntegral.InitializationObject";

         }

         public static class GestionControlSwift
         {
             public const string MensajesNoRecepcionadosKey = "GestionControlSwift.MensajesNoRecepcionados";
         }

        public static class AdminSwift
        {
            public const string MensajesGrillaKey = "AdminSwift.MensajesGrilla";
            public const string TipoGrillaKey = "AdminSwift.TipoGrilla";
            public const string IDCasillaKey = "AdminSwift.IDCasilla";
            public const string FecIniKey = "AdminSwift.FecIni";
            public const string FecFinKey = "AdminSwift.FecFin";
        }

        public static class GeneracionMT300
        {
            public const string DatosGlobalesKey = "GeneracionMT300.DatosGlobales";
        }

        public static class MT300Gestion
        {
            public const string DatosGlobalesKey = "MT300Gestion.DatosGlobales";
        }

        public static class MT300Planilla
        {
            public const string DatosGlobalesKey = "MT300Planilla.DatosGlobales";
        }

        public static class MT300Consulta
        {
            public const string DatosGlobalesKey = "MT300Consulta.DatosGlobales";
        }

    }
}
