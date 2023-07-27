using BCH.Comex.Common.Tracing;
using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Data.DAL.Cext01;
using CodeArchitects.VB6Library;
using System;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public static class MODXDOC0
    {
        //Graba un Documento de Cobranza Exportación.
        //Retorno    <> 0 : Correlativo de la Carta.
        //           =  0 : Error o Grabación no Exitosa.
        public static short SyPut_xDoc(DatosGlobales Globales, UnitOfWorkCext01 unit, string NumOpe, int CodDoc, string Memo, string Usuario)
        {
            using(var tracer = new Tracer("SyPut_xDoc"))
            {
                short _retValue = 0;
                decimal? c = 0;
                int m = 0;
                try
                {
                    // IGNORED: On Error GoTo SyPut_xDocErr
                    c = unit.SceRepository.EjecutarSP<decimal?>("sce_xdoc_s02_MS", VB6Helpers.Mid(NumOpe, 1, 3), VB6Helpers.Mid(NumOpe, 4, 2), VB6Helpers.Mid(NumOpe, 6, 2), VB6Helpers.Mid(NumOpe, 8, 3), VB6Helpers.Mid(NumOpe, 11, 5)).First();
                    //Obtiene el Correlativo para el documento.
                    if (c == null)
                    {
                        c = 1;
                    }
                    else
                    {
                        c = (c.Value + 1);
                    }

                    //Se ejecuta el Procedimiento Almacenado.

                    //VB6Helpers.MsgBox("Se ha producido un error al tratar de leer el correlativo del Documento (Sce_xDoc). El Servidor reporta : [" + VB6Helpers.Left(VB6Helpers.Trim(Mdl_SRM.ParamSrm8k.mensaje.Value), 100) + "]. Reporte este problema.", MsgBoxStyle.Information, "Validación de Datos");


                    //Resultado nulo de la Consulta.

                    //Para un memo.
                    m = MODGMEM.SyPutn_Mem(Globales, unit, "x", 0, Memo);
                    if (m == 0)
                    {
                        tracer.AddToContext("SyPutn_Mem", "Error al Grabar el Memo");
                        return 0;
                    }
                    int resOp = -1;
                    unit.SceRepository.ReadQuerySP((reader) =>
                    {
                        if (reader.Read())
                        {
                            resOp = reader.GetInt32(0);
                        }
                        else
                        {
                            resOp = -1;
                        }
                    }, "sce_xdoc_i01_MS", VB6Helpers.Mid(NumOpe, 1, 3), VB6Helpers.Mid(NumOpe, 4, 2), VB6Helpers.Mid(NumOpe, 6, 2), VB6Helpers.Mid(NumOpe, 8, 3), VB6Helpers.Mid(NumOpe, 11, 5), c.ToString(), VB6Helpers.Left(Usuario, 3), VB6Helpers.Right(Usuario, 2), CodDoc.ToString(), DateTime.Now.ToString("yyyy-MM-dd"), m.ToString());
                    //int rowsAffected = unit.SceRepository.ExecuteNonQuerySP("sce_xdoc_i01_MS", VB6Helpers.Mid(NumOpe, 1, 3), VB6Helpers.Mid(NumOpe, 4, 2), VB6Helpers.Mid(NumOpe, 6, 2), VB6Helpers.Mid(NumOpe, 8, 3), VB6Helpers.Mid(NumOpe, 11, 5), c.ToString(), VB6Helpers.Left(Usuario, 3), VB6Helpers.Right(Usuario, 2), CodDoc.ToString(), DateTime.Now.ToString("yyyy-MM-dd"), m.ToString());
                    //Hace un Put en Sce_xDoc.
                    if (resOp != 0)
                    {
                        tracer.AddToContext("sce_xdoc_i01_MS", "Se ha producido un error al tratar de grabar un Conocimiento de Embarque");
                        Globales.MESSAGES.Add(new UI_Message()
                        {
                            Type = TipoMensaje.Error,
                            Text = "Se ha producido un error al tratar de grabar un Conocimiento de Embarque (Sce_xCem)."
                        });
                        return 0;
                    }
                    //Se ejecuta el Procedimiento Almacenado.
                    _retValue = (short)(c.Value);
                }
                catch (Exception _ex)
                {
                    tracer.AddToContext("Excepcion", "Se ha producido un error al tratar de grabar un Conocimiento de Embarque");
                    Globales.MESSAGES.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de grabar un Conocimiento de Embarque (Sce_xCem)."
                    });
                    _retValue = 0;
                }
                return _retValue;
            }
        }
    }
}
