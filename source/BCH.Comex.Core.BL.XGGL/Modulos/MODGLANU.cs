using BCH.Comex.Common.UI_Modulos;
using BCH.Comex.Core.Entities.Cext01.ContabilidadGenerica;
using BCH.Comex.Data.DAL.Cext01;
using System;
using BCH.Comex.Common.Tracing;
using System.Linq;

namespace BCH.Comex.Core.BL.XGGL.Modulos
{
    public class MODGLANU
    {
        /// <summary>
        /// 1.  Cambia el valor del campo estado en las Tablas Sce_Mcd - Sce_Mch.
        /// </summary>
        /// <param name="NroRpt"></param>
        /// <param name="FecMov"></param>
        /// <returns></returns>
        public static bool SyPute_Gl(int NroRpt, string FecMov, DatosGlobales Globales, UnitOfWorkCext01 uow)
        {
            using (Tracer tracer = new Tracer("Anular_Contabilidad_Generica - SyPute_Gl"))
            {
                try
                {
                    var result = uow.SceRepository.sce_mch_u01_MS(NroRpt, DateTime.Parse(FecMov));
                    if (result.Column1 == 0)
                    {
                        return true;
                    }
                }
                catch (Exception exc)
                {
                    Globales.FrmGlanu.ListaErrores.Add(new UI_Message()
                    {
                        Type = TipoMensaje.Error,
                        Text = "Se ha producido un error al tratar de grabar los datos en las tablas Sce_Mcd - Sce_Mch.",
                        Title = "Anulación de Contabilidad Genérica"
                    });
                }
                return false;
            }
        }

        public static bool LineasYaInyectadasEnOperacion(UnitOfWorkCext01 uow, string codcct, string codpro, string codesp, string codofi, string codope )
        {
            return uow.SceRepository.sce_mcd_s76_MS(codcct, codpro, codesp, codofi, codope).Any();   
        }
    }
}
