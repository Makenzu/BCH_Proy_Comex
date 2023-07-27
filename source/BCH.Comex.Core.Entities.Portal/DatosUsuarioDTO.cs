using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.Entities.Portal
{
    public class DatosUsuarioDTO : IDatosUsuario
    {
        public string BCHComexSwem_Casillas { get; set; }
        public string ConfigImpres_ContabilidadGenerica { get; set; }
        public string ConfigImpres_Formato { get; set; }
        public string ConfigImpres_ImprimeCartas { get; set; }
        public string ConfigImpres_ImprimePlanillas { get; set; }
        public string ConfigImpres_ImprimeReporte { get; set; }
        public string Configuracion_Sonidos { get; set; }
        public string CtaCteLin_ArcHCCL { get; set; }
        public string CtaCteLin_ArcLCCL { get; set; }
        public string CtaCteLin_ServCCL { get; set; }
        public string CtaCteLin_ServSOL { get; set; }
        public string CtaCteLin_VistSOL { get; set; }
        public string Entry_Usuario { get; set; }
        public string Exportaciones_DocSwf { get; set; }
        public string Exportaciones_TcpConDec { get; set; }
        public string Exportaciones_TcpConvenio { get; set; }
        public string Exportaciones_TcpSinPai { get; set; }
        public string EXPOTAR_arch_export { get; set; }
        public string EXPOTAR_dir_arch_export { get; set; }
        public string EXPOTAR_ruta_excel { get; set; }
        public string FirmasLocales { get; set; }
        public string General_MndDol { get; set; }
        public string General_MndNac { get; set; }
        public string General_MndSinDec { get; set; }
        public string General_MontoIVA { get; set; }
        public string Identificacion_Alias { get; set; }
        public string Identificacion_CCtUsr { get; set; }
        public string Identificacion_CCtUsro { get; set; }
        public string Identificacion_Impresora { get; set; }
        public string Identificacion_Rut { get; set; }
        public string Importaciones_TcpAutBcoCen { get; set; }
        public short? MinsAlertaAdminEnvioSwift { get; set; }
        public short? MinsAlertaAutorizacionSwift { get; set; }
        public short? MinsAlertaEnvioSwift { get; set; }
        public short? MinsAlertaRecepcionSwift { get; set; }
        public string MODGUSR_UsrEsp_CentroCosto_CodBCCH { get; set; }
        public string MODGUSR_UsrEsp_CentroCosto_CodBCH { get; set; }
        public string MODGUSR_UsrEsp_CentroCosto_CodPBC { get; set; }
        public string MODGUSR_UsrEsp_CentroCosto_SucBCH { get; set; }
        public string Monedas_CodMonedaDolar { get; set; }
        public string Monedas_CodMonedaNacional { get; set; }
        public string Oficinas_UsrEsp_CentroCosto { get; set; }
        public string Pais_CodPais { get; set; }
        public string Participantes_PartyEnRed { get; set; }
        public string Participantes_PartyNodo { get; set; }
        public string Participantes_PartyServidor { get; set; }
        public string SaldosCtaCte_NodoSalME { get; set; }
        public string SaldosCtaCte_SerSalCCL { get; set; }
        public string SaldosCtaCte_VisSalME { get; set; }
        public string SaldosCtaCte_VisSalMN { get; set; }
        public string samAccountName { get; set; }
        public string SceIdi_PlazaCentral { get; set; }
        public string SgtCCLin_NodoSgt { get; set; }
        public string SgtCCLin_ServSgt { get; set; }
        public string SgtCCLin_TabSgt { get; set; }
        public string SgtCCLin_VisSgt { get; set; }
        public string Swift103_BICEMI { get; set; }
        public string Swift103_BICREC { get; set; }
        public string Swift_23E_Reglas { get; set; }
        public string SyBase_Base { get; set; }
        public string SyBase_Nodo { get; set; }
        public string SyBase_Servidor { get; set; }
        public string SyBase_Usuario { get; set; }
        public IEnumerable<ICodigosSucursal> codigos_sucursal { get; set; }
        public string Ubicacion_Entry { get; set; }
        public string WebServices_IP { get; set; }

        public PrintFormat ConfigImpres_PrintFormat
        {
            get
            {
                PrintFormat value;
                if (!Enum.TryParse<PrintFormat>(this.ConfigImpres_Formato, out value))
                    value = PrintFormat.TIFF;

                return value;
            }
            set
            {
                this.ConfigImpres_Formato = value.ToString();
            }
        }

        public string Identificacion_CentroDeCostosImpersonado
        {
            get
            {
                if (this.Identificacion_CCtUsr.Length == 5)
                {
                    return this.Identificacion_CCtUsr.Substring(0, 3);
                }
                else return String.Empty;
            }
        }

        public string Identificacion_IdEspecialistaImpersonado
        {
            get
            {
                if (this.Identificacion_CCtUsr.Length == 5)
                {
                    return this.Identificacion_CCtUsr.Substring(3, 2);
                }
                else return String.Empty;
            }
        }

        public string Identificacion_CentroDeCostosOriginal
        {
            get
            {
                if (this.Identificacion_CCtUsro.Length == 5)
                {
                    return this.Identificacion_CCtUsro.Substring(0, 3);
                }
                else return String.Empty;
            }
        }

        public string Identificacion_IdEspecialistaOriginal
        {
            get
            {
                if (this.Identificacion_CCtUsro.Length == 5)
                {
                    return this.Identificacion_CCtUsro.Substring(3, 2);
                }
                else return String.Empty;
            }
        }

        public int RutEnFormatoBDSwift
        {
            get
            {
                if (!String.IsNullOrEmpty(this.Identificacion_Rut))
                {
                    if (this.Identificacion_Rut.Length > 8)
                    {
                        return int.Parse(this.Identificacion_Rut.Substring(0, this.Identificacion_Rut.Length - 1)); //dejo afuera el digito verificador
                    }
                    else
                    {
                        return int.Parse(this.Identificacion_Rut);
                    }
                }
                else return 0;
            }
        }

        public string CodBCCH
        {
            get
            {
                var aux = this.codigos_sucursal.Where(c => c.CentroCosto == this.Identificacion_CentroDeCostosImpersonado).SingleOrDefault();
                if (aux == null)
                    return "";
                else
                    return aux.CodBCCH;

            }
        }

        public string CodPBC
        {
            get
            {
                var aux = this.codigos_sucursal.Where(c => c.CentroCosto == this.Identificacion_CentroDeCostosImpersonado).SingleOrDefault();
                if (aux == null)
                    return "";
                else
                    return aux.CodPBC;
            }
        }

        public string CodBCH
        {
            get
            {
                var aux = this.codigos_sucursal.Where(c => c.CentroCosto == this.Identificacion_CentroDeCostosImpersonado).SingleOrDefault();
                if (aux == null)
                    return "";
                else
                    return aux.CodBCH;
            }
        }

        public string SucBCH
        {
            get
            {
                var aux = this.codigos_sucursal.Where(c => c.CentroCosto == this.Identificacion_CentroDeCostosImpersonado).SingleOrDefault();
                if (aux == null)
                    return "";
                else
                    return aux.SucBCH;
            }
        }

        public static DatosUsuarioDTO ToDTO(DatosUsuario input) {
            var output = new DatosUsuarioDTO();
            output.BCHComexSwem_Casillas = input.BCHComexSwem_Casillas;
            output.ConfigImpres_ContabilidadGenerica = input.ConfigImpres_ContabilidadGenerica;
            output.ConfigImpres_Formato = input.ConfigImpres_Formato;
            output.ConfigImpres_ImprimeCartas = input.ConfigImpres_ImprimeCartas;
            output.ConfigImpres_ImprimePlanillas = input.ConfigImpres_ImprimePlanillas;
            output.ConfigImpres_ImprimeReporte = input.ConfigImpres_ImprimeReporte;
            output.Configuracion_Sonidos = input.Configuracion_Sonidos;
            output.CtaCteLin_ArcHCCL = input.CtaCteLin_ArcHCCL;
            output.CtaCteLin_ArcLCCL = input.CtaCteLin_ArcLCCL;
            output.CtaCteLin_ServCCL = input.CtaCteLin_ServCCL;
            output.CtaCteLin_ServSOL = input.CtaCteLin_ServSOL;
            output.CtaCteLin_VistSOL = input.CtaCteLin_VistSOL;
            output.Entry_Usuario = input.Entry_Usuario;
            output.Exportaciones_DocSwf = input.Exportaciones_DocSwf;
            output.Exportaciones_TcpConDec = input.Exportaciones_TcpConDec;
            output.Exportaciones_TcpConvenio = input.Exportaciones_TcpConvenio;
            output.Exportaciones_TcpSinPai = input.Exportaciones_TcpSinPai;
            output.EXPOTAR_arch_export = input.EXPOTAR_arch_export;
            output.EXPOTAR_dir_arch_export = input.EXPOTAR_dir_arch_export;
            output.EXPOTAR_ruta_excel = input.EXPOTAR_ruta_excel;
            output.FirmasLocales = input.FirmasLocales;
            output.General_MndDol = input.General_MndDol;
            output.General_MndNac = input.General_MndNac;
            output.General_MndSinDec = input.General_MndSinDec;
            output.General_MontoIVA = input.General_MontoIVA;
            output.Identificacion_Alias = input.Identificacion_Alias;
            output.Identificacion_CCtUsr = input.Identificacion_CCtUsr;
            output.Identificacion_CCtUsro = input.Identificacion_CCtUsro;
            output.Identificacion_Impresora = input.Identificacion_Impresora;
            output.Identificacion_Rut = input.Identificacion_Rut;
            output.Importaciones_TcpAutBcoCen = input.Importaciones_TcpAutBcoCen;
            output.MinsAlertaAdminEnvioSwift = input.MinsAlertaAdminEnvioSwift;
            output.MinsAlertaAutorizacionSwift = input.MinsAlertaAutorizacionSwift;
            output.MinsAlertaEnvioSwift = input.MinsAlertaEnvioSwift;
            output.MinsAlertaRecepcionSwift = input.MinsAlertaRecepcionSwift;
            output.MODGUSR_UsrEsp_CentroCosto_CodBCCH = input.MODGUSR_UsrEsp_CentroCosto_CodBCCH;
            output.MODGUSR_UsrEsp_CentroCosto_CodBCH = input.MODGUSR_UsrEsp_CentroCosto_CodBCH;
            output.MODGUSR_UsrEsp_CentroCosto_CodPBC = input.MODGUSR_UsrEsp_CentroCosto_CodPBC;
            output.MODGUSR_UsrEsp_CentroCosto_SucBCH = input.MODGUSR_UsrEsp_CentroCosto_SucBCH;
            output.Monedas_CodMonedaDolar = input.Monedas_CodMonedaDolar;
            output.Monedas_CodMonedaNacional = input.Monedas_CodMonedaNacional;
            output.Oficinas_UsrEsp_CentroCosto = input.Oficinas_UsrEsp_CentroCosto;
            output.Pais_CodPais = input.Pais_CodPais;
            output.Participantes_PartyEnRed = input.Participantes_PartyEnRed;
            output.Participantes_PartyNodo = input.Participantes_PartyNodo;
            output.Participantes_PartyServidor = input.Participantes_PartyServidor;
            output.SaldosCtaCte_NodoSalME = input.SaldosCtaCte_NodoSalME;
            output.SaldosCtaCte_SerSalCCL = input.SaldosCtaCte_SerSalCCL;
            output.SaldosCtaCte_VisSalME = input.SaldosCtaCte_VisSalME;
            output.SaldosCtaCte_VisSalMN = input.SaldosCtaCte_VisSalMN;
            output.samAccountName = input.samAccountName;
            output.SceIdi_PlazaCentral = input.SceIdi_PlazaCentral;
            output.SgtCCLin_NodoSgt = input.SgtCCLin_NodoSgt;
            output.SgtCCLin_ServSgt = input.SgtCCLin_ServSgt;
            output.SgtCCLin_TabSgt = input.SgtCCLin_TabSgt;
            output.SgtCCLin_VisSgt = input.SgtCCLin_VisSgt;
            output.Swift103_BICEMI = input.Swift103_BICEMI;
            output.Swift103_BICREC = input.Swift103_BICREC;
            output.Swift_23E_Reglas = input.Swift_23E_Reglas;
            output.SyBase_Base = input.SyBase_Base;
            output.SyBase_Nodo = input.SyBase_Nodo;
            output.SyBase_Servidor = input.SyBase_Servidor;
            output.SyBase_Usuario = input.SyBase_Usuario;
            output.codigos_sucursal = input.tbl_datos_usuario_codigos_sucursal.Select(i => CodigosSucursalDTO.ToDTO(i)).ToList();
            output.Ubicacion_Entry = input.Ubicacion_Entry;
            output.WebServices_IP = input.WebServices_IP;

            return output;

        }
    }
}