using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Impresion.T_Modulos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BCH.Comex.Core.BL.Common
{
    public partial class CommonService
    {

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
            foreach (DocumentoOperacion doc in temp)
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

        public ReporteContable GetReporteContable(int nroReporte, DateTime fecha, IList<sce_dfc> descripcionesFunciones)
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
                    linea.numcct = FormatearNroDeCuenta(linea.numcct, Convert.ToInt16(linea.codmon));
                    sce_cta_s01_1_MS_Result datosCuenta   = uow.SceRepository.sce_cta_s01_1_MS(linea.nemcta).FirstOrDefault();

                    if(datosCuenta != null)
                    {
                        linea.NombreCuenta = datosCuenta.cta_nom;
                        linea.DescAdicional = GetDescripcionAdicionalDeLineaContable(linea);
                    }
                    
                   
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

        public sce_mch_s01_MS_Result GetCabeceraReporteDetalleSwift(int nroReporte, DateTime fecha)
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

        private string FormatearNroDeCuenta(string nroCuenta, short codMoneda)
        {
            if (!String.IsNullOrEmpty(nroCuenta))
            {
                if (nroCuenta.Length > 8)
                {
                    if (codMoneda == short.Parse(ConfigurationManager.AppSettings["FundTransfer.Monedas.CodMonedaNacional"]))
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

            string descBanco, descRef, descCuenta, descFecha;

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
                    descFecha = ((linea.fecven == DateTime.MinValue || linea.fecven == new DateTime(1900,1,1,0,0,0)) ? String.Empty : " Vto: " + linea.fecven.ToString("dd/MM/yyyy"));

                    string result = (descBanco + " " + descCuenta).Trim();
                    if (!String.IsNullOrEmpty(linea.nroref) && linea.idncta != T_MODGCON0.IdCta_OBLACP)
                    {
                        result += (String.IsNullOrEmpty(descCuenta) ? " Ref: " + linea.nroref : "; Ref: " + linea.nroref);

                    }
                    return result += descFecha;
            }

            // Obligaciones
            if(uow.SceRepository.EsObligacion(Convert.ToInt32(linea.idncta)))
            {
                descBanco = (String.IsNullOrEmpty(linea.swibco) ? String.Empty : "Bco: " + linea.swibco);
                descFecha = ((linea.fecven == DateTime.MinValue || linea.fecven == new DateTime(1900, 1, 1, 0, 0, 0)) ? String.Empty : " Vto: " + linea.fecven.ToString("dd/MM/yyyy"));

                return (descBanco + " " + descFecha).Trim();
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
                        doc.DescripcionDoc = T_ModImpresion.GetTipoCartaDesc(short.Parse(doc.CodigoPropio));
                        break;

                    case DocumentoOperacion.TipoDocEnum.Contabilidad:
                        doc.DescripcionDoc = doc.NroRpt + "-" + doc.FechaOperacion.ToString("dd/MM/yyyy");
                        break;

                    case DocumentoOperacion.TipoDocEnum.Swift:
                        doc.DescripcionDoc = "MT-" + doc.TipoSwift.ToString();
                        break;

                }

                doc.DescripcionProducto = T_ModImpresion.GetProductoDesc(doc.CodPro);
            }
        }
        
        public IList<DocumentoOperacion> BuscarDocumentosDevengoPorFecha(DateTime fechaOperacion)
        {
            List<DocumentoOperacion> result = new List<DocumentoOperacion>();
            result.AddRange(uow.SceRepository.sce_mchh_s01_MS(fechaOperacion));
            AgregarDescripcionALosDocumentosDevengo(result);
            return result;
        }

        private void AgregarDescripcionALosDocumentosDevengo(IList<DocumentoOperacion> documentos)
        {
            foreach (DocumentoOperacion doc in documentos)
            {
                switch (doc.TipoDoc)
                {
                    case DocumentoOperacion.TipoDocEnum.Contabilidad:
                        doc.DescripcionDoc = doc.NroRpt + "-" + doc.FechaOperacion.ToString("dd/MM/yyyy");
                        break;
                }
            }
        }


    }
}
