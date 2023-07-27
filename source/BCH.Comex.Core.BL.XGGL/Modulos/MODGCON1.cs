using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica.T_Modulos;
using BCH.Comex.Data.DAL.Cext01;
using System;
using System.Collections.Generic;
using System.Linq;
using BCH.Comex.Common.Tracing;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODGCON1
    {
        //Carga e Imprime un Reporte Contable.-
        public static void Pr_Imprime_Contab80(DatosGlobales Globales, int NroRpt, string FecMch)
        {
            Globales.DocumentosAImprimir.Add("Impresion/ImpresionDeDocumentos/ReporteContable?nroReporte=" + NroRpt + "&fechaOp=" + FecMch + "&generarHtmlCompleto=true");
        }

        /// <summary>
        /// Lee un Reporte Contable.-
        /// Retorno    <> 1 : Lectura Exitosa.-
        ///            =  0 : Error o Lectura no Exitosa.-
        /// </summary>
        /// <param name="Numero"></param>
        /// <param name="Fecha"></param>
        /// <returns></returns>
        public static int SyGet_xMch(int Numero, string Fecha, DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer("Reporte Contable (Sce_Mch) - SyGet_xMch"))
            {
                int SyGet_xMch = 0;
                try
                {
                    Globales.MODGCON1.V_IMch = new T_IMch();
                    sce_mch_s01_MS_Result result = uow.DocumentosOperacionesRepository.sce_mch_s01_MS(Numero, DateTime.Parse(MODGSYB.dbdatesy(Fecha))).FirstOrDefault();
                    if (result != null)
                    {
                        Globales.MODGCON1.V_IMch.CodCct = result.codcct;    // Centro Costo.
                        Globales.MODGCON1.V_IMch.CodPro = result.codpro;    // Producto.
                        Globales.MODGCON1.V_IMch.CodEsp = result.codesp;    // Especialista.
                        Globales.MODGCON1.V_IMch.CodOfi = result.codofi;    // Empresa.
                        Globales.MODGCON1.V_IMch.CodOpe = result.codope;    // Operación.
                        Globales.MODGCON1.V_IMch.NroRpt = result.nrorpt.ToInt();   // Correlativo.
                        Globales.MODGCON1.V_IMch.FecMov = result.fecmov.ToString("dd/MM/yyyy");     // Fecha Movimiento.
                        Globales.MODGCON1.V_IMch.NomCli = result.nomcli;     // Nombre Importador.
                        Globales.MODGCON1.V_IMch.codfun = result.codfun.ToInt();     // Código de Función.
                        Globales.MODGCON1.V_IMch.estado = result.estado.ToInt();     // Estado de la Contabilidad.
                        Globales.MODGCON1.V_IMch.DesGen = result.desgen;     // Observaciones Generales.
                        Globales.MODGCON1.V_IMch.cencos = result.cencos;     // Observaciones Generales.
                        Globales.MODGCON1.V_IMch.codusr = result.codusr;     // Observaciones Generales.
                        SyGet_xMch = true.ToInt();
                        Globales.MODGCON1.V_IMch.Nombre = SyGet_VerU(Globales.MODGCON1.V_IMch.cencos, Globales.MODGCON1.V_IMch.codusr, Globales, uow);
                    }
                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta: ", exc);
                    Globales.MESSAGES.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de leer el Reporte Contable (Sce_Mch)",
                        Title = "Contabilidad Genérica",
                        Type = TipoMensaje.Informacion
                    });
                }
                return SyGet_xMch;
            }
        }

        /// <summary>
        /// Lee el Detalle de un Reporte Contable.-
        /// Retorno    <> ""  : Lectura Exitosa.-
        ///            =  ""  : Error o Lectura no Exitosa.-
        /// </summary>
        /// <returns></returns>
        public static int SyGetn_xMcd(int Numero, string Fecha, DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer("Reporte Contable (Sce_Mcd) - SyGetn_xMcd"))
            {
                int SyGetn_xMcd = 0;
                string cct = string.Empty;
                int i = 0;
                int n = 0;
                Globales.MODGCON1.V_IMcd = new T_IMcd[0];

                try
                {
                    IList<sce_mcd> result = uow.SceRepository.sce_mcd_s07_MS(Numero, DateTime.Parse(MODGSYB.dbdatesy(Fecha)));
                    n = result.Count();
                    Globales.MODGCON1.V_IMcd = new T_IMcd[n];
                    for (i = 0; i < n; i += 1)
                    {
                        Globales.MODGCON1.V_IMcd[i] = new T_IMcd();
                        Globales.MODGCON1.V_IMcd[i].CodMon = result[i].codmon.ToInt();
                        Globales.MODGCON1.V_IMcd[i].NemCta = result[i].nemcta;
                        Globales.MODGCON1.V_IMcd[i].NemMon = result[i].nemmon;
                        Globales.MODGCON1.V_IMcd[i].numcta = result[i].numcta;
                        Globales.MODGCON1.V_IMcd[i].IdnCta = result[i].idncta.ToInt();
                        Globales.MODGCON1.V_IMcd[i].cod_dh = result[i].cod_dh;
                        Globales.MODGCON1.V_IMcd[i].mtomcd = result[i].mtomcd.ToDbl();
                        Globales.MODGCON1.V_IMcd[i].PrtCli = result[i].prtcli;
                        Globales.MODGCON1.V_IMcd[i].rutcli = result[i].rutcli;
                        Globales.MODGCON1.V_IMcd[i].SwiBco = result[i].swibco;
                        Globales.MODGCON1.V_IMcd[i].numcct = result[i].numcct;
                        Globales.MODGCON1.V_IMcd[i].OfiDes = result[i].ofides.ToInt();
                        Globales.MODGCON1.V_IMcd[i].NumPar = result[i].numpar.ToInt();
                        Globales.MODGCON1.V_IMcd[i].TipMov = result[i].tipmov.ToInt();
                        Globales.MODGCON1.V_IMcd[i].NroRef = result[i].nroref;
                        Globales.MODGCON1.V_IMcd[i].TipCam = result[i].tipcam.ToInt();
                        Globales.MODGCON1.V_IMcd[i].FecVen = result[i].fecven.ToString("dd/MM/yyyy");

                        if (Globales.MODGCON1.V_IMcd[i].SwiBco == "")
                        {
                            cct = Globales.MODGCON1.V_IMcd[i].numcct;
                    
                            if (cct != string.Empty)
                            {
                                if (Globales.MODGCON1.V_IMcd[i].numcct.Len() > 8)
                                {
                                    if (Globales.MODGCON1.V_IMcd[i].CodMon == Globales.MODGSCE.VGen.MndNac)
                                    {
                                        Globales.MODGCON1.V_IMcd[i].numcct = cct.Mid(1, 3) + "-" + cct.Mid(4, 5) + "-" + cct.Mid(9, 2);
                                    }
                                    else
                                    {
                                        Globales.MODGCON1.V_IMcd[i].numcct = cct.Mid(1, 4) + "-" + cct.Mid(5, 5) + "-" + cct.Mid(10, 2);
                                    }
                                }
                            }
                        }
                    }

                    SyGetn_xMcd = Convert.ToInt16(true);
                    return SyGetn_xMcd;
                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta: ", exc);
                    Globales.FrmGlanu.ListaErrores.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de leer el detalle de los Reportes Contables (Sce_Mcd).",
                        Title = "Contabilidad Genérica",
                        Type = TipoMensaje.Informacion
                    });
                }
                return SyGetn_xMcd;
            }
        }

        public static string SyGet_VerU(string cencos, string codusr, DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer("Reporte Contable (Sce_Mch) - SyGet_VerU"))
            {
                string SyGet_VerU = string.Empty;

                try
                {
                    SyGet_VerU = uow.SceRepository.sce_usr_s07_MS(cencos, codusr);
                }
                catch (Exception exc)
                {
                    tracer.TraceException("Alerta: ", exc);
                    Globales.MESSAGES.Add(new UI_Message
                    {
                        Text = "Se ha producido un error al tratar de leer el Reporte Contable (Sce_Mch)",
                        Title = "Contabilidad Genérica",
                        Type = TipoMensaje.Informacion
                    });

                }
                
                return SyGet_VerU;
            }
        }

    }
}
