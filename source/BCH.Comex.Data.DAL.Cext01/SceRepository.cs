using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Common.Tracing;
using BCH.Comex.Core.Entities.Cext01;
using BCH.Comex.Core.Entities.Cext01.Common;
using BCH.Comex.Core.Entities.Cext01.FundTransfer.T_Modulos;
using BCH.Comex.Core.Entities.Cext01.MT300Common;
using BCH.Comex.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class SceRepository : GenericRepository<tbl_sce_fts, cext01Entities>
    {
        public SceRepository(cext01Entities context) : base(context) { }
        /// <summary>
        /// Obtiene una planilla visible de exportación
        /// </summary>
        /// <param name="numeroPresentacion">Numero de Presentacion sin guión (ej. 068199K)</param>
        /// <param name="fechaPresentacion">Fecha de Presentacion</param>
        /// <returns></returns>
        public IQueryable<sce_xplv_s11_MS_Result> sce_xplv_s11(string numeroPresentacion, DateTime fechaPresentacion)
        {
            return context.sce_xplv_s11_MS(numeroPresentacion, fechaPresentacion).AsQueryable();
        }

        public sce_xdoc_s01_MS_Result sce_xdoc_s01_MS(string codcct, string codpro, string codesp, string codofi,
            string codope, decimal nrocor)
        {
            return context.sce_xdoc_s01_MS(codcct, codpro, codesp, codofi, codope, nrocor).FirstOrDefault();
        }

        public IList<bool> Pro_sce_cvd_ft_s02_MS(string codcct, string codesp, string codofi, string codope, string codpro)
        {
            return context.pro_sce_cvd_ft_s02_MS(codcct, codesp, codofi, codope, codpro).ToList().Select(i => i.Value).ToList();
        }

        public IEnumerable<sce_usr_s35_MS_Result> sce_usr_s35_MS(string cctUsr, bool soloEspecialistas)
        {
            return context.sce_usr_s35_MS(cctUsr, soloEspecialistas);
        }

        public IEnumerable<string> sce_usr_s01_MS(string cencos, string codusr)
        {
            return context.sce_usr_s01_MS(cencos, codusr);
        }

        public sce_usr_u01_MS_Result sce_usr_u01_MS(string cencos, string codusr, string reemplazos)
        {
            return context.sce_usr_u01_MS(cencos, codusr, reemplazos).FirstOrDefault();
        }



        public int sce_mts_d01_MS(string codcct, string codpro, string codesp, string codopi, string codope, decimal numneg, decimal tippro, decimal numcpa, decimal numcuo, decimal numcob, decimal idmensaje)
        {
            return context.sce_mts_d01_MS(codcct, codpro, codesp, codopi, codope, numneg, tippro, numcpa, numcuo, numcob, idmensaje);
        }

        public List<pro_sce_suc_s01_MS_Result> pro_sce_suc_s01_MS(string succod)
        {
            var result1 = new List<object>(); //resultado a ignorar
            var result2 = new List<pro_sce_suc_s01_MS_Result>();

            ReadQueryMultipleResult<object, pro_sce_suc_s01_MS_Result>("pro_sce_suc_s01_MS", out result1, out result2, succod);

            return result2;
        }

        /// <summary>
        /// Obitene de tabla tbl_sce_relacion_ft el siguiente correlativo de 2 dígitos para la operación seleccionada. Comienza en 00. 
        /// </summary>
        /// <param name="codcct"></param>
        /// <param name="codPro"></param>
        /// <param name="codEsp"></param>
        /// <param name="codOfi"></param>
        /// <param name="codOpe"></param>
        /// <returns></returns>
        public string pro_sce_sup_s04(AbonoCargoResultDTO ac)
        {
            int? correlativo = context.pro_sce_sup_s04_MS(ac.codcct, ac.codpro, ac.codesp, ac.codofi, ac.codope).FirstOrDefault();

            if (correlativo.HasValue)
            {
                return correlativo.Value.ToString("00");
            }
            else return "00";
        }

        public string pro_sce_cvd_ft_s01_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.pro_sce_cvd_ft_s01_MS(codcct, codpro, codesp, codofi, codope).FirstOrDefault();
        }

        public string pro_sce_cvd_ft_s02_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            var resultado = context.pro_sce_cvd_ft_s02_MS(codcct, codpro, codesp, codofi, codope).FirstOrDefault().ToString();
            resultado = (resultado == "" ? "false" : resultado);
            return resultado;
            //string result = string.Empty;
            //ReadQuerySP((reader) =>
            //  {
            //      while (reader.Read())
            //      {
            //          result = Utils.GetStringFromDataReader(reader, 1);
            //      }
            //  }, "pro_sce_cvd_ft_s02_MS", codcct, codpro, codesp, codofi, codope);
            //return (resultado == "" ? "true" : "false");
        }

        public IList<sce_cvd1_s04_MS_Result> sce_cvd1_s04_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            //var parameters = new List<string>();
            //parameters.Add(codcct);
            //parameters.Add(codpro);
            //parameters.Add(codesp);
            //parameters.Add(codofi);
            //parameters.Add(codope);

            var result = new List<sce_cvd1_s04_MS_Result>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    var item = new sce_cvd1_s04_MS_Result
                    {
                        numpln = Utils.GetStringFromDataReader(reader, 0),
                        fecpln = (DateTime)Utils.GetFechaFromDataReader(reader, 1),
                        visinv = Utils.GetStringFromDataReader(reader, 2),
                        tippln = (Decimal)Utils.GetDecimalFromDataReader(reader, 3),
                        codmnd = (Decimal)Utils.GetDecimalFromDataReader(reader, 4),
                        mtopln = (Decimal)Utils.GetDecimalFromDataReader(reader, 5),
                        tipcam = (Decimal)Utils.GetDecimalFromDataReader(reader, 6),
                        tipcamo = (Decimal)Utils.GetDecimalFromDataReader(reader, 7),
                        plzbcc = (Decimal)Utils.GetDecimalFromDataReader(reader, 8),
                        estado = (int)Utils.GetIntByteFromDataReader(reader, 9),
                        numdec = Utils.GetStringFromDataReader(reader, 10),
                        fecdec = (DateTime)Utils.GetFechaFromDataReader(reader, 11),
                        codadn = (Decimal)Utils.GetDecimalFromDataReader(reader, 12),
                        prtexp = Utils.GetStringFromDataReader(reader, 13),
                        codcom = Utils.GetStringFromDataReader(reader, 14)
                    };
                    result.Add(item);
                }
            }, "sce_cvd1_s04_MS", codcct, codpro, codesp, codofi, codope);

            return result;
            //context.sce_cvd1_s04_MS(codcct, codpro, codesp, codofi, codope).ToString();

        }

        public IList<string> sgt_suc_s02_MS(short suc_sucod)
        {
            return context.sgt_suc_s02_MS(suc_sucod).ToList();
        }

        public List<sce_xdoc_s01_MS_Result> sce_xdoc_s01_MS(string codcct, string codpro, string codesp, string codofi,
            string codope, string NroCor)
        {
            using (var trace = new Tracer())
            {
                List<sce_xdoc_s01_MS_Result> result;
                try
                {
                    decimal aux;
                    decimal? nrocor;
                    nrocor = decimal.TryParse(NroCor, out aux) ? aux : (decimal?)null;

                    result = context.sce_xdoc_s01_MS(codcct, codpro, codesp, codofi, codope, nrocor).ToList();

                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }

                return result;
            }
        }

        public string sce_memg_s01_MS(string codtab, decimal codmem)
        {
            using (var trace = new Tracer())
            {
                string texto;
                try
                {
                    texto = context.sce_memg_s01_MS(codtab, codmem).FirstOrDefault();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }
                return texto;
            }
        }

        public string sce_refe_s01_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (var trace = new Tracer())
            {
                string texto;
                try
                {
                    texto = context.sce_refe_s01_MS(codcct, codpro, codesp, codofi, codope).FirstOrDefault();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }
                return texto;
            }
        }

        public int? sce_xdoc_i01_MS(string codcct, string codpro, string codesp, string codofi, string codope, string nrocor,
            string cencos, string codusr, string coddoc, string fecing, string codmem)
        {
            using (var trace = new Tracer())
            {
                int? result;
                try
                {
                    result = context.sce_xdoc_i01_MS(codcct, codpro, codesp, codofi, codope, int.Parse(nrocor), cencos, codusr,
                        int.Parse(coddoc), DateTime.Parse(fecing), int.Parse(codmem)).FirstOrDefault();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }

                return result;
            }
        }

        public int? sce_xdoc_s02_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (var trace = new Tracer())
            {
                int? result;
                try
                {
                    decimal? decResult = context.sce_xdoc_s02_MS(codcct, codpro, codesp, codofi, codope).FirstOrDefault();

                    result = (int?)decResult;

                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }

                return result;
            }
        }

        public List<string> sce_doc_s01_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            using (var trace = new Tracer())
            {
                List<string> result = null;

                try
                {
                    result = context.sce_doc_s01_MS(codcct, codpro, codesp, codofi, codope).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }

                return result;
            }
        }

        public sce_ccof_s01_MS_Result sce_ccof_s01_MS(string ccosto)
        {
            using (var trace = new Tracer())
            {
                sce_ccof_s01_MS_Result result;
                try
                {
                    result = context.sce_ccof_s01_MS(ccosto).FirstOrDefault();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }

                return result;
            }
        }

        public List<sce_mcd_s66_MS_Result> sce_mcd_s66_MS(DateTime fecha, string nemonicoCuenta)
        {
            return context.sce_mcd_s66_MS(nemonicoCuenta, fecha).ToList();
        }

        public List<sce_mcd_s65_MS_Result> sce_mcd_s65_MS(DateTime fecha, string cuenta)
        {
            return context.sce_mcd_s65_MS(cuenta, fecha).ToList();
        }

        public string sce_usr_s25_MS(string rut_eje)
        {
            using (var trace = new Tracer())
            {
                string result;
                try
                {
                    result = context.sce_usr_s25_MS(rut_eje).FirstOrDefault();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }

                return result;
            }
        }

        public List<sce_ini_s01_MS_Result> sce_ini_s01_MS(string grupo, string elem)
        {
            using (var trace = new Tracer())
            {
                List<sce_ini_s01_MS_Result> result = null;

                try
                {
                    result = context.sce_ini_s01_MS(grupo, elem).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }

                return result;
            }
        }

        /// <summary>
        /// Retorna frases para ser usadas en distintas cartas y reportes a imprimir.
        /// </summary>
        /// <param name="CodigoFrase"></param>
        /// <param name="Idioma"></param>
        /// <returns></returns>
        /// 
        public enum IdiomaFrases
        {
            Espanol,
            Ingles
        }
        public IQueryable<sce_fra_s06_MS_Result> Sce_Fra_S06(int codigoFrase, IdiomaFrases idioma)
        {
            var paramIdioma = idioma == IdiomaFrases.Espanol ? "E" : "I";
            return context.sce_fra_s06_MS(codigoFrase, paramIdioma).AsQueryable();
        }

        public List<sce_fra_s06_MS_Result> Sce_Fra_S06(string CodFra, string Idioma)
        {
            using (var trace = new Tracer())
            {
                List<sce_fra_s06_MS_Result> result = null;
                try
                {
                    result = context.sce_fra_s06_MS(decimal.Parse(CodFra), Idioma).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }
                return result;
            }
        }
        public List<sce_fra_s06_MS_Result> sce_fra_s06_MS(string CodFra, string Idioma)
        {
            using (var trace = new Tracer())
            {
                List<sce_fra_s06_MS_Result> result = null;
                try
                {
                    result = context.sce_fra_s06_MS(decimal.Parse(CodFra), Idioma).ToList();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }

                return result;
            }
        }

        public int? sce_memg_s03_MS(string Tabla, string M)
        {
            using (var trace = new Tracer())
            {
                int? result;
                try
                {
                    result = context.sce_memg_s03_MS(Tabla, decimal.Parse(M)).FirstOrDefault();
                }
                catch (Exception e)
                {
                    trace.TraceException("Alerta", e);
                    throw;
                }

                return result;
            }
        }

        public string ScePAMemg11(string Tabla)
        {
            string result;
            try
            {

                result = EjecutarSP<string>("ScePAMemg11", Tabla).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public string sce_memg_s01_MS(string Tabla, string CodMem)
        {
            string result;

            try
            {
                result = context.sce_memg_s01_MS(Tabla, decimal.Parse(CodMem)).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public string rce_memg_s01_MS(string Tabla, string CodMem)
        {
            string result;

            try
            {
                result = context.rce_memg_s01_MS(Tabla, decimal.Parse(CodMem)).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public sce_memg_d01_MS_Result sce_memg_d01_MS(string Tabla, string CodMem)
        {
            sce_memg_d01_MS_Result result;

            try
            {
                result = context.sce_memg_d01_MS(Tabla, decimal.Parse(CodMem)).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public sce_memg_i01_MS_Result sce_memg_i01_MS(string Tabla, string CodMem, string i, string lineas)
        {
            sce_memg_i01_MS_Result result;

            try
            {
                result = context.sce_memg_i01_MS(Tabla, decimal.Parse(CodMem), decimal.Parse(i), lineas).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public string sce_vrng_s01_MS(string codcct, string codpro, string codesp)
        {
            string result;

            try
            {
                result = context.sce_vrng_s01_MS(codcct, codpro, codesp).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public sce_trng_s01_MS_Result sce_trng_s01_MS()
        {
            sce_trng_s01_MS_Result result;
            try
            {
                result = context.sce_trng_s01_MS().FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public sce_rng_i01_MS_Result sce_rng_i01_MS(string cct, string esp, string doc, string rut, string inf, string sup, string act)
        {
            sce_rng_i01_MS_Result result;
            try
            {
                result = context.sce_rng_i01_MS(cct, esp, doc, rut, Format.StringToDouble(inf), Format.StringToDouble(sup),
                    Format.StringToDouble(act)).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;

        }

        public sce_usr_u02_MS_Result sce_usr_u02_MS(string CenCos, string CodUsr, string CodFec, string fecha)
        {

            var result = new List<sce_usr_u02_MS_Result>();
            try
            {
                ReadQuerySP((reader) =>
                {
                    while (reader.Read())
                    {
                        var item = new sce_usr_u02_MS_Result
                        {
                            Column1 = reader.GetInt32(0),
                            Column2 = Utils.GetStringFromDataReader(reader, 1)
                        };
                        result.Add(item);
                    }
                }, "sce_usr_u02_MS", CenCos, CodUsr, CodFec, fecha);
                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public sce_usr_u02_MS_Result sce_usr_u02_MS(string CenCos, string CodUsr, string CodFec, DateTime fecha)
        {
            return sce_usr_u02_MS(CenCos, CodUsr, CodFec, fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
        }

        public IEnumerable<sce_usr_s03_MS_Result> sce_usr_s03_MS(string CenCos)
        {
            IEnumerable<sce_usr_s03_MS_Result> result;

            try
            {
                result = context.sce_usr_s03_MS(CenCos);
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;

        }

        public List<sce_serv_imp_01_MS_Result> sce_serv_imp_01_MS(string ls_rutcli, string ls_codcct)
        {
            List<sce_serv_imp_01_MS_Result> result = null;

            try
            {
                result = context.sce_serv_imp_01_MS(ls_rutcli, ls_codcct).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public List<sgt_suc_s04_MS_Result> sgt_suc_s04_MS(string oficina)
        {
            List<sgt_suc_s04_MS_Result> result;
            try
            {
                decimal aux;
                decimal? ofi = decimal.TryParse(oficina, out aux) ? ofi = aux : ofi = null;


                result = context.sgt_suc_s04_MS(ofi).ToList(); //EjecutarSP<string>("sgt_suc_s04_MS", oficina).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public List<sce_usr_s02_MS_Result> sce_usr_s02_MS(string cencos, string CodUsr)
        {
            List<sce_usr_s02_MS_Result> result;
            try
            {
                result = context.sce_usr_s02_MS(cencos, CodUsr).ToList(); //EjecutarSP<string>("sce_usr_s02_MS", cencos, CodUsr).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        //devuelve el nombre del usuario
        public string sce_usr_s07_MS(string centroCosto, string codUsr)
        {
            return context.sce_usr_s07_MS(centroCosto, codUsr).FirstOrDefault();
        }

        public List<pro_sce_codtran_s01_MS_Result> pro_sce_codtran_s01_MS()
        {
            try
            {
                return context.pro_sce_codtran_s01_MS().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int pro_sce_trxcor_ft_MS(out string ls_retorno, out string ls_mensaje, out int? li_corre)
        {
            //System.Data.Entity.Core.Objects.
            var retorno = new ObjectParameter("ls_retorno", "");
            var mensaje = new ObjectParameter("ls_mensaje", "");
            var corre = new ObjectParameter("li_corre", 0);

            //System.Data.Entity.Core.
            int ret = context.pro_sce_trxcor_ft_MS(retorno, mensaje, corre);

            ls_retorno = retorno.Value as string;
            ls_mensaje = mensaje.Value as string;
            li_corre = corre.Value as int?;

            return ret;
        }

        public short sce_cvd_w01_MS(params string[] parameters)
        {
            try
            {
                int resu = EjecutarSPConRetorno("sce_cvd_w01_MS", "set ansi_padding off set ansi_warnings off", parameters.ToArray());
                return (short)resu;
            }
            catch
            {
                return 9;
            }

        }

        public List<pro_sce_prty_s01_MS_Result> pro_sce_prty_s01_MS(string gb_razon_social)
        {
            //tengo que setear el ansi_padding en off para que traiga resultados
            List<pro_sce_prty_s01_MS_Result> result = context.Database.SqlQuery<pro_sce_prty_s01_MS_Result>(
                "set ansi_padding off exec pro_sce_prty_s01_MS @gb_razon_social",
                new SqlParameter("gb_razon_social", gb_razon_social)).ToList();
            //List<pro_sce_prty_s01_MS_Result> result = context.pro_sce_prty_s01_MS(gb_razon_social).ToList();

            return result;
        }

        public List<pro_sce_prty_s04_MS_Result> pro_sce_prty_s04_MS(string gb_operacion, string gb_estado)
        {
            //tener en cuenta que en este SP no funciona pasarle un filtro de gb_operacion (columna referencia, XREF a nivel de BD), esta mal la query y nunca funciono en el legacy.
            var result = new List<pro_sce_prty_s04_MS_Result>();

            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    /*
                     d.estado            
                     a.FCCFT             
                     d.fecingreso        
                     b.mnd_mndnom        
                     a.DRAMT             
                     a.XREF              
                     e.desc_ft as    desc
                     c.codmnd_bch        
                     a.PRD               
                     a.CRACC             
                     a.DRACC             
                     a.ORD_INST1         
                     a.PMNT_DET1         
                     a.PMNT_DET2         
                     a.PMNT_DET3         
                     a.PMNT_DET4         
                     d.DEBIT_REF         
                     h.desc_ft           
                     f.desc_ft           
                     a.BEN_INST1         
                     a.ULT_BEN1          
                     a.ULT_BEN2          
                     a.ULT_BEN3          
                     a.ULT_BEN4          
                     a.CHG_WHOM          
                     a.DRVALDT           
                     a.INTRMD1           
                     a.INTRMD2           
                     d.US_PAY_ID         
                     a.RECVR_CORRES1     
                     a.RECVR_CORRES2     
                     a.SNDR_RECVR_INFO1  
                     a.SNDR_RECVR_INFO2  
                     a.SNDR_RECVR_INFO3  
                     a.SNDR_RECVR_INFO4  
                     d.trxid             
                     g.contract_reference
                     a.SNDR_RECVR_INFO5  
                     a.SNDR_RECVR_INFO6  
                     
                     */
                    result.Add(new pro_sce_prty_s04_MS_Result
                    {
                        estado = reader.GetString(0),
                        FCCFT = reader.GetString(1),
                        fecingreso = reader.GetString(2),
                        mnd_mndnom = reader.GetString(3),
                        DRAMT = reader.GetString(4),
                        XREF = reader.GetString(5),
                        desc_ft = reader.GetString(6),
                        codmnd_bch = reader.GetDecimal(7),
                        PRD = reader.GetString(8),
                        CRACC = reader.GetString(9),
                        DRACC = reader.GetString(10),
                        ORD_INST1 = reader.GetString(11),
                        PMNT_DET1 = reader.GetString(12),
                        PMNT_DET2 = reader.GetString(13),
                        PMNT_DET3 = reader.GetString(14),
                        PMNT_DET4 = reader.GetString(15),
                        DEBIT_REF = reader.GetString(16),
                        desc_ft1 = reader.GetString(17),
                        desc_ft2 = reader.GetString(18),
                        BEN_INST1 = reader.GetString(19),
                        ULT_BEN1 = reader.GetString(20),
                        ULT_BEN2 = reader.GetString(21),
                        ULT_BEN3 = reader.GetString(22),
                        ULT_BEN4 = reader.GetString(23),
                        CHG_WHOM = reader.GetString(24),
                        DRVALDT = reader.GetString(25),
                        INTRMD1 = reader.GetString(26),
                        INTRMD2 = reader.GetString(27),
                        US_PAY_ID = reader.GetString(28),
                        RECVR_CORRES1 = reader.GetString(29),
                        RECVR_CORRES2 = reader.GetString(30),
                        SNDR_RECVR_INFO1 = reader.GetString(31),
                        SNDR_RECVR_INFO2 = reader.GetString(32),
                        SNDR_RECVR_INFO3 = reader.GetString(33),
                        SNDR_RECVR_INFO4 = reader.GetString(34),
                        trxid = reader.GetString(35),
                        contract_reference = reader.GetDecimal(36),
                        SNDR_RECVR_INFO5 = reader.GetString(37),
                        SNDR_RECVR_INFO6 = reader.GetString(38)
                    });
                }
            }, "pro_sce_prty_s04_MS", gb_operacion, gb_estado);


            return result;
        }

        public pro_sce_prty_s02_MS_Result pro_sce_prty_s02_MS_1(string cod_party, string opcion)
        {
            var result = EjecutarSP<pro_sce_prty_s02_MS_Result>("pro_sce_prty_s02_MS", cod_party, opcion).FirstOrDefault();

            return result;
        }

        public pro_sce_prty_s05_MS_Result pro_sce_prty_s05_MS(string gb_llave, int? gb_id, int? gb_opcion)
        {
            pro_sce_prty_s05_MS_Result result;
            try
            {
                result = context.pro_sce_prty_s05_MS(gb_llave, gb_id, gb_opcion).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public pro_sce_prty_s03_MS_Result pro_sce_prty_s03_MS(string gb_ccosto, string gb_producto, string gb_especialista,
            string gb_empresa, string gb_operacion, string gb_party, int gb_opcion)
        {
            pro_sce_prty_s03_MS_Result retorno = context.pro_sce_prty_s03_MS(gb_ccosto, gb_producto, gb_especialista, gb_empresa, gb_operacion,
                gb_party, gb_opcion).FirstOrDefault();

            return retorno;
        }


        public sce_ctas_s03_MS_Result sce_ctas_s03_MS(string id_party)
        {
            sce_ctas_s03_MS_Result retorno = new sce_ctas_s03_MS_Result();
            if (id_party != null)
            {
                id_party = id_party.Replace("~", "|");
                retorno = context.sce_ctas_s03_MS(id_party).FirstOrDefault();
            }
            return retorno;

        }

        public IList<AbonoCargoResultDTO> pro_sce_abo_car_s03_MS(string centroCosto, string CodigoUsuario)
        {
            IList<AbonoCargoResultDTO> result = context.Database.SqlQuery<AbonoCargoResultDTO>("exec pro_sce_abo_car_s03_MS @centro_costo, @codigo_usuario",
                new SqlParameter("centro_costo", centroCosto), new SqlParameter("codigo_usuario", CodigoUsuario)).ToList<AbonoCargoResultDTO>();

            foreach (AbonoCargoResultDTO ac in result)
            {
                AplicarTrimATodasLasPropiedadesDeAbonoCargo(ac);
            }

            return result;
        }

        public IList<AbonoCargoResultDTO> pro_sce_rev_abocar_s03_MS(string centroCosto, string CodigoUsuario)
        {
            IList<AbonoCargoResultDTO> result = context.Database.SqlQuery<AbonoCargoResultDTO>("exec pro_sce_rev_abocar_s03_MS @centro_costo, @codigo_usuario",
                new SqlParameter("centro_costo", centroCosto), new SqlParameter("codigo_usuario", CodigoUsuario)).ToList<AbonoCargoResultDTO>();

            foreach (AbonoCargoResultDTO ac in result)
            {
                AplicarTrimATodasLasPropiedadesDeAbonoCargo(ac);
            }

            return result;
        }

        //este metodo se podrá obviar cuando se pueda hacer andar el interceptor StringTrimmer para EF
        private void AplicarTrimATodasLasPropiedadesDeAbonoCargo(AbonoCargoResultDTO ac)
        {
            ac.cod_dh = ac.cod_dh.Trim();
            ac.cod_ext = ac.cod_ext.Trim();
            ac.cod_prod = ac.cod_prod.Trim();
            ac.cod_swf = ac.cod_swf.Trim();
            ac.cod_trx_cosmos = ac.cod_trx_cosmos.Trim();
            ac.cod_trx_fc = ac.cod_trx_fc.Trim();
            ac.codcct = ac.codcct.Trim();
            ac.codesp = ac.codesp.Trim();
            ac.codofi = ac.codofi.Trim();
            ac.codope = ac.codope.Trim();
            ac.codpro = ac.codpro.Trim();
            ac.data1 = ac.data1 != null ? ac.data1.Trim() : string.Empty;
            ac.data2 = ac.data2 != null ? ac.data2.Trim() : string.Empty;
            ac.data3 = ac.data3 != null ? ac.data3.Trim() : string.Empty;
            ac.data4 = ac.data4 != null ? ac.data4.Trim() : string.Empty;
            ac.data5 = ac.data5 != null ? ac.data5.Trim() : string.Empty;
            ac.moneda = ac.moneda.Trim();
            ac.nemcta = ac.nemcta.Trim();
            ac.nomcli = ac.nomcli.Trim();
            ac.numcct = ac.numcct.Trim();
            ac.operacion = ac.operacion.Trim();
            ac.tip_cta = ac.tip_cta.Trim();
            ac.trx_id = (!String.IsNullOrEmpty(ac.trx_id) ? ac.trx_id.Trim() : null);
        }

        public IList<tbl_sce_fts> pro_sce_abo_car_s02_MS(AbonoCargoResultDTO ac)
        {
            return context.pro_sce_abo_car_s02_MS(ac.codcct, ac.codpro, ac.codesp, ac.codofi, ac.codope, ac.nroimp).ToList();
        }



        public int? pro_sce_rev_abocar_s02_MS_OpcionCount(AbonoCargoResultDTO ac)
        {
            int opcionCount = 1;
            ObjectResult<int?> result = context.pro_sce_rev_abocar_s02_MS(ac.codcct, ac.codpro, ac.codesp, ac.codofi, ac.codope, ac.trx_id, opcionCount);
            return result.FirstOrDefault();
        }

        public string pro_sce_rev_abocar_s02_MS_OpcionIdTrx(AbonoCargoResultDTO ac)
        {
            int opcionIdTrx = 2;

            return context.Database.SqlQuery<string>("exec pro_sce_rev_abocar_s02_MS @gb_codcct, @gb_codpro, @gb_codesp, @gb_codofi, @gb_codope, @gb_trxid, @gb_opcion",
                new SqlParameter("gb_codcct", ac.codcct),
                new SqlParameter("gb_codpro", ac.codpro),
                new SqlParameter("gb_codesp", ac.codesp),
                new SqlParameter("gb_codofi", ac.codofi),
                new SqlParameter("gb_codope", ac.codope),
                new SqlParameter("gb_trxid", ac.trx_id),
                new SqlParameter("gb_opcion", opcionIdTrx)
                ).FirstOrDefault<string>();
        }

        public void pro_sce_rev_abocar_u01_MS(AbonoCargoResultDTO ac, out string codigo, out string mensaje)
        {
            ObjectParameter codigoParam = new ObjectParameter("ls_codigo", typeof(string));
            ObjectParameter mensajeParam = new ObjectParameter("ls_mensaje", typeof(string));
            context.pro_sce_rev_abocar_u01_MS(ac.codcct, ac.codpro, ac.codesp, ac.codofi, ac.codope, ac.trx_id, codigoParam, mensajeParam);

            codigo = (string)codigoParam.Value;
            mensaje = (string)mensajeParam.Value;
        }

        public void pro_sce_rev_abocar_u02_MS(AbonoCargoResultDTO ac, out string codigo, out string mensaje)
        {
            SqlParameter codigoParam = new SqlParameter("ls_codigo", System.Data.SqlDbType.Char, 3);
            SqlParameter mensajeParam = new SqlParameter("ls_mensaje", System.Data.SqlDbType.Char, 250);
            codigoParam.Direction = System.Data.ParameterDirection.Output;
            mensajeParam.Direction = System.Data.ParameterDirection.Output;

            this.EjecutarSPConRetornoSinTransaccion("pro_sce_rev_abocar_u02_MS", String.Empty, new List<string> { ac.codcct, ac.codpro, ac.codesp, ac.codofi, ac.codope, ac.fecmov.ToString("yyyy-MM-dd"), ac.nrorpt.ToString(), ac.nroimp.ToString() }, new List<SqlParameter> { codigoParam, mensajeParam });

            codigo = (string)codigoParam.Value;
            mensaje = (string)mensajeParam.Value;
        }

        public void pro_sce_abo_car_u01_MS(AbonoCargoResultDTO ac, string respuestaTransaccion, string rutaisPersonaInyecta, out string codigo, out string mensaje)
        {
            ObjectParameter codigoParam = new ObjectParameter("ls_codigo", typeof(string));
            ObjectParameter mensajeParam = new ObjectParameter("ls_mensaje", typeof(string));
            context.pro_sce_abo_car_u01_MS(ac.codcct, ac.codpro, ac.codesp, ac.codofi, ac.codope, ac.nrorpt, ac.fecmov, ac.nroimp, respuestaTransaccion.ToUpper(), rutaisPersonaInyecta, codigoParam, mensajeParam);

            codigo = (string)codigoParam.Value;
            mensaje = (string)mensajeParam.Value;
        }

        public void pro_sce_abo_car_u02_MS(AbonoCargoResultDTO ac, out string codigo, out string mensaje)
        {
            ObjectParameter codigoParam = new ObjectParameter("ls_codigo", typeof(string));
            ObjectParameter mensajeParam = new ObjectParameter("ls_mensaje", typeof(string));
            context.pro_sce_abo_car_u02_MS(ac.codcct, ac.codpro, ac.codesp, ac.codofi, ac.codope, ac.trx_id, codigoParam, mensajeParam);

            codigo = (string)codigoParam.Value;
            mensaje = (string)mensajeParam.Value;
        }

        public IList<sce_usr> sce_usr_s10_MS(string centroCosto, string codigoSupervisor)
        {
            return context.sce_usr_s10_MS(centroCosto, codigoSupervisor).Select(n => new sce_usr()
            {
                cent_costo = n.cent_costo,
                cent_super = n.cent_super,
                ciudad = n.ciudad,
                comuna = n.comuna,
                delegada = n.delegada,
                direccion = n.direccion,
                fax = n.fax,
                id_especia = n.id_especia,
                id_super = n.id_super,
                jerarquia = n.jerarquia,
                nombre = n.nombre,
                ofic_orige = n.ofic_orige,
                rut = n.rut,
                seccion = n.seccion,
                swift = n.swift,
                telefono = n.telefono
            }).ToList();
        }

        public sce_plrm_s03_MS_Result sce_plrm_s03_MS(string numdec, string FecDec, string CodPag, string Party)
        {
            return context.sce_plrm_s03_MS(numdec, Convert.ToDateTime(FecDec), Convert.ToDecimal(CodPag), Party).FirstOrDefault();
        }


        /// <summary>
        /// Carga los destinatarios posibles
        /// </summary>
        /// <returns></returns>
        public IList<sce_tdme> sce_tdme_s01_MS()
        {
            return context.Database.SqlQuery<sce_tdme>("exec sce_tdme_s01_MS").ToList();
        }

        /// <summary>
        /// Carga origen via destino
        /// </summary>
        /// <param name="cta_nor"></param>
        /// <param name="cta_cos"></param>
        /// <returns></returns>
        public List<tbl_sce_ovd_ft> pro_sce_ovd_ft_s01_MS(int cta_nor, int cta_cos)
        {
            return EjecutarSP<tbl_sce_ovd_ft>("pro_sce_ovd_ft_s01_MS", cta_nor.ToString(), cta_cos.ToString());
        }

        public string pro_sce_parametros_s01_MS(short moneda)
        {
            return EjecutarSP<string>("pro_sce_parametros_s01_MS", "APLI_DES/ORI", moneda.ToString()).FirstOrDefault() ?? String.Empty;
        }

        public string pro_sce_parametros_s01_MS(string Cod_parametro, short moneda)
        {
            return context.pro_sce_parametros_s01_MS(Cod_parametro, moneda).FirstOrDefault();
        }

        public List<sce_cta_s01_1_MS_Result> sce_cta_s01_1_MS(string ctaNem)
        {
            return EjecutarSP<sce_cta_s01_1_MS_Result>("sce_cta_s01_1_MS", ctaNem);
        }

        public int pro_sce_codtran_i01_MS(ref string lc_retorno, ref string lc_mensaje, params string[] parameters)
        {
            ObjectParameter par1 = new ObjectParameter("lc_retorno", typeof(String));
            ObjectParameter par2 = new ObjectParameter("lc_mensaje", typeof(String));
            string codcct = parameters[0];
            string codpro = parameters[1];
            string codesp = parameters[2];
            string codofi = parameters[3];
            string codope = parameters[4];
            int nrorpt = int.Parse(parameters[5]);
            DateTime fecmov = DateTime.Parse(parameters[6]);
            int nro_trx = int.Parse(parameters[7]);
            string cod_dh = parameters[8];
            string data1 = parameters[9];
            string data2 = parameters[10];
            string data3 = parameters[11];
            string data4 = parameters[12];
            string data5 = parameters[13];
            int nroimp = int.Parse(parameters[14]);
            int cant = context.pro_sce_codtran_i01_MS(codcct, codpro, codesp, codofi, codope, nrorpt, fecmov, nro_trx, cod_dh, data1, data2, data3, data4, data5, nroimp, par1, par2);
            lc_retorno = (string)par1.Value;
            lc_mensaje = (string)par2.Value;
            return cant;
        }

        public int pro_sce_cvd_ft_i01_MS(ref string lc_retorno, ref string lc_mensaje, params string[] parameters)
        {
            ObjectParameter par1 = new ObjectParameter("lc_retorno", typeof(String));
            ObjectParameter par2 = new ObjectParameter("lc_mensaje", typeof(String));
            string codcct = parameters[0];
            string codpro = parameters[1];
            string codesp = parameters[2];
            string codofi = parameters[3];
            string codope = parameters[4];
            string tipcvd = parameters[5];
            bool Valida_Iny = bool.Parse(parameters[6]);
            int cant = context.pro_sce_cvd_ft_i01_MS(codcct, codpro, codesp, codofi, codope, tipcvd, Valida_Iny, par1, par2);
            lc_retorno = (string)par1.Value;
            lc_mensaje = (string)par2.Value;
            return cant;
        }

        public int pro_sce_relacion_i01_MS(ref string lc_codigo, ref string lc_mensaje, params string[] parameters)
        {
            ObjectParameter par1 = new ObjectParameter("ls_codigo", typeof(String));
            ObjectParameter par2 = new ObjectParameter("ls_mensaje", typeof(String));
            string codcct = parameters[0];
            string codpro = parameters[1];
            string codesp = parameters[2];
            string codofi = parameters[3];
            string codope = parameters[4];
            string XREF = parameters[5]; //XREF
            string transaction_id = parameters[6];
            int moneda = int.Parse(parameters[7]); //Moneda COSMOS
            decimal monto = Format.StringToDecimal(parameters[8].Replace(".", ",")); //Monto
            int nroimp = int.Parse(parameters[9]);
            int auto_manual = int.Parse(parameters[10]);
            DateTime fecing = DateTime.Parse(parameters[11]);
            string fecha2 = parameters[12];
            long contract_reference = long.Parse(parameters[13]);
            int cant = context.pro_sce_relacion_i01_MS(codcct, codpro, codesp, codofi, codope, XREF, transaction_id, moneda, monto, nroimp, auto_manual, fecing, fecha2, contract_reference, par1, par2);
            lc_codigo = (string)par1.Value;
            lc_mensaje = (string)par2.Value;
            return cant;
        }

        public int pro_sce_relacion_i01_MS(AbonoCargoResultDTO ac)
        {
            ObjectParameter par1 = new ObjectParameter("ls_codigo", typeof(String)); //no me intersean pero los tengo que pasar igual
            ObjectParameter par2 = new ObjectParameter("ls_mensaje", typeof(String));

            string XREF = String.Empty;
            int manual = 0;
            int cant = context.pro_sce_relacion_i01_MS(
                ac.codcct, ac.codpro, ac.codesp, ac.codofi, ac.codope, XREF, ac.trx_id, ac.codmon, ac.mtomcd, ac.nroimp, manual, ac.fecmov,
                String.Empty, ac.nrorpt, par1, par2);
            return cant;
        }

        public string pro_sce_prty_i06_MS(string id_party, Nullable<bool> borrado, Nullable<byte> tipo_party,
            Nullable<short> flag, Nullable<byte> clasificac, Nullable<bool> tiene_rut, string rut, string crea_costo,
            string crea_user, Nullable<bool> multiple, string cod_ofieje, string cod_eject, string cod_acteco,
            string clase_ries, Nullable<short> cod_bco, Nullable<bool> tasa_libor, Nullable<bool> tasa_prime,
            Nullable<double> spread, string swift, Nullable<decimal> plaza_alad, string ejec_corre, Nullable<short> flagins,
            Nullable<decimal> insgen_imp, Nullable<decimal> insgen_exp, Nullable<decimal> insgen_ser,
            Nullable<decimal> inscob_imp, Nullable<decimal> inscob_exp, Nullable<decimal> inscre_imp, Nullable<decimal> inscre_exp,
            Nullable<decimal> id_nombre, string razon_soci, string contacto, Nullable<decimal> id_dir, string direccion,
            string comuna, string estado, string ciudad, string pais, string telefono, string fax, string telex,
            Nullable<decimal> envio_sce, Nullable<decimal> recibe_sce, string cod_postal, string cas_postal,
            string cas_banco, string email, Nullable<System.DateTime> fecha, string cuenta, Nullable<decimal> moneda,
            out string ls_retorno, out string ls_mensaje)
        {
            ObjectParameter retorno = new ObjectParameter("ls_retorno", ""),
                mensaje = new ObjectParameter("ls_mensaje", "");

            string ret = context.pro_sce_prty_i06_MS(id_party, borrado, tipo_party, flag, clasificac, tiene_rut, rut, crea_costo,
                crea_user, multiple, cod_ofieje, cod_eject, cod_acteco, clase_ries, cod_bco, tasa_libor, tasa_prime, spread,
                swift, plaza_alad, ejec_corre, flagins, insgen_imp,
                insgen_exp, insgen_ser, inscob_imp, inscob_exp, inscre_imp, inscre_exp, id_nombre, razon_soci, contacto, id_dir,
                direccion, comuna, estado, ciudad, pais, telefono, fax, telex, envio_sce, recibe_sce, cod_postal, cas_postal,
                cas_banco, email, fecha, cuenta, moneda, retorno, mensaje).FirstOrDefault();

            ls_retorno = retorno.Value as string;
            ls_mensaje = mensaje.Value as string;

            return ret;
        }

        //public string sce_mcd_s15_MS(string codcct, string codpro, string codesp, string codofi, string codope, string codanu, string nrorpt, string fecmov)
        //{
        //    try
        //    {
        //        return EjecutarSP<string>("sce_mcd_s15_MS", codcct, codpro, codesp, codofi, codope, codanu,
        //                                             nrorpt.ToString(), fecmov.ToString()).FirstOrDefault();
        //    }
        //    catch (Exception e)
        //    {
        //        return String.Empty;
        //    }

        //}

        //para el put
        public int sce_cov_u01_MS(string codcct, string codpro, string codesp, string codofi, string codope, string estado)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_cov_u01_MS", "", codcct, codpro, codesp, codofi, codope, estado);
                return (int)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_pli_u02_MS(string codcct, string codpro, string codesp, string codofi, string codope, string codanu, decimal? estado)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_pli_u02_MS", "", codcct, codpro, codesp, codofi, codope, codanu, estado.ToString());

                return (int)res;
            }
            catch (Exception e)
            {
                return 9;
            }

        }

        public int sce_vvi_u02_MS(string codcct, string codpro, string codesp, string codofi, string codope, string codanu, decimal? estado)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_vvi_u02_MS", String.Empty, codcct, codpro, codesp, codofi, codope, codanu, estado.ToString());
                return (int)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_chq_u02_MS(string codcct, string codpro, string codesp, string codofi, string codope, string codanu, decimal? estado)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetornoSinTransaccion
                    ("sce_chq_u02_MS", String.Empty, new List<string> { codcct, codpro, codesp, codofi, codope, codanu, estado.ToString() });
                return (int)res;
            }
            catch (Exception e)
            {
                return 9;
            }

        }

        public int sce_swf_u01_MS(string codcct, string codpro, string codesp, string codofi, string codope, string codanu, decimal? estado)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_swf_u01_MS", String.Empty, codcct, codpro, codesp, codofi, codope, codanu, estado.ToString());
                return (int)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_xanu_u02_MS(string codcct, string codpro, string codesp, string codofi, string codope, string codanu, decimal? estado)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_xanu_u02_MS", String.Empty, codcct, codpro, codesp, codofi, codope, codanu, estado.ToString());
                return (int)res;

            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_xplv_u02_MS(string codcct, string codpro, string codesp, string codofi, string codope, string codanu, decimal? estado)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_xplv_u02_MS", String.Empty, codcct, codpro, codesp, codofi, codope, codanu, estado.ToString());
                return (int)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_mch_u02_MS(decimal? nrorpt, string sfecmov, decimal? estado)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_mch_u02_MS", "", nrorpt.ToString(), sfecmov, estado.ToString());
                return (int)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public IList<sce_mcd> sce_mcd_s07_MS(int nroReporte, DateTime fecha)
        {
            List<sce_mcd> resultado = new List<sce_mcd>();

            try
            {
                DbCommand commandExecSP = context.Database.Connection.CreateCommand();
                commandExecSP.CommandText = "exec sce_mcd_s07_MS   @nroReporte, @fecha";
                commandExecSP.Parameters.Add(new SqlParameter("@nroReporte", nroReporte));
                commandExecSP.Parameters.Add(new SqlParameter("@fecha", fecha));

                context.Database.Connection.Open();
                DbDataReader reader = commandExecSP.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sce_mcd lineaReporte = GetLineaContabilidadFromDataReader(reader);
                        resultado.Add(lineaReporte);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!ExceptionPolicy.HandleException(ex, "PoliticaDALFundTransfer")) throw;
            }
            finally
            {
                context.Database.Connection.Close();
            }

            return resultado;
        }

        private sce_mcd GetLineaContabilidadFromDataReader(DbDataReader reader)
        {
            int col = 0;
            sce_mcd lineaReporte = new sce_mcd()
            {
                //nroimp = reader.GetDecimal(col++),
                codmon = reader.GetDecimal(col++),
                nemcta = reader.GetString(col++).Trim(),
                nemmon = reader.GetString(col++).Trim(),
                numcta = reader.GetString(col++).Trim(),
                idncta = reader.GetDecimal(col++),
                cod_dh = reader.GetString(col++).Trim(),
                mtomcd = reader.GetDecimal(col++),
                prtcli = reader.GetString(col++).Trim(),
                rutcli = reader.GetString(col++).Trim(),
                swibco = reader.GetString(col++).Trim(),
                numcct = reader.GetString(col++).Trim(),
                ofides = reader.GetDecimal(col++),
                numpar = reader.GetDecimal(col++),
                tipmov = reader.GetDecimal(col++),
                nroref = reader.GetString(col++).Trim(),
                tipcam = reader.GetDecimal(col++),
                fecven = reader.GetDateTime(col++)
            };

            return lineaReporte;
        }

        public IList<sce_dfc> GetDescripcionesFuncionesContables()
        {
            return context.Set<sce_dfc>().ToList();
        }

        //public int sce_arb_i01_MS(string codcct, string codpro, string codesp, string codofi, string codope, decimal? nrocor, decimal? estado, decimal? codpai, decimal? mndcom, decimal? mndvta, decimal? mtocom, decimal? mtovta, decimal? prdarb, decimal? tipcam, decimal? mtodol, decimal? mtopes, bool? conven)
        public int sce_arb_i01_MS(string codcct, string codpro, string codesp, string codofi, string codope, string nrocor, string estado, string codpai, string mndcom, string mndvta, string mtocom, string mtovta, string prdarb, string tipcam, string mtodol, string mtopes, string conven)
        {
            try
            {
                decimal res = 9;
                //ReadQuerySPTransaction((reader) =>
                //{
                //    if (reader.Read())
                //    {
                //        res = reader.GetDecimal(0);
                //    }
                //    else
                //    {
                //        res = 9;
                //    }
                //}, "sce_arb_i01_MS", codcct, codpro, codesp, codofi, codope, nrocor.ToString(), estado.ToString(), codpai.ToString(), mndcom.ToString(), mndvta.ToString(), mtocom.ToString(), mtovta.ToString(), prdarb.ToString(), tipcam.ToString(), mtodol.ToString(), mtopes.ToString(), conven.Value ? "1" : "0");
                //return (int)res;
                res = EjecutarSPConRetorno("sce_arb_i01_MS", "", codcct, codpro, codesp, codofi, codope, nrocor, estado, codpai, mndcom, mndvta, mtocom, mtovta, prdarb, tipcam, mtodol, mtopes, conven);
                return (int)res;

            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_mcd_s14_MS(string codcct, string codpro, string codesp, string codofi, string codope, decimal? tipope)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_mcd_s14_MS", "", codcct, codpro, codesp, codofi, codope, tipope.ToString());
                return (int)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_mcd_s15_MS(string codcct, string codpro, string codesp, string codofi, string codope, string codanu, decimal? nrorpt, string sfecmov)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_mcd_s15_MS", "", codcct, codpro, codesp, codofi, codope, codanu, nrorpt.ToString(), sfecmov);
                return (short)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_vex_i01_MS(string codcct, string codpro, string codesp, string codofi, string codope, string estado, string codmnd, string tipcam, string mtoliq, string mtoinf, string mtoest)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_vex_i01_MS", String.Empty, codcct, codpro, codesp, codofi, codope, estado, codmnd, tipcam, mtoliq, mtoinf, mtoest);
                return (short)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_xprt_d01_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            try
            {
                int res = 9;
                res = EjecutarSPConRetorno("sce_xprt_d01_MS", String.Empty, codcct, codpro, codesp, codofi, codope);
                return (short)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        /// <summary>
        /// Listado de Planillas Visibles de Exportación en pantalla principal de Administración de Planillas
        /// </summary>
        /// <param name="CentroCosto">Centro de Costo (ej. 714)</param>
        /// <param name="CodigoUsuario">Código de Usuario (ej. 49)</param>
        /// <param name="FechaIngreso">Fecha de Ingreso (ej. 30-4-2014)</param>
        /// <returns></returns>
        public IQueryable<sce_xplv_s08_MS_Result> sce_xplv_s08(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return context.sce_xplv_s08_MS(CentroCosto, CodigoUsuario, FechaIngreso).AsQueryable();
        }

        /// <summary>
        /// Retorna listado de Planillas Visibles de Importaciones Endosadas
        /// </summary>
        /// <param name="CentroCosto"></param>
        /// <param name="CodigoUsuario"></param>
        /// <returns></returns>
        public IQueryable<sce_plan_s07_MS_Result> sce_plan_s07(string CentroCosto, string CodigoUsuario)
        {
            return context.sce_plan_s07_MS(CentroCosto, CodigoUsuario).AsQueryable();
        }


        /// <summary>
        /// Obtiene dirección del party
        /// </summary>
        /// <param name="partyId"></param>
        /// <param name="nombreId"></param>
        /// <returns></returns>
        public IQueryable<string> sce_dad_s04(string partyId, byte nombreId, string tipo)
        {
            return context.sce_dad_s04_MS(partyId, nombreId, tipo).AsQueryable();
        }

        /// <summary>
        /// Obtiene razón social del party
        /// </summary>
        /// <param name="partyId"></param>
        /// <param name="nombreId"></param>
        /// <returns></returns>
        public IQueryable<string> sce_rsa_s03(string partyId, int nombreId)
        {
            return context.sce_rsa_s03_MS(partyId, nombreId).AsQueryable();
        }

        /// <summary>
        /// Obtiene plazas del Banco Central
        /// </summary>       
        /// <returns></returns>
        public IQueryable<sgt_pbc_s01_Result> sgt_pbc_s01()
        {
            return context.sgt_pbc_s01().AsQueryable();
        }

        /// <summary>
        /// Obtiene Aduana
        /// </summary>
        /// <returns></returns>
        public IQueryable<sce_adn_s01_Result> sce_adn_s01()
        {
            return context.sce_adn_s01().AsQueryable();
        }

        /// <summary>
        /// Obtiene estructura de la planilla que tiene el código de país
        /// </summary>
        /// <param name="numeroPlanilla">Número de planilla</param>
        /// <param name="fechaPlanilla">Fecha de planilla</param>
        /// <returns></returns>
        public IQueryable<sce_plia_s01_MS_Result> sce_plia_s01(string numeroPlanilla, DateTime fechaPlanilla)
        {
            return context.sce_plia_s01_MS(numeroPlanilla, fechaPlanilla).AsQueryable();
        }

        /// <summary>
        /// Obtiene feriados
        /// </summary>        
        /// <returns></returns>
        public IQueryable<DateTime?> sce_fer_s01()
        {
            return context.sce_fer_s01_MS().AsQueryable();
        }

        /// <summary>
        /// Guarda una planilla de ingreso visible de exportacion
        /// </summary>        
        /// <returns></returns>
        public void sce_xplv_w03(string numeroPresentacion, DateTime fechaAnterior, DateTime fechaPresentacion,
            decimal montoBruto, decimal comisiones, decimal otrosGastos, decimal tipoCambio, string observaciones)
        {
            context.sce_xplv_w03_MS(numeroPresentacion, fechaAnterior, fechaPresentacion,
                montoBruto, comisiones, otrosGastos, tipoCambio, observaciones);
        }

        /// <summary>
        /// Busca una referencia BAE BCH 
        /// </summary>   
        /// <param name="referenciaBAE">Referencia BAE</param>
        /// <returns>sce_ref_s01_MS_Result</returns>
        public IQueryable<sce_ref_s01_MS_Result> sce_ref_s01(string referenciaBAE)
        {
            return context.sce_ref_s01_MS(referenciaBAE).AsQueryable();
        }

        /// <summary>
        /// Obtiene las cartas relacionadas a la operacion
        /// </summary>
        /// <param name="codcct">Centro de costo</param>
        /// <param name="codpro">Codigo del producto</param>
        /// <param name="codesp">Codigo Especialista</param>
        /// <param name="codofi">Codigo Oficina</param>
        /// <param name="codope">Codigo Operacion</param>
        /// <returns>Listado de cartas relacionadas a la operacion</returns>
        public IQueryable<sce_xdoc_s05_MS_Result> Sce_xdoc_s05_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_xdoc_s05_MS(codcct, codpro, codesp, codofi, codope).AsQueryable();
        }

        /// <summary>
        /// Obtiene la cabecera de la contabilidad relacionada a la operacion
        /// </summary>
        /// <param name="codcct">Centro de costo</param>
        /// <param name="codpro">Codigo del producto</param>
        /// <param name="codesp">Codigo Especialista</param>
        /// <param name="codofi">Codigo Oficina</param>
        /// <param name="codope">Codigo Operacion</param>
        /// <returns>Cabecera contabilidad relacionada a la operacion</returns>
        public IQueryable<sce_mch_s16_MS_Result> Sce_mch_s16_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_mch_s16_MS(codcct, codpro, codesp, codofi, codope).AsQueryable();
        }

        /// <summary>
        /// Obtiene el detalle de la contabilidad relacionada a la operacion
        /// </summary>
        /// <param name="codcct">Centro de costo</param>
        /// <param name="codpro">Codigo del producto</param>
        /// <param name="codesp">Codigo Especialista</param>
        /// <param name="codofi">Codigo Oficina</param>
        /// <param name="codope">Codigo Operacion</param>
        /// <returns>Detalle contabilidad relacionada a la operacion</returns>
        public IQueryable<sce_mcd_s78_MS_Result> Sce_mcd_s78_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_mcd_s78_MS(codcct, codpro, codesp, codofi, codope).AsQueryable();
        }

        /// <summary>
        /// Obtiene planillas de importacion
        /// </summary>
        /// <param name="codcct">Centro de costo</param>
        /// <param name="codpro">Codigo del producto</param>
        /// <param name="codesp">Codigo Especialista</param>
        /// <param name="codofi">Codigo Oficina</param>
        /// <param name="codope">Codigo Operacion</param>
        /// <returns>Listado de planillas de importacion relacionadas a la operacion</returns>
        public IQueryable<sce_plan_s19_MS_Result> Sce_plan_s19_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_plan_s19_MS(codcct, codpro, codesp, codofi, codope).AsQueryable();
        }

        /// <summary>
        /// Obtiene planillas invisibles
        /// </summary>
        /// <param name="codcct">Centro de costo</param>
        /// <param name="codpro">Codigo del producto</param>
        /// <param name="codesp">Codigo Especialista</param>
        /// <param name="codofi">Codigo Oficina</param>
        /// <param name="codope">Codigo Operacion</param>
        /// <returns>Listado de planillas invisibles relacionadas a la operacion</returns>
        public IQueryable<sce_pli_s08_MS_Result> Sce_pli_s08_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_pli_s08_MS(codcct, codpro, codesp, codofi, codope).AsQueryable();
        }

        /// <summary>
        /// Obtiene planillas de anulacion
        /// </summary>
        /// <param name="codcct">Centro de costo</param>
        /// <param name="codpro">Codigo del producto</param>
        /// <param name="codesp">Codigo Especialista</param>
        /// <param name="codofi">Codigo Oficina</param>
        /// <param name="codope">Codigo Operacion</param>
        /// <returns>Listado de planillas de anulacion relacionadas a la operacion</returns>
        public IQueryable<sce_xanu_s04_MS_Result> Sce_xanu_s04_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_xanu_s04_MS(codcct, codpro, codesp, codofi, codope).AsQueryable();
        }

        /// <summary>
        /// Obtiene datos del usuario
        /// </summary>
        /// <param name="cencos">Centro Costo Original</param>
        /// <param name="codusr">Codigo Usuario Original</param>
        /// <returns>Usuario</returns>
        public IQueryable<tbl_datos_usuario_s01_MS_Result> Tbl_datos_usuario_s01_MS(string cencos, string codusr)
        {
            return context.tbl_datos_usuario_s01_MS(cencos, codusr).AsQueryable();
        }

        /// <summary>
        /// Obtiene planillas de exportación
        /// </summary>
        /// <param name="codcct">Centro de costo</param>
        /// <param name="codpro">Codigo del producto</param>
        /// <param name="codesp">Codigo Especialista</param>
        /// <param name="codofi">Codigo Oficina</param>
        /// <param name="codope">Codigo Operacion</param>
        /// <returns>Listado de planillas de exportación relacionadas a la operacion</returns>
        public IQueryable<sce_xplv_s12_MS_Result> Sce_xplv_s12_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_xplv_s12_MS(codcct, codpro, codesp, codofi, codope).AsQueryable();
        }

        /// <summary>
        /// Obtiene datos del participante
        /// </summary>
        /// <param name="rut">Rut del Participante</param>
        /// <param name="idParty">Id del Participante</param>
        /// <returns>Participante</returns>
        public IQueryable<sce_prty_s10_MS_Result> Sce_prty_s10_MS(string idparty, string rut)
        {
            return context.sce_prty_s10_MS(idparty, rut).AsQueryable();
        }

        public IQueryable<sce_mcd_s20_MS_Result> sce_mcd_s20(DateTime fecha, string numeroCuenta)
        {
            return context.sce_mcd_s20_MS(fecha, numeroCuenta).AsQueryable();
        }
        //public int sce_mcd_s20(DateTime fecha, string numeroCuenta)
        //{
        //    return context.sce_mcd_s20_MS(fecha, numeroCuenta);
        //}

        /// <summary>
        /// Obtiene listado de planillas de importación o exportación (I o E) en un rango de fechas 
        /// </summary>
        /// <param name="deDonde">Referencia BAE</param>
        /// <param name="fechaInicio">Referencia BAE</param>
        /// <param name="fechaTermino">Referencia BAE</param>
        /// <returns>Estructura sce_pldc_s01_MS_Result</returns>
        public IQueryable<sce_pldc_s01_MS_Result> sce_pldc_s01(string deDonde, DateTime? fechaInicio, DateTime? fechaTermino)
        {
            return context.sce_pldc_s01_MS(deDonde, fechaInicio, fechaTermino).AsQueryable();
        }

        /// <summary>
        /// Listado de Movimientos de Canje (centro de costo 826)
        /// </summary>
        /// <param name="fechaMovimientos">Fecha para extraer movimientos</param>
        /// <returns></returns>
        public IQueryable<sce_mcd_s56_MS_Result> Sce_Mcd_S56(DateTime fechaMovimientos)
        {
            return context.sce_mcd_s56_MS(fechaMovimientos).AsQueryable();
        }
        //public int Sce_Mcd_S56(DateTime fechaMovimientos)
        //{
        //    return context.sce_mcd_s56_MS(fechaMovimientos);
        //}

        /// <summary>
        /// Guarda (actualiza) una planilla de visible de importación
        /// </summary>        
        /// <returns></returns>
        public void sce_plan_u12(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa,
            string codigoCobranza, decimal? numeroPlanilla, DateTime? fechaVentaAntigua, DateTime? fechaVenta, string numeroConocimientoEmbarque,
            DateTime? fechaConocimientoEmbarque, decimal? formaPago, decimal? codigoPais, string nombrePais, DateTime? fechaVencimiento,
            bool? hayCuadroCuotas, decimal? numeroCuotas, decimal? cuota, bool? hayAcuerdos, decimal? numeroAcuerdos, string acuerdoDesde,
            string acuerdoHasta, DateTime? fechaAutorizacionDebito, string numeroDocumentoChile, string numeroDocumentoExtranjero,
            string observaciones, bool? isZonaFranca)
        {
            context.sce_plan_u12_MS(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza,
                numeroPlanilla, fechaVentaAntigua, fechaVenta, numeroConocimientoEmbarque, fechaConocimientoEmbarque, formaPago,
                codigoPais, nombrePais, fechaVencimiento, hayCuadroCuotas, numeroCuotas, cuota, hayAcuerdos, numeroAcuerdos, acuerdoDesde, acuerdoHasta,
                fechaAutorizacionDebito, numeroDocumentoChile, numeroDocumentoExtranjero, observaciones, isZonaFranca);
        }

        /// <summary>
        /// Obtiene planillas estadisticas de importacion
        /// </summary>        
        /// <returns></returns>
        public IQueryable<sce_plan_s04_MS_Result> sce_plan_s04(string codigoCentroCosto, string codigoProducto, string codigoEspecialista,
            string codigoEmpresa, string idCobr, int numeroPlanilla, DateTime fechaVenta)
        {
            return context.sce_plan_s04_MS(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, idCobr, numeroPlanilla, fechaVenta).AsQueryable();
        }

        /// <summary>
        /// Obtiene planillas estadisticas de importacion
        /// </summary>        
        /// <returns></returns>
        public IQueryable<sce_plan_s05_MS_Result> sce_plan_s05(string codigoCentroCosto, string codigoProducto, string codigoEspecialista,
            string codigoEmpresa, string codigoCobranza, int numeroPlanilla)
        {
            return context.sce_plan_s05_MS(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla).AsQueryable();
        }

        /// <summary>
        /// Obtiene planillas estadisticas de importacion
        /// </summary>        
        /// <returns></returns>
        public IQueryable<sce_inpl_s01_MS_Result> sce_inpl_s01(string codigoCentroCosto, string codigoProducto, string codigoEspecialista,
            string codigoOficina, string codigoOperacion, decimal numeroPlanilla)
        {
            return context.sce_inpl_s01_MS(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoOficina, codigoOperacion, numeroPlanilla).AsQueryable();
        }

        /// <summary>
        /// Listado de planillas Visibles de Importacion
        /// </summary>
        /// <param name="fechaVenta">Fecha de venta</param>
        /// <param name="codigoCentroCosto">Centro de Costo</param>
        /// <param name="codigoUsuario">Usuario</param>
        /// <param name="tipo">Tipo</param>
        /// <returns></returns>
        public IQueryable<sce_plan_s03_MS_Result> sce_plan_s03(DateTime? fechaVenta, string codigoCentroCosto, string codigoUsuario, decimal? tipo)
        {
            return context.sce_plan_s03_MS(fechaVenta, codigoCentroCosto, codigoUsuario, tipo).AsQueryable();
        }

        /// <summary>
        /// Inserta nueva declaración de planilla en tabla sce_pldc
        /// </summary>        
        /// <returns></returns>
        public IQueryable<int?> sce_pldc_i01(string numeroPresentacion, DateTime? fechaPresentacion, string tipo, DateTime? fechaActual, string declaracionImportacion, string declaracionExportacion, DateTime? fechaDeclaracion, int codigoAduana, decimal montoDI, decimal interesDI, decimal montoUSD, DateTime? fechaVencimiento)
        {
            return context.sce_pldc_i01_MS(numeroPresentacion, fechaPresentacion, tipo, fechaActual, declaracionImportacion, declaracionExportacion, fechaDeclaracion, codigoAduana, montoDI, interesDI, montoUSD, fechaVencimiento).AsQueryable();
        }

        /// <summary>
        /// Actualiza la fecha de declaración
        /// </summary>     
        /// <param name="fechaActual">Fecha actual</param>
        /// <returns></returns>
        public IQueryable sce_pldc_u01(DateTime? fechaActual)
        {
            return context.sce_pldc_u01_MS(fechaActual).AsQueryable();
        }

        public List<sce_plan_s16_Result> sce_plan_s16(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, int numeroPlanilla, DateTime fechaVenta)
        {
            return context.sce_plan_s16(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta).ToList();
        }

        public IQueryable<sce_plan_s16_MS_Result> sce_plan_s16_MS(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, int numeroPlanilla, DateTime fechaVenta)
        {
            return context.sce_plan_s16_MS(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta).AsQueryable();
        }

        /// <summary>
        /// Eliminar detalle de intereses de una planilla
        /// </summary>
        /// <param name="codigoCentroCosto">codigoCentroCosto</param>
        /// <param name="codigoProducto">Centro de Producto</param>
        /// <param name="codigoEspecialista">Especialista</param>
        /// <param name="codigoEmpresa">Empresa</param>
        /// <param name="codigoCobranza">Codigo Cobranza</param>
        /// <param name="numeroPlanilla">Numero planilla</param>
        /// <returns>int</returns>
        public void sce_inpl_d01(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, decimal numeroPlanilla)
        {
            context.sce_inpl_d01_MS(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla);
        }

        /// <summary>
        /// Guarda detalle de intereses de una planilla
        /// </summary>
        /// <param name="codigoCentroCosto">codigoCentroCosto</param>
        /// <param name="codigoProducto">Centro de Producto</param>
        /// <param name="codigoEspecialista">Especialista</param>
        /// <param name="codigoEmpresa">Empresa</param>
        /// <param name="codigoCobranza">Codigo Cobranza</param>
        /// <param name="numeroPlanilla">Numero planilla</param>
        /// <param name="fechaVenta"></param>
        /// <param name="numeroLineaInteres"></param>
        /// <param name="concepto"></param>
        /// <param name="tipo"></param>
        /// <param name="monto"></param>
        /// <param name="capitalBase"></param>
        /// <param name="codigoBaseAno"></param>
        /// <param name="tasaInteres"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="numeroDias"></param>        
        /// <returns>int</returns>
        public void sce_inpl_w01(string codigoCentroCosto, string codigoProducto, string codigoEspecialista, string codigoEmpresa, string codigoCobranza, decimal numeroPlanilla, DateTime fechaVenta, decimal numeroLineaInteres, decimal concepto, string tipo, decimal monto, decimal capitalBase, decimal codigoBaseAno, decimal tasaInteres, DateTime fechaInicial, DateTime fechaFinal, decimal numeroDias)
        {
            context.sce_inpl_w01_MS(codigoCentroCosto, codigoProducto, codigoEspecialista, codigoEmpresa, codigoCobranza, numeroPlanilla, fechaVenta, numeroLineaInteres, concepto, tipo, monto, capitalBase, codigoBaseAno, tasaInteres, fechaInicial, fechaFinal, numeroDias);
        }

        /// <summary>
        /// Listado de Planillas Invisibles en pantalla principal de Administración de Planillas
        /// </summary>
        /// <param name="CentroCosto">Centro de Costo (ej. 714)</param>
        /// <param name="CodigoUsuario">Código de Usuario (ej. 49)</param>
        /// <param name="FechaIngreso">Fecha de Ingreso (ej. 30-4-2014)</param>
        /// <returns></returns>
        public IQueryable<sce_pli_s05_MS_Result> sce_pli_s05(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return context.sce_pli_s05_MS(CentroCosto, CodigoUsuario, FechaIngreso).AsQueryable();
        }

        /// <summary>
        /// Obtiene datos de una planilla de exportación visible
        /// </summary>
        /// <param name="NumeroPlanilla"></param>
        /// <param name="FechaPlanilla"></param>
        /// <returns></returns>
        public IQueryable<sce_pli_s06_MS_Result> sce_pli_s06(string NumeroPlanilla, DateTime FechaPlanilla)
        {
            return context.sce_pli_s06_MS(NumeroPlanilla, FechaPlanilla).AsQueryable();
        }

        /// <summary>
        /// Actualiza datos de planilla invisible
        /// </summary>
        /// <param name="NumeroPlanilla"></param>
        /// <param name="FechaAnterior"></param>
        /// <param name="FechaPlanilla"></param>
        /// <param name="CodigoPais"></param>
        /// <param name="Observaciones"></param>
        /// <param name="ZonaFranca"></param>
        /// <returns></returns>
        public IQueryable<int?> sce_pli_w06(string NumeroPlanilla, DateTime FechaAnterior, DateTime FechaPlanilla, int CodigoPais, string Observaciones, bool ZonaFranca)
        {
            return context.sce_pli_w06_MS(NumeroPlanilla, FechaAnterior, FechaPlanilla, CodigoPais, Observaciones, ZonaFranca).AsQueryable();
        }

        /// <summary>
        /// Listado de Planillas Anuladas de Exportación en pantalla principal de Administración de Planillas
        /// </summary>
        /// <param name="CentroCosto">Centro de Costo (ej. 714)</param>
        /// <param name="CodigoUsuario">Código de Usuario (ej. 49)</param>
        /// <param name="FechaIngreso">Fecha de Ingreso (ej. 30-4-2014)</param>
        /// <returns></returns>
        public IQueryable<sce_xanu_s01_MS_Result> sce_xanu_s01(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return context.sce_xanu_s01_MS(CentroCosto, CodigoUsuario, FechaIngreso).AsQueryable();
        }

        /// <summary>
        /// Obtiene datos de una planilla anulada
        /// </summary>
        /// <param name="NumeroPresentacion"></param>
        /// <param name="FechaPresentacion"></param>
        /// <returns></returns>
        public IQueryable<sce_xanu_s02_MS_Result> sce_xanu_s02(string NumeroPresentacion, DateTime FechaPresentacion)
        {
            return context.sce_xanu_s02_MS(NumeroPresentacion, FechaPresentacion).AsQueryable();
        }

        /// <summary>
        /// Actualiza datos de planilla anulada
        /// </summary>
        /// <param name="NumeroPresentacion">Número de la planilla</param>
        /// <param name="FechaPresentacion">Fecha actual de la planilla</param>
        /// <param name="NuevaFechaPresentacion">Nueva fecha de presentación</param>
        /// <param name="Observaciones">Observaciones</param>
        /// <returns>Código y mensaje de error</returns>
        public IQueryable<sce_xanu_u03_MS_Result> sce_xanu_u03(string NumeroPresentacion, DateTime FechaPresentacion, DateTime NuevaFechaPresentacion, string Observaciones)
        {
            return context.sce_xanu_u03_MS(NumeroPresentacion, FechaPresentacion, NuevaFechaPresentacion, Observaciones).AsQueryable();
        }

        /// <summary>
        /// Obtiene usuario especialista (asignado a estación; 'CCtUsr')
        /// </summary>
        /// <param name="CentroCosto"></param>
        /// <param name="CodigoUsuario"></param>
        /// <returns></returns>
        public IQueryable<sce_usr_s05_MS_Result> sce_usr_s05_MS(string CentroCosto, string CodigoUsuario)
        {
            return context.sce_usr_s05_MS(CentroCosto, CodigoUsuario).AsQueryable();
        }

        /// <summary>
        /// Obtiene listado de todas las entidades bancarias
        /// </summary>
        /// <returns></returns>
        public IQueryable<sce_bco_s01_MS_Result> Sce_Bco_S01_MS()
        {
            return context.sce_bco_s01_MS().AsQueryable();
        }

        /// <summary>
        /// Obtiene listado de todas las glosas de codigo comercio
        /// </summary>
        /// <returns></returns>
        public IQueryable<sce_tcp_s01_MS_Result> Sce_Tcp_S01()
        {
            return context.sce_tcp_s01_MS().AsQueryable();
        }


        /// <summary>
        /// Reporte de planillas generadas por usuarios en el sistema.
        /// </summary>
        /// <param name="CentroCosto"></param>
        /// <param name="CodigoUsuario"></param>
        /// <param name="FechaIngreso"></param>
        /// <remarks>El procedimiento almacenado actualmente está convirtiendo internamente a datetime con la cultura
        /// del servidor</remarks>
        public IQueryable<sce_gpln_s10_MS_Result> Sce_Gpln_S10(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return context.sce_gpln_s10_MS(CentroCosto, CodigoUsuario, FechaIngreso.ToString("dd'/'MM'/'yyyy", DateTimeFormatInfo.InvariantInfo)).AsQueryable();
        }

        //public int Sce_Gpln_S10(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        //{
        //    return context.sce_gpln_s10_MS(CentroCosto, CodigoUsuario, FechaIngreso.ToString("dd'/'MM'/'yyyy", DateTimeFormatInfo.InvariantInfo));
        //}

        /// <summary>
        /// Listado Resumen de Planillas (Ingreso y Egreso) agregado (montos)
        /// </summary>
        /// <param name="CentroCosto"></param>
        /// <param name="CodigoUsuario"></param>
        /// <param name="FechaIngreso"></param>
        /// <remarks>El procedimiento almacenado actualmente está convirtiendo internamente a datetime con la cultura
        /// del servidor</remarks>
        public IQueryable<sce_gpln_s11_MS_Result> Sce_Gpln_S11(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return context.sce_gpln_s11_MS(CentroCosto, CodigoUsuario, FechaIngreso.ToString("dd'/'MM'/'yyyy", DateTimeFormatInfo.InvariantInfo)).AsQueryable();
        }
        //public int Sce_Gpln_S11(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        //{
        //    return context.sce_gpln_s11_MS(CentroCosto, CodigoUsuario, FechaIngreso.ToString("dd'/'MM'/'yyyy", DateTimeFormatInfo.InvariantInfo));
        //}
        /// <summary>
        /// Reporte de Planillas vs Conversión por Usuario
        /// </summary>
        /// <param name="CentroCosto"></param>
        /// <param name="CodigoUsuario"></param>
        /// <param name="FechaIngreso"></param>
        /// <remarks>El procedimiento almacenado actualmente está convirtiendo internamente a datetime con la cultura
        /// del servidor</remarks>
        public IQueryable<sce_gpln_s12_MS_Result> Sce_Gpln_S12(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        {
            return context.sce_gpln_s12_MS(CentroCosto, CodigoUsuario, FechaIngreso.ToString("dd'/'MM'/'yyyy", DateTimeFormatInfo.InvariantInfo)).AsQueryable();
        }
        //public int Sce_Gpln_S12(string CentroCosto, string CodigoUsuario, DateTime FechaIngreso)
        //{
        //    return context.sce_gpln_s12_MS(CentroCosto, CodigoUsuario, FechaIngreso.ToString("dd'/'MM'/'yyyy", DateTimeFormatInfo.InvariantInfo));
        //}

        /// <summary>
        /// Planillas - Reporte Informe Posición de Cambios
        /// </summary>
        /// <param name="CentroCosto"></param>
        /// <param name="FechaIngreso"></param>
        /// <remarks>El procedimiento almacenado actualmente está convirtiendo internamente a datetime con la cultura
        /// del servidor</remarks>
        public IQueryable<sce_gpln_s13_MS_Result> Sce_Gpln_S13(string CentroCosto, DateTime FechaIngreso)
        {
            return context.sce_gpln_s13_MS(CentroCosto, FechaIngreso.ToString("dd'/'MM'/'yyyy", DateTimeFormatInfo.InvariantInfo)).AsQueryable();
        }
        //public int Sce_Gpln_S13(string CentroCosto, DateTime FechaIngreso)
        //{
        //    return context.sce_gpln_s13_MS(CentroCosto, FechaIngreso.ToString("dd'/'MM'/'yyyy", DateTimeFormatInfo.InvariantInfo));
        //}

        /// <summary>
        /// Planillas - Reporte Movimiento Corresponsales
        /// </summary>
        /// <param name="CentroCosto"></param>
        /// <param name="FechaIngreso"></param>
        public IQueryable<sce_gpln_s16_MS_Result> Sce_Gpln_S16_MS(string CentroCosto, DateTime FechaIngreso)
        {
            return context.sce_gpln_s16_MS(CentroCosto, FechaIngreso).AsQueryable();
        }

        /// <summary>
        /// Selecciona las Planillas sin declaracion de Importacion o Exportacion para la fecha de actualizacion
        /// </summary>
        /// <param name="tipoPlanilla">Indica planilla de importación o exportación ('I' o 'E')</param>
        /// <param name="fechaActualizacion"></param>
        /// <returns></returns>
        public IQueryable<sce_pldc_s02_MS_Result> Sce_Pldc_S02(char tipoPlanilla, DateTime fechaActualizacion)
        {
            return context.sce_pldc_s02_MS(tipoPlanilla.ToString(), fechaActualizacion).AsQueryable();
        }

        public IQueryable<sce_pldc_s03_MS_Result> Sce_Pldc_S03(string tipoPlanilla, string numeroPresentacion, DateTime fechaPresentacion)
        {
            return context.sce_pldc_s03_MS(tipoPlanilla, numeroPresentacion, fechaPresentacion).AsQueryable();
        }

        public void Sce_Plan_U14(decimal numeroPresentacion, DateTime fechaPresentacion)
        {
            context.sce_plan_u14_MS(numeroPresentacion, fechaPresentacion);

        }

        public void Sce_Xplv_U03(string numeroPresentacion, DateTime fechaPresentacion)
        {
            context.sce_xplv_u03_MS(numeroPresentacion, fechaPresentacion);
        }

        public void Sce_Pli_U04(string numeroPresentacion, DateTime fechaPresentacion)
        {
            context.sce_pli_u04_MS(numeroPresentacion, fechaPresentacion);
        }

        public void Sce_Xanu_U04(string numeroPresentacion, DateTime fechaPresentacion)
        {
            context.sce_xanu_u04_MS(numeroPresentacion, fechaPresentacion);
        }

        public int sce_xprt_i01_MS(string codcct, string codpro, string codesp, string codofi, string codope, string posprt, string codprt, string indnom, string inddir, string enoper)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_xprt_i01_MS", String.Empty, codcct, codpro, codesp, codofi, codope, posprt, codprt, indnom, inddir, enoper);
                return (short)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_xdec_u01_MS(string codcct, string codpro, string codesp, string codofi, string codope, decimal? codneg, decimal? codsec, decimal? nrocan)
        {
            try
            {
                int res = 9;
                //ReadQuerySPTransaction((reader) =>
                //{
                //    if (reader.Read())
                //    {
                //        res = reader.GetDecimal(0);
                //    }
                //    else
                //    {
                //        res = 9;
                //    }
                //}, "sce_xdec_u01_MS", codcct, codpro, codesp, codofi, codope, codneg.ToString(), codsec.ToString(), nrocan.ToString());

                res = EjecutarSPConRetorno("sce_xdec_u01_MS", "", codcct, codpro, codesp, codofi, codope, codneg.ToString(), codsec.ToString(), nrocan.ToString());

                //res = context.sce_xdec_u01_MS(codcct, codpro, codesp, codofi, codope, codneg, codsec, nrocan);
                return (short)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_cov_i01_MS(string codcct, string codpro, string codesp, string codofi, string codope, decimal? nrocor, decimal? estado, string tipcov, decimal? codpai, decimal? codmnd, decimal? mtocov, decimal? tipcam, decimal? mtopes, decimal? mtopar, string codtcp, decimal? codoci, string ingegr, bool? conven, string numdec, string fecdec, decimal? codadn, string dienum, string diefec, decimal? diepbc, bool? inddec, string fecdeb, string docnac, string docext)
        {
            try
            {
                decimal res = 9;
                ReadQuerySPTransaction((reader) =>
                {
                    if (reader.Read())
                    {
                        res = reader.GetDecimal(0);
                    }
                    else
                    {
                        res = 9;
                    }
                }, "sce_cov_i01_MS", codcct, codpro, codesp, codofi, codope, nrocor.ToString(), estado.ToString(), tipcov, codpai.ToString(), codmnd.ToString(), mtocov.ToString(), tipcam.ToString(), mtopes.ToString(), mtopar.ToString(), codtcp, codoci.ToString(), ingegr, conven.Value ? "1" : "0", numdec, fecdec, codadn.ToString(), dienum, diefec, diepbc.ToString(), inddec.ToString(), fecdeb, docnac, docext);
                return (short)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int Sce_mts_u01_MS(string codcct, string codpro, string codesp, string codofi, string codope, decimal numneg, decimal tippro, decimal numcpa, decimal numcuo, decimal numcob, decimal id_mensaje, int estado)
        {
            try
            {
                int res = EjecutarSPConRetorno("sce_mts_u01_MS", String.Empty, codcct, codpro, codesp, codofi, codope, numneg.ToString(), tippro.ToString(), numcpa.ToString(), numcuo.ToString(), numcob.ToString(), id_mensaje.ToString(), estado.ToString());
                return res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        public int sce_plan_u07_MS(string cent_costo, string id_product, string id_especia, string id_empresa, string id_cobranz, string num_presen, string estado)
        {
            try
            {
                decimal res = 9;
                res = EjecutarSPConRetorno("sce_plan_u07_MS", String.Empty, cent_costo, id_product, id_especia, id_empresa, id_cobranz, num_presen, estado);
                return (short)res;
            }
            catch (Exception e)
            {
                return 9;
            }
        }

        //***********************************
        public string sce_dad_s04_MS(string IdParty, short IdDireccion, string Tipo)
        {
            return context.sce_dad_s04_MS(IdParty, (byte)IdDireccion, Tipo).FirstOrDefault();
        }

        public sce_mch_s11_MS_Result sce_mch_s11_MS(string Codcct, string Codpro, string Codesp, string Codofi, string Codope)
        {
            return context.sce_mch_s11_MS(Codcct, Codpro, Codesp, Codofi, Codope).FirstOrDefault();
        }

        public sce_anu_u03_MS_Result sce_anu_u03_MS(string Codcct, string Codpro, string Codesp, string Codofi, string Codope)
        {
            //return context.sce_anu_u03_MS(Codcct, Codpro, Codesp, Codofi, Codope).FirstOrDefault();
            List<sce_anu_u03_MS_Result> result = new List<sce_anu_u03_MS_Result>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    result.Add(new sce_anu_u03_MS_Result
                    {
                        Column1 = (int)Utils.GetIntFromDataReader(reader, 0),
                        Column2 = Utils.GetStringFromDataReader(reader, 1)

                    });
                }
            }, "sce_anu_u03_MS", Codcct, Codpro, Codesp, Codofi, Codope);

            return result.FirstOrDefault();
        }

        public sce_anu_u12_MS_Result sce_anu_u12_MS(string Codcct, string Codpro, string Codesp, string Codofi, string Codope)
        {
            //return context.sce_anu_u12_MS(Codcct, Codpro, Codesp, Codofi, Codope).FirstOrDefault();
            List<sce_anu_u12_MS_Result> result = new List<sce_anu_u12_MS_Result>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    result.Add(new sce_anu_u12_MS_Result
                    {
                        Column1 = (int)Utils.GetIntFromDataReader(reader, 0),
                        Column2 = Utils.GetStringFromDataReader(reader, 1)

                    });
                }
            }, "sce_anu_u12_MS", Codcct, Codpro, Codesp, Codofi, Codope);

            return result.FirstOrDefault();
        }

        public IList<sce_mts_s01_MS_Result> sce_mts_s01_MS(string Codcct, string Codpro, string Codesp, string Codofi, string Codope, Nullable<decimal> numneg, Nullable<decimal> tippro, Nullable<decimal> numcpa, Nullable<decimal> numcuo, Nullable<decimal> numcob, Nullable<decimal> estado, string tipgra)
        {
            var result = context.sce_mts_s01_MS(Codcct, Codpro, Codesp, Codofi, Codope, numneg, tippro, numcpa, numcuo, numcob, estado, tipgra).ToList();
            return result == null ? new List<sce_mts_s01_MS_Result>() : result;
            //return context.sce_mts_s01_MS(Codcct, Codpro, Codesp, Codofi, Codope, numneg, tippro, numcpa, numcuo, numcob, estado, tipgra).FirstOrDefault();
        }
        public List<sce_bco_s01_MS_Result> sce_bco_s01_MS()
        {
            var retorno = context.sce_bco_s01_MS().ToList();

            return retorno;
        }

        /// <summary>
        /// Se usa en reverso de operaciones
        /// </summary>
        /// <param name="codcct"></param>
        /// <param name="codpro"></param>
        /// <param name="codesp"></param>
        /// <param name="codofi"></param>
        /// <param name="codope"></param>
        /// <returns></returns>
        public sce_cvd_s06_MS_DTO sce_cvd_s06_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            List<sce_cvd_s06_MS_DTO> result = new List<sce_cvd_s06_MS_DTO>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    result.Add(Get_sce_cvd_s06_MS_FromDataReader(reader, codpro));
                }
            }, "sce_cvd_s06_MS", codcct, codpro, codesp, codofi, codope);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Reader del metodo sce_cvd_s06_MS
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="codpro"></param>
        /// <returns></returns>
        private sce_cvd_s06_MS_DTO Get_sce_cvd_s06_MS_FromDataReader(DbDataReader reader, string codpro)
        {
            int col = 0;
            sce_cvd_s06_MS_DTO rs = new sce_cvd_s06_MS_DTO();

            switch (Convert.ToInt16(codpro))
            {
                case 3:

                    rs.codpro_03 = new sce_cvd_s06_MS_DTO_03();
                    rs.codpro_03.codcct = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_03.codpro = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_03.codesp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_03.codofi = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_03.codope = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_03.fecing = Utils.GetFechaFromDataReader(reader, col++);
                    rs.codpro_03.campo6 = (int)Utils.GetIntFromDataReader(reader, col++);
                    rs.codpro_03.prtcli = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_03.indnomc = Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_03.inddirc = Utils.GetDecimalFromDataReader(reader, col++);
                    break;
                case 5:

                    rs.codpro_05 = new sce_cvd_s06_MS_DTO_05();
                    rs.codpro_05.codcct = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_05.codpro = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_05.codesp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_05.codofi = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_05.codope = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_05.fecing = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                    rs.codpro_05.campo6 = (int)Utils.GetIntFromDataReader(reader, col++);
                    rs.codpro_05.prtcli = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_05.indnomc = Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_05.inddirc = Utils.GetDecimalFromDataReader(reader, col++);


                    break;
                case 6:

                    rs.codpro_06 = new sce_cvd_s06_MS_DTO_06();
                    rs.codpro_06.codcct = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_06.codpro = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_06.codesp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_06.codofi = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_06.codope = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_06.fecing = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                    rs.codpro_06.campo6 = (int)Utils.GetIntFromDataReader(reader, col++);
                    rs.codpro_06.prtcli = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_06.indnomc = Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_06.inddirc = Utils.GetDecimalFromDataReader(reader, col++);

                    break;
                case 7:

                    rs.codpro_07 = new sce_cvd_s06_MS_DTO_07();
                    rs.codpro_07.codcct = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_07.codpro = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_07.codesp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_07.codofi = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_07.codope = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_07.fecing = Utils.GetFechaFromDataReader(reader, col++);
                    rs.codpro_07.campo6 = Utils.GetIntFromDataReader(reader, col++);
                    rs.codpro_07.prtcli = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_07.indnomc = Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_07.inddirc = Utils.GetDecimalFromDataReader(reader, col++);

                    break;
                case 8:

                    rs.codpro_08 = new sce_cvd_s06_MS_DTO_08();

                    rs.codpro_08.codcct = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_08.codpro = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_08.codesp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_08.codofi = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_08.codope = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_08.fecing = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                    rs.codpro_08.campo6 = (int)Utils.GetIntFromDataReader(reader, col++);
                    rs.codpro_08.prtcli = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_08.indnomc = Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_08.inddirc = Utils.GetDecimalFromDataReader(reader, col++);


                    break;
                case 9:

                    rs.codpro_09 = new sce_cvd_s06_MS_DTO_09();

                    rs.codpro_09.codcct = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_09.codpro = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_09.codesp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_09.codofi = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_09.codope = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_09.fecing = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                    rs.codpro_09.campo6 = (int)Utils.GetIntFromDataReader(reader, col++);
                    rs.codpro_09.prtexp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_09.indexpn = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_09.indexpd = (decimal)Utils.GetDecimalFromDataReader(reader, col++);

                    break;
                case 17:

                    rs.codpro_17 = new sce_cvd_s06_MS_DTO_17();

                    rs.codpro_17.codcct = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_17.codpro = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_17.codesp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_17.codofi = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_17.codope = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_17.fecing = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                    rs.codpro_17.campo6 = (int)Utils.GetIntFromDataReader(reader, col++);
                    rs.codpro_17.prtexp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_17.indnome = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_17.inddire = (decimal)Utils.GetDecimalFromDataReader(reader, col++);

                    break;
                case 18:

                    rs.codpro_18 = new sce_cvd_s06_MS_DTO_18();

                    rs.codpro_18.codcct = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_18.codpro = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_18.codesp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_18.codofi = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_18.codope = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_18.fecing = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                    rs.codpro_18.campo6 = (int)Utils.GetIntFromDataReader(reader, col++);
                    rs.codpro_18.prtexp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_18.indexpn = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_18.indexpd = (decimal)Utils.GetDecimalFromDataReader(reader, col++);

                    break;
                case 20:

                    rs.codpro_20 = new sce_cvd_s06_MS_DTO_20();

                    rs.codpro_20.codcct = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_20.codpro = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_20.codesp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_20.codofi = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_20.codope = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_20.fecing = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                    rs.codpro_20.tipcvd = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_20.prtcli = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_20.indnomc = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_20.inddirc = (decimal)Utils.GetDecimalFromDataReader(reader, col++);

                    break;
                case 30:

                    rs.codpro_30 = new sce_cvd_s06_MS_DTO_30();

                    rs.codpro_30.codcct = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_30.codpro = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_30.codesp = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_30.codofi = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_30.codope = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_30.fecing = (DateTime)Utils.GetFechaFromDataReader(reader, col++);
                    rs.codpro_30.tipcvd = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_30.prtcli = Utils.GetStringFromDataReader(reader, col++);
                    rs.codpro_30.indnomc = (decimal)Utils.GetDecimalFromDataReader(reader, col++);
                    rs.codpro_30.inddirc = (decimal)Utils.GetDecimalFromDataReader(reader, col++);

                    break;
                default:
                    break;
            }

            return rs;
        }

        public List<sgt_suc_s01_MS_Result> sgt_suc_s01_MS()
        {
            return context.sgt_suc_s01_MS().ToList();
        }

        public string sce_pli_s04_MS(string NumOpe, DateTime Fecha)
        {
            string texto;
            try
            {
                texto = context.sce_pli_s04_MS(NumOpe, Fecha).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
            return texto;
        }

        public string sce_xanu_s03_MS(string NumOpe, DateTime? Fecha)
        {
            string texto;
            try
            {
                texto = context.sce_xanu_s03_MS(NumOpe, Fecha).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
            return texto;
        }
        public IList<sce_cvd1_s03_MS_Result> sce_cvd1_s03_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return EjecutarSP<sce_cvd1_s03_MS_Result>("sce_cvd1_s03_MS", codcct, codpro, codesp, codofi, codope).ToList();
        }

        public sce_xplv_s10_MS_Result sce_xplv_s10_MS(string numpre, DateTime? fecpre)
        {
            return context.sce_xplv_s10_MS(numpre, fecpre).FirstOrDefault();
        }

        public T_Pli sce_pli_s07_MS(string numpre, DateTime? fecpre)
        {
            List<T_Pli> result = new List<T_Pli>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    int c = 0;
                    T_Pli rs = new T_Pli();

                    rs.NumPli = Utils.GetStringFromDataReader(reader, c++);
                    rs.FecPli = Utils.GetFechaFromDataReader(reader, c++).ToString();
                    rs.PlzBcc = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.cencos = Utils.GetStringFromDataReader(reader, c++);
                    rs.codusr = Utils.GetStringFromDataReader(reader, c++);
                    rs.codcct = Utils.GetStringFromDataReader(reader, c++);
                    rs.codpro = Utils.GetStringFromDataReader(reader, c++);
                    rs.codesp = Utils.GetStringFromDataReader(reader, c++);
                    rs.codofi = Utils.GetStringFromDataReader(reader, c++);
                    rs.codope = Utils.GetStringFromDataReader(reader, c++);
                    rs.CodOper = Utils.GetStringFromDataReader(reader, c++);
                    rs.rutcli = Utils.GetStringFromDataReader(reader, c++);
                    rs.PrtCli = Utils.GetStringFromDataReader(reader, c++);
                    rs.IndNom = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.IndDir = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.CodOci = (double)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.TipPln = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.codcom = Utils.GetStringFromDataReader(reader, c++);
                    rs.Concep = Utils.GetStringFromDataReader(reader, c++);
                    rs.NumAcu = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.Desacu = Utils.GetStringFromDataReader(reader, c++);
                    rs.codpai = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.CodMnd = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.CodMndBC = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.MtoOpe = (double)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.Mtopar = (double)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.MtoDol = (double)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.DieNum = Utils.GetStringFromDataReader(reader, c++);
                    rs.DieFec = Utils.GetFechaFromDataReader(reader, c++).ToString();
                    rs.DiePbc = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.numdec = Utils.GetStringFromDataReader(reader, c++);
                    rs.FecDec = Utils.GetFechaFromDataReader(reader, c++).ToString();
                    rs.CodAdn = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.FecDeb = Utils.GetFechaFromDataReader(reader, c++).ToString();
                    rs.DocNac = Utils.GetStringFromDataReader(reader, c++);
                    rs.DocExt = Utils.GetStringFromDataReader(reader, c++);
                    rs.BcoExt = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.NumCre = (double)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.FecCre = Utils.GetFechaFromDataReader(reader, c++).ToString();
                    rs.MndCre = (short)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.MtoCre = (double)Utils.GetDecimalFromDataReader(reader, c++);
                    rs.CodAcu = Utils.GetStringFromDataReader(reader, c++);
                    rs.RegAcu = Utils.GetStringFromDataReader(reader, c++);
                    rs.RutAcu = Utils.GetStringFromDataReader(reader, c++);

                    result.Add(rs);
                }
            }, "sce_pli_s07_MS", numpre, fecpre.ToString());

            return result.FirstOrDefault();
        }

        //trae todos los corresponsales
        public IList<T_Cor> sce_cor_s03_MS()
        {
            return context.sce_cor_s03_MS().Select(x => new T_Cor()
            {
                Cor_Swf = x.cor_swf.Trim(),
                Cor_Nom = x.cor_nom.Trim(),
                Cor_Ciu = x.cor_ciu.Trim(),
                Cor_Dir = x.cor_dir.Trim(),
                Cor_Pos = x.cor_pos.Trim(),
                Cor_Pai = x.cor_pai.Trim(),
                Cor_CPa = (short)x.cor_cpa
            }).ToList();
        }

        public IList<sce_cpai> sce_cpai_s01_MS()
        {
            return context.sce_cpai_s01_MS().ToList();
        }

        public sce_ppae_s02_MS_Result sce_ppae_s02_MS(string cent_costo, string id_product, string id_especia, string id_empresa, string id_cobranz)
        {
            sce_ppae_s02_MS_Result result = EjecutarSP<sce_ppae_s02_MS_Result>("sce_ppae_s02_MS", cent_costo, id_product, id_especia, id_empresa, id_cobranz).SingleOrDefault();
            return result;
        }

        public sce_jprt_s02_MS_Result sce_jprt_s02_MS(string cent_costo, string id_product, string id_especia, string id_empresa, string id_cobranz)
        {
            sce_jprt_s02_MS_Result result = EjecutarSP<sce_jprt_s02_MS_Result>("sce_jprt_s02_MS", cent_costo, id_product, id_especia, id_empresa, id_cobranz).SingleOrDefault();
            return result;
        }

        public sce_pcol_s01_MS_Result sce_pcol_s01_MS(string cent_costo, string id_product, string id_especia, string id_empresa, string id_cobranz)
        {
            sce_pcol_s01_MS_Result result = EjecutarSP<sce_pcol_s01_MS_Result>("sce_pcol_s01_MS", cent_costo, id_product, id_especia, id_empresa, id_cobranz).SingleOrDefault();
            return result;
        }

        public sce_cvd_s05_MS_Result sce_cvd_s05_MS(string cent_costo, string id_product, string id_especia, string id_empresa, string id_cobranz)
        {
            sce_cvd_s05_MS_Result result = EjecutarSP<sce_cvd_s05_MS_Result>("sce_cvd_s05_MS", cent_costo, id_product, id_especia, id_empresa, id_cobranz).FirstOrDefault();
            return result;
        }

        public sce_mch_s05_MS_Result sce_mch_s05_MS(string cent_costo, string id_product, string id_especia, string id_empresa, string id_cobranz)
        {
            sce_mch_s05_MS_Result result = EjecutarSP<sce_mch_s05_MS_Result>("sce_mch_s05_MS", cent_costo, id_product, id_especia, id_empresa, id_cobranz).FirstOrDefault();
            return result;
        }

        public sce_xret_s05_MS_Result sce_xret_s05_MS(string cent_costo, string id_product, string id_especia, string id_empresa, string id_cobranz, string codneg1, string codneg2)
        {
            sce_xret_s05_MS_Result result = EjecutarSP<sce_xret_s05_MS_Result>("sce_xret_s05_MS", cent_costo, id_product, id_especia, id_empresa, id_cobranz, codneg1, codneg2).SingleOrDefault();
            return result;
        }

        public sce_xcob_s04_MS_Result sce_xcob_s04_MS(string NumOpe)
        {
            List<sce_xcob_s04_MS_Result> result = new List<sce_xcob_s04_MS_Result>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    int c = 0;
                    result.Add(new sce_xcob_s04_MS_Result
                    {
                        prtexp1 = reader.GetString(c++),
                        indnom1 = reader.GetDecimal(c++),
                        inddir1 = reader.GetDecimal(c++)
                    });
                }
            }, "sce_xcob_s04_MS", NumOpe.Substring(0, 3), NumOpe.Substring(3, 2), NumOpe.Substring(5, 2), NumOpe.Substring(7, 3), NumOpe.Substring(10, 5));


            return result.FirstOrDefault();
        }


        public List<sce_mcd_s04_MS_Result> sce_mcd_s04_MS(string NumOpe)
        {
            List<sce_mcd_s04_MS_Result> result = new List<sce_mcd_s04_MS_Result>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    int c = 0;
                    result.Add(new sce_mcd_s04_MS_Result
                    {
                        codcct = reader.GetString(c++),
                        codpro = reader.GetString(c++),
                        codesp = reader.GetString(c++),
                        codofi = reader.GetString(c++),
                        codope = reader.GetString(c++),
                        nrofac = reader.GetDecimal(c++),
                        nrorpt = reader.GetDecimal(c++),
                        fecfac = reader.GetDateTime(c++),
                        netofac = reader.GetDecimal(c++),
                        ivafac = reader.GetDecimal(c++),
                        montofac = reader.GetDecimal(c++),
                        monedafac = reader.GetDecimal(c++),
                        tipofac = reader.GetString(c++)
                    });
                }
            }, "sce_mcd_s04_MS", NumOpe.Substring(0, 3), NumOpe.Substring(3, 2), NumOpe.Substring(5, 2), NumOpe.Substring(7, 3), NumOpe.Substring(10, 5));

            return result;
        }

        public string pro_sce_cta_s01_MS(string nemonico)
        {
            return context.pro_sce_cta_s01_MS(nemonico).FirstOrDefault();
        }

        public IList<sce_plan_s17_MS_Result> sce_plan_s17_MS(string cent_costo, string id_product, string id_especia, string id_empresa, string id_cobranz, Nullable<decimal> num_presen)
        {
            List<sce_plan_s17_MS_Result> result = new List<sce_plan_s17_MS_Result>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    int c = 0;
                    if (!reader.IsDBNull(0))
                    {
                        result.Add(new sce_plan_s17_MS_Result
                        {
                            cent_costo = reader.GetString(c++),
                            id_product = reader.GetString(c++),
                            id_especia = reader.GetString(c++),
                            id_empresa = reader.GetString(c++),
                            id_cobranz = reader.GetString(c++),
                            num_presen = reader.GetDecimal(c++),
                            rut = reader.GetString(c++),
                            party = reader.GetString(c++),
                            nomimport = reader.GetString(c++),
                            Column1 = reader.GetDateTime(c++),
                            num_dec = reader.GetString(c++),
                            Column2 = reader.GetDateTime(c++),
                            num_con = reader.GetString(c++),
                            Column3 = reader.GetDateTime(c++),
                            codigo = reader.GetString(c++),
                            codbcch = reader.GetDecimal(c++),
                            cod_plaza = reader.GetDecimal(c++),
                            nombplaza = reader.GetString(c++),
                            forma_pag = reader.GetDecimal(c++),
                            codpais = reader.GetDecimal(c++),
                            nompais = reader.GetString(c++),
                            codmone = reader.GetDecimal(c++),
                            nommone = reader.GetString(c++),
                            paridad = reader.GetDecimal(c++),
                            tipo_camb = reader.GetDecimal(c++),
                            mercaderia = reader.GetDecimal(c++),
                            hasta_fob = reader.GetDecimal(c++),
                            mtofob = reader.GetDecimal(c++),
                            mtoflete = reader.GetDecimal(c++),
                            mtoseguro = reader.GetDecimal(c++),
                            mtocif = reader.GetDecimal(c++),
                            mtointer = reader.GetDecimal(c++),
                            mtogastos = reader.GetDecimal(c++),
                            mtototal = reader.GetDecimal(c++),
                            cifdolar = reader.GetDecimal(c++),
                            totaldolar = reader.GetDecimal(c++),
                            Column4 = reader.GetDateTime(c++)
                        });

                    }
                }
            }, "sce_plan_s17_MS", cent_costo, id_product, id_especia, id_empresa, id_cobranz, num_presen.ToString());

            return result;
        }

        public IList<sce_plan_s16_Result> sce_plan_s16_MS(string cent_costo, string id_product, string id_especia, string id_empresa, string id_cobranz, Nullable<decimal> num_presen, Nullable<System.DateTime> fecplan)
        {

            sce_plan_s16_Result res;

            List<sce_plan_s16_Result> result = new List<sce_plan_s16_Result>();
            ReadQuerySP((reader) =>
                {
                    while (reader.Read())
                    {
                        int c = 0;
                        res = new sce_plan_s16_Result();
                        res.Column1 = Utils.GetBooleanFromDataReader(reader, c++);
                        res.Column2 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column3 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column4 = Utils.GetBooleanFromDataReader(reader, c++);
                        res.Column5 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column6 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column7 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column8 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column9 = Utils.GetBooleanFromDataReader(reader, c++);
                        res.Column10 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column11 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column12 = Utils.GetFechaFromDataReader(reader, c++);
                        res.Column13 = Utils.GetFechaFromDataReader(reader, c++);
                        res.Column14 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column15 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column16 = Utils.GetFechaFromDataReader(reader, c++);
                        res.Column17 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column18 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column19 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column20 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column21 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column22 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column23 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column24 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column25 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column26 = Utils.GetFechaFromDataReader(reader, c++);
                        res.Column27 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column28 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column29 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column30 = Utils.GetFechaFromDataReader(reader, c++);
                        res.Column31 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column32 = Utils.GetStringFromDataReader(reader, c++);
                        res.Column33 = Utils.GetFechaFromDataReader(reader, c++);
                        res.Column34 = Utils.GetDecimalFromDataReader(reader, c++);
                        res.Column35 = Utils.GetBooleanFromDataReader(reader, c++);

                        result.Add(res);

                    }
                }, "sce_plan_s16_MS", cent_costo, id_product, id_especia, id_empresa, id_cobranz, Convert.ToString(num_presen), Convert.ToString(fecplan));

            return result;
        }

        public IList<pro_sce_inpl_s01_MS_Result> pro_sce_inpl_s01_MS(string cencos, string codpro, string codesp, string codofi, string codope, Nullable<decimal> nropln)
        {
            return context.pro_sce_inpl_s01_MS(cencos, codpro, codesp, codofi, codope, nropln).ToList();
        }

        public pro_sce_prty_s05_MS_01_Result pro_sce_prty_s05_MS_01(string gb_llave, int? gb_id, int? gb_opcion)
        {
            return EjecutarSP<pro_sce_prty_s05_MS_01_Result>("pro_sce_prty_s05_MS", gb_llave, gb_id.ToString(), gb_opcion.ToString()).FirstOrDefault();
        }

        public IList<sce_plrm_s02_MS_Result> sce_plrm_s02_MS(string codcct, string codpro, string codesp, string codofi, string codope, Nullable<System.DateTime> fecha)
        {

            List<sce_plrm_s02_MS_Result> result = new List<sce_plrm_s02_MS_Result>();
            ReadQuerySP((reader) =>
        {
            while (reader.Read())
            {
                int c = 0;
                if (!reader.IsDBNull(0))
                {
                    result.Add(new sce_plrm_s02_MS_Result
                    {
                        Column1 = reader.GetDecimal(c++),
                        Column2 = reader.GetDecimal(c++),
                        Column3 = reader.GetString(c++).ToString()
                    });

                }
            }
        }, "sce_plrm_s02_MS", codcct, codpro, codesp, codofi, codope, Convert.ToString(fecha));

            return result;
        }

        public IList<sce_plan_s18_MS_Result> sce_plan_s18_MS(string cent_costo, string id_product, string id_especia, string id_empresa, string id_cobranz, Nullable<System.DateTime> fechaventa)
        {

            List<sce_plan_s18_MS_Result> result = new List<sce_plan_s18_MS_Result>();
            ReadQuerySP((reader) =>
        {
            while (reader.Read())
            {
                int c = 0;
                if (!reader.IsDBNull(0))
                {
                    result.Add(new sce_plan_s18_MS_Result
                    {
                        cent_costo = reader.GetString(c++),
                        id_product = reader.GetString(c++),
                        id_especia = reader.GetString(c++),
                        id_empresa = reader.GetString(c++),
                        id_cobranz = reader.GetString(c++),
                        num_presen = reader.GetDecimal(c++),
                        Column1 = reader.GetDateTime(c++),
                        num_dec = reader.GetString(c++),
                        Column2 = reader.GetDateTime(c++),
                        codmone = reader.GetDecimal(c++),
                        forma_pag = reader.GetDecimal(c++),
                        mercaderia = reader.GetDecimal(c++),
                        hasta_fob = reader.GetDecimal(c++),
                        mtofob = reader.GetDecimal(c++),
                        mtoflete = reader.GetDecimal(c++),
                        mtoseguro = reader.GetDecimal(c++),
                        mtocif = reader.GetDecimal(c++),
                        mtointer = reader.GetDecimal(c++),
                        mtogastos = reader.GetDecimal(c++),
                        mtototal = reader.GetDecimal(c++),
                        cifdolar = reader.GetDecimal(c++),
                        totaldolar = reader.GetDecimal(c++),
                        paridad = reader.GetDecimal(c++),
                        tipo_camb = reader.GetDecimal(c++),
                        indanula = reader.GetDecimal(c++)
                    });

                }
            }
        }, "sce_plan_s18_MS", cent_costo, id_product, id_especia, id_empresa, id_cobranz, Convert.ToString(fechaventa));

            return result;
        }

        public IList<sce_aut_s01_MS_Result> sce_aut_s01_MS()
        {
            return context.sce_aut_s01_MS().ToList();
        }

        public pro_sce_xdec_s01_MS_Result pro_sce_xdec_s01_MS_MS(string numdec, string fecdec, short codAdn)
        {
            var result = context.pro_sce_xdec_s01_MS(numdec, fecdec, codAdn).FirstOrDefault();

            return result;
        }

        public double sce_rng_u01_MS(string codcct, string codesp, string codfun, out string mensaje, int? minNumRequerido = null)
        {
            sce_rng_u01_MS_Result _retValue = context.sce_rng_u01_MS(codcct, codesp, codfun, minNumRequerido).FirstOrDefault();

            if (_retValue != null)
            {
                mensaje = _retValue.mensaje ?? string.Empty;
                return Convert.ToDouble(_retValue.valor);
            }
            else
            {
                mensaje = string.Empty;
                return 0;
            }
        }

        public sce_mta0_s01_MS_Result sce_mta0_s01_MS(string codsis, string codpro, string codeta)
        {
            var retorno = context.sce_mta0_s01_MS(codsis, codpro, codeta).FirstOrDefault();
            //var retorno = unit.SceRepository.EjecutarSP<sce_mta0_s01_MS_Result>("sce_mta0_s01_MS", MODGMTA.VDatCob.codsis, MODGMTA.VDatCob.codpro, MODGMTA.VDatCob.CodEta)
            //        

            return retorno;
        }

        public void pro_sce_fts_i01_MS(ref string lc_retorno, ref string lc_mensaje, List<string> listaParam)
        {
            ObjectParameter retornoParam = new ObjectParameter("ls_retorno", typeof(string));
            ObjectParameter mensajeParam = new ObjectParameter("ls_mensaje", typeof(string));

            try
            {
                int r = context.pro_sce_fts_i01_MS(listaParam[0], decimal.Parse(listaParam[1]), decimal.Parse(listaParam[2]), listaParam[3],
                                    listaParam[4], decimal.Parse(listaParam[5]), listaParam[6], decimal.Parse(listaParam[7]),
                                         decimal.Parse(listaParam[8]), decimal.Parse(listaParam[9]), listaParam[10], listaParam[11],
                                    listaParam[12], listaParam[13], listaParam[14], decimal.Parse(listaParam[15]),
                                    decimal.Parse(listaParam[16]), decimal.Parse(listaParam[17]), listaParam[18], decimal.Parse(listaParam[19]),
                                         decimal.Parse(listaParam[20]), decimal.Parse(listaParam[21]), decimal.Parse(listaParam[22]), listaParam[23],
                                    listaParam[24], listaParam[25], listaParam[26], decimal.Parse(listaParam[27]),
                                    listaParam[28], listaParam[29], decimal.Parse(listaParam[30]), decimal.Parse(listaParam[31]),
                                    listaParam[32], decimal.Parse(listaParam[33]), decimal.Parse(listaParam[34]), decimal.Parse(listaParam[35]),
                                    listaParam[36], listaParam[37], decimal.Parse(listaParam[38]), decimal.Parse(listaParam[39]),
                                    listaParam[40], decimal.Parse(listaParam[41]), decimal.Parse(listaParam[42]), decimal.Parse(listaParam[43]),
                                    listaParam[44], listaParam[45], listaParam[46], listaParam[47],
                                    decimal.Parse(listaParam[48]), decimal.Parse(listaParam[49]), listaParam[50], decimal.Parse(listaParam[51]),
                                    decimal.Parse(listaParam[52]), listaParam[53], decimal.Parse(listaParam[54]), listaParam[55],
                                    listaParam[56], decimal.Parse(listaParam[57]), decimal.Parse(listaParam[58]), listaParam[59],
                                    listaParam[60], decimal.Parse(listaParam[61]), decimal.Parse(listaParam[62]), decimal.Parse(listaParam[63]),
                                         decimal.Parse(listaParam[64]), decimal.Parse(listaParam[65]), decimal.Parse(listaParam[66]), decimal.Parse(listaParam[67]),
                                    decimal.Parse(listaParam[68]), decimal.Parse(listaParam[69]), listaParam[70], listaParam[71],
                                         retornoParam, mensajeParam);
                lc_retorno = retornoParam.Value.ToString();
                lc_mensaje = mensajeParam.Value.ToString();
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
            }
        }

        public decimal? sce_obc_s01_MS(short sucBCH)
        {
            return context.sce_obc_s01_MS(sucBCH).FirstOrDefault();
        }

        public IList<sce_tccs> GetDescripcionesDeCodigosDeCamposSwift()
        {
            return context.Set<sce_tccs>().ToList();
        }

        public IList<LineaMensajeSwift> GetCamposManualesSwift(int codMt)
        {
            IList<sce_tccs> tccs = context.Set<sce_tccs>().Where(t => t.codmt == codMt && t.camman == true).ToList();
            List<LineaMensajeSwift> lineas = new List<LineaMensajeSwift>();

            foreach (sce_tccs t in tccs)
            {
                LineaMensajeSwift linea = new LineaMensajeSwift((short)t.numlin)
                {
                    CodCam = t.codcam,
                    CodMT = (int)t.codmt,
                    Descripcion = t.descam,
                    EsManual = t.camman,
                    LenLlinea = (short)t.lenlin,
                    LenTotal = (short)t.lentot
                };

                lineas.Add(linea);
            }

            return lineas;
        }

        public sce_tccs GetDescripcionDeCodigosDeCamposSwift(short codMT, string codCam)
        {
            return context.Set<sce_tccs>().Where(c => c.codmt == codMT && c.codcam == codCam).FirstOrDefault();
        }

        public int? Sce_mts_i01(string codcct, string codpro, string codesp, string codofi, string codope, decimal? nummeg, decimal? tippro, decimal? numcpa, decimal? numcuo, decimal? numcob, decimal? rutais, DateTime fecmsg, decimal? id_mensaje, decimal? estado, string tipgra, decimal? nrorpt, decimal? tipmt)
        {
            return context.sce_mts_i01(codcct, codpro, codesp, codofi, codope, nummeg, tippro, numcpa, numcuo, numcob, rutais, fecmsg, id_mensaje, estado, tipgra, nrorpt, tipmt).FirstOrDefault();
        }

        public IList<sce_rsa_s06_MS_Result> Sce_Rsa_S06_MS(int id_nombre, int crea_costo, int crea_user)
        {
            return context.sce_rsa_s06_MS(id_nombre, crea_costo, crea_user).ToList();
        }

        public IList<sce_rsa_i01_MS_Result> Sce_Rsa_I01_MS(string idParty, int idNombre, string razonSocial, string nombreFantasia, string contacto, string sortKey, string creaCosto, string creaUser)
        {
            try
            {
                return context.sce_rsa_i01_MS(idParty, idNombre, razonSocial, nombreFantasia, contacto, sortKey, creaCosto, creaUser).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool Sce_mcd_u70(decimal nroReporte, DateTime fecMov, decimal nroImp, decimal enLinea, string rutAis)
        {
            int result = this.EjecutarSPConRetorno("sce_mcd_u70_MS", String.Empty, nroReporte.ToString(), fecMov.ToString("yyyy-MM-dd"), nroImp.ToString(), ((int)enLinea).ToString(), rutAis);
            return result == 0;
        }

        public List<sce_mcd_s71_MS_Result> Sce_mcd_s71_MS(string cencos, string codusr, string rutais, DateTime fecmov)
        {
            var retorno = context.sce_mcd_s71_MS(cencos, codusr, rutais, fecmov.Date).ToList();

            return retorno;
        }

        public IList<sce_rsa_parti_listRazon_MS_Result> Sce_Rsa_Parti_ListRazon_MS(string sRazon)
        {
            return context.sce_rsa_parti_listRazon_MS(sRazon).ToList();
        }

        public IList<pro_sce_prty_s07_MS_Result> Pro_Sce_Prty_S07_MS(String searchRazonSocial)
        {
            return context.pro_sce_prty_s07_MS(searchRazonSocial).ToList();
        }

        public IList<sce_rsa_u01_MS_Result> Sce_Rsa_U01_MS(string idParty, int idNombre, int borrado, string razonSocial, string nombreFantasia, string contacto, string sortKey)
        {
            return context.sce_rsa_u01_MS(idParty, idNombre, borrado, razonSocial, nombreFantasia, contacto, sortKey).ToList();
        }

        public IList<sce_rsa_parti_listDir_MS_Result> Sce_Rsa_Parti_ListDir_MS(string sRazon)
        {
            return context.sce_rsa_parti_listDir_MS(sRazon).ToList();
        }

        public IList<LstAcEco_MS_Result> LstAcEco_MS()
        {
            return context.LstAcEco_MS().ToList();
        }

        public IList<LstRiesgo_MS_Result> LstRiesgo_MS()
        {
            return context.LstRiesgo_MS().ToList();
        }

        public IList<LstEjec_MS_Result> LstEjec_MS()
        {
            return context.LstEjec_MS().ToList();
        }

        public IList<sce_rsa_s07_MS_Result> Sce_Rsa_S07_MS(String idParty)
        {
            return context.sce_rsa_s07_MS(idParty).ToList();
        }

        public IList<sce_dad_s08_MS_Result> Sce_Dad_S08_MS(String idParty)
        {
            return context.sce_dad_s08_MS(idParty).ToList();
        }
        public string sce_usr_s16_MS(string cent_costo, string id_especia)
        {
            return context.sce_usr_s16_MS(cent_costo, id_especia).FirstOrDefault();
        }

        public string Sce_Usr_S09_MS(string cencos, string codusr)
        {
            return context.sce_usr_s09_MS(cencos, codusr).FirstOrDefault();
        }

        public sce_usr_s05_MS_Result Sce_Usr_S05_MS(string cencos, string codusr)
        {
            var retorno = context.sce_usr_s05_MS(cencos, codusr).FirstOrDefault();

            return retorno;
        }

        public List<string> Sce_Usr_S06_MS(string centroCosto, string idEspecialista)
        {
            var retorno = context.sce_usr_s06_MS(centroCosto, idEspecialista).ToList();

            return retorno;
        }
        public IList<sgt_ejc_s02_MS_Result> Sgt_ejc_S02_MS(Int32 idCodigo)
        {
            return context.sgt_ejc_s02_MS(idCodigo).ToList();
        }

        public IList<sgt_aec_s01_MS_Result> Sgt_aec_s01_MS()
        {
            return context.sgt_aec_s01_MS().ToList();
        }

        public IList<sgt_clf_s01_MS_Result> Sgt_clf_s01_MS()
        {
            return context.sgt_clf_s01_MS().ToList();
        }

        public IList<sgt_ejc_s03_MS_Result> Sgt_ejc_S03_MS(string EjcoPimp, string Ejcopext, string EjcNegoc)
        {
            return context.sgt_ejc_s03_MS(EjcoPimp, Ejcopext, EjcNegoc).ToList();
        }

        public sce_tcom_s03_MS_Result Sce_Tcom_S03_MS(String keyprt, string codsis, string codpro, string codeta, DateTime fecref, double mtocom)
        {
            return context.sce_tcom_s03_MS(keyprt, codsis, codpro, codeta, fecref, mtocom).FirstOrDefault();
        }

        public IList<sce_tcom_s04_MS_Result> Sce_Tcom_S04_MS(String idParty)
        {
            return context.sce_tcom_s04_MS(idParty).ToList();
        }

        public IList<sce_tgas_s04_MS_Result> Sce_Tgas_S04_MS(String idParty)
        {
            return context.sce_tgas_s04_MS(idParty).ToList();
        }

        public IList<sce_tint_s01_MS_Result> Sce_Tint_S01_MS(String idParty)
        {
            return context.sce_tint_s01_MS(idParty).ToList();
        }
        public IList<sce_prd_i01_MS_Result> sce_prd_i01_MS(string codcct, string codusr, int mes, int ano)
        {
            List<sce_prd_i01_MS_Result> result = new List<sce_prd_i01_MS_Result>();
            try
            {
                ReadQuerySP((reader) =>
                {
                    while (reader.Read())
                    {
                        int c = 0;
                        if (!reader.IsDBNull(0))
                        {
                            sce_prd_i01_MS_Result rw = new sce_prd_i01_MS_Result();

                            rw.cod_cct = Utils.GetStringFromDataReader(reader, c++);
                            rw.cod_usr = Utils.GetStringFromDataReader(reader, c++);
                            rw.nombre_esp = Utils.GetStringFromDataReader(reader, c++);
                            rw.num_cob = (int)reader.GetDecimal(c++);
                            rw.num_ret = (int)reader.GetDecimal(c++);
                            rw.num_pli = (int)reader.GetDecimal(c++);
                            rw.num_plv = (int)reader.GetDecimal(c++);
                            rw.num_dec = (int)reader.GetDecimal(c++);
                            rw.num_gl = (int)reader.GetDecimal(c++);
                            rw.num_cce = (int)reader.GetDecimal(c++);

                            result.Add(rw);
                        }
                    }
                }, "sce_prd_i01_MS", codcct, codusr, mes.ToString(), ano.ToString());

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public sce_usr_u03_MS_Result sce_usr_u03_MS(string cencos, string codusr, string clasup)
        {
            var result = new List<sce_usr_u03_MS_Result>();
            try
            {
                ReadQuerySP((reader) =>
                {
                    while (reader.Read())
                    {
                        var item = new sce_usr_u03_MS_Result
                        {
                            Column1 = reader.GetInt32(0),
                            Column2 = Utils.GetStringFromDataReader(reader, 1)
                        };
                        result.Add(item);
                    }
                }, "sce_usr_u03_MS", cencos, codusr, clasup);
                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual IList<sce_chq_s03_MS_Result> sce_chq_s03_MS(string cctsup, string usrsup, DateTime fecemi)
        {
            return context.sce_chq_s03_MS(cctsup, usrsup, fecemi).ToList();
        }

        public virtual IList<sce_vvi_s03_MS_Result> sce_vvi_s03_MS(string cctsup, string usrsup, DateTime fecemi)
        {
            return context.sce_vvi_s03_MS(cctsup, usrsup, fecemi).ToList();
        }

        public IList<sce_grdo_s01_MS_Result> Sce_grdo_s01(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_grdo_s01_MS(codcct, codpro, codesp, codofi, codope).ToList();
        }

        public IList<sce_grio_s01_MS_Result> Sce_grio_s01(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_grio_s01_MS(codcct, codpro, codesp, codofi, codope).ToList();
        }

        public int? Sce_grdo_d01(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_grdo_d01_MS(codcct, codpro, codesp, codofi, codope).FirstOrDefault();
        }

        public int? Sce_grio_d01(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_grio_d01_MS(codcct, codpro, codesp, codofi, codope).FirstOrDefault();
        }
        public void Pro_Sce_Prty_S08_MS(String id_party, Int32 id_nombre, Int32 id_direccion)
        {

            DbCommand commandExecSP = context.Database.Connection.CreateCommand();
            commandExecSP.CommandText = "exec pro_sce_prty_s08_MS @id_party, @id_nombre, @id_direccion";

            DbParameter param = commandExecSP.CreateParameter();
            param.DbType = System.Data.DbType.String;
            param.ParameterName = "id_party";
            param.Value = id_party;
            commandExecSP.Parameters.Add(param);

            param = commandExecSP.CreateParameter();
            param.DbType = System.Data.DbType.Int32;
            param.ParameterName = "id_nombre";
            param.Value = id_nombre;
            commandExecSP.Parameters.Add(param);

            param = commandExecSP.CreateParameter();
            param.DbType = System.Data.DbType.Int32;
            param.ParameterName = "id_direccion";
            param.Value = id_direccion;
            commandExecSP.Parameters.Add(param);


            context.Database.Connection.Open();
            using (DbDataReader reader = commandExecSP.ExecuteReader())
            {
                //(moo) Razones Sociales
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //return Utils.GetStringFromDataReader(reader, 0);
                    }
                }

                reader.NextResult();

                //(moo) Direcciones
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //return Utils.GetStringFromDataReader(reader, 0);
                    }
                }
            }
        }

        public IList<String> Sce_Prty_S07_MS(String idParty)
        {
            return context.sce_prty_s07_MS(idParty).ToList();
        }

        public IList<String> Sce_Bic_S07_MS(String searchIdParty)
        {
            return context.sce_bic_s07_MS(searchIdParty).ToList();
        }

        public sce_prty_s08_MS_Result Sce_Prty_S08_MS(String searchKeyIdParty)
        {
            return context.sce_prty_s08_MS(searchKeyIdParty).FirstOrDefault();
        }

        public int Sce_Prty_U01_MS(String keyIdParty, Boolean deletePrty)
        {
            return context.sce_prty_u01_MS(keyIdParty, deletePrty);
        }

        public IList<sce_prty_i01_MS_Result> Sce_Prty_I01_MS(String keySearchIdParty, String idParty, String createCosto, String createUser, DateTime dateTime)
        {
            return context.sce_prty_i01_MS(keySearchIdParty, idParty, createCosto, createUser, dateTime).ToList();
        }

        public IQueryable<sce_fdp> sce_fdp()
        {
            return context.sce_fdp.AsQueryable();
        }

        public IList<sce_abr_s01_MS_Result> Sce_Abr_S01_MS()
        {
            return context.sce_abr_s01_MS().ToList();
        }
        public sce_chq_u01_MS_Result sce_chq_u01_MS(string codcct, string codpro, string codesp, string codofi, string codope, string nrocor, string nrofol, string estado)
        {
            var result = new List<sce_chq_u01_MS_Result>();
            try
            {
                ReadQuerySP((reader) =>
                {
                    while (reader.Read())
                    {
                        var item = new sce_chq_u01_MS_Result
                        {
                            Column1 = reader.GetInt32(0),
                            Column2 = Utils.GetStringFromDataReader(reader, 1)
                        };
                        result.Add(item);
                    }
                }, "sce_chq_u01_MS", codcct, codpro, codesp, codofi, codope, nrocor.ToString(), nrofol.ToString(), estado.ToString());
                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public string sce_ras_u01_MS(String idParty, decimal id_nombre)
        {
            string result;

            try
            {
                result = context.sce_ras_u01_MS(idParty, id_nombre).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;


        }

        public List<sce_xplv_MS_Result> sce_xplv_MS(string numpre, string cui, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return context.sce_xplv_MS(numpre, cui, fechaDesde, fechaHasta).ToList();
        }

        /// <summary>
        /// Lista de productos
        /// </summary>
        /// <param name="esAsociacion">Valida si es para productos a relacionar.</param>
        /// <returns></returns>
        public List<sce_prd_s01_MS_Result> sce_prd_s01_MS(bool esAsociacion = false)
        {
            return context.sce_prd_s01_MS(esAsociacion).ToList();
        }

        public sce_gcar_u03_MS_Result sce_gcar_u03_MS(string cencos, string codusr, string cctact, string usract, DateTime? fecact, string party, string productos)
        {
            var result = new List<sce_gcar_u03_MS_Result>();
            try
            {
                ReadQuerySP((reader) =>
                {
                    while (reader.Read())
                    {
                        var item = new sce_gcar_u03_MS_Result
                        {
                            Column1 = reader.GetInt32(0),
                            Column2 = Utils.GetStringFromDataReader(reader, 1)
                        };
                        result.Add(item);
                    }
                }, "sce_gcar_u03_MS", cencos, codusr, cctact, usract, fecact.ToString(), party, productos);
                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<sce_gcar_s05_MS_Result> sce_gcar_s05_MS(string CenCos, string CodUsr, string productos)
        {
            return context.sce_gcar_s05_MS(CenCos, CodUsr, productos).ToList().Select(i => new sce_gcar_s05_MS_Result { prtcli = i }).ToList();
        }

        public IList<sce_gcar_s06_MS_Result> sce_gcar_s06_MS(string CenCos, string CodUsr, string ptrcli, string productos)
        {
            return context.sce_gcar_s06_MS(CenCos, CodUsr, ptrcli, productos).ToList();
        }

        //public Object sce_gcar_s05_MS(string CenCos, string CodUsr, string productos)
        //{
        //    return context.sce_gcar_s05_MS(CenCos, CodUsr, productos);
        //}

        //public Object sce_gcar_s06_MS(string CenCos, string CodUsr, string ptrcli, string productos)
        //{
        //    return context.sce_gcar_s06_MS(CenCos, CodUsr, ptrcli, productos);
        //}

        public sce_mta1_s05_MS_Result sce_mta1_s05_MS(string codsis, string codpro, string codeta)
        {
            return context.sce_mta1_s05_MS(codsis, codpro, codeta).FirstOrDefault();
        }

        public int sce_tcom_d01_MS(String id_party)
        {
            return context.sce_tcom_d01_MS(id_party);
        }

        public int sce_tint_d01_MS(String id_party)
        {
            return context.sce_tint_d01_MS(id_party);
        }

        public int sce_tgas_d01_MS(String id_party)
        {
            return context.sce_tgas_d01_MS(id_party);
        }

        public IList<sce_ctas_s04_MS_Result> sce_ctas_s04_MS(String id_party)
        {
            return context.sce_ctas_s04_MS(id_party).ToList();
        }

        public IList<sce_bcta_s01_MS_Result> sce_bcta_s01_MS(String id_party)
        {
            return context.sce_bcta_s01_MS(id_party).ToList();
        }

        public string sce_prty_w01_MS(string id_party,
                                      bool borrado,
                                      byte tipo_party,
                                      short flag,
                                      byte clasificac,
                                      bool tiene_rut,
                                      string rut,
                                      string crea_costo,
                                      string crea_user,
                                      string mod_costo,
                                      string mod_user,
                                      bool multiple,
                                      string cod_ofieje,
                                      string cod_eject,
                                      string cod_acteco,
                                      string clase_ries,
                                      short cod_bco,
                                      bool tasa_libor,
                                      bool tasa_prime,
                                      float spread,
                                      string swift,
                                      decimal plaza_alad,
                                      string ejec_corre,
                                      short flagins,
                                      decimal insgen_imp,
                                      decimal insgen_exp,
                                      decimal insgen_ser,
                                      decimal inscob_imp,
                                      decimal inscob_exp,
                                      decimal inscre_imp,
                                      decimal inscre_exp,
                                      string par1)
        {
            string retSp = "Error Grabar Participante: " + id_party;

            try
            {
                var res = context.sce_prty_w01_MS(id_party.ToUpper(),
                                        borrado,
                                        tipo_party,
                                        flag,
                                        clasificac,
                                        tiene_rut,
                                        rut,
                                        crea_costo,
                                        crea_user,
                                        mod_costo,
                                        mod_user,
                                        multiple,
                                        cod_ofieje,
                                        cod_eject,
                                        cod_acteco,
                                        clase_ries,
                                        cod_bco,
                                        tasa_libor,
                                        tasa_prime,
                                        spread,
                                        swift,
                                        plaza_alad,
                                        ejec_corre,
                                        flagins,
                                        insgen_imp,
                                        insgen_exp,
                                        insgen_ser,
                                        inscob_imp,
                                        inscob_exp,
                                        inscre_imp,
                                        inscre_exp,
                                        par1).ToList();

                if (res != null)
                {
                    if (res[0] != null)
                    {
                        if (res[0].Column1 == 0)
                        {
                            retSp = "OK";
                        }
                        else
                        {
                            retSp = res[0].Column2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retSp = retSp + " " + ex.Message;
            }

            return retSp;
        }

        public string sce_netd_ejc_clt_w01_MS(string rutcli, decimal odejc, decimal oficina)
        {
            try
            {
                context.sce_netd_ejc_clt_w01_MS(rutcli.ToUpper(), odejc, oficina);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public int sce_rsa_i01_MS(string idparty,
                                    int id_nombre,
                                    string razon_soci,
                                    string nom_fantas,
                                    string contacto,
                                    string sortkey,
                                    string crea_costo,
                                    string crea_user)
        {
            try
            {
                context.sce_rsa_i01_MS(idparty.ToUpper(),
                                    id_nombre,
                                    razon_soci,
                                    nom_fantas,
                                    contacto,
                                    sortkey,
                                    crea_costo,
                                    crea_user).ToList().Count();
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int sce_rsa_u02_MS(string id_party, decimal id_nombre)
        {
            try
            {
                context.sce_rsa_u02_MS(id_party.ToUpper(), id_nombre);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int sce_rsa_u01_MS(string idparty,
                                    int id_nombre,
                                    int borrado,
                                    string razon_soci,
                                    string nom_fantas,
                                    string contacto,
                                    string sortkey)
        {

            try
            {
                context.sce_rsa_u01_MS(idparty.ToUpper(),
                                id_nombre,
                                borrado,
                                razon_soci,
                                nom_fantas,
                                contacto,
                                sortkey).ToList().Count();
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public string sce_dad_i01_MS(string id_party,
                                    decimal id_dir,
                                    bool borrado,
                                    string direccion,
                                    string comuna,
                                    decimal cod_comuna,
                                    string cod_postal,
                                    string estado,
                                    string ciudad,
                                    string pais,
                                    decimal cod_pais,
                                    string telefono,
                                    string fax,
                                    string telex,
                                    decimal envio_sce,
                                    decimal recibe_sce,
                                    string cas_postal,
                                    string cas_banco,
                                    string crea_costo,
                                    string crea_user,
                                    string email)
        {
            try
            {
                context.sce_dad_i01_MS(id_party.ToUpper(),
                                    id_dir,
                                    borrado,
                                    direccion,
                                    comuna,
                                    cod_comuna,
                                    cod_postal,
                                    estado,
                                    ciudad,
                                    pais,
                                    cod_pais,
                                    telefono,
                                    fax,
                                    telex,
                                    envio_sce,
                                    recibe_sce,
                                    cas_postal,
                                    cas_banco,
                                    crea_costo,
                                    crea_user,
                                    email);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string sce_dad_u02_MS(string id_party,
                                    decimal id_dir,
                                    bool borrado,
                                    string direccion,
                                    string comuna,
                                    decimal cod_comuna,
                                    string cod_postal,
                                    string estado,
                                    string ciudad,
                                    string pais,
                                    decimal cod_pais,
                                    string telefono,
                                    string fax,
                                    string telex,
                                    decimal envio_sce,
                                    decimal recibe_sce,
                                    string cas_postal,
                                    string cas_banco,
                                    string email)
        {
            try
            {
                context.sce_dad_u02_MS(id_party.ToUpper(),
                                    id_dir,
                                    borrado,
                                    direccion,
                                    comuna,
                                    cod_comuna,
                                    cod_postal,
                                    estado,
                                    ciudad,
                                    pais,
                                    cod_pais,
                                    telefono,
                                    fax,
                                    telex,
                                    envio_sce,
                                    recibe_sce,
                                    cas_postal,
                                    cas_banco,
                                    email);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string sce_dad_u03_MS(string id_party, byte id_dir)
        {

            try
            {
                context.sce_dad_u03_MS(id_party.ToUpper(), id_dir);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public IList<sce_blin_s01_MS_Result> sce_blin_s01_MS(String id_party)
        {
            return context.sce_blin_s01_MS(id_party).ToList();
        }

        public int sce_tgas_i01_MS(String id_party, String sistema, String producto, String etapa, Boolean? borrado, Boolean? m_tarifa, Decimal? monto)
        {
            try
            {
                context.sce_tgas_i01_MS(id_party.ToUpper(), sistema, producto, etapa, borrado, m_tarifa, monto);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int sce_tgas_u02_MS(String id_party, String sistema, String producto, String etapa, Boolean? borrado, Boolean? m_tarifa, Decimal? monto)
        {
            try
            {
                context.sce_tgas_u02_MS(id_party.ToUpper(), sistema, producto, etapa, borrado, m_tarifa, monto);
                return 0;
            }
            catch
            {

                return -1;
            }
        }

        public int sce_tgas_d02_MS(String id_party, String sistema, String producto, String etapa)
        {
            try
            {
                context.sce_tgas_d02_MS(id_party.ToUpper(), sistema, producto, etapa);
                return 0;
            }
            catch
            {

                return -1;
            }
        }

        public int sce_tint_i01_MS(String id_party, String sistema, String producto, String etapa, Boolean? borrado, Boolean? libor, Boolean? prime, Boolean? flotante, Decimal? tasa)
        {
            return context.sce_tint_i01_MS(id_party, sistema, producto, etapa, borrado, libor, prime, flotante, tasa);
        }

        public int sce_tint_u01_MS(String id_party, String sistema, String producto, String etapa, Boolean? libor, Boolean? prime, Boolean? flotante, Decimal? tasa)
        {
            return context.sce_tint_u01_MS(id_party, sistema, producto, etapa, libor, prime, flotante, tasa);
        }

        public int sce_tint_d02_MS(String id_party, String sistema, String producto, String etapa)
        {
            return context.sce_tint_d02_MS(id_party, sistema, producto, etapa);
        }

        public int? sce_tcom_i01_MS(String id_party, String sistema, String producto, String etapa, Decimal? secuencia, Boolean? borrado, Boolean? manual_t, Boolean? monto_fijo, Decimal? tasa, Decimal? hasta_mon, Decimal? minimo, Decimal? maximo, DateTime? fecha)
        {
            return context.sce_tcom_i01_MS(id_party, sistema, producto, etapa, secuencia, borrado, manual_t, monto_fijo, tasa, hasta_mon, minimo, maximo, fecha).FirstOrDefault();
        }

        public int sce_tcom_u02_MS(String id_party, String sistema, String producto, String etapa, Decimal? secuencia, Boolean? borrado, Boolean? manual_t, Boolean? monto_fijo, Decimal? tasa, Decimal? hasta_mon, Decimal? minimo, Decimal? maximo, DateTime? fecha)
        {
            return context.sce_tcom_u02_MS(id_party, sistema, producto, etapa, secuencia, borrado, manual_t, monto_fijo, tasa, hasta_mon, minimo, maximo, fecha);
        }

        public int sce_tcom_d02_MS(String id_party, String sistema, String producto, String etapa, Decimal? secuencia)
        {
            return context.sce_tcom_d02_MS(id_party, sistema, producto, etapa, secuencia);
        }

        public int sce_blin_i01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activa, Decimal? moneda, String linea)
        {
            return context.sce_blin_i01_MS(id_party, secuencia, borrado, activa, moneda, linea);
        }

        public int sce_blin_u01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activa, Decimal? moneda, String linea)
        {
            return context.sce_blin_u01_MS(id_party, secuencia, borrado, activa, moneda, linea);
        }

        public int sce_bcta_i01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activa, Decimal? moneda, String cuenta, Boolean? especial)
        {
            return context.sce_bcta_i01_MS(id_party, secuencia, borrado, activa, moneda, cuenta, especial);
        }

        public int sce_bcta_u01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activa, Decimal? moneda, String cuenta, Boolean? especial)
        {
            return context.sce_bcta_u01_MS(id_party, secuencia, borrado, activa, moneda, cuenta, especial);
        }

        public int sce_ctas_i01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activabco, Boolean? activace, Boolean? extranjera, Decimal? moneda, String cuenta)
        {
            try
            {
                context.sce_ctas_i01_MS(id_party.ToUpper(), secuencia, borrado, activabco, activace, extranjera, moneda, cuenta);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int sce_ctas_u01_MS(String id_party, Decimal? secuencia, Boolean? borrado, Boolean? activabco, Boolean? activace, Boolean? extranjera, Decimal? moneda, String cuenta)
        {
            try
            {
                context.sce_ctas_u01_MS(id_party.ToUpper(), secuencia, borrado, activabco, activace, extranjera, moneda, cuenta);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public IList<sce_usr_s04_MS_Result> sce_usr_s04_MS(String cencos, String codusr)
        {
            return context.sce_usr_s04_MS(cencos, codusr).ToList();
        }

        public IList<scedev_difdh_dev_MS_Result> scedev_difdh_dev_MS()
        {
            return context.scedev_difdh_dev_MS().ToList();
        }

        public IList<sce_trng_s01_MS_Result> sce_trng_s01_list_MS()
        {
            return context.sce_trng_s01_MS().ToList();
        }

        //obtiene las novedades para el usuario
        public IList<sce_nov_s01_MS_Result> Sce_Nov_s01(string centCosto, string codUsr, decimal jerarquia, DateTime fecha)
        {
            return context.sce_nov_s01_MS("00", fecha, centCosto, codUsr, jerarquia).ToList();
        }

        //obtiene la cantidad de novedades del usuario
        public int? Sce_Nov_s03(string centCosto, string codUsr, decimal jerarquia, DateTime fecha)
        {
            return context.sce_nov_s03_MS("00", fecha, centCosto, codUsr, jerarquia).FirstOrDefault();
        }

        public IList<DateTime> GetFeriadosEntreFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            return context.sce_fer.Where(f => f.dia_fer >= fechaDesde && f.dia_fer <= fechaHasta).Select(f => f.dia_fer).ToList();
        }

        public IList<sce_dev_cons_MS_Result> Sce_Dev_Cons_MS(decimal periodo, string rut, string operacion, decimal moneda, string todos, decimal tipoConsulta, int numeroRegistros)
        {
            return context.sce_dev_cons_MS(periodo, rut, operacion, moneda, todos, tipoConsulta, numeroRegistros).ToList();
        }

        public IList<sce_dev_cons_Result_r> Sce_Dev_Cons_MS_r(decimal periodo, string rut, string operacion, decimal moneda, string todos, decimal tipoConsulta, int numeroRegistros)
        {
            //return context.sce_dev_cons(periodo, rut, operacion, moneda, todos, tipoConsulta, numeroRegistros).ToList();
            return EjecutarSP<sce_dev_cons_Result_r>("sce_dev_cons_MS", periodo.ToString(), rut, operacion, moneda.ToString(), todos, tipoConsulta.ToString(), numeroRegistros.ToString());
        }

        public IList<decimal> pro_sce_datos_cuadratura_s01_MS(int tipoConsulta)
        {
            return context.pro_sce_datos_cuadratura_s01_MS(tipoConsulta).Select(x => x.Value).ToList();
        }

        public IList<sce_cctx_s01_MS_Result> sce_cctx_s01_MS()
        {
            return context.sce_cctx_s01_MS().ToList();
        }

        public int scejacp_s07_MS(string codcct, string codesp, DateTime fecpro)
        {
            int? r = context.scejacp_s07_MS(codcct, codesp, fecpro).FirstOrDefault();
            return (r ?? 0);
        }

        public IList<scejacp_s05_MS_Result> scejacp_s05_MS(string codcct, string codesp, DateTime fecpro)
        {
            return context.scejacp_s05_MS(codcct, codesp, fecpro).ToList();
        }

        public IList<sce_mcd_s05_MS_Result> Sce_mcd_s05_MS(string cencos, string codusr, DateTime fecMov, int jerarquia)
        {
            var retorno = context.sce_mcd_s05_MS(cencos, codusr, fecMov, jerarquia).ToList();

            return retorno;
        }

        public IList<sce_mcd_s28_MS_Result> sce_mcd_s28_MS(string codcct, string codesp, DateTime fecpro)
        {
            return context.sce_mcd_s28_MS(codcct, codesp, fecpro).ToList();
        }

        public IList<sce_mcd_s06_MS_Result> sce_mcd_s06_MS(string codcct, string codesp, DateTime fecpro)
        {
            return context.sce_mcd_s06_MS(codcct, codesp, fecpro).ToList();
        }

        public IList<sce_mcd_s61_MS_Result> sce_mcd_s61_MS(string codcct, string codesp, DateTime fecpro)
        {
            return context.sce_mcd_s61_MS(codcct, codesp, fecpro).ToList();
        }

        public IList<sce_mcd_s40_MS_Result> sce_mcd_s40_MS(string codcct, string codesp, DateTime fecpro)
        {
            return context.sce_mcd_s40_MS(codcct, codesp, fecpro).ToList();
        }

        public IList<sce_mch_s12_MS_Result> sce_mch_s12_MS(string codcct, string codesp)
        {
            return context.sce_mch_s12_MS(codcct, codesp).ToList();
        }

        public List<sce_cuadra_inyecciones_ctacte_MS_Result> Sce_cuadra_inyecciones_ctacte_MS(string cencos, string codusr, DateTime fecmov)
        {
            var result = context.sce_cuadra_inyecciones_ctacte_MS(cencos, codusr, fecmov.Date).ToList();

            return result;
        }


        public IList<sce_mch_s14_MS_Result> sce_mch_s14_MS(string cenCos, string codUsr)
        {
            var result = context.sce_mch_s14_MS(cenCos, codUsr).ToList();
            return result;
        }

        public IList<sce_gpln_s15_MS_Result> sce_gpln_s15_MS(string cenCos, string codUsr, DateTime fecmov)
        {
            return context.sce_gpln_s15_MS(cenCos, codUsr, fecmov).ToList();
        }

        public IList<sce_mcd_s70_MS_Result> Sce_mcd_s70(string cencos, string codusr)
        {
            var result = context.sce_mcd_s70_MS(cencos, codusr).ToList();
            return result;
        }

        public IList<sce_cdev_s01_MS_Result> sce_cdev_s01(int periodo, string rut, string operacion, int moneda, string todos)
        {
            var result = context.sce_cdev_s01_MS(periodo, rut, operacion, moneda, todos).ToList();
            return result;
        }

        public IList<decimal?> pro_sce_cdev_s02()
        {
            var result = context.pro_sce_cdev_s02_MS().ToList();
            return result;
        }

        public IList<sce_mts_s01_MS_Result> sce_mts_s01_1_MS(string Codcct, string Codpro, string Codesp, string Codofi, string Codope, Nullable<decimal> numneg, Nullable<decimal> tippro, Nullable<decimal> numcpa, Nullable<decimal> numcuo, Nullable<decimal> numcob, Nullable<decimal> estado, string tipgra)
        {
            return context.sce_mts_s01_MS(Codcct, Codpro, Codesp, Codofi, Codope, numneg, tippro, numcpa, numcuo, numcob, estado, tipgra).ToList();
        }

        public sce_mch_u01_MS_Result sce_mch_u01_MS(Nullable<decimal> nrorpt, Nullable<System.DateTime> fecmov)
        {
            var result = new List<sce_mch_u01_MS_Result>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    var item = new sce_mch_u01_MS_Result
                    {
                        Column1 = reader.GetInt32(0),
                        Column2 = Utils.GetStringFromDataReader(reader, 1)
                    };
                    result.Add(item);
                }
            }, "sce_mch_u01_MS", nrorpt.ToString(), fecmov.ToString());


            return result.FirstOrDefault();
        }
        public IList<sce_vgt_s02_s21_MS_Result> sce_vgt_s02_s21(string tipope, string indcdr, string Num_me, string dig_me, string rut, string digcli, string saldo, string codcct, string codpro, string codesp, string codofi, string codope)
        {//@ls_retorno  char(3), @ls_mensaje
            var retorno = new ObjectParameter("ls_retorno", "");
            var mensaje = new ObjectParameter("ls_mensaje", "");
            var result = context.sce_vgt_s02_s21_MS(tipope, indcdr, Num_me, dig_me, rut, digcli, codcct, codpro, codesp, codofi, codesp, saldo, retorno, mensaje).ToList();

            return result;
        }


        public IList<DocumentoOperacion> sce_mchh_s01_MS(DateTime fechaMov)
        {
            List<DocumentoOperacion> result = new List<DocumentoOperacion>();
            ReadQuerySP((reader) =>
            {
                while (reader.Read())
                {
                    int c = 0;
                    result.Add(new DocumentoOperacion
                    {
                        CodCct = reader.GetString(c++),
                        CodPro = reader.GetString(c++),
                        CodEsp = reader.GetString(c++),
                        CodOfi = reader.GetString(c++),
                        CodOpe = reader.GetString(c++),
                        NroRpt = (int)reader.GetDecimal(c++),
                        FechaOperacion = reader.GetDateTime(c++),
                        CodigoPropio = reader.GetValue(c++).ToString(),
                        DescripcionProducto = reader.GetString(c++),
                        TipoDoc = DocumentoOperacion.TipoDocEnum.Contabilidad,
                        TipoSwift = 0
                    });
                }
            }, "sce_mchh_s01_MS", fechaMov.ToString());

            return result;

        }


        public IList<string> pro_sce_int_cdr_S01_MS(int tipoConsulta)
        {
            var result = context.pro_sce_int_cdr_S01_MS(tipoConsulta).ToList();
            return result;
        }

        public IList<int?> pro_sce_int_cdr_S02_MS(int tipoConsulta, int anno, int mes)
        {
            var result = context.pro_sce_int_cdr_s02_MS(tipoConsulta, anno, mes).ToList();
            return result;
        }

        public IList<sce_dev_cdr_MS_Result> sce_dev_cdr(int annoMes, string fechaInicio, string fechaFin, string rut, string operacion, string moneda, string plazoTO, string todos, int tipoConsulta, int numeroRegistro, string tipoRegistro)
        {
            var result = context.sce_dev_cdr_MS(annoMes, fechaInicio, fechaFin, rut, operacion, moneda, plazoTO, todos, tipoConsulta, numeroRegistro, tipoRegistro).ToList();
            return result;
        }

        public IList<sce_dev_cdr_MS_Result_Devengo> sce_dev_cdr_Devengo(int annoMes, string fechaInicio, string fechaFin, string rut, string operacion, string moneda, string plazoTO, string todos, int tipoConsulta, int numeroRegistro, string tipoRegistro)
        {
            var result = EjecutarSP<sce_dev_cdr_MS_Result_Devengo>("sce_dev_cdr_MS", annoMes.ToString(), fechaInicio, fechaFin, rut, operacion, moneda, plazoTO, todos, tipoConsulta.ToString(), numeroRegistro.ToString(), tipoRegistro);
            return result;
        }

        public IList<sce_mcdh_s01_MS_Result> sce_mcdh_s01(string nemcta, string numcta, string numctacte, string cencos, DateTime fecini, DateTime fecfin, string todos)
        {
            var result = context.sce_mcdh_s01_MS(nemcta, numcta, numctacte, cencos, fecini, fecfin, todos).ToList();
            return result;
        }

        public void pro_sce_swf_pendientes_i01_MS(string ctecct, string codesp, string archivo, string rutAis, string sistema, DateTime fecha, string tipo, string moneda, decimal monto, string referencia, string mjsSwift, bool esPlantilla)
        {
            context.pro_sce_swf_pendientes_i01_MS(ctecct, codesp, archivo, rutAis, sistema, fecha, tipo, moneda, monto, referencia, mjsSwift, esPlantilla);
        }

        public void pro_sce_swf_pendiente_d01_MS(string ctecct, string codesp, string archivo)
        {
            context.pro_sce_swf_pendientes_d01_MS(ctecct, codesp, archivo);
        }

        public IList<pro_sce_swf_pendientes_s01_MS_Result> pro_sce_swf_pendientes_s01_MS(string ctecct, string codesp)
        {
            var result = context.pro_sce_swf_pendientes_s01_MS(ctecct, codesp).ToList();
            return result;
        }


        public pro_sce_swf_pendientes_s02_MS_Result pro_sce_swf_pendientes_s02_MS(string ctecct, string codesp, string archivo)
        {
            var result = context.pro_sce_swf_pendientes_s02_MS(ctecct, codesp, archivo).FirstOrDefault();
            return result;
        }

        public void pro_sce_rng_u01_MS(string codcct, string codesp, string codfun, int numactual)
        {
            context.proc_sce_rng_u01_MS(codcct, codfun, codesp, numactual);
        }

        public int? ejc_prty_ejc_i_01_MS(string PrtyRut, decimal Ejcofi, decimal EjcCod, string EjcTpo, DateTime FechaCreate, DateTime FechaMod)
        {
            try
            {
                context.ejc_prty_ejc_i_01_MS(PrtyRut, Ejcofi, EjcCod, EjcTpo, FechaCreate, FechaMod).FirstOrDefault();
                return 0;
            }
            catch
            {
                return -1;
            }

        }
        public virtual IList<ejc_prty_ejc_s_01_MS_Result> ejc_prty_ejc_s_01_MS(string prty_rut)
        {
            var result = context.ejc_prty_ejc_s_01_MS(prty_rut).ToList();
            return result;
        }

        public virtual List<sce_usr_s26_MS_Result> sce_usr_s26_MS(string rut_ejec)
        {
            var result = context.sce_usr_s26_MS(rut_ejec).ToList();
            return result;
        }

        public int sce_rng_ui01_MS(string cct, string esp, string doc, string rut, float inf, float sup, float act)
        {
            int result = -1;
            try
            {
                result = this.EjecutarSPConRetornoSinTransaccion("sce_rng_ui01_MS", string.Empty, new List<string> {cct, esp, doc, rut, inf.ToString(), sup.ToString(),
                act.ToString()});
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public int Pro_Sce_Aprty_S01(string cuenta, int flag)
        {
            return context.pro_sce_aprty_s01_MS(cuenta, flag).FirstOrDefault().GetValueOrDefault();
        }

        //Dada una operacion, devuelve las lineas que ya han sido inyectadas
        public List<sce_mcd_s76_MS_Result> sce_mcd_s76_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_mcd_s76_MS(codcct, codpro, codesp, codofi, codope).ToList();
        }

        //dado el numero de operacion retorma el numero de reporte y el estado de la operacion desde sce_mch
        public sce_mch_s15_MS_Result sce_mch_s15_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.sce_mch_s15_MS(codcct, codpro, codesp, codofi, codope).FirstOrDefault();
        }

        /// <summary>
        /// Obtiene el resgitro de la contabilidad filtrado por numero de operación y número de reporte
        /// </summary>
        /// <param name="codcct">Código de centro de costo</param>
        /// <param name="codpro">Código del producto</param>
        /// <param name="codesp">Código del especialista</param>
        /// <param name="codofi">Código de la oficina</param>
        /// <param name="codope">Código de la operación</param>
        /// <param name="numRep">Número de reporte</param>
        /// <returns></returns>
        public sce_mch_s15_MS_Result GetContabilidadPorNroReporte(string codcct, string codpro, string codesp, string codofi, string codope, decimal numRep)
        {
            var contabilidades = context.sce_mch_s15_MS(codcct, codpro, codesp, codofi, codope).ToList();
            return contabilidades.Where(c => c.nrorpt == numRep).FirstOrDefault();
        }

        public proc_get_cantidad_inyecciones_pendientes_MS_Result get_cantidad_inyecciones_pendientes(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            var result = context.proc_get_cantidad_inyecciones_pendientes_MS(codcct, codpro, codesp, codofi, codope).FirstOrDefault();
            return result;
        }

        public string proc_sel_montoOperacionAnulacionDia_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return context.proc_sel_montoOperacionAnulacionDia_MS(codcct, codpro, codesp, codofi, codope).SingleOrDefault();
        }

        /// <summary>
        /// A traves del número de operación, validamos si la operación cuenta con alguna inyeccion en las cuentas contables
        /// </summary>
        /// <param name="codcct">Número Centro de Costo</param>
        /// <param name="codpro">Número Producto</param>
        /// <param name="codesp">Número Especialista</param>
        /// <param name="codofi">Número Oficina</param>
        /// <param name="codope">Número Operación</param>
        /// <returns></returns>
        public bool proc_sel_validaOperacionSiTieneInyeccion_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return (bool)context.proc_sel_validaOperacionSiTieneInyeccion_MS(codcct, codpro, codesp, codofi, codope).FirstOrDefault();
        }
        /// <summary>
        /// Retorna el estado del cargo/abono. En linea = 1 = inyectado. En linea = 0 = no inyectado/reversado. 
        /// </summary>
        /// <param name="codcct"></param>
        /// <param name="codpro"></param>
        /// <param name="codesp"></param>
        /// <param name="codofi"></param>
        /// <param name="codope"></param>
        /// <param name="nemcta"></param>
        /// <param name="nrorpt"></param>
        /// <param name="nroimp"></param>
        /// <returns></returns>
        public sce_mcd_s77_MS_Result sce_mcd_s77_MS(string codcct, string codpro, string codesp, string codofi, string codope, string nemcta, decimal? nrorpt, decimal? nroimp)
        {
            return context.sce_mcd_s77_MS(codcct, codpro, codesp, codofi, codope, nemcta, nrorpt, nroimp).FirstOrDefault();
        }

        /// <summary>
        /// Según numcta retorna 1 en caso de ser una obligacion 0 en caso de no serlo
        /// </summary>
        /// <param name="numcta"></param>
        /// <returns></returns>
        public bool EsObligacion(int numcta)
        {
            return context.sce_ovd_s02_MS(numcta).FirstOrDefault() ?? false;
        }

        public tbl_sce_tabcomex_num_s01_MS_Result GetRangosCtaCorrienteObligaciones()
        {
            return context.tbl_sce_tabcomex_num_s01_MS().FirstOrDefault();
        }

        public proc_UpdateCargoYAbono_MS_Result GetUpdateCargoYAbono(string centroCosto, string producto, string especialista, string oficina, string operacion, string numeroImpresion, string transaccionID, string numeroReporte)
        {
            return context.proc_UpdateCargoYAbono_MS(centroCosto, producto, especialista, oficina, operacion, numeroImpresion, transaccionID, numeroReporte).FirstOrDefault();
        }

        public int Sce_rsa_du01_MS(string idparty, string razonSocial, string nombreFantasia, string contacto, string sortKey, string creaCosto, string creaUser)
        {
            return context.sce_rsa_du01_MS(idparty, razonSocial, nombreFantasia, contacto, sortKey, creaCosto, creaUser).FirstOrDefault();
        }

        // INICIO MODIFICACION CNC - ACCENTURE //
        public string sce_cla_cliente_w01(string id_party,
                                      string clasif_riesgo,
                                      string act_econ,
                                      string eval_riesgo,
                                      string compos_inst,
                                      string tpo_clte_norm)
        {

            string retSp = "Error Grabar Clasificacion Cliente: " + id_party;

            try
            {
                var res = context.sce_cla_cliente_w01(id_party.ToUpper(), clasif_riesgo, act_econ, eval_riesgo, compos_inst, tpo_clte_norm).ToList();
                if (res != null)
                {
                    if (res[0] != null)
                    {
                        if (res[0].Column1 == 0)
                        {
                            retSp = "OK";
                        }

                        else
                        {
                            retSp = res[0].Column2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retSp = retSp + " " + ex.Message;
            }

            return retSp;
        }

        public IList<sce_campos_cla_cliente_S01_Result> sce_campos_cla_cliente_S01(byte id_campo, string cod_campo, string des_campo)
        {
            return context.sce_campos_cla_cliente_S01(id_campo, cod_campo, des_campo).ToList();
        }

        // FIN MODIFICACION CNC - ACCENTURE //

        /// <summary>
        /// Se incorpora para proyecto generacion MT-300
        /// </summary>
        public int mt300_ins_registro_procesado(ref string retorno, ArchivoDetalle registro, string beneficiary)
        {
            ObjectParameter par1 = new ObjectParameter("retorno", typeof(String));
            int cant = context.sce_mt300_insertar_reg_procesado(Convert.ToDecimal(registro.id_archivo_detalle), Convert.ToDecimal(registro.id_swift), registro.reference, registro.amount_mn, registro.amount_me, beneficiary, registro.safekeeping, registro.value_date, registro.rate, registro.booked_by, registro.estado, registro.mensaje_mt, registro.codigo_moneda_mn, registro.codigo_moneda_me, registro.campo22A, registro.campo22C, registro.campo82A, registro.campo87A, registro.campo53A, registro.campo57A, registro.flag_ingresado_nuevo, registro.campo98D, par1);
            retorno = (string)par1.Value;
            return cant;
        }

        public int mt300_upd_registro_procesado(ref string retorno, ArchivoDetalle registro)
        {
            ObjectParameter par1 = new ObjectParameter("retorno", typeof(String));
            int cant = context.sce_mt300_update_reg_procesado(Convert.ToDecimal(registro.id_procesados), Convert.ToDecimal(registro.id_swift), registro.reference, registro.amount_mn, registro.amount_me, registro.beneficiary, registro.safekeeping, registro.value_date, registro.rate, registro.booked_by, registro.estado, registro.mensaje_mt, registro.codigo_moneda_mn, registro.codigo_moneda_me, registro.campo22A, registro.campo22C, registro.campo82A, registro.campo87A, registro.campo53A, registro.campo57A, registro.campo98D, par1);
            retorno = (string)par1.Value;
            return cant;
        }
    }
}
