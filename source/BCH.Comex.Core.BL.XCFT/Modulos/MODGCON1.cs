using BCH.Comex.Core.Entities.Cext01.FundTransfer.DataTypes;
using CodeArchitects.VB6Library;
using System;

namespace BCH.Comex.Core.BL.XCFT.Modulos
{
    public static class MODGCON1
    {
        
        //Carga e Imprime un Reporte Contable.-
        public static void Pr_Imprime_Contab80(InitializationObject initObject, int NroRpt, string FecMch, string filename=null)
        {
            initObject.DocumentosAImprimir.Add(new DataImpresion()
            {
                URL = "Impresion/ImpresionDeDocumentos/ReporteContable?nroReporte=" + NroRpt + "&fechaOp=" + FecMch + "&generarHtmlCompleto=true",
                nroReporte = NroRpt.ToString(),
                fechaOp = DateTime.Parse(FecMch),
                tipoDoc = 3, //conta
                fileName = filename ?? initObject.MODGCVD.VgCvd.OpeSin
            });
            //TODO: GRABAR : IMPRIMIR

            
        }

        //Retorna el Número de Puntos de un String.-
        public static short Puntos(string Numero_Str)
        {
            short Contador = 0;
            short i = 0;
            for (i = 1; i <= (short)VB6Helpers.Len(Numero_Str); i++)
            {
                if (VB6Helpers.Mid(Numero_Str, i, 1) == ".")
                {
                    Contador = (short)(Contador + 1);
                }

            }

            return Contador;
        }
    }
}
