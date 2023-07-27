using BCH.Comex.Common.Tracing;
using System;
using System.Text.RegularExpressions;

namespace BCH.Comex.UI.Web.Common
{
    public static class FormatUtils
    {
        public static string PrioridadDesc(string codigoPrioridad, string codigoTipoMsg = null)
        {
            if (String.IsNullOrEmpty(codigoPrioridad) && codigoTipoMsg != null && codigoTipoMsg == "MT0")
            {
                return "S";
            }
            else
            {
                switch (codigoPrioridad)
                {
                    case "N":
                        return "Normal";
                    case "U":
                        return "Urgente";
                    case "S":
                        return "System";
                    default:
                        return "Normal";
                }
            }
        }

        public static int RutEnFormatoBDSwift(string rut)
        {
            rut = rut.Replace(".", "");
            rut = rut.Replace("-", "");

            if (!String.IsNullOrEmpty(rut))
            {
                if (rut.Length > 8)
                {
                    return int.Parse(rut.Substring(0, rut.Length - 1)); //dejo afuera el digito verificador
                }
                else
                {
                    return int.Parse(rut);
                }
            }
            else return 0;
        }

        // <summary>
        /// Formatea el rut, eliminando puntos, guiones, coloca la K mayuscula y ceros a la izquierda en caso de guardado.
        /// Ejemplo: 93.129.000-2 => 931290002
        /// </summary>
        /// <param name="rut">Rut ingresado</param>
        /// <param name="cerosIzquierda">True por default, en el caso del guardado se requiere que el idparty contenga ceros a la izquierda</param>
        /// <returns>Rut formateado o vacío en caso de que no se envíe</returns>
        public static string TryFormatearRutParticipante(string rut, bool cerosIzquierda = true)
        {
            using (var tracer = new Tracer("FormatearRutParticipante - FormatUtils"))
            {
                tracer.AddToContext("Rut ingresado: ", rut);

                if (!string.IsNullOrWhiteSpace(rut))
                {
                    Regex rgx = new Regex("(-|\\.)");
                    rut = rgx.Replace(rut, ""); // Elimina puntos y guión,
                    rut = rut.ToUpper() // Deja en mayúsculas la K,
                             .Trim();    // Limpia espacios en blanco.

                    if (cerosIzquierda)
                    {
                        rut = rut.PadLeft(10, '0'); // Le pone los ceros a la izquierda
                    }
                }
                else
                {
                    rut = string.Empty;
                }

                tracer.AddToContext("Rut devuelto: ", rut);
                return rut;
            }
        }
    }
}