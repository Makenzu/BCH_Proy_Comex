using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BCH.Comex.Core.BL.XGPL
{
    public class CommaSeparatedCompactableArray
    {
        List<string> items;
        public CommaSeparatedCompactableArray()
            : this("")
        {

        }

        public CommaSeparatedCompactableArray(string commaSeparatedString)
        {
            if (!string.IsNullOrWhiteSpace(commaSeparatedString))
            {
                items = commaSeparatedString.Split(';').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            }
            else
            {
                items = new List<string>(5);
            }
        }

        public string Value
        {
            get
            {
                return string.Join(";", items.Where(s => !string.IsNullOrWhiteSpace(s)));
            }
        }

        public string this[int index]
        {
            get
            {
                if (index >= 0 && index < items.Count)
                {
                    return items[index];
                }
                return "";
            }
            set
            {
                if (index > items.Count)
                {
                    if (items.Count == items.Capacity)
                    {
                        items.Capacity = items.Capacity + 5;
                    }
                    items.Add(value);
                }
                else
                {
                    items[index] = value;
                }
            }
        }

        public int Length
        {
            get
            {
                if (items != null)
                {
                    return items.Count;
                }
                return 0;
            }
        }
    }


    public static class XGPLUtilitiesExtension
    {
        public static CultureInfo culture = null;
        public static DateTimeFormatInfo dtFormat = null;
        public static string Mid(this string str, int start, int count)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            if (start + count - 1 > str.Length)
            {
                count = str.Length - start - 1;
            }
            return str.Substring(start - 1, count);
        }

        private static CultureInfo GetCulture()
        {
            if (culture == null)
            {
                culture = CultureInfo.CreateSpecificCulture("es-CL");
                culture.DateTimeFormat.DateSeparator = "/";
            }
            return culture;
        }

        private static DateTimeFormatInfo GetDateTimeFormat()
        {
            if (dtFormat == null)
            {
                dtFormat = GetCulture().DateTimeFormat;
            }
            return dtFormat;
        }

        public static string FormatoDecimal(this decimal? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString("#,##0.00", GetCulture());
            }
            return "";
        }
        public static string FormatoDecimal(this decimal value)
        {
            return value.ToString("#,##0.00", GetCulture());
        }

        public static string FormatoFecha(this System.DateTime value)
        {
            return value.ToString("dd/MM/yyyy", GetDateTimeFormat());
        }

        public static string FormatoRut(this string value)
        {
            var tmp = value.Trim();
            return tmp.Mid(1, tmp.Length - 1) + "-" + tmp.Mid(tmp.Length, 1);
        }

        public static string FormatoNroPresentacion(this string value)
        {
            var tmp = value.Trim();
            return tmp.Mid(1, tmp.Length - 1) + "-" + tmp.Mid(tmp.Length, 1);
        }
    }
}
