using System;
using System.Globalization;

namespace BCH.Comex.Utils
{
    public static class Format
    {
        private static CultureInfo cl = new CultureInfo("es-CL");
        private static CultureInfo us = new CultureInfo("en-US");

        


        public static string FormatCurrency(double number,string format)
        {
            return number.ToString(format, cl);
        }

        public static string FormatUSA(double number, string format)
        {
            return number.ToString(format, us);
        }

        public static double ParseDblFromDB(string number)
        {
            return double.Parse(number.Replace(".", ","));
        }

        public static string FormatDblFromDB(string number, string format)
        {
            return ParseDblFromDB(number).ToString(format);
        }

        public static double StringToDouble(string number)
        {
            try
            {
                number = number.Trim();
            }
            catch
            {
                number = "0";
            }
            double res = 0;
            bool parsed = double.TryParse(number, out res);
            if (parsed)
            {
                return res;
            }
            else
            {
                return 0;
            }
        }

        public static double StringToDouble(double number)
        {
            return number;
        }

        public static double StringToDouble(bool number)
        {
            return number ? -1 : 0;
        }

        public static decimal StringToDecimal(string number)
        {
            try
            {
                number = number.Trim();
            }
            catch
            {
                number = "0";
            }
            decimal res = 0;
            bool parsed = decimal.TryParse(number, out res);
            if (parsed)
            {
                return res;
            }
            else
            {
                return 0;
            }
        }

        public static decimal StringToDecimal(decimal number)
        {
            return number;
        }

        public static decimal StringToDecimal(bool number)
        {
            return number ? -1 : 0;
        }

        public static double StringToDouble(string number, int divisor = 1)
        {
            double resultValue = double.Parse(number);
            if (resultValue != 0)
            {
                resultValue = resultValue / divisor;
            }
            return resultValue;
        }

        public static object FormatCL(double v)
        {
            return v.ToString(cl);
        }
    }
}
