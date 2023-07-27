using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BCH.Comex.Core.BL.SWSE
{
    public static class Utilidad
    {
        public static string SanitizarString(string input)
        {
            // return Regex.Replace(input, @"[^\w\s.!@$%^&*()\-\/]+", "");
            return input;
        }

        public static string customMessageError(string mod)
        {
            return string.Format("Se ha producido un error al ejecutar la acción {0}", mod);
        }
    }
}
