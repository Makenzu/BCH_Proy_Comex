using BCH.Comex.Common.ExceptionHandling;
using BCH.Comex.Core.Entities.Cext01;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace BCH.Comex.Data.DAL.Cext01
{
    public class DocumentoOperacionRepository
    {
        cext01Entities context;
        

        public DocumentoOperacionRepository(cext01Entities aContext)
        {
            context = aContext;
        }
        
        public IList<DocumentoOperacion> Sce_Mch_s02_MS(DateTime fechaMov, string centroCosto, string codUsr)
        {
            return EjecutarSP("sce_mch_s02_MS", fechaMov, centroCosto, codUsr, DocumentoOperacion.TipoDocEnum.Contabilidad);
        }

        public IList<DocumentoOperacion> Sce_Mch_s03_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return EjecutarSP("sce_mch_s03_MS", codcct, codpro, codesp, codofi, codope, DocumentoOperacion.TipoDocEnum.Contabilidad);
        }

        public IList<DocumentoOperacion> Sce_XDoc_S03_MS(DateTime fechaIngreso, string centroCosto, string codUsr)
        {
            return EjecutarSP("sce_xdoc_s03_MS", fechaIngreso, centroCosto, codUsr, DocumentoOperacion.TipoDocEnum.Carta);
        }

        public IList<DocumentoOperacion> Sce_XDoc_S04_MS(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return EjecutarSP("sce_xdoc_s04_MS", codcct, codpro, codesp, codofi, codope, DocumentoOperacion.TipoDocEnum.Carta);
        }

        public IList<DocumentoOperacion> Sce_Swf_S03(DateTime fechaMov, string centroCosto, string codUsr)
        {
            return EjecutarSP("sce_swf_s03_MS", fechaMov, centroCosto, codUsr, DocumentoOperacion.TipoDocEnum.Swift);
        }

        public IList<DocumentoOperacion> Sce_Swf_S04(string codcct, string codpro, string codesp, string codofi, string codope)
        {
            return EjecutarSP("sce_swf_s04_MS", codcct, codpro, codesp, codofi, codope, DocumentoOperacion.TipoDocEnum.Swift);
        }

        public IList<DocumentoOperacion> Pro_sce_relacion_s01_MS(string contactReference)
        {
            var data = EjecutarSP("pro_sce_relacion_s01_MS", contactReference);
            var dataResult = (from c in data
                             group c by new
                             {
                                 c.CodCct,
                                 c.CodPro,
                                 c.CodEsp,
                                 c.CodOfi,
                                 c.CodOpe,
                             } into gcs
                             select new DocumentoOperacion()
                             {
                                 CodCct = gcs.Key.CodCct,
                                 CodPro = gcs.Key.CodPro,
                                 CodEsp = gcs.Key.CodEsp,
                                 CodOfi = gcs.Key.CodOfi,
                                 CodOpe = gcs.Key.CodOpe
                             }).ToList();

            return dataResult;
        }

        public IList<sce_mch_s01_MS_Result> sce_mch_s01_MS(int nroRpt, DateTime fechaOperacion)
        {
            return context.sce_mch_s01_MS(nroRpt, fechaOperacion).ToList();
        }

        private IList<DocumentoOperacion> EjecutarSP(string nombreSP, DateTime fechaMov, string centroCosto, string codUsr, DocumentoOperacion.TipoDocEnum tipoDoc)
        {
            List<DocumentoOperacion> resultado = new List<DocumentoOperacion>();

            try
            {
                DbCommand commandExecSP = context.Database.Connection.CreateCommand();
                commandExecSP.CommandText = "exec " + nombreSP + " @Fecha, @CentroCosto, @CodUsr";
                commandExecSP.Parameters.Add(new SqlParameter("@Fecha", fechaMov));
                commandExecSP.Parameters.Add(new SqlParameter("@CentroCosto", centroCosto));
                commandExecSP.Parameters.Add(new SqlParameter("@CodUsr", codUsr));

                context.Database.Connection.Open();

                DbDataReader reader = commandExecSP.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DocumentoOperacion doc = GetDocumentoOperacionFromDataReader(reader, tipoDoc);
                        resultado.Add(doc);
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

       private IList<DocumentoOperacion> EjecutarSP(string nombreSP, string codcct, string codpro, string codesp, string codofi, string codope, DocumentoOperacion.TipoDocEnum tipoDoc)
       {
           List<DocumentoOperacion> resultado = new List<DocumentoOperacion>();

           try
           {
               DbCommand commandExecSP = context.Database.Connection.CreateCommand();
               commandExecSP.CommandText = "exec " + nombreSP + "   @Codcct, @Codpro, @Codesp, @Codofi, @Codope";
               commandExecSP.Parameters.Add(new SqlParameter("@Codcct", codcct));
               commandExecSP.Parameters.Add(new SqlParameter("@Codpro", codpro));
               commandExecSP.Parameters.Add(new SqlParameter("@Codesp", codesp));
               commandExecSP.Parameters.Add(new SqlParameter("@Codofi", codofi));
               commandExecSP.Parameters.Add(new SqlParameter("@Codope", codope));

               context.Database.Connection.Open();
               DbDataReader reader = commandExecSP.ExecuteReader();

               if (reader.HasRows)
               {
                   while (reader.Read())
                   {
                       DocumentoOperacion doc = GetDocumentoOperacionFromDataReader(reader, tipoDoc);
                       resultado.Add(doc);
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

       private IList<DocumentoOperacion> EjecutarSP(string nombreSP, string contactReference)
       {               
           List<DocumentoOperacion> resultado = new List<DocumentoOperacion>();
           
           try
           {
               DbCommand commandExecSP = context.Database.Connection.CreateCommand();
               commandExecSP.CommandText = "exec " + nombreSP + " @ctf, @flag";
               commandExecSP.Parameters.Add(new SqlParameter("@ctf", contactReference));
               commandExecSP.Parameters.Add(new SqlParameter("@flag", 1)); //cualquier valor <> 0 devuelve lo que me interesa

               context.Database.Connection.Open();
               DbDataReader reader = commandExecSP.ExecuteReader();


               if (reader.HasRows)
               {
                   while (reader.Read())
                   {
                       DocumentoOperacion doc = GetDocumentoOperacionFromDataReader(reader, DocumentoOperacion.TipoDocEnum.SinEspecificar);
                       resultado.Add(doc);
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

       private DocumentoOperacion GetDocumentoOperacionFromDataReader(DbDataReader reader, DocumentoOperacion.TipoDocEnum tipoDoc)
        {
            int col = 0;
            DocumentoOperacion result = new DocumentoOperacion()
            {
                TipoDoc = tipoDoc,
                CodCct = Utils.GetStringFromDataReader(reader, col++),
                CodPro = Utils.GetStringFromDataReader(reader, col++),
                CodEsp = Utils.GetStringFromDataReader(reader, col++),
                CodOfi = Utils.GetStringFromDataReader(reader, col++),
                CodOpe = Utils.GetStringFromDataReader(reader, col++)
            };
            
            switch(tipoDoc)
            {
                case DocumentoOperacion.TipoDocEnum.Carta:
                case DocumentoOperacion.TipoDocEnum.Contabilidad:
                    result.NroRpt = (int)Utils.GetDecimalFromDataReader(reader, col++).Value;
                    result.FechaOperacion = Utils.GetFechaFromDataReader(reader, col++).Value;
                    decimal? codigo = Utils.GetDecimalFromDataReader(reader, col++);
                    if (codigo.HasValue)
                        result.CodigoPropio = codigo.ToString();  //en el caso de carta, es CodDoc, y en el caso de Contabilidad es CodFun, pero da lo mismo como se llame el campo, es la misma posicion 
                    break;

                case DocumentoOperacion.TipoDocEnum.Swift:
                    result.NroRpt = (int)Utils.GetDecimalFromDataReader(reader, col++).Value;
                    result.TipoSwift = (short)reader.GetDecimal(col++);
                    result.FechaOperacion = Utils.GetFechaFromDataReader(reader, col++).Value;
                    decimal? nroMensaje = Utils.GetDecimalFromDataReader(reader, col++);
                    if(nroMensaje.HasValue)
                    {
                        result.CodigoPropio = nroMensaje.ToString();
                    }
                    result.NroRpt = (int)Utils.GetDecimalFromDataReader(reader, col++).Value;
                    break;
            }
            
            return result;
        }








        
    }
}
