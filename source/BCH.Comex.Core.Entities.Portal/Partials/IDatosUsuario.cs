using System.Collections.Generic;

namespace BCH.Comex.Core.Entities.Portal
{
    public interface IDatosUsuario
    {
        string BCHComexSwem_Casillas { get; set; }
        string CodBCCH { get; }
        string CodBCH { get; }
        string CodPBC { get; }
        string ConfigImpres_ContabilidadGenerica { get; set; }
        string ConfigImpres_Formato { get; set; }
        string ConfigImpres_ImprimeCartas { get; set; }
        string ConfigImpres_ImprimePlanillas { get; set; }
        string ConfigImpres_ImprimeReporte { get; set; }
        PrintFormat ConfigImpres_PrintFormat { get; set; }
        string Configuracion_Sonidos { get; set; }
        string CtaCteLin_ArcHCCL { get; set; }
        string CtaCteLin_ArcLCCL { get; set; }
        string CtaCteLin_ServCCL { get; set; }
        string CtaCteLin_ServSOL { get; set; }
        string CtaCteLin_VistSOL { get; set; }
        string Entry_Usuario { get; set; }
        string Exportaciones_DocSwf { get; set; }
        string Exportaciones_TcpConDec { get; set; }
        string Exportaciones_TcpConvenio { get; set; }
        string Exportaciones_TcpSinPai { get; set; }
        string EXPOTAR_arch_export { get; set; }
        string EXPOTAR_dir_arch_export { get; set; }
        string EXPOTAR_ruta_excel { get; set; }
        string FirmasLocales { get; set; }
        string General_MndDol { get; set; }
        string General_MndNac { get; set; }
        string General_MndSinDec { get; set; }
        string General_MontoIVA { get; set; }
        string Identificacion_Alias { get; set; }
        string Identificacion_CCtUsr { get; set; }
        string Identificacion_CCtUsro { get; set; }
        string Identificacion_CentroDeCostosImpersonado { get; }
        string Identificacion_CentroDeCostosOriginal { get; }
        string Identificacion_IdEspecialistaImpersonado { get; }
        string Identificacion_IdEspecialistaOriginal { get; }
        string Identificacion_Impresora { get; set; }
        string Identificacion_Rut { get; set; }
        string Importaciones_TcpAutBcoCen { get; set; }
        short? MinsAlertaAdminEnvioSwift { get; set; }
        short? MinsAlertaAutorizacionSwift { get; set; }
        short? MinsAlertaEnvioSwift { get; set; }
        short? MinsAlertaRecepcionSwift { get; set; }
        string MODGUSR_UsrEsp_CentroCosto_CodBCCH { get; set; }
        string MODGUSR_UsrEsp_CentroCosto_CodBCH { get; set; }
        string MODGUSR_UsrEsp_CentroCosto_CodPBC { get; set; }
        string MODGUSR_UsrEsp_CentroCosto_SucBCH { get; set; }
        string Monedas_CodMonedaDolar { get; set; }
        string Monedas_CodMonedaNacional { get; set; }
        string Oficinas_UsrEsp_CentroCosto { get; set; }
        string Pais_CodPais { get; set; }
        string Participantes_PartyEnRed { get; set; }
        string Participantes_PartyNodo { get; set; }
        string Participantes_PartyServidor { get; set; }
        int RutEnFormatoBDSwift { get; }
        string SaldosCtaCte_NodoSalME { get; set; }
        string SaldosCtaCte_SerSalCCL { get; set; }
        string SaldosCtaCte_VisSalME { get; set; }
        string SaldosCtaCte_VisSalMN { get; set; }
        string samAccountName { get; set; }
        string SceIdi_PlazaCentral { get; set; }
        string SgtCCLin_NodoSgt { get; set; }
        string SgtCCLin_ServSgt { get; set; }
        string SgtCCLin_TabSgt { get; set; }
        string SgtCCLin_VisSgt { get; set; }
        string SucBCH { get; }
        string Swift103_BICEMI { get; set; }
        string Swift103_BICREC { get; set; }
        string Swift_23E_Reglas { get; set; }
        string SyBase_Base { get; set; }
        string SyBase_Nodo { get; set; }
        string SyBase_Servidor { get; set; }
        string SyBase_Usuario { get; set; }
        IEnumerable<ICodigosSucursal> codigos_sucursal { get; }
        string Ubicacion_Entry { get; set; }
        string WebServices_IP { get; set; }
    }
}